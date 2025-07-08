Imports System.IO
Imports System.IO.Pipes
Imports System.Threading
Imports System.Web.Script.Serialization
Imports Microsoft.Web
Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms

Public Class MainFrm
    Private WithEvents WebViewEmergency As WebView2
    Public Sub ClickButton()
        Application.Exit()
    End Sub
    Public Shared rsCheckCriticalFlg = ""
    Private isRunning As Boolean = False
    Public chk_spec_line As String = "0"
    Public dbClass As New Backoffice_model
    Public dbClass2 As New Backoffice_model
    Public check_status_date As Integer = 0
    Public Shared c As String = ""
    'Private Sub button1_click(sender As Object, e As EventArgs)
    '    dbClass.ConnectDB()
    '    dbClass.myConnection.Close()
    'End Sub
    Dim p() As Process
    Public Declare Auto Function FindWindowNullClassName Lib "user32.dll" Alias "FindWindow" (ByVal ipClassname As Integer, ByVal IpWindownName As String) As Integer
    Dim Counter As Integer = 0
    Dim dataPlan As String = ""
    Public Shared ArrayDataPlan As New List(Of DataPlan)
    Public Sub check_process()
        Dim hWnd As Integer = FindWindowNullClassName(0, "TBK FA System.exe")
        If hWnd = 0 Then
            'MsgBox("No process found!")
        Else
            'MsgBox(" process found!")
        End If
    End Sub
    Public Function CheckIfRunning()
        p = Process.GetProcessesByName("TBK FA System")
        If p.Count > 1 Then
            Application.Exit()
            Return 1
            ' Process is running
        Else
            Return 0
            ' Process is not running
        End If
    End Function
    Public Sub check_close_fa()
        If Application.OpenForms().OfType(Of MainFrm).Any Then
        Else
            MsgBox("end program")
        End If
    End Sub
    Public Async Function Main() As Task
        ' Create PipeServer
        Dim pipeServer As New NamedPipeServerStream("mypipe", PipeDirection.In)
        'Console.WriteLine("Waiting for connection...")

        ' Wait asynchronously for a connection from the client
        Await pipeServer.WaitForConnectionAsync()
        'Console.WriteLine("Client connected.")

        ' Read command from the client
        Dim reader As New StreamReader(pipeServer)
        Dim command As String = Await reader.ReadLineAsync()
        'Console.WriteLine("Received command from client: " & command)

        ' Check the command
        If command = "click button" Then
            reader.Close()
            pipeServer.Close()
        ElseIf command = "Wait_DATA" Then
            'Console.WriteLine("Wait_DATA")
        End If
        ' Close the connection
        'Console.WriteLine("close Connection main")
    End Function
    Public Async Function CheckMemoryLeak() As Task
        Dim memUsed As Long = GC.GetTotalMemory(False) \ 1024 \ 1024 ' >= 2.5 GB Clear Memory 
        If memUsed >= 2560 Then
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
            ' Logging & UI
        End If
    End Function
    Public Async Function ShowInformationByStatus(pd As String, line_cd As String) As Task
        Try
            Dim api = New api()
            Dim infoUrl As String = "http://" & Backoffice_model.svApi &
            "/API_NEW_FA/index.php/Api_Information/CheckInformation?pd=" & pd & "&line_cd=" & line_cd

            Console.WriteLine(infoUrl)

            Dim result_data As String = Await Task.Run(Function() api.Load_data(infoUrl))

            If result_data = "0" Then
                PanelShowInformation.Visible = False
                PanelWebviewInformation.Visible = False
                Return
            End If

            ' === แสดง Panel ===
            PanelShowInformation.Visible = True
            PanelShowInformation.Location = New Point(0, 0)
            PanelShowInformation.Size = New Size(800, 600)

            PanelWebviewInformation.Visible = True
            PanelWebviewInformation.Location = New Point(0, 0)
            PanelWebviewInformation.Size = New Size(800, 600)

            ' === ตรวจสอบ WebViewEmergency ===
            If WebViewEmergency Is Nothing OrElse WebViewEmergency.IsDisposed Then
                WebViewEmergency = New WebView2 With {
                .Dock = DockStyle.Fill
            }
                Dim webViewEnvironment = Await CoreWebView2Environment.CreateAsync(Nothing, "C:\Temp")
                Await WebViewEmergency.EnsureCoreWebView2Async(webViewEnvironment)
            End If

            ' ล้าง Control ถ้ายังไม่ได้เพิ่ม WebView และปุ่ม
            If Not PanelWebviewInformation.Controls.Contains(WebViewEmergency) Then
                PanelWebviewInformation.Controls.Clear()

                ' === Panel แสดง WebView ===
                Dim panelMain As New Panel With {
                .Dock = DockStyle.Fill,
                .BackColor = Color.White
            }
                panelMain.Controls.Add(WebViewEmergency)

                ' === Panel ปุ่มล่าง ===
                Dim panelButtons As New Panel With {
                .Dock = DockStyle.Bottom,
                .Height = 80,
                .BackColor = Color.LightGray
            }

                ' ตรวจสอบว่ามีปุ่มรับทราบหรือยัง
                Dim BtnAccept As New Button With {
                .Name = "BtnAcceptInfo",
                .Text = "รับทราบ",
                .Size = New Size(140, 50),
                .Location = New Point(620, 15),
                .Font = New Font("Segoe UI", 20, FontStyle.Bold),
                .BackColor = Color.FromArgb(40, 167, 69),
                .ForeColor = Color.White
            }
                AddHandler BtnAccept.Click, Async Sub(sender As Object, e As EventArgs)
                                                Await Task.Run(Sub()
                                                                   api.Load_data("http://" & Backoffice_model.svApi &
                                   "/API_NEW_FA/index.php/Api_Information/AcceptInformation?pd=" & pd & "&line_cd=" & line_cd)
                                                               End Sub)
                                                PanelWebviewInformation.Visible = False
                                                PanelShowInformation.Visible = False
                                            End Sub
                panelButtons.Controls.Add(BtnAccept)
                ' เพิ่มเข้า Panel หลัก
                PanelWebviewInformation.Controls.Add(panelMain)
                PanelWebviewInformation.Controls.Add(panelButtons)
            End If
            ' === Navigate WebView ===
            If WebViewEmergency.CoreWebView2 IsNot Nothing Then
                WebViewEmergency.CoreWebView2.Navigate(infoUrl)
            End If
        Catch ex As Exception
            MsgBox("ไม่สามารถโหลดข้อมูลได้ กรุณาตรวจสอบเครือข่าย", MsgBoxStyle.Exclamation)
        End Try
    End Function

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        check_process()
        If CheckIfRunning() = 0 Then
            Await Task.Run(Sub()
                               dbClass.GetLocalServerAPI()
                               dbClass.GetLocalServerping()
                               dbClass.GetLocalServerOEE()
                               dbClass.sqlite_conn_dbsv()
                           End Sub)
            Await WaitForSQLiteEmptyAsync()
            ' Await dbClass.updated_data_to_dbsvr(Me, "1")
            Timer1.Start()
            Timer2.Start()
            Dim sqlss = Backoffice_model.ConnectDBSQLite()
            While sqlss.Read()
                Label6.Text = sqlss("pd").ToString()
                Label4.Text = sqlss("line_cd").ToString()
                count_type.Text = sqlss("count_type").ToString()
                cavity.Text = sqlss("cavity").ToString()
                lb_scanner_port.Text = sqlss("scanner_port").ToString()
                lb_printer_port.Text = sqlss("printer_port").ToString()
                lb_dio_port.Text = sqlss("dio_port").ToString()
                Backoffice_model.SCANNER_PORT = sqlss("scanner_port").ToString()
            End While

            If Backoffice_model.SCANNER_PORT <> "" AndAlso Backoffice_model.SCANNER_PORT <> "USB" Then
                lb_ctrl_sc_flg.Text = "emp"
            End If
            Insert_list.Label3.Text = Label4.Text
            Prd_detail.Label3.Text = Label4.Text
            Await ShowInformationByStatus(Label6.Text, Label4.Text)
            Await checkcmd()
        Else
            Application.Exit()
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        check_close_fa()

        Label2.Text = TimeOfDay.ToString("H:mm:ss")
        Label3.Text = DateTime.Now.ToString("D")
        Label1.Text = DateTime.Now.ToString("yyyy/MM/dd")
    End Sub
    Private Sub Label2_Click(sender As Object, e As EventArgs)
    End Sub
    Private Sub menu3_Click(sender As Object, e As EventArgs)
        MsgBox("New version")
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
    End Sub
    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs)
    End Sub
    Public Async Function checkcmd() As Task
        Dim cts As New CancellationTokenSource()
        Await Task.Delay(300000, cts.Token).ContinueWith(Sub(task)
                                                             ' ตรวจสอบว่าถ้า Task ไม่ถูกยกเลิก
                                                             If Not task.IsCanceled Then
                                                                 ' ตรวจสอบว่า Handle ของฟอร์มยังถูกสร้างอยู่และไม่ถูก Dispose
                                                                 If Me.IsHandleCreated AndAlso Not Me.IsDisposed Then
                                                                     Try
                                                                         Dim api = New api()
                                                                         Dim Command As String = ""
                                                                         Dim parameters As String = ""
                                                                         Dim result_data As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/GET_DATA_NEW_FA/RunCmd?line_cd=" & Label4.Text)
                                                                         If result_data <> "0" Then
                                                                             Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
                                                                             For Each item As Object In dict2
                                                                                 Command = item("command").ToString()
                                                                                 parameters = item("parameters").ToString()
                                                                                 System.Diagnostics.Process.Start(Command, parameters)
                                                                             Next
                                                                         End If
                                                                     Catch ex As Exception
                                                                         status_emergency = "0"
                                                                     End Try
                                                                 End If
                                                             End If
                                                         End Sub, TaskScheduler.FromCurrentSynchronizationContext())
    End Function
    Public Sub check_lot()
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Working_Pro.Label24.Text = Label4.Text
                Dim i = List_Emp.ListView1.Items.Count
                If i > 0 Then
                    Dim LoadSQL = Backoffice_model.Get_Line_id(Label4.Text)
                    While LoadSQL.Read()
                        line_id.Text = LoadSQL("line_id").ToString()
                    End While
                    Prd_detail.Label2.Text = i
                    Me.Enabled = False
                    Dim lotSubstYear As String = DateTime.Now.ToString("yyyy").Substring(3, 1)
                    Dim lotFirstDigit As String = ""
                    If lotSubstYear = "1" Then
                        lotFirstDigit = "A"
                    ElseIf lotSubstYear = "2" Then
                        lotFirstDigit = "B"
                    ElseIf lotSubstYear = "3" Then
                        lotFirstDigit = "C"
                    ElseIf lotSubstYear = "4" Then
                        lotFirstDigit = "D"
                    ElseIf lotSubstYear = "5" Then
                        lotFirstDigit = "E"
                    ElseIf lotSubstYear = "6" Then
                        lotFirstDigit = "F"
                    ElseIf lotSubstYear = "7" Then
                        lotFirstDigit = "G"
                    ElseIf lotSubstYear = "8" Then
                        lotFirstDigit = "H"
                    ElseIf lotSubstYear = "9" Then
                        lotFirstDigit = "I"
                    ElseIf lotSubstYear = "0" Then
                        lotFirstDigit = "J"
                    End If
                    Dim lotSubstMonth As String = DateTime.Now.ToString("MM")
                    Dim lotSecondDigit As String = ""
                    If lotSubstMonth = "01" Then
                        lotSecondDigit = "A"
                    ElseIf lotSubstMonth = "02" Then
                        lotSecondDigit = "B"
                    ElseIf lotSubstMonth = "03" Then
                        lotSecondDigit = "C"
                    ElseIf lotSubstMonth = "04" Then
                        lotSecondDigit = "D"
                    ElseIf lotSubstMonth = "05" Then
                        lotSecondDigit = "E"
                    ElseIf lotSubstMonth = "06" Then
                        lotSecondDigit = "F"
                    ElseIf lotSubstMonth = "07" Then
                        lotSecondDigit = "G"
                    ElseIf lotSubstMonth = "08" Then
                        lotSecondDigit = "H"
                    ElseIf lotSubstMonth = "09" Then
                        lotSecondDigit = "I"
                    ElseIf lotSubstMonth = "10" Then
                        lotSecondDigit = "J"
                    ElseIf lotSubstMonth = "11" Then
                        lotSecondDigit = "K"
                    ElseIf lotSubstMonth = "12" Then
                        lotSecondDigit = "L"
                    End If
                    Dim lotthirdDigit = DateTime.Now.ToString("dd")
                    Dim d As Date = DateTime.Now.ToString("dd-MM-yyyy")
                    Dim timeShift As String = DateTime.Now.ToString("HH")
                    Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
                    Dim date_st As Integer = lotthirdDigit
                    If time_now >= "00:00:00 AM" And time_now <= "08:00:00 AM" Then
                        date_st = lotthirdDigit - 1
                        If date_st <= 0 Then
                            Dim tmp_date As String = d.AddDays(-1)
                            lotthirdDigit = tmp_date.Substring(0, 2)
                            check_status_date = 1
                            lotSecondDigit = set_data_Month(tmp_date.Substring(3, 2))
                            lotFirstDigit = set_data_Year(tmp_date.Substring(6, 2))
                        Else
                            lotthirdDigit = date_st
                        End If
                    Else
                        'lotthirdDigit -= 1
                    End If
                    Dim defaultShift As String = ""
                    'If timeShift = "05" Or timeShift = "06" Or timeShift = "07" Then
                    'defaultShift = "N (05:00 - 08:00)"
                    'ElseIf timeShift = "08" Or timeShift = "09" Or timeShift = "10" Or timeShift = "11" Or timeShift = "12" Or timeShift = "13" Or timeShift = "14" Or timeShift = "15" Or timeShift = "16" Or timeShift = "17" Then
                    'defaultShift = "A (08:00 - 17:00)"
                    'ElseIf timeShift = "17" Or timeShift = "18" Or timeShift = "19" Then
                    'defaultShift = "M (17:00 - 20:00)"
                    'ElseIf timeShift = "20" Or timeShift = "21" Or timeShift = "22" Or timeShift = "23" Or timeShift = "24" Or timeShift = "00" Or timeShift = "01" Or timeShift = "02" Or timeShift = "03" Or timeShift = "04" Or timeShift = "05" Then
                    'defaultShift = "B (20:00 - 05:00)"
                    'End If
                    If timeShift = "05" Or timeShift = "06" Or timeShift = "07" Then
                        'defaultShift = "N (05:00 - 08:00)"
                        defaultShift = "Q (20:00 - 08:00)"
                    ElseIf timeShift = "08" Or timeShift = "09" Or timeShift = "10" Or timeShift = "11" Or timeShift = "12" Or timeShift = "13" Or timeShift = "14" Or timeShift = "15" Or timeShift = "16" Or timeShift = "17" Then
                        defaultShift = "P (08:00 - 20:00)"
                    ElseIf timeShift = "17" Or timeShift = "18" Or timeShift = "19" Then
                        ' defaultShift = "M (17:00 - 20:00)"
                        defaultShift = "P (08:00 - 20:00)"
                    ElseIf timeShift = "20" Or timeShift = "21" Or timeShift = "22" Or timeShift = "23" Or timeShift = "24" Or timeShift = "00" Or timeShift = "01" Or timeShift = "02" Or timeShift = "03" Or timeShift = "04" Or timeShift = "05" Then
                        defaultShift = "Q (20:00 - 08:00)"
                    End If
                    Prd_detail.Label12.Text = defaultShift
                    If Len(Trim(date_st)) <= 1 Then
                        Dim date_digit
                        If date_st = 0 Then
                            Dim DATES As Date = DateTime.Now.ToString("dd-MM-yyyy")
                            If check_status_date = 0 Then
                                Dim GET_DATA As String = GetLastDayOfMonth(DATES)
                                Dim re = GET_DATA.Substring(0, 2)
                                lotthirdDigit = re
                                date_digit = re
                            Else
                                Dim tmp_date As String = d.AddDays(-1)
                                'MsgBox(tmp_date)
                                'MsgBox("day = " & tmp_date.Substring(0, 2))
                                lotthirdDigit = tmp_date.Substring(0, 2)
                                date_digit = lotthirdDigit
                            End If
                        Else
                            lotthirdDigit = "0" & date_st
                            date_digit = "0" & date_st
                        End If
                        Prd_detail.Label6.Text = lotFirstDigit & lotSecondDigit & date_digit
                    Else
                        Prd_detail.Label6.Text = lotFirstDigit & lotSecondDigit & lotthirdDigit
                    End If
                Else
                    menu1.Enabled = False
                    menu4.Enabled = False
                    menu2.Enabled = False
                    menu3.Enabled = False
                    PictureBox8.Enabled = False
                    PictureBox1.Enabled = False
                    Dim listdetail = "Please enter employee information to start production."
                    PictureBox9.BringToFront()
                    PictureBox9.Show()
                    PictureBox11.BringToFront()
                    PictureBox11.Show()
                    Panel3.BringToFront()
                    Panel3.Show()
                    Label5.Text = listdetail
                    Label5.BringToFront()
                    Label5.Show()
                    ' MsgBox("กรุณาลงข้อมูลพนักงานเพื่อเริ่มการผลิต")
                End If
            Else
                load_show.Show()
                Me.Enabled = True
            End If
        Catch ex As Exception
            load_show.Show()
            Me.Enabled = True
        End Try
    End Sub
    Public Async Function Check_critical_flg() As Task(Of String)
        Dim rs = Backoffice_model.load_config_master_database()
        ' Try
        '     If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
        Dim critical_flg As String = ""
        If rs <> " " Then
            Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
            For Each item As Object In dict
                critical_flg = item("critical_flg").ToString()
            Next
        End If
        Return critical_flg
        '      Else
        '   load_show.Show()
        '      End If
        '   Catch ex As Exception
        '       load_show.Show()
        '   End Try
    End Function

    Private Async Sub menu1_Click_1(sender As Object, e As EventArgs) Handles menu1.Click
        Await CheckMemoryLeak()
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Await WaitForSQLiteEmptyAsync()
                Backoffice_model.gobal_Flg_autoTranferProductions = Await Backoffice_model.Check_detail_actual_insert_act(Me) 'กรณีเครื่องดับ'
                check_lot()
                'Prd_detail.Label2.Text = ListView1.Items.Count
                Working_Pro.Label24.Text = Label4.Text
                Dim i = List_Emp.ListView1.Items.Count
                If i > 0 Then
                    Prd_detail.Timer3.Enabled = True
                    Backoffice_model.SET_LINE_PRODUCTION(Label4.Text)
                    Insert_list.Label3.Text = Backoffice_model.GET_LINE_PRODUCTION()
                    Prd_detail.Label3.Text = Backoffice_model.GET_LINE_PRODUCTION()
                    'Sel_prod_start.Show()
                    rsCheckCriticalFlg = Await Check_critical_flg()
                    Await load_page()
                End If
            Else
                load_show.Show()
                Me.Enabled = True ' กัน หน้าจอ ล็อค
            End If
        Catch ex As Exception
            load_show.Show()
            Me.Enabled = True ' กัน หน้าจอ ล็อค
            ' MsgBox("Please Wait Trasnfer Data.")
        End Try
    End Sub
    Private Async Function WaitForSQLiteEmptyAsync() As Task
        Dim hasData As Boolean = True
        ' ฟังก์ชันช่วย ping แบบปลอดภัย ป้องกัน exception
        Dim SafePing As Func(Of String, Boolean) = Function(host As String) As Boolean
                                                       Try
                                                           Return My.Computer.Network.Ping(host)
                                                       Catch ex As InvalidOperationException
                                                           ' ไม่มี network connection
                                                           Return False
                                                       Catch ex As Exception
                                                           ' error อื่น ๆ
                                                           Return False
                                                       End Try
                                                   End Function
        Do
            If Not SafePing(Backoffice_model.svp_ping) Then
                Me.Enabled = False
                load_show.Show()
                Await Task.Delay(3000) ' รอ 3 วินาที ก่อนเช็คใหม่
                Continue Do
            Else
                If load_show.Visible Then load_show.Hide()
                Me.Enabled = True
            End If
            ModelSqliteDefect.cancelCloselotStatusUpdate("2")
            Dim LoadSQL = Backoffice_model.get_trdata_sqlite()
            Dim LoadSQL_tag_print_detail = Backoffice_model.get_tr_tag_print_detail()
            Dim LoadSQL_check_loss_actual = Backoffice_model.check_loss_actual()
            Dim LoadSQL_get_defect_tag_information = Backoffice_model.get_defect_tag_information()
            hasData = LoadSQL.HasRows OrElse LoadSQL_tag_print_detail.HasRows OrElse LoadSQL_get_defect_tag_information.HasRows OrElse LoadSQL_check_loss_actual > 0
            If hasData Then
                Me.Enabled = False
                Await dbClass.updated_data_to_dbsvr(Me, "1")
                Await Task.Delay(2000) ' รอ 2 วินาที ก่อนเช็คใหม่
            Else
                Me.Enabled = True
            End If
        Loop While hasData
    End Function

    Public Async Function load_page() As Task(Of String)
        Working_Pro.lb_nc_qty.Text = "0"
        Working_Pro.lb_ng_qty.Text = "0"
        'MsgBox(line_id.Text)
        Try
            ArrayDataPlan = New List(Of DataPlan)
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim LoadSQL_prd_plan As String = ""
                If rsCheckCriticalFlg = "0" Then
                    LoadSQL_prd_plan = Backoffice_model.Get_prd_plan_new(Label4.Text)
                    dataPlan = LoadSQL_prd_plan
                    Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(LoadSQL_prd_plan)
                    If LoadSQL_prd_plan <> " " Then
                        For Each item As Object In dict
                            ArrayDataPlan.Add(New DataPlan With {.IND_ROW = item("IND_ROW").ToString(), .PS_UNIT_NUMERATOR = "PS_UNIT_NUMERATOR", .CT = item("CT").ToString(), .seq_no = item("seq_no").ToString(), .WORK_ODR_DLV_DATE = item("WORK_ODR_DLV_DATE").ToString(), .LOCATION_PART = item("LOCATION_PART").ToString(), .MODEL = item("MODEL").ToString(), .PRODUCT_TYP = item("PRODUCT_TYP").ToString(), .wi = item("WI").ToString(), .item_cd = item("ITEM_CD").ToString(), .item_name = item("ITEM_NAME").ToString()})
                            chk_spec_line = item("chk_spec_line").ToString()
                            Working_Pro.Label27.Text = item("PS_UNIT_NUMERATOR").ToString()
                            Working_Pro.Product_type = item("PRODUCT_TYP").ToString()
                            Prd_detail.lb_snp.Text = item("PS_UNIT_NUMERATOR").ToString()
                            Prd_detail.lb_item_cd.Text = item("ITEM_CD").ToString()
                            Prd_detail.lb_item_name.Text = CStr(item("ITEM_NAME").ToString())
                            Prd_detail.lb_model.Text = item("MODEL").ToString()
                            Prd_detail.lb_plan_qty.Text = item("QTY").ToString()
                            Prd_detail.lb_remain_qty.Text = (item("QTY").ToString() - item("prd_qty_sum").ToString())
                            Prd_detail.lb_wi.Text = item("WI").ToString()
                            Prd_detail.LB_PLAN_DATE.Text = item("WORK_ODR_DLV_DATE").ToString().Substring(0, 10)
                        Next
                        Prd_detail.Show()
                    Else
                        dataPlan = ""
                        menu1.Enabled = False
                        menu4.Enabled = False
                        menu2.Enabled = False
                        menu3.Enabled = False
                        PictureBox8.Enabled = False
                        PictureBox1.Enabled = False
                        Dim listdetail = "Not have production plan !"
                        PictureBox9.BringToFront()
                        PictureBox9.Show()
                        PictureBox11.BringToFront()
                        PictureBox11.Show()
                        Panel3.BringToFront()
                        Panel3.Show()
                        Label5.Text = listdetail
                        Label5.BringToFront()
                        Label5.Show()
                        Me.Enabled = True
                    End If
                Else
                    chk_spec_line = "0"
                    ManagePlan.Show()
                End If
                Dim LoadSQLskill = Backoffice_model.Get_Line_skill_id(line_id.Text)
                While LoadSQLskill.Read()
                    List_Emp.ListBox1.Items.Add(LoadSQLskill("sk_id").ToString())
                End While
                LoadSQLskill.Close()
            End If
        Catch ex As Exception
            Console.WriteLine("error ===>" & ex.Message)
            load_show.Show()
            Me.Enabled = True
        End Try
    End Function
    Function GetLastDayOfMonth(ByVal CurrentDate As DateTime) As DateTime
        With CurrentDate
            Return (New DateTime(.Year, .Month, Date.DaysInMonth(.Year, .Month)))
        End With
    End Function
    Public Function set_data_Month(lotSubstMonth)
        Dim d As Date = DateTime.Now.ToString("dd-MM-yyyy")
        Dim GET_DATA As String = GetLastDayOfMonth(d)
        Dim re = GET_DATA.Substring(3, 2)
        Dim date_st As Integer = lotthirdDigit
        If lotSubstMonth = "01" Then
            lotSecondDigit = "A"
        ElseIf lotSubstMonth = "02" Then
            lotSecondDigit = "B"
        ElseIf lotSubstMonth = "03" Then
            lotSecondDigit = "C"
        ElseIf lotSubstMonth = "04" Then
            lotSecondDigit = "D"
        ElseIf lotSubstMonth = "05" Then
            lotSecondDigit = "E"
        ElseIf lotSubstMonth = "06" Then
            lotSecondDigit = "F"
        ElseIf lotSubstMonth = "07" Then
            lotSecondDigit = "G"
        ElseIf lotSubstMonth = "08" Then
            lotSecondDigit = "H"
        ElseIf lotSubstMonth = "09" Then
            lotSecondDigit = "I"
        ElseIf lotSubstMonth = "10" Then
            lotSecondDigit = "J"
        ElseIf lotSubstMonth = "11" Then
            lotSecondDigit = "K"
        ElseIf lotSubstMonth = "12" Then
            lotSecondDigit = "L"
        End If
        Return lotSecondDigit
    End Function
    Public Function set_data_Year(lotSubstYear)
        'MsgBox("(((((999")
        lotSubstYear = lotSubstYear.Substring(1, 1)
        'lotSubstYear = lotSubstYear - 1
        If lotSubstYear = "1" Then
            lotFirstDigit = "A"
        ElseIf lotSubstYear = "2" Then
            lotFirstDigit = "B"
        ElseIf lotSubstYear = "3" Then
            lotFirstDigit = "C"
        ElseIf lotSubstYear = "4" Then
            lotFirstDigit = "D"
        ElseIf lotSubstYear = "5" Then
            lotFirstDigit = "E"
        ElseIf lotSubstYear = "6" Then
            lotFirstDigit = "F"
        ElseIf lotSubstYear = "7" Then
            lotFirstDigit = "G"
        ElseIf lotSubstYear = "8" Then
            lotFirstDigit = "H"
        ElseIf lotSubstYear = "9" Then
            lotFirstDigit = "I"
        ElseIf lotSubstYear = "0" Then
            lotFirstDigit = "J"
        End If
        Return lotFirstDigit
    End Function
    Private Sub menu4_Click_1(sender As Object, e As EventArgs) Handles menu4.Click
        'Application.Exit()
        Confrm_end.Show()
    End Sub
    Private Sub menu3_Click_1(sender As Object, e As EventArgs)

    End Sub
    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs)
    End Sub
    Private Sub menu2_Click(sender As Object, e As EventArgs) Handles menu2.Click
        Conf_login.TextBox1.Select()
        Conf_login.Show()
        'List_Emp.Enabled = True
        'Line_conf.Show()
        Me.Enabled = False
    End Sub
    Private Async Sub Timer2_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles Timer2.Elapsed
        Await RunCmd(Label4.Text)
        Await CheckMemoryLeak()
        If Me.Enabled Then
            Await ShowInformationByStatus(Label6.Text, Label4.Text)
        End If
        '  Await ShowInformationByStatus(Label6.Text, Label4.Text)
        If isRunning Then Exit Sub
        isRunning = True
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Await dbClass.updated_data_to_dbsvr(Me, "2")
            End If
        Catch ex As Exception
            ' Optional: log error
        Finally
            isRunning = False
        End Try
    End Sub
    Private Sub menu3_Click_2(sender As Object, e As EventArgs) Handles menu3.Click
        Dim mdD = New modelDefect
        Backoffice_model.Check_detail_actual_insert_act(Me) 'กรณีเครื่องดับ'
        Dim data = mdD.mGetDatamsterLine(Label4.Text)
        If data <> "0" Then
            Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(data)
            For Each item As Object In dict
                chk_spec_line = item("chk_spec_line").ToString()
            Next
        End If
        Dim LoadSQL = Backoffice_model.get_information()
        While LoadSQL.Read()
            Adm_page.TextBox1.Text = LoadSQL("inf_txt").ToString
        End While
        Adm_page.TextBox1.SelectionStart = Adm_page.TextBox1.Text.Length
        Adm_page.Show()
        Me.Hide()
    End Sub
    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Hide()
        List_Emp.lb_link.Text = "main"
        List_Emp.Show()
        List_Emp.Enabled = False
        ' Me.Enabled = False
        Sc.TextBox2.Select()
        'Sc.Show()
        Sc.ShowDialog()
    End Sub
    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        load_worker()
    End Sub
    Public Sub load_worker()
        Dim i = List_Emp.ListView1.Items.Count
        'If i > 6 Then
        Dim t = New Show_Worker
        t.show()
        'End If
    End Sub
    Private Sub Test_Click(sender As Object, e As EventArgs)
        load_worker()
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        load_worker()
    End Sub
    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub
    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        PictureBox9.Hide()
        PictureBox11.Hide()
        Panel3.Hide()
        menu1.Enabled = True
        menu4.Enabled = True
        menu2.Enabled = True
        menu3.Enabled = True
        PictureBox8.Enabled = True
        PictureBox1.Enabled = True
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub Button1_Click_2(sender As Object, e As EventArgs)
        TEST_PRINTLABEL.Show()
    End Sub
    Private Sub PictureBox12_Click(sender As Object, e As EventArgs) Handles PictureBox12.Click

    End Sub
    Private Async Sub Button1_Click_3(sender As Object, e As EventArgs)
        Await AnotherAsyncMethod()
    End Sub
    Public Async Function AnotherAsyncMethod() As Task
        ' Call the Main() method asynchronously
        Await Main()
    End Function
    Public Async Function RunCmd(line_cd As String) As Task
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim api = New api()
                Dim Command As String = ""
                Dim parameters As String = ""
                Dim url = "http://" & Backoffice_model.svApi & "/API_NEW_FA/GET_DATA_NEW_FA/RunCmd?line_cd=" & line_cd
                Dim result_data As String = Await api.Load_data(url)
                If result_data <> "0" Then
                    Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
                    For Each item As Object In dict2
                        Command = item("command").ToString()
                        parameters = item("parameters").ToString()
                        System.Diagnostics.Process.Start(Command, parameters)
                    Next
                End If
            Else
                status_emergency = "0"
            End If
        Catch ex As Exception
            status_emergency = "0"
        End Try
    End Function
End Class