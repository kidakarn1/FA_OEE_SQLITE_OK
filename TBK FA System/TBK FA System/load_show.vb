Public Class load_show
    Dim check_net As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
recheck:
        If check_net = 15 Then
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    Timer1.Enabled = False
                    Me.Close()
                End If
            Catch ex As Exception
                check_net = 0
                GoTo recheck
            End Try
        Else
            check_net = check_net + 1
        End If
    End Sub
    Private Sub load_show_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

End Class