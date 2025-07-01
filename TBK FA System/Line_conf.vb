Imports System.Net
Imports System.Web.Script.Serialization
Public Class Line_conf
    Dim device_id As Integer = 0
    Dim pd As String = ""
    Dim line_cd As String = ""
    Dim count_type As String = ""
    Dim cavity As String = ""
    Dim scanner_port As String = ""
    Dim printer_port As String = ""
    Dim dio_port1 As String = ""
    Dim cat_dio As String = ""
    Dim dio_detail As String = ""
    Dim s_pd As String = ""
    Dim s_line_cd As String = ""
    Dim s_count_type As String = ""
    Dim s_cavity As String = ""
    Dim s_scanner_port As String = ""
    Dim s_printer_port As String = ""
    Dim s_dio_port1 As String = ""
    Dim s_cat_dio As String = ""
    Dim s_dio_detail As String = ""
    Dim s_mecg_name As String = ""
    Dim me_sig_del As String = ""
    Dim tower_lamps As String = ""
    Dim BF = New Backoffice_model()
    Private Sub Label11_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Label1.Text = TimeOfDay.ToString("H:mm:ss")
        Label3.Text = DateTime.Now.ToString("D")
        Label22.Text = DateTime.Now.ToString("yyyy/MM/dd")
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Public Sub load_datamaster_realtime()
        'Dim result_data As String = BF.load_config_master_database()
        ' If result_data <> "0" Then
        'Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
        'For Each item As Object In dict2
        'BF.saveLineConfig(item("dep_cd").ToString(), item("me_line_cd").ToString(), item("mect_name").ToString(), item("me_cnt_qty").ToString(), item("mes_name").ToString(), item("mep_name").ToString(), item("mec_name").ToString())
        ' Next
        ' End If
    End Sub
    Private Sub Line_conf_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim api = New api()
        Timer2.Start()
        Dim load_sqlite_master_line = BF.Get_default_line_detail()
        While load_sqlite_master_line.Read()
            pd = load_sqlite_master_line("pd").ToString()
            line_cd = load_sqlite_master_line("line_cd").ToString()
            count_type = load_sqlite_master_line("count_type").ToString()
            cavity = load_sqlite_master_line("cavity").ToString()
            scanner_port = load_sqlite_master_line("scanner_port").ToString()
            printer_port = load_sqlite_master_line("printer_port").ToString()
            dio_port1 = load_sqlite_master_line("dio_port").ToString()
            cat_dio = load_sqlite_master_line("cat_dio").ToString()
            dio_detail = load_sqlite_master_line("dio_detail").ToString()
            ' tower_lamps = load_sqlite_master_line("Towerlamp").ToString()
            tower_lamps = "NO_DEVICE"
        End While
        load_sqlite_master_line.close()
        If load_defeult_master_server(line_cd) <> "0" Then 'load data on server
            ' MsgBox("HAVE")
            load_data_defeult_master_server(line_cd)
            Load_PD()
            Load_Line()
            S_Load_COUNTER()
            Load_scanner()
            Load_printer()
            combo_cavity.SelectedIndex = (s_cavity - 1)
            device_id = Load_CAT_COUNTER()
            Load_DIO_PORT(device_id)
            Load_delay()
            Load_TowerLamp()
        Else ' load data on sqlite
            '   MsgBox("NO HAVE")
            Load_PD()
            Load_Line()
            Load_COUNTER()
            Load_scanner()
            Load_printer()
            Dim device_id As Integer = Load_CAT_COUNTER()
            Load_DIO_PORT(device_id)
            combo_cavity.SelectedIndex = 0
            Load_delay()
            Load_TowerLamp()
        End If
    End Sub
    Private Sub Load_TowerLamp()
        tower_lamp.Items.Clear()
        Dim api = New api()
        Dim BF = New Backoffice_model()
        Dim I_Towerlamp As Integer = 0
        Dim check_I_Towerlamp As Integer = 0
        Dim PD As String = ""
        ' Dim towerLampSqlite = BF.Get_default_pd_detail_PD("TowerLamp")
        Dim towerLampSqlite = "NO_DEVICE"
        Dim result_data As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_TowerLamp")
        If result_data <> "0" Then
            Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
            For Each item As Object In dict2
                tower_lamp.Items.Add(item("metl_comport").ToString())
                If item("metl_comport").ToString() = towerLampSqlite Then
                    check_I_Towerlamp = I_Towerlamp
                End If
                I_Towerlamp += 1
            Next
            tower_lamp.SelectedIndex = check_I_Towerlamp
        End If
    End Sub
    Private Sub S_Load_COUNTER()
        type_counter.Items.Clear()
        Dim api = New api()
        Dim BF = New Backoffice_model()
        Dim I_PD_CD As Integer = 0
        Dim check_I_PD As Integer = 0
        Dim PD As String = ""
        Dim result_data As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_COUNTER")
        If result_data <> "0" Then
            Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
            For Each item As Object In dict2
                type_counter.Items.Add(item("mect_name").ToString())
                If item("mect_name").ToString() = s_count_type Then
                    check_I_PD = I_PD_CD
                End If
                I_PD_CD += 1
            Next
            type_counter.SelectedIndex = check_I_PD
        End If
    End Sub
    Public Function load_data_defeult_master_server(line_cd As String)
        Dim api = New api()
        Dim result_data As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/JOIN_CHECK_LINE_MASTER?line_cd=" & line_cd)
        If result_data <> "0" Then
            Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
            For Each item As Object In dict2
                's_pd = item("PD").ToString()
                s_line_cd = item("me_line_cd").ToString()
                s_count_type = item("mect_name").ToString()
                s_cavity = item("me_cnt_qty").ToString()
                s_scanner_port = item("mes_name").ToString()
                s_printer_port = item("mep_name").ToString()
                s_dio_port1 = item("mec_name").ToString()
                s_mecg_name = item("mecg_name").ToString()
                me_sig_del = item("me_sig_del").ToString()
            Next
        End If
    End Function
    Public Function load_defeult_master_server(line_cd As String)
        Dim api = New api()
        Dim result_data As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/JOIN_CHECK_LINE_MASTER?line_cd=" & line_cd)
        Return result_data
    End Function
    Private Sub Load_scanner()
        scanner.Items.Clear()
        Dim api = New api()
        Dim BF = New Backoffice_model()
        Dim I_PD_CD As Integer = 0
        Dim check_I_PD As Integer = 0
        Dim PD = BF.Get_default_pd_detail_PD("scanner_port")
        Dim result_data As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_SCANNER")
        If result_data <> "0" Then
            Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
            For Each item As Object In dict2
                scanner.Items.Add(item("mes_name").ToString())
                If item("mes_name").ToString() = PD Then
                    check_I_PD = I_PD_CD
                End If
                I_PD_CD += 1
            Next
            scanner.SelectedIndex = check_I_PD
        End If
    End Sub
    Private Sub Load_PD()
        ComboBox3.Items.Clear()
        Dim api = New api()
        Dim BF = New Backoffice_model()
        Dim I_PD_CD As Integer = 0
        Dim check_I_PD As Integer = 0
        Dim PD = BF.Get_default_pd_detail_PD("pd")
        Dim result_data As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_PD")
        If result_data <> "0" Then
            Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
            For Each item As Object In dict2
                ComboBox3.Items.Add(item("PD").ToString())
                If item("PD").ToString() = PD Then
                    check_I_PD = I_PD_CD
                End If
                I_PD_CD += 1
            Next
            ComboBox3.SelectedIndex = check_I_PD
        End If
    End Sub
    Private Sub Load_Line()
        ComboBox2.Items.Clear()
        Dim api = New api()
        Dim BF = New Backoffice_model()
        Dim result_data = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_LINE?PD=" & ComboBox3.Text)
        Dim PD As String = "NO_DATA"
        Dim I_PD As Integer = 0
        Dim check_I_LINE As Integer = 0
        Dim I_LINE_CD As Integer = 0
        Dim LINE_CD = BF.Get_default_pd_detail_PD("line_cd")
        If result_data <> "0" Then
            Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
            For Each item As Object In dict2
                ComboBox2.Items.Add(item("LINE_CD").ToString())
                If item("LINE_CD").ToString() = LINE_CD Then
                    check_I_LINE = I_LINE_CD
                End If
                I_LINE_CD += 1
            Next
            ComboBox2.SelectedIndex = check_I_LINE
        End If
    End Sub
    Public Sub Load_delay()
        If me_sig_del = "" Then
            me_sig_del = 0
        End If
        delay_sec.Items.Clear()
        Dim delay_defalue As Integer = 3
        Dim delay_load As Integer = 0
        For index As Integer = 0 To 3600
            If index = (me_sig_del / 10) Then
                delay_load = index
            End If
            delay_sec.Items.Add(index)
        Next
        delay_sec.SelectedIndex = delay_load
    End Sub
    Private Sub Load_COUNTER()
        type_counter.Items.Clear()
        Dim api = New api()
        Dim BF = New Backoffice_model()
        Dim I_PD_CD As Integer = 0
        Dim check_I_PD As Integer = 0
        Dim PD As String = ""
        Dim result_data As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_COUNTER")
        If result_data <> "0" Then
            Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
            For Each item As Object In dict2
                type_counter.Items.Add(item("mect_name").ToString())
                If item("mect_name").ToString() = PD Then
                    check_I_PD = I_PD_CD
                End If
                I_PD_CD += 1
            Next
            type_counter.SelectedIndex = check_I_PD
        End If
    End Sub
    Private Sub Load_printer()
        printer.Items.Clear()
        Dim api = New api()
        Dim BF = New Backoffice_model()
        Dim result_data = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_PRINTER")
        Dim PD As String = "NO_DATA"
        Dim LINE_CD As String = "NO_DATA"
        Dim I_PD As Integer = 0
        Dim check_port_printer As Integer = 0
        Dim I As Integer = 0
        ' Dim get_default_line = BF.Get_default_line_detail()
        If result_data <> "0" Then
            Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
            For Each item As Object In dict2
                printer.Items.Add(item("mep_name").ToString())
                If item("mep_name").ToString() = s_printer_port Then
                    check_port_printer = I
                End If
                I += 1
            Next
            printer.SelectedIndex = check_port_printer
        End If
    End Sub
    Private Function Load_CAT_COUNTER()
        ComboBox_master_device.Items.Clear()
        Dim api = New api()
        Dim I_PD_CD As Integer = 0
        Dim check_I_PD As Integer = 0
        Dim PD As String = ""
        Dim ID As Integer = 0
        Dim result_data As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_CAT_COUNTER")
        If result_data <> "0" Then
            Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
            For Each item As Object In dict2
                ComboBox_master_device.Items.Add(item("mecg_name").ToString())
                If s_mecg_name = item("mecg_name").ToString() Then
                    check_I_PD = I_PD_CD
                End If
                I_PD_CD += 1
            Next
            ComboBox_master_device.SelectedIndex = check_I_PD
            Return ID
        End If
    End Function

    Public Function Load_DIO_PORT(device_id)
        DIO_PORT.Items.Clear()
        Dim api = New api()
        Dim check_cat_dio As Integer = 0
        Dim i = 0
        Dim result_data As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_DIO_PORT?device_name=" & ComboBox_master_device.Text)
        If result_data <> "0" Then
            Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
            For Each item As Object In dict2
                DIO_PORT.Items.Add(item("mec_name").ToString())
                If item("mec_name").ToString() = s_dio_port1 Then
                    check_cat_dio = i
                End If
                i += 1
                DIO_PORT.SelectedIndex = check_cat_dio
            Next
        End If
        Return device_id
    End Function


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim LoadSQL As String
        Dim pd As String = ComboBox3.Items(ComboBox3.SelectedIndex)
        Dim line_cd As String = ComboBox2.Text
        Dim count_type As String = type_counter.Text
        Dim cavity As String = combo_cavity.Text
        Dim scanner_port As String = scanner.Text
        Dim printer_port As String = printer.Text
        Dim dio_port As String = Me.DIO_PORT.Text
        Dim Towerlampss As String = tower_lamp.Text

        Backoffice_model.saveLineConfig(pd, line_cd, count_type, cavity, scanner_port, printer_port, dio_port, Towerlampss)
        'Dim f2 = New MainFrm()
        Dim sqlss = Backoffice_model.ConnectDBSQLite()
        While sqlss.Read()
            MainFrm.Label6.Text = sqlss("pd").ToString()
            MainFrm.Label4.Text = sqlss("line_cd").ToString()
            MainFrm.count_type.Text = sqlss("count_type").ToString()
            MainFrm.cavity.Text = sqlss("cavity").ToString()
            MainFrm.lb_scanner_port.Text = sqlss("scanner_port").ToString()
            MainFrm.lb_printer_port.Text = sqlss("printer_port").ToString()
            MainFrm.lb_dio_port.Text = sqlss("dio_port").ToString()
            Backoffice_model.SCANNER_PORT = sqlss("scanner_port").ToString()
            'MsgBox(sqlss("pd").ToString())
        End While
        sqlss.close
        If Backoffice_model.SCANNER_PORT <> "" And Backoffice_model.SCANNER_PORT <> "USB" Then
            MainFrm.lb_ctrl_sc_flg.Text = "emp"
        End If
        Insert_list.Label3.Text = MainFrm.Label4.Text
        Prd_detail.Label3.Text = MainFrm.Label4.Text
        Dim total_delay As Integer = (CDbl(Val(delay_sec.Text)) * 10)
        Dim api = New api()
        Dim result_data As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/INSERT_COTROL_MASTER?line_cd=" & ComboBox2.Text & "&ComboBox_master_device=" & ComboBox_master_device.Text & "&device_dio_port_id=" & dio_port & "&printer=" & printer.Text & "&typ_counter=" & type_counter.Text & "&cavity=" & combo_cavity.Text & "&total_delay=" & total_delay & "&scanner=" & scanner.Text & "&TowerLamp=" & tower_lamp.Text)
        'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/INSERT_COTROL_MASTER?line_cd=" & ComboBox2.Text & "&ComboBox_master_device=" & ComboBox_master_device.Text & "&device_dio_port_id=" & dio_port & "&printer=" & printer.Text & "&typ_counter=" & type_counter.Text & "&cavity=" & combo_cavity.Text & "&total_delay=" & total_delay & "&scanner=" & scanner.Text & "&TowerLamp=" & tower_lamp.Text)
        ' Button1.Enabled = False
        'btn_start.Enabled = False
        'btn_back.Enabled = False
        Dim listdetail = "Updated Success !"
        PictureBox6.Visible = True
        Panel2.Visible = True
        Label2.Visible = True
        PictureBox16.Visible = True
        PictureBox6.BringToFront()
        PictureBox6.Show()
        PictureBox16.BringToFront()
        PictureBox16.Show()
        Panel2.BringToFront()
        Panel2.Show()
        Label2.Text = listdetail
        Label2.BringToFront()
        Label2.Show()
        MainFrm.LB_Number_worker.Text = 0
        List_Emp.ListView1.Items.Clear()
        List_Emp.ListBox2.Items.Clear()
        'MsgBox("Update Success.")
    End Sub
    Private Sub menu3_Click(sender As Object, e As EventArgs)
        Me.Enabled = False
        Me.ComboBox2.DataSource = Nothing
        Try
            If My.Computer.Network.Ping("192.168.161.102") Then
                Dim webClient As New System.Net.WebClient
                Dim result As String = webClient.DownloadString("http://192.168.161.102/exp_api3party/Api_sync_newfa/update_line_mst")
                MsgBox("Synchronous completed")
            Else
                MsgBox("Synchronous not completed")
            End If
        Catch ex As Exception

        End Try
        'Me.ComboBox2.DataSource = GetLineItems()
        'Me.ComboBox2.DisplayMember = "Name"
        'Me.ComboBox2.ValueMember = "ID"
        Dim pd As String = ComboBox3.Items(ComboBox3.SelectedIndex)
        Dim line_cd As String = ComboBox2.Text
        Me.Enabled = True


        'Dim f2 = New Line_conf()
        'Me.Close()
        'Me.Show()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Dim f2 = New MainFrm()

        MainFrm.Show()
        Me.Close()
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub
    Function GetLineItems() As List(Of LineItem)
        Dim LineItems = New List(Of LineItem)
        'Command = New MySqlCommand("SELECT * FROM `Linelist` WHERE l_id = '" & id & "'", connection)
        'Command.CommandTimeout = 30
        'Reader = Command.ExecuteReader()
        Dim LoadSQL = Backoffice_model.GetLine_mst()
        'MsgBox("test")
        ' While LoadSQL.Read()
        'Label6.Text = LoadSQL("pd").ToString()
        'Label4.Text = LoadSQL("line_cd").ToString()
        'MsgBox(LoadSQL("dep_cd").ToString())
        'End While
        If LoadSQL.HasRows = True Then
            While LoadSQL.Read()
                LineItems.Add(New LineItem(LoadSQL("line_id"), LoadSQL("line_cd")))
            End While
        End If
        LoadSQL.close
        Return LineItems
    End Function
    Public Class LineItem
        Public Sub New(ByVal id As Integer, ByVal name As String)
            mID = id
            mName = name
        End Sub
        Private mID As Integer
        Public Property ID() As Integer
            Get
                Return mID
            End Get
            Set(ByVal value As Integer)
                mID = value
            End Set
        End Property

        Private mName As String
        Public Property Name() As String
            Get
                Return mName
            End Get
            Set(ByVal value As String)
                mName = value
            End Set
        End Property

    End Class
    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        'TextBox1.Text = ComboBox3.Items(ComboBox3.SelectedIndex)
        'ComboBox1.Items(ComboBox1.SelectedIndex) = TextBox1.Text
        Try
            ComboBox2.Items.Clear()
            Load_Line()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub
    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DIO_PORT.SelectedIndexChanged

    End Sub
    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click

    End Sub
    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles scanner.SelectedIndexChanged

    End Sub

    Private Sub ComboBox_master_device_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_master_device.SelectedIndexChanged
        Load_DIO_PORT(device_id)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox16_Click(sender As Object, e As EventArgs) Handles PictureBox16.Click
        PictureBox6.Visible = False
        Panel2.Visible = False
        Label2.Visible = False
        PictureBox16.Visible = False
        MainFrm.Show()
        Me.Hide()
    End Sub
End Class
