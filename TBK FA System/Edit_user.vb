Public Class Edit_user
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        user_manage.Show()
        Me.Close()

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click


        Me.Enabled = False


        Conf_edit_user.Show()
        'Me.Close()


    End Sub
End Class