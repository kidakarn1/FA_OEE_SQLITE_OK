<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class defectSelecttype
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(defectSelecttype))
        Me.btnPartfg = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.PictureBox()
        Me.btnDown = New System.Windows.Forms.PictureBox()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.lvChildpart = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ITEMNAME = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.wis = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.seq = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pwi_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.mainChildPart = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.lbType = New System.Windows.Forms.Label()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPartfg
        '
        Me.btnPartfg.BackColor = System.Drawing.Color.White
        Me.btnPartfg.FlatAppearance.BorderSize = 0
        Me.btnPartfg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPartfg.Font = New System.Drawing.Font("Panton-Trial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPartfg.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnPartfg.Location = New System.Drawing.Point(42, 179)
        Me.btnPartfg.Margin = New System.Windows.Forms.Padding(2)
        Me.btnPartfg.Name = "btnPartfg"
        Me.btnPartfg.Size = New System.Drawing.Size(550, 41)
        Me.btnPartfg.TabIndex = 38
        Me.btnPartfg.Text = "XXXX"
        Me.btnPartfg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPartfg.UseVisualStyleBackColor = False
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Location = New System.Drawing.Point(682, 298)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(97, 103)
        Me.btnUp.TabIndex = 39
        Me.btnUp.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Location = New System.Drawing.Point(682, 408)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(97, 102)
        Me.btnDown.TabIndex = 40
        Me.btnDown.TabStop = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(27, 514)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(192, 78)
        Me.btnBack.TabIndex = 41
        Me.btnBack.TabStop = False
        '
        'lvChildpart
        '
        Me.lvChildpart.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvChildpart.BackColor = System.Drawing.Color.White
        Me.lvChildpart.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvChildpart.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ITEMNAME, Me.wis, Me.seq, Me.pwi_id, Me.mainChildPart})
        Me.lvChildpart.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvChildpart.Font = New System.Drawing.Font("Panton-Trial", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lvChildpart.ForeColor = System.Drawing.Color.Black
        Me.lvChildpart.FullRowSelect = True
        Me.lvChildpart.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvChildpart.HideSelection = False
        Me.lvChildpart.HotTracking = True
        Me.lvChildpart.HoverSelection = True
        Me.lvChildpart.LabelEdit = True
        Me.lvChildpart.Location = New System.Drawing.Point(38, 305)
        Me.lvChildpart.Name = "lvChildpart"
        Me.lvChildpart.Size = New System.Drawing.Size(629, 201)
        Me.lvChildpart.TabIndex = 46
        Me.lvChildpart.UseCompatibleStateImageBehavior = False
        Me.lvChildpart.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "NO"
        Me.ColumnHeader1.Width = 55
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "PART NO"
        Me.ColumnHeader2.Width = 165
        '
        'ITEMNAME
        '
        Me.ITEMNAME.Width = 262
        '
        'wis
        '
        Me.wis.Width = 140
        '
        'seq
        '
        Me.seq.Width = 1
        '
        'pwi_id
        '
        Me.pwi_id.Width = 1
        '
        'mainChildPart
        '
        Me.mainChildPart.Width = 0
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox4.Location = New System.Drawing.Point(589, 515)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(190, 77)
        Me.PictureBox4.TabIndex = 47
        Me.PictureBox4.TabStop = False
        '
        'lbType
        '
        Me.lbType.AutoSize = True
        Me.lbType.BackColor = System.Drawing.Color.Transparent
        Me.lbType.Font = New System.Drawing.Font("Panton-Trial ExtraBold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbType.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.lbType.Location = New System.Drawing.Point(8, 6)
        Me.lbType.Name = "lbType"
        Me.lbType.Size = New System.Drawing.Size(51, 35)
        Me.lbType.TabIndex = 52
        Me.lbType.Text = "XX"
        '
        'defectSelecttype
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.lbType)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.lvChildpart)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.btnPartfg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "defectSelecttype"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "qqq"
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnPartfg As Button
    Friend WithEvents btnUp As PictureBox
    Friend WithEvents btnDown As PictureBox
    Friend WithEvents btnBack As PictureBox
    Friend WithEvents lvChildpart As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents lbType As Label
    Friend WithEvents ITEMNAME As ColumnHeader
    Friend WithEvents wis As ColumnHeader
    Friend WithEvents seq As ColumnHeader
    Friend WithEvents pwi_id As ColumnHeader
    Friend WithEvents mainChildPart As ColumnHeader
End Class
