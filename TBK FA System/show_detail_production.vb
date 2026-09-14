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
        RefreshOldIncompleteResumeDisplay()
        AddHandler Working_Pro.lb_qty_for_box.TextChanged, AddressOf RecoveryPackagingChanged
    End Sub

    Private Sub RecoveryPackagingChanged(sender As Object, e As EventArgs)
        RefreshOldIncompleteResumeDisplay()
    End Sub

    Private Sub DetailClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler Working_Pro.lb_qty_for_box.TextChanged, AddressOf RecoveryPackagingChanged
    End Sub

    Public Sub RefreshOldIncompleteResumeDisplay()
        lblIncompleteBaseQtyCaption.Text = If(ProductionStartFlowState.SelectedOldActiveRecovery IsNot Nothing, "Qty", "Base Qty")
        Dim sourceWi As String = String.Empty
        Dim currentPackagingQty As Integer = 0

        If Working_Pro.TryGetResumeDetailValues(sourceWi, currentPackagingQty) Then
            pnlOldIncomplete.Visible = True
            lblOldIncompleteWI.Text = sourceWi
            lblIncompleteBaseQty.Text = currentPackagingQty.ToString()
        ElseIf Working_Pro.IsContinueIncompleteDisplayFinished() Then
            pnlOldIncomplete.Visible = True
            lblOldIncompleteWI.Text = "Finish"
            lblIncompleteBaseQty.Text = "Finish"
        Else
            pnlOldIncomplete.Visible = False
            lblOldIncompleteWI.Text = "000"
            lblIncompleteBaseQty.Text = "000"
        End If

        WriteResumeUiTrace("RefreshOldIncompleteResumeDisplay")
    End Sub

    Private Sub show_detail_production_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        RefreshOldIncompleteResumeDisplay()
    End Sub

    Private Sub show_detail_production_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        RefreshOldIncompleteResumeDisplay()
    End Sub

    <System.Diagnostics.Conditional("DEBUG")>
    Private Sub WriteResumeUiTrace(caller As String)
        Dim context = Working_Pro.ResumeContext
        System.Diagnostics.Debug.WriteLine("[RESUME-UI] caller=" & caller &
                                           " | IsActive=" & context.IsResumeActive.ToString() &
                                           " | Durable=" & context.DurableActiveConfirmed.ToString() &
                                           " | TransferId=" & context.BackendTransferId.ToString() &
                                           " | SourceWi=" & context.SourceWiAtResume &
                                           " | Base=" & context.BaseBoxQuantity.ToString() &
                                           " | lb_qty_for_box=" & Working_Pro.lb_qty_for_box.Text &
                                           " | DisplayedWi=" & lblOldIncompleteWI.Text &
                                           " | DisplayedBase=" & lblIncompleteBaseQty.Text)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Working_Pro.Enabled = True
        Me.Close()
    End Sub
End Class
