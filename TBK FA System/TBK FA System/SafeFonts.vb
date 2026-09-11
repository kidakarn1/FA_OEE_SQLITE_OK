Public NotInheritable Class SafeFonts
    Public Shared ReadOnly Title As New Font("Segoe UI", 10.0F, FontStyle.Bold)
    Public Shared ReadOnly Body As New Font("Segoe UI", 10.0F, FontStyle.Regular)
    Public Shared ReadOnly Small As New Font("Segoe UI", 8.5F, FontStyle.Regular)
    Public Shared ReadOnly Big As New Font("Segoe UI", 16.0F, FontStyle.Bold)
    Public Shared ReadOnly Mid As New Font("Segoe UI", 12.0F, FontStyle.Bold)

    Public Shared Sub DisposeAll()
        Try
            Title.Dispose() : Body.Dispose() : Small.Dispose() : Big.Dispose() : Mid.Dispose()
        Catch
        End Try
    End Sub
End Class