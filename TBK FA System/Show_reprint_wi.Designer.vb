<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Show_reprint_wi
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Show_reprint_wi))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListView_WI = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.DATE_TIME_PROD = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.hide_wi_select = New System.Windows.Forms.Label()
        Me.pcBack = New System.Windows.Forms.PictureBox()
        Me.pcOK = New System.Windows.Forms.PictureBox()
        CType(Me.pcBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcOK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.DarkSlateGray
        Me.Label1.Enabled = False
        Me.Label1.Font = New System.Drawing.Font("Arial Black", 25.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Aqua
        Me.Label1.Location = New System.Drawing.Point(530, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(238, 48)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SELECT  WI"
        Me.Label1.Visible = False
        '
        'ListView_WI
        '
        Me.ListView_WI.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListView_WI.AllowColumnReorder = True
        Me.ListView_WI.AllowDrop = True
        Me.ListView_WI.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.ListView_WI.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView_WI.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.DATE_TIME_PROD})
        Me.ListView_WI.Cursor = System.Windows.Forms.Cursors.Default
        Me.ListView_WI.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.ListView_WI.ForeColor = System.Drawing.Color.White
        Me.ListView_WI.FullRowSelect = True
        Me.ListView_WI.HideSelection = False
        Me.ListView_WI.Location = New System.Drawing.Point(32, 100)
        Me.ListView_WI.Name = "ListView_WI"
        Me.ListView_WI.Size = New System.Drawing.Size(734, 372)
        Me.ListView_WI.TabIndex = 35
        Me.ListView_WI.UseCompatibleStateImageBehavior = False
        Me.ListView_WI.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "No."
        Me.ColumnHeader1.Width = 195
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "WI"
        Me.ColumnHeader2.Width = 257
        '
        'DATE_TIME_PROD
        '
        Me.DATE_TIME_PROD.Text = "PLAN DATE"
        Me.DATE_TIME_PROD.Width = 430
        '
        'hide_wi_select
        '
        Me.hide_wi_select.AutoSize = True
        Me.hide_wi_select.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hide_wi_select.ForeColor = System.Drawing.Color.Coral
        Me.hide_wi_select.Location = New System.Drawing.Point(209, 516)
        Me.hide_wi_select.Name = "hide_wi_select"
        Me.hide_wi_select.Size = New System.Drawing.Size(78, 20)
        Me.hide_wi_select.TabIndex = 39
        Me.hide_wi_select.Text = "HIDE_WI"
        '
        'pcBack
        '
        Me.pcBack.BackColor = System.Drawing.Color.Transparent
        Me.pcBack.Location = New System.Drawing.Point(25, 496)
        Me.pcBack.Name = "pcBack"
        Me.pcBack.Size = New System.Drawing.Size(183, 83)
        Me.pcBack.TabIndex = 40
        Me.pcBack.TabStop = False
        '
        'pcOK
        '
        Me.pcOK.BackColor = System.Drawing.Color.Transparent
        Me.pcOK.Location = New System.Drawing.Point(585, 496)
        Me.pcOK.Name = "pcOK"
        Me.pcOK.Size = New System.Drawing.Size(183, 83)
        Me.pcOK.TabIndex = 41
        Me.pcOK.TabStop = False
        '
        'Show_reprint_wi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.BackColor = System.Drawing.Color.DarkSlateGray
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(770, 577)
        Me.ControlBox = False
        Me.Controls.Add(Me.pcOK)
        Me.Controls.Add(Me.pcBack)
        Me.Controls.Add(Me.hide_wi_select)
        Me.Controls.Add(Me.ListView_WI)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Show_reprint_wi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Show_reprint_wi"
        CType(Me.pcBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcOK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents ListView_WI As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents hide_wi_select As Label
    Friend WithEvents DATE_TIME_PROD As ColumnHeader
    Friend WithEvents pcBack As PictureBox
    Friend WithEvents pcOK As PictureBox
End Class
