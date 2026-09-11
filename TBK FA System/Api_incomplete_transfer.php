<?php
defined('BASEPATH') OR exit('No direct script access allowed');

/**
 * Option A incomplete-box transfer ownership controller.
 *
 * This controller deliberately creates only ACTIVE history_box_log records.
 * It never changes tag_print_detail, production actual, Good, OEE, or tags.
 */
class Api_incomplete_transfer extends CI_Controller
{
    /** @var CI_DB_mssql_driver */
    private $db_fa;

    public function __construct()
    {
        parent::__construct();
        $this->db_fa = $this->load->database('tbkkfa01_db', true);
        $this->db_fa->db_debug = false;
    }

    public function index()
    {
        $this->json_response(array(
            'success' => true,
            'service' => 'Incomplete Transfer API'
        ));
    }

    /** Read-only database and required-table check. */
    public function health()
    {
        try {
            $dbCheck = $this->db_fa->query('SELECT 1 AS db_ok');
            if ($dbCheck === false) {
                throw new RuntimeException($this->db_error_message());
            }

            $tableCheck = $this->db_fa->query(
                "SELECT CASE WHEN OBJECT_ID('dbo.history_box_log', 'U') IS NULL THEN 0 ELSE 1 END AS history_table_ok"
            );
            if ($tableCheck === false) {
                throw new RuntimeException($this->db_error_message());
            }

            $row = $tableCheck->row_array();
            $this->json_response(array(
                'success' => true,
                'db_ok' => true,
                'history_table_ok' => isset($row['history_table_ok']) && (int)$row['history_table_ok'] === 1
            ));
        } catch (Throwable $e) {
            $this->json_response(array(
                'success' => false,
                'message' => 'Database health check failed',
                'error' => $this->safe_exception_message($e)
            ), 500);
        }
    }

    /** Read-only source tag parser diagnostic. */
    public function source_test()
    {
        $sourceTagId = $this->input->get('source_tag_id', true);
        if (!$this->is_positive_integer($sourceTagId)) {
            $this->json_response(array('success' => false, 'message' => 'source_tag_id is required'), 400);
            return;
        }

        try {
            $query = $this->db_fa->query(
                'SELECT id, wi, pwi_id, seq_no, box_no, qr_detail, flg_control '
                . 'FROM dbo.tag_print_detail WHERE id = ?',
                array((int)$sourceTagId)
            );
            if ($query === false) {
                throw new RuntimeException($this->db_error_message());
            }

            $row = $query->row_array();
            if (!$row) {
                $this->json_response(array('success' => false, 'message' => 'Source tag was not found'), 404);
                return;
            }

            $row['qty'] = $this->parse_normal_tag_quantity(isset($row['qr_detail']) ? $row['qr_detail'] : '');
            $this->json_response(array('success' => true, 'data' => $row));
        } catch (Throwable $e) {
            $this->json_response(array(
                'success' => false,
                'message' => 'Source tag lookup failed',
                'error' => $this->safe_exception_message($e)
            ), 500);
        }
    }

    /** Read-only active-transfer lookup for restart recovery. */
    public function active()
    {
        $currentPwi = trim((string)$this->input->get('current_pwi', true));
        $currentSeq = trim((string)$this->input->get('current_seq', true));
        if ($currentPwi === '' || $currentSeq === '') {
            $this->json_response(array('success' => false, 'message' => 'current_pwi and current_seq are required'), 400);
            return;
        }

        try {
            $query = $this->db_fa->query(
                'SELECT TOP 1 * FROM dbo.history_box_log '
                . 'WHERE hbl_current_pwi = ? AND TRY_CONVERT(INT, hbl_current_seq) = ? AND hbl_flag = 0 '
                . 'ORDER BY hbl_id DESC',
                array($currentPwi, (int)$currentSeq)
            );
            if ($query === false) {
                throw new RuntimeException($this->db_error_message());
            }

            $row = $query->row_array();
            if (!$row) {
                $this->json_response(array('success' => true, 'hasActive' => false));
                return;
            }

            $this->json_response(array('success' => true, 'hasActive' => true, 'data' => $row));
        } catch (Throwable $e) {
            $this->json_response(array(
                'success' => false,
                'message' => 'Active transfer lookup failed',
                'error' => $this->safe_exception_message($e)
            ), 500);
        }
    }

    /** Read-only active durable-transfer lookup for one authoritative line. */
    public function active_for_line()
    {
        $lineCd = trim((string)$this->input->get('line_cd', true));
        if ($lineCd === '') {
            $this->json_response(array('success' => false, 'message' => 'line_cd is required'), 400);
            return;
        }
        try {
            $query = $this->db_fa->query(
                'SELECT TOP 1 hbl.* FROM dbo.history_box_log hbl '
                . 'INNER JOIN dbo.production_working_info pwi ON pwi.pwi_id = hbl.hbl_current_pwi '
                . 'INNER JOIN dbo.sup_work_plan_supply_dev sw ON sw.IND_ROW = pwi.ind_row '
                . 'WHERE sw.LINE_CD = ? AND hbl.hbl_flag = 0 ORDER BY hbl.hbl_id DESC',
                array($lineCd)
            );
            if ($query === false) throw new RuntimeException($this->db_error_message());
            $row = $query->row_array();
            $this->json_response($row
                ? array('success' => true, 'hasActive' => true, 'data' => $row)
                : array('success' => true, 'hasActive' => false));
        } catch (Throwable $e) {
            $this->json_response(array('success' => false, 'message' => 'Line active transfer lookup failed',
                'error' => $this->safe_exception_message($e)), 500);
        }
    }

    /**
     * Releases an ACTIVE Continue selection only after the crash-recovery
     * caller has authoritatively established zero signed production for that
     * exact old session.  The original source tag remains untouched.
     *
     * hbl_flag = 3 means CRASH_RELEASED_ZERO_NET.  It is deliberately not a
     * normal cancel, partial close, or full completion state.
     */
    public function release_zero_net()
    {
        if (strtoupper($this->input->method(true)) !== 'POST') {
            $this->json_response(array('success' => false, 'message' => 'POST is required'), 405);
            return;
        }

        $request = $this->request_data();
        $hblId = $this->request_value($request, 'hbl_id', 'hblId');
        $sourceTagId = $this->request_value($request, 'source_tag_id', 'sourceTagId');
        $netQty = $this->request_value($request, 'net_qty', 'netQty');
        if (!$this->is_positive_integer($hblId) || !$this->is_positive_integer($sourceTagId)
            || !is_scalar($netQty) || !preg_match('/^[+-]?0+$/', trim((string)$netQty))) {
            $this->json_response(array('success' => false, 'message' => 'Exact zero-net release identity is required'), 400);
            return;
        }

        $hblId = (int)$hblId;
        $sourceTagId = (int)$sourceTagId;
        $this->db_fa->trans_begin();
        try {
            $history = $this->query_one_or_throw(
                'SELECT TOP 1 * FROM dbo.history_box_log WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE hbl_id = ?',
                array($hblId)
            );
            if (!$history) {
                throw new DomainException("History box log record {$hblId} not found");
            }
            if ((int)$history['hbl_source_tag_id'] !== $sourceTagId) {
                throw new DomainException('Source tag ID does not match the transfer');
            }

            $flag = (int)$history['hbl_flag'];
            if ($flag === 3) {
                $this->db_fa->trans_commit();
                $this->json_response(array(
                    'success' => true,
                    'alreadyReleased' => true,
                    'hbl_id' => $hblId,
                    'hbl_flag' => 3,
                    'message' => 'Transfer was already released after zero-net crash recovery'
                ));
                return;
            }
            if ($flag === 1 || $flag === 2) {
                throw new DomainException('Transfer is already closed and cannot be released');
            }
            if ($flag !== 0) {
                throw new DomainException('Transfer has an unsupported lifecycle state');
            }
            if (!empty($history['hbl_current_tag_id'])) {
                throw new DomainException('ACTIVE transfer already has a current tag reference and cannot be zero-net released');
            }

            $sourceTag = $this->query_one_or_throw(
                'SELECT id, flg_control FROM dbo.tag_print_detail WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE id = ?',
                array($sourceTagId)
            );
            if (!$sourceTag || trim((string)$sourceTag['flg_control']) !== '0') {
                throw new DomainException('Source tag is no longer pending and cannot be zero-net released');
            }

            $activeCount = (int)$this->query_scalar_or_throw(
                'SELECT COUNT(1) FROM dbo.history_box_log WITH (UPDLOCK, HOLDLOCK) '
                . 'WHERE hbl_source_tag_id = ? AND hbl_flag = 0',
                array($sourceTagId)
            );
            if ($activeCount !== 1) {
                throw new DomainException('Source ACTIVE ownership is ambiguous and cannot be released');
            }

            $released = $this->db_fa->query(
                'UPDATE dbo.history_box_log '
                . 'SET hbl_flag = 3, hbl_completed_at = GETDATE(), hbl_current_tag_id = NULL '
                . 'WHERE hbl_id = ? AND hbl_flag = 0',
                array($hblId)
            );
            if ($released !== true || $this->db_fa->affected_rows() !== 1) {
                throw new RuntimeException('Failed to release zero-net transfer');
            }

            $this->db_fa->trans_commit();
            $this->json_response(array(
                'success' => true,
                'alreadyReleased' => false,
                'hbl_id' => $hblId,
                'hbl_flag' => 3,
                'message' => 'Transfer released after zero-net crash recovery'
            ));
        } catch (Throwable $e) {
            $this->db_fa->trans_rollback();
            $this->json_response(array(
                'success' => false,
                'message' => $e instanceof DomainException ? $e->getMessage() : 'Unable to release zero-net transfer',
                'error' => $e instanceof DomainException ? null : $this->safe_exception_message($e)
            ), $e instanceof DomainException ? 409 : 500);
        }
    }

