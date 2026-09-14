Public Class NumpadQtySet_Scan_Rm
    Private Sub pbOK_Click(sender As Object, e As EventArgs) Handles pbOK.Click
        SetDataFinal()
    End Sub
    Private Sub SetDataFinal()
        Try
            Rm_scan.lbPartNo.Text = lbPartNumber.Text
            If CDbl(Val(tbAddqty.Text)) > 0 Then
                Rm_scan.lbQty.Text = CDbl(Val(tbAddqty.Text))
            Else
                Rm_scan.lbQty.Text = 1
            End If
        Catch ex As Exception
            Rm_scan.lbQty.Text = 1
        End Try
         OrderSelectPart.Close()
        Me.Close()
    End Sub
    Private Sub NumpadQtySet_Scan_Rm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbAddqty.Text = 0
        lbQtyShow.Text = 0
    End Sub

    Private Sub pbBack_Click(sender As Object, e As EventArgs) Handles pbBack.Click
        Me.Close()
    End Sub

    Private Sub btnNumber0_Click(sender As Object, e As EventArgs) Handles btnNumber0.Click
        tbAddqty.Text = tbAddqty.Text + "0"
        lbQtyShow.Text = tbAddqty.Text
    End Sub

    Private Sub btnNumber1_Click(sender As Object, e As EventArgs) Handles btnNumber1.Click
        tbAddqty.Text = tbAddqty.Text + "1"
        lbQtyShow.Text = tbAddqty.Text
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim txt_lenght As Integer = tbAddqty.Text.Length
        Try
            tbAddqty.Text = tbAddqty.Text.Substring(0, txt_lenght - 1)
            lbQtyShow.Text = tbAddqty.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnNumber7_Click(sender As Object, e As EventArgs) Handles btnNumber7.Click
        tbAddqty.Text = tbAddqty.Text + "7"
        lbQtyShow.Text = tbAddqty.Text
    End Sub

    Private Sub btnNumber8_Click(sender As Object, e As EventArgs) Handles btnNumber8.Click
        tbAddqty.Text = tbAddqty.Text + "8"
        lbQtyShow.Text = tbAddqty.Text
    End Sub

    Private Sub btnNumber9_Click(sender As Object, e As EventArgs) Handles btnNumber9.Click
        tbAddqty.Text = tbAddqty.Text + "9"
        lbQtyShow.Text = tbAddqty.Text
    End Sub

    Private Sub btnNumber4_Click(sender As Object, e As EventArgs) Handles btnNumber4.Click
        tbAddqty.Text = tbAddqty.Text + "4"
        lbQtyShow.Text = tbAddqty.Text
    End Sub

    Private Sub btnNumber5_Click(sender As Object, e As EventArgs) Handles btnNumber5.Click
        tbAddqty.Text = tbAddqty.Text + "5"
        lbQtyShow.Text = tbAddqty.Text
    End Sub

    Private Sub btnNumber6_Click(sender As Object, e As EventArgs) Handles btnNumber6.Click
        tbAddqty.Text = tbAddqty.Text + "6"
        lbQtyShow.Text = tbAddqty.Text
    End Sub

    Private Sub btnNumber2_Click(sender As Object, e As EventArgs) Handles btnNumber2.Click
        tbAddqty.Text = tbAddqty.Text + "2"
        lbQtyShow.Text = tbAddqty.Text
    End Sub

    Private Sub btnNumber3_Click(sender As Object, e As EventArgs) Handles btnNumber3.Click
        tbAddqty.Text = tbAddqty.Text + "3"
        lbQtyShow.Text = tbAddqty.Text
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        tbAddqty.Clear()
        lbQtyShow.Text = tbAddqty.Text
    End Sub
End Class