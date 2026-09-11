Public Class Finish_work
	Dim count_close_popup As Integer = 0
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles PictureBox16.Click
        click_popup()
    End Sub
    Public Async Sub click_popup()
        Dim line_id As String = MainFrm.line_id.Text
        Backoffice_model.line_status_upd(line_id)
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Await Backoffice_model.updated_data_to_dbsvr(Me, "2")
            End If
        Catch ex As Exception
        End Try
        Prd_detail.Timer3.Enabled = True
        Prd_detail.Enabled = True
        Prd_detail.Close()
        'Working_Pro.Close()
        'MainFrm.Enabled = True
        'MainFrm.Show()
        closeLotsummary.Show()
        Me.Close()
    End Sub
    Private Sub Finish_work_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Working_Pro.stop_working()
        Working_Pro.btn_start.Enabled = False
        Working_Pro.btn_closelot.Enabled = False
		Working_Pro.btn_setup.Enabled = False
		Working_Pro.btn_defect.Enabled = False
		Working_Pro.btn_ins_act.Enabled = False
        Working_Pro.btn_desc_act.Enabled = False
        Me.Enabled = True
        PictureBox16.Enabled = True
        Label2.Enabled = True
        Me.Visible = True
        PictureBox16.Visible = True
        Label2.Visible = True
        Timer1.Enabled = True
    End Sub
	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		count_close_popup += 1
		If count_close_popup = 30 Then
			Timer1.Enabled = False
			click_popup()
			count_close_popup = 0
		End If
	End Sub
End Class
