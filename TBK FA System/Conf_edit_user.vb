Public Class Conf_edit_user
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Edit_user.Enabled = True
        Dim group_id As Integer
        If Edit_user.ComboBox2.SelectedIndex = 0 Then
            group_id = 3
        Else
            group_id = 2
        End If
        Dim created_emp_cd As String = Edit_user.Label3.Text.Substring(1, 5)
        Backoffice_model.edit_user(Edit_user.TextBox3.Text, Edit_user.TextBox1.Text, Edit_user.TextBox2.Text, Edit_user.ListBox2.Items(Edit_user.ComboBox1.SelectedIndex), created_emp_cd, group_id, Edit_user.lb_su_id.Text)

        Backoffice_model.del_user_skill_old(Edit_user.lb_su_id.Text, created_emp_cd)

        For i = 0 To Edit_user.ListView1.Items.Count - 1
            If Edit_user.ListView1.Items(i).Checked = True Then
                'MsgBox(ListBox1.Items(i))
                Backoffice_model.Insert_user_skill(Edit_user.lb_su_id.Text, Edit_user.ListBox1.Items(i), created_emp_cd)
            End If
        Next


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



        user_manage.Show()
        Edit_user.Close()
        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Edit_user.Enabled = True
        Me.Close()

    End Sub
End Class