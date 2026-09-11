Public Class Confrm_end
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MainFrm.Enabled = True
        Me.Close()
    End Sub

    Private Sub menu4_Click(sender As Object, e As EventArgs) Handles menu4.Click
        System.Diagnostics.Process.Start("shutdown", "-s -t 00")
        Application.Exit()
    End Sub
    Private Sub Confrm_end_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MainFrm.Enabled = False
    End Sub

    Private Sub btn_restart_Click(sender As Object, e As EventArgs) Handles btn_restart.Click
        System.Diagnostics.Process.Start("shutdown", "-r -t 00")
        Application.Exit()
    End Sub
End Class