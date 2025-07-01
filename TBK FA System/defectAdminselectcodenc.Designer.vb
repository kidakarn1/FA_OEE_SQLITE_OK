<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class defectAdminselectcodenc
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
        Me.lvDefectcode = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lbPartfg = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.btnDown = New System.Windows.Forms.PictureBox()
        Me.btnUp = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lvDefectcode
        '
        Me.lvDefectcode.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvDefectcode.AllowColumnReorder = True
        Me.lvDefectcode.AllowDrop = True
        Me.lvDefectcode.AutoArrange = False
        Me.lvDefectcode.BackColor = System.Drawing.Color.Peru
        Me.lvDefectcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvDefectcode.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lvDefectcode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvDefectcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lvDefectcode.ForeColor = System.Drawing.Color.Black
        Me.lvDefectcode.FullRowSelect = True
        Me.lvDefectcode.GridLines = True
        Me.lvDefectcode.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvDefectcode.HideSelection = False
        Me.lvDefectcode.Location = New System.Drawing.Point(21, 231)
        Me.lvDefectcode.MultiSelect = False
        Me.lvDefectcode.Name = "lvDefectcode"
        Me.lvDefectcode.ShowGroups = False
        Me.lvDefectcode.Size = New System.Drawing.Size(660, 270)
        Me.lvDefectcode.TabIndex = 57
        Me.lvDefectcode.UseCompatibleStateImageBehavior = False
        Me.lvDefectcode.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Defect code"
        Me.ColumnHeader1.Width = 172
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Defect detail"
        Me.ColumnHeader2.Width = 486
        '
        'lbPartfg
        '
        Me.lbPartfg.AutoSize = True
        Me.lbPartfg.BackColor = System.Drawing.Color.Transparent
        Me.lbPartfg.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.75!, System.Drawing.FontStyle.Bold)
        Me.lbPartfg.ForeColor = System.Drawing.Color.White
        Me.lbPartfg.Location = New System.Drawing.Point(138, 152)
        Me.lbPartfg.Name = "lbPartfg"
        Me.lbPartfg.Size = New System.Drawing.Size(265, 29)
        Me.lbPartfg.TabIndex = 53
        Me.lbPartfg.Text = "XXXXXXXXXXXXXX"
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(8, 508)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(193, 84)
        Me.btnBack.TabIndex = 59
        Me.btnBack.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Location = New System.Drawing.Point(601, 508)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(193, 84)
        Me.btnDown.TabIndex = 60
        Me.btnDown.TabStop = False
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Location = New System.Drawing.Point(690, 231)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(100, 130)
        Me.btnUp.TabIndex = 61
        Me.btnUp.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Location = New System.Drawing.Point(690, 364)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(100, 130)
        Me.PictureBox2.TabIndex = 62
        Me.PictureBox2.TabStop = False
        '
        'defectAdminselectcodenc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.defectAdminselectcodeNC
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.lvDefectcode)
        Me.Controls.Add(Me.lbPartfg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "defectAdminselectcodenc"
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvDefectcode As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
	Friend WithEvents lbPartfg As Label
	Friend WithEvents btnBack As PictureBox
	Friend WithEvents btnDown As PictureBox
	Friend WithEvents btnUp As PictureBox
	Friend WithEvents PictureBox2 As PictureBox
End Class
