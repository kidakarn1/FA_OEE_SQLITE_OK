Public Class line_manage
    Private Sub menu3_Click(sender As Object, e As EventArgs) Handles menu3.Click
        Me.Enabled = False
        Try
            If My.Computer.Network.Ping("192.168.161.102") Then
                Dim webClient As New System.Net.WebClient
                Dim result As String = webClient.DownloadString("http://192.168.161.102/exp_api3party/Api_sync_newfa/update_line_mst")


                MsgBox("Synchronous completed")
            Else
                MsgBox("Synchronous not completed")
            End If
        Catch ex As Exception

        End Try

        ListView1.Items.Clear()
        Dim LoadSQL = Backoffice_model.get_all_line()
        Dim num As Integer = 1
        While LoadSQL.Read()
            ListView1.View = View.Details
            ListView1.Items.Add(num).SubItems.AddRange(New String() {LoadSQL("line_cd").ToString(), LoadSQL("line_name").ToString()})
            ListBox1.Items.Add(LoadSQL("line_id").ToString())
            num = num + 1
        End While

        Me.Enabled = True
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Adm_manage.Show()
        Me.Close()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count <= 0 Then
            PictureBox4.Visible = False
            PictureBox5.Visible = False
        ElseIf ListView1.SelectedItems.Count > 0 Then
            PictureBox4.Visible = True
            PictureBox5.Visible = True
        End If
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Me.Enabled = False
        Conf_del_line.Show()

    End Sub

    Private Sub line_manage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Dim sel_number As Integer = ListView1.SelectedIndices(0)
        Dim line_id As Integer = ListBox1.Items(sel_number)

        Edit_line_mst.TextBox1.Text = ListView1.Items(sel_number).SubItems(1).Text
        Edit_line_mst.TextBox2.Text = ListView1.Items(sel_number).SubItems(2).Text

        Dim created_emp_cd As String = Label3.Text.Substring(1, 5)

        Dim LoadSQL2 = Backoffice_model.get_all_skill()
        Dim num As Integer = 0
        While LoadSQL2.Read()
            Edit_line_mst.ListView1.View = View.Details
            Edit_line_mst.ListView1.Items.Add("", HorizontalAlignment.Center).SubItems.AddRange(New String() {LoadSQL2("sk_name").ToString()})
            Edit_line_mst.ListBox1.Items.Add(LoadSQL2("sk_id").ToString())

        End While

        Dim LoadSQLussk = Backoffice_model.get_line_skill(ListView1.Items(sel_number).SubItems(1).Text)
        While LoadSQLussk.Read()
            Edit_line_mst.ListView1.Items(LoadSQLussk("sk_id") - 1).checked = True
            'Edit_line_mst.lb_line_id.Text = LoadSQLussk("line_id").ToString()
        End While
        Edit_line_mst.lb_line_id.Text = line_id

        Edit_line_mst.Label2.Text = Label2.Text
        Edit_line_mst.Label3.Text = Label3.Text

        Edit_line_mst.Show()
        Me.Hide()

    End Sub
End Class