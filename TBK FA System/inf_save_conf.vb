Public Class inf_save_conf
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Inf_manage.Enabled = True
        Inf_manage.Show()
        Me.Close()


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim inf_text As String = Inf_manage.TextBox1.Text
        Dim emp_cd As String = Inf_manage.Label3.Text.Substring(1, 5)
        'MsgBox(emp_cd)
        Backoffice_model.inf_update(inf_text, emp_cd)

        'Adm_page.TextBox1.Text = Inf_manage.TextBox1.Text
        Inf_manage.Enabled = True
        Adm_manage.Show()
        Me.Close()

    End Sub
End Class