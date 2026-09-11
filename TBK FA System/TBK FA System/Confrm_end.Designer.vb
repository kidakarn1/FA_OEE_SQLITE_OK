<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Confrm_end
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
        Me.menu4 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btn_restart = New System.Windows.Forms.PictureBox()
        CType(Me.btn_restart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'menu4
        '
        Me.menu4.BackColor = System.Drawing.Color.Transparent
        Me.menu4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.menu4.FlatAppearance.BorderSize = 0
        Me.menu4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.menu4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.menu4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.menu4.Location = New System.Drawing.Point(316, 334)
        Me.menu4.Name = "menu4"
        Me.menu4.Size = New System.Drawing.Size(239, 101)
        Me.menu4.TabIndex = 8
        Me.menu4.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(64, 334)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(239, 101)
        Me.Button1.TabIndex = 9
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btn_restart
        '
        Me.btn_restart.BackColor = System.Drawing.Color.Transparent
        Me.btn_restart.Location = New System.Drawing.Point(471, 27)
        Me.btn_restart.Name = "btn_restart"
        Me.btn_restart.Size = New System.Drawing.Size(137, 106)
        Me.btn_restart.TabIndex = 10
        Me.btn_restart.TabStop = False
        '
        'Confrm_end
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSlateGray
        Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.exitProgram
        Me.ClientSize = New System.Drawing.Size(620, 450)
        Me.ControlBox = False
        Me.Controls.Add(Me.btn_restart)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.menu4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Confrm_end"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.btn_restart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents menu4 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents btn_restart As PictureBox
End Class
