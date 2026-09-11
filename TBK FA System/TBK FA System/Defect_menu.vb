Public Class Defect_menu
	Private Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
		Working_Pro.Enabled = True
		Me.Close()
	End Sub
	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Dim snp As Integer = Convert.ToInt32(Working_Pro.Label27.Text)
		Try
			Defect_nc_adj.Label1.Text = Working_Pro._Edit_Up_0.Text Mod snp
		Catch ex As Exception
			Defect_nc_adj.Label1.Text = "0"
		End Try
		Sel_defect_cd_nc.ListView2.View = View.Details
		Loss_reg.Label2.Text = MainFrm.Label4.Text
		Try
			If My.Computer.Network.Ping("192.168.161.101") Then
				Dim LoadSQL = Backoffice_model.get_defect_mst()
				While LoadSQL.Read()
					Sel_defect_cd_nc.ListView2.ForeColor = Color.White
					Sel_defect_cd_nc.ListView2.Items.Add(LoadSQL("defect_cd").ToString()).SubItems.AddRange(New String() {LoadSQL("description").ToString()})
					Sel_defect_cd_nc.ListBox1.Items.Add(LoadSQL("defect_grp").ToString())
				End While
				Sel_defect_cd_nc.Show()
				Me.Hide()
			Else
				load_show.Show()
			End If
		Catch ex As Exception
			load_show.Show()
		End Try
	End Sub
	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		MsgBox("Can't use this menu.")
	End Sub
End Class
