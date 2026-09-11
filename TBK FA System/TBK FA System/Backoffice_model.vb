Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.Globalization
Imports System.Data
Imports System.Web.Script.Serialization
Imports System.IO.Ports
Imports Newtonsoft.Json.Linq
Imports System.Threading
Imports System.IO
Imports System.Net.NetworkInformation

Public Enum ContinueTagPersistenceResolution
    RetrySafe = 0
    Committed = 1
    Inconsistent = 2
    Unconfirmed = 3
End Enum

Public Class IncompleteTransferApiRecord
    Public Property TransferId As Integer
    Public Property SourceTagId As Integer
    Public Property SourceWi As String
    Public Property SourcePwi As String
    Public Property SourceSeq As String
    Public Property SourceBoxNo As Integer
    Public Property BaseQty As Integer
    Public Property CurrentWi As String
    Public Property CurrentPwi As String
    Public Property CurrentSeq As String
    Public Property CurrentBoxNo As Integer
    Public Property CurrentSnp As Integer
    Public Property GoodSnapshot As Integer
    Public Property Flag As Integer
    ' This remains Nothing until the current BOX001 tag is persisted.  A null
    ' value is valid for an ACTIVE incomplete-transfer row.
    Public Property CurrentTagId As Nullable(Of Integer)
    Public Property AlreadyActive As Boolean
End Class

' Read-only crash-recovery evidence for one exact historical Current session.
' The event summary is never used to reconstruct a partial/full tag in Phase 1.
Public Class IncompleteTransferCrashRecoveryRecord
    Public Property Transfer As IncompleteTransferApiRecord
    Public Property ServerEventCount As Long
    Public Property ServerNetQty As Long
    ' Recovery quantity is read only from the historical production detail.
    Public Property DetailQuerySucceeded As Boolean
    Public Property DetailQueryReason As String
    Public Property NetMovement As Long
    Public Property LotNo As String
    Public Property Shift As String
    Public Property CreatedDate As DateTime

    Public ReadOnly Property RecoveredQty As Long
        Get
            If Transfer Is Nothing Then Return 0
            Return CLng(Transfer.BaseQty) + NetMovement
        End Get
    End Property
End Class

Public Class Backoffice_model
    Public Shared total_nc As Integer = 0

    Public Shared Function RollForwardActiveRecovery(transfer As IncompleteTransferApiRecord, baseQty As Integer,
                                                     newWi As String, newPwi As String, newSeq As String,
                                                     ByRef reason As String) As Boolean
        reason = String.Empty
        Try
            Dim payload As New JObject From {{"hbl_id", transfer.TransferId},
                {"expected_current_pwi", transfer.CurrentPwi}, {"expected_current_seq", transfer.CurrentSeq},
                {"new_base_qty", baseQty}, {"new_current_wi", newWi},
                {"new_current_pwi", newPwi}, {"new_current_seq", newSeq}}
            Dim status As Integer = 0
            Dim raw = New api().Load_dataPOST("http://" & svApi & "/API_NEW_FA/index.php/Api_incomplete_transfer/roll_forward_active", payload, True, status)
            Dim root = JObject.Parse(raw)
            Dim success As Boolean
            Dim id, returnedBase, flag As Long
            If status >= 200 AndAlso status < 300 AndAlso
                TryReadBoolean(root("success"), success) AndAlso success AndAlso
                TryReadLongToken(root("hbl_id"), id) AndAlso id = transfer.TransferId AndAlso
                TryReadLongToken(root("base_qty"), returnedBase) AndAlso returnedBase = baseQty AndAlso
                TryReadLongToken(root("flag"), flag) AndAlso flag = 0 AndAlso
                ReadTokenText(root("current_wi")) = newWi AndAlso
                ReadTokenText(root("current_pwi")) = newPwi AndAlso
                ReadTokenText(root("current_seq")) = newSeq Then Return True
            reason = "Recovery anchor was not confirmed. " & ReadTokenText(root("message"))
        Catch ex As Exception
            reason = "Recovery anchor was not confirmed. " & ex.Message
        End Try
        Return False
    End Function

    Public Shared Function StartIncompleteTransfer(sourceTagId As Integer, currentWi As String, currentPwi As String, currentSeq As String, currentSnp As Integer, goodSnapshot As Integer, ByRef transfer As IncompleteTransferApiRecord, ByRef reason As String, ByRef definitelyNotReserved As Boolean) As Boolean
        transfer = Nothing : reason = String.Empty : definitelyNotReserved = False
        Try
            Dim payload As New JObject From {{"source_tag_id", sourceTagId}, {"current_wi", currentWi}, {"current_pwi", currentPwi}, {"current_seq", currentSeq}, {"current_snp", currentSnp}, {"good_snapshot", goodSnapshot}}
            ' Preserve a safe backend JSON error payload (for example an ACTIVE
            ' conflict) for this feature-specific operator diagnostic.  Other
            ' callers retain the existing POST helper behaviour.
            Dim httpStatusCode As Integer = 0
            Dim raw As String = New api().Load_dataPOST("http://" & svApi & "/API_NEW_FA/index.php/Api_incomplete_transfer/start", payload, True, httpStatusCode)
            Dim parsed As Boolean = ParseIncompleteTransferResponse(raw, False, transfer, reason)
            ' Only a valid 409 business rejection proves that this request did
            ' not reserve an ACTIVE transfer.  Timeout, 5xx, malformed JSON,
            ' and an invalid success payload remain ambiguous and must reconcile.
            definitelyNotReserved = Not parsed AndAlso httpStatusCode = CInt(System.Net.HttpStatusCode.Conflict) AndAlso IsValidDefinitiveStartRejection(raw)
            Return parsed
        Catch ex As Exception
            reason = "Unable to create the incomplete-transfer record. " & ex.Message
            Return False
        End Try
    End Function

    Private Shared Function IsValidDefinitiveStartRejection(raw As String) As Boolean
        If String.IsNullOrWhiteSpace(raw) Then Return False
        Try
            Dim root As JObject = JObject.Parse(raw)
            Dim successValue As Boolean
            Return TryReadBoolean(root("success"), successValue) AndAlso
                   Not successValue AndAlso
                   Not String.IsNullOrWhiteSpace(ReadTokenText(root("message")))
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function GetActiveIncompleteTransfer(currentPwi As String, currentSeq As String, ByRef hasActive As Boolean, ByRef transfer As IncompleteTransferApiRecord, ByRef reason As String) As Boolean
        hasActive = False : transfer = Nothing : reason = String.Empty
        Try
            Dim url As String = "http://" & svApi & "/API_NEW_FA/index.php/Api_incomplete_transfer/active?current_pwi=" & Uri.EscapeDataString(currentPwi) & "&current_seq=" & Uri.EscapeDataString(currentSeq)
            Dim parsed As Boolean = ParseIncompleteTransferResponse(New api().Load_data(url), True, transfer, reason, hasActive)
            WriteDebugDiagnostic("IncompleteTransfer.GET | Success=" & parsed.ToString() & " | HasActive=" & hasActive.ToString() & " | TransferId=" & If(transfer Is Nothing, "0", transfer.TransferId.ToString()))
            Return parsed
        Catch ex As Exception
            reason = "Unable to check active incomplete transfer. " & ex.Message
            Return False
        End Try
    End Function

    Public Shared Function GetActiveIncompleteTransferForLine(lineCd As String, ByRef hasActive As Boolean, ByRef transfer As IncompleteTransferApiRecord, ByRef reason As String) As Boolean
        hasActive = False : transfer = Nothing : reason = String.Empty
        If String.IsNullOrWhiteSpace(lineCd) Then
            reason = "Current line is unavailable."
            Return False
        End If
        Try
            Dim url As String = "http://" & svApi & "/API_NEW_FA/index.php/Api_incomplete_transfer/active_for_line?line_cd=" & Uri.EscapeDataString(lineCd.Trim())
            Dim parsed As Boolean = ParseIncompleteTransferResponse(New api().Load_data(url), True, transfer, reason, hasActive)
            WriteDebugDiagnostic("IncompleteTransfer.GET_LINE | Success=" & parsed.ToString() & " | HasActive=" & hasActive.ToString() & " | TransferId=" & If(transfer Is Nothing, "0", transfer.TransferId.ToString()))
            Return parsed
        Catch ex As Exception
            reason = "Unable to check active incomplete transfer for the current line. " & ex.Message
            Return False
        End Try
    End Function

    ' Read-only discovery for Crash Recovery Phase 1.  Unlike the diagnostic
    ' active_for_line endpoint, this returns every ACTIVE row deterministically
    ' and associates each only with its own historical Current WI/PWI/Seq.
    Public Shared Function GetCrashRecoveryActiveTransfersForLine(lineCd As String,
                                                                    ByRef transfers As List(Of IncompleteTransferCrashRecoveryRecord),
                                                                    ByRef reason As String) As Boolean
        transfers = New List(Of IncompleteTransferCrashRecoveryRecord)()
        reason = String.Empty
        If String.IsNullOrWhiteSpace(lineCd) Then
            reason = "Current line is unavailable."
            Return False
        End If

        Try
            Dim url As String = "http://" & svApi & "/API_NEW_FA/index.php/Api_incomplete_transfer/recovery_active_for_line?line_cd=" & Uri.EscapeDataString(lineCd.Trim())
            Dim raw As String = New api().Load_data(url)
            If String.IsNullOrWhiteSpace(raw) Then
                reason = "Crash-recovery active lookup did not respond."
                Return False
            End If

            Dim root As JObject = JObject.Parse(raw)
            Dim success As Boolean
            If Not TryReadBoolean(root("success"), success) OrElse Not success Then
                Dim fallbackHasActive As Boolean = False
                Dim fallbackTransfer As IncompleteTransferApiRecord = Nothing
                Dim fallbackReason As String = String.Empty
                If GetActiveIncompleteTransferForLine(lineCd, fallbackHasActive, fallbackTransfer, fallbackReason) Then
                    If fallbackHasActive AndAlso fallbackTransfer IsNot Nothing Then
                        transfers.Add(New IncompleteTransferCrashRecoveryRecord With {.Transfer = fallbackTransfer})
                    End If
                    Return True
                End If
                reason = ReadTokenText(root("message"))
                If String.IsNullOrWhiteSpace(reason) Then reason = "Crash-recovery active lookup failed."
                Return False
            End If

            Dim hasActive As Boolean
            If Not TryReadBoolean(root("hasActive"), hasActive) Then
                reason = "Crash-recovery active lookup returned no valid hasActive value."
                Return False
            End If
            If Not hasActive Then Return True

            Dim data As JArray = TryCast(root("data"), JArray)
            If data Is Nothing OrElse data.Count = 0 Then
                reason = "Crash-recovery active lookup reported ACTIVE records without a valid data array."
                Return False
            End If

            For Each token As JToken In data
                Dim item As JObject = TryCast(token, JObject)
                If item Is Nothing Then
                    reason = "Crash-recovery active lookup returned a malformed record."
                    Return False
                End If

                Dim transfer As IncompleteTransferApiRecord = Nothing
                Dim parseReason As String = String.Empty
                If Not TryParseIncompleteTransferData(item, transfer, parseReason) Then
                    reason = "Crash-recovery active record is invalid. " & parseReason
                    Return False
                End If
                If transfer.Flag <> 0 Then
                    reason = "Crash-recovery lookup returned a non-ACTIVE transfer."
                    Return False
                End If

                Dim eventCount As Long = 0
                Dim netQty As Long = 0
                TryReadLongToken(item("recovery_event_count"), eventCount)
                TryReadLongToken(item("recovery_net_qty"), netQty)
                Dim createdDate As DateTime
                DateTime.TryParse(ReadTokenText(item("recovery_created_date")), CultureInfo.InvariantCulture, DateTimeStyles.None, createdDate)
                transfers.Add(New IncompleteTransferCrashRecoveryRecord With {
                              .Transfer = transfer,
                              .LotNo = ReadTokenText(item("recovery_lot")),
                              .Shift = ReadTokenText(item("recovery_shift")),
                              .CreatedDate = createdDate,
                              .ServerEventCount = eventCount,
                              .ServerNetQty = netQty})
            Next
            Return True
        Catch ex As Exception
            Dim fallbackHasActive As Boolean = False
            Dim fallbackTransfer As IncompleteTransferApiRecord = Nothing
            Dim fallbackReason As String = String.Empty
            If GetActiveIncompleteTransferForLine(lineCd, fallbackHasActive, fallbackTransfer, fallbackReason) Then
                If fallbackHasActive AndAlso fallbackTransfer IsNot Nothing Then
                    transfers.Add(New IncompleteTransferCrashRecoveryRecord With {.Transfer = fallbackTransfer})
                End If
                Return True
            End If
            reason = "Unable to read crash-recovery active transfers. " & ex.GetType().Name & ": " & ex.Message
            Return False
        End Try
    End Function

    ' Intentionally narrow and read-only.  Crash recovery must use the exact
    ' historical PWI/sequence already stored by the ACTIVE HBL; it never reads
    ' act_ins and never derives a value from production_actual.
    Public Shared Function GetProductionActualDetailNetMovement(oldPwi As String,
                                                                  oldSeq As String,
                                                                  ByRef netMovement As Long,
                                                                  ByRef reason As String) As Boolean
        netMovement = 0
        reason = String.Empty
        If String.IsNullOrWhiteSpace(oldPwi) OrElse String.IsNullOrWhiteSpace(oldSeq) Then
            reason = "Historical PWI or sequence is unavailable."
            Return False
        End If

        Try
            Using connection As New SqlConnection(sqlConnect)
                connection.Open()
                Const sql As String = "SELECT COALESCE(SUM(qty), 0) FROM production_actual_detail " &
                                      "WHERE pwi_id = @pwi_id AND seq_no = @seq_no"
                Using command As New SqlCommand(sql, connection)
                    command.Parameters.Add("@pwi_id", SqlDbType.VarChar, 100).Value = oldPwi.Trim()
                    command.Parameters.Add("@seq_no", SqlDbType.VarChar, 50).Value = oldSeq.Trim()
                    Dim value As Object = command.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                        netMovement = Convert.ToInt64(value, CultureInfo.InvariantCulture)
                    End If
                End Using
            End Using
            Return True
        Catch ex As Exception
            reason = "Unable to read production detail movement. " & ex.GetType().Name & ": " & ex.Message
            Return False
        End Try
    End Function

    ' Batched quantity query for crash recovery. Executes ONE read-only SQL Server
    ' query for all distinct (pwi_id, seq_no) pairs in this recovery set.
    Public Shared Function GetProductionActualDetailNetMovementsBatch(
        pairs As List(Of Tuple(Of String, String)),
        ByRef netMovementsByPair As Dictionary(Of String, Long),
        ByRef reason As String) As Boolean

        netMovementsByPair = New Dictionary(Of String, Long)(StringComparer.OrdinalIgnoreCase)
        reason = String.Empty

        If pairs Is Nothing OrElse pairs.Count = 0 Then
            Return True
        End If

        ' Filter out invalid pairs and collect distinct valid pairs
        Dim distinctPairs As New List(Of Tuple(Of String, String))()
        Dim seenKeys As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each pair In pairs
            If pair Is Nothing OrElse String.IsNullOrWhiteSpace(pair.Item1) OrElse String.IsNullOrWhiteSpace(pair.Item2) Then
                Continue For
            End If
            Dim key As String = pair.Item1.Trim() & "|" & pair.Item2.Trim()
            If seenKeys.Add(key) Then
                distinctPairs.Add(New Tuple(Of String, String)(pair.Item1.Trim(), pair.Item2.Trim()))
            End If
        Next

        If distinctPairs.Count = 0 Then
            Return True
        End If

        Try
            Using connection As New SqlConnection(sqlConnect)
                connection.Open()

                Dim whereClauses As New List(Of String)()
                Using command As New SqlCommand()
                    command.Connection = connection
                    For i As Integer = 0 To distinctPairs.Count - 1
                        Dim pwiParam As String = "@pwi" & i.ToString(CultureInfo.InvariantCulture)
                        Dim seqParam As String = "@seq" & i.ToString(CultureInfo.InvariantCulture)
                        whereClauses.Add("(pwi_id = " & pwiParam & " AND seq_no = " & seqParam & ")")

                        command.Parameters.Add(pwiParam, SqlDbType.VarChar, 100).Value = distinctPairs(i).Item1
                        command.Parameters.Add(seqParam, SqlDbType.VarChar, 50).Value = distinctPairs(i).Item2
                    Next

                    Dim sql As String = "SELECT pwi_id, seq_no, COALESCE(SUM(qty), 0) AS net_movement " &
                                        "FROM production_actual_detail " &
                                        "WHERE " & String.Join(" OR ", whereClauses) & " " &
                                        "GROUP BY pwi_id, seq_no"
                    command.CommandText = sql

                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            Dim rPwi As String = reader("pwi_id").ToString().Trim()
                            Dim rSeq As String = reader("seq_no").ToString().Trim()
                            Dim rKey As String = rPwi & "|" & rSeq
                            Dim netVal As Long = Convert.ToInt64(reader("net_movement"), CultureInfo.InvariantCulture)
                            netMovementsByPair(rKey) = netVal
                        End While
                    End Using
                End Using
            End Using
            Return True
        Catch ex As Exception
            reason = "Unable to read batched production detail movement. " & ex.GetType().Name & ": " & ex.Message
            Return False
        End Try
    End Function

    ' The only automatic Phase-1 mutation.  The caller has already required
    ' zero server and local events for this exact historical session.
    Public Shared Function ReleaseZeroNetIncompleteTransfer(hblId As Integer,
                                                              sourceTagId As Integer,
                                                              ByRef alreadyReleased As Boolean,
                                                              ByRef reason As String) As Boolean
        alreadyReleased = False
        reason = String.Empty
        If hblId <= 0 OrElse sourceTagId <= 0 Then
            reason = "Crash-recovery release identity is invalid."
            Return False
        End If
        Try
            Dim payload As New JObject From {{"hbl_id", hblId}, {"source_tag_id", sourceTagId}, {"net_qty", 0}}
            Dim raw As String = New api().Load_dataPOST("http://" & svApi & "/API_NEW_FA/index.php/Api_incomplete_transfer/release_zero_net", payload, True)
            If String.IsNullOrWhiteSpace(raw) Then
                reason = "Zero-net release API did not respond."
                Return False
            End If
            Dim root As JObject = JObject.Parse(raw)
            Dim success As Boolean
            If Not TryReadBoolean(root("success"), success) OrElse Not success Then
                reason = ReadTokenText(root("message"))
                If String.IsNullOrWhiteSpace(reason) Then reason = "Zero-net release failed on server."
                Return False
            End If
            If Not TryReadBoolean(root("alreadyReleased"), alreadyReleased) Then
                reason = "Zero-net release API returned an invalid idempotency result."
                Return False
            End If
            Return True
        Catch ex As Exception
            reason = "Unable to release zero-net transfer. " & ex.GetType().Name & ": " & ex.Message
            Return False
        End Try
    End Function

    ' Reads every local status for one exact historical session.  A status-0,
    ' status-1 or status-2 row is still evidence that this machine recorded a
    ' production event, so Phase 1 does not infer zero net from it.
    Public Shared Function GetLocalProductionActualSummary(wi As String,
                                                            pwi As String,
                                                            seq As String,
                                                            ByRef eventCount As Long,
                                                            ByRef signedNetQty As Long,
                                                            ByRef reason As String) As Boolean
        eventCount = 0
        signedNetQty = 0
        reason = String.Empty
        Dim normalizedSeq As Integer
        If String.IsNullOrWhiteSpace(wi) OrElse String.IsNullOrWhiteSpace(pwi) OrElse
           Not Integer.TryParse(Trim(seq), NumberStyles.Integer, CultureInfo.InvariantCulture, normalizedSeq) Then
            reason = "Historical local-event identity is invalid."
            Return False
        End If
        If Not File.Exists("c:\sqlite3\FA_local_db.db3") Then
            reason = "Local production-event database is unavailable."
            Return False
        End If

        Try
            Using connection As New SQLiteConnection(sqliteConnect)
                connection.Open()
                Using command As New SQLiteCommand("SELECT COUNT(1), COALESCE(SUM(qty), 0) FROM act_ins " &
                                                   "WHERE TRIM(COALESCE(wi_plan, '')) = @wi " &
                                                   "AND TRIM(COALESCE(pwi_id, '')) = @pwi " &
                                                   "AND CAST(seq_no AS INTEGER) = @seq", connection)
                    command.Parameters.AddWithValue("@wi", wi.Trim())
                    command.Parameters.AddWithValue("@pwi", pwi.Trim())
                    command.Parameters.AddWithValue("@seq", normalizedSeq)
                    Using reader As SQLiteDataReader = command.ExecuteReader()
                        If Not reader.Read() Then
                            reason = "Local production-event summary returned no row."
                            Return False
                        End If
                        eventCount = Convert.ToInt64(reader.GetValue(0), CultureInfo.InvariantCulture)
                        signedNetQty = Convert.ToInt64(reader.GetValue(1), CultureInfo.InvariantCulture)
                    End Using
                End Using
            End Using
            Return eventCount >= 0
        Catch ex As Exception
            reason = "Unable to read local production-event summary. " & ex.GetType().Name & ": " & ex.Message
            Return False
        End Try
    End Function

    Public Shared Function CompleteIncompleteTransfer(hblId As Integer,
                                                      sourceTagId As Integer,
                                                      currentWi As String,
                                                      currentPwi As String,
                                                      currentSeq As String,
                                                      currentBoxNo As Integer,
                                                      currentSnp As Integer,
                                                      qrDetail As String,
                                                      shift As String,
                                                      flgControl As Integer,
                                                      itemCd As String,
                                                      tagGroupNo As String,
                                                      goodQty As Integer,
                                                      nextProcess As String,
                                                      ByRef resultTagId As Integer,
                                                      ByRef reason As String,
                                                      ByRef alreadyCompleted As Boolean) As Boolean
        resultTagId = 0
        reason = String.Empty
        alreadyCompleted = False

        Try
            Dim payload As New JObject From {
                {"hbl_id", hblId},
                {"source_tag_id", sourceTagId},
                {"current_wi", currentWi},
                {"current_pwi", currentPwi},
                {"current_seq", currentSeq},
                {"current_box_no", currentBoxNo},
                {"current_snp", currentSnp},
                {"qr_detail", qrDetail},
                {"shift", shift},
                {"flg_control", flgControl},
                {"item_cd", itemCd},
                {"tag_group_no", tagGroupNo},
                {"good_qty", goodQty},
                {"next_proc", nextProcess}
            }

 #If DEBUG Then
            System.Diagnostics.Debug.WriteLine("[TRANSFER] COMPLETE REQUEST | TransferId=" & hblId.ToString() &
                                               " | SourceTagId=" & sourceTagId.ToString() &
                                               " | CurrentWi=" & currentWi &
                                               " | CurrentPwi=" & currentPwi &
                                               " | CurrentSeq=" & currentSeq &
                                               " | CurrentBoxNo=" & currentBoxNo.ToString() &
                                               " | Qty=" & goodQty.ToString() &
                                               " | Snp=" & currentSnp.ToString())
 #End If
            Console.WriteLine("[TRANSFER] COMPLETE REQUEST | TransferId=" & hblId.ToString() &
                              " | SourceTagId=" & sourceTagId.ToString() &
                              " | CurrentWi=" & currentWi &
                              " | CurrentPwi=" & currentPwi &
                              " | CurrentSeq=" & currentSeq &
                              " | CurrentBoxNo=" & currentBoxNo.ToString() &
                              " | Qty=" & goodQty.ToString() &
                              " | Snp=" & currentSnp.ToString())

            Dim httpStatusCode As Integer = 0
            Dim raw As String = New api().Load_dataPOST("http://" & svApi & "/API_NEW_FA/index.php/Api_incomplete_transfer/complete", payload, True, httpStatusCode)

 #If DEBUG Then
            System.Diagnostics.Debug.WriteLine("[TRANSFER] COMPLETE RAW RESULT | HTTP status=" & httpStatusCode.ToString() & " | body=" & raw)
 #End If
            Console.WriteLine("[TRANSFER] COMPLETE RAW RESULT | HTTP status=" & httpStatusCode.ToString() & " | body=" & raw)
            If String.IsNullOrWhiteSpace(raw) Then
                reason = "Incomplete-transfer complete API did not respond."
                LogCompleteTransferResult(False, alreadyCompleted, resultTagId, reason)
                Return False
            End If

            Dim root As JObject
            Try
                root = JObject.Parse(raw)
            Catch ex As Exception
                reason = "Invalid response from incomplete-transfer complete API: " & raw
                LogCompleteTransferResult(False, alreadyCompleted, resultTagId, reason)
                Return False
            End Try

            Dim successToken = root("success")
            Dim isSuccess As Boolean = (successToken IsNot Nothing AndAlso successToken.Type = JTokenType.Boolean AndAlso CBool(successToken))

            Dim messageToken = root("message")
            If messageToken IsNot Nothing Then reason = messageToken.ToString()

            Dim alreadyCompletedToken = root("alreadyCompleted")
            If alreadyCompletedToken IsNot Nothing AndAlso alreadyCompletedToken.Type = JTokenType.Boolean Then
                alreadyCompleted = CBool(alreadyCompletedToken)
            End If

            Dim currentTagIdToken = root("current_tag_id")
            If currentTagIdToken IsNot Nothing Then
                TryReadOptionalInteger(currentTagIdToken, resultTagId)
            End If

            If isSuccess AndAlso resultTagId > 0 Then
                LogCompleteTransferResult(True, alreadyCompleted, resultTagId, reason)
                Return True
            Else
                If String.IsNullOrWhiteSpace(reason) Then reason = "Complete transfer failed on server."
                LogCompleteTransferResult(False, alreadyCompleted, resultTagId, reason)
                Return False
            End If
        Catch ex As Exception
            reason = "Unable to complete incomplete transfer. " & ex.Message
            LogCompleteTransferResult(False, alreadyCompleted, resultTagId, reason)
            Return False
        End Try
    End Function

    Private Shared Sub LogCompleteTransferResult(success As Boolean, alreadyCompleted As Boolean, currentTagId As Integer, reason As String)
#If DEBUG Then
        System.Diagnostics.Debug.WriteLine("[TRANSFER] COMPLETE RESULT | Success=" & success.ToString() &
                                           " | AlreadyComplete=" & alreadyCompleted.ToString() &
                                           " | CurrentTagId=" & currentTagId.ToString() &
                                           " | Reason=" & reason)
#End If
        Console.WriteLine("[TRANSFER] COMPLETE RESULT | Success=" & success.ToString() &
                          " | AlreadyComplete=" & alreadyCompleted.ToString() &
                          " | CurrentTagId=" & currentTagId.ToString() &
                          " | Reason=" & reason)
    End Sub

    Public Shared Function PartialIncompleteTransfer(hblId As Integer,
                                                     sourceTagId As Integer,
                                                     currentWi As String,
                                                     currentPwi As String,
                                                     currentSeq As String,
                                                     currentBoxNo As Integer,
                                                     currentSnp As Integer,
                                                     qrDetail As String,
                                                     shift As String,
                                                     itemCd As String,
                                                     tagGroupNo As String,
                                                     liveBoxQty As Integer,
                                                     nextProcess As String,
                                                     ByRef resultTagId As Integer,
                                                     ByRef reason As String,
                                                     ByRef alreadyPartial As Boolean) As Boolean
        resultTagId = 0
        reason = String.Empty
        alreadyPartial = False

        Try
            Dim payload As New JObject From {
                {"hbl_id", hblId},
                {"source_tag_id", sourceTagId},
                {"current_wi", currentWi},
                {"current_pwi", currentPwi},
                {"current_seq", currentSeq},
                {"current_box_no", currentBoxNo},
                {"current_snp", currentSnp},
                {"qr_detail", qrDetail},
                {"shift", shift},
                {"flg_control", 0},
                {"item_cd", itemCd},
                {"tag_group_no", tagGroupNo},
                {"good_qty", liveBoxQty},
                {"next_proc", nextProcess}
            }

#If DEBUG Then
            System.Diagnostics.Debug.WriteLine("[TRANSFER] PARTIAL REQUEST | TransferId=" & hblId.ToString() &
                                               " | SourceTagId=" & sourceTagId.ToString() &
                                               " | CurrentWi=" & currentWi &
                                               " | CurrentPwi=" & currentPwi &
                                               " | CurrentSeq=" & currentSeq &
                                               " | CurrentBoxNo=" & currentBoxNo.ToString() &
                                               " | Qty=" & liveBoxQty.ToString() &
                                               " | Snp=" & currentSnp.ToString())
#End If
            Console.WriteLine("[TRANSFER] PARTIAL REQUEST | TransferId=" & hblId.ToString() &
                              " | SourceTagId=" & sourceTagId.ToString() &
                              " | CurrentWi=" & currentWi &
                              " | CurrentPwi=" & currentPwi &
                              " | CurrentSeq=" & currentSeq &
                              " | CurrentBoxNo=" & currentBoxNo.ToString() &
                              " | Qty=" & liveBoxQty.ToString() &
                              " | Snp=" & currentSnp.ToString())

            Dim httpStatusCode As Integer = 0
            Dim raw As String = New api().Load_dataPOST("http://" & svApi & "/API_NEW_FA/index.php/Api_incomplete_transfer/partial", payload, True, httpStatusCode)

#If DEBUG Then
            System.Diagnostics.Debug.WriteLine("[TRANSFER] PARTIAL RAW RESULT | HTTP status=" & httpStatusCode.ToString() &
                                               " | body=" & raw)
#End If
            Console.WriteLine("[TRANSFER] PARTIAL RAW RESULT | HTTP status=" & httpStatusCode.ToString() &
                              " | body=" & raw)

            If String.IsNullOrWhiteSpace(raw) Then
                reason = "Incomplete-transfer partial API did not respond."
                LogPartialTransferResult(False, alreadyPartial, resultTagId, reason)
                Return False
            End If

            Dim root As JObject = JObject.Parse(raw)
            Dim success As Boolean
            If Not TryReadBoolean(root("success"), success) OrElse Not success Then
                reason = ReadTokenText(root("message"))
                If String.IsNullOrWhiteSpace(reason) Then reason = "Partial transfer failed on server."
                LogPartialTransferResult(False, alreadyPartial, resultTagId, reason)
                Return False
            End If

            Dim tagToken As JToken = root("current_tag_id")
            If tagToken Is Nothing OrElse Not TryReadOptionalInteger(tagToken, resultTagId) OrElse resultTagId <= 0 Then
                reason = "Partial transfer API returned no valid current tag ID."
                LogPartialTransferResult(False, alreadyPartial, resultTagId, reason)
                Return False
            End If

            Dim partialToken As JToken = root("alreadyPartial")
            If partialToken IsNot Nothing AndAlso Not TryReadBoolean(partialToken, alreadyPartial) Then
                reason = "Partial transfer API returned an invalid alreadyPartial value."
                LogPartialTransferResult(False, alreadyPartial, resultTagId, reason)
                Return False
            End If
            LogPartialTransferResult(True, alreadyPartial, resultTagId, reason)
            Return True
        Catch ex As Exception
            reason = "Unable to partially close incomplete transfer. " & ex.Message
            LogPartialTransferResult(False, alreadyPartial, resultTagId, reason)
            Return False
        End Try
    End Function

    Private Shared Sub LogPartialTransferResult(success As Boolean, alreadyPartial As Boolean, currentTagId As Integer, reason As String)
#If DEBUG Then
        System.Diagnostics.Debug.WriteLine("[TRANSFER] PARTIAL RESULT | Success=" & success.ToString() &
                                           " | AlreadyPartial=" & alreadyPartial.ToString() &
                                           " | CurrentTagId=" & currentTagId.ToString() &
                                           " | Reason=" & reason)
