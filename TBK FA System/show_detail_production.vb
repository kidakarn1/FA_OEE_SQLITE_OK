Public Class show_detail_production
    Private Sub show_detail_production_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lbversion.Text = MainFrm.Label9.Text
        Me.Location = New Point(7, 78)
        Dim plan_seq As String
        Dim num_char_seq As Integer
        num_char_seq = Working_Pro.Label22.Text.Length
        If num_char_seq = 1 Then
            plan_seq = "00" & Working_Pro.Label22.Text
        ElseIf num_char_seq = 2 Then
            plan_seq = "0" & Working_Pro.Label22.Text
        Else
            plan_seq = Working_Pro.Label22.Text
        End If
        LBGOOD.Text = Working_Pro.lb_good.Text
        LBSEQCOUNT.Text = Working_Pro.LB_COUNTER_SEQ.Text
        LBPLAN.Text = Working_Pro.Label8.Text
        LBREMAIN.Text = Working_Pro.Label10.Text
        LB_PARTNO.Text = Working_Pro.Label3.Text
        LB_PART_NAME.Text = Working_Pro.Label12.Text
        LB_MODEL.Text = Working_Pro.lb_model.Text
        LB_SEQ.Text = plan_seq
        LB_SNP.Text = Working_Pro.Label27.Text
        LB_SHIFT.Text = Working_Pro.Label14.Text
        LB_START_TIME.Text = Working_Pro.Label16.Text
        LB_END_TIME.Text = Working_Pro.Label20.Text
        LB_STD_CT.Text = Working_Pro.Label38.Text
        LB_ACT_CT.Text = Working_Pro.Label37.Text
        LB_PLAN_DATE.Text = Prd_detail.LB_PLAN_DATE.Text
        LB_WORKER.Text = MainFrm.LB_Number_worker.Text
        LB_WI.Text = Prd_detail.lb_wi.Text
        lbNextTime.Text = Working_Pro.lbNextTime.Text
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Working_Pro.Enabled = True
        Me.Close()
    End Sub
    Private Sub LB_WI_Click(sender As Object, e As EventArgs) Handles LB_WI.Click

    End Sub
    Private Sub LB_ACT_CT_Click(sender As Object, e As EventArgs) Handles LB_ACT_CT.Click

    End Sub
End Class