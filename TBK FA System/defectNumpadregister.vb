Public Class defectNumpadregister

    Shared objdefectregisSelect As New defectSelecttype()
    ' Shared objdefectRegister As New defectRegister()
    Shared pd As New Working_Pro()
    Shared actTotal As Integer = objdefectregisSelect.actTotal
    Shared ncTotal As Integer = objdefectregisSelect.ncTotal
    Shared ngTotal As Integer = objdefectregisSelect.ngTotal
    Private Sub defectNumpadregister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setVariable()
        If defectSelecttype.type = "1" Then
            lbMaxqty.Text = calMaxqtyregister(actTotal, ncTotal, ngTotal)
        Else
            Dim md = New modelDefect
            ' Dim UseQty = md.mGetdefectdetailncPartno(Working_Pro.wi_no.Text, Working_Pro.Label22.Text, Working_Pro.Label18.Text, defectSelecttype.dtType, lbPartno.Text)
            Dim UseQty = md.mGetdefectdetailPartno(Working_Pro.wi_no.Text, Working_Pro.Label22.Text, Working_Pro.Label18.Text, defectSelecttype.dtType, lbPartno.Text)
            Dim maxQty As Integer = (999 - Convert.ToInt32(UseQty))
            lbMaxqty.Text = maxQty '999 '"Unlimited"
        End If
    End Sub
    Public Sub setVariable()
        Dim objdefectregis As New defectRegister()
        'MsgBox("objdefectregis.lbPart.Text =  " & objdefectregis.lbPart.Text)
        lbPartno.Text = objdefectregis.lbPart.Text
        tbAddqty.Text = objdefectregis.dfQty
        lbPartno.Text = objdefectregis.sPart
        actTotal = objdefectregisSelect.actTotal
        ncTotal = objdefectregisSelect.ncTotal
        ngTotal = objdefectregisSelect.ngTotal
        'MsgBox("actTotal = >>>" & actTotal)
    End Sub
	Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
		clickOk()
	End Sub
    Public Sub clickOk()

        If defectSelecttype.type = "1" Then
                If tbAddqty.Text = "" Then
                    tbAddqty.Text = "0"
                End If
                Dim rsCheck = calNumpadregister(tbAddqty.Text, lbMaxqty.Text)
                If rsCheck Then
                    ' Dim objdefectregis As New defectRegister()
                    defectRegister.dfQty = CDbl(Val(tbAddqty.Text))
                defectRegister.tbQtydefectnc.Text = CDbl(Val(tbAddqty.Text))
                Me.Close()
                defectRegister.Show()
                Else
                    MsgBox("Please Check QTY Input")
                End If
            Else
                If tbAddqty.Text = "" Then
                    tbAddqty.Text = "0"
                End If
                Dim rsCheck = calNumpadregister(tbAddqty.Text, lbMaxqty.Text)
            If rsCheck Then
                If CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text)) > 0 Then
                    ' Dim objdefectregis As New defectRegister()
                    defectRegister.dfQty = CDbl(Val(tbAddqty.Text))
                    defectRegister.tbQtydefectnc.Text = CDbl(Val(tbAddqty.Text))
                    Me.Close()
                    defectRegister.Show()
                Else
                    MsgBox("Please Input Actaual QTY")
                End If
            Else
                MsgBox("Please Check QTY Input")
                End If
                'defectRegister.dfQty = CDbl(Val(tbAddqty.Text))
                'defectRegister.tbQtydefectnc.Text = CDbl(Val(tbAddqty.Text))
                'Me.Close()
                'defectRegister.Show()
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
        'Dim objdefectregis As New defectRegister()
        defectRegister.tbQtydefectnc.Text = defectRegister.dfQty
        defectRegister.Show()
        Me.Close()
	End Sub
	Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
		Dim txt_lenght As Integer = tbAddqty.Text.Length
		Try
			tbAddqty.Text = tbAddqty.Text.Substring(0, txt_lenght - 1)
		Catch ex As Exception

		End Try
	End Sub
	Public Function calMaxqtyregister(actTotal As Integer, ncTotal As Integer, ngTotal As Integer)
        Dim maxQty = actTotal - (ncTotal + ngTotal)
        Return maxQty
    End Function

    Private Sub lbMaxqty_Click(sender As Object, e As EventArgs) Handles lbMaxqty.Click

    End Sub
End Class
