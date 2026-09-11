Public Class Wait_data
	Public check_load_wit_data As String = "0"
	Private Sub Wait_data_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		load_wait_data()
	End Sub
	Public Sub load_wait_data()
        'With Label1
        '      .Location = New Point(Panel1.Width \ 2 - Label1.Width \ 2, Label1.Location.Y)
        '       End With
        '        With Label2
        '        .Location = New Point(Panel1.Width \ 2 - Label2.Width \ 2, Label2.Location.Y)
        '        End With
        check_load_wit_data = "1"
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class