    /**
     * Read-only crash-recovery discovery for one physical line.  Each ACTIVE
     * history row carries only the signed server event summary for its own
     * durable Current WI/PWI/Seq identity; it never infers ownership from a
     * candidate tag.
     */
    public function recovery_active_for_line()
    {
        $lineCd = trim((string)$this->input->get('line_cd', true));
        if ($lineCd === '') {
            $this->json_response(array('success' => false, 'message' => 'line_cd is required'), 400);
            return;
        }

        try {
            $query = $this->db_fa->query(
                'SELECT hbl.*, pwi.pwi_lot_no AS recovery_lot, pwi.pwi_shift AS recovery_shift, t.created_date AS recovery_created_date '
                . 'FROM dbo.history_box_log hbl '
                . 'INNER JOIN dbo.production_working_info pwi ON pwi.pwi_id = hbl.hbl_current_pwi '
                . 'INNER JOIN dbo.sup_work_plan_supply_dev sw ON sw.IND_ROW = pwi.ind_row '
                . 'LEFT JOIN dbo.tag_print_detail t ON t.id = hbl.hbl_source_tag_id '
                . 'WHERE sw.LINE_CD = ? AND hbl.hbl_flag = 0 '
                . 'ORDER BY hbl.hbl_id ASC',
                array($lineCd)
            );
            if ($query === false) {
                throw new RuntimeException($this->db_error_message());
            }

            $this->json_response(array(
                'success' => true,
                'hasActive' => $query->num_rows() > 0,
                'data' => $query->result_array()
            ));
        } catch (Throwable $e) {
            $this->json_response(array(
                'success' => false,
                'message' => 'Crash-recovery active transfer lookup failed',
                'error' => $this->safe_exception_message($e)
            ), 500);
        }
    }

    /** Move only an existing ACTIVE crash-recovery anchor; no lifecycle change. */
    public function roll_forward_active()
    {
        if (strtoupper($this->input->method(true)) !== 'POST') {
            $this->json_response(array('success' => false, 'message' => 'POST is required'), 405);
            return;
        }
        $request = $this->request_data();
        $values = array();
        foreach (array('hbl_id', 'expected_current_pwi', 'expected_current_seq', 'new_base_qty',
                       'new_current_wi', 'new_current_pwi', 'new_current_seq') as $key) {
            if (!isset($request[$key]) || !is_scalar($request[$key]) || trim((string)$request[$key]) === '') {
                $this->json_response(array('success' => false, 'message' => $key . ' is required'), 400);
                return;
            }
            $values[$key] = trim((string)$request[$key]);
        }
        if (!$this->is_positive_integer($values['hbl_id']) ||
            !$this->is_positive_integer($values['expected_current_pwi']) ||
            !$this->is_positive_integer($values['new_current_pwi']) ||
            !preg_match('/^[0-9]+$/', $values['expected_current_seq']) ||
            !preg_match('/^[0-9]+$/', $values['new_current_seq']) ||
            !preg_match('/^[0-9]+$/', $values['new_base_qty']) ||
            (float)$values['new_base_qty'] > 2147483647) {
            $this->json_response(array('success' => false, 'message' => 'Invalid recovery anchor values'), 400);
            return;
        }
        try {
            if ($this->db_fa->trans_begin() === false) { throw new RuntimeException('Recovery transaction could not begin'); }
            $history = $this->query_one_or_throw(
                'SELECT * FROM dbo.history_box_log WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE hbl_id = ?',
                array($values['hbl_id'])
            );
            if (!$history || (int)$history['hbl_flag'] !== 0 ||
                trim((string)$history['hbl_current_pwi']) !== $values['expected_current_pwi'] ||
                trim((string)$history['hbl_current_seq']) !== $values['expected_current_seq']) {
                throw new DomainException('ACTIVE recovery anchor has changed');
            }
            if ($values['new_current_pwi'] === $values['expected_current_pwi'] &&
                (int)$values['new_current_seq'] === (int)$values['expected_current_seq']) {
                throw new DomainException('Recovery requires a new production anchor');
            }
            $current = $this->query_current_pwi_for_update($values['new_current_pwi']);
            if (!$current || trim((string)$current['current_wi']) !== $values['new_current_wi']) {
                throw new DomainException('New WI/PWI is not authoritative');
            }
            $updated = $this->db_fa->query(
                'UPDATE dbo.history_box_log SET hbl_base_qty = ?, hbl_current_wi = ?, hbl_current_pwi = ?, hbl_current_seq = ? '
                . 'WHERE hbl_id = ? AND hbl_flag = 0 AND hbl_current_pwi = ? AND hbl_current_seq = ?',
                array($values['new_base_qty'], $values['new_current_wi'], $values['new_current_pwi'],
                      $values['new_current_seq'], $values['hbl_id'], $values['expected_current_pwi'], $values['expected_current_seq'])
            );
            if ($updated === false) { throw new RuntimeException($this->db_error_message()); }
            $after = $this->query_one_or_throw('SELECT * FROM dbo.history_box_log WHERE hbl_id = ?', array($values['hbl_id']));
            if (!$after || (int)$after['hbl_flag'] !== 0 ||
                (int)$after['hbl_base_qty'] !== (int)$values['new_base_qty'] ||
                trim((string)$after['hbl_current_wi']) !== $values['new_current_wi'] ||
                trim((string)$after['hbl_current_pwi']) !== $values['new_current_pwi'] ||
                trim((string)$after['hbl_current_seq']) !== $values['new_current_seq'] ||
                $this->db_fa->trans_status() === false) {
                throw new RuntimeException('Recovery anchor readback failed');
            }
            if ($this->db_fa->trans_commit() === false) { throw new RuntimeException('Recovery anchor commit failed'); }
            $this->json_response(array('success' => true, 'hbl_id' => (int)$after['hbl_id'],
                'base_qty' => (int)$after['hbl_base_qty'], 'current_wi' => trim((string)$after['hbl_current_wi']),
                'current_pwi' => trim((string)$after['hbl_current_pwi']), 'current_seq' => trim((string)$after['hbl_current_seq']), 'flag' => 0));
        } catch (Throwable $e) {
            $this->db_fa->trans_rollback();
            $this->json_response(array('success' => false, 'message' => $this->safe_exception_message($e)), $e instanceof DomainException ? 409 : 500);
        }
    }

