Public Class loadingFA
    Private Sub WebView21_Click(sender As Object, e As EventArgs) Handles WebView21.Click
        Me.WebView21.Source = New Uri("http://192.168.161.219/test_ci/loading.php")
    End Sub
End Class