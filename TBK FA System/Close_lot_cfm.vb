Public Class Close_lot_cfm
	Public act_by_seq As String = Working_Pro.LB_COUNTER_SEQ.Text
	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		con_close_lot()
	End Sub
    Public Async Function con_close_lot() As Task(Of String)
        If CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text)) = "0" Then
            Working_Pro.close_lot_seq()
        End If
        If Working_Pro.check_in_up_seq > 0 Then
            Dim line_id As String = MainFrm.line_id.Text
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    Backoffice_model.line_status_upd(line_id)
                Else
                    Backoffice_model.line_status_upd_sqlite(line_id)
                End If
            Catch ex As Exception
                Backoffice_model.line_status_upd_sqlite(line_id)
            End Try
            Dim radioValue As String
            If RadioButton1.Checked = True Then
                radioValue = "sel1"
            Else
                radioValue = "sel2"
            End If
            'If lb_qty_count.Text = Working_Pro.Label6.Text Then
            'MsgBox("NO INSERT DATA")
            'Nothing.... 'อาจจะเป็นสาเหตุที่ยอดการผลิตไม่เข้า'
            'Working_Pro.Label6.Text = ""
            'Prd_detail.Enabled = True
            'Working_Pro.Enabled = True
            'Working_Pro.Close()
            'Insert_list.Close()
            'Else
            If radioValue = "sel2" Then
                Dim pd As String = MainFrm.Label6.Text
                Dim line_cd As String = MainFrm.Label4.Text
                Dim wi_plan As String = Working_Pro.wi_no.Text
                Dim item_cd As String = Working_Pro.Label3.Text
                Dim item_name As String = Working_Pro.Label12.Text
                Dim staff_no As String = Working_Pro.Label29.Text
                Dim seq_no As Integer = CDbl(Val(Working_Pro.Label22.Text))
                If seq_no <= 0 Then
                    seq_no = 1
                End If
                Dim plan_qty As Integer = Working_Pro.Label8.Text
                Dim act_qty As Integer
                Try
                    act_qty = Working_Pro.LB_COUNTER_SEQ.Text 'Working_Pro._Edit_Up_0.Text

                Catch ex As Exception
                    act_qty = 0
                End Try
                Dim shift_prd As String = Working_Pro.Label14.Text
                Dim prd_st_datetime As Date = Working_Pro.st_time.Text
                Dim prd_end_datetime As Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                Dim lot_no As String = Working_Pro.Label18.Text
                Dim comp_flg2 As String = "0"
                Dim transfer_flg As String = "1"
                Dim del_flg As String = "0"
                Dim prd_flg As String = "1"
                Dim close_lot_flg As String = "1"
                Dim avarage_eff As Double
                Try
                    avarage_eff = Working_Pro.lb_sum_prg.Text
                Catch ex As Exception
                    avarage_eff = 0
                End Try
                Dim avarage_act_prd_time As Double
                Try
                    avarage_act_prd_time = Working_Pro.Label37.Text
                Catch ex As Exception
                    avarage_act_prd_time = 0
                End Try

                Try
                    If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                        transfer_flg = "1"
                        Backoffice_model.Insert_prd_close_lot(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                        Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                        'Backoffice_model.Insert_prd_detail(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty, start_time, end_time, use_timee, number_qty)
                        Dim temp_co_emp As Integer = List_Emp.ListView1.Items.Count
                        Dim emp_cd As String
                        For i = 0 To temp_co_emp - 1
                            emp_cd = List_Emp.ListView1.Items(i).Text
                            Backoffice_model.Insert_emp_cd(wi_plan, emp_cd, seq_no)
                            'MsgBox(List_Emp.ListView1.Items(i).Text)
                        Next
                        'MsgBox("Ins completed")
                    Else
                        transfer_flg = "0"
                        Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                        'Backoffice_model.insPrdDetail_sqlite(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty, number_qty, start_time, end_time, use_timee, tr_status)

                        'MsgBox("Ins incompleted1")
                    End If
                Catch ex As Exception
                    'MsgBox("Ins incompleted2")
                    transfer_flg = "0"
                    Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                    'Backoffice_model.insPrdDetail_sqlite(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty, number_qty, start_time, end_time, use_timee, tr_status)
                End Try
                'List_Emp.Button2.Enabled = False
                'List_Emp.ListView1.Items.Clear()
                'List_Emp.ListBox2.Items.Clear()

                Dim result_mod As Double = Working_Pro.LB_COUNTER_SEQ.Text Mod Integer.Parse(Working_Pro.Label27.Text) 'Integer.Parse(_Edit_Up_0.Text) Mod Integer.Parse(Label27.Text)
                Dim result_total As Double = Working_Pro.Label6.Text Mod Integer.Parse(Working_Pro.Label27.Text) 'Integer.Parse(_Edit_Up_0.Text) Mod Integer.Parse(Label27.Text)
                '	Dim check_format_tag As String = Backoffice_model.B_check_format_tag()
                If result_mod = "0" Then
                    If Backoffice_model.check_line_reprint() = "1" Then
                        If Working_Pro.LB_COUNTER_SEQ.Text > 0 Then
                            If CDbl(Val(Working_Pro.Label27.Text)) = 1 Or CDbl(Val(Working_Pro.Label27.Text)) = 999999 Then
                                Working_Pro.lb_box_count.Text = Working_Pro.lb_box_count.Text + 1
                                Working_Pro.Label_bach.Text = Working_Pro.Label_bach.Text + 1
                                'MsgBox("result __A1__>")
                                Working_Pro.Label_bach.Text = Working_Pro.Label_bach.Text + 1
                                Working_Pro.tag_print()
                            Else
                            End If
                        End If
                    End If
                Else
                    If Working_Pro.LB_COUNTER_SEQ.Text > 0 And result_total > "0" Then
                        Working_Pro.lb_box_count.Text = Working_Pro.lb_box_count.Text + 1
                        Working_Pro.Label_bach.Text = Working_Pro.Label_bach.Text + 1
                        Working_Pro.Label_bach.Text = Working_Pro.Label_bach.Text + 1
                        'MsgBox("108 Working_Pro.lb_qty_for_box.Text" & Working_Pro.lb_qty_for_box.Text)
                        'MsgBox("result __B1__>")
                        Working_Pro.tag_print()
                    End If
                End If
                Working_Pro.LB_COUNTER_SHIP.Text = 0
                Working_Pro.LB_COUNTER_SEQ.Text = 0
                Working_Pro.Label6.Text = ""
                Prd_detail.Enabled = True
                Working_Pro.Enabled = True
                Working_Pro.Close()
                Insert_list.Close()
            ElseIf radioValue = "sel1" Then
                Dim pd As String = MainFrm.Label6.Text
                Dim line_cd As String = MainFrm.Label4.Text
                Dim wi_plan As String = Working_Pro.wi_no.Text
                Dim item_cd As String = Working_Pro.Label3.Text
                Dim item_name As String = Working_Pro.Label12.Text
                Dim staff_no As String = Working_Pro.Label29.Text
                Dim seq_no As Integer = CDbl(Val(Working_Pro.Label22.Text))
                If seq_no <= 0 Then
                    seq_no = 1
                End If
                Dim plan_qty As Integer = Working_Pro.Label8.Text
                Dim act_qty As Integer
                Try
                    act_qty = Working_Pro.LB_COUNTER_SEQ.Text 'Working_Pro._Edit_Up_0.Text

                Catch ex As Exception
                    act_qty = 0
                End Try
                Dim shift_prd As String = Working_Pro.Label14.Text
                Dim prd_st_datetime As Date = Working_Pro.st_time.Text
                Dim prd_end_datetime As Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                Dim lot_no As String = Working_Pro.Label18.Text
                Dim comp_flg2 As String = "0"
                Dim transfer_flg As String = "1"
                Dim del_flg As String = "0"
                Dim prd_flg As String = "1"
                Dim close_lot_flg As String = "1"
                Dim avarage_eff As Double
                Try
                    avarage_eff = Working_Pro.lb_sum_prg.Text
                Catch ex As Exception
                    avarage_eff = 0
                End Try
                Dim avarage_act_prd_time As Double
                Try
                    avarage_act_prd_time = Working_Pro.Label37.Text
                Catch ex As Exception
                    avarage_act_prd_time = 0
                End Try
                Try
                    If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                        transfer_flg = "1"
                        Backoffice_model.Insert_prd_close_lot(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                        Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                        'Backoffice_model.Insert_prd_detail(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty, start_time, end_time, use_timee, number_qty)
                        Dim temp_co_emp As Integer = List_Emp.ListView1.Items.Count
                        Dim emp_cd As String
                        For i = 0 To temp_co_emp - 1
                            emp_cd = List_Emp.ListView1.Items(i).Text
                            Backoffice_model.Insert_emp_cd(wi_plan, emp_cd, seq_no)
                            'MsgBox(List_Emp.ListView1.Items(i).Text)
                        Next
                        'MsgBox("Ins completed")
                    Else
                        transfer_flg = "0"
                        Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                        'Backoffice_model.insPrdDetail_sqlite(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty, number_qty, start_time, end_time, use_timee, tr_status)
                        'MsgBox("Ins incompleted1")
                    End If
                Catch ex As Exception
                    'MsgBox("Ins incompleted2")
                    transfer_flg = "0"
                    Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                    'Backoffice_model.insPrdDetail_sqlite(pd, line_cd, wi_plan, item_cd, item_name, staff_no, seq_no, prd_qty, number_qty, start_time, end_time, use_timee, tr_status)
                End Try
                'List_Emp.Button2.Enabled = False
                'List_Emp.ListView1.Items.Clear()
                'List_Emp.ListBox2.Items.Clear()
                Try
                    If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                        Await Backoffice_model.updated_data_to_dbsvr(Me, "2")
                    End If
                Catch ex As Exception

                End Try
                Dim result_mod As Double = Working_Pro.LB_COUNTER_SEQ.Text Mod Integer.Parse(Working_Pro.Label27.Text) 'Integer.Parse(_Edit_Up_0.Text) Mod Integer.Parse(Label27.Text)
                Dim result_total As Double = Working_Pro.Label6.Text Mod Integer.Parse(Working_Pro.Label27.Text) 'Integer.Parse(_Edit_Up_0.Text) Mod Integer.Parse(Label27.Text)

                If result_mod = "0" Then
                    If Backoffice_model.check_line_reprint() = "1" Then
                        If Working_Pro.LB_COUNTER_SEQ.Text > 0 Then
                            If CDbl(Val(Working_Pro.Label27.Text)) = 1 Or CDbl(Val(Working_Pro.Label27.Text)) = 999999 Then
                                Working_Pro.lb_box_count.Text = Working_Pro.lb_box_count.Text + 1
                                'MsgBox("result __C1__>")
                                Working_Pro.Label_bach.Text = Working_Pro.Label_bach.Text + 1
                                Working_Pro.tag_print()
                            Else

                            End If
                        End If
                    End If
                Else
                    If Working_Pro.LB_COUNTER_SEQ.Text > 0 And result_total > "0" Then
                        Working_Pro.lb_box_count.Text = Working_Pro.lb_box_count.Text + 1
                        Working_Pro.Label_bach.Text = Working_Pro.Label_bach.Text + 1
                        'Working_Pro.tag_print_incomplete() 'print incompleted tag'
                        'MsgBox("199 Working_Pro.lb_qty_for_box.Text" & Working_Pro.lb_qty_for_box.Text)
                        'MsgBox("result __D1__>")
                        Working_Pro.tag_print()
                    End If
                End If
                Working_Pro.LB_COUNTER_SHIP.Text = 0
                Working_Pro.LB_COUNTER_SEQ.Text = 0
                Working_Pro.Label6.Text = ""
                MainFrm.Enabled = True
                Prd_detail.Enabled = True
                Working_Pro.Enabled = True
                Working_Pro.Close()
                Insert_list.Close()
                Sel_prd_setup.Close()
                MainFrm.Show()
            End If
            'End If
            'MsgBox(radioValue)
        End If
        Working_Pro.Label6.Text = ""
        Prd_detail.Timer3.Enabled = True
        Prd_detail.Enabled = True
        Working_Pro.Enabled = True
        Working_Pro.Close()
        Insert_list.Close()
        Sel_prd_setup.Close()
        'Sel_prd_setup.Close()
        Working_Pro.Close()
        MainFrm.Show()
        Auto_close_lot.Close()
        Me.Close()
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Working_Pro.Enabled = True
		Me.Close()
	End Sub
	Private Sub Close_lot_cfm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		act_by_seq = Working_Pro.LB_COUNTER_SEQ.Text
		'lb_qty_count.Visible = True
	End Sub
End Class
