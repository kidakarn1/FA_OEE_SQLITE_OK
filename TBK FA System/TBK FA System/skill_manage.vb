Public Class skill_manage
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Adm_manage.Show()
        Me.Close()
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

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Add_skill.Label2.Text = Label2.Text
        Add_skill.Label3.Text = Label3.Text

        Add_skill.TextBox1.Select()

        Add_skill.Show()
        Me.Hide()


    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Me.Enabled = False
        Conf_del_skill.Show()

    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Dim del_number As Integer = ListView1.SelectedIndices(0)
        Dim sk_id As Integer = ListBox1.Items(del_number)

        Edit_skill.TextBox1.Text = ListView1.SelectedItems(0).SubItems(1).Text


        Edit_skill.Label6.Text = sk_id
        Edit_skill.Label2.Text = Label2.Text
        Edit_skill.Label3.Text = Label3.Text

        Edit_skill.TextBox1.Select()

        Edit_skill.Show()
        Me.Hide()
    End Sub

    Private Sub skill_manage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class