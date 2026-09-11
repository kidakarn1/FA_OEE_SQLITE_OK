<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SetStartTime
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
        Me.lb_showSetTime = New System.Windows.Forms.Label()
        Me.tbHour = New System.Windows.Forms.TextBox()
        Me.Down_time = New System.Windows.Forms.Label()
        Me.Crr_time = New System.Windows.Forms.Label()
        Me.tbMin = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lb_dateCrr = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lb_showSetTime
        '
        Me.lb_showSetTime.AutoSize = True
        Me.lb_showSetTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lb_showSetTime.Location = New System.Drawing.Point(116, 208)
        Me.lb_showSetTime.Name = "lb_showSetTime"
        Me.lb_showSetTime.Size = New System.Drawing.Size(0, 29)
        Me.lb_showSetTime.TabIndex = 0
        '
        'tbHour
        '
        Me.tbHour.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.tbHour.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbHour.Enabled = False
        Me.tbHour.Font = New System.Drawing.Font("Panton-Trial ExtraBold", 20.0!, System.Drawing.FontStyle.Bold)
        Me.tbHour.ForeColor = System.Drawing.Color.Transparent
        Me.tbHour.Location = New System.Drawing.Point(408, 370)
        Me.tbHour.Name = "tbHour"
        Me.tbHour.Size = New System.Drawing.Size(78, 34)
        Me.tbHour.TabIndex = 1
        Me.tbHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Down_time
        '
        Me.Down_time.AutoSize = True
        Me.Down_time.BackColor = System.Drawing.Color.Transparent
        Me.Down_time.Font = New System.Drawing.Font("Panton-Trial ExtraBold", 20.0!, System.Drawing.FontStyle.Bold)
        Me.Down_time.ForeColor = System.Drawing.Color.White
        Me.Down_time.Location = New System.Drawing.Point(407, 141)
        Me.Down_time.Name = "Down_time"
        Me.Down_time.Size = New System.Drawing.Size(139, 35)
        Me.Down_time.TabIndex = 5
        Me.Down_time.Text = "XX:XX:XX"
        '
        'Crr_time
        '
        Me.Crr_time.AutoSize = True
        Me.Crr_time.BackColor = System.Drawing.Color.Transparent
        Me.Crr_time.Font = New System.Drawing.Font("Panton-Trial ExtraBold", 20.0!, System.Drawing.FontStyle.Bold)
        Me.Crr_time.ForeColor = System.Drawing.Color.White
        Me.Crr_time.Location = New System.Drawing.Point(577, 256)
        Me.Crr_time.Name = "Crr_time"
        Me.Crr_time.Size = New System.Drawing.Size(139, 35)
        Me.Crr_time.TabIndex = 7
        Me.Crr_time.Text = "XX:XX:XX"
        '
        'tbMin
        '
        Me.tbMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.tbMin.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbMin.Enabled = False
        Me.tbMin.Font = New System.Drawing.Font("Panton-Trial ExtraBold", 20.0!, System.Drawing.FontStyle.Bold)
        Me.tbMin.ForeColor = System.Drawing.Color.White
        Me.tbMin.Location = New System.Drawing.Point(511, 371)
        Me.tbMin.Name = "tbMin"
        Me.tbMin.Size = New System.Drawing.Size(69, 34)
        Me.tbMin.TabIndex = 9
        Me.tbMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.Transparent
        Me.btnOK.Location = New System.Drawing.Point(325, 493)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(199, 95)
        Me.btnOK.TabIndex = 11
        Me.btnOK.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.point2
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Image = Global.TBK_FA_System.My.Resources.Resources.giphy
        Me.PictureBox1.Location = New System.Drawing.Point(9, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(120, 94)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'lb_dateCrr
        '
        Me.lb_dateCrr.AutoSize = True
        Me.lb_dateCrr.BackColor = System.Drawing.Color.Transparent
        Me.lb_dateCrr.Font = New System.Drawing.Font("Panton-Trial ExtraBold", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lb_dateCrr.ForeColor = System.Drawing.Color.White
        Me.lb_dateCrr.Location = New System.Drawing.Point(406, 256)
        Me.lb_dateCrr.Name = "lb_dateCrr"
        Me.lb_dateCrr.Size = New System.Drawing.Size(187, 35)
        Me.lb_dateCrr.TabIndex = 14
        Me.lb_dateCrr.Text = "YYYY-MM-DD"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PictureBox2.Location = New System.Drawing.Point(583, 320)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(78, 110)
        Me.PictureBox2.TabIndex = 15
        Me.PictureBox2.TabStop = False
        '
        'SetStartTime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.ErrorTImeOld
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.lb_dateCrr)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.tbMin)
        Me.Controls.Add(Me.Crr_time)
        Me.Controls.Add(Me.Down_time)
        Me.Controls.Add(Me.tbHour)
        Me.Controls.Add(Me.lb_showSetTime)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SetStartTime"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wwww"
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lb_showSetTime As Label
    Friend WithEvents tbHour As TextBox
    Friend WithEvents Down_time As Label
    Friend WithEvents Crr_time As Label
    Friend WithEvents tbMin As TextBox
    Friend WithEvents btnOK As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lb_dateCrr As Label
    Friend WithEvents PictureBox2 As PictureBox
End Class
