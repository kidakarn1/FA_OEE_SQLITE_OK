<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class tag_reprint_new
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(tag_reprint_new))
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pb_down = New System.Windows.Forms.PictureBox()
        Me.pbUp = New System.Windows.Forms.PictureBox()
        Me.Label9_fontModel = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.lb_font1 = New System.Windows.Forms.Label()
        Me.lb_font2 = New System.Windows.Forms.Label()
        Me.lb_font3 = New System.Windows.Forms.Label()
        Me.lb_font5 = New System.Windows.Forms.Label()
        Me.lb_font4_B = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label_wi_type = New System.Windows.Forms.Label()
        Me.LB_QTY = New System.Windows.Forms.Label()
        Me.LB_FONT_DATA = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_down, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox2.Location = New System.Drawing.Point(8, 518)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(192, 76)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 32
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Location = New System.Drawing.Point(601, 517)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(192, 76)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 33
        Me.PictureBox1.TabStop = False
        '
        'ListView1
        '
        Me.ListView1.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.AllowDrop = True
        Me.ListView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.ListView1.Cursor = System.Windows.Forms.Cursors.Default
        Me.ListView1.Font = New System.Drawing.Font("Catamaran", 17.75!, System.Drawing.FontStyle.Bold)
        Me.ListView1.ForeColor = System.Drawing.Color.White
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(13, 146)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(688, 364)
        Me.ListView1.TabIndex = 34
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "No."
        Me.ColumnHeader1.Width = 81
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Date - Time"
        Me.ColumnHeader2.Width = 316
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Seq."
        Me.ColumnHeader3.Width = 97
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Shift"
        Me.ColumnHeader4.Width = 111
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Box"
        Me.ColumnHeader5.Width = 81
        '
        'PrintDocument1
        '
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(44, 16)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(110, 43)
        Me.ListBox1.TabIndex = 36
        Me.ListBox1.Visible = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox4.Location = New System.Drawing.Point(667, 16)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(77, 64)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 38
        Me.PictureBox4.TabStop = False
        Me.PictureBox4.Visible = False
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'Timer1
        '
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial Black", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(555, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 15)
        Me.Label4.TabIndex = 4636
        Me.Label4.Text = "Label4"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Catamaran", 25.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(549, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 37)
        Me.Label1.TabIndex = 4635
        Me.Label1.Text = "Label1"
        '
        'pb_down
        '
        Me.pb_down.BackColor = System.Drawing.Color.Transparent
        Me.pb_down.Location = New System.Drawing.Point(709, 308)
        Me.pb_down.Name = "pb_down"
        Me.pb_down.Size = New System.Drawing.Size(76, 201)
        Me.pb_down.TabIndex = 4638
        Me.pb_down.TabStop = False
        '
        'pbUp
        '
        Me.pbUp.BackColor = System.Drawing.Color.Transparent
        Me.pbUp.Location = New System.Drawing.Point(710, 102)
        Me.pbUp.Name = "pbUp"
        Me.pbUp.Size = New System.Drawing.Size(75, 201)
        Me.pbUp.TabIndex = 4637
        Me.pbUp.TabStop = False
        '
        'Label9_fontModel
        '
        Me.Label9_fontModel.AutoSize = True
        Me.Label9_fontModel.Font = New System.Drawing.Font("Consolas", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label9_fontModel.Location = New System.Drawing.Point(183, 57)
        Me.Label9_fontModel.Name = "Label9_fontModel"
        Me.Label9_fontModel.Size = New System.Drawing.Size(186, 23)
        Me.Label9_fontModel.TabIndex = 4655
        Me.Label9_fontModel.Text = "Label9_fontModel"
        Me.Label9_fontModel.Visible = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox3.Location = New System.Drawing.Point(333, 3)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(76, 64)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 37
        Me.PictureBox3.TabStop = False
        Me.PictureBox3.Visible = False
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(316, 19)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(118, 30)
        Me.ListBox2.TabIndex = 39
        Me.ListBox2.Visible = False
        '
        'lb_font1
        '
        Me.lb_font1.AutoSize = True
        Me.lb_font1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_font1.Location = New System.Drawing.Point(311, 9)
        Me.lb_font1.Name = "lb_font1"
        Me.lb_font1.Size = New System.Drawing.Size(98, 29)
        Me.lb_font1.TabIndex = 40
        Me.lb_font1.Text = "lb_font1"
        Me.lb_font1.Visible = False
        '
        'lb_font2
        '
        Me.lb_font2.AutoSize = True
        Me.lb_font2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_font2.Location = New System.Drawing.Point(292, 9)
        Me.lb_font2.Name = "lb_font2"
        Me.lb_font2.Size = New System.Drawing.Size(89, 25)
        Me.lb_font2.TabIndex = 41
        Me.lb_font2.Text = "lb_font2"
        Me.lb_font2.Visible = False
        '
        'lb_font3
        '
        Me.lb_font3.AutoSize = True
        Me.lb_font3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_font3.Location = New System.Drawing.Point(349, 4)
        Me.lb_font3.Name = "lb_font3"
        Me.lb_font3.Size = New System.Drawing.Size(60, 18)
        Me.lb_font3.TabIndex = 42
        Me.lb_font3.Text = "lb_font3"
        Me.lb_font3.Visible = False
        '
        'lb_font5
        '
        Me.lb_font5.AutoSize = True
        Me.lb_font5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_font5.Location = New System.Drawing.Point(364, 9)
        Me.lb_font5.Name = "lb_font5"
        Me.lb_font5.Size = New System.Drawing.Size(45, 13)
        Me.lb_font5.TabIndex = 43
        Me.lb_font5.Text = "lb_font5"
        Me.lb_font5.Visible = False
        '
        'lb_font4_B
        '
        Me.lb_font4_B.AutoSize = True
        Me.lb_font4_B.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_font4_B.Location = New System.Drawing.Point(348, 22)
        Me.lb_font4_B.Name = "lb_font4_B"
        Me.lb_font4_B.Size = New System.Drawing.Size(97, 20)
        Me.lb_font4_B.TabIndex = 44
        Me.lb_font4_B.Text = "lb_font4_B"
        Me.lb_font4_B.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(343, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 16)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Label2"
        Me.Label2.Visible = False
        '
        'Label_wi_type
        '
        Me.Label_wi_type.AutoSize = True
        Me.Label_wi_type.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_wi_type.Location = New System.Drawing.Point(343, 9)
        Me.Label_wi_type.Name = "Label_wi_type"
        Me.Label_wi_type.Size = New System.Drawing.Size(75, 13)
        Me.Label_wi_type.TabIndex = 47
        Me.Label_wi_type.Text = "Label_wi_type"
        Me.Label_wi_type.Visible = False
        '
        'LB_QTY
        '
        Me.LB_QTY.AutoSize = True
        Me.LB_QTY.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB_QTY.Location = New System.Drawing.Point(325, 3)
        Me.LB_QTY.Name = "LB_QTY"
        Me.LB_QTY.Size = New System.Drawing.Size(67, 29)
        Me.LB_QTY.TabIndex = 48
        Me.LB_QTY.Text = "QTY"
        Me.LB_QTY.Visible = False
        '
        'LB_FONT_DATA
        '
        Me.LB_FONT_DATA.AutoSize = True
        Me.LB_FONT_DATA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB_FONT_DATA.Location = New System.Drawing.Point(306, 19)
        Me.LB_FONT_DATA.Name = "LB_FONT_DATA"
        Me.LB_FONT_DATA.Size = New System.Drawing.Size(112, 16)
        Me.LB_FONT_DATA.TabIndex = 49
        Me.LB_FONT_DATA.Text = "LB_FONT_DATA"
        Me.LB_FONT_DATA.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Consolas", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(151, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(186, 23)
        Me.Label3.TabIndex = 4655
        Me.Label3.Text = "Label9_fontModel"
        Me.Label3.Visible = False
        '
        'tag_reprint_new
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkSlateGray
        Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.reprintTag1
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label9_fontModel)
        Me.Controls.Add(Me.pb_down)
        Me.Controls.Add(Me.pbUp)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LB_FONT_DATA)
        Me.Controls.Add(Me.LB_QTY)
        Me.Controls.Add(Me.Label_wi_type)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lb_font4_B)
        Me.Controls.Add(Me.lb_font5)
        Me.Controls.Add(Me.lb_font3)
        Me.Controls.Add(Me.lb_font2)
        Me.Controls.Add(Me.lb_font1)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.PictureBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "tag_reprint_new"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "p"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_down, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents pb_down As PictureBox
    Friend WithEvents pbUp As PictureBox
    Friend WithEvents Label9_fontModel As Label
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents lb_font1 As Label
    Friend WithEvents lb_font2 As Label
    Friend WithEvents lb_font3 As Label
    Friend WithEvents lb_font5 As Label
    Friend WithEvents lb_font4_B As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label_wi_type As Label
    Friend WithEvents LB_QTY As Label
    Friend WithEvents LB_FONT_DATA As Label
    Friend WithEvents Label3 As Label
End Class
