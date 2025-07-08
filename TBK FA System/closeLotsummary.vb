Imports System.Net.NetworkInformation
Imports System.Web.Script.Serialization
Public Class closeLotsummary
    Shared datlvDefectsumary As ListViewItem
    Shared listviewSpecial As ListViewItem
    Shared rs
    Shared rsFg
    Shared sPart As String = ""
    Shared sWi As String = ""
    Shared sModel As String = ""
    Shared sSeq As String = ""
    Shared sAct As String = ""
    Shared sGood As String = ""
    Shared sNc As String = ""
    Shared sNg As String = ""
    Dim cBuottndown As Integer = 0
    Shared sLot As String = ""
    Shared sType As String = ""
    Shared pQty As String = ""
    Shared sShift As String = ""
    Shared stDatetime As String = ""
    Shared eDatetime As String = ""
    Shared avarage_eff As Double = 0.0
    Shared avarage_act_prd_time As String = ""
    Shared sLine As String = ""
    Shared staffNo As String = ""
    Shared seqQty As Integer = 0
    Shared aDefectcode As List(Of String) = New List(Of String)
    Shared aDefectQty As List(Of String) = New List(Of String)
    Public S_index As Integer = 0
    Shared NextProcess As Integer = 0
    Shared ClickCloseLot As Integer = 0
    Dim ctsDelay As New Threading.CancellationTokenSource()
    Private Sub closeLotsummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            sizeLocation()
            checkNetColselot()
            setVariable()
            Finish_work.Close()
            'If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
            If MainFrm.chk_spec_line = "2" Then
                lbLine.BackColor = Color.FromArgb(44, 88, 130)
                lbLine.Location = New Point(166, 113)
                lbLine.Font = New Font(lbLine.Font.FontFamily, 23)
                pbSpecialSummary.Visible = True
                ListView2.Visible = True
                PictureBox3.Visible = True
                PictureBox3.Enabled = True
                setDataSpecial()
            Else
                pbSpecialSummary.Visible = False
            End If
            getDefectdetailnc(sWi, sSeq, sLot)
            'Else
            'load_show.Show()
            '    End If
        Catch ex As Exception
            checkNetColselot()
        End Try
    End Sub
    Public Sub sizeLocation()
        loadNet.Width = 800
        loadNet.Height = 600
        loadNet.Location = New Point(1, 0)
    End Sub
    Public Sub setDataSpecial()
        Dim i As Integer = 0
        Dim arrayWI As List(Of String) = New List(Of String)
        Dim GenSEQ As Integer = Working_Pro.Label22.Text - MainFrm.ArrayDataPlan.ToArray.Length
        Dim Iseq = GenSEQ
        Dim md As New modelDefect()
        Dim mdSqlite As New ModelSqliteDefect
        For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
            i += 1
            Iseq += 1
            Dim special_wi As String = itemPlanData.wi
            Dim special_item_cd As String = itemPlanData.item_cd
            arrayWI.Add(itemPlanData.wi)
            rs = mdSqlite.mSqlitegroupDataWiSpc(itemPlanData.wi, Iseq, Working_Pro.Label18.Text)
            listviewSpecial = New ListViewItem(i)
            listviewSpecial.SubItems.Add(special_wi)
            listviewSpecial.SubItems.Add(special_item_cd)
            listviewSpecial.SubItems.Add(Working_Pro.LB_COUNTER_SEQ.Text)
            If rs <> "0" Then
                Dim rsData As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
                For Each item As Object In rsData
                    listviewSpecial.SubItems.Add(calGoodqty(Working_Pro.LB_COUNTER_SEQ.Text, item("TotaldfNC").ToString(), item("TotaldfNG").ToString()))
                    listviewSpecial.SubItems.Add(item("TotaldfNC").ToString())
                    listviewSpecial.SubItems.Add(item("TotaldfNG").ToString())
                Next
            Else
                listviewSpecial.SubItems.Add(calGoodqty(Working_Pro.LB_COUNTER_SEQ.Text, 0, 0))
                listviewSpecial.SubItems.Add(0)
                listviewSpecial.SubItems.Add(0)
            End If
            ListView2.Items.Add(listviewSpecial)
        Next
    End Sub
    Public Function getDefectdetailnc(wi As String, seq As String, lot As String)
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                aDefectcode.Clear()
                aDefectQty.Clear()
                Dim md As New modelDefect()
                Dim mdSqlite As New ModelSqliteDefect
                '  rs = md.mGetdatachildpartsummarychild(wi, seq, lot)
                rs = mdSqlite.mSqliteGetdatachildpartsummarychild(wi, seq, lot)
                ' rsFg = md.mGetdatachildpartsummaryfg(wi, seq, lot)
                rsFg = mdSqlite.mSqliteGetdatachildpartsummaryfg(wi, seq, lot)
                cListview = 0
                If rs <> "0" Then
                    Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
                    Dim i As Integer = 1
                    For Each item As Object In dcResultdata
                        cListview += 1
                        Dim dt_item_type As String = ""
                        If item("dt_type").ToString() = "1" Then
                            dt_item_type = "NG"
                        Else
                            dt_item_type = "NC"
                        End If
                        datlvDefectsumary = New ListViewItem(i)
                        datlvDefectsumary.SubItems.Add(item("dt_item_cd").ToString())
                        datlvDefectsumary.SubItems.Add(dt_item_type)
                        datlvDefectsumary.SubItems.Add(item("dt_code").ToString())
                        datlvDefectsumary.SubItems.Add(item("defect_name").ToString())
                        datlvDefectsumary.SubItems.Add(item("total_nc").ToString())
                        aDefectcode.Add(item("dt_code").ToString())
                        aDefectQty.Add(item("total_nc").ToString()) ' ที่ทำการ ปรับเปลี่ยน ตอนแรกเป็น aDefectcode
                        lvSumarychild.Items.Add(datlvDefectsumary)
                        i += 1
                        cBuottndown += 1
                    Next
                Else
                    datlvDefectsumary = New ListViewItem("1")
                    datlvDefectsumary.SubItems.Add("NO DATA")
                    datlvDefectsumary.SubItems.Add("NO DATA")
                    datlvDefectsumary.SubItems.Add("NO DATA")
                    datlvDefectsumary.SubItems.Add("NO DATA")
                    datlvDefectsumary.SubItems.Add("NO DATA")
                    lvSumarychild.Items.Add(datlvDefectsumary)
                End If
                If rsFg <> "0" Then
                    Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsFg)
                    Dim i As Integer = 1
                    For Each item As Object In dcResultdata
                        cListview += 1
                        Dim dt_item_type As String = ""
                        If item("dt_type").ToString() = "1" Then
                            dt_item_type = "NG"
                        Else
                            dt_item_type = "NC"
                        End If
                        datlvDefectsumary = New ListViewItem(i)
                        datlvDefectsumary.SubItems.Add(item("dt_item_cd").ToString())
                        datlvDefectsumary.SubItems.Add(dt_item_type)
                        datlvDefectsumary.SubItems.Add(item("dt_code").ToString())
                        datlvDefectsumary.SubItems.Add(item("defect_name").ToString())
                        datlvDefectsumary.SubItems.Add(item("total_nc").ToString())
                        aDefectcode.Add(item("dt_code").ToString())
                        aDefectcode.Add(item("total_nc").ToString())
                        lvSumaryfg.Items.Add(datlvDefectsumary)
                        i += 1
                    Next
                Else
                    datlvDefectsumary = New ListViewItem("1")
                    datlvDefectsumary.SubItems.Add("NO DATA")
                    datlvDefectsumary.SubItems.Add("NO DATA")
                    datlvDefectsumary.SubItems.Add("NO DATA")
                    datlvDefectsumary.SubItems.Add("NO DATA")
                    datlvDefectsumary.SubItems.Add("NO DATA")
                    lvSumaryfg.Items.Add(datlvDefectsumary)
                End If
                Return rs
            End If
        Catch ex As Exception
            checkNetColselot()
        End Try
    End Function
    Public Sub setVariable()
        lbLine.Text = Working_Pro.Label24.Text
        lbPart.Text = Working_Pro.Label3.Text
        lbWi.Text = Working_Pro.wi_no.Text
        lbModel.Text = Working_Pro.lb_model.Text
        lbAct.Text = Working_Pro.LB_COUNTER_SEQ.Text 'Working_Pro.Label6.Text
        lbNc.Text = Working_Pro.lb_nc_qty.Text
        lbNg.Text = Working_Pro.lb_ng_qty.Text
        lbDefectAll.Text = CDbl(Val(lbNg.Text)) + CDbl(Val(lbNc.Text))
        sWi = lbWi.Text
        sAct = Working_Pro.Label6.Text 'Working_Pro.LB_COUNTER_SEQ.Text 
        sSeq = Working_Pro.Label22.Text
        sLot = Working_Pro.Label18.Text
        lbGood.Text = calGoodqty(lbAct.Text, lbNc.Text, lbNg.Text)
        Try
            avarage_eff = Working_Pro.lb_sum_prg.Text
        Catch ex As Exception
            avarage_eff = 0.0
        End Try
        avarage_act_prd_time = Working_Pro.Label37.Text
        sLine = Working_Pro.Label24.Text
        sPart = Working_Pro.Label3.Text
        pQty = Working_Pro.Label8.Text
        sShift = Working_Pro.Label14.Text
        stDatetime = Working_Pro.st_time.Text
        eDatetime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
        staffNo = Working_Pro.Label29.Text
        seqQty = Working_Pro.LB_COUNTER_SEQ.Text
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Working_Pro.Enabled = True
        Working_Pro.btn_start.Enabled = True
        Working_Pro.btn_closelot.Enabled = True
        Working_Pro.btn_setup.Enabled = True
        Working_Pro.CheckMenu()
        Working_Pro.btn_ins_act.Enabled = True
        Working_Pro.btn_desc_act.Enabled = True
        If statusPage.Text = "MAN" Then
            Sel_prd_setup.Enabled = True
            Me.Close()
        Else
            Working_Pro.Show()
            Working_Pro.Enabled = True
            Me.Close()
        End If
    End Sub

    Public Function calGoodqty(act As Integer, nc As Integer, ng As Integer)
        Dim result = act - (nc + ng)
        Return result
    End Function
    Public Async Function WaitForNetworkWithPopup() As Task
        Do While Not IsNetworkAvailable() OrElse Not My.Computer.Network.Ping(Backoffice_model.svp_ping)
            If Not load_show.Visible Then
                load_show.Show()
            End If
            Console.WriteLine("⛔ ยังไม่มี Network หรือ Ping ไม่ผ่าน... รอ 1 วินาที")
            Await Task.Delay(1000)
        Loop
        If load_show.Visible Then
            load_show.Hide()
        End If
    End Function

    Private Function IsNetworkAvailable() As Boolean
        Return NetworkInterface.GetIsNetworkAvailable()
    End Function

    Public Async Function Manage_closelot() As Task
        'Try
        Await WaitForNetworkWithPopup()
        If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
            If Loss_reg.Visible = True Then
                Loss_reg.Submit_loss()
            End If
            Dim md As New modelDefect()
            Dim cFlg = comPleteflg(sAct, pQty)
            Dim trFlg As String = "1"
            Dim dFlg As String = "0"
            Dim prdFlg As String = "1"
            Dim clFlg As String = "1"
            btnOk.Visible = False
            If MainFrm.chk_spec_line = "2" Then
                'Special
                Dim GenSEQ As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
                Dim Iseq = GenSEQ
                Dim mdSqlite = New ModelSqliteDefect
                For Each itemPlanData As DataPlan In MainFrm.ArrayDataPlan
                    Iseq = Iseq + 1
                    ' rsFg = md.mGetdatachildpartsummaryfg(itemPlanData.wi, Iseq, sLot)
                    rsFg = mdSqlite.mSqliteGetdatachildpartsummaryfg(itemPlanData.wi, Iseq, sLot)
                    ' rs = md.mGetdatachildpartsummarychild(itemPlanData.wi, Iseq, sLot)
                    rs = mdSqlite.mSqliteGetdatachildpartsummarychild(itemPlanData.wi, Iseq, sLot)
                    If rsFg <> "0" Then
                        Dim dcResultdatafg As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsFg)
                        Dim i As Integer = 1
                        For Each itemchild As Object In dcResultdatafg
                            Dim date_now = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
                            Await WaitForNetworkWithPopup()
                            ClickOk(itemPlanData.wi, lbLine.Text, itemchild("dt_item_cd").ToString(), "1", sLot, Iseq, itemchild("dt_type").ToString(), itemchild("dt_code").ToString(), itemchild("total_nc").ToString(), date_now, itemchild("pwi_id").ToString())
                            mdSqlite.UpdateStatusCloselotSqlite("1", itemchild("pwi_id").ToString())
                        Next
                    End If
                    If rs <> "0" Then
                        Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
                        Dim i As Integer = 1
                        For Each itemfg As Object In dcResultdata
                            Dim date_now = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
                            Await WaitForNetworkWithPopup()
                            ClickOk(itemPlanData.wi, lbLine.Text, itemfg("dt_item_cd").ToString(), "2", sLot, Iseq, itemfg("dt_type").ToString(), itemfg("dt_code").ToString(), itemfg("total_nc").ToString(), date_now, itemfg("pwi_id").ToString())
                            mdSqlite.UpdateStatusCloselotSqlite("1", itemfg("pwi_id").ToString())
                        Next
                    End If
                    If Backoffice_model.S_chk_spec_line = 1 Then
                        Await WaitForNetworkWithPopup()
                        Dim data = Backoffice_model.GET_START_END_PRODUCTION_DETAIL_SPECTAIL_TIME(Working_Pro.pwi_id)
                        If data <> "0" Then
                            Dim dFg As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(data)
                            For Each item As Object In dFg
                                stDatetime = item("st_time").ToString()
                                eDatetime = item("end_time").ToString()
                                'Console.WriteLine(stDatetime)
                                'Console.WriteLine(eDatetime)
                            Next
                        End If
                    End If
                Next
            Else
                'Normal
                'rsFg = md.mGetdatachildpartsummaryfg(sWi, sSeq, sLot)
                Dim mdSqlite = New ModelSqliteDefect
                rsFg = mdSqlite.mSqliteGetdatachildpartsummaryfg(sWi, sSeq, sLot)
                Dim flg_print As String = ""
                If rsFg <> "0" Then
                    Dim dcResultdatafg As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsFg)
                    Dim i As Integer = 1
                    For Each itemchild As Object In dcResultdatafg
                        Dim date_now = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
                        Await WaitForNetworkWithPopup()
                        ClickOk(sWi, lbLine.Text, itemchild("dt_item_cd").ToString(), "1", sLot, sSeq, itemchild("dt_type").ToString(), itemchild("dt_code").ToString(), itemchild("total_nc").ToString(), date_now, Working_Pro.pwi_id)
                    Next
                End If
                ' rs = md.mGetdatachildpartsummarychild(sWi, sSeq, sLot)
                rs = mdSqlite.mSqliteGetdatachildpartsummarychild(sWi, sSeq, sLot)
                If rs <> "0" Then
                    Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
                    Dim i As Integer = 1
                    For Each itemfg As Object In dcResultdata
                        Dim date_now = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
                        Await WaitForNetworkWithPopup()
                        ClickOk(sWi, lbLine.Text, itemfg("dt_item_cd").ToString(), "2", sLot, sSeq, itemfg("dt_type").ToString(), itemfg("dt_code").ToString(), itemfg("total_nc").ToString(), date_now, Working_Pro.pwi_id)
                    Next
                End If
                If Backoffice_model.S_chk_spec_line = 1 Then
                    Await WaitForNetworkWithPopup()
                    Dim data = Backoffice_model.GET_START_END_PRODUCTION_DETAIL_SPECTAIL_TIME(Working_Pro.pwi_id)
                    If data <> "0" Then
                        Dim dFg As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(data)
                        For Each item As Object In dFg
                            stDatetime = item("st_time").ToString()
                            eDatetime = item("end_time").ToString()
                            'Console.WriteLine(stDatetime)
                            'Console.WriteLine(eDatetime)
                        Next
                    End If
                End If
                mdSqlite.UpdateStatusCloselotSqlite("1", Working_Pro.pwi_id)
            End If
            If MainFrm.chk_spec_line = "2" Then
                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                    Dim special_wi As String = itemPlanData.wi
                    Await WaitForNetworkWithPopup()
                    If cFlg = 1 Then
                        Backoffice_model.work_complete(special_wi)
                    Else
                        Backoffice_model.work_complete_offline(special_wi)
                    End If
                Next
            Else
                Await WaitForNetworkWithPopup()

                If cFlg = 1 Then
                    Backoffice_model.work_complete(sWi)
                Else
                    Backoffice_model.work_complete_offline(sWi)
                End If
            End If
            Await WaitForNetworkWithPopup()
            insertProductionactual(sWi, sLine, sPart, pQty, seqQty, sSeq, sShift, staffNo, stDatetime, eDatetime, sLot, cFlg, trFlg, dFlg, prdFlg, clFlg, avarage_eff, avarage_act_prd_time)
            Await WaitForNetworkWithPopup()
            checkPrintnormal()
            If MainFrm.chk_spec_line = "2" Then
                Dim GenSEQ As Integer = sSeq - MainFrm.ArrayDataPlan.ToArray.Length
                Dim Iseq = GenSEQ
                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                    Iseq = Iseq + 1
                    Dim special_wi As String = itemPlanData.wi
                    Await checkPrintdefect(special_wi, Iseq, sLot)
                Next
            Else
                Await checkPrintdefect(sWi, sSeq, sLot)
            End If

            If statusPage.Text = "MAN" Then
                Sel_prd_setup.Close()
                List_Emp.lb_link.Text = "working"
                List_Emp.Show()
                Me.Close()
            Else
                Working_Pro.Close()
                Prd_detail.Close()
                MainFrm.Enabled = True
                MainFrm.Show()
                Me.Close()
            End If
        Else
            checkNetColselot()
        End If
        'Catch ex As Exception
        'load_show.Show()
        ' End Try
    End Function
    Private Async Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        ClickCloseLot = 1
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                If StopMenu.Visible Then
                    StopMenu.SatrtWork()
                End If
                Await Backoffice_model.updated_data_to_dbsvr(Me, "2") ' check ก่อน Close Lot อีกครั้ง
                If Working_Pro.s_mecg_name = "RS232" Then '
                    Working_Pro.serialPort.Close()
                End If
                If Working_Pro.RemainScanDmc > 0 Then
                    ScanQRprod.lbTopices.Visible = False
                    ScanQRprod.ManageQrScanFA("6", Working_Pro.RemainScanDmc)
                    ScanQRprod.Show()
                    Me.Enabled = False
                Else
                    Await Manage_closelot()
                End If
            Else
                checkNetColselot()
            End If
        Catch ex As Exception
            '  Working_Pro.Close()
            '  Prd_detail.Close()
            '  MainFrm.Enabled = True
            '  MainFrm.Show()
            '  Me.Close() ' add on 
            '  MsgBox("In Catch Function btnok In CloseLotSummary =>" & ex.Message)
            checkNetColselot()
        End Try
    End Sub
    Public Async Function checkPrintdefect(wi As String, seq As String, lot As String) As Task
        Dim md = New modelDefect()
        Dim mdSqlite = New ModelSqliteDefect()
        Dim dfType As String = "2" 'NC
        'rsNc = md.mGetdatachildpartsummarychildgrouppart(wi, seq, lot, dfType) 'NC
        'rsFgNc = md.Getdatachildpartsummaryfggrouppart(wi, seq, lot, dfType) 'NC
        rsNc = mdSqlite.mSqliteGetdatachildpartsummarychildgrouppart(wi, seq, lot, dfType) 'NC
        rsFgNc = mdSqlite.mSqliteGetdatachildpartsummaryfggrouppart(wi, seq, lot, dfType) 'NC
        Dim itemType = "1"
        If rsFgNc <> "0" Then
            Dim dcResultdatafg As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsFgNc)
            Dim i As Integer = 1
            Dim factory_cd As String = "NO DATA"
            Dim plan_cd As String = "NO_DATA"
            If MainFrm.Label6.Text = "K2PD06" Then
                factory_cd = "8"
                plan_cd = "52"
            Else
                factory_cd = "10"
                plan_cd = "51"
            End If
            For Each itemdf As Object In dcResultdatafg
                itemType = "1"
                Dim rsApi = Await md.mGetdatepartdetail(itemdf("dt_item_cd").ToString, "1")
                Dim dFg As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsApi)
                For Each detailItemfg As Object In dFg
                    Dim objTagprintdefect = New printDefect()
                    Dim menu = "1"
                    Await WaitForNetworkWithPopup()
                    objTagprintdefect.Set_parameter_print(itemdf("dt_item_cd").ToString(), detailItemfg("ITEM_NAME").ToString(), detailItemfg("MODEL").ToString(), sLine, stDatetime, detailItemfg("LOCATION_PART").ToString(), sShift, factory_cd, sLot, itemdf("total_nc"), seq, wi, itemType, dfType, menu)
                Next
            Next
        End If
        If rsNc <> "0" Then
            Dim dcResultdatachild As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsNc)
            Dim i As Integer = 1
            Dim factory_cd As String = "NO DATA"
            Dim plan_cd As String = "NO_DATA"
            If MainFrm.Label6.Text = "K2PD06" Then
                factory_cd = "8"
                plan_cd = "52"
            Else
                factory_cd = "10"
                plan_cd = "51"
            End If
            For Each itemd As Object In dcResultdatachild
                itemType = "2"
                Await WaitForNetworkWithPopup()
                Dim rsApi = Await md.mGetdatepartdetail(itemd("dt_item_cd").ToString, "2")
                Dim dChild As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsApi)
                For Each detailItemchild As Object In dChild
                    Dim objTagprintdefect = New printDefect()
                    Dim menu = "1"
                    Await WaitForNetworkWithPopup()
                    objTagprintdefect.Set_parameter_print(itemd("dt_item_cd").ToString(), detailItemchild("ITEM_NAME").ToString(), detailItemchild("MODEL").ToString(), sLine, stDatetime, detailItemchild("LOCATION_PART").ToString(), sShift, factory_cd, sLot, itemd("total_nc"), seq, wi, itemType, dfType, menu)
                Next
            Next
        End If
        dfType = "1" 'NG
        ' rsNc = md.mGetdatachildpartsummarychildgrouppart(wi, seq, lot, dfType) 'NG
        'rsFgNc = md.Getdatachildpartsummaryfggrouppart(wi, seq, lot, dfType) 'NG
        rsNc = mdSqlite.mSqliteGetdatachildpartsummarychildgrouppart(wi, seq, lot, dfType) 'NC
        rsFgNc = mdSqlite.mSqliteGetdatachildpartsummaryfggrouppart(wi, seq, lot, dfType) 'NC
        itemType = "1"
        If rsFgNc <> "0" Then
            Dim dcResultdatafg As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsFgNc)
            Dim i As Integer = 1
            Dim factory_cd As String = "NO DATA"
            Dim plan_cd As String = "NO_DATA"
            If MainFrm.Label6.Text = "K2PD06" Then
                factory_cd = "8"
                plan_cd = "52"
            Else
                factory_cd = "10"
                plan_cd = "51"
            End If
            For Each itemdf As Object In dcResultdatafg
                itemType = "1"
                Await WaitForNetworkWithPopup()
                Dim rsApi = Await md.mGetdatepartdetail(itemdf("dt_item_cd").ToString, "1")
                Dim dFg As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsApi)
                For Each detailItemfg As Object In dFg
                    Dim objTagprintdefect = New printDefect()
                    Dim menu = "1"
                    Await WaitForNetworkWithPopup()
                    objTagprintdefect.Set_parameter_print(itemdf("dt_item_cd").ToString(), detailItemfg("ITEM_NAME").ToString(), detailItemfg("MODEL").ToString(), sLine, stDatetime, detailItemfg("LOCATION_PART").ToString(), sShift, factory_cd, sLot, itemdf("total_nc"), seq, wi, itemType, dfType, menu)
                Next
            Next
        End If
        If rsNc <> "0" Then
            Dim dcResultdatachild As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsNc)
            Dim i As Integer = 1
            Dim factory_cd As String = "NO DATA"
            Dim plan_cd As String = "NO_DATA"
            If MainFrm.Label6.Text = "K2PD06" Then
                factory_cd = "8"
                plan_cd = "52"
            Else
                factory_cd = "10"
                plan_cd = "51"
            End If
            For Each itemd As Object In dcResultdatachild
                itemType = "2"
                Await WaitForNetworkWithPopup()
                Dim rsApi = Await md.mGetdatepartdetail(itemd("dt_item_cd").ToString, "2")
                Dim dChild As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsApi)
                For Each detailItemchild As Object In dChild
                    Dim objTagprintdefect = New printDefect()
                    Dim menu = "1"
                    Await WaitForNetworkWithPopup()
                    objTagprintdefect.Set_parameter_print(itemd("dt_item_cd").ToString(), detailItemchild("ITEM_NAME").ToString(), detailItemchild("MODEL").ToString(), sLine, stDatetime, detailItemchild("LOCATION_PART").ToString(), sShift, factory_cd, sLot, itemd("total_nc"), seq, wi, itemType, dfType, menu)
                Next
            Next
        End If
    End Function
    Public Sub checkPrintnormal()
        Dim defectAll = CDbl(Val(Working_Pro.lb_ng_qty.Text)) + CDbl(Val(Working_Pro.lb_nc_qty.Text))
        Dim result_mod As Double = (Integer.Parse(Working_Pro.lb_good.Text)) Mod Integer.Parse(Working_Pro.Label27.Text) 'Integer.Parse(_Edit_Up_0.Text) Mod Integer.Parse(Label27.Text)
        Dim result_total As Double = (Integer.Parse(Working_Pro.LB_COUNTER_SEQ.Text) - defectAll) Mod Integer.Parse(Working_Pro.Label27.Text) '(Integer.Parse(Working_Pro.LB_COUNTER_SEQ.Text) - defectAll) Mod Integer.Parse(Working_Pro.Label27.Text)
        'Console.WriteLine("Working_Pro.LB_COUNTER_SEQ.Text===>" & Working_Pro.LB_COUNTER_SEQ.Text)
        'Console.WriteLine("result_total===>" & result_total)
        'Console.WriteLine("Working_Pro.Label10.Text===>" & Working_Pro.Label10.Text)
        'If result_mod = "0" Then
        'If Backoffice_model.check_line_reprint() = "1" Then
        'If Working_Pro.LB_COUNTER_SEQ.Text > 0 Then
        'If CDbl(Val(Working_Pro.Label27.Text)) = 1 Or CDbl(Val(Working_Pro.Label27.Text)) = 999999 Then
        'MsgBox("A1")
        'Working_Pro.lb_box_count.Text = Working_Pro.lb_box_count.Text + 1
        'Working_Pro.Label_bach.Text = Working_Pro.Label_bach.Text + 1
        'Working_Pro.Label_bach.Text = Working_Pro.Label_bach.Text + 1
        'Working_Pro.tag_print()
        'Else
        'End If
        'End If
        'End If
        '  Else
        Working_Pro.statusPrint = "CloseLot"
        If Backoffice_model.check_line_reprint() = "1" Then
            If result_total = "0" Then
                result_total = "1"
            End If
        End If
        'If Integer.Parse(lbGood.Text) > 0 And result_mod > 0 And CDbl(Val(Working_Pro.Label10.Text)) < 0 Then
        If Integer.Parse(lbGood.Text) > 0 And result_mod > 0 Then
            If CDbl(Val(Working_Pro.Label10.Text)) < 0 Or result_mod > 0 And Working_Pro.flg_tag_print = 0 Then
                Working_Pro.lb_box_count.Text = Working_Pro.lb_box_count.Text + 1
                Working_Pro.Label_bach.Text = Working_Pro.Label_bach.Text + 1
                Dim cupprint = 0
                Dim rs = (CDbl(Val(lbNc.Text)) + (CDbl(Val(lbNg.Text))))
                If rs > 0 Then
                    If rs <= Working_Pro.Label27.Text Then
                        cupprint = 1
                    Else
                        '      MsgBox("rs ===>" & rs)
                        '       MsgBox("CDbl(Val(Working_Pro.Label27.Text)) ===>" & CDbl(Val(Working_Pro.Label27.Text)))
                        cupprint = rs / CDbl(Val(Working_Pro.Label27.Text))
                    End If
                End If
                If MainFrm.chk_spec_line = "2" Then
                    If result_mod <> 0 Then
                        Working_Pro.GoodQty = Working_Pro.lb_good.Text 'lbGood.Text
                        Working_Pro.tag_print()
                        Dim GenSEQ As Integer = sSeq - MainFrm.ArrayDataPlan.ToArray.Length
                        Dim Iseq = GenSEQ
                        Dim j As Integer = 0
                        For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                            'special
                            Iseq += 1
                            Backoffice_model.update_tagprintforDefect(itemPlanData.wi, "2", "1", Working_Pro.Spwi_id(j), (CDbl(Val(Working_Pro.lb_box_count.Text)) - 1), lbGood.Text, Math.Ceiling(cupprint))
                            j += 1
                        Next
                    End If
                Else
                    Working_Pro.GoodQty = Working_Pro.lb_good.Text
                    Working_Pro.tag_print()
                    'MsgBox(Math.Ceiling(cupprint))
                    'Backoffice_model.update_tagprintforDefect(sWi, "2", "1", Working_Pro.pwi_id, (CDbl(Val(Working_Pro.lb_box_count.Text)) - 1), Working_Pro.GoodQty, Math.Ceiling(cupprint))
                End If
                ' Working_Pro.Label_bach.Text = Working_Pro.Label_bach.Text + 1

            End If ' end if   If CDbl(Val(Working_Pro.Label10.Text)) <= 0 And result_mod > 0 Then
        End If
        Try
            Working_Pro.LB_COUNTER_SEQ.Text = 0
        Catch ex As Exception
            Working_Pro.LB_COUNTER_SEQ.Text = 0
        End Try
        ' End If
    End Sub
    Public Async Sub insertProductionactual(wi_plan As String, line_cd As String, item_cd As String, plan_qty As String, act_qty As String, seq_no As String, shift_prd As String, staff_no As String, prd_st_datetime As String, prd_end_datetime As String, lot_no As String, comp_flg2 As String, transfer_flg As String, del_flg As String, prd_flg As String, close_lot_flg As String, avarage_eff As String, avarage_act_prd_time As String)
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                transfer_flg = "1"
                If MainFrm.chk_spec_line = "2" Then
                    Dim GenSEQ As Integer = seq_no - MainFrm.ArrayDataPlan.ToArray.Length
                    Dim Iseq = GenSEQ
                    Dim j As Integer = 0
                    For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                        Iseq += 1
                        Dim special_wi As String = itemPlanData.wi
                        Dim special_item_cd As String = itemPlanData.item_cd
                        Dim special_item_name As String = itemPlanData.item_name
                        Await WaitForNetworkWithPopup()
                        Backoffice_model.Insert_prd_close_lot(special_wi, line_cd, special_item_cd, plan_qty, act_qty, Iseq, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                        Backoffice_model.Insert_prd_close_lot_sqlite(special_wi, line_cd, special_item_cd, plan_qty, act_qty, Iseq, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                        Dim temp_co_emp As Integer = List_Emp.ListView1.Items.Count
                        Dim emp_cd As String
                        For i = 0 To temp_co_emp - 1
                            emp_cd = List_Emp.ListView1.Items(i).Text
                            Backoffice_model.Insert_emp_cd(special_wi, emp_cd, Iseq)
                        Next
                    Next
                Else
                    Await WaitForNetworkWithPopup()

                    Backoffice_model.Insert_prd_close_lot(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                    Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                    Dim temp_co_emp As Integer = List_Emp.ListView1.Items.Count
                    Dim emp_cd As String
                    For i = 0 To temp_co_emp - 1
                        emp_cd = List_Emp.ListView1.Items(i).Text
                        Backoffice_model.Insert_emp_cd(wi_plan, emp_cd, seq_no)
                    Next
                End If
            Else
                transfer_flg = "0"
                If MainFrm.chk_spec_line = "2" Then
                    Dim GenSEQ As Integer = seq_no - MainFrm.ArrayDataPlan.ToArray.Length
                    Dim Iseq = GenSEQ
                    Dim j As Integer = 0
                    For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                        Iseq += 1
                        Dim special_wi As String = itemPlanData.wi
                        Dim special_item_cd As String = itemPlanData.item_cd
                        Dim special_item_name As String = itemPlanData.item_name
                        Backoffice_model.Insert_prd_close_lot_sqlite(special_wi, line_cd, special_item_cd, plan_qty, act_qty, Iseq, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                    Next
                Else
                    Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                End If
            End If
        Catch ex As Exception
            transfer_flg = "0"
            'MsgBox("error = > " & ex.Message)
            If MainFrm.chk_spec_line = "2" Then
                Dim GenSEQ As Integer = seq_no - MainFrm.ArrayDataPlan.ToArray.Length
                Dim Iseq = GenSEQ
                Dim j As Integer = 0
                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                    Iseq += 1
                    Dim special_wi As String = itemPlanData.wi
                    Dim special_item_cd As String = itemPlanData.item_cd
                    Dim special_item_name As String = itemPlanData.item_name
                    Backoffice_model.Insert_prd_close_lot_sqlite(special_wi, line_cd, special_item_cd, plan_qty, act_qty, Iseq, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
                Next
            Else
                Backoffice_model.Insert_prd_close_lot_sqlite(wi_plan, line_cd, item_cd, plan_qty, act_qty, seq_no, shift_prd, staff_no, prd_st_datetime, prd_end_datetime, lot_no, comp_flg2, transfer_flg, del_flg, prd_flg, close_lot_flg, avarage_eff, avarage_act_prd_time)
            End If
        End Try
    End Sub
    Public Async Sub ClickOk(dtWino As String, dtLineno As String, dtItemcd As String, dtItemtype As String, dtLotno As String, dtSeqno As String, dtType As String, dtCode As String, dtQty As String, dtActualdate As String, pwi_id As String)
        Dim md As New modelDefect()
        Dim apimdSQLite = New model_api_sqlite()
        Dim mdSQLite = New ModelSqliteDefect
        Dim cFlg As Integer = comPleteflg(sAct, pQty)
        Try
recheck_defect:
            ' ✅ ตรวจสอบ network ก่อนเริ่ม insert defect actual
            Await WaitForNetworkWithPopup()
            Dim da_tranfer_flg As Integer = 1
            Dim lastId = Await md.mInsertdefectactual(dtWino, dtLineno, dtItemcd, dtItemtype, dtLotno, dtSeqno, dtType, dtCode, dtQty, "1", dtActualdate, pwi_id)
            Dim lastIdsqlite = Await apimdSQLite.mInsertdefectactualsqlites(dtWino, dtLineno, dtItemcd, dtItemtype, dtLotno, dtSeqno, dtType, dtCode, dtQty, "1", dtActualdate, pwi_id, da_tranfer_flg)
            'If lastId = "0" Then
            ' 🔁 Net down หรือ insert fail ให้ loop ไปใหม่
            'GoTo recheck_defect
            'End If
            ' ✅ ถ้าเป็นประเภท itemtype = 2 ต้อง Insert Supplier ด้วย
            If dtItemtype = "2" Then
                Try
                    Dim getData = Await mdSQLite.mSqliteGetdefectdetail(dtWino, dtSeqno, dtLotno, dtType, dtItemcd, dtCode)
                    If getData <> "0" Then
                        Dim rsData2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(getData)
                        For Each item As Object In rsData2
                            If Not String.IsNullOrWhiteSpace(item("dt_supplier_code").ToString()) Then
                                ' ✅ ตรวจ network ก่อน insert supplier defect
                                Await WaitForNetworkWithPopup()
                                Dim insData = Await md.minsertDefectTrascetionSupplier(lastId, item("dt_supplier_code").ToString(), item("total_nc").ToString(), dtLineno)
                            End If
                        Next
                    End If
                Catch ex As Exception
                    ' ❗ ถ้า error ก็รอ Network แล้วปล่อย error ไปเพื่อไม่ให้ระบบ crash
                    Task.Run(Async Function()
                                 Await WaitForNetworkWithPopup()
                             End Function)
                End Try
            End If
            Dim getDataLeader = Await mdSQLite.mSqliteGetdefectdetailLeaderConFirm(dtWino, dtSeqno, dtLotno, dtType, dtItemcd, dtCode)
            If getDataLeader <> "0" Then
                Dim rsDataLeaderCon As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(getDataLeader)
                For Each itemLeader As Object In rsDataLeaderCon
                    If dtLineno <> itemLeader("dt_created_by").ToString() Then
                        ' ✅ ตรวจ network ก่อน insert supplier defect
                        Await WaitForNetworkWithPopup()
                        Dim insData = Await md.minsertDefectLeaderConFirm(lastId, itemLeader("dt_created_by").ToString(), itemLeader("total_nc").ToString(), dtLineno)
                    End If
                Next
            End If
        Catch ex As Exception
            ' ❗ ถ้าเกิด exception ทั้งหมดในขั้นตอนหลักก็แสดง popup network และหยุดที่นี่
            Task.Run(Async Function()
                         Await WaitForNetworkWithPopup()
                     End Function)
        End Try
    End Sub

    Public Function comPleteflg(Act As Integer, Plan As Integer)
        Dim cFlg As Integer = 0
        If Act < Plan Then
            cFlg = 0
        Else
            cFlg = 1
        End If
        Return cFlg
    End Function
    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        BTNUPCHILDPART()
    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        BTNDOWNCHILDPART()
    End Sub
    Public Sub BTNUPCHILDPART()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvSumarychild.Items.Count - 1))) Then
            S_index = CDbl(Val((lvSumarychild.Items.Count - 1)))
        End If
        Try
            lvSumarychild.Items(S_index).Selected = False
            S_index -= 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf lvSumarychild.Items.Count > S_index Then
                ' S_index = CDbl(Val((lvSumarychild.Items.Count - 1)))
            End If
            lvSumarychild.Items(S_index).Selected = True
            lvSumarychild.Items(S_index).EnsureVisible()
            lvSumarychild.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BTNDOWNCHILDPART()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvSumarychild.Items.Count - 1))) Then
            S_index = CDbl(Val((lvSumarychild.Items.Count - 1)))
        End If
        Try
            lvSumarychild.Items(S_index).Selected = False
            S_index += 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf lvSumarychild.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvSumarychild.Items.Count - 1)))
            End If
            lvSumarychild.Items(S_index).Selected = True
            lvSumarychild.Items(S_index).EnsureVisible()
            lvSumarychild.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub lvSumarychild_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvSumarychild.SelectedIndexChanged

    End Sub

    Private Sub lvSumaryfg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvSumaryfg.SelectedIndexChanged

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        BTNUPFG()
    End Sub
    Public Sub BTNUPFG()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvSumaryfg.Items.Count - 1))) Then
            S_index = CDbl(Val((lvSumaryfg.Items.Count - 1)))
        End If
        Try
            lvSumaryfg.Items(S_index).Selected = False
            S_index -= 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf lvSumaryfg.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvSumaryfg.Items.Count - 1)))
            End If
            lvSumaryfg.Items(S_index).Selected = True
            lvSumaryfg.Items(S_index).EnsureVisible()
            lvSumaryfg.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BTNDOWNFG()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvSumaryfg.Items.Count - 1))) Then
            S_index = CDbl(Val((lvSumaryfg.Items.Count - 1)))
        End If
        Try
            lvSumaryfg.Items(S_index).Selected = False
            S_index += 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf lvSumaryfg.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvSumaryfg.Items.Count - 1)))
            End If
            lvSumaryfg.Items(S_index).Selected = True
            lvSumaryfg.Items(S_index).EnsureVisible()
            lvSumaryfg.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        BTNDOWNFG()
    End Sub
    Private Sub lbNg_Click(sender As Object, e As EventArgs) Handles lbNg.Click

    End Sub
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        btnOk.Visible = True
        ShowSpcDetailDefect.Show()
    End Sub
    Async Function checkNetColselot() As Task
        Await Task.Delay(3000).ContinueWith(Sub(task)
                                                If Not task.IsCanceled Then
                                                    ' ตรวจสอบว่า Handle ของฟอร์มยังถูกสร้างอยู่และไม่ถูก Dispose
                                                    If Me.IsHandleCreated AndAlso Not Me.IsDisposed Then
                                                        Try
                                                            Me.Invoke(Sub()
                                                                          Try
                                                                              If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                                                                                  loadNet.Visible = False
                                                                                  NextProcess = 1
                                                                              Else
                                                                                  loadNet.Visible = True
                                                                                  NextProcess = 0
                                                                              End If
                                                                              checkNetColselot()
                                                                          Catch ex As Exception
                                                                              loadNet.Visible = True
                                                                              NextProcess = 0
                                                                              checkNetColselot()
                                                                          End Try
                                                                      End Sub)
                                                        Catch ex As Exception
                                                        End Try
                                                    End If
                                                End If
                                            End Sub, TaskScheduler.FromCurrentSynchronizationContext())
    End Function
End Class