Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms
Public Class MainFA
    Private WithEvents WebView21 As WebView2
    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize the WebView2 control
        Await InitializeWebView2()
    End Sub
    Private Async Function InitializeWebView2() As Task
        ' Create a new instance of WebView2 control
        WebView21 = New WebView2() With {
            .Dock = DockStyle.Fill
        }

        ' Add WebView2 control to the form's controls collection
        PanelProgressbar.Controls.Add(WebView21)
        Try
            ' Create a WebView2 environment with a user data folder
            Dim webViewEnvironment = Await CoreWebView2Environment.CreateAsync(Nothing, "C:\Temp")

            ' Initialize the WebView2 control with the created environment
            Await WebView21.EnsureCoreWebView2Async(webViewEnvironment)

            ' Navigate to a web page
            WebView21.CoreWebView2.Navigate("http://192.168.161.219/productionHrProgressTest/?line_cd=K1A003&shift=P")
        Catch ex As Exception
            MessageBox.Show($"Failed to initialize WebView2: {ex.Message}")
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        WebView21.Reload()
    End Sub

    ' Protected Overrides Sub Dispose(disposing As Boolean)
    '  If disposing AndAlso components IsNot Nothing Then
    '          components.Dispose()
    '   End If
    '    If disposing AndAlso WebView21 IsNot Nothing Then
    '            WebView21.Dispose()'

    'End If
    ' MyBase.Dispose(disposing)
    ' End Sub
End Class

