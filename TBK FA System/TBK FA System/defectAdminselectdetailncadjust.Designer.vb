<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class defectAdminselectdetailncadjust
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(defectAdminselectdetailncadjust))
        Me.lvAction = New System.Windows.Forms.ListView()
        Me.PART_NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.WI = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SEQ = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LOT_NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pwi_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lbLine = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.PictureBox()
        Me.btnUp = New System.Windows.Forms.PictureBox()
        Me.btnDown = New System.Windows.Forms.PictureBox()
        Me.backgroundNg = New System.Windows.Forms.PictureBox()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Button1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.backgroundNg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lvAction
        '
        Me.lvAction.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvAction.AllowColumnReorder = True
        Me.lvAction.AllowDrop = True
        Me.lvAction.AutoArrange = False
        Me.lvAction.BackColor = System.Drawing.Color.Peru
        Me.lvAction.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvAction.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.PART_NO, Me.WI, Me.SEQ, Me.LOT_NO, Me.pwi_id})
        Me.lvAction.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvAction.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lvAction.ForeColor = System.Drawing.Color.Black
        Me.lvAction.FullRowSelect = True
        Me.lvAction.GridLines = True
        Me.lvAction.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvAction.HideSelection = False
        Me.lvAction.Location = New System.Drawing.Point(20, 180)
        Me.lvAction.MultiSelect = False
        Me.lvAction.Name = "lvAction"
        Me.lvAction.ShowGroups = False
        Me.lvAction.Size = New System.Drawing.Size(659, 323)
        Me.lvAction.TabIndex = 51
        Me.lvAction.UseCompatibleStateImageBehavior = False
        Me.lvAction.View = System.Windows.Forms.View.Details
        '
        'PART_NO
        '
        Me.PART_NO.Text = "PART NO"
        Me.PART_NO.Width = 247
        '
        'WI
        '
        Me.WI.Text = "WI"
        Me.WI.Width = 228
        '
        'SEQ
        '
        Me.SEQ.Text = "SEQ"
        Me.SEQ.Width = 76
        '
        'LOT_NO
        '
        Me.LOT_NO.Text = "LOT NO"
        Me.LOT_NO.Width = 107
        '
        'pwi_id
        '
        Me.pwi_id.Width = 0
        '
        'lbLine
        '
        Me.lbLine.AutoSize = True
        Me.lbLine.BackColor = System.Drawing.Color.Transparent
        Me.lbLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLine.ForeColor = System.Drawing.Color.Transparent
        Me.lbLine.Location = New System.Drawing.Point(107, 82)
        Me.lbLine.Name = "lbLine"
        Me.lbLine.Size = New System.Drawing.Size(77, 25)
        Me.lbLine.TabIndex = 4
        Me.lbLine.Text = "Label1"
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(6, 509)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(194, 86)
        Me.btnBack.TabIndex = 52
        Me.btnBack.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.Location = New System.Drawing.Point(600, 509)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(194, 86)
        Me.Button1.TabIndex = 53
        Me.Button1.TabStop = False
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Location = New System.Drawing.Point(693, 174)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(98, 163)
        Me.btnUp.TabIndex = 54
        Me.btnUp.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Location = New System.Drawing.Point(693, 340)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(98, 158)
        Me.btnDown.TabIndex = 55
        Me.btnDown.TabStop = False
        '
        'backgroundNg
        '
        Me.backgroundNg.BackgroundImage = CType(resources.GetObject("backgroundNg.BackgroundImage"), System.Drawing.Image)
        Me.backgroundNg.Location = New System.Drawing.Point(0, 0)
        Me.backgroundNg.Name = "backgroundNg"
        Me.backgroundNg.Size = New System.Drawing.Size(800, 600)
        Me.backgroundNg.TabIndex = 56
        Me.backgroundNg.TabStop = False
        '
        'defectAdminselectdetailncadjust
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.defectAdmindetailadjustNC1
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.lvAction)
        Me.Controls.Add(Me.lbLine)
        Me.Controls.Add(Me.backgroundNg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "defectAdminselectdetailncadjust"
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Button1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.backgroundNg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lvAction As ListView
	Friend WithEvents PART_NO As ColumnHeader
	Friend WithEvents WI As ColumnHeader
	Friend WithEvents SEQ As ColumnHeader
	Friend WithEvents LOT_NO As ColumnHeader
	Friend WithEvents lbLine As Label
	Friend WithEvents btnBack As PictureBox
	Friend WithEvents Button1 As PictureBox
	Friend WithEvents btnUp As PictureBox
    Friend WithEvents btnDown As PictureBox
    Friend WithEvents backgroundNg As PictureBox
    Friend WithEvents pwi_id As ColumnHeader
End Class
