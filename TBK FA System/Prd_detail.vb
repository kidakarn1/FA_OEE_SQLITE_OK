Imports System.Net
Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class Prd_detail
    Public Shared S_index As Integer = 0
    Public Shared requestBody As New JObject()
    Public Shared status_check_ping = 0
    'Dim part1Color As Color = Color.FromArgb(44, 88, 131)
    'Dim part2Color As Color = Color.FromArgb(44, 85, 131)
    'Dim part3Color As Color = Color.FromArgb(44, 83, 131)
    'Dim part4Color As Color = Color.FromArgb(44, 80, 131)
    'Dim part5Color As Color = Color.FromArgb(44, 78, 131)
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        set_shift()
        'Label2.Text = TimeOfDay.ToString("H:mm:ss")
        'Label1.Text = DateTime.Now.ToString("yyyy/MM/dd")
    End Sub
    Private Sub Label4_Click(sender As Object, e As EventArgs)
    End Sub
    Public Function check_network()
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                status_check_ping = 1
            Else
                status_check_ping = 0
            End If
        Catch ex As Exception
            status_check_ping = 0
        End Try
        Return status_check_ping
    End Function
    Private Sub Prd_detail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainFrm.chk_spec_line = "2" Then
            lvWISpc.Visible = True
            lvWISpc.Enabled = True
            btnUp.Visible = True
            btnUp.Enabled = True
            btnDown.Visible = True
            btnDown.Enabled = True
            setDataSpecial()
        Else
            lvWISpc.Visible = False
            lvWISpc.Enabled = False
            picSpecial.Visible = False
            btnUp.Visible = False
            btnUp.Enabled = False
            btnDown.Visible = False
            btnDown.Enabled = False
        End If
        Dim i = List_Emp.ListView1.Items.Count
        If MainFrm.chk_spec_line = "2" Then
            Dim arrayWI As List(Of String) = New List(Of String)
            For Each itemPlanData As DataPlan In MainFrm.ArrayDataPlan
                arrayWI.Add(itemPlanData.wi)
            Next
            Backoffice_model.UpdateFlgZeroSpecial(arrayWI.ToArray)
            Backoffice_model.UpdateWorkingSpecial(arrayWI.ToArray)
        Else
            Backoffice_model.UpdateFlgZero(Label4.Text)
            Backoffice_model.UpdateWorking(lb_wi.Text)
        End If
        Label2.Text = i
        QTY_NG.Visible = False
        QTY_NC.Visible = False
        Timer1.Start()
        Timer2.Start()
        'Timer3.Enabled = True
        'sc_wi_plan.SerialPort1.Close()
        'If sc_wi_plan.SerialPort1.IsOpen Then sc_wi_plan.SerialPort1.Close()
    End Sub
    Private Sub Label13_Click(sender As Object, e As EventArgs)
        List_Emp.Show()
        Chang_sh.Close()
        Me.Close()
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Me.Enabled = True
        Chang_sh.Show()
    End Sub
    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Timer1.Enabled = False
        Me.Enabled = False
        Chang_sh.Show()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim line_id As String = MainFrm.line_id.Text
        Backoffice_model.line_status_upd(line_id)
        'List_Emp.Show()
        If MainFrm.chk_spec_line = "2" Then
            For Each itemPlanData As DataPlan In MainFrm.ArrayDataPlan
                Dim special_wi As String = itemPlanData.wi
                Backoffice_model.work_complete_offline(special_wi)
            Next
        Else
            Backoffice_model.work_complete_offline(lb_wi.Text)
        End If
        sc_wi_plan.TextBox1.Clear()
        MainFrm.Enabled = True
        MainFrm.Show()
        Me.Close()
    End Sub
    Public Sub check_lot()
        Try
            ' If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
            Dim i = List_Emp.ListView1.Items.Count
            If i > 0 Then
                Dim lotSubstYear As String = DateTime.Now.ToString("yyyy").Substring(3, 1)
                Dim lotFirstDigit As String = ""
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
                    lotSecondDigit = "G"
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
                Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
                Dim date_st As Integer = lotthirdDigit
                If time_now >= "00:00:00 AM" And time_now <= "08:00:00 AM" Then
                    date_st = lotthirdDigit - 1
                    If date_st <= 0 Then
                        Dim tmp_date As String = d.AddDays(-1)
                        lotthirdDigit = tmp_date.Substring(0, 2)
                        check_status_date = 1
                        lotSecondDigit = MainFrm.set_data_Month(tmp_date.Substring(3, 2))
                        lotFirstDigit = MainFrm.set_data_Year(tmp_date.Substring(6, 2))
                    Else
                        lotthirdDigit = date_st
                    End If
                Else
                    'lotthirdDigit -= 1
                End If
                'Dim defaultShift As String = ""
                'If timeShift = "05" Or timeShift = "06" Or timeShift = "07" Then
                'defaultShift = "Q (20:00 - 08:00)"
                'ElseIf timeShift = "08" Or timeShift = "09" Or timeShift = "10" Or timeShift = "11" Or timeShift = "12" Or timeShift = "13" Or timeShift = "14" Or timeShift = "15" Or timeShift = "16" Or timeShift = "17" Then
                'defaultShift = "P (08:00 - 20:00)"
                'ElseIf timeShift = "17" Or timeShift = "18" Or timeShift = "19" Then
                '	defaultShift = "P (08:00 - 20:00)"
                'ElseIf timeShift = "20" Or timeShift = "21" Or timeShift = "22" Or timeShift = "23" Or timeShift = "24" Or timeShift = "00" Or timeShift = "01" Or timeShift = "02" Or timeShift = "03" Or timeShift = "04" Or timeShift = "05" Then
                'defaultShift = "Q (20:00 - 08:00)"
                'End If
                '		Label12.Text = defaultShift
                If Len(Trim(date_st)) <= 1 Then
                    Dim date_digit
                    If date_st = 0 Then
                        Dim DATES As Date = DateTime.Now.ToString("dd-MM-yyyy")
                        If check_status_date = 0 Then
                            Dim GET_DATA As String = MainFrm.GetLastDayOfMonth(DATES)
                            Dim re = GET_DATA.Substring(0, 2)
                            lotthirdDigit = re
                            date_digit = re
                        Else
                            Dim tmp_date As String = d.AddDays(-1)
                            lotthirdDigit = tmp_date.Substring(0, 2)
                            date_digit = lotthirdDigit
                        End If
                    Else
                        lotthirdDigit = "0" & date_st
                        date_digit = "0" & date_st
                    End If
                    Label6.Text = lotFirstDigit & lotSecondDigit & date_digit
                Else
                    Label6.Text = lotFirstDigit & lotSecondDigit & lotthirdDigit
                End If
            Else
                'MsgBox("กรุณาลงข้อมูลพนักงานเพื่อเริ่มการผลิต")
            End If
            '    Else
            '      load_show.Show()
            '   End If
        Catch ex As Exception
            '  load_show.Show()
        End Try
    End Sub
    Public Sub set_shift()
        Dim timeShift As String = DateTime.Now.ToString("HH")
        Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
        Dim defaultShift As String = ""
        If timeShift = "05" Or timeShift = "06" Or timeShift = "07" Then
            'defaultShift = "N (05:00 - 08:00)"
            defaultShift = "Q (20:00 - 08:00)"
        ElseIf timeShift = "08" Or timeShift = "09" Or timeShift = "10" Or timeShift = "11" Or timeShift = "12" Or timeShift = "13" Or timeShift = "14" Or timeShift = "15" Or timeShift = "16" Or timeShift = "17" Then
            defaultShift = "P (08:00 - 20:00)"
        ElseIf timeShift = "17" Or timeShift = "18" Or timeShift = "19" Then
            ' defaultShift = "M (17:00 - 20:00)"
            defaultShift = "P (08:00 - 20:00)"
        ElseIf timeShift = "20" Or timeShift = "21" Or timeShift = "22" Or timeShift = "23" Or timeShift = "24" Or timeShift = "00" Or timeShift = "01" Or timeShift = "02" Or timeShift = "03" Or timeShift = "04" Or timeShift = "05" Then
            defaultShift = "Q (20:00 - 08:00)"
        End If
        Label12.Text = defaultShift
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Label1.Text = TimeOfDay.ToString("H:mm:ss")
        Label4.Text = DateTime.Now.ToString("D")
        Label22.Text = DateTime.Now.ToString("yyyy/MM/dd")
    End Sub
    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click
    End Sub
    Public Sub setDataSpecial()
        picSpecial.Visible = True
        Dim widthP = 70
        Dim heightP = 135
        Dim widthW = 320
        Dim heightW = 120
        Dim widthN = 47
        Dim heightN = 120
        Dim i As Integer = 0
        'Dim colorsArray() As Color = {part1Color, part2Color, part3Color, part4Color, part5Color}
        For Each itemPlanData As DataPlan In MainFrm.ArrayDataPlan
            Dim newLabelN As New Label()
            Dim newLabelP As New Label()
            Dim newLabelW As New Label()
            Dim No As Integer = i + 1
            Dim special_wi As String = itemPlanData.wi
            Dim special_item_cd As String = itemPlanData.item_cd
            ' newLabelN.Text = No
            ' newLabelN.Location = New Point(widthN, heightN) ' Set the position of the label
            ' newLabelN.Font = New Font("Catamaran", 16) ' Change "Arial" to the desired font and 12 to the desired font size
            ' newLabelN.AutoSize = True ' Adjust the size of the label based on its content
            ' newLabelN.ForeColor = Color.White
            'heightN += 37.5
            'Me.Controls.Add(newLabelN)
            'newLabelP.Text = special_item_cd
            'newLabelP.Location = New Point(widthP, heightP) ' Set the position of the label
            'newLabelP.Font = New Font("Catamaran", 19) ' Change "Arial" to the desired font and 12 to the desired font size
            'newLabelP.AutoSize = True ' Adjust the size of the label based on its content
            'newLabelP.BackColor = Color.Transparent ' Change LightBlue to the desired color
            'newLabelP.ForeColor = Color.White
            'heightP += 37.5
            'Me.Controls.Add(newLabelP)
            'newLabelW.Text = special_wi
            'newLabelW.Location = New Point(widthW, heightW) ' Set the position of the label
            'newLabelW.Font = New Font("Catamaran", 19) ' Change "Arial" to the desired font and 12 to the desired font size
            'newLabelW.AutoSize = True ' Adjust the size of the label based on its content
            'newLabelW.ForeColor = Color.White
            'heightW += 37.5
            'Me.Controls.Add(newLabelW)
            datlvDefectsumary = New ListViewItem(No)
            datlvDefectsumary.SubItems.Add(special_item_cd)
            datlvDefectsumary.SubItems.Add(special_wi)
            lvWISpc.Items.Add(datlvDefectsumary)
            'newLabelN.BringToFront()
            'newLabelP.BringToFront()
            'newLabelW.BringToFront()
            'newLabelN.BackColor = colorsArray(i)
            'newLabelP.BackColor = colorsArray(i)
            'newLabelW.BackColor = colorsArray(i)
            i += 1
        Next '
        Button4.BringToFront()
        Button3.BringToFront()
        Label6.BackColor = Color.FromArgb(44, 85, 130)
        Label6.Location = New Point(584, 159)
        Label6.BringToFront()
        LB_PLAN_DATE.BackColor = Color.FromArgb(44, 77, 131)
        LB_PLAN_DATE.Location = New Point(570, 270)
        LB_PLAN_DATE.BringToFront()
        lb_remain_qty.Location = New Point(628, 371)
        lb_remain_qty.BringToFront()
        lb_plan_qty.Location = New Point(426, 372)
        lb_plan_qty.BringToFront()
        Label12.Location = New Point(57, 442)
        Label12.BringToFront()
        lb_model.BackColor = Color.FromArgb(44, 72, 132)
        lb_model.Location = New Point(40, 355)
        lb_model.BringToFront()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim shift = Trim(Label12.Text.Substring(0, 1))
        Try
            If shift = "A" Then 'กะเช้า'
                If TimeOfDay.ToString("HH:mm:ss") <= "07:40:00" And TimeOfDay.ToString("HH:mm:ss") <= "07:40:00" Then 'กะเช้า'
                    Confrime_work_production.Show()
                Else
                    Confrime_work_production.next_pae()
                End If
            ElseIf shift = "P" Then 'กะเช้า'
                If TimeOfDay.ToString("HH:mm:ss") >= "19:40:00" And TimeOfDay.ToString("HH:mm:ss") <= "20:00:00" Then 'กะเช้า'
                    Confrime_work_production.Show()
                Else
                    Confrime_work_production.next_pae()
                End If
            ElseIf shift = "B" Then 'กะดึก'
                If TimeOfDay.ToString("HH:mm:ss") >= "19:40:00" And TimeOfDay.ToString("HH:mm:ss") <= "20:00:00" Then 'กะเช้า'
                    Confrime_work_production.Show()
                Else
                    Confrime_work_production.next_pae()
                End If
            ElseIf shift = "Q" Then 'กะดึก'
                If TimeOfDay.ToString("HH:mm:ss") >= "20:00:00" And TimeOfDay.ToString("HH:mm:ss") <= "23:59:59" Then
                    Confrime_work_production.next_pae()
                ElseIf TimeOfDay.ToString("HH:mm:ss") >= "00:00:00" And TimeOfDay.ToString("HH:mm:ss") <= "07:39:59" Then
                    'Confrime_work_production.next_pae()
                    Confrime_work_production.next_pae()
                Else
                    Confrime_work_production.Show()
                End If
                'If TimeOfDay.ToString("HH:mm:ss") <= "19:40:00" Then
                '	Confrime_work_production.Show()
                '	Else
                '			Confrime_work_production.next_pae()
                '			End If
            Else
                Confrime_work_production.next_pae()
            End If
        Catch ex As Exception
            MsgBox("ERROR = 111 " & ex.Message)
            Me.Enabled = True
        End Try
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
        Rm_scan.Panel_scan_picking.Visible = True
        Rm_scan.scan_item_cd.Select()
        Rm_scan.scan_item_cd.Focus()
        Rm_scan.Show()
    End Sub
    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Rm_scan.Panel_scan_picking.Visible = True
        Rm_scan.scan_item_cd.Select()
        Rm_scan.scan_item_cd.Focus()
        Rm_scan.ShowDialog()
    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs)
    End Sub
    Private Sub cc(sender As Object, e As EventArgs) Handles Timer3.Tick
        check_lot()
    End Sub
    Private Sub Timer_delay_api_Tick(sender As Object, e As EventArgs)

    End Sub
    Private Sub lb_seq_Click(sender As Object, e As EventArgs) Handles lb_seq.Click

    End Sub

    Private Sub LB_ShowWorker_Click(sender As Object, e As EventArgs) Handles LB_ShowWorker.Click
        Dim showWork = New Show_Worker
        showWork.show
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        tbnUp()
    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        tbnDown()
    End Sub
    Public Sub tbnUp()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvWISpc.Items.Count - 1))) Then
            S_index = CDbl(Val((lvWISpc.Items.Count - 1)))
        End If
        Try
            lvWISpc.Items(S_index).Selected = False
            S_index -= 1
            If S_index < 0 Then
                S_index = 0
                ' ElseIf lvChildpart.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvChildpart.Items.Count - 1)))
            End If
            lvWISpc.Items(S_index).Selected = True
            lvWISpc.Items(S_index).EnsureVisible()
            lvWISpc.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub tbnDown()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvWISpc.Items.Count - 1))) Then
            S_index = CDbl(Val((lvWISpc.Items.Count - 1)))
        End If
        Try
            lvWISpc.Items(S_index).Selected = False
            S_index += 1
            If S_index < 0 Then
                S_index = 0
                ' ElseIf lvChildpart.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvChildpart.Items.Count - 1)))
            End If
            lvWISpc.Items(S_index).Selected = True
            lvWISpc.Items(S_index).EnsureVisible()
            lvWISpc.Select()
        Catch ex As Exception

        End Try
    End Sub
End Class