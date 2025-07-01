<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScanQRprod
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScanQRprod))
        Me.tb_QrProm = New System.Windows.Forms.TextBox()
        Me.lb_Remain = New System.Windows.Forms.Label()
        Me.lbTopices = New System.Windows.Forms.Label()
        Me.clear_remain = New System.Windows.Forms.Button()
        Me.btn_close = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'tb_QrProm
        '
        Me.tb_QrProm.BackColor = System.Drawing.Color.FromArgb(CType(CType(6, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.tb_QrProm.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tb_QrProm.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tb_QrProm.ForeColor = System.Drawing.Color.White
        Me.tb_QrProm.Location = New System.Drawing.Point(55, 186)
        Me.tb_QrProm.Name = "tb_QrProm"
        Me.tb_QrProm.Size = New System.Drawing.Size(693, 73)
        Me.tb_QrProm.TabIndex = 0
        '
        'lb_Remain
        '
        Me.lb_Remain.AutoSize = True
        Me.lb_Remain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.lb_Remain.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lb_Remain.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lb_Remain.ForeColor = System.Drawing.Color.White
        Me.lb_Remain.Location = New System.Drawing.Point(128, 439)
        Me.lb_Remain.Name = "lb_Remain"
        Me.lb_Remain.Size = New System.Drawing.Size(72, 33)
        Me.lb_Remain.TabIndex = 4
        Me.lb_Remain.Text = "XXX"
        '
        'lbTopices
        '
        Me.lbTopices.AutoSize = True
        Me.lbTopices.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.lbTopices.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lbTopices.ForeColor = System.Drawing.Color.White
        Me.lbTopices.Location = New System.Drawing.Point(176, 33)
        Me.lbTopices.Name = "lbTopices"
        Me.lbTopices.Size = New System.Drawing.Size(434, 42)
        Me.lbTopices.TabIndex = 5
        Me.lbTopices.Text = "XXXXXXXXXXXXXXXX"
        '
        'clear_remain
        '
        Me.clear_remain.BackColor = System.Drawing.Color.Transparent
        Me.clear_remain.FlatAppearance.BorderSize = 0
        Me.clear_remain.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.clear_remain.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.clear_remain.Image = Global.TBK_FA_System.My.Resources.Resources.btnClearRemainDMC
        Me.clear_remain.Location = New System.Drawing.Point(12, 338)
        Me.clear_remain.Name = "clear_remain"
        Me.clear_remain.Size = New System.Drawing.Size(277, 88)
        Me.clear_remain.TabIndex = 7
        Me.clear_remain.UseVisualStyleBackColor = False
        Me.clear_remain.Visible = False
        '
        'btn_close
        '
        Me.btn_close.BackColor = System.Drawing.Color.Transparent
        Me.btn_close.FlatAppearance.BorderSize = 0
        Me.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_close.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btn_close.Image = CType(resources.GetObject("btn_close.Image"), System.Drawing.Image)
        Me.btn_close.Location = New System.Drawing.Point(310, 338)
        Me.btn_close.Name = "btn_close"
        Me.btn_close.Size = New System.Drawing.Size(194, 88)
        Me.btn_close.TabIndex = 8
        Me.btn_close.UseVisualStyleBackColor = False
        '
        'ScanQRprod
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(775, 477)
        Me.Controls.Add(Me.btn_close)
        Me.Controls.Add(Me.clear_remain)
        Me.Controls.Add(Me.lbTopices)
        Me.Controls.Add(Me.lb_Remain)
        Me.Controls.Add(Me.tb_QrProm)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ScanQRprod"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScanDMC"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tb_QrProm As TextBox
    Friend WithEvents lb_Remain As Label
    Friend WithEvents lbTopices As Label
    Friend WithEvents clear_remain As Button
    Friend WithEvents btn_close As Button
End Class
