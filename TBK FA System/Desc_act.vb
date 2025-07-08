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
Public Class Desc_act
	Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
		Working_Pro.Enabled = True
		Me.Close()
	End Sub
	Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

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
                    If reader("QTY_ACTUAL").ToString() <> "" Then
                        qty = reader("QTY_ACTUAL").ToString()
                    End If
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
                        '  MsgBox("If TOP")
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
                            tr_status = "0"
                            Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq, tmp_qty_update, tr_status)
                        End Try
                    End If
                    If qty_update <= 0 Then
                        ' MsgBox("If DOWN")
                        Try
                            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                                tr_status = "1"
                                Dim update_qty_seq = Backoffice_model.update_qty_seq(Working_Pro.wi_no.Text, number_seq, Math.Abs(qty_update))
                                update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq, Math.Abs(qty_update), tr_status)
                            Else
                                tr_status = "0"
                                Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq, Math.Abs(qty_update), tr_status)
                            End If
                        Catch ex As Exception
                            tr_status = "0"
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
                    ' MsgBox("SEQ = " & number_seq - 1 & "  LOAD_QTY = " & reader("QTY_ACTUAL").ToString())
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
                        ' MsgBox("If TOP3 ")
                        If tmp_del3 > 0 Then
                            tmp_qty_update = 0
                        End If
                        Try
                            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                                tr_status = "1"
                                Dim update_qty_seq = Backoffice_model.update_qty_seq(Working_Pro.wi_no.Text, number_seq - 1, tmp_qty_update)
                                update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq - 1, tmp_qty_update, tr_status)
                            Else
                                Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq - 1, tmp_qty_update, tr_status)
                            End If
                        Catch ex As Exception
                            Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(Working_Pro.wi_no.Text, number_seq - 1, tmp_qty_update, tr_status)
                        End Try
                    End If
                    If qty_update <= 0 Then
                        ' MsgBox("If DOWN3 ")
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
    Public Sub cal_qty_special()
        Dim want_del As Integer = CDbl(Val(TextBox1.Text))
        Dim qty As Integer = 0
        Dim App_qty_seq As Integer = Working_Pro.LB_COUNTER_SEQ.Text
        Dim App_qty_ship As Integer = Working_Pro.LB_COUNTER_SHIP.Text
        Dim qty_update As Integer = 0
        Dim tmp_qty_update As Integer = 0
        Dim check_qty_frith = CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text)) - want_del
        Dim tmp_del3 As Integer = CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text))
        Dim GenSEQ As Integer = CDbl(Val(Working_Pro.Label22.Text)) - 5
        Dim Iseq = GenSEQ
        Dim j As Integer = 0
        For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
            Iseq = Iseq + 1
            Dim special_wi As String = itemPlanData.wi
            Dim special_item_cd As String = itemPlanData.item_cd
            Dim special_item_name As String = itemPlanData.item_name
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    reader = Backoffice_model.GET_QTY_SEQ_ACTUAL_DESC(special_wi, Working_Pro.Label14.Text, Iseq)
                Else
                    reader = Backoffice_model.GET_QTY_SEQ_ACTUAL_DESC_SQLITE(special_wi, Working_Pro.Label14.Text, Iseq)
                End If
            Catch ex As Exception
                reader = Backoffice_model.GET_QTY_SEQ_ACTUAL_DESC_SQLITE(special_wi, Working_Pro.Label14.Text, Iseq)
            End Try
            If App_qty_seq > 0 And check_qty_frith >= 0 Then 'เดินแผนไปแล้ว
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
            ElseIf App_qty_seq = 0 And App_qty_ship > 0 Then 'ยังไม่ได้เดินแผน แต่มี qty seq เก่าค้างอยู่
                While reader.read()
                    If reader("QTY_ACTUAL").ToString() <> "" Then
                        qty = reader("QTY_ACTUAL").ToString()
                    End If
                End While
                reader.close()
                If qty > 0 Then
                    qty_update = want_del - qty
                    tmp_qty_update = qty_update
                    want_del = qty_update
                    If qty_update > 0 Then
                        If want_del > 0 Then
                            tmp_qty_update = 0
                        End If
                        Try
                            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                                tr_status = "1"
                                Dim update_qty_seq = Backoffice_model.update_qty_seq(special_wi, Iseq, tmp_qty_update)
                                update_qty_seq = Backoffice_model.update_qty_seq_sqlite(special_wi, Iseq, tmp_qty_update, tr_status)
                            Else
                                tr_status = "0"
                                Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(special_wi, Iseq, tmp_qty_update, tr_status)
                            End If
                        Catch ex As Exception
                            tr_status = "0"
                            Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(special_wi, Iseq, tmp_qty_update, tr_status)
                        End Try
                    End If
                    If qty_update <= 0 Then
                        Try
                            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                                tr_status = "1"
                                Dim update_qty_seq = Backoffice_model.update_qty_seq(special_wi, Iseq, Math.Abs(qty_update))
                                update_qty_seq = Backoffice_model.update_qty_seq_sqlite(special_wi, Iseq, Math.Abs(qty_update), tr_status)
                            Else
                                tr_status = "0"
                                Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(special_wi, Iseq, Math.Abs(qty_update), tr_status)
                            End If
                        Catch ex As Exception
                            tr_status = "0"
                            Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(special_wi, Iseq, Math.Abs(qty_update), tr_status)
                        End Try
                        GoTo break_loop
                    End If
                End If
            ElseIf App_qty_seq > 0 And App_qty_ship > 0 Then
                Dim temp_data_seq_3 As Integer = 0
                temp_data_seq_3 = tmp_del3 'CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text))
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
                        reader = Backoffice_model.GET_QTY_SEQ_ACTUAL_DESC(special_wi, Working_Pro.Label14.Text, Iseq)
                    Else
                        reader = Backoffice_model.GET_QTY_SEQ_ACTUAL_DESC_SQLITE(special_wi, Working_Pro.Label14.Text, Iseq)
                    End If
                Catch ex As Exception
                    reader = Backoffice_model.GET_QTY_SEQ_ACTUAL_DESC_SQLITE(special_wi, Working_Pro.Label14.Text, Iseq)
                End Try
                While reader.read()
                    qty = reader("QTY_ACTUAL").ToString()
                End While
                reader.close()
                If qty > 0 Then
                    qty_update = tmp_del3 - qty
                    tmp_qty_update = qty_update
                    want_del = qty_update
                    If qty_update > 0 Then
                        If tmp_del3 > 0 Then
                            tmp_qty_update = 0
                        End If
                        Try
                            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                                tr_status = "1"
                                Dim update_qty_seq = Backoffice_model.update_qty_seq(special_wi, Iseq, tmp_qty_update)
                                update_qty_seq = Backoffice_model.update_qty_seq_sqlite(special_wi, Iseq, tmp_qty_update, tr_status)
                            Else
                                Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(special_wi, Iseq, tmp_qty_update, tr_status)
                            End If
                        Catch ex As Exception
                            Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(special_wi, Iseq, tmp_qty_update, tr_status)
                        End Try
                    End If
                    If qty_update <= 0 Then
                        Try
                            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                                tr_status = "1"
                                Dim update_qty_seq = Backoffice_model.update_qty_seq(special_wi, Iseq, Math.Abs(qty_update))
                                update_qty_seq = Backoffice_model.update_qty_seq_sqlite(special_wi, Iseq, Math.Abs(qty_update), tr_status)
                            Else
                                tr_status = "0"
                                Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(special_wi, Iseq, Math.Abs(qty_update), tr_status)
                            End If
                        Catch ex As Exception
                            tr_status = "0"
                            Dim update_qty_seq = Backoffice_model.update_qty_seq_sqlite(special_wi, Iseq, Math.Abs(qty_update), tr_status)
                        End Try
                        GoTo break_loop
                    End If
                End If
            End If
