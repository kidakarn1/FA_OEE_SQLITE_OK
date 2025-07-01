Public Class Conf_add_skill
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Add_skill.Enabled = True

        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim created_emp_cd As String = Add_skill.Label3.Text.Substring(1, 5)

        Backoffice_model.Insert_skill(Add_skill.TextBox1.Text, created_emp_cd)

        Dim LoadSQL = Backoffice_model.get_all_skill()
        Dim num As Integer = 1
        While LoadSQL.Read()
            skill_manage.ListView1.View = View.Details
            skill_manage.ListView1.Items.Add(num).SubItems.AddRange(New String() {LoadSQL("sk_name").ToString()})
            num = num + 1
        End While

        skill_manage.Label2.Text = Add_skill.Label2.Text
        skill_manage.Label3.Text = Add_skill.Label3.Text

        skill_manage.Show()

        Add_skill.Enabled = True
        Add_skill.Close()
        Me.Close()


    End Sub
End Class