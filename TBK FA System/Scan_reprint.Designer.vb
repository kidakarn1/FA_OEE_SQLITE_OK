<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Scan_reprint
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Scan_reprint))
        Me.Date_start_reprint = New System.Windows.Forms.DateTimePicker()
        Me.Date_end_reprint = New System.Windows.Forms.DateTimePicker()
        Me.pbBack = New System.Windows.Forms.PictureBox()
        Me.BbOk = New System.Windows.Forms.PictureBox()
        CType(Me.pbBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BbOk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Date_start_reprint
        '
        Me.Date_start_reprint.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 52.0!)
        Me.Date_start_reprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Date_start_reprint.Location = New System.Drawing.Point(37, 109)
        Me.Date_start_reprint.Name = "Date_start_reprint"
        Me.Date_start_reprint.Size = New System.Drawing.Size(434, 44)
        Me.Date_start_reprint.TabIndex = 4
        Me.Date_start_reprint.Value = New Date(2021, 10, 11, 0, 0, 0, 0)
        '
        'Date_end_reprint
        '
        Me.Date_end_reprint.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Date_end_reprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Date_end_reprint.Location = New System.Drawing.Point(37, 237)
        Me.Date_end_reprint.Name = "Date_end_reprint"
        Me.Date_end_reprint.Size = New System.Drawing.Size(434, 44)
        Me.Date_end_reprint.TabIndex = 5
        Me.Date_end_reprint.Value = New Date(2021, 10, 11, 0, 0, 0, 0)
        '
        'pbBack
        '
        Me.pbBack.BackColor = System.Drawing.Color.Transparent
        Me.pbBack.Location = New System.Drawing.Point(62, 309)
        Me.pbBack.Name = "pbBack"
        Me.pbBack.Size = New System.Drawing.Size(184, 79)
        Me.pbBack.TabIndex = 6
        Me.pbBack.TabStop = False
        '
        'BbOk
        '
        Me.BbOk.BackColor = System.Drawing.Color.Transparent
        Me.BbOk.Location = New System.Drawing.Point(269, 309)
        Me.BbOk.Name = "BbOk"
        Me.BbOk.Size = New System.Drawing.Size(184, 79)
        Me.BbOk.TabIndex = 7
        Me.BbOk.TabStop = False
        '
        'Scan_reprint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(500, 413)
        Me.Controls.Add(Me.BbOk)
        Me.Controls.Add(Me.pbBack)
        Me.Controls.Add(Me.Date_end_reprint)
        Me.Controls.Add(Me.Date_start_reprint)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Scan_reprint"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Scan_reprint"
        CType(Me.pbBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BbOk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Date_start_reprint As DateTimePicker
    Friend WithEvents Date_end_reprint As DateTimePicker
    Friend WithEvents pbBack As PictureBox
    Friend WithEvents BbOk As PictureBox
End Class
