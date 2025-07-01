Imports VB = Microsoft.VisualBasic
Imports System
Imports System.Management
Imports System.ComponentModel
Imports System.Threading
Imports System.IO
Imports System.IO.Ports
Imports System.Text
Imports System.Globalization
Imports System.Drawing.Imaging
Imports IDAutomation.Windows.Forms.LinearBarCode
Imports System.Drawing.Printing
Imports System.Configuration
Imports GenCode128
Imports BarcodeLib.Barcode
'Imports NationalInstruments.DAQmx
Imports System.Net
Imports System.Web.Script.Serialization
Public Class Working_OEE
    ' Public Shared ArrayDataSpecial As New List(Of DataPlan)
    Public check_cal_eff As Integer = 0
    Public Gdate_now_date As Date
    Public Gtime As Date
    Public counterNewDIO
    Dim QR_Generator As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
    Dim start_flg As Integer = 0
    Dim comp_flg As Integer = 0
    Dim start_year As Integer = 0
    Dim start_month As Integer = 0
    Dim start_days As Integer = 0
    Dim Id As Short                             ' Device ID
    Dim Ret As Integer                          ' Return Code
    Dim szError As New StringBuilder("", 256)   ' Error String
    Dim szText As New String("", 100)
    Public Spwi_id As New List(Of String)
    Dim UpCount(2) As Short                     'zr Up Counter
    Dim DownCount(2) As Short                   ' Down Counter
    Dim Check(7) As CheckBox
    Dim Edit_Up(1) As TextBox
    Dim Edit_Down(7) As TextBox
    Dim delay_btn As Integer = 0
    Dim check_bull As Integer = 0
    Public check_in_up_seq As Integer = 0
    Dim value_next_process As String = ""
    Public check_format_tag As String = Backoffice_model.B_check_format_tag()
    Public Shared api = New api()
    Public Shared check_tag_type = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_LINE_TYPE?line_cd=" & MainFrm.Label4.Text)
    Public Shared load_data = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_DATA_WORKING?WI=" & Prd_detail.lb_wi.Text)
    Public Shared V_check_line_reprint As String = Backoffice_model.check_line_reprint()
    Public WithEvents serialPort As New SerialPort
    'Dim digitalReadTask_new_dio As New Task()
    'Dim reader_new_dio As DigitalSingleChannelReader
    'Dim data_new_dio As UInt32
    Public s_mecg_name As String = ""
    Public s_delay As String = ""
    Public Shared wiNo As String = ""
    Public Shared pFg As String = ""
    Public Shared lineCd As String = ""
    Public Shared lotNo As String = ""
    Public Shared seqNo As String = ""
    Public Shared model As String = ""
    Public Shared pwi_id As String = ""
    Public Shared rsWindow
    Public Shared status_conter As String = ""
    Public Shared statusDefect As String = ""
    Public Shared tag_group_no As String = ""
    Public Shared mec_name As String = ""
    Public Shared GoodQty As Double = 0.0
    Public Shared carvity As Integer = MainFrm.cavity.Text
    Public Shared ResultPrint As Integer = 0
    Public shift As String = ""
    Public TarGetOverall As String = "0"
    Public check_network_frist As String = ""
    Private Sub Working_OEE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setData()
        loadDataProgressBar()
    End Sub
    Public Sub setData()
        Dim OEE = New OEE
        picStart.Visible = True
        picMenuAll.Visible = False
        btnDefect.Visible = False
        btnDesc.Visible = False
        btnInc.Visible = False
        st_time.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
        st_count_ct.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
        shift = Prd_detail.Label12.Text.Substring(0, 1)
        lbShiftQty.Text = OEE.OEE_GET_TARGET(shift, Prd_detail.lb_wi.Text, "0")
        TarGetOverall = lbShiftQty.Text
        lbShiftPlan.Text = OEE.OEE_GET_Hour(shift)
    End Sub
    Private Sub CircularProgressBar2_Click(sender As Object, e As EventArgs)

    End Sub
    Public Sub loadDataProgressBar()
        Me.WebView21.Source = New Uri("http://192.168.161.219/test_ci/table.html")

    End Sub
    Public Sub StartOEE()
        picStart.Visible = False
        BtnStart.Visible = False
        btnStop.Visible = True
        btnBack.Visible = False
        btnDefect.Visible = False
        btnDesc.Visible = False
        btnInc.Visible = False
        PanelMenu.Visible = False
    End Sub
    Public Sub ManageMenu()
        picMenuAll.Visible = True
    End Sub
    Public Sub StopOEE()
        picStart.Visible = True
        btnStop.Visible = False
        BtnStart.Visible = True
        btnDefect.Visible = True
        btnDesc.Visible = True
        btnInc.Visible = True
    End Sub
    Public Sub Back()
        Prd_detail.Enabled = True
        Prd_detail.Show()
        Me.Close()
    End Sub

    Private Sub btnBack_Click_1(sender As Object, e As EventArgs) Handles btnBack.Click
        Back()
    End Sub

    Private Sub btnStop_Click_1(sender As Object, e As EventArgs) Handles btnStop.Click
        StopOEE()
    End Sub

    Private Sub BtnStart_Click_1(sender As Object, e As EventArgs) Handles BtnStart.Click
        StartOEE()
    End Sub
    Private Sub btnMenu_Click_1(sender As Object, e As EventArgs) Handles btnMenu.Click
        If PanelMenu.Visible = True Then
            PanelMenu.Visible = False
        Else
            PanelMenu.Visible = True
        End If
    End Sub

    Private Sub btnDesc_Click(sender As Object, e As EventArgs) Handles btnDesc.Click
        Desc()
    End Sub
    Public Async Sub Desc()
        Dim snp As Integer = Convert.ToInt32(Label27.Text)
        Try
            Desc_act.Label1.Text = Label12.Text 'LB_COUNTER_SHIP.Text '_Edit_Up_0.Text Mod snp
        Catch ex As Exception
            Desc_act.Label1.Text = "0"
        End Try
        Dim result As Integer = 0
        If CDbl(Val(LB_COUNTER_SHIP.Text)) = 0 Or CDbl(Val(Label12.Text)) = 0 Then
            result = 0 'LB_COUNTER_SHIP.Text
        ElseIf CDbl(Val(LB_COUNTER_SHIP.Text)) > CDbl(Val(Label12.Text)) Then
            result = CDbl(Val(Label12.Text)) 'CDbl(Val(LB_COUNTER_SHIP.Text)) - CDbl(Val(Label6.Text))
        ElseIf CDbl(Val(LB_COUNTER_SHIP.Text)) < CDbl(Val(Label12.Text)) Then
            result = LB_COUNTER_SHIP.Text
        Else
            result = LB_COUNTER_SHIP.Text
        End If
        Desc_act.Label1.Text = CDbl(Val(LB_COUNTER_SEQ.Text)) 'result
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Await Backoffice_model.updated_data_to_dbsvr(Me, "2")
                Desc_act.Show()
                Me.Enabled = False
            Else
                load_show.Show()
            End If
        Catch ex As Exception
            load_show.Show()
        End Try
    End Sub
    Public Sub cal_eff()

    End Sub

    Private Sub btnInc_Click(sender As Object, e As EventArgs) Handles btnInc.Click
        Me.Enabled = False
        select_int_qty.Show()
    End Sub
End Class