    /** Creates an ACTIVE ownership record only; existing normal Continue path. */
    public function start()
    {
        if (strtoupper($this->input->method(true)) !== 'POST') {
            $this->json_response(array('success' => false, 'message' => 'POST is required'), 405);
            return;
        }

        $request = $this->request_data();
        $sourceTagId = $this->request_value($request, 'source_tag_id', 'sourceTagId');
        $currentWi = trim((string)$this->request_value($request, 'current_wi', 'currentWi'));
        $currentPwi = trim((string)$this->request_value($request, 'current_pwi', 'currentPwi'));
        $currentSeq = trim((string)$this->request_value($request, 'current_seq', 'currentSeq'));
        $currentSnp = $this->request_value($request, 'current_snp', 'currentSnp');
        $goodSnapshot = $this->request_value($request, 'good_snapshot', 'goodSnapshot');

        if (!$this->is_positive_integer($sourceTagId) || $currentWi === '' || $currentPwi === '' || $currentSeq === ''
            || !is_numeric($currentSnp) || !is_numeric($goodSnapshot)) {
            $this->json_response(array('success' => false, 'message' => 'Missing or invalid required start fields'), 400);
            return;
        }

        $sourceTagId = (int)$sourceTagId;
        $currentSnp = (int)$currentSnp;
        $goodSnapshot = max(0, (int)$goodSnapshot);
        if ($currentSnp <= 1 || $currentSnp === 999999) {
            $this->json_response(array('success' => false, 'message' => 'Current SNP is invalid for Continue Existing Box'), 400);
            return;
        }

        $this->db_fa->trans_begin();
        try {
            // An ACTIVE row is the durable owner.  Check it before requiring
            // a pending source tag: a prior successful /start may legitimately
            // have changed that tag from pending while the client lost its
            // response.  The exact same current identity is idempotent; every
            // other identity remains an exclusive-source conflict.
            $activeSource = $this->query_one_or_throw(
                'SELECT TOP 1 * FROM dbo.history_box_log WITH (UPDLOCK, HOLDLOCK, ROWLOCK) '
                . 'WHERE hbl_source_tag_id = ? AND hbl_flag = 0 ORDER BY hbl_id DESC',
                array($sourceTagId)
            );
            if ($activeSource) {
                if (strcasecmp(trim((string)$activeSource['hbl_current_wi']), $currentWi) === 0
                    && trim((string)$activeSource['hbl_current_pwi']) === $currentPwi
                    && trim((string)$activeSource['hbl_current_seq']) === $currentSeq) {
                    $this->db_fa->trans_commit();
                    $this->json_response(array('success' => true, 'alreadyActive' => true, 'data' => $activeSource));
                    return;
                }
                throw new DomainException('Source tag already has an active transfer');
            }

            $source = $this->query_source_for_update($sourceTagId);
            if (!$source) {
                throw new DomainException('Source tag is unavailable, not pending, or does not have a valid level-1 plan');
            }

            $sourceQty = $this->parse_normal_tag_quantity($source['qr_detail']);
            if ($sourceQty <= 0 || $sourceQty >= $currentSnp) {
                throw new DomainException('Source quantity is not incomplete for the current SNP');
            }

            $current = $this->query_current_pwi_for_update($currentPwi);
            if (!$current) {
                throw new DomainException('Current PWI is unavailable or does not have a valid level-1 plan');
            }
            if (strcasecmp(trim((string)$current['current_wi']), $currentWi) !== 0) {
                throw new DomainException('Current WI does not match the authoritative current PWI');
            }
            if (strcasecmp(trim((string)$source['item_cd']), trim((string)$current['item_cd'])) !== 0) {
                throw new DomainException('Source part does not match the current part');
            }

            $activeCurrent = $this->query_one_or_throw(
                'SELECT TOP 1 * FROM dbo.history_box_log WITH (UPDLOCK, HOLDLOCK, ROWLOCK) '
                . 'WHERE hbl_current_pwi = ? AND TRY_CONVERT(INT, hbl_current_seq) = ? AND hbl_flag = 0 ORDER BY hbl_id DESC',
                array($currentPwi, (int)$currentSeq)
            );
            if ($activeCurrent) {
                throw new DomainException('Current PWI and sequence already have an active transfer');
            }

            $existingTagCount = $this->query_scalar_or_throw(
                'SELECT COUNT(1) FROM dbo.tag_print_detail WITH (UPDLOCK, HOLDLOCK, ROWLOCK) '
                . 'WHERE wi = ? AND pwi_id = ? AND TRY_CONVERT(INT, seq_no) = ?',
                array($currentWi, $currentPwi, (int)$currentSeq)
            );
            if ((int)$existingTagCount > 0) {
                throw new DomainException('Current sequence already has persisted box data');
            }

            $inserted = $this->db_fa->query(
                'INSERT INTO dbo.history_box_log '
                . '(hbl_source_tag_id, hbl_source_wi, hbl_source_pwi, hbl_source_seq, hbl_source_box_no, hbl_base_qty, '
                . 'hbl_current_wi, hbl_current_pwi, hbl_current_seq, hbl_current_box_no, hbl_current_snp, hbl_good_snapshot, '
                . 'hbl_current_tag_id, hbl_flag, hbl_started_at, hbl_completed_at) '
                . 'VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, NULL, 0, GETDATE(), NULL)',
                array(
                    $sourceTagId,
                    $source['wi'],
                    $source['pwi_id'],
                    $source['seq_no'],
                    str_pad((string)(int)$source['box_no'], 3, '0', STR_PAD_LEFT),
                    $sourceQty,
                    $current['current_wi'],
                    $currentPwi,
                    $currentSeq,
                    '001',
                    $currentSnp,
                    $goodSnapshot
                )
            );
            if ($inserted !== true) {
                throw new RuntimeException($this->db_error_message());
            }

            // CI3 returns boolean for INSERT. Read the row through a SELECT result.
            $created = $this->query_one_or_throw(
                'SELECT TOP 1 * FROM dbo.history_box_log '
                . 'WHERE hbl_source_tag_id = ? AND hbl_current_pwi = ? AND TRY_CONVERT(INT, hbl_current_seq) = ? AND hbl_flag = 0 '
                . 'ORDER BY hbl_id DESC',
                array($sourceTagId, $currentPwi, (int)$currentSeq)
            );
            if (!$created) {
                throw new RuntimeException('The active transfer could not be read after insert');
            }

            $this->db_fa->trans_commit();
            $this->json_response(array('success' => true, 'alreadyActive' => false, 'data' => $created), 201);
        } catch (Throwable $e) {
            $this->db_fa->trans_rollback();
            $status = $e instanceof DomainException ? 409 : 500;
            $this->json_response(array(
                'success' => false,
                'message' => $e instanceof DomainException ? $e->getMessage() : 'Unable to start incomplete transfer',
                'error' => $e instanceof DomainException ? null : $this->safe_exception_message($e)
            ), $status);
        }
    }

