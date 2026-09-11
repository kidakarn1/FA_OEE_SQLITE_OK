<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class defectAlertupdatesuccess
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.Button1 = New System.Windows.Forms.PictureBox()
		CType(Me.Button1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Button1
		'
		Me.Button1.BackColor = System.Drawing.Color.Transparent
		Me.Button1.Location = New System.Drawing.Point(152, 206)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(202, 84)
		Me.Button1.TabIndex = 49
		Me.Button1.TabStop = False
		'
		'defectAlertupdatesuccess
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.defectAlertupdate
		Me.ClientSize = New System.Drawing.Size(510, 310)
		Me.Controls.Add(Me.Button1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.Name = "defectAlertupdatesuccess"
		Me.Text = "defectAlertupdatesuccess"
		CType(Me.Button1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents Button1 As PictureBox
End Class
