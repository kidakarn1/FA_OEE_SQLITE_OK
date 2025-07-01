Imports System.Globalization

Public Class numPadSetStartTime
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnNumber1.Click
        Dim text_now As String = tbAddjustH.Text
        If text_now.Length > 1 Then
            Dim text_to2 As String = tbAddjustM.Text
            If text_to2.Length = 1 Then
                text_to2 = text_to2 & "1"
                tbAddjustM.Text = text_to2
                allena()
            Else
                text_to2 = text_to2 & "1"
                tbAddjustM.Text = text_to2
            End If
        Else
            text_now = text_now & "1"
            tbAddjustH.Text = text_now
        End If
        chk_ins()
        Dim text_to2_now As String = tbAddjustM.Text
        If text_to2_now.Length > 1 Then
            alldis()
        End If
    End Sub
    Private Sub ena012()
        btnNumber1.Enabled = True
        btnNumber2.Enabled = True
        btnNumber3.Enabled = False
        btnNumber4.Enabled = False
        btnNumber5.Enabled = False
        btnNumber6.Enabled = False
        btnNumber7.Enabled = False
        btnNumber8.Enabled = False
        btnNumber9.Enabled = False
        btnNumber0.Enabled = True
    End Sub
    Private Sub ena0123()
        btnNumber1.Enabled = True
        btnNumber2.Enabled = True
        btnNumber3.Enabled = True
        btnNumber4.Enabled = False
        btnNumber5.Enabled = False
        Button6.Enabled = False
        btnNumber7.Enabled = False
        btnNumber8.Enabled = False
        btnNumber9.Enabled = False
        btnNumber0.Enabled = True
    End Sub
    Private Sub ena012345()
        btnNumber1.Enabled = True
        btnNumber2.Enabled = True
        btnNumber3.Enabled = True
        btnNumber4.Enabled = True
        btnNumber5.Enabled = True
        btnNumber6.Enabled = False
        btnNumber7.Enabled = False
        btnNumber8.Enabled = False
        btnNumber9.Enabled = False
        btnNumber0.Enabled = True
    End Sub
    Private Sub alldis()
        btnNumber1.Enabled = False
        btnNumber2.Enabled = False
        btnNumber3.Enabled = False
        btnNumber4.Enabled = False
        btnNumber5.Enabled = False
        btnNumber6.Enabled = False
        btnNumber7.Enabled = False
        btnNumber8.Enabled = False
        btnNumber9.Enabled = False
        btnNumber0.Enabled = False
    End Sub
    Private Sub allena()
        btnNumber1.Enabled = True
        btnNumber2.Enabled = True
        btnNumber3.Enabled = True
        btnNumber4.Enabled = True
        btnNumber5.Enabled = True
        btnNumber6.Enabled = True
        btnNumber7.Enabled = True
        btnNumber8.Enabled = True
        btnNumber9.Enabled = True
        btnNumber0.Enabled = True
    End Sub
    Private Sub chk_ins()
        btnNumber0.Enabled = True
        btnNumber1.Enabled = True
        btnNumber2.Enabled = True
        btnNumber3.Enabled = True
        btnNumber4.Enabled = True
        btnNumber5.Enabled = True
        btnNumber6.Enabled = True
        btnNumber7.Enabled = True
        btnNumber8.Enabled = True
        btnNumber9.Enabled = True
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnNumber2.Click
        Dim number As String = "2"
        Dim text_now As String = tbAddjustH.Text
        If text_now.Length > 1 Then
            Dim text_to2 As String = tbAddjustM.Text
            If text_to2.Length = 1 Then
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
                allena()
            Else
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
            End If
        Else
            text_now = text_now & number
            tbAddjustH.Text = text_now
        End If
        chk_ins()
        Dim text_to2_now As String = tbAddjustM.Text
        If text_to2_now.Length > 1 Then
            alldis()
        End If
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnNumber3.Click
        Dim number As String = "3"
        Dim text_now As String = tbAddjustH.Text
        If text_now.Length > 1 Then
            Dim text_to2 As String = tbAddjustM.Text
            If text_to2.Length = 1 Then
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
                allena()
            Else
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
            End If
        Else
            text_now = text_now & number
            tbAddjustH.Text = text_now
        End If
        chk_ins()
        Dim text_to2_now As String = tbAddjustM.Text
        If text_to2_now.Length > 1 Then
            alldis()
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnNumber4.Click
        Dim number As String = "4"
        Dim text_now As String = tbAddjustH.Text
        If text_now.Length > 1 Then
            Dim text_to2 As String = tbAddjustM.Text
            If text_to2.Length = 1 Then
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
                allena()
            Else
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
            End If
        Else
            text_now = text_now & number
            tbAddjustH.Text = text_now
        End If
        chk_ins()
        Dim text_to2_now As String = tbAddjustM.Text
        If text_to2_now.Length > 1 Then
            alldis()
        End If
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btnNumber5.Click
        Dim number As String = "5"
        Dim text_now As String = tbAddjustH.Text
        If text_now.Length > 1 Then
            Dim text_to2 As String = tbAddjustM.Text
            If text_to2.Length = 1 Then
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
                allena()
            Else
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
            End If
        Else
            text_now = text_now & number
            tbAddjustH.Text = text_now
        End If
        chk_ins()
        Dim text_to2_now As String = tbAddjustM.Text
        If text_to2_now.Length > 1 Then
            alldis()
        End If
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles btnNumber6.Click
        Dim number As String = "6"
        Dim text_now As String = tbAddjustH.Text
        If text_now.Length > 1 Then
            Dim text_to2 As String = tbAddjustM.Text
            If text_to2.Length = 1 Then
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
                allena()
            Else
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
            End If
        Else
            text_now = text_now & number
            tbAddjustH.Text = text_now
        End If
        chk_ins()
        Dim text_to2_now As String = tbAddjustM.Text
        If text_to2_now.Length > 1 Then
            alldis()
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnNumber7.Click
        Dim number As String = "7"
        Dim text_now As String = tbAddjustH.Text
        If text_now.Length > 1 Then
            Dim text_to2 As String = tbAddjustM.Text
            If text_to2.Length = 1 Then
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
                allena()
            Else
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
            End If
        Else
            text_now = text_now & number
            tbAddjustH.Text = text_now
        End If
        chk_ins()
        Dim text_to2_now As String = tbAddjustM.Text
        If text_to2_now.Length > 1 Then
            alldis()
        End If
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles btnNumber8.Click
        Dim number As String = "8"
        Dim text_now As String = tbAddjustH.Text
        If text_now.Length > 1 Then
            Dim text_to2 As String = tbAddjustM.Text
            If text_to2.Length = 1 Then
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
                allena()
            Else
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
            End If
        Else
            text_now = text_now & number
            tbAddjustH.Text = text_now
        End If
        chk_ins()
        Dim text_to2_now As String = tbAddjustM.Text
        If text_to2_now.Length > 1 Then
            alldis()
        End If
    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles btnNumber9.Click
        Dim number As String = "9"
        Dim text_now As String = tbAddjustH.Text
        If text_now.Length > 1 Then
            Dim text_to2 As String = tbAddjustM.Text
            If text_to2.Length = 1 Then
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
                allena()
            Else
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
            End If
        Else
            text_now = text_now & number
            tbAddjustH.Text = text_now
        End If
        chk_ins()
        Dim text_to2_now As String = tbAddjustM.Text
        If text_to2_now.Length > 1 Then
            alldis()
        End If
    End Sub
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles btnNumber0.Click
        Dim number As String = "0"
        Dim text_now As String = tbAddjustH.Text
        If text_now.Length > 1 Then
            Dim text_to2 As String = tbAddjustM.Text
            If text_to2.Length = 1 Then
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
                allena()
            Else
                text_to2 = text_to2 & number
                tbAddjustM.Text = text_to2
            End If
        Else
            text_now = text_now & number
            tbAddjustH.Text = text_now
        End If
        chk_ins()
        Dim text_to2_now As String = tbAddjustM.Text
        If text_to2_now.Length > 1 Then
            alldis()
        End If
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        tbAddjustH.Clear()
        tbAddjustM.Clear()
        ' tbAddjustH.Text = "0"
        ' tbAddjustM.Text = "0"
        ReselAll()
    End Sub
    Private Sub ReselAll()
        btnNumber1.Enabled = True
        btnNumber2.Enabled = True
        btnNumber3.Enabled = True
        btnNumber4.Enabled = True
        btnNumber5.Enabled = True
        btnNumber6.Enabled = True
        btnNumber7.Enabled = True
        btnNumber8.Enabled = True
        btnNumber9.Enabled = True
        btnNumber0.Enabled = True
    End Sub
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim text_to2 As String = tbAddjustM.Text
        Dim text_to1 As String = tbAddjustH.Text
        If text_to2.Length > 0 Then
            'MsgBox(text_to2.Length)
            If text_to2.Length = 1 Then
                Dim result_str As String = text_to2.Substring(1)
                tbAddjustM.Text = result_str
            ElseIf text_to2.Length = 2 Then
                Dim result_str As String = text_to2.Substring(0, 1)
                tbAddjustM.Text = result_str
            End If
        End If
        If text_to2.Length = 0 Then
            If text_to1.Length > 0 Then
                'MsgBox(text_to2.Length)
                If text_to1.Length = 1 Then
                    Dim result_str As String = text_to1.Substring(1)
                    tbAddjustH.Text = result_str
                ElseIf text_to1.Length = 2 Then
                    Dim result_str As String = text_to1.Substring(0, 1)
                    tbAddjustH.Text = result_str
                End If
            End If
        End If
        'text_to2 = TextBox2.Text
        'MsgBox(TextBox2.Text.Length)
        chk_ins()
    End Sub

    Private Sub btn_next_Click(sender As Object, e As EventArgs) Handles btn_next.Click
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim timeAdjust As String = tbAddjustH.Text & ":" & tbAddjustM.Text & ":" & "00"
                Dim DateTimecompuerdown As String = SetStartTime.Down_time.Text
                Dim dateTimeAdjust As DateTime = DateTime.Now.ToString("yyyy-MM-dd") & " " & timeAdjust
                Dim dateStartAdjust As DateTime = dateTimeAdjust
                If Trim(DateTime.Now.ToString("HH:mm:ss")) >= "00:00:00" And Trim(DateTime.Now.ToString("HH:mm:ss")) <= "07:59:59" Then ' Night shift 
                    If timeAdjust >= "00:00:00" And Trim(timeAdjust) <= "07:59:59" Then
                        '  dateStartAdjust = dateTimeAdjust.AddDays(-1)
                    Else
                        dateStartAdjust = dateTimeAdjust.AddDays(-1)
                    End If
                End If
                dateStartAdjust = dateStartAdjust.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                'MsgBox("dateStartAdjust===>" & dateStartAdjust)
                If dateStartAdjust <= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") Then
                    If dateStartAdjust >= DateTimecompuerdown Then  'check condition about time 
                        SetStartTime.tbHour.Text = tbAddjustH.Text
                        SetStartTime.tbMin.Text = tbAddjustM.Text
                        Me.Close() ' OK
                    Else
                        MsgBox("Please Check Time.")
                    End If
                Else
                    MsgBox("Please Check Time Current.")
                End If
            Else
                load_show.Show()
            End If
        Catch ex As Exception
            'load_show.Show()
            MsgBox("Please Check Times.")
        End Try
    End Sub
End Class