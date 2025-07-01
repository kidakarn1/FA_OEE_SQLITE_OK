<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrintLabelBreak
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PrintLabelBreak))
        Me.PrintDocument31 = New System.Drawing.Printing.PrintDocument()
        Me.lbButton = New System.Windows.Forms.Label()
        Me.lbPartNumber = New System.Windows.Forms.Label()
        Me.lbQrCode = New System.Windows.Forms.Label()
        Me.lb_font2 = New System.Windows.Forms.Label()
        Me.logoLabel = New System.Windows.Forms.PictureBox()
        Me.lbPartMmodelBreak = New System.Windows.Forms.Label()
        Me.picBarcode = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.logoLabel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbButton
        '
        Me.lbButton.AutoSize = True
        Me.lbButton.Font = New System.Drawing.Font("Consolas", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbButton.Location = New System.Drawing.Point(427, 260)
        Me.lbButton.Name = "lbButton"
        Me.lbButton.Size = New System.Drawing.Size(32, 18)
        Me.lbButton.TabIndex = 30
        Me.lbButton.Text = "001"
        '
        'lbPartNumber
        '
        Me.lbPartNumber.AutoSize = True
        Me.lbPartNumber.Font = New System.Drawing.Font("HGPGothicE", 18.75!)
        Me.lbPartNumber.Location = New System.Drawing.Point(178, 340)
        Me.lbPartNumber.Name = "lbPartNumber"
        Me.lbPartNumber.Size = New System.Drawing.Size(281, 25)
        Me.lbPartNumber.TabIndex = 31
        Me.lbPartNumber.Text = "755190-1280       Rr RH"
        '
        'lbQrCode
        '
        Me.lbQrCode.AutoSize = True
        Me.lbQrCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lbQrCode.Location = New System.Drawing.Point(584, 198)
        Me.lbQrCode.Name = "lbQrCode"
        Me.lbQrCode.Size = New System.Drawing.Size(100, 24)
        Me.lbQrCode.TabIndex = 32
        Me.lbQrCode.Text = "lbQrCode"
        '
        'lb_font2
        '
        Me.lb_font2.AutoSize = True
        Me.lb_font2.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_font2.Location = New System.Drawing.Point(345, 208)
        Me.lb_font2.Name = "lb_font2"
        Me.lb_font2.Size = New System.Drawing.Size(61, 23)
        Me.lb_font2.TabIndex = 4590
        Me.lb_font2.Text = "Label7"
        Me.lb_font2.Visible = False
        '
        'logoLabel
        '
        Me.logoLabel.Image = CType(resources.GetObject("logoLabel.Image"), System.Drawing.Image)
        Me.logoLabel.Location = New System.Drawing.Point(642, 328)
        Me.logoLabel.Name = "logoLabel"
        Me.logoLabel.Size = New System.Drawing.Size(28, 25)
        Me.logoLabel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.logoLabel.TabIndex = 4593
        Me.logoLabel.TabStop = False
        '
        'lbPartMmodelBreak
        '
        Me.lbPartMmodelBreak.AutoSize = True
        Me.lbPartMmodelBreak.Font = New System.Drawing.Font("HGPGothicE", 18.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbPartMmodelBreak.Location = New System.Drawing.Point(459, 88)
        Me.lbPartMmodelBreak.Name = "lbPartMmodelBreak"
        Me.lbPartMmodelBreak.Size = New System.Drawing.Size(222, 25)
        Me.lbPartMmodelBreak.TabIndex = 4594
        Me.lbPartMmodelBreak.Text = "lbPartMmodelBreak"
        '
        'picBarcode
        '
        Me.picBarcode.Location = New System.Drawing.Point(98, 88)
        Me.picBarcode.Name = "picBarcode"
        Me.picBarcode.Size = New System.Drawing.Size(100, 50)
        Me.picBarcode.TabIndex = 4595
        Me.picBarcode.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(648, 260)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 19)
        Me.Label1.TabIndex = 4596
        Me.Label1.Text = "Label1"
        '
        'PrintLabelBreak
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.picBarcode)
        Me.Controls.Add(Me.lbPartMmodelBreak)
        Me.Controls.Add(Me.logoLabel)
        Me.Controls.Add(Me.lb_font2)
        Me.Controls.Add(Me.lbQrCode)
        Me.Controls.Add(Me.lbPartNumber)
        Me.Controls.Add(Me.lbButton)
        Me.Name = "PrintLabelBreak"
        Me.Text = "PrintLabelBreak"
        CType(Me.logoLabel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PrintDocument31 As Printing.PrintDocument
    Friend WithEvents lbButton As Label
    Friend WithEvents lbPartNumber As Label
    Friend WithEvents lbQrCode As Label
    Friend WithEvents lb_font2 As Label
    Friend WithEvents logoLabel As PictureBox
    Friend WithEvents lbPartMmodelBreak As Label
    Friend WithEvents picBarcode As PictureBox
    Friend WithEvents Label1 As Label
End Class
