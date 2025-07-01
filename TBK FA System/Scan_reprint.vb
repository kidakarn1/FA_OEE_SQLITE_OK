Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Globalization
Public Class Scan_reprint
    Public date_now_start
    Public date_now_end
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles pbBack.Click
        Adm_manage.Enabled = True
        Me.Close()

    End Sub
    Public Sub Button2_Click(sender As Object, e As EventArgs) Handles BbOk.Click
        Dim date_reprint As String
        Dim data_start_naja As String = Date_start_reprint.Value
        Dim data_end_naja As String = Date_end_reprint.Value
        'Dim format As String = data_naja.conv("yyyy-MM-dd", New CultureInfo("en-US"))

        Dim date1 As DateTime = data_start_naja
        Dim format As String = date1.ToString("yyyy-MM-dd", New CultureInfo("en-US"))
        date_now_start = date1.ToString(format)

        Dim date2 As DateTime = data_end_naja
        Dim format2 As String = date2.ToString("yyyy-MM-dd", New CultureInfo("en-US"))
        date_now_end = date2.ToString(format2)

        'If TextBox1.Text.Length = 10 Then
        'MsgBox("OK")
        'Dim line_cd As String = MainFrm.Label4.Text
        date_reprint = Date_start_reprint.Value
        'MsgBox(wi_cd)
        Tag_reprint.Label2.Text = Adm_manage.Label2.Text
        Tag_reprint.Label3.Text = Adm_manage.Label3.Text
        Show_reprint_wi.Show()
        'Tag_reprint.Show()
        Adm_manage.Enabled = True
        Adm_manage.Hide()
        Me.Close()
        'Else
        'MsgBox("Please input WI plan")
        'End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Scan_reprint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Date_start_reprint.Value = DateTime.Now
        Date_end_reprint.Value = DateTime.Now
    End Sub
End Class