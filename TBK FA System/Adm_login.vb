Imports System.Web.Script.Serialization

Public Class Adm_login
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Adm_page.Enabled = True
        Me.Close()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub
    Public Sub checkPermissionReader()
        Dim tempp As String = ""
        Dim emp_cd As String = ""
        Dim name_sur As String = ""
        If TextBox1.Text.Length = 5 Then
            Dim LoadSQL = Backoffice_model.chk_user_skill_line(Trim(TextBox1.Text), MainFrm.Label4.Text)
            Dim mp_adm_control_flg As String = ""
            Dim mp_prod_control_flg As String = ""
            ' While LoadSQL.Read()
            'tempp = LoadSQL("sug_id").ToString()
            'emp_cd = LoadSQL("emp_id").ToString()
            'name_sur = LoadSQL("fname").ToString() & " " & LoadSQL("lname").ToString()
            'End While
            If LoadSQL <> "0" Then
                Dim dict3 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(LoadSQL)
                For Each item As Object In dict3
                    mp_adm_control_flg = item("mp_adm_control_flg").ToString()
                    mp_prod_control_flg = item("mp_prod_control_flg").ToString()
                    Backoffice_model.user_pd = item("dep_cd").ToString()
                    emp_cd = item("sau_username").ToString()
                    emp_name = item("sau_fname").ToString() & " " & item("sau_lname").ToString()
                Next
            End If
            If mp_adm_control_flg = "1" Then
                'MsgBox("OK")
                Adm_manage.Label2.Text = name_sur
                Adm_manage.Label3.Text = "(" & emp_cd & ")"
                Adm_manage.Show()
                Me.Close()
                Adm_page.Hide()
                Adm_page.Enabled = True
                '  ElseIf tempp = "" Then
                '      MsgBox("Error! Please login by Admin")
                '      TextBox1.Text = ""
                '      'TextBox1.Select()
                '      Adm_page.Enabled = True
                '      Me.Close()
            Else
                MsgBox("Error! Please login by Admin")
                TextBox1.Text = ""
                'TextBox1.Select()
                Adm_page.Enabled = True
                Me.Close()
            End If
        Else
            MsgBox("Can't to login! Please scan your employee card.")
            TextBox1.Text = ""
            TextBox1.Focus()
            TextBox1.Select()
            ' Me.Close()
        End If
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            checkPermissionReader()
        End If
    End Sub

    Private Sub Adm_login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Enabled = False
        KeyboardAdmin.Show()
    End Sub
End Class
