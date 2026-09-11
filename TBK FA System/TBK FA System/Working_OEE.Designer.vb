<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Working_OEE
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Working_OEE))
        Me.lbPartNo = New System.Windows.Forms.Label()
        Me.lbPartName = New System.Windows.Forms.Label()
        Me.lbLine = New System.Windows.Forms.Label()
        Me.lbShiftPlan = New System.Windows.Forms.Label()
        Me.lbShiftQty = New System.Windows.Forms.Label()
        Me.lbOverTimeAvailability = New System.Windows.Forms.Label()
        Me.lbOverTimeQuality = New System.Windows.Forms.Label()
        Me.lbOverTimePerformance = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lvDetailAvailabillty = New System.Windows.Forms.ListView()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.CircularProgressBar2 = New CircularProgressBar.CircularProgressBar()
        Me.CircularProgressBar1 = New CircularProgressBar.CircularProgressBar()
        Me.CircularProgressBar3 = New CircularProgressBar.CircularProgressBar()
        Me.CircularProgressBar4 = New CircularProgressBar.CircularProgressBar()
        Me.lbAccTarget = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.WebView21 = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.btnMenu = New System.Windows.Forms.PictureBox()
        Me.PanelMenu = New System.Windows.Forms.Panel()
        Me.btnDefect = New System.Windows.Forms.PictureBox()
        Me.btnInc = New System.Windows.Forms.PictureBox()
        Me.btnDesc = New System.Windows.Forms.PictureBox()
        Me.btnStop = New System.Windows.Forms.PictureBox()
        Me.BtnStart = New System.Windows.Forms.PictureBox()
        Me.btnInfo = New System.Windows.Forms.PictureBox()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintDocument2 = New System.Drawing.Printing.PrintDocument()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_new_dio = New System.Windows.Forms.Timer(Me.components)
        Me.TIME_CAL_EFF = New System.Windows.Forms.Timer(Me.components)
        Me.SerialPortLamp = New System.IO.Ports.SerialPort(Me.components)
        Me.picStart = New System.Windows.Forms.PictureBox()
        Me.picMenuAll = New System.Windows.Forms.PictureBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label9_fontModel = New System.Windows.Forms.Label()
        Me.lbPosition2 = New System.Windows.Forms.Label()
        Me.lbPosition1 = New System.Windows.Forms.Label()
        Me.LB_IND_ROW = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.wi_no = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lb_dlv_date = New System.Windows.Forms.Label()
        Me.lb_model = New System.Windows.Forms.Label()
        Me.lb_location = New System.Windows.Forms.Label()
        Me.lb_prd_type = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.LB_COUNTER_SHIP = New System.Windows.Forms.Label()
        Me.LB_COUNTER_SEQ = New System.Windows.Forms.Label()
        Me.lb_good = New System.Windows.Forms.Label()
        Me.lb_ng_qty = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lb_nc_qty = New System.Windows.Forms.Label()
        Me.st_count_ct = New System.Windows.Forms.Label()
        Me.st_time = New System.Windows.Forms.Label()
        Me.lb_qty_for_box = New System.Windows.Forms.Label()
        CType(Me.WebView21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelMenu.SuspendLayout()
        CType(Me.btnDefect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnInc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnStop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picMenuAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbPartNo
        '
        Me.lbPartNo.AutoSize = True
        Me.lbPartNo.BackColor = System.Drawing.Color.Transparent
        Me.lbPartNo.Font = New System.Drawing.Font("Panton Narrow-Trial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPartNo.ForeColor = System.Drawing.Color.White
        Me.lbPartNo.Location = New System.Drawing.Point(151, 24)
        Me.lbPartNo.Name = "lbPartNo"
        Me.lbPartNo.Size = New System.Drawing.Size(220, 26)
        Me.lbPartNo.TabIndex = 0
        Me.lbPartNo.Text = "XXXXXXXXXXXXXXXX"
        '
        'lbPartName
        '
        Me.lbPartName.AutoSize = True
        Me.lbPartName.BackColor = System.Drawing.Color.Transparent
        Me.lbPartName.Font = New System.Drawing.Font("Panton Narrow-Trial", 15.75!, System.Drawing.FontStyle.Bold)
        Me.lbPartName.ForeColor = System.Drawing.Color.White
        Me.lbPartName.Location = New System.Drawing.Point(151, 70)
        Me.lbPartName.Name = "lbPartName"
        Me.lbPartName.Size = New System.Drawing.Size(220, 26)
        Me.lbPartName.TabIndex = 1
        Me.lbPartName.Text = "XXXXXXXXXXXXXXXX"
        '
        'lbLine
        '
        Me.lbLine.AutoSize = True
        Me.lbLine.BackColor = System.Drawing.Color.Transparent
        Me.lbLine.Font = New System.Drawing.Font("Panton Narrow-Trial ExtraBold", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLine.ForeColor = System.Drawing.Color.White
        Me.lbLine.Location = New System.Drawing.Point(7, 52)
        Me.lbLine.Name = "lbLine"
        Me.lbLine.Size = New System.Drawing.Size(125, 38)
        Me.lbLine.TabIndex = 2
        Me.lbLine.Text = "XXXXXX"
        '
        'lbShiftPlan
        '
        Me.lbShiftPlan.AutoSize = True
        Me.lbShiftPlan.BackColor = System.Drawing.Color.Transparent
        Me.lbShiftPlan.Font = New System.Drawing.Font("Panton Narrow-Trial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbShiftPlan.ForeColor = System.Drawing.Color.White
        Me.lbShiftPlan.Location = New System.Drawing.Point(418, 25)
        Me.lbShiftPlan.Name = "lbShiftPlan"
        Me.lbShiftPlan.Size = New System.Drawing.Size(84, 25)
        Me.lbShiftPlan.TabIndex = 3
        Me.lbShiftPlan.Text = "XXXXXX"
        '
        'lbShiftQty
        '
        Me.lbShiftQty.AutoSize = True
        Me.lbShiftQty.BackColor = System.Drawing.Color.Transparent
        Me.lbShiftQty.Font = New System.Drawing.Font("Panton Narrow-Trial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.lbShiftQty.ForeColor = System.Drawing.Color.White
        Me.lbShiftQty.Location = New System.Drawing.Point(419, 69)
        Me.lbShiftQty.Name = "lbShiftQty"
        Me.lbShiftQty.Size = New System.Drawing.Size(83, 30)
        Me.lbShiftQty.TabIndex = 4
        Me.lbShiftQty.Text = "99999"
        '
        'lbOverTimeAvailability
        '
        Me.lbOverTimeAvailability.AutoSize = True
        Me.lbOverTimeAvailability.BackColor = System.Drawing.Color.Transparent
        Me.lbOverTimeAvailability.Font = New System.Drawing.Font("Panton Narrow-Trial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.lbOverTimeAvailability.ForeColor = System.Drawing.Color.Red
        Me.lbOverTimeAvailability.Location = New System.Drawing.Point(39, 163)
        Me.lbOverTimeAvailability.Name = "lbOverTimeAvailability"
        Me.lbOverTimeAvailability.Size = New System.Drawing.Size(55, 30)
        Me.lbOverTimeAvailability.TabIndex = 5
        Me.lbOverTimeAvailability.Text = "999"
        '
        'lbOverTimeQuality
        '
        Me.lbOverTimeQuality.AutoSize = True
        Me.lbOverTimeQuality.BackColor = System.Drawing.Color.Transparent
        Me.lbOverTimeQuality.Font = New System.Drawing.Font("Panton Narrow-Trial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.lbOverTimeQuality.ForeColor = System.Drawing.Color.Red
        Me.lbOverTimeQuality.Location = New System.Drawing.Point(216, 163)
        Me.lbOverTimeQuality.Name = "lbOverTimeQuality"
        Me.lbOverTimeQuality.Size = New System.Drawing.Size(55, 30)
        Me.lbOverTimeQuality.TabIndex = 6
        Me.lbOverTimeQuality.Text = "999"
        '
        'lbOverTimePerformance
        '
        Me.lbOverTimePerformance.AutoSize = True
        Me.lbOverTimePerformance.BackColor = System.Drawing.Color.Transparent
        Me.lbOverTimePerformance.Font = New System.Drawing.Font("Panton Narrow-Trial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.lbOverTimePerformance.ForeColor = System.Drawing.Color.Red
        Me.lbOverTimePerformance.Location = New System.Drawing.Point(385, 165)
        Me.lbOverTimePerformance.Name = "lbOverTimePerformance"
        Me.lbOverTimePerformance.Size = New System.Drawing.Size(55, 30)
        Me.lbOverTimePerformance.TabIndex = 7
        Me.lbOverTimePerformance.Text = "999"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Panton Narrow-Trial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(373, 214)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 30)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "999"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Panton Narrow-Trial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(372, 254)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 30)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "999"
        '
        'lvDetailAvailabillty
        '
        Me.lvDetailAvailabillty.HideSelection = False
        Me.lvDetailAvailabillty.Location = New System.Drawing.Point(9, 222)
        Me.lvDetailAvailabillty.Name = "lvDetailAvailabillty"
        Me.lvDetailAvailabillty.Size = New System.Drawing.Size(161, 73)
        Me.lvDetailAvailabillty.TabIndex = 12
        Me.lvDetailAvailabillty.UseCompatibleStateImageBehavior = False
        '
        'ListView1
        '
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(185, 222)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(161, 73)
        Me.ListView1.TabIndex = 13
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'CircularProgressBar2
        '
        Me.CircularProgressBar2.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.CircularProgressBar2.AnimationSpeed = 500
        Me.CircularProgressBar2.BackColor = System.Drawing.Color.Transparent
        Me.CircularProgressBar2.Font = New System.Drawing.Font("Panton Narrow-Trial", 36.0!, System.Drawing.FontStyle.Bold)
        Me.CircularProgressBar2.ForeColor = System.Drawing.Color.Black
        Me.CircularProgressBar2.InnerColor = System.Drawing.Color.Transparent
        Me.CircularProgressBar2.InnerMargin = 2
        Me.CircularProgressBar2.InnerWidth = -1
        Me.CircularProgressBar2.Location = New System.Drawing.Point(26, 301)
        Me.CircularProgressBar2.MarqueeAnimationSpeed = 2000
        Me.CircularProgressBar2.Name = "CircularProgressBar2"
        Me.CircularProgressBar2.OuterColor = System.Drawing.Color.White
        Me.CircularProgressBar2.OuterMargin = -25
        Me.CircularProgressBar2.OuterWidth = 25
        Me.CircularProgressBar2.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CircularProgressBar2.ProgressWidth = 25
        Me.CircularProgressBar2.SecondaryFont = New System.Drawing.Font("Panton Narrow-Trial", 36.0!, System.Drawing.FontStyle.Bold)
        Me.CircularProgressBar2.Size = New System.Drawing.Size(121, 113)
        Me.CircularProgressBar2.StartAngle = 270
        Me.CircularProgressBar2.SubscriptColor = System.Drawing.Color.White
        Me.CircularProgressBar2.SubscriptMargin = New System.Windows.Forms.Padding(10, -35, 0, 0)
        Me.CircularProgressBar2.SubscriptText = ""
        Me.CircularProgressBar2.SuperscriptColor = System.Drawing.Color.White
        Me.CircularProgressBar2.SuperscriptMargin = New System.Windows.Forms.Padding(10, 35, 0, 0)
        Me.CircularProgressBar2.SuperscriptText = ""
        Me.CircularProgressBar2.TabIndex = 4559
        Me.CircularProgressBar2.Text = "0"
        Me.CircularProgressBar2.TextMargin = New System.Windows.Forms.Padding(8, 8, 0, 0)
        Me.CircularProgressBar2.Value = 85
        '
        'CircularProgressBar1
        '
        Me.CircularProgressBar1.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.CircularProgressBar1.AnimationSpeed = 500
        Me.CircularProgressBar1.BackColor = System.Drawing.Color.Transparent
        Me.CircularProgressBar1.Font = New System.Drawing.Font("Panton Narrow-Trial", 36.0!, System.Drawing.FontStyle.Bold)
        Me.CircularProgressBar1.ForeColor = System.Drawing.Color.Black
        Me.CircularProgressBar1.InnerColor = System.Drawing.Color.Transparent
        Me.CircularProgressBar1.InnerMargin = 2
        Me.CircularProgressBar1.InnerWidth = -1
        Me.CircularProgressBar1.Location = New System.Drawing.Point(203, 301)
        Me.CircularProgressBar1.MarqueeAnimationSpeed = 2000
        Me.CircularProgressBar1.Name = "CircularProgressBar1"
        Me.CircularProgressBar1.OuterColor = System.Drawing.Color.White
        Me.CircularProgressBar1.OuterMargin = -25
        Me.CircularProgressBar1.OuterWidth = 25
        Me.CircularProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CircularProgressBar1.ProgressWidth = 25
        Me.CircularProgressBar1.SecondaryFont = New System.Drawing.Font("Panton Narrow-Trial", 36.0!, System.Drawing.FontStyle.Bold)
        Me.CircularProgressBar1.Size = New System.Drawing.Size(124, 115)
        Me.CircularProgressBar1.StartAngle = 270
        Me.CircularProgressBar1.SubscriptColor = System.Drawing.Color.White
        Me.CircularProgressBar1.SubscriptMargin = New System.Windows.Forms.Padding(10, -35, 0, 0)
        Me.CircularProgressBar1.SubscriptText = ""
        Me.CircularProgressBar1.SuperscriptColor = System.Drawing.Color.White
        Me.CircularProgressBar1.SuperscriptMargin = New System.Windows.Forms.Padding(10, 35, 0, 0)
        Me.CircularProgressBar1.SuperscriptText = ""
        Me.CircularProgressBar1.TabIndex = 4559
        Me.CircularProgressBar1.Text = "0"
        Me.CircularProgressBar1.TextMargin = New System.Windows.Forms.Padding(8, 8, 0, 0)
        Me.CircularProgressBar1.Value = 85
        '
        'CircularProgressBar3
        '
        Me.CircularProgressBar3.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.CircularProgressBar3.AnimationSpeed = 500
        Me.CircularProgressBar3.BackColor = System.Drawing.Color.Transparent
        Me.CircularProgressBar3.Font = New System.Drawing.Font("Panton Narrow-Trial", 36.0!, System.Drawing.FontStyle.Bold)
        Me.CircularProgressBar3.ForeColor = System.Drawing.Color.Black
        Me.CircularProgressBar3.InnerColor = System.Drawing.Color.Transparent
        Me.CircularProgressBar3.InnerMargin = 2
        Me.CircularProgressBar3.InnerWidth = -1
        Me.CircularProgressBar3.Location = New System.Drawing.Point(390, 301)
        Me.CircularProgressBar3.MarqueeAnimationSpeed = 2000
        Me.CircularProgressBar3.Name = "CircularProgressBar3"
        Me.CircularProgressBar3.OuterColor = System.Drawing.Color.White
        Me.CircularProgressBar3.OuterMargin = -25
        Me.CircularProgressBar3.OuterWidth = 25
        Me.CircularProgressBar3.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CircularProgressBar3.ProgressWidth = 25
        Me.CircularProgressBar3.SecondaryFont = New System.Drawing.Font("Microsoft Sans Serif", 23.0!)
        Me.CircularProgressBar3.Size = New System.Drawing.Size(117, 115)
        Me.CircularProgressBar3.StartAngle = 270
        Me.CircularProgressBar3.SubscriptColor = System.Drawing.Color.White
        Me.CircularProgressBar3.SubscriptMargin = New System.Windows.Forms.Padding(10, -35, 0, 0)
        Me.CircularProgressBar3.SubscriptText = ""
        Me.CircularProgressBar3.SuperscriptColor = System.Drawing.Color.White
        Me.CircularProgressBar3.SuperscriptMargin = New System.Windows.Forms.Padding(10, 35, 0, 0)
        Me.CircularProgressBar3.SuperscriptText = ""
        Me.CircularProgressBar3.TabIndex = 4561
        Me.CircularProgressBar3.Text = "0"
        Me.CircularProgressBar3.TextMargin = New System.Windows.Forms.Padding(8, 8, 0, 0)
        Me.CircularProgressBar3.Value = 85
        '
        'CircularProgressBar4
        '
        Me.CircularProgressBar4.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.CircularProgressBar4.AnimationSpeed = 500
        Me.CircularProgressBar4.BackColor = System.Drawing.Color.Transparent
        Me.CircularProgressBar4.Font = New System.Drawing.Font("Panton Narrow-Trial", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CircularProgressBar4.ForeColor = System.Drawing.Color.Black
        Me.CircularProgressBar4.InnerColor = System.Drawing.Color.Transparent
        Me.CircularProgressBar4.InnerMargin = 2
        Me.CircularProgressBar4.InnerWidth = -1
        Me.CircularProgressBar4.Location = New System.Drawing.Point(659, 282)
        Me.CircularProgressBar4.MarqueeAnimationSpeed = 2000
        Me.CircularProgressBar4.Name = "CircularProgressBar4"
        Me.CircularProgressBar4.OuterColor = System.Drawing.Color.White
        Me.CircularProgressBar4.OuterMargin = -25
        Me.CircularProgressBar4.OuterWidth = 25
        Me.CircularProgressBar4.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CircularProgressBar4.ProgressWidth = 25
        Me.CircularProgressBar4.SecondaryFont = New System.Drawing.Font("Panton Narrow-Trial", 20.25!, System.Drawing.FontStyle.Bold)
        Me.CircularProgressBar4.Size = New System.Drawing.Size(138, 132)
        Me.CircularProgressBar4.StartAngle = 270
        Me.CircularProgressBar4.SubscriptColor = System.Drawing.Color.White
        Me.CircularProgressBar4.SubscriptMargin = New System.Windows.Forms.Padding(10, -35, 0, 0)
        Me.CircularProgressBar4.SubscriptText = ""
        Me.CircularProgressBar4.SuperscriptColor = System.Drawing.Color.White
        Me.CircularProgressBar4.SuperscriptMargin = New System.Windows.Forms.Padding(10, 35, 0, 0)
        Me.CircularProgressBar4.SuperscriptText = ""
        Me.CircularProgressBar4.TabIndex = 4562
        Me.CircularProgressBar4.Text = "0"
        Me.CircularProgressBar4.TextMargin = New System.Windows.Forms.Padding(8, 8, 0, 0)
        Me.CircularProgressBar4.Value = 85
        '
        'lbAccTarget
        '
        Me.lbAccTarget.AutoSize = True
        Me.lbAccTarget.BackColor = System.Drawing.Color.Transparent
        Me.lbAccTarget.Font = New System.Drawing.Font("Panton Narrow-Trial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbAccTarget.ForeColor = System.Drawing.Color.Lime
        Me.lbAccTarget.Location = New System.Drawing.Point(656, 159)
        Me.lbAccTarget.Name = "lbAccTarget"
        Me.lbAccTarget.Size = New System.Drawing.Size(95, 35)
        Me.lbAccTarget.TabIndex = 4563
        Me.lbAccTarget.Text = "99999"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Panton Narrow-Trial", 20.25!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(658, 197)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 35)
        Me.Label4.TabIndex = 4564
        Me.Label4.Text = "99999"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Panton Narrow-Trial", 20.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(697, 231)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 35)
        Me.Label5.TabIndex = 4565
        Me.Label5.Text = "20"
        '
        'WebView21
        '
        Me.WebView21.CreationProperties = Nothing
        Me.WebView21.DefaultBackgroundColor = System.Drawing.Color.White
        Me.WebView21.Location = New System.Drawing.Point(0, 418)
        Me.WebView21.Name = "WebView21"
        Me.WebView21.Size = New System.Drawing.Size(803, 184)
        Me.WebView21.TabIndex = 4568
        Me.WebView21.ZoomFactor = 1.0R
        '
        'btnMenu
        '
        Me.btnMenu.BackColor = System.Drawing.Color.Transparent
        Me.btnMenu.Location = New System.Drawing.Point(723, 1)
        Me.btnMenu.Name = "btnMenu"
        Me.btnMenu.Size = New System.Drawing.Size(80, 95)
        Me.btnMenu.TabIndex = 4569
        Me.btnMenu.TabStop = False
        '
        'PanelMenu
        '
        Me.PanelMenu.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.PanelMenu.Controls.Add(Me.btnDefect)
        Me.PanelMenu.Controls.Add(Me.btnInc)
        Me.PanelMenu.Controls.Add(Me.btnDesc)
        Me.PanelMenu.Controls.Add(Me.btnStop)
        Me.PanelMenu.Controls.Add(Me.BtnStart)
        Me.PanelMenu.Controls.Add(Me.btnInfo)
        Me.PanelMenu.Controls.Add(Me.btnBack)
        Me.PanelMenu.Location = New System.Drawing.Point(0, 524)
        Me.PanelMenu.Name = "PanelMenu"
        Me.PanelMenu.Size = New System.Drawing.Size(800, 77)
        Me.PanelMenu.TabIndex = 4575
        '
        'btnDefect
        '
        Me.btnDefect.BackColor = System.Drawing.Color.Orange
        Me.btnDefect.Location = New System.Drawing.Point(306, 6)
        Me.btnDefect.Name = "btnDefect"
        Me.btnDefect.Size = New System.Drawing.Size(144, 66)
        Me.btnDefect.TabIndex = 4581
        Me.btnDefect.TabStop = False
        '
        'btnInc
        '
        Me.btnInc.BackColor = System.Drawing.Color.Blue
        Me.btnInc.Location = New System.Drawing.Point(156, 7)
        Me.btnInc.Name = "btnInc"
        Me.btnInc.Size = New System.Drawing.Size(144, 66)
        Me.btnInc.TabIndex = 4580
        Me.btnInc.TabStop = False
        '
        'btnDesc
        '
        Me.btnDesc.BackColor = System.Drawing.Color.DarkRed
        Me.btnDesc.Location = New System.Drawing.Point(7, 6)
        Me.btnDesc.Name = "btnDesc"
        Me.btnDesc.Size = New System.Drawing.Size(144, 66)
        Me.btnDesc.TabIndex = 4579
        Me.btnDesc.TabStop = False
        '
        'btnStop
        '
        Me.btnStop.BackColor = System.Drawing.Color.Coral
        Me.btnStop.Location = New System.Drawing.Point(628, 7)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(165, 66)
        Me.btnStop.TabIndex = 4577
        Me.btnStop.TabStop = False
        Me.btnStop.Visible = False
        '
        'BtnStart
        '
        Me.BtnStart.BackColor = System.Drawing.Color.DimGray
        Me.BtnStart.Location = New System.Drawing.Point(628, 6)
        Me.BtnStart.Name = "BtnStart"
        Me.BtnStart.Size = New System.Drawing.Size(165, 66)
        Me.BtnStart.TabIndex = 4576
        Me.BtnStart.TabStop = False
        '
        'btnInfo
        '
        Me.btnInfo.BackColor = System.Drawing.Color.DimGray
        Me.btnInfo.Location = New System.Drawing.Point(456, 6)
        Me.btnInfo.Name = "btnInfo"
        Me.btnInfo.Size = New System.Drawing.Size(169, 66)
        Me.btnInfo.TabIndex = 4575
        Me.btnInfo.TabStop = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Red
        Me.btnBack.Location = New System.Drawing.Point(9, 7)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(142, 66)
        Me.btnBack.TabIndex = 4574
        Me.btnBack.TabStop = False
        '
        'TIME_CAL_EFF
        '
        Me.TIME_CAL_EFF.Interval = 1000
        '
        'picStart
        '
        Me.picStart.BackgroundImage = CType(resources.GetObject("picStart.BackgroundImage"), System.Drawing.Image)
        Me.picStart.Location = New System.Drawing.Point(688, 451)
        Me.picStart.Name = "picStart"
        Me.picStart.Size = New System.Drawing.Size(112, 151)
        Me.picStart.TabIndex = 4570
        Me.picStart.TabStop = False
        '
        'picMenuAll
        '
        Me.picMenuAll.BackgroundImage = CType(resources.GetObject("picMenuAll.BackgroundImage"), System.Drawing.Image)
        Me.picMenuAll.Location = New System.Drawing.Point(688, 451)
        Me.picMenuAll.Name = "picMenuAll"
        Me.picMenuAll.Size = New System.Drawing.Size(112, 83)
        Me.picMenuAll.TabIndex = 4576
        Me.picMenuAll.TabStop = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(531, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(131, 37)
        Me.Label27.TabIndex = 4577
        Me.Label27.Text = "Label27"
        '
        'Label9_fontModel
        '
        Me.Label9_fontModel.AutoSize = True
        Me.Label9_fontModel.Font = New System.Drawing.Font("Consolas", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label9_fontModel.Location = New System.Drawing.Point(531, 1)
        Me.Label9_fontModel.Name = "Label9_fontModel"
        Me.Label9_fontModel.Size = New System.Drawing.Size(186, 23)
        Me.Label9_fontModel.TabIndex = 4657
        Me.Label9_fontModel.Text = "Label9_fontModel"
        Me.Label9_fontModel.Visible = False
        '
        'lbPosition2
        '
        Me.lbPosition2.AutoSize = True
        Me.lbPosition2.Location = New System.Drawing.Point(532, 1)
        Me.lbPosition2.Name = "lbPosition2"
        Me.lbPosition2.Size = New System.Drawing.Size(58, 13)
        Me.lbPosition2.TabIndex = 4656
        Me.lbPosition2.Text = "lbPosition2"
        Me.lbPosition2.Visible = False
        '
        'lbPosition1
        '
        Me.lbPosition1.AutoSize = True
        Me.lbPosition1.Location = New System.Drawing.Point(535, 25)
        Me.lbPosition1.Name = "lbPosition1"
        Me.lbPosition1.Size = New System.Drawing.Size(58, 13)
        Me.lbPosition1.TabIndex = 4655
        Me.lbPosition1.Text = "lbPosition1"
        Me.lbPosition1.Visible = False
        '
        'LB_IND_ROW
        '
        Me.LB_IND_ROW.AutoSize = True
        Me.LB_IND_ROW.Location = New System.Drawing.Point(532, 1)
        Me.LB_IND_ROW.Name = "LB_IND_ROW"
        Me.LB_IND_ROW.Size = New System.Drawing.Size(78, 13)
        Me.LB_IND_ROW.TabIndex = 4658
        Me.LB_IND_ROW.Text = "LB_IND_ROW"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(535, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 4659
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(565, 14)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(45, 13)
        Me.Label29.TabIndex = 4660
        Me.Label29.Text = "Label29"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(616, 13)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(45, 13)
        Me.Label18.TabIndex = 4661
        Me.Label18.Text = "Label18"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(535, 69)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(45, 13)
        Me.Label14.TabIndex = 4662
        Me.Label14.Text = "Label14"
        '
        'wi_no
        '
        Me.wi_no.AutoSize = True
        Me.wi_no.Location = New System.Drawing.Point(586, 69)
        Me.wi_no.Name = "wi_no"
        Me.wi_no.Size = New System.Drawing.Size(35, 13)
        Me.wi_no.TabIndex = 4663
        Me.wi_no.Text = "wi_no"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(627, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 4664
        Me.Label3.Text = "Label3"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(685, 37)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 13)
        Me.Label12.TabIndex = 4665
        Me.Label12.Text = "Label12"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(685, 69)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 13)
        Me.Label8.TabIndex = 4666
        Me.Label8.Text = "Label8"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(535, 52)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(45, 13)
        Me.Label37.TabIndex = 4667
        Me.Label37.Text = "Label37"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(578, 52)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(45, 13)
        Me.Label38.TabIndex = 4668
        Me.Label38.Text = "Label38"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(623, 52)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(45, 13)
        Me.Label34.TabIndex = 4669
        Me.Label34.Text = "Label34"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(586, 29)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(45, 13)
        Me.Label22.TabIndex = 4670
        Me.Label22.Text = "Label22"
        '
        'lb_dlv_date
        '
        Me.lb_dlv_date.AutoSize = True
        Me.lb_dlv_date.Location = New System.Drawing.Point(535, 34)
        Me.lb_dlv_date.Name = "lb_dlv_date"
        Me.lb_dlv_date.Size = New System.Drawing.Size(62, 13)
        Me.lb_dlv_date.TabIndex = 4671
        Me.lb_dlv_date.Text = "lb_dlv_date"
        '
        'lb_model
        '
        Me.lb_model.AutoSize = True
        Me.lb_model.Location = New System.Drawing.Point(630, 39)
        Me.lb_model.Name = "lb_model"
        Me.lb_model.Size = New System.Drawing.Size(49, 13)
        Me.lb_model.TabIndex = 4672
        Me.lb_model.Text = "lb_model"
        '
        'lb_location
        '
        Me.lb_location.AutoSize = True
        Me.lb_location.Location = New System.Drawing.Point(573, 42)
        Me.lb_location.Name = "lb_location"
        Me.lb_location.Size = New System.Drawing.Size(58, 13)
        Me.lb_location.TabIndex = 4673
        Me.lb_location.Text = "lb_location"
        '
        'lb_prd_type
        '
        Me.lb_prd_type.AutoSize = True
        Me.lb_prd_type.Location = New System.Drawing.Point(617, 65)
        Me.lb_prd_type.Name = "lb_prd_type"
        Me.lb_prd_type.Size = New System.Drawing.Size(62, 13)
        Me.lb_prd_type.TabIndex = 4674
        Me.lb_prd_type.Text = "lb_prd_type"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(582, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 4675
        Me.Label6.Text = "Label6"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(586, 47)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(45, 13)
        Me.Label10.TabIndex = 4676
        Me.Label10.Text = "Label10"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(582, 56)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(45, 13)
        Me.Label33.TabIndex = 4677
        Me.Label33.Text = "Label33"
        '
        'LB_COUNTER_SHIP
        '
        Me.LB_COUNTER_SHIP.AutoSize = True
        Me.LB_COUNTER_SHIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB_COUNTER_SHIP.Location = New System.Drawing.Point(549, 381)
        Me.LB_COUNTER_SHIP.Name = "LB_COUNTER_SHIP"
        Me.LB_COUNTER_SHIP.Size = New System.Drawing.Size(31, 33)
        Me.LB_COUNTER_SHIP.TabIndex = 4678
        Me.LB_COUNTER_SHIP.Text = "0"
        '
        'LB_COUNTER_SEQ
        '
        Me.LB_COUNTER_SEQ.AutoSize = True
        Me.LB_COUNTER_SEQ.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB_COUNTER_SEQ.Location = New System.Drawing.Point(582, 379)
        Me.LB_COUNTER_SEQ.Name = "LB_COUNTER_SEQ"
        Me.LB_COUNTER_SEQ.Size = New System.Drawing.Size(35, 37)
        Me.LB_COUNTER_SEQ.TabIndex = 4679
        Me.LB_COUNTER_SEQ.Text = "0"
        '
        'lb_good
        '
        Me.lb_good.AutoSize = True
        Me.lb_good.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_good.Location = New System.Drawing.Point(627, 381)
        Me.lb_good.Name = "lb_good"
        Me.lb_good.Size = New System.Drawing.Size(29, 31)
        Me.lb_good.TabIndex = 4680
        Me.lb_good.Text = "0"
        '
        'lb_ng_qty
        '
        Me.lb_ng_qty.BackColor = System.Drawing.Color.Transparent
        Me.lb_ng_qty.Font = New System.Drawing.Font("Catamaran", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_ng_qty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(97, Byte), Integer))
        Me.lb_ng_qty.Location = New System.Drawing.Point(698, 99)
        Me.lb_ng_qty.Name = "lb_ng_qty"
        Me.lb_ng_qty.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lb_ng_qty.Size = New System.Drawing.Size(90, 54)
        Me.lb_ng_qty.TabIndex = 4682
        Me.lb_ng_qty.Text = "0"
        Me.lb_ng_qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lb_ng_qty.UseMnemonic = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.Controls.Add(Me.lb_nc_qty)
        Me.Panel7.Location = New System.Drawing.Point(576, 107)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(90, 42)
        Me.Panel7.TabIndex = 4681
        '
        'lb_nc_qty
        '
        Me.lb_nc_qty.BackColor = System.Drawing.Color.Transparent
        Me.lb_nc_qty.Font = New System.Drawing.Font("Catamaran", 27.75!)
        Me.lb_nc_qty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lb_nc_qty.Location = New System.Drawing.Point(9, -5)
        Me.lb_nc_qty.Name = "lb_nc_qty"
        Me.lb_nc_qty.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lb_nc_qty.Size = New System.Drawing.Size(75, 51)
        Me.lb_nc_qty.TabIndex = 4512
        Me.lb_nc_qty.Text = "0"
        Me.lb_nc_qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lb_nc_qty.UseMnemonic = False
        '
        'st_count_ct
        '
        Me.st_count_ct.AutoSize = True
        Me.st_count_ct.Location = New System.Drawing.Point(629, 52)
        Me.st_count_ct.Name = "st_count_ct"
        Me.st_count_ct.Size = New System.Drawing.Size(63, 13)
        Me.st_count_ct.TabIndex = 4683
        Me.st_count_ct.Text = "st_count_ct"
        '
        'st_time
        '
        Me.st_time.AutoSize = True
        Me.st_time.Location = New System.Drawing.Point(629, 35)
        Me.st_time.Name = "st_time"
        Me.st_time.Size = New System.Drawing.Size(40, 13)
        Me.st_time.TabIndex = 4684
        Me.st_time.Text = "st_time"
        '
        'lb_qty_for_box
        '
        Me.lb_qty_for_box.AutoSize = True
        Me.lb_qty_for_box.Location = New System.Drawing.Point(685, 80)
        Me.lb_qty_for_box.Name = "lb_qty_for_box"
        Me.lb_qty_for_box.Size = New System.Drawing.Size(13, 13)
        Me.lb_qty_for_box.TabIndex = 4685
        Me.lb_qty_for_box.Text = "0"
        '
        'Working_OEE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.lb_qty_for_box)
        Me.Controls.Add(Me.st_time)
        Me.Controls.Add(Me.st_count_ct)
        Me.Controls.Add(Me.lb_ng_qty)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.lb_good)
        Me.Controls.Add(Me.LB_COUNTER_SEQ)
        Me.Controls.Add(Me.LB_COUNTER_SHIP)
        Me.Controls.Add(Me.Label33)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lb_prd_type)
        Me.Controls.Add(Me.lb_location)
        Me.Controls.Add(Me.lb_model)
        Me.Controls.Add(Me.lb_dlv_date)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label34)
        Me.Controls.Add(Me.Label38)
        Me.Controls.Add(Me.Label37)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.wi_no)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.LB_IND_ROW)
        Me.Controls.Add(Me.lbPosition2)
        Me.Controls.Add(Me.Label9_fontModel)
        Me.Controls.Add(Me.lbPosition1)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.picMenuAll)
        Me.Controls.Add(Me.PanelMenu)
        Me.Controls.Add(Me.picStart)
        Me.Controls.Add(Me.btnMenu)
        Me.Controls.Add(Me.WebView21)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lbAccTarget)
        Me.Controls.Add(Me.CircularProgressBar4)
        Me.Controls.Add(Me.CircularProgressBar3)
        Me.Controls.Add(Me.CircularProgressBar1)
        Me.Controls.Add(Me.CircularProgressBar2)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.lvDetailAvailabillty)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbOverTimePerformance)
        Me.Controls.Add(Me.lbOverTimeQuality)
        Me.Controls.Add(Me.lbOverTimeAvailability)
        Me.Controls.Add(Me.lbShiftQty)
        Me.Controls.Add(Me.lbShiftPlan)
        Me.Controls.Add(Me.lbLine)
        Me.Controls.Add(Me.lbPartName)
        Me.Controls.Add(Me.lbPartNo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Working_OEE"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Working_OEE"
        CType(Me.WebView21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelMenu.ResumeLayout(False)
        CType(Me.btnDefect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnInc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnStop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picMenuAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbPartNo As Label
    Friend WithEvents lbPartName As Label
    Friend WithEvents lbLine As Label
    Friend WithEvents lbShiftPlan As Label
    Friend WithEvents lbShiftQty As Label
    Friend WithEvents lbOverTimeAvailability As Label
    Friend WithEvents lbOverTimeQuality As Label
    Friend WithEvents lbOverTimePerformance As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lvDetailAvailabillty As ListView
    Friend WithEvents ListView1 As ListView
    Friend WithEvents CircularProgressBar2 As CircularProgressBar.CircularProgressBar
    Friend WithEvents CircularProgressBar1 As CircularProgressBar.CircularProgressBar
    Friend WithEvents CircularProgressBar3 As CircularProgressBar.CircularProgressBar
    Friend WithEvents CircularProgressBar4 As CircularProgressBar.CircularProgressBar
    Friend WithEvents lbAccTarget As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents WebView21 As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents btnMenu As PictureBox
    Friend WithEvents PanelMenu As Panel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents PrintDocument2 As Printing.PrintDocument
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Timer3 As Timer
    Friend WithEvents Timer_new_dio As Timer
    Friend WithEvents TIME_CAL_EFF As Timer
    Friend WithEvents SerialPortLamp As IO.Ports.SerialPort
    Friend WithEvents picStart As PictureBox
    Friend WithEvents picMenuAll As PictureBox
    Friend WithEvents btnStop As PictureBox
    Friend WithEvents BtnStart As PictureBox
    Friend WithEvents btnInfo As PictureBox
    Friend WithEvents btnBack As PictureBox
    Friend WithEvents btnDefect As PictureBox
    Friend WithEvents btnInc As PictureBox
    Friend WithEvents btnDesc As PictureBox
    Friend WithEvents Label27 As Label
    Friend WithEvents Label9_fontModel As Label
    Friend WithEvents lbPosition2 As Label
    Friend WithEvents lbPosition1 As Label
    Friend WithEvents LB_IND_ROW As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Label29 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents wi_no As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label37 As Label
    Friend WithEvents Label38 As Label
    Friend WithEvents Label34 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents lb_dlv_date As Label
    Friend WithEvents lb_model As Label
    Friend WithEvents lb_location As Label
    Friend WithEvents lb_prd_type As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents LB_COUNTER_SHIP As Label
    Friend WithEvents LB_COUNTER_SEQ As Label
    Friend WithEvents lb_good As Label
    Friend WithEvents lb_ng_qty As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents lb_nc_qty As Label
    Friend WithEvents st_count_ct As Label
    Friend WithEvents st_time As Label
    Friend WithEvents lb_qty_for_box As Label
End Class
