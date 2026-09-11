<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ManagePlan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ManagePlan))
        Me.pbQrCode = New System.Windows.Forms.PictureBox()
        Me.pbNext = New System.Windows.Forms.PictureBox()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.WI = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ITEM_CD = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.QTY = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        CType(Me.pbQrCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbNext, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbQrCode
        '
        Me.pbQrCode.BackColor = System.Drawing.Color.Transparent
        Me.pbQrCode.Location = New System.Drawing.Point(663, 4)
        Me.pbQrCode.Name = "pbQrCode"
        Me.pbQrCode.Size = New System.Drawing.Size(125, 115)
        Me.pbQrCode.TabIndex = 5
        Me.pbQrCode.TabStop = False
        '
        'pbNext
        '
        Me.pbNext.BackColor = System.Drawing.Color.Transparent
        Me.pbNext.Location = New System.Drawing.Point(588, 512)
        Me.pbNext.Name = "pbNext"
        Me.pbNext.Size = New System.Drawing.Size(200, 79)
        Me.pbNext.TabIndex = 6
        Me.pbNext.TabStop = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(12, 512)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(200, 79)
        Me.btnBack.TabIndex = 7
        Me.btnBack.TabStop = False
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.AllowDrop = True
        Me.ListView1.AutoArrange = False
        Me.ListView1.BackColor = System.Drawing.Color.White
        Me.ListView1.BackgroundImageTiled = True
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NO, Me.WI, Me.ITEM_CD, Me.QTY})
        Me.ListView1.Cursor = System.Windows.Forms.Cursors.Default
        Me.ListView1.Font = New System.Drawing.Font("Catamaran", 22.0!, System.Drawing.FontStyle.Bold)
        Me.ListView1.ForeColor = System.Drawing.Color.Black
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListView1.HideSelection = False
        Me.ListView1.HoverSelection = True
        Me.ListView1.Location = New System.Drawing.Point(21, 174)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ListView1.ShowGroups = False
        Me.ListView1.Size = New System.Drawing.Size(758, 334)
        Me.ListView1.TabIndex = 8
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'NO
        '
        Me.NO.Text = "NO"
        Me.NO.Width = 90
        '
        'WI
        '
        Me.WI.Text = "WI"
        Me.WI.Width = 225
        '
        'ITEM_CD
        '
        Me.ITEM_CD.Text = "ITEM CD"
        Me.ITEM_CD.Width = 335
        '
        'QTY
        '
        Me.QTY.Text = "QTY"
        Me.QTY.Width = 90
        '
        'ManagePlan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.pbNext)
        Me.Controls.Add(Me.pbQrCode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ManagePlan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ManagePlan"
        CType(Me.pbQrCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbNext, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pbQrCode As PictureBox
    Friend WithEvents pbNext As PictureBox
    Friend WithEvents btnBack As PictureBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents NO As ColumnHeader
    Friend WithEvents WI As ColumnHeader
    Friend WithEvents ITEM_CD As ColumnHeader
    Friend WithEvents QTY As ColumnHeader
End Class
