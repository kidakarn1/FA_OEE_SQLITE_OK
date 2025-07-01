<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Line_conf
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
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.delay_sec = New System.Windows.Forms.ComboBox()
        Me.ComboBox_master_device = New System.Windows.Forms.ComboBox()
        Me.DIO_PORT = New System.Windows.Forms.ComboBox()
        Me.printer = New System.Windows.Forms.ComboBox()
        Me.scanner = New System.Windows.Forms.ComboBox()
        Me.type_counter = New System.Windows.Forms.ComboBox()
        Me.combo_cavity = New System.Windows.Forms.ComboBox()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.PictureBox16 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.tower_lamp = New System.Windows.Forms.ComboBox()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox16, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label22
        '
        Me.Label22.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial Black", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(600, 517)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(84, 28)
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
        Me.Label1.Location = New System.Drawing.Point(590, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 37)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Label1"
        '
        'delay_sec
        '
        Me.delay_sec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.delay_sec.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.delay_sec.Font = New System.Drawing.Font("Catamaran", 22.0!, System.Drawing.FontStyle.Bold)
        Me.delay_sec.FormattingEnabled = True
        Me.delay_sec.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20"})
        Me.delay_sec.Location = New System.Drawing.Point(182, 423)
        Me.delay_sec.Name = "delay_sec"
        Me.delay_sec.Size = New System.Drawing.Size(180, 40)
        Me.delay_sec.TabIndex = 43
        '
        'ComboBox_master_device
        '
        Me.ComboBox_master_device.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_master_device.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox_master_device.Font = New System.Drawing.Font("Catamaran", 22.0!, System.Drawing.FontStyle.Bold)
        Me.ComboBox_master_device.FormattingEnabled = True
        Me.ComboBox_master_device.Items.AddRange(New Object() {"DIO000", "DIO001", "DIO002", "DIO003", "DIO004", "DIO005", "DIO006", "DIO007", "DIO008", "DIO009"})
        Me.ComboBox_master_device.Location = New System.Drawing.Point(180, 263)
        Me.ComboBox_master_device.Name = "ComboBox_master_device"
        Me.ComboBox_master_device.Size = New System.Drawing.Size(183, 40)
        Me.ComboBox_master_device.TabIndex = 32
        '
        'DIO_PORT
        '
        Me.DIO_PORT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DIO_PORT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DIO_PORT.Font = New System.Drawing.Font("Catamaran", 22.0!, System.Drawing.FontStyle.Bold)
        Me.DIO_PORT.FormattingEnabled = True
        Me.DIO_PORT.Items.AddRange(New Object() {"DIO000", "DIO001", "DIO002", "DIO003", "DIO004", "DIO005", "DIO006", "DIO007", "DIO008", "DIO009"})
        Me.DIO_PORT.Location = New System.Drawing.Point(567, 263)
        Me.DIO_PORT.Name = "DIO_PORT"
        Me.DIO_PORT.Size = New System.Drawing.Size(183, 40)
        Me.DIO_PORT.TabIndex = 30
        '
        'printer
        '
        Me.printer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.printer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.printer.Font = New System.Drawing.Font("Catamaran", 22.0!, System.Drawing.FontStyle.Bold)
        Me.printer.FormattingEnabled = True
        Me.printer.Items.AddRange(New Object() {"USB", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9"})
        Me.printer.Location = New System.Drawing.Point(568, 185)
        Me.printer.Name = "printer"
        Me.printer.Size = New System.Drawing.Size(182, 40)
        Me.printer.TabIndex = 28
        '
        'scanner
        '
        Me.scanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.scanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.scanner.Font = New System.Drawing.Font("Catamaran", 22.0!, System.Drawing.FontStyle.Bold)
        Me.scanner.FormattingEnabled = True
        Me.scanner.Items.AddRange(New Object() {"USB", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9"})
        Me.scanner.Location = New System.Drawing.Point(568, 109)
        Me.scanner.Name = "scanner"
        Me.scanner.Size = New System.Drawing.Size(182, 40)
        Me.scanner.TabIndex = 26
        '
        'type_counter
        '
        Me.type_counter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.type_counter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.type_counter.Font = New System.Drawing.Font("Catamaran", 22.0!, System.Drawing.FontStyle.Bold)
        Me.type_counter.FormattingEnabled = True
        Me.type_counter.Items.AddRange(New Object() {"BUTTON", "TOUCH", "AUTO"})
        Me.type_counter.Location = New System.Drawing.Point(568, 341)
        Me.type_counter.Name = "type_counter"
        Me.type_counter.Size = New System.Drawing.Size(182, 40)
        Me.type_counter.TabIndex = 24
        '
        'combo_cavity
        '
        Me.combo_cavity.DropDownHeight = 110
        Me.combo_cavity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.combo_cavity.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_cavity.Font = New System.Drawing.Font("Catamaran", 22.0!, System.Drawing.FontStyle.Bold)
        Me.combo_cavity.FormattingEnabled = True
        Me.combo_cavity.IntegralHeight = False
        Me.combo_cavity.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20"})
        Me.combo_cavity.Location = New System.Drawing.Point(183, 341)
        Me.combo_cavity.Name = "combo_cavity"
        Me.combo_cavity.Size = New System.Drawing.Size(180, 40)
        Me.combo_cavity.TabIndex = 23
        '
        'ComboBox3
        '
        Me.ComboBox3.BackColor = System.Drawing.Color.White
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox3.Font = New System.Drawing.Font("Catamaran", 22.0!, System.Drawing.FontStyle.Bold)
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Items.AddRange(New Object() {"K1PL00", "K1PD01", "K1PD02", "K1PD03", "K1PD04", "K1PD05", "K1PD07", "K2PD06", "K2PL00", "K1PE00", "K2PE00"})
        Me.ComboBox3.Location = New System.Drawing.Point(183, 107)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(180, 40)
        Me.ComboBox3.TabIndex = 21
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownHeight = 320
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox2.Font = New System.Drawing.Font("Catamaran", 22.0!, System.Drawing.FontStyle.Bold)
        Me.ComboBox2.ForeColor = System.Drawing.Color.Black
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.IntegralHeight = False
        Me.ComboBox2.Location = New System.Drawing.Point(182, 184)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(180, 40)
        Me.ComboBox2.TabIndex = 20
        '
        'Timer2
        '
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.Transparent
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Button4.Location = New System.Drawing.Point(14, 491)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(200, 93)
        Me.Button4.TabIndex = 20
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Transparent
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Button3.Location = New System.Drawing.Point(585, 488)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(203, 93)
        Me.Button3.TabIndex = 19
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial Black", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(596, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 15)
        Me.Label3.TabIndex = 4632
        Me.Label3.Text = "Label3"
        '
        'PictureBox6
        '
        Me.PictureBox6.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox6.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.sweetAlertSuccess
        Me.PictureBox6.Location = New System.Drawing.Point(245, 110)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(310, 380)
        Me.PictureBox6.TabIndex = 4633
        Me.PictureBox6.TabStop = False
        Me.PictureBox6.Visible = False
        '
        'PictureBox16
        '
        Me.PictureBox16.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.btnOK
        Me.PictureBox16.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox16.Location = New System.Drawing.Point(329, 419)
        Me.PictureBox16.Name = "PictureBox16"
        Me.PictureBox16.Size = New System.Drawing.Size(145, 61)
        Me.PictureBox16.TabIndex = 4653
        Me.PictureBox16.TabStop = False
        Me.PictureBox16.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("Catamaran", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(275, 35)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "XXXXX"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label2.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Location = New System.Drawing.Point(251, 333)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(298, 81)
        Me.Panel2.TabIndex = 4652
        Me.Panel2.Visible = False
        '
        'tower_lamp
        '
        Me.tower_lamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tower_lamp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tower_lamp.Font = New System.Drawing.Font("Catamaran", 22.0!, System.Drawing.FontStyle.Bold)
        Me.tower_lamp.FormattingEnabled = True
        Me.tower_lamp.Items.AddRange(New Object() {"BUTTON", "TOUCH", "AUTO"})
        Me.tower_lamp.Location = New System.Drawing.Point(568, 423)
        Me.tower_lamp.Name = "tower_lamp"
        Me.tower_lamp.Size = New System.Drawing.Size(182, 40)
        Me.tower_lamp.TabIndex = 4654
        '
        'Line_conf
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.BackgroundImage = Global.TBK_FA_System.My.Resources.Resources.configLineGemba
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.ControlBox = False
        Me.Controls.Add(Me.tower_lamp)
        Me.Controls.Add(Me.PictureBox16)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.delay_sec)
        Me.Controls.Add(Me.ComboBox_master_device)
        Me.Controls.Add(Me.DIO_PORT)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.type_counter)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.combo_cavity)
        Me.Controls.Add(Me.printer)
        Me.Controls.Add(Me.ComboBox3)
        Me.Controls.Add(Me.scanner)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.Label22)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Line_conf"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Line_conf"
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox16, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label22 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents ComboBox2 As ComboBox
    Public WithEvents combo_cavity As ComboBox
    Public WithEvents type_counter As ComboBox
    Friend WithEvents DIO_PORT As ComboBox
    Friend WithEvents printer As ComboBox
    Friend WithEvents scanner As ComboBox
    Friend WithEvents ComboBox_master_device As ComboBox
    Public WithEvents delay_sec As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents PictureBox16 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel2 As Panel
    Public WithEvents tower_lamp As ComboBox
End Class
