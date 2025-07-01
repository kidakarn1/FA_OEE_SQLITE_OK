Imports System.Web.Script.Serialization
Public Class Sel_prod_start
	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		sc_wi_plan.TextBox1.Select()
		sc_wi_plan.Show()
		Working_Pro.lb_nc_qty.Text = "0"
		Working_Pro.lb_ng_qty.Text = "0"
		'MainFrm.Enabled = True
		'MainFrm.Hide()
		Me.Close()
	End Sub
	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

	End Sub
	Private Sub Sel_prod_start_Load(sender As Object, e As EventArgs) Handles MyBase.Load

	End Sub
End Class