    /**
     * Atomically completes a durable incomplete-box transfer for full BOX001.
     *
     * In a single SQL transaction:
     * 1. Locks the active history_box_log row.
     * 2. Validates transfer identity (source_tag_id, current_wi, current_pwi, current_seq, current_snp, current_box_no == 1).
     * 3. Handles idempotency:
     *    - If already completed (hbl_flag = 1): verifies matching current tag and returns success.
     *    - If active (hbl_flag = 0):
     *      - Checks matching tags for (pwi_id, seq_no, box_no):
     *        - Count > 1: Ambiguous -> FAILS CLOSED (409 Conflict).
     *        - Count == 1: Single matching tag -> reconciles by completing source and history.
     *        - Count == 0: Clean insert -> checks source flg_control IN ('0','2'), inserts current tag,
     *                      completes source (flg_control = '1'), completes history (hbl_flag = 1, hbl_current_tag_id = id).
     * 4. Commits and returns JSON response.
     */
    /**
     * Validates whether a loaded tag_print_detail row matches the exact expected identity
     * and immutable payload of the completion request.
     *
     * @param array $tagRow Row loaded from tag_print_detail
     * @param array $expected Array with keys: id (optional), wi, pwi_id, seq_no, box_no, qr_detail, shift, next_proc, tag_group_no
     * @param string &$mismatchReason Populated with detail if mismatch
     * @return bool True if exact match, false otherwise
     */
    public function validate_exact_current_tag(array $tagRow, array $expected, &$mismatchReason = '')
    {
        $mismatchReason = '';

        if (isset($expected['id']) && (int)$expected['id'] > 0) {
            if ((int)$tagRow['id'] !== (int)$expected['id']) {
                $mismatchReason = "Tag ID mismatch: expected {$expected['id']}, got {$tagRow['id']}";
                return false;
            }
        }

        if (strcasecmp(trim((string)$tagRow['wi']), trim((string)$expected['wi'])) !== 0) {
            $mismatchReason = "WI mismatch: expected {$expected['wi']}, got {$tagRow['wi']}";
            return false;
        }

        if (trim((string)$tagRow['pwi_id']) !== trim((string)$expected['pwi_id'])) {
            $mismatchReason = "PWI mismatch: expected {$expected['pwi_id']}, got {$tagRow['pwi_id']}";
            return false;
        }

        $rowSeq = trim((string)$tagRow['seq_no']);
        $expSeq = trim((string)$expected['seq_no']);
        if ($rowSeq !== $expSeq && (int)$rowSeq !== (int)$expSeq) {
            $mismatchReason = "Seq mismatch: expected {$expSeq}, got {$rowSeq}";
            return false;
        }

        if ((int)$tagRow['box_no'] !== (int)$expected['box_no']) {
            $mismatchReason = "BoxNo mismatch: expected {$expected['box_no']}, got {$tagRow['box_no']}";
            return false;
        }

        if (trim((string)$tagRow['qr_detail']) !== trim((string)$expected['qr_detail'])) {
            $mismatchReason = "QR detail mismatch";
            return false;
        }

        if (isset($expected['shift']) && $expected['shift'] !== '') {
            if (strcasecmp(trim((string)$tagRow['shift']), trim((string)$expected['shift'])) !== 0) {
                $mismatchReason = "Shift mismatch: expected {$expected['shift']}, got {$tagRow['shift']}";
                return false;
            }
        }

        if (isset($expected['next_proc']) && $expected['next_proc'] !== '') {
            if (strcasecmp(trim((string)$tagRow['next_proc']), trim((string)$expected['next_proc'])) !== 0) {
                $mismatchReason = "NextProcess mismatch: expected {$expected['next_proc']}, got {$tagRow['next_proc']}";
                return false;
            }
        }

        if (isset($expected['tag_group_no']) && $expected['tag_group_no'] !== '') {
            $rowGrp = trim((string)$tagRow['tag_group_no']);
            $expGrp = trim((string)$expected['tag_group_no']);
            if ($rowGrp !== $expGrp && (int)$rowGrp !== (int)$expGrp) {
                $mismatchReason = "TagGroupNo mismatch: expected {$expGrp}, got {$rowGrp}";
                return false;
            }
        }

        return true;
    }

