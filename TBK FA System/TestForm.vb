Imports NationalInstruments.DAQmx

Public Class TestForm
    Private diTask As Task
    Private reader As DigitalSingleChannelReader

    Private goodCount As Long = 0
    Private defectCount As Long = 0

    Private lastState(3) As Boolean ' เก็บสถานะรอบก่อน

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' อ่าน 4 line (port0 line0 ถึง line3)
            diTask = New Task()
            diTask.DIChannels.CreateChannel("Dev1/port0/line0:3",
                                            "ProductionLines",
                                            ChannelLineGrouping.OneChannelForAllLines)

            reader = New DigitalSingleChannelReader(diTask.Stream)

            ' เริ่มอ่านทุก 50ms
            Timer1.Interval = 50
            Timer1.Start()

        Catch ex As DaqException
            MessageBox.Show("เกิดข้อผิดพลาด: " & ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            ' อ่านสถานะปุ่มทั้ง 4
            Dim states As Boolean() = reader.ReadSingleSampleMultiLine()

            ' ===== Port 0 (ปุ่มดี) =====
            If Not states(0) AndAlso lastState(0) Then ' High → Low
                goodCount += 1
            End If

            ' ===== Port 1 (ปุ่มดี) =====
            If Not states(1) AndAlso lastState(1) Then
                goodCount += 1
            End If

            ' ===== Port 2 (ปุ่มเสีย) =====
            If Not states(2) AndAlso lastState(2) Then
                defectCount += 1
            End If

            ' ===== Port 3 (ปุ่มเสีย) =====
            If Not states(3) AndAlso lastState(3) Then
                defectCount += 1
            End If

            ' เก็บสถานะล่าสุด
            For i As Integer = 0 To 3
                lastState(i) = states(i)
            Next

            ' แสดงผล
            lblGood.Text = "ชิ้นดี: " & goodCount
            lblDefect.Text = "ชิ้นเสีย: " & defectCount

        Catch ex As DaqException
            MessageBox.Show("เกิดข้อผิดพลาดในการอ่านค่า: " & ex.Message)
        End Try
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If diTask IsNot Nothing Then diTask.Dispose()
    End Sub
End Class
