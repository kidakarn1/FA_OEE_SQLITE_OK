<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Test_scrobar
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
        Me.PrintDocument2 = New System.Drawing.Printing.PrintDocument()
        Me.PrintDocument5 = New System.Drawing.Printing.PrintDocument()
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'PrintButton
        '
        Me.PrintButton.Location = New System.Drawing.Point(117, 60)
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.Size = New System.Drawing.Size(277, 84)
        Me.PrintButton.TabIndex = 0
        Me.PrintButton.Text = "Button1"
        Me.PrintButton.UseVisualStyleBackColor = True
        '
        'RichTextBox2
        '
        Me.RichTextBox2.Location = New System.Drawing.Point(56, 161)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.Size = New System.Drawing.Size(370, 263)
        Me.RichTextBox2.TabIndex = 1
        Me.RichTextBox2.Text = ""
        '
        'Test_scrobar
        '
        Me.ClientSize = New System.Drawing.Size(496, 475)
        Me.Controls.Add(Me.RichTextBox2)
        Me.Controls.Add(Me.PrintButton)
        Me.Name = "Test_scrobar"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOpenPort As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents Label1 As Label
    Friend WithEvents close As Button
    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents widthText As TextBox
    Friend WithEvents HeightText As TextBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents PORT1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents PrintDocument2 As Printing.PrintDocument
    Friend WithEvents PrintDocument5 As Printing.PrintDocument
    Friend WithEvents PrintButton As Button
    Friend WithEvents RichTextBox2 As RichTextBox
End Class
