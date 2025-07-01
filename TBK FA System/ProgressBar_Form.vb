Public Class ProgressBar_Form
    Private Const StartHour As Integer = 8
    Private Const EndHour As Integer = 20
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ManageTimeline()
    End Sub
    Private Sub ManageTimeline()
        Dim currentTime As DateTime = DateTime.Now
        Dim currentHour As Integer = currentTime.Hour

        Dim timeArray() As String
        If currentHour >= StartHour AndAlso currentHour <= EndHour Then
            timeArray = {"8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20"}
        Else
            timeArray = {"20", "21", "22", "23", "00", "01", "02", "03", "04", "05", "06", "07", "08"}
        End If

        For i As Integer = 0 To 12
            CreateLabel(timeArray(i), 54 * (i + 1), 445)
            CreatePanels(28, 470)
        Next
    End Sub
    Private Sub CreateLabel(text As String, x As Integer, y As Integer)
        Dim labelTime As New Label()
        labelTime.Location = New Point(x, y)
        labelTime.Font = New Font("Arial", 10)
        labelTime.AutoSize = True
        labelTime.Text = text
        Me.Controls.Add(labelTime)
        labelTime.BringToFront()
    End Sub
    Private Sub CreatePanels(numPanels As Integer, y As Integer)
        Dim W As Integer = 2
        For j As Integer = 0 To numPanels
            Dim panel As New Panel()
            panel.Location = New Point(W, y)
            panel.BackColor = If((j + 1) Mod 12 = 0, Color.FromArgb(183, 34, 34), Color.FromArgb(113, 255, 36))
            panel.Width = If(j = numPanels, 3, 2)
            panel.Height = 50
            Me.Controls.Add(panel)
            W += If(j = 0, 18, If(j = numPanels, 3, 2))
            panel.BringToFront()
        Next
    End Sub
End Class
