<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class defectAdminsearchng
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
        Me.lbLine = New System.Windows.Forms.Label()
        Me.LbShift = New System.Windows.Forms.Label()
        Me.dtActdate = New System.Windows.Forms.DateTimePicker()
        Me.btnOk = New System.Windows.Forms.PictureBox()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbLine
        '
        Me.lbLine.AutoSize = True
        Me.lbLine.BackColor = System.Drawing.Color.Transparent
        Me.lbLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLine.ForeColor = System.Drawing.Color.White
        Me.lbLine.Location = New System.Drawing.Point(128, 105)
        Me.lbLine.Name = "lbLine"
        Me.lbLine.Size = New System.Drawing.Size(77, 25)
        Me.lbLine.TabIndex = 32
        Me.lbLine.Text = "Label1"
        '
        'LbShift
        '
        Me.LbShift.AutoSize = True
        Me.LbShift.BackColor = System.Drawing.Color.Transparent
        Me.LbShift.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LbShift.Font = New System.Drawing.Font("Arial Black", 20.0!)
        Me.LbShift.ForeColor = System.Drawing.Color.White
        Me.LbShift.Location = New System.Drawing.Point(49, 366)
        Me.LbShift.Name = "LbShift"
        Me.LbShift.Size = New System.Drawing.Size(258, 38)
        Me.LbShift.TabIndex = 34
        Me.LbShift.Text = "A (08:00 - 17:00)"
        '
        'dtActdate
        '
        Me.dtActdate.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtActdate.CalendarForeColor = System.Drawing.Color.White
        Me.dtActdate.CalendarMonthBackground = System.Drawing.Color.Transparent
        Me.dtActdate.CalendarTitleBackColor = System.Drawing.Color.Transparent
        Me.dtActdate.CalendarTitleForeColor = System.Drawing.Color.Transparent
        Me.dtActdate.CalendarTrailingForeColor = System.Drawing.Color.Transparent
        Me.dtActdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtActdate.Location = New System.Drawing.Point(46, 211)
        Me.dtActdate.Name = "dtActdate"
        Me.dtActdate.Size = New System.Drawing.Size(703, 49)
        Me.dtActdate.TabIndex = 33
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.Transparent
        Me.btnOk.Location = New System.Drawing.Point(600, 509)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(193, 87)
        Me.btnOk.TabIndex = 61
        Me.btnOk.TabStop = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(7, 509)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(193, 87)
        Me.btnBack.TabIndex = 60
        Me.btnBack.TabStop = False
        '
        'defectAdminsearchng
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.defectAdminsearchNG
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.LbShift)
        Me.Controls.Add(Me.dtActdate)
        Me.Controls.Add(Me.lbLine)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "defectAdminsearchng"
        Me.Text = "defectAdminsearchng"
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbLine As Label
	Friend WithEvents LbShift As Label
	Friend WithEvents dtActdate As DateTimePicker
	Friend WithEvents btnOk As PictureBox
	Friend WithEvents btnBack As PictureBox
End Class
