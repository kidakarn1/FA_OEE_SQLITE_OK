<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TestForm
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TestForm))
        Me.btnCheckNetwork = New System.Windows.Forms.Button()
        Me.NormalQTY = New System.Windows.Forms.Label()
        Me.lblGood = New System.Windows.Forms.Label()
        Me.lblDefect = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cboExpected = New System.Windows.Forms.ComboBox()
        Me.lblDetected = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnCheckNetwork
        '
        Me.btnCheckNetwork.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCheckNetwork.Location = New System.Drawing.Point(12, 253)
        Me.btnCheckNetwork.Name = "btnCheckNetwork"
        Me.btnCheckNetwork.Size = New System.Drawing.Size(391, 115)
        Me.btnCheckNetwork.TabIndex = 4641
        Me.btnCheckNetwork.Text = "Sent Parameter"
        Me.btnCheckNetwork.UseVisualStyleBackColor = True
        '
        'NormalQTY
        '
        Me.NormalQTY.AutoSize = True
        Me.NormalQTY.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NormalQTY.Location = New System.Drawing.Point(434, 236)
        Me.NormalQTY.Name = "NormalQTY"
        Me.NormalQTY.Size = New System.Drawing.Size(172, 31)
        Me.NormalQTY.TabIndex = 4642
        Me.NormalQTY.Text = "NormalQTY :"
        '
        'lblGood
        '
        Me.lblGood.AutoSize = True
        Me.lblGood.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGood.Location = New System.Drawing.Point(627, 236)
        Me.lblGood.Name = "lblGood"
        Me.lblGood.Size = New System.Drawing.Size(29, 31)
        Me.lblGood.TabIndex = 4643
        Me.lblGood.Text = "0"
        '
        'lblDefect
        '
        Me.lblDefect.AutoSize = True
        Me.lblDefect.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDefect.Location = New System.Drawing.Point(627, 296)
        Me.lblDefect.Name = "lblDefect"
        Me.lblDefect.Size = New System.Drawing.Size(29, 31)
        Me.lblDefect.TabIndex = 4645
        Me.lblDefect.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(434, 296)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(165, 31)
        Me.Label3.TabIndex = 4644
        Me.Label3.Text = "DefectQTY :"
        '
        'Timer1
        '
        '
        'cboExpected
        '
        Me.cboExpected.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.cboExpected.FormattingEnabled = True
        Me.cboExpected.Location = New System.Drawing.Point(39, 123)
        Me.cboExpected.Name = "cboExpected"
        Me.cboExpected.Size = New System.Drawing.Size(169, 33)
        Me.cboExpected.TabIndex = 4646
        '
        'lblDetected
        '
        Me.lblDetected.AutoSize = True
        Me.lblDetected.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetected.Location = New System.Drawing.Point(277, 120)
        Me.lblDetected.Name = "lblDetected"
        Me.lblDetected.Size = New System.Drawing.Size(172, 31)
        Me.lblDetected.TabIndex = 4647
        Me.lblDetected.Text = "NormalQTY :"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(277, 166)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(172, 31)
        Me.lblStatus.TabIndex = 4648
        Me.lblStatus.Text = "NormalQTY :"
        '
        'TestForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblDetected)
        Me.Controls.Add(Me.cboExpected)
        Me.Controls.Add(Me.lblDefect)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblGood)
        Me.Controls.Add(Me.NormalQTY)
        Me.Controls.Add(Me.btnCheckNetwork)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "TestForm"
        Me.Text = "TestForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCheckNetwork As Button
    Friend WithEvents NormalQTY As Label
    Friend WithEvents lblGood As Label
    Friend WithEvents lblDefect As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents cboExpected As ComboBox
    Friend WithEvents lblDetected As Label
    Friend WithEvents lblStatus As Label
End Class
