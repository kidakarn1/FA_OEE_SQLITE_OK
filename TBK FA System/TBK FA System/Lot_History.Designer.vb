<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Lot_History
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Lot_History))
        Me.pbBack = New System.Windows.Forms.PictureBox()
        Me.lvShowData = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnUp = New System.Windows.Forms.PictureBox()
        Me.btnDown = New System.Windows.Forms.PictureBox()
        CType(Me.pbBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbBack
        '
        Me.pbBack.BackColor = System.Drawing.Color.Transparent
        Me.pbBack.Location = New System.Drawing.Point(12, 490)
        Me.pbBack.Name = "pbBack"
        Me.pbBack.Size = New System.Drawing.Size(204, 113)
        Me.pbBack.TabIndex = 34
        Me.pbBack.TabStop = False
        '
        'lvShowData
        '
        Me.lvShowData.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvShowData.AllowColumnReorder = True
        Me.lvShowData.AllowDrop = True
        Me.lvShowData.AutoArrange = False
        Me.lvShowData.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.lvShowData.BackgroundImageTiled = True
        Me.lvShowData.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvShowData.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lvShowData.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvShowData.Font = New System.Drawing.Font("Catamaran", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvShowData.ForeColor = System.Drawing.Color.White
        Me.lvShowData.FullRowSelect = True
        Me.lvShowData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvShowData.HideSelection = False
        Me.lvShowData.Location = New System.Drawing.Point(20, 129)
        Me.lvShowData.MultiSelect = False
        Me.lvShowData.Name = "lvShowData"
        Me.lvShowData.ShowGroups = False
        Me.lvShowData.Size = New System.Drawing.Size(643, 355)
        Me.lvShowData.TabIndex = 36
        Me.lvShowData.UseCompatibleStateImageBehavior = False
        Me.lvShowData.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "No"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "QrCode"
        Me.ColumnHeader2.Width = 300
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "DateTime"
        Me.ColumnHeader3.Width = 280
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Location = New System.Drawing.Point(681, 63)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(108, 203)
        Me.btnUp.TabIndex = 4640
        Me.btnUp.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Location = New System.Drawing.Point(680, 271)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(108, 232)
        Me.btnDown.TabIndex = 4641
        Me.btnDown.TabStop = False
        '
        'Lot_History
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.lvShowData)
        Me.Controls.Add(Me.pbBack)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Lot_History"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lot_History"
        CType(Me.pbBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pbBack As PictureBox
    Friend WithEvents lvShowData As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents btnUp As PictureBox
    Friend WithEvents btnDown As PictureBox
End Class
