Public Class StopMenu
    Dim contDelay As Integer = 0
    Dim flg_check As Integer = 0
    Public date_start_data = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
    Public Async Function SatrtWork() As Task
        If PanelShowLoss.Visible Then
            UpdateAutoLoss()
            Dim BreakTime = Backoffice_model.GetTimeAutoBreakTime(MainFrm.Label4.Text, Working_Pro.Label14.Text) ' for set data 
            ' MsgBox("Next BreakTime ===>" & BreakTime)
            If MainFrm.chk_spec_line = "2" Then
                Dim GenSEQ As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                Dim Iseq = GenSEQ
                Dim j As Integer = 0
                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                    Iseq += 1
                    Dim special_wi As String = itemPlanData.wi
                    Dim special_item_cd As String = itemPlanData.item_cd
                    Dim special_item_name As String = itemPlanData.item_name
                    Backoffice_model.ILogLossBreakTime(MainFrm.Label4.Text, special_wi, Iseq)
                Next
            Else
                Backoffice_model.ILogLossBreakTime(MainFrm.Label4.Text, Working_Pro.wi_no.Text, Working_Pro.Label22.Text)
            End If
            Working_Pro.lbNextTime.Text = BreakTime
            If Working_Pro.slm_flg_qr_prod = 1 Then ' Scan Qrproduct
                If Working_Pro.RemainScanDmc = 1 Then
                    Working_Pro.Enabled = True
                    TimerLossBT.Start()
                    Working_Pro.Start_Production()
                Else
                    Working_Pro.Enabled = True
                    'ScanQRprod.ManageQrScanFA("1", RemainScanDmc)
                    'ScanQRprod.ShowDialog()
                    TimerLossBT.Start()
                    Me.Hide()
                    Working_Pro.Start_Production()
                End If
            Else
                Working_Pro.Enabled = True
                TimerLossBT.Start()
                Working_Pro.Start_Production()
            End If
            Me.Close()
        Else
            Dim BreakTime = Backoffice_model.GetTimeAutoBreakTime(MainFrm.Label4.Text, Working_Pro.Label14.Text) ' for set data 
            If MainFrm.chk_spec_line = "2" Then
                Dim GenSEQ As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                Dim Iseq = GenSEQ
                Dim j As Integer = 0
                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                    Iseq += 1
                    Dim special_wi As String = itemPlanData.wi
                    Dim special_item_cd As String = itemPlanData.item_cd
                    Dim special_item_name As String = itemPlanData.item_name
                    Backoffice_model.ILogLossBreakTime(MainFrm.Label4.Text, special_wi, Iseq)
                Next
            Else
                Backoffice_model.ILogLossBreakTime(MainFrm.Label4.Text, Working_Pro.wi_no.Text, Working_Pro.Label22.Text)
            End If
            Working_Pro.lbNextTime.Text = BreakTime
            If Working_Pro.slm_flg_qr_prod = 1 Then ' Scan Qrproduct
                If Working_Pro.RemainScanDmc < 1 Then
                    Working_Pro.Enabled = True
                    TimerLossBT.Start()
                    Working_Pro.Start_Production()
                Else
                    Working_Pro.Enabled = True
                    ' ScanQRprod.ManageQrScanFA("1", RemainScanDmc)
                    ' ScanQRprod.ShowDialog()
                    TimerLossBT.Start()
                    Me.Hide()
                    Working_Pro.Start_Production()
                End If
            Else
                Working_Pro.Enabled = True
                TimerLossBT.Start()
                Working_Pro.Start_Production()
            End If
            Me.Close()
        End If
    End Function
    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        SatrtWork()
    End Sub
    Public Async Function UpdateAutoLoss() As Task
        Dim pd As String = MainFrm.Label6.Text
        Dim sel_combo As String = 0 'ComboBox1.SelectedIndex
        Dim wi_plan As String = Working_Pro.wi_no.Text
        Dim line_cd As String = MainFrm.Label4.Text
        Dim item_cd As String = Working_Pro.Label3.Text
        Dim seq_no As Integer = Working_Pro.Label22.Text
        Dim shift_prd As String = Working_Pro.Label14.Text
        Dim start_loss As Date = Date.Parse(TimeOfDay.ToString("H:mm:ss"))
        Dim end_loss As Date = Date.Parse(TimeOfDay.ToString("H:mm:ss"))
        Dim date1 As Date = Date.Parse(TimeOfDay.ToString("H:mm:ss"))
        Dim date2 As Date = Date.Parse(TimeOfDay.ToString("H:mm:ss"))
        ' MsgBox("SAAAA")
        Dim total_loss As Integer = DateDiff(DateInterval.Minute, date1, date2)
        'MsgBox("SFFFFF")
        Dim loss_type As String = "0"  '0:Normally,1:Manual
        Dim op_id As String = "0"
        'MsgBox("SCCCC")
        Dim loss_cd_id As String = "35"
        'MsgBox("Ready load UpdateAutoLoss 2.")
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                transfer_flg = "1"
                If closeLotsummary.Visible Then
                    loss_cd_id = "36"
                Else
                    loss_cd_id = "35"
                End If
                If MainFrm.chk_spec_line = "2" Then
                    'Console.WriteLine(MainFrm.ArrayDataPlan.ToArray().Length)
                    Dim GenSEQ As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                    Dim Iseq = GenSEQ
                    Dim j As Integer = 0
                    For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                        Iseq += 1
                        Dim special_wi As String = itemPlanData.wi
                        Dim special_item_cd As String = itemPlanData.item_cd
                        Dim special_item_name As String = itemPlanData.item_name
                        Backoffice_model.Update_flg_loss(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, date_start_data, end_loss, test_time_loss_time.Text, loss_type, loss_cd_id, op_id, transfer_flg, "1")
                        Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, date_start_data, end_loss, test_time_loss_time.Text, loss_type, loss_cd_id, op_id, transfer_flg, "1")
                        Backoffice_model.alert_loss(special_wi, "1", pd, loss_cd_id)
                    Next
                Else
                    Backoffice_model.Update_flg_loss(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, date_start_data, end_loss, test_time_loss_time.Text, loss_type, loss_cd_id, op_id, transfer_flg, "1")
                    Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, date_start_data, end_loss, test_time_loss_time.Text, loss_type, loss_cd_id, op_id, transfer_flg, "1")
                    Backoffice_model.alert_loss(wi_plan, "1", pd, loss_cd_id)
                End If
            Else
                transfer_flg = "0"
                If MainFrm.chk_spec_line = "2" Then
                    Dim GenSEQ As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                    Dim Iseq = GenSEQ
                    Dim j As Integer = 0
                    For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                        Iseq += 1
                        Dim special_wi As String = itemPlanData.wi
                        Dim special_item_cd As String = itemPlanData.item_cd
                        Dim special_item_name As String = itemPlanData.item_name
                        Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, date_start_data, end_loss, test_time_loss_time.Text, loss_type, loss_cd_id, op_id, transfer_flg, "1")                'Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                    Next
                Else
                    Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, date_start_data, end_loss, test_time_loss_time.Text, loss_type, loss_cd_id, op_id, transfer_flg, "1")                'Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                End If
            End If
        Catch ex As Exception
            transfer_flg = "0"
            If MainFrm.chk_spec_line = "2" Then
                Dim GenSEQ As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                Dim Iseq = GenSEQ
                Dim j As Integer = 0
                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                    Iseq += 1
                    Dim special_wi As String = itemPlanData.wi
                    Dim special_item_cd As String = itemPlanData.item_cd
                    Dim special_item_name As String = itemPlanData.item_name
                    Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, date_start_data, end_loss, test_time_loss_time.Text, loss_type, loss_cd_id, op_id, transfer_flg, "1")            'Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                Next
            Else
                Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, date_start_data, end_loss, test_time_loss_time.Text, loss_type, loss_cd_id, op_id, transfer_flg, "1")            'Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
            End If
        End Try
        Await Task.Delay(100) ' Mock delay for async operation
    End Function
    Private Sub btnBreakTime_Click(sender As Object, e As EventArgs) Handles btnBreakTime.Click
        If Backoffice_model.CountDelay <> "" Then
            'Console.WriteLine(contDelay)
            'MsgBox("ครบ 5 นาที")
            btnContinue.BringToFront()
            btnContinue.Visible = True
            'btnContinue.BackColor = Color.FromArgb(63, 63, 63)
            'btnContinue.Size = New Size(312, 555)
            insLoss()
            lock.Visible = True
            btnBreakTime.Visible = False
            lbLossCode.Text = Backoffice_model.LossCodeAuto
            lbStartCount.Text = Backoffice_model.TimeStartBreakTime
            lbEndCount.Text = TimeOfDay.ToString("H:mm:ss")
            btnBreakTime.Visible = False
            PanelShowLoss.Visible = True
            lbEndCount.Text = TimeOfDay.ToString("H:mm:ss")
            Dim date_end_data As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
            Dim date_end As Date = date_end_data
            Dim total_loss As Integer = DateDiff(DateInterval.Minute, date_start_data, date_end)
            test_time_loss_time.Text = total_loss
            If total_loss < 0 Then
                Minutes_total = Math.Abs(CDbl(Val(test_time_loss_time.Text)))
                test_time_loss_time.Text = Minutes_total
            End If
        End If
        lbEndCount.Text = TimeOfDay.ToString("H:mm:ss")
    End Sub
    Private Sub TimerLossBT_Tick(sender As Object, e As EventArgs) Handles TimerLossBT.Tick
        Try
            contDelay += 1
            If Application.OpenForms().OfType(Of Loss_reg).Any Or Application.OpenForms().OfType(Of Loss_reg_pass).Any Then
            Else
                If Backoffice_model.CountDelay <> "" Then
                    If btnBreakTime.Visible = True Then

                        'Console.WriteLine(contDelay)
                        If CDbl(Val(contDelay)) = CDbl(Val((Backoffice_model.CountDelay))) Then
                            'MsgBox("ครบ 5 นาที")
                            btnContinue.BringToFront()
                            btnContinue.Visible = True
                            'btnContinue.BackColor = Color.FromArgb(63, 63, 63)
                            'btnContinue.Size = New Size(312, 555)
                            insLoss()
                            lock.Visible = True
                            btnBreakTime.Visible = False
                            lbLossCode.Text = Backoffice_model.LossCodeAuto
                            lbStartCount.Text = Backoffice_model.TimeStartBreakTime
                            lbEndCount.Text = TimeOfDay.ToString("H:mm:ss")
                            btnBreakTime.Visible = False
                            PanelShowLoss.Visible = True
                        End If
                    End If
                    lbEndCount.Text = TimeOfDay.ToString("H:mm:ss")
                    Dim date_end_data As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
                    Dim date_end As Date = date_end_data
                    Dim total_loss As Integer = DateDiff(DateInterval.Minute, date_start_data, date_end)
                    test_time_loss_time.Text = total_loss
                    If total_loss < 0 Then
                        Minutes_total = Math.Abs(CDbl(Val(test_time_loss_time.Text)))
                        test_time_loss_time.Text = Minutes_total
                    End If
                End If
                lbEndCount.Text = TimeOfDay.ToString("H:mm:ss")

            End If
        Catch ex As Exception
            Working_Pro.Enabled = True
        End Try
    End Sub
    Public Sub insLoss()
        If Working_Pro.check_in_up_seq = 0 Then
            date_start_data = Working_Pro.Gdate_now_date
        End If
        Dim pd As String = MainFrm.Label6.Text
        Dim sel_combo As String = 0 'ComboBox1.SelectedIndex
        Dim wi_plan As String = Working_Pro.wi_no.Text
        Dim line_cd As String = MainFrm.Label4.Text
        Dim item_cd As String = Working_Pro.Label3.Text
        Dim seq_no As Integer = Working_Pro.Label22.Text
        Dim shift_prd As String = Working_Pro.Label14.Text
        Dim start_loss As Date = date_start_data 'Date.Parse(TimeOfDay.ToString("H:mm:ss"))
        Dim end_loss As Date = Date.Parse(TimeOfDay.ToString("H:mm:ss")) ' back night result 12:00:00 AM
        Dim date1 As Date = Date.Parse(TimeOfDay.ToString("H:mm:ss"))
        Dim date2 As Date = Date.Parse(TimeOfDay.ToString("H:mm:ss"))
        Dim total_loss As Integer = DateDiff(DateInterval.Minute, date1, date2)
        Dim loss_type As String = "0"  '0:Normally,1:Manual
        Dim op_id As String = "0"
        Dim statusManualE1 As Integer = 0
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                transfer_flg = "1"
                If MainFrm.chk_spec_line = "2" Then
                    Dim GenSEQ As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                    Dim Iseq = GenSEQ
                    Dim j As Integer = 0
                    For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                        Iseq += 1
                        Dim special_wi As String = itemPlanData.wi
                        Dim special_item_cd As String = itemPlanData.item_cd
                        Dim special_item_name As String = itemPlanData.item_name
                        Dim localPwis As String = ""
                        Try
                            localPwis = Working_Pro.Spwi_id(j)
                        Catch ex As Exception
                            localPwis = ""
                        End Try
                        Backoffice_model.ins_loss_act(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, start_loss, end_loss, total_loss, loss_type, Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0", localPwis, statusManualE1)
                        Backoffice_model.ins_loss_act_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, start_loss, end_loss, total_loss, loss_type, Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0", localPwis, statusManualE1)
                        j = j + 1
                    Next
                Else
                    Backoffice_model.ins_loss_act(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, start_loss, end_loss, total_loss, loss_type, Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0", Working_Pro.pwi_id, statusManualE1)
                    Backoffice_model.ins_loss_act_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, start_loss, end_loss, total_loss, loss_type, Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0", Working_Pro.pwi_id, statusManualE1)
                End If
            Else
                transfer_flg = "0"
                If MainFrm.chk_spec_line = "2" Then
                    Dim GenSEQ As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                    Dim Iseq = GenSEQ
                    Dim j As Integer = 0
                    For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                        Iseq += 1
                        Dim special_wi As String = itemPlanData.wi
                        Dim special_item_cd As String = itemPlanData.item_cd
                        Dim special_item_name As String = itemPlanData.item_name
                        Dim localPwis As String = ""
                        Try
                            localPwis = Working_Pro.Spwi_id(j)
                        Catch ex As Exception
                            localPwis = ""
                        End Try
                        Backoffice_model.ins_loss_act_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, start_loss, end_loss, total_loss, loss_type, Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0", localPwis, statusManualE1)
                        j = j + 1
                    Next
                Else
                    Backoffice_model.ins_loss_act_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, start_loss, end_loss, total_loss, loss_type, Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0", Working_Pro.pwi_id, statusManualE1)
                End If
            End If
        Catch ex As Exception
            transfer_flg = "0"
            If MainFrm.chk_spec_line = "2" Then
                Dim GenSEQ As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                Dim Iseq = GenSEQ
                Dim j As Integer = 0
                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                    Iseq += 1
                    Dim localPwis As String = ""
                    Try
                        localPwis = Working_Pro.Spwi_id(j)
                    Catch ex1 As Exception
                        localPwis = ""
                    End Try
                    Dim special_wi As String = itemPlanData.wi
                    Dim special_item_cd As String = itemPlanData.item_cd
                    Dim special_item_name As String = itemPlanData.item_name
                    Backoffice_model.ins_loss_act_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, start_loss, end_loss, total_loss, loss_type, Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0", localPwis, statusManualE1)
                    j = j + 1
                Next
            Else
                Backoffice_model.ins_loss_act_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, start_loss, end_loss, total_loss, loss_type, Backoffice_model.IDLossCodeAuto, op_id, transfer_flg, "0", Working_Pro.pwi_id, statusManualE1)
            End If
        End Try
    End Sub
    Private Sub StopMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Console.WriteLine("date_start_data ===>" & date_start_data) ' ไม่ต้องลบ 
        Console.WriteLine("Working_Pro.slm_flg_qr_prod ===>" & Working_Pro.slm_flg_qr_prod)
        Console.WriteLine("Working_Pro.check_bull ===>" & Working_Pro.check_bull)
        Console.WriteLine("Working_Pro.start_flg ===>" & Working_Pro.start_flg)
        If Working_Pro.check_in_up_seq = 0 Then
            date_start_data = Working_Pro.Gdate_now_date
        Else
            date_start_data = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        End If
        TimerLossBT.Start()
        TimerLossBT.Enabled = True
        lbLineCode.Text = MainFrm.Label4.Text

    End Sub
End Class