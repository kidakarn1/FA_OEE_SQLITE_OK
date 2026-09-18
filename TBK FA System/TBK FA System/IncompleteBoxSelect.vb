Public Class IncompleteBoxSelect
    Private ReadOnly _mode As ProductionStartMode
    Private ReadOnly _lineCode As String
    Private ReadOnly _currentWi As String
    Private ReadOnly _partNo As String
    Private ReadOnly _snp As Integer
    Private ReadOnly _nextProcess As String
    ' Optional, precomputed OLD ACTIVE candidates.  The normal selector keeps
    ' its existing server load/revalidation path when this is Nothing.
    Private ReadOnly _oldActiveRecoveryCandidates As List(Of IncompleteBoxRecord)
    Private ReadOnly _oldActiveRecoveryByTagId As Dictionary(Of Integer, IncompleteTransferCrashRecoveryRecord)
    Private ReadOnly _cardBorders As New List(Of Panel)()
    ' UI-only guards: prevent a rapid operator click from opening two server
    ' reads or confirming the same selected box twice. The SQL/query semantics
    ' and selected TagId validation remain unchanged.
    Private _boxesLoading As Boolean = False
    Private _confirmingSelection As Boolean = False

    Public Property SelectedBox As IncompleteBoxRecord
    Public Property SelectedOldActiveRecovery As IncompleteTransferCrashRecoveryRecord
    Public Property RequestedMode As ProductionStartMode = ProductionStartMode.None

    Public Sub New(mode As ProductionStartMode,
                   currentWi As String,
                   lineCode As String,
                   partNo As String,
                   partName As String,
                   model As String,
                   snp As Integer,
                   nextProcess As String,
                   Optional oldActiveRecoveryCandidates As List(Of IncompleteBoxRecord) = Nothing,
                   Optional oldActiveRecoveryByTagId As Dictionary(Of Integer, IncompleteTransferCrashRecoveryRecord) = Nothing)
        InitializeComponent()
        _mode = mode
        _currentWi = If(currentWi, String.Empty).Trim()
        _lineCode = If(lineCode, String.Empty).Trim()
        _partNo = If(partNo, String.Empty).Trim()
        _snp = snp
        _nextProcess = If(nextProcess, String.Empty).Trim()
        _oldActiveRecoveryCandidates = oldActiveRecoveryCandidates
        _oldActiveRecoveryByTagId = oldActiveRecoveryByTagId

        Dim continueMode As Boolean = mode = ProductionStartMode.ContinueExistingBox
        Text = If(continueMode, "Continue Existing Box", "Reprint Incomplete Tag")
        lblTitle.Text = If(continueMode, "CONTINUE INCOMPLETE BOX", "REPRINT INCOMPLETE TAG")
        btnConfirm.Text = If(continueMode, "CONTINUE", "REPRINT TAG")
        lblLineValue.Text = _lineCode
        lblPartValue.Text = _partNo
        lblPartNameValue.Text = If(partName, String.Empty).Trim()
        lblModelValue.Text = If(model, String.Empty).Trim()
        lblSnpValue.Text = _snp.ToString()
    End Sub

    Private Sub IncompleteBoxSelect_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ApplyResponsiveLayout()
        LoadBoxes()
    End Sub

    Private Sub IncompleteBoxSelect_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If pnlContent IsNot Nothing Then ApplyResponsiveLayout()
    End Sub

    Private Sub ApplyResponsiveLayout()
        If pnlContent.ClientSize.Width <= 0 OrElse pnlContent.ClientSize.Height <= 0 Then Return

        Dim contentWidth As Integer = pnlContent.ClientSize.Width
        Dim contentHeight As Integer = pnlContent.ClientSize.Height
        Dim rightWidth As Integer = 205
        Dim gap As Integer = 8
        Dim listWidth As Integer = contentWidth - 20 - rightWidth - gap
        Dim footerTop As Integer = contentHeight - 62
        Dim statusTop As Integer = footerTop - 42

        lblTitle.Width = contentWidth - 16
        pnlInfoBorder.Width = contentWidth - 20
        ' Keep the original surrounding layout. Only the column-label row below
        ' INCOMPLETE BOX LIST is hidden because every card labels its own data.
        pnlListHeader.Visible = True
        lblListHeader.Visible = False
        pnlListHeader.Height = 35
        pnlSelected.Visible = True
        pnlListBorder.SetBounds(10, 146, listWidth, statusTop - 154)
        pnlSelected.SetBounds(10 + listWidth + gap, 146, rightWidth, statusTop - 154)
        LayoutSelectedBoxMetrics()
        pnlStatusBar.SetBounds(10, statusTop, contentWidth - 20, 34)
        lblStatus.SetBounds(12, 2, pnlStatusBar.Width - 24, 28)
        pnlFooter.SetBounds(10, footerTop, contentWidth - 20, 52)
        btnConfirm.SetBounds(pnlFooter.Width - 208, 5, 198, 42)
        btnCancel.SetBounds(pnlFooter.Width - 370, 5, 152, 42)
        btnRefresh.SetBounds(pnlFooter.Width - 532, 5, 152, 42)

        For Each cardBorder As Panel In _cardBorders
            ' FlowLayout also includes child margins and the vertical scrollbar
            ' in its extent calculation.  Leave a real right gutter so it never
            ' creates a horizontal scrollbar.
            cardBorder.Width = GetFixedCardWidth()
        Next
    End Sub

    Private Function GetFixedCardWidth() As Integer
        Dim reservedWidth As Integer = flowBoxes.Padding.Horizontal +
                                       SystemInformation.VerticalScrollBarWidth +
                                       12
        ' Width must not depend on ClientSize: it changes after the vertical
        ' scrollbar appears and made early and later cards different lengths.
        Return Math.Max(320, flowBoxes.Width - reservedWidth)
    End Function

    ' The selector is fixed at 800x600 but Windows DPI and caption metrics can
    ' reduce the usable panel height.  Keep all three operator metrics visible
    ' instead of relying on static y-coordinates from the Designer.
    Private Sub LayoutSelectedBoxMetrics()
        Dim width As Integer = Math.Max(0, pnlSelected.ClientSize.Width - 10)
        Dim height As Integer = Math.Max(0, pnlSelected.ClientSize.Height)
        Dim topBox As Integer = 8
        Dim metricStart As Integer = 80
        Dim metricGap As Integer = Math.Max(58, CInt((height - metricStart - 34) / 3))

        lblSelectedCaption.SetBounds(5, topBox, width, 24)
        lblSelectedBox.SetBounds(5, topBox + 24, width, 34)

        lblCurrentCaption.SetBounds(5, metricStart, width, 22)
        lblSelectedQty.SetBounds(5, metricStart + 20, width, 34)

        lblSelectedSnpCaption.SetBounds(5, metricStart + metricGap, width, 22)
        lblSelectedSnp.SetBounds(5, metricStart + metricGap + 20, width, 34)

        lblProduceCaption.SetBounds(5, metricStart + (metricGap * 2), width, 22)
        lblProduceQty.SetBounds(5, metricStart + (metricGap * 2) + 20, width, 34)
    End Sub

    Private Sub LoadBoxes()
        If _boxesLoading Then Return
        _boxesLoading = True
        Dim performanceTimer As Stopwatch = Stopwatch.StartNew()
        SelectedBox = Nothing
        btnConfirm.Enabled = False
        btnRefresh.Enabled = False
        flowBoxes.Controls.Clear()
        _cardBorders.Clear()
        ClearSelectedSummary()
        pnlEmptyState.Visible = False
        flowBoxes.Visible = True
        lblStatus.Text = "Loading incomplete boxes from server..."
        Cursor = Cursors.WaitCursor

        Try
            Dim boxes As List(Of IncompleteBoxRecord)
            If _oldActiveRecoveryCandidates IsNot Nothing Then
                boxes = _oldActiveRecoveryCandidates
            Else
                boxes = Backoffice_model.GetIncompleteBoxes(_currentWi, _lineCode, _partNo, _snp, _nextProcess,
                                                             includeLegacyPendingStatus:=False,
                                                             allowCrossWi:=_mode = ProductionStartMode.ContinueExistingBox,
                                                             excludeActiveSourceOwnership:=_mode = ProductionStartMode.ContinueExistingBox)
            End If

            For Each box As IncompleteBoxRecord In boxes
                flowBoxes.Controls.Add(CreateBoxCard(box))
            Next

            pnlEmptyState.Visible = boxes.Count = 0
            flowBoxes.Visible = boxes.Count > 0

            Dim actionText As String = If(_mode = ProductionStartMode.ContinueExistingBox,
                                          "continue production",
                                          "reprint the tag")
            lblStatus.Text = If(boxes.Count = 0,
                                "NO INCOMPLETE BOX FOUND FOR THIS PART",
                                boxes.Count.ToString() & " INCOMPLETE BOX(ES) — SELECT ONE TO " & actionText.ToUpperInvariant())
        Catch ex As Exception
            lblStatus.Text = "CANNOT LOAD INCOMPLETE BOXES FROM SERVER"
            MessageBox.Show("Continue/Reprint requires a live server connection." & vbCrLf & ex.Message,
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
        Finally
            Cursor = Cursors.Default
            _boxesLoading = False
            btnRefresh.Enabled = True
            Backoffice_model.LogPerformance("IncompleteBoxSelect.Refresh", performanceTimer.ElapsedMilliseconds)
        End Try
    End Sub

    Private Function IsFullOldActiveRecovery(box As IncompleteBoxRecord) As Boolean
        If box Is Nothing OrElse _oldActiveRecoveryByTagId Is Nothing OrElse box.Snp <= 0 Then Return False
        Dim recovery As IncompleteTransferCrashRecoveryRecord = Nothing
        Return _oldActiveRecoveryByTagId.TryGetValue(box.TagId, recovery) AndAlso
               recovery IsNot Nothing AndAlso recovery.Transfer IsNot Nothing AndAlso
               recovery.Transfer.Flag = 0 AndAlso box.Quantity >= box.Snp
    End Function

    Private Function CreateBoxCard(box As IncompleteBoxRecord) As Panel
        Dim border As New Panel()
        border.Width = GetFixedCardWidth()
        border.Height = 116
        border.AutoSize = False
        border.Margin = New Padding(3, 5, 3, 5)
        border.Padding = New Padding(2)
        border.BackColor = Color.FromArgb(210, 219, 230)
        border.Cursor = Cursors.Hand
        border.Tag = box
        ApplyRoundedRegion(border, 9)
        _cardBorders.Add(border)

        Dim body As New Panel()
        body.Dock = DockStyle.Fill
        body.AutoSize = False
        body.BackColor = Color.White
        body.Tag = box
        border.Controls.Add(body)
        AddHandler body.Resize, Sub(sender As Object, e As EventArgs) ApplyRoundedRegion(body, 7)
        ApplyRoundedRegion(body, 7)

        Dim layoutWidth As Double = Math.Max(1, border.Width - 4)
        Dim scale As Double = layoutWidth / 520.0R
        Dim sx As Func(Of Integer, Integer) = Function(value) CInt(Math.Round(value * scale))

        Dim marker As Label = MakeCardLabel("○", sx(7), 17, sx(42), 46, 26.0!, Color.FromArgb(132, 153, 178), ContentAlignment.MiddleCenter)
        marker.Name = "selectionMarker"
        Dim textColor As Color = Color.FromArgb(20, 48, 75)
        Dim boxCaption As Label = MakeCardLabel("BOX", sx(64), 4, sx(92), 18, 9.0!, Color.FromArgb(91, 111, 140), ContentAlignment.MiddleLeft)
        Dim boxValue As Label = MakeCardLabel(box.BoxNo.ToString("000"), sx(64), 21, sx(100), 48, 21.0!, textColor, ContentAlignment.MiddleLeft)
        Dim fullOldRecovery As Boolean = IsFullOldActiveRecovery(box)
        Dim displaySnp As Integer = If(fullOldRecovery, box.Snp, If(_mode = ProductionStartMode.ContinueExistingBox, _snp, box.Snp))
        Dim displayRemaining As Integer = Math.Max(0, displaySnp - box.Quantity)
        Dim qtyCaption As Label = MakeCardLabel("QTY / SNP", sx(178), 4, sx(140), 18, 9.0!, Color.FromArgb(91, 111, 140), ContentAlignment.MiddleLeft)
        Dim qtyValue As Label = MakeCardLabel(box.Quantity.ToString() & " / " & displaySnp.ToString(), sx(178), 21, sx(144), 48, 19.0!, textColor, ContentAlignment.MiddleLeft)
        Dim remainCaption As Label = MakeCardLabel(If(fullOldRecovery, "STATUS", "REMAIN"), sx(342), 4, sx(140), 18, 9.0!, Color.FromArgb(91, 111, 140), ContentAlignment.MiddleLeft)
        Dim remainValue As Label = MakeCardLabel(If(fullOldRecovery, "FULL", displayRemaining.ToString()), sx(342), 21, sx(140), 48, 21.0!, Color.FromArgb(220, 112, 18), ContentAlignment.MiddleLeft)

        Dim dividerOne As Panel = MakeDivider(sx(52), 10, 1, 58)
        Dim dividerTwo As Panel = MakeDivider(sx(166), 10, 1, 58)
        Dim dividerThree As Panel = MakeDivider(sx(330), 10, 1, 58)

        Dim metadataX As Integer = sx(7)
        Dim metadataRow As Panel = MakeMetadataRow(box, metadataX, 76,
                                                   Math.Max(1, border.Width - (metadataX * 2) - 4), 32)

        body.Controls.AddRange(New Control() {marker, dividerOne, boxCaption, boxValue, dividerTwo,
                                              qtyCaption, qtyValue, dividerThree, remainCaption, remainValue,
                                              metadataRow})
        AddCardClickHandlers(border, box)
        Return border
    End Function

    Private Function MakeMetadataRow(box As IncompleteBoxRecord,
                                     x As Integer,
                                     y As Integer,
                                     width As Integer,
                                     height As Integer) As Panel
        Dim row As New Panel()
        row.SetBounds(x, y, width, height)
        row.AutoSize = False
        row.BackColor = Color.FromArgb(242, 246, 251)
        row.Tag = "identityChip"

        Dim wiWidth As Integer = CInt(width * 0.31R)
        Dim pwiWidth As Integer = CInt(width * 0.18R)
        Dim seqWidth As Integer = CInt(width * 0.17R)
        Dim lotWidth As Integer = CInt(width * 0.17R)
        Dim shiftWidth As Integer = width - wiWidth - pwiWidth - seqWidth - lotWidth
        Dim columnX As Integer = 0

        Dim wi As Label = MakeMetadataLabel("WI  |  " & box.Wi, columnX, wiWidth, height)
        columnX += wiWidth
        Dim pwi As Label = MakeMetadataLabel("PWI  |  " & box.PwiId, columnX, pwiWidth, height)
        columnX += pwiWidth
        Dim seq As Label = MakeMetadataLabel("SEQ  |  " & box.SeqNo, columnX, seqWidth, height)
        columnX += seqWidth
        Dim lot As Label = MakeMetadataLabel("LOT  |  " & If(String.IsNullOrWhiteSpace(box.LotNo), "-", box.LotNo), columnX, lotWidth, height)
        columnX += lotWidth
        Dim shift As Label = MakeMetadataLabel("SHIFT  |  " & If(String.IsNullOrWhiteSpace(box.Shift), "-", box.Shift), columnX, shiftWidth, height)

        row.Controls.AddRange(New Control() {wi, pwi, seq, lot, shift})
        Return row
    End Function

    Private Function MakeMetadataLabel(textValue As String,
                                       x As Integer,
                                       width As Integer,
                                       height As Integer) As Label
        Return MakeCardLabel(textValue, x, 0, width, height, 7.5!,
                             Color.FromArgb(16, 38, 67), ContentAlignment.MiddleCenter)
    End Function

    Private Function MakeDivider(x As Integer, y As Integer, width As Integer, height As Integer) As Panel
        Dim divider As New Panel()
        divider.SetBounds(x, y, width, height)
        divider.BackColor = Color.FromArgb(222, 230, 239)
        Return divider
    End Function

    Private Function MakeIdentityChip(caption As String,
                                      value As String,
                                      x As Integer,
                                      y As Integer,
                                      width As Integer,
                                      height As Integer) As Panel
        Dim chip As New Panel()
        chip.SetBounds(x, y, width, height)
        chip.BackColor = Color.FromArgb(242, 246, 251)
        chip.Tag = "identityChip"
        ApplyRoundedRegion(chip, 8)

        Dim captionWidth As Integer
        Select Case caption
            Case "WI"
                captionWidth = 26
            Case "PWI", "SEQ"
                captionWidth = 31
            Case "LOT"
                captionWidth = 27
            Case "SHIFT"
                captionWidth = 39
            Case Else
                captionWidth = 31
        End Select
        Dim captionLabel As Label = MakeCardLabel(caption, 3, 0, captionWidth, height, 7.5!, Color.FromArgb(91, 111, 140), ContentAlignment.MiddleCenter)
        Dim separator As Label = MakeCardLabel("|", captionWidth, 0, 8, height, 8.0!, Color.FromArgb(165, 180, 199), ContentAlignment.MiddleCenter)
        Dim valueLabel As Label = MakeCardLabel(If(value, String.Empty), captionWidth + 8, 0,
                                                Math.Max(0, width - captionWidth - 11), height,
                                                8.0!, Color.FromArgb(16, 38, 67), ContentAlignment.MiddleCenter)
        chip.Controls.AddRange(New Control() {captionLabel, separator, valueLabel})
        Return chip
    End Function

    Private Sub ApplyRoundedRegion(control As Control, radius As Integer)
        If control.Width <= 0 OrElse control.Height <= 0 Then Return
        Dim diameter As Integer = Math.Max(2, radius * 2)
        Dim bounds As New Rectangle(0, 0, control.Width, control.Height)
        Using path As New System.Drawing.Drawing2D.GraphicsPath()
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90)
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90)
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90)
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90)
            path.CloseFigure()
            Dim oldRegion As Region = control.Region
            control.Region = New Region(path)
            If oldRegion IsNot Nothing Then oldRegion.Dispose()
        End Using
    End Sub

    Private Function MakeCardLabel(textValue As String,
                                   x As Integer,
                                   y As Integer,
                                   width As Integer,
                                   height As Integer,
                                   fontSize As Single,
                                   colorValue As Color,
                                   alignment As ContentAlignment) As Label
        Dim label As New Label()
        label.Text = textValue
        label.AutoSize = False
        label.AutoEllipsis = True
        label.UseMnemonic = False
        label.SetBounds(x, y, width, height)
        label.ForeColor = colorValue
        label.Font = New Font("Microsoft Sans Serif", fontSize, FontStyle.Bold)
        label.TextAlign = alignment
        label.BackColor = Color.Transparent
        label.Cursor = Cursors.Hand
        Return label
    End Function

    Private Sub AddCardClickHandlers(control As Control, box As IncompleteBoxRecord)
        AddHandler control.Click, Sub(sender As Object, e As EventArgs) SelectBox(box)
        For Each child As Control In control.Controls
            AddCardClickHandlers(child, box)
        Next
    End Sub

    Private Sub SelectBox(box As IncompleteBoxRecord)
        SelectedBox = box
        btnConfirm.Enabled = True

        For Each border As Panel In _cardBorders
            Dim isSelected As Boolean = DirectCast(border.Tag, IncompleteBoxRecord).TagId = box.TagId
            border.BackColor = If(isSelected, Color.FromArgb(27, 170, 82), Color.FromArgb(190, 204, 216))
            If border.Controls.Count > 0 Then
                border.Controls(0).BackColor = If(isSelected, Color.FromArgb(239, 252, 244), Color.White)
            End If
            Dim marker As Control = FindNamedControl(border, "selectionMarker")
            If marker IsNot Nothing Then
                marker.Text = If(isSelected, "✓", "○")
                marker.ForeColor = If(isSelected, Color.FromArgb(18, 153, 70), Color.FromArgb(132, 153, 178))
                marker.BackColor = Color.Transparent
                marker.Font = New Font("Microsoft Sans Serif", If(isSelected, 17.0!, 26.0!), FontStyle.Bold)
                marker.Region = Nothing
            End If
            UpdateIdentityChipColors(border, isSelected)
        Next

        lblSelectedBox.Text = "BOX " & box.BoxNo.ToString("000")
        lblSelectedQty.Text = box.Quantity.ToString()
        Dim fullOldRecovery As Boolean = IsFullOldActiveRecovery(box)
        Dim displaySnp As Integer = If(fullOldRecovery, box.Snp, If(_mode = ProductionStartMode.ContinueExistingBox, _snp, box.Snp))
        Dim displayRemaining As Integer = Math.Max(0, displaySnp - box.Quantity)
        lblSelectedSnp.Text = displaySnp.ToString()
        lblProduceQty.Text = displayRemaining.ToString()
        lblStatus.Text = If(fullOldRecovery,
                            "FULL BOX — PENDING TAG COMPLETION",
                            "BOX " & box.BoxNo.ToString("000") & " SELECTED — " & box.Quantity & "/" & displaySnp & " PACKAGED")
    End Sub

    Private Sub UpdateIdentityChipColors(parent As Control, selected As Boolean)
        For Each child As Control In parent.Controls
            Dim chip As Panel = TryCast(child, Panel)
            If chip IsNot Nothing AndAlso TypeOf chip.Tag Is String AndAlso CStr(chip.Tag) = "identityChip" Then
                chip.BackColor = If(selected, Color.FromArgb(226, 247, 235), Color.FromArgb(242, 246, 251))
            End If
            UpdateIdentityChipColors(child, selected)
        Next
    End Sub

    Private Function FindNamedControl(parent As Control, controlName As String) As Control
        For Each child As Control In parent.Controls
            If child.Name = controlName Then Return child
            Dim nested As Control = FindNamedControl(child, controlName)
            If nested IsNot Nothing Then Return nested
        Next
        Return Nothing
    End Function

    Private Sub ClearSelectedSummary()
        lblSelectedBox.Text = "SELECT A BOX"
        lblSelectedQty.Text = "—"
        lblSelectedSnp.Text = "—"
        lblProduceQty.Text = "—"
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadBoxes()
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        ConfirmSelection()
    End Sub

    Private Sub ConfirmSelection()
        If SelectedBox Is Nothing Then Return
        If _confirmingSelection Then Return
        _confirmingSelection = True
        Dim performanceTimer As Stopwatch = Stopwatch.StartNew()

        Dim refreshed As IncompleteBoxRecord = Nothing
        Dim reason As String = String.Empty
        Dim valid As Boolean

        Dim oldRecovery As IncompleteTransferCrashRecoveryRecord = Nothing
        If _oldActiveRecoveryByTagId IsNot Nothing Then
            _oldActiveRecoveryByTagId.TryGetValue(SelectedBox.TagId, oldRecovery)
        End If
        If oldRecovery IsNot Nothing Then
            ' OLD ACTIVE rows are intentionally excluded from normal candidates.
            ' The read-only recovery discovery/detail query already established
            ' this selected row and its recovered packaging quantity.
            SelectedOldActiveRecovery = oldRecovery
            DialogResult = DialogResult.OK
            Close()
            Return
        End If

        Cursor = Cursors.WaitCursor
        btnConfirm.Enabled = False
        Try
            ' Continue is Part-based and single-terminal by scope.  Do not
            ' repurpose legacy flg_control = 2 as a reservation; retain the
            ' selected tag ID in the application until a full tag succeeds.
            valid = Backoffice_model.RevalidateIncompleteBox(SelectedBox.TagId,
                                                             _currentWi,
                                                             _lineCode,
                                                             _partNo,
                                                             _snp,
                                                             _nextProcess,
                                                              refreshed,
                                                               reason,
                                                                includeLegacyPendingStatus:=False,
                                                               allowCrossWi:=_mode = ProductionStartMode.ContinueExistingBox,
                                                               excludeActiveSourceOwnership:=_mode = ProductionStartMode.ContinueExistingBox)
        Finally
            Cursor = Cursors.Default
            _confirmingSelection = False
            Backoffice_model.LogPerformance("IncompleteBoxSelect.Confirm", performanceTimer.ElapsedMilliseconds)
        End Try

        If Not valid Then
            MessageBox.Show(reason, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            LoadBoxes()
            Return
        End If

        SelectedBox = refreshed
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click, btnClose.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
End Class
