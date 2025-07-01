<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Rm_scan
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Rm_scan))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel_scan_picking = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.scan_item_cd = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        Me.Panel_scan_picking.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Controls.Add(Me.Panel_scan_picking)
        Me.Panel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(763, 473)
        Me.Panel1.TabIndex = 0
        '
        'Panel_scan_picking
        '
        Me.Panel_scan_picking.BackColor = System.Drawing.Color.CadetBlue
        Me.Panel_scan_picking.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.scanMaterial
        Me.Panel_scan_picking.Controls.Add(Me.PictureBox1)
        Me.Panel_scan_picking.Controls.Add(Me.Button3)
        Me.Panel_scan_picking.Controls.Add(Me.scan_item_cd)
        Me.Panel_scan_picking.Location = New System.Drawing.Point(0, 0)
        Me.Panel_scan_picking.Name = "Panel_scan_picking"
        Me.Panel_scan_picking.Size = New System.Drawing.Size(763, 473)
        Me.Panel_scan_picking.TabIndex = 23
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Transparent
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(278, 362)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(202, 79)
        Me.Button3.TabIndex = 2
        Me.Button3.UseVisualStyleBackColor = False
        '
        'scan_item_cd
        '
        Me.scan_item_cd.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.scan_item_cd.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.scan_item_cd.Font = New System.Drawing.Font("Catamaran", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.scan_item_cd.ForeColor = System.Drawing.SystemColors.Info
        Me.scan_item_cd.Location = New System.Drawing.Point(163, 231)
        Me.scan_item_cd.Name = "scan_item_cd"
        Me.scan_item_cd.Size = New System.Drawing.Size(447, 51)
        Me.scan_item_cd.TabIndex = 0
        Me.scan_item_cd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(626, 210)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(105, 98)
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'Rm_scan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(763, 473)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Rm_scan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rm_scan"
        Me.Panel1.ResumeLayout(False)
        Me.Panel_scan_picking.ResumeLayout(False)
        Me.Panel_scan_picking.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel_scan_picking As Panel
    Friend WithEvents Button3 As Button
    Friend WithEvents scan_item_cd As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As PictureBox
End Class
