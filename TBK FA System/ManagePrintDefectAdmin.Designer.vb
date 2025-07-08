<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ManagePrintDefectAdmin
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.lvShowDataDefect = New System.Windows.Forms.ListView()
        Me.NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Part_NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Lot_NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SEQ = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.QTY = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.BOX = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Shift = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.comboxitemtype = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 41.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(55, 11)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(405, 79)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Print Defect"
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!)
        Me.btnBack.Location = New System.Drawing.Point(16, 624)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(215, 105)
        Me.btnBack.TabIndex = 3
        Me.btnBack.Text = "Back"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'lvShowDataDefect
        '
        Me.lvShowDataDefect.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NO, Me.Part_NO, Me.Lot_NO, Me.SEQ, Me.QTY, Me.BOX, Me.Shift})
        Me.lvShowDataDefect.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lvShowDataDefect.FullRowSelect = True
        Me.lvShowDataDefect.HideSelection = False
        Me.lvShowDataDefect.Location = New System.Drawing.Point(56, 112)
        Me.lvShowDataDefect.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lvShowDataDefect.Name = "lvShowDataDefect"
        Me.lvShowDataDefect.Size = New System.Drawing.Size(897, 499)
        Me.lvShowDataDefect.TabIndex = 4
        Me.lvShowDataDefect.UseCompatibleStateImageBehavior = False
        Me.lvShowDataDefect.View = System.Windows.Forms.View.Details
        '
        'NO
        '
        Me.NO.Text = "NO"
        '
        'Part_NO
        '
        Me.Part_NO.Text = "Part NO"
        Me.Part_NO.Width = 190
        '
        'Lot_NO
        '
        Me.Lot_NO.Text = "Lot No"
        Me.Lot_NO.Width = 100
        '
        'SEQ
        '
        Me.SEQ.Text = "SEQ"
        Me.SEQ.Width = 80
        '
        'QTY
        '
        Me.QTY.Text = "QTY"
        Me.QTY.Width = 80
        '
        'BOX
        '
        Me.BOX.Text = "BOX"
        Me.BOX.Width = 80
        '
        'Shift
        '
        Me.Shift.Text = "Shift"
        Me.Shift.Width = 80
        '
        'comboxitemtype
        '
        Me.comboxitemtype.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!)
        Me.comboxitemtype.FormattingEnabled = True
        Me.comboxitemtype.Location = New System.Drawing.Point(575, 15)
        Me.comboxitemtype.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.comboxitemtype.Name = "comboxitemtype"
        Me.comboxitemtype.Size = New System.Drawing.Size(379, 77)
        Me.comboxitemtype.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!)
        Me.Button1.Location = New System.Drawing.Point(836, 624)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(215, 105)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Print"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ManagePrintDefectAdmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1067, 738)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.comboxitemtype)
        Me.Controls.Add(Me.lvShowDataDefect)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "ManagePrintDefectAdmin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ManagePrintDefectAdmin"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents btnBack As Button
    Friend WithEvents lvShowDataDefect As ListView
    Friend WithEvents comboxitemtype As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents NO As ColumnHeader
    Friend WithEvents Part_NO As ColumnHeader
    Friend WithEvents Lot_NO As ColumnHeader
    Friend WithEvents SEQ As ColumnHeader
    Friend WithEvents QTY As ColumnHeader
    Friend WithEvents BOX As ColumnHeader
    Friend WithEvents Shift As ColumnHeader
End Class
