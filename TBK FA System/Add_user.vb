Public Class Add_user
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        user_manage.Show()
        Me.Close()

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click

        If TextBox1.Text.Length < 1 Then
            MsgBox("please enter name!")

        ElseIf TextBox2.Text.Length < 1 Then
            MsgBox("please enter surname!")

        ElseIf TextBox3.Text.Length < 1 Then
            MsgBox("please enter employee code!")

        Else
            Dim group_id As Integer
            If ComboBox2.SelectedIndex = 0 Then
                group_id = 3
            Else
                group_id = 2
            End If
            Dim created_emp_cd As String = Label3.Text.Substring(1, 5)

            Backoffice_model.Insert_user(TextBox3.Text, TextBox1.Text, TextBox2.Text, ListBox2.Items(ComboBox1.SelectedIndex), created_emp_cd, group_id)

            Dim LoadSQLlt = Backoffice_model.get_user_last_id()
            Dim num2 As Integer = 0
            While LoadSQLlt.Read()
                num2 = LoadSQLlt("last_id")
            End While

            'MsgBox(ComboBox1.SelectedItem)
            'MsgBox(ListBox2.Items(ComboBox1.SelectedIndex))

            For i = 0 To ListView1.Items.Count - 1
                If ListView1.Items(i).Checked = True Then
                    'MsgBox(ListBox1.Items(i))
                    Backoffice_model.Insert_user_skill(num2, ListBox1.Items(i), created_emp_cd)
                End If
            Next


            user_manage.ComboBox1.Items.Add("ALL")
            Dim LoadSQLdep = Backoffice_model.get_department()
            While LoadSQLdep.Read()
                user_manage.ComboBox1.Items.Add(LoadSQLdep("sec_name").ToString())
            End While


            Dim LoadSQL = Backoffice_model.get_all_user()
            Dim num As Integer = 0
            While LoadSQL.Read()
                user_manage.ListView1.View = View.Details
                user_manage.ListView1.Items.Add(LoadSQL("emp_id").ToString()).SubItems.AddRange(New String() {LoadSQL("fname").ToString() & " " & LoadSQL("lname").ToString(), LoadSQL("sec_name").ToString()})
            End While

            user_manage.ComboBox1.SelectedIndex = 0
            user_manage.Label2.Text = Label2.Text
            user_manage.Label3.Text = Label3.Text
            Me.Enabled = False
            Add_success.Show()


        End If





        'MsgBox(ListBox1.Items(2))

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub FontDialog1_Apply(sender As Object, e As EventArgs)

    End Sub
End Class