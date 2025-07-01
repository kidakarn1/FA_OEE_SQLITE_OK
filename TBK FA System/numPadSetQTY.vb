Public Class numPadSetQTY
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub btn_next_Click(sender As Object, e As EventArgs) Handles btn_next.Click
        Try
            If CDbl(Val(tbAddjustQTY.Text)) <= CDbl(Val(lb_MAX_QTY.Text)) Then
                'SetStartTime.tbQTY.Text = tbAddjustQTY.Text
                Me.Close()
            Else
                MsgBox("Please Check QTY.")
            End If
        Catch ex As Exception
            MsgBox("Please Checks QTY.")
        End Try
    End Sub

    Private Sub btnNumber1_Click(sender As Object, e As EventArgs) Handles btnNumber1.Click
        Dim text_now As String = tbAddjustQTY.Text
        text_now = text_now & "1"
        tbAddjustQTY.Text = text_now
    End Sub

    Private Sub numPadSetQTY_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lb_MAX_QTY.Text = CDbl(Val(Working_Pro.Label8.Text)) - CDbl(Val(Working_Pro.Label6.Text))

    End Sub

    Private Sub btnNumber2_Click(sender As Object, e As EventArgs) Handles btnNumber2.Click
        Dim text_now As String = tbAddjustQTY.Text
        text_now = text_now & "2"
        tbAddjustQTY.Text = text_now
    End Sub

    Private Sub btnNumber3_Click(sender As Object, e As EventArgs) Handles btnNumber3.Click
        Dim text_now As String = tbAddjustQTY.Text
        text_now = text_now & "3"
        tbAddjustQTY.Text = text_now
    End Sub

    Private Sub btnNumber4_Click(sender As Object, e As EventArgs) Handles btnNumber4.Click
        Dim text_now As String = tbAddjustQTY.Text
        text_now = text_now & "4"
        tbAddjustQTY.Text = text_now
    End Sub

    Private Sub btnNumber5_Click(sender As Object, e As EventArgs) Handles btnNumber5.Click
        Dim text_now As String = tbAddjustQTY.Text
        text_now = text_now & "5"
        tbAddjustQTY.Text = text_now
    End Sub

    Private Sub btnNumber6_Click(sender As Object, e As EventArgs) Handles btnNumber6.Click
        Dim text_now As String = tbAddjustQTY.Text
        text_now = text_now & "6"
        tbAddjustQTY.Text = text_now
    End Sub

    Private Sub btnNumber7_Click(sender As Object, e As EventArgs) Handles btnNumber7.Click
        Dim text_now As String = tbAddjustQTY.Text
        text_now = text_now & "7"
        tbAddjustQTY.Text = text_now
    End Sub

    Private Sub btnNumber8_Click(sender As Object, e As EventArgs) Handles btnNumber8.Click
        Dim text_now As String = tbAddjustQTY.Text
        text_now = text_now & "8"
        tbAddjustQTY.Text = text_now
    End Sub

    Private Sub btnNumber9_Click(sender As Object, e As EventArgs) Handles btnNumber9.Click
        Dim text_now As String = tbAddjustQTY.Text
        text_now = text_now & "9"
        tbAddjustQTY.Text = text_now
    End Sub

    Private Sub ReselAll()
        btnNumber1.Enabled = True
        btnNumber2.Enabled = True
        btnNumber3.Enabled = True
        btnNumber4.Enabled = True
        btnNumber5.Enabled = True
        btnNumber6.Enabled = True
        btnNumber7.Enabled = True
        btnNumber8.Enabled = True
        btnNumber9.Enabled = True
        btnNumber0.Enabled = True
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        tbAddjustQTY.Clear()
        ReselAll()
    End Sub
    Private Sub btnNumber0_Click(sender As Object, e As EventArgs) Handles btnNumber0.Click
        Dim text_now As String = tbAddjustQTY.Text
        text_now = text_now & "0"
        tbAddjustQTY.Text = text_now
    End Sub
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim text_to1 As String = tbAddjustQTY.Text
        Dim lengthAdj As Integer = text_to1.Length
        Dim result_str As String = ""
        If text_to1.Length > 0 Then
            result_str = tbAddjustQTY.Text.Substring(0, lengthAdj - 1)
            tbAddjustQTY.Text = result_str
        End If
    End Sub
End Class