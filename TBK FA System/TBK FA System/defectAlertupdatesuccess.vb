Public Class defectAlertupdatesuccess
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dfAdminHome As New defectAdminhome
        defectAdminregister.Close()

        If dfAdminHome.dfType = "NG" Then
            Dim dfAdmindetailng As New defectAdmindetailng()
            dfAdmindetailng.Show()
        ElseIf dfAdminHome.dfType = "NC" Then
            Dim dfAdmindetailnc As New defectAdmindetailnc()
            dfAdmindetailnc.Show()
        End If
        'Dim objAdmindetailnc As New defectAdmindetailnc()
        'objAdmindetailnc.Show()
        Me.Close()
    End Sub
End Class