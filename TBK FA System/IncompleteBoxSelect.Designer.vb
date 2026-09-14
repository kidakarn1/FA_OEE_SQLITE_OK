<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class IncompleteBoxSelect
    Inherits System.Windows.Forms.Form

    Friend WithEvents pnlSidebar As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents lblStartTitle As Label
    Friend WithEvents lblStartSubtitle As Label
    Friend WithEvents btnModeContinue As Button
    Friend WithEvents btnModeNew As Button
    Friend WithEvents pnlContent As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlInfoBorder As Panel
    Friend WithEvents tblInfo As TableLayoutPanel
    Friend WithEvents lblLineValue As Label
    Friend WithEvents lblPartValue As Label
    Friend WithEvents lblPartNameValue As Label
    Friend WithEvents lblModelValue As Label
    Friend WithEvents lblSnpValue As Label
    Friend WithEvents pnlListBorder As Panel
    Friend WithEvents pnlListHeader As Panel
    Friend WithEvents lblListTitle As Label
    Friend WithEvents lblListHeader As Label
    Friend WithEvents flowBoxes As FlowLayoutPanel
    Friend WithEvents pnlEmptyState As Panel
    Friend WithEvents lblEmptyIcon As Label
    Friend WithEvents lblEmptyText As Label
    Friend WithEvents pnlSelected As Panel
    Friend WithEvents lblSelectedCaption As Label
    Friend WithEvents lblSelectedBox As Label
    Friend WithEvents lblCurrentCaption As Label
    Friend WithEvents lblSelectedQty As Label
    Friend WithEvents lblSelectedSnpCaption As Label
    Friend WithEvents lblSelectedSnp As Label
    Friend WithEvents lblProduceCaption As Label
    Friend WithEvents lblProduceQty As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents pnlStatusBar As Panel
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnConfirm As Button
    Friend WithEvents pnlFooter As Panel

    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlSidebar = New Panel()
        Me.btnClose = New Button()
        Me.lblStartTitle = New Label()
        Me.lblStartSubtitle = New Label()
        Me.btnModeContinue = New Button()
        Me.btnModeNew = New Button()
        Me.pnlContent = New Panel()
        Me.lblTitle = New Label()
        Me.pnlInfoBorder = New Panel()
        Me.tblInfo = New TableLayoutPanel()
        Me.lblLineValue = New Label()
        Me.lblPartValue = New Label()
        Me.lblPartNameValue = New Label()
        Me.lblModelValue = New Label()
        Me.lblSnpValue = New Label()
        Me.pnlListBorder = New Panel()
        Me.pnlListHeader = New Panel()
        Me.lblListTitle = New Label()
        Me.lblListHeader = New Label()
        Me.flowBoxes = New FlowLayoutPanel()
        Me.pnlEmptyState = New Panel()
        Me.lblEmptyIcon = New Label()
        Me.lblEmptyText = New Label()
        Me.pnlSelected = New Panel()
        Me.lblSelectedCaption = New Label()
        Me.lblSelectedBox = New Label()
        Me.lblCurrentCaption = New Label()
        Me.lblSelectedQty = New Label()
        Me.lblSelectedSnpCaption = New Label()
        Me.lblSelectedSnp = New Label()
        Me.lblProduceCaption = New Label()
        Me.lblProduceQty = New Label()
        Me.lblStatus = New Label()
        Me.pnlStatusBar = New Panel()
        Me.btnRefresh = New Button()
        Me.btnCancel = New Button()
        Me.btnConfirm = New Button()
        Me.pnlFooter = New Panel()
        Me.pnlSidebar.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.pnlInfoBorder.SuspendLayout()
        Me.tblInfo.SuspendLayout()
        Me.pnlListBorder.SuspendLayout()
        Me.pnlListHeader.SuspendLayout()
        Me.pnlEmptyState.SuspendLayout()
        Me.pnlSelected.SuspendLayout()
        Me.pnlStatusBar.SuspendLayout()
        Me.pnlFooter.SuspendLayout()
        Me.SuspendLayout()

        Me.BackColor = Color.FromArgb(3, 22, 39)
        Me.ForeColor = Color.White
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.WindowState = FormWindowState.Normal
        Me.AutoScaleMode = AutoScaleMode.None
        Me.ClientSize = New Size(800, 600)
        Me.MinimumSize = New Size(800, 600)
        Me.MaximumSize = New Size(800, 600)
        Me.Text = "Continue Existing Box"

        ' The production type is chosen on Sel_prod_start.  This dialog only
        ' selects an existing box, so it deliberately has no secondary Actions menu.
        Me.pnlSidebar.Visible = False

        ConfigureFlatButton(Me.btnClose, "×", Color.Transparent, 18.0!, FontStyle.Regular)
        Me.btnClose.Location = New Point(8, 6)
        Me.btnClose.Size = New Size(38, 38)

        Me.lblStartTitle.Text = "ACTIONS"
        Me.lblStartTitle.Font = New Font("Microsoft Sans Serif", 16.0!, FontStyle.Bold)
        Me.lblStartTitle.TextAlign = ContentAlignment.MiddleCenter
        Me.lblStartTitle.Location = New Point(12, 58)
        Me.lblStartTitle.Size = New Size(211, 36)

        Me.lblStartSubtitle.Text = "Select production type"
        Me.lblStartSubtitle.ForeColor = Color.FromArgb(190, 207, 225)
        Me.lblStartSubtitle.Font = New Font("Microsoft Sans Serif", 9.0!, FontStyle.Bold)
        Me.lblStartSubtitle.TextAlign = ContentAlignment.MiddleCenter
        Me.lblStartSubtitle.Location = New Point(12, 94)
        Me.lblStartSubtitle.Size = New Size(211, 24)

        ConfigureModeButton(Me.btnModeContinue, "▣  CONTINUE EXISTING BOX" & vbCrLf & "    INCOMPLETE BOX", Color.FromArgb(255, 145, 15), 132)
        ConfigureModeButton(Me.btnModeNew, "▤  START NEW BOX" & vbCrLf & "    NORMAL PRODUCTION", Color.FromArgb(25, 185, 90), 224)

        Me.pnlContent.BackColor = Color.FromArgb(3, 22, 39)
        Me.pnlContent.Dock = DockStyle.Fill
        Me.pnlContent.Padding = New Padding(10)
        Me.pnlContent.Controls.AddRange(New Control() {Me.btnClose, Me.pnlFooter, Me.pnlStatusBar,
                                                       Me.pnlSelected, Me.pnlListBorder, Me.pnlInfoBorder, Me.lblTitle})

        Me.lblTitle.Text = "CONTINUE INCOMPLETE BOX"
        Me.lblTitle.Font = New Font("Microsoft Sans Serif", 24.0!, FontStyle.Bold)
        Me.lblTitle.ForeColor = Color.White
        Me.lblTitle.TextAlign = ContentAlignment.MiddleCenter
        Me.lblTitle.Location = New Point(8, 6)
        Me.lblTitle.Size = New Size(784, 44)
        Me.lblTitle.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        Me.pnlInfoBorder.BackColor = Color.FromArgb(6, 20, 37)
        Me.pnlInfoBorder.Padding = New Padding(0)
        Me.pnlInfoBorder.Location = New Point(10, 50)
        Me.pnlInfoBorder.Height = 88
        Me.pnlInfoBorder.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.pnlInfoBorder.Controls.Add(Me.tblInfo)

        Me.tblInfo.Dock = DockStyle.Fill
        Me.tblInfo.BackColor = Color.FromArgb(3, 22, 39)
        Me.tblInfo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        ' Keep the top summary to the four production identifiers.  SNP is still
        ' shown in the selected-box panel, where it is needed for the decision.
        Me.tblInfo.ColumnCount = 4
        Me.tblInfo.RowCount = 1
        Me.tblInfo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.0!))
        Me.tblInfo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 27.0!))
        Me.tblInfo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 35.0!))
        Me.tblInfo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 22.0!))
        Me.tblInfo.Controls.Add(CreateInfoPanel("LINE", Me.lblLineValue), 0, 0)
        Me.tblInfo.Controls.Add(CreateInfoPanel("PART NO.", Me.lblPartValue), 1, 0)
        Me.tblInfo.Controls.Add(CreateInfoPanel("PART NAME", Me.lblPartNameValue), 2, 0)
        Me.tblInfo.Controls.Add(CreateInfoPanel("MODEL", Me.lblModelValue), 3, 0)

        Me.pnlListBorder.BackColor = Color.FromArgb(205, 215, 224)
        Me.pnlListBorder.Location = New Point(10, 146)
        Me.pnlListBorder.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.pnlListBorder.Padding = New Padding(1)
        Me.pnlListBorder.Controls.AddRange(New Control() {Me.pnlEmptyState, Me.flowBoxes, Me.pnlListHeader})

        Me.pnlListHeader.BackColor = Color.White
        Me.pnlListHeader.Dock = DockStyle.Top
        Me.pnlListHeader.Height = 35
        Me.pnlListHeader.Visible = True
        Me.pnlListHeader.Controls.AddRange(New Control() {Me.lblListHeader, Me.lblListTitle})
        Me.lblListTitle.Dock = DockStyle.Top
        Me.lblListTitle.Height = 35
        Me.lblListTitle.Text = "INCOMPLETE BOX LIST"
        Me.lblListTitle.ForeColor = Color.FromArgb(16, 48, 78)
        Me.lblListTitle.Font = New Font("Microsoft Sans Serif", 16.0!, FontStyle.Bold)
        Me.lblListTitle.Padding = New Padding(10, 0, 0, 0)
        Me.lblListTitle.TextAlign = ContentAlignment.MiddleLeft
        Me.lblListHeader.Dock = DockStyle.Bottom
        Me.lblListHeader.Height = 29
        Me.lblListHeader.Text = "SELECT     BOX     QTY / SNP     REMAIN     LOT     SHIFT     UPDATED"
        Me.lblListHeader.Visible = False
        Me.lblListHeader.ForeColor = Color.FromArgb(27, 102, 164)
        Me.lblListHeader.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Bold)
        Me.lblListHeader.TextAlign = ContentAlignment.MiddleCenter

        Me.flowBoxes.BackColor = Color.FromArgb(245, 248, 251)
        Me.flowBoxes.Dock = DockStyle.Fill
        ' Keep the vertical scroll for multiple incomplete boxes, but suppress
        ' the unnecessary horizontal scrollbar.
        Me.flowBoxes.AutoScroll = True
        Me.flowBoxes.HorizontalScroll.Enabled = False
        Me.flowBoxes.HorizontalScroll.Visible = False
        Me.flowBoxes.FlowDirection = FlowDirection.TopDown
        Me.flowBoxes.WrapContents = False
        Me.flowBoxes.Padding = New Padding(6)

        Me.pnlEmptyState.BackColor = Color.FromArgb(245, 248, 251)
        Me.pnlEmptyState.Dock = DockStyle.Fill
        Me.pnlEmptyState.Controls.AddRange(New Control() {Me.lblEmptyText, Me.lblEmptyIcon})
        Me.lblEmptyIcon.Text = "□"
        Me.lblEmptyIcon.ForeColor = Color.FromArgb(83, 130, 170)
        Me.lblEmptyIcon.Font = New Font("Microsoft Sans Serif", 34.0!, FontStyle.Regular)
        Me.lblEmptyIcon.Dock = DockStyle.Top
        Me.lblEmptyIcon.Height = 100
        Me.lblEmptyIcon.TextAlign = ContentAlignment.BottomCenter
        Me.lblEmptyText.Text = "No incomplete box records"
        Me.lblEmptyText.ForeColor = Color.FromArgb(65, 85, 105)
        Me.lblEmptyText.Font = New Font("Microsoft Sans Serif", 10.0!, FontStyle.Regular)
        Me.lblEmptyText.Dock = DockStyle.Top
        Me.lblEmptyText.Height = 40
        Me.lblEmptyText.TextAlign = ContentAlignment.TopCenter

        Me.pnlSelected.BackColor = Color.White
        Me.pnlSelected.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        Me.pnlSelected.BorderStyle = BorderStyle.FixedSingle
        Me.pnlSelected.Controls.AddRange(New Control() {Me.lblSelectedCaption, Me.lblSelectedBox,
                                                        Me.lblCurrentCaption, Me.lblSelectedQty,
                                                        Me.lblSelectedSnpCaption, Me.lblSelectedSnp,
                                                        Me.lblProduceCaption, Me.lblProduceQty})
        ConfigureSummaryLabel(Me.lblSelectedCaption, "BOX DETAILS", 5, 12, 10.0!, Color.FromArgb(25, 104, 174))
        ConfigureSummaryLabel(Me.lblSelectedBox, "SELECT A BOX", 5, 40, 17.0!, Color.FromArgb(16, 48, 78))
        ConfigureSummaryLabel(Me.lblCurrentCaption, "CURRENT QTY", 5, 105, 9.0!, Color.FromArgb(25, 104, 174))
        ConfigureSummaryLabel(Me.lblSelectedQty, "—", 5, 132, 22.0!, Color.FromArgb(16, 48, 78))
        ConfigureSummaryLabel(Me.lblSelectedSnpCaption, "SNP (PLAN)", 5, 194, 9.0!, Color.FromArgb(25, 104, 174))
        ConfigureSummaryLabel(Me.lblSelectedSnp, "—", 5, 221, 22.0!, Color.FromArgb(16, 48, 78))
        ConfigureSummaryLabel(Me.lblProduceCaption, "PRODUCE (REMAIN)", 5, 283, 8.0!, Color.FromArgb(25, 104, 174))
        ConfigureSummaryLabel(Me.lblProduceQty, "—", 5, 310, 22.0!, Color.FromArgb(220, 112, 18))

        Me.pnlStatusBar.BackColor = Color.FromArgb(5, 31, 54)
        Me.pnlStatusBar.BorderStyle = BorderStyle.FixedSingle
        Me.pnlStatusBar.Controls.Add(Me.lblStatus)
        Me.lblStatus.ForeColor = Color.FromArgb(230, 240, 250)
        Me.lblStatus.Font = New Font("Microsoft Sans Serif", 9.0!, FontStyle.Bold)
        Me.lblStatus.TextAlign = ContentAlignment.MiddleLeft
        Me.lblStatus.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        Me.pnlFooter.BackColor = Color.FromArgb(3, 22, 39)
        Me.pnlFooter.BorderStyle = BorderStyle.FixedSingle
        Me.pnlFooter.Controls.AddRange(New Control() {Me.btnRefresh, Me.btnCancel, Me.btnConfirm})

        ConfigureFlatButton(Me.btnRefresh, "↻  REFRESH", Color.White, 12.0!, FontStyle.Bold)
        Me.btnRefresh.ForeColor = Color.FromArgb(25, 104, 174)
        Me.btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(120, 150, 180)
        Me.btnRefresh.FlatAppearance.BorderSize = 1
        ConfigureFlatButton(Me.btnCancel, "←  BACK", Color.White, 12.0!, FontStyle.Bold)
        Me.btnCancel.ForeColor = Color.FromArgb(16, 48, 78)
        Me.btnCancel.FlatAppearance.BorderColor = Color.FromArgb(120, 150, 180)
        Me.btnCancel.FlatAppearance.BorderSize = 1
        ConfigureFlatButton(Me.btnConfirm, "▶  CONTINUE", Color.FromArgb(29, 168, 82), 14.0!, FontStyle.Bold)
        Me.btnRefresh.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.btnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.btnConfirm.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.btnConfirm.Enabled = False
        Me.AcceptButton = Me.btnConfirm
        Me.CancelButton = Me.btnCancel

        Me.Controls.Add(Me.pnlContent)
        Me.pnlSidebar.ResumeLayout(False)
        Me.pnlContent.ResumeLayout(False)
        Me.pnlInfoBorder.ResumeLayout(False)
        Me.tblInfo.ResumeLayout(False)
        Me.pnlListBorder.ResumeLayout(False)
        Me.pnlListHeader.ResumeLayout(False)
        Me.pnlEmptyState.ResumeLayout(False)
        Me.pnlSelected.ResumeLayout(False)
        Me.pnlStatusBar.ResumeLayout(False)
        Me.pnlFooter.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Private Sub ConfigureModeButton(button As Button, textValue As String, colorValue As Color, top As Integer)
        ConfigureFlatButton(button, textValue, colorValue, 9.0!, FontStyle.Bold)
        button.Location = New Point(14, top)
        button.Size = New Size(207, 80)
        button.TextAlign = ContentAlignment.MiddleLeft
        button.Padding = New Padding(10, 0, 4, 0)
    End Sub

    Private Sub ConfigureFlatButton(button As Button, textValue As String, colorValue As Color, fontSize As Single, style As FontStyle)
        button.Text = textValue
        button.BackColor = colorValue
        button.ForeColor = Color.White
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
        button.Cursor = Cursors.Hand
        button.Font = New Font("Microsoft Sans Serif", fontSize, style)
        button.UseVisualStyleBackColor = False
    End Sub

    Private Function CreateInfoPanel(caption As String, valueLabel As Label) As Panel
        Dim panel As New Panel()
        panel.Dock = DockStyle.Fill
        panel.Margin = New Padding(5)
        Select Case caption
            Case "LINE" : panel.BackColor = Color.FromArgb(35, 112, 205)
            Case "PART NO." : panel.BackColor = Color.FromArgb(20, 150, 142)
            Case "PART NAME" : panel.BackColor = Color.FromArgb(118, 77, 180)
            Case Else : panel.BackColor = Color.FromArgb(226, 126, 35)
        End Select
        panel.BorderStyle = BorderStyle.FixedSingle
        ' Explicit bounds are used instead of Dock/TableLayout here.  The legacy
        ' application is DPI-unaware and the nested layout could hide field values
        ' on the fixed 800 x 600 production terminal.
        Dim title As New Label()
        title.Text = caption
        title.ForeColor = Color.FromArgb(230, 244, 255)
        title.Font = New Font("Microsoft Sans Serif", 9.0!, FontStyle.Bold)
        title.TextAlign = ContentAlignment.BottomCenter
        valueLabel.ForeColor = Color.White
        valueLabel.Font = New Font("Microsoft Sans Serif", 13.0!, FontStyle.Bold)
        ' Keep the production values directly below their captions; vertical
        ' centering placed them too close to the lower edge of each header card.
        valueLabel.TextAlign = ContentAlignment.TopCenter
        valueLabel.AutoEllipsis = False
        panel.Controls.AddRange(New Control() {title, valueLabel})
        AddHandler panel.Resize,
            Sub(sender As Object, e As EventArgs)
                title.SetBounds(2, 6, Math.Max(0, panel.ClientSize.Width - 4), 20)
                valueLabel.SetBounds(2, 30, Math.Max(0, panel.ClientSize.Width - 4), Math.Max(0, panel.ClientSize.Height - 32))
            End Sub
        title.SetBounds(2, 6, 10, 20)
        valueLabel.SetBounds(2, 30, 10, 35)
        Return panel
    End Function

    Private Sub ConfigureSummaryLabel(label As Label, textValue As String, x As Integer, y As Integer, fontSize As Single, colorValue As Color)
        label.Text = textValue
        label.Location = New Point(x, y)
        label.Size = New Size(195, 38)
        label.TextAlign = ContentAlignment.MiddleCenter
        label.ForeColor = colorValue
        label.Font = New Font("Microsoft Sans Serif", fontSize, FontStyle.Bold)
    End Sub
End Class