#End If
        Console.WriteLine("[TRANSFER] PARTIAL RESULT | Success=" & success.ToString() &
                          " | AlreadyPartial=" & alreadyPartial.ToString() &
                          " | CurrentTagId=" & currentTagId.ToString() &
                          " | Reason=" & reason)
    End Sub

    ' Both /active and /start return the same history_box_log shape.  Keep one
    ' explicit JToken parser so CodeIgniter number/string/null serialisation
    ' cannot fall through to VB late binding.
    Private Shared Function ParseIncompleteTransferResponse(raw As String, isActiveLookup As Boolean, ByRef transfer As IncompleteTransferApiRecord, ByRef reason As String, Optional ByRef hasActive As Boolean = False) As Boolean
        transfer = Nothing
        reason = String.Empty
        hasActive = False
        If String.IsNullOrWhiteSpace(raw) Then
            reason = "Incomplete-transfer API did not respond."
            Return False
        End If

        Dim root As JObject
        Try
            root = JObject.Parse(raw)
        Catch ex As Newtonsoft.Json.JsonReaderException
            reason = "Incomplete-transfer API returned invalid JSON syntax. " & ex.Message
            Return False
        Catch ex As Exception
            reason = "Incomplete-transfer client could not parse the JSON response. " & ex.Message
            Return False
        End Try

        Dim apiSuccess As Boolean
        If Not TryReadBoolean(root("success"), apiSuccess) Then
            reason = "Incomplete-transfer API response is missing a valid success value."
            Return False
        End If
        If Not apiSuccess Then
            reason = ReadTokenText(root("message"))
            If String.IsNullOrWhiteSpace(reason) Then reason = "Incomplete-transfer API reported a failure."
            Return False
        End If

        If isActiveLookup Then
            If Not TryReadBoolean(root("hasActive"), hasActive) Then
                reason = "Incomplete-transfer API response is missing a valid hasActive value."
                Return False
            End If
            If Not hasActive Then Return True
        End If

        Dim data As JObject = TryCast(root("data"), JObject)
        If data Is Nothing Then
            reason = "Incomplete-transfer API returned success but no valid transfer data."
            Return False
        End If

        If Not TryParseIncompleteTransferData(data, transfer, reason) Then Return False

        Dim alreadyActive As Boolean = False
        If Not isActiveLookup AndAlso root("alreadyActive") IsNot Nothing AndAlso Not TryReadBoolean(root("alreadyActive"), alreadyActive) Then
            reason = "Incomplete-transfer API returned an invalid alreadyActive value."
            transfer = Nothing
            Return False
        End If
        transfer.AlreadyActive = alreadyActive
        If isActiveLookup Then hasActive = True
        Return True
    End Function

    Private Shared Function TryParseIncompleteTransferData(data As JObject,
                                                            ByRef transfer As IncompleteTransferApiRecord,
                                                            ByRef reason As String) As Boolean
        transfer = Nothing
        reason = String.Empty
        Dim transferId As Integer
        Dim sourceTagId As Integer
        Dim sourceBoxNo As Integer
        Dim baseQty As Integer
        Dim currentBoxNo As Integer
        Dim currentSnp As Integer
        Dim goodSnapshot As Integer
        Dim flag As Integer
        Dim invalidField As String = String.Empty
        If Not TryReadRequiredInteger(data, "hbl_id", transferId, invalidField) OrElse
           Not TryReadRequiredInteger(data, "hbl_source_tag_id", sourceTagId, invalidField) OrElse
           Not TryReadRequiredInteger(data, "hbl_source_box_no", sourceBoxNo, invalidField) OrElse
           Not TryReadRequiredInteger(data, "hbl_base_qty", baseQty, invalidField) OrElse
           Not TryReadRequiredInteger(data, "hbl_current_box_no", currentBoxNo, invalidField) OrElse
           Not TryReadRequiredInteger(data, "hbl_current_snp", currentSnp, invalidField) OrElse
           Not TryReadRequiredInteger(data, "hbl_good_snapshot", goodSnapshot, invalidField) OrElse
           Not TryReadRequiredInteger(data, "hbl_flag", flag, invalidField) Then
            reason = "Incomplete-transfer API returned an invalid required numeric field: " & invalidField & "."
            Return False
        End If

        Dim sourceWi As String = ReadTokenText(data("hbl_source_wi"))
        Dim sourcePwi As String = ReadTokenText(data("hbl_source_pwi"))
        Dim sourceSeq As String = ReadTokenText(data("hbl_source_seq"))
        Dim currentWi As String = ReadTokenText(data("hbl_current_wi"))
        Dim currentPwi As String = ReadTokenText(data("hbl_current_pwi"))
        Dim currentSeq As String = ReadTokenText(data("hbl_current_seq"))
        If String.IsNullOrWhiteSpace(sourceWi) OrElse String.IsNullOrWhiteSpace(sourcePwi) OrElse String.IsNullOrWhiteSpace(sourceSeq) OrElse
           String.IsNullOrWhiteSpace(currentWi) OrElse String.IsNullOrWhiteSpace(currentPwi) OrElse String.IsNullOrWhiteSpace(currentSeq) Then
            reason = "Incomplete-transfer API returned a missing required identity string."
            Return False
        End If

        Dim currentTagId As Nullable(Of Integer) = Nothing
        If Not TryReadOptionalInteger(data("hbl_current_tag_id"), currentTagId) Then
            reason = "Incomplete-transfer API returned an invalid optional current tag id."
            Return False
        End If

        Dim record As New IncompleteTransferApiRecord With {
            .TransferId = transferId, .SourceTagId = sourceTagId, .SourceWi = sourceWi, .SourcePwi = sourcePwi,
            .SourceSeq = sourceSeq, .SourceBoxNo = sourceBoxNo, .BaseQty = baseQty, .CurrentWi = currentWi,
            .CurrentPwi = currentPwi, .CurrentSeq = currentSeq, .CurrentBoxNo = currentBoxNo, .CurrentSnp = currentSnp,
            .GoodSnapshot = goodSnapshot, .Flag = flag, .CurrentTagId = currentTagId}
        If record.TransferId <= 0 OrElse record.SourceTagId <= 0 OrElse record.SourceBoxNo <= 0 OrElse record.BaseQty <= 0 OrElse
           record.CurrentBoxNo <> 1 OrElse record.CurrentSnp <= 1 OrElse record.CurrentSnp = 999999 Then
            reason = "Incomplete-transfer API returned invalid transfer identity data."
            Return False
        End If

        transfer = record
        Return True
    End Function

    Private Shared Function ReadTokenText(token As JToken) As String
        If token Is Nothing OrElse token.Type = JTokenType.Null OrElse token.Type = JTokenType.Undefined Then Return String.Empty
        Return Convert.ToString(token.ToString(), CultureInfo.InvariantCulture).Trim()
    End Function

    Private Shared Function TryReadBoolean(token As JToken, ByRef value As Boolean) As Boolean
        value = False
        If token Is Nothing OrElse token.Type = JTokenType.Null OrElse token.Type = JTokenType.Undefined Then Return False
        If token.Type = JTokenType.Boolean Then
            value = token.Value(Of Boolean)()
            Return True
        End If
        Dim text As String = ReadTokenText(token)
        If Boolean.TryParse(text, value) Then Return True
        Dim numericValue As Integer
        If Integer.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, numericValue) AndAlso (numericValue = 0 OrElse numericValue = 1) Then
            value = numericValue = 1
            Return True
        End If
        Return False
    End Function

    Private Shared Function TryReadRequiredInteger(data As JObject, fieldName As String, ByRef value As Integer, ByRef invalidField As String) As Boolean
        invalidField = fieldName
        Return TryReadIntegerToken(data(fieldName), value)
    End Function

    Private Shared Function TryReadOptionalInteger(token As JToken, ByRef value As Nullable(Of Integer)) As Boolean
        value = Nothing
        If token Is Nothing OrElse token.Type = JTokenType.Null OrElse token.Type = JTokenType.Undefined Then Return True
        Dim parsedValue As Integer
        If Not TryReadIntegerToken(token, parsedValue) Then Return False
        value = parsedValue
        Return True
    End Function

    Private Shared Function TryReadIntegerToken(token As JToken, ByRef value As Integer) As Boolean
        value = 0
        If token Is Nothing OrElse token.Type = JTokenType.Null OrElse token.Type = JTokenType.Undefined Then Return False
        Return Integer.TryParse(ReadTokenText(token), NumberStyles.Integer, CultureInfo.InvariantCulture, value)
    End Function

    Private Shared Function TryReadLongToken(token As JToken, ByRef value As Long) As Boolean
        value = 0
        If token Is Nothing OrElse token.Type = JTokenType.Null OrElse token.Type = JTokenType.Undefined Then Return False
        Return Long.TryParse(ReadTokenText(token), NumberStyles.Integer, CultureInfo.InvariantCulture, value)
    End Function
    Public Shared statusTransfer As Integer = 0
    Public Shared flg_cat_layout_line As Integer = 0
    'Public Shared myConnection As New SqlConnection 'ตัวแปรสำหรับติดต่อฐานข้อมูล
    'Public Shared sqlConnect As String = "Server=192.168.161.101\PCSDBSV;Initial Catalog=tbkkfa01_dev;User ID=sa;Password=Te@m1nw;"
    Public Shared statusActionSetingMachine As Integer = 0
    Public Shared temp2Str As String
    Public Shared arr_backet_camp As List(Of String) = New List(Of String)
    Public Shared sqlConnect As String
    Public Shared qty_int As String
    Public Shared sSql As String 'ตัวแปรคำสั่ง sql
    Public Shared sqliteConnect As String = "Data Source=c:\sqlite3\FA_local_db.db3;Version=3"
    Public Shared img_user1 As Bitmap
    Public Shared img_user2 As Bitmap
    Public Shared img_user3 As Bitmap
    Public Shared img_user4 As Bitmap
    Public Shared img_user5 As Bitmap
    Public Shared img_user6 As Bitmap
    Public Shared MIN_PK_LOSS_ID As Integer = 0
    Public Shared LINE_PRODUCTION As String
    Public Shared SCANNER_PORT As String = ""
    Public Shared coles_lot_start_shift As String = ""
    Public Shared coles_lot_end_shift As String = ""
    Public Shared NEXT_PROCESS As String = ""
    Public Shared check_user As Integer = 0
    Public Shared arr_list_user As List(Of String) = New List(Of String)
    Public Shared S_chk_spec_line As String = "0"
    Public Shared start_check_date_paralell_line As String = ""
    Public Shared end_check_date_paralell_line As String = ""
    Public Shared start_master_shift As String = ""
    Public Shared date_time_start_master_shift As Date
    Public Shared date_time_end_check_date_paralell_linet As Date
    Public Shared date_time_click_start As Date
    Public Shared TimeShowBreakTime As String = ""
    Public Shared TimeStartBreakTime As String = ""
    Public Shared LossCodeAuto As String = ""
    Public Shared IDLossCodeAuto As String = ""
    Public Shared CountDelay As String = ""
    Public Shared svApi As String = ""
    Public Shared svOEE As String = ""
    Public Shared svDatabase As String = ""
    Public Shared svp_ping As String = ""
    Public Shared user_pd As String = ""
    Public Shared gobal_Flg_autoTranferProductions As Integer = 0
    Public Shared gobal_DateTimeComputerDown As String = ""
    Public Shared gobal_QTYComputerDown As String = ""
    Public Shared WithEvents serialPort As New SerialPort
    Public Shared printedTags As New List(Of String)
    Public Shared checkSqliteTrasnfer As Boolean = False
    Public Shared isRunningupdated_data_to_dbsvr As Boolean = False
    Private Shared semTransfer As New SemaphoreSlim(1, 1)
    Private Shared ReadOnly performanceLogLock As New Object()
    Private Shared performanceLogDirectoryReady As Boolean = False

    ' Release builds must not write diagnostic SQL, URLs, or timer state to the
    ' debugger/console.  Calls to this helper are compiled out in Release and
    ' have no production, database, or API side effect.
    <System.Diagnostics.Conditional("DEBUG")>
    Private Shared Sub WriteDebugDiagnostic(message As String)
        Console.WriteLine(message)
    End Sub

    ' Diagnostic-only timing.  This intentionally has no database/API side
    ' effect and excludes credentials, QR payloads and production quantities.
    Public Shared Sub LogPerformance(stage As String, elapsedMilliseconds As Long)
        Try
            Dim directoryPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs")
            Dim logPath As String = Path.Combine(directoryPath, "performance_" & DateTime.Now.ToString("yyyyMMdd") & ".log")
            Dim line As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") &
                                 " | " & If(stage, String.Empty) &
                                 " | " & Math.Max(0, elapsedMilliseconds).ToString() & " ms" & Environment.NewLine
            SyncLock performanceLogLock
                If Not performanceLogDirectoryReady Then
                    Directory.CreateDirectory(directoryPath)
                    performanceLogDirectoryReady = True
                End If
                File.AppendAllText(logPath, line)
            End SyncLock
        Catch
            ' Diagnostics must never alter the existing production fallback.
        End Try
    End Sub

    Public Shared Function GetCurrentLineTagType() As String
        Try
            Dim service As New api()
            Return If(service.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_LINE_TYPE?line_cd=" & MainFrm.Label4.Text), String.Empty).Trim()
        Catch
            Return String.Empty
        End Try
    End Function

    ' Incomplete-box selection is intentionally server-only.  The existing local
    ' database remains untouched so Start New Box and all offline flows keep their
    ' current behaviour.
    Public Shared Function GetIncompleteBoxes(currentWi As String,
                                              lineCode As String,
                                              partNo As String,
                                               snp As Integer,
                                               nextProcess As String,
                                               Optional includeLegacyPendingStatus As Boolean = False,
                                               Optional allowCrossWi As Boolean = False,
                                               Optional excludeActiveSourceOwnership As Boolean = False) As List(Of IncompleteBoxRecord)
        Dim performanceTimer As Stopwatch = Stopwatch.StartNew()
        Dim result As New List(Of IncompleteBoxRecord)()
        If String.IsNullOrWhiteSpace(partNo) OrElse snp <= 0 Then Return result

        Using connection As New SqlConnection(sqlConnect)
            connection.Open()
            Using command As SqlCommand = CreateIncompleteBoxSelectCommand(connection, Nothing, False, False, False, includeLegacyPendingStatus, allowCrossWi, excludeActiveSourceOwnership)
                command.Parameters.Add("@partNo", SqlDbType.VarChar, 100).Value = partNo.Trim()

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim candidate As IncompleteBoxRecord = MapIncompleteBox(reader)
                        WriteDebugDiagnostic("[INCOMPLETE] Candidate TagId=" & candidate.TagId.ToString() &
                                             " Flag=" & candidate.FlgControl & " Pwi=" & candidate.PwiId &
                                             " Seq=" & candidate.SeqNo & " Qty=" & candidate.Quantity.ToString() &
                                             " SQL PATH=GetIncompleteBoxes")
                        If IsCompatibleIncompleteBox(candidate, currentWi, lineCode, partNo, snp, nextProcess, allowCrossWi) Then
                            ' tag_print_detail has no SNP column.  PS_UNIT_NUMERATOR on the
                            ' historical supply row is often the default value 1, so display
                            ' and continue against the current, already-confirmed plan SNP.
                            candidate.Snp = snp
                            result.Add(candidate)
                        End If
                    End While
                End Using
            End Using
        End Using

        LogPerformance("GetIncompleteBoxes", performanceTimer.ElapsedMilliseconds)
        Return result
    End Function

    Public Shared Function RevalidateIncompleteBox(tagId As Integer,
                                                   currentWi As String,
                                                   lineCode As String,
                                                   partNo As String,
                                                   snp As Integer,
                                                   nextProcess As String,
                                                     ByRef refreshedBox As IncompleteBoxRecord,
                                                     ByRef reason As String,
                                                     Optional includeLegacyPendingStatus As Boolean = False,
                                                     Optional allowCrossWi As Boolean = False,
                                                     Optional excludeActiveSourceOwnership As Boolean = False) As Boolean
        Dim performanceTimer As Stopwatch = Stopwatch.StartNew()
        refreshedBox = Nothing
        reason = String.Empty

        If String.IsNullOrWhiteSpace(partNo) OrElse snp <= 0 Then
            reason = "Current SNP is missing or invalid. Reload the current production plan before continuing an incomplete box."
            Return False
        End If

        Try
            Dim sourceActivelyOwned As Boolean = False
            Using connection As New SqlConnection(sqlConnect)
                connection.Open()
                refreshedBox = ReadIncompleteBox(connection, Nothing, tagId, False, False, includeLegacyPendingStatus, excludeActiveSourceOwnership)
                If refreshedBox Is Nothing AndAlso excludeActiveSourceOwnership Then
                    sourceActivelyOwned = IsIncompleteSourceActivelyOwned(connection, tagId)
                End If
            End Using

            If refreshedBox Is Nothing Then
                reason = If(sourceActivelyOwned,
                            "This incomplete source is already reserved by another active transfer. Please refresh and select another box.",
                            "The selected box is no longer available. Please refresh and select again.")
                Return False
            End If

            If Not IsCompatibleIncompleteBox(refreshedBox, currentWi, lineCode, partNo, snp, nextProcess, allowCrossWi) Then
                refreshedBox = Nothing
                reason = "The selected box no longer matches the current production context."
                Return False
            End If

            refreshedBox.Snp = snp
            Return True
        Catch ex As Exception
            refreshedBox = Nothing
            reason = "Unable to validate the selected box with the server: " & ex.Message
            Return False
        Finally
            LogPerformance("RevalidateIncompleteBox", performanceTimer.ElapsedMilliseconds)
        End Try
    End Function

    ' Option A must fail closed unless the Current WI/sequence has no persisted
    ' tag rows that could collide with its prepared Current BOX001 identity.
    ' This is a read-only check against the existing server table; no API or
    ' schema is introduced here.
    Public Shared Function IsOptionACurrentSequenceFresh(currentWi As String,
                                                          currentPwiId As String,
                                                          currentSeqNo As String,
                                                          ByRef reason As String) As Boolean
        reason = String.Empty
        If String.IsNullOrWhiteSpace(currentWi) OrElse String.IsNullOrWhiteSpace(currentSeqNo) Then
            reason = "The current WI or sequence is unavailable. Continue Existing Box requires a fresh current sequence."
            Return False
        End If

        Try
            Using connection As New SqlConnection(sqlConnect)
                connection.Open()
                Dim sql As String = "SELECT COUNT(1) FROM tag_print_detail WHERE wi = @currentWi AND seq_no = @currentSeqNo"
                If Not String.IsNullOrWhiteSpace(currentPwiId) Then sql &= " AND pwi_id = @currentPwiId"

                Using command As New SqlCommand(sql, connection)
                    command.Parameters.Add("@currentWi", SqlDbType.VarChar, 50).Value = currentWi.Trim()
                    command.Parameters.Add("@currentSeqNo", SqlDbType.VarChar, 50).Value = currentSeqNo.Trim()
                    If Not String.IsNullOrWhiteSpace(currentPwiId) Then
                        command.Parameters.Add("@currentPwiId", SqlDbType.VarChar, 50).Value = currentPwiId.Trim()
                    End If

                    Dim persistedTagCount As Integer = Convert.ToInt32(command.ExecuteScalar())
                    If persistedTagCount > 0 Then
                        reason = "The current sequence already has persisted box data. Continue Existing Box can only start on a fresh current sequence."
                        Return False
                    End If
                End Using
            End Using
            Return True
        Catch ex As Exception
            reason = "The current sequence freshness could not be verified. Continue Existing Box was not started. " & ex.Message
            Return False
        End Try
    End Function

    Public Shared Function RevalidateClaimedIncompleteBox(tagId As Integer,
                                                          currentWi As String,
                                                          lineCode As String,
                                                          partNo As String,
                                                          snp As Integer,
                                                          nextProcess As String,
                                                          ByRef refreshedBox As IncompleteBoxRecord,
                                                          ByRef reason As String) As Boolean
        refreshedBox = Nothing
        reason = String.Empty

        Try
            Using connection As New SqlConnection(sqlConnect)
                connection.Open()
                refreshedBox = ReadIncompleteBox(connection, Nothing, tagId, False, True)
            End Using

            If refreshedBox Is Nothing Then
                reason = "The selected box reservation is no longer valid. Please select the box again."
                Return False
            End If

            If Not IsCompatibleIncompleteBox(refreshedBox, currentWi, lineCode, partNo, snp, nextProcess) Then
                refreshedBox = Nothing
                reason = "The selected box no longer matches the current production context."
                Return False
            End If

            refreshedBox.Snp = snp
            Return True
        Catch ex As Exception
            refreshedBox = Nothing
            reason = "Unable to validate the selected box on the server: " & ex.Message
            Return False
        End Try
    End Function

    Public Shared Function TryClaimIncompleteBox(tagId As Integer,
                                                 currentWi As String,
                                                 lineCode As String,
                                                 partNo As String,
                                                 snp As Integer,
                                                 nextProcess As String,
                                                 ByRef claimedBox As IncompleteBoxRecord,
                                                 ByRef reason As String) As Boolean
        claimedBox = Nothing
        reason = String.Empty

        Try
            Using connection As New SqlConnection(sqlConnect)
                connection.Open()
                Using transaction As SqlTransaction = connection.BeginTransaction(IsolationLevel.Serializable)
                    Dim currentBox As IncompleteBoxRecord = ReadIncompleteBox(connection, transaction, tagId, True)
                    If currentBox Is Nothing Then
                        transaction.Rollback()
                        reason = "The selected box has already been used or is no longer available."
                        Return False
                    End If

                    If Not IsCompatibleIncompleteBox(currentBox, currentWi, lineCode, partNo, snp, nextProcess) Then
                        transaction.Rollback()
                        reason = "The selected box no longer matches the current production context."
                        Return False
                    End If

                    currentBox.Snp = snp

                    Using updateCommand As New SqlCommand(
                        "UPDATE tag_print_detail " &
                        "SET flg_control = '2', updated_date = GETDATE() " &
                        "WHERE id = @tagId AND flg_control = '0'", connection, transaction)
                        updateCommand.Parameters.Add("@tagId", SqlDbType.Int).Value = tagId
                        If updateCommand.ExecuteNonQuery() <> 1 Then
                            transaction.Rollback()
                            reason = "The selected box was taken by another terminal. Please select again."
                            Return False
                        End If
                    End Using

                    transaction.Commit()
                    claimedBox = currentBox
                    Return True
                End Using
            End Using
        Catch ex As Exception
            claimedBox = Nothing
            reason = "Unable to reserve the selected box on the server: " & ex.Message
            Return False
        End Try
    End Function

    Public Shared Function ReleaseIncompleteBoxClaim(tagId As Integer) As Boolean
        If tagId <= 0 Then Return False

        Using connection As New SqlConnection(sqlConnect)
            connection.Open()
            Using command As New SqlCommand(
                "UPDATE tag_print_detail " &
                "SET flg_control = '0', updated_date = GETDATE() " &
                "WHERE id = @tagId AND flg_control = '2'", connection)
                command.Parameters.Add("@tagId", SqlDbType.Int).Value = tagId
                Return command.ExecuteNonQuery() = 1
            End Using
        End Using
    End Function

    ' Marks only the Continue-selected tag as completed.  This must never update
    ' by WI because a newly-created incomplete tag for the same WI may coexist.
    Public Shared Function CompleteClaimedIncompleteBox(tagId As Integer) As Boolean
        If tagId <= 0 Then Return False

        Using connection As New SqlConnection(sqlConnect)
            connection.Open()
            Using command As New SqlCommand(
                "UPDATE tag_print_detail " &
                "SET flg_control = '1', updated_date = GETDATE() " &
                "WHERE id = @tagId AND flg_control = '2'", connection)
                command.Parameters.Add("@tagId", SqlDbType.Int).Value = tagId
                Return command.ExecuteNonQuery() = 1
            End Using
        End Using
    End Function

    ' Continue Existing Box does not claim flg_control = 2.  Legacy printing may
    ' use that value for a WI-wide transition, so a selected pending tag is
    ' completed by its immutable tag ID only after the replacement full tag was
    ' inserted successfully.
    Public Shared Function CompleteSelectedIncompleteBox(tagId As Integer) As Boolean
        If tagId <= 0 Then Return False

        Using connection As New SqlConnection(sqlConnect)
            connection.Open()
            Using command As New SqlCommand(
                "UPDATE tag_print_detail " &
                "SET flg_control = '1', updated_date = GETDATE() " &
                "WHERE id = @tagId AND flg_control IN ('0','2')", connection)
                command.Parameters.Add("@tagId", SqlDbType.Int).Value = tagId
                Return command.ExecuteNonQuery() = 1
            End Using
        End Using
    End Function

    ' Continue-only replacement of one selected incomplete tag.  The source
    ' validation, replacement insert and source completion share one SQL Server
    ' transaction so a partial Close Lot can never commit two pending records.
    Public Shared Function ReplaceSelectedIncompleteBoxAtomic(oldTagId As Integer,
                                                               sourceWi As String,
                                                               currentWi As String,
                                                               currentSnp As Integer,
                                                               qrDetail As String,
                                                               sourceBoxNo As Integer,
                                                               currentBoxNo As Integer,
                                                               printCount As Integer,
                                                               seqNo As String,
                                                               shift As String,
                                                               replacementFlgControl As Integer,
                                                               itemCd As String,
                                                               pwiId As String,
                                                               tagGroupNo As String,
                                                               goodQty As Integer,
                                                               nextProcess As String,
                                                               ByRef attemptedNewTagId As Integer) As Integer
        If oldTagId <= 0 OrElse String.IsNullOrWhiteSpace(sourceWi) OrElse String.IsNullOrWhiteSpace(currentWi) OrElse currentSnp <= 0 OrElse
             String.IsNullOrWhiteSpace(qrDetail) OrElse sourceBoxNo <= 0 OrElse currentBoxNo <= 0 Then Return 0

        Dim insertedTagId As Integer = 0
        Dim committed As Boolean = False
        attemptedNewTagId = 0
        Dim createdDate As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")

        Try
            Using connection As New SqlConnection(sqlConnect)
                connection.Open()
                Using transaction As SqlTransaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)
                    Try
                        Using validateCommand As New SqlCommand(
                            "SELECT COUNT(1) FROM tag_print_detail t WITH (UPDLOCK, ROWLOCK) " &
                            "INNER JOIN production_working_info pwi ON pwi.pwi_id = t.pwi_id " &
                            "INNER JOIN sup_work_plan_supply_dev sw ON sw.IND_ROW = pwi.ind_row " &
                            "WHERE t.id = @oldTagId AND t.wi = @sourceWi " &
                             "AND t.box_no = @sourceBoxNo AND t.flg_control IN ('0','2') " &
                            "AND NULLIF(LTRIM(RTRIM(sw.ITEM_CD)), '') IS NOT NULL " &
                            "AND UPPER(LTRIM(RTRIM(sw.ITEM_CD))) = UPPER(@itemCd) " &
                            "AND TRY_CONVERT(INT, LTRIM(RTRIM(SUBSTRING(t.qr_detail, 53, 6)))) > 0 " &
                            "AND TRY_CONVERT(INT, LTRIM(RTRIM(SUBSTRING(t.qr_detail, 53, 6)))) < @currentSnp", connection, transaction)
                            validateCommand.Parameters.Add("@oldTagId", SqlDbType.Int).Value = oldTagId
                            validateCommand.Parameters.Add("@sourceWi", SqlDbType.VarChar, 100).Value = sourceWi.Trim()
                            validateCommand.Parameters.Add("@sourceBoxNo", SqlDbType.Int).Value = sourceBoxNo
                            validateCommand.Parameters.Add("@itemCd", SqlDbType.VarChar, 100).Value = itemCd.Trim()
                            validateCommand.Parameters.Add("@currentSnp", SqlDbType.Int).Value = currentSnp
                            If Convert.ToInt32(validateCommand.ExecuteScalar()) <> 1 Then
                                transaction.Rollback()
                                Return 0
                            End If
                        End Using

                        ' Idempotency check: verify whether replacement tag for this exact (PWI, Seq, CurrentBoxNo) already exists
                        If Not String.IsNullOrWhiteSpace(Trim(pwiId)) AndAlso currentBoxNo > 0 Then
                            Using checkExistingCmd As New SqlCommand(
                                "SELECT TOP 1 id FROM tag_print_detail WITH (UPDLOCK, ROWLOCK) " &
                                "WHERE pwi_id = @check_pwi_id AND (seq_no = @check_seq_no OR TRY_CONVERT(INT, seq_no) = TRY_CONVERT(INT, @check_seq_no)) " &
                                "AND box_no = @check_box_no AND flg_control IN ('0','1','2') ORDER BY id DESC;", connection, transaction)
                                checkExistingCmd.Parameters.Add("@check_pwi_id", SqlDbType.VarChar, 100).Value = Trim(pwiId)
                                checkExistingCmd.Parameters.Add("@check_seq_no", SqlDbType.VarChar, 50).Value = If(seqNo, String.Empty).Trim()
                                checkExistingCmd.Parameters.Add("@check_box_no", SqlDbType.Int).Value = currentBoxNo
                                Dim existingIdObj As Object = checkExistingCmd.ExecuteScalar()
                                If existingIdObj IsNot Nothing AndAlso Not IsDBNull(existingIdObj) Then
                                    Dim existingId As Integer = Convert.ToInt32(existingIdObj)
                                    If existingId > 0 Then
                                        attemptedNewTagId = existingId
                                        Using completeExistingSourceCommand As New SqlCommand(
                                            "UPDATE tag_print_detail SET flg_control = '1', updated_date = GETDATE() " &
                                            "WHERE id = @oldTagId AND wi = @sourceWi AND box_no = @sourceBoxNo " &
                                            "AND flg_control IN ('0','2')", connection, transaction)
                                            completeExistingSourceCommand.Parameters.Add("@oldTagId", SqlDbType.Int).Value = oldTagId
                                            completeExistingSourceCommand.Parameters.Add("@sourceWi", SqlDbType.VarChar, 100).Value = sourceWi.Trim()
                                            completeExistingSourceCommand.Parameters.Add("@sourceBoxNo", SqlDbType.Int).Value = sourceBoxNo
                                            completeExistingSourceCommand.ExecuteNonQuery()
                                        End Using
                                        transaction.Commit()
                                        committed = True
                                        Return existingId
                                    End If
                                End If
                            End Using
                        End If

                        Using insertCommand As New SqlCommand(
                            "INSERT INTO tag_print_detail " &
                            "(wi, qr_detail, box_no, print_count, created_date, updated_date, seq_no, shift, next_proc, flg_control, pwi_id, tag_group_no) " &
                            "VALUES (@wi, @qrDetail, @boxNo, @printCount, @createdDate, @updatedDate, @seqNo, @shift, @nextProcess, @flgControl, @pwiId, @tagGroupNo); " &
                            "SELECT SCOPE_IDENTITY();", connection, transaction)
                            insertCommand.Parameters.Add("@wi", SqlDbType.VarChar, 100).Value = currentWi.Trim()
                            insertCommand.Parameters.Add("@qrDetail", SqlDbType.VarChar, -1).Value = qrDetail
                            insertCommand.Parameters.Add("@boxNo", SqlDbType.Int).Value = currentBoxNo
                            insertCommand.Parameters.Add("@printCount", SqlDbType.Int).Value = printCount
                            insertCommand.Parameters.Add("@createdDate", SqlDbType.VarChar, 30).Value = createdDate
                            insertCommand.Parameters.Add("@updatedDate", SqlDbType.VarChar, 30).Value = createdDate
                            insertCommand.Parameters.Add("@seqNo", SqlDbType.VarChar, 50).Value = If(seqNo, String.Empty)
                            insertCommand.Parameters.Add("@shift", SqlDbType.VarChar, 50).Value = If(shift, String.Empty)
                            insertCommand.Parameters.Add("@nextProcess", SqlDbType.VarChar, 100).Value = If(nextProcess, String.Empty)
                            insertCommand.Parameters.Add("@flgControl", SqlDbType.Int).Value = replacementFlgControl
                            insertCommand.Parameters.Add("@pwiId", SqlDbType.VarChar, 100).Value = If(pwiId, String.Empty)
                            insertCommand.Parameters.Add("@tagGroupNo", SqlDbType.VarChar, 50).Value = "1"
                            insertedTagId = Convert.ToInt32(insertCommand.ExecuteScalar())
                            ' Keep this identity even if a later COMMIT response is
                            ' ambiguous.  Retry code must reconcile it first, never
                            ' blindly insert a second replacement tag.
                            attemptedNewTagId = insertedTagId
                        End Using

                        If insertedTagId <= 0 Then Throw New DBConcurrencyException("Replacement tag insert did not return an ID.")

                        Using completeCommand As New SqlCommand(
                            "UPDATE tag_print_detail SET flg_control = '1', updated_date = GETDATE() " &
                             "WHERE id = @oldTagId AND wi = @sourceWi AND box_no = @sourceBoxNo " &
                            "AND flg_control IN ('0','2')", connection, transaction)
                            completeCommand.Parameters.Add("@oldTagId", SqlDbType.Int).Value = oldTagId
                            completeCommand.Parameters.Add("@sourceWi", SqlDbType.VarChar, 100).Value = sourceWi.Trim()
                            completeCommand.Parameters.Add("@sourceBoxNo", SqlDbType.Int).Value = sourceBoxNo
                            If completeCommand.ExecuteNonQuery() <> 1 Then
                                Throw New DBConcurrencyException("The selected source tag changed before replacement commit.")
                            End If
                        End Using

                        transaction.Commit()
                        committed = True
                    Catch
                        Try
                            transaction.Rollback()
                        Catch
                        End Try
                        Throw
                    End Try
                End Using
            End Using
        Catch
            Return 0
        End Try

        If committed Then
            ' Mirror only a committed server record.  tr_status=1 prevents the
            ' local transfer queue from inserting the replacement a second time.
            model_api_sqlite.mas_Insert_tag_print(currentWi, qrDetail, currentBoxNo, printCount,
                                                  seqNo, shift, replacementFlgControl, itemCd,
                                                  pwiId, tagGroupNo, goodQty, nextProcess, "1", True)
        End If
        Return insertedTagId
    End Function

    ' Reads the Continue-specific transaction result without changing server state.
    ' A retry is safe only when the selected source is still pending and there is no
    ' matching replacement record.  Any mixed state is deliberately treated as a
    ' data-integrity stop rather than guessed at by the client.
    Public Shared Function ReconcileSelectedIncompleteBoxReplacement(oldTagId As Integer,
                                                                       attemptedNewTagId As Integer,
                                                                       sourceWi As String,
                                                                       currentWi As String,
                                                                       sourceBoxNo As Integer,
                                                                       currentBoxNo As Integer,
                                                                       qrDetail As String) As ContinueTagPersistenceResolution
        If oldTagId <= 0 OrElse String.IsNullOrWhiteSpace(sourceWi) OrElse String.IsNullOrWhiteSpace(currentWi) OrElse sourceBoxNo <= 0 OrElse currentBoxNo <= 0 OrElse String.IsNullOrWhiteSpace(qrDetail) Then
            Return ContinueTagPersistenceResolution.Unconfirmed
        End If

        Try
            Using connection As New SqlConnection(sqlConnect)
                connection.Open()

                Dim sourceStatus As String = Nothing
                Using sourceCommand As New SqlCommand("SELECT flg_control FROM tag_print_detail WHERE id = @oldTagId AND wi = @sourceWi AND box_no = @sourceBoxNo", connection)
                    sourceCommand.Parameters.Add("@oldTagId", SqlDbType.Int).Value = oldTagId
                    sourceCommand.Parameters.Add("@sourceWi", SqlDbType.VarChar, 100).Value = sourceWi.Trim()
                    sourceCommand.Parameters.Add("@sourceBoxNo", SqlDbType.Int).Value = sourceBoxNo
                    Dim value As Object = sourceCommand.ExecuteScalar()
                    If value Is Nothing OrElse value Is DBNull.Value Then Return ContinueTagPersistenceResolution.Unconfirmed
                    sourceStatus = value.ToString().Trim()
                End Using

                Dim replacementExists As Boolean = False
                Dim replacementMatches As Boolean = False
                Dim replacementId As Integer = attemptedNewTagId
                Using replacementCommand As New SqlCommand(
                    If(attemptedNewTagId > 0,
                       "SELECT id, wi, box_no, qr_detail FROM tag_print_detail WHERE id = @attemptedNewTagId",
                       "SELECT TOP 1 id, wi, box_no, qr_detail FROM tag_print_detail WHERE wi = @currentWi AND box_no = @currentBoxNo AND qr_detail = @qrDetail AND id <> @oldTagId ORDER BY id DESC"), connection)
                    If attemptedNewTagId > 0 Then
                        replacementCommand.Parameters.Add("@attemptedNewTagId", SqlDbType.Int).Value = attemptedNewTagId
                    Else
                        replacementCommand.Parameters.Add("@currentWi", SqlDbType.VarChar, 100).Value = currentWi.Trim()
                        replacementCommand.Parameters.Add("@currentBoxNo", SqlDbType.Int).Value = currentBoxNo
                        replacementCommand.Parameters.Add("@qrDetail", SqlDbType.VarChar, -1).Value = qrDetail
                        replacementCommand.Parameters.Add("@oldTagId", SqlDbType.Int).Value = oldTagId
                    End If

                    Using reader As SqlDataReader = replacementCommand.ExecuteReader(CommandBehavior.SingleRow)
                        If reader.Read() Then
                            replacementExists = True
                            replacementId = Convert.ToInt32(reader("id"))
                            replacementMatches = String.Equals(DbString(reader, "wi").Trim(), currentWi.Trim(), StringComparison.OrdinalIgnoreCase) AndAlso
                                                  DbInteger(reader, "box_no") = currentBoxNo AndAlso
                                                 String.Equals(DbString(reader, "qr_detail"), qrDetail, StringComparison.Ordinal)
                        End If
                    End Using
                End Using

                Dim sourcePending As Boolean = sourceStatus = "0" OrElse sourceStatus = "2"
                Dim sourceCompleted As Boolean = sourceStatus = "1"

                If sourceCompleted AndAlso replacementExists AndAlso replacementMatches Then
                    Return ContinueTagPersistenceResolution.Committed
                End If
                If sourcePending AndAlso Not replacementExists Then
                    Return ContinueTagPersistenceResolution.RetrySafe
                End If

                Return ContinueTagPersistenceResolution.Inconsistent
            End Using
        Catch
            Return ContinueTagPersistenceResolution.Unconfirmed
        End Try
    End Function

    Public Shared Function IncrementIncompleteTagPrintCount(tagId As Integer) As Boolean
        If tagId <= 0 Then Return False

        Using connection As New SqlConnection(sqlConnect)
            connection.Open()
            Using transaction As SqlTransaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)
                Using command As New SqlCommand(
                    "UPDATE tag_print_detail " &
                    "SET print_count = ISNULL(print_count, 0) + 1, updated_date = GETDATE() " &
                    "WHERE id = @tagId AND flg_control = '0'", connection, transaction)
                    command.Parameters.Add("@tagId", SqlDbType.Int).Value = tagId
                    Dim updated As Boolean = command.ExecuteNonQuery() = 1
                    If updated Then
                        transaction.Commit()
                    Else
                        transaction.Rollback()
                    End If
                    Return updated
                End Using
            End Using
        End Using
    End Function

    Private Shared Function ReadIncompleteBox(connection As SqlConnection,
                                              transaction As SqlTransaction,
                                              tagId As Integer,
                                              lockForUpdate As Boolean,
                                              Optional claimed As Boolean = False,
                                              Optional includeLegacyPendingStatus As Boolean = False,
                                              Optional excludeActiveSourceOwnership As Boolean = False) As IncompleteBoxRecord
        Using command As SqlCommand = CreateIncompleteBoxSelectCommand(connection, transaction, lockForUpdate, True, claimed, includeLegacyPendingStatus, False, excludeActiveSourceOwnership)
            command.Parameters.Add("@tagId", SqlDbType.Int).Value = tagId
            Using reader As SqlDataReader = command.ExecuteReader(CommandBehavior.SingleRow)
                If reader.Read() Then Return MapIncompleteBox(reader)
            End Using
        End Using
        Return Nothing
    End Function

    Private Shared Function CreateIncompleteBoxSelectCommand(connection As SqlConnection,
                                                             transaction As SqlTransaction,
                                                             lockForUpdate As Boolean,
                                                              selectById As Boolean,
                                                               Optional claimed As Boolean = False,
                                                              Optional includeLegacyPendingStatus As Boolean = False,
                                                              Optional allowCrossWi As Boolean = False,
                                                              Optional excludeActiveSourceOwnership As Boolean = False) As SqlCommand
        Dim lockHint As String = If(lockForUpdate, " WITH (UPDLOCK, ROWLOCK)", String.Empty)
        Dim filter As String
        If selectById Then
            filter = "t.id = @tagId"
        Else
            filter = "NULLIF(LTRIM(RTRIM(sw.ITEM_CD)), '') IS NOT NULL " &
                     "AND UPPER(LTRIM(RTRIM(sw.ITEM_CD))) = UPPER(@partNo)"
            If Not allowCrossWi Then filter &= " AND t.wi = @currentWi"
        End If

        Dim sql As String =
            "SELECT t.id, t.wi, t.pwi_id, t.box_no, t.seq_no, t.shift, t.flg_control, " &
            "t.next_proc, t.qr_detail, t.created_date, t.print_count, " &
            "pwi.pwi_lot_no, sw.LINE_CD, sw.ITEM_CD, sw.ITEM_NAME, " &
            "sw.MODEL, sw.PS_UNIT_NUMERATOR " &
            "FROM tag_print_detail t" & lockHint & " " &
            "INNER JOIN production_working_info pwi ON pwi.pwi_id = t.pwi_id " &
            "INNER JOIN sup_work_plan_supply_dev sw ON sw.IND_ROW = pwi.ind_row " &
            "WHERE " & If(claimed,
                            "t.flg_control = '2'",
                            If(includeLegacyPendingStatus, "t.flg_control IN ('0','2')", "t.flg_control = '0'")) &
            " AND sw.LVL = '1' AND " & filter & If(excludeActiveSourceOwnership,
            " AND NOT EXISTS (SELECT 1 FROM history_box_log hbl WHERE hbl.hbl_source_tag_id = t.id AND hbl.hbl_flag = 0)", String.Empty) & " " &
            "ORDER BY t.created_date DESC, t.id DESC"

        Return New SqlCommand(sql, connection, transaction)
    End Function

    ' Read-only ownership check used only to give a stale Continue selection a
    ' precise operator message. history_box_log remains server-authoritative.
    Private Shared Function IsIncompleteSourceActivelyOwned(connection As SqlConnection, tagId As Integer) As Boolean
        Using command As New SqlCommand("SELECT TOP 1 1 FROM history_box_log WHERE hbl_source_tag_id = @tagId AND hbl_flag = 0", connection)
            command.Parameters.Add("@tagId", SqlDbType.Int).Value = tagId
            Return command.ExecuteScalar() IsNot Nothing
        End Using
    End Function

    Private Shared Function MapIncompleteBox(reader As SqlDataReader) As IncompleteBoxRecord
        Dim qrDetail As String = DbString(reader, "qr_detail")
        Dim box As New IncompleteBoxRecord With {
            .TagId = DbInteger(reader, "id"),
            .Wi = DbString(reader, "wi"),
            .PwiId = DbString(reader, "pwi_id"),
            .LineCode = DbString(reader, "LINE_CD"),
            .PartNo = DbString(reader, "ITEM_CD"),
            .PartName = DbString(reader, "ITEM_NAME"),
            .Model = DbString(reader, "MODEL"),
            .LotNo = DbString(reader, "pwi_lot_no"),
            .SeqNo = DbString(reader, "seq_no"),
            .FlgControl = DbString(reader, "flg_control"),
            .BoxNo = DbInteger(reader, "box_no"),
            .Quantity = ParseNormalTagQuantity(qrDetail),
            .Snp = DbInteger(reader, "PS_UNIT_NUMERATOR"),
            .Shift = DbString(reader, "shift"),
            .NextProcess = DbString(reader, "next_proc"),
            .QrDetail = qrDetail,
            .PrintCount = DbInteger(reader, "print_count")
        }

        Dim createdValue As Object = reader("created_date")
        If createdValue IsNot DBNull.Value Then
            Dim createdDate As DateTime
            If DateTime.TryParse(createdValue.ToString(), createdDate) Then box.CreatedDate = createdDate
        End If
        Return box
    End Function

    Private Shared Function IsCompatibleIncompleteBox(box As IncompleteBoxRecord,
                                                      currentWi As String,
                                                      lineCode As String,
                                                       partNo As String,
                                                       snp As Integer,
                                                       nextProcess As String,
                                                       Optional allowCrossWi As Boolean = False) As Boolean
        If box Is Nothing OrElse box.TagId <= 0 Then Return False
        If Not allowCrossWi AndAlso Not String.Equals(box.Wi.Trim(), currentWi.Trim(), StringComparison.OrdinalIgnoreCase) Then Return False
        If Not String.Equals(box.PartNo.Trim(), partNo.Trim(), StringComparison.OrdinalIgnoreCase) Then Return False
        ' Incomplete tags do not persist their own SNP.  Do not compare against
        ' sw.PS_UNIT_NUMERATOR here: for legacy rows it is commonly 1.  The
        ' selected line/part/next-process and the current plan's SNP remain the
        ' compatibility boundary, and the tag quantity must fit that SNP.
        If snp <= 1 OrElse snp = 999999 Then Return False
        If box.Quantity <= 0 OrElse box.Quantity >= snp Then Return False

        'Legacy reprint retains its original context check. Continue Existing Box
        'uses the approved part-only cross-WI rule, so Source next-process is not a gate.
        If Not allowCrossWi Then
            Dim expectedNextProcess As String = If(nextProcess, String.Empty).Trim()
            Dim boxNextProcess As String = If(box.NextProcess, String.Empty).Trim()
            If expectedNextProcess.Length > 0 AndAlso
               Not String.Equals(boxNextProcess, expectedNextProcess, StringComparison.OrdinalIgnoreCase) Then Return False
        End If

        Return True
    End Function

    Private Shared Function ParseNormalTagQuantity(qrDetail As String) As Integer
        If String.IsNullOrEmpty(qrDetail) OrElse qrDetail.Length < 58 Then Return 0
        Dim value As Integer
        If Integer.TryParse(qrDetail.Substring(52, 6).Trim(), value) Then Return value
        Return 0
    End Function

    Private Shared Function DbString(reader As SqlDataReader, columnName As String) As String
        Dim value As Object = reader(columnName)
        Return If(value Is DBNull.Value, String.Empty, value.ToString())
    End Function

    Private Shared Function DbInteger(reader As SqlDataReader, columnName As String) As Integer
        Dim value As Integer
        Integer.TryParse(DbString(reader, columnName), value)
        Return value
    End Function
    Public Shared Async Function CheckSingnalNetwork() As Task(Of Boolean)
        Dim targetHost As String = svp_ping
        Dim sw As New Stopwatch()
        sw.Start()

        ' ลด timeout และจำนวนครั้ง
        Dim isStable As Boolean = Await IsNetworkStableAsync(targetHost, 2, 300)
        sw.Stop()

        Dim elapsedSec As Double = sw.Elapsed.TotalSeconds

        If isStable Then
            'Console.WriteLine("check Network : " & $"✅ Network เสถียร พร้อมใช้งาน{vbCrLf}⏱ เวลา: {elapsedSec:F2} วินาที")
            Return True
        Else
            'Console.WriteLine($"⚠️ Network ไม่เสถียร หรือเชื่อมต่อไม่ได้{vbCrLf}⏱ เวลา: {elapsedSec:F2} วินาที")
            Return False
        End If
    End Function
    Public Shared Async Function IsNetworkStableAsync(host As String, Optional attempts As Integer = 2, Optional timeout As Integer = 300) As Task(Of Boolean)
        Dim successCount As Integer = 0
        Try
            Dim pingSender As New Ping()
            For i As Integer = 1 To attempts
                Dim reply As PingReply = Await pingSender.SendPingAsync(host, timeout)
                If reply.Status = IPStatus.Success Then
                    successCount += 1
                End If
                Await Task.Delay(50) ' ลด delay เหลือ 50ms
            Next

            Dim successRate As Double = successCount / attempts
            Return successRate >= 0.5 ' ใช้เกณฑ์ 50% ก็พอ
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function sqlite_conn_dbsv()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Check_connect_sqlite()
        Clear_sqlite()
        Try
            sqliteConn.Open()
            Dim temp_stre As String = ""
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select * from db_svr_info"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            While LoadSQL.Read()
                'temp2Str = "test"
                'temp_stre = "Server=" & LoadSQL("ipaddress").ToString() & ";Initial Catalog=" & LoadSQL("db_name").ToString() & ";User ID=" & LoadSQL("username").ToString() & ";Password=" & LoadSQL("passwd").ToString() & ";"
                ' temp_stre = "Server=0.tcp.ap.ngrok.io,13414;Initial Catalog=gemba_db;User ID=sa;Password=Te@m1nw;"
                '''Console.WriteLine("Server=0.tcp.ap.ngrok.io,13414;Initial Catalog=gemba_db;User ID=sa;Password=Te@m1nw;")
                temp_stre = "Server=" & LoadSQL("ipaddress").ToString() & ";Initial Catalog=" & LoadSQL("db_name").ToString() & ";User ID=" & LoadSQL("username").ToString() & ";Password=" & LoadSQL("passwd").ToString() & ";"
                '   ''Console.WriteLine(temp_stre)
                svDatabase = LoadSQL("ip_database").ToString()
            End While
            sqlConnect = temp_stre
            '  model_api_sqlite.UpdateStatus_tag_print_detail()
            Return temp2Str
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function sqlite_conn_dbsv]")
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function OpenRS232(mec_name)
        If serialPort.IsOpen Then
            CloseRS232()
        End If
        serialPort = New SerialPort(mec_name, 9600, Parity.None, 8, StopBits.One)
        serialPort.Open()
        serialPort.RtsEnable = True
        Return serialPort
    End Function
    Public Shared Sub CloseRS232()
        serialPort.Close()
    End Sub
    Public Shared Function GetTimeAutoBreakTime(lineCd As String, shift As String)
        Dim result As String = ""
        Try
            Dim api = New api()
            ' Fetch data from the API
            Dim GetData = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GetTimeAutoBreakTime?lineCd=" & MainFrm.Label4.Text & "&shift=" & shift)
            ' 'Console.WriteLine("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GetTimeAutoBreakTime?lineCd=" & MainFrm.Label4.Text & "&shift=" & shift)
            If GetData <> "0" Then
                ' Deserialize JSON response into a list of objects
                Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(GetData)
                ' Loop through each item in the deserialized data
                For Each item As Object In dcResultdata
                    result = item("sltc_show_time").ToString()
                    TimeShowBreakTime = result
                    LossCodeAuto = item("sltc_loss_cd").ToString()
                    IDLossCodeAuto = item("id").ToString()
                    CountDelay = item("sltc_rec_time").ToString()
                Next
            End If
        Catch ex As Exception
            ' Handle exceptions
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function GetTimeAutoBreakTime]" & ex.Message)
        End Try
        Return result
    End Function
    Public Shared Function GET_START_END_PRODUCTION_DETAIL_SPECTAIL_TIME(pwi_id As String)
        Dim api = New api()
        Dim rs = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_START_END_PRODUCTION_DETAIL_SPECTAIL_TIME?pwi_id=" & pwi_id)
        Return rs
    End Function
    Public Shared Sub GetLocalServerAPI()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Check_connect_sqlite()
        Clear_sqlite()
        Try
            sqliteConn.Open()
            Dim sva_ip_address As String = ""
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "Select * from  Server_API"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            While LoadSQL.Read()
                sva_ip_address = LoadSQL("sva_ip_address").ToString()
            End While
            svApi = sva_ip_address
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function GetLocalServerAPI]")
            sqliteConn.Close()
        End Try
    End Sub
    Public Shared Sub GetLocalServerOEE()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Check_connect_sqlite()
        Clear_sqlite()
        Try
            sqliteConn.Open()
            Dim svo_ip_address As String = ""
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select * from  Server_OEE"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            While LoadSQL.Read()
                svo_ip_address = LoadSQL("svo_ipaddress_and_port").ToString()
            End While
            svOEE = svo_ip_address
            'svOEE = "192.168.161.78:3000"
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function GetLocalServerOEE]")
            sqliteConn.Close()
        End Try
    End Sub
    Public Shared Sub GetLocalServerping()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Check_connect_sqlite()
        Clear_sqlite()
        Try
            sqliteConn.Open()
            Dim tmpsvp_ping As String = ""
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select * from  Server_ping"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            While LoadSQL.Read()
                tmpsvp_ping = LoadSQL("svp_ip_address").ToString()
            End While
            svp_ping = tmpsvp_ping
            'svp_ping = "192.168.161.101"
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function svp_ping]")
            sqliteConn.Close()
        End Try
    End Sub
    Public Shared Function checkTransection(pwi_id As String, number_qty As String, DateTime As String)
        Dim api = New api()
        Dim rs = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/CheckTrancetion?pwi_id=" & pwi_id & "&number_qty=" & number_qty & "&st_time=" & DateTime)
        Return rs
    End Function
    Public Shared Function Get_PD_CONFIG(line As String)
        Dim api = New api()
        Dim rs = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_PD_CONFIG?line_cd=" & line)
        Return rs
    End Function
    Public Shared Function ILogLossBreakTime(lineCd As String, wi As String, seq As String)
        Dim api = New api()
        Dim GetData = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/InsertLogLoss?lineCd=" & MainFrm.Label4.Text & "&wi=" & wi & "&seq=" & seq)
        Return GetData
    End Function
    Public Shared Function B_master_device()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
        Catch ex As Exception
            sqliteConn.Close()
            sqliteConn.Open()
        End Try
        Try
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select * from catagory_counter where count_flg='1'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            sqliteConn.Close()
            Return LoadSQL
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function Clear_sqlite]" & ex.Message)
            sqliteConn.Dispose()
            'sqliteConn.Close()
            sqliteConn = Nothing
        End Try
    End Function
    Public Shared Function load_config_master_database()
        Dim api = New api()
        Dim check_tag_type = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/JOIN_CHECK_LINE_MASTER?line_cd=" & MainFrm.Label4.Text)
        ''Console.WriteLine("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/JOIN_CHECK_LINE_MASTER?line_cd=" & MainFrm.Label4.Text)
        Return check_tag_type
    End Function
    Public Shared Function F_NEXT_PROCESS(ITEM_CD As String)
        Dim api = New api()
        Dim check_tag_type = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_LINE_TYPE?line_cd=" & MainFrm.Label4.Text)
        If check_tag_type = "1" Then
            Dim result_update_count_pro1 = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/Api_next_process?line_cd=" & GET_LINE_PRODUCTION() & "&item_cd=" & ITEM_CD)
            Return result_update_count_pro1
        Else
            Return "ISUZU"
        End If
    End Function
    Public Shared Sub Clear_sqlite()
        Dim currdated As String = DateTime.Now.ToString("yyyy-MM-dd")
        Dim today As Date = Date.Today
        Dim date_start As DateTime = today.AddDays(-200)
        Dim format_tommorow = "yyyy-MM-dd"
        Dim formatdateTime_FA = "yyyy-MM-dd HH:mm:ss"
        Dim formatdate_FA = "yyyy-MM-dd"
        Dim formatTime_FA = "HH:mm:ss"
        Dim convert_date_start = date_start.ToString(formatdate_FA) & " 00:00:00"
        Dim del_2_week As DateTime = today.AddDays(-14)
        Dim convert_del_2_week = del_2_week.ToString(formatdate_FA) & " 23:59:59"
        Dim currdated1 As String = DateTime.Now.ToString("yyyy/MM/dd")
        Dim today1 As Date = Date.Today
        Dim date_start1 As DateTime = today1.AddDays(-200)
        Dim format_tommorow1 = "yyyy/MM/dd"
        Dim convert_date_start1 = date_start1.ToString(format_tommorow1)
        Dim currdatedDefect As DateTime = DateTime.Today.AddMonths(-2)
        Dim date_startDefect As DateTime = DateTime.Today.AddMonths(-6)
        Dim convert_date_startDefect = date_startDefect.ToString("yyyy-MM-dd") & " 00:00:00"
        Dim ConvertcurrdatedDefect = currdatedDefect.ToString("yyyy-MM-dd")
        Dim command_data() As String = {
                "DELETE FROM act_ins where st_time BETWEEN '" & convert_date_start & "'AND '" & convert_del_2_week & "' and tr_status = '1' ",
                "DELETE FROM act_ins_by_op where st_time BETWEEN '" & convert_date_start & "'AND '" & convert_del_2_week & "' and tr_status = '1' ",
                "DELETE FROM tag_print_detail where created_date BETWEEN '" & convert_date_start & "' AND '" & convert_del_2_week & "'" & " and tr_status = '1' ",
                "DELETE FROM tag_print_detail_sub where created_date BETWEEN '" & convert_date_start & "' AND '" & convert_del_2_week & "'" & " and tr_status = '1' ",
                "DELETE FROM tag_print_detail_main where created_date BETWEEN '" & convert_date_start & "' AND '" & convert_del_2_week & "'" & " and tr_status = '1' ",
                "DELETE FROM close_lot_act where prd_st_date BETWEEN '" & convert_date_start & "' AND '" & convert_del_2_week & "' and transfer_flg = '1'",
                "DELETE FROM loss_actual where start_loss BETWEEN '" & convert_date_start & "' AND '" & convert_del_2_week & "' and transfer_flg = '1'",
                "DELETE FROM maintenance where mn_create_date BETWEEN '" & convert_date_start1 & "' AND '" & currdated1 & "' and mn_status = '1'",
                "DELETE FROM defect_tag_information where dti_created_date BETWEEN '" & convert_date_start & "' AND '" & convert_del_2_week & "' and dti_tranfer_flg = '1'",
                "DELETE FROM defect_actual where da_created_date BETWEEN '" & convert_date_start & "' AND '" & convert_del_2_week & "' and da_transfer_flg = '1'",
                "DELETE FROM line_status_detail",
                "Delete FROM production_working_info where pwi_created_date BETWEEN '" & convert_date_start & "' AND '" & convert_del_2_week & "'"
            }
        For i = 0 To command_data.Length - 1
            '  'Console.WriteLine(command_data(i))
            Check_connect_sqlite()
            Dim sqliteConn As New SQLiteConnection(sqliteConnect)
            Try
                sqliteConn.Open()
            Catch ex As Exception
                sqliteConn.Close()
                sqliteConn.Open()
            End Try
            Try
                Dim cmd As New SQLiteCommand
                cmd.Connection = sqliteConn
                cmd.CommandText = command_data(i)
                Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
                sqliteConn.Close()
            Catch ex As Exception
                'msgBox("SQLite Database connect failed. Please contact PC System [Function Clear_sqlite]" & ex.Message)
                sqliteConn.Dispose()
                'sqliteConn.Close()
                sqliteConn = Nothing
            End Try
        Next
    End Sub
    Public Shared Function check_version_result(NAME_VERSION As String)
        Dim api = New api()
        Dim result_update_count_pro = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/UPDATE_PATCH/F_UPDATE_PATCH?VERSION_NAME=" & NAME_VERSION)
        Return result_update_count_pro
    End Function
    Public Shared Function CHECK_VERSION(NAME_VERSION As String)
        Dim api = New api()
        Dim result_update_count_pro = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/UPDATE_PATCH/F_UPDATE_PATCH?VERSION_NAME=" & NAME_VERSION)
        Return result_update_count_pro
    End Function
    Public Shared Function SET_LINE_PRODUCTION(line As String)
        LINE_PRODUCTION = line
    End Function
    Public Shared Function GET_LINE_PRODUCTION()
        Return LINE_PRODUCTION
    End Function
    Public Shared Function Get_close_lot_time(SHIFT As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
        Try
            SQLConn.Open()
        Catch ex As Exception
            SQLConn.Close()
            SQLConn.Open()
        End Try
        Try
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[GET_TIME_CLOSE_LOT_SHIFT] @SHIFT = '" & SHIFT & "'"
            reader = SQLCmd.ExecuteReader()
            While reader.Read()
                'temp2Str = "test"
                'If reader.Read() Then
                coles_lot_start_shift = reader("coles_lot_start_time").ToString()
                coles_lot_end_shift = reader("coles_lot_end_time").ToString()
                start_master_shift = reader("master_start_shift").ToString()
                date_time_start_master_shift = DateTime.Now.ToString("yyyy-MM-dd") & " " & start_master_shift
                If Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "B" Or Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "Q" Or Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "S" Then
                    date_time_end_check_date_paralell_linet = DateTime.Now.ToString("yyyy-MM-dd") & " " & coles_lot_end_shift
                    date_time_end_check_date_paralell_linet = date_time_end_check_date_paralell_linet.AddDays(1)
                Else
                    date_time_end_check_date_paralell_linet = DateTime.Now.ToString("yyyy-MM-dd") & " " & coles_lot_end_shift
                End If
                'Else
                ''msgBox("ไม่มีข้อมูลกะการผลิต")
                'End If
            End While
            reader.Close()
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function Get_close_lot_time]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function check_time(SEQ_NO, WI_PLAN, ST_TIME, END_TIME)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
        Try
            SQLConn.Open()
        Catch ex As Exception
            SQLConn.Close()
            SQLConn.Open()
        End Try
        Try
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[CHECK_SPECIAL_TIME] @seq_no = '" & SEQ_NO & "' , @wi_plan = '" & WI_PLAN & "', @st_time = '" & ST_TIME & "', @end_time = '" & END_TIME & "'"
            reader = SQLCmd.ExecuteReader()
            Dim result = 0
            While reader.Read()
                result = reader("c_id").ToString()
            End While
            reader.Close()
            Return result
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function check_time]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function get_new_information()
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
        Try
            SQLConn.Open()
        Catch ex As Exception
            SQLConn.Close()
            '  SQLConn.Open()
        End Try
        Try
            SQLCmd.Connection = SQLConn
            Dim start_date = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
            SQLCmd.CommandText = "EXEC [dbo].[GET_INFORMATION] @date_now = '" & start_date & "'"
            reader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function get_new_information]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function insert_lot_print_defact(wi_plan, item_cd, tag_defact_lot_no, tag_defact_seq, tag_defact_created_date, tag_defact_created_by, tag_defact_qr, tag_defact_status, tag_defact_line_cd)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
        Try
            SQLConn.Open()
        Catch ex As Exception
            SQLConn.Close()
            '  SQLConn.Open()
        End Try
        Try
            SQLCmd.Connection = SQLConn
            Dim time_tomorrow As DateTime = tag_defact_created_date
            Dim format_tommorow = "yyyy-MM-dd"
            Dim date_now_cerrnet = time_tomorrow.ToString(format_tommorow)
            Dim start_date = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
            SQLCmd.CommandText = "EXEC [dbo].[INSERT_LOG_PRINT_DEFACT_NC]  @tag_defact_wi = '" & wi_plan & "' , @tag_defact_item_cd = '" & item_cd & "' , @tag_defact_lot_no = '" & tag_defact_lot_no & "' , @tag_defact_seq = '" & tag_defact_seq & "' , @tag_defact_created_date = '" & date_now_cerrnet & "' , @tag_defact_qr = '" & tag_defact_qr & " ' ,  @tag_defact_status = '" & tag_defact_status & " ' , @tag_defact_line_cd = '" & tag_defact_line_cd & "'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function INSERT_LOG_PRINT_DEFACT_NC]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function load_qty_defact(item_cd As String, lot_no As String, seq As String, date_now As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
        Try
            SQLConn.Open()
        Catch ex As Exception
            SQLConn.Close()
            '  SQLConn.Open()
        End Try
        Try
            SQLCmd.Connection = SQLConn
            Dim start_date = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
            SQLCmd.CommandText = "EXEC [dbo].[LOAD_QTY_DEFACT] @item_cd = '" & item_cd & "' , @lot_no = '" & lot_no & "' , @seq = '" & seq & "' , @date_now = '" & date_now & "'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function LOAD_QTY_DEFACT]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function

    Public Shared Function GET_QTY_DEFACT_NC(WI As String, line_cd As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
        Try
            SQLConn.Open()
        Catch ex As Exception
            SQLConn.Close()
            '  SQLConn.Open()
        End Try
        Try
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[GET_QTY_DEFACT_NC] @WI = '" & WI & "' , @line_cd = '" & line_cd & "'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function GET_QTY_DEFACT_NC]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function


    Public Shared Function get_data_wi_reprint(start_date, end_date, line_cd)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            start_date = start_date & " 00:00:00"
            end_date = end_date & " 23:59:59"
            SQLCmd.CommandText = "EXEC [dbo].[GET_WI_OF_DAY] @Date_work_production_start = '" & start_date & "' , @Date_work_production_end = '" & end_date & "',
 @line_cd = '" & line_cd & "'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function get_data_wi_reprint]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function get_data_item(WI As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[GET_DATA_ABOUT_ITEM] @WI = '" & WI & "'"

            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function get_data_wi_reprint]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function get_list_rm_scan(WI)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[GET_RM_SCAN] @WI = '" & WI & "'"
            reader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function get_list_rm_scan]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function Insert_Rm_Scan(WI As String, ITEM_CD As String, LOT_PO As String, SEQ As String, SHIFT As String, Rm_created_date As String, Rm_created_by As String, Rm_Updated_date As String, Rm_updated_by As String, Rm_line_cd As String, Rm_QR_code As String, ref_id As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[INSERT_RM_SCAN] 
                                    @WI='" & WI & "' , 
                                    @ITEM_CD='" & ITEM_CD & "' , 
                                    @LOT_PO='" & LOT_PO & "' ,
                                    @SEQ='" & SEQ & "' ,
                                    @SHIFT='" & SHIFT.Substring(0, 1) & "' ,
                                    @Rm_created_date='" & Rm_created_date & "' ,
                                    @Rm_created_by='" & Rm_created_by & "' , 
                                    @Rm_Updated_date='" & Rm_Updated_date & "' , 
                                    @Rm_updated_by='" & Rm_updated_by & "' ,
                                    @Rm_line_cd='" & Rm_line_cd & "',
                                    @Rm_QR_code='" & Rm_QR_code & "',
									@Rm_ref_id='" & ref_id & "'"
            reader = SQLCmd.ExecuteReader()
            reader.Close()
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function Insert_Rm_Scan]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function GET_QTY_SEQ(WI, SEQ_NO)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[GET_QTY_SEQ] @WI = '" & WI & "' , @SEQ = '" & SEQ_NO & "'"
            reader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function GET_QTY_SEQ]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function

    Public Shared Function CHECK_TRANSCETION_PRODUCTION_DETAIL(line_cd As String, date_start As String, date_end As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[CHECK_TRANSCETION_PRODUCTION_DETAIL] @line_cd = '" & line_cd & "' , @date_start = '" & date_start & "' , @date_end = '" & date_end & "'"
            ''Console.WriteLine(SQLCmd.CommandText)
            reader = SQLCmd.ExecuteReader()
            Dim id As String = ""
            While reader.Read()
                id = reader("id").ToString()
            End While
            reader.Close()
            If id = "" Then
                id = 0
            End If
            ''Console.WriteLine(id)
            Return id
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function CHECK_TRANSCETION_PRODUCTION_DETAIL]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function

    Public Shared Function INSERT_DATA_RM_SCAN()
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[GET_QTY_SEQ] @WI = '" & WI & "' , @SEQ = '" & SEQ_NO & "'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function INSERT_DATA_RM_SCAN]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function

    Public Shared Function GET_CHECK_LOSS(start_loss As String, end_loss As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            Try
                SQLConn.Open()
            Catch ex As Exception
                SQLConn.Close()
                SQLConn.Open()
            End Try
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[CHECK_LOSS_DOUBLE] @date_start = '" & start_loss.ToString() & "' , @date_end = '" & end_loss.ToString() & "', @line_cd = '" & GET_LINE_PRODUCTION() & "'"
            reader = SQLCmd.ExecuteReader()
            Dim tmp_result As Integer = 0
            While reader.Read()
                tmp_result = reader("c_id").ToString()
            End While
            reader.Close()
            Return tmp_result
            'Return reader
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function GET_CHECK_LOSS]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function INSERT_REWORK_ACTUAL(RAW_PART_NO, RAW_QTY, RAW_SHIFT, RWA_CREATED_DATE_TIME, RWA_WI, RWA_PART_NAME, RWA_MODEL)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[INSERT_DATA_REWORK_ACTUAL] @RAW_PART_NO = '" & RAW_PART_NO & "' , @RAW_QTY = '" & RAW_QTY & "', @RAW_SHIFT = '" & RAW_SHIFT & "', @RWA_CREATED_DATE_TIME = '" & RWA_CREATED_DATE_TIME & "', @RWA_WI = '" & RWA_WI & "', @RWA_PART_NAME = '" & RWA_PART_NAME & "', @RWA_MODEL = '" & RWA_MODEL & "'"
            reader = SQLCmd.ExecuteReader()
            'Return reader
            reader.Close()
            SQLConn.Close()
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function INSERT_REWORK_ACTUAL]")
            SQLConn.Close()
            load_show.Show()
            ' Application.Exit()
        End Try
    End Function



    Public Shared Function INSERT_REWORK_ACTUAL_SQLITE(RAW_PART_NO, RAW_QTY, RAW_SHIFT, RWA_CREATED_DATE_TIME, RWA_WI, RWA_PART_NAME, RWA_MODEL, tr_status)
re_insert_rework_act:
        Check_connect_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "INSERT INTO rework_actual (rwa_part_no,rwa_qty,rwa_ship,rwa_created_date_time,ref_wi ,rwa_part_name ,rwa_model,tr_status)
		VALUES(
				'" & RAW_PART_NO & "', 
				'" & RAW_QTY & "',
				'" & RAW_SHIFT & "',
				'" & RWA_CREATED_DATE_TIME & "',
				'" & RWA_WI & "',
				'" & RWA_PART_NAME & "',
				'" & RWA_MODEL & "',
                '" & tr_status & "'
		)"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            sqliteConn.Close()
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function ConnectDBSQLite]")
            sqliteConn.Close()
            GoTo re_insert_rework_act
        End Try
    End Function

    Public Shared Function chk_spec_line()
        Dim api = New api()
        Dim result_update_count_pro = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/Api_check_data/chk_spec_line?line_cd=" & GET_LINE_PRODUCTION())
        Return result_update_count_pro
    End Function
    Public Shared Function INSERT_tmp_planseq(wi, line_cd, date_start, date_end, seq)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            'SQLCmd.CommandText = "SELECT max(seq_no) as seq_no from production_actual where wi = '" & wi & "'"
            SQLCmd.CommandText = "insert into tmp_planseq (tmp_line_cd , tmp_production_date , tmp_last_sequence , tmp_created_date , tmp_created_by , tmp_updated_date , tmp_updated_by) 
			values(
					'" & line_cd & "',
					'" & date_start & "',
					'" & seq & "',
					'" & date_start & "',
					'SYSTEM',
					'" & date_start & "',
					'SYSTEM'
			)"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function INSERT_tmp_planseq]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function GET_DATA_PRODUCTION_WORKING_INFO(ind_row, pwi_lot_no, pwi_seq_no)
        Try
            Dim api = New api()
            Dim result_update_count_pro = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/GET_DATA_PRODUCTION_WORKING_INFO?ind_row=" & ind_row & "&pwi_lot_no=" & pwi_lot_no & "&pwi_seq_no=" & pwi_seq_no)
            Return result_update_count_pro
        Catch ex As Exception
            'msgBox("MSSQL Database connect failed. Please contact PC System [Function GET_DATA_PRODUCTION_WORKING_INFO]" & ex.Message)
        End Try
    End Function

    ' Read-only diagnostic wrapper for the legacy scalar PWI lookup. The legacy
    ' endpoint returns only a PWI value (or 0/no value), not a row collection.
    Public Shared Function TryGetProductionWorkingInfoReadOnly(indRow As String,
                                                                lotNo As String,
                                                                seqNo As String,
                                                                ByRef found As Boolean,
                                                                ByRef pwiValue As String,
                                                                ByRef rawResponse As String,
                                                                ByRef reason As String) As Boolean
        found = False
        pwiValue = String.Empty
        rawResponse = String.Empty
        reason = String.Empty
        Try
            Dim url As String = "http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/GET_DATA_PRODUCTION_WORKING_INFO?ind_row=" &
                                Uri.EscapeDataString(indRow) & "&pwi_lot_no=" & Uri.EscapeDataString(lotNo) &
                                "&pwi_seq_no=" & Uri.EscapeDataString(seqNo)
            rawResponse = Convert.ToString(New api().Load_data(url)).Trim()
            If String.IsNullOrWhiteSpace(rawResponse) Then
                reason = "PWI lookup returned no response."
                Return False
            End If
            If rawResponse = "0" Then Return True

            pwiValue = rawResponse
            found = True
            Return True
        Catch ex As Exception
            reason = ex.GetType().Name & ": " & ex.Message
            Return False
        End Try
    End Function
    Public Shared Function INSERT_production_working_info(ind_row, pwi_lot_no, pwi_seq_no, pwi_shift)
        Try
            Dim api = New api()
            Dim result_update_count_pro = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/INSERT_production_working_info?ind_row=" & ind_row & "&pwi_lot_no=" & pwi_lot_no & "&pwi_seq_no=" & pwi_seq_no & "&pwi_shift=" & pwi_shift)
            Return result_update_count_pro
        Catch ex As Exception
            'msgBox("MSSQL Database connect failed. Please contact PC System [Function INSERT_production_working_info]" & ex.Message)
        End Try
    End Function

    Public Shared Function GET_SEQ_PLAN_current(wi, line_cd, date_start, date_end)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Dim time As String = Trim(date_start.Substring(11))
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd")
        Dim today As Date = Date.Today
        Dim time_tomorrow As DateTime = today.AddDays(1)
        Dim format_tommorow = "yyyy/MM/dd"
        Dim date_tommorow = time_tomorrow.ToString(format_tommorow)
        date_end_covert = date_tommorow & " 07:59:59"
        Try
            Dim time_now As DateTime
            time_now = DateTime.Now.ToString("hh:mm:ss tt")
            If time_now >= "08:00:00 AM" And time_now <= "07:59:59 PM" Then
                date_start = currdated & " 08:00:00"
                ' date_start = date_start & " 08:00:00"
            Else
                date_start = currdated & " 08:00:00"
            End If
            If time_now >= "12:00:00 AM" And time_now <= "08:00:00 AM" Then
                Dim format_tommorow_re = "yyyy/MM/dd"
                Dim del_date1 As DateTime = today.AddDays(-1)
                date_start = del_date1.ToString(format_tommorow_re)
                Dim sub_date_end1 = Trim(date_end.ToString.Substring(0, 10))
                date_start = date_start & " 08:00:00"
                date_end_covert = sub_date_end1 & " 07:59:59"
            End If
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            'SQLCmd.CommandText = "SELECT max(seq_no) as seq_no from production_actual where wi = '" & wi & "'"
            SQLCmd.CommandText = "SELECT max(tmp_last_sequence) as seq_no from tmp_planseq where tmp_created_date BETWEEN  '" & date_start & "' and '" & date_end_covert & "' and tmp_line_cd = '" & line_cd & "'"
            reader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function GET_SEQ_PLAN_current]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function

    Public Shared Function Update_seqplan(wi, line_cd, date_start, date_end, Update_seq)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Dim tmp_id As String = ""
        '		If time >= "08:00:00" And time >= "08:00:00" Then
        '		date_start = currdated & " 08:00:00"
        '		Else
        '		date_start = currdated & " 20:00:00"
        '		End If
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd")
        Dim today As Date = Date.Today
        Dim time_tomorrow As DateTime = today.AddDays(1)
        Dim format_tommorow = "yyyy/MM/dd"
        Dim date_tommorow = time_tomorrow.ToString(format_tommorow)
        date_end_covert = date_tommorow & " 07:59:59"
        Try
            Dim time_now As DateTime
            time_now = DateTime.Now.ToString("hh:mm:ss tt")
            If time_now >= "08:00:00 AM" And time_now <= "07:59:59 PM" Then
                date_start = currdated & " 08:00:00"
                ' date_start = date_start & " 08:00:00"
            Else
                date_start = currdated & " 08:00:00"
            End If
            If time_now >= "12:00:00 AM" And time_now <= "08:00:00 AM" Then
                Dim format_tommorow_re = "yyyy/MM/dd"
                Dim del_date1 As DateTime = today.AddDays(-1)
                date_start = del_date1.ToString(format_tommorow_re)
                Dim sub_date_end1 = Trim(date_end.ToString.Substring(0, 10))
                date_start = date_start & " 08:00:00"
                date_end_covert = sub_date_end1 & " 07:59:59"
            End If
        Catch ex As Exception

        End Try
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "SELECT tmp_id from tmp_planseq where tmp_created_date BETWEEN  '" & date_start & "' and '" & date_end_covert & "' and tmp_line_cd = '" & line_cd & "'"
            reader = SQLCmd.ExecuteReader()
            While reader.Read()
                tmp_id = reader("tmp_id").ToString()
            End While
            reader.Close()
            'SQLCmd.CommandText = "update tmp_planseq set tmp_last_sequence = '" & Update_seq & "' , tmp_updated_date = '" & date_end & "' where tmp_line_cd = '" & line_cd & "' and tmp_created_date BETWEEN  '" & date_start & "' and '" & date_end & "'"
            SQLCmd.CommandText = "update tmp_planseq set tmp_last_sequence = '" & Update_seq & "' , tmp_updated_date = '" & date_end & "' where tmp_id = '" & tmp_id & "'"
            ''Console.WriteLine(SQLCmd.CommandText)
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function GET_SEQ_PLAN_current]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function GET_QTY_SHIFT(LINE_CD, WI, SHIFT, DATE_NOW, date_end, time_st, time_end, lot_no)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[GET_QTY_SHIFT_NO_WI_LOT_NEW]  @line_cd = '" & LINE_CD & "' , @WI = '" & WI & "' , @ship  = '" & SHIFT & "', @date_now  = '" & DATE_NOW & "' , @date_end  = '" & date_end & "', @time_st  = '" & time_st & "', @time_end  = '" & time_end & "' , @lot_no='" & lot_no & "'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function GET_QTY_SHIFT_NO_WI]" & ex.Message)
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function Insert_production_emp_detail_realtime(wi_plan, staff_cd, prd_seq_no, pwi_id)
        If wi_plan <> "Lable41" Then
            Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
            Dim reader As SqlDataReader
            Dim SQLConn As New SqlConnection() 'The SQL Connection
            Dim SQLCmd As New SqlCommand()
            Try
                SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
                SQLConn.Open()
                SQLCmd.Connection = SQLConn
                SQLCmd.CommandText = "Insert into production_emp_detail_realtime (wi_plan , staff_cd , prd_seq_no , updated_date , pwi_id) values('" & wi_plan & "','" & staff_cd & "','" & prd_seq_no & "','" & currdated & "','" & pwi_id & "')"
                reader = SQLCmd.ExecuteReader()
                reader.Close()
            Catch ex As Exception
                ''msgBox("MSSQL Database connect failed. Please contact PC System [Function Insert_production_emp_detail_realtime]" & ex.Message)
                SQLConn.Close()
                load_show.Show()
                'Application.Exit()
            End Try
        End If
    End Function

    'Public Shared Async Function Check_detail_actual_insert_act(parentForm As Form) As Task(Of String)
    '    Await updated_data_to_dbsvr(parentForm, "1")
    ' Dim api = New api()
    '  Dim result_update_count_pro = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/TESTAPITRANFER/Get_detail_act?line_cd=" & MainFrm.Label4.Text)
    '   Return result_update_count_pro
    '   End Function
    Public Shared Async Function Check_detail_actual_insert_act(parentForm As Form) As Task(Of String)
        Try
            Dim url As String = "http://" & svApi & "/API_NEW_FA/index.php/TESTAPITRANFER/Get_detail_act?line_cd=" & MainFrm.Label4.Text
            ' ✅ แปลงให้ async โดยรันบน background thread
            Dim api = New api()
            Dim rsData As String = Await api.Load_dataAsync(url)
            Return rsData
        Catch ex As Exception
            'msgBox("❗ connect Api Fail in GetPercenPlanned_OEE = " & ex.Message)
            Return "0"
        End Try
    End Function

    Public Shared Async Function Check_detail_actual_insert_act_no_api(parentForm As Form) As Task(Of String)
        Await updated_data_to_dbsvr(parentForm, "1")
    End Function
    Public Shared Function update_qty_seq(WI, SEQ, result_qty)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Dim updateQtyRetry As Integer = 0
        Try
recheck:
            updateQtyRetry += 1
            If updateQtyRetry > 3 Then Return False
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandTimeout = 10
            SQLCmd.CommandText = "EXEC [dbo].[UPDATE_QTY_DESC_ACTUAL] @WI = '" & WI & "' , @SEQ = '" & SEQ & "' , @QTY_UPDATE='" & result_qty & "'"
            reader = SQLCmd.ExecuteReader()
            reader.Close()
            Return True
        Catch ex As Exception
            Try : SQLConn.Close() : Catch : End Try
            If updateQtyRetry < 3 Then
                Threading.Thread.Sleep(250)
                GoTo recheck
            End If
            Return False
        End Try
    End Function
    Public Shared Function GET_NEXT_PROCESS()
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[GET_NEXT_PROCESS] @PD = '" & MainFrm.Label6.Text & "'"
            reader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
        Catch ex As Exception
            'msgBox("MSSQL Database connect failed. Please contact PC System [Function GET_NEXT_PROCESS]")
            SQLConn.Close()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function update_qty_seq_sqlite(WI, SEQ, result_qty, tr_status)
re_up_date_data:
        Check_connect_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
        Catch ex As Exception
            sqliteConn.Close()
            sqliteConn.Open()
        End Try
        Try
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "UPDATE close_lot_act set act_qty = '" & result_qty & "' ,  transfer_flg = '" & tr_status & "' where  wi = '" & WI & "' and seq_no = '" & SEQ & "'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            'Return LoadSQL
            'sqliteConn.Dispose()
            sqliteConn.Close()
            'sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function update_qty_seq_sqlite]" & ex.Message)
            sqliteConn.Dispose()
            'sqliteConn.Close()
            sqliteConn = Nothing
            GoTo re_up_date_data
        End Try
    End Function

    Public Shared Function GET_QTY_SEQ_ACTUAL_DESC(WI, ship, SEQ)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String  
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[GET_QTY_DESC_ACTUAl] @WI = '" & WI & "' , @ship = '" & ship & "' , @SEQ='" & SEQ & "'"
            reader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function GET_QTY_SEQ_ACTUAL_DESC]")
            SQLConn.Close()
            load_show.Show()
            ' Application.Exit()
        End Try
    End Function
    Public Shared Function GET_QTY_SEQ_ACTUAL_DESC_SQLITE(WI, ship, SEQ)
re_update_data:
        Check_connect_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
        Catch ex As Exception
            sqliteConn.Open()
            sqliteConn.Close()
        End Try
        Try
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "SELECT SUM(act_qty) as QTY_ACTUAL
 from close_lot_act 
where 
	wi = '" & WI & "' and 
	shift_prd = '" & ship & "' and 
  seq_no = '" & SEQ & "'
"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function GET_QTY_SEQ_ACTUAL_DESC_SQLITE]" & ex.Message)
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
            GoTo re_update_data
        End Try
    End Function
    Public Shared Function ConnectDB() ' ฟังชั่นก์ไว้สำหรับติดต่อฐานข้อมูลเมื่อต้องการใช้งาน
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try

            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            ''msgBox("Database connect successfully")
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function ConnectDB]")
            SQLConn.Close()
            load_show.Show()
            ' Application.Exit()
        End Try
    End Function
    Public Shared Function GetLine_mst()
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "SELECT * FROM sys_line_mst WHERE enable='1'"

            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            '  'msgBox("MSSQL Database connect failed. Please contact PC System [Function GetLine_mst]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function

    Public Shared Function update_print_count(wi, seq_plan, seq_box, qty)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Dim seq_box1 As Integer = CDbl(Val(seq_box))
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "SELECT * FROM tag_print_detail WHERE wi = '" & wi & "' and seq_no = '" & seq_plan & "' and box_no = '" & seq_box1 & "' AND TRIM(SUBSTRING(qr_detail, 53, 6)) = '" & qty & "'"
            ' ''Console.WriteLine("update===>" & SQLCmd.CommandText)
            reader = SQLCmd.ExecuteReader()
            Dim print_count As Integer = 0
            Dim id As Integer = 0
            While reader.Read()
                id = reader("id").ToString()
                print_count = CDbl(Val(reader("print_count").ToString())) + 1
            End While
            reader.Close()
            Dim api = New api()
            Dim result_update_count_pro = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/UPDATE_DATA/UPDATE_PRINT_TEST_SYSTEM?ID=" & id & "&PRINT_COUNT=" & print_count)
            Dim table_created As Integer = 2
            If flg_cat_layout_line = "1" Then
                table_created = 2
            ElseIf flg_cat_layout_line = "2" Then
                table_created = 3
            End If
            ins_log_print(MainFrm.Label4.Text, table_created, id)
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function GetLine_mst]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function get_information()
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection()
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "SELECT * FROM sys_information WHERE enable = '1'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function get_information]")
            SQLConn.Close()
            load_show.Show()
            ' Application.Exit()
        End Try
    End Function
    Public Shared Function inf_update(inf_text As String, staff_cd As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()

        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "UPDATE sys_information SET inf_txt = '" & inf_text & "', created_date = '" & currdated & "', created_by = '" & staff_cd & "'  WHERE id = 1 "
            reader = SQLCmd.ExecuteReader()
            Return reader
            SQLConn.Dispose()
            SQLConn.Close()
            SQLConn = Nothing
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function inf_update]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    'Public Shared Function Get_User_Line_detail(usernm As String, passwd As String)
    Public Shared Function Get_User_Line_detail()
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            'SQLCmd.CommandText = "SELECT * FROM sys_user WHERE emp_id = '" & usernm & "' AND passwd = '" & passwd & "'"
            Dim line_cd As String = MainFrm.Label4.Text
            SQLCmd.CommandText = "SELECT * FROM sys_line_mst WHERE line_cd = '" & line_cd & "'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function Get_User_Line_detail]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function Get_prd_plan_new(line_cd As String)
        Try
            Dim api = New api()
            Dim result_api_checkper As String = ""
            result_api_checkper = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/Api_Get_plan_production?line_cd=" & GET_LINE_PRODUCTION())
            ''Console.WriteLine("http://" & svApi & "/API_NEW_FA/index.php/Api_Get_plan_production?line_cd=" & GET_LINE_PRODUCTION())
            Return result_api_checkper
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function Get_prd_plan_new]")
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function Tag_seq_rec_sqlite(wi_plan As String, seq_no As Integer, qty As Integer, ref_key As String)
        Check_connect_sqlite()
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "INSERT INTO sc_inc_tag(wi,seq_no,qty,created_date,ref_key) VALUES ('" & wi_plan & "','" & seq_no & "','" & qty & "','" & currdated & "','" & ref_key & "')"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function Tag_seq_rec_sqlite]")
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function Insert_prd_detail(
    pd As String, line_cd As String, wi_plan As String,
    item_cd As String, item_name As String, staff_no As Integer,
    seq_no As Integer, qty As Integer, st_time As String,
    end_time As String, use_time As Double, number_qty As Integer,
    pwi_id As String, status_sqlite As String
) As Integer
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss", CultureInfo.InvariantCulture)
        Dim result_date_start As Date = Date.Parse(st_time)
        Dim st_time2 As String = result_date_start.ToString("yyyy/MM/dd H:mm:ss", CultureInfo.InvariantCulture)
        Dim result_date_end As Date = Date.Parse(end_time)
        Dim end_time2 As String = result_date_end.ToString("yyyy/MM/dd H:mm:ss", CultureInfo.InvariantCulture)
        Dim insertId As Integer = 0
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim sql As String = "
        INSERT INTO production_actual_detail 
            (pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, qty, st_time, end_time, use_time, updated_date, number_qty, pwi_id, status_transfer_sqlite) 
        VALUES 
            (@pd, @line_cd, @wi_plan, @item_cd, @item_name, @staff_no, @seq_no, @qty, @st_time, @end_time, @use_time, @updated_date, @number_qty, @pwi_id, @status_sqlite);
        SELECT SCOPE_IDENTITY();"
                Try
                    Using SQLConn As New SqlConnection(sqlConnect)
                        Using SQLCmd As New SqlCommand(sql, SQLConn)
                            SQLCmd.CommandTimeout = 120
                            SQLCmd.Parameters.AddWithValue("@pd", pd)
                            SQLCmd.Parameters.AddWithValue("@line_cd", line_cd)
                            SQLCmd.Parameters.AddWithValue("@wi_plan", wi_plan)
                            SQLCmd.Parameters.AddWithValue("@item_cd", item_cd)
                            SQLCmd.Parameters.AddWithValue("@item_name", item_name)
                            SQLCmd.Parameters.AddWithValue("@staff_no", staff_no)
                            SQLCmd.Parameters.AddWithValue("@seq_no", seq_no)
                            SQLCmd.Parameters.AddWithValue("@qty", qty)
                            SQLCmd.Parameters.AddWithValue("@st_time", st_time2)
                            SQLCmd.Parameters.AddWithValue("@end_time", end_time2)
                            SQLCmd.Parameters.AddWithValue("@use_time", use_time)
                            SQLCmd.Parameters.AddWithValue("@updated_date", currdated)
                            SQLCmd.Parameters.AddWithValue("@number_qty", number_qty)
                            SQLCmd.Parameters.AddWithValue("@pwi_id", pwi_id)
                            SQLCmd.Parameters.AddWithValue("@status_sqlite", status_sqlite)
                            SQLConn.Open()
                            Try
                                insertId = Convert.ToInt32(SQLCmd.ExecuteScalar())
                                'Console.WriteLine("Try insertId ==>" & insertId)
                            Catch exTimeout As SqlException
                                If exTimeout.Number = -2 OrElse exTimeout.Message.Contains("Timeout") Then
                                    Console.WriteLine("⚠️ Timeout detected. Trying to recover inserted ID...")
                                    '  insertId = GetInsertedIdFromData(pd, line_cd, wi_plan, seq_no)
                                    Console.WriteLine("catch insertId ==>" & insertId)
                                Else
                                    Console.WriteLine("catch else ")
                                    Throw
                                End If
                            End Try
                        End Using
                    End Using
                Catch ex As Exception
                    Console.WriteLine("❌ Status = 1 แต่ไม่เข้า DB  Error inserting data: " & ex.Message)
                    insertId = 0
                End Try
            Else
                Console.WriteLine("else Insert_prd_detail No insert naja")
                insertId = 0
            End If
        Catch ex As Exception
            insertId = 0
        End Try
        Return insertId
    End Function

    Public Shared Function Insert_prd_detail_by_op(
    pd As String, line_cd As String, wi_plan As String,
    item_cd As String, item_name As String, staff_no As Integer,
    seq_no As Integer, qty As Integer, st_time As String,
    end_time As String, use_time As Double, number_qty As Integer,
    pwi_id As String, status_sqlite As String, op_id As String, status_work As String
) As Integer
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss", CultureInfo.InvariantCulture)
        Dim result_date_start As Date = Date.Parse(st_time)
        Dim st_time2 As String = result_date_start.ToString("yyyy/MM/dd H:mm:ss", CultureInfo.InvariantCulture)
        Dim result_date_end As Date = Date.Parse(end_time)
        Dim end_time2 As String = result_date_end.ToString("yyyy/MM/dd H:mm:ss", CultureInfo.InvariantCulture)
        Dim insertId As Integer = 0
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim sql As String = "
        INSERT INTO production_actual_detail_by_op 
            (pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, qty, st_time, end_time, use_time, updated_date, pwi_id, status_transfer_sqlite , op_id , status_work)
        VALUES 
            (@pd, @line_cd, @wi_plan, @item_cd, @item_name, @staff_no, @seq_no, @qty, @st_time, @end_time, @use_time, @updated_date, @pwi_id, @status_sqlite , @op_id , @status_work);
        SELECT SCOPE_IDENTITY();"
                Try
                    Using SQLConn As New SqlConnection(sqlConnect)
                        Using SQLCmd As New SqlCommand(sql, SQLConn)
                            SQLCmd.CommandTimeout = 120
                            SQLCmd.Parameters.AddWithValue("@pd", pd)
                            SQLCmd.Parameters.AddWithValue("@line_cd", line_cd)
                            SQLCmd.Parameters.AddWithValue("@wi_plan", wi_plan)
                            SQLCmd.Parameters.AddWithValue("@item_cd", item_cd)
                            SQLCmd.Parameters.AddWithValue("@item_name", item_name)
                            SQLCmd.Parameters.AddWithValue("@staff_no", staff_no)
                            SQLCmd.Parameters.AddWithValue("@seq_no", seq_no)
                            SQLCmd.Parameters.AddWithValue("@qty", qty)
                            SQLCmd.Parameters.AddWithValue("@st_time", st_time2)
                            SQLCmd.Parameters.AddWithValue("@end_time", end_time2)
                            SQLCmd.Parameters.AddWithValue("@use_time", use_time)
                            SQLCmd.Parameters.AddWithValue("@updated_date", currdated)
                            SQLCmd.Parameters.AddWithValue("@pwi_id", pwi_id)
                            SQLCmd.Parameters.AddWithValue("@status_sqlite", status_sqlite)
                            SQLCmd.Parameters.AddWithValue("@op_id", op_id)
                            SQLCmd.Parameters.AddWithValue("@status_work", status_work)
                            SQLConn.Open()
                            WriteDebugDiagnostic(SQLCmd.CommandText)
                            For Each p As SqlParameter In SQLCmd.Parameters
                                WriteDebugDiagnostic($"  {p.ParameterName} = {p.Value}")
                            Next
                            WriteDebugDiagnostic("========================================")
                            Try
                                insertId = Convert.ToInt32(SQLCmd.ExecuteScalar())
                            Catch exTimeout As SqlException
                                If exTimeout.Number = -2 OrElse exTimeout.Message.Contains("Timeout") Then
                                    Console.WriteLine("⚠️ Timeout detected. Trying to recover inserted ID...")
                                    Console.WriteLine("catch insertId ==>" & insertId)
                                Else
                                    Console.WriteLine("catch else ")
                                    Throw
                                End If
                            End Try
                        End Using
                    End Using
                Catch ex As Exception
                    Console.WriteLine("❌ Status = 1 แต่ไม่เข้า DB  Error inserting data: " & ex.Message)
                    insertId = 0
                End Try
            Else
                Console.WriteLine("else Insert_prd_detail No insert naja")
                insertId = 0
            End If
        Catch ex As Exception
            insertId = 0
        End Try
        Return insertId
    End Function




    Public Shared Async Function Insert_prd_detail_main(
    pd As String, line_cd As String, wi_plan As String,
    item_cd As String, item_name As String, staff_no As Integer,
    seq_no As Integer, qty As Integer, st_time As String,
    end_time As String, use_time As Double, number_qty As Integer,
    pwi_id As String, status_sqlite As String, id_sqlite As String
) As Task(Of Integer)
        If checkSqliteTrasnfer Then Return 0
        Dim insertId As Integer = 0
        Dim currdated As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim st_time2 As String = Date.Parse(st_time).ToString("yyyy-MM-dd HH:mm:ss")
        Dim end_time2 As String = Date.Parse(end_time).ToString("yyyy-MM-dd HH:mm:ss")
        Dim api = New api()
        Dim retryCount As Integer = 0
        Dim logDir = "C:\sqlite3\logs"
        Dim logPath = $"{logDir}\insert_error.log"
        Directory.CreateDirectory(logDir)
        Do
            ' ✅ Check network
            If Not My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                'Console.WriteLine("⛔ Network unavailable... retrying in 2 sec")
                Await Task.Delay(2000)
                Continue Do
            End If
            'File.AppendAllText(logPath, $"{Now:yyyy-MM-dd HH:mm:ss} | ▶️ Start Insert Attempt {retryCount + 1} | ID={id_sqlite}{Environment.NewLine}")
            Dim sql As String = "
            INSERT INTO production_actual_detail 
            (pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, qty, st_time, end_time, use_time, updated_date, number_qty, pwi_id, status_transfer_sqlite) 
            VALUES 
            (@pd, @line_cd, @wi_plan, @item_cd, @item_name, @staff_no, @seq_no, @qty, @st_time, @end_time, @use_time, @updated_date, @number_qty, @pwi_id, @status_sqlite);
            SELECT SCOPE_IDENTITY();"
            Try
                Using SQLConn As New SqlConnection(sqlConnect)
                    Using SQLCmd As New SqlCommand(sql, SQLConn)
                        SQLCmd.CommandTimeout = 120
                        SQLCmd.Parameters.AddWithValue("@pd", pd)
                        SQLCmd.Parameters.AddWithValue("@line_cd", line_cd)
                        SQLCmd.Parameters.AddWithValue("@wi_plan", wi_plan)
                        SQLCmd.Parameters.AddWithValue("@item_cd", item_cd)
                        SQLCmd.Parameters.AddWithValue("@item_name", item_name)
                        SQLCmd.Parameters.AddWithValue("@staff_no", staff_no)
                        SQLCmd.Parameters.AddWithValue("@seq_no", seq_no)
                        SQLCmd.Parameters.AddWithValue("@qty", qty)
                        SQLCmd.Parameters.AddWithValue("@st_time", st_time2)
                        SQLCmd.Parameters.AddWithValue("@end_time", end_time2)
                        SQLCmd.Parameters.AddWithValue("@use_time", use_time)
                        SQLCmd.Parameters.AddWithValue("@updated_date", currdated)
                        SQLCmd.Parameters.AddWithValue("@number_qty", number_qty)
                        SQLCmd.Parameters.AddWithValue("@pwi_id", pwi_id)
                        SQLCmd.Parameters.AddWithValue("@status_sqlite", status_sqlite)
                        Await SQLConn.OpenAsync()
                        insertId = Convert.ToInt32(Await SQLCmd.ExecuteScalarAsync())
                    End Using
                End Using
                WriteDebugDiagnostic("Production-detail transfer status recorded")
                If insertId > 0 Then
                    ' ✅ สำเร็จ → update SQLite
                    Dim sqlUpdate = $"UPDATE act_ins SET tr_status = '1', updated_date = '{currdated}' WHERE id = '{id_sqlite}'"
                    Await api.Load_dataSQLiteAsync(sqlUpdate)
                    WriteDebugDiagnostic("Production-detail transfer status update requested")
                    'msgBox("function Insert_prd_detail_main => " & sqlUpdate)
                    'File.AppendAllText(logPath, $"{Now:yyyy-MM-dd HH:mm:ss} | ✅ Insert Success | ID={id_sqlite} | SQL_ID={insertId}{Environment.NewLine}")
                    Exit Do
                Else
                    'msgBox("function insertId => " & insertId)
                    ''File.AppendAllText(logPath, $"{Now:yyyy-MM-dd HH:mm:ss} | ⚠️ Insert Failed (ID=0) | Retry={retryCount + 1} | ID={id_sqlite}{Environment.NewLine}")
                End If
            Catch ex As Exception
                Dim functionName As String = New StackTrace().GetFrame(0).GetMethod().Name
                'Console.WriteLine($"❌ Insert Error ({functionName}): {ex.Message}")
                ' File.AppendAllText(logPath, $"{Now:yyyy-MM-dd HH:mm:ss} | ❌ Error ({functionName}) | Retry={retryCount + 1} | ID={id_sqlite} | Msg={ex.Message}{Environment.NewLine}")
            End Try
            retryCount += 1
            Await Task.Delay(2000) ' ⏳ รอแล้วลองใหม่
        Loop While insertId = 0
        Return insertId
    End Function
    Public Shared Async Function Insert_prd_detail_by_op_main(
    pd As String, line_cd As String, wi_plan As String,
    item_cd As String, item_name As String, staff_no As Integer,
    seq_no As Integer, qty As Integer, st_time As String,
    end_time As String, use_time As Double, number_qty As Integer,
    pwi_id As String, status_sqlite As String, id_sqlite As String, op_id As Integer, status_work As Integer
) As Task(Of Integer)
        If checkSqliteTrasnfer Then Return 0
        Dim insertId As Integer = 0
        Dim currdated As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim st_time2 As String = Date.Parse(st_time).ToString("yyyy-MM-dd HH:mm:ss")
        Dim end_time2 As String = Date.Parse(end_time).ToString("yyyy-MM-dd HH:mm:ss")
        Dim api = New api()
        Dim retryCount As Integer = 0
        Dim logDir = "C:\sqlite3\logs"
        Dim logPath = $"{logDir}\insert_error.log"
        Directory.CreateDirectory(logDir)
        Do
            ' ✅ Check network
            If Not My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                'Console.WriteLine("⛔ Network unavailable... retrying in 2 sec")
                Await Task.Delay(2000)
                Continue Do
            End If
            'File.AppendAllText(logPath, $"{Now:yyyy-MM-dd HH:mm:ss} | ▶️ Start Insert Attempt {retryCount + 1} | ID={id_sqlite}{Environment.NewLine}")
            Dim sql As String = "
            INSERT INTO production_actual_detail_by_op 
            (pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, qty, st_time, end_time, use_time, updated_date, pwi_id, status_transfer_sqlite , op_id , status_work ) 
            VALUES 
            (@pd, @line_cd, @wi_plan, @item_cd, @item_name, @staff_no, @seq_no, @qty, @st_time, @end_time, @use_time, @updated_date, @pwi_id, @status_sqlite , @op_id , @status_work);
            SELECT SCOPE_IDENTITY();"
            Try
                Using SQLConn As New SqlConnection(sqlConnect)
                    Using SQLCmd As New SqlCommand(sql, SQLConn)
                        SQLCmd.CommandTimeout = 120
                        SQLCmd.Parameters.AddWithValue("@pd", pd)
                        SQLCmd.Parameters.AddWithValue("@line_cd", line_cd)
                        SQLCmd.Parameters.AddWithValue("@wi_plan", wi_plan)
                        SQLCmd.Parameters.AddWithValue("@item_cd", item_cd)
                        SQLCmd.Parameters.AddWithValue("@item_name", item_name)
                        SQLCmd.Parameters.AddWithValue("@staff_no", staff_no)
                        SQLCmd.Parameters.AddWithValue("@seq_no", seq_no)
                        SQLCmd.Parameters.AddWithValue("@qty", qty)
                        SQLCmd.Parameters.AddWithValue("@st_time", st_time2)
                        SQLCmd.Parameters.AddWithValue("@end_time", end_time2)
                        SQLCmd.Parameters.AddWithValue("@use_time", use_time)
                        SQLCmd.Parameters.AddWithValue("@updated_date", currdated)
                        SQLCmd.Parameters.AddWithValue("@pwi_id", pwi_id)
                        SQLCmd.Parameters.AddWithValue("@status_sqlite", status_sqlite)
                        SQLCmd.Parameters.AddWithValue("@op_id", op_id)
                        SQLCmd.Parameters.AddWithValue("@status_work", status_work)
                        Await SQLConn.OpenAsync()
                        insertId = Convert.ToInt32(Await SQLCmd.ExecuteScalarAsync())
                    End Using
                End Using
                WriteDebugDiagnostic("Production-detail transfer status recorded")
                If insertId > 0 Then
                    ' ✅ สำเร็จ → update SQLite
                    Dim sqlUpdate = $"UPDATE act_ins_by_op SET tr_status = '1', updated_date = '{currdated}' WHERE id = '{id_sqlite}'"
                    Await api.Load_dataSQLiteAsync(sqlUpdate)
                    WriteDebugDiagnostic("Production-detail transfer status update requested")
                    'msgBox("function Insert_prd_detail_main => " & sqlUpdate)
                    'File.AppendAllText(logPath, $"{Now:yyyy-MM-dd HH:mm:ss} | ✅ Insert Success | ID={id_sqlite} | SQL_ID={insertId}{Environment.NewLine}")
                    Exit Do
                Else
                    'msgBox("function insertId => " & insertId)
                    ''File.AppendAllText(logPath, $"{Now:yyyy-MM-dd HH:mm:ss} | ⚠️ Insert Failed (ID=0) | Retry={retryCount + 1} | ID={id_sqlite}{Environment.NewLine}")
                End If
            Catch ex As Exception
                Dim functionName As String = New StackTrace().GetFrame(0).GetMethod().Name
                'Console.WriteLine($"❌ Insert Error ({functionName}): {ex.Message}")
                ' File.AppendAllText(logPath, $"{Now:yyyy-MM-dd HH:mm:ss} | ❌ Error ({functionName}) | Retry={retryCount + 1} | ID={id_sqlite} | Msg={ex.Message}{Environment.NewLine}")
            End Try
            retryCount += 1
            Await Task.Delay(2000) ' ⏳ รอแล้วลองใหม่
        Loop While insertId = 0
        Return insertId
    End Function
    Public Shared Async Function ResetTransferTimeout() As Task
        Dim sql As String = "
        UPDATE act_ins 
        SET tr_status = 0 
        WHERE tr_status = 2;
            UPDATE act_ins_by_op
            SET tr_status = 0 
            WHERE tr_status = 2;
    "
        Try
            Dim api = New api()
            Dim result As String = Await api.Load_dataSQLiteAsync(sql)
            'Console.WriteLine($"🔄 ResetTransferTimeout executed via API | Result: {result}" & sql)
            ' 🔍 Optional: Logging
            Dim logDir = "C:\sqlite3\logs"
            Directory.CreateDirectory(logDir)
            Dim logPath = $"{logDir}\reset_timeout.log"
            ' File.AppendAllText(logPath,
            '  $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | 🔄 API call Reset tr_status=0 | SQL={sql.Trim()} | Result={result}{Environment.NewLine}")

        Catch ex As Exception
            Dim functionName = New StackTrace().GetFrame(0).GetMethod().Name
            'Console.WriteLine($"❌ Error in {functionName}: {ex.Message}")

            ' ❗ Log Error
            Dim logPath = "C:\sqlite3\logs\reset_timeout_error.log"
            ''  File.AppendAllText(logPath,
            '$"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | ❌ Error in {functionName}: {ex.Message}{Environment.NewLine}")
        End Try
    End Function
    Private Shared Function GetInsertedIdFromData(pd As String, line_cd As String, wi_plan As String, seq_no As Integer) As Integer
        Try
            Using conn As New SqlConnection(sqlConnect)
                Dim sqlCheck As String = "
                SELECT TOP 1 id FROM production_actual_detail
                WHERE pd = @pd AND line_cd = @line_cd AND wi_plan = @wi_plan AND seq_no = @seq_no
                ORDER BY id DESC"
                Using cmd As New SqlCommand(sqlCheck, conn)
                    cmd.Parameters.AddWithValue("@pd", pd)
                    cmd.Parameters.AddWithValue("@line_cd", line_cd)
                    cmd.Parameters.AddWithValue("@wi_plan", wi_plan)
                    cmd.Parameters.AddWithValue("@seq_no", seq_no)
                    conn.Open()
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing Then
                        Return Convert.ToInt32(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            'Console.WriteLine("⚠️ Error while checking inserted ID: " & ex.Message)
        End Try
        Return 0
    End Function
    Public Shared Function work_complete_offline(wi As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        '  Dim reader As SqlDataReader
        '  Dim SQLConn As New SqlConnection() 'The SQL Connection
        '  Dim SQLCmd As New SqlCommand()
        Try
            ' SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            'SQLConn.Open()
            Dim api = New api()
            Dim reusult_data = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/work_complete_offline?wi=" & wi & "&currdated=" & currdated)
            'SQLCmd.Connection = SQLConn
            'SQLCmd.CommandText = "UPDATE sup_work_plan_supply_dev SET PRD_COMP_FLG = '0', PRD_COMP_DATE = '" & currdated & "' WHERE WI = '" & wi & "'"
            'reader = SQLCmd.ExecuteReader()
            'Return reader
            'SQLConn.Dispose()
            'SQLConn.Close()
            'SQLConn = Nothing
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function work_complete_offline]")
            '  SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Sub UpdateWorking(wi)
        Dim api = New api()
        Dim reusult_data = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/Update_supply_dev_Working?wi=" & wi)
    End Sub
    Public Shared Function Insert_prd_detail_defact(pd As String, line_cd As String, wi_plan As String, item_cd As String, item_name As String, staff_no As Integer, seq_no As Integer, qty As Integer, st_time As DateTime, end_time As DateTime, use_time As Double, D As Integer, tr_status As String, flg_defact As String, defact_id As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        ''msgBox(st_time.ToString("dd'/'MM'/'yyyy H':'m':'ss"))
        Dim st_time2 As String = st_time.ToString("yyyy/MM/dd H:m:s")
        Dim end_time2 As String = end_time.ToString("yyyy/MM/dd H:m:s")
        ''msgBox("INSERT INTO production_actual_detail(pd,line_cd,wi_plan,item_cd,item_name,staff_no,seq_no,qty,st_time,end_time,use_time,updated_date) VALUES ('" & pd & "','" & line_cd & "','" & wi_plan & "','" & item_cd & "','" & item_name & "','" & staff_no & "','" & seq_no & "','" & qty & "','" & st_time & "','" & end_time & "','" & use_time & "','" & currdated & "')")
        Try
            Check_connect_sqlite()
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            'SQLCmd.CommandText = "SELECT * FROM sys_user WHERE emp_id = '" & usernm & "' AND passwd = '" & passwd & "'"
            'SQLCmd.CommandText = "INSERT INTO production_actual_detail(pd,line_cd,st_time,updated_date) VALUES ('" & pd & "','" & line_cd & "','" & st_time2 & "','" & currdated & "')"
            SQLCmd.CommandText = "INSERT INTO production_defect_detail(pd,line_cd,wi_plan,item_cd,item_name,staff_no,seq_no,qty,st_time,end_time,use_time,updated_date,number_qty , flg_defact , defact_id ) VALUES ('" & pd & "','" & line_cd & "','" & wi_plan & "','" & item_cd & "','" & item_name & "','" & staff_no & "','" & seq_no & "','" & qty & "','" & st_time2 & "','" & end_time2 & "','" & use_time & "','" & currdated & "','" & number_qty & "' , '" & flg_defact & "' , '" & defact_id & "')"
            reader = SQLCmd.ExecuteReader()
            ''msgBox(reader)
            'Return reader
            reader.Close()
            Check_connect_sqlite()
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function Insert_prd_detail_defact]")
            SQLConn.Close()
            Check_connect_sqlite()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function line_status_ins(line_id As String, st_time As DateTime, end_time As DateTime, st_type As String, comp_flg As String, loss_id As String, efficiancy As String, wi_plan As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Dim st_time2 As String = st_time.ToString("yyyy/MM/dd H:m:s")
        Dim end_time2 As String = end_time.ToString("yyyy/MM/dd H:m:s")
        ''msgBox("INSERT INTO production_actual_detail(pd,line_cd,wi_plan,item_cd,item_name,staff_no,seq_no,qty,st_time,end_time,use_time,updated_date) VALUES ('" & pd & "','" & line_cd & "','" & wi_plan & "','" & item_cd & "','" & item_name & "','" & staff_no & "','" & seq_no & "','" & qty & "','" & st_time & "','" & end_time & "','" & use_time & "','" & currdated & "')")
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            'SQLCmd.CommandText = "SELECT * FROM sys_user WHERE emp_id = '" & usernm & "' AND passwd = '" & passwd & "'"
            'SQLCmd.CommandText = "INSERT INTO production_actual_detail(pd,line_cd,st_time,updated_date) VALUES ('" & pd & "','" & line_cd & "','" & st_time2 & "','" & currdated & "')"
            SQLCmd.CommandText = "INSERT INTO line_status_detail(line_id,st_time,end_time,st_type,comp_flg,loss_id,updated_date,efficientcy,wi_plan) VALUES ('" & line_id & "','" & st_time2 & "','" & end_time2 & "','" & st_type & "','" & comp_flg & "','" & loss_id & "','" & currdated & "','" & efficiancy & "','" & wi_plan & "')"
            reader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            ''msgBox(reader)
            Return reader
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function line_status_ins]")
            SQLConn.Close()
            load_show.Show()
            ' Application.Exit()
        End Try
    End Function
    Public Shared Function line_status_ins_sqlite(line_id As String, st_time As DateTime, end_time As DateTime, st_type As String, comp_flg As String, loss_id As String, efficiancy As String, wi_plan As String)
        Check_connect_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "INSERT INTO line_status_detail(line_id,st_time,end_time,st_type,comp_flg,loss_id,updated_date,efficientcy,wi_plan) VALUES ('" & line_id & "','" & st_time2 & "','" & end_time2 & "','" & st_type & "','" & comp_flg & "','" & loss_id & "','" & currdated & "','" & efficiancy & "','" & wi_plan & "')"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function line_status_ins_sqlite]" & ex.Message)
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function line_status_upd(line_id As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "UPDATE line_status_detail SET end_time = '" & currdated & "' ,comp_flg = '1', updated_date = '" & currdated & "'  WHERE id = (SELECT MAX(id) AS id FROM line_status_detail WHERE line_id = '" & line_id & "' AND comp_flg = '0') "
            reader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
            SQLConn.Dispose()
            SQLConn.Close()
            SQLConn = Nothing
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function line_status_upd]")
            SQLConn.Close()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function line_status_upd_sqlite(line_id As String)
        Check_connect_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            Try
                sqliteConn.Open()
            Catch ex As Exception
                sqliteConn.Close()
                sqliteConn.Open()
            End Try
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "UPDATE line_status_detail SET end_time = '" & currdated & "' ,comp_flg = '1', updated_date = '" & currdated & "'  WHERE id = (SELECT MAX(id) AS id FROM line_status_detail WHERE line_id = '" & line_id & "' AND comp_flg = '0') "
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function ConnectDBSQLite]")
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function GetDefectMenu(line_cd As String)
        Dim api = New api()
        Dim rs = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GetDefectMenu?line_cd=" & line_cd)
        Return rs
    End Function
    Public Shared Function GetManageDefectMenu(line_cd As String)
        Dim api = New api()
        Dim rs = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GetManageDefectMenu?line_cd=" & line_cd)
        Return rs
    End Function
    Public Shared Function GetManageReprintMenu(line_cd As String)
        Dim api = New api()
        Dim rs = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GetManageReprintMenu?line_cd=" & line_cd)
        Return rs
    End Function
    Public Shared Function GetDefectMenuMaintenance(line_cd As String)
        Dim api = New api()
        Dim rs = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GetDefectMenuMaintenance?line_cd=" & line_cd)
        Return rs
    End Function
    Public Shared Function GetSetMachine(line_cd As String)
        Dim api = New api()
        Dim rs = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GetSetMachine?line_cd=" & line_cd)
        Return rs
    End Function
    Public Shared Function GET_STATUS_DELAY_BY_LINE(line_cd As String)
        Dim api = New api()
        Dim rs = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_STATUS_DELAY_BY_LINE?line_cd=" & line_cd)
        Return rs
    End Function
    Public Shared Function Get_Last_part(line_cd As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            'SQLCmd.CommandText = "SELECT * FROM sys_user WHERE emp_id = '" & usernm & "' AND passwd = '" & passwd & "'"
            'Dim line_cd2 As String = "K1M057"
            SQLCmd.CommandText = "SELECT
	                                    *
                                    FROM
	                                    production_actual
                                    WHERE
	                                    id = (
		                                    SELECT
			                                    MAX (id) AS id
		                                    FROM
			                                    production_actual
		                                    WHERE
			                                    line_cd = '" & line_cd & "'
		                                    AND del_flg = '0'
	                                    )"
            reader = SQLCmd.ExecuteReader()
            ''msgBox(reader)
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function Get_Last_part]")
            SQLConn.Close()
            load_show.Show()
            ' Application.Exit()
        End Try
    End Function
    Public Shared Function Get_Line_id(line_cd As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            'SQLCmd.CommandText = "SELECT * FROM sys_user WHERE emp_id = '" & usernm & "' AND passwd = '" & passwd & "'"
            'Dim line_cd As String = "K1A027"
            SQLCmd.CommandText = "SELECT * FROM sys_line_mst WHERE line_cd = '" & line_cd & "'"
            reader = SQLCmd.ExecuteReader()
            ''msgBox(reader)
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function Get_Line_id]")
            SQLConn.Close()
            load_show.Show()
            '  Application.Exit()
        End Try
    End Function
    Public Shared Function Get_Line_skill_id(line_id As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            'SQLCmd.CommandText = "SELECT * FROM sys_user WHERE emp_id = '" & usernm & "' AND passwd = '" & passwd & "'"
            'Dim line_cd As String = "K1A027"
            SQLCmd.CommandText = "SELECT * FROM sys_skill_line_detail WHERE line_id = '" & line_id & "' AND enable = 1 "
            reader = SQLCmd.ExecuteReader()
            ''msgBox(reader)
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function Get_Line_skill_id]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Async Function GetPermissionLeader(emp_cd As String, line_cd As String, pd As String) As Task(Of String)
        Try
            Dim url As String = "http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_permission_LeaderDefect" &
                            "?emp_code=" & emp_cd &
                            "&line_cd=" & line_cd &
                            "&pd=" & pd
            ' ✅ แปลงให้ async โดยรันบน background thread
            Dim api = New api()
            Dim rsData As String = Await api.Load_dataAsync(url)
            Return rsData
        Catch ex As Exception
            'msgBox("❗ connect Api Fail in GetPermissionLeader = " & ex.Message)
            Return "0"
        End Try
    End Function
    Public Shared Function GetDeviceCounterByop(line_cd As String)
        Try
            Dim api = New api()
            Dim rs As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GetDeviceCounterByop?line_cd=" & line_cd)
            WriteDebugDiagnostic("GetDeviceCounterByop requested for line " & line_cd)
            ' ✅ แปลงให้ async โดยรันบน background thread
            Return rs
        Catch ex As Exception
            'msgBox("❗ connect Api Fail in GetPermissionLeader = " & ex.Message)
            Return "0"
        End Try
    End Function
    Public Shared Function chk_user_skill_line(emp_cd As String, line_cd As String)
        ' Dim reader As SqlDataReader
        ' Dim SQLConn As New SqlConnection() 'The SQL Connection
        ' Dim SQLCmd As New SqlCommand()
        Try
            Dim api = New api()
            Dim result_worker = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_permission_worker?emp_code=" & emp_cd & "&line_cd=" & line_cd)
            WriteDebugDiagnostic("Get_permission_worker requested for line " & line_cd)
            Return result_worker
            ' SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            ' SQLConn.Open()
            ' SQLCmd.Connection = SQLConn
            ' SQLCmd.CommandText = "Select su.*, sk.sk_id From sys_user As su Left Join sys_user_skill_detail AS sk On su.su_id=sk.su_id WHERE su.emp_id = '" & emp_cd & "' And sk.enable = '1' AND su.enable = '1'"
            ' reader = SQLCmd.ExecuteReader()
            ' Return reader
        Catch ex As Exception
            'msgBox("MSSQL Database connect failed. Please contact PC System [Function chk_user_skill_line]")
            '    SQLConn.Close()
            '   Application.Exit()
        End Try
    End Function
    Public Shared Function get_all_skill()
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "Select * From sys_skill_chart_mst WHERE enable = '1' ORDER BY sk_id ASC"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function get_all_skill]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function get_department()
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "Select * From sys_department WHERE enable = '1'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function get_department]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function update_tagprint(wi As String, flgUpdate As String, conditionflg As String)  '2 , 0
        ' Dim reader As SqlDataReader
        'Dim SQLConn As New SqlConnection() 'The SQL Connection
        ' Dim SQLCmd As New SqlCommand()
        Try
            '  SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            '  SQLConn.Open()
            '  SQLCmd.Connection = SQLConn
            '  SQLCmd.CommandText = "update tag_print_detail set flg_control = '2' where flg_control = '0' and  wi = '" & wi & "'"
            '  reader = SQLCmd.ExecuteReader()
            '  reader.Close()
            'Return reader
            Dim api = New api()
            Dim result = api.Load_data("http://" & svApi & "/apiShopfloor/index.php/updateDatadefect/update_tagprint_detail?wi=" & wi & "&flgUpdate=" & flgUpdate & "&conditionflg=" & conditionflg)
            ''Console.WriteLine("http://" & svApi & "/apiShopfloor/index.php/updateDatadefect/update_tagprint_detail?wi=" & wi & "&flgUpdate=" & flgUpdate & "&conditionflg=" & conditionflg)
            Return result
        Catch ex As Exception
            '  SQLConn.Close()
        End Try
    End Function
    Public Shared Function update_tagprintforDefect(wi As String, flgUpdate As String, conditionflg As String, pwi_id As String, BoxNo As Integer, goodQty As String, cupprint As String)
        ' Dim reader As SqlDataReader
        'Dim SQLConn As New SqlConnection() 'The SQL Connection
        ' Dim SQLCmd As New SqlCommand()
        '2 ,1 
        Try
            '  SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            '  SQLConn.Open()
            '  SQLCmd.Connection = SQLConn
            '  SQLCmd.CommandText = "update tag_print_detail set flg_control = '2' where flg_control = '0' and  wi = '" & wi & "'"
            '  reader = SQLCmd.ExecuteReader()
            '  reader.Close()
            'Return reader
            Dim mdDefect = New modelDefect
            ''Console.WriteLine("http: //" & svApi & "/apiShopfloor/index.php/updateDatadefect/update_tagprint_detailforDefect?wi=" & wi & "&flgUpdate=" & flgUpdate & "&conditionflg=" & conditionflg & "&pwi_id=" & pwi_id & "&BoxNo=" & BoxNo & "&goodQty=" & goodQty & "&cupprint=" & cupprint)
            If mdDefect.mGetDataEnableFGPart(MainFrm.Label4.Text) = "1" Then
                Dim api = New api()
                Dim result = api.Load_data("http://" & svApi & "/apiShopfloor/index.php/updateDatadefect/update_tagprint_detailforDefect?wi=" & wi & "&flgUpdate=" & flgUpdate & "&conditionflg=" & conditionflg & "&pwi_id=" & pwi_id & "&BoxNo=" & BoxNo & "&goodQty=" & goodQty & "&cupprint=" & cupprint)
                Return result
            Else
                Return 0
            End If
        Catch ex As Exception
            '  SQLConn.Close()
        End Try
    End Function
    Public Shared Function update_tagprint_sub(wi As String, flgUpdate As String, conditionflg As String)
        ' Dim reader As SqlDataReader
        ' Dim SQLConn As New SqlConnection() 'The SQL Connection
        ' Dim SQLCmd As New SqlCommand()
        Try
            '  SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            '  SQLConn.Open()
            '  SQLCmd.Connection = SQLConn
            '  SQLCmd.CommandText = "update tag_print_detail_sub set flg_control = '2' where flg_control = '0' and  wi = '" & wi & "'"
            '  reader = SQLCmd.ExecuteReader()
            '  reader.Close()
            Dim api = New api()
            Dim result = api.Load_data("http://" & svApi & "/apiShopfloor/index.php/updateDatadefect/update_tagprint_sub?wi=" & wi & "&flgUpdate=" & flgUpdate & "&conditionflg=" & conditionflg)
            Return result
        Catch ex As Exception
            '    SQLConn.Close()
        End Try
    End Function
    Public Shared Function update_tagprint_main(wi As String, flgUpdate As String, conditionflg As String)
        'Dim reader As SqlDataReader
        'Dim SQLConn As New SqlConnection() 'The SQL Connection
        'Dim SQLCmd As New SqlCommand()

        Try
            '   SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            '   SQLConn.Open()
            '   SQLCmd.Connection = SQLConn
            '   SQLCmd.CommandText = "update tag_print_detail_main set flg_control = '2' where flg_control = '0' and  tag_wi_no = '" & wi & "'"
            '   reader = SQLCmd.ExecuteReader()
            '   reader.Close()
            Dim api = New api()
            ''Console.WriteLine("http://" & svApi & "/apiShopfloor/index.php/updateDatadefect/update_tagprint_main?wi=" & wi & "&flgUpdate=" & flgUpdate & "&conditionflg=" & conditionflg)
            Dim result = api.Load_data("http://" & svApi & "/apiShopfloor/index.php/updateDatadefect/update_tagprint_main?wi=" & wi & "&flgUpdate=" & flgUpdate & "&conditionflg=" & conditionflg)
            Return result
            'Return reader
        Catch ex As Exception
            ' SQLConn.Close()
        End Try
    End Function
    Public Shared Async Function Trasnfer_tag_print_detail(
    wi As String, qr_detail As String, box_no As Integer, print_count As Integer,
    seq_no As String, shift As String, flg_control As Integer, item_cd As String,
    pwi_id As String, tag_group_no As String, goodQty As Integer,
    Gobal_NEXT_PROCESS As String, tr_status As Integer) As Task(Of Integer)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss")
        Backoffice_model.update_tagprint(wi, "2", "0")
        Dim retryCount As Integer = 0
        Dim maxRetries As Integer = 3
        Dim baseDelay As Integer = 2000 ' 2 seconds
        Do
            Dim errorOccured As Boolean = False
            Dim exMsg As String = ""
            Try
                Using SQLConn As New SqlConnection(sqlConnect)
                    Using SQLCmd As New SqlCommand("
                    INSERT INTO tag_print_detail 
                    (wi, qr_detail, box_no, print_count, created_date, updated_date, seq_no, shift, next_proc, flg_control, pwi_id, tag_group_no) 
                    VALUES (@wi, @qr_detail, @box_no, @print_count, @created_date, @updated_date, @seq_no, @shift, @next_proc, @flg_control, @pwi_id, @tag_group_no); 
                    SELECT SCOPE_IDENTITY();", SQLConn)
                        SQLCmd.Parameters.AddWithValue("@wi", wi)
                        SQLCmd.Parameters.AddWithValue("@qr_detail", qr_detail)
                        SQLCmd.Parameters.AddWithValue("@box_no", box_no)
                        SQLCmd.Parameters.AddWithValue("@print_count", print_count)
                        SQLCmd.Parameters.AddWithValue("@created_date", currdated)
                        SQLCmd.Parameters.AddWithValue("@updated_date", currdated)
                        SQLCmd.Parameters.AddWithValue("@seq_no", seq_no)
                        SQLCmd.Parameters.AddWithValue("@shift", shift)
                        SQLCmd.Parameters.AddWithValue("@next_proc", Gobal_NEXT_PROCESS)
                        SQLCmd.Parameters.AddWithValue("@flg_control", flg_control)
                        SQLCmd.Parameters.AddWithValue("@pwi_id", pwi_id)
                        SQLCmd.Parameters.AddWithValue("@tag_group_no", If(String.IsNullOrEmpty(tag_group_no), "1", tag_group_no))
                        Await SQLConn.OpenAsync()
                        Dim insertId As Integer = Convert.ToInt32(Await SQLCmd.ExecuteScalarAsync())
                        Return insertId
                    End Using
                End Using
            Catch ex As Exception
                errorOccured = True
                exMsg = ex.Message
            End Try
            If errorOccured Then
                retryCount += 1
                If retryCount >= maxRetries Then
                    'Console.WriteLine($"❌ Trasnfer_tag_print_detail failed after {maxRetries} retries: {exMsg}")
                    Return 0
                Else
                    'Console.WriteLine($"⚠️ Trasnfer_tag_print_detail error, retry {retryCount}/{maxRetries}: {exMsg}")
                    Await Task.Delay(baseDelay * retryCount)
                End If
            End If
        Loop
    End Function


    Public Shared Function Insert_tag_print(wi As String, qr_detail As String, box_no As Integer, print_count As Integer, seq_no As String, shift As String, flg_control As Integer, item_cd As String, pwi_id As String, tag_group_no As String, goodQty As Integer, Gobal_NEXT_PROCESS As String, tr_status As Integer, Optional preserveExistingIncompleteTags As Boolean = False) As Integer
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        If Not preserveExistingIncompleteTags Then update_tagprint(wi, "2", "0")
        Dim SQLConn As New SqlConnection()
        Dim SQLCmd As New SqlCommand()
        Try
            ' 'Console.WriteLine("F1")
            ' กำหนดค่าเริ่มต้นให้ tag_group_no ถ้ามันเป็น Nothing หรือ ว่างเปล่า
            ' Set the connection string and open the connection
            SQLConn.ConnectionString = sqlConnect
            SQLConn.Open()
            SQLCmd.Connection = SQLConn

            ' Idempotency check: verify whether a tag for this exact (PWI, Seq, BoxNo) already exists
            If Not String.IsNullOrWhiteSpace(Trim(pwi_id)) AndAlso box_no > 0 Then
                Using checkCmd As New SqlCommand(
                    "SELECT TOP 1 id FROM tag_print_detail WITH (NOLOCK) " &
                    "WHERE pwi_id = @check_pwi_id AND (seq_no = @check_seq_no OR TRY_CONVERT(INT, seq_no) = TRY_CONVERT(INT, @check_seq_no)) " &
                    "AND box_no = @check_box_no AND flg_control IN ('0','1','2') ORDER BY id DESC;", SQLConn)
                    checkCmd.Parameters.AddWithValue("@check_pwi_id", Trim(pwi_id))
                    checkCmd.Parameters.AddWithValue("@check_seq_no", If(seq_no, String.Empty).Trim())
                    checkCmd.Parameters.AddWithValue("@check_box_no", box_no)
                    Dim existingIdObj As Object = checkCmd.ExecuteScalar()
                    If existingIdObj IsNot Nothing AndAlso Not IsDBNull(existingIdObj) Then
                        Dim existingId As Integer = Convert.ToInt32(existingIdObj)
                        If existingId > 0 Then
                            Return existingId
                        End If
                    End If
                End Using
            End If
            ' Prepare the SQL command
            SQLCmd.CommandText = "INSERT INTO tag_print_detail (wi, qr_detail, box_no, print_count, created_date, updated_date, seq_no, shift, next_proc, flg_control, pwi_id, tag_group_no) " &
                                 "VALUES (@wi, @qr_detail, @box_no, @print_count, @created_date, @updated_date, @seq_no, @shift, @next_proc, @flg_control, @pwi_id, @tag_group_no); " &
                                 "SELECT SCOPE_IDENTITY();"
            ' Add parameters
            SQLCmd.Parameters.AddWithValue("@wi", wi)
            SQLCmd.Parameters.AddWithValue("@qr_detail", qr_detail)
            SQLCmd.Parameters.AddWithValue("@box_no", box_no)
            SQLCmd.Parameters.AddWithValue("@print_count", print_count)
            SQLCmd.Parameters.AddWithValue("@created_date", currdated)
            SQLCmd.Parameters.AddWithValue("@updated_date", currdated)
            SQLCmd.Parameters.AddWithValue("@seq_no", seq_no)
            SQLCmd.Parameters.AddWithValue("@shift", shift)
            SQLCmd.Parameters.AddWithValue("@next_proc", Gobal_NEXT_PROCESS)
            SQLCmd.Parameters.AddWithValue("@flg_control", flg_control)
            SQLCmd.Parameters.AddWithValue("@pwi_id", pwi_id)
            SQLCmd.Parameters.AddWithValue("@tag_group_no", "1") ' ✅ ใช้ค่า param ป้องกัน null
            ''Console.WriteLine("F221")
            ' Execute the query and get the insert id
            Try
                If My.Computer.Network.Ping(svp_ping) Then
                    model_api_sqlite.mas_Insert_tag_print(wi, qr_detail, box_no, print_count, seq_no, shift, flg_control, item_cd, pwi_id, tag_group_no, goodQty, Gobal_NEXT_PROCESS, "1", preserveExistingIncompleteTags)
                    Dim insertId As Integer = Convert.ToInt32(SQLCmd.ExecuteScalar())
                    ' 'Console.WriteLine("F1333")
                    Return insertId
                Else
                    model_api_sqlite.mas_Insert_tag_print(wi, qr_detail, box_no, print_count, seq_no, shift, flg_control, item_cd, pwi_id, tag_group_no, goodQty, Gobal_NEXT_PROCESS, "0", preserveExistingIncompleteTags)
                    Return 0
                End If
            Catch ex As Exception
                ''Console.WriteLine("catch TRY ===>" & ex.Message)
                model_api_sqlite.mas_Insert_tag_print(wi, qr_detail, box_no, print_count, seq_no, shift, flg_control, item_cd, pwi_id, tag_group_no, goodQty, Gobal_NEXT_PROCESS, "0", preserveExistingIncompleteTags)
                Return 0
            End Try
        Catch ex As Exception
            ' 'Console.WriteLine("F555555")
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function Insert_tag_print]")
            '  'Console.WriteLine("Error tag_print_detail: " & ex.Message)
            model_api_sqlite.mas_Insert_tag_print(wi, qr_detail, box_no, print_count, seq_no, shift, flg_control, item_cd, pwi_id, tag_group_no, goodQty, Gobal_NEXT_PROCESS, "0", preserveExistingIncompleteTags)
            Return 0 ' Return 0 in case of error
        Finally
            ' Ensure connection is closed
            If SQLConn.State = ConnectionState.Open Then
                SQLConn.Close()
            End If
        End Try
    End Function
    Public Shared Sub ins_log_print(created_by As String, table_created As String, log_ref_tag_id As String)
        Dim api = New api()
        Dim result = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/Api_insert_log_reprint/ins_los_reprint_test_system?created_by=" & created_by & "&table_created=" & table_created & "&log_ref_tag_id=" & log_ref_tag_id)
    End Sub
    Public Shared Function Get_tag_group_no()
        Dim api = New api()
        Dim result = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_tag_group_no")
        Return result
    End Function
    Public Shared Async Function Trasnfer_tag_print_main(tag_ref_str_id As String, tag_ref_end_id As String, line_cd As String, tag_qr_detail As String, tag_batch_no As String, tag_next_proc As String, flg_control As String, created_date As String, updated_date As String, wi As String, pwi_no As String, tag_group_no As String, tr_status As String, seq_no As String, lot_no As String) As Task(Of Integer)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        ' 'Console.WriteLine("Transfer_tag_print_main ====>")
        ' อัปเดตค่าใน tag_print
        update_tagprint(wi, "2", "0")
        update_tagprint_main(wi, "2", "0")
        Dim start_id As String = Get_ref_start_id(wi, seq_no, lot_no)
        Dim end_id As String = Get_ref_end_id(wi, seq_no, lot_no)
        Dim insertedId As Integer = 0 ' ค่า default สำหรับกรณี error
        Using SQLConn As New SqlConnection(sqlConnect)
            Using SQLCmd As New SqlCommand("
            INSERT INTO tag_print_detail_main 
                (tag_ref_str_id, tag_ref_end_id, line_cd, tag_qr_detail, tag_batch_no, tag_next_proc, flg_control, created_date, updated_date, tag_wi_no, pwi_id, tag_group_no) 
            OUTPUT INSERTED.tag_id
            VALUES 
                (@start_id, @end_id, @line_cd, @tag_qr_detail, @tag_batch_no, @tag_next_proc, @flg_control, @created_date, @updated_date, @wi, @pwi_no, @tag_group_no)", SQLConn)
                SQLCmd.Parameters.AddWithValue("@start_id", start_id)
                SQLCmd.Parameters.AddWithValue("@end_id", end_id)
                SQLCmd.Parameters.AddWithValue("@line_cd", line_cd)
                SQLCmd.Parameters.AddWithValue("@tag_qr_detail", tag_qr_detail)
                SQLCmd.Parameters.AddWithValue("@tag_batch_no", tag_batch_no)
                SQLCmd.Parameters.AddWithValue("@tag_next_proc", tag_next_proc)
                SQLCmd.Parameters.AddWithValue("@flg_control", flg_control)
                SQLCmd.Parameters.AddWithValue("@created_date", currdated)
                SQLCmd.Parameters.AddWithValue("@updated_date", currdated)
                SQLCmd.Parameters.AddWithValue("@wi", wi)
                SQLCmd.Parameters.AddWithValue("@pwi_no", pwi_no)
                SQLCmd.Parameters.AddWithValue("@tag_group_no", tag_group_no)
                Try
                    SQLConn.Open()
                    insertedId = Convert.ToInt32(SQLCmd.ExecuteScalar()) ' รับค่า PK ที่ insert กลับมา
                    'Console.WriteLine("Inserted ID: " & insertedId)
                    Return insertedId
                Catch ex As Exception
                    ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function Transfer_tag_print_main]" & vbCrLf & ex.Message)
                    Return insertedId
                End Try
            End Using
        End Using
        Return insertedId
    End Function

    Public Shared Function Insert_tag_print_main(wi As String, qr_detail As String, batch_no As Integer, print_count As Integer, seq_no As String, shift As String, flg_control As Integer, item_cd As String, pwi_id As String, tag_group_no As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        update_tagprint(wi, "2", "0")
        update_tagprint_main(wi, "2", "0")
        Dim start_id As String = Get_ref_start_id(wi, seq_no, Working_Pro.Label18.Text)
        Dim end_id As String = Get_ref_end_id(wi, seq_no, Working_Pro.Label18.Text)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "INSERT INTO tag_print_detail_main(tag_ref_str_id ,tag_ref_end_id , line_cd , tag_qr_detail , tag_batch_no , tag_next_proc , flg_control , created_date , updated_date , tag_wi_no , pwi_id , tag_group_no) VALUES ('" & start_id & "','" & end_id & "','" & MainFrm.Label4.Text & "','" & qr_detail & "' ,'" & batch_no & "' ,'" & F_NEXT_PROCESS(item_cd) & "','" & flg_control & "','" & currdated & "','" & currdated & "','" & wi & "','" & pwi_id & "' ,'" & tag_group_no & "')"
            reader = SQLCmd.ExecuteReader()
        Catch ex As Exception
            'msgBox("MSSQL Database connect failed. Please contact PC System [Function Insert_tag_print_main]")
            SQLConn.Close()
        End Try
        Return 1
    End Function
    Public Shared Function Get_ref_start_id(wi As String, seq_no As String, lot_no As String)
        Dim api = New api()
        ''Console.WriteLine("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_ref_start_id?wi=" & wi & "&seq_no=" & seq_no & "&lot_no=" & lot_no)
        Dim result = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_ref_start_id?wi=" & wi & "&seq_no=" & seq_no & "&lot_no=" & lot_no)
        Return result
    End Function
    Public Shared Function Get_ref_end_id(wi As String, seq_no As String, lot_no As String)
        Dim api = New api()
        ''Console.WriteLine("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_ref_end_id?wi=" & wi & "&seq_no=" & seq_no & "&lot_no=" & lot_no)
        Dim result = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_ref_end_id?wi=" & wi & "&seq_no=" & seq_no & "&lot_no=" & lot_no)
        Return result
    End Function
    Public Shared Function get_qr_detail_sub(ref_id)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection()
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[GET_DATA_SUB] @REF_ID = '" & ref_id & "'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            'msgBox("MSSQL Database connect failed. Please contact PC System [Function get_qr_detail_sub]")
            SQLConn.Close()
        End Try
    End Function
    Public Shared Async Function Transfer_Tag_Print_sub(wi As String, tag_print_detail_id As String, line_cd As String, tag_qr_detail As String, flg_control As String, created_date As String, updated_date As String, tag_wi_no As String, tag_group_no As String) As Task(Of String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            update_tagprint_sub(wi, "2", "0")
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "INSERT INTO tag_print_detail_sub(tag_ref_id , line_cd , tag_qr_detail , flg_control , created_date , updated_date , tag_wi_no , tag_group_no) VALUES ('" & tag_print_detail_id & "','" & line_cd & "','" & tag_qr_detail & "' ,'1' , '" & currdated & "' , '" & currdated & "' , '" & wi & "' , '" & tag_group_no & "')"
            reader = SQLCmd.ExecuteReader()
        Catch ex As Exception
            'msgBox("MSSQL Database connect failed. Please contact PC System [Function Insert_tag_print_sub]")
            SQLConn.Close()
        End Try
    End Function
    Public Shared Function Insert_tag_print_sub(ref_id As String, line As String, qr_code As String, wi As String, tag_group_no As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            update_tagprint_sub(wi, "2", "0")
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "INSERT INTO tag_print_detail_sub(tag_ref_id , line_cd , tag_qr_detail , flg_control , created_date , updated_date , tag_wi_no , tag_group_no) VALUES ('" & ref_id & "','" & line & "','" & qr_code & "' ,'" & print_back.check_tagprint_main() & "' , '" & currdated & "' , '" & currdated & "' , '" & wi & "' , '" & tag_group_no & "')"
            'Console.WriteLine(SQLCmd.CommandText)
            reader = SQLCmd.ExecuteReader()
        Catch ex As Exception
            'msgBox("MSSQL Database connect failed. Please contact PC System [Function Insert_tag_print_sub]")
            SQLConn.Close()
        End Try
        Return 1
    End Function

    Public Shared Function Insert_user(emp_cd As String, fname As String, lname As String, dep_id As Integer, created_by As String, group_id As Integer)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "INSERT INTO sys_user(emp_id,passwd,fname,lname,department_id,enable,created_date,created_by,updated_date,updated_by,sug_id) VALUES ('" & emp_cd & "','Sysadmin!','" & fname & "','" & lname & "','" & dep_id & "','1','" & currdated & "','" & created_by & "','" & currdated & "','" & created_by & "','" & group_id & "')"
            reader = SQLCmd.ExecuteReader()
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function

    Public Shared Function Insert_skill(sk_des As String, emp_cd As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()

        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "INSERT INTO sys_skill_chart_mst(sk_name,sk_description,enable,created_date,created_by,updated_date,updated_by) VALUES ('" & sk_des & "','" & sk_des & "','1','" & currdated & "','" & emp_cd & "','" & currdated & "','" & emp_cd & "')"
            reader = SQLCmd.ExecuteReader()
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function

    Public Shared Function del_skill(sk_id As String, emp_cd As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()

        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn

            SQLCmd.CommandText = "UPDATE sys_skill_chart_mst SET enable = '0', updated_date = '" & currdated & "', updated_by = '" & emp_cd & "'  WHERE sk_id = '" & sk_id & "' "
            reader = SQLCmd.ExecuteReader()

            Return reader
            SQLConn.Dispose()
            SQLConn.Close()
            SQLConn = Nothing
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function
    Public Shared Function work_complete(wi As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection()
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect
            SQLConn.Open()
            SQLCmd.Connection = SQLConn

            SQLCmd.CommandText = "UPDATE sup_work_plan_supply_dev SET PRD_COMP_FLG = '9', PRD_COMP_DATE = '" & currdated & "' WHERE WI = '" & wi & "' "
            SQLCmd.CommandTimeout = 10
            SQLCmd.ExecuteNonQuery()
            SQLConn.Close()
            Return True
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function

    Public Shared Function edit_skill(sk_id As Integer, sk_des As String, emp_cd As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection()
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "UPDATE sys_skill_chart_mst SET sk_name = '" & sk_des & "',sk_description = '" & sk_des & "',  updated_date = '" & currdated & "', updated_by = '" & emp_cd & "'  WHERE sk_id = '" & sk_id & "' "
            reader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
            SQLConn.Dispose()
            SQLConn.Close()
            SQLConn = Nothing
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function

    Public Shared Function get_user_last_id()
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection()
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "SELECT IDENT_CURRENT('sys_user') AS last_id"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function

    Public Shared Function Insert_user_skill(su_id As Integer, sk_id As Integer, created_by As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()

        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "INSERT INTO sys_user_skill_detail(su_id,sk_id,enable,created_date,created_by,updated_date,updated_by) VALUES ('" & su_id & "','" & sk_id & "','1','" & currdated & "','" & created_by & "','" & currdated & "','" & created_by & "')"
            reader = SQLCmd.ExecuteReader()
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function


    Public Shared Function Insert_line_skill(line_id As Integer, sk_id As Integer, process_no As String, created_by As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()

        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "INSERT INTO sys_skill_line_detail(line_id,sk_id,process_no,enable,created_date,created_by,updated_date,updated_by) VALUES ('" & line_id & "','" & sk_id & "','" & process_no & "','1','" & currdated & "','" & created_by & "','" & currdated & "','" & created_by & "')"
            reader = SQLCmd.ExecuteReader()
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function


    Public Shared Function get_all_line()
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "Select * FROM sys_line_mst WHERE enable = '1' ORDER BY line_id ASC"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function
    Public Shared Function del_line(line_id As String, emp_cd As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "UPDATE sys_line_mst SET enable = '0', updated_date = '" & currdated & "', updated_by = '" & emp_cd & "'  WHERE line_id = '" & line_id & "' "
            reader = SQLCmd.ExecuteReader()
            Return reader
            SQLConn.Dispose()
            SQLConn.Close()
            SQLConn = Nothing
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function
    Public Shared Function get_all_user()
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "Select su.*, sd.sec_name From sys_user As su Left Join sys_department AS sd On su.department_id=sd.dep_id WHERE su.enable = '1' AND su.sug_id <> '1' ORDER BY su.emp_id ASC"
            reader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function
    Public Shared Function get_tag_reprint_spaceial(wi As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[REPRINT_SPACEIAL] @WI = '" & wi & "'"
            reader = SQLCmd.ExecuteReader()
            Dim result = reader
            Return result
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function
    Public Shared Function get_tag_reprint_sum_detail(wi As String, lot As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[REPRINT_NORAML_SPC] @WI = '" & wi & "' and @lot_no = '" & lot_no & "'"
            reader = SQLCmd.ExecuteReader()
            Dim result As Integer = 0
            While reader.Read()
                result = reader("c_id").ToString()
            End While
            reader.Close()
            Return result
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function
    Public Shared Function check_line_reprint()
        Dim api = New api()
        Dim result_api_checkper = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/Api_check_data/check_line_reprint?line_cd=" & MainFrm.Label4.Text)
        Return result_api_checkper
    End Function
    Public Shared Function B_check_format_tag()
        Dim api = New api()
        Dim reusult_data = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/Api_check_data/check_format_tag?line_cd=" & MainFrm.Label4.Text)
        Return reusult_data
    End Function

    Public Shared Function get_tag_reprint_detail(wi As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[REPRINT_NORAML_BY_BOX] @WI = '" & wi & "'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            load_show.Show()
        End Try
    End Function
    Public Shared Function get_tag_reprint_batch(wi As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            Try
                SQLConn.Open()
            Catch ex As Exception
                SQLConn.Close()
                SQLConn.Open()
            End Try
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "EXEC [dbo].[REPRINT_BATCH] @WI = '" & wi & "'"
            reader = SQLCmd.ExecuteReader()
            Dim result = reader
            Return result
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function
    Public Shared Function update_data_new_qr_detail(qr_code As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "select count(id) as c_id from tag_print_detail where qr_detail = '" & qr_code & "'"
            ''Console.WriteLine("SQLCmd.CommandText===>" & SQLCmd.CommandText)
            Dim LoadSQL As SqlDataReader = SQLCmd.ExecuteReader()
            Dim check_status As Integer = 0
            While LoadSQL.Read()
                check_status = LoadSQL("c_id").ToString()
            End While
            LoadSQL.Close()
            Dim wi2 As String = "NO_DATA"
            Dim qr_detailss2 As String = "NO_DATA"
            Dim box_no2 As String = "NO_DATA"
            Dim plan_seq2 As String = "NO_DATA"
            Dim shift2 As String = "NO_DATA"
            If check_status = 0 Then
                SQLCmd.CommandText = "select * from tag_print_detail_genarate where new_qr_detail = '" & qr_code & "'"
                Dim LoadSQL2 As SqlDataReader = SQLCmd.ExecuteReader()
                While LoadSQL2.Read()
                    wi2 = LoadSQL2("wi").ToString()
                    qr_detailss2 = LoadSQL2("new_qr_detail").ToString()
                    box_no2 = LoadSQL2("box_no").ToString()
                    plan_seq2 = qr_detailss2.Substring(95, 3)
                    shift2 = LoadSQL2("shift").ToString()
                End While
                LoadSQL2.Close()
                Dim arr_item_cd = qr_detailss2.Substring(19).Split(" ")
                Dim item_cd As String = arr_item_cd(0)
                Insert_tag_print(wi2, qr_detailss2, box_no2, 1, plan_seq2, shift2, "", item_cd, pwi_id, "", 0, "Next process ", 1)
                SQLCmd.CommandText = "update tag_print_detail_genarate set flg_print = '0' where new_qr_detail = '" & qr_code & "'"
                reader = SQLCmd.ExecuteReader()
                reader.Close()
            Else
                Dim wi3 As String = "NO_DATA"
                Dim qr_detailss3 As String = "NO_DATA"
                Dim box_no3 As String = "NO_DATA"
                Dim seq_plan3 As String = "NO_DATA"
                Dim shift3 As String = "NO_DATA"
                Dim seq_box3 As String = "NO_DATA"
                Dim qty As String = "NODATA"
                SQLCmd.CommandText = "select * from tag_print_detail_genarate where new_qr_detail = '" & qr_code & "'"
                Dim LoadSQL3 As SqlDataReader = SQLCmd.ExecuteReader()
                While LoadSQL3.Read()
                    wi3 = LoadSQL3("wi").ToString()
                    seq_box3 = LoadSQL3("box_no").ToString()
                    qr_detailss3 = LoadSQL3("new_qr_detail").ToString()
                    seq_plan3 = qr_detailss3.Substring(95, 3)
                    qty = Trim(seq_plan3.Substring(52, 6))
                End While
                LoadSQL3.Close()
                If wi3 = "NO_DATA" Then
                    SQLCmd.CommandText = "select * from tag_print_detail where qr_detail = '" & qr_code & "'"
                    Dim LoadSQL1 As SqlDataReader = SQLCmd.ExecuteReader()
                    Dim wi4 As String = "NODATA"
                    Dim box_no4 As String = "NODATA"
                    Dim qr_detail4 As String = "NODATA"
                    Dim seq_plan4 As String = "NODATA"
                    Dim qtys As String = "NODATA"
                    While LoadSQL1.Read()
                        wi4 = LoadSQL1("wi").ToString()
                        box_no4 = LoadSQL1("box_no").ToString()
                        qr_detail4 = LoadSQL1("qr_detail").ToString()
                        seq_plan4 = qr_detail4.Substring(95, 3)
                        qtys = Trim(qr_detail4.Substring(52, 6))
                    End While
                    LoadSQL1.Close()
                    update_print_count(wi4, seq_plan4, box_no4, qtys)
                Else
                    update_print_count(wi3, seq_plan3, seq_box3, qty)
                End If
            End If
        Catch
        End Try
    End Function
    Public Shared Function update_data_new_qr_detail_main(qr_code As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "select * from tag_print_detail_main where tag_qr_detail = '" & qr_code & "'"
            Dim LoadSQL1 As SqlDataReader = SQLCmd.ExecuteReader()
            Dim id As String = ""
            While LoadSQL1.Read()
                id = LoadSQL1("tag_id").ToString()
            End While
            LoadSQL1.Close()
            ins_log_print(MainFrm.Label4.Text, "3", id)
        Catch
            'msgBox("error function update_data_new_qr_detail_main == ")
        End Try
    End Function
    Public Shared Function get_tag_reprint_detail_genarate(wi As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try

            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "select * from tag_print_detail_genarate where wi = '" & wi & "'  and flg_print = '1'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function
    Public Shared Function get_sec_user(sec_name As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "Select su.*, sd.sec_name From sys_user As su Left Join sys_department AS sd On su.department_id=sd.dep_id WHERE su.enable = '1' AND su.sug_id <> '1' AND sd.sec_name = '" & sec_name & "' ORDER BY su.emp_id ASC"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function

    Public Shared Function Get_data_picking(ref_id As String)
        Dim api = New api()
        Dim result_api_checkper = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_data_picking?ref_id=" & ref_id)
        Return result_api_checkper
    End Function
    Public Shared Function del_user(su_id As String, emp_cd As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()

        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "UPDATE sys_user SET enable = '0', updated_date = '" & currdated & "', updated_by = '" & emp_cd & "'  WHERE emp_id = '" & su_id & "' "
            reader = SQLCmd.ExecuteReader()
            Return reader
            SQLConn.Dispose()
            SQLConn.Close()
            SQLConn = Nothing
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function

    Public Shared Function edit_user(emp_id As String, fname As String, lname As String, dept_id As Integer, emp_cd As String, sug_id As Integer, su_id As Integer)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "UPDATE sys_user SET emp_id = '" & emp_id & "',fname = '" & fname & "',lname = '" & lname & "',department_id = '" & dept_id & "',  updated_date = '" & currdated & "', updated_by = '" & emp_cd & "' , sug_id = '" & sug_id & "' WHERE su_id = '" & su_id & "' "
            reader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
            SQLConn.Dispose()
            SQLConn.Close()
            SQLConn = Nothing
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function

    Public Shared Function del_user_skill_old(su_id As String, emp_cd As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "UPDATE sys_user_skill_detail SET enable = '0', updated_date = '" & currdated & "', updated_by = '" & emp_cd & "'  WHERE su_id = '" & su_id & "' "
            reader = SQLCmd.ExecuteReader()
            Return reader
            SQLConn.Dispose()
            SQLConn.Close()
            SQLConn = Nothing
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function

    Public Shared Function del_line_skill_old(line_id As String, emp_cd As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "UPDATE sys_skill_line_detail SET enable = '0', updated_date = '" & currdated & "', updated_by = '" & emp_cd & "'  WHERE line_id = '" & line_id & "' "
            reader = SQLCmd.ExecuteReader()
            Return reader
            SQLConn.Dispose()
            SQLConn.Close()
            SQLConn = Nothing
        Catch ex As Exception
            'msgBox("MSSQL Database connect failed. Please contact PC System [Function del_line_skill_old]")
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function


    Public Shared Function get_user_detail(emp_id As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "Select * From sys_user WHERE emp_id = '" & emp_id & "'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function

    Public Shared Function get_line_skill(line_cd As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "SELECT skl.* FROM sys_skill_line_detail AS skl LEFT JOIN sys_line_mst AS sl ON skl.line_id = sl.line_id WHERE sl.line_cd = '" & line_cd & "' AND skl.enable = '1' AND sl.enable = '1' ORDER BY skl.process_no ASC"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function


    Public Shared Function get_user_skill(emp_id As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "Select sk_id From sys_user_skill_detail WHERE su_id = '" & emp_id & "' AND enable = '1' ORDER BY sk_id ASC"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function
    Public Shared Function chk_adm_login(emp_cd As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "Select * From sys_user WHERE emp_id = '" & emp_cd & "' And enable = '1'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function
    Public Shared Function get_prd_plan_reprint(line_cd As String)
        Dim reader As SqlDataReader
        Dim reader2 As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "Select sw.WI,sw.ITEM_CD,sw.ITEM_NAME,sw.QTY,sw.qty - SUM (ISNULL(pa.act_qty, 0 )) as 'remain_qty',ISNULL(pa.prd_flg , 0 ) as 'prd_flg',sw.WORK_ODR_DLV_DATE AS 'DLV_DATE', sw.LOCATION_PART,sw.PS_UNIT_NUMERATOR,sw.CT,COUNT(pa.seq_no) AS seq_count,sw.MODEL , sw.PRODUCT_TYP  from sup_work_plan_supply_dev as sw full outer JOIN production_actual as pa on sw.WI = pa.wi WHERE sw.LINE_CD = '" & line_cd & "' and sw.LVL = '1' and (pa.comp_flg <> '1' or pa.comp_flg is NULL) GROUP BY sw.wi,sw.ITEM_CD,sw.ITEM_NAME,sw.QTY,pa.prd_flg,sw.WORK_ODR_DLV_DATE, sw.LOCATION_PART,sw.PS_UNIT_NUMERATOR,sw.CT,sw.MODEL,sw.PRODUCT_TYP"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function
    Public Shared Function get_prd_plan(line_cd As String)
        Dim reader As SqlDataReader
        Dim reader2 As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "Select sw.WI,sw.ITEM_CD,sw.ITEM_NAME,sw.QTY,sw.qty - SUM (ISNULL(pa.act_qty, 0 )) as 'remain_qty',ISNULL(pa.prd_flg , 0 ) as 'prd_flg',sw.WORK_ODR_DLV_DATE AS 'DLV_DATE', sw.LOCATION_PART,sw.PS_UNIT_NUMERATOR,sw.CT,COUNT(pa.seq_no) AS seq_count,sw.MODEL , sw.PRODUCT_TYP  from sup_work_plan_supply_dev as sw full outer JOIN production_actual as pa on sw.WI = pa.wi WHERE sw.LINE_CD = '" & line_cd & "' and sw.LVL = '1'  and (sw.PRD_COMP_FLG IS NULL OR sw.PRD_COMP_FLG <> '9') and (pa.comp_flg <> '1' or pa.comp_flg is NULL) GROUP BY sw.wi,sw.ITEM_CD,sw.ITEM_NAME,sw.QTY,pa.prd_flg,sw.WORK_ODR_DLV_DATE, sw.LOCATION_PART,sw.PS_UNIT_NUMERATOR,sw.CT,sw.MODEL,sw.PRODUCT_TYP"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function
    Public Shared Function get_sum_loss(wi As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "SELECT wi,seq_no,SUM(loss_time) AS sum_loss FROM loss_actual WHERE wi = '" & wi & "' GROUP BY wi,seq_no"
            reader = SQLCmd.ExecuteReader()
            Return reader
            SQLConn.Dispose()
            SQLConn.Close()
            SQLConn = Nothing
        Catch ex As Exception
            SQLConn.Close()
            load_show.Show()
        End Try
    End Function

    Public Shared Function get_wi_plan_fromsc(wi_cd As String)
        Dim reader As SqlDataReader
        Dim reader2 As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            'line_cd = "K1A027"
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "SELECT
	                                sw.WI,
	                                sw.ITEM_CD,
	                                sw.ITEM_NAME,
	                                sw.LINE_CD,
	                                sw.QTY,
	                                sw.qty - SUM (ISNULL(pa.qty, 0)) AS 'remain_qty',
	                                sw.WORK_ODR_DLV_DATE AS 'DLV_DATE',
	                                sw.LOCATION_PART,
	                                sw.PS_UNIT_NUMERATOR,
	                                sw.CT,
	                                COUNT (pa.seq_no) AS seq_count,
	                                sw.MODEL,
	                                sw.PRODUCT_TYP,
									sw.PRD_COMP_FLG
                                FROM
	                                sup_work_plan_supply_dev AS sw
                                FULL OUTER JOIN production_actual_detail AS pa ON sw.WI = pa.wi_plan
                                WHERE
	                                sw.LVL = '1'
                                AND sw.wi = '" & wi_cd & "'
                                GROUP BY
	                                sw.wi,
	                                sw.ITEM_CD,
	                                sw.ITEM_NAME,
	                                sw.LINE_CD,
	                                sw.QTY,
	                                sw.WORK_ODR_DLV_DATE,
	                                sw.LOCATION_PART,
	                                sw.PS_UNIT_NUMERATOR,
	                                sw.CT,
	                                sw.MODEL,
	                                sw.PRODUCT_TYP,
									sw.PRD_COMP_FLG"
            'SQLCmd.CommandText = "select * from sup_work_plan_supply_dev where LINE_CD = '" & line_cd & "' AND LVL = '1'"
            reader = SQLCmd.ExecuteReader()
            Return reader
            SQLConn.Close()
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function get_prd_plan]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function get_prd_plan_fromsc(line_cd As String, wi_cd As String)
        Dim reader As SqlDataReader
        Dim reader2 As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            'line_cd = "K1A027"
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "Select sw.WI,sw.ITEM_CD,sw.ITEM_NAME,sw.QTY,sw.qty - SUM (ISNULL(pa.act_qty, 0 )) as 'remain_qty',ISNULL(pa.prd_flg , 0 ) as 'prd_flg',sw.WORK_ODR_DLV_DATE AS 'DLV_DATE', sw.LOCATION_PART,sw.PS_UNIT_NUMERATOR,sw.CT,COUNT(pa.seq_no) AS seq_count,sw.MODEL , sw.PRODUCT_TYP  from sup_work_plan_supply_dev as sw full outer JOIN production_actual as pa on sw.WI = pa.wi WHERE sw.LINE_CD = '" & line_cd & "' and sw.LVL = '1' and (pa.comp_flg <> '1' or pa.comp_flg is NULL) AND sw.wi = '" & wi_cd & "' GROUP BY sw.wi,sw.ITEM_CD,sw.ITEM_NAME,sw.QTY,pa.prd_flg,sw.WORK_ODR_DLV_DATE, sw.LOCATION_PART,sw.PS_UNIT_NUMERATOR,sw.CT,sw.MODEL,sw.PRODUCT_TYP"
            'SQLCmd.CommandText = "select * from sup_work_plan_supply_dev where LINE_CD = '" & line_cd & "' AND LVL = '1'"
            reader = SQLCmd.ExecuteReader()
            ''msgBox("tet efsdf")
            ''msgBox(reader.Read)
            'SQLCmd.CommandText = "select * from production_actual where wi = '5100131123'"
            'reader = SQLCmd.ExecuteReader()
            ''msgBox(reader.Read)
            ''msgBox(reader("wi").ToString())
            'While reader.Read()
            'SQLCmd.CommandText = "select * from production_actual where wi = '" & reader("wi").ToString() & "'"
            'reader2 = SQLCmd.ExecuteReader()
            ''msgBox(reader2.Read)
            'If reader2.Read = False Then
            ''msgBox("dai naa")
            'End If
            ''msgBox(reader("wi").ToString())
            'List_Emp.ListBox1.Items.Add(LoadSQLskill("sk_id").ToString())
            'End While
            ''msgBox(reader)
            Return reader
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function get_prd_plan]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function


    Public Shared Function get_loss_mst()
        Dim reader As SqlDataReader
        Dim reader2 As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            'SQLCmd.CommandText = "Select sw.WI,sw.ITEM_CD,sw.ITEM_NAME,sw.QTY,sw.qty - SUM (ISNULL(pa.act_qty, 0 )) as 'remain_qty',ISNULL(pa.prd_flg , 0 ) as 'prd_flg'  from sup_work_plan_supply_dev as sw full outer JOIN production_actual as pa on sw.WI = pa.wi WHERE sw.LINE_CD = '" & line_cd & "' and sw.LVL = '1' and (pa.comp_flg <> '1' or pa.comp_flg is NULL) GROUP BY sw.wi,sw.ITEM_CD,sw.ITEM_NAME,sw.QTY,pa.prd_flg"
            'SQLCmd.CommandText = "select * from sys_loss_mst where enable = '1'"
            ' SQLCmd.CommandText = "select * from sys_loss_mst where enable = '1'"
            SQLCmd.CommandText = "EXEC [dbo].[GET_LOSS_GROUP] @LINE_CD = '" & MainFrm.Label4.Text & "'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function get_loss_mst]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function

    Public Shared Function get_defect_mst()
        Dim reader As SqlDataReader
        Dim reader2 As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            'SQLCmd.CommandText = "Select sw.WI,sw.ITEM_CD,sw.ITEM_NAME,sw.QTY,sw.qty - SUM (ISNULL(pa.act_qty, 0 )) as 'remain_qty',ISNULL(pa.prd_flg , 0 ) as 'prd_flg'  from sup_work_plan_supply_dev as sw full outer JOIN production_actual as pa on sw.WI = pa.wi WHERE sw.LINE_CD = '" & line_cd & "' and sw.LVL = '1' and (pa.comp_flg <> '1' or pa.comp_flg is NULL) GROUP BY sw.wi,sw.ITEM_CD,sw.ITEM_NAME,sw.QTY,pa.prd_flg"
            SQLCmd.CommandText = "select * from sys_defect_mst where enable = '1'"
            reader = SQLCmd.ExecuteReader()
            Return reader
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function get_defect_mst]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function

    Public Function Get_default_line_detail()
        Check_connect_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select * from line_detail"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            Return LoadSQL
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function ConnectDBSQLite]")
            sqliteConn.Close()
        End Try
    End Function
    Public Function Get_default_pd_detail()
        Check_connect_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()

            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select * from line_detail"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            Return LoadSQL
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function ConnectDBSQLite]")
            sqliteConn.Close()
        End Try
    End Function
    Public Function Get_default_pd_detail_PD(attr As String)
        Check_connect_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()

            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select " & attr & "  from line_detail"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            Dim pd = 0
            While LoadSQL.Read()
                pd = LoadSQL(0).ToString()
            End While
            LoadSQL.Close()
            Return pd
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function Get_default_pd_detail_PD]")
            sqliteConn.Close()
        End Try
    End Function

    Public Shared Function ConnectDBSQLite()
        Check_connect_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()

            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select * from line_detail"

            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function ConnectDBSQLite]")
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function Check_sc_inc_dup(ref_key As String)
        Check_connect_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            ref_key = RTrim(ref_key)
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select * from sc_inc_tag where ref_key = '" & ref_key & "' "
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function Check_sc_inc_dup]")
            sqliteConn.Close()
        End Try
    End Function

    Public Shared Function saveLineConfig(pd As String, line_cd As String, count_type As String, cavity As Integer, scanner_port As String, printer_port As String, dio_port As String, Towerlampss As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd")
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
        Catch ex As Exception
            sqliteConn.Close()
            sqliteConn = New SQLiteConnection(sqliteConnect)
            sqliteConn.Open()
        End Try
        Try
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            ' cmd.CommandText = "UPDATE line_detail SET pd = '" & pd & "', line_cd = '" & line_cd & "', updated_date = '" & currdated & "', count_type = '" & count_type & "', cavity = '" & cavity & "', scanner_port = '" & scanner_port & "', printer_port = '" & printer_port & "', dio_port = '" & dio_port & "'  , Towerlamp = '" & Towerlampss & "'  WHERE id = 1 "
            cmd.CommandText = "UPDATE line_detail SET pd = '" & pd & "', line_cd = '" & line_cd & "', updated_date = '" & currdated & "', count_type = '" & count_type & "', cavity = '" & cavity & "', scanner_port = '" & scanner_port & "', printer_port = '" & printer_port & "', dio_port = '" & dio_port & "'WHERE id = 1 "
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            LoadSQL.Close()
            ''msgBox(LoadSQL)
            Return LoadSQL
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function saveLineConfig] = " & ex.Message)
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function UpdateLineConfig(line_cd As String)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd")
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
        Catch ex As Exception
            sqliteConn.Close()
            sqliteConn = New SQLiteConnection(sqliteConnect)
            sqliteConn.Open()
        End Try
        Try
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "UPDATE line_detail SET line_cd = '" & line_cd & "', updated_date = '" & currdated & "' WHERE id = 1 "
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            LoadSQL.Close()
            ''msgBox(LoadSQL)
            Return LoadSQL
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function UpdateLineConfig] = " & ex.Message)
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function insPrdDetail_sqlite(pd As String, line_cd As String, wi_plan As String, item_cd As String, item_name As String, staff_no As Integer, seq_no As Integer, qty As Integer, number_qty As Integer, st_time As String, end_time As String, use_time As Double, tr_status As String, pwi_id As String) As Integer
re_insert_data:
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") ' ปรับเป็น timestamp

        ' เชื่อมต่อ SQLite
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Check_connect_sqlite()

        Try
            sqliteConn.Open()
            ' ใช้ Parameterized Query เพื่อป้องกัน SQL Injection
            Dim sql As String = "INSERT INTO act_ins (pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, qty, number_qty, st_time, end_time, use_time, tr_status, updated_date, pwi_id) " &
                            "VALUES (@pd, @line_cd, @wi_plan, @item_cd, @item_name, @staff_no, @seq_no, @qty, @number_qty, @st_time, @end_time, @use_time, @tr_status, @updated_date, @pwi_id); " &
                            "SELECT last_insert_rowid();" ' ✅ ดึงค่า PK ที่ถูก Insert
            Dim cmd As New SQLiteCommand(sql, sqliteConn)
            cmd.Parameters.AddWithValue("@pd", pd)
            cmd.Parameters.AddWithValue("@line_cd", line_cd)
            cmd.Parameters.AddWithValue("@wi_plan", wi_plan)
            cmd.Parameters.AddWithValue("@item_cd", item_cd)
            cmd.Parameters.AddWithValue("@item_name", item_name)
            cmd.Parameters.AddWithValue("@staff_no", staff_no)
            cmd.Parameters.AddWithValue("@seq_no", seq_no)
            cmd.Parameters.AddWithValue("@qty", qty)
            cmd.Parameters.AddWithValue("@number_qty", number_qty)
            cmd.Parameters.AddWithValue("@st_time", st_time)
            cmd.Parameters.AddWithValue("@end_time", end_time)
            cmd.Parameters.AddWithValue("@use_time", use_time)
            cmd.Parameters.AddWithValue("@tr_status", tr_status)
            cmd.Parameters.AddWithValue("@updated_date", currdated)
            cmd.Parameters.AddWithValue("@pwi_id", pwi_id)
            ' ✅ ดึงค่า Primary Key ที่เพิ่มเข้าไป
            Dim insertedId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            sqliteConn.Close()
            Return insertedId ' ✅ คืนค่า Primary Key
        Catch ex As Exception
            'msgBox("SQLite Insert Record failed. Please contact PC System [Function insPrdDetail_sqlite]: " & ex.Message)
            sqliteConn.Close()
            GoTo re_insert_data
        End Try
        Return 0
    End Function
    Public Shared Function insPrdDetail_sqlite_by_op(pd As String, line_cd As String, wi_plan As String, item_cd As String, item_name As String, staff_no As Integer, seq_no As Integer, qty As Integer, number_qty As Integer, st_time As String, end_time As String, use_time As Double, tr_status As String, pwi_id As String, op_id As Integer, status_work As Integer) As Integer
re_insert_data:
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") ' ปรับเป็น timestamp
        ' เชื่อมต่อ SQLite
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Check_connect_sqlite()
        Try
            sqliteConn.Open()
            ' ใช้ Parameterized Query เพื่อป้องกัน SQL Injection
            Dim sql As String = "INSERT INTO act_ins_by_op (pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, qty, st_time, end_time, use_time, tr_status, updated_date, pwi_id , op_id , status_work) " &
                            "VALUES (@pd, @line_cd, @wi_plan, @item_cd, @item_name, @staff_no, @seq_no, @qty, @st_time, @end_time, @use_time, @tr_status, @updated_date, @pwi_id , @op_id , @status_work); " &
                            "SELECT last_insert_rowid();" ' ✅ ดึงค่า PK ที่ถูก Insert
            Dim cmd As New SQLiteCommand(sql, sqliteConn)
            cmd.Parameters.AddWithValue("@pd", pd)
            cmd.Parameters.AddWithValue("@line_cd", line_cd)
            cmd.Parameters.AddWithValue("@wi_plan", wi_plan)
            cmd.Parameters.AddWithValue("@item_cd", item_cd)
            cmd.Parameters.AddWithValue("@item_name", item_name)
            cmd.Parameters.AddWithValue("@staff_no", staff_no)
            cmd.Parameters.AddWithValue("@seq_no", seq_no)
            cmd.Parameters.AddWithValue("@qty", qty)
            cmd.Parameters.AddWithValue("@st_time", st_time)
            cmd.Parameters.AddWithValue("@end_time", end_time)
            cmd.Parameters.AddWithValue("@use_time", use_time)
            cmd.Parameters.AddWithValue("@tr_status", tr_status)
            cmd.Parameters.AddWithValue("@updated_date", currdated)
            cmd.Parameters.AddWithValue("@pwi_id", pwi_id)
            cmd.Parameters.AddWithValue("@op_id", op_id)
            cmd.Parameters.AddWithValue("@status_work", status_work)
            ' ✅ ดึงค่า Primary Key ที่เพิ่มเข้าไป
            Dim insertedId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            sqliteConn.Close()
            Return insertedId ' ✅ คืนค่า Primary Key
        Catch ex As Exception
            'msgBox("SQLite Insert Record failed. Please contact PC System [Function insPrdDetail_sqlite]: " & ex.Message)
            sqliteConn.Close()
            GoTo re_insert_data
        End Try
        Return 0
    End Function

    Public Shared Function insPrdDetail_sqlite_defact(pd As String, line_cd As String, wi_plan As String, item_cd As String, item_name As String, staff_no As Integer, seq_no As Integer, qty As Integer, number_qty As Integer, st_time As String, end_time As String, use_time As Double, tr_status As String, flg_defact As String, NC As String)
re_insert_data:
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd")
        'st_time = Date.st_time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        ''msgBox(st_time)
        Try
            sqliteConn.Open()
        Catch ex As Exception
            sqliteConn.Close()
            sqliteConn.Open()
        End Try
        Try
            Dim cmd1 As New SQLiteCommand
            cmd1.Connection = sqliteConn
            'cmd.CommandText = "UPDATE line_detail SET pd = '" & pd & "', line_cd = '" & line_cd & "', updated_date = '" & currdated & "' WHERE id = 1 "
            'cmd.CommandText = "INSERT INTO act_ins (pd,line_cd,wi_plan,item_cd,item_name,tr_status) VALUES ('pd123','line123','wi123','item123','nm123','1');"
            cmd1.CommandText = "INSERT INTO production_defect_detail(pd,line_cd,wi_plan,item_cd,item_name,staff_no,seq_no,qty,number_qty,st_time,end_time,use_time,tr_status,updated_date , flg_defact , defact_id) VALUES ('" & pd & "','" & line_cd & "','" & wi_plan & "','" & item_cd & "','" & item_name & "','" & staff_no & "','" & seq_no & "','" & qty & "','" & number_qty & "','" & st_time & "','" & end_time & "','" & use_time & "','" & tr_status & "','" & currdated & "' , '" & flg_defact & "', '" & NC & "')"
            'cmd1.CommandText = "select * from act_ins"
            Dim LoadSQL As SQLiteDataReader = cmd1.ExecuteReader()
            ''msgBox(LoadSQL)
            'Return LoadSQL
            'sqliteConn.Dispose()
            sqliteConn.Close()
            ' sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Insert Reccord failed. Please contact PC System [Function insPrdDetail_sqlite_defact]" & ex.Message)
            sqliteConn.Close()
            'GoTo re_insert_data
        End Try
        Return 0
    End Function

    Public Shared Function chkLogin(usernm As String, passwd As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "SELECT * FROM sys_user WHERE emp_id = '" & usernm & "' AND passwd = '" & passwd & "'"
            reader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function chkLogin]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try

    End Function
    Public Shared Function check_production_actual_detail_server(wi_plan As String, number_qty As String, seq_no As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "SELECT count(id) as c_id from  production_actual_detail where wi_plan = '" & wi_plan & "' and  number_qty = '" & number_qty & "' and seq_no = '" & seq_no & "' "
            reader = SQLCmd.ExecuteReader()
            Dim data As String = "0"
            While reader.Read()
                If reader("c_id").ToString() = "0" Then
                    data = "0"
                Else
                    data = "1"
                End If
            End While
            reader.Close()
            Return data
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function chkLogin]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Async Function updated_data_to_dbsvrOld(parentForm As Form, statusCheckData As String) As Task
        Dim formName As String = parentForm.Name
        Dim objTranferData As TrasnferData = Nothing
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd")
                Dim api = New api
                api.InitSQLiteWAL()
                Dim LoadSQL = Backoffice_model.get_trdata_sqlite()
                Dim LoadSQLcl = Backoffice_model.get_tr_closelot_flg_sqlite()
                Dim LoadSQL_tag_print_detail = Backoffice_model.get_tr_tag_print_detail()
                Dim LoadSQLcl_tag_print_detail_main = Backoffice_model.get_tr_tag_print_detail_main()
                Dim LoadSQLcl_tag_print_detail_sub = Backoffice_model.get_tr_tag_print_detail_sub()
                If LoadSQL.HasRows Or LoadSQLcl.HasRows Or LoadSQL_tag_print_detail.HasRows Or LoadSQLcl_tag_print_detail_main.HasRows Or LoadSQLcl_tag_print_detail_sub.HasRows Then
                    If objTranferData IsNot Nothing AndAlso Not objTranferData.IsDisposed AndAlso objTranferData.Visible Then
                        ' ฟอร์มเปิดอยู่แล้ว ไม่ต้องทำอะไร
                    Else
                        ' ฟอร์มยังไม่ถูกสร้าง หรือถูกปิดไปแล้ว ต้องสร้างใหม่
                        If objTranferData Is Nothing OrElse objTranferData.IsDisposed Then
                            objTranferData = New TrasnferData()
                        End If
                        ' ถ้ายังไม่แสดง ก็แสดงฟอร์ม
                        If Not objTranferData.Visible Then
                            If statusTransfer = 0 Then
                                objTranferData.Show()
                            End If
                        End If
                    End If
                    statusTransfer = 1 ' 1 = Tranfering 0 = Not Trasnfer
                    If formName = "MainFrm" Then
                        parentForm.Enabled = False
                    End If
                    Task.Run(Sub()
                                 Dim tag_print_detail_id = model_api_sqlite.UpdateStatus_tag_print_detail()
                                 Dim num_arr As Integer = 0
                                 Dim arr_list_id As ArrayList = New ArrayList()
                                 Dim tmp_wi As String = ""
                                 Check_connect_sqlite()
                                 'If LoadSQL.read Then
                                 Dim i As Integer = 0
                                 If LoadSQL.HasRows Then
                                     While LoadSQL.Read()
                                         i = i + 1
                                         Dim id As String = LoadSQL("id").ToString()
                                         Dim pd As String = LoadSQL("pd").ToString()
                                         Dim line_cd As String = LoadSQL("line_cd").ToString()
                                         Dim wi_plan As String = LoadSQL("wi_plan").ToString()
                                         tmp_wi = wi_plan
                                         Dim item_cd As String = LoadSQL("item_cd").ToString()
                                         Dim item_name As String = LoadSQL("item_name").ToString()
                                         Dim staff_no As Integer = LoadSQL("staff_no").ToString()
                                         Dim seq_no As Integer = LoadSQL("seq_no").ToString()
                                         Dim qty As Integer = LoadSQL("qty").ToString()
                                         Dim number_qty As Integer = LoadSQL("number_qty").ToString()
                                         Dim st_time As Date = LoadSQL("st_time").ToString()
                                         Dim end_time As Date = LoadSQL("end_time").ToString()
                                         Dim use_time As Integer = LoadSQL("use_time").ToString()
                                         Dim pwi_id As Integer = LoadSQL("pwi_id").ToString()
                                         Dim status_sqlite = "0"
                                         Check_connect_sqlite()
                                         '  Dim check_rs = checkTransection(pwi_id, number_qty, st_time) ' ตรวจสอบว่าเข้า DB ไปรึยัง
                                         ' 'msgBox(check_rs)
                                         '  If check_rs = "1" Then
                                         Dim rsInsertid = Insert_prd_detail(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, qty, st_time, end_time, use_time, number_qty, pwi_id, status_sqlite)
                                         If rsInsertid <> 0 Then
                                             arr_list_id.Add(id)
                                         End If
                                         ' End If
                                     End While
                                     'End If
                                     Check_connect_sqlite()
                                     Dim array_id() As Object = arr_list_id.ToArray()
                                     Check_connect_sqlite()
                                     For Each element_id In array_id
                                         Dim value As String = element_id
                                         Check_connect_sqlite()
                                         Dim LoadSQLUpdate = Backoffice_model.update_tr_status(value)
                                         num_arr = num_arr + 1
                                     Next
                                     Check_connect_sqlite()
                                 End If
                                 Dim num_arr2 As Integer = 0
                                 Dim arr_list_id2 As ArrayList = New ArrayList()
                                 Dim j = 0
                                 If LoadSQLcl.HasRows Then
                                     While LoadSQLcl.Read()
                                         j = j + 1
                                         Dim wi_plan As String = LoadSQLcl("wi").ToString()
                                         Dim line_cd As String = LoadSQLcl("line_cd").ToString()
                                         Dim item_cd As String = LoadSQLcl("item_cd").ToString()
                                         Dim plan_qty As Integer = LoadSQLcl("plan_qty").ToString()
                                         Dim act_qty As Integer = LoadSQLcl("act_qty").ToString()
                                         Dim seq_no As Integer = LoadSQLcl("seq_no").ToString()
                                         Dim shift_prd As String = LoadSQLcl("shift_prd").ToString()
                                         Dim staff_no As Integer = LoadSQLcl("manpower_no").ToString()
                                         Dim prd_st_date As Date = LoadSQLcl("prd_st_date").ToString()
                                         'Dim prd_st_time As Date = LoadSQLcl("prd_st_time").ToString()
                                         Dim prd_end_date As Date = LoadSQLcl("prd_end_date").ToString()
                                         'Dim prd_end_time As Date = LoadSQLcl("prd_end_time").ToString()
                                         Dim lot_no As String = LoadSQLcl("lot_no").ToString()
                                         Dim comp_flg As String = check_completed_plan(wi_plan, plan_qty)
                                         Dim transfer_flg As String = "1"
                                         Dim del_flg As String = "0"
                                         Dim prd_flg As String = "1"
                                         Dim close_lot_flg As String = "1"
                                         Dim avarage_eff As Double = LoadSQLcl("avarage_eff").ToString()
                                         Dim avarage_act_prd_time As Double = LoadSQLcl("avarage_act_prd_time").ToString()
                                         Dim closeLotInserted As Boolean = False
                                         If check_data(wi_plan, seq_no) = 0 Then
                                             Check_connect_sqlite()
                                             closeLotInserted = Backoffice_model.Insert_prd_close_lot(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_date, prd_end_date, lot_no, comp_flg, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                                             If closeLotInserted AndAlso comp_flg = "1" Then
                                                 Backoffice_model.work_complete(wi_plan)
                                             End If
                                         Else
                                             Check_connect_sqlite()
                                             closeLotInserted = CBool(update_qty_seq(wi_plan, seq_no, act_qty))
                                         End If
                                         If closeLotInserted Then arr_list_id2.Add(LoadSQLcl("id").ToString())
                                     End While
                                 End If
                                 Dim Load_check_act_rework = check_rework_actual()
                                 If Load_check_act_rework > 0 Then
                                     Check_connect_sqlite()
                                     Get_data_rework_actual()
                                 End If
                                 Dim Load_check_loss_actual = check_loss_actual()
                                 If Load_check_loss_actual > 0 Then
                                     Check_connect_sqlite()
                                     Get_data_loss_actual()
                                 End If
                                 Dim check_defact_detail = check_data_defact_detail()
                                 If check_defact_detail > 0 Then
                                     Check_connect_sqlite()
                                     Get_data_defact_actual()
                                 End If
                                 Dim array_id2() As Object = arr_list_id2.ToArray()
                                 For Each element_id2 In array_id2
                                     Dim value2 As String = element_id2
                                     Check_connect_sqlite()
                                     Dim LoadSQLUpdate2 = Backoffice_model.update_tr_close_lot_status(value2)
                                     num_arr2 = num_arr2 + 1
                                 Next
                                 If objTranferData IsNot Nothing Then
                                     If objTranferData.IsHandleCreated Then
                                         objTranferData.Invoke(Sub()
                                                                   statusTransfer = 0
                                                                   parentForm.Enabled = True
                                                                   objTranferData.Close()
                                                               End Sub)
                                     End If
                                 End If
                                 '''Console.WriteLine(array_id(2))
                                 'Dim array() As Object = arr_list.ToArray()
                                 'For Each element In array
                                 '    ' Cast object to string.
                                 '    Dim value As String = element
                                 '    ''Console.WriteLine(value)
                                 'Next
                                 ''msgBox(arr_id(0))
                             End Sub)
                Else
                    If objTranferData IsNot Nothing Then
                        If objTranferData.IsHandleCreated Then
                            objTranferData.Invoke(Sub()
                                                      statusTransfer = 0
                                                      parentForm.Enabled = True
                                                      objTranferData.Close()
                                                  End Sub)
                        End If
                    End If
                End If
            Else
                ' NoNet
                If objTranferData IsNot Nothing Then
                    If objTranferData.IsHandleCreated Then
                        objTranferData.Invoke(Sub()
                                                  statusTransfer = 0
                                                  parentForm.Enabled = True
                                                  objTranferData.Close()
                                              End Sub)
                    End If
                End If
            End If
        Catch ex As Exception
            If objTranferData IsNot Nothing Then
                If objTranferData.IsHandleCreated Then
                    objTranferData.Invoke(Sub()
                                              statusTransfer = 0
                                              parentForm.Enabled = True
                                              objTranferData.Close()
                                          End Sub)
                End If
            End If
        End Try
    End Function
    Public Shared Async Function updated_data_to_dbsvr(parentForm As Form, statusCheckData As String) As Task
        If Not Await semTransfer.WaitAsync(0) Then Exit Function ' ป้องกันเรียกซ้ำ
        Try
            Await ResetTransferTimeout() ' ← ✅ เรียกที่นี่ 
            Await updated_data_to_dbsvr_main(parentForm, statusCheckData)
        Finally
            semTransfer.Release()
        End Try
    End Function

    Public Shared Async Function updated_data_to_dbsvr_main(parentForm As Form, statusCheckData As String) As Task
        Dim objTransferData As Form = Nothing
        Try
            ' ❌ หยุดถ้า statusCheckData = 2 และไม่มี Network
            Dim rsNetwork = Await Backoffice_model.CheckSingnalNetwork()
            If statusCheckData = "2" AndAlso (rsNetwork = False) Then
                Console.WriteLine("❌ ไม่มี Network และ statusCheckData = 2 → ยกเลิก Transfer")
                Exit Function
            End If
            rsNetwork = Await Backoffice_model.CheckSingnalNetwork()
            If rsNetwork Then
                ' If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim api = New api()
                api.InitSQLiteWAL()
                ' Load ข้อมูลทุกตาราง
                Dim LoadSQL_By_op = Backoffice_model.get_trdata_sqlite_by_op()
                Dim LoadSQL = Backoffice_model.get_trdata_sqlite()
                Dim LoadSQLcl = Backoffice_model.get_tr_closelot_flg_sqlite()
                Dim LoadSQL_tag_print_detail = Backoffice_model.get_tr_tag_print_detail()
                Dim LoadSQL_check_loss_actual = Backoffice_model.check_loss_actual()
                Dim LoadSQL_get_defect_tag_information = Backoffice_model.get_defect_tag_information()
                '  Dim LoadSQL_tag_print_detail_main = Backoffice_model.get_tr_tag_print_detail_main()
                '  Dim LoadSQL_tag_print_detail_sub = Backoffice_model.get_tr_tag_print_detail_sub()
                Dim hasData = LoadSQL.HasRows Or LoadSQLcl.HasRows Or LoadSQL_tag_print_detail.HasRows Or LoadSQL_check_loss_actual > 0 Or LoadSQL_get_defect_tag_information.HasRows Or LoadSQL_By_op.HasRows
                If hasData Then
                    ' ✅ อัปเดตข้อมูล tag_print ทั้งหมด
                    Await model_api_sqlite.UpdateStatus_tag_print_detail()
                    Await model_api_sqlite.UpdateStatus_defect_tag_information()
                    'สร้างหน้าต่าง Transfer ถ้ายังไม่มี
                    If objTransferData Is Nothing OrElse objTransferData.IsDisposed Then
                        objTransferData = New TrasnferData()
                    End If
                    ' แสดงฟอร์มถ้ายังไม่แสดง และสถานะ Transfer ยังไม่ทำงาน
                    If Not objTransferData.Visible AndAlso statusTransfer = 0 Then
                        If parentForm.InvokeRequired Then
                            parentForm.Invoke(Sub() objTransferData.Show())
                        Else
                            objTransferData.Show()
                        End If
                    End If
                    'statusTransfer = 1
                    ' เรียกฟังก์ชัน Transfer หลัก
                    Await DoTransferWork(parentForm, objTransferData, LoadSQL, LoadSQL_By_op, LoadSQLcl, statusCheckData)
                End If
            End If

        Catch ex As Exception
            ' TODO: Logging
        Finally
            If objTransferData IsNot Nothing AndAlso objTransferData.IsHandleCreated Then
                objTransferData.Invoke(Sub()

                                           statusTransfer = 0
                                           objTransferData.Close()
                                       End Sub)
            End If
        End Try
    End Function

    Private Shared Async Function DoTransferWork(parentForm As Form, objTransferData As Form,
                                             LoadSQL As Object, LoadSQL_By_op As Object, LoadSQLcl As Object, statusCheckData As String) As Task
        Dim retryMap As New Dictionary(Of String, Integer)()
        ' ========== Transfer production detail ==========
        If LoadSQL.HasRows Then
            While LoadSQL.Read()
                Dim id = LoadSQL("id").ToString()
                Dim pd = LoadSQL("pd").ToString()
                Dim line_cd = LoadSQL("line_cd").ToString()
                Dim wi_plan = LoadSQL("wi_plan").ToString()
                Dim item_cd = LoadSQL("item_cd").ToString()
                Dim item_name = LoadSQL("item_name").ToString()
                Dim staff_no = Integer.Parse(LoadSQL("staff_no").ToString())
                Dim seq_no = Integer.Parse(LoadSQL("seq_no").ToString())
                Dim qty = Integer.Parse(LoadSQL("qty").ToString())
                Dim number_qty = Integer.Parse(LoadSQL("number_qty").ToString())
                Dim st_time = Date.Parse(LoadSQL("st_time").ToString())
                Dim end_time = Date.Parse(LoadSQL("end_time").ToString())
                Dim use_time = Integer.Parse(LoadSQL("use_time").ToString())
                Dim pwi_id = Integer.Parse(LoadSQL("pwi_id").ToString())
                Dim status_sqlite = "0"
                While Not My.Computer.Network.Ping(Backoffice_model.svp_ping)
                    Await Task.Delay(1000)
                End While
                Dim api = New api
                If Not retryMap.ContainsKey(id) Then retryMap(id) = 0
                If statusCheckData = "2" AndAlso retryMap(id) >= 3 Then
                    'Console.WriteLine($"🔴 ข้าม ID={id} เพราะ Retry ครบ 3 ครั้งแล้ว (statusCheckData=2)")
                    'File.AppendAllText("logs\reserve_fail.log", $"{Now:yyyy-MM-dd HH:mm:ss} | ID={id} | Retry=3 | Reserve Fail{Environment.NewLine}")
                    Continue While
                End If
                Dim curruntTime As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                Dim reserveSql = $"UPDATE act_ins SET tr_status = 2, updated_date = '" & curruntTime & "' WHERE id = " & id & " AND tr_status = 0"
                WriteDebugDiagnostic("SQLite transfer reservation update requested")
                Try
                    Dim result = Await api.Load_dataSQLiteAsync(reserveSql)
                    If result = "0" Then
                        retryMap(id) += 1
                        'Console.WriteLine($"⛔ จองข้อมูลไม่สำเร็จ (ID={id}) รอบที่ {retryMap(id)}" & reserveSql)
                        Continue While
                    End If
                Catch ex As Exception
                    Dim functionName As String = New StackTrace().GetFrame(0).GetMethod().Name
                    'Console.WriteLine($"❌ Error ({functionName}) while reserving: {ex.Message}")
                    retryMap(id) += 1
                    Continue While
                End Try
                Await Insert_prd_detail_main(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no,
                                     qty, st_time, end_time, use_time, number_qty, pwi_id, status_sqlite, id)
            End While
        End If
        If LoadSQL_By_op.HasRows Then
            Dim number_qty_by_op = 0
            While LoadSQL_By_op.Read()
                Dim id = LoadSQL_By_op("id").ToString()
                Dim pd = LoadSQL_By_op("pd").ToString()
                Dim line_cd = LoadSQL_By_op("line_cd").ToString()
                Dim wi_plan = LoadSQL_By_op("wi_plan").ToString()
                Dim item_cd = LoadSQL_By_op("item_cd").ToString()
                Dim item_name = LoadSQL_By_op("item_name").ToString()
                Dim staff_no = Integer.Parse(LoadSQL_By_op("staff_no").ToString())
                Dim seq_no = Integer.Parse(LoadSQL_By_op("seq_no").ToString())
                Dim qty = Integer.Parse(LoadSQL_By_op("qty").ToString())
                Dim st_time = Date.Parse(LoadSQL_By_op("st_time").ToString())
                Dim end_time = Date.Parse(LoadSQL_By_op("end_time").ToString())
                Dim use_time = Integer.Parse(LoadSQL_By_op("use_time").ToString())
                Dim pwi_id = Integer.Parse(LoadSQL_By_op("pwi_id").ToString())
                Dim op_id = Integer.Parse(LoadSQL_By_op("op_id").ToString())
                Dim status_work = Integer.Parse(LoadSQL_By_op("status_work").ToString())
                Dim status_sqlite = "0"
                While Not My.Computer.Network.Ping(Backoffice_model.svp_ping)
                    Await Task.Delay(1000)
                End While
                Dim api = New api
                If Not retryMap.ContainsKey(id) Then retryMap(id) = 0
                If statusCheckData = "2" AndAlso retryMap(id) >= 3 Then
                    'Console.WriteLine($"🔴 ข้าม ID={id} เพราะ Retry ครบ 3 ครั้งแล้ว (statusCheckData=2)")
                    'File.AppendAllText("logs\reserve_fail.log", $"{Now:yyyy-MM-dd HH:mm:ss} | ID={id} | Retry=3 | Reserve Fail{Environment.NewLine}")
                    Continue While
                End If
                Dim curruntTime As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                Dim reserveSql = $"UPDATE act_ins_by_op SET tr_status = 2, updated_date = '" & curruntTime & "' WHERE id = " & id & " AND tr_status = 0"
                WriteDebugDiagnostic("SQLite transfer reservation update requested")
                Try
                    Dim result = Await api.Load_dataSQLiteAsync(reserveSql)
                    If result = "0" Then
                        retryMap(id) += 1
                        'Console.WriteLine($"⛔ จองข้อมูลไม่สำเร็จ (ID={id}) รอบที่ {retryMap(id)}" & reserveSql)
                        Continue While
                    End If
                Catch ex As Exception
                    Dim functionName As String = New StackTrace().GetFrame(0).GetMethod().Name
                    'Console.WriteLine($"❌ Error ({functionName}) while reserving: {ex.Message}")
                    retryMap(id) += 1
                    Continue While
                End Try
                Await Insert_prd_detail_by_op_main(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no,
                                     qty, st_time, end_time, use_time, number_qty_by_op, pwi_id, status_sqlite, id, op_id, status_work)
            End While
        End If


        ' ========== Transfer close lot ==========
        If LoadSQLcl.HasRows Then
            Dim arr_list_id2 As New ArrayList()
            While LoadSQLcl.Read()
                Dim wi_plan = LoadSQLcl("wi").ToString()
                Dim line_cd = LoadSQLcl("line_cd").ToString()
                Dim item_cd = LoadSQLcl("item_cd").ToString()
                Dim plan_qty = Integer.Parse(LoadSQLcl("plan_qty").ToString())
                Dim act_qty = Integer.Parse(LoadSQLcl("act_qty").ToString())
                Dim seq_no = Integer.Parse(LoadSQLcl("seq_no").ToString())
                Dim shift_prd = LoadSQLcl("shift_prd").ToString()
                Dim staff_no = Integer.Parse(LoadSQLcl("manpower_no").ToString())
                Dim prd_st_date = Date.Parse(LoadSQLcl("prd_st_date").ToString())
                Dim prd_end_date = Date.Parse(LoadSQLcl("prd_end_date").ToString())
                Dim lot_no = LoadSQLcl("lot_no").ToString()
                Dim comp_flg = check_completed_plan(wi_plan, plan_qty)
                Dim transfer_flg = "1"
                Dim del_flg = "0"
                Dim prd_flg = "1"
                Dim close_lot_flg = "1"
                Dim avarage_eff = Double.Parse(LoadSQLcl("avarage_eff").ToString())
                Dim avarage_act_prd_time = Double.Parse(LoadSQLcl("avarage_act_prd_time").ToString())

                While Not My.Computer.Network.Ping(Backoffice_model.svp_ping)
                    Await Task.Delay(1000)
                End While

                Dim closeLotInserted As Boolean = False
                If check_data(wi_plan, seq_no) = 0 Then
                    Check_connect_sqlite()
                    closeLotInserted = Backoffice_model.Insert_prd_close_lot(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd,
                                                   staff_no, prd_st_date, prd_end_date, lot_no, comp_flg, transfer_flg,
                                                   del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                    If closeLotInserted AndAlso comp_flg = "1" Then
                        Backoffice_model.work_complete(wi_plan)
                    End If
                Else
                    closeLotInserted = CBool(update_qty_seq(wi_plan, seq_no, act_qty)) 'add condition lot
                End If

                If closeLotInserted Then
                    arr_list_id2.Add(LoadSQLcl("id").ToString())
                End If
            End While

            For Each element_id2 In arr_list_id2.ToArray()
                Backoffice_model.update_tr_close_lot_status(element_id2.ToString())
            Next
        End If

        ' ========== Optional: ดึงข้อมูล defect, loss, rework ==========
        If check_rework_actual() > 0 Then Get_data_rework_actual()
        If check_loss_actual() > 0 Then Get_data_loss_actual()
        If check_data_defact_detail() > 0 Then Get_data_defact_actual()

        ' ========== ปิดหน้าต่าง Transfer ==========
        If objTransferData IsNot Nothing AndAlso objTransferData.IsHandleCreated Then
            objTransferData.Invoke(Sub()
                                       statusTransfer = 0
                                       objTransferData.Close()
                                   End Sub)
        End If
    End Function

    Public Shared Function check_rework_actual()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select count(rwa_id) as check_data from rework_actual where tr_status ='0'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            Dim get_qty_sqlite As Integer = 0
            Try
                While LoadSQL.Read()
                    get_qty_sqlite = LoadSQL("check_data").ToString()
                End While
                LoadSQL.Close()
                'sqliteConn.Dispose()
                sqliteConn.Close()
                'sqliteConn = Nothing
                If get_qty_sqlite = 0 Then
                    Return 0
                Else
                    Return 1
                End If
            Catch ex As Exception
                Return 0
            End Try
            ''msgBox(LoadSQL)
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function check_rework_actual]" & ex.Message)
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function Get_data_rework_actual()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
recheck:
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select * from rework_actual where tr_status ='0'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            Dim get_qty_sqlite As Integer = 0
            Try
                While LoadSQL.Read()
                    Dim rwa_part_no As String = LoadSQL("rwa_part_no").ToString()
                    Dim rwa_qty As String = LoadSQL("rwa_qty").ToString()
                    Dim rwa_shift As String = LoadSQL("rwa_qty").ToString()
                    Dim date_now_rework As String = LoadSQL("rwa_created_date_time").ToString()
                    Dim ref_wi As String = LoadSQL("ref_wi").ToString()
                    Dim ref_part_name As String = LoadSQL("rwa_part_name").ToString()
                    Dim rwa_model As String = LoadSQL("rwa_model").ToString()
                    Dim tr_status As String = "1"
                    INSERT_REWORK_ACTUAL(rwa_part_no, rwa_qty, rwa_shift, date_now_rework, ref_wi, ref_part_name, rwa_model)
                End While
                LoadSQL.Close()
                update_flg_rework_act_sqlite()
            Catch ex As Exception
                'msgBox("error function Get_data_rework_actual == > " & ex.Message)
                Check_connect_sqlite()
                GoTo recheck
            End Try
            ''msgBox(LoadSQL)
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function Get_data_rework_actual]" & ex.Message)
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function update_flg_rework_act_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "update rework_actual set tr_status = '1'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            ''msgBox("SQLite Database connect failed. Please contact PC System [Function update_flg_rework_act_sqlite]")
            sqliteConn.Close()
        End Try
    End Function

    Public Shared Function update_flg_loss_atc_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()

            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "update loss_actual set transfer_flg = '1'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            '  'msgBox("SQLite Database connect failed. Please contact PC System [Function update_flg_loss_atc_sqlite]")
            sqliteConn.Close()
        End Try
    End Function

    Public Shared Function update_flg_defact_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "update production_defect_detail  set tr_status = '1'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function update_flg_defact_sqlite]")
            sqliteConn.Close()
        End Try
    End Function


    Public Shared Function check_data_sqlite(wi As String, seq_no As String)
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select count(id) as c_id from close_lot_act where wi = '" & wi & "' and seq_no = '" & seq_no & "'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            '  'msgBox("SQLite Database connect failed. Please contact PC System [Function check_data_sqlite]")
            sqliteConn.Close()
        End Try
    End Function

    Public Shared Function check_data(wi As String, seq_no As String)
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        SQLConn.ConnectionString = sqlConnect 'Set the Connection String
        Try
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandText = "select count(id) as c_id from production_actual where wi = '" & wi & "' and seq_no = '" & seq_no & "'"
            reader = SQLCmd.ExecuteReader()
            Try
                If reader.Read() Then
                    If reader("c_id").ToString() = "0" Then
                        Return 0
                    Else
                        get_qty_sqlite = reader("c_id").ToString()
                        Return 1
                    End If
                Else
                    Return 0
                End If
                reader.Close()
            Catch ex As Exception
                reader.Close()
                Return 0
            End Try
        Catch ex As Exception
            ''msgBox("MSSQL Database connect failed. Please contact PC System [Function chkLogin]")
            SQLConn.Close()
            load_show.Show()
            ' Application.Exit()
        End Try
    End Function
    Public Shared Function check_completed_plan(wi, plan_qty)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd")
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select sum(act_qty) as total_qty from close_lot_act where wi='" & wi & "'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            Dim get_qty_sqlite As Integer = 0
            Try
                While LoadSQL.Read()
                    get_qty_sqlite = LoadSQL("total_qty").ToString()
                End While
                sqliteConn.Dispose()
                'sqliteConn.Close()
                'sqliteConn = Nothing
                If CDbl(Val(plan_qty)) > get_qty_sqlite Then
                    ''msgBox("0" & "plan_qty = " & plan_qty & "-->total_act_qty = " & get_qty_sqlite)
                    Return 0
                Else
                    Return 1
                End If
            Catch ex As Exception
                Return 0
            End Try
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function check_completed_plan]" & ex.Message)
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function Check_connect_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
        Catch ex As Exception
            sqliteConn.Dispose()
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function update_tr_status_load(id As Integer)
        Check_connect_sqlite()
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd")
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            'recheck_update:
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "UPDATE act_ins SET tr_status = '1', updated_date = '" & currdated & "' WHERE id = '" & id & "'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            'Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
            'Check_connect_sqlite()
        Catch ex As Exception
            ''msgBox("SQLite Database connect failed. Please contact PC System [Function update_tr_status]")
            'Check_connect_sqlite()
            'GoTo recheck_update
            sqliteConn.Close()
        End Try
    End Function
    'Public Shared Function update_tr_status(id As Integer)
    '    Check_connect_sqlite()
    '    Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss")
    '    Dim sqliteConn As New SQLiteConnection(sqliteConnect)
    '    Try
    '        sqliteConn.Open()
    '        Dim cmd As New SQLiteCommand
    '        cmd.Connection = sqliteConn
    '        cmd.CommandText = "UPDATE act_ins SET tr_status = '1', updated_date = '" & currdated & "' WHERE id = '" & id & "' "
    '        Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
    '        Return LoadSQL
    '        sqliteConn.Dispose()
    '        sqliteConn.Close()
    '        sqliteConn = Nothing
    '    Catch ex As Exception
    '        sqliteConn.Close()
    ' End Try
    Public Shared Function update_tr_status(id As Integer)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss")
        Try
            Dim api = New api
            Dim Sql = " UPDATE act_ins SET tr_status = '1', updated_date = '" & currdated & "' WHERE id = '" & id & "'"
            Dim jsonData As String = api.Load_dataSQLite(Sql)
        Catch ex As Exception
            ' 'msgBox("Error Files Backoffice_model In Function update_tr_status")
        End Try
    End Function

    Public Shared Function update_tr_close_lot_status(id As Integer)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd")
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Dim closeLotSqliteRetry As Integer = 0
        Try
