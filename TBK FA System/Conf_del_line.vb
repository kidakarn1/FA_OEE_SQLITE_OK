Public Class Conf_del_line
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim del_number As Integer = line_manage.ListView1.SelectedIndices(0)
        Dim line_id As Integer = line_manage.ListBox1.Items(del_number)

        Dim created_emp_cd As String = line_manage.Label3.Text.Substring(1, 5)
        Backoffice_model.del_line(line_id, created_emp_cd)



        line_manage.ListView1.Items.Clear()
        Dim LoadSQL = Backoffice_model.get_all_line()
        Dim num As Integer = 1
        While LoadSQL.Read()
            line_manage.ListView1.View = View.Details
            line_manage.ListView1.Items.Add(num).SubItems.AddRange(New String() {LoadSQL("line_cd").ToString(), LoadSQL("line_name").ToString()})
            line_manage.ListBox1.Items.Add(LoadSQL("line_id").ToString())
            num = num + 1
        End While

        line_manage.Enabled = True
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        line_manage.Enabled = True

        Me.Close()
    End Sub
End Class