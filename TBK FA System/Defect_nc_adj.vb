Imports VB = Microsoft.VisualBasic
Imports System
Imports System.Management
Imports System.ComponentModel
Imports System.Threading
Imports System.IO
Imports System.IO.Ports
Imports System.Text
Imports System.Globalization
Imports System.Drawing.Imaging
Imports IDAutomation.Windows.Forms.LinearBarCode
Imports System.Drawing.Printing
Imports System.Configuration
Imports GenCode128
Public Class Defect_nc_adj
	Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
		Sel_defect_cd_nc.ListView2.View = View.Details
		Loss_reg.Label2.Text = MainFrm.Label4.Text
		Dim LoadSQL = Backoffice_model.get_defect_mst()
		While LoadSQL.Read()
			Sel_defect_cd_nc.ListView2.ForeColor = Color.White
			Sel_defect_cd_nc.ListView2.Items.Add(LoadSQL("defect_cd").ToString()).SubItems.AddRange(New String() {LoadSQL("description").ToString()})
			Sel_defect_cd_nc.ListBox1.Items.Add(LoadSQL("defect_grp").ToString())
		End While
		Sel_defect_cd_nc.Show()
		Me.Close()

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		TextBox1.Text = TextBox1.Text + "1"
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		TextBox1.Text = TextBox1.Text + "2"
	End Sub

	Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
		TextBox1.Text = TextBox1.Text + "3"
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		TextBox1.Text = TextBox1.Text + "4"
	End Sub

	Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
		TextBox1.Text = TextBox1.Text + "5"
	End Sub

	Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
		TextBox1.Text = TextBox1.Text + "6"
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		TextBox1.Text = TextBox1.Text + "7"
	End Sub

	Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
		TextBox1.Text = TextBox1.Text + "8"
	End Sub

	Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
		TextBox1.Text = TextBox1.Text + "9"
	End Sub

	Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
		TextBox1.Text = TextBox1.Text + "0"
	End Sub

	Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
		TextBox1.Clear()
	End Sub

	Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
		Dim txt_lenght As Integer = TextBox1.Text.Length
		Try
			TextBox1.Text = TextBox1.Text.Substring(0, txt_lenght - 1)
		Catch ex As Exception

		End Try
	End Sub

	Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
		Dim cal_nc As Integer = CDbl(Val(Working_Pro.Label6.Text))  '- CDbl(Val(Working_Pro.lb_nc_qty.Text))
		If cal_nc > 0 Then
			If CDbl(Val(cal_nc)) <= CDbl(Val(Working_Pro.Label6.Text)) Then
				Dim prd_qty As Integer = Convert.ToInt32(Label1.Text)
				Dim inp_qty As Integer
				Try
					inp_qty = Convert.ToInt32(TextBox1.Text)
				Catch ex As Exception
					inp_qty = 0
				End Try
				If inp_qty > prd_qty Then
					MsgBox("Can't input the Qty. over : " + Label1.Text)
					TextBox1.Clear()
				Else
					Try
						'MsgBox("DATA")
						Dim yearNow As Integer = DateTime.Now.ToString("yyyy")
						Dim monthNow As Integer = DateTime.Now.ToString("MM")
						Dim dayNow As Integer = DateTime.Now.ToString("dd")
						Dim hourNow As Integer = DateTime.Now.ToString("HH")
						Dim minNow As Integer = DateTime.Now.ToString("mm")
						Dim secNow As Integer = DateTime.Now.ToString("ss")
						Dim yearSt As Integer = Working_Pro.st_time.Text.Substring(0, 4)
						Dim monthSt As Integer = Working_Pro.st_time.Text.Substring(5, 2)
						Dim daySt As Integer = Working_Pro.st_time.Text.Substring(8, 2)
						Dim hourSt As Integer = Working_Pro.st_time.Text.Substring(11, 2)
						Dim minSt As Integer = Working_Pro.st_time.Text.Substring(14, 2)
						Dim secSt As Integer = Working_Pro.st_time.Text.Substring(17, 2)
						Dim firstDate As New System.DateTime(yearSt, monthSt, daySt, hourSt, minSt, secSt)
						Dim secondDate As New System.DateTime(yearNow, monthNow, dayNow, hourNow, minNow, secNow)
						Dim diff As System.TimeSpan = secondDate.Subtract(firstDate)
						Dim diff1 As System.TimeSpan = secondDate - firstDate
						Dim diff2 As String = (secondDate - firstDate).TotalSeconds.ToString()
						'MsgBox("DATA2_3")
						Dim actCT As Double = Format(diff2 / 60, "0.00")
						'MsgBox("DATA2_4")
						Dim cnt_btn As Integer = Integer.Parse(MainFrm.cavity.Text)
						'MsgBox("DATA2_5")
						Dim snp As Integer = Integer.Parse(Working_Pro.Label27.Text)
						'MsgBox("DATA2_6")
						'MsgBox("Working_Pro._Edit_Up_0.Text = " & Working_Pro._Edit_Up_0.Text)
						'MsgBox("inp_qty = " & inp_qty)
						'Working_Pro._Edit_Up_0.Text = Integer.Parse(Working_Pro._Edit_Up_0.Text) - inp_qty
						'MsgBox("DATA2_6_1")
						'Working_Pro.Label6.Text = Integer.Parse(Working_Pro.Label6.Text) - inp_qty
						'Working_Pro.Label10.Text = Integer.Parse(Working_Pro.Label10.Text.Substring(1)) + inp_qty
						'MsgBox("DATA2_7")
						'Working_Pro.Label10.Text = "-" + Working_Pro.Label10.Text
						'Working_Pro.count = Working_Pro.count - inp_qty
						'Working_Pro.lb_qty_for_box.Text = Integer.Parse(Working_Pro.lb_qty_for_box.Text) - inp_qty
						'Working_Pro.LB_COUNTER_SHIP.Text -= inp_qty
						'Working_Pro.LB_COUNTER_SEQ.Text -= inp_qty
						'MsgBox("DATA3")
						Dim sum_prg As Integer = (Working_Pro.Label6.Text * 100) / Working_Pro.Label8.Text
						If sum_prg > 100 Then
							sum_prg = 100
						End If
						'MsgBox("DATA4")
						Working_Pro.CircularProgressBar1.Text = sum_prg & "%"
						Working_Pro.CircularProgressBar1.Value = sum_prg
						Dim start_time As Date = Working_Pro.st_count_ct.Text
						Dim pd As String = MainFrm.Label6.Text
						Dim line_cd As String = MainFrm.Label4.Text
						Dim wi_plan As String = Working_Pro.wi_no.Text
						Dim item_cd As String = Working_Pro.Label3.Text
						Dim item_name As String = Working_Pro.Label12.Text
						Dim staff_no As String = Working_Pro.Label29.Text
						Dim seq_no As String = Working_Pro.Label22.Text
						Dim prd_qty2 As Integer = inp_qty - (inp_qty * 2)
						Dim end_time As Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
						Dim use_timee As Double = "0"
						Dim tr_status As String = "0"
						Dim number_qty As Integer = Working_Pro.Label6.Text
						'Backoffice_model.insPrdDetail_sqlite(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty, start_time, end_time, use_timee, tr_status)
						Dim start_time2 As String = start_time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
						Dim end_time2 As String = end_time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
						Working_Pro.LB_COUNTER_SHIP.Text = CDbl(Val(Working_Pro.LB_COUNTER_SHIP.Text)) - Convert.ToInt32(TextBox1.Text)
						If CDbl(Val(Working_Pro.LB_COUNTER_SHIP.Text)) < 0 Then
							Working_Pro.LB_COUNTER_SHIP.Text = 0
						End If
						Working_Pro.LB_COUNTER_SEQ.Text = CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text)) - Convert.ToInt32(TextBox1.Text)
						If CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text)) < 0 Then
							Working_Pro.LB_COUNTER_SEQ.Text = 0
						End If
						Working_Pro.Label6.Text = CDbl(Val(Working_Pro.Label6.Text)) - Convert.ToInt32(TextBox1.Text)
						If CDbl(Val(Working_Pro.Label6.Text)) < 0 Then
							Working_Pro.Label6.Text = 0
						End If
						Try
							If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
								tr_status = "1"
								Dim sel_cd As Integer = Sel_defect_cd_nc.ListView2.SelectedIndices(0)
								NC_ID = Sel_defect_cd_nc.ListView2.Items(sel_cd).SubItems(0).Text
								Backoffice_model.insPrdDetail_sqlite_defact(pd, line_cd, wi_plan, item_cd, item_name, staff_no, stotal_qeq_no, prd_qty2, number_qty, start_time2, end_time2, use_timee, tr_status, "1", NC_ID)
								Backoffice_model.Insert_prd_detail_defact(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty2, start_time, end_time, use_timee, number_qty, tr_status, "1", NC_ID)
							Else
								Dim sel_cd As Integer = Sel_defect_cd_nc.ListView2.SelectedIndices(0)
								NC_ID = Sel_defect_cd_nc.ListView2.Items(sel_cd).SubItems(0).Text
								tr_status = "0"
								Backoffice_model.insPrdDetail_sqlite_defact(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty2, number_qty, start_time2, end_time2, use_timee, tr_status, "1", NC_ID)
								'MsgBox("Ping incompleted")
							End If
						Catch ex As Exception
							Dim sel_cd As Integer = Sel_defect_cd_nc.ListView2.SelectedIndices(0)
							NC_ID = Sel_defect_cd_nc.ListView2.Items(sel_cd).SubItems(0).Text
							tr_status = "0"
							Backoffice_model.insPrdDetail_sqlite_defact(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty2, number_qty, start_time2, end_time2, use_timee, tr_status, "1", NC_ID)
						End Try
						Working_Pro.lb_nc_qty.Text = CDbl(Val(Working_Pro.lb_nc_qty.Text)) + CDbl(Val(TextBox1.Text))
						Working_Pro.Enabled = True
						Dim Drift_total_qty As Integer = TextBox1.Text
