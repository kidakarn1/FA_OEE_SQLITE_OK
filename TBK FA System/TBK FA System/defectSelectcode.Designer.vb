<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class defectSelectcode
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
        Me.lbPartfg = New System.Windows.Forms.Label()
        Me.lvDefectcode = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lbType = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.PictureBox()
        Me.btnUp = New System.Windows.Forms.PictureBox()
        Me.btnDown = New System.Windows.Forms.PictureBox()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Button1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbPartfg
        '
        Me.lbPartfg.AutoSize = True
        Me.lbPartfg.BackColor = System.Drawing.Color.Transparent
        Me.lbPartfg.Font = New System.Drawing.Font("Panton-Trial ExtraBold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPartfg.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.lbPartfg.Location = New System.Drawing.Point(183, 119)
        Me.lbPartfg.Name = "lbPartfg"
        Me.lbPartfg.Size = New System.Drawing.Size(267, 35)
        Me.lbPartfg.TabIndex = 42
        Me.lbPartfg.Text = "XXXXXXXXXXXXXX"
        '
        'lvDefectcode
        '
        Me.lvDefectcode.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvDefectcode.AllowColumnReorder = True
        Me.lvDefectcode.AllowDrop = True
        Me.lvDefectcode.AutoArrange = False
        Me.lvDefectcode.BackColor = System.Drawing.Color.White
        Me.lvDefectcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvDefectcode.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lvDefectcode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvDefectcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lvDefectcode.ForeColor = System.Drawing.Color.Black
        Me.lvDefectcode.FullRowSelect = True
        Me.lvDefectcode.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvDefectcode.HideSelection = False
        Me.lvDefectcode.Location = New System.Drawing.Point(35, 194)
        Me.lvDefectcode.MultiSelect = False
        Me.lvDefectcode.Name = "lvDefectcode"
        Me.lvDefectcode.ShowGroups = False
        Me.lvDefectcode.Size = New System.Drawing.Size(636, 310)
        Me.lvDefectcode.TabIndex = 47
        Me.lvDefectcode.UseCompatibleStateImageBehavior = False
        Me.lvDefectcode.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Defect code"
        Me.ColumnHeader1.Width = 160
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Defect detail"
        Me.ColumnHeader2.Width = 350
        '
        'lbType
        '
        Me.lbType.AutoSize = True
        Me.lbType.BackColor = System.Drawing.Color.Transparent
        Me.lbType.Font = New System.Drawing.Font("Panton-Trial ExtraBold", 20.25!, System.Drawing.FontStyle.Bold)
        Me.lbType.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.lbType.Location = New System.Drawing.Point(6, 8)
        Me.lbType.Name = "lbType"
        Me.lbType.Size = New System.Drawing.Size(51, 35)
        Me.lbType.TabIndex = 51
        Me.lbType.Text = "XX"
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(23, 514)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(193, 79)
        Me.btnBack.TabIndex = 101
        Me.btnBack.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.Location = New System.Drawing.Point(589, 517)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(190, 74)
        Me.Button1.TabIndex = 102
        Me.Button1.TabStop = False
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Location = New System.Drawing.Point(682, 184)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(97, 155)
        Me.btnUp.TabIndex = 103
        Me.btnUp.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Location = New System.Drawing.Point(682, 364)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(97, 146)
        Me.btnDown.TabIndex = 104
        Me.btnDown.TabStop = False
        '
        'defectSelectcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.defectSelectcodeNew23
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.lbType)
        Me.Controls.Add(Me.lvDefectcode)
        Me.Controls.Add(Me.lbPartfg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "defectSelectcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "defectSelectcode"
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Button1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbPartfg As Label
	Friend WithEvents lvDefectcode As ListView
	Friend WithEvents ColumnHeader1 As ColumnHeader
	Friend WithEvents ColumnHeader2 As ColumnHeader
	Friend WithEvents lbType As Label
	Friend WithEvents btnBack As PictureBox
	Friend WithEvents Button1 As PictureBox
	Friend WithEvents btnUp As PictureBox
	Friend WithEvents btnDown As PictureBox
End Class
