Imports System.Web.Script.Serialization
Public Class defectAlertsuredefect
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dfAdminHome As New defectAdminhome
        Dim dfType As String = ""
        If dfAdminHome.dfCatType = "Register" Then
            manage_Register()
        ElseIf dfAdminHome.dfCatType = "Adjust" Then
            manage_Adjust()
        End If
    End Sub
    Public Sub manage_Adjust()
        'MsgBox("defectAdminregister.tbQtydefect.Text = " & defectAdminregister.tbQtydefect.Text)
        Dim dfAdminHome As New defectAdminhome
        Dim dfAdminRegister As New defectAdminregister
        If defectAdminAdjustnumpadadjust.tbAddjust.Text < 0 Then
            defectAdminAdjustnumpadadjust.tbAddjust.Text = 0
        End If
        If defectAdminAdjustnumpadadjust.tbAddjust.Text = "" Then
            defectAdminAdjustnumpadadjust.tbAddjust.Text = 0
        End If
        If dfAdminHome.dfType = "NC" Then
            dfType = "2"
        ElseIf dfAdminHome.dfType = "NG" Then
            dfType = "1"
        End If
        Dim dtActualdate = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
        'MsgBox("defectAdminregister.dtQty = " & defectAdminregister.dtQty)
        Dim clSumarry As New closeLotsummary()
        Dim trFlg As String = "1"
        Dim dFlg As String = "0"
        Dim prdFlg As String = "1"
        Dim clFlg As String = "1"
        Dim rsDataupdate = defectAdminAdjustnumpadadjust.updateAddjustqty(defectAdminAdjustnumpadadjust.dtWino, defectAdminAdjustnumpadadjust.dtLotNo, defectAdminAdjustnumpadadjust.dtSeqno, dfType, defectAdminAdjustnumpadadjust.dtCode, defectAdminAdjustnumpadadjust.lbPart.Text, defectAdminselectdetailncadjust.Apwi_id)
        defectAdminAdjustnumpadadjust.Close()
        defectAdminAdjustdetail.loadData()
        'Dim objDefect As New defectAdminAdjustdetailnc()
        'objDefect.Show()
        checkPrintdefectAdmin(defectAdminAdjustnumpadadjust.dtWino, defectAdminAdjustnumpadadjust.dtSeqno, defectAdminAdjustnumpadadjust.dtLotNo)
        If defectAdminAdjustnumpadadjust.dtItemtype = "1" Then
            Dim objTagPrintNormal = New tag_print_normal
            objTagPrintNormal.set_tag_print_normal(defectAdmindetailnc.sWi, defectAdminregister.dtLotno, defectAdminregister.dtSeqno, defectAdmindetailnc.sshift, defectAdminAdjustnumpadadjust.GuseQty, defectAdminAdjustnumpadadjust.GmaxQty)
        End If
        Me.Close()
    End Sub
    Public Sub manage_Register()
        'MsgBox("defectAdminregister.tbQtydefect.Text = " & defectAdminregister.tbQtydefect.Text)
        Dim dfAdminHome As New defectAdminhome
        Dim dfAdminRegister As New defectAdminregister
        If defectAdminregister.lbQtydefect.Text < 0 Then
            defectAdminregister.lbQtydefect.Text = 0
        End If
        If defectAdminregister.lbQtydefect.Text = "" Then
            defectAdminregister.lbQtydefect.Text = 0
        End If
        Dim Apiw_id As String = ""
        If dfAdminHome.dfType = "NC" Then
            dfType = "2"
            Apiw_id = defectAdmindetailnc.Apwi_id
            ' MsgBox("Apiw_id === > " & Apiw_id)
        ElseIf dfAdminHome.dfType = "NG" Then
            dfType = "1"
            Apiw_id = defectAdmindetailng.Apwi_id
        End If
        Dim dtActualdate = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
        ' asdasd
        Dim rs = defectAdminregister.insertDefectregister(defectAdminregister.dtWino, defectAdminregister.dtLineno, defectAdminregister.dtItemcd, defectAdminregister.dtItemtype, defectAdminregister.dtLotno, defectAdminregister.dtSeqno, dfType, defectAdminregister.dtCode, defectAdminregister.lbQtydefect.Text, "2", dtActualdate, Apiw_id)
        If rs Then
            Dim clSumarry As New closeLotsummary()
            Dim trFlg As String = "1"
            Dim dFlg As String = "0"
            Dim prdFlg As String = "1"
            Dim clFlg As String = "1"
            Dim md As New modelDefect()
            'ก่อน Update Update Defect Actual
            Dim oldQty = md.mGetQtyofdefectcode(defectAdminregister.dtWino, defectAdminregister.dtLotno, defectAdminregister.dtSeqno, dfType, defectAdminregister.dtCode, defectAdminregister.dtItemcd)
            Dim rsDataupdate = defectAdminregister.updateDefectdata(defectAdminregister.dtWino, defectAdminregister.dtLotno, defectAdminregister.dtSeqno, dfType, defectAdminregister.dtCode, defectAdminregister.dtItemcd, defectAdminregister.dtItemtype)
            If dfAdminHome.dfType = "NC" Then
                manageNc(rsDataupdate, oldQty)
            ElseIf dfAdminHome.dfType = "NG" Then
                manageNg(rsDataupdate, oldQty)
            End If
            defectAdminregister.Close()
            defectAlertupdatesuccess.Show()
            Me.Close()
        End If
    End Sub
    Public Sub manageNc(rsDataupdate As Boolean, oldQtyofPartNo As String)
        If rsDataupdate Then
            Dim md As New modelDefect()
            Dim pDefect As New adminPrintdefect()
            Dim dfAdmindetailnc As New defectAdmindetailnc()
            Dim dfAdminHome As New defectAdminhome
            Dim dtType As String = ""
            If dfAdminHome.dfType = "NC" Then
                dtType = "2"
            ElseIf dfAdminHome.dfType = "NG" Then
                dtType = "1"
            End If
            dtQty = oldQtyofPartNo + CDbl(Val(defectAdminregister.tbQty))
            Dim dtActualdate = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
            Dim rsDatainsert = md.mInsertdefectactual(defectAdminregister.dtWino, defectAdminregister.dtLineno, defectAdminregister.dtItemcd, defectAdminregister.dtItemtype, defectAdminregister.dtLotno, defectAdminregister.dtSeqno, dtType, defectAdminregister.dtCode, dtQty, "2", dtActualdate, defectAdmindetailnc.Apwi_id)
            Dim rsApi = md.mGetdatepartdetail(defectAdminregister.dtItemcd, defectAdminselecttypenc.type)
            Dim dChild As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsApi)
            Dim factory_cd As String = "NO DATA"
            Dim plan_cd As String = "NO_DATA"
            If MainFrm.Label6.Text = "K2PD06" Then
                factory_cd = "8"
                plan_cd = "52"
            Else
                factory_cd = "10"
                plan_cd = "51"
            End If
            For Each data As Object In dChild
                Dim menu As String = "2"
                pDefect.Set_parameter_print(data("ITEM_CD").ToString(), data("ITEM_NAME").ToString(), data("MODEL").ToString(), defectAdminregister.dtLineno, Date.Now, data("LOCATION_PART").ToString(), getShift(), factory_cd, defectAdmindetailnc.sLot, defectAdminregister.tbQty, defectAdmindetailnc.dSeq, defectAdmindetailnc.sWi, defectAdminselecttypenc.type, defectAdminselectcodenc.sDefectcode, dfAdminHome.dfType, menu)
            Next
            If defectAdminregister.dtItemtype = "1" Then
                Dim objTagPrintNormal = New tag_print_normal
                objTagPrintNormal.set_tag_print_normal(defectAdmindetailnc.sWi, defectAdminregister.dtLotno, defectAdminregister.dtSeqno, defectAdmindetailnc.sshift, defectAdminregister.tbQty, defectAdminregister.GmaxQty)
            End If
        End If
    End Sub
    Public Function getShift()
        Dim time = DateTime.Now.ToString("HH:mm:ss")
        Dim shift As String = ""
        If time >= "08:00:00" And time <= "20:00:00" Then
            shift = "P"
        Else
            shift = "Q"
        End If
        Return shift
    End Function
    Public Sub checkPrintdefectAdmin(wi As String, seq As String, lot As String)
        stDatetime = DateTime.Now.ToString("HH:mm:ss")
        Dim start_date As Date = Date.Now
        Dim md = New modelDefect()
        Dim obj As New defectAdminAdjustdetail
        Dim dfAdminHome As New defectAdminhome
        Dim dfType As String = "" 'NC
        If dfAdminHome.dfType = "NC" Then
            dfType = "2"
        ElseIf dfAdminHome.dfType = "NG" Then
            dfType = "1"
        End If
        Dim mdsqlie = New ModelSqliteDefect
        'rsNc = md.GetdatachildpartsummarychildgrouppartAdminAdjust(wi, seq, lot, dfType, obj.SPART) 'NC
        'rsFgNc = md.Getdatachildpartsummaryfggrouppart(wi, seq, lot, dfType) 'NC
        rsNc = mdsqlie.mSqliteGetdatachildpartsummarychildgrouppartAdminAdjust(wi, seq, lot, dfType, obj.SPART)
        rsFgNc = mdsqlie.mSqliteGetdatachildpartsummaryfggrouppart(wi, seq, lot, dfType)
        Dim itemType = "1"
        If obj.SItemType = "FG" Then
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
                    Dim rsApi = md.mGetdatepartdetail(itemdf("dt_item_cd").ToString, "1")
                    If rsApi <> "0" Then
                        Dim dFg As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsApi)
                        For Each detailItemfg As Object In dFg
                            Dim objTagprintdefect = New adminPrintdefect()
                            Dim menu As String = "2"
                            objTagprintdefect.Set_parameter_print(itemdf("dt_item_cd").ToString(), detailItemfg("ITEM_NAME").ToString(), detailItemfg("MODEL").ToString(), MainFrm.Label4.Text, start_date, detailItemfg("LOCATION_PART").ToString(), defectAdminsearch.Pshift, factory_cd, defectAdminselectdetailncadjust.sLot, itemdf("total_nc"), seq, wi, dfType, defectAdminAdjustnumpadadjust.dtCode, dfType, menu)
                        Next
                    End If
                Next
            End If
        ElseIf obj.SItemType = "CP" Then
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
                    Dim rsApi = md.mGetdatepartdetail(itemd("dt_item_cd").ToString, "2")
                    Dim dChild As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsApi)
                    For Each detailItemchild As Object In dChild
                        Dim objTagprintdefect = New adminPrintdefect()
                        Dim menu = "2"
                        Dim defectCode As String = ""
                        objTagprintdefect.Set_parameter_print(itemd("dt_item_cd").ToString(), detailItemchild("ITEM_NAME").ToString(), detailItemchild("MODEL").ToString(), MainFrm.Label4.Text, start_date, detailItemchild("LOCATION_PART").ToString(), defectAdminsearch.Pshift, factory_cd, defectAdminselectdetailncadjust.sLot, itemd("total_nc"), seq, wi, itemType, defectCode, dfType, menu)
                    Next
                Next
            End If
        End If
        dfType = "1" 'NG
        rsNc = md.mGetdatachildpartsummarychildgrouppart(wi, seq, lot, dfType) 'NG
        rsFgNc = md.Getdatachildpartsummaryfggrouppart(wi, seq, lot, dfType) 'NG
        itemType = "1"
    End Sub
    Public Sub manageNg(rsDataupdate As Boolean, oldQtyofPartNo As String)
        If rsDataupdate Then
            Dim Apwi_id As String = ""
            Dim md As New modelDefect()
            Dim pDefect As New adminPrintdefect()
            Dim dfAdmindetailng As New defectAdmindetailng()
            Dim dfAdminHome As New defectAdminhome
            dtQty = oldQtyofPartNo + CDbl(Val(defectAdminregister.tbQty))
            Dim dfType = "1"
            Dim dtActualdate = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
            If dfAdminHome.dfType = "NC" Then
                Apiw_id = defectAdmindetailnc.Apwi_id
            ElseIf dfAdminHome.dfType = "NG" Then
                Apiw_id = defectAdmindetailng.Apwi_id
            End If
            Dim rsDatainsert = md.mInsertdefectactual(defectAdminregister.dtWino, defectAdminregister.dtLineno, defectAdminregister.dtItemcd, defectAdminregister.dtItemtype, defectAdminregister.dtLotno, defectAdminregister.dtSeqno, dfType, defectAdminregister.dtCode, dtQty, "2", dtActualdate, Apiw_id)
            Dim rsApi = md.mGetdatepartdetail(defectAdminregister.sPart, defectAdminselecttypeng.type)
            Dim dChild As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsApi)
            Dim factory_cd As String = "NO DATA"
            Dim plan_cd As String = "NO_DATA"
            If MainFrm.Label6.Text = "K2PD06" Then
                factory_cd = "8"
                plan_cd = "52"
            Else
                factory_cd = "10"
                plan_cd = "51"
            End If
            For Each data As Object In dChild
                Dim menu As String = "2"
                pDefect.Set_parameter_print(data("ITEM_CD").ToString(), data("ITEM_NAME").ToString(), data("MODEL").ToString(), defectAdminregister.dtLineno, Date.Now, data("LOCATION_PART").ToString(), getShift(), factory_cd, defectAdmindetailng.sLot, defectAdminregister.tbQty, defectAdmindetailng.dSeq, defectAdmindetailng.sWi, defectAdminselecttypeng.type, defectAdminselectcodeng.sDefectcode, dfAdminHome.dfType, menu)
            Next
            If defectAdminregister.dtItemtype = "1" Then
                Dim objTagPrintNormal = New tag_print_normal
                objTagPrintNormal.set_tag_print_normal(defectAdmindetailng.sWi, defectAdminregister.dtLotno, defectAdminregister.dtSeqno, defectAdmindetailng.sshift, defectAdminregister.tbQty, defectAdminregister.GmaxQty)
            End If
        End If
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub
End Class