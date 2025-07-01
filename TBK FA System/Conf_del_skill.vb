Public Class Conf_del_skill
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        Dim del_number As Integer = skill_manage.ListView1.SelectedIndices(0)
        Dim sk_id As Integer = skill_manage.ListBox1.Items(del_number)

        Dim created_emp_cd As String = skill_manage.Label3.Text.Substring(1, 5)
        Backoffice_model.del_skill(sk_id, created_emp_cd)



        skill_manage.ListView1.Items.Clear()
        Dim LoadSQL = Backoffice_model.get_all_skill()
        Dim num As Integer = 1
        While LoadSQL.Read()
            skill_manage.ListView1.View = View.Details
            skill_manage.ListView1.Items.Add(num).SubItems.AddRange(New String() {LoadSQL("sk_name").ToString()})
            num = num + 1
        End While

        skill_manage.Enabled = True
        Me.Close()


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        skill_manage.Enabled = True

        Me.Close()
    End Sub
End Class