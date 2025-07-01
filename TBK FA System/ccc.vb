Public Class Sel_prd_setup
    Dim x As ListViewItem
    Private Sub Label1_Click(sender As Object, e As EventArgs)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Working_Pro.Enabled = True
        Me.Hide()
    End Sub
    Public Sub loadDataLossCrr()
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Chang_Loss.ListView2.View = View.Details
                'Chang_Loss.ListView2.Scrollable = Size()
                'List_Emp.ListBox2.Items.Add(Trim(TextBox2.Text))
                Loss_reg.Label2.Text = MainFrm.Label4.Text
                Dim LoadSQL = Backoffice_model.get_loss_mst()
                Dim i As Integer = 1
                Dim checkRs As Integer = 0
                While LoadSQL.Read()
                    Chang_Loss.ListView2.ForeColor = Color.Blue
                    Chang_Loss.ListView2.Items.Add(LoadSQL("id_mst").ToString()).SubItems.AddRange(New String() {LoadSQL("loss_cd").ToString(), LoadSQL("description_th").ToString()})
                    Chang_Loss.ListBox1.Items.Add(LoadSQL("loss_type").ToString())
                    i += 1
                    checkRs = 1
                End While
                LoadSQL.close
                If checkRs = 1 Then
                    Chang_Loss.Show()
                    Me.Hide()
                Else
                    MsgBox("Please Check Master Loss.")
                End If
            End If
        Catch ex As Exception
            load_show.Show()
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        loadDataLossCrr()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Change_Loss2.ListView2.View = View.Details
                Dim checkRs As Integer = 0
                Loss_reg_pass.Label2.Text = MainFrm.Label4.Text
                Dim LoadSQL = Backoffice_model.get_loss_mst()
                While LoadSQL.Read()
                    Change_Loss2.ListView2.ForeColor = Color.Blue
                    Change_Loss2.ListView2.Items.Add(LoadSQL("id_mst").ToString()).SubItems.AddRange(New String() {LoadSQL("loss_cd").ToString(), LoadSQL("description_th").ToString()})
                    Change_Loss2.ListBox1.Items.Add(LoadSQL("loss_type").ToString())
                    checkRs = 1
                End While
                If checkRs = 1 Then
                    LoadSQL.close()
                    Change_Loss2.Show()
                    Me.Hide()
                Else
                    MsgBox("Please Check Master Loss.")
                End If
            Else
                load_show.Show()
            End If
        Catch ex As Exception
            load_show.Show()
        End Try
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        closeLotsummary.statusPage.Text = "MAN"
        closeLotsummary.Show()
        Me.Enabled = False
        ' List_Emp.lb_link.Text = "working"
        ' List_Emp.Show()
        'Me.Hide()
    End Sub
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Working_Pro.Enabled = True
        Me.Hide()
    End Sub
    Public Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim check_format_tag As String = Backoffice_model.B_check_format_tag()
                Dim LoadSQL
                Dim num As Integer = 0
                If check_format_tag = "1" Then ' for tag_type = '2' and tag_issue_flg = '2'  OR K1M183
                    Dim LoadSQL_BATCH = Backoffice_model.get_tag_reprint_batch(Working_Pro.wi_no.Text) ' by batch for k1m083
                    While LoadSQL_BATCH.Read()
                        num = num + 1
                        tag_reprint_new.ListView1.View = View.Details
                        tag_reprint_new.ListView1.Items.Add(num).SubItems.AddRange(New String() {"(BATCH)" & LoadSQL_BATCH("updated_date").ToString(), LoadSQL_BATCH("tag_qr_detail").ToString().Substring(95, 3), LoadSQL_BATCH("shift").ToString(), CDbl(Val(LoadSQL_BATCH("tag_qr_detail").ToString().Substring(100, 3)))})
                        tag_reprint_new.ListBox1.Items.Add(LoadSQL_BATCH("tag_qr_detail").ToString())
                        tag_reprint_new.ListBox2.Items.Add(LoadSQL_BATCH("shift").ToString())
                        tag_reprint_new.ListBox2.Items.Add(LoadSQL_BATCH("tag_qr_detail").ToString().Substring(100, 3))
                    End While
                    LoadSQL_BATCH.close()
                    LoadSQL = Backoffice_model.get_tag_reprint_spaceial(Working_Pro.wi_no.Text) ' by box for k1m083 
                Else
                    LoadSQL = Backoffice_model.get_tag_reprint_detail(Working_Pro.wi_no.Text)
                End If
                While LoadSQL.Read()
                    num = num + 1
                    'tag_reprint_new.ListView1.View = View.Details
                    Dim date_act As String = LoadSQL("qr_detail").Substring(44, 8)
                    Dim actual_date = (Trim(date_act.Substring(0, 4))) & "-" & (Trim(date_act.Substring(4, 2))) & "-" & (Trim(date_act.Substring(6)))
                    tag_reprint_new.ListView1.Items.Add(num).SubItems.AddRange(New String() {actual_date, LoadSQL("seq_no").ToString(), LoadSQL("shift").ToString(), LoadSQL("box_no").ToString()})
                    tag_reprint_new.ListBox1.Items.Add(LoadSQL("qr_detail").ToString())
                    tag_reprint_new.ListBox2.Items.Add(LoadSQL("shift").ToString())
                    tag_reprint_new.ListBox2.Items.Add(LoadSQL("box_no").ToString())
                End While
                LoadSQL.close()
                tag_reprint_new.Show()
                Me.Enabled = False
                Me.Hide()
            Else
                load_show.Show()
            End If
        Catch ex As Exception
            'MsgBox("error==>" & ex.Message)
            load_show.Show()
        End Try
    End Sub
    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Rm_scan.Panel_scan_picking.Visible = True
        Rm_scan.scan_item_cd.Select()
        Rm_scan.scan_item_cd.Focus()
        Rm_scan.ShowDialog()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub Sel_prd_setup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Text = TimeOfDay.ToString("H:mm:ss")
        Label4.Text = DateTime.Now.ToString("D")
    End Sub

    Private Sub Label1_Click_1(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
