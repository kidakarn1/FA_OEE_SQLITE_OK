Public Class Add_success
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        user_manage.Show()
        Add_user.Enabled = True
        'user_manage.Enabled = True
        Add_user.Close()
        Me.Close()
    End Sub
End Class