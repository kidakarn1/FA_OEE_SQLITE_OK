Imports System.Web.Script.Serialization
Public Class Adm_manage
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Adm_page.Show()
        Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim LoadSQL = Backoffice_model.get_information()
        While LoadSQL.Read()
            Inf_manage.TextBox1.Text = LoadSQL("inf_txt").ToString
        End While
        Inf_manage.TextBox1.SelectionStart = Inf_manage.TextBox1.Text.Length
        Inf_manage.Label2.Text = Label2.Text
        Inf_manage.Label3.Text = Label3.Text
        Inf_manage.Show()
        Me.Hide()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Line_conf.ComboBox3.Enabled = False
        'Line_conf.ComboBox_master_device.Enabled = False
        'Line_conf.combo_cavity.Enabled = False
        'Line_conf.delay_sec.Enabled = False
        'Line_conf.scanner.Enabled = False
        'Line_conf.printer.Enabled = False
        'Line_conf.DIO_PORT.Enabled = False
        'Line_conf.type_counter.Enabled = False
        'Line_conf.Show()
        'If Backoffice_model.user_pd = "K1PD01" Then
        Dim rs As String = Backoffice_model.Get_PD_CONFIG(MainFrm.Label4.Text)
        If rs <> "0" Then
            Panel_configLine.Visible = True
            Panel_configLine.Enabled = True
            ComboBox1.Enabled = True
            ComboBox1.Visible = True
        End If
        ' End If
        'user_manage.ComboBox1.Items.Add("ALL")
        'Dim LoadSQLdep = Backoffice_model.get_department()
        'While LoadSQLdep.Read()
        'user_manage.ComboBox1.Items.Add(LoadSQLdep("sec_name").ToString())
        'End While
        '
        'Dim LoadSQL = Backoffice_model.get_all_user()
        'Dim num As Integer = 0
        'While LoadSQL.Read()
        'user_manage.ListView1.View = View.Details
        'user_manage.ListView1.Items.Add(LoadSQL("emp_id").ToString()).SubItems.AddRange(New String() {LoadSQL("fname").ToString() & " " & LoadSQL("lname").ToString(), LoadSQL("sec_name").ToString()})
        'End While
        '        user_manage.ComboBox1.SelectedIndex = 0
        '        user_manage.Label2.Text = Label2.Text
        '        user_manage.Label3.Text = Label3.Text
        '        user_manage.Show()
        'Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Dim LoadSQL = Backoffice_model.get_all_skill()
        Dim num As Integer = 1
        While LoadSQL.Read()
            skill_manage.ListView1.View = View.Details
            skill_manage.ListView1.Items.Add(num).SubItems.AddRange(New String() {LoadSQL("sk_name").ToString()})
            skill_manage.ListBox1.Items.Add(LoadSQL("sk_id").ToString())
            num = num + 1
        End While

        skill_manage.Label2.Text = Label2.Text
        skill_manage.Label3.Text = Label3.Text
        skill_manage.Show()
        Me.Hide()

    End Sub
    ' Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
    ' Dim objDefectAdminhome As New defectAdminhome()
    '     objDefectAdminhome.Show()
    ' End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.Enabled = False
        'Scan_reprint.TextBox1.Select()
        Scan_reprint.Show()
    End Sub
    Private Sub Adm_manage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbLine.Text = MainFrm.Label4.Text
        'If Backoffice_model.user_pd = "K1PD01" Then
        Dim rs As String = Backoffice_model.Get_PD_CONFIG(MainFrm.Label4.Text)
        If rs <> "0" Then
            Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
            For Each item As Object In dict2
                ComboBox1.Items.Add(item("line_cd").ToString)
            Next
            ComboBox1.SelectedIndex = 0
            Button2.Enabled = True
        End If
        ' End If
    End Sub
    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        'defectAdminhome.Show()
        'Me.Hide()
        MsgBox("Not Permission")
    End Sub
    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles pbOK.Click
        'If Backoffice_model.user_pd = "K1PD01" Then
        Panel_configLine.Visible = False
        Panel_configLine.Enabled = False
        ComboBox1.Enabled = False
        ComboBox1.Visible = False
        Backoffice_model.UpdateLineConfig(ComboBox1.Text)
        MainFrm.Label4.Text = ComboBox1.Text
        lbLine.Text = ComboBox1.Text
        '  End If
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles pb_back.Click
        Panel_configLine.Visible = False
        Panel_configLine.Enabled = False
        ComboBox1.Enabled = False
        ComboBox1.Visible = False
    End Sub
End Class