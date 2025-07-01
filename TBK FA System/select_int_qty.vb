Public Class select_int_qty
	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
		ins_qty.Show()
	End Sub
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Working_Pro.Enabled = True
        Working_OEE.Enabled = True
        Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        int_qty_rework.Show()
        Me.Hide()
    End Sub
    Private Sub select_int_qty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
    Private Sub SPECIAL_MENU_Click(sender As Object, e As EventArgs)
		ins_qty_special_time.Show()
	End Sub
End Class