goto_recheck_data:
						Drift_total_qty = CDbl(Val(Drift_total_qty)) - CDbl(Val(Working_Pro.Label27.Text))
						If Drift_total_qty >= 0 Then
							Dim result_mod As Double = Integer.Parse(Working_Pro.lb_nc_qty.Text) Mod Integer.Parse(Working_Pro.Label27.Text)
							If result_mod = 0 And CDbl(Val(Working_Pro.Label10.Text)) <> 0 Then
								Dim LB_PART_NO As String = Working_Pro.Label3.Text
								Dim LB_PART_NAME As String = Working_Pro.Label12.Text
								Dim LB_MODEL As String = Working_Pro.lb_model.Text
								Dim LB_LOT As String = Working_Pro.Label18.Text
								Dim LB_COUNTBOX As String = "1"
								Dim LB_SNP As String = Working_Pro.Label27.Text
								Dim LB_Hide_QR_FA_SCAN As String = ""
								Dim max_box As String = "      "
								Dim QR_PRODUCT_SCAN As String = "QR_SCAN"
								Dim result_seq_ng As String = "QR_SCAN"
								Dim para_shift As String = Working_Pro.Label14.Text
								Dim LB_STATUS As String = "NC"
								Dim lot_no As String = Working_Pro.Label18.Text
								Dim SEQ As String = Working_Pro.Label22.Text
								Dim LINE_CD2 As String = Working_Pro.Label24.Text
								Dim date_now As Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
								Dim load_qty = Backoffice_model.load_qty_defact(item_cd, lot_no, SEQ, date_now)
								Dim seq_defact As String = ""
								While load_qty.read()
									seq_defact = load_qty("SEQ_DEFACT").ToString
								End While
								load_qty.close()
								If seq_defact = "" Then
									seq_defact = 999
								Else
									seq_defact = CDbl(Val(seq_defact)) - 1
								End If
								tag_defact_qr = LB_Hide_QR_FA_SCAN
								'Dim year As String = LB_Hide_QR_FA_SCAN.Substring(8, 4)
								'Dim mouth As String = LB_Hide_QR_FA_SCAN.Substring(12, 2)
								'Dim day As String = LB_Hide_QR_FA_SCAN.Substring(14, 2)
								WASHING_DATE_NG = DateTime.Now.ToString("yyyy/MM/dd")
								'WASHING_DATE_NG = Year() & "/" & mouth & "/" & Day
								'MsgBox("default = " & LB_Hide_QR_FA_SCAN)
								Dim iden_cd As String
								If MainFrm.Label6.Text = "K1PD01" Then
									iden_cd = "GA"
								Else
									iden_cd = "GB"
								End If
								Dim act_date As String
								Dim actdateConv As Date = DateTime.Now.ToString("dd/MM/yyyy")
								act_date = Format(actdateConv, "yyyyMMdd")
								Dim part_no_res1 As String
								Dim part_no_res As String
								Dim part_numm As Integer = 0
								For part_numm = Working_Pro.Label3.Text.Length To 24
									part_no_res = part_no_res & " "
								Next part_numm
								part_no_res1 = Working_Pro.Label3.Text & part_no_res
								Dim qty_num As String
								Dim num_char_qty As Integer
								num_char_qty = Working_Pro.lb_qty_for_box.Text.Length
								If num_char_qty = 1 Then
									qty_num = "     " & LB_SNP
								ElseIf num_char_qty = 2 Then
									qty_num = "    " & LB_SNP
								ElseIf num_char_qty = 3 Then
									qty_num = "   " & LB_SNP
								ElseIf num_char_qty = 4 Then
									qty_num = "  " & LB_SNP
								ElseIf num_char_qty = 5 Then
									qty_num = " " & LB_SNP
								Else
									qty_num = LB_SNP
								End If
								Dim plan_cd As String
								Dim factory_cd As String
								If MainFrm.Label6.Text = "K2PD06" Then
									factory_cd = "Phase8"
									plan_cd = "52"
								Else
									factory_cd = "Phase10"
									plan_cd = "51"
								End If
								Dim cus_part_no As String = "                         "
								Dim seq_con As String
								If Len(Working_Pro.Label22.Text) = 1 Then
									seq_con = "00" & Working_Pro.Label22.Text
								ElseIf Len(Working_Pro.Label22.Text) = 2 Then
									seq_con = "0" & Working_Pro.Label22.Text
								ElseIf Len(Working_Pro.Label22.Text) = 3 Then
									seq_con = Working_Pro.Label22.Text
								End If

								Print_Defact.Set_parameter_print(LB_PART_NO, LB_PART_NAME, LB_MODEL, LB_LOT, seq_defact, LB_SNP, LB_Hide_QR_FA_SCAN, max_box, QR_PRODUCT_SCAN, result_seq_ng, para_shift, LB_STATUS)
								Backoffice_model.insert_lot_print_defact(wi_plan, item_cd, lot_no, seq_defact, date_now, LB_PART_NAME, tag_defact_qr, "1", LINE_CD2)

							End If
							GoTo goto_recheck_data
						Else
							Sel_defect_cd_nc.Close()
							Me.Close()
						End If
					Catch ex As Exception
						MsgBox("Please input Qty." & ex.Message)
					End Try
				End If
			Else
				MsgBox("QTY OVER SHIFT COUNT")
			End If
		Else
			MsgBox("QTY OVER SHIFT COUNT")
		End If
	End Sub
	Public Sub cal_qty()
		Dim want_del As Integer = CDbl(Val(TextBox1.Text))
		Dim qty As Integer = 0
		Dim seq As Integer = CDbl(Val(Working_Pro.Label22.Text))
		Dim App_qty_seq As Integer = Working_Pro.LB_COUNTER_SEQ.Text
		Dim App_qty_ship As Integer = Working_Pro.LB_COUNTER_SHIP.Text
		Dim qty_update As Integer = 0
		Dim tmp_qty_update As Integer = 0
		Dim check_qty_frith = CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text)) - want_del
		Dim tmp_del3 As Integer = CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text))
		If CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text)) = 0 Then
			seq = CDbl(Val(Working_Pro.Label22.Text)) - 1
		Else
			seq = CDbl(Val(Working_Pro.Label22.Text))
		End If
		For number_seq As Integer = seq To 1 Step -1
			Try
				If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
					reader = Backoffice_model.GET_QTY_SEQ_ACTUAL_DESC(Working_Pro.wi_no.Text, Working_Pro.Label14.Text, number_seq)
				Else
					reader = Backoffice_model.GET_QTY_SEQ_ACTUAL_DESC_SQLITE(Working_Pro.wi_no.Text, Working_Pro.Label14.Text, number_seq)
				End If
			Catch ex As Exception
				reader = Backoffice_model.GET_QTY_SEQ_ACTUAL_DESC_SQLITE(Working_Pro.wi_no.Text, Working_Pro.Label14.Text, number_seq)
			End Try
			If App_qty_seq > 0 And check_qty_frith >= 0 Then 'เดินแผนไปแล้ว
				' MsgBox("condition 01")
				App_qty_seq = CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text)) - want_del
				App_qty_ship = CDbl(Val(Working_Pro.LB_COUNTER_SHIP.Text)) - want_del
				If CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text)) = want_del Then
					Working_Pro.LB_COUNTER_SEQ.Text = 0
					App_qty_seq = 0
					GoTo break_loop
				End If
				If App_qty_seq < 0 Then
					Working_Pro.LB_COUNTER_SEQ.Text = 0
					App_qty_seq = 0
				End If
				'End If
			ElseIf App_qty_seq = 0 And App_qty_ship > 0 Then 'ยังไม่ได้เดินแผน แต่มี qty seq เก่าค้างอยู่
				'MsgBox("condition 02")
				While reader.read()
					'    MsgBox("SEQ = " & number_seq & " : LOAD_QTY = " & reader("QTY_ACTUAL").ToString())
					qty = reader("QTY_ACTUAL").ToString()
				End While
				reader.close()
				If qty > 0 Then
					qty_update = want_del - qty
					' App_qty_seq = CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text)) - want_del
					' App_qty_ship = CDbl(Val(Working_Pro.LB_COUNTER_SHIP.Text)) - want_del
					tmp_qty_update = qty_update
					want_del = qty_update
					'    MsgBox("want_del = " & want_del)
					'   MsgBox("App_qty_seq = " & App_qty_seq)
					If qty_update > 0 Then
						'  MsgBox("IF TOP")
						If want_del > 0 Then
							tmp_qty_update = 0
						End If
						Try
							If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
								tr_status = "1"
								Dim update_qty_seq = Backoffice_model.update_qty_seq(Working_Pro.wi_no.Text, number_seq, tmp_qty_update)
								update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq, tmp_qty_update, tr_status)
							Else
								tr_status = "0"
								Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq, tmp_qty_update, tr_status)
							End If
						Catch ex As Exception
							Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq, tmp_qty_update, tr_status)
						End Try
					End If
					If qty_update <= 0 Then
						' MsgBox("IF DOWN")
						Try
							If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
								tr_stataus = "1"
								Dim update_qty_seq = Backoffice_model.update_qty_seq(Working_Pro.wi_no.Text, number_seq, Math.Abs(qty_update))
								update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq, Math.Abs(qty_update), tr_stataus)
							Else
								tr_stataus = "0"
								Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq, Math.Abs(qty_update), tr_status)
							End If
						Catch ex As Exception
							Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq, Math.Abs(qty_update), tr_status)
						End Try
						GoTo break_loop
					End If
				End If
			ElseIf App_qty_seq > 0 And App_qty_ship > 0 Then
				'  MsgBox("condition 03")
				Dim temp_data_seq_3 As Integer = 0
				temp_data_seq_3 = tmp_del3 'CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text))
				'App_qty_ship = CDbl(Val(Working_Pro.LB_COUNTER_SHIP.Text)) - want_del
				If number_seq = Working_Pro.Label22.Text Then
					want_del = want_del - temp_data_seq_3
				End If
				tmp_del3 = want_del
				If App_qty_seq < 0 Then
					Working_Pro.LB_COUNTER_SEQ.Text = 0
					App_qty_seq = 0
				End If
				Try
					If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
						reader = Backoffice_model.GET_QTY_SEQ_ACTUAL_DESC(Working_Pro.wi_no.Text, Working_Pro.Label14.Text, number_seq - 1)
					Else
						reader = Backoffice_model.GET_QTY_SEQ_ACTUAL_DESC_SQLITE(Working_Pro.wi_no.Text, Working_Pro.Label14.Text, number_seq - 1)
					End If
				Catch ex As Exception
					reader = Backoffice_model.GET_QTY_SEQ_ACTUAL_DESC_SQLITE(Working_Pro.wi_no.Text, Working_Pro.Label14.Text, number_seq - 1)
				End Try
				While reader.read()
					' MsgBox("SEQ = " & number_seq - 1 & " : LOAD_QTY = " & reader("QTY_ACTUAL").ToString())
					qty = reader("QTY_ACTUAL").ToString()
				End While
				reader.close()
				If qty > 0 Then
					'  MsgBox("reulst tmp_del3 = " & tmp_del3)
					qty_update = tmp_del3 - qty
					' MsgBox("reulst qty_update = " & qty_update)
					' App_qty_seq = CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text)) - want_del
					' App_qty_ship = CDbl(Val(Working_Pro.LB_COUNTER_SHIP.Text)) - want_del
					tmp_qty_update = qty_update
					want_del = qty_update
					If qty_update > 0 Then
						' MsgBox("IF TOP3 ")
						If tmp_del3 > 0 Then
							tmp_qty_update = 0
						End If
						Try
							If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
								tr_status = "0"
								Dim update_qty_seq = Backoffice_model.update_qty_seq(Working_Pro.wi_no.Text, number_seq - 1, tmp_qty_update)
								update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq - 1, tmp_qty_update, tr_status)
							Else
								tr_status = "0"
								Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq - 1, tmp_qty_update, tr_status)
							End If
						Catch ex As Exception
							tr_status = "0"
							Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq - 1, tmp_qty_update, tr_status)
						End Try
					End If
					If qty_update <= 0 Then
						' MsgBox("IF DOWN3 ")
						Try
							If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
								tr_status = "1"
								Dim update_qty_seq = Backoffice_model.update_qty_seq(Working_Pro.wi_no.Text, number_seq - 1, Math.Abs(qty_update))
								update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq - 1, Math.Abs(qty_update), tr_status)
							Else
								tr_status = "0"
								Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq - 1, Math.Abs(qty_update), tr_status)
							End If
						Catch ex As Exception
							tr_status = "0"
							Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq - 1, Math.Abs(qty_update), tr_status)
						End Try
						GoTo break_loop
					End If
				End If
			End If
		Next
break_loop:
	End Sub
	Private Sub Defect_nc_adj_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		TextBox1.Enabled = False
		Dim cal_nc As Integer = CDbl(Val(Working_Pro.Label6.Text)) '- CDbl(Val(Working_Pro.lb_nc_qty.Text))
		If cal_nc <= 0 Then
			Label1.Text = 0
		ElseIf cal_nc > 0 Then
			Label1.Text = cal_nc
		End If
	End Sub
End Class
