Imports System.Globalization
Public Class SetStartTime
    Public Shared downTime
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim timeAdjust As String = tbHour.Text & ":" & tbMin.Text & ":" & DateTime.Now.ToString("ss") '"00"
                'MsgBox("timeAdjust====>" & timeAdjust)
                Dim DateTimecompuerdown As String = Down_time.Text
                Dim dateTimeAdjust As DateTime = DateTime.Now.ToString("yyyy-MM-dd") & " " & timeAdjust
                Dim dateStartAdjusts As DateTime = dateTimeAdjust
                If Trim(DateTime.Now.ToString("HH:mm:ss")) >= "00:00:00" And Trim(DateTime.Now.ToString("HH:mm:ss")) <= "07:59:59" Then ' Night shift 
                    If timeAdjust >= "00:00:00" And Trim(timeAdjust) <= "07:59:59" Then
                    Else
                        dateStartAdjusts = dateTimeAdjust.AddDays(-1)
                    End If
                End If
                dateStartAdjusts = dateStartAdjusts.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                'MsgBox("page start dateStartAdjusts===>" & dateStartAdjusts)
                Backoffice_model.gobal_DateTimeComputerDown = dateStartAdjusts
                ' MsgBox("page start Backoffice_model.gobal_DateTimeComputerDown===>" & Backoffice_model.gobal_DateTimeComputerDown)
                'Backoffice_model.gobal_QTYComputerDown = tbQTY.Text
            Else
                load_show.Show()
            End If
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    Dim timeAdjust As String = tbHour.Text & ":" & tbMin.Text & ":" & "00"
                    Dim DateTimecompuerdown As String = Down_time.Text
                    Dim dateTimeAdjust As DateTime = DateTime.Now.ToString("yyyy-MM-dd") & " " & timeAdjust
                    Dim dateStartAdjust As DateTime = dateTimeAdjust
                    If Trim(DateTime.Now.ToString("HH:mm:ss")) >= "00:00:00" And Trim(DateTime.Now.ToString("HH:mm:ss")) <= "07:59:59" Then ' Night shift 
                        If timeAdjust >= "00:00:00" And Trim(timeAdjust) <= "07:59:59" Then
                        Else
                            dateStartAdjust = dateTimeAdjust.AddDays(-1)
                        End If
                    End If
                    dateStartAdjust = dateStartAdjust.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                    'MsgBox("dateStartAdjust===>" & dateStartAdjust)
                    If dateStartAdjust <= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") Then
                        If dateStartAdjust >= DateTimecompuerdown Then  'check condition about time 
                            'Try
                            'Dim MAX_QTY As String = CDbl(Val(Working_Pro.Label8.Text)) - CDbl(Val(Working_Pro.Label6.Text))
                            'If CDbl(Val(tbQTY.Text)) <= CDbl(Val(MAX_QTY)) Then
                            Working_Pro.Start_Production()
                            Me.Close()
                            'Else
                            'MsgBox("Please Check QTY.")
                            'End If
                            'Catch ex As Exception
                            'MsgBox("Please Checks QTY.")
                            'End Try
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
                load_show.Show()
            End Try
        Catch ex As Exception
            load_show.Show()
        End Try
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
    Private Sub SetStartTime_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim OEE = New OEE_NODE
        tbHour.Text = TimeOfDay.ToString("HH")
        tbMin.Text = TimeOfDay.ToString("mm")
        downTime = OEE.OEE_getDataProductionActual(Prd_detail.Label12.Text.Substring(3, 5), MainFrm.Label4.Text, Working_Pro.Label3.Text)
        Down_time.Text = downTime
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        numPadSetStartTime.Show()
    End Sub
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        numPadSetStartTime.Down_time.Text = downTime.substring(10)
        numPadSetStartTime.Show()
    End Sub
    Private Sub btnNumpadQTY_Click(sender As Object, e As EventArgs)
        numPadSetQTY.Show()
    End Sub
End Class