<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Sel_prod_start
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.btnContinueBox = New System.Windows.Forms.Button()
        Me.btnNewBox = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(70, 42)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(620, 48)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "START PRODUCTION"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSubtitle
        '
        Me.lblSubtitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(190, 215, 240)
        Me.lblSubtitle.Location = New System.Drawing.Point(70, 91)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(620, 28)
        Me.lblSubtitle.TabIndex = 1
        Me.lblSubtitle.Text = "Select production type"
        Me.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnContinueBox
        '
        Me.btnContinueBox.BackColor = System.Drawing.Color.FromArgb(255, 145, 15)
        Me.btnContinueBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnContinueBox.FlatAppearance.BorderSize = 0
        Me.btnContinueBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnContinueBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold)
        Me.btnContinueBox.ForeColor = System.Drawing.Color.White
        Me.btnContinueBox.Location = New System.Drawing.Point(46, 145)
        Me.btnContinueBox.Name = "btnContinueBox"
        Me.btnContinueBox.Size = New System.Drawing.Size(320, 145)
        Me.btnContinueBox.TabIndex = 2
        Me.btnContinueBox.Text = "CONTINUE EXISTING BOX" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Select incomplete box"
        Me.btnContinueBox.UseVisualStyleBackColor = False
        '
        'btnNewBox
        '
        Me.btnNewBox.BackColor = System.Drawing.Color.FromArgb(25, 185, 90)
        Me.btnNewBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNewBox.FlatAppearance.BorderSize = 0
        Me.btnNewBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold)
        Me.btnNewBox.ForeColor = System.Drawing.Color.White
        Me.btnNewBox.Location = New System.Drawing.Point(394, 145)
        Me.btnNewBox.Name = "btnNewBox"
        Me.btnNewBox.Size = New System.Drawing.Size(320, 145)
        Me.btnNewBox.TabIndex = 3
        Me.btnNewBox.Text = "START NEW BOX" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Use normal production flow"
        Me.btnNewBox.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(704, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(42, 42)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "X"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Sel_prod_start
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(10, 35, 62)
        Me.ClientSize = New System.Drawing.Size(760, 330)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnNewBox)
        Me.Controls.Add(Me.btnContinueBox)
        Me.Controls.Add(Me.lblSubtitle)
        Me.Controls.Add(Me.lblTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Sel_prod_start"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Start Production"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents btnContinueBox As Button
    Friend WithEvents btnNewBox As Button
    Friend WithEvents btnClose As Button
End Class
