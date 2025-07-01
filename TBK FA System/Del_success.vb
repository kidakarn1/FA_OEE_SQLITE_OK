Public Class Del_success
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        user_manage.ListView1.Items.Clear()


        Dim LoadSQL = Backoffice_model.get_all_user()
        Dim num As Integer = 0
        While LoadSQL.Read()
            user_manage.ListView1.View = View.Details
            user_manage.ListView1.Items.Add(LoadSQL("emp_id").ToString()).SubItems.AddRange(New String() {LoadSQL("fname").ToString() & " " & LoadSQL("lname").ToString(), LoadSQL("sec_name").ToString()})
        End While
        user_manage.PictureBox4.Visible = False
        user_manage.PictureBox5.Visible = False
        user_manage.Enabled = True
        'user_manage.Show()
        Me.Close()

    End Sub
End Class