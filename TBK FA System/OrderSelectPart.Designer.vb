<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderSelectPart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OrderSelectPart))
        Me.lvOrderPartNo = New System.Windows.Forms.ListView()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.btnSelect = New System.Windows.Forms.PictureBox()
        Me.btnUp = New System.Windows.Forms.PictureBox()
        Me.btnDown = New System.Windows.Forms.PictureBox()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lvOrderPartNo
        '
        Me.lvOrderPartNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!)
        Me.lvOrderPartNo.HideSelection = False
        Me.lvOrderPartNo.Location = New System.Drawing.Point(15, 136)
        Me.lvOrderPartNo.Name = "lvOrderPartNo"
        Me.lvOrderPartNo.Size = New System.Drawing.Size(698, 341)
        Me.lvOrderPartNo.TabIndex = 1
        Me.lvOrderPartNo.UseCompatibleStateImageBehavior = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(15, 495)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(198, 79)
        Me.btnBack.TabIndex = 2
        Me.btnBack.TabStop = False
        '
        'btnSelect
        '
        Me.btnSelect.BackColor = System.Drawing.Color.Transparent
        Me.btnSelect.Location = New System.Drawing.Point(590, 494)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(198, 79)
        Me.btnSelect.TabIndex = 3
        Me.btnSelect.TabStop = False
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Location = New System.Drawing.Point(715, 89)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(78, 189)
        Me.btnUp.TabIndex = 4
        Me.btnUp.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Location = New System.Drawing.Point(716, 283)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(78, 205)
        Me.btnDown.TabIndex = 5
        Me.btnDown.TabStop = False
        '
        'OrderSelectPart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.lvOrderPartNo)
        Me.Controls.Add(Me.btnBack)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "OrderSelectPart"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OrderSelectPart"
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lvOrderPartNo As ListView
    Friend WithEvents btnBack As PictureBox
    Friend WithEvents btnSelect As PictureBox
    Friend WithEvents btnUp As PictureBox
    Friend WithEvents btnDown As PictureBox
End Class
