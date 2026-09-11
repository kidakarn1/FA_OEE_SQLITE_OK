Public Class PopUpLossDelayStart
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Working_Pro.Start_Production()
                Me.Close()
            Else
                load_show.Show()
            End If
        Catch ex As Exception
            load_show.Show()
        End Try
    End Sub
End Class