recheck:
            closeLotSqliteRetry += 1
            If closeLotSqliteRetry > 3 Then Return Nothing
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "UPDATE close_lot_act SET transfer_flg = '1', updated_date = '" & currdated & "' WHERE id = '" & id & "' "
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            Try : sqliteConn.Close() : Catch : End Try
            If closeLotSqliteRetry < 3 Then
                Threading.Thread.Sleep(100)
                Check_connect_sqlite()
                GoTo recheck
            End If
            Return Nothing
        End Try
    End Function



    Public Shared Function get_trdata_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "SELECT * FROM act_ins WHERE tr_status =0"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function get_trdata_sqlite]")
            sqliteConn.Close()
        End Try

    End Function
    Public Shared Function get_trdata_sqlite_by_op()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "SELECT * FROM act_ins_by_op WHERE tr_status =0"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function get_trdata_sqlite]")
            sqliteConn.Close()
        End Try
    End Function

    Public Shared Function get_tr_closelot_flg_sqlite()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            Check_connect_sqlite()
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "SELECT * FROM close_lot_act where transfer_flg = '0'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function get_tr_closelot_flg_sqlite]")
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function get_tr_tag_print_detail()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            Check_connect_sqlite()
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "SELECT * FROM tag_print_detail where tr_status = '0'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function get_tr_tag_print_detail]")
            sqliteConn.Close()
        End Try
    End Function

    Public Shared Function get_defect_tag_information()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            Check_connect_sqlite()
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "SELECT * FROM defect_tag_information where dti_tranfer_flg = '0'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function get_tr_tag_print_detail]")
            sqliteConn.Close()
        End Try
    End Function

    Public Shared Function get_tr_tag_print_detail_main()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            Check_connect_sqlite()
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "SELECT * FROM tag_print_detail_main where tr_status = '0'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function get_tr_tag_print_detail_main]")
            sqliteConn.Close()
        End Try
    End Function

    Public Shared Function get_tr_tag_print_detail_sub()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            Check_connect_sqlite()
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "SELECT * FROM tag_print_detail_sub where tr_status = '0'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function tag_print_detail_sub]")
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function get_trdata_sqlite_act()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()

            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "SELECT * FROM close_lot_act WHERE transfer_flg = 0 "

            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ''msgBox(LoadSQL)
            Return LoadSQL
            sqliteConn.Dispose()
            sqliteConn.Close()
            sqliteConn = Nothing
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function get_trdata_sqlite]")
            sqliteConn.Close()
        End Try
    End Function


    Public Shared Function Insert_prd_close_lot(wi_plan As String, line_cd As String, item_cd As String, plan_qty As Integer, act_qty As Integer, seq_no As Integer, shift_prd As String, manpower_no As Integer, st_time As DateTime, end_time As DateTime, lot_no As String, comp_flg As String, transfer_flg As String, del_flg As String, prd_flg As String, close_lot_flg As String, avarage_eff As Double, avarage_act_prd_time As Double)
        If wi_plan = "Label41" Then Return True

        For closeLotRetry As Integer = 1 To 3
            Try
                Using SQLConn As New SqlConnection(sqlConnect)
                    Using SQLCmd As New SqlCommand("INSERT INTO production_actual " &
                        "(wi,line_cd,item_cd,plan_qty,act_qty,seq_no,shift_prd,manpower_no,prd_st_date,prd_st_time,prd_end_date,prd_end_time,lot_no,comp_flg,transfer_flg,del_flg,updated_date,prd_flg,close_lot_flg,avarage_eff,avarage_act_prd_time) " &
                        "VALUES (@wi,@line_cd,@item_cd,@plan_qty,@act_qty,@seq_no,@shift_prd,@manpower_no,@prd_st_date,@prd_st_time,@prd_end_date,@prd_end_time,@lot_no,@comp_flg,@transfer_flg,@del_flg,@updated_date,@prd_flg,@close_lot_flg,@avarage_eff,@avarage_act_prd_time)", SQLConn)
                        SQLCmd.CommandTimeout = 10
                        SQLCmd.Parameters.Add("@wi", SqlDbType.BigInt).Value = Convert.ToInt64(wi_plan)
                        SQLCmd.Parameters.Add("@line_cd", SqlDbType.VarChar, 10).Value = If(line_cd, "")
                        SQLCmd.Parameters.Add("@item_cd", SqlDbType.VarChar, 50).Value = If(item_cd, "")
                        SQLCmd.Parameters.Add("@plan_qty", SqlDbType.Int).Value = plan_qty
                        SQLCmd.Parameters.Add("@act_qty", SqlDbType.Int).Value = act_qty
                        SQLCmd.Parameters.Add("@seq_no", SqlDbType.Int).Value = seq_no
                        SQLCmd.Parameters.Add("@shift_prd", SqlDbType.VarChar, 5).Value = If(shift_prd, "")
                        SQLCmd.Parameters.Add("@manpower_no", SqlDbType.Int).Value = manpower_no
                        SQLCmd.Parameters.Add("@prd_st_date", SqlDbType.SmallDateTime).Value = st_time
                        SQLCmd.Parameters.Add("@prd_st_time", SqlDbType.Time).Value = st_time.TimeOfDay
                        SQLCmd.Parameters.Add("@prd_end_date", SqlDbType.SmallDateTime).Value = end_time
                        SQLCmd.Parameters.Add("@prd_end_time", SqlDbType.Time).Value = end_time.TimeOfDay
                        SQLCmd.Parameters.Add("@lot_no", SqlDbType.VarChar, 10).Value = If(lot_no, "")
                        SQLCmd.Parameters.Add("@comp_flg", SqlDbType.VarChar, 2).Value = If(comp_flg, "")
                        SQLCmd.Parameters.Add("@transfer_flg", SqlDbType.VarChar, 2).Value = If(transfer_flg, "")
                        SQLCmd.Parameters.Add("@del_flg", SqlDbType.VarChar, 2).Value = If(del_flg, "")
                        SQLCmd.Parameters.Add("@updated_date", SqlDbType.SmallDateTime).Value = DateTime.Now
                        SQLCmd.Parameters.Add("@prd_flg", SqlDbType.VarChar, 2).Value = If(prd_flg, "")
                        SQLCmd.Parameters.Add("@close_lot_flg", SqlDbType.VarChar, 2).Value = If(close_lot_flg, "")
                        SQLCmd.Parameters.Add("@avarage_eff", SqlDbType.Float).Value = avarage_eff
                        SQLCmd.Parameters.Add("@avarage_act_prd_time", SqlDbType.Float).Value = avarage_act_prd_time
                        SQLConn.Open()
                        SQLCmd.ExecuteNonQuery()
                    End Using
                End Using
                Return True
            Catch ex As Exception
                Debug.WriteLine("Insert_prd_close_lot attempt " & closeLotRetry.ToString() & " failed: " & ex.Message)
                If closeLotRetry < 3 Then Threading.Thread.Sleep(250)
            End Try
            '  Try
            '   Dim api = New api()
            '    Dim GetData = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/InsertProductionActualAppFA?wi=" & wi_plan & "&line_cd=" & line_cd & "&item_cd=" & item_cd & "&plan_qty=" & plan_qty & "&act_qty=" & act_qty & "&seq_no=" & seq_no & "&shift_prd=" & shift_prd & "&manpower_no=" & manpower_no & "&st_time=" & st_time & "&end_time=" & end_time & "&lot_no=" & lot_no & "&comp_flg=" & comp_flg & "&transfer_flg=" & transfer_flg & "&del_flg=" & del_flg & "&prd_flg=" & prd_flg & "&close_lot_flg=" & close_lot_flg & "&avarage_eff=" & avarage_eff & "&avarage_act_prd_time=" & avarage_act_prd_time & "&prd_st_date=" & st_datetime2 & "&prd_st_time=" & st_time2 & "&prd_end_date=" & end_datetime2 & "&prd_end_time=" & end_time2 & "&updated_date=" & currdated)
            '    'Console.WriteLine("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/InsertProductionActualAppFA?wi=" & wi_plan & "&line_cd=" & line_cd & "&item_cd=" & item_cd & "&plan_qty=" & plan_qty & "&act_qty=" & act_qty & "&seq_no=" & seq_no & "&shift_prd=" & shift_prd & "&manpower_no=" & manpower_no & "&st_time=" & st_time & "&end_time=" & end_time & "&lot_no=" & lot_no & "&comp_flg=" & comp_flg & "&transfer_flg=" & transfer_flg & "&del_flg=" & del_flg & "&prd_flg=" & prd_flg & "&close_lot_flg=" & close_lot_flg & "&avarage_eff=" & avarage_eff & "&avarage_act_prd_time=" & avarage_act_prd_time & "&prd_st_date=" & st_datetime2 & "&prd_st_time=" & st_time2 & "&prd_end_date=" & end_datetime2 & "&prd_end_time=" & end_time2 & "&updated_date=" & currdated)
            '     Return GetData
            '     Catch ex As Exception
            ''msgBox("Error Function Get_Plan_All_By_Line_LOSS_A In Backoffice_model")
            '    GoTo recheck
            '   End Try
        Next
        Return False
    End Function



    Public Shared Function Insert_prd_close_lot_sqlite(wi_plan As String, line_cd As String, item_cd As String, plan_qty As Integer, act_qty As Integer, seq_no As Integer, shift_prd As String, manpower_no As Integer, st_time As DateTime, end_time As DateTime, lot_no As String, comp_flg As String, transfer_flg As String, del_flg As String, prd_flg As String, close_lot_flg As String, avarage_eff As Double, avarage_act_prd_time As Double)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd")
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Dim st_time2 As String = st_time.ToString("H:m:s")
        'Dim st_datetime2 As String = st_time.ToString("yyyy/MM/dd H:m:s")
        Dim st_datetime2 As String = st_time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
        Dim end_time2 As String = end_time.ToString("H:m:s")
        'Dim end_datetime2 As String = end_time.ToString("yyyy/MM/dd H:m:s")
        Dim end_datetime2 As String = end_time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
        Try
            sqliteConn.Open()
            Using cmd As New SQLiteCommand
                cmd.Connection = sqliteConn
                cmd.CommandText = "INSERT INTO close_lot_act (wi,line_cd,item_cd,plan_qty,act_qty,seq_no,shift_prd,manpower_no,prd_st_date,prd_st_time,prd_end_date,prd_end_time,lot_no,comp_flg,transfer_flg,del_flg,updated_date,prd_flg,close_lot_flg,avarage_eff,avarage_act_prd_time) VALUES ('" & wi_plan & "','" & line_cd & "','" & item_cd & "','" & plan_qty & "','" & act_qty & "','" & seq_no & "','" & shift_prd & "','" & manpower_no & "','" & st_datetime2 & "','" & st_time2 & "','" & end_datetime2 & "','" & end_time2 & "','" & lot_no & "','" & comp_flg & "','" & transfer_flg & "','" & del_flg & "','" & currdated & "','" & prd_flg & "','" & close_lot_flg & "','" & avarage_eff & "','" & avarage_act_prd_time & "')"
                cmd.CommandTimeout = 5
                cmd.ExecuteNonQuery()
            End Using
            sqliteConn.Close()
            Return True
        Catch ex As Exception
            'msgBox("SQLite Insert Reccord failed. Please contact PC System [Function insPrdDetail_sqlite]")
            sqliteConn.Close()
        End Try
    End Function



    Public Shared Function Insert_emp_cd(wi_plan As String, staff_cd As String, prd_seq As Integer)
        Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()

        Try
            SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            SQLConn.Open()
            SQLCmd.Connection = SQLConn

            SQLCmd.CommandText = "INSERT INTO production_emp_detail (wi_plan,staff_cd,prd_seq_no,updated_date) VALUES ('" & wi_plan & "','" & staff_cd & "','" & prd_seq & "','" & currdated & "')"

            reader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection)
            '''Console.WriteLine("INSERT INTO production_emp_detail (wi_plan,staff_cd,prd_seq_no,updated_date) VALUES ('" & wi_plan & "','" & staff_cd & "','" & prd_seq & "','" & currdated & "')")
            ''msgBox("INSERT INTO production_emp_detail (wi_plan,staff_cd,prd_seq_no,updated_date) VALUES ('" & wi_plan & "','" & staff_cd & "','" & prd_seq & "','" & currdated & "')")

            Return reader
            SQLConn.Dispose()
            SQLConn.Close()
            SQLConn = Nothing
        Catch ex As Exception
            ' 'msgBox("MSSQL Database connect failed. Please contact PC System [Function Insert_emp_cd]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function get_loss_op_mst(lind_cd As String)
        'Dim reader As SqlDataReader
        'Dim reader2 As SqlDataReader
        'Dim SQLConn As New SqlConnection() 'The SQL Conn   ection
        'Dim SQLCmd As New SqlCommand()
        Try
            'SQLConn.ConnectionString = sqlConnect 'Set the Connection String
            'SQLConn.Open()
            ' SQLCmd.Connection = SQLConn
            ' 'SQLCmd.CommandText = "Select sw.WI,sw.ITEM_CD,sw.ITEM_NAME,sw.QTY,sw.qty - SUM (ISNULL(pa.act_qty, 0 )) as 'remain_qty',ISNULL(pa.prd_flg , 0 ) as 'prd_flg'  from sup_work_plan_supply_dev as sw full outer JOIN production_actual as pa on sw.WI = pa.wi WHERE sw.LINE_CD = '" & line_cd & "' and sw.LVL = '1' and (pa.comp_flg <> '1' or pa.comp_flg is NULL) GROUP BY sw.wi,sw.ITEM_CD,sw.ITEM_NAME,sw.QTY,pa.prd_flg"
            ' SQLCmd.CommandText = "SELECT DISTINCT skld.sk_id,skld.process_no,sscm.sk_name FROM sys_line_mst slm LEFT JOIN sys_skill_line_detail skld ON slm.line_id = skld.line_id LEFT JOIN sys_skill_chart_mst sscm ON skld.sk_id = sscm.sk_id WHERE slm.line_cd = '" & lind_cd & "'"
            ' reader = SQLCmd.ExecuteReader()
            'Return reader
            Dim api = New api()
            Dim result_api_checkper = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/getOpLineProduxtion?LineCd=" & lind_cd)
            Return result_api_checkper
        Catch ex As Exception
            'msgBox("MSSQL Database connect failed. Please contact PC System [Function get_loss_op_mst]")
            'SQLConn.Close()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function get_loss_op_mst_sqlite(lind_cd As String)
re_insert_rework_act:
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            ' sqliteConn.Open()
            ' Dim cmd As New SQLiteCommand
            ' cmd.Connection = sqliteConn
            ' Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss")
            ' Dim reader As SqlDataReader
            ' Dim SQLConn As New SqlConnection() 'The SQL Connection
            ' Dim SQLCmd As New SqlCommand()
            ' cmd.CommandText = "SELECT DISTINCT skld.sk_id,skld.process_no,sscm.sk_name FROM sys_line_mst slm LEFT JOIN sys_skill_line_detail skld ON slm.line_id = skld.line_id LEFT JOIN sys_skill_chart_mst sscm ON skld.sk_id = sscm.sk_id WHERE slm.line_cd = '" & lind_cd & "'"
            ' Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            ' Return LoadSQL
            ' LoadSQL.Close()
            ' sqliteConn.Close()
            Dim api = New api()
            Dim result_api_checkper = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/getOpLineProduxtion?LineCd=" & lind_cd)
            Return result_api_checkper
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function get_loss_op_mst_sqlite]" & ex.Message)
            sqliteConn.Close()
            GoTo re_insert_rework_act
        End Try
    End Function

    Public Shared Function ins_loss_act(pd As String, line_cd As String, wi_plan As String, item_cd As String, seq_no As String, shift_prd As String, st_time As DateTime, end_time As DateTime, loss_time As Integer, loss_type As String, loss_id As String, op_id As String, transfer_flg As String, flg_control As String, pwi_id As String, statusManualE1 As Integer)
        If MainFrm.chk_spec_line = "2" Then
            '
        Else
            Check_loss_and_update_flg_loss()
        End If
        Dim currdated As String
        If statusManualE1 = 0 Then ' Update_datetime ที่ไม่ได้ เกิด จาก Loss Manual
            currdated = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Else  ' Update_datetime ที่ เกิด จาก Loss Manual
            currdated = end_time.ToString("yyyy/MM/dd H:m:s")
        End If
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Dim st_datetime2 As String = st_time.ToString("yyyy/MM/dd H:m:s")
        Dim end_datetime2 As String = end_time.ToString("yyyy/MM/dd H:m:s")
        '''Console.WriteLine("INSERT INTO loss_actual (wi,line_cd,item_cd,seq_no,shift_prd,start_loss,end_loss,loss_time,updated_date,loss_type,loss_cd_id,line_op_id,pd,transfer_flg) VALUES ('" & wi_plan & "','" & line_cd & "','" & item_cd & "','" & seq_no & "','" & shift_prd & "','" & st_datetime2 & "','" & end_datetime2 & "','" & loss_time & "','" & currdated & "','" & loss_type & "','" & loss_id & "','" & op_id & "','" & pd & "','" & transfer_flg & "')")
        ''msgBox("INSERT INTO loss_actual (wi,line_cd,item_cd,seq_no,shift_prd,start_loss,end_loss,loss_time,updated_date,loss_type,loss_cd_id,line_op_id,pd,transfer_flg) VALUES ('" & wi_plan & "','" & line_cd & "','" & item_cd & "','" & seq_no & "','" & shift_prd & "','" & st_datetime2 & "','" & end_datetime2 & "','" & loss_time & "','" & currdated & "','" & loss_type & "','" & loss_id & "','" & op_id & "','" & pd & "','" & transfer_flg & "')")
        Try
            SQLConn.ConnectionString = sqlConnect
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            Try
                If Working_Pro.pwi_id = "0" Then
                    SQLCmd.CommandText = "INSERT INTO loss_actual (wi,line_cd,item_cd,seq_no,shift_prd,start_loss,end_loss,loss_time,updated_date,loss_type,loss_cd_id,line_op_id,pd,transfer_flg , flg_control , pwi_id) VALUES ('" & wi_plan & "','" & line_cd & "','" & item_cd & "','" & seq_no & "','" & shift_prd & "','" & st_datetime2 & "','" & end_datetime2 & "','" & loss_time & "','" & currdated & "','" & loss_type & "','" & loss_id & "','" & op_id & "','" & pd & "','" & transfer_flg & "','" & flg_control & "', '" & DBNull.Value & "')"
                Else
                    SQLCmd.CommandText = "INSERT INTO loss_actual (wi,line_cd,item_cd,seq_no,shift_prd,start_loss,end_loss,loss_time,updated_date,loss_type,loss_cd_id,line_op_id,pd,transfer_flg , flg_control , pwi_id) VALUES ('" & wi_plan & "','" & line_cd & "','" & item_cd & "','" & seq_no & "','" & shift_prd & "','" & st_datetime2 & "','" & end_datetime2 & "','" & loss_time & "','" & currdated & "','" & loss_type & "','" & loss_id & "','" & op_id & "','" & pd & "','" & transfer_flg & "','" & flg_control & "','" & pwi_id & "')"
                End If
            Catch ex As Exception
                'case Load โปรแกรม ครั้ง แรก
                SQLCmd.CommandText = "INSERT INTO loss_actual (wi,line_cd,item_cd,seq_no,shift_prd,start_loss,end_loss,loss_time,updated_date,loss_type,loss_cd_id,line_op_id,pd,transfer_flg , flg_control , pwi_id) VALUES ('" & wi_plan & "','" & line_cd & "','" & item_cd & "','" & seq_no & "','" & shift_prd & "','" & st_datetime2 & "','" & end_datetime2 & "','" & loss_time & "','" & currdated & "','" & loss_type & "','" & loss_id & "','" & op_id & "','" & pd & "','" & transfer_flg & "','" & flg_control & "','" & pwi_id & "')"
            End Try
            'Console.WriteLine(SQLCmd.CommandText)
            reader = SQLCmd.ExecuteReader()
            'SQLConn.Dispose()
            SQLConn.Close()
            Dim api = New api()
            Dim result_api_checkper = api.Load_data("http://" & svApi & "/linenotify_fa/Alert_loss_fa/notify_fa?wi=" & wi_plan & "&flg_control=" & flg_control & "&pd=" & pd & "&loss_id=" & loss_id)
            Loss_reg.date_time_commit_data.Text = st_datetime2
            'SQLConn = Nothing
        Catch ex As Exception
            'msgBox("MSSQL Database connect failed. Please contact PC System [Function ins_loss_act]")
            SQLConn.Close()
            load_show.Show()
            'Application.Exit()
        End Try
    End Function
    Public Shared Function Check_loss_and_update_flg_loss()
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        Try
            SQLConn.ConnectionString = sqlConnect
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            '    'msgBox("INSERT INTO loss_actual (wi,line_cd,item_cd,seq_no,shift_prd,start_loss,end_loss,loss_time,updated_date,loss_type,loss_cd_id,line_op_id,pd,transfer_flg) VALUES ('" & wi_plan & "','" & line_cd & "','" & item_cd & "','" & seq_no & "','" & shift_prd & "','" & st_datetime2 & "','" & end_datetime2 & "','" & loss_time & "','" & currdated & "','" & loss_type & "','" & loss_id & "','" & op_id & "','" & pd & "','" & transfer_flg & "')")
            SQLCmd.CommandText = "Update loss_actual set flg_control = '1' where flg_control = '0' and line_cd = '" & GET_LINE_PRODUCTION() & "'"
            reader = SQLCmd.ExecuteReader()
            'SQLConn.Dispose()
            SQLConn.Close()
        Catch

        End Try
    End Function
    Public Shared Function ins_loss_act_sqlite(pd As String, line_cd As String, wi_plan As String, item_cd As String, seq_no As String, shift_prd As String, st_time As DateTime, end_time As DateTime, loss_time As Integer, loss_type As String, loss_id As String, op_id As String, transfer_flg As String, flg_control As String, pwi_id As String, statusManualE1 As Integer)
