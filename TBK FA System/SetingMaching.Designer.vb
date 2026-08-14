<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetingMaching
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SetingMaching))
        Me.StartLoss = New System.Windows.Forms.Label()
        Me.EndLoss = New System.Windows.Forms.Label()
        Me.LossCode = New System.Windows.Forms.Label()
        Me.pcLeader = New System.Windows.Forms.PictureBox()
        Me.EmpCodeLeader = New System.Windows.Forms.Label()
        Me.LossCD = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.pcLeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StartLoss
        '
        Me.StartLoss.AutoSize = True
        Me.StartLoss.BackColor = System.Drawing.Color.Transparent
        Me.StartLoss.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StartLoss.Location = New System.Drawing.Point(476, 312)
        Me.StartLoss.Name = "StartLoss"
        Me.StartLoss.Size = New System.Drawing.Size(256, 29)
        Me.StartLoss.TabIndex = 3
        Me.StartLoss.Text = "yyyy-mm-dd HH:ii:SS"
        '
        'EndLoss
        '
        Me.EndLoss.AutoSize = True
        Me.EndLoss.BackColor = System.Drawing.Color.Transparent
        Me.EndLoss.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold)
        Me.EndLoss.Location = New System.Drawing.Point(476, 388)
        Me.EndLoss.Name = "EndLoss"
        Me.EndLoss.Size = New System.Drawing.Size(256, 29)
        Me.EndLoss.TabIndex = 4
        Me.EndLoss.Text = "yyyy-mm-dd HH:ii:SS"
        '
        'LossCode
        '
        Me.LossCode.AutoSize = True
        Me.LossCode.BackColor = System.Drawing.Color.Transparent
        Me.LossCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LossCode.Location = New System.Drawing.Point(510, 241)
        Me.LossCode.Name = "LossCode"
        Me.LossCode.Size = New System.Drawing.Size(35, 33)
        Me.LossCode.TabIndex = 5
        Me.LossCode.Text = "K"
        '
        'pcLeader
        '
        Me.pcLeader.BackColor = System.Drawing.Color.White
        Me.pcLeader.Location = New System.Drawing.Point(76, 247)
        Me.pcLeader.Name = "pcLeader"
        Me.pcLeader.Size = New System.Drawing.Size(119, 118)
        Me.pcLeader.TabIndex = 6
        Me.pcLeader.TabStop = False
        '
        'EmpCodeLeader
        '
        Me.EmpCodeLeader.AutoSize = True
        Me.EmpCodeLeader.BackColor = System.Drawing.Color.Transparent
        Me.EmpCodeLeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.EmpCodeLeader.Location = New System.Drawing.Point(85, 391)
        Me.EmpCodeLeader.Name = "EmpCodeLeader"
        Me.EmpCodeLeader.Size = New System.Drawing.Size(100, 33)
        Me.EmpCodeLeader.TabIndex = 7
        Me.EmpCodeLeader.Text = "02033"
        '
        'LossCD
        '
        Me.LossCD.AutoSize = True
        Me.LossCD.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LossCD.Location = New System.Drawing.Point(624, 9)
        Me.LossCD.Name = "LossCD"
        Me.LossCD.Size = New System.Drawing.Size(125, 33)
        Me.LossCD.TabIndex = 10
        Me.LossCD.Text = "LossCD"
        Me.LossCD.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.TBK_FA_System.My.Resources.Resources.ok
        Me.PictureBox1.Location = New System.Drawing.Point(595, 506)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(193, 83)
        Me.PictureBox1.TabIndex = 11
        Me.PictureBox1.TabStop = False
        '
        'SetingMaching
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Coral
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.LossCD)
        Me.Controls.Add(Me.EmpCodeLeader)
        Me.Controls.Add(Me.pcLeader)
        Me.Controls.Add(Me.LossCode)
        Me.Controls.Add(Me.EndLoss)
        Me.Controls.Add(Me.StartLoss)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SetingMaching"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SetingMaching"
        CType(Me.pcLeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StartLoss As Label
    Friend WithEvents EndLoss As Label
    Friend WithEvents LossCode As Label
    Friend WithEvents pcLeader As PictureBox
    Friend WithEvents EmpCodeLeader As Label
    Friend WithEvents LossCD As Label
    Friend WithEvents PictureBox1 As PictureBox
End Class
