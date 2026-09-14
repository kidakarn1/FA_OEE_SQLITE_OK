Public Class load_show_OEE
    Private Const SuppressDisplay As Boolean = True

    Protected Overrides Sub SetVisibleCore(value As Boolean)
        If SuppressDisplay Then
            MyBase.SetVisibleCore(False)
        Else
            MyBase.SetVisibleCore(value)
        End If
    End Sub

    Private Sub load_show_OEE_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Shared Sub HideIfOpen()
        Try
            For Each openForm As Form In Application.OpenForms
                If TypeOf openForm Is load_show_OEE Then
                    Dim loader As load_show_OEE = DirectCast(openForm, load_show_OEE)
                    If loader.IsDisposed Then Return
                    If loader.InvokeRequired Then
                        loader.BeginInvoke(New Action(AddressOf HideIfOpen))
                    Else
                        loader.Hide()
                    End If
                    Return
                End If
            Next
        Catch
            ' Cleanup must never interrupt the production screen.
        End Try
    End Sub
End Class
