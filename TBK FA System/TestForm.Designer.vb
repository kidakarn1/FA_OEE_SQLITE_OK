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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TestForm))
        Me.btnCheckNetwork = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnCheckNetwork
        '
        Me.btnCheckNetwork.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCheckNetwork.Location = New System.Drawing.Point(16, 311)
        Me.btnCheckNetwork.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCheckNetwork.Name = "btnCheckNetwork"
        Me.btnCheckNetwork.Size = New System.Drawing.Size(521, 142)
        Me.btnCheckNetwork.TabIndex = 4641
        Me.btnCheckNetwork.Text = "Sent Parameter"
        Me.btnCheckNetwork.UseVisualStyleBackColor = True
        '
        'TestForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(1067, 738)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCheckNetwork)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "TestForm"
        Me.Text = "TestForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCheckNetwork As Button
End Class
