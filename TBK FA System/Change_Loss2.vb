Imports System.Globalization
Imports System.Web.Script.Serialization
Public Class Change_Loss2
    Public Shared S_index As Integer = 0
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Working_Pro.Enabled = True
        Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                If Backoffice_model.S_chk_spec_line = 0 Then
                    ins_time_loss.Label2.Text = Working_Pro.Label16.Text
                Else ' for K1M025
                    ' ins_time_loss.Label2.Text = Backoffice_model.date_time_start_master_shift.ToString("HH:mm:ss", CultureInfo.InvariantCulture)
                    Dim dateTimeValue As DateTime = DateTime.Parse(Working_Pro.DateTimeStartofShift.Text)
                    ins_time_loss.Label2.Text = dateTimeValue.ToString("HH:mm:ss")
                End If
                Loss_reg_pass.Label2.Text = MainFrm.Label4.Text
                Dim sel_cd As Integer = ListView2.SelectedIndices(0)
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
                Loss_reg_pass.date_start_data = date_st
                Try
                    If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                        If MainFrm.chk_spec_line = "2" Then
                            Dim j As Integer = 0
                            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                Dim special_wi As String = itemPlanData.wi
                                Backoffice_model.line_status_ins(line_id, date_st, date_end, "1", "0", ListView2.Items(sel_cd).SubItems(0).Text, "0", special_wi)
                                Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", ListView2.Items(sel_cd).SubItems(0).Text, "0", special_wi)
                                j = j + 1
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
                                j = j + 1
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
                            j = j + 1
                        Next
                    Else
                        Backoffice_model.line_status_ins_sqlite(line_id, date_st, date_end, "1", "0", ListView2.Items(sel_cd).SubItems(0).Text, "0", Prd_detail.lb_wi.Text)
                    End If
                End Try
                Loss_reg_pass.loss_cd.Text = ListView2.Items(sel_cd).SubItems(0).Text
                Loss_reg_pass.Label7.Text = ListView2.Items(sel_cd).SubItems(1).Text
                Loss_reg_pass.TextBox1.Text = ListView2.Items(sel_cd).SubItems(2).Text
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    Dim LoadSQL = Backoffice_model.get_loss_op_mst(MainFrm.Label4.Text)
                    If LoadSQL <> "0" Then
                        Dim numm As Integer = 0
                        Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(LoadSQL)
                        For Each item As Object In dict
                            Loss_reg_pass.ComboBox1.Items.Add(item("op_name").ToString())
                        Next
                    Else
                        Loss_reg_pass.ComboBox1.Items.Add("Proc :[NO PROCESS]")
                    End If
                Else
                    load_show.Show()
                End If
                Loss_reg_pass.ComboBox1.Visible = True
                Loss_reg_pass.ComboBox1.SelectedIndex = 0
                Loss_reg_pass.Show()
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            load_show.Show()
        End Try
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

    Private Sub Change_Loss2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ListView2.Items(1).Selected = True
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
