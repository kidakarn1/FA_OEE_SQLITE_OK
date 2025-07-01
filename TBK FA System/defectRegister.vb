Imports System.Web.Script.Serialization

Public Class defectRegister
    Shared Type As Integer = 0
    Shared dSelectcode As New defectSelectcode()
    Shared dSelecttype As New defectSelecttype()
    Public Shared GdfRegister
    Public Shared dfQty As Integer = 0
    Public Shared pd As New Working_Pro()
    Public dtWino = pd.wiNo '"5100287204" 'pd.wi_no.Text
    Public dtLineno = pd.lineCd '"K1A003" 'pd.Label24.Text
    Public dtItemcd = dSelecttype.sPart '"ABC123" 'dSelecttype.sPart
    Public dtItemtype = dSelecttype.type '"1" 'dSelecttype.type
    Public dtLotno = pd.lotNo '"BJ28" 'pd.Label18.Text
    Public dtSeqno = pd.seqNo '"001" 'pd.Label22.Text
    Public dtpwi_id = pd.pwi_id '"001" 'pd.Label22.Text
    Public dtType = dSelecttype.dtType '"2" 'dSelecttype.dtType.Text
    Public dtCode = dSelectcode.sDefectcode '"009" 'dSelectcode.sDefectcode
    Public dtQty As Integer = 0
    Public actTotal = dSelecttype.actTotal
    Public ncTotal = dSelecttype.ncTotal
    Public ngTotal = dSelecttype.ngTotal
    Public Shared SeqSpc = "NO DATA"
    Public Shared PwiSpc
    Public Shared sPart
    Public Shared mainCP = ""
    Public Shared swi As String = "NO DATA"
    Public Shared source_cd_supplier As String = ""
    Private Shared _instance As defectRegister
    Public Sub defectRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setVariable()
    End Sub
    Public Sub setVariable()
        Dim dfHome As New defectHome()
        lbType.Text = dfHome.dtType
        lbPart.Text = dSelecttype.sPart
        sPart = lbPart.Text
        lbDefectcode.Text = dSelectcode.sDefectcode
        lbDefectdetail.Text = dSelectcode.sDefectdetail
        mainCP = dSelecttype.maincp
        If MainFrm.chk_spec_line = "2" Then
            dtWino = swi
            dtSeqno = SeqSpc
            dtpwi_id = PwiSpc
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        defectSelectcode.Show()
        Me.Close()
    End Sub

    Private Sub btnDecreasingnc_Click(sender As Object, e As EventArgs) Handles btnDecreasingnc.Click
        delRegisterNc(1)
    End Sub
    Private Sub btnPlusnc_Click(sender As Object, e As EventArgs) Handles btnPlusnc.Click
        plusRegisterNc(1)
    End Sub
    Public Sub delRegisterNc(number As Integer)
        If CDbl(Val(tbQtydefectnc.Text)) > 0 Then
            tbQtydefectnc.Text = CDbl(Val(tbQtydefectnc.Text)) - number
        End If
    End Sub
    Public Sub plusRegisterNc(number As Integer)
        If defectSelecttype.type = "1" Then
            Dim dfNumpadregister As New defectNumpadregister
            Dim maxQty As Integer = dfNumpadregister.calMaxqtyregister(actTotal, ncTotal, ngTotal)
            Dim rsCheck = dfNumpadregister.calNumpadregister((tbQtydefectnc.Text + 1), maxQty)
            If rsCheck Then
                tbQtydefectnc.Text = CDbl(Val(tbQtydefectnc.Text)) + number
            Else
                MsgBox("Please Check QTY Input")
            End If
        Else
            Dim dfNumpadregister As New defectNumpadregister
            Dim md = New modelDefect()
            'Dim UseQty = md.mGetdefectdetailncPartno(dtWino, dtSeqno, dtLotno, dtType, lbPart.Text)
            Dim UseQty = md.mGetdefectdetailPartno(dtWino, dtSeqno, dtLotno, dtType, lbPart.Text)
            Dim maxQty As Integer = (999 - Convert.ToInt32(UseQty))
            Dim rsCheck = dfNumpadregister.calNumpadregister((tbQtydefectnc.Text + 1), maxQty)
            If rsCheck Then
                If CDbl(Val(Working_Pro.LB_COUNTER_SEQ.Text)) > 0 Then
                    tbQtydefectnc.Text = CDbl(Val(tbQtydefectnc.Text)) + number
                Else
                    MsgBox("Please Input Actaual QTY")
                End If
            Else
                MsgBox("Please Check QTY Input")
            End If
            'tbQtydefectnc.Text = CDbl(Val(tbQtydefectnc.Text)) + number
        End If

    End Sub
    Private Sub tbQtydefectnc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbQtydefectnc.Click, Panel1.Click
        ' Dim dfNumpadregister As New defectNumpadregister
        'dfNumpadregister.Show()
        dfQty = tbQtydefectnc.Text
        defectNumpadregister.Show()
        Me.Close()
    End Sub
    Private Sub oK_Click(sender As Object, e As EventArgs) Handles oK.Click
        If tbQtydefectnc.Text <> "0" Then
            If defectSelecttype.type = "1" Then
                If Working_Pro.slm_flg_qr_prod = "1" Then ' scan QR 
                    Working_Pro.RemainScanDmcDefect = tbQtydefectnc.Text
                    ScanQRprod.ManageQrScanFA(4, tbQtydefectnc.Text)
                    ScanQRprod.Show()
                Else ' case Normal
                    CalFG()
                End If
            Else
                CalChildPart()
            End If
        Else
            MsgBox("Please Check QTY.")
        End If
    End Sub
    Public Shared ReadOnly Property Instance As defectRegister
        Get
            If _instance Is Nothing Then
                _instance = New defectRegister()
            End If
            Return _instance
        End Get
    End Property

    Public Sub CalFG()
        mainCP = modelDefect.mGetCalPartOEE(MainFrm.Label4.Text, dtItemcd, dtItemcd, defectSelecttype.type, MainFrm.chk_spec_line, MainFrm.Label6.Text)
        'mainCP = "1"
        If tbQtydefectnc.Text < 0 Then
            tbQtydefectnc.Text = 0
        End If
        If tbQtydefectnc.Text = "" Then
            tbQtydefectnc.Text = 0
        End If
        dtQty = tbQtydefectnc.Text
        Dim dtActualdate = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
        Dim pwi_id As String = ""
        If MainFrm.chk_spec_line = "2" Then
            pwi_id = PwiSpc
        Else
            pwi_id = Working_Pro.pwi_id
        End If
        Dim rs = insertDefectregister(dtWino, dtLineno, dtItemcd, dtItemtype, dtLotno, dtSeqno, dtType, dtCode, dtQty, "1", dtActualdate, pwi_id, dSelectcode.sDefectdetail, mainCP, source_cd_supplier, defectHome.leaderConfrime)
        Dim dataQty
        If rs Then
            If dtType = "1" Then
                dataQty = calQtytotalncregisterNG(tbQtydefectnc.Text, actTotal, ncTotal, ngTotal)
                Working_Pro.lb_ng_qty.Text = dataQty
            ElseIf dtType = "2" Then
                dataQty = calQtytotalncregister(tbQtydefectnc.Text, actTotal, ncTotal, ngTotal)
                Working_Pro.lb_nc_qty.Text = dataQty
            End If
            Dim dfAll = CDbl(Val(Working_Pro.lb_ng_qty.Text)) + CDbl(Val(Working_Pro.lb_nc_qty.Text))
            Dim OEE As New OEE_NODE
            Working_Pro.set_AccTarget(Prd_detail.Label12.Text.Substring(3, 5), Working_Pro.Label38.Text, Working_Pro.gobal_stTimeModel)
            Working_Pro.setlvA(Working_Pro.Label24.Text, Working_Pro.Label18.Text, Working_Pro.Label14.Text, DateTime.Now.ToString("yyyy-MM-dd"), Prd_detail.Label12.Text.Substring(3, 5), Working_Pro.gobal_stTimeModel, MainFrm.chk_spec_line)
            Working_Pro.setlvQ(Working_Pro.Label24.Text, Working_Pro.Label18.Text, Prd_detail.Label12.Text.Substring(3, 5), Working_Pro.gobal_stTimeModel)
            Dim P = Working_Pro.setgetSpeedLoss(Working_Pro.lbOverTimeQuality.Text, Working_Pro.lb_good.Text, Prd_detail.Label12.Text.Substring(3, 5), Working_Pro.Label38.Text, Working_Pro.Label24.Text, Working_Pro.gobal_stTimeModel)
            Dim GoodByPartNo As Integer = CDbl(Val(Working_Pro.actualP.Text)) - CDbl(Val(Working_Pro.lbOverTimeQuality.Text))
            Dim Q = Working_Pro.cal_progressbarQ(Working_Pro.lbOverTimeQuality.Text, GoodByPartNo)
            Dim A = Working_Pro.cal_progressbarA(Working_Pro.Label24.Text, Prd_detail.Label12.Text.Substring(3, 5), Prd_detail.Label12.Text.Substring(11, 5))
            Working_Pro.setNgByHour(Working_Pro.Label24.Text, Working_Pro.Label18.Text)
            'Dim rswebview = loadDataProgressBar(Label24.Text, Label14.Text)
            ' WebViewProgressbar.Reload()
            Working_Pro.calProgressOEE(A, Q, P)
            Working_Pro.cal_eff()
            Dim SQLite = New ModelSqliteDefect
            Working_Pro.lb_good.Text = CDbl(Val(Working_Pro.lb_good.Text)) - CDbl(Val(tbQtydefectnc.Text))
            Working_Pro.Enabled = True
            Working_Pro.ResetRed()
            Dim rslvQ = SQLite.mSqliteGetDataQualityOverAllNG(dtLineno, dtLotno, Working_Pro.DateTimeStartofShift.Text)
            If rslvQ <> "0" Then
                Dim dict3 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rslvQ)
                Try
                    For Each item As Object In dict3
                        Working_Pro.lbNG.Text = item("AllDefect").ToString()
                    Next
                Catch ex As Exception

                End Try
            End If
            Me.Close()
        End If
    End Sub
    Public Sub CalChildPart()
        If tbQtydefectnc.Text < 0 Then
            tbQtydefectnc.Text = 0
        End If
        If tbQtydefectnc.Text = "" Then
            tbQtydefectnc.Text = 0
        End If
        dtQty = tbQtydefectnc.Text
        Dim dtActualdate = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
        If MainFrm.chk_spec_line = "2" Then
            pwi_id = PwiSpc
        Else
            pwi_id = Working_Pro.pwi_id
        End If
        Dim rs = insertDefectregister(dtWino, dtLineno, dtItemcd, dtItemtype, dtLotno, dtSeqno, dtType, dtCode, dtQty, "1", dtActualdate, pwi_id, dSelectcode.sDefectdetail, mainCP, source_cd_supplier, defectHome.leaderConfrime)
        If rs Then
            If dtType = "1" Then
                Dim dataQty = calQtytotalncregisterNGChildPart(tbQtydefectnc.Text, actTotal, Working_Pro.lb_nc_child_part.Text, Working_Pro.lb_ng_child_part.Text)
                Working_Pro.lb_ng_child_part.Text = dataQty
            ElseIf dtType = "2" Then
                Dim dataQty = calQtytotalncregisterChildPart(tbQtydefectnc.Text, actTotal, Working_Pro.lb_nc_child_part.Text, Working_Pro.lb_ng_child_part.Text)
                Working_Pro.lb_nc_child_part.Text = dataQty
            End If
            Working_Pro.Enabled = True
            Working_Pro.cal_eff()
            Working_Pro.ResetRed()
            Dim SQLite = New ModelSqliteDefect
            Dim rslvQ = SQLite.mSqliteGetDataQualityOverAllNG(dtLineno, dtLotno, Working_Pro.DateTimeStartofShift.Text)
            If rslvQ <> "0" Then
                Dim dict3 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rslvQ)
                Try
                    For Each item As Object In dict3
                        Working_Pro.lbNG.Text = item("AllDefect").ToString()
                    Next
                Catch ex As Exception

                End Try
            End If
            Me.Close()
        End If
    End Sub

    Public Shared Function calQtytotalncregisterChildPart(tbQtydefectnc As Integer, actTotal As Integer, ncTotal As Integer, ngTotal As Integer)
        Dim setNc = ncTotal + tbQtydefectnc
        Return setNc
    End Function
    Public Shared Function calQtytotalncregisterNGChildPart(tbQtydefectnc As Integer, actTotal As Integer, ncTotal As Integer, ngTotal As Integer)
        Dim setNg = ngTotal + tbQtydefectnc
        Return setNg
    End Function
    Public Shared Function calQtytotalncregister(tbQtydefectnc As Integer, actTotal As Integer, ncTotal As Integer, ngTotal As Integer)
        Dim setNc = ncTotal + tbQtydefectnc
        Return setNc
    End Function
    Public Shared Function calQtytotalncregisterNG(tbQtydefectnc As Integer, actTotal As Integer, ncTotal As Integer, ngTotal As Integer)
        Dim setNg = ngTotal + tbQtydefectnc
        Return setNg
    End Function
    Public Function insertDefectregister(dtWino As String, dtLineno As String, dtItemcd As String, dtItemtype As String, dtLotno As String, dtSeqno As String, dtType As String, dtCode As String, dtQty As String, dtMenu As String, dtActualdate As String, pwi_id As String, def_name As String, mainCP As String, source_cd_supplier As String, leaderConfrime As String)
        Try
            Dim mdDefect = New modelDefect()
            Dim mdSQLiteDefect = New ModelSqliteDefect()
            Dim rsDataSQLite = mdSQLiteDefect.mSqliteInsertDefectTransection(dtWino, dtLineno, dtItemcd, dtItemtype, dtLotno, dtSeqno, dtType, dtCode, dtQty, dtMenu, dtActualdate, pwi_id, def_name, mainCP, source_cd_supplier, leaderConfrime)
            'Dim rsData = mdDefect.mInsertdefectregister(dtWino, dtLineno, dtItemcd, dtItemtype, dtLotno, dtSeqno, dtType, dtCode, dtQty, dtMenu, dtActualdate, pwi_id)
            'If rsData Then
            If rsDataSQLite Then
                Return True
            Else
                'MsgBox("insertDefectregister FAILL Please Check rsData=" & rsData)
                MsgBox("insertDefectregister FAILL Please Check rsData=" & rsDataSQLite)
                Return False
            End If
        Catch ex As Exception
            MsgBox("insertDefectregister FAILL Please Check" & ex.Message)
            Return False
        End Try
    End Function
End Class