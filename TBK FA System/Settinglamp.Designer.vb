<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Settinglamp
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Settinglamp))
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbLoss = New System.Windows.Forms.ComboBox()
        Me.cbStop = New System.Windows.Forms.ComboBox()
        Me.cbRun = New System.Windows.Forms.ComboBox()
        Me.lbLoss = New System.Windows.Forms.Label()
        Me.lbStop = New System.Windows.Forms.Label()
        Me.lbRun = New System.Windows.Forms.Label()
        Me.lbNC = New System.Windows.Forms.Label()
        Me.lbNCMax = New System.Windows.Forms.Label()
        Me.lbNG = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(195, 498)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(155, 62)
        Me.TextBox2.TabIndex = 52
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(357, 497)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(142, 62)
        Me.TextBox1.TabIndex = 51
        '
        'Button9
        '
        Me.Button9.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button9.Location = New System.Drawing.Point(505, 490)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(114, 37)
        Me.Button9.TabIndex = 50
        Me.Button9.Text = "STOP LAMP"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button10.Location = New System.Drawing.Point(296, 490)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(189, 100)
        Me.Button10.TabIndex = 53
        Me.Button10.Text = "RESET"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.Location = New System.Drawing.Point(675, 254)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(89, 45)
        Me.Button8.TabIndex = 49
        Me.Button8.Text = "TEST LAMP"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.Location = New System.Drawing.Point(675, 191)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(89, 45)
        Me.Button7.TabIndex = 48
        Me.Button7.Text = "TEST LAMP"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.Location = New System.Drawing.Point(675, 134)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(89, 45)
        Me.Button6.TabIndex = 47
        Me.Button6.Text = "TEST LAMP"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(208, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 39)
        Me.Label1.TabIndex = 41
        '
        'cbLoss
        '
        Me.cbLoss.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!)
        Me.cbLoss.FormattingEnabled = True
        Me.cbLoss.Location = New System.Drawing.Point(167, 255)
        Me.cbLoss.Name = "cbLoss"
        Me.cbLoss.Size = New System.Drawing.Size(502, 46)
        Me.cbLoss.TabIndex = 38
        '
        'cbStop
        '
        Me.cbStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!)
        Me.cbStop.FormattingEnabled = True
        Me.cbStop.Location = New System.Drawing.Point(167, 193)
        Me.cbStop.Name = "cbStop"
        Me.cbStop.Size = New System.Drawing.Size(502, 46)
        Me.cbStop.TabIndex = 37
        '
        'cbRun
        '
        Me.cbRun.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!)
        Me.cbRun.FormattingEnabled = True
        Me.cbRun.Location = New System.Drawing.Point(167, 134)
        Me.cbRun.Name = "cbRun"
        Me.cbRun.Size = New System.Drawing.Size(502, 46)
        Me.cbRun.TabIndex = 36
        '
        'lbLoss
        '
        Me.lbLoss.AutoSize = True
        Me.lbLoss.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLoss.Location = New System.Drawing.Point(39, 449)
        Me.lbLoss.Name = "lbLoss"
        Me.lbLoss.Size = New System.Drawing.Size(0, 31)
        Me.lbLoss.TabIndex = 34
        '
        'lbStop
        '
        Me.lbStop.AutoSize = True
        Me.lbStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbStop.Location = New System.Drawing.Point(39, 375)
        Me.lbStop.Name = "lbStop"
        Me.lbStop.Size = New System.Drawing.Size(0, 31)
        Me.lbStop.TabIndex = 33
        '
        'lbRun
        '
        Me.lbRun.AutoSize = True
        Me.lbRun.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbRun.Location = New System.Drawing.Point(53, 293)
        Me.lbRun.Name = "lbRun"
        Me.lbRun.Size = New System.Drawing.Size(0, 31)
        Me.lbRun.TabIndex = 32
        '
        'lbNC
        '
        Me.lbNC.AutoSize = True
        Me.lbNC.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNC.Location = New System.Drawing.Point(73, 145)
        Me.lbNC.Name = "lbNC"
        Me.lbNC.Size = New System.Drawing.Size(0, 31)
        Me.lbNC.TabIndex = 30
        '
        'lbNCMax
        '
        Me.lbNCMax.AutoSize = True
        Me.lbNCMax.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNCMax.Location = New System.Drawing.Point(8, 220)
        Me.lbNCMax.Name = "lbNCMax"
        Me.lbNCMax.Size = New System.Drawing.Size(0, 31)
        Me.lbNCMax.TabIndex = 31
        '
        'lbNG
        '
        Me.lbNG.AutoSize = True
        Me.lbNG.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNG.Location = New System.Drawing.Point(72, 71)
        Me.lbNG.Name = "lbNG"
        Me.lbNG.Size = New System.Drawing.Size(0, 31)
        Me.lbNG.TabIndex = 29
        '
        'Button4
        '
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.Location = New System.Drawing.Point(16, 490)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(147, 100)
        Me.Button4.TabIndex = 43
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.Location = New System.Drawing.Point(645, 490)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(147, 100)
        Me.Button3.TabIndex = 42
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(43, 139)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 37)
        Me.Label5.TabIndex = 57
        Me.Label5.Text = "RUN  :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(32, 205)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(131, 37)
        Me.Label6.TabIndex = 58
        Me.Label6.Text = "STOP : "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(32, 262)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(129, 37)
        Me.Label7.TabIndex = 59
        Me.Label7.Text = "LOSS  :"
        '
        'Settinglamp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbLoss)
        Me.Controls.Add(Me.cbStop)
        Me.Controls.Add(Me.cbRun)
        Me.Controls.Add(Me.lbLoss)
        Me.Controls.Add(Me.lbStop)
        Me.Controls.Add(Me.lbRun)
        Me.Controls.Add(Me.lbNC)
        Me.Controls.Add(Me.lbNCMax)
        Me.Controls.Add(Me.lbNG)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Settinglamp"
        Me.Text = "Settinglamp"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button9 As Button
    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents Button10 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cbLoss As ComboBox
    Friend WithEvents cbStop As ComboBox
    Friend WithEvents cbRun As ComboBox
    Friend WithEvents lbLoss As Label
    Friend WithEvents lbStop As Label
    Friend WithEvents lbRun As Label
    Friend WithEvents lbNC As Label
    Friend WithEvents lbNCMax As Label
    Friend WithEvents lbNG As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
End Class
