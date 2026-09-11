<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class defectAdmindetailnc
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(defectAdmindetailnc))
        Me.lbLine = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.btnOk = New System.Windows.Forms.PictureBox()
        Me.btnUp = New System.Windows.Forms.PictureBox()
        Me.btnDown = New System.Windows.Forms.PictureBox()
        Me.lvDefectact = New System.Windows.Forms.ListView()
        Me.PART_NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.WI = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SHIFT = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SEQ = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LOT = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.QTY = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GOOD = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.DEFECT = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TOTAL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PWI_ID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbLine
        '
        Me.lbLine.AutoSize = True
        Me.lbLine.BackColor = System.Drawing.Color.Transparent
        Me.lbLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.25!, System.Drawing.FontStyle.Bold)
        Me.lbLine.ForeColor = System.Drawing.Color.White
        Me.lbLine.Location = New System.Drawing.Point(105, 84)
        Me.lbLine.Name = "lbLine"
        Me.lbLine.Size = New System.Drawing.Size(83, 25)
        Me.lbLine.TabIndex = 4
        Me.lbLine.Text = "Label1"
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(7, 509)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(193, 87)
        Me.btnBack.TabIndex = 45
        Me.btnBack.TabStop = False
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.Transparent
        Me.btnOk.Location = New System.Drawing.Point(600, 509)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(193, 87)
        Me.btnOk.TabIndex = 46
        Me.btnOk.TabStop = False
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Location = New System.Drawing.Point(691, 174)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(105, 164)
        Me.btnUp.TabIndex = 47
        Me.btnUp.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Location = New System.Drawing.Point(691, 340)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(105, 158)
        Me.btnDown.TabIndex = 48
        Me.btnDown.TabStop = False
        '
        'lvDefectact
        '
        Me.lvDefectact.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvDefectact.AllowColumnReorder = True
        Me.lvDefectact.AllowDrop = True
        Me.lvDefectact.AutoArrange = False
        Me.lvDefectact.BackColor = System.Drawing.Color.Peru
        Me.lvDefectact.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvDefectact.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.PART_NO, Me.WI, Me.SHIFT, Me.SEQ, Me.LOT, Me.QTY, Me.GOOD, Me.DEFECT, Me.TOTAL, Me.PWI_ID})
        Me.lvDefectact.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvDefectact.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lvDefectact.ForeColor = System.Drawing.Color.White
        Me.lvDefectact.FullRowSelect = True
        Me.lvDefectact.GridLines = True
        Me.lvDefectact.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvDefectact.HideSelection = False
        Me.lvDefectact.HoverSelection = True
        Me.lvDefectact.LabelWrap = False
        Me.lvDefectact.Location = New System.Drawing.Point(18, 181)
        Me.lvDefectact.MultiSelect = False
        Me.lvDefectact.Name = "lvDefectact"
        Me.lvDefectact.ShowGroups = False
        Me.lvDefectact.Size = New System.Drawing.Size(662, 320)
        Me.lvDefectact.TabIndex = 49
        Me.lvDefectact.UseCompatibleStateImageBehavior = False
        Me.lvDefectact.View = System.Windows.Forms.View.Details
        '
        'PART_NO
        '
        Me.PART_NO.Text = "PART NO"
        Me.PART_NO.Width = 140
        '
        'WI
        '
        Me.WI.Text = "WI"
        Me.WI.Width = 115
        '
        'SHIFT
        '
        Me.SHIFT.Text = "S"
        Me.SHIFT.Width = 57
        '
        'SEQ
        '
        Me.SEQ.Text = "SEQ"
        Me.SEQ.Width = 58
        '
        'LOT
        '
        Me.LOT.Text = "LOT"
        Me.LOT.Width = 65
        '
        'QTY
        '
        Me.QTY.Text = "QTY"
        Me.QTY.Width = 57
        '
        'GOOD
        '
        Me.GOOD.Text = "GOOD"
        Me.GOOD.Width = 53
        '
        'DEFECT
        '
        Me.DEFECT.Text = "NC"
        Me.DEFECT.Width = 48
        '
        'TOTAL
        '
        Me.TOTAL.Text = "TOTAL"
        Me.TOTAL.Width = 63
        '
        'PWI_ID
        '
        Me.PWI_ID.Width = 0
        '
        'defectAdmindetailnc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.lvDefectact)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.lbLine)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "defectAdmindetailnc"
        Me.Text = "w"
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbLine As Label
    Friend WithEvents btnBack As PictureBox
    Friend WithEvents btnOk As PictureBox
    Friend WithEvents btnUp As PictureBox
    Friend WithEvents btnDown As PictureBox
    Friend WithEvents PART_NO As ColumnHeader
    Friend WithEvents WI As ColumnHeader
    Friend WithEvents SHIFT As ColumnHeader
    Friend WithEvents SEQ As ColumnHeader
    Friend WithEvents LOT As ColumnHeader
    Friend WithEvents QTY As ColumnHeader
    Friend WithEvents GOOD As ColumnHeader
    Friend WithEvents DEFECT As ColumnHeader
    Friend WithEvents TOTAL As ColumnHeader
    Friend WithEvents lvDefectact As ListView
    Friend WithEvents PWI_ID As ColumnHeader
End Class
