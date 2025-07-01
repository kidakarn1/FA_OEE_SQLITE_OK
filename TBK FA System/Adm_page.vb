Public Class Adm_page
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        MainFrm.Show()
        Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Adm_login.TextBox1.Select()
        Me.Enabled = False
        Adm_login.Show()
    End Sub
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs)
        Me.Enabled = False
        ' Scan_reprint.TextBox1.Select()
        Scan_reprint.Show()
    End Sub
    Private Sub Label10_Click(sender As Object, e As EventArgs)
    End Sub
    Private Sub Adm_page_Load(sender As Object, e As EventArgs)
    End Sub
End Class