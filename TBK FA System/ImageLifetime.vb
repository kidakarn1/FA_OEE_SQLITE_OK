Imports System.Drawing
Imports System.Windows.Forms

Friend Module ImageLifetime
    ' Use only for images created at runtime and owned by the target PictureBox.
    ' Embedded/shared My.Resources images must not be passed to this helper.
    Public Sub ReplaceOwned(target As PictureBox, replacement As Image)
        If target Is Nothing Then
            If replacement IsNot Nothing Then replacement.Dispose()
            Return
        End If

        Dim previous As Image = target.Image
        target.Image = replacement

        If previous IsNot Nothing AndAlso Not Object.ReferenceEquals(previous, replacement) Then
            previous.Dispose()
        End If
    End Sub
End Module
