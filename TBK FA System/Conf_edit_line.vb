Public Class Conf_edit_line
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Edit_line_mst.Enabled = True

        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim created_emp_cd As String = Edit_line_mst.Label3.Text.Substring(1, 5)
        'MsgBox(Edit_line_mst.lb_line_id.Text)

        Try
            Backoffice_model.del_line_skill_old(Edit_line_mst.lb_line_id.Text, created_emp_cd)
        Catch ex As Exception

        End Try

        Dim numss As Integer = 1
        For i = 0 To Edit_line_mst.ListView1.Items.Count - 1
            If Edit_line_mst.ListView1.Items(i).Checked = True Then
                'MsgBox(ListBox1.Items(i))
                Backoffice_model.Insert_line_skill(Edit_line_mst.lb_line_id.Text, numss, Edit_line_mst.ListBox1.Items(i), created_emp_cd)
                numss = numss + 1
            End If
        Next


        line_manage.ListView1.Items.Clear()
        Dim LoadSQL = Backoffice_model.get_all_line()
        Dim num As Integer = 1
        While LoadSQL.Read()
            line_manage.ListView1.View = View.Details
            line_manage.ListView1.Items.Add(num).SubItems.AddRange(New String() {LoadSQL("line_cd").ToString(), LoadSQL("line_name").ToString()})
            line_manage.ListBox1.Items.Add(LoadSQL("line_id").ToString())
            num = num + 1
        End While

        line_manage.Label2.Text = Edit_line_mst.Label2.Text
        line_manage.Label3.Text = Edit_line_mst.Label3.Text
        line_manage.Show()
        Edit_line_mst.Enabled = True
        Edit_line_mst.Close()

        Me.Hide()


    End Sub
End Class