<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ShowSpcDetailDefect
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ShowSpcDetailDefect))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lvFG = New System.Windows.Forms.ListView()
        Me.lvCp = New System.Windows.Forms.ListView()
        Me.wi = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.partNO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.defectCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.type = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.qty = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnFGUp = New System.Windows.Forms.PictureBox()
        Me.btnFGDown = New System.Windows.Forms.PictureBox()
        Me.btnCPDOWN = New System.Windows.Forms.PictureBox()
        Me.btnCPUP = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnFGUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnFGDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCPDOWN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCPUP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Location = New System.Drawing.Point(12, 509)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(203, 79)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'lvFG
        '
        Me.lvFG.AllowColumnReorder = True
        Me.lvFG.AllowDrop = True
        Me.lvFG.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.lvFG.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!)
        Me.lvFG.FullRowSelect = True
        Me.lvFG.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvFG.HideSelection = False
        Me.lvFG.Location = New System.Drawing.Point(25, 166)
        Me.lvFG.Name = "lvFG"
        Me.lvFG.Size = New System.Drawing.Size(679, 124)
        Me.lvFG.TabIndex = 1
        Me.lvFG.UseCompatibleStateImageBehavior = False
        Me.lvFG.View = System.Windows.Forms.View.Details
        '
        'lvCp
        '
        Me.lvCp.AllowColumnReorder = True
        Me.lvCp.AllowDrop = True
        Me.lvCp.AutoArrange = False
        Me.lvCp.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.lvCp.BackgroundImageTiled = True
        Me.lvCp.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvCp.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.wi, Me.partNO, Me.defectCode, Me.type, Me.qty})
        Me.lvCp.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvCp.Font = New System.Drawing.Font("Catamaran", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lvCp.ForeColor = System.Drawing.Color.White
        Me.lvCp.FullRowSelect = True
        Me.lvCp.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvCp.HideSelection = False
        Me.lvCp.HoverSelection = True
        Me.lvCp.Location = New System.Drawing.Point(25, 375)
        Me.lvCp.MultiSelect = False
        Me.lvCp.Name = "lvCp"
        Me.lvCp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lvCp.ShowGroups = False
        Me.lvCp.Size = New System.Drawing.Size(679, 119)
        Me.lvCp.TabIndex = 65
        Me.lvCp.UseCompatibleStateImageBehavior = False
        Me.lvCp.View = System.Windows.Forms.View.Details
        '
        'wi
        '
        Me.wi.Text = "wi"
        Me.wi.Width = 125
        '
        'partNO
        '
        Me.partNO.Text = "CD"
        Me.partNO.Width = 185
        '
        'defectCode
        '
        Me.defectCode.Text = "Detail_TH"
        Me.defectCode.Width = 200
        '
        'type
        '
        Me.type.Width = 108
        '
        'qty
        '
        Me.qty.Width = 55
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Width = 125
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Width = 185
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Width = 200
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Width = 108
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Width = 55
        '
        'btnFGUp
        '
        Me.btnFGUp.BackgroundImage = CType(resources.GetObject("btnFGUp.BackgroundImage"), System.Drawing.Image)
        Me.btnFGUp.Location = New System.Drawing.Point(717, 118)
        Me.btnFGUp.Name = "btnFGUp"
        Me.btnFGUp.Size = New System.Drawing.Size(71, 92)
        Me.btnFGUp.TabIndex = 4638
        Me.btnFGUp.TabStop = False
        '
        'btnFGDown
        '
        Me.btnFGDown.BackgroundImage = CType(resources.GetObject("btnFGDown.BackgroundImage"), System.Drawing.Image)
        Me.btnFGDown.Location = New System.Drawing.Point(717, 207)
        Me.btnFGDown.Name = "btnFGDown"
        Me.btnFGDown.Size = New System.Drawing.Size(71, 92)
        Me.btnFGDown.TabIndex = 4639
        Me.btnFGDown.TabStop = False
        '
        'btnCPDOWN
        '
        Me.btnCPDOWN.BackgroundImage = CType(resources.GetObject("btnCPDOWN.BackgroundImage"), System.Drawing.Image)
        Me.btnCPDOWN.Location = New System.Drawing.Point(717, 415)
        Me.btnCPDOWN.Name = "btnCPDOWN"
        Me.btnCPDOWN.Size = New System.Drawing.Size(71, 92)
        Me.btnCPDOWN.TabIndex = 4641
        Me.btnCPDOWN.TabStop = False
        '
        'btnCPUP
        '
        Me.btnCPUP.BackgroundImage = CType(resources.GetObject("btnCPUP.BackgroundImage"), System.Drawing.Image)
        Me.btnCPUP.Location = New System.Drawing.Point(717, 326)
        Me.btnCPUP.Name = "btnCPUP"
        Me.btnCPUP.Size = New System.Drawing.Size(71, 92)
        Me.btnCPUP.TabIndex = 4640
        Me.btnCPUP.TabStop = False
        '
        'ShowSpcDetailDefect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.btnCPDOWN)
        Me.Controls.Add(Me.btnCPUP)
        Me.Controls.Add(Me.btnFGDown)
        Me.Controls.Add(Me.btnFGUp)
        Me.Controls.Add(Me.lvCp)
        Me.Controls.Add(Me.lvFG)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ShowSpcDetailDefect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ShowSpcDetailDefect"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnFGUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnFGDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCPDOWN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCPUP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lvFG As ListView
    Friend WithEvents lvCp As ListView
    Friend WithEvents wi As ColumnHeader
    Friend WithEvents partNO As ColumnHeader
    Friend WithEvents defectCode As ColumnHeader
    Friend WithEvents type As ColumnHeader
    Friend WithEvents qty As ColumnHeader
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents btnFGUp As PictureBox
    Friend WithEvents btnFGDown As PictureBox
    Friend WithEvents btnCPDOWN As PictureBox
    Friend WithEvents btnCPUP As PictureBox
End Class
