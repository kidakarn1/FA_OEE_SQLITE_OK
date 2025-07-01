Imports System.Web.Script.Serialization

Public Class Chang_Loss
    Public Shared S_index As Integer = 0
    Public G_loss_cd As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Time_Loss.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Working_Pro.Enabled = True
        Me.Close()
    End Sub
    Public Sub btnNextLossCrr()
        Dim sel_cd As Integer = ListView2.SelectedIndices(0)
        Dim line_id As String = MainFrm.line_id.Text
        Dim status_loss As Integer = 0
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                check_loss_sys()
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
                                Backoffice_model.line_status_ins(line_id, date_st, date_end, "1", "0", ListView2.Items(sel_cd).SubItems(0).Text, "0", special_wi)
                                Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", ListView2.Items(sel_cd).SubItems(0).Text, "0", special_wi)
                            Next
                        Else
                            Backoffice_model.line_status_ins(line_id, date_st, date_end, "1", "0", ListView2.Items(sel_cd).SubItems(0).Text, "0", Prd_detail.lb_wi.Text)
                            Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", ListView2.Items(sel_cd).SubItems(0).Text, "0", Prd_detail.lb_wi.Text)
                        End If
                    Else
                        If MainFrm.chk_spec_line = "2" Then
                            Dim j As Integer = 0
                            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                Dim special_wi As String = itemPlanData.wi
                                Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", ListView2.Items(sel_cd).SubItems(0).Text, "0", special_wi)
                            Next
                        Else
                            Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", ListView2.Items(sel_cd).SubItems(0).Text, "0", Prd_detail.lb_wi.Text)
                        End If
                    End If
                Catch ex As Exception
                    If MainFrm.chk_spec_line = "2" Then
                        Dim j As Integer = 0
                        For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                            Dim special_wi As String = itemPlanData.wi
                            Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", ListView2.Items(sel_cd).SubItems(0).Text, "0", special_wi)
                        Next
                    Else
                        Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", ListView2.Items(sel_cd).SubItems(0).Text, "0", Prd_detail.lb_wi.Text)
                    End If
                End Try
                Loss_reg.loss_cd.Text = ListView2.Items(sel_cd).SubItems(0).Text
                G_loss_cd = ListView2.Items(sel_cd).SubItems(0).Text
                Loss_reg.Label7.Text = ListView2.Items(sel_cd).SubItems(1).Text
                Loss_reg.TextBox1.Text = ListView2.Items(sel_cd).SubItems(2).Text
                Loss_reg.Label2.Text = MainFrm.Label4.Text
                If ListBox1.Items(sel_cd) = 1 Then
                    Dim LoadSQL
                    Try
                        If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                            LoadSQL = Backoffice_model.get_loss_op_mst(MainFrm.Label4.Text)
                            If LoadSQL <> "0" Then
                                Dim numm As Integer = 0
                                Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(LoadSQL)
                                For Each item As Object In dict
                                    Loss_reg.ComboBox1.Items.Add(item("op_name").ToString())
                                Next
                            Else
                                Loss_reg.ComboBox1.Items.Add("Proc :[NO PROCESS]")
                            End If

                        Else
                            load_show.Show()
                        End If
                    Catch ex As Exception
                        status_loss = 1
                        MsgBox(ex.Message)
                        load_show.Show()
                    End Try
                    Loss_reg.ComboBox1.Visible = True
                    Loss_reg.ComboBox1.SelectedIndex = 0
                    Loss_reg.btnMaintenance.Visible = True
                End If
                Loss_reg.Label8.Text = TimeOfDay.ToString("H:mm:ss")
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
                Dim total_loss As Integer = DateDiff(DateInterval.Minute, date1, date2)
                Dim loss_type As String = "0"  '0:Normally,1:Manual
                Dim loss_cd_id As String = G_loss_cd
                Dim op_id As String
                If sel_combo < 0 Then
                    op_id = "0"
                Else
                    op_id = ListBox1.Items(sel_combo).ToString()
                End If
                Loss_reg.date_start_data = Date.Parse(TimeOfDay.ToString("H:mm:ss"))
                Try
                    If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                        transfer_flg = "1"
                        If MainFrm.chk_spec_line = "2" Then
                            Dim j As Integer = 0
                            Dim GenSEQ As Integer = seq_no - 5
                            Dim Iseq = GenSEQ
                            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                Iseq += 1
                                Dim special_wi As String = itemPlanData.wi
                                Dim special_item_cd As String = itemPlanData.item_cd
                                Backoffice_model.ins_loss_act(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "0", Working_Pro.Spwi_id(j))
                                Backoffice_model.ins_loss_act_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "0", Working_Pro.Spwi_id(j))
                                j += 1
                            Next
                        Else
                            Backoffice_model.ins_loss_act(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "0", Working_Pro.pwi_id)
                            Backoffice_model.ins_loss_act_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "0", Working_Pro.pwi_id)
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
                                Backoffice_model.ins_loss_act_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "0", Working_Pro.Spwi_id(j))
                                j += 1
                            Next
                        Else
                            Backoffice_model.ins_loss_act_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "0", Working_Pro.pwi_id)
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
                            Backoffice_model.ins_loss_act_sqlite(pd, line_cd, special_wi, special_item_cd, Iseq, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "0", Working_Pro.Spwi_id(j))
                            j += 1
                        Next
                    Else
                        Backoffice_model.ins_loss_act_sqlite(pd, line_cd, wi_plan, item_cd, seq_no, shift_prd, start_loss, end_loss, total_loss, loss_type, loss_cd_id, op_id, transfer_flg, "0", Working_Pro.pwi_id)
                    End If
                End Try
                If status_loss = 0 Then
                    Loss_reg.Show()
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            load_show.Show()
        End Try
    End Sub
    Public Shared Sub check_loss_sys()
        Working_Pro.insLossClickStart_Loss_E1(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"))
        Working_Pro.insLossClickStart_Loss_X(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"))
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        btnNextLossCrr()
    End Sub
    Private Sub ListView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView2.SelectedIndexChanged

        If ListView2.SelectedItems.Count <= 0 Then
            Button1.Enabled = False
            ListView2.ForeColor = Color.White
        ElseIf ListView2.SelectedItems.Count > 0 Then
            Button1.Enabled = True
            ListView2.ForeColor = Color.White
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Chang_Loss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ListView2.Items(0).Selected = True
        Catch ex As Exception
            MsgBox("Please Check Loss Master.")
        End Try
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Text = TimeOfDay.ToString("H:mm:ss")
        Label4.Text = DateTime.Now.ToString("D")
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        BTNUP1()
    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        BTNDOWN1()
    End Sub
    Public Sub BTNUP1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((ListView2.Items.Count - 1))) Then
            S_index = CDbl(Val((ListView2.Items.Count - 1)))
        End If
        Try
            ListView2.Items(S_index).Selected = False
            S_index -= 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf lvDefectact.Items.Count > S_index Then
                'S_index = CDbl(Val((lvDefectact.Items.Count - 1)))
            End If
            ListView2.Items(S_index).Selected = True
            ListView2.Items(S_index).EnsureVisible()
            ListView2.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BTNDOWN1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((ListView2.Items.Count - 1))) Then
            S_index = CDbl(Val((ListView2.Items.Count - 1)))
        End If
        Try
            ListView2.Items(S_index).Selected = False
            S_index += 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf S_index > lvDefectact.Items.Count Then
                '  S_index = CDbl(Val((lvDefectact.Items.Count - 1)))
            End If
            ListView2.Items(S_index).Selected = True
            ListView2.Items(S_index).EnsureVisible()
            ListView2.Select()
        Catch ex As Exception
        End Try
    End Sub
End Class
