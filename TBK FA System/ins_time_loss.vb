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
Imports System.Web.Script.Serialization

Public Class ins_time_loss
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
        Loss_reg_pass.Enabled = True
        Me.Hide()
    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Try
                    If Backoffice_model.S_chk_spec_line = 0 Then ' Normal
                        Dim date_start_shift As Date = Backoffice_model.date_time_start_master_shift
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
                        Dim total_time_loss As Integer
                        Dim date1 As Date = Date.Parse(TextBox1.Text)
                        Dim date2 As Date = Date.Parse(TextBox2.Text)
                        If Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "B" Or Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "Q" Or Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "S" Then
                            If TimeOfDay.ToString("HH:mm:ss") >= "00:00:00" And TimeOfDay.ToString("HH:mm:ss") <= "07:59:59" Then
                                ' MsgBox("IF -1 DAYS")
                                '   MsgBox("date_cerrunt_now1===>" & date_cerrunt_now1)
                                '   MsgBox("date_start_time===>" & date_start_time)
                                If date_cerrunt_now1 > date_start_time Then
                                    ' MsgBox("---->")
                                    ' MsgBox("date1 =" & date1)
                                    If TextBox1.Text >= "00:00:00" And TextBox1.Text <= "07:59:59" Then
                                        ' MsgBox("ggggg->>>")
                                        ' date1 = date1.AddDays(1)
                                    Else
                                        '  MsgBox("AAAA->>>")
                                        date1 = date1.AddDays(-1)
                                    End If
                                ElseIf date_cerrunt_now1 = date_start_time Then
                                    Dim update_start_shift_date_time As Date = date_start_shift.AddDays(-1)
                                    convert_date_start_time = update_start_shift_date_time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                                End If
                                If TextBox2.Text >= "00:00:00" And TextBox2.Text <= "07:59:59" Then
                                Else
                                    ' MsgBox("Please check time 5")
                                End If
                                If date_cerrunt_now1 > date_start_time Then
                                    If TextBox2.Text >= "00:00:00" And TextBox2.Text <= "07:59:59" Then
                                    Else
                                        date2 = date2.AddDays(-1)
                                    End If
                                ElseIf date_cerrunt_now1 = date_start_time Then
                                    Dim update_end_shift_date_time As Date = date_end_shift.AddDays(-1)
                                    convert_date_end_time = update_end_shift_date_time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                                End If
                            End If
                        End If
                        Dim start_time As String = date1.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                        Dim end_time As String = date2.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                        Dim date_cerrunt As Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        Dim time_cerrunt As Date = DateTime.Now.ToString("HH:mm:ss")
                        Dim result_date_click_start
                        result_date_click_start = Backoffice_model.date_time_click_start.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                        '     MsgBox("start_time = " & start_time)
                        '     MsgBox("result_date_click_start = " & result_date_click_start)
                        '    MsgBox("end_time = " & end_time)
                        '   MsgBox("convert_date_end_time = " & convert_date_end_time)
                        Dim Minutes1 As Long = DateDiff(DateInterval.Minute, date1, date2)
                        ' MsgBox("Minutes1 = " & Minutes1.ToString)
                        If start_time >= result_date_click_start And Minutes1 > 0 Then ' check ตอน clisk  start
                            If start_time >= convert_date_start_time And start_time <= convert_date_end_time Then ' Check Time Start or check shift
                                'MsgBox("OK1")
                                'MsgBox("end_time = " & end_time)
                                'MsgBox("convert_date_end_time = " & convert_date_end_time)
                                'MsgBox("convert_date_start_time = " & convert_date_start_time)
                                If end_time <= convert_date_end_time And end_time >= convert_date_start_time Then ' Check Time Start 
                                    'MsgBox("OK2")
                                    Dim date_now As Date = Date.Now
                                    Dim date_time = date_now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                                    'MsgBox("date_time = " & date_time)
                                    'MsgBox("end_time = " & end_time)
                                    If date_time >= end_time Then
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
                                        Dim GET_CHECK_LOSS_reuslt = Backoffice_model.GET_CHECK_LOSS(start_time, end_time)
                                        Dim check_double_loss As Integer = 0
                                        Dim count_check_double As String = ""
                                        count_check_double = GET_CHECK_LOSS_reuslt
                                        'If CDbl(Val(Backoffice_model.CHECK_TRANSCETION_PRODUCTION_DETAIL(MainFrm.Label4.Text, start_time, end_time))) = 0 Then
                                        If CDbl(Val(count_check_double)) > 0 Then
                                            Dim LossCodeDup As String = ""
                                            Dim recordedDate As String = ""
                                            Dim rsDataLossDup = Backoffice_model.GetDataLoss(start_time, end_time, MainFrm.Label4.Text)
                                            If rsDataLossDup <> "0" Then
                                                Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsDataLossDup)
                                                For Each item As Object In dict
                                                    LossCodeDup = item("loss_cd").ToString()
                                                    recordedDate = item("start_timeLoss").ToString() & " - " & item("end_timeLoss").ToString()
                                                Next
                                            End If
                                            ' MsgBox("Loss double please check start loss and end loss")
                                            ena012()
                                            Button10.Enabled = False
                                            Button11.Enabled = False
                                            'Dim listdetail = "Loss double please check start loss and end loss"
                                            Dim listdetail = "This time has been recorded loss." & vbCrLf
                                            lbDetailLoss.Text = "Loss : " & LossCodeDup & " " & vbCrLf & recordedDate
                                            lbDetailLoss.Visible = True
                                            PictureBox10.BringToFront()
                                            PictureBox10.Show()
                                            PictureBox2.BringToFront()
                                            PictureBox2.Show()
                                            Panel2.BringToFront()
                                            Panel2.Show()
                                            Label3.Text = listdetail
                                            Label3.BringToFront()
                                            Label3.Show()
                                        Else
                                            ena012()
                                            lbDetailLoss.Visible = False
                                            Loss_reg_pass.Label8.Text = TextBox1.Text
                                            Loss_reg_pass.Label9.Text = TextBox2.Text
                                            Loss_reg_pass.Enabled = True
                                            Loss_reg_pass.Button1.Visible = True
                                            Me.Hide()
                                        End If
                                    Else
                                        ena012()
                                        lbDetailLoss.Visible = False
                                        Button10.Enabled = False
                                        Button11.Enabled = False
                                        Dim listdetail = "Please Check Time 2 ."
                                        PictureBox10.BringToFront()
                                        PictureBox10.Show()
                                        PictureBox2.BringToFront()
                                        PictureBox2.Show()
                                        Panel2.BringToFront()
                                        Panel2.Show()
                                        Label3.Text = listdetail
                                        Label3.BringToFront()
                                        Label3.Show()
                                        'MsgBox("Please Check Time.")
                                    End If
                                Else
                                    ena012()
                                    lbDetailLoss.Visible = False
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
                                    'MsgBox("Please Check Shift.")
                                End If
                            Else
                                ena012()
                                lbDetailLoss.Visible = False
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
                                ' MsgBox("Please Check Shift.")
                            End If
                        Else
                            ena012()
                            Button10.Enabled = False
                            Button11.Enabled = False
                            lbDetailLoss.Visible = False
                            Dim listdetail = "Please Check time 3 ."
                            PictureBox10.BringToFront()
                            PictureBox10.Show()
                            PictureBox2.BringToFront()
                            PictureBox2.Show()
                            Panel2.BringToFront()
                            Panel2.Show()
                            Label3.Text = listdetail
                            Label3.BringToFront()
                            Label3.Show()
                            ' MsgBox("Please Check time.")
                        End If
                    Else ' For K1M025
                        ' MsgBox("K1M025")
                        Dim date_start_shift As Date = Working_Pro.DateTimeStartofShift.Text ' Backoffice_model.date_time_start_master_shift
                        Dim date_end_shift As Date = Backoffice_model.date_time_end_check_date_paralell_linet
                        Dim date_start_time As String = date_start_shift.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        Dim convert_date_start_time As String = date_start_shift.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                        Dim convert_date_end_time As String = date_end_shift.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                        Dim date_end_time As String = date_end_shift.ToString("dd/mm/yy", CultureInfo.InvariantCulture)
                        Dim convert_date_time_start As String = date_start_shift.ToString("HH:mm:ss", CultureInfo.InvariantCulture)
                        Dim convert_date_end_shift As String = date_end_shift.ToString("HH:mm:ss", CultureInfo.InvariantCulture)
                        ' ' MsgBox("--->")
                        ' MsgBox("convert_date_time_start = " & convert_date_time_start)
                        'MsgBox("convert_date_end_shift = " & convert_date_end_shift)
                        'MsgBox("=============<><>===")
                        Dim date_cerrunt_now As Date = DateTime.Now.ToString()
                        Dim date_cerrunt_now1 = date_cerrunt_now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        '  MsgBox("re====")
                        'MsgBox("date_cerrunt_now = " & date_cerrunt_now1)
                        '  MsgBox("date_start_time = " & date_start_time)
                        ' If TimeOfDay.ToString("HH:mm:ss") >= "00:00:00" And TimeOfDay.ToString("HH:mm:ss") <= "07:59:59" Then
                        If date_cerrunt_now1 > date_start_time Then ' check เลย เที่ยงคืน 
                            '   MsgBox("===>")
                            date_end_shift = Backoffice_model.date_time_end_check_date_paralell_linet.AddDays(1)
                            '   MsgBox("===>")
                        End If
                        ' MsgBox("01")
                        Dim total_time_loss As Integer
                        Dim date1 As Date = Date.Parse(TextBox1.Text)
                        'MsgBox("02")
                        Dim date2 As Date = Date.Parse(TextBox2.Text)
                        If Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "B" Or Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "Q" Or Trim(Prd_detail.Label12.Text.Substring(0, 1)) = "S" Then
                            '   MsgBox("TimeOfDay   = " & TimeOfDay.ToString("HH:mm:ss"))
                            If TimeOfDay.ToString("HH:mm:ss") >= "00:00:00" And TimeOfDay.ToString("HH:mm:ss") <= "07:59:59" Then
                                ' MsgBox("IF -1 DAYS")
                                '   MsgBox("date_cerrunt_now1===>" & date_cerrunt_now1)
                                '   MsgBox("date_start_time===>" & date_start_time)
                                If date_cerrunt_now1 > date_start_time Then
                                    ' MsgBox("---->")
                                    ' MsgBox("date1 =" & date1)
                                    If TextBox1.Text >= "00:00:00" And TextBox1.Text <= "07:59:59" Then
                                        ' MsgBox("ggggg->>>")
                                        ' date1 = date1.AddDays(1)
                                    Else
                                        '  MsgBox("AAAA->>>")
                                        date1 = date1.AddDays(-1)
                                    End If
                                ElseIf date_cerrunt_now1 = date_start_time Then
                                    Dim update_start_shift_date_time As Date = date_start_shift.AddDays(-1)
                                    convert_date_start_time = update_start_shift_date_time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                                End If
                                If TextBox2.Text >= "00:00:00" And TextBox2.Text <= "07:59:59" Then
                                Else
                                    ' MsgBox("Please check time 5")
                                End If
                                If date_cerrunt_now1 > date_start_time Then
                                    If TextBox2.Text >= "00:00:00" And TextBox2.Text <= "07:59:59" Then
                                    Else
                                        date2 = date2.AddDays(-1)
                                    End If
                                ElseIf date_cerrunt_now1 = date_start_time Then
                                    Dim update_end_shift_date_time As Date = date_end_shift.AddDays(-1)
                                    convert_date_end_time = update_end_shift_date_time.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                                End If
                            End If
                        End If
                        Dim start_time As String = date1.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                        Dim end_time As String = date2.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                        Dim date_cerrunt As Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        Dim time_cerrunt As Date = DateTime.Now.ToString("HH:mm:ss")
                        Dim result_date_click_start
                        Dim dateTimeValue As DateTime = DateTime.ParseExact(Working_Pro.DateTimeStartofShift.Text, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                        ' แปลง DateTime เป็น String ในรูปแบบที่ต้องการ
                        result_date_click_start = dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                        Dim Minutes1 As Long = DateDiff(DateInterval.Minute, date1, date2)
                        'MsgBox(start_time & ">=" & result_date_click_start)
                        'MsgBox("Minutes1===>" & Minutes1)
                        If start_time >= result_date_click_start And Minutes1 > 0 Then ' check ตอน clisk  start
                            ' MsgBox(start_time & ">=" & convert_date_start_time & "AND" & start_time & "<=" & convert_date_end_time)
                            If start_time >= convert_date_start_time And start_time <= convert_date_end_time Then ' Check Time Start or check shift
                                'MsgBox("OK1")
                                'MsgBox("end_time = " & end_time)
                                'MsgBox("convert_date_end_time = " & convert_date_end_time)
                                'MsgBox("convert_date_start_time = " & convert_date_start_time)
                                If end_time <= convert_date_end_time And end_time >= convert_date_start_time Then ' Check Time Start 
                                    'MsgBox("OK2")
                                    Dim date_now As Date = Date.Now
                                    Dim date_time = date_now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                                    'MsgBox("date_time = " & date_time)
                                    'MsgBox("end_time = " & end_time)
                                    If date_time >= end_time Then
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
                                        Dim GET_CHECK_LOSS_reuslt = Backoffice_model.GET_CHECK_LOSS(start_time, end_time)
                                        Dim check_double_loss As Integer = 0
                                        Dim count_check_double As String = ""
                                        count_check_double = GET_CHECK_LOSS_reuslt
                                        'If CDbl(Val(Backoffice_model.CHECK_TRANSCETION_PRODUCTION_DETAIL(MainFrm.Label4.Text, start_time, end_time))) = 0 Then
                                        ena012()
                                        Loss_reg_pass.Label8.Text = TextBox1.Text
                                        Loss_reg_pass.Label9.Text = TextBox2.Text
                                        Loss_reg_pass.Enabled = True
                                        Loss_reg_pass.Button1.Visible = True
                                        Me.Hide()
                                        'Else
                                        ' Button10.Enabled = False
                                        ' Button11.Enabled = False
                                        ' Dim listdetail = "Have Data In Production Actual Detail Please Check."
                                        ' PictureBox10.BringToFront()
                                        ' PictureBox10.Show()
                                        ' PictureBox2.BringToFront()
                                        ' PictureBox2.Show()
                                        ' Panel2.BringToFront()
                                        ' Panel2.Show()
                                        ' Label3.Text = listdetail
                                        ' Label3.BringToFront()
                                        ' Label3.Show()
                                        'End If
                                        'MsgBox("OK READY CAL")
                                    Else
                                        ena012()
                                        Button10.Enabled = False
                                        Button11.Enabled = False
                                        Dim listdetail = "Please Check Time 5 ."
                                        PictureBox10.BringToFront()
                                        PictureBox10.Show()
                                        PictureBox2.BringToFront()
                                        PictureBox2.Show()
                                        Panel2.BringToFront()
                                        Panel2.Show()
                                        Label3.Text = listdetail
                                        Label3.BringToFront()
                                        Label3.Show()
                                        'MsgBox("Please Check Time.")
                                    End If
                                Else
                                    ena012()
                                    Button10.Enabled = False
                                    Button11.Enabled = False
                                    Dim listdetail = "Please Check Shift 6."
                                    PictureBox10.BringToFront()
                                    PictureBox10.Show()
                                    PictureBox2.BringToFront()
                                    PictureBox2.Show()
                                    Panel2.BringToFront()
                                    Panel2.Show()
                                    Label3.Text = listdetail
                                    Label3.BringToFront()
                                    Label3.Show()
                                    'MsgBox("Please Check Shift.")
                                End If
                            Else
                                ena012()
                                Button10.Enabled = False
                                Button11.Enabled = False
                                Dim listdetail = "Please Check Shift 7."
                                PictureBox10.BringToFront()
                                PictureBox10.Show()
                                PictureBox2.BringToFront()
                                PictureBox2.Show()
                                Panel2.BringToFront()
                                Panel2.Show()
                                Label3.Text = listdetail
                                Label3.BringToFront()
                                Label3.Show()
                                ' MsgBox("Please Check Shift.")
                            End If
                        Else
                            Button10.Enabled = False
                            Button11.Enabled = False
                            Dim listdetail = "Please Check time 8 ."
                            PictureBox10.BringToFront()
                            PictureBox10.Show()
                            PictureBox2.BringToFront()
                            PictureBox2.Show()
                            Panel2.BringToFront()
                            Panel2.Show()
                            Label3.Text = listdetail
                            Label3.BringToFront()
                            Label3.Show()
                            ' MsgBox("Please Check time.")
                        End If
                    End If
                Catch ex As Exception
                    ena012()
                    Button10.Enabled = False
                    Button11.Enabled = False
                    Dim listdetail = "Please Check time 9."
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
        If TextBox1.Text.Length = 1 Then
            If TextBox1.Text = "0" Or TextBox1.Text = "1" Then
                allena()
            Else
                ena0123()
            End If
        ElseIf TextBox1.Text.Length = 2 Then
            ena012345()
        ElseIf TextBox1.Text.Length = 4 Then
            allena()
        ElseIf TextBox1.Text.Length = 5 Then
            ena012()
        End If

        If TextBox2.Text.Length = 1 Then
            If TextBox2.Text = "0" Or TextBox2.Text = "1" Then
                allena()
            Else
                ena0123()
            End If
        ElseIf TextBox2.Text.Length = 2 Then
            ena012345()
        ElseIf TextBox2.Text.Length = 4 Then
            allena()
        ElseIf TextBox2.Text.Length = 5 Then
            ena012()
        End If

        If TextBox1.Text.Length = 0 Then
            ena012()
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub ins_time_loss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lb_PartNo.Text = Working_Pro.Label3.Text
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Button10.Enabled = True
        Button11.Enabled = True
        PictureBox10.Visible = False
        PictureBox2.Visible = False
        Panel2.Visible = False
    End Sub
End Class
