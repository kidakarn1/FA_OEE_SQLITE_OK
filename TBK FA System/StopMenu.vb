Imports System.Windows.Forms

Public Class StopMenu

    ' ----------------- ฟิลด์สถานะ -----------------
    Private contDelay As Integer = 0
    Private _timerStarted As Boolean = False
    Public date_start_data As DateTime = DateTime.Now

    ' ----------------- คุม TimerLossBT ให้สะอาด -----------------
    Private Sub StartTimerSafe()
        If Not _timerStarted Then
            TimerLossBT.Start()
            _timerStarted = True
        End If
    End Sub

    Private Sub StopTimerSafe()
        If _timerStarted Then
            TimerLossBT.Stop()
            _timerStarted = False
        End If
    End Sub

    ' ----------------- Lifecycle -----------------
    Private Sub StopMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' เวลาเริ่มต้นของช่วง Loss
        If Working_Pro.check_in_up_seq = 0 Then
            Try
                ' ถ้า Gdate_now_date เป็น DateTime จะเข้าได้เลย
                date_start_data = Working_Pro.Gdate_now_date
            Catch
                date_start_data = DateTime.Now
            End Try
        Else
            date_start_data = DateTime.Now
        End If

        lbLineCode.Text = MainFrm.Label4.Text

        ' เปิด Timer แบบปลอดภัย
        StartTimerSafe()
        TimerLossBT.Enabled = True
    End Sub

    Private Sub StopMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' หยุด Timer + ตัด event ก่อนปิดฟอร์ม กัน Tick ยิงใส่ Handle ที่ถูก Dispose แล้ว
        StopTimerSafe()
        RemoveHandler TimerLossBT.Tick, AddressOf TimerLossBT_Tick
    End Sub

    ' ----------------- ปุ่ม Continue -----------------
    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        SatrtWork()
    End Sub

    ' ----------------- เริ่มงานต่อ (ไม่ใช้ Async) -----------------
    Public Sub SatrtWork()
        ' ถ้ามี Panel สรุป Loss แสดงอยู่ ให้ Update Auto Loss ก่อน
        If PanelShowLoss.Visible Then
            UpdateAutoLoss()
        End If

        ' คำนวณ BreakTime รอบต่อไป + Log BreakTime
        Dim BreakTime As String = Backoffice_model.GetTimeAutoBreakTime(MainFrm.Label4.Text, Working_Pro.Label14.Text)

        If MainFrm.chk_spec_line = "2" Then
            Dim genSeq As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
            Dim iSeq = genSeq
            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                iSeq += 1
                Backoffice_model.ILogLossBreakTime(MainFrm.Label4.Text, itemPlanData.wi, iSeq)
            Next
        Else
            Backoffice_model.ILogLossBreakTime(MainFrm.Label4.Text, Working_Pro.wi_no.Text, Working_Pro.Label22.Text)
        End If

        ' ส่งเวลาใหม่กลับไปให้ Working_Pro ตั้งรอบถัดไป
        Working_Pro.lbNextTime.Text = BreakTime
        Working_Pro.RescheduleAfterNextTimeChanged(BreakTime)

        ' เดินการผลิตต่อ
        Working_Pro.Enabled = True
        Working_Pro.Start_Production()

        ' ปิดฟอร์มนี้อย่างปลอดภัย: หยุด Timer ก่อนทุกครั้ง
        StopTimerSafe()
        Me.Hide()
        Me.Close()
    End Sub

    ' ----------------- Update Auto Loss (Online/Offline) -----------------
    Public Sub UpdateAutoLoss()
        Dim pd As String = MainFrm.Label6.Text
        Dim wi_plan As String = Working_Pro.wi_no.Text
        Dim line_cd As String = MainFrm.Label4.Text
        Dim item_cd As String = Working_Pro.Label3.Text
        Dim seq_no As Integer = Working_Pro.Label22.Text
        Dim shift_prd As String = Working_Pro.Label14.Text
        Dim end_loss As DateTime = DateTime.Now

        Dim loss_type As String = "0"  ' 0:Normally, 1:Manual
        Dim op_id As String = "0"
        Dim loss_cd_id As String = If(closeLotsummary.Visible, "36", "35")

        Try
            Dim online As Boolean
            Try
                online = My.Computer.Network.Ping(Backoffice_model.svp_ping)
            Catch
                online = False
            End Try
            transfer_flg = If(online, "1", "0")

            If MainFrm.chk_spec_line = "2" Then
                Dim genSeq As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                Dim iSeq = genSeq
                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                    iSeq += 1
                    If online Then
                        Backoffice_model.Update_flg_loss(pd, line_cd, itemPlanData.wi, itemPlanData.item_cd, iSeq, shift_prd,
                                                         date_start_data, end_loss, test_time_loss_time.Text, loss_type,
                                                         loss_cd_id, op_id, transfer_flg, "1")
                        Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, itemPlanData.wi, itemPlanData.item_cd, iSeq, shift_prd,
                                                                date_start_data, end_loss, test_time_loss_time.Text, loss_type,
                                                                loss_cd_id, op_id, transfer_flg, "1")
                        Backoffice_model.alert_loss(itemPlanData.wi, "1", pd, loss_cd_id)
                    Else
                        Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, itemPlanData.wi, itemPlanData.item_cd, iSeq, shift_prd,
                                                                date_start_data, end_loss, test_time_loss_time.Text, loss_type,
                                                                loss_cd_id, op_id, transfer_flg, "1")
                    End If
                Next
            Else
                If online Then
                    Backoffice_model.Update_flg_loss(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd,
                                                     date_start_data, end_loss, test_time_loss_time.Text, loss_type,
                                                     loss_cd_id, op_id, transfer_flg, "1")
                    Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd,
                                                            date_start_data, end_loss, test_time_loss_time.Text, loss_type,
                                                            loss_cd_id, op_id, transfer_flg, "1")
                    Backoffice_model.alert_loss(wi_plan, "1", pd, loss_cd_id)
                Else
                    Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd,
                                                            date_start_data, end_loss, test_time_loss_time.Text, loss_type,
                                                            loss_cd_id, op_id, transfer_flg, "1")
                End If
            End If

        Catch
            ' โหมดกันตาย: ลง SQLite อย่างเดียว
            transfer_flg = "0"
            If MainFrm.chk_spec_line = "2" Then
                Dim genSeq As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                Dim iSeq = genSeq
                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                    iSeq += 1
                    Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, itemPlanData.wi, itemPlanData.item_cd, iSeq, shift_prd,
                                                            date_start_data, end_loss, test_time_loss_time.Text, loss_type,
                                                            loss_cd_id, op_id, transfer_flg, "1")
                Next
            Else
                Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd,
                                                        date_start_data, end_loss, test_time_loss_time.Text, loss_type,
                                                        loss_cd_id, op_id, transfer_flg, "1")
            End If
        End Try
    End Sub

    ' ----------------- Timer: เดินนาที/ตัดเข้าสู่ PanelShowLoss -----------------
    Private Sub TimerLossBT_Tick(sender As Object, e As EventArgs) Handles TimerLossBT.Tick
        Try
            contDelay += 1

            ' ถ้ามีหน้ากรอก Loss อื่นเปิดอยู่ ให้ข้าม
            If Application.OpenForms().OfType(Of Loss_reg)().Any() _
               OrElse Application.OpenForms().OfType(Of Loss_reg_pass)().Any() Then
                Return
            End If

            ' ครบ CountDelay → แสดงสรุป
            If Not String.IsNullOrEmpty(Backoffice_model.CountDelay) AndAlso btnBreakTime.Visible Then
                Dim target As Integer
                If Integer.TryParse(Backoffice_model.CountDelay, target) Then
                    If contDelay >= target Then
                        contDelay = 0 ' สำคัญ: รีเซ็ต
                        btnContinue.BringToFront()
                        btnContinue.Visible = True
                        insLoss()
                        lock.Visible = True
                        btnBreakTime.Visible = False
                        lbLossCode.Text = Backoffice_model.LossCodeAuto
                        lbStartCount.Text = Backoffice_model.TimeStartBreakTime
                        lbEndCount.Text = DateTime.Now.ToString("H:mm:ss")
                        PanelShowLoss.Visible = True
                    End If
                End If
            End If

            ' อัปเดตเวลาปัจจุบัน + รวมเวลาที่ผ่านไป
            Dim nowDt As DateTime = DateTime.Now
            lbEndCount.Text = nowDt.ToString("H:mm:ss")

            Dim totalMin As Integer = CInt((nowDt - date_start_data).TotalMinutes)
            If totalMin < 0 Then totalMin = Math.Abs(totalMin)
            test_time_loss_time.Text = totalMin.ToString()

        Catch
            ' อย่าให้ผู้ใช้ค้าง
            Working_Pro.Enabled = True
        End Try
    End Sub

    ' ----------------- บันทึก Loss ครั้งนี้ (Online/Offline) -----------------
    Public Sub insLoss()
        If Working_Pro.check_in_up_seq = 0 Then
            Try
                date_start_data = Working_Pro.Gdate_now_date
            Catch
                date_start_data = DateTime.Now
            End Try
        End If

        Dim pd As String = MainFrm.Label6.Text
        Dim wi_plan As String = Working_Pro.wi_no.Text
        Dim line_cd As String = MainFrm.Label4.Text
        Dim item_cd As String = Working_Pro.Label3.Text
        Dim seq_no As Integer = Working_Pro.Label22.Text
        Dim shift_prd As String = Working_Pro.Label14.Text

        Dim start_loss As DateTime = date_start_data
        Dim end_loss As DateTime = DateTime.Now
        Dim total_loss As Integer = CInt((end_loss - start_loss).TotalMinutes)

        Dim loss_type As String = "0"
        Dim op_id As String = "0"
        Dim statusManualE1 As Integer = 0

        Try
            Dim online As Boolean
            Try
                online = My.Computer.Network.Ping(Backoffice_model.svp_ping)
            Catch
                online = False
            End Try
            transfer_flg = If(online, "1", "0")

            If MainFrm.chk_spec_line = "2" Then
                Dim genSeq As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                Dim iSeq = genSeq
                Dim j As Integer = 0
                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                    iSeq += 1
                    Dim localPwis As String = ""
                    Try : localPwis = Working_Pro.Spwi_id(j) : Catch : localPwis = "" : End Try

                    If online Then
                        Backoffice_model.ins_loss_act(pd, line_cd, itemPlanData.wi, itemPlanData.item_cd, iSeq, shift_prd,
                                                      start_loss, end_loss, total_loss, loss_type,
                                                      Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0",
                                                      localPwis, statusManualE1)
                    End If
                    Backoffice_model.ins_loss_act_sqlite(pd, line_cd, itemPlanData.wi, itemPlanData.item_cd, iSeq, shift_prd,
                                                         start_loss, end_loss, total_loss, loss_type,
                                                         Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0",
                                                         localPwis, statusManualE1)
                    j += 1
                Next
            Else
                If online Then
                    Backoffice_model.ins_loss_act(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd,
                                                  start_loss, end_loss, total_loss, loss_type,
                                                  Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0",
                                                  Working_Pro.pwi_id, statusManualE1)
                End If
                Backoffice_model.ins_loss_act_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd,
                                                     start_loss, end_loss, total_loss, loss_type,
                                                     Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0",
                                                     Working_Pro.pwi_id, statusManualE1)
            End If

        Catch
            ' fallback offline
            transfer_flg = "0"
            If MainFrm.chk_spec_line = "2" Then
                Dim genSeq As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                Dim iSeq = genSeq
                Dim j As Integer = 0
                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                    iSeq += 1
                    Dim localPwis As String = ""
                    Try : localPwis = Working_Pro.Spwi_id(j) : Catch : localPwis = "" : End Try
                    Backoffice_model.ins_loss_act_sqlite(pd, line_cd, itemPlanData.wi, itemPlanData.item_cd, iSeq, shift_prd,
                                                         start_loss, end_loss, total_loss, loss_type,
                                                         Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0",
                                                         localPwis, statusManualE1)
                    j += 1
                Next
            Else
                Backoffice_model.ins_loss_act_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd,
                                                     start_loss, end_loss, total_loss, loss_type,
                                                     Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0",
                                                     Working_Pro.pwi_id, statusManualE1)
            End If
        End Try
    End Sub

    ' ----------------- ปุ่ม Break → แสดง Panel + คำนวณทันที -----------------
    Private Sub btnBreakTime_Click(sender As Object, e As EventArgs) Handles btnBreakTime.Click
        If String.IsNullOrEmpty(Backoffice_model.CountDelay) Then Return

        btnContinue.BringToFront()
        btnContinue.Visible = True
        insLoss()
        lock.Visible = True
        btnBreakTime.Visible = False
        lbLossCode.Text = Backoffice_model.LossCodeAuto
        lbStartCount.Text = Backoffice_model.TimeStartBreakTime
        lbEndCount.Text = DateTime.Now.ToString("H:mm:ss")
        PanelShowLoss.Visible = True

        ' อัปเดตรวมเวลาที่ผ่านไป ณ ตอนกด
        Dim totalMin As Integer = CInt((DateTime.Now - date_start_data).TotalMinutes)
        If totalMin < 0 Then totalMin = Math.Abs(totalMin)
        test_time_loss_time.Text = totalMin.ToString()
    End Sub

End Class
