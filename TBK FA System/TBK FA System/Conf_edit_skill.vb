Public Class Conf_edit_skill
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Edit_skill.Enabled = True
        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim created_emp_cd As String = skill_manage.Label3.Text.Substring(1, 5)
        Backoffice_model.edit_skill(Edit_skill.Label6.Text, Edit_skill.TextBox1.Text, created_emp_cd)

        skill_manage.ListView1.Items.Clear()
        Dim LoadSQL = Backoffice_model.get_all_skill()
        Dim num As Integer = 1
        While LoadSQL.Read()
            skill_manage.ListView1.View = View.Details
            skill_manage.ListView1.Items.Add(num).SubItems.AddRange(New String() {LoadSQL("sk_name").ToString()})
            num = num + 1
        End While

        skill_manage.Show()
        Edit_skill.Enabled = True
        Me.Close()

    End Sub
End Class