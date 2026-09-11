<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class sc_wi_plan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(sc_wi_plan))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.lb_text_buffer = New System.Windows.Forms.Label()
        Me.lb_text_wi_seq = New System.Windows.Forms.Label()
        Me.lb_text_line = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Label1.Location = New System.Drawing.Point(37, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(529, 37)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Please scan the incomplete fa tag"
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(135, 139)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(312, 40)
        Me.TextBox1.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Red
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(388, 366)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(124, 62)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = False
        Me.Button1.Visible = False
        '
        'SerialPort1
        '
        '
        'lb_text_buffer
        '
        Me.lb_text_buffer.AutoSize = True
        Me.lb_text_buffer.Location = New System.Drawing.Point(113, 243)
        Me.lb_text_buffer.Name = "lb_text_buffer"
        Me.lb_text_buffer.Size = New System.Drawing.Size(71, 13)
        Me.lb_text_buffer.TabIndex = 4
        Me.lb_text_buffer.Text = "lb_text_buffer"
        Me.lb_text_buffer.Visible = False
        '
        'lb_text_wi_seq
        '
        Me.lb_text_wi_seq.AutoSize = True
        Me.lb_text_wi_seq.Location = New System.Drawing.Point(370, 272)
        Me.lb_text_wi_seq.Name = "lb_text_wi_seq"
        Me.lb_text_wi_seq.Size = New System.Drawing.Size(77, 13)
        Me.lb_text_wi_seq.TabIndex = 5
        Me.lb_text_wi_seq.Text = "lb_text_wi_seq"
        Me.lb_text_wi_seq.Visible = False
        '
        'lb_text_line
        '
        Me.lb_text_line.AutoSize = True
        Me.lb_text_line.Location = New System.Drawing.Point(113, 310)
        Me.lb_text_line.Name = "lb_text_line"
        Me.lb_text_line.Size = New System.Drawing.Size(60, 13)
        Me.lb_text_line.TabIndex = 6
        Me.lb_text_line.Text = "lb_text_line"
        Me.lb_text_line.Visible = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(12, 399)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(110, 64)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 31
        Me.PictureBox2.TabStop = False
        '
        'sc_wi_plan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(600, 475)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.lb_text_line)
        Me.Controls.Add(Me.lb_text_wi_seq)
        Me.Controls.Add(Me.lb_text_buffer)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "sc_wi_plan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "sc_wi_plan"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents lb_text_buffer As Label
    Friend WithEvents lb_text_wi_seq As Label
    Friend WithEvents lb_text_line As Label
    Friend WithEvents PictureBox2 As PictureBox
End Class
