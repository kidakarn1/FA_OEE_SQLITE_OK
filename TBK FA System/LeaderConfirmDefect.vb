Imports System.Web.Script.Serialization

Public Class LeaderConfirmDefect
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        'Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub tbEmpCodeleader_Enter(sender As Object, e As EventArgs) Handles tbEmpCodeleader.Enter

    End Sub
    Private Async Sub tbEmpCodeleader_KeyDown(sender As Object, e As KeyEventArgs) Handles tbEmpCodeleader.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    Dim rs = Await checkPermissionLeader(tbEmpCodeleader.Text, MainFrm.Label4.Text, MainFrm.Label6.Text)
                    If rs = "1" Then
                        defectHome.leaderConfrime = tbEmpCodeleader.Text
                        tbEmpCodeleader.Text = ""
                        Me.DialogResult = DialogResult.OK
                        Me.Close()
                    ElseIf rs = "2" Then
                        MsgBox("Not Permission.")
                        tbEmpCodeleader.Text = ""
                        defectHome.leaderConfrime = ""
                        tbEmpCodeleader.Select()
                    Else
                        MsgBox("Not Permission.")
                        tbEmpCodeleader.Text = ""
                        defectHome.leaderConfrime = ""
                        tbEmpCodeleader.Select()
                    End If
                Else
                    load_show.ShowDialog()
                End If
            Catch ex As Exception
                load_show.ShowDialog()
            End Try
        End If
    End Sub
    Public Async Function checkPermissionLeader(Empcode As String, Line_cd As String, pd As String) As Task(Of String)
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim checkPer = Await Backoffice_model.GetPermissionLeader(Empcode, Line_cd, pd)
                Dim mp_adm_control_flg As String = 0
                If checkPer <> "0" Then
                    ' Dim dict3 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(checkPer)
                    ' For Each item As Object In dict3
                    ' mp_adm_control_flg = item("mp_adm_control_flg").ToString()
                    ' Next
                    ' If mp_adm_control_flg = "1" Then
                    Return "1"
                    ' Else
                    '     Return "2"
                    ' End If
                Else
                    Return "0"
                End If
            Else
                load_show.Show()
            End If
        Catch ex As Exception
            load_show.Show()
        End Try
    End Function
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        KeyboardDefect.ShowDialog()
    End Sub
End Class