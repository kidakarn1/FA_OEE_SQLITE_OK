Public Class defectAdminregister
    Shared Type As Integer = 0
    Shared dfAdminselectcode As New defectAdminselectcodenc()
    Shared dfAdminselecttype As New defectAdminselecttypenc()
    Public Shared dfQty As Integer = 0
    Public Shared dfAdmindetailnc As New defectAdmindetailnc()
    Public dtWino = dfAdmindetailnc.sWi
    Public dtLineno = MainFrm.Label4.Text
    Public dtItemcd = dfAdminselecttype.sPart
    Public dtItemtype = dfAdminselecttype.type
    Public dtLotno = dfAdmindetailnc.sLot
    Public dtSeqno = dfAdmindetailnc.dSeq
    Public dtType = dfAdminselecttype.dtType
    Public dtCode = dfAdminselectcode.sDefectcode
    Public dtQty As Integer = 0
    Public Shared tbQty As Integer = 0
    Public actTotal = dfAdminselecttype.actTotal
    Public ncTotal = dfAdminselecttype.ncTotal
    Public ngTotal = dfAdminselecttype.ngTotal
    Public Shared swi = ""
    Public Shared dtShift
    Public Shared sPart
    Public Shared GmaxQty As Integer = 0
    Public Shared source_cd_supplier
    Public Shared SeqSpc = defectAdminregister.dtSeqno
    Public Shared PwiSpc
    Public Shared mainCP
    Private Sub defectRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setVariable()
        Dim dfType
    End Sub
    Public Sub setVariable()
        Dim dfAdminhome As New defectAdminhome
        If dfAdminhome.dfType = "NC" Then
            dtWino = dfAdmindetailnc.sWi
            dtShift = dfAdmindetailnc.sshift
            dtLineno = MainFrm.Label4.Text
            dtItemcd = dfAdminselecttype.sPart
            dtItemtype = dfAdminselecttype.type
            dtLotno = dfAdmindetailnc.sLot
            dtSeqno = dfAdmindetailnc.dSeq
            dtType = dfAdminselecttype.dtType
            dtCode = dfAdminselectcode.sDefectcode
            dtQty = 0
            tbQty = 0
            actTotal = dfAdminselecttype.actTotal
            ncTotal = dfAdminselecttype.ncTotal
            ngTotal = dfAdminselecttype.ngTotal
            lbType.Text = "NC"
            lbPart.Text = dfAdminselecttype.sPart
            sPart = lbPart.Text
            lbDefectcode.Text = dfAdminselectcode.sDefectcode
            lbDefectdetail.Text = dfAdminselectcode.sDefectdetail
        ElseIf dfAdminhome.dfType = "NG" Then
            Dim dfAdminselectcodeng As New defectAdminselectcodeng()
            Dim dfAdminselecttypeng As New defectAdminselecttypeng()
            Dim dfAdminng As New defectAdmindetailng
            dtWino = dfAdminng.sWi
            dtLineno = MainFrm.Label4.Text
            dtItemcd = dfAdminselecttypeng.sPart
            dtShift = dfAdminng.sshift
            dtItemtype = dfAdminselecttypeng.type
            dtLotno = dfAdminng.sLot
            dtSeqno = dfAdminng.dSeq
            dtType = dfAdminselecttypeng.dtType
            dtCode = dfAdminselectcodeng.sDefectcode
            dtQty = 0
            tbQty = 0
            actTotal = dfAdminselecttypeng.actTotal
            ncTotal = dfAdminselecttypeng.ncTotal
            ngTotal = dfAdminselecttypeng.ngTotal
            lbType.Text = "NG"
            lbPart.Text = dfAdminselecttypeng.sPart
            sPart = lbPart.Text
            lbDefectcode.Text = dfAdminselectcodeng.sDefectcode
            lbDefectdetail.Text = dfAdminselectcodeng.sDefectdetail
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dfAdminhome As New defectAdminhome
        If dfAdminhome.dfType = "NC" Then
            defectAdminselectcodenc.Show()
        ElseIf dfAdminhome.dfType = "NG" Then
            defectAdminselectcodeng.Show()
        End If
        Me.Close()
    End Sub
    Private Sub btnDecreasingnc_Click(sender As Object, e As EventArgs) Handles btnDecreasingnc.Click
        Dim dfAdminhome As New defectAdminhome
        If dfAdminhome.dfType = "NC" Then
            delRegisterNc(1)
        ElseIf dfAdminhome.dfType = "NG" Then
            delRegisterNg(1)
        End If

    End Sub
    Private Sub btnPlusnc_Click(sender As Object, e As EventArgs) Handles btnPlusnc.Click
        plusRegisterNc(1)
    End Sub
    Public Sub delRegisterNc(number As Integer)
        If CDbl(Val(lbQtydefect.Text)) > 0 Then
            lbQtydefect.Text = CDbl(Val(lbQtydefect.Text)) - number
        End If
    End Sub
    Public Sub delRegisterNg(number As Integer)
        If CDbl(Val(lbQtydefect.Text)) > 0 Then
            lbQtydefect.Text = CDbl(Val(lbQtydefect.Text)) - number
        End If
    End Sub
    Public Sub plusRegisterNc(number As Integer)
        Dim dfNumpadregister As New defecAdmintnumpadregister
        Dim maxQty As Integer
        Dim dfAdminhome As New defectAdminhome
        If dfAdminhome.dfType = "NC" Then
            If defectAdminselecttypenc.type = "1" Then
                maxQty = dfNumpadregister.calMaxqtyregisternc(actTotal, ncTotal, ngTotal)
                GmaxQty = maxQty
            Else
                Dim md = New modelDefect
                'Dim UseQty = md.mGetdefectdetailncPartno(dtWino, dtSeqno, dtLotno, dtType, dtItemcd)
                Dim UseQty = md.mGetdefectdetailPartno(dtWino, dtSeqno, dtLotno, dtType, dtItemcd)
                maxQty = (999 - Convert.ToInt32(UseQty))
                maxQty = maxQty
                GmaxQty = maxQty
            End If
        Else
            If defectAdminselecttypeng.type = "1" Then
                maxQty = dfNumpadregister.calMaxqtyregisterng(actTotal, ncTotal, ngTotal)
                GmaxQty = maxQty
            Else
                Dim md = New modelDefect
                '  Dim UseQty = md.mGetdefectdetailncPartno(dtWino, dtSeqno, dtLotno, dtType, dtItemcd)
                Dim UseQty = md.mGetdefectdetailPartno(dtWino, dtSeqno, dtLotno, dtType, dtItemcd)
                maxQty = (999 - Convert.ToInt32(UseQty))
                maxQty = maxQty
                GmaxQty = maxQty
            End If
        End If

        Dim rsCheck = dfNumpadregister.calNumpadregister((lbQtydefect.Text + 1), maxQty)
        If rsCheck Then
            lbQtydefect.Text = CDbl(Val(lbQtydefect.Text)) + number
        Else
            MsgBox("Please Check QTY Input")
        End If
    End Sub

    Public Sub plusRegisterNg(number As Integer)
        Dim dfNumpadregister As New defecAdmintnumpadregister
        Dim maxQty As Integer = dfNumpadregister.calMaxqtyregisternc(actTotal, ncTotal, ngTotal)
        GmaxQty = maxQty
        Dim rsCheck = dfNumpadregister.calNumpadregister((lbQtydefect.Text + 1), maxQty)
        If rsCheck Then
            lbQtydefect.Text = CDbl(Val(lbQtydefect.Text)) + number
        Else
            MsgBox("Please Check QTY Input")
        End If
    End Sub
    Private Sub tbQtydefectnc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbQtydefect.Click, Panel1.Click
        dfQty = lbQtydefect.Text
        ' Dim dfAdminnumpadregister = New defecAdmintnumpadregister
        defecAdmintnumpadregister.Show()
        Me.Close()
    End Sub
    Private Sub oK_Click(sender As Object, e As EventArgs) Handles oK.Click
        If CDbl(Val(lbQtydefect.Text)) > 0 Then
            tbQty = lbQtydefect.Text
            Dim dfAlert As New defectAlertsuredefect
            dfAlert.Show()
        Else
            MsgBox("Please Check QTY Input")
        End If
    End Sub
    Public Function updateDefectdata(dtWino As String, dtLotNo As String, dtSeqno As String, dtType As String, dtCode As String, ItemCd As String, dtItemtype As String)
        Dim md As New modelDefect()
        Dim rs = md.mUpdatedefectactualAdmin(dtWino, dtLotNo, dtSeqno, dtType, dtCode, dtItemtype, ItemCd)
        If rs Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function calQtytotalncregisternc(tbQtydefect As Integer, actTotal As Integer, ncTotal As Integer, ngTotal As Integer)
        Dim setNc = ncTotal + tbQtydefect
        Return setNc
    End Function
    Public Shared Function calQtytotalncregisterng(tbQtydefect As Integer, actTotal As Integer, ncTotal As Integer, ngTotal As Integer)
        Dim setNc = ngTotal + tbQtydefect
        Return setNc
    End Function
    Public Function insertDefectregister(dtWino As String, dtLineno As String, dtItemcd As String, dtItemtype As String, dtLotno As String, dtSeqno As String, dtType As String, dtCode As String, dtQty As String, dtMenu As String, dtActualdate As String, Apwi_id As String)
        Try
            Dim mdDefect = New modelDefect()
            Dim mdDefectSqlite = New ModelSqliteDefect()
            ' Dim rsData = mdDefect.mInsertdefectregister(dtWino, dtLineno, dtItemcd, dtItemtype, dtLotno, dtSeqno, dtType, dtCode, dtQty, dtMenu, dtActualdate, Apwi_id)
            Dim name_en As String = mdDefect.mGetmasterDataDefect(dtCode)
            Dim rsData = mdDefectSqlite.mSqliteInsertDefectTransection(dtWino, dtLineno, dtItemcd, dtItemtype, dtLotno, dtSeqno, dtType, dtCode, dtQty, dtMenu, dtActualdate, Apwi_id, name_en, mainCP, source_cd_supplier)
            mdDefectSqlite.UpdateStatusCloselotSqlite("1", Apwi_id)
            If rsData Then
                Return True
            Else
                MsgBox("insertDefectregister FAILL Please Check rsData=" & rsData)
                Return False
            End If
        Catch ex As Exception
            MsgBox("insertDefectregister FAILL Please Check" & ex.Message)
            Return False
        End Try
    End Function
End Class