<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ManagePrintLable
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
        Me.lvShowDataDefect = New System.Windows.Forms.ListView()
        Me.NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PartNo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Lot_NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SEQ = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.POSITION1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.POSITION2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lvShowDataDefect
        '
        Me.lvShowDataDefect.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NO, Me.PartNo, Me.Lot_NO, Me.SEQ, Me.POSITION1, Me.POSITION2})
        Me.lvShowDataDefect.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.0!)
        Me.lvShowDataDefect.FullRowSelect = True
        Me.lvShowDataDefect.HideSelection = False
        Me.lvShowDataDefect.Location = New System.Drawing.Point(42, 91)
        Me.lvShowDataDefect.Name = "lvShowDataDefect"
        Me.lvShowDataDefect.Size = New System.Drawing.Size(674, 406)
        Me.lvShowDataDefect.TabIndex = 9
        Me.lvShowDataDefect.UseCompatibleStateImageBehavior = False
        Me.lvShowDataDefect.View = System.Windows.Forms.View.Details
        '
        'NO
        '
        Me.NO.Text = "NO"
        Me.NO.Width = 65
        '
        'PartNo
        '
        Me.PartNo.Text = "Part NO"
        Me.PartNo.Width = 140
        '
        'Lot_NO
        '
        Me.Lot_NO.Text = "Lot No"
        Me.Lot_NO.Width = 90
        '
        'SEQ
        '
        Me.SEQ.Text = "SEQ"
        Me.SEQ.Width = 75
        '
        'POSITION1
        '
        Me.POSITION1.Text = "POSITION1"
        Me.POSITION1.Width = 150
        '
        'POSITION2
        '
        Me.POSITION2.Text = "POSITION2"
        Me.POSITION2.Width = 150
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!)
        Me.btnBack.Location = New System.Drawing.Point(12, 507)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(161, 85)
        Me.btnBack.TabIndex = 8
        Me.btnBack.Text = "Back"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 41.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(31, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(304, 63)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Print Label"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!)
        Me.Button1.Location = New System.Drawing.Point(627, 507)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(161, 85)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Print"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ManagePrintLable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.lvShowDataDefect)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "ManagePrintLable"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ManagePrintLable"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvShowDataDefect As ListView
    Friend WithEvents NO As ColumnHeader
    Friend WithEvents PartNo As ColumnHeader
    Friend WithEvents Lot_NO As ColumnHeader
    Friend WithEvents SEQ As ColumnHeader
    Friend WithEvents POSITION1 As ColumnHeader
    Friend WithEvents POSITION2 As ColumnHeader
    Friend WithEvents btnBack As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
End Class
