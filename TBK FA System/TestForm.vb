Imports System.Net.NetworkInformation
Imports System.Threading.Tasks
Imports System.Diagnostics
Public Class TestForm
    ''' <summary>
    ''' ตรวจสอบ Network เสถียรหรือไม่แบบ Async
    ''' </summary>
    Private Async Function IsNetworkStableAsync(host As String, Optional attempts As Integer = 3, Optional timeout As Integer = 500) As Task(Of Boolean)
        Dim successCount As Integer = 0
        Try
            Dim pingSender As New Ping()

            For i As Integer = 1 To attempts
                Dim reply As PingReply = Await pingSender.SendPingAsync(host, timeout)
                If reply.Status = IPStatus.Success Then
                    successCount += 1
                End If
                Await Task.Delay(100) ' ลด delay เพื่อให้เร็วขึ้น
            Next

            Dim successRate As Double = successCount / attempts
            Return successRate >= 0.8 ' อย่างน้อย 80% ถือว่าเสถียร
        Catch ex As Exception
            Return False
        End Try
    End Function
    ''' <summary>
    ''' ปุ่มเช็ค Network เสถียรหรือไม่
    ''' </summary>
    Private Async Sub btnCheckNetwork_Click(sender As Object, e As EventArgs) Handles btnCheckNetwork.Click
        btnCheckNetwork.Enabled = False
        btnCheckNetwork.Text = "Checking..."
        Dim targetHost As String = "192.168.161.101" ' เปลี่ยนเป็น IP/hostname ที่คุณต้องการเช็ค
        Dim sw As New Stopwatch()
        sw.Start()
        Dim isStable As Boolean = Await IsNetworkStableAsync(targetHost, 3, 500)
        sw.Stop()
        Dim elapsedSec As Double = sw.Elapsed.TotalSeconds
        If isStable Then
            MessageBox.Show($"✅ Network เสถียร พร้อมใช้งาน{vbCrLf}⏱ เวลา: {elapsedSec:F2} วินาที", "Network Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show($"⚠️ Network ไม่เสถียร หรือเชื่อมต่อไม่ได้{vbCrLf}⏱ เวลา: {elapsedSec:F2} วินาที", "Network Status", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        btnCheckNetwork.Text = "Check Network"
        btnCheckNetwork.Enabled = True
    End Sub
End Class