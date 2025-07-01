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
Imports BarcodeLib.Barcode
Public Class ins_qty_special_time
	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim text_now As String = TextBox1.Text
        If text_now.Length > 4 Then
			Dim text_to2 As String = TextBox2.Text
			If text_to2.Length = 2 Then
				text_to2 = text_to2 & ":1"
				TextBox2.Text = text_to2
			ElseIf text_to2.Length = 1 Then
				text_to2 = text_to2 & "1"
				TextBox2.Text = text_to2
				allena()
			Else
				text_to2 = text_to2 & "1"
				TextBox2.Text = text_to2
			End If
		Else
			If text_now.Length = 2 Then
				text_now = text_now & ":1"
				TextBox1.Text = text_now
			Else
				text_now = text_now & "1"
				TextBox1.Text = text_now
			End If
		End If
		chk_ins()
		Dim text_to2_now As String = TextBox2.Text
		If text_to2_now.Length > 4 Then
			alldis()
		End If
	End Sub
	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Dim text_now As String = TextBox1.Text
		If text_now.Length > 4 Then
			Dim text_to2 As String = TextBox2.Text
			If text_to2.Length = 2 Then
				text_to2 = text_to2 & ":2"
				TextBox2.Text = text_to2
			Else
				text_to2 = text_to2 & "2"
				TextBox2.Text = text_to2
			End If
		Else
			If text_now.Length = 2 Then
				text_now = text_now & ":2"
				TextBox1.Text = text_now
			Else
				text_now = text_now & "2"
				TextBox1.Text = text_now
			End If
		End If
		chk_ins()
		Dim text_to2_now As String = TextBox2.Text
		If text_to2_now.Length > 4 Then
			alldis()
		End If
	End Sub
	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		Dim text_now As String = TextBox1.Text
		If text_now.Length > 4 Then
			Dim text_to2 As String = TextBox2.Text

			If text_to2.Length = 2 Then
				text_to2 = text_to2 & ":3"
				TextBox2.Text = text_to2
			Else
				text_to2 = text_to2 & "3"
				TextBox2.Text = text_to2
			End If
		Else
			If text_now.Length = 2 Then
				text_now = text_now & ":3"
				TextBox1.Text = text_now
			Else
				text_now = text_now & "3"
				TextBox1.Text = text_now
			End If
		End If
		chk_ins()
		Dim text_to2_now As String = TextBox2.Text
		If text_to2_now.Length > 4 Then
			alldis()
		End If
	End Sub
	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		Dim text_now As String = TextBox1.Text
		If text_now.Length > 4 Then
			Dim text_to2 As String = TextBox2.Text
			If text_to2.Length = 2 Then
				text_to2 = text_to2 & ":4"
				TextBox2.Text = text_to2
			Else
				text_to2 = text_to2 & "4"
				TextBox2.Text = text_to2
			End If
		Else
			If text_now.Length = 2 Then
				text_now = text_now & ":4"
				TextBox1.Text = text_now
			Else
				text_now = text_now & "4"
				TextBox1.Text = text_now
			End If
		End If
		chk_ins()
		Dim text_to2_now As String = TextBox2.Text
		If text_to2_now.Length > 4 Then
			alldis()
		End If
	End Sub
	Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
		Dim text_now As String = TextBox1.Text
		If text_now.Length > 4 Then
			Dim text_to2 As String = TextBox2.Text
			If text_to2.Length = 2 Then
				text_to2 = text_to2 & ":5"
				TextBox2.Text = text_to2
			Else
				text_to2 = text_to2 & "5"
				TextBox2.Text = text_to2
			End If
		Else
			If text_now.Length = 2 Then
				text_now = text_now & ":5"
				TextBox1.Text = text_now
			Else
				text_now = text_now & "5"
				TextBox1.Text = text_now
			End If
		End If
		chk_ins()
		Dim text_to2_now As String = TextBox2.Text
		If text_to2_now.Length > 4 Then
			alldis()
		End If
	End Sub
	Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
		Dim text_now As String = TextBox1.Text
		If text_now.Length > 4 Then
			Dim text_to2 As String = TextBox2.Text
			If text_to2.Length = 2 Then
				text_to2 = text_to2 & ":6"
				TextBox2.Text = text_to2
			Else
				text_to2 = text_to2 & "6"
				TextBox2.Text = text_to2
			End If
		Else
			If text_now.Length = 2 Then
				text_now = text_now & ":6"
				TextBox1.Text = text_now
			Else
				text_now = text_now & "6"
				TextBox1.Text = text_now
			End If
		End If
		chk_ins()
		Dim text_to2_now As String = TextBox2.Text
		If text_to2_now.Length > 4 Then
			alldis()
		End If
	End Sub
	Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
		Dim text_now As String = TextBox1.Text
        If text_now.Length > 4 Then
            Dim text_to2 As String = TextBox2.Text
            If text_to2.Length = 2 Then
                text_to2 = text_to2 & ":7"
                TextBox2.Text = text_to2
            Else
                text_to2 = text_to2 & "7"
                TextBox2.Text = text_to2
            End If
        Else
            If text_now.Length = 2 Then
				text_now = text_now & ":7"
				TextBox1.Text = text_now
			Else
				text_now = text_now & "7"
				TextBox1.Text = text_now
			End If
		End If
		chk_ins()
		Dim text_to2_now As String = TextBox2.Text
        If text_to2_now.Length > 4 Then
            alldis()
        End If
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim text_now As String = TextBox1.Text
        If text_now.Length > 4 Then
            Dim text_to2 As String = TextBox2.Text

            If text_to2.Length = 2 Then
                text_to2 = text_to2 & ":8"
                TextBox2.Text = text_to2
            Else
                text_to2 = text_to2 & "8"
                TextBox2.Text = text_to2
            End If
        Else
            If text_now.Length = 2 Then
                text_now = text_now & ":8"
                TextBox1.Text = text_now
            Else
                text_now = text_now & "8"
                TextBox1.Text = text_now
            End If
        End If
        chk_ins()
        Dim text_to2_now As String = TextBox2.Text
        If text_to2_now.Length > 4 Then
            alldis()
        End If
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
		Dim text_now As String = TextBox1.Text
		If text_now.Length > 4 Then
			Dim text_to2 As String = TextBox2.Text
			If text_to2.Length = 2 Then
				text_to2 = text_to2 & ":9"
				TextBox2.Text = text_to2
			Else
				text_to2 = text_to2 & "9"
				TextBox2.Text = text_to2
			End If
		Else
			If text_now.Length = 2 Then
				text_now = text_now & ":9"
				TextBox1.Text = text_now
			Else
				text_now = text_now & "9"
				TextBox1.Text = text_now
			End If
		End If
		chk_ins()
		Dim text_to2_now As String = TextBox2.Text
		If text_to2_now.Length > 4 Then
			alldis()
		End If
	End Sub
    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim text_now As String = TextBox1.Text
        If text_now.Length > 4 Then
            Dim text_to2 As String = TextBox2.Text

            If text_to2.Length = 2 Then
                text_to2 = text_to2 & ":0"
                TextBox2.Text = text_to2
            Else
                text_to2 = text_to2 & "0"
                TextBox2.Text = text_to2
            End If
        Else
            If text_now.Length = 2 Then
                text_now = text_now & ":0"
                TextBox1.Text = text_now
            Else
                text_now = text_now & "0"
                TextBox1.Text = text_now
            End If
        End If
        chk_ins()
        Dim text_to2_now As String = TextBox2.Text
        If text_to2_now.Length > 4 Then
            alldis()
        End If
    End Sub
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        TextBox1.Clear()
        TextBox2.Clear()
        ena012()
        'Button1.Enabled = True
        'Button2.Enabled = True
        'Button3.Enabled = True
        'Button4.Enabled = True
        'Button5.Enabled = True
        'Button6.Enabled = True
        'Button7.Enabled = True
        'Button8.Enabled = True
        'Button9.Enabled = True
        'Button13.Enabled = True
    End Sub
    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim text_to2 As String = TextBox2.Text
        Dim text_to1 As String = TextBox1.Text
        If text_to2.Length > 0 Then
            'MsgBox(text_to2.Length)
            If text_to2.Length = 1 Then
                Dim result_str As String = text_to2.Substring(1)
                TextBox2.Text = result_str
            ElseIf text_to2.Length = 2 Then
                Dim result_str As String = text_to2.Substring(0, 1)
                TextBox2.Text = result_str
            ElseIf text_to2.Length = 4 Then
                Dim result_str As String = text_to2.Substring(0, 2)
                TextBox2.Text = result_str
            ElseIf text_to2.Length = 5 Then
                Dim result_str As String = text_to2.Substring(0, 4)
                TextBox2.Text = result_str
            End If
        End If
        If text_to2.Length = 0 Then
            If text_to1.Length > 0 Then
                'MsgBox(text_to2.Length)
                If text_to1.Length = 1 Then
                    Dim result_str As String = text_to1.Substring(1)
                    TextBox1.Text = result_str
                ElseIf text_to1.Length = 2 Then
                    Dim result_str As String = text_to1.Substring(0, 1)
                    TextBox1.Text = result_str
                ElseIf text_to1.Length = 4 Then
                    Dim result_str As String = text_to1.Substring(0, 2)
                    TextBox1.Text = result_str
                ElseIf text_to1.Length = 5 Then
                    Dim result_str As String = text_to1.Substring(0, 4)
                    TextBox1.Text = result_str
                End If
            End If
        End If
        'text_to2 = TextBox2.Text
        'MsgBox(TextBox2.Text.Length)
        chk_ins()
    End Sub
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        TextBox1.Clear()
        TextBox2.Clear()
        ena012()
        Me.Close()
    End Sub
    Public Sub check_spc_time_time()
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Try
                    Dim timeclickStart As Date
                    Dim DatetimeclickStart
                    'MsgBox("Backoffice_model.S_chk_spec_line===>" & Backoffice_model.S_chk_spec_line)
                    If Backoffice_model.S_chk_spec_line = 0 Then
                        timeclickStart = DateTime.Now.ToString()
                        DatetimeclickStart = Backoffice_model.date_time_click_start.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                    Else ' For K1M025
                        timeclickStart = DateTime.Now.ToString()
                        'DatetimeclickStart = Backoffice_model.date_time_start_master_shift.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                        Dim dateTimeValue As DateTime = DateTime.Parse(Working_Pro.DateTimeStartofShift.Text)
                        DatetimeclickStart = dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss")
                    End If
                    'Dim date_start_shift As Date = Backoffice_model.date_time_start_master_shift เอาเวลา ของ Shift ตั้งต้นขึ้นมา
                    '''' เป็นการเอาเวลาของการ กด ปุ่ม STarts มาแทน 
                    'DatetimeclickStart = DatetimeclickStart & " " & show_time_add_qty.Text & ":00"
                    ''''end เป็นการเอาเวลาของการ กด ปุ่ม STarts มาแทน 
                    Dim date_start_shift As Date = DatetimeclickStart
                    Dim date_end_shift As Date = Backoffice_model.date_time_end_check_date_paralell_linet
                    Dim date_start_time As String = date_start_shift.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                    Dim convert_date_start_time As String = date_start_shift.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                    Dim convert_date_end_time As String = date_end_shift.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                    Dim date_end_time As String = date_end_shift.ToString("dd/mm/yy", CultureInfo.InvariantCulture)
                    Dim convert_date_time_start As String = date_start_shift.ToString("HH:mm:ss", CultureInfo.InvariantCulture)
                    Dim convert_date_end_shift As String = date_end_shift.ToString("HH:mm:ss", CultureInfo.InvariantCulture)
                    Dim date_cerrunt_now As Date = DateTime.Now.ToString()
                    Dim date_cerrunt_now1 = date_cerrunt_now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                    If date_cerrunt_now1 > date_start_time Then ' check เลย เที่ยงคืน 
                        date_end_shift = Backoffice_model.date_time_end_check_date_paralell_linet.AddDays(1)
                    End If
                    Dim time_cerrunt = date_cerrunt_now.ToString("HH:mm:ss", CultureInfo.InvariantCulture)
                    Dim total_time_loss As Integer
                    Dim date1 As Date = Date.Parse(TextBox1.Text)
                    Dim date2 As Date = Date.Parse(TextBox2.Text)
                    If Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "B" Or Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "Q" Or Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "S" Then
                        If TimeOfDay.ToString("HH:mm:ss") >= "00:00:00" And TimeOfDay.ToString("HH:mm:ss") <= "07:59:59" Then
                            If date_cerrunt_now1 > date_start_time Then
                                If TextBox1.Text >= "00:00:00" And TextBox1.Text <= "07:59:59" Then
                                Else
                                    date1 = date1.AddDays(-1)
                                End If
                            ElseIf date_cerrunt_now1 = date_start_time Then
                                date1 = date1.AddDays(-1)
                                Dim update_start_shift_date_time As Date = date_start_shift.AddDays(-1)
                                convert_date_start_time = update_start_shift_date_time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                            End If
                            If TextBox2.Text >= "00:00:00" And TextBox2.Text <= "07:59:59" Then
                            Else
                            End If
                            If date_cerrunt_now1 > date_start_time Then
                                ' MsgBox("IF1")
                                If TextBox2.Text >= "00:00:00" And TextBox2.Text <= "07:59:59" Then
                                Else
                                    ' MsgBox("ELSE 1")
                                    date2 = date2.AddDays(-1)
                                End If
                                ' MsgBox("date2==>" & date2)
                            ElseIf date_cerrunt_now1 = date_start_time Then
                                '  MsgBox("ELSE IF1")
                                'MsgBox(TextBox2.Text)
                                If TextBox2.Text >= "00:00:00" And TextBox2.Text <= "07:59:59" Then
                                    '  MsgBox("AA1")
                                    If time_cerrunt >= "00:00:00" And time_cerrunt <= "07:59:59" Then
                                        ' MsgBox("AAS1")
                                        ' MsgBox(TextBox1.Text)
                                        ' MsgBox(Trim(TextBox1.Text) >= "00:00:00")
                                        ' MsgBox(TextBox1.Text <= "07:59:59")
                                        If Trim(TextBox1.Text) >= "00:00:00" And Trim(TextBox1.Text) <= "07:59:59" Then
                                            '    MsgBox("FFFF2")
                                            date1 = date1.AddDays(1)
                                        End If
                                        'date2 = date2.AddDays(-1)
                                    Else
                                        'MsgBox("ELSE 1")
                                    End If
                                Else
                                    'MsgBox("B1")
                                    date2 = date2.AddDays(-1)
                                    'MsgBox("ELSE 2 ")
                                End If
                                ' Dim update_end_shift_date_time As Date = date_end_shift.AddDays(-1)
                                'convert_date_end_time = update_end_shift_date_time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                            End If
                        End If
                    End If
                    'MsgBox("date1==>" & date1)
                    Dim start_time As String = date1.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                    Dim end_time As String = date2.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                    ' MsgBox("start_time==>" & start_time)
                    ' MsgBox("end_time==>" & end_time)
                    Dim date_cerrunt As Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    date_cerrunt1 = date_cerrunt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & ":00"
                    Dim result_date_click_start = Backoffice_model.date_time_click_start.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                    Dim Minutes1 As Long = DateDiff(DateInterval.Minute, date1, date2)
                    ' MsgBox("DatetimeclickStart ===>" & DatetimeclickStart)
                    ' MsgBox("start_time ===>" & start_time)
                    ' MsgBox("end time===>" & end_time)
                    Dim result As Boolean
                    If Backoffice_model.S_chk_spec_line = 1 Then
                        If Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "B" Or Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "Q" Or Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "S" Then
                            result = True
                        Else
                            If start_time >= DatetimeclickStart Then
                                result = True
                            Else
                                result = False
                            End If
                        End If
                    Else
                        If start_time >= DatetimeclickStart Then
                            result = True
                        Else
                            result = False
                        End If
                    End If
                    If result Then
                        If date_cerrunt1 >= start_time And date_cerrunt1 >= end_time Then ' check ตอน clisk  start
                            ' MsgBox("start_time====>" & start_time & " >= " & convert_date_start_time & " AND " & start_time & " <= " & convert_date_end_time)
                            If start_time >= convert_date_start_time And start_time <= convert_date_end_time Then ' Check Time Start or check shift
                                If end_time <= convert_date_end_time Then ' Check Time Start 
                                    Dim date_now As Date = Date.Now
                                    Dim date_time = date_now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                                    If Minutes1 > 0 Then
                                        Try
                                            Dim Minutes As Long = DateDiff(DateInterval.Minute, date1, date2)
                                            If Minutes.ToString < 0 Then
                                                Dim temmpola As String = Minutes.ToString.Substring(0)
                                                Dim tempp As Integer = temmpola
                                                tempp = Math.Abs(tempp)
                                                total_time_loss = 1440 - tempp
                                            Else
                                                total_time_loss = Minutes.ToString
                                            End If
                                        Catch ex As Exception

                                        End Try
                                        Dim check_time = Backoffice_model.check_time(Working_Pro.Label22.Text, Working_Pro.wi_no.Text, start_time, end_time)
                                        If check_time = "0" Then
                                            ins_qty.Label1.Visible = True
                                            ins_qty.Label3.Visible = True
                                            ins_qty.Start_input_data_spc.Visible = True
                                            ins_qty.End_input_data_spc.Visible = True
                                            ins_qty.Start_input_data_spc.Text = TextBox1.Text
                                            ins_qty.End_input_data_spc.Text = TextBox2.Text
                                            Backoffice_model.start_check_date_paralell_line = start_time
                                            Backoffice_model.end_check_date_paralell_line = end_time
                                            ins_qty.pb_ok.Visible = True
                                            Me.Close()
                                        Else
                                            'MsgBox("TIME DOUBLE.")
                                            Button10.Enabled = False
                                            Button11.Enabled = False
                                            Dim listdetail = "TIME DOUBLE ."
                                            PictureBox10.BringToFront()
                                            PictureBox10.Show()
                                            PictureBox2.BringToFront()
                                            PictureBox2.Show()
                                            Panel2.BringToFront()
                                            Panel2.Show()
                                            Label3.Text = listdetail
                                            Label3.BringToFront()
                                            Label3.Show()
                                        End If
                                    Else
                                        ' MsgBox("Please Check Time")
                                        Button10.Enabled = False
                                        Button11.Enabled = False
                                        Dim listdetail = "TIME DOUBLE ."
                                        PictureBox10.BringToFront()
                                        PictureBox10.Show()
                                        PictureBox2.BringToFront()
                                        PictureBox2.Show()
                                        Panel2.BringToFront()
                                        Panel2.Show()
                                        Label3.Text = listdetail
                                        Label3.BringToFront()
                                        Label3.Show()
                                        'MsgBox("Please Check Time")
                                    End If
                                Else
                                    ' MsgBox("Please Check Shift")
                                    ' MsgBox("Please Check Time")
                                    Button10.Enabled = False
                                    Button11.Enabled = False
                                    Dim listdetail = "Please Check Shift."
                                    PictureBox10.BringToFront()
                                    PictureBox10.Show()
                                    PictureBox2.BringToFront()
                                    PictureBox2.Show()
                                    Panel2.BringToFront()
                                    Panel2.Show()
                                    Label3.Text = listdetail
                                    Label3.BringToFront()
                                    Label3.Show()
                                End If
                            Else
                                'MsgBox("Please Check Shift  Or Time Start.")
                                Button10.Enabled = False
                                Button11.Enabled = False
                                Dim listdetail = "Please Check Shift  Or Time Start  ."
                                PictureBox10.BringToFront()
                                PictureBox10.Show()
                                PictureBox2.BringToFront()
                                PictureBox2.Show()
                                Panel2.BringToFront()
                                Panel2.Show()
                                Label3.Text = listdetail
                                Label3.BringToFront()
                                Label3.Show()
                            End If
                        Else
                            'MsgBox("Please Check time")
                            Button10.Enabled = False
                            Button11.Enabled = False
                            Dim listdetail = "Please Check time."
                            PictureBox10.BringToFront()
                            PictureBox10.Show()
                            PictureBox2.BringToFront()
                            PictureBox2.Show()
                            Panel2.BringToFront()
                            Panel2.Show()
                            Label3.Text = listdetail
                            Label3.BringToFront()
                            Label3.Show()
                        End If
                    Else
                        Button10.Enabled = False
                        Button11.Enabled = False
                        Dim listdetail = "Please Check time."
                        PictureBox10.BringToFront()
                        PictureBox10.Show()
                        PictureBox2.BringToFront()
                        PictureBox2.Show()
                        Panel2.BringToFront()
                        Panel2.Show()
                        Label3.Text = listdetail
                        Label3.BringToFront()
                        Label3.Show()
                    End If
                Catch ex As Exception
                    'MsgBox("Please check time")
                    Button10.Enabled = False
                    Button11.Enabled = False
                    Dim listdetail = "Please Check STart Time."
                    PictureBox10.BringToFront()
                    PictureBox10.Show()
                    PictureBox2.BringToFront()
                    PictureBox2.Show()
                    Panel2.BringToFront()
                    Panel2.Show()
                    Label3.Text = listdetail
                    Label3.BringToFront()
                    Label3.Show()
                End Try
            Else
                load_show.Show()
            End If
        Catch ex As Exception
            load_show.Show()
        End Try
    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        check_spc_time_time()
        '	Try
        '   Dim date_data As Date = DateTime.Now.ToString("HH:mm:ss")
        '  MsgBox("TextBox1.Text = " & TextBox1.Text)
        ' MsgBox("Backoffice_model.start_master_shift.Substring(0, 5) = " & Backoffice_model.start_master_shift.Substring(0, 5))
        'MsgBox("Backoffice_model.coles_lot_end_shift.Substring(0, 5) = " & Backoffice_model.coles_lot_end_shift.Substring(0, 5))
        'If (TextBox1.Text >= Backoffice_model.start_master_shift.Substring(0, 5) And TextBox1.Text <= Backoffice_model.coles_lot_end_shift.Substring(0, 5)) Then
        '       If (TextBox2.Text >= Backoffice_model.start_master_shift.Substring(0, 5) And TextBox2.Text <= Backoffice_model.coles_lot_end_shift.Substring(0, 5)) Then
        '        Dim total_time_loss As Integer
        '       Dim date1 As Date = Date.Parse(TextBox1.Text)
        '        Dim date2 As Date = Date.Parse(TextBox2.Text)'
        'Try
        '       Dim Minutes As Long = DateDiff(DateInterval.Minute, date1, date2)
        '        If Minutes.ToString < 0 Then'
        'Dim temmpola As String = Minutes.ToString.Substring(0)
        '       Dim tempp As Integer = temmpola
        '        tempp = Math.Abs(tempp)
        'total_time_loss = 1440 - tempp
        '       Else
        '        total_time_loss = Minutes.ToString
        ' End If
        '       Catch ex As Exception
        '        End Try'
        '   Dim start_time As String = date1.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
        '   Dim end_time As String = date2.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
        '        Dim date_now As String = date1.ToString("yyyy-MM-dd ", CultureInfo.InvariantCulture)
        '        Dim check_end As String = TextBox2.Text
        '        Dim now_time As Date = DateTime.Now.ToString("H:m:s")
        '        Dim end_check_date_paralell_line As String = ""
        '        Dim start_check_date_paralell_line As String = ""
        '       If TextBox1.Text < TextBox2.Text Then
        '      If TextBox2.Text >= "00:00" And TextBox2.Text <= "07:59" Then
        '     Dim date_cerrunt As Date = DateTime.Now.ToString("yyyy-MM-dd")
        '    Dim result_date_end As Date = date_cerrunt.AddDays(1) & " " & TextBox2.Text
        '   end_check_date_paralell_line = result_date_end.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
        '  Else
        ' end_check_date_paralell_line = end_time
        'End If
        'If TextBox1.Text >= "00:00" And TextBox1.Text <= "07:59" Then
        'Dim date_cerrunt As Date = DateTime.Now.ToString("yyyy-MM-dd")
        'Dim result_date_start As Date = date_cerrunt.AddDays(1) & " " & TextBox1.Text
        'start_check_date_paralell_line = result_date_start.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
        'Else
        'start_check_date_paralell_line = start_time
        'End If
        'Dim check_time = Backoffice_model.check_time(Working_Pro.Label22.Text, Working_Pro.wi_no.Text, start_check_date_paralell_line, end_check_date_paralell_line)
        'If check_time = "0" Then
        'Backoffice_model.start_check_date_paralell_line = start_check_date_paralell_line
        'Backoffice_model.end_check_date_paralell_line = end_check_date_paralell_line
        'Me.Close()
        'Else
        'MsgBox("TIME DOUBLE")
        'End If
        'Else
        'MsgBox("Please check Time 1")
        'End If
        '       Else
        '      MsgBox("Please check Time 2")
        '     End If
        '        Else
        '       MsgBox("Please check shift 3")
        '      End If
        '        Catch ex As Exception
        '       MsgBox("Please check Time 4")
        '      'MsgBox("ex.message = " & ex.Message)
        '     End Try
    End Sub
    Private Sub ena012()
		Button1.Enabled = True
		Button2.Enabled = True
		Button3.Enabled = False
		Button4.Enabled = False
		Button5.Enabled = False
		Button6.Enabled = False
		Button7.Enabled = False
		Button8.Enabled = False
		Button9.Enabled = False
		Button13.Enabled = True
	End Sub
	Private Sub ena0123()
		Button1.Enabled = True
		Button2.Enabled = True
		Button3.Enabled = True
		Button4.Enabled = False
		Button5.Enabled = False
		Button6.Enabled = False
		Button7.Enabled = False
		Button8.Enabled = False
		Button9.Enabled = False
		Button13.Enabled = True
	End Sub
	Private Sub ena012345()
		Button1.Enabled = True
		Button2.Enabled = True
		Button3.Enabled = True
		Button4.Enabled = True
		Button5.Enabled = True
		Button6.Enabled = False
		Button7.Enabled = False
		Button8.Enabled = False
		Button9.Enabled = False
		Button13.Enabled = True
	End Sub
    Private Sub alldis()
        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        Button5.Enabled = False
        Button6.Enabled = False
        Button7.Enabled = False
        Button8.Enabled = False
        Button9.Enabled = False
        Button13.Enabled = False
        Button10.Enabled = True
    End Sub
    Private Sub allena()
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True
        Button6.Enabled = True
        Button7.Enabled = True
        Button8.Enabled = True
        Button9.Enabled = True
        Button13.Enabled = True
    End Sub
    Private Sub chk_ins()
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True
        Button6.Enabled = True
        Button7.Enabled = True
        Button8.Enabled = True
        Button9.Enabled = True
        Button13.Enabled = True
    End Sub
    Private Sub ins_qty_special_time_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Dim date_start_shift As Date = Backoffice_model.date_time_start_master_shift
        If Backoffice_model.S_chk_spec_line = 0 Then
            Dim timestart As Date = Working_Pro.Label16.Text
            show_time_add_qty.Text = timestart.ToString("HH:mm:ss", CultureInfo.InvariantCulture)
        Else
            Dim dateTimeValue As DateTime = DateTime.Parse(Working_Pro.DateTimeStartofShift.Text)
            DatetimeclickStart = dateTimeValue.ToString("HH:mm:ss")
            show_time_add_qty.Text = DatetimeclickStart
            ' show_time_add_qty.Text = Backoffice_model.date_time_start_master_shift.ToString("HH:mm:ss", CultureInfo.InvariantCulture)
        End If
        chk_ins()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Text = TimeOfDay.ToString("H:mm:ss")
    End Sub
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Button10.Enabled = True
        Button11.Enabled = True
        PictureBox10.Hide()
        PictureBox2.Hide()
        Panel2.Hide()
    End Sub
End Class
