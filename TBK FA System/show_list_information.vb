Public Class show_list_information
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        Dim reader = Backoffice_model.get_new_information()
        While reader.read()
            MsgBox(reader("inf_txt").ToString)
        End While
        reader.close
    End Sub
End Class