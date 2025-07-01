Public Class defectAdminhome
    Public Shared dfType As String = ""
    Public Shared dfCatType As String = ""
    Private Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        Adm_manage.Show()
        Me.Close()
    End Sub

    Private Sub btnRegisternc_Click(sender As Object, e As EventArgs) Handles btnRegisternc.Click
        dfType = "NC"
        dfCatType = "Register"
        'Dim dfSelecttype = New defectAdmindetailnc()
        defectAdmindetailnc.Show()
        Me.Close()
    End Sub

    Private Sub btnAdjustnc_Click(sender As Object, e As EventArgs) Handles btnAdjustnc.Click
        dfType = "NC"
        dfCatType = "Adjust"
        ' Dim dfAdmininsertnc As New defectAdminsearchnc()
        defectAdminsearch.backgroundNG.Visible = False
        defectAdminsearch.Show()
        Me.Hide()
    End Sub
    Private Sub btnRegisterng_Click(sender As Object, e As EventArgs) Handles btnRegisterng.Click
        dfType = "NG"
        dfCatType = "Register"
        ' Dim dfSelecttype = New defectAdmindetailng()
        defectAdmindetailng.Show()
        Me.Close()
    End Sub
    Private Sub btnAdjustng_Click(sender As Object, e As EventArgs) Handles btnAdjustng.Click
        dfType = "NG"
        dfCatType = "Adjust"
        defectAdminsearch.backgroundNG.Visible = True
        ' Dim dfAdmininsertng As New defectAdmindetailng()
        ' Dim dfAdmininsertnc As New defectAdminsearchnc()
        defectAdminsearch.Show()
        Me.Hide()
    End Sub
End Class
