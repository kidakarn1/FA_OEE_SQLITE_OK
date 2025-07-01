Imports System.Net
Imports System.IO
Public Class List_Emp
    Public MaxManPower As Integer = 0
    Public Shared S_index As Integer = 0
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim i = ListView1.Items.Count
        If i >= MaxManPower Then
            MsgBox("ไลน์นี้มีพนักงานเดินไลน์ได้ " & MaxManPower & " คนเท่านั้น")
        Else
            Dim sc_type As String = MainFrm.lb_scanner_port.Text
            If sc_type = "USB" Then
                'MsgBox("Data")
            Else
                MainFrm.lb_ctrl_sc_flg.Text = "emp"
            End If
            Sc.TextBox2.Select()
            Me.Enabled = False
            Sc.Show()
        End If
    End Sub
    Public Function set_data_Month(lotSubstMonth)
        If lotSubstMonth = "01" Then
            lotSecondDigit = "A"
        ElseIf lotSubstMonth = "02" Then
            lotSecondDigit = "B"
        ElseIf lotSubstMonth = "03" Then
            lotSecondDigit = "C"
        ElseIf lotSubstMonth = "04" Then
            lotSecondDigit = "D"
        ElseIf lotSubstMonth = "05" Then
            lotSecondDigit = "E"
        ElseIf lotSubstMonth = "06" Then
            lotSecondDigit = "F"
        ElseIf lotSubstMonth = "07" Then
            lotSecondDigit = "E"
        ElseIf lotSubstMonth = "08" Then
            lotSecondDigit = "H"
        ElseIf lotSubstMonth = "09" Then
            lotSecondDigit = "I"
        ElseIf lotSubstMonth = "10" Then
            lotSecondDigit = "J"
        ElseIf lotSubstMonth = "11" Then
            lotSecondDigit = "K"
        ElseIf lotSubstMonth = "12" Then
            lotSecondDigit = "L"
        End If
        Return lotSubstMonth
    End Function
    Public Function set_data_Year(lotSubstYear)
        If lotSubstYear = "1" Then
            lotFirstDigit = "A"
        ElseIf lotSubstYear = "2" Then
            lotFirstDigit = "B"
        ElseIf lotSubstYear = "3" Then
            lotFirstDigit = "C"
        ElseIf lotSubstYear = "4" Then
            lotFirstDigit = "D"
        ElseIf lotSubstYear = "5" Then
            lotFirstDigit = "E"
        ElseIf lotSubstYear = "6" Then
            lotFirstDigit = "F"
        ElseIf lotSubstYear = "7" Then
            lotFirstDigit = "G"
        ElseIf lotSubstYear = "8" Then
            lotFirstDigit = "H"
        ElseIf lotSubstYear = "9" Then
            lotFirstDigit = "I"
        ElseIf lotSubstYear = "0" Then
            lotFirstDigit = "J"
        End If
        Return lotFirstDigit
    End Function
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'MsgBox(DateTime.Now.ToString("yyyy").Substring(3, 1))
        'MsgBox(DateTime.Now.ToString("HH"))
        Sc.SerialPort1.Close()
        'Sc.SerialPort1.Close()
        Dim tclient As New WebClient
        Dim i As Integer = ListView1.Items.Count
        lb_count_emp.Text = i
        MainFrm.LB_Number_worker.Text = lb_count_emp.Text
        Backoffice_model.gobal_DateTimeComputerDown = "" ' set คือ ให้เวลา com ดับ ไม่มีผล
        If i > 0 Then
            flag_emp.Text = "1"
        ElseIf i = 0 Then
            flag_emp.Text = "0"
        End If
        Dim temp_co_emp As Integer = ListView1.Items.Count
        If temp_co_emp = 0 Then
            MainFrm.lb_emp1.Text = ""
            MainFrm.lb_emp2.Text = ""
            MainFrm.lb_emp3.Text = ""
            MainFrm.lb_emp4.Text = ""
            MainFrm.lb_emp5.Text = ""
            MainFrm.lb_emp6.Text = ""
            MainFrm.PictureBox2.Image = Nothing
            MainFrm.PictureBox3.Image = Nothing
            MainFrm.PictureBox4.Image = Nothing
            MainFrm.PictureBox5.Image = Nothing
            MainFrm.PictureBox6.Image = Nothing
            MainFrm.PictureBox7.Image = Nothing
        End If
        'MsgBox(temp_co_emp)
        For i = 0 To temp_co_emp - 1
            If i = 0 Then
                'MsgBox("SOON")
                Try
                    Dim emp_cd As String = ListView1.Items(i).Text
                    Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
                    Backoffice_model.img_user1 = tImage
                    MainFrm.PictureBox2.Image = tImage
                    MainFrm.lb_emp1.Visible = True
                    MainFrm.lb_emp1.Text = emp_cd
                    MainFrm.lb_emp2.Text = ""
                    MainFrm.lb_emp3.Text = ""
                    MainFrm.lb_emp4.Text = ""
                    MainFrm.lb_emp5.Text = ""
                    MainFrm.lb_emp6.Text = ""
                    MainFrm.PictureBox3.Image = Nothing
                    MainFrm.PictureBox4.Image = Nothing
                    MainFrm.PictureBox5.Image = Nothing
                    MainFrm.PictureBox6.Image = Nothing
                    MainFrm.PictureBox7.Image = Nothing
                    Working_Pro.PictureBox2.Image = tImage
                    'Working_Pro.lb_emp1.Visible = True
                    Working_Pro.lb_emp1.Text = emp_cd
                    Working_Pro.lb_emp2.Text = ""
                    Working_Pro.lb_emp3.Text = ""
                    Working_Pro.lb_emp4.Text = ""
                    Working_Pro.lb_emp5.Text = ""
                    Working_Pro.lb_emp6.Text = ""
                    Working_Pro.PictureBox3.Image = Nothing
                    Working_Pro.PictureBox4.Image = Nothing
                    Working_Pro.PictureBox5.Image = Nothing
                    Working_Pro.PictureBox6.Image = Nothing
                    Working_Pro.PictureBox7.Image = Nothing
                Catch ex As Exception
                    Dim emp_cd As String = ListView1.Items(i).Text
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                    Backoffice_model.img_user1 = tImage
                    MainFrm.PictureBox2.Image = tImage
                    MainFrm.lb_emp1.Visible = True
                    MainFrm.lb_emp1.Text = emp_cd
                    MainFrm.lb_emp2.Text = ""
                    MainFrm.lb_emp3.Text = ""
                    MainFrm.lb_emp4.Text = ""
                    MainFrm.lb_emp5.Text = ""
                    MainFrm.lb_emp6.Text = ""
                    MainFrm.PictureBox3.Image = Nothing
                    MainFrm.PictureBox4.Image = Nothing
                    MainFrm.PictureBox5.Image = Nothing
                    MainFrm.PictureBox6.Image = Nothing
                    MainFrm.PictureBox7.Image = Nothing
                    Working_Pro.PictureBox2.Image = tImage
                    'Working_Pro.lb_emp1.Visible = True
                    Working_Pro.lb_emp1.Text = emp_cd
                    Working_Pro.lb_emp2.Text = ""
                    Working_Pro.lb_emp3.Text = ""
                    Working_Pro.lb_emp4.Text = ""
                    Working_Pro.lb_emp5.Text = ""
                    Working_Pro.lb_emp6.Text = ""
                    Working_Pro.PictureBox3.Image = Nothing
                    Working_Pro.PictureBox4.Image = Nothing
                    Working_Pro.PictureBox5.Image = Nothing
                    Working_Pro.PictureBox6.Image = Nothing
                    Working_Pro.PictureBox7.Image = Nothing
                End Try
            ElseIf i = 1 Then
                Try
                    Dim emp_cd As String = ListView1.Items(i).Text
                    Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
                    Backoffice_model.img_user2 = tImage
                    MainFrm.PictureBox3.Image = tImage
                    MainFrm.lb_emp2.Visible = True
                    MainFrm.lb_emp2.Text = emp_cd

                    MainFrm.lb_emp3.Text = ""
                    MainFrm.PictureBox4.Image = Nothing
                    Working_Pro.PictureBox3.Image = tImage
                    'Working_Pro.lb_emp2.Visible = True
                    Working_Pro.lb_emp2.Text = emp_cd
                    Working_Pro.lb_emp3.Text = ""
                    Working_Pro.PictureBox4.Image = Nothing

                Catch ex As Exception
                    Dim emp_cd As String = ListView1.Items(i).Text
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                    MainFrm.PictureBox3.Image = tImage
                    MainFrm.lb_emp2.Visible = True
                    MainFrm.lb_emp2.Text = emp_cd
                    Backoffice_model.img_user2 = tImage
                    MainFrm.lb_emp3.Text = ""
                    MainFrm.PictureBox4.Image = Nothing
                    Working_Pro.PictureBox3.Image = tImage
                    'Working_Pro.lb_emp2.Visible = True
                    Working_Pro.lb_emp2.Text = emp_cd
                    Working_Pro.lb_emp3.Text = ""
                    Working_Pro.PictureBox4.Image = Nothing
                End Try
            ElseIf i = 2 Then
                Try
                    Dim emp_cd As String = ListView1.Items(i).Text
                    Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
                    Backoffice_model.img_user3 = tImage
                    MainFrm.PictureBox4.Image = tImage
                    MainFrm.lb_emp3.Visible = True
                    MainFrm.lb_emp3.Text = emp_cd
                    MainFrm.lb_emp4.Text = ""
                    MainFrm.PictureBox5.Image = Nothing
                    Working_Pro.PictureBox4.Image = tImage
                    'Working_Pro.lb_emp3.Visible = True
                    Working_Pro.lb_emp3.Text = emp_cd
                    Working_Pro.lb_emp4.Text = ""
                    Working_Pro.PictureBox5.Image = Nothing
                Catch ex As Exception
                    Dim emp_cd As String = ListView1.Items(i).Text
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                    MainFrm.PictureBox4.Image = tImage
                    MainFrm.lb_emp3.Visible = True
                    MainFrm.lb_emp3.Text = emp_cd
                    Backoffice_model.img_user3 = tImage
                    MainFrm.lb_emp4.Text = ""
                    MainFrm.PictureBox5.Image = Nothing
                    Working_Pro.PictureBox4.Image = tImage
                    ' Working_Pro.lb_emp3.Visible = True
                    Working_Pro.lb_emp3.Text = emp_cd
                    Working_Pro.lb_emp4.Text = ""
                    Working_Pro.PictureBox5.Image = Nothing
                End Try
            ElseIf i = 3 Then
                Try
                    Dim emp_cd As String = ListView1.Items(i).Text
                    Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
                    Backoffice_model.img_user4 = tImage
                    MainFrm.PictureBox5.Image = tImage
                    MainFrm.lb_emp4.Visible = True
                    MainFrm.lb_emp4.Text = emp_cd
                    MainFrm.lb_emp5.Text = ""
                    MainFrm.PictureBox6.Image = Nothing
                    Working_Pro.PictureBox5.Image = tImage
                    'Working_Pro.lb_emp4.Visible = True
                    Working_Pro.lb_emp4.Text = emp_cd
                    Working_Pro.lb_emp5.Text = ""
                    Working_Pro.PictureBox6.Image = Nothing

                Catch ex As Exception
                    Dim emp_cd As String = ListView1.Items(i).Text
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                    Backoffice_model.img_user4 = tImage
                    MainFrm.PictureBox5.Image = tImage
                    MainFrm.lb_emp4.Visible = True
                    MainFrm.lb_emp4.Text = emp_cd
                    MainFrm.lb_emp5.Text = ""
                    MainFrm.PictureBox6.Image = Nothing
                    Working_Pro.PictureBox5.Image = tImage
                    'Working_Pro.lb_emp4.Visible = True
                    Working_Pro.lb_emp4.Text = emp_cd
                    Working_Pro.lb_emp5.Text = ""
                    Working_Pro.PictureBox6.Image = Nothing

                End Try
            ElseIf i = 4 Then
                Try
                    Dim emp_cd As String = ListView1.Items(i).Text
                    Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
                    MainFrm.PictureBox6.Image = tImage
                    MainFrm.lb_emp5.Visible = True
                    MainFrm.lb_emp5.Text = emp_cd
                    Backoffice_model.img_user5 = tImage
                    MainFrm.lb_emp6.Text = ""
                    MainFrm.PictureBox7.Image = Nothing
                    Working_Pro.PictureBox6.Image = tImage
                    ' Working_Pro.lb_emp5.Visible = True
                    Working_Pro.lb_emp5.Text = emp_cd
                    Working_Pro.lb_emp6.Text = ""
                    Working_Pro.PictureBox7.Image = Nothing
                Catch ex As Exception
                    Dim emp_cd As String = ListView1.Items(i).Text
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                    MainFrm.PictureBox6.Image = tImage
                    MainFrm.lb_emp5.Visible = True
                    MainFrm.lb_emp5.Text = emp_cd
                    Backoffice_model.img_user5 = tImage
                    MainFrm.lb_emp6.Text = ""
                    MainFrm.PictureBox7.Image = Nothing

                    Working_Pro.PictureBox6.Image = tImage
                    ' Working_Pro.lb_emp5.Visible = True
                    Working_Pro.lb_emp5.Text = emp_cd

                    Working_Pro.lb_emp6.Text = ""
                    Working_Pro.PictureBox7.Image = Nothing
                End Try
            ElseIf i = 5 Then
                If temp_co_emp <= 6 Then
                    Try
                        Dim emp_cd As String = ListView1.Items(i).Text
                        Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
                        Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
                        MainFrm.PictureBox7.Image = tImage
                        MainFrm.lb_emp6.Visible = True
                        MainFrm.lb_emp6.Text = emp_cd
                        Backoffice_model.img_user6 = tImage
                        Working_Pro.PictureBox7.Image = tImage
                        'Working_Pro.lb_emp6.Visible = True
                        Working_Pro.lb_emp6.Text = emp_cd
                    Catch ex As Exception
                        Dim emp_cd As String = ListView1.Items(i).Text
                        Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                        Backoffice_model.img_user6 = tImage
                        MainFrm.PictureBox7.Image = tImage
                        'MainFrm.lb_emp6.Visible = True
                        MainFrm.lb_emp6.Text = emp_cd
                    End Try
                Else
                    Dim emp_cd As String = "OTHER" 'ListView1.Items(i).Text
                    Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg"
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
                    MainFrm.PictureBox7.Image = tImage
                    MainFrm.lb_emp6.Visible = True
                    MainFrm.lb_emp6.Text = emp_cd
                    Backoffice_model.img_user6 = tImage
                    Working_Pro.PictureBox7.Image = tImage
                    ' Working_Pro.lb_emp6.Visible = True
                    Working_Pro.lb_emp6.Text = emp_cd
                End If
            End If
        Next
        If lotSubstYear = "1" Then
            lotFirstDigit = "A"
        ElseIf lotSubstYear = "2" Then
            lotFirstDigit = "B"
        ElseIf lotSubstYear = "3" Then
            lotFirstDigit = "C"
        ElseIf lotSubstYear = "4" Then
            lotFirstDigit = "D"
        ElseIf lotSubstYear = "5" Then
            lotFirstDigit = "E"
        ElseIf lotSubstYear = "6" Then
            lotFirstDigit = "F"
        ElseIf lotSubstYear = "7" Then
            lotFirstDigit = "G"
        ElseIf lotSubstYear = "8" Then
            lotFirstDigit = "H"
        ElseIf lotSubstYear = "9" Then
            lotFirstDigit = "I"
        ElseIf lotSubstYear = "0" Then
            lotFirstDigit = "J"
        End If
        Dim lotSubstMonth As String = DateTime.Now.ToString("MM")
        Dim lotSecondDigit As String = ""
        If lotSubstMonth = "01" Then
            lotSecondDigit = "A"
        ElseIf lotSubstMonth = "02" Then
            lotSecondDigit = "B"
        ElseIf lotSubstMonth = "03" Then
            lotSecondDigit = "C"
        ElseIf lotSubstMonth = "04" Then
            lotSecondDigit = "D"
        ElseIf lotSubstMonth = "05" Then
            lotSecondDigit = "E"
        ElseIf lotSubstMonth = "06" Then
            lotSecondDigit = "F"
        ElseIf lotSubstMonth = "07" Then
            lotSecondDigit = "E"
        ElseIf lotSubstMonth = "08" Then
            lotSecondDigit = "H"
        ElseIf lotSubstMonth = "09" Then
            lotSecondDigit = "I"
        ElseIf lotSubstMonth = "10" Then
            lotSecondDigit = "J"
        ElseIf lotSubstMonth = "11" Then
            lotSecondDigit = "K"
        ElseIf lotSubstMonth = "12" Then
            lotSecondDigit = "L"
        End If
        Dim lotthirdDigit = DateTime.Now.ToString("dd")
        Dim d As Date = DateTime.Now.ToString("dd-MM-yyyy")
        Dim timeShift As String = DateTime.Now.ToString("HH")
        'MsgBox("List EMP timeShift===>" & timeShift)
        Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
        Dim date_st As Integer = lotthirdDigit
        If time_now >= "00:00:00 AM" And time_now <= "08:00:00 AM" Then
            date_st = lotthirdDigit - 1
            If date_st <= 0 Then
                Dim tmp_date As String = d.AddDays(-1)
                'MsgBox(tmp_date)
                'MsgBox("day = " & tmp_date.Substring(0, 2))
                lotthirdDigit = tmp_date.Substring(0, 2)
                lotSecondDigit = set_data_Month(tmp_date.Substring(3, 2))
                'MsgBox("M = " & tmp_date.Substring(3, 2))
                lotFirstDigit = set_data_Year(tmp_date.Substring(6, 2))
                'MsgBox("Y = " & tmp_date.Substring(6, 2))
            Else
                lotthirdDigit = date_st
            End If
        Else
            'lotthirdDigit -= 1
        End If
        Dim defaultShift As String = ""
        'If timeShift = "05" Or timeShift = "06" Or timeShift = "07" Then
        'defaultShift = "Q (20:00 - 08:00)"
        'ElseIf timeShift = "08" Or timeShift = "09" Or timeShift = "10" Or timeShift = "11" Or timeShift = "12" Or timeShift = "13" Or timeShift = "14" Or timeShift = "15" Or timeShift = "16" Or timeShift = "17" Then
        'defaultShift = "P (08:00 - 20:00)"
        'ElseIf timeShift = "17" Or timeShift = "18" Or timeShift = "19" Then
        'defaultShift = "P (08:00 - 20:00)"
        'ElseIf timeShift = "20" Or timeShift = "21" Or timeShift = "22" Or timeShift = "23" Or timeShift = "24" Or timeShift = "00" Or timeShift = "01" Or timeShift = "02" Or timeShift = "03" Or timeShift = "04" Or timeShift = "05" Then
        'defaultShift = "Q (20:00 - 08:00)"
        'End If
        ' Prd_detail.Label12.Text = defaultShift
        If Len(date_st) <= 9 Then
            lotthirdDigit = "0" & date_st
            Dim date_digit = "0" & date_st
            Prd_detail.Label6.Text = lotFirstDigit & lotSecondDigit & date_digit
        Else
            Prd_detail.Label6.Text = lotFirstDigit & lotSecondDigit & lotthirdDigit
        End If
        Prd_detail.Label2.Text = ListView1.Items.Count
        If lb_link.Text = "main" Then
            MainFrm.Enabled = True
            MainFrm.menu1.Enabled = True
            MainFrm.Show()
            Me.Hide()
        ElseIf lb_link.Text = "working" Then
            If ListView1.Items.Count = 0 Then
                MsgBox("Please scan employee card")
            Else
                Working_Pro.check_seq_data()
                Dim pd As String = MainFrm.Label6.Text
                Dim line_cd As String = MainFrm.Label4.Text
                Dim wi_plan As String = Working_Pro.wi_no.Text
                Dim item_cd As String = Working_Pro.Label3.Text
                Dim item_name As String = Working_Pro.Label12.Text
                Dim staff_no As String = Working_Pro.Label29.Text
                Dim date_starts As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
                Dim GET_SEQ = Backoffice_model.GET_SEQ_PLAN_current(Working_Pro.wi_no.Text, Backoffice_model.GET_LINE_PRODUCTION(), date_starts, date_starts) ' Working_Pro.Label22.Text
                Dim seq_no As String = ""
                Try
                    If GET_SEQ.Read() Then
                        If MainFrm.chk_spec_line = "2" Then
                            seq_no = GET_SEQ("seq_no") + MainFrm.ArrayDataPlan.ToArray.Length
                        Else
                            seq_no = GET_SEQ("seq_no") + 1
                        End If
                        Dim update_data = Backoffice_model.Update_seqplan(Prd_detail.lb_wi.Text, Backoffice_model.GET_LINE_PRODUCTION(), date_starts, date_starts, CDbl(Val(seq_no)))
                    End If
                Catch ex As Exception
                    Dim insert_data = Backoffice_model.INSERT_tmp_planseq(Prd_detail.lb_wi.Text, Backoffice_model.GET_LINE_PRODUCTION(), date_starts, date_starts, seq_no)
                    If MainFrm.chk_spec_line = "2" Then
                        seq_no = 5
                    Else
                        seq_no = 1
                    End If
                End Try
                GET_SEQ.close()
                Dim rsInsertData
                If MainFrm.chk_spec_line = "2" Then
                    Dim GenSEQ As Integer = seq_no - 5
                    Dim Iseq = GenSEQ
                    Working_Pro.Spwi_id = New List(Of String)
                    Dim j As Integer = 0
                    For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                        Iseq += 1
                        Dim indRow As String = itemPlanData.IND_ROW
                        Dim pwi_shift As String = itemPlanData.IND_ROW
                        rsInsertData = Backoffice_model.INSERT_production_working_info(indRow, Working_Pro.Label18.Text, Iseq, Working_Pro.Label14.Text)
                        Working_Pro.pwi_id = Backoffice_model.GET_DATA_PRODUCTION_WORKING_INFO(indRow, Working_Pro.Label18.Text, Iseq)
                        rsInsertDataSqlite = model_api_sqlite.mas_INSERT_production_working_info(indRow, Working_Pro.Label18.Text, Iseq, Working_Pro.Label14.Text, Working_Pro.pwi_id)
                        Dim temp_co_emp_realtime As Integer = ListView1.Items.Count
                        Dim emp_cd_realtime As String
                        Dim wi As String = itemPlanData.wi
                        Working_Pro.Spwi_id.Add(Working_Pro.pwi_id)
                        For i = 0 To temp_co_emp_realtime - 1
                            emp_cd_realtime = ListView1.Items(i).Text
                            Backoffice_model.Insert_production_emp_detail_realtime(wi, emp_cd_realtime, Iseq, Working_Pro.Spwi_id(j))
                        Next
                        j = j + 1
                    Next
                Else
                    rsInsertData = Backoffice_model.INSERT_production_working_info(Working_Pro.LB_IND_ROW.Text, Working_Pro.Label18.Text, seq_no, Working_Pro.Label14.Text)
                    Working_Pro.pwi_id = Backoffice_model.GET_DATA_PRODUCTION_WORKING_INFO(Working_Pro.LB_IND_ROW.Text, Working_Pro.Label18.Text, seq_no)
                    rsInsertDataSqlite = model_api_sqlite.mas_INSERT_production_working_info(Working_Pro.LB_IND_ROW.Text, Working_Pro.Label18.Text, seq_no, Working_Pro.Label14.Text, Working_Pro.pwi_id)
                    Dim temp_co_emp_realtime As Integer = ListView1.Items.Count
                    Dim emp_cd_realtime As String
                    For i = 0 To temp_co_emp_realtime - 1
                        emp_cd_realtime = ListView1.Items(i).Text
                        Backoffice_model.Insert_production_emp_detail_realtime(wi_plan, emp_cd_realtime, seq_no, Working_Pro.pwi_id)
                    Next
                End If
                If MainFrm.chk_spec_line = "2" Then
                    seq_no = seq_no - 5
                Else
                    seq_no = seq_no - 1
                End If
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
                        'Backoffice_model.Insert_prd_close_lot(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                        Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                        Dim temp_co_emp1 As Integer = Me.ListView1.Items.Count
                        Dim emp_cd As String
                        For i = 0 To temp_co_emp1 - 1
                            emp_cd = Me.ListView1.Items(i).Text
                            Backoffice_model.Insert_emp_cd(wi_plan, emp_cd, seq_no)
                        Next
                    Else
                        transfer_flg = "0"
                        Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                    End If
                Catch ex As Exception
                    transfer_flg = "0"
                    Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                End Try
                'Working_Pro.Label22.Text = Convert.ToInt32(Working_Pro.Label22.Text) + 1
                GET_SEQ = Backoffice_model.GET_SEQ_PLAN_current(Working_Pro.wi_no.Text, Backoffice_model.GET_LINE_PRODUCTION(), date_starts, date_starts) ' Working_Pro.Label22.Text
                Try
                    If GET_SEQ.Read() Then
                        seq_no = GET_SEQ("seq_no")
                    End If
                Catch ex As Exception
                End Try
                If Len(seq_no) = "1" Then
                    seq_no = "0" & seq_no
                ElseIf Len(seq_no) = "2" Then
                    seq_no = "0" & seq_no
                End If
                Working_Pro.lb_nc_child_part.Text = 0
                Working_Pro.lb_ng_child_part.Text = 0
                Working_Pro.lb_nc_qty.Text = 0
                Working_Pro.lb_ng_qty.Text = 0
                Working_Pro.Label22.Text = seq_no
                Working_Pro.seqNo = seq_no
                Working_Pro.Label29.Text = Prd_detail.Label2.Text
                Working_Pro.Label16.Text = TimeOfDay.ToString("H : mm")
                Working_Pro.Label32.Text = "0"
                Working_Pro.CircularProgressBar2.Text = 0 & "%"
                Working_Pro.CircularProgressBar2.Value = 0
                Dim temp_co_emp3 As Integer = ListView1.Items.Count
                Dim time_req As Double = Prd_detail.lb_ct.Text * Prd_detail.lb_remain_qty.Text
                Working_Pro.Label34.Text = time_req
                Dim emp_no As Integer = Convert.ToInt32(Working_Pro.Label29.Text)
                Working_Pro.Label34.Text = time_req
                Working_Pro.lb_ch_man_flg.Text = "1"
                Working_Pro.Label20.Text = " -- : --"
                Working_Pro.Enabled = True
                Backoffice_model.Check_detail_actual_insert_act_no_api(Me) 'ทำให้ช้า'
                Working_Pro.showWorkker()
                Working_Pro.Enabled = True
                Working_Pro.Show()
                Me.Hide()
            End If
        End If
        If i > 0 Then
            Working_Pro.LB_COUNTER_SEQ.Text = 0
        End If
        'Prd_detail.Show()
    End Sub
    Private Sub List_Emp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim back = New Backoffice_model
        MaxManPower = back.Get_MaxManPower(MainFrm.Label4.Text)
        Timer1.Start()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Dim list_item As ListViewItem
        Dim str As String = lb_count_emp.Text
        'MsgBox(str)
        Dim temp1 As Integer = Convert.ToInt32(lb_count_emp.Text)
        Dim ListView1_count As Integer = ListView1.Items.Count
        Dim flag_emp2 As Integer = Convert.ToInt32(flag_emp.Text)
        Dim temp3 = ListView1_count - temp1
        If temp3 > 0 Then
            For i As Integer = 1 To temp3
                If temp1 = 0 Then
                    ListView1.Items(0).Remove()
                    ListBox2.Items().RemoveAt(0)
                Else
                    Try
                        ListView1.Items(temp1).Remove()
                        ListBox2.Items().RemoveAt(temp1)
                    Catch ex As Exception

                    End Try
                End If
            Next
        End If
        'MsgBox(temp2)
        lb_count_emp.Text = ListView1.Items.Count
        MainFrm.Enabled = True
        MainFrm.Show()
        Me.Hide()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = TimeOfDay.ToString("H:mm:ss")
        Label1.Text = DateTime.Now.ToString("D")
        Label2.Text = DateTime.Now.ToString("yyyy/MM/dd")
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim del_number As Integer = ListView1.SelectedIndices(0)

        For Each item As ListViewItem In ListView1.SelectedItems
            Dim temp_selected As Integer = ListView1.SelectedIndices(0)
            'MsgBox(temp_selected)
            Dim lb_count As Integer = Convert.ToInt32(lb_count_emp.Text)
            lb_count = lb_count - 1
            lb_count_emp.Text = lb_count
            ListBox2.Items.RemoveAt(temp_selected)
            item.Remove()
        Next

        Dim numPrd As Integer = Me.ListView1.Items.Count
        If numPrd < 1 Then
            'Me.Button2.Enabled = False
        End If

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

        If ListView1.SelectedItems.Count <= 0 Then
            Button3.Enabled = False
        ElseIf ListView1.SelectedItems.Count > 0 Then
            Button3.Enabled = True
        End If

    End Sub

    Private Function Ck_dup(ByVal Lis As ListBox, ByVal Str As String)
        'Dim Lit = ListBox1.Items.Count - 1
        For i = 0 To Lis.Items.Count - 1

            If Lis.Items(i) = Str Then
                Return True
            End If

            'Lit = ListBox1.Items.Count - 1
            'MsgBox("Code is : " & ListBox1.Items(0).ToString, 0, "Show")
            'ListBox1.Items.RemoveAt(0)
        Next

        Return False
    End Function

    Private Sub pb_down_Click(sender As Object, e As EventArgs) Handles pb_down.Click
        BTNDOWN1()
    End Sub
    Public Sub BTNUP1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((ListView1.Items.Count - 1))) Then
            S_index = CDbl(Val((ListView1.Items.Count - 1)))
        End If
        Try
            ListView1.Items(S_index).Selected = False
            S_index -= 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf lvDefectact.Items.Count > S_index Then
                'S_index = CDbl(Val((lvDefectact.Items.Count - 1)))
            End If
            ListView1.Items(S_index).Selected = True
            ListView1.Items(S_index).EnsureVisible()
            ListView1.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BTNDOWN1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((ListView1.Items.Count - 1))) Then
            S_index = CDbl(Val((ListView1.Items.Count - 1)))
        End If
        Try
            ListView1.Items(S_index).Selected = False
            S_index += 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf S_index > lvDefectact.Items.Count Then
                '  S_index = CDbl(Val((lvDefectact.Items.Count - 1)))
            End If
            ListView1.Items(S_index).Selected = True
            ListView1.Items(S_index).EnsureVisible()
            ListView1.Select()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub pbUp_Click(sender As Object, e As EventArgs) Handles pbUp.Click
        BTNUP1()
    End Sub
End Class
