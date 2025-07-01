<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Prd_detail
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Prd_detail))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lb_remain_qty = New System.Windows.Forms.Label()
        Me.lb_plan_qty = New System.Windows.Forms.Label()
        Me.lb_wi = New System.Windows.Forms.Label()
        Me.lb_model = New System.Windows.Forms.Label()
        Me.lb_item_name = New System.Windows.Forms.Label()
        Me.lb_item_cd = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.lb_snp = New System.Windows.Forms.Label()
        Me.lb_ct = New System.Windows.Forms.Label()
        Me.lb_seq = New System.Windows.Forms.Label()
        Me.lb_dlv_date = New System.Windows.Forms.Label()
        Me.lb_location = New System.Windows.Forms.Label()
        Me.lb_prd_type = New System.Windows.Forms.Label()
        Me.lb_temp_txt = New System.Windows.Forms.Label()
        Me.lb_temp_line = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.QTY_NC = New System.Windows.Forms.Label()
        Me.QTY_NG = New System.Windows.Forms.Label()
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LB_PLAN_DATE = New System.Windows.Forms.Label()
        Me.LB_ShowWorker = New System.Windows.Forms.PictureBox()
        Me.picSpecial = New System.Windows.Forms.PictureBox()
        Me.lvWISpc = New System.Windows.Forms.ListView()
        Me.NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.WI = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PART_NO = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnUp = New System.Windows.Forms.PictureBox()
        Me.btnDown = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LB_ShowWorker, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picSpecial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        '
        'lb_remain_qty
        '
        Me.lb_remain_qty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lb_remain_qty.AutoSize = True
        Me.lb_remain_qty.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.lb_remain_qty.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lb_remain_qty.Font = New System.Drawing.Font("Catamaran", 29.25!, System.Drawing.FontStyle.Bold)
        Me.lb_remain_qty.ForeColor = System.Drawing.Color.Tomato
        Me.lb_remain_qty.Location = New System.Drawing.Point(641, 361)
        Me.lb_remain_qty.Name = "lb_remain_qty"
        Me.lb_remain_qty.Size = New System.Drawing.Size(139, 43)
        Me.lb_remain_qty.TabIndex = 38
        Me.lb_remain_qty.Text = "XXXXX"
        Me.lb_remain_qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lb_plan_qty
        '
        Me.lb_plan_qty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lb_plan_qty.AutoSize = True
        Me.lb_plan_qty.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.lb_plan_qty.Font = New System.Drawing.Font("Catamaran", 29.25!, System.Drawing.FontStyle.Bold)
        Me.lb_plan_qty.ForeColor = System.Drawing.Color.SpringGreen
        Me.lb_plan_qty.Location = New System.Drawing.Point(443, 361)
        Me.lb_plan_qty.Name = "lb_plan_qty"
        Me.lb_plan_qty.Size = New System.Drawing.Size(139, 43)
        Me.lb_plan_qty.TabIndex = 37
        Me.lb_plan_qty.Text = "XXXXX"
        Me.lb_plan_qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lb_wi
        '
        Me.lb_wi.AutoSize = True
        Me.lb_wi.BackColor = System.Drawing.Color.Transparent
        Me.lb_wi.Font = New System.Drawing.Font("Catamaran", 22.0!, System.Drawing.FontStyle.Bold)
        Me.lb_wi.ForeColor = System.Drawing.Color.White
        Me.lb_wi.Location = New System.Drawing.Point(422, 139)
        Me.lb_wi.Name = "lb_wi"
        Me.lb_wi.Size = New System.Drawing.Size(205, 33)
        Me.lb_wi.TabIndex = 36
        Me.lb_wi.Text = "XXXXXXXXXX"
        '
        'lb_model
        '
        Me.lb_model.AutoSize = True
        Me.lb_model.BackColor = System.Drawing.Color.Transparent
        Me.lb_model.Font = New System.Drawing.Font("Catamaran", 19.0!, System.Drawing.FontStyle.Bold)
        Me.lb_model.ForeColor = System.Drawing.Color.White
        Me.lb_model.Location = New System.Drawing.Point(44, 333)
        Me.lb_model.Name = "lb_model"
        Me.lb_model.Size = New System.Drawing.Size(76, 28)
        Me.lb_model.TabIndex = 35
        Me.lb_model.Text = "XXXX"
        '
        'lb_item_name
        '
        Me.lb_item_name.AutoSize = True
        Me.lb_item_name.BackColor = System.Drawing.Color.Transparent
        Me.lb_item_name.Font = New System.Drawing.Font("Catamaran", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lb_item_name.ForeColor = System.Drawing.Color.White
        Me.lb_item_name.Location = New System.Drawing.Point(38, 237)
        Me.lb_item_name.Name = "lb_item_name"
        Me.lb_item_name.Size = New System.Drawing.Size(285, 29)
        Me.lb_item_name.TabIndex = 34
        Me.lb_item_name.Text = "XXXXXXXXXXXXXXXX"
        '
        'lb_item_cd
        '
        Me.lb_item_cd.AutoSize = True
        Me.lb_item_cd.BackColor = System.Drawing.Color.Transparent
        Me.lb_item_cd.Font = New System.Drawing.Font("Catamaran", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lb_item_cd.ForeColor = System.Drawing.Color.White
        Me.lb_item_cd.Location = New System.Drawing.Point(39, 142)
        Me.lb_item_cd.Name = "lb_item_cd"
        Me.lb_item_cd.Size = New System.Drawing.Size(166, 29)
        Me.lb_item_cd.TabIndex = 33
        Me.lb_item_cd.Text = "XXXXXXXXX"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Catamaran", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(540, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 22)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "X"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label12.Font = New System.Drawing.Font("Catamaran", 21.25!, System.Drawing.FontStyle.Bold)
        Me.Label12.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Label12.Location = New System.Drawing.Point(44, 422)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(263, 32)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "A (08:00 - 17:00)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Catamaran", 26.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(650, 134)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(105, 38)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "XXXX"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TextBox1.Font = New System.Drawing.Font("Arial Black", 10.0!)
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.Window
        Me.TextBox1.Location = New System.Drawing.Point(60, 429)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(221, 19)
        Me.TextBox1.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial Black", 26.0!)
        Me.Label3.ForeColor = System.Drawing.Color.Aqua
        Me.Label3.Location = New System.Drawing.Point(31, 514)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(170, 50)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "K1A003"
        '
        'Timer2
        '
        '
        'lb_snp
        '
        Me.lb_snp.AutoSize = True
        Me.lb_snp.Font = New System.Drawing.Font("Arial Black", 20.0!)
        Me.lb_snp.ForeColor = System.Drawing.Color.DarkOrange
        Me.lb_snp.Location = New System.Drawing.Point(122, 514)
        Me.lb_snp.Name = "lb_snp"
        Me.lb_snp.Size = New System.Drawing.Size(70, 38)
        Me.lb_snp.TabIndex = 39
        Me.lb_snp.Text = "snp"
        Me.lb_snp.Visible = False
        '
        'lb_ct
        '
        Me.lb_ct.AutoSize = True
        Me.lb_ct.Font = New System.Drawing.Font("Arial Black", 20.0!)
        Me.lb_ct.ForeColor = System.Drawing.Color.DarkOrange
        Me.lb_ct.Location = New System.Drawing.Point(50, 526)
        Me.lb_ct.Name = "lb_ct"
        Me.lb_ct.Size = New System.Drawing.Size(47, 38)
        Me.lb_ct.TabIndex = 40
        Me.lb_ct.Text = "ct"
        Me.lb_ct.Visible = False
        '
        'lb_seq
        '
        Me.lb_seq.AutoSize = True
        Me.lb_seq.Font = New System.Drawing.Font("Arial Black", 20.0!)
        Me.lb_seq.ForeColor = System.Drawing.Color.DarkOrange
        Me.lb_seq.Location = New System.Drawing.Point(95, 526)
        Me.lb_seq.Name = "lb_seq"
        Me.lb_seq.Size = New System.Drawing.Size(70, 38)
        Me.lb_seq.TabIndex = 41
        Me.lb_seq.Text = "seq"
        Me.lb_seq.Visible = False
        '
        'lb_dlv_date
        '
        Me.lb_dlv_date.AutoSize = True
        Me.lb_dlv_date.Font = New System.Drawing.Font("Arial Black", 10.0!)
        Me.lb_dlv_date.ForeColor = System.Drawing.Color.DarkOrange
        Me.lb_dlv_date.Location = New System.Drawing.Point(622, 527)
        Me.lb_dlv_date.Name = "lb_dlv_date"
        Me.lb_dlv_date.Size = New System.Drawing.Size(72, 19)
        Me.lb_dlv_date.TabIndex = 42
        Me.lb_dlv_date.Text = "dlv_date"
        Me.lb_dlv_date.Visible = False
        '
        'lb_location
        '
        Me.lb_location.AutoSize = True
        Me.lb_location.Font = New System.Drawing.Font("Arial Black", 10.0!)
        Me.lb_location.ForeColor = System.Drawing.Color.DarkOrange
        Me.lb_location.Location = New System.Drawing.Point(618, 523)
        Me.lb_location.Name = "lb_location"
        Me.lb_location.Size = New System.Drawing.Size(70, 19)
        Me.lb_location.TabIndex = 43
        Me.lb_location.Text = "location"
        Me.lb_location.Visible = False
        '
        'lb_prd_type
        '
        Me.lb_prd_type.AutoSize = True
        Me.lb_prd_type.Font = New System.Drawing.Font("Arial Black", 10.0!)
        Me.lb_prd_type.ForeColor = System.Drawing.Color.DarkOrange
        Me.lb_prd_type.Location = New System.Drawing.Point(622, 515)
        Me.lb_prd_type.Name = "lb_prd_type"
        Me.lb_prd_type.Size = New System.Drawing.Size(73, 19)
        Me.lb_prd_type.TabIndex = 44
        Me.lb_prd_type.Text = "prd_type"
        Me.lb_prd_type.Visible = False
        '
        'lb_temp_txt
        '
        Me.lb_temp_txt.AutoSize = True
        Me.lb_temp_txt.Font = New System.Drawing.Font("Arial Black", 10.0!)
        Me.lb_temp_txt.ForeColor = System.Drawing.Color.DarkOrange
        Me.lb_temp_txt.Location = New System.Drawing.Point(621, 534)
        Me.lb_temp_txt.Name = "lb_temp_txt"
        Me.lb_temp_txt.Size = New System.Drawing.Size(75, 19)
        Me.lb_temp_txt.TabIndex = 45
        Me.lb_temp_txt.Text = "temp_txt"
        Me.lb_temp_txt.Visible = False
        '
        'lb_temp_line
        '
        Me.lb_temp_line.AutoSize = True
        Me.lb_temp_line.Font = New System.Drawing.Font("Arial Black", 20.0!)
        Me.lb_temp_line.ForeColor = System.Drawing.Color.DarkOrange
        Me.lb_temp_line.Location = New System.Drawing.Point(22, 527)
        Me.lb_temp_line.Name = "lb_temp_line"
        Me.lb_temp_line.Size = New System.Drawing.Size(187, 38)
        Me.lb_temp_line.TabIndex = 39
        Me.lb_temp_line.Text = "TEMP_LINE"
        Me.lb_temp_line.Visible = False
        '
        'Label22
        '
        Me.Label22.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Catamaran", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(622, 524)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(66, 16)
        Me.Label22.TabIndex = 0
        Me.Label22.Text = "Label2"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Catamaran", 25.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(574, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 37)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Label1"
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.Transparent
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Button4.Location = New System.Drawing.Point(15, 503)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(195, 75)
        Me.Button4.TabIndex = 18
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Transparent
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Button3.Location = New System.Drawing.Point(597, 502)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(195, 75)
        Me.Button3.TabIndex = 17
        Me.Button3.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Location = New System.Drawing.Point(408, 451)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(139, 131)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 41
        Me.PictureBox1.TabStop = False
        '
        'QTY_NC
        '
        Me.QTY_NC.AutoSize = True
        Me.QTY_NC.Font = New System.Drawing.Font("Arial Black", 10.0!)
        Me.QTY_NC.ForeColor = System.Drawing.Color.DarkOrange
        Me.QTY_NC.Location = New System.Drawing.Point(622, 527)
        Me.QTY_NC.Name = "QTY_NC"
        Me.QTY_NC.Size = New System.Drawing.Size(72, 19)
        Me.QTY_NC.TabIndex = 47
        Me.QTY_NC.Text = "QTY_NC"
        Me.QTY_NC.Visible = False
        '
        'QTY_NG
        '
        Me.QTY_NG.AutoSize = True
        Me.QTY_NG.Font = New System.Drawing.Font("Arial Black", 10.0!)
        Me.QTY_NG.ForeColor = System.Drawing.Color.DarkOrange
        Me.QTY_NG.Location = New System.Drawing.Point(632, 524)
        Me.QTY_NG.Name = "QTY_NG"
        Me.QTY_NG.Size = New System.Drawing.Size(73, 19)
        Me.QTY_NG.TabIndex = 48
        Me.QTY_NG.Text = "QTY_NG"
        Me.QTY_NG.Visible = False
        '
        'Timer3
        '
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial Black", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(580, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 15)
        Me.Label4.TabIndex = 4632
        Me.Label4.Text = "Label4"
        '
        'LB_PLAN_DATE
        '
        Me.LB_PLAN_DATE.AutoSize = True
        Me.LB_PLAN_DATE.BackColor = System.Drawing.Color.Transparent
        Me.LB_PLAN_DATE.Font = New System.Drawing.Font("Catamaran", 22.0!, System.Drawing.FontStyle.Bold)
        Me.LB_PLAN_DATE.ForeColor = System.Drawing.Color.White
        Me.LB_PLAN_DATE.Location = New System.Drawing.Point(445, 237)
        Me.LB_PLAN_DATE.Name = "LB_PLAN_DATE"
        Me.LB_PLAN_DATE.Size = New System.Drawing.Size(207, 33)
        Me.LB_PLAN_DATE.TabIndex = 4633
        Me.LB_PLAN_DATE.Text = "YYYY-MM-DD"
        '
        'LB_ShowWorker
        '
        Me.LB_ShowWorker.BackColor = System.Drawing.Color.Transparent
        Me.LB_ShowWorker.Location = New System.Drawing.Point(475, 12)
        Me.LB_ShowWorker.Name = "LB_ShowWorker"
        Me.LB_ShowWorker.Size = New System.Drawing.Size(88, 62)
        Me.LB_ShowWorker.TabIndex = 4634
        Me.LB_ShowWorker.TabStop = False
        '
        'picSpecial
        '
        Me.picSpecial.BackgroundImage = CType(resources.GetObject("picSpecial.BackgroundImage"), System.Drawing.Image)
        Me.picSpecial.Location = New System.Drawing.Point(0, 0)
        Me.picSpecial.Name = "picSpecial"
        Me.picSpecial.Size = New System.Drawing.Size(800, 600)
        Me.picSpecial.TabIndex = 4635
        Me.picSpecial.TabStop = False
        Me.picSpecial.Visible = False
        '
        'lvWISpc
        '
        Me.lvWISpc.AllowColumnReorder = True
        Me.lvWISpc.AllowDrop = True
        Me.lvWISpc.AutoArrange = False
        Me.lvWISpc.BackColor = System.Drawing.Color.White
        Me.lvWISpc.BackgroundImageTiled = True
        Me.lvWISpc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvWISpc.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NO, Me.WI, Me.PART_NO})
        Me.lvWISpc.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvWISpc.Font = New System.Drawing.Font("Catamaran", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvWISpc.ForeColor = System.Drawing.Color.Black
        Me.lvWISpc.FullRowSelect = True
        Me.lvWISpc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvWISpc.HideSelection = False
        Me.lvWISpc.HoverSelection = True
        Me.lvWISpc.Location = New System.Drawing.Point(37, 148)
        Me.lvWISpc.MultiSelect = False
        Me.lvWISpc.Name = "lvWISpc"
        Me.lvWISpc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lvWISpc.ShowGroups = False
        Me.lvWISpc.Size = New System.Drawing.Size(440, 148)
        Me.lvWISpc.TabIndex = 4636
        Me.lvWISpc.UseCompatibleStateImageBehavior = False
        Me.lvWISpc.View = System.Windows.Forms.View.Details
        Me.lvWISpc.Visible = False
        '
        'NO
        '
        Me.NO.Text = "N"
        Me.NO.Width = 40
        '
        'WI
        '
        Me.WI.Text = "WI"
        Me.WI.Width = 200
        '
        'PART_NO
        '
        Me.PART_NO.Text = "PART NO"
        Me.PART_NO.Width = 180
        '
        'btnUp
        '
        Me.btnUp.BackgroundImage = CType(resources.GetObject("btnUp.BackgroundImage"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(492, 112)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(71, 92)
        Me.btnUp.TabIndex = 4637
        Me.btnUp.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.BackgroundImage = CType(resources.GetObject("btnDown.BackgroundImage"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(492, 210)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(71, 92)
        Me.btnDown.TabIndex = 4638
        Me.btnDown.TabStop = False
        '
        'Prd_detail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.productionDetail
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.lvWISpc)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LB_ShowWorker)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lb_model)
        Me.Controls.Add(Me.lb_remain_qty)
        Me.Controls.Add(Me.lb_plan_qty)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.LB_PLAN_DATE)
        Me.Controls.Add(Me.picSpecial)
        Me.Controls.Add(Me.lb_item_name)
        Me.Controls.Add(Me.lb_wi)
        Me.Controls.Add(Me.lb_item_cd)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lb_temp_txt)
        Me.Controls.Add(Me.QTY_NG)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.QTY_NC)
        Me.Controls.Add(Me.lb_prd_type)
        Me.Controls.Add(Me.lb_location)
        Me.Controls.Add(Me.lb_dlv_date)
        Me.Controls.Add(Me.lb_seq)
        Me.Controls.Add(Me.lb_ct)
        Me.Controls.Add(Me.lb_snp)
        Me.Controls.Add(Me.lb_temp_line)
        Me.Controls.Add(Me.Label22)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Prd_detail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Prd_detail"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LB_ShowWorker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picSpecial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Label22 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Label2 As Label
    Friend WithEvents lb_remain_qty As Label
    Friend WithEvents lb_plan_qty As Label
    Friend WithEvents lb_wi As Label
    Friend WithEvents lb_model As Label
    Friend WithEvents lb_item_name As Label
    Friend WithEvents lb_item_cd As Label
    Friend WithEvents lb_snp As Label
    Friend WithEvents lb_ct As Label
    Friend WithEvents lb_seq As Label
    Friend WithEvents lb_dlv_date As Label
    Friend WithEvents lb_location As Label
    Friend WithEvents lb_prd_type As Label
    Friend WithEvents lb_temp_txt As Label
    Friend WithEvents lb_temp_line As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents QTY_NC As Label
    Friend WithEvents QTY_NG As Label
    Friend WithEvents Timer3 As Timer
    Friend WithEvents Label4 As Label
    Friend WithEvents LB_PLAN_DATE As Label
    Friend WithEvents LB_ShowWorker As PictureBox
    Friend WithEvents picSpecial As PictureBox
    Friend WithEvents lvWISpc As ListView
    Friend WithEvents NO As ColumnHeader
    Friend WithEvents WI As ColumnHeader
    Friend WithEvents PART_NO As ColumnHeader
    Friend WithEvents btnUp As PictureBox
    Friend WithEvents btnDown As PictureBox
End Class
