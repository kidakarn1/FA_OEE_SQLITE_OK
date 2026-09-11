<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StopMenu
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
        Me.components = New System.ComponentModel.Container()
        Me.btnBreakTime = New System.Windows.Forms.Button()
        Me.btnContinue = New System.Windows.Forms.Button()
        Me.TimerLossBT = New System.Windows.Forms.Timer(Me.components)
        Me.test_time_loss_time = New System.Windows.Forms.Label()
        Me.lbLossCode = New System.Windows.Forms.Label()
        Me.lbEndCount = New System.Windows.Forms.Label()
        Me.lbStartCount = New System.Windows.Forms.Label()
        Me.lbLineCode = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PanelShowLoss = New System.Windows.Forms.Panel()
        Me.lock = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnBreakTime
        '
        Me.btnBreakTime.BackColor = System.Drawing.Color.Transparent
        Me.btnBreakTime.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.IconBreakTime
        Me.btnBreakTime.FlatAppearance.BorderSize = 0
        Me.btnBreakTime.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBreakTime.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBreakTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBreakTime.Font = New System.Drawing.Font("Arial Rounded MT Bold", 48.0!)
        Me.btnBreakTime.ForeColor = System.Drawing.SystemColors.Control
        Me.btnBreakTime.Location = New System.Drawing.Point(466, 293)
        Me.btnBreakTime.Name = "btnBreakTime"
        Me.btnBreakTime.Size = New System.Drawing.Size(319, 280)
        Me.btnBreakTime.TabIndex = 8
        Me.btnBreakTime.UseVisualStyleBackColor = False
        '
        'btnContinue
        '
        Me.btnContinue.BackColor = System.Drawing.Color.Transparent
        Me.btnContinue.FlatAppearance.BorderSize = 0
        Me.btnContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnContinue.Font = New System.Drawing.Font("Arial Rounded MT Bold", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinue.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.btnContinue.Location = New System.Drawing.Point(469, 13)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(312, 279)
        Me.btnContinue.TabIndex = 7
        Me.btnContinue.UseVisualStyleBackColor = False
        '
        'TimerLossBT
        '
        Me.TimerLossBT.Interval = 1000
        '
        'test_time_loss_time
        '
        Me.test_time_loss_time.AutoSize = True
        Me.test_time_loss_time.BackColor = System.Drawing.Color.Black
        Me.test_time_loss_time.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.test_time_loss_time.ForeColor = System.Drawing.Color.White
        Me.test_time_loss_time.Location = New System.Drawing.Point(274, 378)
        Me.test_time_loss_time.Name = "test_time_loss_time"
        Me.test_time_loss_time.Size = New System.Drawing.Size(180, 25)
        Me.test_time_loss_time.TabIndex = 13
        Me.test_time_loss_time.Text = "XXXXXXXXXXXX"
        Me.test_time_loss_time.Visible = False
        '
        'lbLossCode
        '
        Me.lbLossCode.AutoSize = True
        Me.lbLossCode.BackColor = System.Drawing.Color.Transparent
        Me.lbLossCode.Font = New System.Drawing.Font("Catamaran", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLossCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.lbLossCode.Location = New System.Drawing.Point(228, 412)
        Me.lbLossCode.Name = "lbLossCode"
        Me.lbLossCode.Size = New System.Drawing.Size(167, 51)
        Me.lbLossCode.TabIndex = 12
        Me.lbLossCode.Text = "XXXXX"
        '
        'lbEndCount
        '
        Me.lbEndCount.AutoSize = True
        Me.lbEndCount.BackColor = System.Drawing.Color.Transparent
        Me.lbEndCount.Font = New System.Drawing.Font("Catamaran", 29.25!, System.Drawing.FontStyle.Bold)
        Me.lbEndCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.lbEndCount.Location = New System.Drawing.Point(229, 518)
        Me.lbEndCount.Name = "lbEndCount"
        Me.lbEndCount.Size = New System.Drawing.Size(189, 43)
        Me.lbEndCount.TabIndex = 11
        Me.lbEndCount.Text = "XX:XX:XX"
        '
        'lbStartCount
        '
        Me.lbStartCount.AutoSize = True
        Me.lbStartCount.BackColor = System.Drawing.Color.Transparent
        Me.lbStartCount.Font = New System.Drawing.Font("Catamaran", 25.25!, System.Drawing.FontStyle.Bold)
        Me.lbStartCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.lbStartCount.Location = New System.Drawing.Point(231, 472)
        Me.lbStartCount.Name = "lbStartCount"
        Me.lbStartCount.Size = New System.Drawing.Size(165, 37)
        Me.lbStartCount.TabIndex = 9
        Me.lbStartCount.Text = "XX:XX:XX"
        '
        'lbLineCode
        '
        Me.lbLineCode.AutoSize = True
        Me.lbLineCode.BackColor = System.Drawing.Color.Transparent
        Me.lbLineCode.Font = New System.Drawing.Font("Catamaran", 31.75!, System.Drawing.FontStyle.Bold)
        Me.lbLineCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbLineCode.Location = New System.Drawing.Point(150, 313)
        Me.lbLineCode.Name = "lbLineCode"
        Me.lbLineCode.Size = New System.Drawing.Size(182, 46)
        Me.lbLineCode.TabIndex = 10
        Me.lbLineCode.Text = "Label5"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.TBK_FA_System.My.Resources.Resources.sw
        Me.PictureBox1.Location = New System.Drawing.Point(40, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(383, 304)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 11
        Me.PictureBox1.TabStop = False
        '
        'PanelShowLoss
        '
        Me.PanelShowLoss.BackColor = System.Drawing.Color.Transparent
        Me.PanelShowLoss.Location = New System.Drawing.Point(40, 247)
        Me.PanelShowLoss.Name = "PanelShowLoss"
        Me.PanelShowLoss.Size = New System.Drawing.Size(244, 201)
        Me.PanelShowLoss.TabIndex = 14
        '
        'lock
        '
        Me.lock.BackColor = System.Drawing.Color.Transparent
        Me.lock.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.BLOCK
        Me.lock.FlatAppearance.BorderSize = 0
        Me.lock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.lock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.lock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lock.Font = New System.Drawing.Font("Arial Rounded MT Bold", 48.0!)
        Me.lock.ForeColor = System.Drawing.SystemColors.Control
        Me.lock.Location = New System.Drawing.Point(469, 293)
        Me.lock.Name = "lock"
        Me.lock.Size = New System.Drawing.Size(319, 280)
        Me.lock.TabIndex = 15
        Me.lock.UseVisualStyleBackColor = False
        Me.lock.Visible = False
        '
        'StopMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.breaktime3
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.lbEndCount)
        Me.Controls.Add(Me.lock)
        Me.Controls.Add(Me.test_time_loss_time)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lbStartCount)
        Me.Controls.Add(Me.lbLossCode)
        Me.Controls.Add(Me.lbLineCode)
        Me.Controls.Add(Me.btnBreakTime)
        Me.Controls.Add(Me.btnContinue)
        Me.Controls.Add(Me.PanelShowLoss)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "StopMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "StopMenu"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnBreakTime As Button
    Friend WithEvents btnContinue As Button
    Friend WithEvents TimerLossBT As Timer
    Friend WithEvents test_time_loss_time As Label
    Friend WithEvents lbLossCode As Label
    Friend WithEvents lbEndCount As Label
    Friend WithEvents lbStartCount As Label
    Friend WithEvents lbLineCode As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PanelShowLoss As Panel
    Friend WithEvents lock As Button
End Class
