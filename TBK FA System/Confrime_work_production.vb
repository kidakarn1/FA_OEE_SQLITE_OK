Imports System.Net
Imports System.IO
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.Web.Script.Serialization
Public Class Confrime_work_production
    Public Shared ArrayDataPlan As New List(Of DataPlan)
    Public Shared Function next_pae()
        Dim count_reload As Integer = 0
        Backoffice_model.gobal_DateTimeComputerDown = "" ' ห้ามลบ ตัวเช็คคอมดับ
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim line_cd_na As String = Prd_detail.Label3.Text
                Dim LoadSQL1 = Backoffice_model.Get_Last_part(line_cd_na)
                While LoadSQL1.Read()
                    Prd_detail.lb_temp_txt.Text = LoadSQL1("item_cd").ToString()
                End While
                Prd_detail.Enabled = False
                Prd_detail.lb_temp_txt.Text = Prd_detail.lb_item_cd.Text
                ' If Prd_detail.lb_temp_txt.Text = Prd_detail.lb_item_cd.Text Then
                Dim line_id As String = MainFrm.line_id.Text
                    Dim date_st As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
                    Dim date_end As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
                    If Prd_detail.check_network() = 1 Then
                        Backoffice_model.line_status_ins(line_id, date_st, date_end, "1", "0", "24", "0", Prd_detail.lb_wi.Text)
                        're_load:

                        'If count_reload = 200 Then
                        '  Dim a = Backoffice_model.Check_detail_actual_insert_act() 'กรณีเครื่องดับ'
                        'Else
                        ' count_reload += 1
                        '  GoTo re_load
                        'End If
                        Backoffice_model.NEXT_PROCESS = Backoffice_model.F_NEXT_PROCESS(Prd_detail.lb_item_cd.Text)
                        Insert_list.Label3.Text = MainFrm.Label4.Text
                        Insert_list.ListView1.View = View.Details
                        Dim line_cd As String = MainFrm.Label4.Text
                        Dim LoadSQL_prd_plan As String = ""
                        If MainFrm.rsCheckCriticalFlg = "0" Then
                            LoadSQL_prd_plan = Backoffice_model.Get_prd_plan_new(line_cd)
                        Else
                            LoadSQL_prd_plan = Backoffice_model.GetDataPlanCritical(Prd_detail.lb_wi.Text)
                        End If
                        Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(LoadSQL_prd_plan)
                        ArrayDataPlan = New List(Of DataPlan)
                        For Each item As Object In dict
                            If MainFrm.rsCheckCriticalFlg <> "0" Then
                                Working_Pro.lbPosition1.Text = item("SPOSITION1").ToString()
                                Working_Pro.lbPosition2.Text = item("SPOSITION2").ToString()
                            End If
                            ArrayDataPlan.Add(New DataPlan With {.IND_ROW = item("IND_ROW").ToString(), .PS_UNIT_NUMERATOR = "PS_UNIT_NUMERATOR", .CT = item("CT").ToString(), .seq_no = item("seq_no").ToString(), .WORK_ODR_DLV_DATE = item("WORK_ODR_DLV_DATE").ToString(), .LOCATION_PART = item("LOCATION_PART").ToString(), .MODEL = item("MODEL").ToString(), .PRODUCT_TYP = item("PRODUCT_TYP").ToString(), .wi = item("WI").ToString(), .item_cd = item("ITEM_CD").ToString(), .item_name = item("ITEM_NAME").ToString()})
                        Working_Pro.LB_IND_ROW.Text = item("IND_ROW").ToString()
                        Working_Pro.Product_type = item("PRODUCT_TYP").ToString()
                        Working_Pro.Label27.Text = item("PS_UNIT_NUMERATOR").ToString()
                        '		Prd_detail.lb_snp.Text = LoadSQL("PS_UNIT_NUMERATOR")
                        Prd_detail.lb_snp.Text = item("PS_UNIT_NUMERATOR").ToString()
                            Prd_detail.lb_ct.Text = item("CT").ToString()
                            Prd_detail.lb_seq.Text = item("seq_no").ToString()
                            Prd_detail.lb_dlv_date.Text = item("WORK_ODR_DLV_DATE").ToString()
                            Prd_detail.lb_location.Text = item("LOCATION_PART").ToString()
                            Prd_detail.lb_model.Text = item("MODEL").ToString()
                            Prd_detail.lb_prd_type.Text = item("PRODUCT_TYP").ToString()
                            'If Prd_detail.QTY_NC.Text Is Nothing Then
                            'Prd_detail.QTY_NC.Text = item("QTY_NC").ToString()
                            'Else
                            Prd_detail.QTY_NC.Text = 0
                            'End If
                            'If Prd_detail.QTY_NG.Text Is Nothing Then
                            '	Prd_detail.QTY_NG.Text = item("QTY_NG").ToString()
                            '	Else
                            Prd_detail.QTY_NG.Text = 0
                            '	End If
                            Prd_detail.lb_plan_qty.Text = item("QTY").ToString()
                            Prd_detail.lb_remain_qty.Text = (item("QTY").ToString() - item("prd_qty_sum").ToString())
                        Next
                        Dim numberOfindex As Integer = 0
                        'If LoadSQL.Read() Then
                        '	Working_Pro.Label27.Text = LoadSQL("PS_UNIT_NUMERATOR")
                        '		Prd_detail.lb_snp.Text = LoadSQL("PS_UNIT_NUMERATOR")
                        '	Prd_detail.lb_ct.Text = LoadSQL("CT")
                        '	Prd_detail.lb_seq.Text = LoadSQL("seq_no")
                        '	Prd_detail.lb_dlv_date.Text = LoadSQL("ORIGINAL_PLAN")
                        '	Prd_detail.lb_location.Text = LoadSQL("LOCATION_PART")
                        '	Prd_detail.lb_model.Text = LoadSQL("MODEL")
                        '	Prd_detail.lb_prd_type.Text = LoadSQL("PRODUCT_TYP")
                        '	Prd_detail.QTY_NC.Text = LoadSQL("QTY_NC").ToString
                        '	Prd_detail.QTY_NG.Text = LoadSQL("QTY_NG").ToString
                        '	Prd_detail.lb_plan_qty.Text = LoadSQL("PLAN_QTY").ToString()
                        '	Prd_detail.lb_remain_qty.Text = (LoadSQL("PLAN_QTY").ToString() - LoadSQL("prd_qty_sum").ToString())
                        '	End If
                        '		LoadSQL.close()
                        Dim GET_SEQ = Backoffice_model.GET_SEQ_PLAN_current(Prd_detail.lb_wi.Text, Backoffice_model.GET_LINE_PRODUCTION(), date_st, date_end)
                        Try
                            If GET_SEQ.Read() Then
                                Prd_detail.lb_seq.Text = GET_SEQ("seq_no")
                                'Dim update_data = Backoffice_model.Update_seqplan(Prd_detail.lb_wi.Text, Backoffice_model.GET_LINE_PRODUCTION(), date_st, date_end, CDbl(Val(Prd_detail.lb_seq.Text)) + 1)
                            End If
                        Catch ex As Exception
                            'Dim insert_data = Backoffice_model.INSERT_tmp_planseq(Prd_detail.lb_wi.Text, Backoffice_model.GET_LINE_PRODUCTION(), date_st, date_end)
                            Prd_detail.lb_seq.Text = 0
                        End Try
                        GET_SEQ.close()
                    End If
                    If Prd_detail.lb_seq.Text = "" Then

                    End If
                    Dim tclient As New WebClient
                    Dim i As Integer = Prd_detail.Label2.Text
                    Dim temp_co_emp As Integer = List_Emp.ListView1.Items.Count
                    status_check_ping = 1
                    For i = 0 To temp_co_emp - 1
                        If i = 0 Then
                            If status_check_ping = 1 Then
                                Try
                                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                    Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
                                    'Console.WriteLine(url)
                                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
                                    Working_Pro.PictureBox2.Image = tImage
                                    'Working_Pro.lb_emp1.Visible = True
                                    Working_Pro.lb_emp1.Text = emp_cd
                                Catch ex As Exception
                                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                                    'Console.WriteLine("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")
                                    Working_Pro.PictureBox2.Image = tImage
                                    'Working_Pro.lb_emp1.Visible = True
                                    Working_Pro.lb_emp1.Text = emp_cd
                                End Try
                            Else
                                Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                Working_Pro.PictureBox2.Image = Backoffice_model.img_user1
                                'Working_Pro.lb_emp1.Visible = True
                                Working_Pro.lb_emp1.Text = emp_cd
                            End If
                        ElseIf i = 1 Then
                            If status_check_ping = 1 Then
                                Try
                                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                    Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
                                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
                                    Working_Pro.PictureBox3.Image = tImage
                                    'Working_Pro.lb_emp2.Visible = True
                                    Working_Pro.lb_emp2.Text = emp_cd
                                Catch ex As Exception
                                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                                    Working_Pro.PictureBox3.Image = tImage
                                    'Working_Pro.lb_emp2.Visible = True
                                    Working_Pro.lb_emp2.Text = emp_cd
                                End Try
                            Else
                                Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                Working_Pro.PictureBox3.Image = Backoffice_model.img_user2
                                'Working_Pro.lb_emp2.Visible = True
                                Working_Pro.lb_emp2.Text = emp_cd
                            End If
                        ElseIf i = 2 Then
                            If status_check_ping = 1 Then
                                Try
                                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                    Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
                                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
                                    Working_Pro.PictureBox4.Image = tImage
                                    'Working_Pro.lb_emp3.Visible = True
                                    Working_Pro.lb_emp3.Text = emp_cd
                                Catch ex As Exception
                                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                                    Working_Pro.PictureBox4.Image = tImage
                                    'Working_Pro.lb_emp3.Visible = True
                                    Working_Pro.lb_emp3.Text = emp_cd
                                End Try
                            Else
                                Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                Working_Pro.PictureBox4.Image = Backoffice_model.img_user3
                                'Working_Pro.lb_emp3.Visible = True
                                Working_Pro.lb_emp3.Text = emp_cd
                            End If
                        ElseIf i = 3 Then
                            If status_check_ping = 1 Then
                                Try
                                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                    Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
                                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
                                    Working_Pro.PictureBox5.Image = tImage
                                    'Working_Pro.lb_emp4.Visible = True
                                    Working_Pro.lb_emp4.Text = emp_cd
                                Catch ex As Exception
                                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                                    Working_Pro.PictureBox5.Image = tImage
                                    'Working_Pro.lb_emp4.Visible = True
                                    Working_Pro.lb_emp4.Text = emp_cd
                                End Try
                            Else
                                Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                Working_Pro.PictureBox5.Image = Backoffice_model.img_user4
                                'Working_Pro.lb_emp4.Visible = True
                                Working_Pro.lb_emp4.Text = emp_cd
                            End If
                        ElseIf i = 4 Then
                            If status_check_ping = 1 Then
                                Try
                                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                    Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
                                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
                                    Working_Pro.PictureBox6.Image = tImage
                                    'Working_Pro.lb_emp5.Visible = True
                                    Working_Pro.lb_emp5.Text = emp_cd
                                Catch ex As Exception
                                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                                    Working_Pro.PictureBox6.Image = tImage
                                    'Working_Pro.lb_emp5.Visible = True
                                    Working_Pro.lb_emp5.Text = emp_cd
                                End Try
                            Else
                                Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                Working_Pro.PictureBox6.Image = Backoffice_model.img_user5
                                Working_Pro.lb_emp5.Visible = True
                                Working_Pro.lb_emp5.Text = emp_cd
                            End If
                        ElseIf i = 5 Then
                            If status_check_ping = 1 Then
                                Try
                                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                    Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
                                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
                                    Working_Pro.PictureBox7.Image = tImage
                                    'Working_Pro.lb_emp6.Visible = True
                                    Working_Pro.lb_emp6.Text = emp_cd
                                Catch ex As Exception
                                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                                    Working_Pro.PictureBox7.Image = tImage
                                    'Working_Pro.lb_emp6.Visible = True
                                    Working_Pro.lb_emp6.Text = emp_cd
                                End Try
                            Else
                                Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                                Working_Pro.PictureBox7.Image = Backoffice_model.img_user6
                                'Working_Pro.lb_emp6.Visible = True
                                Working_Pro.lb_emp6.Text = emp_cd
                            End If
                        End If
                    Next
                    'Working_Pro.Button1.Text = MainFrm.cavity.Text & " Qty."
                    Working_Pro.Label18.Text = Prd_detail.Label6.Text
                    Working_Pro.Label29.Text = Prd_detail.Label2.Text
                    Working_Pro.Label14.Text = Prd_detail.Label12.Text.Substring(0, 1)
                    Working_Pro.wi_no.Text = Prd_detail.lb_wi.Text
                    Working_Pro.Label3.Text = Prd_detail.lb_item_cd.Text
                    Working_Pro.Label12.Text = Prd_detail.lb_item_name.Text
                    Working_Pro.newPartname.Text = Prd_detail.lb_item_name.Text
                If Working_Pro.newPartname.Text.Length > 17 Then
                    Working_Pro.newPartname.Text = Working_Pro.newPartname.Text.ToString.Substring(0, 17) & "..."
                End If
                Working_Pro.Label8.Text = Prd_detail.lb_plan_qty.Text
                    'Working_Pro.Label6.Text = ListView1.Items(numOfindex).SubItems(4).Text.ToString
                    'SNP
                    'Working_Pro.Label27.Text = Prd_detail.lb_snp.Text
                    Dim cavi_ty As Integer = MainFrm.cavity.Text
                    'CT = ListBox2
                    Working_Pro.CycleTime.Text = Prd_detail.lb_ct.Text
                    Dim time_req As Double = Prd_detail.lb_ct.Text * Prd_detail.lb_remain_qty.Text
                    Dim emp_no As Integer = Convert.ToInt32(Prd_detail.Label2.Text)
                    'Working_Pro.Label34.Text = (time_req / emp_no)
                    Working_Pro.Label34.Text = time_req
                    Working_Pro.Label38.Text = Format((Prd_detail.lb_ct.Text * 60) * cavi_ty, "0.0")
                    Working_Pro.Label37.Text = "0.0"
                    'MsgBox(lb_seq.Text)
                    'SEQ = ListBox3
                    If Convert.ToInt32(Prd_detail.lb_seq.Text) < 10 Then
                        'MsgBox("dfsdfg")
                        If MainFrm.chk_spec_line = "2" Then
                            Working_Pro.Label22.Text = "0" & Prd_detail.lb_seq.Text + MainFrm.ArrayDataPlan.ToArray.Length
                        Else
                            Working_Pro.Label22.Text = "0" & Prd_detail.lb_seq.Text + 1
                        End If
                    Else
                        If MainFrm.chk_spec_line = "2" Then
                            Working_Pro.Label22.Text = Prd_detail.lb_seq.Text + MainFrm.ArrayDataPlan.ToArray.Length
                        Else
                            Working_Pro.Label22.Text = Prd_detail.lb_seq.Text + 1
                        End If
                    End If
                    'DLV DATE
                    Working_Pro.lb_dlv_date.Text = Prd_detail.lb_dlv_date.Text
                    'MODEL
                    Working_Pro.lb_model.Text = Prd_detail.lb_model.Text
                    'LOCATION
                    Working_Pro.lb_location.Text = Prd_detail.lb_location.Text
                    'PRODUCT_TYPE
                    Try
                        Working_Pro.lb_prd_type.Text = Prd_detail.lb_prd_type.Text
                    Catch ex As Exception
                        Working_Pro.lb_prd_type.Text = "30"
                    End Try
                    Dim sum_progress As Integer = (Prd_detail.lb_remain_qty.Text * 100) / Prd_detail.lb_plan_qty.Text
                    sum_progress = 100 - sum_progress
                    ' Dim total_defect As Integer = CDbl(Val(Prd_detail.QTY_NG.Text)) - CDbl(Val(Prd_detail.QTY_NC.Text))
                    Dim sum_act As Integer = (Prd_detail.lb_plan_qty.Text - Prd_detail.lb_remain_qty.Text) '- total_defect
                    Working_Pro.Label6.Text = sum_act
                    Close_lot_cfm.lb_qty_count.Text = sum_act
                    Dim sum_diff As String = Prd_detail.lb_remain_qty.Text
                    sum_diff = "-" & sum_diff
                    Working_Pro.Label10.Text = sum_diff
                    Working_Pro.Label33.Text = Prd_detail.lb_remain_qty.Text
                    Working_Pro.CircularProgressBar1.Text = sum_progress & "%"
                    Working_Pro.CircularProgressBar1.Value = sum_progress
                    Working_Pro.CircularProgressBar2.Text = 0 & "%"
                    Working_Pro.CircularProgressBar2.Value = 0
                    Working_Pro.Panel1.BackColor = Color.Red
                    Working_Pro.Label30.Text = "STOPPED"
                    Working_Pro.btn_start.Visible = True
                    Working_Pro.btn_stop.Visible = False
                    loadData_Working_OEE()
                    Prd_detail.Hide()
                ' Else
                ' Prd_detail.Enabled = False
                ' Model_change_alert.Show()
                'End If
            End If
        Catch ex As Exception
            MsgBox("Over Plan Or  ===> " & ex.Message)
            load_show.Show()
            Prd_detail.Enabled = True
        End Try
    End Function
    Public Shared Sub loadData_Working_OEE()
        Working_Pro.Show()
        '  Working_OEE.lbLine.Text = MainFrm.Label4.Text
        '  Working_OEE.lbPartNo.Text = Prd_detail.lb_item_cd.Text
        '  Working_OEE.lbPartName.Text = Prd_detail.lb_item_name.Text
        '  Working_OEE.Show()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        next_pae()
        Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Private Sub Confrime_work_production_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If TimeOfDay.ToString("HH:mm:ss") >= "07:58:00" And TimeOfDay.ToString("HH:mm:ss") <= "08:00:00" Then
            Button2.Visible = False
        End If
        If TimeOfDay.ToString("HH:mm:ss") >= "19:58:00" And TimeOfDay.ToString("HH:mm:ss") <= "20:00:00" Then
            Button2.Visible = False
        End If
        If TimeOfDay.ToString("HH:mm:ss") >= "20:00:00" And TimeOfDay.ToString("HH:mm:ss") <= "23:59:59" Then
            LB_SHIFT_ENG.Text = "P"
            LB_SHIFT_TH.Text = "P"
        ElseIf TimeOfDay.ToString("HH:mm:ss") >= "00:00:00" And TimeOfDay.ToString("HH:mm:ss") <= "08:00:00" Then
            LB_SHIFT_ENG.Text = "P"
            LB_SHIFT_TH.Text = "P"
        Else
            LB_SHIFT_ENG.Text = "Q"
            LB_SHIFT_TH.Text = "Q"
        End If
        Timer1.Enabled = True
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub
    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If TimeOfDay.ToString("HH:mm:ss") >= "07:58:00" And TimeOfDay.ToString("HH:mm:ss") <= "08:00:00" Then
            If TimeOfDay.ToString("HH:mm:ss") >= "07:58:00" And TimeOfDay.ToString("HH:mm:ss") <= "08:00:00" Then
                Button2.Visible = False
            Else
                Button2.Visible = True
            End If
        Else
            If TimeOfDay.ToString("HH:mm:ss") >= "19:58:00" And TimeOfDay.ToString("HH:mm:ss") <= "20:00:00" Then
                Button2.Visible = False
            Else
                Button2.Visible = True
            End If
        End If
    End Sub
End Class