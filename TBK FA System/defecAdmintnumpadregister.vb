Public Class defecAdmintnumpadregister
    Shared objdefectregisSelect As New defectAdminselecttypenc()
    Shared objdefectRegister As New defectAdminregister()
    Shared dfAdmindetailnc As New defectAdmindetailnc()
    Shared dfAdmindetailng As New defectAdmindetailng()
    Public Shared actTotal As Integer = 0
    Public Shared ncTotal As Integer = 0
    Public Shared ngTotal As Integer = 0
    Private Sub defectNumpadregister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setVariable()
        Dim dfAdminhome As New defectAdminhome

        If dfAdminhome.dfType = "NC" Then
            If defectAdminselecttypenc.type = "1" Then
                lbMaxqty.Text = calMaxqtyregisternc(actTotal, ncTotal, ngTotal)
            Else
                Dim md = New modelDefect
                ' Dim UseQty = md.mGetdefectdetailncPartno(defectAdmindetailnc.sWi, defectAdmindetailnc.dSeq, defectAdmindetailnc.sLot, "2", lbPartno.Text)
                Dim UseQty = md.mGetdefectdetailPartno(defectAdmindetailnc.sWi, defectAdmindetailnc.dSeq, defectAdmindetailnc.sLot, "2", lbPartno.Text)
                maxQty = (999 - Convert.ToInt32(UseQty))
                lbMaxqty.Text = maxQty
            End If
        ElseIf dfAdminhome.dfType = "NG" Then
            If defectAdminselecttypeng.type = "1" Then
                lbMaxqty.Text = calMaxqtyregisterng(actTotal, ncTotal, ngTotal)
            Else
                Dim md = New modelDefect
                ' Dim UseQty = md.mGetdefectdetailncPartno(defectAdmindetailng.sWi, defectAdmindetailng.dSeq, defectAdmindetailng.sLot, "1", lbPartno.Text)
                Dim UseQty = md.mGetdefectdetailPartno(defectAdmindetailng.sWi, defectAdmindetailng.dSeq, defectAdmindetailng.sLot, "1", lbPartno.Text)
                maxQty = (999 - Convert.ToInt32(UseQty))
                lbMaxqty.Text = maxQty
            End If
        End If
    End Sub
    Public Sub setVariable()
        Dim objdefectregis As New defectAdminregister()
        actTotal = dfAdmindetailnc.sAct
        ncTotal = dfAdmindetailnc.sNc
        ngTotal = dfAdmindetailnc.sNg
        tbAddqty.Text = objdefectregis.dfQty
        lbPartno.Text = objdefectregis.sPart
        Dim dfAdminhome As New defectAdminhome
        If dfAdminhome.dfType = "NC" Then
            actTotal = dfAdmindetailnc.sAct
            ncTotal = dfAdmindetailnc.sNc
            ngTotal = dfAdmindetailnc.sNg
        ElseIf dfAdminhome.dfType = "NG" Then
            actTotal = dfAdmindetailng.sAct
            ncTotal = dfAdmindetailng.sNc
            ngTotal = dfAdmindetailng.sNg
        End If
    End Sub
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        clickOk()
    End Sub
    Public Sub clickOk()
        If tbAddqty.Text = "" Then
            tbAddqty.Text = 0
        End If
        Dim rsCheck = calNumpadregister(tbAddqty.Text, lbMaxqty.Text)
        If rsCheck Then
            'Dim objdefectregis As New defectAdminregister()
            defectAdminregister.lbQtydefect.Text = CDbl(Val(tbAddqty.Text))
            Me.Close()
            defectAdminregister.Show()
        Else
            MsgBox("Please Check QTY Input")
        End If
    End Sub
    Public Shared Function calNumpadregister(rtQty As Integer, maxQty As Integer)
        If rtQty <= maxQty And rtQty >= 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnNumber1.Click
        tbAddqty.Text = tbAddqty.Text + "1"
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnNumber2.Click
        tbAddqty.Text = tbAddqty.Text + "2"
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnNumber3.Click
        tbAddqty.Text = tbAddqty.Text + "3"
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnNumber4.Click
        tbAddqty.Text = tbAddqty.Text + "4"
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btnNumber5.Click
        tbAddqty.Text = tbAddqty.Text + "5"
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles btnNumber6.Click
        tbAddqty.Text = tbAddqty.Text + "6"
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnNumber7.Click
        tbAddqty.Text = tbAddqty.Text + "7"
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles btnNumber8.Click
        tbAddqty.Text = tbAddqty.Text + "8"
    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles btnNumber9.Click
        tbAddqty.Text = tbAddqty.Text + "9"
    End Sub
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles btnNumber0.Click
        tbAddqty.Text = tbAddqty.Text + "0"
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        tbAddqty.Clear()
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ' Dim objdefectregis As New defectAdminregister()
        Dim objdefectregis As New defectAdminregister()
        defectAdminregister.lbQtydefect.Text = objdefectregis.dfQty
        defectAdminregister.Show()
        Me.Close()
    End Sub
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim txt_lenght As Integer = tbAddqty.Text.Length
        Try
            tbAddqty.Text = tbAddqty.Text.Substring(0, txt_lenght - 1)
        Catch ex As Exception

        End Try
    End Sub
    Public Function calMaxqtyregisternc(actTotal As Integer, ncTotal As Integer, ngTotal As Integer)
        Dim maxQty = actTotal - (ncTotal + ngTotal)
        If maxQty < 0 Then
            maxQty = actTotal
        End If
        'MsgBox(actTotal & " - " & "(" & ncTotal & " + " & ngTotal & ") = " & maxQty)
        Return maxQty
    End Function

    Public Function calMaxqtyregisterng(actTotal As Integer, ncTotal As Integer, ngTotal As Integer)
        Dim maxQty = actTotal - (ncTotal + ngTotal)
        If maxQty < 0 Then
            maxQty = actTotal
        End If
        Return maxQty
    End Function
End Class
