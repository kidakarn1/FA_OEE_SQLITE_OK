<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LeaderConfirmDefect
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LeaderConfirmDefect))
        Me.PanelEmpCodeleader = New System.Windows.Forms.PictureBox()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.tbEmpCodeleader = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PanelEmpCodeleader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelEmpCodeleader
        '
        Me.PanelEmpCodeleader.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PanelEmpCodeleader.BackColor = System.Drawing.Color.Transparent
        Me.PanelEmpCodeleader.Location = New System.Drawing.Point(131, 315)
        Me.PanelEmpCodeleader.Name = "PanelEmpCodeleader"
        Me.PanelEmpCodeleader.Size = New System.Drawing.Size(443, 55)
        Me.PanelEmpCodeleader.TabIndex = 4
        Me.PanelEmpCodeleader.TabStop = False
        '
        'btnBack
        '
        Me.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(285, 436)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(228, 102)
        Me.btnBack.TabIndex = 5
        Me.btnBack.TabStop = False
        '
        'tbEmpCodeleader
        '
        Me.tbEmpCodeleader.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.tbEmpCodeleader.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.tbEmpCodeleader.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbEmpCodeleader.Font = New System.Drawing.Font("Microsoft Sans Serif", 32.0!)
        Me.tbEmpCodeleader.ForeColor = System.Drawing.Color.White
        Me.tbEmpCodeleader.Location = New System.Drawing.Point(149, 317)
        Me.tbEmpCodeleader.Name = "tbEmpCodeleader"
        Me.tbEmpCodeleader.Size = New System.Drawing.Size(408, 49)
        Me.tbEmpCodeleader.TabIndex = 6
        Me.tbEmpCodeleader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(598, 297)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(102, 96)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'LeaderConfirmDefect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.tbEmpCodeleader)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.PanelEmpCodeleader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LeaderConfirmDefect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LeaderConfirm"
        CType(Me.PanelEmpCodeleader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PanelEmpCodeleader As PictureBox
    Friend WithEvents btnBack As PictureBox
    Friend WithEvents tbEmpCodeleader As TextBox
    Friend WithEvents PictureBox1 As PictureBox
End Class
