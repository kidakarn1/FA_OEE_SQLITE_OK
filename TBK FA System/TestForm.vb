Imports NationalInstruments.DAQmx
Imports System.Drawing

Public Class TestForm
    Private diTask As Task
    Private reader As DigitalSingleChannelReader

    Private Const CHAN_SPEC As String = "Dev1/port2/line0:3"
    Private Const PART_A As String = "898248-1601"
    Private Const PART_B As String = "898248-1612"

    Private Sub TestForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' ตั้งค่ารายการรุ่น
            cboExpected.Items.Clear()
            cboExpected.Items.Add("Model A")
            cboExpected.Items.Add("Model B")
            cboExpected.SelectedIndex = 0

            diTask = New Task()
            diTask.DIChannels.CreateChannel(
                CHAN_SPEC, "ProductionLines",
                ChannelLineGrouping.OneChannelForAllLines)
            diTask.Start()

            reader = New DigitalSingleChannelReader(diTask.Stream)

            Timer1.Interval = 50
            Timer1.Start()
        Catch ex As DaqException
            Timer1.Stop()
            MessageBox.Show("เกิดข้อผิดพลาดเริ่มต้น: " & ex.Message)
        End Try
    End Sub

    Private Function GetExpectedModel() As String
        Return cboExpected.SelectedItem.ToString()
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim states As Boolean() = reader.ReadSingleSampleMultiLine()
            Dim lineA As Boolean = states(1) ' Port 2:1
            Dim lineB As Boolean = states(2) ' Port 2:2

            ' ------------------------------
            ' False = รุ่นที่กำลังเดินอยู่
            ' ------------------------------
            Dim detected As String = ""
            Dim partNo As String = ""

            If Not lineA And lineB Then
                detected = "Model A"
                partNo = PART_A
            ElseIf lineA And Not lineB Then
                detected = "Model B"
                partNo = PART_B
            Else
                detected = "ไม่สามารถระบุได้"
            End If

            lblDetected.Text = "Detected: " & detected & If(partNo <> "", $" (Part {partNo})", "")

            Dim expected As String = GetExpectedModel()

            If detected <> expected AndAlso detected <> "ไม่สามารถระบุได้" Then
                lblStatus.Text = $"❌ เดินผิดรุ่น! ควรเป็น {expected}"
                lblStatus.BackColor = Color.Red
                lblStatus.ForeColor = Color.White
            ElseIf detected = expected Then
                lblStatus.Text = $"✔ รุ่นถูกต้อง: {expected}"
                lblStatus.BackColor = Color.LimeGreen
                lblStatus.ForeColor = Color.Black
            Else
                lblStatus.Text = "⚠ ไม่พบสัญญาณที่ชัดเจน"
                lblStatus.BackColor = Color.Yellow
                lblStatus.ForeColor = Color.Black
            End If

            ' log ดูค่าดิบ
            Console.WriteLine($"Model A Line (port2:1) = {lineA}, Model B Line (port2:2) = {lineB}")

        Catch ex As DaqException
            Timer1.Stop()
            MessageBox.Show("เกิดข้อผิดพลาดในการอ่านค่า: " & ex.Message)
        End Try
    End Sub

    Private Sub TestForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Timer1.Stop()
            If diTask IsNot Nothing Then
                diTask.Stop()
                diTask.Dispose()
            End If
        Catch
        End Try
    End Sub
End Class
