Imports System
Imports System.ComponentModel
Imports System.Threading
Imports System.IO.Ports
Imports System.Net
Imports System.IO
Public Class sc_wi_plan
	Dim myPort As Array
	Delegate Sub SetTextCallback(ByVal [text] As String)
	Dim check_comp_flg As String = "0"
	Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown

		If e.KeyCode = Keys.Enter Then
			If TextBox1.Text.Length > 10 Then
				'lb_text_wi_seq.Text = TextBox1.Text
				Working_Pro.lb_ref_scan.Text = TextBox1.Text
				Try
					If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
						Dim LoadSQLite = Backoffice_model.Check_sc_inc_dup(lb_text_wi_seq.Text)
						Dim numq As Integer = 0

						While LoadSQLite.Read()
							numq = numq + 1
						End While
						Dim wi_cd As String = TextBox1.Text.Substring(0, 10)
						Dim line_cd As String = MainFrm.Label4.Text

						Dim line_cd_chk As String
						'MsgBox(TextBox1.Text.Length - 13)

						Dim temp_qty As String = TextBox1.Text.Substring(13, (TextBox1.Text.Length - 13))
						'MsgBox(wi_cd)
						'MsgBox(line_cd)

						Dim LoadSQL = Backoffice_model.get_wi_plan_fromsc(wi_cd)
						'MsgBox(LoadSQL("WI").Read().Count())

						Dim numberOfindex As Integer = 0
						While LoadSQL.Read()
							Prd_detail.lb_item_cd.Text = LoadSQL("ITEM_CD").ToString()
							Prd_detail.lb_item_name.Text = LoadSQL("ITEM_NAME").ToString()
							line_cd_chk = LoadSQL("LINE_CD").ToString()
							Prd_detail.lb_model.Text = LoadSQL("MODEL").ToString()
							Prd_detail.lb_plan_qty.Text = LoadSQL("QTY").ToString()
							Prd_detail.lb_remain_qty.Text = LoadSQL("remain_qty").ToString()
							Prd_detail.lb_wi.Text = LoadSQL("WI").ToString()
							check_comp_flg = LoadSQL("PRD_COMP_FLG").ToString()
							numberOfindex = numberOfindex + 1
							'Insert_list.ListView1.Items(0).ForeColor = Color.Red
							'Insert_list.ListView1.ForeColor = Color.Red
							'Insert_list.ListView1.Items.Add(LoadSQL("WI").ToString()).SubItems.AddRange(New String() {LoadSQL("ITEM_CD").ToString(), LoadSQL("ITEM_NAME").ToString(), LoadSQL("QTY").ToString(), LoadSQL("remain_qty").ToString()})
							'line_id.Text = LoadSQL("line_id").ToString()
						End While
						If line_cd = line_cd_chk Then
                            'Try
                            'Dim webClient As New System.Net.WebClient
                            'MsgBox("succ")
                            'Catch ex As Exception
                            'MsgBox("fail")
                            'End Try
                            If check_comp_flg = "9" Then
								MsgBox("WI COMPLETED")
								TextBox1.Text = ""
							Else
								MainFrm.Hide()
								Prd_detail.Show()
								Working_Pro.count = temp_qty
								Working_Pro.lb_qty_for_box.Text = temp_qty
								MainFrm.Enabled = True
								closeForm()
							End If

						Else
							MsgBox("Please scan tag in " & line_cd)
							TextBox1.Text = ""
						End If
					Else
						load_show.Show()
					End If
				Catch ex As Exception
					load_show.Show()
				End Try
			Else
				MsgBox("Please scan incomplete tag")
				TextBox1.Text = ""
			End If
		End If

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		MainFrm.Enabled = True
		Me.Close()

	End Sub

	Private Sub sc_wi_plan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		myPort = IO.Ports.SerialPort.GetPortNames()

		Dim sc_type As String = MainFrm.lb_scanner_port.Text

		'MsgBox(sc_type)

		If sc_type = "USB" Then
			'MsgBox("Data")
		Else
			Try
				SerialPort1.PortName = sc_type
				SerialPort1.BaudRate = 9600
				SerialPort1.Parity = IO.Ports.Parity.None
				SerialPort1.StopBits = IO.Ports.StopBits.One
				SerialPort1.DataBits = 8
				SerialPort1.Open()
			Catch ex As Exception
				MsgBox("Please check the USB cable or contact administrator!")
			End Try
		End If
	End Sub

	Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
		ReceivedText(SerialPort1.ReadExisting())

	End Sub

	Private Sub ReceivedText(ByVal [text] As String)
		If Me.TextBox1.InvokeRequired Then
			Dim x As New SetTextCallback(AddressOf ReceivedText)
			Me.Invoke(x, New Object() {(text)})
		Else
			'MsgBox(MainFrm.lb_ctrl_sc_flg.Text)

			Me.TextBox1.Text &= [text]

			If TextBox1.Text.Length >= 15 Then
				'MsgBox("scanned")
				'lb_text_wi_seq.Text = TextBox1.Text
				Working_Pro.lb_ref_scan.Text = TextBox1.Text
				Dim LoadSQLite = Backoffice_model.Check_sc_inc_dup(TextBox1.Text)
				Dim txt_val As String = ""
				While LoadSQLite.Read()
					txt_val = LoadSQLite("id").ToString()
				End While
				If txt_val IsNot "" Then
					MsgBox("You're already scan this tag! Please contact leader.")
				Else
					Dim wi_cd As String = TextBox1.Text.Substring(0, 10)
					Dim line_cd As String = MainFrm.Label4.Text
					Dim line_cd_chk As String
					Dim temp_qty As String
					Try
						temp_qty = TextBox1.Text.Substring(13, (TextBox1.Text.Length - 13))
					Catch ex As Exception
						temp_qty = "0"
					End Try
					Dim LoadSQL = Backoffice_model.get_wi_plan_fromsc(wi_cd)
					Dim numberOfindex As Integer = 0
					Dim check_flg_wi As String = ""
					While LoadSQL.Read()
						Prd_detail.lb_item_cd.Text = LoadSQL("ITEM_CD").ToString()
						Prd_detail.lb_item_name.Text = LoadSQL("ITEM_NAME").ToString()
						line_cd_chk = LoadSQL("LINE_CD").ToString()
						Prd_detail.lb_model.Text = LoadSQL("MODEL").ToString()
						Prd_detail.lb_plan_qty.Text = LoadSQL("QTY").ToString()
						Prd_detail.lb_remain_qty.Text = LoadSQL("remain_qty").ToString()
						Prd_detail.lb_wi.Text = LoadSQL("WI").ToString()
						check_flg_wi = LoadSQL("PRD_COMP_FLG").ToString()
						numberOfindex = numberOfindex + 1
					End While
					If line_cd = line_cd_chk Then
						If check_flg_wi = "0" Then
                            'Try
                            'Dim webclient As New System.Net.WebClient
                            'msgbox("succ")
                            'Catch ex As Exception
                            'msgbox("fail")
                            'End Try
                            MainFrm.Hide()
							Prd_detail.Show()
							TextBox1.Clear()
							Working_Pro.count = temp_qty.Substring(0, (temp_qty.Length - 1))
							Working_Pro.lb_qty_for_box.Text = temp_qty.Substring(0, (temp_qty.Length - 1))
							MainFrm.Enabled = True
							Me.Hide()
						Else
							MsgBox("แผนนี้เดินครบแผนไปแล้ว")
						End If

					Else
						MsgBox("please scan tag in " & line_cd)
					End If
				End If
				TextBox1.Clear()
			End If
		End If
	End Sub



	Private Sub sc_wi_plan_Closing(ByVal sender As Object,
		   ByVal e As ComponentModel.CancelEventArgs) Handles MyBase.Closing
		If SerialPort1.IsOpen Then SerialPort1.Close()
	End Sub

	Sub closeForm()
		If Me.InvokeRequired Then
			Me.Invoke(New MethodInvoker(AddressOf Me.closeForm))
		Else
			Me.Close()
		End If
	End Sub

	Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
		If TextBox1.Text.Length >= 15 Then '17 or the number of characters your scanner gets. 
			'MsgBox("scanned")
			'TextBox1.Clear()
		Else
			'If TextBox1.Text.Length <> 0 Then TextBox1.Clear()
		End If
	End Sub

	Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
		MainFrm.Enabled = True
		Me.Close()
	End Sub
End Class
