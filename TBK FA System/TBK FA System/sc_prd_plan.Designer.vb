<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class sc_prd_plan
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
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.lb_text_buffer = New System.Windows.Forms.Label()
        Me.lb_sc_count = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(153, 155)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(312, 31)
        Me.TextBox1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(4, 98)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(604, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "PLEASE SCAN QR OR BARCODE ON THE INSTRUCTION TAG"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Red
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(237, 272)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(124, 62)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'SerialPort1
        '
        '
        'lb_text_buffer
        '
        Me.lb_text_buffer.AutoSize = True
        Me.lb_text_buffer.Location = New System.Drawing.Point(45, 55)
        Me.lb_text_buffer.Name = "lb_text_buffer"
        Me.lb_text_buffer.Size = New System.Drawing.Size(71, 13)
        Me.lb_text_buffer.TabIndex = 3
        Me.lb_text_buffer.Text = "lb_text_buffer"
        Me.lb_text_buffer.Visible = False
        '
        'lb_sc_count
        '
        Me.lb_sc_count.AutoSize = True
        Me.lb_sc_count.Location = New System.Drawing.Point(473, 9)
        Me.lb_sc_count.Name = "lb_sc_count"
        Me.lb_sc_count.Size = New System.Drawing.Size(65, 13)
        Me.lb_sc_count.TabIndex = 4
        Me.lb_sc_count.Text = "lb_sc_count"
        Me.lb_sc_count.Visible = False
        '
        'sc_prd_plan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Teal
        Me.ClientSize = New System.Drawing.Size(620, 384)
        Me.Controls.Add(Me.lb_sc_count)
        Me.Controls.Add(Me.lb_text_buffer)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "sc_prd_plan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "sc_prd_plan"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents lb_text_buffer As Label
    Friend WithEvents lb_sc_count As Label
End Class
