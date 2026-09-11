<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TEST_PRINTLABEL
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TEST_PRINTLABEL))
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.RichTextBoxPart = New System.Windows.Forms.RichTextBox()
        Me.WidthPart = New System.Windows.Forms.TextBox()
        Me.HeightPart = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PrintDocument31 = New System.Drawing.Printing.PrintDocument()
        Me.PrintDocument32 = New System.Drawing.Printing.PrintDocument()
        Me.comboFontPart = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.fontSizePart = New System.Windows.Forms.TextBox()
        Me.logoLabel = New System.Windows.Forms.PictureBox()
        Me.picBarcode = New System.Windows.Forms.PictureBox()
        Me.lbPartMmodelBreak = New System.Windows.Forms.Label()
        Me.lb_font2 = New System.Windows.Forms.Label()
        Me.lbQrCode = New System.Windows.Forms.Label()
        Me.lbPartNumber = New System.Windows.Forms.Label()
        Me.lbButton = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.RichTextBoxModel = New System.Windows.Forms.RichTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ComboFontModel = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.fontSizeModel = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.WidthModel = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.HeightModel = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.RichTextBoxFooter = New System.Windows.Forms.RichTextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ComboBoxFontFooter = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.fontSizeFooter = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.WidthFooter = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.HeightFooter = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.RichTextBoxBarcode = New System.Windows.Forms.RichTextBox()
        Me.HeightBarcode = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.WidthBarcode = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.WidthBarcode2 = New System.Windows.Forms.TextBox()
        Me.WC = New System.Windows.Forms.Label()
        Me.HeightBarcode2 = New System.Windows.Forms.TextBox()
        Me.HC = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        CType(Me.logoLabel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PrintButton
        '
        Me.PrintButton.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.PrintButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!)
        Me.PrintButton.Location = New System.Drawing.Point(181, 94)
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.Size = New System.Drawing.Size(192, 82)
        Me.PrintButton.TabIndex = 0
        Me.PrintButton.Text = "PRINT"
        Me.PrintButton.UseVisualStyleBackColor = False
        '
        'RichTextBoxPart
        '
        Me.RichTextBoxPart.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxPart.Location = New System.Drawing.Point(129, 12)
        Me.RichTextBoxPart.Name = "RichTextBoxPart"
        Me.RichTextBoxPart.Size = New System.Drawing.Size(263, 90)
        Me.RichTextBoxPart.TabIndex = 1
        Me.RichTextBoxPart.Text = "--"
        '
        'WidthPart
        '
        Me.WidthPart.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WidthPart.Location = New System.Drawing.Point(67, 207)
        Me.WidthPart.Name = "WidthPart"
        Me.WidthPart.Size = New System.Drawing.Size(84, 29)
        Me.WidthPart.TabIndex = 2
        Me.WidthPart.Text = "20"
        '
        'HeightPart
        '
        Me.HeightPart.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.HeightPart.Location = New System.Drawing.Point(231, 207)
        Me.HeightPart.Name = "HeightPart"
        Me.HeightPart.Size = New System.Drawing.Size(96, 29)
        Me.HeightPart.TabIndex = 3
        Me.HeightPart.Text = "10"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(768, 529)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 37)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Label1"
        '
        'comboFontPart
        '
        Me.comboFontPart.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboFontPart.FormattingEnabled = True
        Me.comboFontPart.Location = New System.Drawing.Point(151, 119)
        Me.comboFontPart.Name = "comboFontPart"
        Me.comboFontPart.Size = New System.Drawing.Size(241, 33)
        Me.comboFontPart.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 119)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 24)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Name Font :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(38, 162)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 24)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "font size :"
        '
        'fontSizePart
        '
        Me.fontSizePart.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!)
        Me.fontSizePart.Location = New System.Drawing.Point(151, 162)
        Me.fontSizePart.Name = "fontSizePart"
        Me.fontSizePart.Size = New System.Drawing.Size(197, 38)
        Me.fontSizePart.TabIndex = 9
        Me.fontSizePart.Text = "25"
        '
        'logoLabel
        '
        Me.logoLabel.Image = CType(resources.GetObject("logoLabel.Image"), System.Drawing.Image)
        Me.logoLabel.Location = New System.Drawing.Point(796, 479)
        Me.logoLabel.Name = "logoLabel"
        Me.logoLabel.Size = New System.Drawing.Size(28, 25)
        Me.logoLabel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.logoLabel.TabIndex = 4592
        Me.logoLabel.TabStop = False
        '
        'picBarcode
        '
        Me.picBarcode.Image = CType(resources.GetObject("picBarcode.Image"), System.Drawing.Image)
        Me.picBarcode.Location = New System.Drawing.Point(725, 548)
        Me.picBarcode.Name = "picBarcode"
        Me.picBarcode.Size = New System.Drawing.Size(65, 41)
        Me.picBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picBarcode.TabIndex = 4593
        Me.picBarcode.TabStop = False
        '
        'lbPartMmodelBreak
        '
        Me.lbPartMmodelBreak.AutoSize = True
        Me.lbPartMmodelBreak.Font = New System.Drawing.Font("Bell MT", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPartMmodelBreak.Location = New System.Drawing.Point(414, 529)
        Me.lbPartMmodelBreak.Name = "lbPartMmodelBreak"
        Me.lbPartMmodelBreak.Size = New System.Drawing.Size(255, 31)
        Me.lbPartMmodelBreak.TabIndex = 4599
        Me.lbPartMmodelBreak.Text = "lbPartMmodelBreak"
        '
        'lb_font2
        '
        Me.lb_font2.AutoSize = True
        Me.lb_font2.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_font2.Location = New System.Drawing.Point(703, 533)
        Me.lb_font2.Name = "lb_font2"
        Me.lb_font2.Size = New System.Drawing.Size(61, 23)
        Me.lb_font2.TabIndex = 4598
        Me.lb_font2.Text = "Label7"
        Me.lb_font2.Visible = False
        '
        'lbQrCode
        '
        Me.lbQrCode.AutoSize = True
        Me.lbQrCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lbQrCode.Location = New System.Drawing.Point(690, 465)
        Me.lbQrCode.Name = "lbQrCode"
        Me.lbQrCode.Size = New System.Drawing.Size(100, 24)
        Me.lbQrCode.TabIndex = 4597
        Me.lbQrCode.Text = "lbQrCode"
        '
        'lbPartNumber
        '
        Me.lbPartNumber.AutoSize = True
        Me.lbPartNumber.Font = New System.Drawing.Font("Arial Narrow", 20.0!, System.Drawing.FontStyle.Bold)
        Me.lbPartNumber.Location = New System.Drawing.Point(717, 411)
        Me.lbPartNumber.Name = "lbPartNumber"
        Me.lbPartNumber.Size = New System.Drawing.Size(162, 31)
        Me.lbPartNumber.TabIndex = 4596
        Me.lbPartNumber.Text = "lbPartNumber"
        '
        'lbButton
        '
        Me.lbButton.AutoSize = True
        Me.lbButton.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbButton.Location = New System.Drawing.Point(798, 529)
        Me.lbButton.Name = "lbButton"
        Me.lbButton.Size = New System.Drawing.Size(81, 19)
        Me.lbButton.TabIndex = 4595
        Me.lbButton.Text = "lbButton"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(33, 210)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 24)
        Me.Label4.TabIndex = 4600
        Me.Label4.Text = "W"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(201, 210)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 24)
        Me.Label5.TabIndex = 4601
        Me.Label5.Text = "H"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(7, 37)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(116, 24)
        Me.Label6.TabIndex = 4602
        Me.Label6.Text = "Part Number"
        '
        'RichTextBoxModel
        '
        Me.RichTextBoxModel.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxModel.Location = New System.Drawing.Point(609, 12)
        Me.RichTextBoxModel.Name = "RichTextBoxModel"
        Me.RichTextBoxModel.Size = New System.Drawing.Size(263, 90)
        Me.RichTextBoxModel.TabIndex = 4603
        Me.RichTextBoxModel.Text = "--"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(473, 37)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(116, 24)
        Me.Label7.TabIndex = 4604
        Me.Label7.Text = "Model Break"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(475, 123)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(114, 24)
        Me.Label8.TabIndex = 4605
        Me.Label8.Text = "Name Font :"
        '
        'ComboFontModel
        '
        Me.ComboFontModel.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboFontModel.FormattingEnabled = True
        Me.ComboFontModel.Location = New System.Drawing.Point(609, 123)
        Me.ComboFontModel.Name = "ComboFontModel"
        Me.ComboFontModel.Size = New System.Drawing.Size(263, 33)
        Me.ComboFontModel.TabIndex = 4606
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(501, 176)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 24)
        Me.Label9.TabIndex = 4607
        Me.Label9.Text = "font size :"
        '
        'fontSizeModel
        '
        Me.fontSizeModel.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!)
        Me.fontSizeModel.Location = New System.Drawing.Point(609, 166)
        Me.fontSizeModel.Name = "fontSizeModel"
        Me.fontSizeModel.Size = New System.Drawing.Size(197, 38)
        Me.fontSizeModel.TabIndex = 4608
        Me.fontSizeModel.Text = "26"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(501, 212)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(28, 24)
        Me.Label10.TabIndex = 4609
        Me.Label10.Text = "W"
        '
        'WidthModel
        '
        Me.WidthModel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WidthModel.Location = New System.Drawing.Point(539, 212)
        Me.WidthModel.Name = "WidthModel"
        Me.WidthModel.Size = New System.Drawing.Size(84, 29)
        Me.WidthModel.TabIndex = 4610
        Me.WidthModel.Text = "195"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(664, 210)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 24)
        Me.Label11.TabIndex = 4611
        Me.Label11.Text = "H"
        '
        'HeightModel
        '
        Me.HeightModel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.HeightModel.Location = New System.Drawing.Point(694, 212)
        Me.HeightModel.Name = "HeightModel"
        Me.HeightModel.Size = New System.Drawing.Size(96, 29)
        Me.HeightModel.TabIndex = 4612
        Me.HeightModel.Text = "10"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(24, 317)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 24)
        Me.Label12.TabIndex = 4613
        Me.Label12.Text = "Footer"
        '
        'RichTextBoxFooter
        '
        Me.RichTextBoxFooter.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxFooter.Location = New System.Drawing.Point(95, 292)
        Me.RichTextBoxFooter.Name = "RichTextBoxFooter"
        Me.RichTextBoxFooter.Size = New System.Drawing.Size(297, 90)
        Me.RichTextBoxFooter.TabIndex = 4614
        Me.RichTextBoxFooter.Text = "--"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(12, 417)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(114, 24)
        Me.Label13.TabIndex = 4615
        Me.Label13.Text = "Name Font :"
        '
        'ComboBoxFontFooter
        '
        Me.ComboBoxFontFooter.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxFontFooter.FormattingEnabled = True
        Me.ComboBoxFontFooter.Location = New System.Drawing.Point(151, 409)
        Me.ComboBoxFontFooter.Name = "ComboBoxFontFooter"
        Me.ComboBoxFontFooter.Size = New System.Drawing.Size(241, 33)
        Me.ComboBoxFontFooter.TabIndex = 4616
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(38, 476)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(88, 24)
        Me.Label14.TabIndex = 4617
        Me.Label14.Text = "font size :"
        '
        'fontSizeFooter
        '
        Me.fontSizeFooter.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!)
        Me.fontSizeFooter.Location = New System.Drawing.Point(151, 466)
        Me.fontSizeFooter.Name = "fontSizeFooter"
        Me.fontSizeFooter.Size = New System.Drawing.Size(197, 38)
        Me.fontSizeFooter.TabIndex = 4618
        Me.fontSizeFooter.Text = "12"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(38, 524)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(28, 24)
        Me.Label15.TabIndex = 4619
        Me.Label15.Text = "W"
        '
        'WidthFooter
        '
        Me.WidthFooter.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WidthFooter.Location = New System.Drawing.Point(72, 521)
        Me.WidthFooter.Name = "WidthFooter"
        Me.WidthFooter.Size = New System.Drawing.Size(84, 29)
        Me.WidthFooter.TabIndex = 4620
        Me.WidthFooter.Text = "0"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(201, 521)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(24, 24)
        Me.Label16.TabIndex = 4621
        Me.Label16.Text = "H"
        '
        'HeightFooter
        '
        Me.HeightFooter.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.HeightFooter.Location = New System.Drawing.Point(231, 518)
        Me.HeightFooter.Name = "HeightFooter"
        Me.HeightFooter.Size = New System.Drawing.Size(96, 29)
        Me.HeightFooter.TabIndex = 4622
        Me.HeightFooter.Text = "0"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(501, 317)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(81, 24)
        Me.Label17.TabIndex = 4623
        Me.Label17.Text = "Barcode"
        '
        'RichTextBoxBarcode
        '
        Me.RichTextBoxBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxBarcode.Location = New System.Drawing.Point(588, 292)
        Me.RichTextBoxBarcode.Name = "RichTextBoxBarcode"
        Me.RichTextBoxBarcode.Size = New System.Drawing.Size(297, 90)
        Me.RichTextBoxBarcode.TabIndex = 4624
        Me.RichTextBoxBarcode.Text = "--"
        '
        'HeightBarcode
        '
        Me.HeightBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.HeightBarcode.Location = New System.Drawing.Point(751, 395)
        Me.HeightBarcode.Name = "HeightBarcode"
        Me.HeightBarcode.Size = New System.Drawing.Size(96, 29)
        Me.HeightBarcode.TabIndex = 4628
        Me.HeightBarcode.Text = "0"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(721, 398)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(24, 24)
        Me.Label18.TabIndex = 4627
        Me.Label18.Text = "H"
        '
        'WidthBarcode
        '
        Me.WidthBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WidthBarcode.Location = New System.Drawing.Point(590, 395)
        Me.WidthBarcode.Name = "WidthBarcode"
        Me.WidthBarcode.Size = New System.Drawing.Size(84, 29)
        Me.WidthBarcode.TabIndex = 4626
        Me.WidthBarcode.Text = "0"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(556, 400)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(28, 24)
        Me.Label19.TabIndex = 4625
        Me.Label19.Text = "W"
        '
        'WidthBarcode2
        '
        Me.WidthBarcode2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WidthBarcode2.Location = New System.Drawing.Point(590, 451)
        Me.WidthBarcode2.Name = "WidthBarcode2"
        Me.WidthBarcode2.Size = New System.Drawing.Size(84, 29)
        Me.WidthBarcode2.TabIndex = 4630
        Me.WidthBarcode2.Text = "0"
        '
        'WC
        '
        Me.WC.AutoSize = True
        Me.WC.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WC.Location = New System.Drawing.Point(550, 455)
        Me.WC.Name = "WC"
        Me.WC.Size = New System.Drawing.Size(41, 24)
        Me.WC.TabIndex = 4629
        Me.WC.Text = "WC"
        '
        'HeightBarcode2
        '
        Me.HeightBarcode2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.HeightBarcode2.Location = New System.Drawing.Point(751, 447)
        Me.HeightBarcode2.Name = "HeightBarcode2"
        Me.HeightBarcode2.Size = New System.Drawing.Size(96, 29)
        Me.HeightBarcode2.TabIndex = 4632
        Me.HeightBarcode2.Text = "0"
        '
        'HC
        '
        Me.HC.AutoSize = True
        Me.HC.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HC.Location = New System.Drawing.Point(708, 450)
        Me.HC.Name = "HC"
        Me.HC.Size = New System.Drawing.Size(37, 24)
        Me.HC.TabIndex = 4631
        Me.HC.Text = "HC"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PrintButton)
        Me.Panel1.Location = New System.Drawing.Point(407, 404)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(485, 185)
        Me.Panel1.TabIndex = 4633
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"B", "R"})
        Me.ComboBox1.Location = New System.Drawing.Point(67, 242)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(71, 41)
        Me.ComboBox1.TabIndex = 4634
        '
        'ComboBox2
        '
        Me.ComboBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"B", "R"})
        Me.ComboBox2.Location = New System.Drawing.Point(337, 519)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(71, 41)
        Me.ComboBox2.TabIndex = 4635
        '
        'TEST_PRINTLABEL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(891, 582)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.WidthBarcode2)
        Me.Controls.Add(Me.WC)
        Me.Controls.Add(Me.HeightBarcode2)
        Me.Controls.Add(Me.HC)
        Me.Controls.Add(Me.HeightBarcode)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.WidthBarcode)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RichTextBoxBarcode)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.HeightFooter)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.WidthFooter)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.fontSizeFooter)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.ComboBoxFontFooter)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.RichTextBoxFooter)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.HeightModel)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.WidthModel)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.fontSizeModel)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ComboFontModel)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.RichTextBoxModel)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lbPartMmodelBreak)
        Me.Controls.Add(Me.lb_font2)
        Me.Controls.Add(Me.lbQrCode)
        Me.Controls.Add(Me.lbPartNumber)
        Me.Controls.Add(Me.lbButton)
        Me.Controls.Add(Me.picBarcode)
        Me.Controls.Add(Me.logoLabel)
        Me.Controls.Add(Me.fontSizePart)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.comboFontPart)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.HeightPart)
        Me.Controls.Add(Me.WidthPart)
        Me.Controls.Add(Me.RichTextBoxPart)
        Me.Name = "TEST_PRINTLABEL"
        Me.Text = "Form1"
        CType(Me.logoLabel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PrintButton As Button
    Friend WithEvents RichTextBoxPart As RichTextBox
    Friend WithEvents WidthPart As TextBox
    Friend WithEvents HeightPart As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PrintDocument31 As Printing.PrintDocument
    Friend WithEvents PrintDocument32 As Printing.PrintDocument
    Friend WithEvents comboFontPart As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents fontSizePart As TextBox
    Friend WithEvents logoLabel As PictureBox
    Friend WithEvents picBarcode As PictureBox
    Friend WithEvents lbPartMmodelBreak As Label
    Friend WithEvents lb_font2 As Label
    Friend WithEvents lbQrCode As Label
    Friend WithEvents lbPartNumber As Label
    Friend WithEvents lbButton As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents RichTextBoxModel As RichTextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents ComboFontModel As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents fontSizeModel As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents WidthModel As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents HeightModel As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents RichTextBoxFooter As RichTextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents ComboBoxFontFooter As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents fontSizeFooter As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents WidthFooter As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents HeightFooter As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents RichTextBoxBarcode As RichTextBox
    Friend WithEvents HeightBarcode As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents WidthBarcode As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents WidthBarcode2 As TextBox
    Friend WithEvents WC As Label
    Friend WithEvents HeightBarcode2 As TextBox
    Friend WithEvents HC As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents ComboBox2 As ComboBox
End Class
