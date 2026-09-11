<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class defectSelectSupplier
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(defectSelectSupplier))
        Me.lvDefectact = New System.Windows.Forms.ListView()
        Me.Supcd = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Supcode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.supname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.btnUp = New System.Windows.Forms.PictureBox()
        Me.btnDown = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lvDefectact
        '
        Me.lvDefectact.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvDefectact.AllowColumnReorder = True
        Me.lvDefectact.AllowDrop = True
        Me.lvDefectact.AutoArrange = False
        Me.lvDefectact.BackColor = System.Drawing.Color.White
        Me.lvDefectact.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvDefectact.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Supcd, Me.Supcode, Me.supname})
        Me.lvDefectact.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvDefectact.Font = New System.Drawing.Font("Panton-Trial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvDefectact.ForeColor = System.Drawing.Color.Black
        Me.lvDefectact.FullRowSelect = True
        Me.lvDefectact.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvDefectact.HideSelection = False
        Me.lvDefectact.Location = New System.Drawing.Point(38, 162)
        Me.lvDefectact.MultiSelect = False
        Me.lvDefectact.Name = "lvDefectact"
        Me.lvDefectact.ShowGroups = False
        Me.lvDefectact.Size = New System.Drawing.Size(631, 339)
        Me.lvDefectact.TabIndex = 57
        Me.lvDefectact.UseCompatibleStateImageBehavior = False
        Me.lvDefectact.View = System.Windows.Forms.View.Details
        '
        'Supcd
        '
        Me.Supcd.Text = "NO"
        '
        'Supcode
        '
        Me.Supcode.Text = "PART NO"
        Me.Supcode.Width = 281
        '
        'supname
        '
        Me.supname.Width = 290
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Location = New System.Drawing.Point(19, 513)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(201, 80)
        Me.PictureBox1.TabIndex = 61
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Location = New System.Drawing.Point(586, 516)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(200, 75)
        Me.PictureBox2.TabIndex = 62
        Me.PictureBox2.TabStop = False
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Location = New System.Drawing.Point(681, 154)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(98, 153)
        Me.btnUp.TabIndex = 63
        Me.btnUp.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Location = New System.Drawing.Point(683, 364)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(96, 142)
        Me.btnDown.TabIndex = 64
        Me.btnDown.TabStop = False
        '
        'defectSelectSupplier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lvDefectact)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "defectSelectSupplier"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "defectSelectSupplier"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lvDefectact As ListView
    Friend WithEvents Supcd As ColumnHeader
    Friend WithEvents Supcode As ColumnHeader
    Friend WithEvents supname As ColumnHeader
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents btnUp As PictureBox
    Friend WithEvents btnDown As PictureBox
End Class