break_loop:
        Next
    End Sub
    Public Function set_data_del()
		Dim prd_qty As Integer = Convert.ToInt32(Label1.Text)
        Dim inp_qty As Integer
        Dim result As Integer = 0
        '    MsgBox("M0")
        Dim md As New modelDefect()
        Working_Pro._Edit_Up_0.Text = "0"
        Try
            inp_qty = Convert.ToInt32(TextBox1.Text)
        Catch ex As Exception
            ' inp_qty = 0
            PictureBox3.Enabled = False
            PictureBox2.Enabled = False
            Dim listdetail = "Please Input QTY."
            PictureBox10.BringToFront()
            PictureBox10.Show()
            PictureBox1.BringToFront()
            PictureBox1.Show()
            Panel2.BringToFront()
            Panel2.Show()
            Label2.Text = listdetail
            Label2.BringToFront()
            Label2.Show()
            'MsgBox("Can't input the Qty. over : " + Label1.Text)
            TextBox1.Clear()
            Return 0
        End Try
        Dim totalDefect As Integer = (CDbl(Val(Working_Pro.lb_nc_qty.Text)) + CDbl(Val(Working_Pro.lb_ng_qty.Text))) 'CDbl(Val(md.mGetDefect(Working_Pro.wi_no.Text, Working_Pro.Label18.Text, Working_Pro.Label14.Text) + (CDbl(Val(Working_Pro.lb_nc_qty.Text)) + CDbl(Val(Working_Pro.lb_ng_qty.Text)))))
        Dim totalDefectCP As Integer = (CDbl(Val(Working_Pro.lb_nc_child_part.Text)) + CDbl(Val(Working_Pro.lb_ng_child_part.Text)))
        'If inp_qty > (prd_qty - totalDefect) Then
        ' MsgBox("M6")
        Dim totalDefectAll As Integer = totalDefect
        If inp_qty > (CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text) - (totalDefectAll))) Then
            PictureBox3.Enabled = False
            PictureBox2.Enabled = False
            Dim listdetail = "Can't input the Qty. over : " & Label1.Text & " And Have Defect : " & totalDefectAll
            PictureBox10.BringToFront()
            PictureBox10.Show()
            PictureBox1.BringToFront()
            PictureBox1.Show()
            Panel2.BringToFront()
            Panel2.Show()
            Label2.Text = listdetail
            Label2.BringToFront()
            Label2.Show()
            'MsgBox("Can't input the Qty. over : " & Label1.Text & " And Have Defect : " & totalDefect)
            TextBox1.Clear()
        Else
            If CDbl(Val(Working_Pro.LB_COUNTER_SHIP.Text)) > 0 Then
                Working_Pro._Edit_Up_0.Text = Working_Pro.LB_COUNTER_SHIP.Text
                If CDbl(Val(TextBox1.Text)) <= CDbl(Val(Label1.Text)) Then
                    If MainFrm.chk_spec_line = "2" Then
                        cal_qty_special()
                    Else
                        cal_qty()
                    End If
                    Working_Pro.flg_tag_print = 0
                    Working_Pro.LB_COUNTER_SHIP.Text -= inp_qty
                    Working_Pro.lb_good.Text -= inp_qty
                    ' MsgBox("M1")
                    Working_Pro.LB_COUNTER_SEQ.Text -= inp_qty
                    Dim yearNow As Integer = DateTime.Now.ToString("yyyy")
                    Dim monthNow As Integer = DateTime.Now.ToString("MM")
                    Dim dayNow As Integer = DateTime.Now.ToString("dd")
                    Dim hourNow As Integer = DateTime.Now.ToString("HH")
                    Dim minNow As Integer = DateTime.Now.ToString("mm")
                    Dim secNow As Integer = DateTime.Now.ToString("ss")
                    ' MsgBox("M2")
                    Dim yearSt As Integer = Working_Pro.st_time.Text.Substring(0, 4)
                    Dim monthSt As Integer = Working_Pro.st_time.Text.Substring(5, 2)
                    Dim daySt As Integer = Working_Pro.st_time.Text.Substring(8, 2)
                    Dim hourSt As Integer = Working_Pro.st_time.Text.Substring(11, 2)
                    Dim minSt As Integer = Working_Pro.st_time.Text.Substring(14, 2)
                    Dim secSt As Integer = Working_Pro.st_time.Text.Substring(17, 2)
                    Dim firstDate As New System.DateTime(yearSt, monthSt, daySt, hourSt, minSt, secSt)
                    Dim secondDate As New System.DateTime(yearNow, monthNow, dayNow, hourNow, minNow, secNow)
                    ' MsgBox("M3")
                    Dim diff As System.TimeSpan = secondDate.Subtract(firstDate)
                    Dim diff1 As System.TimeSpan = secondDate - firstDate
                    Dim diff2 As String = (secondDate - firstDate).TotalSeconds.ToString()
                    Dim actCT As Double = Format(diff2 / 60, "0.00")
                    Dim cnt_btn As Integer = Integer.Parse(MainFrm.cavity.Text)
                    Dim snp As Integer = Integer.Parse(Working_Pro.Label27.Text)
                    '  MsgBox("M4")
                    Working_Pro._Edit_Up_0.Text = Integer.Parse(Working_Pro._Edit_Up_0.Text) - inp_qty
                    Working_Pro.Label6.Text = Integer.Parse(Working_Pro.Label6.Text) - inp_qty
                    Try
                        Working_Pro.Label10.Text = Integer.Parse(Working_Pro.Label10.Text.Substring(1)) + inp_qty
                    Catch ex As Exception
                        Working_Pro.Label10.Text = Integer.Parse(Working_Pro.Label10.Text) + inp_qty
                        ' Working_Pro.start_flg = 1
                        ' Working_Pro.comp_flg = 0
                    End Try
                    Working_Pro.Label10.Text = "-" + Working_Pro.Label10.Text
                    '   MsgBox("M5")
                    Working_Pro.count = Working_Pro.count - inp_qty
                    Working_Pro.lb_qty_for_box.Text = Integer.Parse(Working_Pro.lb_qty_for_box.Text) - inp_qty
                    If Working_Pro.lb_qty_for_box.Text < 0 Then
                        Working_Pro.lb_qty_for_box.Text = "0"
                    End If
                    Dim sum_prg As Integer = (Working_Pro.Label6.Text * 100) / Working_Pro.Label8.Text
                    If sum_prg > 100 Then
                        sum_prg = 100
                    End If
                    ' Working_Pro.CircularProgressBar1.Text = sum_prg & "%"
                    ' Working_Pro.CircularProgressBar1.Value = sum_prg
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
                    Try
                        If Working_Pro.LB_COUNTER_SEQ.Text.Substring(0, 1) = "-" Then
                            Working_Pro.LB_COUNTER_SEQ.Text = 0
                        End If
                        If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                            tr_status = "1"
                            If MainFrm.chk_spec_line = "2" Then
                                Dim GenSEQ As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                                Dim Iseq = GenSEQ
                                Dim j As Integer = 0
                                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                    Iseq += 1
                                    Dim special_wi As String = itemPlanData.wi
                                    Dim special_item_cd As String = itemPlanData.item_cd
                                    Dim special_item_name As String = itemPlanData.item_name
                                    Backoffice_model.insPrdDetail_sqlite(pd, line_cd, special_wi, special_item_cd, special_item_name, staff_no, Iseq, prd_qty2, number_qty, start_time2, end_time2, use_timee, tr_status, Working_Pro.Spwi_id(j))
                                    Backoffice_model.Insert_prd_detail(pd, line_cd, special_wi, special_item_cd, special_item_name, staff_no, Iseq, prd_qty2, start_time, end_time, use_timee, number_qty, Working_Pro.Spwi_id(j), tr_status)
                                    j = j + 1
                                Next
                            Else
                                Backoffice_model.insPrdDetail_sqlite(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty2, number_qty, start_time2, end_time2, use_timee, tr_status, Working_Pro.pwi_id)
                                Backoffice_model.Insert_prd_detail(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty2, start_time, end_time, use_timee, number_qty, Working_Pro.pwi_id, tr_status)
                            End If
                        Else
                            tr_status = "0"
                            If MainFrm.chk_spec_line = "2" Then
                                Dim GenSEQ As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                                Dim Iseq = GenSEQ
                                Dim j As Integer = 0
                                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                    Iseq += 1
                                    Dim special_wi As String = itemPlanData.wi
                                    Dim special_item_cd As String = itemPlanData.item_cd
                                    Dim special_item_name As String = itemPlanData.item_name
                                    Backoffice_model.insPrdDetail_sqlite(pd, line_cd, special_wi, special_item_cd, special_item_name, staff_no, Iseq, prd_qty2, number_qty, start_time2, end_time2, use_timee, tr_status, Working_Pro.Spwi_id(j))
                                    j = j + 1
                                Next
                            Else
                                Backoffice_model.insPrdDetail_sqlite(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty2, number_qty, start_time2, end_time2, use_timee, tr_status, Working_Pro.pwi_id)
                            End If
                        End If
                    Catch ex As Exception
                        tr_status = "0"
                        If MainFrm.chk_spec_line = "2" Then
                            Dim GenSEQ As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                            Dim Iseq = GenSEQ
                            Dim j As Integer = 0
                            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                                Iseq += 1
                                Dim special_wi As String = itemPlanData.wi
                                Dim special_item_cd As String = itemPlanData.item_cd
                                Dim special_item_name As String = itemPlanData.item_name
                                Backoffice_model.insPrdDetail_sqlite(pd, line_cd, special_wi, special_item_cd, special_item_name, staff_no, Iseq, prd_qty2, number_qty, start_time2, end_time2, use_timee, tr_status, Working_Pro.Spwi_id(j))
                                j = j + 1
                            Next
                        Else
                            Backoffice_model.insPrdDetail_sqlite(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty2, number_qty, start_time2, end_time2, use_timee, tr_status, Working_Pro.pwi_id)
                        End If
                    End Try
                    Working_Pro.cal_eff()
                    Working_Pro.Enabled = True
                    Me.Close()
                Else
                    'MsgBox("Please check QTY OF SHIFT")
                    PictureBox3.Enabled = False
                    PictureBox2.Enabled = False
                    Dim listdetail = "Please check QTY OF SHIFT"
                    PictureBox10.BringToFront()
                    PictureBox10.Show()
                    PictureBox1.BringToFront()
                    PictureBox1.Show()
                    Panel2.BringToFront()
                    Panel2.Show()
                    Label2.Text = listdetail
                    Label2.BringToFront()
                    Label2.Show()
                End If
            Else
                PictureBox3.Enabled = False
                PictureBox2.Enabled = False
                Dim listdetail = "Can't input the Qty. over : " + Label1.Text
                            PictureBox10.BringToFront()
                PictureBox10.Show()
                PictureBox1.BringToFront()
                PictureBox1.Show()
                Panel2.BringToFront()
                Panel2.Show()
                Label2.Text = listdetail
                Label2.BringToFront()
                Label2.Show()
                'MsgBox("Can't input the Qty. over : " + Label1.Text)
                TextBox1.Clear()
			End If
		End If
	End Function
	Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        'Try

        If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
            'Backoffice_model.updated_data_to_dbsvr()
            If Working_Pro.slm_flg_qr_prod = 1 Then
                Dim prd_qty As Integer = Convert.ToInt32(Label1.Text)
                Dim inp_qty As Integer
                Dim result As Integer = 0
                '    MsgBox("M0")
                Dim md As New modelDefect()
                Working_Pro._Edit_Up_0.Text = "0"
                Try
                    inp_qty = Convert.ToInt32(TextBox1.Text)
                Catch ex As Exception
                    ' inp_qty = 0
                    PictureBox3.Enabled = False
                    PictureBox2.Enabled = False
                    Dim listdetail = "Please Input QTY."
                    PictureBox10.BringToFront()
                    PictureBox10.Show()
                    PictureBox1.BringToFront()
                    PictureBox1.Show()
                    Panel2.BringToFront()
                    Panel2.Show()
                    Label2.Text = listdetail
                    Label2.BringToFront()
                    Label2.Show()
                    'MsgBox("Can't input the Qty. over : " + Label1.Text)
                    TextBox1.Clear()
                 End Try
                Dim totalDefect As Integer = (CDbl(Val(Working_Pro.lb_nc_qty.Text)) + CDbl(Val(Working_Pro.lb_ng_qty.Text))) 'CDbl(Val(md.mGetDefect(Working_Pro.wi_no.Text, Working_Pro.Label18.Text, Working_Pro.Label14.Text) + (CDbl(Val(Working_Pro.lb_nc_qty.Text)) + CDbl(Val(Working_Pro.lb_ng_qty.Text)))))
                Dim totalDefectCP As Integer = (CDbl(Val(Working_Pro.lb_nc_child_part.Text)) + CDbl(Val(Working_Pro.lb_ng_child_part.Text)))
                Dim totalDefectAll As Integer = totalDefect
                'If inp_qty > (prd_qty - totalDefect) Then
                If inp_qty > (CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text) - (totalDefectAll))) Then ' 
                    PictureBox3.Enabled = False
                    PictureBox2.Enabled = False
                    Dim listdetail = "Can't input the Qty. over : " & Label1.Text & " And Have Defect : " & totalDefectAll
                    PictureBox10.BringToFront()
                    PictureBox10.Show()
                    PictureBox1.BringToFront()
                    PictureBox1.Show()
                    Panel2.BringToFront()
                    Panel2.Show()
                    Label2.Text = listdetail
                    Label2.BringToFront()
                    Label2.Show()
                    'MsgBox("Can't input the Qty. over : " & Label1.Text & " And Have Defect : " & totalDefect)
                    TextBox1.Clear()
                Else
                    Working_Pro.RemainScanDmcDelQty = TextBox1.Text
                    For i As Integer = 1 To CDbl(Val(TextBox1.Text))
                        Dim newItem2 As New ListViewItem(i)
                        newItem2.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        Working_Pro.lvRemainmanual.Items.Add(newItem2)
                    Next
                    ScanQRprod.ManageQrScanFA("3", Working_Pro.RemainScanDmcDelQty)
                    ScanQRprod.Show()
                End If

            Else
                set_data_del()
            End If
        Else
            ' MsgBox("ASD")
            load_show.Show()
			End If
			'Catch ex As Exception
		'MsgBox(ex.Message)
		'load_show.Show()
		'End Try
		'Dim prd_qty As Integer = Convert.ToInt32(Label1.Text)
		'Dim inp_qty As Integer
		'Try
		'    inp_qty = Convert.ToInt32(TextBox1.Text)
		'Catch ex As Exception
		'    inp_qty = 0
		'End Try
		'If inp_qty > prd_qty Then
		'    MsgBox("Can't input the Qty. over : " + Label1.Text)
		'   TextBox1.Clear()
		'Else
		'   Try
		'Working_Pro.LB_COUNTER_SHIP.Text -= inp_qty
		' Working_Pro.LB_COUNTER_SEQ.Text -= inp_qty
		' Dim yearNow As Integer = DateTime.Now.ToString("yyyy")
		' Dim monthNow As Integer = DateTime.Now.ToString("MM")
		' Dim dayNow As Integer = DateTime.Now.ToString("dd")
		' Dim hourNow As Integer = DateTime.Now.ToString("HH")
		' Dim minNow As Integer = DateTime.Now.ToString("mm")
		' Dim secNow As Integer = DateTime.Now.ToString("ss")
		' Dim yearSt As Integer = Working_Pro.st_time.Text.Substring(0, 4)
		' Dim monthSt As Integer = Working_Pro.st_time.Text.Substring(5, 2)
		' Dim daySt As Integer = Working_Pro.st_time.Text.Substring(8, 2)
		' Dim hourSt As Integer = Working_Pro.st_time.Text.Substring(11, 2)
		' Dim minSt As Integer = Working_Pro.st_time.Text.Substring(14, 2)
		'Dim secSt As Integer = Working_Pro.st_time.Text.Substring(17, 2)
		' Dim firstDate As New System.DateTime(yearSt, monthSt, daySt, hourSt, minSt, secSt)
		' Dim secondDate As New System.DateTime(yearNow, monthNow, dayNow, hourNow, minNow, secNow)
		'  Dim diff As System.TimeSpan = secondDate.Subtract(firstDate)
		'   Dim diff1 As System.TimeSpan = secondDate - firstDate
		'   Dim diff2 As String = (secondDate - firstDate).TotalSeconds.ToString()
		'Dim actCT As Double = Format(diff2 / 60, "0.00")
		'   Dim cnt_btn As Integer = Integer.Parse(MainFrm.cavity.Text)
		'Dim snp As Integer = Integer.Parse(Working_Pro.Label27.Text)
		'Working_Pro._Edit_Up_0.Text = Integer.Parse(Working_Pro._Edit_Up_0.Text) - inp_qty
		' Working_Pro.Label6.Text = Integer.Parse(Working_Pro.Label6.Text) - inp_qty
		'  Working_Pro.Label10.Text = Integer.Parse(Working_Pro.Label10.Text.Substring(1)) + inp_qty
		'   Working_Pro.Label10.Text = "-" + Working_Pro.Label10.Text
		' Working_Pro.count = Working_Pro.count - inp_qty
		' Working_Pro.lb_qty_for_box.Text = Integer.Parse(Working_Pro.lb_qty_for_box.Text) - inp_qty
		' Dim sum_prg As Integer = (Working_Pro.Label6.Text * 100) / Working_Pro.Label8.Text
		' If sum_prg > 100 Then
		'    sum_prg = 100
		'End If
		'Working_Pro.CircularProgressBar1.Text = sum_prg & "%"
		'Working_Pro.CircularProgressBar1.Value = sum_prg
		'Dim start_time As Date = Working_Pro.st_count_ct.Text
		'Dim pd As String = MainFrm.Label6.Text
		'D im line_cd As String = MainFrm.Label4.Text
		'Dim wi_plan As String = Working_Pro.wi_no.Text
		'Dim item_cd As String = Working_Pro.Label3.Text
		'Dim item_name As String = Working_Pro.Label12.Text
		'Dim staff_no As String = Working_Pro.Label29.Text
		'Dim seq_no As String = Working_Pro.Label22.Text
		'Dim prd_qty2 As Integer = inp_qty - (inp_qty * 2)
		'Dim end_time As Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
		'Dim use_timee As Double = "0"
		'Dim tr_status As String = "0"
		'Dim number_qty As Integer = Working_Pro.Label6.Text
		'Dim start_time2 As String = start_time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
		'Dim end_time2 As String = end_time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
		'Try
		'If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
		' tr_status = "1"
		'  Backoffice_model.insPrdDetail_sqlite(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty2, number_qty, start_time2, end_time2, use_timee, tr_status)
		'   Backoffice_model.Insert_prd_detail(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty2, start_time, end_time, use_timee, number_qty)
		'Else
		'   tr_status = "0"
		'    Backoffice_model.insPrdDetail_sqlite(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty2, number_qty, start_time2, end_time2, use_timee, tr_status)
		'  End If
		'Catch ex As Exception
		'   tr_status = "0"
		'   Backoffice_model.insPrdDetail_sqlite(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty2, number_qty, start_time2, end_time2, use_timee, tr_status)
		'End Try
		'Working_Pro.Enabled = True
		'  Me.Close()
		' Catch ex As Exception
		'     MsgBox("Please input Qty." & ex.Message())
		' End Try
		'End If
	End Sub
    Private Sub Desc_act_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbpartNo.Text = Working_Pro.Label3.Text
        'TextBox1.Enabled = False
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

	End Sub

	Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

	End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        PictureBox3.Enabled = True
        PictureBox2.Enabled = True
        PictureBox10.Visible = False
        Panel2.Visible = False
        PictureBox1.Visible = False
    End Sub
End Class
