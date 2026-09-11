Public Class Form1
    ' Declare a Timer control to simulate status updates
    Dim WithEvents Timer1 As New Timer()
    Dim statusIndex As Integer = 0
    Dim statuses As String() = {"เริ่มต้น", "กำลังดำเนินการ", "เสร็จสิ้น"}
    ' This event fires when the form is loaded
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up the timeline panel and status labels
        FlowLayoutPanel1.FlowDirection = FlowDirection.TopDown
        FlowLayoutPanel1.AutoScroll = True
        ' Create and add a label for each status in the timeline
        For Each status In statuses
            Dim statusLabel As New Label()
            statusLabel.Text = status
            statusLabel.AutoSize = True
            statusLabel.BackColor = Color.LightGray
            FlowLayoutPanel1.Controls.Add(statusLabel)
        Next
        'Set the timer to simulate status update every 2 seconds
        Timer1.Interval = 2000
        Timer1.Start()
    End Sub
    ' Timer tick event to update the timeline status
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If statusIndex < statuses.Length Then
            ' Highlight the current status in the timeline
            Dim currentLabel As Label = FlowLayoutPanel1.Controls(statusIndex)
            currentLabel.BackColor = Color.Green
            currentLabel.ForeColor = Color.White
            ' Move to the next status
            statusIndex += 1
        Else
            ' Stop the timer once all statuses are displayed
            Timer1.Stop()
        End If
    End Sub
End Class