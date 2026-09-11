<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MenuReprint
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MenuReprint))
        Me.pbRefectData = New System.Windows.Forms.PictureBox()
        Me.lbLineProd = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        CType(Me.pbRefectData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbRefectData
        '
        Me.pbRefectData.BackColor = System.Drawing.Color.Transparent
        Me.pbRefectData.Location = New System.Drawing.Point(645, 40)
        Me.pbRefectData.Name = "pbRefectData"
        Me.pbRefectData.Size = New System.Drawing.Size(74, 78)
        Me.pbRefectData.TabIndex = 3
        Me.pbRefectData.TabStop = False
        '
        'lbLineProd
        '
        Me.lbLineProd.AutoSize = True
        Me.lbLineProd.BackColor = System.Drawing.Color.Transparent
        Me.lbLineProd.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lbLineProd.ForeColor = System.Drawing.Color.White
        Me.lbLineProd.Location = New System.Drawing.Point(110, 150)
        Me.lbLineProd.Name = "lbLineProd"
        Me.lbLineProd.Size = New System.Drawing.Size(162, 25)
        Me.lbLineProd.TabIndex = 5
        Me.lbLineProd.Text = "XXXXXXXXXX"
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(12, 505)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(197, 83)
        Me.btnBack.TabIndex = 6
        Me.btnBack.TabStop = False
        '
        'MenuReprint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.lbLineProd)
        Me.Controls.Add(Me.pbRefectData)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "MenuReprint"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MenuReprint"
        CType(Me.pbRefectData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbRefectData As PictureBox
    Friend WithEvents lbLineProd As Label
    Friend WithEvents btnBack As PictureBox
End Class
