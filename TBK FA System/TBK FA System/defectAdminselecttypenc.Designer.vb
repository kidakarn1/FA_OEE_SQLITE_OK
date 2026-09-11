<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class defectAdminselecttypenc
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(defectAdminselecttypenc))
        Me.lvChildpart = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnUp = New System.Windows.Forms.PictureBox()
        Me.btnDown = New System.Windows.Forms.PictureBox()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.lbType = New System.Windows.Forms.Label()
        Me.btnPartfg = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lvChildpart
        '
        Me.lvChildpart.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvChildpart.AllowColumnReorder = True
        Me.lvChildpart.AllowDrop = True
        Me.lvChildpart.AutoArrange = False
        Me.lvChildpart.BackColor = System.Drawing.Color.Peru
        Me.lvChildpart.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvChildpart.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lvChildpart.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvChildpart.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lvChildpart.ForeColor = System.Drawing.Color.Black
        Me.lvChildpart.FullRowSelect = True
        Me.lvChildpart.GridLines = True
        Me.lvChildpart.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvChildpart.HideSelection = False
        Me.lvChildpart.Location = New System.Drawing.Point(21, 318)
        Me.lvChildpart.MultiSelect = False
        Me.lvChildpart.Name = "lvChildpart"
        Me.lvChildpart.ShowGroups = False
        Me.lvChildpart.Size = New System.Drawing.Size(661, 183)
        Me.lvChildpart.TabIndex = 45
        Me.lvChildpart.UseCompatibleStateImageBehavior = False
        Me.lvChildpart.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "NO"
        Me.ColumnHeader3.Width = 61
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "PART NO"
        Me.ColumnHeader4.Width = 598
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Location = New System.Drawing.Point(690, 305)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(100, 99)
        Me.btnUp.TabIndex = 49
        Me.btnUp.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Location = New System.Drawing.Point(690, 408)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(100, 99)
        Me.btnDown.TabIndex = 50
        Me.btnDown.TabStop = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(8, 509)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(193, 87)
        Me.btnBack.TabIndex = 61
        Me.btnBack.TabStop = False
        '
        'lbType
        '
        Me.lbType.AutoSize = True
        Me.lbType.Location = New System.Drawing.Point(34, 9)
        Me.lbType.Name = "lbType"
        Me.lbType.Size = New System.Drawing.Size(39, 13)
        Me.lbType.TabIndex = 62
        Me.lbType.Text = "Label1"
        Me.lbType.Visible = False
        '
        'btnPartfg
        '
        Me.btnPartfg.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.btnPartfg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnPartfg.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.btnPartfg.FlatAppearance.BorderSize = 0
        Me.btnPartfg.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.btnPartfg.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.btnPartfg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPartfg.Font = New System.Drawing.Font("Microsoft Sans Serif", 28.25!)
        Me.btnPartfg.ForeColor = System.Drawing.Color.White
        Me.btnPartfg.Location = New System.Drawing.Point(25, 177)
        Me.btnPartfg.Name = "btnPartfg"
        Me.btnPartfg.Size = New System.Drawing.Size(749, 52)
        Me.btnPartfg.TabIndex = 64
        Me.btnPartfg.Text = "XXXXXXXXXXXXX"
        Me.btnPartfg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPartfg.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Location = New System.Drawing.Point(595, 513)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(195, 83)
        Me.PictureBox1.TabIndex = 66
        Me.PictureBox1.TabStop = False
        '
        'defectAdminselecttypenc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnPartfg)
        Me.Controls.Add(Me.lbType)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.lvChildpart)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "defectAdminselecttypenc"
        Me.Text = resources.GetString("$this.Text")
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvChildpart As ListView
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents btnUp As PictureBox
    Friend WithEvents btnDown As PictureBox
    Friend WithEvents btnBack As PictureBox
    Friend WithEvents lbType As Label
    Friend WithEvents btnPartfg As Button
    Friend WithEvents PictureBox1 As PictureBox
End Class
