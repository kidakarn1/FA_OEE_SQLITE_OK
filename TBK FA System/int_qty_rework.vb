Public Class int_qty_rework
	Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
		Working_Pro.Enabled = True
		Me.Close()


	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Try
			If Len(INP_QTY_REWORK.Text) <= 8 Then
				Dim text_now As String = INP_QTY_REWORK.Text
				Dim sum_str As Integer = text_now & "1"
				'  Dim rem_qty As Integer = Working_Pro.Label10.Text.Substring(1)

				' If sum_str > rem_qty Then
				'     MsgBox("Insert mitaked! Please try it again.")
				' Else
				INP_QTY_REWORK.Text = text_now & "1"
				Button10.Enabled = True
				' End If
			Else
				MsgBox("Number Over 9 Digit")
			End If
		Catch ex As Exception
			MsgBox("Number Over 9 Digit")
		End Try

	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Try
			If Len(INP_QTY_REWORK.Text) <= 8 Then
				Dim text_now As String = INP_QTY_REWORK.Text
				Dim sum_str As Integer = text_now & "2"
				'  Dim rem_qty As Integer = Working_Pro.Label10.Text.Substring(1)
				' If sum_str > rem_qty Then
				'     MsgBox("Insert mitaked! Please try it again.")
				' Else
				INP_QTY_REWORK.Text = text_now & "2"
				Button10.Enabled = True
			Else
				MsgBox("Number Over 9 Digit")
			End If
		Catch ex As Exception
			MsgBox("Number Over 9 Digit")
		End Try
		' End If
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		Try
			If Len(INP_QTY_REWORK.Text) <= 8 Then
				Dim text_now As String = INP_QTY_REWORK.Text
				Dim sum_str As Integer = text_now & "3"
				'   Dim rem_qty As Integer = Working_Pro.Label10.Text.Substring(1)
				'   If sum_str > rem_qty Then
				'     MsgBox("Insert mitaked! Please try it again.")
				'  Else
				INP_QTY_REWORK.Text = text_now & "3"
				Button10.Enabled = True
				' End If
			Else
				MsgBox("Number Over 9 Digit")
			End If
		Catch ex As Exception
			MsgBox("Number Over 9 Digit")
		End Try
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		Try
			If Len(INP_QTY_REWORK.Text) <= 8 Then
				Dim text_now As String = INP_QTY_REWORK.Text
				Dim sum_str As Integer = text_now & "4"
				'  Dim rem_qty As Integer = Working_Pro.Label10.Text.Substring(1)

				' If sum_str > rem_qty Then
				'MsgBox("Insert mitaked! Please try it again.")
				' Else
				INP_QTY_REWORK.Text = text_now & "4"
				Button10.Enabled = True
				'End If
			Else
				MsgBox("Number Over 9 Digit")
			End If
		Catch ex As Exception
			MsgBox("Number Over 9 Digit")
		End Try
	End Sub

	Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
		Try
			If Len(INP_QTY_REWORK.Text) <= 8 Then
				Dim text_now As String = INP_QTY_REWORK.Text
				Dim sum_str As Integer = text_now & "5"
				'Dim rem_qty As Integer = Working_Pro.Label10.Text.Substring(1)
				'  If sum_str > rem_qty Then
				'MsgBox("Insert mitaked! Please try it again.")
				' Else
				INP_QTY_REWORK.Text = text_now & "5"
				Button10.Enabled = True
				' End If
			Else
				MsgBox("Number Over 9 Digit")
			End If
		Catch ex As Exception
			MsgBox("Number Over 9 Digit")
		End Try
	End Sub

	Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
		Try
			If Len(INP_QTY_REWORK.Text) <= 8 Then
				Dim text_now As String = INP_QTY_REWORK.Text
				Dim sum_str As Integer = text_now & "6"
				' Dim rem_qty As Integer = Working_Pro.Label10.Text.Substring(1)

				'  If sum_str > rem_qty Then
				'   MsgBox("Insert mitaked! Please try it again.")
				'  Else
				INP_QTY_REWORK.Text = text_now & "6"
				Button10.Enabled = True
				'  End If
			Else
				MsgBox("Number Over 9 Digit")
			End If
		Catch ex As Exception
			MsgBox("Number Over 9 Digit")
		End Try
	End Sub

	Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
		Try
			If Len(INP_QTY_REWORK.Text) <= 8 Then
				Dim text_now As String = INP_QTY_REWORK.Text
				Dim sum_str As Integer = text_now & "7"
				' Dim rem_qty As Integer = Working_Pro.Label10.Text.Substring(1)

				'    If sum_str > rem_qty Then
				'      MsgBox("Insert mitaked! Please try it again.")
				'   Else
				INP_QTY_REWORK.Text = text_now & "7"
				Button10.Enabled = True
				'End If
			Else
				MsgBox("Number Over 9 Digit")
			End If
		Catch ex As Exception
			MsgBox("Number Over 9 Digit")
		End Try

	End Sub

	Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
		Try
			If Len(INP_QTY_REWORK.Text) <= 8 Then
				Dim text_now As String = INP_QTY_REWORK.Text
				Dim sum_str As Integer = text_now & "8"
				' Dim rem_qty As Integer = Working_Pro.Label10.Text.Substring(1)
				' If sum_str > rem_qty Then
				'MsgBox("Insert mitaked! Please try it again.")
				' Else
				INP_QTY_REWORK.Text = text_now & "8"
				Button10.Enabled = True
				' End If
			Else
				MsgBox("Number Over 9 Digit")
			End If
		Catch ex As Exception
			MsgBox("Number Over 9 Digit")
		End Try
	End Sub

	Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
		Try
			If Len(INP_QTY_REWORK.Text) <= 8 Then
				Dim text_now As String = INP_QTY_REWORK.Text
				Dim sum_str As Integer = text_now & "9"
				' Dim rem_qty As Integer = Working_Pro.Label10.Text.Substring(1)

				' If sum_str > rem_qty Then
				'MsgBox("Insert mitaked! Please try it again.")
				'  Else
				INP_QTY_REWORK.Text = text_now & "9"
				Button10.Enabled = True
				' End If
			Else
				MsgBox("Number Over 9 Digit")
			End If
		Catch ex As Exception
			MsgBox("Number Over 9 Digit")
		End Try
	End Sub

	Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
		Try
			If Len(INP_QTY_REWORK.Text) <= 8 Then
				Dim text_now As String = INP_QTY_REWORK.Text
				Dim sum_str As Integer = text_now & "0"
				' Dim rem_qty As Integer = Working_Pro.Label10.Text.Substring(1)
				' If sum_str > rem_qty Then
				'MsgBox("Insert mitaked! Please try it again.")
				'Else
				INP_QTY_REWORK.Text = text_now & "0"
				Button10.Enabled = True
				'  End If
			Else
				MsgBox("Number Over 9 Digit")
			End If
		Catch ex As Exception
			MsgBox("Number Over 9 Digit")
		End Try
	End Sub

	Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
		INP_QTY_REWORK.Text = ""
		INP_QTY_REWORK.Select()

	End Sub

	Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
		Dim ins_qtyy As Integer = INP_QTY_REWORK.Text
		Dim max_val As String = Working_Pro.Label10.Text
		max_val = max_val.Substring(1, max_val.Length - 1)

		Dim max_val_int As Integer = Convert.ToInt32(max_val)
		'MsgBox(max_val)

		If ins_qtyy > 0 And ins_qtyy <= max_val_int Then
			Working_Pro.lb_ins_qty.Text = INP_QTY_REWORK.Text

			Working_Pro.ins_qty_fn()
			Working_Pro.Enabled = True
			Me.Close()
		Else
			MsgBox("Insert mitaked! Please try it again.")
			Working_Pro.Enabled = False

		End If
	End Sub
	Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
		select_int_qty.Show()
		'Working_Pro.Enabled = True
		Me.Close()
	End Sub
	Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
		Dim date_now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
		Try
			If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
				Dim tr_status = "1"
				Backoffice_model.INSERT_REWORK_ACTUAL(Working_Pro.Label3.Text, INP_QTY_REWORK.Text, Working_Pro.Label14.Text, date_now, Prd_detail.lb_wi.Text, Working_Pro.Label12.Text, Working_Pro.lb_model.Text)
				Backoffice_model.INSERT_REWORK_ACTUAL_SQLITE(Working_Pro.Label3.Text, INP_QTY_REWORK.Text, Working_Pro.Label14.Text, date_now, Prd_detail.lb_wi.Text, Working_Pro.Label12.Text, Working_Pro.lb_model.Text, tr_status)
			Else
				Dim tr_status = "0"
				Backoffice_model.INSERT_REWORK_ACTUAL_SQLITE(Working_Pro.Label3.Text, INP_QTY_REWORK.Text, Working_Pro.Label14.Text, date_now, Prd_detail.lb_wi.Text, Working_Pro.Label12.Text, Working_Pro.lb_model.Text, tr_status)
			End If
		Catch ex As Exception
			Dim tr_status = "0"
			Backoffice_model.INSERT_REWORK_ACTUAL_SQLITE(Working_Pro.Label3.Text, INP_QTY_REWORK.Text, Working_Pro.Label14.Text, date_now, Prd_detail.lb_wi.Text, Working_Pro.Label12.Text, Working_Pro.lb_model.Text, tr_status)
		End Try
		select_int_qty.Close()
		Completed_rework.Show()

		Working_Pro.Enabled = True
		' Dim ins_qtyy As Integer = TextBox1.Text
		'  Dim max_val As String = Working_Pro.Label10.Text
		'  max_val = max_val.Substring(1, max_val.Length - 1)
		'  Working_Pro.LB_COUNTER_SHIP.Text = CDbl(Val(Working_Pro.LB_COUNTER_SHIP.Text)) + CDbl(Val(TextBox1.Text))
		'  Working_Pro.LB_COUNTER_SEQ.Text = CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text)) + CDbl(Val(TextBox1.Text))
		'  Dim max_val_int As Integer = Convert.ToInt32(max_val)
		'  If ins_qtyy > 0 And ins_qtyy <= max_val_int Then
		'      Working_Pro.lb_ins_qty.Text = TextBox1.Text
		'      Working_Pro.ins_qty_fn()
		'      Working_Pro.Enabled = True
		'      select_int_qty.Close()
		'      Me.Close()
		'  Else
		'      MsgBox("Insert mitaked! Please try it again.")
		'      Working_Pro.Enabled = False
		'   End If
	End Sub

	Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
		Dim txt_lenght As Integer = INP_QTY_REWORK.Text.Length
		Try
			INP_QTY_REWORK.Text = INP_QTY_REWORK.Text.Substring(0, txt_lenght - 1)
		Catch ex As Exception

		End Try
	End Sub
    Private Sub int_qty_rework_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbPartNo.Text = Working_Pro.Label3.Text
        '   INP_QTY_REWORK.Enabled = False
    End Sub
End Class
