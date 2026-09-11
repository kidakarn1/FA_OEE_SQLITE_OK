Public Class Inf_manage
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Adm_manage.Show()
        Me.Close()

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        inf_save_conf.Show()
        Me.Enabled = False
    End Sub
End Class