    /**
     * Atomically completes a durable incomplete-box transfer for full BOX001.
     *
     * In a single SQL transaction:
     * 1. Locks the active history_box_log row.
     * 2. Validates transfer identity against locked history row.
     * 3. Locks and reads exact source tag in tag_print_detail.
     * 4. Handles idempotency (hbl_flag = 1): verifies exact matching tag ID and payload.
     * 5. For active history (hbl_flag = 0):
     *    - 0 candidate tags: verifies source pending, inserts new tag, verifies SCOPE_IDENTITY, completes source & history.
     *    - 1 candidate tag: validates exact payload against completion request. If mismatch -> CONFLICT.
     *                       If exact match -> safe reconciliation of source and history.
     *    - >1 candidate tags: AMBIGUOUS -> CONFLICT & rollback.
     * 6. Commits and returns JSON response.
     */
    public function complete()
    {
        if (strtoupper($this->input->method(true)) !== 'POST') {
            $this->json_response(array('success' => false, 'message' => 'POST is required'), 405);
            return;
        }

        $request = $this->request_data();
        $hblId = $this->request_value($request, 'hbl_id', 'hblId');
        $sourceTagId = $this->request_value($request, 'source_tag_id', 'sourceTagId');
        $currentWi = trim((string)$this->request_value($request, 'current_wi', 'currentWi'));
        $currentPwi = trim((string)$this->request_value($request, 'current_pwi', 'currentPwi'));
        $currentSeq = trim((string)$this->request_value($request, 'current_seq', 'currentSeq'));
        $currentBoxNo = $this->request_value($request, 'current_box_no', 'currentBoxNo');
        $currentSnp = $this->request_value($request, 'current_snp', 'currentSnp');

        // Tag persistence fields
        $qrDetail = (string)$this->request_value($request, 'qr_detail', 'qrDetail');
        $shift = trim((string)$this->request_value($request, 'shift', 'shift'));
        $flgControl = $this->request_value($request, 'flg_control', 'flgControl');
        $itemCd = trim((string)$this->request_value($request, 'item_cd', 'itemCd'));
        $tagGroupNo = trim((string)$this->request_value($request, 'tag_group_no', 'tagGroupNo'));
        $goodQty = $this->request_value($request, 'good_qty', 'goodQty');
        $nextProc = trim((string)$this->request_value($request, 'next_proc', 'nextProc'));

        if (!$this->is_positive_integer($hblId) || !$this->is_positive_integer($sourceTagId)
            || $currentWi === '' || $currentPwi === '' || $currentSeq === ''
            || !is_numeric($currentBoxNo) || !is_numeric($currentSnp)
            || strlen($qrDetail) < 10) {
            $this->json_response(array('success' => false, 'message' => 'Missing or invalid required complete fields'), 400);
            return;
        }

        $hblId = (int)$hblId;
        $sourceTagId = (int)$sourceTagId;
        $currentBoxNo = (int)$currentBoxNo;
        $currentSnp = (int)$currentSnp;
        $currentSeqInt = (int)$currentSeq;
        // Full Continue completion always creates a consumed/current-complete
        // tag. Do not inherit a legacy caller's incomplete/claim flag.
        $flgControl = 1;
        if ($tagGroupNo === '') $tagGroupNo = '1';

        // In this phase, Continue full box completion is strictly for BOX001
        if ($currentBoxNo !== 1) {
            $this->json_response(array('success' => false, 'message' => 'Continue full box completion must be BOX001'), 400);
            return;
        }

        $expectedTagPayload = array(
            'wi' => $currentWi,
            'pwi_id' => $currentPwi,
            'seq_no' => $currentSeq,
            'box_no' => $currentBoxNo,
            'qr_detail' => $qrDetail,
            'shift' => $shift,
            'next_proc' => $nextProc,
            'tag_group_no' => $tagGroupNo
        );

        $completeStep = 'COMPLETE_LOCK_HISTORY';
        $completeDbError = null;
        $this->db_fa->trans_begin();
        try {
            // 1. Lock and inspect history_box_log row
            $history = $this->query_one_or_throw(
                'SELECT TOP 1 * FROM dbo.history_box_log WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE hbl_id = ?',
                array($hblId)
            );
            if (!$history) {
                throw new DomainException("History box log record {$hblId} not found");
            }

            // 2. Validate transfer identity against locked history row
            if ((int)$history['hbl_source_tag_id'] !== $sourceTagId) {
                throw new DomainException('Source tag ID does not match the active transfer');
            }
            if (strcasecmp(trim((string)$history['hbl_current_wi']), $currentWi) !== 0) {
                throw new DomainException('Current WI does not match the active transfer');
            }
            if (trim((string)$history['hbl_current_pwi']) !== $currentPwi) {
                throw new DomainException('Current PWI does not match the active transfer');
            }
            if (trim((string)$history['hbl_current_seq']) !== $currentSeq
                && (int)$history['hbl_current_seq'] !== (int)$currentSeq) {
                throw new DomainException('Current sequence does not match the active transfer');
            }
            if ((int)$history['hbl_current_snp'] !== $currentSnp) {
                throw new DomainException('Current SNP does not match the active transfer');
            }

            // 3. Lock and read exact source tag in tag_print_detail inside same transaction
            $sourceTag = $this->query_one_or_throw(
                'SELECT id, wi, pwi_id, seq_no, box_no, flg_control FROM dbo.tag_print_detail WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE id = ?',
                array($sourceTagId)
            );
            if (!$sourceTag) {
                throw new DomainException("Source tag {$sourceTagId} not found in tag_print_detail");
            }

            // 4. Check if already completed (Idempotent exact retry)
            if ((int)$history['hbl_flag'] === 1) {
                $existingCurrentTagId = isset($history['hbl_current_tag_id']) ? (int)$history['hbl_current_tag_id'] : 0;
                if ($existingCurrentTagId <= 0) {
                    throw new DomainException("Transfer {$hblId} is flagged completed but hbl_current_tag_id is missing");
                }

                $verifyTag = $this->query_one_or_throw(
                    'SELECT TOP 1 id, wi, pwi_id, seq_no, box_no, qr_detail, shift, next_proc, tag_group_no, flg_control '
                    . 'FROM dbo.tag_print_detail WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE id = ?',
                    array($existingCurrentTagId)
                );
                if (!$verifyTag) {
                    throw new DomainException("Transfer {$hblId} references current tag {$existingCurrentTagId}, but the tag does not exist");
                }

                $historyExpected = $expectedTagPayload;
                $historyExpected['id'] = $existingCurrentTagId;
                $historyExpected['wi'] = (string)$history['hbl_current_wi'];
                $historyExpected['pwi_id'] = (string)$history['hbl_current_pwi'];
                $historyExpected['seq_no'] = (string)$history['hbl_current_seq'];
                $historyExpected['box_no'] = (int)$history['hbl_current_box_no'];

                $mismatchReason = '';
                if (!$this->validate_exact_current_tag($verifyTag, $historyExpected, $mismatchReason)) {
                    throw new DomainException("Completed transfer {$hblId} references tag {$existingCurrentTagId} with mismatching identity/payload: {$mismatchReason}");
                }

                $this->db_fa->trans_commit();
                $this->json_response(array(
                    'success' => true,
                    'alreadyCompleted' => true,
                    'hbl_id' => $hblId,
                    'current_tag_id' => $existingCurrentTagId,
                    'hbl_flag' => 1,
                    'message' => 'Transfer already completed'
                ));
                return;
            }

            if ((int)$history['hbl_flag'] === 3) {
                throw new DomainException('Transfer was released after zero-net crash recovery and cannot be completed');
            }
            if ((int)$history['hbl_flag'] !== 0) {
                throw new DomainException('Transfer is not ACTIVE for completion');
            }

            // 5. Query matching candidate tags in tag_print_detail for this exact logical box identity
            $completeStep = 'COMPLETE_FIND_EXISTING_CURRENT_TAG';
            $matchingTagsQuery = $this->db_fa->query(
                'SELECT id, wi, pwi_id, seq_no, box_no, qr_detail, shift, next_proc, tag_group_no, flg_control '
                . 'FROM dbo.tag_print_detail WITH (UPDLOCK, HOLDLOCK, ROWLOCK) '
                . 'WHERE pwi_id = ? AND TRY_CONVERT(INT, seq_no) = ? '
                . 'AND box_no = ? AND flg_control IN (\'0\',\'1\',\'2\') '
                . 'ORDER BY id ASC',
                array($currentPwi, $currentSeqInt, $currentBoxNo)
            );
            if ($matchingTagsQuery === false || !is_object($matchingTagsQuery)) {
                $completeDbError = $this->db_fa->error();
                throw new RuntimeException($this->db_error_message());
            }
            $matchingTags = $matchingTagsQuery->result_array();
            $matchCount = count($matchingTags);

            // 5a. Ambiguous case: multiple tags already exist -> FAIL CLOSED
            if ($matchCount > 1) {
                throw new DomainException("Ambiguous state: {$matchCount} matching current tags exist for PWI {$currentPwi} Seq {$currentSeq} Box {$currentBoxNo}. Automated reconciliation aborted.");
            }

            // 5b. Uncertain commit case: exactly one matching tag already exists -> Exact payload check
            if ($matchCount === 1) {
                $candidateTag = $matchingTags[0];
                $reconciledTagId = (int)$candidateTag['id'];

                $mismatchReason = '';
                if (!$this->validate_exact_current_tag($candidateTag, $expectedTagPayload, $mismatchReason)) {
                    throw new DomainException("Existing candidate tag {$reconciledTagId} payload does not match completion request: {$mismatchReason}. Reconciliation aborted.");
                }

                // Source state validation for uncertain reconciliation
                if ($sourceTag['flg_control'] === '1' || (int)$sourceTag['flg_control'] === 1) {
                    // Source was already completed by earlier attempt
                } elseif ($sourceTag['flg_control'] === '0' || $sourceTag['flg_control'] === '2' || (int)$sourceTag['flg_control'] === 0 || (int)$sourceTag['flg_control'] === 2) {
                    // Complete the source tag now
                    $updateSourceResult = $this->db_fa->query(
                        'UPDATE dbo.tag_print_detail SET flg_control = \'1\', updated_date = GETDATE() '
                        . 'WHERE id = ? AND flg_control IN (\'0\',\'2\')',
                        array($sourceTagId)
                    );
                    if ($updateSourceResult !== true) {
                        throw new RuntimeException('Failed to complete source tag during reconciliation');
                    }
                    $completeStep = 'COMPLETE_VERIFY_RECONCILED_SOURCE';
                    $sourceAfter = $this->query_one_or_throw(
                        'SELECT id, flg_control FROM dbo.tag_print_detail WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE id = ?',
                        array($sourceTagId)
                    );
                    if (!$sourceAfter || (int)$sourceAfter['flg_control'] !== 1) {
                        throw new RuntimeException('Source tag was not confirmed completed during reconciliation');
                    }
                } else {
                    throw new DomainException("Source tag {$sourceTagId} has invalid control state: {$sourceTag['flg_control']}");
                }

                // Update history row
                $updatedHbl = $this->db_fa->query(
                    'UPDATE dbo.history_box_log '
                    . 'SET hbl_current_tag_id = ?, hbl_flag = 1, hbl_completed_at = GETDATE() '
                    . 'WHERE hbl_id = ? AND hbl_flag = 0',
                    array($reconciledTagId, $hblId)
                );
                if ($updatedHbl !== true) {
                    throw new RuntimeException($this->db_error_message());
                }
                $completeStep = 'COMPLETE_VERIFY_RECONCILED_HISTORY';
                $historyAfter = $this->query_one_or_throw(
                    'SELECT TOP 1 hbl_current_tag_id, hbl_flag, hbl_completed_at FROM dbo.history_box_log WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE hbl_id = ?',
                    array($hblId)
                );
                if (!$historyAfter || (int)$historyAfter['hbl_flag'] !== 1
                    || !isset($historyAfter['hbl_current_tag_id']) || (int)$historyAfter['hbl_current_tag_id'] !== $reconciledTagId
                    || empty($historyAfter['hbl_completed_at'])) {
                    throw new RuntimeException('History transfer was not confirmed completed during reconciliation');
                }

                $this->db_fa->trans_commit();
                $this->json_response(array(
                    'success' => true,
                    'alreadyCompleted' => false,
                    'reconciled' => true,
                    'hbl_id' => $hblId,
                    'current_tag_id' => $reconciledTagId,
                    'hbl_flag' => 1,
                    'message' => 'Reconciled existing single matching tag with history log'
                ));
                return;
            }

            // 5c. Clean first-complete case: zero matching tags exist
            // Validate source tag is currently pending
            if ($sourceTag['flg_control'] !== '0' && $sourceTag['flg_control'] !== '2' && (int)$sourceTag['flg_control'] !== 0 && (int)$sourceTag['flg_control'] !== 2) {
                throw new DomainException("Source tag {$sourceTagId} is not pending (flg_control = {$sourceTag['flg_control']})");
            }

            // Keep the ODBC INSERT simple. Resolve the exact inserted row while
            // this transaction still holds the history/source locks.
            $completeStep = 'COMPLETE_INSERT_CURRENT_TAG';
            $inserted = $this->db_fa->query(
                'INSERT INTO dbo.tag_print_detail '
                . '(wi, qr_detail, box_no, print_count, created_date, updated_date, seq_no, shift, next_proc, flg_control, pwi_id, tag_group_no) '
                . 'VALUES (?, ?, ?, 1, GETDATE(), GETDATE(), ?, ?, ?, ?, ?, ?)',
                array(
                    $currentWi,
                    $qrDetail,
                    $currentBoxNo,
                    $currentSeq,
                    $shift,
                    $nextProc,
                    $flgControl,
                    $currentPwi,
                    $tagGroupNo
                )
            );
            if ($inserted === false) {
                throw new RuntimeException($this->db_error_message());
            }

            $completeStep = 'COMPLETE_READ_INSERTED_CURRENT_TAG';
            $insertedMatches = $this->db_fa->query(
                'SELECT id, wi, pwi_id, seq_no, box_no, qr_detail, shift, next_proc, tag_group_no, flg_control '
                . 'FROM dbo.tag_print_detail WITH (UPDLOCK, HOLDLOCK, ROWLOCK) '
                . 'WHERE wi = ? AND pwi_id = ? AND TRY_CONVERT(INT, seq_no) = ? AND box_no = ? '
                . 'AND qr_detail = ? AND shift = ? AND next_proc = ? AND tag_group_no = ? AND flg_control = 1 '
                . 'ORDER BY id ASC',
                array($currentWi, $currentPwi, $currentSeqInt, $currentBoxNo, $qrDetail, $shift, $nextProc, $tagGroupNo)
            );
            if ($insertedMatches === false || !is_object($insertedMatches)) {
                $completeDbError = $this->db_fa->error();
                throw new RuntimeException('Unable to re-read exact inserted completed tag');
            }
            $insertedRows = $insertedMatches->result_array();
            if (count($insertedRows) !== 1) {
                throw new RuntimeException('Complete tag insert did not resolve to exactly one matching tag');
            }
            $mismatchReason = '';
            if (!$this->validate_exact_current_tag($insertedRows[0], $expectedTagPayload, $mismatchReason)) {
                throw new RuntimeException("Inserted complete tag does not match the requested identity: {$mismatchReason}");
            }
            $newTagId = isset($insertedRows[0]['id']) ? (int)$insertedRows[0]['id'] : 0;
            if ($newTagId <= 0) throw new RuntimeException('Failed to obtain exact complete tag ID after insert');

            // Complete the source tag
            $sourceUpdated = $this->db_fa->query(
                'UPDATE dbo.tag_print_detail SET flg_control = \'1\', updated_date = GETDATE() '
                . 'WHERE id = ? AND flg_control IN (\'0\',\'2\')',
                array($sourceTagId)
            );
            if ($sourceUpdated !== true) {
                throw new RuntimeException('Failed to complete source tag - concurrent modification detected');
            }
            $completeStep = 'COMPLETE_VERIFY_SOURCE';
            $sourceAfter = $this->query_one_or_throw(
                'SELECT id, flg_control FROM dbo.tag_print_detail WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE id = ?',
                array($sourceTagId)
            );
            if (!$sourceAfter || (int)$sourceAfter['flg_control'] !== 1) {
                throw new RuntimeException('Source tag was not confirmed completed');
            }

            // Complete the history_box_log row
            $historyUpdated = $this->db_fa->query(
                'UPDATE dbo.history_box_log '
                . 'SET hbl_current_tag_id = ?, hbl_flag = 1, hbl_completed_at = GETDATE() '
                . 'WHERE hbl_id = ? AND hbl_flag = 0',
                array($newTagId, $hblId)
            );
            if ($historyUpdated !== true) {
                throw new RuntimeException('Failed to update history box log');
            }
            $completeStep = 'COMPLETE_VERIFY_HISTORY';
            $historyAfter = $this->query_one_or_throw(
                'SELECT TOP 1 hbl_current_tag_id, hbl_flag, hbl_completed_at FROM dbo.history_box_log WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE hbl_id = ?',
                array($hblId)
            );
            if (!$historyAfter || (int)$historyAfter['hbl_flag'] !== 1
                || !isset($historyAfter['hbl_current_tag_id']) || (int)$historyAfter['hbl_current_tag_id'] !== $newTagId
                || empty($historyAfter['hbl_completed_at'])) {
                throw new RuntimeException('History transfer was not confirmed completed');
            }

            $this->db_fa->trans_commit();
            $this->json_response(array(
                'success' => true,
                'alreadyCompleted' => false,
                'reconciled' => false,
                'hbl_id' => $hblId,
                'current_tag_id' => $newTagId,
                'hbl_flag' => 1,
                'message' => 'Transfer completed and tag persisted'
            ), 200);
        } catch (Throwable $e) {
            $this->db_fa->trans_rollback();
            $status = $e instanceof DomainException ? 409 : 500;
            $error = is_array($completeDbError) ? $completeDbError : $this->db_fa->error();
            $payload = array(
                'success' => false,
                'message' => $e instanceof DomainException ? $e->getMessage() : 'Unable to complete incomplete transfer',
                'error' => $e instanceof DomainException ? null : $this->safe_exception_message($e)
            );
            if (!($e instanceof DomainException)) {
                $payload['step'] = $completeStep;
                $payload['db_code'] = isset($error['code']) ? (string)$error['code'] : '';
                $payload['db_message'] = isset($error['message']) ? (string)$error['message'] : '';
                log_message('error', 'Incomplete transfer complete failed at ' . $completeStep
                    . ' | DB code=' . $payload['db_code'] . ' | DB message=' . $payload['db_message']);
            }
            $this->json_response($payload, $status);
        }
    }

