Public Class ScanTagPlan
    Public Shared partsNo As String = ""
    Public Shared qty As String = ""
    Public Shared dates As String = ""
    Public Shared seq As String = ""
    Public Shared line As String = ""
    Public Shared keyInfo As String = ""
    Public Shared plantCode As String = ""
    Public Shared wi As String = ""
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ManagePlan.Enabled = True
        Me.Close()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles tbScanQr.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Enter
                Try
                    If MainFrm.rsCheckCriticalFlg = "1" Then
                        partsNo = tbScanQr.Text.Substring(2, 25).Trim()
                        qty = tbScanQr.Text.Substring(26, 7).Trim()
                        dates = tbScanQr.Text.Substring(33, 8).Trim()
                        seq = tbScanQr.Text.Substring(41, 3).Trim()
                        line = tbScanQr.Text.Substring(44, 6).Trim()
                        keyInfo = tbScanQr.Text.Substring(58, 3).Trim()
                        plantCode = tbScanQr.Text.Substring(61, 2).Trim()
                        wi = tbScanQr.Text.Substring(63, 10).Trim()
                        ManagePlan.SelectPlan(wi)
                    ElseIf MainFrm.rsCheckCriticalFlg = "2" Then
                        partsNo = tbScanQr.Text.Substring(0, 16).Trim()
                        qty = tbScanQr.Text.Substring(16, 2).Trim()
                        dates = tbScanQr.Text.Substring(18, 6).Trim()
                        seq = tbScanQr.Text.Substring(24, 3).Trim()
                        line = MainFrm.Label4.Text
                        keyInfo = tbScanQr.Text.Substring(24, 3).Trim()
                        plantCode = "51"
                        wi = " - "
                    End If
                    '                    MsgBox("partsNo===>" & partsNo)
                    '                    MsgBox("qty===>" & qty)
                    '                    MsgBox("dates===>" & dates)
                    '                    MsgBox("seq===>" & seq)
                    '                    MsgBox("line===>" & line)
                    '                    MsgBox("keyInfo===>" & keyInfo)

                Catch ex As Exception
                    Me.PictureBox9.Show()
                    Me.Panel3.Show()
                    Me.Label5.Show()
                    Me.PictureBox11.Show()
                    Dim listdetail = "Not have production plan !"
                    Me.tbScanQr.Text = ""
                    Me.PictureBox9.BringToFront()
                    Me.PictureBox9.Show()
                    Me.PictureBox11.BringToFront()
                    Me.PictureBox11.Show()
                    Me.Panel3.BringToFront()
                    Me.Panel3.Show()
                    Me.Label5.Text = listdetail
                    Me.Label5.BringToFront()
                    Me.Label5.Show()
                    Me.Enabled = True
                End Try
        End Select
    End Sub
    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        PictureBox9.Hide()
        Panel3.Hide()
        Label5.Hide()
        PictureBox11.Hide()
    End Sub
End Class