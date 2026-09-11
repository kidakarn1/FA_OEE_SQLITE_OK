<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Alert_scan_qr_product
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Alert_scan_qr_product))
        Me.lbTitle = New System.Windows.Forms.Label()
        Me.lbDetail = New System.Windows.Forms.Label()
        Me.pcIcon = New System.Windows.Forms.PictureBox()
        Me.btn_close = New System.Windows.Forms.PictureBox()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_close, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbTitle
        '
        Me.lbTitle.AutoSize = True
        Me.lbTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.lbTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lbTitle.ForeColor = System.Drawing.Color.White
        Me.lbTitle.Location = New System.Drawing.Point(136, 268)
        Me.lbTitle.Name = "lbTitle"
        Me.lbTitle.Size = New System.Drawing.Size(437, 37)
        Me.lbTitle.TabIndex = 1
        Me.lbTitle.Text = "XXXXXXXXXXXXXXXXXXXX"
        '
        'lbDetail
        '
        Me.lbDetail.AutoSize = True
        Me.lbDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.lbDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lbDetail.ForeColor = System.Drawing.Color.White
        Me.lbDetail.Location = New System.Drawing.Point(162, 317)
        Me.lbDetail.Name = "lbDetail"
        Me.lbDetail.Size = New System.Drawing.Size(374, 31)
        Me.lbDetail.TabIndex = 2
        Me.lbDetail.Text = "XXXXXXXXXXXXXXXXXXXX"
        '
        'pcIcon
        '
        Me.pcIcon.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.pcIcon.Location = New System.Drawing.Point(317, 94)
        Me.pcIcon.Name = "pcIcon"
        Me.pcIcon.Size = New System.Drawing.Size(175, 155)
        Me.pcIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pcIcon.TabIndex = 3
        Me.pcIcon.TabStop = False
        '
        'btn_close
        '
        Me.btn_close.BackColor = System.Drawing.Color.Transparent
        Me.btn_close.Location = New System.Drawing.Point(307, 347)
        Me.btn_close.Name = "btn_close"
        Me.btn_close.Size = New System.Drawing.Size(198, 97)
        Me.btn_close.TabIndex = 5
        Me.btn_close.TabStop = False
        '
        'Alert_scan_qr_product
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 456)
        Me.Controls.Add(Me.btn_close)
        Me.Controls.Add(Me.pcIcon)
        Me.Controls.Add(Me.lbDetail)
        Me.Controls.Add(Me.lbTitle)
        Me.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Alert_scan_qr_product"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Alert_scan_qr_product"
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_close, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbTitle As Label
    Friend WithEvents lbDetail As Label
    Friend WithEvents pcIcon As PictureBox
    Friend WithEvents btn_close As PictureBox
End Class
