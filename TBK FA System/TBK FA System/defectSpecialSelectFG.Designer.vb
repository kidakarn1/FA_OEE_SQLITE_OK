<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class defectSpecialSelectFG
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(defectSpecialSelectFG))
        Me.lvDefectFGSpc = New System.Windows.Forms.ListView()
        Me.NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.WI = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PART_NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PART_NAME = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btn_back = New System.Windows.Forms.PictureBox()
        Me.btn_next = New System.Windows.Forms.PictureBox()
        Me.seq = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pwi_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        CType(Me.btn_back, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_next, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lvDefectFGSpc
        '
        Me.lvDefectFGSpc.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvDefectFGSpc.AllowColumnReorder = True
        Me.lvDefectFGSpc.AllowDrop = True
        Me.lvDefectFGSpc.AutoArrange = False
        Me.lvDefectFGSpc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvDefectFGSpc.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NO, Me.WI, Me.PART_NO, Me.PART_NAME, Me.seq, Me.pwi_id})
        Me.lvDefectFGSpc.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvDefectFGSpc.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvDefectFGSpc.ForeColor = System.Drawing.Color.Black
        Me.lvDefectFGSpc.FullRowSelect = True
        Me.lvDefectFGSpc.GridLines = True
        Me.lvDefectFGSpc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvDefectFGSpc.HideSelection = False
        Me.lvDefectFGSpc.Location = New System.Drawing.Point(34, 153)
        Me.lvDefectFGSpc.MultiSelect = False
        Me.lvDefectFGSpc.Name = "lvDefectFGSpc"
        Me.lvDefectFGSpc.ShowGroups = False
        Me.lvDefectFGSpc.Size = New System.Drawing.Size(684, 342)
        Me.lvDefectFGSpc.TabIndex = 57
        Me.lvDefectFGSpc.UseCompatibleStateImageBehavior = False
        Me.lvDefectFGSpc.View = System.Windows.Forms.View.Details
        '
        'NO
        '
        Me.NO.Text = "NO"
        Me.NO.Width = 45
        '
        'WI
        '
        Me.WI.Text = "PART NO"
        Me.WI.Width = 160
        '
        'PART_NO
        '
        Me.PART_NO.Text = "CODE"
        Me.PART_NO.Width = 185
        '
        'PART_NAME
        '
        Me.PART_NAME.Text = "DETAILS"
        Me.PART_NAME.Width = 292
        '
        'btn_back
        '
        Me.btn_back.BackColor = System.Drawing.Color.Transparent
        Me.btn_back.Location = New System.Drawing.Point(13, 504)
        Me.btn_back.Name = "btn_back"
        Me.btn_back.Size = New System.Drawing.Size(104, 87)
        Me.btn_back.TabIndex = 58
        Me.btn_back.TabStop = False
        '
        'btn_next
        '
        Me.btn_next.BackColor = System.Drawing.Color.Transparent
        Me.btn_next.Location = New System.Drawing.Point(691, 502)
        Me.btn_next.Name = "btn_next"
        Me.btn_next.Size = New System.Drawing.Size(104, 87)
        Me.btn_next.TabIndex = 59
        Me.btn_next.TabStop = False
        '
        'defectSpecialSelectFG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.btn_next)
        Me.Controls.Add(Me.btn_back)
        Me.Controls.Add(Me.lvDefectFGSpc)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "defectSpecialSelectFG"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "defectSpecialSelectFG"
        CType(Me.btn_back, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_next, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lvDefectFGSpc As ListView
    Friend WithEvents NO As ColumnHeader
    Friend WithEvents WI As ColumnHeader
    Friend WithEvents PART_NO As ColumnHeader
    Friend WithEvents PART_NAME As ColumnHeader
    Friend WithEvents btn_back As PictureBox
    Friend WithEvents btn_next As PictureBox
    Friend WithEvents seq As ColumnHeader
    Friend WithEvents pwi_id As ColumnHeader
End Class
