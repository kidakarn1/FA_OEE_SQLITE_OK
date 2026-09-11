<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class defectDetailng
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
        Me.lvDefectdetails = New System.Windows.Forms.ListView()
        Me.NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PARTNO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TYPE = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CODE = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.DETAIL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.QTY = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.WI = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SEQ = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pwi_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnOk = New System.Windows.Forms.PictureBox()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.btnDown = New System.Windows.Forms.PictureBox()
        Me.btnUp = New System.Windows.Forms.PictureBox()
        Me.lbType = New System.Windows.Forms.Label()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lvDefectdetails
        '
        Me.lvDefectdetails.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvDefectdetails.AllowColumnReorder = True
        Me.lvDefectdetails.AllowDrop = True
        Me.lvDefectdetails.AutoArrange = False
        Me.lvDefectdetails.BackColor = System.Drawing.Color.White
        Me.lvDefectdetails.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvDefectdetails.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NO, Me.PARTNO, Me.TYPE, Me.CODE, Me.DETAIL, Me.QTY, Me.WI, Me.SEQ, Me.pwi_id})
        Me.lvDefectdetails.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvDefectdetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lvDefectdetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lvDefectdetails.FullRowSelect = True
        Me.lvDefectdetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvDefectdetails.HideSelection = False
        Me.lvDefectdetails.Location = New System.Drawing.Point(33, 194)
        Me.lvDefectdetails.MultiSelect = False
        Me.lvDefectdetails.Name = "lvDefectdetails"
        Me.lvDefectdetails.ShowGroups = False
        Me.lvDefectdetails.Size = New System.Drawing.Size(640, 305)
        Me.lvDefectdetails.TabIndex = 38
        Me.lvDefectdetails.UseCompatibleStateImageBehavior = False
        Me.lvDefectdetails.View = System.Windows.Forms.View.Details
        '
        'NO
        '
        Me.NO.Text = "NO"
        Me.NO.Width = 50
        '
        'PARTNO
        '
        Me.PARTNO.Text = "PART NO"
        Me.PARTNO.Width = 120
        '
        'TYPE
        '
        Me.TYPE.Text = "TYPE"
        Me.TYPE.Width = 67
        '
        'CODE
        '
        Me.CODE.Text = "CODE"
        Me.CODE.Width = 65
        '
        'DETAIL
        '
        Me.DETAIL.Text = "DETAIL"
        Me.DETAIL.Width = 165
        '
        'QTY
        '
        Me.QTY.Text = "QTY"
        Me.QTY.Width = 58
        '
        'WI
        '
        Me.WI.Width = 115
        '
        'SEQ
        '
        Me.SEQ.Width = 0
        '
        'pwi_id
        '
        Me.pwi_id.Width = 0
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.Transparent
        Me.btnOk.Location = New System.Drawing.Point(589, 515)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(190, 80)
        Me.btnOk.TabIndex = 49
        Me.btnOk.TabStop = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(26, 512)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(189, 80)
        Me.btnBack.TabIndex = 48
        Me.btnBack.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Location = New System.Drawing.Point(681, 365)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(97, 145)
        Me.btnDown.TabIndex = 47
        Me.btnDown.TabStop = False
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Location = New System.Drawing.Point(681, 179)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(97, 167)
        Me.btnUp.TabIndex = 46
        Me.btnUp.TabStop = False
        '
        'lbType
        '
        Me.lbType.AutoSize = True
        Me.lbType.BackColor = System.Drawing.Color.Transparent
        Me.lbType.Font = New System.Drawing.Font("Panton-Trial ExtraBold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbType.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.lbType.Location = New System.Drawing.Point(727, 108)
        Me.lbType.Name = "lbType"
        Me.lbType.Size = New System.Drawing.Size(51, 35)
        Me.lbType.TabIndex = 50
        Me.lbType.Text = "XX"
        '
        'defectDetailng
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.defectDetailadjustNew231
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.lbType)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.lvDefectdetails)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "defectDetailng"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "defectDetailng"
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvDefectdetails As ListView
    Friend WithEvents NO As ColumnHeader
    Friend WithEvents PARTNO As ColumnHeader
    Friend WithEvents TYPE As ColumnHeader
    Friend WithEvents CODE As ColumnHeader
    Friend WithEvents DETAIL As ColumnHeader
    Friend WithEvents QTY As ColumnHeader
    Friend WithEvents btnOk As PictureBox
    Friend WithEvents btnBack As PictureBox
    Friend WithEvents btnDown As PictureBox
    Friend WithEvents btnUp As PictureBox
    Friend WithEvents lbType As Label
    Friend WithEvents WI As ColumnHeader
    Friend WithEvents SEQ As ColumnHeader
    Friend WithEvents pwi_id As ColumnHeader
End Class
