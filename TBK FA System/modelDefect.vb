Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.Globalization
Imports System.Data
Imports Newtonsoft.Json.Linq
Public Class modelDefect
    Public Shared bf = New Backoffice_model()
    Public Shared Function mCheckTypeChildPart(item_cd As String, wi As String)
        ' 0 = machine In house /  1 Order Supplier
        Return "1"
    End Function
    Public Shared Function mGetDataSupplier(item_cd As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/EXP_CHECK_SUPP?item_cd=" & item_cd)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/EXP_CHECK_SUPP?item_cd=" & item_cd)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetDataSupplier = " & ex.Message)
            Return 0
        End Try
    End Function
    Public Shared Async Function minsertDefectTrascetionSupplier(lastId As String, dt_supplier_code As String, dt_qty As String, Line As String) As Task(Of String)
        Try
            Dim api = New api()
            ' Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectTransctionSupplier?lastId=" & lastId & "&dt_supplier_code=" & dt_supplier_code & "&dt_qty=" & dt_qty & "&Line=" & Line)
            ' Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectTransctionSupplier?lastId=" & lastId & "&dt_supplier_code=" & dt_supplier_code & "&dt_qty=" & dt_qty & "&Line=" & Line)
            ' Return rsData
            Try
                Dim url As String = "http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectTransctionSupplier" &
                            "?lastId=" & lastId &
                            "&dt_supplier_code=" & dt_supplier_code &
                            "&dt_qty=" & dt_qty &
                            "&Line=" & Line
                Console.WriteLine("Calling API URL: " & url)
                ' ✅ แปลงให้ async โดยรันบน background thread
                Dim rsData As String = Await Task.Run(Function() api.Load_data(url))
                Return rsData
            Catch ex As Exception
                MsgBox("❗ connect Api Fail in minsertDefectTrascetionSupplier = " & ex.Message)
                Return "0"
            End Try
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function minsertDefectTrascetionSupplier = " & ex.Message)
            Return "0"
        End Try
    End Function
    Public Shared Async Function minsertDefectLeaderConFirm(lastId As String, dpl_emp_cd As String, dpl_qty As String, line_cd As String) As Task(Of String)
        Try
            Dim api = New api()
            ' Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectTransctionSupplier?lastId=" & lastId & "&dt_supplier_code=" & dt_supplier_code & "&dt_qty=" & dt_qty & "&Line=" & Line)
            ' Console.WriteLine("http://" & Backoffice_mssodel.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectTransctionSupplier?lastId=" & lastId & "&dt_supplier_code=" & dt_supplier_code & "&dt_qty=" & dt_qty & "&Line=" & Line)
            ' Return rsData
            Try
                Dim url As String = "http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectLeaderConFirm" &
                            "?lastId=" & lastId &
                            "&dpl_emp_cd=" & dpl_emp_cd &
                            "&dpl_qty=" & dpl_qty &
                            "&line_cd=" & line_cd
                Console.WriteLine("Calling API URL: " & url)
                ' ✅ แปลงให้ async โดยรันบน background thread
                Dim rsData As String = Await Task.Run(Function() api.Load_data(url))
                Return rsData
            Catch ex As Exception
                MsgBox("❗ connect Api Fail in minsertDefectLeaderConFirm = " & ex.Message)
                Return "0"
            End Try
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function minsertDefectLeaderConFirm = " & ex.Message)
            Return "0"
        End Try
    End Function
    Public Shared Function mGetPwi_id(WI As String, LOT_NO As String, SEQ_NO As String, SHIFT As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getPwi?WI=" & WI & "&LOT_NO=" & LOT_NO & "&SEQ_NO=" & SEQ_NO & "&SHIFT=" & SHIFT)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getPwi?WI=" & WI & "&LOT_NO=" & LOT_NO & "&SEQ_NO=" & SEQ_NO & "&SHIFT=" & SHIFT)
            If rsData <> "0" Then
                Return rsData
            Else
                MsgBox("connect Api Faill Please check modelDefect in Function mGetPwi_id Data = 0 ")
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetPwi_id = " & ex.Message)
            Return 0
        End Try
    End Function
    Public Shared Function mGetTagprintDetailSpecial(wi As String, lot As String, seq As String, shift As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetTagprintDetailSpecial?wi=" & wi & "&lot=" & lot & "&seq=" & seq & "&shift=" & shift)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetTagprintDetailSpecial?wi=" & wi & "&lot=" & lot & "&seq=" & seq & "&shift=" & shift)
            If rsData <> "0" Then
                Return rsData
            Else
                MsgBox("connect Api Faill Please check modelDefect in Function mGetTagprintDetailSpecial Data = 0 ")
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetTagprintDetailSpecial = " & ex.Message)
            Return 0
        End Try
    End Function
    Public Shared Function mGetDataEnableFGPart(line_cd As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetDataEnableFGPart?line_cd=" & line_cd)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetDataEnableFGPart?line_cd=" & line_cd)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetDataEnableFGPart = " & ex.Message)
            Return 0
        End Try
    End Function
    Public Shared Function mGetGoodWILot(wi As String, lot_no As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetGoodWILot?wi=" & wi & "&lot_no=" & lot_no)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetGoodWILot?wi=" & wi & "&lot_no=" & lot_no)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetGoodWILot = " & ex.Message)
            Return 0
        End Try
    End Function
    Public Shared Function mGetTagprintDetailNormal(wi As String, lot As String, seq As String, shift As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetTagprintDetailNormal?wi=" & wi & "&lot=" & lot & "&seq=" & seq & "&shift=" & shift)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetTagprintDetailNormal?wi=" & wi & "&lot=" & lot & "&seq=" & seq & "&shift=" & shift)
            If rsData <> "0" Then
                Return rsData
            Else
                MsgBox("connect Api Faill Please check modelDefect in Function mGetTagprintDetailNormal Data = 0 ")
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetTagprintDetailNormal = " & ex.Message)
            Return 0
        End Try
    End Function
    Public Shared Function mGetchildpart(wi)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getChildpart?wi=" & wi)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getChildpart?wi=" & wi)
            If rsData <> "0" Then
                Return rsData
            Else
                '  MsgBox("connect Api Faill Please check modelDefect in Function mGetchildpart Data = 0 ")
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetchildpart = " & ex.Message)
            Return 0
        End Try
    End Function
    Public Shared Function mGetDatamsterLine(line As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetDataLine?line=" & line)
            If rsData <> "0" Then
                Return rsData
            Else
                MsgBox("connect Api Faill Please check modelDefect in Function mGetDatamsterLine Data = 0 ")
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetDatamsterLine = " & ex.Message)
            Return 0
        End Try
    End Function
    Public Shared Function mGetchildpartSpc(arrWi As Array)
        Try
            Dim api = New api()
            Dim requestFunction As New JObject()
            Dim jArrayWI As New JArray(arrWi)
            'Console.WriteLine(jArrayWI)
            requestFunction("arrWi") = jArrayWI
            Dim url As String = "http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefectSpecial/getChildpart"
            Dim result = api.Load_dataPOST(url, requestFunction)
            If result <> "0" Then
                Return result
            Else
                MsgBox("connect Api Faill Please check modelDefect in Function mGetchildpartSpc Data = 0 ")
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetchildpartSpc = " & ex.Message)
            Return 0
        End Try
    End Function
    Public Function mGetPartno(wi As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetMainPartNo?Wi=" & wi)
            If rsData <> "0" Then
                Return rsData
            Else
                MsgBox("connect Api Faill Please check modelDefect in Function mGetPartno Data = 0 ")
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetPartno = " & ex.Message)
            Return 0
        End Try
    End Function
    Public Async Function mGetdatepartdetail(pNo As String, flg As String) As Task(Of String)
        Try
            Dim api = New api()
            ' Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectTransctionSupplier?lastId=" & lastId & "&dt_supplier_code=" & dt_supplier_code & "&dt_qty=" & dt_qty & "&Line=" & Line)
            ' Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectTransctionSupplier?lastId=" & lastId & "&dt_supplier_code=" & dt_supplier_code & "&dt_qty=" & dt_qty & "&Line=" & Line)
            ' Return rsData
            Try
                Dim url As String = "http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDataplan" &
                            "?itemCd=" & pNo &
                            "&flg=" & flg
                Console.WriteLine("Calling API URL: " & url)
                ' ✅ แปลงให้ async โดยรันบน background thread
                Dim rsData As String = Await Task.Run(Function() api.Load_data(url))
                If rsData <> "0" Then
                    Return rsData
                Else
                    MsgBox("connect Api Faill Please check modelDefect in Function mGetdatepartdetail Data = 0 ")
                    Return 0
                End If
            Catch ex As Exception
                MsgBox("❗ connect Api Fail in minsertDefectTrascetionSupplier = " & ex.Message)
                Return "0"
            End Try
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdatepartdetail = " & ex.Message)
            Return "0"
        End Try
    End Function
    Public Shared Function mGetdefectcode(LineCd As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDefectcode?LineCd=" & LineCd)
            If rsData <> "0" Then
                Return rsData
            Else
                MsgBox("Please Check Master Defect Code.")
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdefectcode = " & ex.Message)
            Return 0
        End Try
    End Function
    Public Shared Function mGetDatadefectcodeprint(wi As String, lot As String, seqNo As String, itemCd As String, dfType As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDatadefectcodeprint?wi=" & wi & "&lot=" & lot & "&seqNo=" & seqNo & "&itemCd=" & itemCd & "&dfType=" & dfType)
            'MsgBox("model====>" & rsData)
            Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDatadefectcodeprint?wi=" & wi & "&lot=" & lot & "&seqNo=" & seqNo & "&itemCd=" & itemCd & "&dfType=" & dfType)
            If rsData <> "0" Then
                Return rsData
            Else
                '     MsgBox("connect Api Faill Please check modelDefect in Function mGetDatadefectcodeprint Data = 0 ")
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetDatadefectcodeprint = " & ex.Message)
            Return 0
        End Try
    End Function
    Public Shared Function mGetboxInformation(wi As String, lot As String, seqNo As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getBoxInformation?wi=" & wi & "&lot=" & lot & "&seq=" & seqNo)
            If rsData <> "0" Then
                Return rsData
            Else
                ' MsgBox("connect Api Faill Please check modelDefect in Function mGetboxInformation Data = 0 ")
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetboxInformation = " & ex.Message)
            Return 0
        End Try
    End Function
    Public Shared Function mInsertdefectactualsqlite(wi As String, lot As String, seqNo As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getBoxInformation?wi=" & wi & "&lot=" & lot & "&seq=" & seqNo)
            If rsData <> "0" Then
                Return rsData
            Else
                ' MsgBox("connect Api Faill Please check modelDefect in Function mGetboxInformation Data = 0 ")
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetboxInformation = " & ex.Message)
            Return 0
        End Try
    End Function

    Public Shared Function mInsertdefectregister(dtWino As String, dtLineno As String, dtItemcd As String, dtItemtype As String, dtLotno As String, dtSeqno As String, dtType As String, dtCode As String, dtQty As String, dtMenu As String, dtActualdate As String, pwi_id As String)
        Try
            Dim api = New api()
            Dim rsData As Boolean = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectregister?dtWino=" & dtWino & "&dtLineno=" & dtLineno & "&dtItemcd=" & dtItemcd & "&dtItemtype=" & dtItemtype & "&dtLotNo=" & dtLotno & "&dtSeqno=" & dtSeqno & "&dtType=" & dtType & "&dtCode=" & dtCode & "&dtQty=" & dtQty & "&dtMenu=" & dtMenu & "&dtActualdate=" & dtActualdate & "&pwi_id=" & pwi_id)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectregister?dtWino=" & dtWino & "&dtLineno=" & dtLineno & "&dtItemcd=" & dtItemcd & "&dtItemtype=" & dtItemtype & "&dtLotNo=" & dtLotno & "&dtSeqno=" & dtSeqno & "&dtType=" & dtType & "&dtCode=" & dtCode & "&dtQty=" & dtQty & "&dtMenu=" & dtMenu & "&dtActualdate=" & dtActualdate & "&pwi_id=" & pwi_id)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mInsertdefectregister = " & ex.Message)
            Return False
        End Try
    End Function
    ' Public Shared Function mInsertdefectactual(dtWino As String, dtLineno As String, dtItemcd As String, dtItemtype As String, dtLotno As String, dtSeqno As String, dtType As String, dtCode As String, dtQty As String, dtMenu As String, dtActualdate As String, pwi_id As String)
    '  Try
    '   Dim api = New api()
    '    Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectactual?dtWino=" & dtWino & "&dtLineno=" & dtLineno & "&dtItemcd=" & dtItemcd & "&dtItemtype=" & dtItemtype & "&dtLotNo=" & dtLotno & "&dtSeqno=" & dtSeqno & "&dtType=" & dtType & "&dtCode=" & dtCode & "&dtQty=" & dtQty & "&dtMenu=" & dtMenu & "&dtActualdate=" & dtActualdate & "&pwi_id=" & pwi_id)
    '            Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectactual?dtWino=" & dtWino & "&dtLineno=" & dtLineno & "&dtItemcd=" & dtItemcd & "&dtItemtype=" & dtItemtype & "&dtLotNo=" & dtLotno & "&dtSeqno=" & dtSeqno & "&dtType=" & dtType & "&dtCode=" & dtCode & "&dtQty=" & dtQty & "&dtMenu=" & dtMenu & "&dtActualdate=" & dtActualdate & "&pwi_id=" & pwi_id)
    '            Return rsData
    '  Catch ex As Exception
    '         MsgBox("connect Api Faill Please check modelDefect in Function mInsertdefectactual = " & ex.Message)
    '  Return False
    '   End Try
    '   End Function
    Public Shared Async Function mInsertdefectactual(
    dtWino As String,
    dtLineno As String,
    dtItemcd As String,
    dtItemtype As String,
    dtLotno As String,
    dtSeqno As String,
    dtType As String,
    dtCode As String,
    dtQty As String,
    dtMenu As String,
    dtActualdate As String,
    pwi_id As String
) As Task(Of String)
        Try
            Dim api = New api()
            Dim url As String = "http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectactual" &
                            "?dtWino=" & dtWino &
                            "&dtLineno=" & dtLineno &
                            "&dtItemcd=" & dtItemcd &
                            "&dtItemtype=" & dtItemtype &
                            "&dtLotNo=" & dtLotno &
                            "&dtSeqno=" & dtSeqno &
                            "&dtType=" & dtType &
                            "&dtCode=" & dtCode &
                            "&dtQty=" & dtQty &
                            "&dtMenu=" & dtMenu &
                            "&dtActualdate=" & dtActualdate &
                            "&pwi_id=" & pwi_id

            Console.WriteLine("Calling API URL: " & url)
            ' ✅ แปลงให้ async โดยรันบน background thread
            Dim rsData As String = Await Task.Run(Function() api.Load_data(url))
            Return rsData
        Catch ex As Exception
            MsgBox("❗ connect Api Fail in mInsertdefectactual = " & ex.Message)
            Return "0"
        End Try
    End Function

    Public Shared Function mUpdateaddjust(dtWino As String, dtLotNo As String, dtSeqno As String, dtType As String, dtCode As String, ItemType As String, PartNo As String)
        Try
            ' MsgBox("dtWino = " & dtWino)
            ' MsgBox("dtLotNo = " & dtLotNo)
            ' MsgBox("dtSeqno = " & dtSeqno)
            ' MsgBox("dtType = " & dtType)
            ' MsgBox("dtCode = " & dtCode)
            ' MsgBox("ItemType = " & ItemType)
            ' MsgBox("PartNo = " & PartNo)
            Dim api = New api()
            Dim rsData As Boolean = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/updateDatadefect/updateDatadefectregister?dtWino=" & dtWino & "&dtLotNo=" & dtLotNo & "&dtSeqno=" & CDbl(Val(dtSeqno)) & "&dtType=" & dtType & "&dtCode=" & dtCode & "&dtItemType=" & ItemType & "&PartNo=" & PartNo)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mUpdateaddjust = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mUpdatedefectactual(dtWino As String, dtLotNo As String, dtSeqno As String, dtType As String, dtCode As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/updateDatadefect/updateDefectactual?dtWino=" & dtWino & "&dtLotNo=" & dtLotNo & "&dtSeqno=" & dtSeqno & "&dtType=" & dtType & "&dtCode=" & dtCode & "&itemCd=" & itemCd)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mUpdatedefectactual = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mUpdatedefectactualAdmin(dtWino As String, dtLotNo As String, dtSeqno As String, dtType As String, dtCode As String, dtItemType As String, ItemCd As String)
        Try
            Dim api = New api()
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/updateDatadefect/updateDefectactualAdmin?dtWino=" & dtWino & "&dtLotNo=" & dtLotNo & "&dtSeqno=" & dtSeqno & "&dtType=" & dtType & "&dtCode=" & dtCode & "&dtItemType=" & dtItemType & "&ItemCd=" & ItemCd)
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/updateDatadefect/updateDefectactualAdmin?dtWino=" & dtWino & "&dtLotNo=" & dtLotNo & "&dtSeqno=" & dtSeqno & "&dtType=" & dtType & "&dtCode=" & dtCode & "&dtItemType=" & dtItemType & "&ItemCd=" & ItemCd)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mUpdatedefectactual = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mGetdefectdetailnc(dtWino As String, dtSeq As String, dtLot As String, Type As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDefectnc?dfWi=" & dtWino & "&dfSeq=" & dtSeq & "&dfLot=" & dtLot & "&dfType=" & Type)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDefectnc?dfWi=" & dtWino & "&dfSeq=" & dtSeq & "&dfLot=" & dtLot & "&dfType=" & Type)
            If rsData <> "0" Then
                Return rsData
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdefectdetailnc = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mGetdefectdetailncSpc(arrayWI As Array, lengthDataPlan As Integer, dtLot As String, Type As String, startSeq As Integer)
        Try
            Dim api = New api()
            Dim requestFunction As New JObject()
            Dim jArrayWI As New JArray(arrayWI)
            requestFunction("arrWi") = jArrayWI
            requestFunction("lengthDataPlan") = lengthDataPlan
            requestFunction("startseq") = startSeq
            requestFunction("dfLot") = dtLot
            requestFunction("dfType") = Type
            Dim url As String = "http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefectSpecial/getDefectnc"
            Dim rsData = api.Load_dataPOST(url, requestFunction)
            'Console.WriteLine(rsData)
            If rsData <> "0" Then
                Return rsData
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdefectdetailncSpc = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mGetdefectdetailncPartno(dtWino As String, dtSeq As String, dtLot As String, Type As String, PartNo As String)
        Try
            Dim api = New api()
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDefectncPartNo?dfWi=" & dtWino & "&dfSeq=" & dtSeq & "&dfLot=" & dtLot & "&dfType=" & Type & "&PartNo=" & PartNo)
            Dim rsData As String = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDefectncPartNo?dfWi=" & dtWino & "&dfSeq=" & dtSeq & "&dfLot=" & dtLot & "&dfType=" & Type & "&PartNo=" & PartNo)
            If rsData <> "0" Then
                Return rsData
            Else
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdefectdetailnc = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mGetdefectdetailPartno(dtWino As String, dtSeq As String, dtLot As String, Type As String, PartNo As String)
        Try
            Dim api = New api()
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDefectPartNo?dfWi=" & dtWino & "&dfSeq=" & dtSeq & "&dfLot=" & dtLot & "&dfType=" & Type & "&PartNo=" & PartNo)
            Dim rsData As String = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDefectPartNo?dfWi=" & dtWino & "&dfSeq=" & dtSeq & "&dfLot=" & dtLot & "&dfType=" & Type & "&PartNo=" & PartNo)
            If rsData <> "0" Then
                Return rsData
            Else
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdefectdetailPartno = " & ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function mGetdefectdetailncPartnoSpc(arrayWI As Array, lengthDataPlan As Integer, dtLot As String, Type As String, PartNo As String, dfWiSel As String, dfSeqSel As String)
        Try
            Dim api = New api()
            Dim requestFunction As New JObject()
            Dim jArrayWI As New JArray(arrayWI)
            requestFunction("arrWi") = jArrayWI
            requestFunction("lengthDataPlan") = lengthDataPlan
            requestFunction("dfLot") = dtLot
            requestFunction("Type") = Type
            requestFunction("PartNo") = PartNo
            requestFunction("dfWiSel") = dfWiSel
            requestFunction("dfSeqSel") = dfSeqSel
            Dim url As String = "http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefectSpecial/getDefectncPartNo"
            Dim rsData = api.Load_dataPOST(url, requestFunction)
            If rsData <> "0" Then
                Return rsData
            Else
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdefectdetailncPartnoSpc = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mGetdatachildpartsummarychild(dtWino As String, dtSeq As String, dtLot As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/Getdatachildpartsummarychild?dfWi=" & dtWino & "&dfSeq=" & dtSeq & "&dfLot=" & dtLot)
            If rsData <> "0" Then
                Return rsData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdatachildpartsummarychild = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function
    Public Shared Function mGetdatachildpartsummarychildSpc(arrayWI As Array, lengthDataPlan As Integer, dtLot As String, startseq As Integer)
        Try
            Dim api = New api()
            Dim requestFunction As New JObject()
            Dim jArrayWI As New JArray(arrayWI)
            requestFunction("dfWi") = jArrayWI
            requestFunction("lengthDataPlan") = lengthDataPlan
            requestFunction("dfLot") = dtLot
            requestFunction("startseq") = startseq
            Dim url As String = "http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefectSpecial/GetdatachildpartsummarychildSpc"
            Dim result = api.Load_dataPOST(url, requestFunction)
            'Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefectSpecial/GetdatachildpartsummarychildSpc?dfWi1=" & dtWino1 & "&dfWi2=" & dtWino2 & "&dfWi3=" & dtWino3 & "&dfWi4=" & dtWino4 & "&dfWi5=" & dtWino5 & "&dfSeq1=" & dtSeq1 & "&dfSeq2=" & dtSeq2 & "&dfSeq3=" & dtSeq3 & "&dfSeq4=" & dtSeq4 & "&dfSeq5=" & dtSeq5 & "&dfLot=" & dtLot)
            If result <> "0" Then
                Return result
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdatachildpartsummarychildSpc = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function
    Public Shared Function mGetdatachildpartsummaryfg(dtWino As String, dtSeq As String, dtLot As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/Getdatachildpartsummaryfg?dfWi=" & dtWino & "&dfSeq=" & dtSeq & "&dfLot=" & dtLot)
            If rsData <> "0" Then
                Return rsData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdatachildpartsummary = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function
    Public Shared Function mGetdatachildpartsummaryfgSpc(arrayWI As Array, lengthDataPlan As Integer, dtLot As String, startseq As Integer)
        Try
            Dim requestFunction As New JObject()
            Dim api = New api()
            Dim jArrayWI As New JArray(arrayWI)
            requestFunction("dfWi") = jArrayWI
            requestFunction("lengthDataPlan") = lengthDataPlan
            requestFunction("dfLot") = dtLot
            requestFunction("startseq") = startseq
            Dim url As String = "http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefectSpecial/GetdatachildpartsummaryfgSpc"
            Dim rsData = api.Load_dataPOST(url, requestFunction)
            '  Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefectSpecial/GetdatachildpartsummaryfgSpc?dfWi1=" & dtWino1 & "&dfWi2=" & dtWino2 & "&dfWi3=" & dtWino3 & "&dfWi4=" & dtWino4 & "&dfWi5=" & dtWino5 & "&dfSeq1=" & dtSeq1 & "&dfSeq2=" & dtSeq2 & "&dfSeq3=" & dtSeq3 & "&dfSeq4=" & dtSeq4 & "&dfSeq5=" & dtSeq5 & "&dfLot=" & dtLot)
            If rsData <> "0" Then
                Return rsData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdatachildpartsummaryfgSpc = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function

    Public Shared Function mgroupDataWiSpc(WI As String, seq As String, dtLot As String)
        Try
            Dim requestFunction As New JObject()
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefectSpecial/groupDataWiSpc?dfWi=" & WI & "&dfseq=" & seq & "&dfLot=" & dtLot)
            If rsData <> "0" Then
                Return rsData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mgroupDataWiSpc = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function

    Public Shared Function Getdatachildpartsummaryfggrouppart(dtWino As String, dtSeq As String, dtLot As String, dfType As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/Getdatachildpartsummaryfggrouppart?dfWi=" & dtWino & "&dfSeq=" & dtSeq & "&dfLot=" & dtLot & "&dfType=" & dfType)
            If rsData <> "0" Then
                Return rsData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdatachildpartsummary = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function
    Public Shared Function mGetdatachildpartsummarychildgrouppart(dtWino As String, dtSeq As String, dtLot As String, dfType As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/Getdatachildpartsummarychildgrouppart?dfWi=" & dtWino & "&dfSeq=" & dtSeq & "&dfLot=" & dtLot & "&dfType=" & dfType)
            If rsData <> "0" Then
                Return rsData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdatachildpartsummary = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function

    Public Shared Function GetdatachildpartsummarychildgrouppartAdminAdjust(dtWino As String, dtSeq As String, dtLot As String, dfType As String, itemCd As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetdatachildpartsummarychildgrouppartAdminAdjust?dfWi=" & dtWino & "&dfSeq=" & dtSeq & "&dfLot=" & dtLot & "&dfType=" & dfType & "&ItemCd=" & itemCd)
            If rsData <> "0" Then
                Return rsData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function GetdatachildpartsummarychildgrouppartAdminAdjust = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function
    Public Shared Function mGetdefectactual(LineCd As String, sDate As String, eDate As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDefectactualadmin?LineCd=" & LineCd & "&sDate=" & sDate & "&eDate=" & eDate)
            If rsData <> "0" Then
                Return rsData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetdefectactual = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function
    Public Shared Function mGetmasterDataDefect(def_cd As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getmasterDataDefect?def_cd=" & def_cd)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getmasterDataDefect?def_cd=" & def_cd)
            If rsData <> "0" Then
                Return rsData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetmasterDataDefect = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function
    Public Shared Function mGetDefectadmindetailncFG(LineCd As String, sDate As String, eDate As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDefectadmindetailncFG?LineCd=" & LineCd & "&sDate=" & sDate & "&eDate=" & eDate)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDefectadmindetailncFG?LineCd=" & LineCd & "&sDate=" & sDate & "&eDate=" & eDate)
            If rsData <> "0" Then
                Return rsData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetDefectadmindetailnc = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function
    Public Shared Function mGetDataAdminAdjust()

    End Function
    Public Shared Function mGetDefect(wi As String, lotNo As String, shift As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetDefect?wi=" & wi & "&lotNo=" & lotNo & "&shift=" & shift)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetDefect = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function
    Public Shared Function mGetPartBySelectDate(LineCd As String, sDate As String, Shift As String, Type As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetPartBySelectDate?LineCd=" & LineCd & "&sDate=" & sDate & "&Shift=" & Shift & "&Type=" & Type)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetPartBySelectDate?LineCd=" & LineCd & "&sDate=" & sDate & "&Shift=" & Shift & "&Type=" & Type)
            If rsData <> "0" Then
                Return rsData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetPartBySelectDate = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function
    Public Shared Function mGetDataAdminAdjustByWi(LineCd As String, sDate As String, Shift As String, wi As String, itemType As String, seqNo As String, Type As String)
        Try
            Dim api = New api()
            'MsgBox("LineCd = " & LineCd)
            'MsgBox("sDate = " & sDate)
            'MsgBox("Shift = " & Shift)
            'MsgBox("wi = " & wi)
            'MsgBox("itemType = " & itemType)
            'MsgBox("seqNo = " & seqNo)
            'MsgBox("Type = " & Type)
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetPartBySelectDateWi?LineCd=" & LineCd & "&sDate=" & sDate & "&Shift=" & Shift & "&Wi=" & wi & "&itemType=" & itemType & "&seqNo=" & seqNo & "&Type=" & Type)
            ' MsgBox(rsData)
            If rsData <> "0" Then
                Return rsData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetPartBySelectDate = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function
    Public Shared Function mGetDefectadmindetailng(LineCd As String, sDate As String, eDate As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDefectadmindetailngFG?LineCd=" & LineCd & "&sDate=" & sDate & "&eDate=" & eDate)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDefectadmindetailngFG?LineCd=" & LineCd & "&sDate=" & sDate & "&eDate=" & eDate)
            If rsData <> "0" Then
                Return rsData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function getDefectadmindetailng = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function
    Public Shared Function mGetQtyofdefectcode(dtWino As String, dtLotNo As String, dtSeqno As String, dtType As String, dtCode As String, itemCd As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getQtyofdefectcode?dtWino=" & dtWino & "&dtLotNo=" & dtLotNo & "&dtSeqno=" & dtSeqno & "&dtType=" & dtType & "&dtCode=" & dtCode & "&ItemCd=" & itemCd)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetQtyofdefectcode = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mInserttagdefect(dti_wi_no As String, dti_line_cd As String, dti_item_cd As String, dti_item_type As String, dti_lot_no As String, dti_seq_no As String, dti_type As String, dti_sum_qty As String, dti_menu As String, dti_box_no As String, dti_info_qr_cd As String, dti_defect_qr_cd As String, dti_status_flg As String, dti_created_date As String, dti_created_by As String, dti_updated_date As String, dti_updated_by As String, pwi_id As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/inserttagdefect?dti_wi_no=" & dti_wi_no & "&dti_line_cd=" & dti_line_cd & "&dti_item_cd=" & dti_item_cd & "&dti_item_type=" & dti_item_type & "&dti_lot_no=" & dti_lot_no & "&dti_seq_no=" & dti_seq_no & "&dti_type=" & dti_type & "&dti_sum_qty=" & dti_sum_qty & "&dti_menu=" & dti_menu & "&dti_box_no=" & dti_box_no & "&dti_info_qr_cd=" & dti_info_qr_cd & "&dti_defect_qr_cd=" & dti_defect_qr_cd & "&dti_status_flg=" & dti_status_flg & "&dti_created_date=" & dti_created_date & "&dti_created_by=" & dti_created_by & "&dti_updated_date=" & dti_updated_date & "&dti_updated_by=" & dti_updated_by & "&pwi_id=" & pwi_id)
            Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/inserttagdefect?dti_wi_no=" & dti_wi_no & "&dti_line_cd=" & dti_line_cd & "&dti_item_cd=" & dti_item_cd & "&dti_item_type=" & dti_item_type & "&dti_lot_no=" & dti_lot_no & "&dti_seq_no=" & dti_seq_no & "&dti_type=" & dti_type & "&dti_sum_qty=" & dti_sum_qty & "&dti_menu=" & dti_menu & "&dti_box_no=" & dti_box_no & "&dti_info_qr_cd=" & dti_info_qr_cd & "&dti_defect_qr_cd=" & dti_defect_qr_cd & "&dti_status_flg=" & dti_status_flg & "&dti_created_date=" & dti_created_date & "&dti_created_by=" & dti_created_by & "&dti_updated_date=" & dti_updated_date & "&dti_updated_by=" & dti_updated_by & "&pwi_id=" & pwi_id)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mInserttagdefect = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Async Function mTrasferInserttagdefect(dti_wi_no As String, dti_line_cd As String, dti_item_cd As String, dti_item_type As String, dti_lot_no As String, dti_seq_no As String, dti_type As String, dti_sum_qty As String, dti_menu As String, dti_box_no As String, dti_info_qr_cd As String, dti_defect_qr_cd As String, dti_status_flg As String, dti_created_date As String, dti_created_by As String, dti_updated_date As String, dti_updated_by As String, pwi_id As String) As Task(Of String)
        Try
            Dim api = New api()
            ' Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectTransctionSupplier?lastId=" & lastId & "&dt_supplier_code=" & dt_supplier_code & "&dt_qty=" & dt_qty & "&Line=" & Line)
            ' Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/insertDefectTransctionSupplier?lastId=" & lastId & "&dt_supplier_code=" & dt_supplier_code & "&dt_qty=" & dt_qty & "&Line=" & Line)
            ' Return rsData
            Try
                Dim url As String = "http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/insertDatadefect/inserttagdefect?dti_wi_no=" & dti_wi_no & "&dti_line_cd=" & dti_line_cd & "&dti_item_cd=" & dti_item_cd & "&dti_item_type=" & dti_item_type & "&dti_lot_no=" & dti_lot_no & "&dti_seq_no=" & dti_seq_no & "&dti_type=" & dti_type & "&dti_sum_qty=" & dti_sum_qty & "&dti_menu=" & dti_menu & "&dti_box_no=" & dti_box_no & "&dti_info_qr_cd=" & dti_info_qr_cd & "&dti_defect_qr_cd=" & dti_defect_qr_cd & "&dti_status_flg=" & dti_status_flg & "&dti_created_date=" & dti_created_date & "&dti_created_by=" & dti_created_by & "&dti_updated_date=" & dti_updated_date & "&dti_updated_by=" & dti_updated_by & "&pwi_id=" & pwi_id
                Console.WriteLine("Calling API URL: " & url)
                ' ✅ แปลงให้ async โดยรันบน background thread
                Dim rsData As String = Await Task.Run(Function() api.Load_data(url))
                Return rsData
            Catch ex As Exception
                MsgBox("❗ connect Api Fail in mInserttagdefect = " & ex.Message)
                Return "0"
            End Try
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mInserttagdefect = " & ex.Message)
            Return "0"
        End Try
    End Function
    Public Shared Function mGetDatasys_exp_defect_mst(def_cd As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetDatasys_exp_defect_mst?def_cd=" & def_cd)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetDatasys_exp_defect_mst = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mGetCalPartOEE(line_cd As String, mainPartNo As String, CPPartNo As String, type_defect As String, special_flg As String, PD As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetCalPartOEE?line_cd=" & line_cd & "&mainPartNo=" & mainPartNo & "&CPPartNo=" & CPPartNo & "&type_defect=" & type_defect & "&special_flg=" & special_flg & "&PD=" & PD)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetCalPartOEE?line_cd=" & line_cd & "&mainPartNo=" & mainPartNo & "&CPPartNo" & "&CPPartNo=" & CPPartNo & "&type_defect=" & type_defect & "&special_flg=" & special_flg & "&PD=" & PD)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mGetCalPartOEE = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function LoadDataTagDefect(df_type As String, df_wi As String, DateStart As String, DateEnd As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/GetGroupDataTagDefect?df_type=" & df_type & "&df_wi=" & df_wi & "&DateStart=" & DateStart & "&DateEnd=" & DateEnd)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function LoadDataTagDefect = " & ex.Message)
            Return False
        End Try
        Return 1
    End Function
End Class