    /**
     * Atomically closes an ACTIVE Continue session below Current SNP.
     * The selected source is consumed and exactly one Current BOX001 pending
     * tag is created. This is intentionally separate from full completion.
     */
    public function partial()
    {
        if (strtoupper($this->input->method(true)) !== 'POST') {
            $this->json_response(array('success' => false, 'message' => 'POST is required'), 405);
            return;
        }

        $request = $this->request_data();
        $hblId = $this->request_value($request, 'hbl_id', 'hblId');
        $sourceTagId = $this->request_value($request, 'source_tag_id', 'sourceTagId');
        $currentWi = trim((string)$this->request_value($request, 'current_wi', 'currentWi'));
        $currentPwi = trim((string)$this->request_value($request, 'current_pwi', 'currentPwi'));
        $currentSeq = trim((string)$this->request_value($request, 'current_seq', 'currentSeq'));
        $currentBoxNo = $this->request_value($request, 'current_box_no', 'currentBoxNo');
        $currentSnp = $this->request_value($request, 'current_snp', 'currentSnp');
        $qrDetail = (string)$this->request_value($request, 'qr_detail', 'qrDetail');
        $shift = trim((string)$this->request_value($request, 'shift', 'shift'));
        $itemCd = trim((string)$this->request_value($request, 'item_cd', 'itemCd'));
        $tagGroupNo = trim((string)$this->request_value($request, 'tag_group_no', 'tagGroupNo'));
        $goodQty = $this->request_value($request, 'good_qty', 'goodQty');
        $nextProc = trim((string)$this->request_value($request, 'next_proc', 'nextProc'));

        if (!$this->is_positive_integer($hblId) || !$this->is_positive_integer($sourceTagId)
            || $currentWi === '' || $currentPwi === '' || $currentSeq === ''
            || !is_numeric($currentBoxNo) || !is_numeric($currentSnp) || !is_numeric($goodQty)
            || strlen($qrDetail) < 58) {
            $this->json_response(array('success' => false, 'message' => 'Missing or invalid required partial fields'), 400);
            return;
        }

        $hblId = (int)$hblId;
        $sourceTagId = (int)$sourceTagId;
        $currentBoxNo = (int)$currentBoxNo;
        $currentSnp = (int)$currentSnp;
        $goodQty = (int)$goodQty;
        $currentSeqInt = (int)$currentSeq;
        if ($currentBoxNo !== 1 || $currentSnp <= 1 || $currentSnp === 999999
            || $goodQty <= 0 || $goodQty >= $currentSnp
            || $this->parse_normal_tag_quantity($qrDetail) !== $goodQty) {
            $this->json_response(array('success' => false, 'message' => 'Partial quantity or Current BOX001 identity is invalid'), 400);
            return;
        }
        if ($tagGroupNo === '') $tagGroupNo = '1';

        $expected = array(
            'wi' => $currentWi, 'pwi_id' => $currentPwi, 'seq_no' => $currentSeq,
            'box_no' => $currentBoxNo, 'qr_detail' => $qrDetail, 'shift' => $shift,
            'next_proc' => $nextProc, 'tag_group_no' => $tagGroupNo
        );

        $partialStep = 'PARTIAL_LOCK_HISTORY';
        $partialDbError = null;
        $this->db_fa->trans_begin();
        try {
            $history = $this->query_one_or_throw(
                'SELECT TOP 1 * FROM dbo.history_box_log WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE hbl_id = ?',
                array($hblId)
            );
            if (!$history) throw new DomainException("History box log record {$hblId} not found");
            if ((int)$history['hbl_source_tag_id'] !== $sourceTagId
                || strcasecmp(trim((string)$history['hbl_current_wi']), $currentWi) !== 0
                || trim((string)$history['hbl_current_pwi']) !== $currentPwi
                || (trim((string)$history['hbl_current_seq']) !== $currentSeq && (int)$history['hbl_current_seq'] !== $currentSeqInt)
                || (int)$history['hbl_current_snp'] !== $currentSnp
                || (int)$history['hbl_current_box_no'] !== $currentBoxNo) {
                throw new DomainException('Partial request does not match the durable transfer identity');
            }

            $partialStep = 'PARTIAL_LOAD_SOURCE';
            $sourceTag = $this->query_one_or_throw(
                'SELECT id, flg_control FROM dbo.tag_print_detail WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE id = ?',
                array($sourceTagId)
            );
            if (!$sourceTag) throw new DomainException("Source tag {$sourceTagId} not found in tag_print_detail");

            if ((int)$history['hbl_flag'] === 2) {
                $tagId = isset($history['hbl_current_tag_id']) ? (int)$history['hbl_current_tag_id'] : 0;
                $tag = $tagId > 0 ? $this->query_one_or_throw(
                    'SELECT id, wi, pwi_id, seq_no, box_no, qr_detail, shift, next_proc, tag_group_no, flg_control '
                    . 'FROM dbo.tag_print_detail WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE id = ?', array($tagId)) : null;
                $mismatch = '';
                if (!$tag || (int)$tag['flg_control'] !== 0 || !$this->validate_exact_current_tag($tag, $expected, $mismatch)
                    || (int)$sourceTag['flg_control'] !== 1) {
                    throw new DomainException("Partial transfer {$hblId} has inconsistent committed state: {$mismatch}");
                }
                $this->db_fa->trans_commit();
                $this->json_response(array('success' => true, 'alreadyPartial' => true, 'hbl_id' => $hblId,
                    'current_tag_id' => $tagId, 'hbl_flag' => 2, 'message' => 'Transfer already partially closed'));
                return;
            }
            if ((int)$history['hbl_flag'] !== 0) throw new DomainException('Transfer is not ACTIVE for partial close');
            if ((int)$sourceTag['flg_control'] !== 0) throw new DomainException('Source tag is not pending for partial close');

            $partialStep = 'PARTIAL_FIND_EXISTING_CURRENT_TAG';
            $matches = $this->db_fa->query(
                'SELECT id, wi, pwi_id, seq_no, box_no, qr_detail, shift, next_proc, tag_group_no, flg_control '
                . 'FROM dbo.tag_print_detail WITH (UPDLOCK, HOLDLOCK, ROWLOCK) '
                . 'WHERE pwi_id = ? AND TRY_CONVERT(INT, seq_no) = ? AND box_no = ? AND flg_control IN (\'0\',\'1\',\'2\') ORDER BY id ASC',
                array($currentPwi, $currentSeqInt, $currentBoxNo)
            );
            if ($matches === false || !is_object($matches)) {
                $partialDbError = $this->db_fa->error();
                throw new RuntimeException($this->db_error_message());
            }
            $rows = $matches->result_array();
            if (count($rows) > 1) throw new DomainException('Ambiguous current partial tag candidates; reconciliation aborted');

            if (count($rows) === 1) {
                $currentTag = $rows[0];
                $mismatch = '';
                if ((int)$currentTag['flg_control'] !== 0 || !$this->validate_exact_current_tag($currentTag, $expected, $mismatch)) {
                    throw new DomainException("Existing current tag does not match partial request: {$mismatch}");
                }
                $currentTagId = (int)$currentTag['id'];
            } else {
                // The legacy SQL Server ODBC driver cannot describe parameter
                // markers in the previous multi-statement OUTPUT batch.  Keep
                // this INSERT simple, then identify its exact row while this
                // transaction still holds the durable transfer/source locks.
                $partialStep = 'PARTIAL_INSERT_CURRENT_TAG';
                $inserted = $this->db_fa->query(
                    'INSERT INTO dbo.tag_print_detail '
                    . '(wi, qr_detail, box_no, print_count, created_date, updated_date, seq_no, shift, next_proc, flg_control, pwi_id, tag_group_no) '
                    . 'VALUES (?, ?, ?, 1, GETDATE(), GETDATE(), ?, ?, ?, 0, ?, ?)',
                    array($currentWi, $qrDetail, $currentBoxNo, $currentSeq, $shift, $nextProc, $currentPwi, $tagGroupNo)
                );
                if ($inserted === false) {
                    $partialDbError = $this->db_fa->error();
                    throw new RuntimeException($this->db_error_message());
                }

                $partialStep = 'PARTIAL_READ_INSERTED_CURRENT_TAG';
                $insertedMatches = $this->db_fa->query(
                    'SELECT id, wi, pwi_id, seq_no, box_no, qr_detail, shift, next_proc, tag_group_no, flg_control '
                    . 'FROM dbo.tag_print_detail WITH (UPDLOCK, HOLDLOCK, ROWLOCK) '
                    . 'WHERE wi = ? AND pwi_id = ? AND TRY_CONVERT(INT, seq_no) = ? AND box_no = ? '
                    . 'AND qr_detail = ? AND shift = ? AND next_proc = ? AND tag_group_no = ? AND flg_control = 0 '
                    . 'ORDER BY id ASC',
                    array($currentWi, $currentPwi, $currentSeqInt, $currentBoxNo, $qrDetail, $shift, $nextProc, $tagGroupNo)
                );
                if ($insertedMatches === false || !is_object($insertedMatches)) {
                    $partialDbError = $this->db_fa->error();
                    throw new RuntimeException('Unable to re-read exact inserted partial tag');
                }
                $insertedRows = $insertedMatches->result_array();
                if (count($insertedRows) !== 1) {
                    throw new RuntimeException('Partial tag insert did not resolve to exactly one matching tag');
                }
                $mismatch = '';
                if (!$this->validate_exact_current_tag($insertedRows[0], $expected, $mismatch)) {
                    throw new RuntimeException("Inserted partial tag does not match the requested identity: {$mismatch}");
                }
                $currentTagId = isset($insertedRows[0]['id']) ? (int)$insertedRows[0]['id'] : 0;
                if ($currentTagId <= 0) throw new RuntimeException('Failed to obtain exact partial tag ID after insert');
            }

            $partialStep = 'PARTIAL_UPDATE_SOURCE';
            $sourceUpdated = $this->db_fa->query(
                "UPDATE dbo.tag_print_detail SET flg_control = '1', updated_date = GETDATE() WHERE id = ? AND flg_control = '0'",
                array($sourceTagId)
            );
            if ($sourceUpdated !== true) throw new RuntimeException('Failed to consume source tag during partial close');
            $partialStep = 'PARTIAL_VERIFY_SOURCE';
            $sourceAfter = $this->query_one_or_throw(
                'SELECT id, flg_control FROM dbo.tag_print_detail WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE id = ?',
                array($sourceTagId)
            );
            if (!$sourceAfter || (int)$sourceAfter['flg_control'] !== 1) {
                throw new RuntimeException('Source tag was not confirmed consumed during partial close');
            }

            $partialStep = 'PARTIAL_UPDATE_HISTORY';
            $historyUpdated = $this->db_fa->query(
                'UPDATE dbo.history_box_log SET hbl_current_tag_id = ?, hbl_flag = 2, hbl_completed_at = GETDATE() WHERE hbl_id = ? AND hbl_flag = 0',
                array($currentTagId, $hblId)
            );
            if ($historyUpdated !== true) throw new RuntimeException('Failed to close history transfer as partial');
            $partialStep = 'PARTIAL_VERIFY_HISTORY';
            $historyAfter = $this->query_one_or_throw(
                'SELECT TOP 1 hbl_current_tag_id, hbl_flag, hbl_completed_at FROM dbo.history_box_log WITH (UPDLOCK, HOLDLOCK, ROWLOCK) WHERE hbl_id = ?',
                array($hblId)
            );
            if (!$historyAfter || (int)$historyAfter['hbl_flag'] !== 2
                || !isset($historyAfter['hbl_current_tag_id']) || (int)$historyAfter['hbl_current_tag_id'] !== $currentTagId
                || empty($historyAfter['hbl_completed_at'])) {
                throw new RuntimeException('History transfer was not confirmed partially closed');
            }

            $this->db_fa->trans_commit();
            $this->json_response(array('success' => true, 'alreadyPartial' => false, 'hbl_id' => $hblId,
                'current_tag_id' => $currentTagId, 'hbl_flag' => 2, 'message' => 'Transfer partially closed and tag persisted'));
        } catch (Throwable $e) {
            $this->db_fa->trans_rollback();
            $error = is_array($partialDbError) ? $partialDbError : $this->db_fa->error();
            $payload = array('success' => false,
                'message' => $e instanceof DomainException ? $e->getMessage() : 'Unable to partially close incomplete transfer',
                'error' => $e instanceof DomainException ? null : $this->safe_exception_message($e));
            if (!($e instanceof DomainException)) {
                $payload['step'] = $partialStep;
                $payload['db_code'] = isset($error['code']) ? (string)$error['code'] : '';
                $payload['db_message'] = isset($error['message']) ? (string)$error['message'] : '';
                log_message('error', 'Incomplete transfer partial failed at ' . $partialStep
                    . ' | DB code=' . $payload['db_code'] . ' | DB message=' . $payload['db_message']);
            }
            $this->json_response($payload,
                $e instanceof DomainException ? 409 : 500);
        }
    }