re_insert_rework_act:
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            Dim currdated As String
            If statusManualE1 = 0 Then ' Update_datetime ที่ไม่ได้ เกิด จาก Loss Manual
                currdated = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            Else  ' Update_datetime ที่ เกิด จาก Loss Manual
                currdated = end_time.ToString("yyyy/MM/dd HH:mm:ss")
            End If
            Dim reader As SqlDataReader
            Dim SQLConn As New SqlConnection() 'The SQL Connection
            Dim SQLCmd As New SqlCommand()
            Dim st_datetime2 As String = st_time.ToString("yyyy/MM/dd HH:mm:ss")
            Dim end_datetime2 As String = end_time.ToString("yyyy/MM/dd HH:mm:ss")
            st_datetime2 = DateTime.ParseExact(st_datetime2, "yyyy/MM/dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss")
            end_datetime2 = DateTime.ParseExact(end_datetime2, "yyyy/MM/dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss")
            currdated = DateTime.ParseExact(currdated, "yyyy/MM/dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss")
            If Working_Pro.pwi_id = "0" Then
                cmd.CommandText = "INSERT INTO loss_actual(wi,line_cd,item_cd,seq_no,shift_prd,start_loss,end_loss,loss_time,updated_date,loss_type,loss_cd_id,line_op_id,pd,transfer_flg , flg_control , pwi_id) VALUES ('" & wi_plan & "','" & line_cd & "','" & item_cd & "','" & seq_no & "','" & shift_prd & "','" & st_datetime2 & "','" & end_datetime2 & "','" & loss_time & "','" & currdated & "','" & loss_type & "','" & loss_id & "','" & op_id & "','" & pd & "','" & transfer_flg & "','" & flg_control & "','" & DBNull.Value & "')"
            Else
                cmd.CommandText = "INSERT INTO loss_actual(wi,line_cd,item_cd,seq_no,shift_prd,start_loss,end_loss,loss_time,updated_date,loss_type,loss_cd_id,line_op_id,pd,transfer_flg , flg_control , pwi_id) VALUES ('" & wi_plan & "','" & line_cd & "','" & item_cd & "','" & seq_no & "','" & shift_prd & "','" & st_datetime2 & "','" & end_datetime2 & "','" & loss_time & "','" & currdated & "','" & loss_type & "','" & loss_id & "','" & op_id & "','" & pd & "','" & transfer_flg & "','" & flg_control & "','" & pwi_id & "')"
            End If
            ' 'Console.WriteLine("LOSSSS=>>>" & cmd.CommandText)
            Loss_reg.date_time_commit_data.Text = st_datetime2
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            sqliteConn.Close()
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function ins_loss_act_sqlite]" & ex.Message)
            sqliteConn.Close()
            GoTo re_insert_rework_act
        End Try
    End Function
    Public Shared Function check_loss_actual()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select count(id) as check_data from loss_actual where transfer_flg ='0'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            Dim get_qty_sqlite As Integer = 0
            Try
                While LoadSQL.Read()
                    get_qty_sqlite = LoadSQL("check_data").ToString()
                End While
                LoadSQL.Close()
                'sqliteConn.Dispose()
                sqliteConn.Close()
                'sqliteConn = Nothing
                If get_qty_sqlite = 0 Then
                    Return 0
                Else
                    Return 1
                End If
            Catch ex As Exception
                Return 0
            End Try
            ''msgBox(LoadSQL)
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function check_loss_actual]" & ex.Message)
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function check_data_defact_detail()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select count(id) as check_data from production_defect_detail where tr_status ='0'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            Dim get_qty_sqlite As Integer = 0
            Try
                While LoadSQL.Read()
                    get_qty_sqlite = LoadSQL("check_data").ToString()
                End While
                LoadSQL.Close()
                'sqliteConn.Dispose()
                sqliteConn.Close()
                'sqliteConn = Nothing
                If get_qty_sqlite = 0 Then
                    Return 0
                Else
                    Return 1
                End If
            Catch ex As Exception
                Return 0
            End Try
            ''msgBox(LoadSQL)
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function check_data_defact_detail]" & ex.Message)
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function Get_data_loss_actual()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
recheck:
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select * from loss_actual where transfer_flg ='0'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            Dim get_qty_sqlite As Integer = 0
            Try
                While LoadSQL.Read()
                    Dim wi As String = LoadSQL("wi").ToString()
                    Dim line_cd As String = LoadSQL("line_cd").ToString()
                    Dim item_cd As String = LoadSQL("item_cd").ToString()
                    Dim seq_no As String = LoadSQL("seq_no").ToString()
                    Dim shift_prd As String = LoadSQL("shift_prd").ToString()
                    Dim start_loss As String = LoadSQL("start_loss").ToString()
                    Dim end_loss As String = LoadSQL("end_loss").ToString()
                    Dim loss_time As String = LoadSQL("loss_time").ToString()
                    Dim updated_date As String = LoadSQL("updated_date").ToString()
                    Dim loss_type As String = LoadSQL("loss_type").ToString()
                    Dim loss_cd_id As String = LoadSQL("loss_cd_id").ToString()
                    Dim op_id As String = LoadSQL("line_op_id").ToString()
                    Dim pd As String = LoadSQL("pd").ToString()
                    Dim transfer_flg As String = LoadSQL("transfer_flg").ToString()
                    Dim flg_control As String = LoadSQL("flg_control").ToString()
                    Dim pwi_id As String = LoadSQL("pwi_id").ToString()
                    Backoffice_model.ins_loss_act(pd, line_cd, wi, item_cd, seq_no, shift_prd, start_loss, end_loss, loss_time, loss_type, loss_cd_id, op_id, "1", flg_control, pwi_id, "0")
                End While
                LoadSQL.Close()
                update_flg_loss_atc_sqlite()
            Catch ex As Exception
                'msgBox("error function Get_data_rework_actual == > " & ex.Message)
                Check_connect_sqlite()
                GoTo recheck
            End Try
            ''msgBox(LoadSQL)
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function Get_data_rework_actual]" & ex.Message)
            sqliteConn.Close()
        End Try
    End Function
    Public Shared Function Get_data_defact_actual()
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
recheck:
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "select * from production_defect_detail where tr_status ='0'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            Dim get_qty_sqlite As Integer = 0
            Try
                While LoadSQL.Read()
                    Dim id As String = LoadSQL("id").ToString()
                    Dim pd As String = LoadSQL("pd").ToString()
                    Dim line_cd As String = LoadSQL("line_cd").ToString()
                    Dim wi_plan As String = LoadSQL("wi_plan").ToString()
                    tmp_wi = wi_plan
                    Dim item_cd As String = LoadSQL("item_cd").ToString()
                    Dim item_name As String = LoadSQL("item_name").ToString()
                    Dim staff_no As Integer = LoadSQL("staff_no").ToString()
                    Dim seq_no As Integer = LoadSQL("seq_no").ToString()
                    Dim qty As Integer = LoadSQL("qty").ToString()
                    Dim number_qty As String = LoadSQL("number_qty").ToString()
                    Dim st_time As String = LoadSQL("st_time").ToString()
                    Dim end_time As String = LoadSQL("end_time").ToString()
                    Dim use_time As Integer = LoadSQL("use_time").ToString()
                    Dim tr_status As Integer = LoadSQL("tr_status").ToString()
                    Dim flg_defact As String = LoadSQL("flg_defact").ToString()
                    Dim defact_id As String = LoadSQL("defact_id").ToString()
                    Insert_prd_detail_defact(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, qty, st_time, end_time, use_time, number_qty, tr_status, "1", defact_id)
                End While
                LoadSQL.Close()
                update_flg_defact_sqlite()
            Catch ex As Exception
                'msgBox("error function Get_data_defact_actual == > " & ex.Message)
            End Try
            ''msgBox(LoadSQL)
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function Get_data_defact_actual]" & ex.Message)
            sqliteConn.Close()
            Check_connect_sqlite()
            GoTo recheck
        End Try
    End Function
    Public Shared Function alert_loss(wi_plan, flg_control, pd, loss_id)
        Dim api = New api()
        Dim result_api_checkper = api.Load_data("http://" & svApi & "/linenotify_fa/Alert_loss_fa/notify_fa?wi=" & wi_plan & "&flg_control=" & flg_control & "&pd=" & pd & "&loss_id=" & loss_id)
    End Function
    Public Shared Function Update_flg_loss(pd As String, line_cd As String, wi_plan As String, item_cd As String, seq_no As String, shift_prd As String, st_time As DateTime, end_time As DateTime, loss_time As Integer, loss_type As String, loss_id As String, op_id As String, transfer_flg As String, flg_control As String)
