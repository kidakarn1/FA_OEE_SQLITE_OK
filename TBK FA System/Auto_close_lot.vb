Public Class Auto_close_lot
	Public count_close_lot As Integer = 0
	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		count_close_lot += 1
		If Count_close_lot_number.Text = 0 Then
			Timer1.Enabled = False
			Close_lot_cfm.con_close_lot()
		End If
		If count_close_lot = 10 Then
			Count_close_lot_number.Text -= 1
			count_close_lot = 0
		End If
	End Sub
	Private Sub Auto_close_lot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Count_close_lot_number.Text = 4
		Count_close_lot_number.Visible = False
		Timer1.Enabled = True
	End Sub
	Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

	End Sub
End Class
