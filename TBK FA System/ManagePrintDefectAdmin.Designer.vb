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
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.comboxitemtype = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Part_NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Lot_NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SEQ = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.QTY = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.BOX = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Shift = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 41.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(41, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(328, 63)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Print Defect"
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!)
        Me.btnBack.Location = New System.Drawing.Point(12, 507)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(161, 85)
        Me.btnBack.TabIndex = 3
        Me.btnBack.Text = "btnBack"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NO, Me.Part_NO, Me.Lot_NO, Me.SEQ, Me.QTY, Me.BOX, Me.Shift})
        Me.ListView1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(42, 91)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(674, 406)
        Me.ListView1.TabIndex = 4
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'comboxitemtype
        '
        Me.comboxitemtype.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!)
        Me.comboxitemtype.FormattingEnabled = True
        Me.comboxitemtype.Location = New System.Drawing.Point(431, 12)
        Me.comboxitemtype.Name = "comboxitemtype"
        Me.comboxitemtype.Size = New System.Drawing.Size(285, 63)
        Me.comboxitemtype.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!)
        Me.Button1.Location = New System.Drawing.Point(627, 507)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(161, 85)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Print"
        Me.Button1.UseVisualStyleBackColor = True
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
        'NO
        '
        Me.NO.Text = "NO"
        '
        'Shift
        '
        Me.Shift.Text = "Shift"
        Me.Shift.Width = 80
        '
        'ManagePrintDefectAdmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.comboxitemtype)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ManagePrintDefectAdmin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ManagePrintDefectAdmin"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents btnBack As Button
    Friend WithEvents ListView1 As ListView
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