    private function query_source_for_update($sourceTagId)
    {
        return $this->query_one_or_throw(
            'SELECT t.id, t.wi, t.pwi_id, t.seq_no, t.box_no, t.qr_detail, t.flg_control, sw.ITEM_CD AS item_cd '
            . 'FROM dbo.tag_print_detail t WITH (UPDLOCK, HOLDLOCK, ROWLOCK) '
            . 'INNER JOIN dbo.production_working_info pwi ON pwi.pwi_id = t.pwi_id '
            . 'INNER JOIN dbo.sup_work_plan_supply_dev sw ON sw.IND_ROW = pwi.ind_row '
            . "WHERE t.id = ? AND t.flg_control = '0' AND sw.LVL = '1'",
            array($sourceTagId)
        );
    }

    private function query_current_pwi_for_update($currentPwi)
    {
        return $this->query_one_or_throw(
            'SELECT pwi.pwi_id, sw.WI AS current_wi, sw.ITEM_CD AS item_cd, sw.LINE_CD AS line_cd '
            . 'FROM dbo.production_working_info pwi WITH (UPDLOCK, HOLDLOCK, ROWLOCK) '
            . 'INNER JOIN dbo.sup_work_plan_supply_dev sw ON sw.IND_ROW = pwi.ind_row '
            . "WHERE pwi.pwi_id = ? AND sw.LVL = '1'",
            array($currentPwi)
        );
    }

