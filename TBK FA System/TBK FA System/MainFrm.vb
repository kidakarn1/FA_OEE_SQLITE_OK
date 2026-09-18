Imports System.IO
Imports System.IO.Pipes
Imports System.Threading
Imports System.Web.Script.Serialization
Imports System.Drawing.Drawing2D
Imports Microsoft.Web
Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms
Public Class MainFrm
    Private WithEvents WebViewEmergency As WebView2
    Private Shared _productionStartFlowActive As Integer
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
            ''msgBox("No process found!")
        Else
            ''msgBox(" process found!")
        End If
    End Sub
    Public Function CheckIfRunning()
        p = Process.GetProcessesByName("TBK FA System")
        If p.Count > 1 Then
            Application.Exit()
            Return 1
            'Process Is running
        Else
            Return 0
            ' Process is not running
        End If
    End Function
    Public Sub check_close_fa()
        If Application.OpenForms().OfType(Of MainFrm).Any Then
        Else
            'msgBox("end program")
        End If
    End Sub
    Public Async Function Main() As Task
        ' Create PipeServer
        Dim pipeServer As New NamedPipeServerStream("mypipe", PipeDirection.In)
        ''Console.WriteLine("Waiting for connection...")

        ' Wait asynchronously for a connection from the client
        Await pipeServer.WaitForConnectionAsync()
        ''Console.WriteLine("Client connected.")

        ' Read command from the client
        Dim reader As New StreamReader(pipeServer)
        Dim command As String = Await reader.ReadLineAsync()
        ''Console.WriteLine("Received command from client: " & command)

        ' Check the command
        If command = "click button" Then
            reader.Close()
            pipeServer.Close()
        ElseIf command = "Wait_DATA" Then
            ''Console.WriteLine("Wait_DATA")
        End If
        ' Close the connection
        ''Console.WriteLine("close Connection main")
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

            'Console.WriteLine(infoUrl)

            Dim result_data As String = Await api.Load_dataAsync(infoUrl)

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
            'msgBox("ไม่สามารถโหลดข้อมูลได้ กรุณาตรวจสอบเครือข่าย", 'msgBoxStyle.Exclamation)
        End Try
    End Function
    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        Try : Timer2.SynchronizingObject = Me : Catch : End Try
    End Sub

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'CheckSetingMachine.sentParameterLossIO(False)
        check_process()
        If CheckIfRunning() = 0 Then
            Await Task.Run(Sub()
                               dbClass.GetLocalServerAPI()
                               dbClass.GetLocalServerping()
                               dbClass.GetLocalServerOEE()
                               dbClass.sqlite_conn_dbsv()
                           End Sub)
            If Not Await WaitForSQLiteEmptyAsync() Then
                Me.Close()
                Return
            End If
            ' Await dbClass.updated_data_to_dbsvr(Me, "1")
            Timer1.Start()
            Timer2.Start()
            Dim sqlss = Backoffice_model.ConnectDBSQLite()
            If sqlss Is Nothing Then
                Backoffice_model.LogPerformance("MainFrm.LocalLineConfiguration | ReaderUnavailable", 0)
                MessageBox.Show("Local line configuration could not be loaded." & vbCrLf &
                                "Please check the local FA database/configuration.",
                                "Local Configuration",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
                Me.Close()
                Return
            End If

            Dim configuredPd As String = String.Empty
            Dim configuredLineCode As String = String.Empty
            Dim configuredCountType As String = String.Empty
            Dim configuredCavity As String = String.Empty
            Dim configuredScannerPort As String = String.Empty
            Dim configuredPrinterPort As String = String.Empty
            Dim configuredDioPort As String = String.Empty
            Try
                While sqlss.Read()
                    configuredPd = Convert.ToString(sqlss("pd")).Trim()
                    configuredLineCode = Convert.ToString(sqlss("line_cd")).Trim()
                    configuredCountType = Convert.ToString(sqlss("count_type"))
                    configuredCavity = Convert.ToString(sqlss("cavity"))
                    configuredScannerPort = Convert.ToString(sqlss("scanner_port"))
                    configuredPrinterPort = Convert.ToString(sqlss("printer_port"))
                    configuredDioPort = Convert.ToString(sqlss("dio_port"))
                End While
            Catch ex As Exception
                Backoffice_model.LogPerformance("MainFrm.LocalLineConfiguration | ReadFailed", 0)
                MessageBox.Show("Local line configuration could not be loaded." & vbCrLf &
                                "Please check the local FA database/configuration.",
                                "Local Configuration",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
                Me.Close()
                Return
            Finally
                Try
                    sqlss.Close()
                Catch ex As Exception
                    Backoffice_model.LogPerformance("MainFrm.LocalLineConfiguration | ReaderCloseFailed", 0)
                End Try
            End Try

            If String.IsNullOrWhiteSpace(configuredPd) OrElse String.IsNullOrWhiteSpace(configuredLineCode) Then
                Backoffice_model.LogPerformance("MainFrm.LocalLineConfiguration | MissingPdOrLine", 0)
                MessageBox.Show("Local line configuration is incomplete." & vbCrLf &
                                "Please configure a valid PD and Line Code in the local FA database.",
                                "Local Configuration",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
                Me.Close()
                Return
            End If

            Label6.Text = configuredPd
            Label4.Text = configuredLineCode
            count_type.Text = configuredCountType
            cavity.Text = configuredCavity
            lb_scanner_port.Text = configuredScannerPort
            lb_printer_port.Text = configuredPrinterPort
            lb_dio_port.Text = configuredDioPort
            Backoffice_model.SCANNER_PORT = configuredScannerPort
            If Backoffice_model.SCANNER_PORT <> "" AndAlso Backoffice_model.SCANNER_PORT <> "USB" Then
                lb_ctrl_sc_flg.Text = "emp"
            End If
            Insert_list.Label3.Text = Label4.Text
            Prd_detail.Label3.Text = Label4.Text
            'Await F_UpdateSqlite()
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
        'msgBox("New version")
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
    Public Async Function check_lot() As Task
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
                    ' Me.Enabled = False
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
                    If time_now >= "00:00:00 AM" And time_now <= "07:59:59 AM" Then
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
                                ''msgBox(tmp_date)
                                ''msgBox("day = " & tmp_date.Substring(0, 2))
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
                    ' 'msgBox("กรุณาลงข้อมูลพนักงานเพื่อเริ่มการผลิต")
                End If
            Else
                load_show.Show()
                Me.Enabled = True
            End If
        Catch ex As Exception
            load_show.Show()
            Me.Enabled = True
        End Try
    End Function
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
        If Interlocked.CompareExchange(_productionStartFlowActive, 1, 0) <> 0 Then
            Console.WriteLine("[PRODUCTION-START] Duplicate click ignored.")
            Return
        End If

        Try
            menu1.Enabled = False
            Await CheckMemoryLeak()
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                If Not Await WaitForSQLiteEmptyAsync() Then Return
                Backoffice_model.gobal_Flg_autoTranferProductions = Await Backoffice_model.Check_detail_actual_insert_act(Me) 'กรณีเครื่องดับ'
                Await check_lot()
                'Prd_detail.Label2.Text = ListView1.Items.Count
                Dim i = List_Emp.ListView1.Items.Count
                If i > 0 Then
                    ProductionStartFlowState.Reset(True)
                    If Backoffice_model.gobal_Flg_autoTranferProductions = 1 Then
                        ' Computer-down recovery must keep the existing automatic
                        ' resume path and must not ask the operator to choose again.
                        ProductionStartFlowState.Reset()
                    Else
                        ' The Current-WI incomplete-box check below is now the single
                        ' production-start decision surface.  Start as NewBox, then
                        ' show its warning only when eligible pending boxes exist.
                        ' Sel_prod_start remains available in the project for legacy
                        ' references, but must not be shown in this normal entry flow.
                        ProductionStartFlowState.SelectedMode = ProductionStartMode.NewBox
                    End If

                    Working_Pro.Label24.Text = Label4.Text
                    Prd_detail.Timer3.Enabled = True
                    Backoffice_model.SET_LINE_PRODUCTION(Label4.Text)
                    Insert_list.Label3.Text = Backoffice_model.GET_LINE_PRODUCTION()
                    Prd_detail.Label3.Text = Backoffice_model.GET_LINE_PRODUCTION()
                    rsCheckCriticalFlg = Await Check_critical_flg()
                    ' Load the current plan first. Part-based incomplete-box
                    ' detection needs the current Part and runtime SNP.
                    Await load_page(False)

                    If ProductionStartFlowState.SelectedMode = ProductionStartMode.NewBox Then
                        Dim startNewBoxWarningAcknowledged As Boolean = False
                        ' The normal standard-tag flow creates the next sequence
                        ' from this displayed prior sequence.  Discover and log
                        ' line-level older ACTIVE transfers before this fresh
                        ' candidate query; the recovery routine never selects a
                        ' source or starts production.
                        Dim candidateSequence As String = String.Empty
                        Dim priorSequence As Integer
                        If Integer.TryParse(Trim(Prd_detail.lb_seq.Text), priorSequence) AndAlso priorSequence >= 0 Then
                            candidateSequence = (priorSequence + 1).ToString()
                        ElseIf ArrayDataPlan IsNot Nothing AndAlso ArrayDataPlan.Count > 0 Then
                            candidateSequence = Trim(ArrayDataPlan(0).seq_no)
                        End If
                        Dim oldActiveRecoveries As List(Of IncompleteTransferCrashRecoveryRecord) = Nothing
                        Dim oldActiveReason As String = String.Empty
                        If Backoffice_model.GetCrashRecoveryActiveTransfersForLine(Label4.Text, oldActiveRecoveries, oldActiveReason) AndAlso
                           oldActiveRecoveries IsNot Nothing AndAlso oldActiveRecoveries.Count > 0 Then
                            Await LoadOldActiveRecoveryQuantitiesAsync(oldActiveRecoveries)
                            ' Use the same operator decision that precedes the
                            ' normal incomplete-box picker.  Choosing Start New
                            ' deliberately leaves recovery unselected; choosing
                            ' View opens the unchanged picker with recovery rows.
                            Dim recoveryChoice As ProductionStartMode = ShowIncompleteBoxWarning(New IncompleteBoxRecord())
                            If recoveryChoice = ProductionStartMode.None Then
                                ProductionStartFlowState.Reset(False)
                                Me.Enabled = True
                                Return
                            End If
                            If recoveryChoice = ProductionStartMode.ContinueExistingBox Then
                                Dim selectedOldActive As IncompleteTransferCrashRecoveryRecord = SelectOldActiveRecoveryBeforePlanConfirmation(oldActiveRecoveries)
                                If selectedOldActive Is Nothing Then
                                    ProductionStartFlowState.Reset(False)
                                    Me.Enabled = True
                                    Return
                                End If
                                ProductionStartFlowState.SelectedOldActiveRecovery = selectedOldActive
                                ProductionStartFlowState.OldActiveRecoverySeedApplied = False
                                Dim transfer = selectedOldActive.Transfer
                                Console.WriteLine("[OLD-RECOVERY] SELECT HBL=" & transfer.TransferId.ToString() &
                                                  " Tag=" & transfer.SourceTagId.ToString() &
                                                  " Recovered=" & selectedOldActive.RecoveredQty.ToString() &
                                                  " NewPWI=pending NewSeq=" & candidateSequence)
                            Else
                                ProductionStartFlowState.SelectedOldActiveRecovery = Nothing
                                ProductionStartFlowState.OldActiveRecoverySeedApplied = False
                                startNewBoxWarningAcknowledged = True
                                Console.WriteLine("[INCOMPLETE-WARNING] DECISION=NEW_BOX source=OLD-RECOVERY; pending warning skipped.")
                            End If
                        ElseIf Not String.IsNullOrWhiteSpace(oldActiveReason) Then
                            Console.WriteLine("[OLD-RECOVERY] DISCOVERY ERROR | " & oldActiveReason)
                        End If

                        ' A chosen old ACTIVE recovery box is a NewBox packaging
                        ' hand-off, never a normal Continue candidate.  Do not let
                        ' the normal picker replace that explicit selection.
                        If ProductionStartFlowState.SelectedOldActiveRecovery Is Nothing AndAlso
                           Not startNewBoxWarningAcknowledged Then
                            Dim incompleteBoxes = Backoffice_model.GetIncompleteBoxes(
                                Prd_detail.lb_wi.Text,
                                Label4.Text,
                                Prd_detail.lb_item_cd.Text,
                                CInt(Val(Prd_detail.lb_snp.Text)),
                                Backoffice_model.F_NEXT_PROCESS(Prd_detail.lb_item_cd.Text),
                                False,
                                True,
                                True)

                            If incompleteBoxes.Count > 0 Then
                                Dim newBoxChoice As ProductionStartMode = ShowIncompleteBoxWarning(incompleteBoxes(0))
                                If newBoxChoice = ProductionStartMode.None Then
                                    ProductionStartFlowState.Reset(True)
                                    Me.Enabled = True
                                    Return
                                End If
                                ProductionStartFlowState.SelectedMode = newBoxChoice
                            End If
                        End If
                    End If

                    If ProductionStartFlowState.SelectedMode = ProductionStartMode.ContinueExistingBox Then
                        ' Type 2 uses the legacy main/sub-tag contract.  Continue
                        ' Existing Box is intentionally limited to the standard
                        ' tag_print_detail route for this release.
                        If String.Equals(Backoffice_model.GetCurrentLineTagType(), "2", StringComparison.OrdinalIgnoreCase) Then
                            MessageBox.Show("Continue Existing Box is not supported for this tag type." & vbCrLf &
                                            "You may start a new box or cancel.",
                                            "Continue Existing Box",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information)
                            ProductionStartFlowState.Reset(False)
                            Me.Enabled = True
                            Return
                        End If

                        Dim selectedBox As IncompleteBoxRecord = SelectIncompleteBoxBeforePlanConfirmation()
                        If selectedBox Is Nothing Then
                            ProductionStartFlowState.Reset(True)
                            Me.Enabled = True
                            Return
                        End If

                        ProductionStartFlowState.SelectedBox = selectedBox
                    End If

                    ' This is the same legacy plan-confirmation screen used by
                    ' Start New Box.  No counter or production-start behaviour is
                    ' changed by the Phase 1 warning.
                    Me.Enabled = False
                    Prd_detail.Show()
                End If
            Else
                load_show.Show()
                Me.Enabled = True ' กัน หน้าจอ ล็อค
            End If
        Catch ex As Exception
            ProductionStartFlowState.Reset(True)
            load_show.Show()
            Me.Enabled = True ' กัน หน้าจอ ล็อค
            MessageBox.Show("Unable to open the production flow." & vbCrLf & ex.Message,
                            "Start Production",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
            ' 'msgBox("Please Wait Trasnfer Data.")
        Finally
            If Not IsDisposed Then menu1.Enabled = True
            Interlocked.Exchange(_productionStartFlowActive, 0)
        End Try
    End Sub

    Private Sub MainFrm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ProductionStartFlowState.Reset(True)
    End Sub
    Private Sub CloseSQLiteQueueReader(reader As Object, readerName As String)
        If reader Is Nothing Then Return

        Try
            reader.Close()
        Catch ex As Exception
            Backoffice_model.LogPerformance("MainFrm.SQLiteQueue | " & readerName & " | CloseFailed | " & ex.GetType().Name & " | " & ex.Message, 0)
        Finally
            Try
                reader.Dispose()
            Catch ex As Exception
                Backoffice_model.LogPerformance("MainFrm.SQLiteQueue | " & readerName & " | DisposeFailed | " & ex.GetType().Name & " | " & ex.Message, 0)
            End Try
        End Try
    End Sub

    Private Function StopStartupForSQLiteQueueFailure(getterName As String, exceptionType As String, exceptionMessage As String) As Boolean
        Backoffice_model.LogPerformance("MainFrm.SQLiteQueue | " & getterName & " | " & exceptionType & " | " & exceptionMessage, 0)

        Try
            If load_show IsNot Nothing Then load_show.Hide()
        Catch ex As Exception
        End Try

        Me.Enabled = False
        MessageBox.Show("Local FA database could not be read." & vbCrLf &
                        "Production cannot start safely." & vbCrLf &
                        "Please contact System Service.",
                        "Local FA Database",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        Return False
    End Function

    Private Async Function WaitForSQLiteEmptyAsync() As Task(Of Boolean)
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
            Dim LoadSQL_by_op As Object = Nothing
            Dim LoadSQL As Object = Nothing
            Dim LoadSQL_tag_print_detail As Object = Nothing
            Dim LoadSQL_check_loss_actual As Object = Nothing
            Dim LoadSQL_get_defect_tag_information As Object = Nothing

            Try
                Try
                    LoadSQL_by_op = Backoffice_model.get_trdata_sqlite_by_op()
                Catch ex As Exception
                    Return StopStartupForSQLiteQueueFailure("get_trdata_sqlite_by_op", ex.GetType().Name, ex.Message)
                End Try
                If LoadSQL_by_op Is Nothing Then Return StopStartupForSQLiteQueueFailure("get_trdata_sqlite_by_op", "ReaderUnavailable", "The local SQLite reader returned Nothing.")

                Try
                    LoadSQL = Backoffice_model.get_trdata_sqlite()
                Catch ex As Exception
                    Return StopStartupForSQLiteQueueFailure("get_trdata_sqlite", ex.GetType().Name, ex.Message)
                End Try
                If LoadSQL Is Nothing Then Return StopStartupForSQLiteQueueFailure("get_trdata_sqlite", "ReaderUnavailable", "The local SQLite reader returned Nothing.")

                Try
                    LoadSQL_tag_print_detail = Backoffice_model.get_tr_tag_print_detail()
                Catch ex As Exception
                    Return StopStartupForSQLiteQueueFailure("get_tr_tag_print_detail", ex.GetType().Name, ex.Message)
                End Try
                If LoadSQL_tag_print_detail Is Nothing Then Return StopStartupForSQLiteQueueFailure("get_tr_tag_print_detail", "ReaderUnavailable", "The local SQLite reader returned Nothing.")

                Try
                    LoadSQL_get_defect_tag_information = Backoffice_model.get_defect_tag_information()
                Catch ex As Exception
                    Return StopStartupForSQLiteQueueFailure("get_defect_tag_information", ex.GetType().Name, ex.Message)
                End Try
                If LoadSQL_get_defect_tag_information Is Nothing Then Return StopStartupForSQLiteQueueFailure("get_defect_tag_information", "ReaderUnavailable", "The local SQLite reader returned Nothing.")

                Try
                    LoadSQL_check_loss_actual = Backoffice_model.check_loss_actual()
                Catch ex As Exception
                    Return StopStartupForSQLiteQueueFailure("check_loss_actual", ex.GetType().Name, ex.Message)
                End Try
                If LoadSQL_check_loss_actual Is Nothing Then Return StopStartupForSQLiteQueueFailure("check_loss_actual", "ResultUnavailable", "The local SQLite query returned Nothing.")

                hasData = LoadSQL.HasRows OrElse LoadSQL_tag_print_detail.HasRows OrElse LoadSQL_get_defect_tag_information.HasRows OrElse Convert.ToInt32(LoadSQL_check_loss_actual) > 0 OrElse LoadSQL_by_op.HasRows
            Catch ex As Exception
                Return StopStartupForSQLiteQueueFailure("WaitForSQLiteEmptyAsync", ex.GetType().Name, ex.Message)
            Finally
                CloseSQLiteQueueReader(LoadSQL_by_op, "get_trdata_sqlite_by_op")
                CloseSQLiteQueueReader(LoadSQL, "get_trdata_sqlite")
                CloseSQLiteQueueReader(LoadSQL_tag_print_detail, "get_tr_tag_print_detail")
                CloseSQLiteQueueReader(LoadSQL_get_defect_tag_information, "get_defect_tag_information")
            End Try
            If hasData Then
                Me.Enabled = False
                Await dbClass.updated_data_to_dbsvr(Me, "1")
                Await Task.Delay(2000) ' รอ 2 วินาที ก่อนเช็คใหม่
            Else
                Me.Enabled = True
            End If
        Loop While hasData
        Return True
    End Function

    Private Async Function LoadOldActiveRecoveryQuantitiesAsync(recoveries As List(Of IncompleteTransferCrashRecoveryRecord)) As Task
        If recoveries Is Nothing OrElse recoveries.Count = 0 Then Return

        ' Collect all candidate (CurrentPwi, CurrentSeq) pairs for batched query
        Dim pairs As New List(Of Tuple(Of String, String))()
        For Each item In recoveries
            If item IsNot Nothing AndAlso item.Transfer IsNot Nothing Then
                pairs.Add(New Tuple(Of String, String)(item.Transfer.CurrentPwi, item.Transfer.CurrentSeq))
            End If
        Next

        Dim batchMovements As Dictionary(Of String, Long) = Nothing
        Dim batchReason As String = String.Empty
        Dim batchSuccess As Boolean = Await Task.Run(Function()
                                                          Return Backoffice_model.GetProductionActualDetailNetMovementsBatch(
                                                              pairs, batchMovements, batchReason)
                                                      End Function)

        For Each recovery As IncompleteTransferCrashRecoveryRecord In recoveries
            If recovery Is Nothing OrElse recovery.Transfer Is Nothing Then Continue For
            Dim transfer = recovery.Transfer

            If String.IsNullOrWhiteSpace(transfer.CurrentPwi) OrElse String.IsNullOrWhiteSpace(transfer.CurrentSeq) Then
                recovery.DetailQuerySucceeded = False
                recovery.DetailQueryReason = "Historical PWI or sequence is unavailable."
                recovery.NetMovement = 0
            ElseIf Not batchSuccess Then
                recovery.DetailQuerySucceeded = False
                recovery.DetailQueryReason = batchReason
                recovery.NetMovement = 0
            Else
                Dim key As String = transfer.CurrentPwi.Trim() & "|" & transfer.CurrentSeq.Trim()
                Dim netMovement As Long = 0
                If batchMovements IsNot Nothing AndAlso batchMovements.TryGetValue(key, netMovement) Then
                    recovery.NetMovement = netMovement
                Else
                    ' Valid pair with no detail rows recorded in production_actual_detail: movement is 0
                    recovery.NetMovement = 0
                End If
                recovery.DetailQuerySucceeded = True
                recovery.DetailQueryReason = String.Empty

                If recovery.RecoveredQty > Integer.MaxValue OrElse recovery.RecoveredQty < Integer.MinValue Then
                    recovery.DetailQuerySucceeded = False
                    recovery.DetailQueryReason = "Recovered quantity is outside the packaging counter range."
                End If
            End If

            If recovery.DetailQuerySucceeded Then
                Console.WriteLine("[OLD-RECOVERY] HBL=" & transfer.TransferId.ToString() &
                                  " OldPWI=" & transfer.CurrentPwi &
                                  " OldSeq=" & transfer.CurrentSeq &
                                  " Base=" & transfer.BaseQty.ToString() &
                                  " Net=" & recovery.NetMovement.ToString() &
                                  " Recovered=" & recovery.RecoveredQty.ToString())
            Else
                Console.WriteLine("[OLD-RECOVERY] ERROR HBL=" & transfer.TransferId.ToString() &
                                  " OldPWI=" & transfer.CurrentPwi &
                                  " OldSeq=" & transfer.CurrentSeq &
                                  " Reason=" & recovery.DetailQueryReason)
            End If
        Next
    End Function

    ' Supplies recovery rows to the existing IncompleteBoxSelect layout.  The
    ' only displayed quantity change is Quantity=RecoveredQty for these rows;
    ' normal candidate records continue to be loaded by their existing path.
    Private Function SelectOldActiveRecoveryBeforePlanConfirmation(recoveries As List(Of IncompleteTransferCrashRecoveryRecord)) As IncompleteTransferCrashRecoveryRecord
        Dim recoveryBoxes As New List(Of IncompleteBoxRecord)()
        Dim recoveryByTagId As New Dictionary(Of Integer, IncompleteTransferCrashRecoveryRecord)()
        Dim currentSnp As Integer = CInt(Val(Prd_detail.lb_snp.Text))

        For Each recovery As IncompleteTransferCrashRecoveryRecord In recoveries
            If recovery Is Nothing OrElse recovery.Transfer Is Nothing OrElse Not recovery.DetailQuerySucceeded Then Continue For
            Dim transfer = recovery.Transfer
            If transfer.SourceTagId <= 0 OrElse recoveryByTagId.ContainsKey(transfer.SourceTagId) Then Continue For

            recoveryBoxes.Add(New IncompleteBoxRecord With {
                              .TagId = transfer.SourceTagId,
                              .Wi = transfer.CurrentWi,
                              .PwiId = transfer.CurrentPwi,
                              .SeqNo = transfer.CurrentSeq,
                              .BoxNo = transfer.SourceBoxNo,
                              .Quantity = CInt(recovery.RecoveredQty),
                              .Snp = If(transfer.CurrentSnp > 0, transfer.CurrentSnp, currentSnp),
                              .LotNo = recovery.LotNo,
                              .Shift = recovery.Shift,
                              .CreatedDate = recovery.CreatedDate})
            recoveryByTagId.Add(transfer.SourceTagId, recovery)
        Next

        If recoveryBoxes.Count = 0 Then
            MessageBox.Show("No old ACTIVE box has an available production-detail quantity. Please retry after the connection is restored.",
                            "Incomplete Box", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If

        Backoffice_model.NEXT_PROCESS = Backoffice_model.F_NEXT_PROCESS(Prd_detail.lb_item_cd.Text)
        Using picker As New IncompleteBoxSelect(ProductionStartMode.ContinueExistingBox,
                                                Prd_detail.lb_wi.Text,
                                                Label4.Text,
                                                Prd_detail.lb_item_cd.Text,
                                                Prd_detail.lb_item_name.Text,
                                                Prd_detail.lb_model.Text,
                                                currentSnp,
                                                Backoffice_model.NEXT_PROCESS,
                                                recoveryBoxes,
                                                recoveryByTagId)
            If picker.ShowDialog(Me) = DialogResult.OK Then Return picker.SelectedOldActiveRecovery
        End Using
        Return Nothing
    End Function

    Private Function SelectIncompleteBoxBeforePlanConfirmation() As IncompleteBoxRecord
        If chk_spec_line = "2" Then
            MessageBox.Show("This selection flow currently supports normal FA tags only.",
                            "Incomplete Box",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
            Return Nothing
        End If

        Dim snp As Integer = CInt(Val(Prd_detail.lb_snp.Text))
        If snp <= 0 Then
            MessageBox.Show("SNP is invalid. Please reload the production plan.",
                            "Incomplete Box",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Return Nothing
        End If

        Backoffice_model.NEXT_PROCESS = Backoffice_model.F_NEXT_PROCESS(Prd_detail.lb_item_cd.Text)
        Using picker As New IncompleteBoxSelect(ProductionStartFlowState.SelectedMode,
                                                Prd_detail.lb_wi.Text,
                                                Label4.Text,
                                                Prd_detail.lb_item_cd.Text,
                                                Prd_detail.lb_item_name.Text,
                                                Prd_detail.lb_model.Text,
                                                snp,
                                                Backoffice_model.NEXT_PROCESS)
            If picker.ShowDialog(Me) = DialogResult.OK Then Return picker.SelectedBox
        End Using
        Return Nothing
    End Function

    ' Phase 1: this warning is intentionally local to Start New Box.  It does
    ' not claim, alter, or otherwise change the pending incomplete tag.
    Private Function ShowIncompleteBoxWarning(box As IncompleteBoxRecord) As ProductionStartMode
        If box Is Nothing Then Return ProductionStartMode.NewBox

        Using dialog As New Form()
            dialog.Text = "Incomplete Box Detected"
            dialog.FormBorderStyle = FormBorderStyle.None
            dialog.StartPosition = FormStartPosition.CenterParent
            dialog.MinimizeBox = False
            dialog.MaximizeBox = False
            dialog.ShowInTaskbar = False
            dialog.AutoScaleMode = AutoScaleMode.None
            dialog.ClientSize = New Size(720, 516)
            dialog.BackColor = Color.FromArgb(247, 251, 255)
            dialog.Region = CreateRoundedRegion(New Rectangle(0, 0, dialog.ClientSize.Width, dialog.ClientSize.Height), 24)
            AddHandler dialog.Paint, Sub(sender, e)
                                         Using borderPen As New Pen(Color.FromArgb(19, 49, 76), 2)
                                             Using borderPath As GraphicsPath = CreateRoundedPath(New Rectangle(1, 1, dialog.ClientSize.Width - 3, dialog.ClientSize.Height - 3), 24)
                                                 e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                                                 e.Graphics.DrawPath(borderPen, borderPath)
                                             End Using
                                         End Using
                                     End Sub

            Dim title As New Label With {
                .Text = "INCOMPLETE BOX DETECTED",
                .ForeColor = Color.FromArgb(24, 35, 45),
                .BackColor = Color.FromArgb(255, 197, 37),
                .Font = New Font("Segoe UI", 24.0!, FontStyle.Bold),
                .TextAlign = ContentAlignment.MiddleCenter,
                .Location = New Point(0, 0),
                .Size = New Size(720, 86)
            }

            Dim warningIcon As New Panel With {
                .BackColor = Color.Transparent,
                .Location = New Point(24, 22),
                .Size = New Size(46, 46)
            }
            AddHandler warningIcon.Paint, Sub(sender, e)
                                              e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                                              Dim points() As Point = {New Point(23, 1), New Point(45, 42), New Point(1, 42)}
                                              Using fillBrush As New SolidBrush(Color.FromArgb(31, 36, 43)), outlinePen As New Pen(Color.FromArgb(31, 36, 43), 2)
                                                  e.Graphics.FillPolygon(fillBrush, points)
                                                  e.Graphics.DrawPolygon(outlinePen, points)
                                              End Using
                                              Using iconFont As New Font("Segoe UI", 19.0!, FontStyle.Bold), textBrush As New SolidBrush(Color.FromArgb(255, 197, 37))
                                                  e.Graphics.DrawString("!", iconFont, textBrush, New PointF(17, 8))
                                              End Using
                                          End Sub

            Dim packageImage As New PictureBox With {
                .Image = My.Resources.IncompleteBoxWarning,
                .BackColor = Color.Transparent,
                .SizeMode = PictureBoxSizeMode.Zoom,
                .Location = New Point(30, 108),
                .Size = New Size(264, 228),
                .TabStop = False
            }

            Dim detailsMain As New Label With {
                .Text = "There are incomplete" & vbCrLf & "boxes in the current" & vbCrLf & "production.",
                .ForeColor = Color.FromArgb(22, 45, 68),
                .Font = New Font("Segoe UI", 19.0!, FontStyle.Bold),
                .TextAlign = ContentAlignment.MiddleLeft,
                .Location = New Point(319, 125),
                .Size = New Size(360, 115)
            }
            Dim detailsSub As New Label With {
                .Text = "Please review them before" & vbCrLf & "starting a new box.",
                .ForeColor = Color.FromArgb(67, 84, 102),
                .Font = New Font("Segoe UI", 13.5!, FontStyle.Regular),
                .TextAlign = ContentAlignment.MiddleLeft,
                .Location = New Point(322, 245),
                .Size = New Size(336, 58)
            }

            Dim result As ProductionStartMode = ProductionStartMode.None
            Dim continueButton As New Button With {.Text = String.Empty, .AccessibleName = "VIEW INCOMPLETE BOXES", .DialogResult = DialogResult.None, .Location = New Point(18, 403), .Size = New Size(216, 84)}
            Dim anywayButton As New Button With {.Text = String.Empty, .AccessibleName = "START NEW BOX ANYWAY", .DialogResult = DialogResult.None, .Location = New Point(252, 403), .Size = New Size(216, 84)}
            Dim cancelButton As New Button With {.Text = String.Empty, .AccessibleName = "CANCEL", .DialogResult = DialogResult.None, .Location = New Point(486, 403), .Size = New Size(216, 84)}
            ConfigureIncompleteBoxActionButton(continueButton, Color.FromArgb(37, 121, 214), True)
            ConfigureIncompleteBoxActionButton(anywayButton, Color.FromArgb(28, 164, 81), False)
            ConfigureIncompleteBoxCancelButton(cancelButton)

            Dim decisionTaken As Boolean = False
            Dim completeDecision As Action(Of ProductionStartMode) =
                Sub(selectedMode)
                    If decisionTaken Then
                        Console.WriteLine("[INCOMPLETE-WARNING] Duplicate modal click ignored.")
                        Return
                    End If
                    decisionTaken = True
                    continueButton.Enabled = False
                    anywayButton.Enabled = False
                    cancelButton.Enabled = False
                    result = selectedMode
                    Console.WriteLine("[INCOMPLETE-WARNING] Modal decision=" & selectedMode.ToString())
                    dialog.Close()
                End Sub

            Dim closeButton As New Button With {
                .Text = "×", .FlatStyle = FlatStyle.Flat, .BackColor = Color.FromArgb(255, 197, 37),
                .ForeColor = Color.FromArgb(24, 35, 45), .Font = New Font("Segoe UI", 22.0!, FontStyle.Bold),
                .Location = New Point(665, 22), .Size = New Size(36, 36), .TabStop = False
            }
            closeButton.FlatAppearance.BorderSize = 0
            Dim cancelDialog As Action = Sub() completeDecision(ProductionStartMode.None)
            AddHandler closeButton.Click, Sub() cancelDialog()

            AddHandler continueButton.Click, Sub() completeDecision(ProductionStartMode.ContinueExistingBox)
            AddHandler anywayButton.Click, Sub() completeDecision(ProductionStartMode.NewBox)
            AddHandler cancelButton.Click, Sub() cancelDialog()

            dialog.CancelButton = cancelButton
            dialog.Controls.AddRange(New Control() {title, warningIcon, packageImage, detailsMain, detailsSub, continueButton, anywayButton, cancelButton, closeButton})
            dialog.ShowDialog(Me)
            Return result
        End Using
    End Function

    Private Shared Function CreateRoundedPath(bounds As Rectangle, radius As Integer) As GraphicsPath
        Dim diameter As Integer = Math.Max(2, radius * 2)
        Dim path As New GraphicsPath()
        path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90)
        path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90)
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90)
        path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    Private Shared Function CreateRoundedRegion(bounds As Rectangle, radius As Integer) As Region
        Using path As GraphicsPath = CreateRoundedPath(bounds, radius)
            Return New Region(path)
        End Using
    End Function

    Private Shared Sub ConfigureIncompleteBoxActionButton(button As Button, baseColor As Color, isViewButton As Boolean)
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
        button.BackColor = baseColor
        button.ForeColor = Color.White
        button.Font = New Font("Segoe UI", 13.0!, FontStyle.Bold)
        button.TextAlign = ContentAlignment.MiddleCenter
        button.Padding = New Padding(14, 0, 14, 0)
        button.Region = CreateRoundedRegion(New Rectangle(0, 0, button.Width, button.Height), 11)
        AddHandler button.MouseEnter, Sub() button.BackColor = ControlPaint.Light(baseColor, 0.12F)
        AddHandler button.MouseLeave, Sub() button.BackColor = baseColor
        AddHandler button.Paint, Sub(sender, e)
                                     e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                                     Dim iconArea As New Rectangle(12, 13, 58, 58)
                                     Dim textArea As New Rectangle(74, 7, Math.Max(1, button.ClientSize.Width - 88), Math.Max(1, button.ClientSize.Height - 14))
                                     Using iconPen As New Pen(Color.White, 2.6F)
                                         If isViewButton Then
                                             e.Graphics.DrawRectangle(iconPen, iconArea.X + 12, iconArea.Y + 8, 27, 32)
                                             e.Graphics.DrawLine(iconPen, iconArea.X + 18, iconArea.Y + 16, iconArea.X + 33, iconArea.Y + 16)
                                             e.Graphics.DrawLine(iconPen, iconArea.X + 18, iconArea.Y + 24, iconArea.X + 33, iconArea.Y + 24)
                                             e.Graphics.DrawLine(iconPen, iconArea.X + 18, iconArea.Y + 32, iconArea.X + 28, iconArea.Y + 32)
                                         Else
                                             e.Graphics.DrawRectangle(iconPen, iconArea.X + 11, iconArea.Y + 17, 30, 22)
                                             e.Graphics.DrawLine(iconPen, iconArea.X + 11, iconArea.Y + 17, iconArea.X + 26, iconArea.Y + 8)
                                             e.Graphics.DrawLine(iconPen, iconArea.X + 26, iconArea.Y + 8, iconArea.X + 41, iconArea.Y + 17)
                                             e.Graphics.DrawLine(iconPen, iconArea.X + 26, iconArea.Y + 8, iconArea.X + 26, iconArea.Y + 39)
                                             e.Graphics.DrawLine(iconPen, iconArea.X + 49, iconArea.Y + 16, iconArea.X + 49, iconArea.Y + 33)
                                             e.Graphics.DrawLine(iconPen, iconArea.X + 41, iconArea.Y + 24, iconArea.X + 56, iconArea.Y + 24)
                                         End If
                                     End Using
                                     Using textFormat As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
                                         e.Graphics.DrawString(If(isViewButton, "VIEW INCOMPLETE" & vbCrLf & "BOXES", "START NEW BOX" & vbCrLf & "ANYWAY"), button.Font, Brushes.White, textArea, textFormat)
                                     End Using
                                 End Sub
    End Sub

    Private Shared Sub ConfigureIncompleteBoxCancelButton(button As Button)
        Dim baseColor As Color = Color.FromArgb(205, 58, 61)
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
        button.BackColor = baseColor
        button.ForeColor = Color.White
        button.Font = New Font("Segoe UI", 13.0!, FontStyle.Bold)
        button.TextAlign = ContentAlignment.MiddleCenter
        button.Padding = New Padding(14, 0, 14, 0)
        button.Region = CreateRoundedRegion(New Rectangle(0, 0, button.Width, button.Height), 11)
        AddHandler button.MouseEnter, Sub() button.BackColor = ControlPaint.Light(baseColor, 0.12F)
        AddHandler button.MouseLeave, Sub() button.BackColor = baseColor
        AddHandler button.Paint, Sub(sender, e)
                                     e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                                     Dim iconArea As New Rectangle(12, 13, 58, 58)
                                     Dim textArea As New Rectangle(74, 7, Math.Max(1, button.ClientSize.Width - 88), Math.Max(1, button.ClientSize.Height - 14))
                                     Using iconPen As New Pen(Color.White, 2.8F)
                                         e.Graphics.DrawEllipse(iconPen, iconArea.X + 12, iconArea.Y + 12, 34, 34)
                                         e.Graphics.DrawLine(iconPen, iconArea.X + 22, iconArea.Y + 22, iconArea.X + 36, iconArea.Y + 36)
                                         e.Graphics.DrawLine(iconPen, iconArea.X + 36, iconArea.Y + 22, iconArea.X + 22, iconArea.Y + 36)
                                     End Using
                                     Using textFormat As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
                                         e.Graphics.DrawString("CANCEL", button.Font, Brushes.White, textArea, textFormat)
                                     End Using
                                 End Sub
    End Sub

    Public Async Function load_page(Optional showProductionConfirmation As Boolean = True) As Task(Of String)
        Working_Pro.lb_nc_qty.Text = "0"
        Working_Pro.lb_ng_qty.Text = "0"
        ''msgBox(line_id.Text)
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
                        If showProductionConfirmation Then
                            Me.Enabled = False
                            Prd_detail.Show()
                        End If
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
            'Console.WriteLine("error ===>" & ex.Message)
            load_show.Show()
            Me.Enabled = True
            MessageBox.Show("Unable to load the production plan." & vbCrLf & ex.Message,
                            "Start Production",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
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
        ''msgBox("(((((999")
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
        ' Backoffice_model.Check_detail_actual_insert_act(Me) 'กรณีเครื่องดับ' เพราะ ใช้ ทับ  
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
