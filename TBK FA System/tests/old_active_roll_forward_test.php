<?php
// Offline controller tests: no SQL connection, production writes, or HTTP calls.
define('BASEPATH', __DIR__);
class CI_Controller { public $input; public $output; }
require dirname(__DIR__) . '/Api_incomplete_transfer.php';
class RecoveryInput {
    public $raw_input_stream = '';
    public $payload;
    public $verb = 'POST';
    function method($unused) { return $this->verb; }
    function post($unused, $filter) { return $this->payload; }
}
class RecoveryOutput {
    public $status; public $body;
    function set_status_header($status) { $this->status = $status; return $this; }
    function set_content_type($type, $encoding) { return $this; }
    function set_output($body) { $this->body = json_decode($body, true); return $this; }
}
class RecoveryRows {
    private $row;
    function __construct($row) { $this->row = $row; }
    function row_array() { return $this->row; }
}
class RecoveryDb {
    public $row; public $saved; public $commits = 0; public $rollbacks = 0; public $updates = 0;
    public $failUpdate = false; public $badReadback = false; public $failCommit = false;
    function trans_begin() { $this->saved = $this->row; return true; }
    function trans_status() { return true; }
    function trans_commit() { if ($this->failCommit) return false; ++$this->commits; return true; }
    function trans_rollback() { $this->row = $this->saved; ++$this->rollbacks; }
    function error() { return array('message' => 'simulated failure'); }
    function query($sql, $params) {
        if (strpos($sql, 'FROM dbo.production_working_info') !== false) {
            return new RecoveryRows(array('current_wi' => '5100427641'));
        }
        if (strpos($sql, 'UPDATE ') === 0) {
            check(strpos($sql, 'WHERE hbl_id = ? AND hbl_flag = 0 AND hbl_current_pwi = ? AND hbl_current_seq = ?') !== false, 'SQL guard');
            check(strpos($sql, 'SET hbl_base_qty = ?, hbl_current_wi = ?, hbl_current_pwi = ?, hbl_current_seq = ? WHERE') !== false, 'only four fields');
            ++$this->updates;
            if ($this->failUpdate) return false;
            foreach (array('hbl_base_qty', 'hbl_current_wi', 'hbl_current_pwi', 'hbl_current_seq') as $i => $key) $this->row[$key] = $params[$i];
            return true;
        }
        $row = $this->row;
        if ($this->badReadback && $this->updates) $row['hbl_base_qty'] = -1;
        return new RecoveryRows($row);
    }
}
function check($condition, $label) { if (!$condition) throw new Exception('FAIL: ' . $label); }
function invokeRecovery($db, $payload, $verb = 'POST') {
    $class = new ReflectionClass('Api_incomplete_transfer');
    $controller = $class->newInstanceWithoutConstructor();
    $property = $class->getProperty('db_fa'); $property->setAccessible(true); $property->setValue($controller, $db);
    $controller->input = new RecoveryInput(); $controller->input->payload = $payload; $controller->input->verb = $verb;
    $controller->output = new RecoveryOutput();
    $controller->roll_forward_active();
    return $controller->output;
}
$initial = array('hbl_id' => 6, 'hbl_flag' => 0, 'hbl_base_qty' => 6,
    'hbl_current_wi' => 'old', 'hbl_current_pwi' => '2623', 'hbl_current_seq' => '015',
    'hbl_source_tag_id' => 99, 'hbl_current_tag_id' => null, 'hbl_started_at' => 'historical');
$payload = array('hbl_id' => 6, 'expected_current_pwi' => '2623', 'expected_current_seq' => '015',
    'new_base_qty' => 6, 'new_current_wi' => '5100427641', 'new_current_pwi' => '2688', 'new_current_seq' => '01');
$db = new RecoveryDb(); $db->row = $initial;
$response = invokeRecovery($db, $payload);
check($response->body['success'] && $db->commits === 1 && $db->row['hbl_flag'] === 0, 'first recovery');
check($db->row['hbl_source_tag_id'] === 99 && $db->row['hbl_started_at'] === 'historical' && $db->row['hbl_current_tag_id'] === null, 'identity/history unchanged');
check((int)$db->row['hbl_base_qty'] + 10 === 16, 'first crash formula');
$payload2 = array_replace($payload, array('expected_current_pwi' => '2688', 'expected_current_seq' => '01',
    'new_base_qty' => 16, 'new_current_pwi' => '2700', 'new_current_seq' => '02'));
check(invokeRecovery($db, $payload2)->body['success'], 'second recovery');
check((int)$db->row['hbl_base_qty'] + 5 === 21, 'second crash formula');
check(!invokeRecovery($db, $payload)->body['success'] && $db->updates === 2, 'stale anchor rejected');
foreach (array('failUpdate', 'badReadback', 'failCommit') as $failure) {
    $db = new RecoveryDb(); $db->row = $initial; $db->$failure = true;
    check(!invokeRecovery($db, $payload)->body['success'] && $db->rollbacks === 1 && $db->row === $initial, $failure . ' rolls back');
}
$db = new RecoveryDb(); $db->row = array_replace($initial, array('hbl_flag' => 1));
check(!invokeRecovery($db, $payload)->body['success'] && $db->updates === 0, 'non-ACTIVE rejected');
check(invokeRecovery($db, $payload, 'GET')->status === 405, 'POST only');
check(invokeRecovery($db, array_replace($payload, array('new_base_qty' => -1)))->status === 400, 'invalid base rejected');
check(invokeRecovery($db, array_diff_key($payload, array('expected_current_seq' => true)))->status === 400, 'old anchor required');
$db = new RecoveryDb(); $db->row = $initial;
check(!invokeRecovery($db, array_replace($payload, array('new_current_pwi' => '2623', 'new_current_seq' => '015')))->body['success'], 'same anchor cannot reseed');
check(!invokeRecovery($db, array_replace($payload, array('new_current_wi' => 'wrong')))->body['success'], 'new WI/PWI validated');
$db->row = null;
check(!invokeRecovery($db, $payload)->body['success'] && $db->updates === 0, 'missing HBL rejected');
echo "PASS: roll-forward, repeated recovery, guarded rejection, rollback/readback/commit failures, unchanged history, input validation\n";
