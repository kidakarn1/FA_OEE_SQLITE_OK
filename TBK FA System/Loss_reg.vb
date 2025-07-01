Imports System.IO
Imports System.IO.Pipes
Public Class Loss_reg
    Public date_start_data As Date
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Try
                    Chang_Loss.ListView2.View = View.Details
                    'Chang_Loss.ListView2.Scrollable = Size()
                    'List_Emp.ListBox2.Items.Add(Trim(TextBox2.Text))
                    Dim sel_combo As String = ComboBox1.SelectedIndex
                    Dim line_cd As String = Label2.Text
                    Dim item_cd As String = Working_Pro.Label3.Text
                    Dim seq_no As Integer = Working_Pro.Label22.Text
                    Dim shift_prd As String = Working_Pro.Label14.Text
                    Dim start_loss As Date = Date.Parse(Label8.Text)
                    Dim end_loss As Date = Date.Parse(Label9.Text)
                    Dim date1 As Date = Date.Parse(Label8.Text)
                    Dim date2 As Date = Date.Parse(Label9.Text)
                    Dim total_loss As Integer = DateDiff(DateInterval.Minute, date1, date2)
                    Dim loss_type As String = "0"  '0:Normally,1:Manual
                    Dim loss_cd_id As String = loss_cd.Text
                    Dim op_id As String = ComboBox1.Text
                    'Dim op_id As String
                    'If sel_combo < 0 Then
                    '                 op_id = "0"
                    '                    Else'
                    'op_id = 'ListBox1.Items(sel_combo).ToString()
                    'End If
                    Dim pd As String = MainFrm.Label6.Text
                    Dim transfer_flg As String = "0"
                    Try
                        If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                            transfer_flg = 1
                            If MainFrm.chk_spec_line = "2" Then
                                Dim GenSEQ As Integer = seq_no - 5
                                Dim Iseq = GenSEQ
                                Dim j As Integer = 0
                                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                    Iseq += 1
                                    Dim special_wi As String = itemPlanData.wi
                                    Dim special_item_cd As String = itemPlanData.item_cd
                                    Dim special_item_name As String = itemPlanData.item_name
                                    Backoffice_model.Update_flg_loss(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, date_start_data, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "2")
                                    Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, date_start_data, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "2")
                                    Backoffice_model.alert_loss(special_wi, "2", pd, loss_cd_id)
                                Next
                            Else
                                Backoffice_model.Update_flg_loss(pd, line_cd, Working_Pro.wi_no.Text, item_cd, seq_no, shift_prd, date_start_data, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "2")
                                Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, Working_Pro.wi_no.Text, item_cd, seq_no, shift_prd, date_start_data, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "2")
                                Backoffice_model.alert_loss(wi_plan, "2", pd, loss_cd_id)
                            End If
                        Else
                            transfer_flg = 0
                            If MainFrm.chk_spec_line = "2" Then
                                Dim GenSEQ As Integer = seq_no - 5
                                Dim Iseq = GenSEQ
                                Dim j As Integer = 0
                                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                    Iseq += 1
                                    Dim special_wi As String = itemPlanData.wi
                                    Dim special_item_cd As String = itemPlanData.item_cd
                                    Dim special_item_name As String = itemPlanData.item_name
                                    Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, date_start_data, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "2")
                                Next
                            Else
                                Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, Working_Pro.wi_no.Text, item_cd, seq_no, shift_prd, date_start_data, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "2")
                            End If
                        End If
                    Catch ex As Exception
                        transfer_flg = 0
                        If MainFrm.chk_spec_line = "2" Then
                            Dim GenSEQ As Integer = seq_no - 5
                            Dim Iseq = GenSEQ
                            Dim j As Integer = 0
                            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                Iseq += 1
                                Dim special_wi As String = itemPlanData.wi
                                Dim special_item_cd As String = itemPlanData.item_cd
                                Dim special_item_name As String = itemPlanData.item_name
                                Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, date_start_data, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "2")
                            Next
                        Else
                            Backoffice_model.Update_flg_loss_sqlite(pd, line_cd, Working_Pro.wi_no.Text, item_cd, seq_no, shift_prd, date_start_data, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "2")
                        End If
                    End Try
                    Dim LoadSQL = Backoffice_model.get_loss_mst()
                    While LoadSQL.Read()
                        If LoadSQL("loss_type").ToString() = "1" Then
                            btnMaintenance.Visible = True
                        Else
                            btnMaintenance.Visible = False
                        End If
                        Chang_Loss.ListView2.ForeColor = Color.Blue
                        Chang_Loss.ListView2.Items.Add(LoadSQL("id_mst").ToString()).SubItems.AddRange(New String() {LoadSQL("loss_cd").ToString(), LoadSQL("description_th").ToString()})
                        Chang_Loss.ListBox1.Items.Add(LoadSQL("loss_type").ToString())
                    End While
                    Working_Pro.ResetRed()
                    Chang_Loss.Show()
                    Me.Close()
                Catch ex As Exception
                    load_show.Show()
                End Try
            End If
        Catch ex As Exception
            load_show.Show()
        End Try
    End Sub
    Public Sub Submit_loss()
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim line_id As String = MainFrm.line_id.Text
                Try
                    If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                        Backoffice_model.line_status_upd(line_id)
                        Backoffice_model.line_status_upd_sqlite(line_id)
                    Else
                        Backoffice_model.line_status_upd_sqlite(line_id)
                    End If
                Catch ex As Exception
                    Backoffice_model.line_status_upd_sqlite(line_id)
                End Try
                Dim date_st As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
                Dim date_end As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
                Try
                    If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                        If MainFrm.chk_spec_line = "2" Then
                            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                Dim special_wi As String = itemPlanData.wi
                                Backoffice_model.line_status_ins(line_id, date_st, date_end, "1", "0", "24", "0", special_wi)
                                Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", "24", "0", special_wi)
                            Next
                        Else
                            Backoffice_model.line_status_ins(line_id, date_st, date_end, "1", "0", "24", "0", Prd_detail.lb_wi.Text)
                            Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", "24", "0", Prd_detail.lb_wi.Text)
                        End If
                    Else
                        If MainFrm.chk_spec_line = "2" Then
                            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                Dim special_wi As String = itemPlanData.wi
                                Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", "24", "0", special_wi)
                            Next
                        Else
                            Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", "24", "0", Prd_detail.lb_wi.Text)
                        End If
                    End If
                Catch ex As Exception
                    If MainFrm.chk_spec_line = "2" Then
                        For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                            Dim special_wi As String = itemPlanData.wi
                            Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", "24", "0", special_wi)
                        Next
                    Else
                        Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", "24", "0", Prd_detail.lb_wi.Text)
                    End If
                End Try
                Dim sel_combo As String = ComboBox1.SelectedIndex
                Dim wi_plan As String = Working_Pro.wi_no.Text
                Dim line_cd As String = Label2.Text
                Dim item_cd As String = Working_Pro.Label3.Text
                Dim seq_no As Integer = Working_Pro.Label22.Text
                Dim shift_prd As String = Working_Pro.Label14.Text
                Dim start_loss As Date = Date.Parse(Label8.Text)
                Dim end_loss As Date = Date.Parse(Label9.Text)
                Dim date1 As Date = Date.Parse(Label8.Text)
                Dim date2 As Date = Date.Parse(Label9.Text)
                Dim date11 As Date = Date.Parse(date_st)
                Dim date22 As Date = Date.Parse(date_end)
                'Dim total_loss As Integer = DateDiff(DateInterval.Minute, date1, date2)
                Dim total_loss As Integer = DateDiff(DateInterval.Minute, date1, date2)
                Dim loss_type As String = "0"  '0:Normally,1:Manual
                Dim loss_cd_id As String = loss_cd.Text
                Dim op_id As String
                'If sel_combo < 0 Then
                '           op_id = "0"
                '			Else
                ' op_id = ListBox1.Items(sel_combo).ToString()
                '		End If
                op_id = ComboBox1.Text
                Dim pd As String = MainFrm.Label6.Text
                Dim transfer_flg As String = "0"
                Try
                    If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                        transfer_flg = "1"
                        If MainFrm.chk_spec_line = "2" Then
                            Dim GenSEQ As Integer = seq_no - 5
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
                        ' maintenance.updMaintenanceSqlite()
                        ' maintenance.UpdateMaintenance(line_cd, ComboBox1.Text, shift_prd, test_time_loss_time.Text, DateTime.Now.ToString("yyyy/MM/dd H:m:s"), date_start_data)
                    Else
                        transfer_flg = "0"
                        If MainFrm.chk_spec_line = "2" Then
                            Dim GenSEQ As Integer = seq_no - 5
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
                        Dim GenSEQ As Integer = seq_no - 5
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
                Working_Pro.ResetRed()
                Working_Pro.Enabled = True
                Working_Pro.setlvA(Working_Pro.Label24.Text, Working_Pro.Label18.Text, Working_Pro.Label14.Text, DateTime.Now.ToString("yyyy-MM-dd"), Prd_detail.Label12.Text.Substring(3, 5), Working_Pro.gobal_stTimeModel, MainFrm.chk_spec_line)
                Dim A = Working_Pro.cal_progressbarA(Working_Pro.Label24.Text, Prd_detail.Label12.Text.Substring(3, 5), Prd_detail.Label12.Text.Substring(11, 5))
                Me.Close()
            Else
                load_show.Show()
            End If
        Catch ex As Exception
            load_show.Show()
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim BreakTime = Backoffice_model.GetTimeAutoBreakTime(MainFrm.Label4.Text, Working_Pro.Label14.Text) ' for set data 
        If MainFrm.chk_spec_line = "2" Then
            Dim GenSEQ As Integer = CDbl(Val(Working_Pro.Label22.Text)) - 5
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
        Working_Pro.Enabled = True
        Working_Pro.ResetRed()
        Submit_loss()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim date_end_data As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim date_end As Date = date_end_data
        Dim total_loss As Integer = DateDiff(DateInterval.Minute, date_start_data, date_end)
        test_time_loss_time.Text = total_loss
        If total_loss < 0 Then
            Minutes_total = Math.Abs(CDbl(Val(test_time_loss_time.Text)))
            test_time_loss_time.Text = Minutes_total
        End If
        Label9.Text = TimeOfDay.ToString("H:mm:ss")
        'Label1.Text = DateTime.Now.ToString("yyyy/MM/dd")
    End Sub
    Private Sub Loss_reg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        GetDefectMenuMaintenance()
        date_time_commit_data.Visible = False
        test_time_loss_time.Visible = False
        Label2.Text = MainFrm.Label4.Text
        Working_Pro.TowerLamp(6, 9800)
    End Sub
    Public Sub GetDefectMenuMaintenance()
        statusDefect = Backoffice_model.GetDefectMenuMaintenance(MainFrm.Label4.Text)
        If statusDefect = "0" Then
            btnMaintenance.Enabled = False
        Else
            btnMaintenance.Enabled = True
        End If
    End Sub
    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub Label5_Click(sender As Object, e As EventArgs)
    End Sub
    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub
    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub
    Private Async Sub Button4_Click(sender As Object, e As EventArgs) Handles btnMaintenance.Click
        LoadMN()
    End Sub
    Public Async Function LoadMN() As Task
        If MainFrm.chk_spec_line = "2" Then
            Dim j As Integer = 0
            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                Dim special_wi As String = itemPlanData.wi
                Dim special_item_cd As String = itemPlanData.item_cd
                Dim special_item_name As String = itemPlanData.item_name
                Dim special_model As String = itemPlanData.MODEL
                maintenance.insMaintenance(MainFrm.Label4.Text, ComboBox1.Text, Working_Pro.Label14.Text, date_start_data, special_wi, special_model)
            Next
        Else
            maintenance.insMaintenance(MainFrm.Label4.Text, ComboBox1.Text, Working_Pro.Label14.Text, date_start_data, Working_Pro.wi_no.Text, Working_Pro.lb_model.Text)
        End If
        Try
            Shell("C:\TicketMaintenances\TicketMaintenances\MaintenanceSystem.exe")
            Await AnotherAsyncMethod()
        Catch ex As Exception
            '  MsgBox("Maintenance System Open.")
        End Try
    End Function
    Public Sub Wait_DATA()
        'Console.WriteLine("OK Connect MS SYS")
    End Sub
    Public Async Function AnotherAsyncMethod() As Task
        ' Call the Main() method asynchronously
        Await ConnectMaintenanceSys()
    End Function

    Public Async Function ConnectMaintenanceSys() As Task
        ' สร้าง PipeServer
        Dim pipeServer As New NamedPipeServerStream("mypipe", PipeDirection.In)
        'Console.WriteLine("Waiting for connection...")
        ' รอการเชื่อมต่อจาก Client
        Await pipeServer.WaitForConnectionAsync()
        'Console.WriteLine("Client connected.")
        ' อ่านคำสั่งจาก Client
        Dim reader As New StreamReader(pipeServer)
        Dim command As String = Await reader.ReadLineAsync()
        'Console.WriteLine("Received command from client: " & command)
        ' ตรวจสอบคำสั่ง
        If command = "click button" Then
            ' ทำงานเมื่อได้รับคำสั่ง "click button"
            ' เช่น คลิกปุ่ม
            Dim BreakTime = Backoffice_model.GetTimeAutoBreakTime(MainFrm.Label4.Text, Working_Pro.Label14.Text) ' for set data 
            If MainFrm.chk_spec_line = "2" Then
                Dim GenSEQ As Integer = CDbl(Val(Working_Pro.Label22.Text)) - 5
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
            Working_Pro.Enabled = True
            Submit_loss()
            reader.Close()
            pipeServer.Close()
        ElseIf command = "Wait_DATA" Then
            'Console.WriteLine("Wait_DATA")
        End If
        Try
            reader.Close()
            'pipeServer.Close()
        Catch ex As Exception

        End Try
        ' ปิดการเชื่อมต่อ
        'Console.WriteLine("close Connection main")
    End Function
End Class
