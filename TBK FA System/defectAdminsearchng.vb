Imports System.Globalization

Public Class defectAdminsearchng
    Public Shared lineCd As String
    Public Shared dateCheck As String
    Public Shared Pshift As String
	Private Sub btnBack_Click(sender As Object, e As EventArgs)
		Me.Close()
	End Sub
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        lineCd = MainFrm.Label4.Text
        Dim lDateCheck As Date = dtActdate.Text
        dateCheck = lDateCheck.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        Pshift = LbShift.Text.Substring(0, 1)
        defectAdminselectdetailncadjust.Show()
        Me.Hide()
    End Sub
    Private Sub Label12_Click(sender As Object, e As EventArgs)
		Me.Enabled = False
		'Dim CAdminShift As New Chang_Admin_Shift()
		Chang_Admin_Shift.Show()
	End Sub
	Public Sub loadDataAdjust(LineCd As String, sDate As String, Shift As String)

	End Sub
	Private Sub defectAdminsearchnc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		lbLine.Text = MainFrm.Label4.Text
	End Sub

    Private Sub Shift_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnOk_Click_1(sender As Object, e As EventArgs)

    End Sub
End Class
