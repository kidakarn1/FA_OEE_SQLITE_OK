Public Class user_manage
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Adm_manage.Show()
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ListView1.Items.Clear()
        Dim sec_nm As String = ComboBox1.SelectedItem

        If sec_nm = "ALL" Then
            Dim LoadSQL = Backoffice_model.get_all_user()
            Dim num As Integer = 0
            While LoadSQL.Read()
                ListView1.View = View.Details
                ListView1.Items.Add(LoadSQL("emp_id").ToString()).SubItems.AddRange(New String() {LoadSQL("fname").ToString() & " " & LoadSQL("lname").ToString(), LoadSQL("sec_name").ToString()})
            End While
        Else
            Dim LoadSQL = Backoffice_model.get_sec_user(sec_nm)
            Dim num As Integer = 0
            While LoadSQL.Read()
                ListView1.View = View.Details
                ListView1.Items.Add(LoadSQL("emp_id").ToString()).SubItems.AddRange(New String() {LoadSQL("fname").ToString() & " " & LoadSQL("lname").ToString(), LoadSQL("sec_name").ToString()})
            End While
        End If


    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count <= 0 Then
            PictureBox4.Visible = False
            PictureBox5.Visible = False
        ElseIf ListView1.SelectedItems.Count > 0 Then
            PictureBox4.Visible = True
            PictureBox5.Visible = True
        End If
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        'MsgBox("Delete")
        Dim del_number As Integer = ListView1.SelectedIndices(0)
        Dim created_emp_cd As String = Label3.Text.Substring(1, 5)
        'MsgBox(del_number)
        'MsgBox(ListView1.Items(del_number).Text.ToString)
        Backoffice_model.del_user(ListView1.Items(del_number).Text.ToString, created_emp_cd)

        Me.Enabled = False
        Del_success.Show()

    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        'MsgBox("Edit")
        Dim sel_number As Integer = ListView1.SelectedIndices(0)
        Dim LoadSQL = Backoffice_model.get_user_detail(ListView1.Items(sel_number).Text.ToString)
        Dim dep_id As Integer
        Dim group_user As Integer
        While LoadSQL.Read()
            Edit_user.TextBox1.Text = LoadSQL("fname").ToString()
            Edit_user.TextBox2.Text = LoadSQL("lname").ToString()
            Edit_user.TextBox3.Text = LoadSQL("emp_id").ToString()
            dep_id = LoadSQL("department_id").ToString()
            group_user = LoadSQL("sug_id").ToString()
            Edit_user.lb_su_id.Text = LoadSQL("su_id").ToString
        End While

        Dim LoadSQL2 = Backoffice_model.get_all_skill()
        Dim num As Integer = 0
        While LoadSQL2.Read()
            Edit_user.ListView1.View = View.Details
            Edit_user.ListView1.Items.Add("  ", HorizontalAlignment.Center).SubItems.AddRange(New String() {LoadSQL2("sk_name").ToString()})
            Edit_user.ListBox1.Items.Add(LoadSQL2("sk_id").ToString())
        End While

        Dim LoadSQLdep = Backoffice_model.get_department()
        While LoadSQLdep.Read()
            Edit_user.ComboBox1.Items.Add(LoadSQLdep("sec_name").ToString())
            Edit_user.ListBox2.Items.Add(LoadSQLdep("dep_id").ToString())
        End While

        Edit_user.ComboBox1.SelectedIndex = dep_id - 1

        If group_user = 3 Then
            Edit_user.ComboBox2.SelectedIndex = 0
        Else
            Edit_user.ComboBox2.SelectedIndex = 1
        End If

        Dim LoadSQLussk = Backoffice_model.get_user_skill(Edit_user.lb_su_id.Text)
        While LoadSQLussk.Read()
            Edit_user.ListView1.Items(LoadSQLussk("sk_id") - 1).checked = True
        End While

        Edit_user.Label2.Text = Label2.Text
        Edit_user.Label3.Text = Label3.Text


        Edit_user.Show()
        Me.Hide()

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Add_user.Show()
        Me.Hide()

    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Dim LoadSQLdep = Backoffice_model.get_department()
        While LoadSQLdep.Read()
            Add_user.ComboBox1.Items.Add(LoadSQLdep("sec_name").ToString())
            Add_user.ListBox2.Items.Add(LoadSQLdep("dep_id").ToString())
        End While

        Add_user.ComboBox1.SelectedIndex = 0
        Add_user.ComboBox2.SelectedIndex = 0
        Add_user.ListView1.LabelEdit = True
        Add_user.ListView1.AllowColumnReorder = True
        Dim LoadSQL = Backoffice_model.get_all_skill()
        Dim num As Integer = 0
        While LoadSQL.Read()
            Add_user.ListView1.View = View.Details
            Add_user.ListView1.Items.Add("", HorizontalAlignment.Center).SubItems.AddRange(New String() {LoadSQL("sk_name").ToString()})
            Add_user.ListBox1.Items.Add(LoadSQL("sk_id").ToString())
        End While
        Add_user.Label2.Text = Label2.Text
        Add_user.Label3.Text = Label3.Text

        Add_user.Show()
        Me.Hide()

    End Sub
End Class