    private function query_one_or_throw($sql, array $params)
    {
        $query = $this->db_fa->query($sql, $params);
        if ($query === false) {
            throw new RuntimeException($this->db_error_message());
        }
        return $query->row_array();
    }

    private function query_scalar_or_throw($sql, array $params)
    {
        $query = $this->db_fa->query($sql, $params);
        if ($query === false) {
            throw new RuntimeException($this->db_error_message());
        }
        $row = $query->row_array();
        return $row ? reset($row) : 0;
    }

    private function request_data()
    {
        $data = $this->input->post(NULL, true);
        if (!is_array($data)) {
            $data = array();
        }

        $raw = $this->input->raw_input_stream;
        if ($raw !== '') {
            $json = json_decode($raw, true);
            if (is_array($json)) {
                $data = array_merge($data, $json);
            }
        }
        return $data;
    }

    private function request_value(array $data, $snakeCase, $camelCase)
    {
        if (array_key_exists($snakeCase, $data)) {
            return $data[$snakeCase];
        }
        return array_key_exists($camelCase, $data) ? $data[$camelCase] : null;
    }

    /** Equivalent to VB: trim(substr(qr_detail, 52, 6)). */
    private function parse_normal_tag_quantity($qrDetail)
    {
        $qrDetail = (string)$qrDetail;
        if (strlen($qrDetail) < 58) {
            return 0;
        }
        $text = trim(substr($qrDetail, 52, 6));
        return preg_match('/^[0-9]+$/', $text) ? (int)$text : 0;
    }

    private function is_positive_integer($value)
    {
        return is_scalar($value) && preg_match('/^[1-9][0-9]*$/', trim((string)$value));
    }

    private function db_error_message()
    {
        $error = $this->db_fa->error();
        return isset($error['message']) && $error['message'] !== '' ? $error['message'] : 'Database operation failed';
    }

    private function safe_exception_message(Throwable $e)
    {
        return $e->getMessage() !== '' ? $e->getMessage() : 'Unknown error';
    }

    private function json_response(array $payload, $status = 200)
    {
        $this->output
            ->set_status_header((int)$status)
            ->set_content_type('application/json', 'utf-8')
            ->set_output(json_encode($payload));
    }
}
