Imports System.Globalization

Public Class Loss_reg_pass
    Public date_start_data As Date
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ins_time_loss.Button10.Enabled = False
        Me.Enabled = False
        ins_time_loss.TextBox1.Text = ""
        ins_time_loss.TextBox2.Text = ""
        ins_time_loss.Show()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Change_Loss2.ListView2.Items(2).Selected = True
        Change_Loss2.ListView2.View = View.Details
        'Chang_Loss.ListView2.Scrollable = Size()
        'List_Emp.ListBox2.Items.Add(Trim(TextBox2.Text))
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim LoadSQL = Backoffice_model.get_loss_mst()
                While LoadSQL.Read()
                    Change_Loss2.ListView2.ForeColor = Color.Blue
                    Change_Loss2.ListView2.Items.Add(LoadSQL("id_mst").ToString()).SubItems.AddRange(New String() {LoadSQL("loss_cd").ToString(), LoadSQL("description_th").ToString()})
                    Change_Loss2.ListBox1.Items.Add(LoadSQL("loss_type").ToString())
                End While
                Working_Pro.ResetRed()
                Change_Loss2.Show()
                Me.Hide()
            Else
                load_show.Show()
            End If
        Catch ex As Exception
            load_show.Show()
        End Try
    End Sub
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim line_id As String = MainFrm.line_id.Text
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
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
                            Dim j As Integer = 0
                            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                Dim special_wi As String = itemPlanData.wi
                                Dim special_item_cd As String = itemPlanData.item_cd
                                Dim special_item_name As String = itemPlanData.item_name
                                Backoffice_model.line_status_ins(line_id, date_st, date_end, "1", "0", "24", "0", special_wi)
                                Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", "24", "0", special_wi)
                                j = j + 1
                            Next
                        Else
                            Backoffice_model.line_status_ins(line_id, date_st, date_end, "1", "0", "24", "0", Prd_detail.lb_wi.Text)
                            Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", "24", "0", Prd_detail.lb_wi.Text)
                        End If
                    Else
                        If MainFrm.chk_spec_line = "2" Then
                            Dim j As Integer = 0
                            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                Dim special_wi As String = itemPlanData.wi
                                Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", "24", "0", special_wi)
                                j = j + 1
                            Next
                        Else
                            Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", "24", "0", Prd_detail.lb_wi.Text)
                        End If
                    End If
                Catch ex As Exception
                    If MainFrm.chk_spec_line = "2" Then
                        Dim j As Integer = 0
                        For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                            Dim special_wi As String = itemPlanData.wi
                            Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", "24", "0", special_wi)
                            j = j + 1
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
                Dim date_cerrunt As Date = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
                Dim time_cerrunt As Date = DateTime.Now.ToString("H:m:s")
                If Label8.Text >= time_cerrunt Then
                    start_loss = date_cerrunt.AddDays(-1)
                    Dim s As String = start_loss.ToString().Substring(0, 9)
                    start_loss = s & Label8.Text
                End If
                If Label9.Text > time_cerrunt Then
                    end_loss = date_cerrunt.AddDays(-1)
                    Dim s As String = end_loss.ToString().Substring(0, 9)
                    end_loss = s & Label9.Text
                End If
                Dim total_loss As Integer = DateDiff(DateInterval.Minute, start_loss, end_loss)
                If total_loss < 0 Then
                    total_loss = Math.Abs(CDbl(Val(total_loss)))
                End If
                Dim loss_type As String = "1"  '0:Normally,1:Manual
                Dim loss_cd_id As String = loss_cd.Text
                Dim op_id As String
                op_id = 0 'ComboBox1.Text
                Dim pd As String = MainFrm.Label6.Text
                Dim transfer_flg As String = "0"
                Dim statusManualE1 As Integer = 0
                Try
                    If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                        transfer_flg = "1"
                        ' Dim onlyDate As String = Working_Pro.DatTimeCodeXEndUpdate.Substring(0, 10)     ' "2025-05-05"
                        ' Dim onlyTime As String = Working_Pro.DatTimeCodeXEndUpdate.Substring(11, 8)     ' "14:48:27"
                        ' Working_Pro.insLossClickStart_Loss_X(onlyDate, onlyTime)
                        Await Working_Pro.insLossClickStart_Loss_E1(start_loss.ToString("yyyy/MM/dd"), start_loss.ToString("HH:mm:ss"), "1")
                        Dim rsClickSTart = Backoffice_model.date_time_click_start.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        Working_Pro.insLossClickStart_Loss_X_adjust_loss(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture), rsClickSTart, Label8.Text)
                        If MainFrm.chk_spec_line = "2" Then
                            Dim j As Integer = 0
                            Dim GenSEQ As Integer = seq_no - 5
                            Dim Iseq = GenSEQ
                            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                Iseq += 1
                                Dim special_wi As String = itemPlanData.wi
                                Dim special_item_cd As String = itemPlanData.item_cd
                                Backoffice_model.ins_loss_act(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "1", Working_Pro.Spwi_id(j), statusManualE1)
                                Backoffice_model.ins_loss_act_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "1", Working_Pro.Spwi_id(j), statusManualE1)
                                j = j + 1
                            Next
                        Else
                            Console.WriteLine("start_loss ===>" & start_loss)
                            Console.WriteLine("end_loss ===>" & end_loss)
                            Console.WriteLine("total_loss ===>" & total_loss)
                            Backoffice_model.ins_loss_act(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "1", Working_Pro.pwi_id, statusManualE1)
                            Backoffice_model.ins_loss_act_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "1", Working_Pro.pwi_id, statusManualE1)
                        End If
                    Else
                        transfer_flg = "0"
                        If MainFrm.chk_spec_line = "2" Then
                            Dim j As Integer = 0
                            Dim GenSEQ As Integer = seq_no - 5
                            Dim Iseq = GenSEQ
                            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                Iseq += 1
                                Dim special_wi As String = itemPlanData.wi
                                Dim special_item_cd As String = itemPlanData.item_cd
                                Backoffice_model.ins_loss_act_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "1", Working_Pro.Spwi_id(j), statusManualE1)
                                j = j + 1
                            Next
                        Else
                            Backoffice_model.ins_loss_act_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "1", Working_Pro.pwi_id, statusManualE1)
                        End If
                    End If
                Catch ex As Exception
                    transfer_flg = "0"
                    If MainFrm.chk_spec_line = "2" Then
                        Dim j As Integer = 0
                        Dim GenSEQ As Integer = seq_no - 5
                        Dim Iseq = GenSEQ
                        For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                            Iseq += 1
                            Dim special_wi As String = itemPlanData.wi
                            Dim special_item_cd As String = itemPlanData.item_cd
                            Backoffice_model.ins_loss_act_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "1", Working_Pro.Spwi_id(j), statusManualE1)
                            j = j + 1
                        Next
                    Else
                        Backoffice_model.ins_loss_act_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "1", Working_Pro.pwi_id, statusManualE1)
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
    Private Sub Loss_reg_pass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Working_Pro.TowerLamp(6, 9800)
        Label2.Text = MainFrm.Label4.Text
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        '  Shell("C:\Program Files (x86)\Default Company Name\Maintenance\Maintenance.exe")
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs)
        Shell("C:\Program Files (x86)\Default Company Name\Maintenance\Maintenance.exe")
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub
End Class
