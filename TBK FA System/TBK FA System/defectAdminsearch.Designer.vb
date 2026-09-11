<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class defectAdminsearch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(defectAdminsearch))
        Me.lbLine = New System.Windows.Forms.Label()
        Me.dtActdate = New System.Windows.Forms.DateTimePicker()
        Me.LbShift = New System.Windows.Forms.Label()
        Me.btnOk = New System.Windows.Forms.PictureBox()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.backgroundNG = New System.Windows.Forms.PictureBox()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.backgroundNG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbLine
        '
        Me.lbLine.AutoSize = True
        Me.lbLine.BackColor = System.Drawing.Color.Transparent
        Me.lbLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLine.ForeColor = System.Drawing.Color.White
        Me.lbLine.Location = New System.Drawing.Point(129, 105)
        Me.lbLine.Name = "lbLine"
        Me.lbLine.Size = New System.Drawing.Size(77, 25)
        Me.lbLine.TabIndex = 0
        Me.lbLine.Text = "Label1"
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
        Me.dtActdate.TabIndex = 5
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
        Me.LbShift.TabIndex = 20
        Me.LbShift.Text = "A (08:00 - 17:00)"
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.Transparent
        Me.btnOk.Location = New System.Drawing.Point(600, 509)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(193, 87)
        Me.btnOk.TabIndex = 59
        Me.btnOk.TabStop = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(7, 509)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(193, 87)
        Me.btnBack.TabIndex = 58
        Me.btnBack.TabStop = False
        '
        'backgroundNG
        '
        Me.backgroundNG.BackgroundImage = CType(resources.GetObject("backgroundNG.BackgroundImage"), System.Drawing.Image)
        Me.backgroundNG.Location = New System.Drawing.Point(0, 0)
        Me.backgroundNG.Name = "backgroundNG"
        Me.backgroundNG.Size = New System.Drawing.Size(800, 600)
        Me.backgroundNG.TabIndex = 60
        Me.backgroundNG.TabStop = False
        '
        'defectAdminsearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.defectAdminsearchNC
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.LbShift)
        Me.Controls.Add(Me.dtActdate)
        Me.Controls.Add(Me.lbLine)
        Me.Controls.Add(Me.backgroundNG)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "defectAdminsearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "defectAdminsearchnc"
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.backgroundNG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbLine As Label
	Friend WithEvents dtActdate As DateTimePicker
	Friend WithEvents LbShift As Label
	Friend WithEvents btnOk As PictureBox
    Friend WithEvents btnBack As PictureBox
    Friend WithEvents backgroundNG As PictureBox
End Class