re_insert_rework_act:
        Dim reader As SqlDataReader
        Dim SQLConn As New SqlConnection() 'The SQL Connection
        Dim SQLCmd As New SqlCommand()
        SQLConn.ConnectionString = sqlConnect 'Set the Connection String
        Try
            SQLConn.Open()
        Catch ex As Exception
            SQLConn.Close()
            SQLConn.Open()
        End Try
        If op_id = "Proc :[NO PROCESS]" Then
            op_id = "0"
        End If
        SQLCmd.Connection = SQLConn
        Try
            If flg_control = "2" Then
                loss_time = "0"
            End If
            Dim currdated As String = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss")
            Dim st_datetime2 As String = st_time.ToString("yyyy/MM/dd H:mm:ss")
            Dim end_datetime2 As String = end_time.ToString("yyyy/MM/dd H:mm:ss")
            Dim date_now As String = end_time.ToString("dd/MM/yyyy")
            ''Console.WriteLine("Update_flg_loss loss_id ====>" & loss_id)
            If loss_id = "36" Then
                SQLCmd.CommandText = "Update loss_actual 
			set end_loss = '" & end_datetime2 & "',
			loss_time = '" & loss_time & "' , 
			updated_date = '" & currdated & "' , 
			line_op_id = '" & op_id & "'  , 
			transfer_flg = '" & transfer_flg & "' , 
			flg_control ='" & flg_control & "' , 
            loss_cd_id = '" & loss_id & "'
			where wi='" & wi_plan & "' and 
			line_cd = '" & line_cd & "' and 
			item_cd = '" & item_cd & "' and 
			seq_no = '" & seq_no & "' and 
			shift_prd = '" & shift_prd & "' and 
			start_loss = '" & st_datetime2 & "'"
            Else
                SQLCmd.CommandText = "Update loss_actual 
			set end_loss = '" & end_datetime2 & "',
			loss_time = '" & loss_time & "' , 
			updated_date = '" & currdated & "' , 
			line_op_id = '" & op_id & "'  , 
			transfer_flg = '" & transfer_flg & "' , 
			flg_control ='" & flg_control & "'
			where wi='" & wi_plan & "' and 
			line_cd = '" & line_cd & "' and 
			item_cd = '" & item_cd & "' and 
			seq_no = '" & seq_no & "' and 
			shift_prd = '" & shift_prd & "' and 
			start_loss = '" & st_datetime2 & "'"
            End If
            ''Console.WriteLine(SQLCmd.CommandText)
            reader = SQLCmd.ExecuteReader()
            SQLConn.Close()
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function Update_flg_loss]" & ex.Message)
            SQLConn.Close()
            GoTo re_insert_rework_act
        End Try
    End Function
    Public Shared Function Update_flg_loss_sqlite(pd As String, line_cd As String, wi_plan As String, item_cd As String, seq_no As String, shift_prd As String, st_time As DateTime, end_time As DateTime, loss_time As Integer, loss_type As String, loss_id As String, op_id As String, transfer_flg As String, flg_control As String)
        Dim sqliteConn As New SQLiteConnection(sqliteConnect)
        Try
            sqliteConn.Open()
            '  Dim currdated As String = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")
            '  Dim st_datetime2 As String = st_time.ToString("yyyy-MM-dd H:mm:ss")
            '  Dim end_datetime2 As String = end_time.ToString("yyyy-MM-dd H:mm:ss")
            Dim currdated As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            Dim st_datetime2 As String = st_time.ToString("yyyy-MM-dd HH:mm:ss")
            Dim end_datetime2 As String = end_time.ToString("yyyy-MM-dd HH:mm:ss")
            Dim date_now As String = end_time.ToString("dd/MM/yyyy")
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            If loss_id = "36" Then
                cmd.CommandText = "Update loss_actual set  loss_cd_id = '" & loss_id & "', end_loss = '" & end_datetime2 & "',loss_time = '" & loss_time & "' , updated_date = '" & currdated & "' , line_op_id = '" & op_id & "'  , transfer_flg = '" & transfer_flg & "' , flg_control ='" & flg_control & "' where wi='" & wi_plan & "' and line_cd = '" & line_cd & "' and item_cd = '" & item_cd & "' and seq_no = '" & seq_no & "' and shift_prd = '" & shift_prd & "' and start_loss = '" & st_datetime2 & "'"
            Else
                cmd.CommandText = "Update loss_actual set end_loss = '" & end_datetime2 & "',loss_time = '" & loss_time & "' , updated_date = '" & currdated & "' , line_op_id = '" & op_id & "'  , transfer_flg = '" & transfer_flg & "' , flg_control ='" & flg_control & "' where wi='" & wi_plan & "' and line_cd = '" & line_cd & "' and item_cd = '" & item_cd & "' and seq_no = '" & seq_no & "' and shift_prd = '" & shift_prd & "' and start_loss = '" & st_datetime2 & "'"
            End If
            ''Console.WriteLine("Update_flg_loss_sqlite update ===>" & cmd.CommandText)
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            LoadSQL.Close()
            sqliteConn.Close()
            Dim api = New api()
            ''msgBox(LoadSQL)
        Catch ex As Exception
            'msgBox("SQLite Database connect failed. Please contact PC System [Function Update_flg_loss_sqlite]" & ex.Message)
            sqliteConn.Close()
        End Try
    End Function
    Public Function Get_MaxManPower(line_cd As String)
        Try
            Dim api = New api()
            Dim GetData = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_man_limit?line_cd=" & line_cd)
            ''Console.WriteLine("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_man_limit?line_cd=" & line_cd)
            If GetData <> "0" Then
                Return GetData
            Else
                'msgBox("LoadManPower = 0")
                Return 0
            End If
        Catch ex As Exception
            'msgBox("Error Function Get_MaxManPower In Backoffice_model")
        End Try
        Return 0
    End Function
    Public Function Get_Plan_All_By_Line(line_cd As String, shift As String, dateStart As String, timeStart As String, flg_spec As String, item_cd As String)
        Try
            Dim api = New api()
            Dim GetData = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_Plan_All_By_Line?line_cd=" & line_cd & "&shift=" & shift & "&dateStart=" & dateStart & "&timeStart=" & timeStart & "&flg_spec=" & flg_spec & "&item_cd=" & item_cd)
            ''Console.WriteLine("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_Plan_All_By_Line?line_cd=" & line_cd & "&shift=" & shift & "&dateStart=" & dateStart & "&timeStart=" & timeStart & "&flg_spec=" & flg_spec & "&item_cd=" & item_cd)
            Return GetData
        Catch ex As Exception
            'msgBox("Error Function Get_Plan_All_By_Line In Backoffice_model")
        End Try
        Return 0
    End Function
    Public Function Get_Plan_All_By_Line_Auto_Loss_X(line_cd As String, shift As String, dateStart As String, timeStart As String, flg_spec As String, item_cd As String)
        Try
            Dim api = New api()
            Dim GetData = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_Plan_All_By_Line_Auto_Loss_X?line_cd=" & line_cd & "&shift=" & shift & "&dateStart=" & dateStart & "&timeStart=" & timeStart & "&flg_spec=" & flg_spec & "&item_cd=" & item_cd)
            WriteDebugDiagnostic("Get_Plan_All_By_Line_Auto_Loss_X requested for line " & line_cd)
            Return GetData
        Catch ex As Exception
            'msgBox("Error Function Get_Plan_All_By_Line_Auto_Loss_X In Backoffice_model")
        End Try
        Return 0
    End Function
    Public Function Get_Plan_All_By_Line_Auto_Loss_X_adjust_loss(line_cd As String, shift As String, dateStart As String, timeStart As String, flg_spec As String, item_cd As String, dateEnd As String, timeEnd As String)
        Try
            Dim api = New api()
            Dim GetData = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_Plan_All_By_Line_Auto_Loss_X_adjust_loss?line_cd=" & line_cd & "&shift=" & shift & "&dateStart=" & dateStart & "&timeStart=" & timeStart & "&flg_spec=" & flg_spec & "&item_cd=" & item_cd & "&dateEnd=" & dateEnd & "&timeEnd=" & timeEnd)
            'Console.WriteLine("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_Plan_All_By_Line_Auto_Loss_X_adjust_loss?line_cd=" & line_cd & "&shift=" & shift & "&dateStart=" & dateStart & "&timeStart=" & timeStart & "&flg_spec=" & flg_spec & "&item_cd=" & item_cd & "&dateEnd=" & dateEnd & "&timeEnd=" & timeEnd)
            Return GetData
        Catch ex As Exception
            'msgBox("Error Function Get_Plan_All_By_Line_Auto_Loss_X_adjust_loss In Backoffice_model")
        End Try
        Return 0
    End Function
    Public Function Get_Plan_All_By_Line_LOSS_A(line_cd As String, shift As String, dateStart As String, timeStart As String, flg_spec As String, item_cd As String)
        Try
            Dim api = New api()
            Dim GetData = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_Plan_All_By_Line_Auto_Loss_A?line_cd=" & line_cd & "&shift=" & shift & "&dateStart=" & dateStart & "&timeStart=" & timeStart & "&flg_spec=" & flg_spec & "&item_cd=" & item_cd)
            Return GetData
        Catch ex As Exception
            'msgBox("Error Function Get_Plan_All_By_Line_LOSS_A In Backoffice_model")
        End Try
        Return 0
    End Function
    Public Function Get_Plan_All_By_Line_LOSS_E1(line_cd As String, shift As String, dateStart As String, timeStart As String, flg_spec As String, item_cd As String)
        Try
            Dim api = New api()
            Dim GetData = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_Plan_All_By_Line_Auto_Loss_E1?line_cd=" & line_cd & "&shift=" & shift & "&dateStart=" & dateStart & "&timeStart=" & timeStart & "&flg_spec=" & flg_spec & "&item_cd=" & item_cd)
            WriteDebugDiagnostic("Get_Plan_All_By_Line_Auto_Loss_E1 requested for line " & line_cd)
            Return GetData
        Catch ex As Exception
            'msgBox("Error Function Get_Plan_All_By_Line_LOSS_E1 In Backoffice_model")
        End Try
        Return 0
    End Function
    Public Shared Function AlertCheck_close_lot(line_cd As String, dep_cd As String)
        Try
            Dim api = New api()
            Dim GetData = api.Load_data("http://192.168.161.77:5002/API_NEW_FA_PY2/notify/send?line_cd=" & line_cd & "&dep_cd=" & dep_cd)
            Return GetData
        Catch ex As Exception
            'msgBox("Error Function AlertCheck_close_lot In Backoffice_model")
        End Try
        Return 0
    End Function
    Public Shared Function Get_plan_production_critical()
        Try
            Dim api = New api()
            Dim result_api_checkper = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/Api_Get_plan_production_critical?line_cd=" & GET_LINE_PRODUCTION())
            Return result_api_checkper
        Catch ex As Exception
            'msgBox("Error Function Get_plan_production_critical In Backoffice_model")
        End Try
    End Function
    Public Shared Function GetDataPlanCritical(wi As String)
        Try
            Dim api = New api()
            Dim result_api_checkper = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/Api_Get_plan_production_critical/GetDataPlanCritical?wi=" & wi & "&line_cd=" & GET_LINE_PRODUCTION())
            Return result_api_checkper
        Catch ex As Exception
            'msgBox("Error Function GetDataPlanCritical In Backoffice_model")
        End Try
    End Function
    Public Shared Sub UpdateFlgZero(line_cd As String)
        Try
            Dim api = New api()
            Dim result_api_checkper = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/UpdateFlgZero?line_cd=" & line_cd)
        Catch ex As Exception
            'msgBox("Error Function UpdateFlgZeroSpecial In Backoffice_model")
        End Try

    End Sub
    Public Shared Sub UpdateFlgZeroSpecial(arrayWI As Array)
        Try
            Dim requestFunction As New JObject()
            Dim api = New api()
            Dim jArrayWI As New JArray(arrayWI)
            requestFunction("wi") = jArrayWI
            Dim url As String = "http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/UpdateFlgZeroSpecial"
            Dim result = api.Load_dataPOST(url, requestFunction)
        Catch ex As Exception
            'msgBox("Error Function UpdateFlgZeroSpecial In Backoffice_model")
        End Try
    End Sub
    Public Shared Sub UpdateWorkingSpecial(arrayWI As Array)
        Try
            Dim api = New api()
            'Dim reusult_data = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/Update_supply_dev_WorkingSpecial?wi1=" & wi1 & "&wi2=" & wi2 & "&wi3=" & wi3 & "&wi4=" & wi4 & "&wi5=" & wi5)
            Dim requestFunction As New JObject()
            Dim jArrayWI As New JArray(arrayWI)
            requestFunction("wi") = jArrayWI
            Dim url As String = "http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/Update_supply_dev_WorkingSpecial"
            Dim result = api.Load_dataPOST(url, requestFunction)
        Catch ex As Exception
            'msgBox("Error Function UpdateWorkingSpecial In Backoffice_model")
        End Try
    End Sub
    Public Shared Function M_Get_mst_line(line_cd As String)
        Try
            Dim api = New api()
            'Dim reusult_data = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/Update_supply_dev_WorkingSpecial?wi1=" & wi1 & "&wi2=" & wi2 & "&wi3=" & wi3 & "&wi4=" & wi4 & "&wi5=" & wi5)
            Dim result = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/Get_mst_line?line_cd=" & line_cd)
            Return result
        Catch ex As Exception
            'msgBox("Error Function M_Get_mst_line In Backoffice_model")
        End Try
    End Function
    Public Shared Function M_loadsecPopUp_Loss_E1(dateStart As String, timeStart As String, shift As String, item_cd As String, flg_spec As String, line_cd As String)
        Try
            Dim api = New api()
            'Dim reusult_data = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/Update_supply_dev_WorkingSpecial?wi1=" & wi1 & "&wi2=" & wi2 & "&wi3=" & wi3 & "&wi4=" & wi4 & "&wi5=" & wi5)
            Dim result = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/loadsecPopUp_Loss_E1?dateStart=" & dateStart & "&timeStart=" & timeStart & "&Shift=" & shift & "&item_cd=" & item_cd & "&flg_spec=" & flg_spec & "&line_cd=" & line_cd)
            Return result
        Catch ex As Exception
            'msgBox("Error Function M_loadsecPopUp_Loss_E1 In Backoffice_model")
        End Try
    End Function
    Public Shared Function GetDataLoss(start_loss As String, end_loss As String, line_cd As String)
        Try
            Dim api = New api()
            'Dim reusult_data = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/Update_supply_dev_WorkingSpecial?wi1=" & wi1 & "&wi2=" & wi2 & "&wi3=" & wi3 & "&wi4=" & wi4 & "&wi5=" & wi5)
            Dim result = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/getDataloss?dateStart=" & start_loss & "&dateEnd=" & end_loss & "&line_cd=" & line_cd)
            Return result
        Catch ex As Exception
            'msgBox("Error Function GetDataLoss In Backoffice_model")
        End Try
    End Function
    Public Shared Async Function GetPercenPlanned_OEE(line_cd As String) As Task(Of String)
        Try
            Dim url As String = "http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GetPercenPlanned_OEE " &
                            "?line_cd=" & line_cd
            ' ✅ แปลงให้ async โดยรันบน background thread
            Dim api = New api()
            Dim rsData As String = Await api.Load_dataAsync(url)
            Return rsData
        Catch ex As Exception
            'msgBox("❗ connect Api Fail in GetPercenPlanned_OEE = " & ex.Message)
            Return "0"
        End Try
    End Function
    Public Shared Sub insert_info_loss_setting_machine(line_cd As String, start_loss As String, end_loss As String, created_by As String, updated_by As String, pwi_id As String, status_flg As String, loss_code As String)
        Try
            Dim api = New api()
            'Dim reusult_data = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/Update_supply_dev_WorkingSpecial?wi1=" & wi1 & "&wi2=" & wi2 & "&wi3=" & wi3 & "&wi4=" & wi4 & "&wi5=" & wi5)
            Dim result = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/insert_info_loss_setting_machine?line_cd=" & line_cd & "&start_loss=" & start_loss & "&end_loss=" & end_loss & "&created_by=" & created_by & "&updated_by=" & updated_by & "&pwi_id=" & pwi_id & "&status_flg=" & status_flg & "&loss_code=" & loss_code)
            WriteDebugDiagnostic("insert_info_loss_setting_machine requested for line " & line_cd)
        Catch ex As Exception
            WriteDebugDiagnostic("insert_info_loss_setting_machine requested for line " & line_cd)
            MsgBox("Error Function insert_info_loss_setting_machine In Backoffice_model")
        End Try
    End Sub
    Public Shared Sub updated_info_loss_setting_machine(line_cd As String, start_loss As String, end_loss As String, created_by As String, updated_by As String, pwi_id As String, status_flg As String, loss_code As String)
        Try
            Dim api = New api()
            WriteDebugDiagnostic("updated_info_loss_setting_machine requested for line " & line_cd)
            Dim result = api.Load_data("http://" & svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/updated_info_loss_setting_machine?line_cd=" & line_cd & "&start_loss=" & start_loss & "&end_loss=" & end_loss & "&created_by=" & created_by & "&updated_by=" & updated_by & "&pwi_id=" & pwi_id & "&status_flg=" & status_flg & "&loss_code=" & loss_code)
        Catch ex As Exception
            WriteDebugDiagnostic("updated_info_loss_setting_machine requested for line " & line_cd)
            MsgBox("Error Function updated_info_loss_setting_machine In Backoffice_model")
        End Try
    End Sub
End Class
