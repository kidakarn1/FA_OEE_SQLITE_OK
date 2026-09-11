<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class defectAdminAdjustdetail
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(defectAdminAdjustdetail))
        Me.lbLine = New System.Windows.Forms.Label()
        Me.setCombobox = New System.Windows.Forms.ComboBox()
        Me.lvDefectact = New System.Windows.Forms.ListView()
        Me.NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PART_NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CODE = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.DETAILS = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.QTY_DEFECT = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnUp = New System.Windows.Forms.PictureBox()
        Me.btnDown = New System.Windows.Forms.PictureBox()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.btnOk = New System.Windows.Forms.PictureBox()
        Me.backgroundNg = New System.Windows.Forms.PictureBox()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.backgroundNg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbLine
        '
        Me.lbLine.AutoSize = True
        Me.lbLine.BackColor = System.Drawing.Color.Transparent
        Me.lbLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLine.ForeColor = System.Drawing.Color.White
        Me.lbLine.Location = New System.Drawing.Point(105, 88)
        Me.lbLine.Name = "lbLine"
        Me.lbLine.Size = New System.Drawing.Size(77, 25)
        Me.lbLine.TabIndex = 45
        Me.lbLine.Text = "Label1"
        '
        'setCombobox
        '
        Me.setCombobox.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.setCombobox.FormattingEnabled = True
        Me.setCombobox.Location = New System.Drawing.Point(20, 137)
        Me.setCombobox.Name = "setCombobox"
        Me.setCombobox.Size = New System.Drawing.Size(299, 45)
        Me.setCombobox.TabIndex = 55
        Me.setCombobox.Text = "XX"
        '
        'lvDefectact
        '
        Me.lvDefectact.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvDefectact.AllowColumnReorder = True
        Me.lvDefectact.AllowDrop = True
        Me.lvDefectact.AutoArrange = False
        Me.lvDefectact.BackColor = System.Drawing.Color.Peru
        Me.lvDefectact.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvDefectact.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NO, Me.PART_NO, Me.CODE, Me.DETAILS, Me.QTY_DEFECT})
        Me.lvDefectact.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvDefectact.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lvDefectact.ForeColor = System.Drawing.Color.Black
        Me.lvDefectact.FullRowSelect = True
        Me.lvDefectact.GridLines = True
        Me.lvDefectact.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvDefectact.HideSelection = False
        Me.lvDefectact.Location = New System.Drawing.Point(25, 231)
        Me.lvDefectact.MultiSelect = False
        Me.lvDefectact.Name = "lvDefectact"
        Me.lvDefectact.ShowGroups = False
        Me.lvDefectact.Size = New System.Drawing.Size(659, 271)
        Me.lvDefectact.TabIndex = 56
        Me.lvDefectact.UseCompatibleStateImageBehavior = False
        Me.lvDefectact.View = System.Windows.Forms.View.Details
        '
        'NO
        '
        Me.NO.Text = "NO"
        Me.NO.Width = 1
        '
        'PART_NO
        '
        Me.PART_NO.Text = "PART NO"
        Me.PART_NO.Width = 248
        '
        'CODE
        '
        Me.CODE.Text = "CODE"
        Me.CODE.Width = 85
        '
        'DETAILS
        '
        Me.DETAILS.Text = "DETAILS"
        Me.DETAILS.Width = 216
        '
        'QTY_DEFECT
        '
        Me.QTY_DEFECT.Text = "QTY DEFECT"
        Me.QTY_DEFECT.Width = 108
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Location = New System.Drawing.Point(690, 230)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(105, 139)
        Me.btnUp.TabIndex = 57
        Me.btnUp.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Location = New System.Drawing.Point(683, 371)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(105, 131)
        Me.btnDown.TabIndex = 58
        Me.btnDown.TabStop = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(6, 512)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(194, 80)
        Me.btnBack.TabIndex = 59
        Me.btnBack.TabStop = False
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.Transparent
        Me.btnOk.Location = New System.Drawing.Point(600, 512)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(194, 80)
        Me.btnOk.TabIndex = 60
        Me.btnOk.TabStop = False
        '
        'backgroundNg
        '
        Me.backgroundNg.BackgroundImage = CType(resources.GetObject("backgroundNg.BackgroundImage"), System.Drawing.Image)
        Me.backgroundNg.Location = New System.Drawing.Point(0, 0)
        Me.backgroundNg.Name = "backgroundNg"
        Me.backgroundNg.Size = New System.Drawing.Size(800, 600)
        Me.backgroundNg.TabIndex = 61
        Me.backgroundNg.TabStop = False
        '
        'defectAdminAdjustdetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.defectAdmindetailselectadjustNC1
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.lvDefectact)
        Me.Controls.Add(Me.setCombobox)
        Me.Controls.Add(Me.lbLine)
        Me.Controls.Add(Me.backgroundNg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "defectAdminAdjustdetail"
        Me.Text = "1g"
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.backgroundNg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbLine As Label
	Friend WithEvents setCombobox As ComboBox
	Friend WithEvents lvDefectact As ListView
	Friend WithEvents NO As ColumnHeader
	Friend WithEvents PART_NO As ColumnHeader
	Friend WithEvents CODE As ColumnHeader
	Friend WithEvents DETAILS As ColumnHeader
	Friend WithEvents QTY_DEFECT As ColumnHeader
	Friend WithEvents btnUp As PictureBox
	Friend WithEvents btnDown As PictureBox
	Friend WithEvents btnBack As PictureBox
    Friend WithEvents btnOk As PictureBox
    Friend WithEvents backgroundNg As PictureBox
End Class
