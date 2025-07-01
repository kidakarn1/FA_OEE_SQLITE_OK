Public Class defectNumpadadjust
    Shared dfDetailsnc As New defectDetailnc()
    Shared dfDetailsng As New defectDetailng()
    Public Shared GdfNumpadafjust
    Shared maxQty As Integer = 0
    Shared sPart As String = ""
    Shared actQty As Integer = 0
    Public Shared dtWino
    Shared dtItemcd
    Shared dtItemtype
    Shared dtLotNo
    Shared dtSeqno
    Shared dtType
    Shared dtCode
    Shared dtQty
    Shared dtMenu
    Shared dtActualdate
    Shared pd As New Confrime_work_production()
    Shared nc As Integer = Working_Pro.lb_nc_qty.Text
    Shared ng As Integer = Working_Pro.lb_ng_qty.Text
    Shared sNc
    Shared sNg
    Shared mainCP
    Shared dtpwi_id_spc
    Shared source_code_supplier As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnNumber1.Click
        tbAddjust.Text = tbAddjust.Text + "1"
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnNumber2.Click
        tbAddjust.Text = tbAddjust.Text + "2"
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnNumber3.Click
        tbAddjust.Text = tbAddjust.Text + "3"
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnNumber4.Click
        tbAddjust.Text = tbAddjust.Text + "4"
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btnNumber5.Click
        tbAddjust.Text = tbAddjust.Text + "5"
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles btnNumber6.Click
        tbAddjust.Text = tbAddjust.Text + "6"
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnNumber7.Click
        tbAddjust.Text = tbAddjust.Text + "7"
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles btnNumber8.Click
        tbAddjust.Text = tbAddjust.Text + "8"
    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles btnNumber9.Click
        tbAddjust.Text = tbAddjust.Text + "9"
    End Sub
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles btnNumber0.Click
        tbAddjust.Text = tbAddjust.Text + "0"
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        tbAddjust.Clear()
        tbAddjust.Text = "0"
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        'Dim objDefectdetailnc As New defectDetailnc()
        Dim dfHome As defectHome
        If dfHome.dtType = "NC" Then
            defectDetailnc.Show()
        ElseIf dfHome.dtType = "NG" Then
            defectDetailng.Show()
        End If
        Me.Close()
    End Sub
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim txt_lenght As Integer = tbAddjust.Text.Length
        Try
            tbAddjust.Text = tbAddjust.Text.Substring(0, txt_lenght - 1)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub setVariable()
        Dim pd As New Working_Pro()
        Dim dfHome As defectHome
        If dfHome.dtType = "NC" Then
            Dim objDefectdetailnc As New defectDetailnc
            sPart = dfDetailsnc.dtItemcd '"J107-11820-RM" 'pd.pFg
            dtWino = objDefectdetailnc.dtWino
            source_code_supplier = objDefectdetailnc.source_code_supplier
            'MsgBox("NC source_code_supplier ===>" & source_code_supplier)
            If MainFrm.chk_spec_line = "2" Then
                dtLineno = MainFrm.Label4.Text
            Else
                dtLineno = objDefectdetailnc.dtLineno
            End If
            dtItemcd = objDefectdetailnc.dtItemcd
            dtItemtype = objDefectdetailnc.dtItemtype
            dtLotNo = objDefectdetailnc.dtLotNo
            dtSeqno = objDefectdetailnc.dtSeqno
            dtType = objDefectdetailnc.dtType
            dtCode = objDefectdetailnc.dtCode
            dtQty = objDefectdetailnc.dtQty
            dtMenu = objDefectdetailnc.dtMenu
            dtActualdate = objDefectdetailnc.dtActualdate
            sNc = objDefectdetailnc.sNc
            If defectDetailnc.Types = "FG" Then
                actQty = Working_Pro.LB_COUNTER_SEQ.Text
            Else
                actQty = CDbl(Val(sNc))
            End If
        ElseIf dfHome.dtType = "NG" Then
            Dim objDefectdetailng As New defectDetailng
            sPart = dfDetailsng.dtItemcd '"J107-11820-RM" 'pd.pFg
            dtWino = objDefectdetailng.dtWino
            source_code_supplier = objDefectdetailng.source_code_supplier
            If MainFrm.chk_spec_line = "2" Then
                dtLineno = MainFrm.Label4.Text
            Else
                dtLineno = objDefectdetailng.dtLineno
            End If
            dtItemcd = objDefectdetailng.dtItemcd
            If objDefectdetailng.dtnameItemtype = "FG" Then
                dtItemtype = "1"
            ElseIf objDefectdetailng.dtnameItemtype = "CP" Then
                dtItemtype = "2"
            End If
            dtLotNo = objDefectdetailng.dtLotNo
            dtSeqno = objDefectdetailng.dtSeqno
            dtType = objDefectdetailng.dtType
            dtCode = objDefectdetailng.dtCode
            dtQty = objDefectdetailng.dtQty
            dtMenu = objDefectdetailng.dtMenu
            dtActualdate = objDefectdetailng.dtActualdate
            sNg = objDefectdetailng.sNg
            If defectDetailng.types = "FG" Then
                actQty = Working_Pro.LB_COUNTER_SEQ.Text
            Else
                actQty = CDbl(Val(sNg))
            End If
        End If
        nc = Working_Pro.lb_nc_qty.Text
        ng = Working_Pro.lb_ng_qty.Text
    End Sub

    Private Sub defectNumpadadjust_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setVariable()
        lbAct.Text = actQty
        lbPart.Text = sPart
        Dim dfHome As defectHome
        Try
            lbMax.Text = ""
            If dfHome.dtType = "NC" Then
                mainCP = dfDetailsnc.mainCP
                If defectDetailnc.Types = "FG" Then
                    lbMax.Text = calNumpadadjustNc(actQty, nc, ng, sNc)
                Else
                    Dim md = New modelDefect
                    Dim mdSqlite = New ModelSqliteDefect

                    Dim UseQty = ""
                    If MainFrm.chk_spec_line = "2" Then
                        Dim arrayWI As List(Of String) = New List(Of String)
                        For Each itemPlanData As DataPlan In MainFrm.ArrayDataPlan
                            arrayWI.Add(itemPlanData.wi)
                        Next
                        Dim GenSEQ As Integer = Working_Pro.Label22.Text - MainFrm.ArrayDataPlan.ToArray.Length
                        Dim Iseq = GenSEQ
                        UseQty = mdSqlite.mSqliteGetdefectdetailncPartnoSpc(arrayWI.ToArray, MainFrm.ArrayDataPlan.ToArray.Length, Working_Pro.Label18.Text, "2", lbPart.Text, dtWino, dtSeqno) ' md.mGetdefectdetailncPartnoSpc(arrayWI.ToArray, MainFrm.ArrayDataPlan.ToArray.Length, Working_Pro.Label18.Text, "2", lbPart.Text, dtWino, dtSeqno)
                    Else
                        '  UseQty = md.mGetdefectdetailncPartno(Working_Pro.wi_no.Text, Working_Pro.Label22.Text, Working_Pro.Label18.Text, "2", lbPart.Text)
                        UseQty = mdSqlite.mSqliteGetdefectdetailncPartno(Working_Pro.wi_no.Text, Working_Pro.Label22.Text, Working_Pro.Label18.Text, "2", lbPart.Text)
                    End If

                    Dim maxQty As Integer = (999 - Convert.ToInt32(UseQty)) + sNc

                    lbMax.Text = maxQty '"Unlimited"
                End If
            ElseIf dfHome.dtType = "NG" Then
                Dim mdSqlite = New ModelSqliteDefect
                mainCP = dfDetailsng.mainCP
                If defectDetailng.types = "FG" Then
                    lbMax.Text = calNumpadadjustNg(actQty, nc, ng, sNg)
                Else
                    Dim md = New modelDefect
                    Dim UseQty
                    If MainFrm.chk_spec_line = "2" Then
                        Dim arrayWI As List(Of String) = New List(Of String)
                        For Each itemPlanData As DataPlan In MainFrm.ArrayDataPlan
                            arrayWI.Add(itemPlanData.wi)
                        Next
                        Dim GenSEQ As Integer = Working_Pro.Label22.Text - MainFrm.ArrayDataPlan.ToArray.Length
                        Dim Iseq = GenSEQ
                        UseQty = mdSqlite.mSqliteGetdefectdetailncPartnoSpc(arrayWI.ToArray, MainFrm.ArrayDataPlan.ToArray.Length, Working_Pro.Label18.Text, "1", lbPart.Text, dtWino, dtSeqno)
                    Else
                        ' UseQty = md.mGetdefectdetailncPartno(Working_Pro.wi_no.Text, Working_Pro.Label22.Text, Working_Pro.Label18.Text, "1", lbPart.Text)
                        UseQty = mdSqlite.mSqliteGetdefectdetailncPartno(Working_Pro.wi_no.Text, Working_Pro.Label22.Text, Working_Pro.Label18.Text, "1", lbPart.Text)
                    End If
                    Dim maxQty As Integer = (999 - Convert.ToInt32(UseQty)) + sNg
                    lbMax.Text = maxQty '"Unlimited"
                End If
            End If
        Catch ex As Exception
            MsgBox("Please Select Data")
        End Try
    End Sub
    Public Sub manageNc()
        If defectDetailnc.Types = "FG" Then
            Dim rs = ckInputqtyaddjust(tbAddjust.Text, lbMax.Text)
            If rs Then
                Dim dfHome As New defectHome
                Dim wi
                Dim lot
                Dim seq
                Dim dfType
                Dim dtCode
                Dim dfName
                If dfHome.dtType = "NC" Then
                    wi = dfDetailsnc.dtWino
                    lot = dfDetailsnc.dtLotNo
                    seq = dfDetailsnc.dtSeqno
                    dfType = dfDetailsnc.dtType
                    dtCode = dfDetailsnc.dtCode
                    dfName = dfDetailsnc.dtName
                    setValuenc(actQty, nc, ng, sNc, tbAddjust.Text)

                ElseIf dfHome.dtType = "NG" Then
                    wi = dfDetailsng.dtWino
                    lot = dfDetailsng.dtLotNo
                    seq = dfDetailsng.dtSeqno
                    dfType = dfDetailsng.dtType
                    dtCode = dfDetailsng.dtCode
                    dfName = dfDetailsng.dtName
                    setValueng(actQty, nc, ng, sNg, tbAddjust.Text)
                End If
                'MsgBox("wi = " & wi)
                'MsgBox("lot = " & lot)
                'MsgBox("seq = " & seq)
                'MsgBox("dfType = " & dfType)
                'MsgBox("dtCode = " & dtCode)

                updateAddjustqty(wi, lot, seq, dfType, dtCode, dfName, mainCP, source_code_supplier)
                Working_Pro.cal_eff()
                Working_Pro.ResetRed()
                Working_Pro.Enabled = True
                Me.Close()
            Else
                MsgBox("Please Check QTY.")
            End If
        Else ' Child Part
            Dim LdfDetailsnc As New defectDetailnc()
            Dim LdfDetailsng As New defectDetailng()
            Dim dfHome As New defectHome
            Dim wi
            Dim lot
            Dim seq
            Dim dfType
            Dim dtCode
            Dim dfName
            If dfHome.dtType = "NC" Then
                wi = LdfDetailsnc.dtWino
                lot = LdfDetailsnc.dtLotNo
                seq = LdfDetailsnc.dtSeqno
                dfType = LdfDetailsnc.dtType
                dtCode = LdfDetailsnc.dtCode
                dfName = LdfDetailsnc.dtName
                Dim rs = ckInputqtyaddjust(tbAddjust.Text, lbMax.Text)
                If rs Then
                    setValuenc(actQty, Working_Pro.lb_nc_child_part.Text, Working_Pro.lb_ng_child_part.Text, sNc, tbAddjust.Text)
                    updateAddjustqty(wi, lot, seq, dfType, dtCode, dfName, mainCP, source_code_supplier)
                    Working_Pro.cal_eff()
                    Working_Pro.ResetRed()
                    Working_Pro.Enabled = True
                    Me.Close()
                Else
                    MsgBox("Please Check QTY.")
                End If
                'MsgBox("-->")
                'MsgBox("wi = " & wi)
                'MsgBox("lot = " & lot)
                'MsgBox("seq = " & seq)
            ElseIf dfHome.dtType = "NG" Then
                wi = LdfDetailsng.dtWino
                lot = LdfDetailsng.dtLotNo
                seq = Working_Pro.seqNo
                dfType = LdfDetailsng.dtType
                dtCode = LdfDetailsng.dtCode
                dfName = LdfDetailsng.dtName
                Dim rs = ckInputqtyaddjust(tbAddjust.Text, lbMax.Text)
                If rs Then
                    setValueng(actQty, Working_Pro.lb_nc_child_part.Text, Working_Pro.lb_ng_child_part.Text, sNg, tbAddjust.Text)
                    updateAddjustqty(wi, lot, seq, dfType, dtCode, dfName, mainCP, source_code_supplier)
                    Working_Pro.cal_eff()
                    Working_Pro.ResetRed()
                    Working_Pro.Enabled = True
                    Me.Close()
                End If
                'MsgBox("-->")
                'MsgBox("wi = " & wi)
                'MsgBox("lot = " & lot)
                'MsgBox("seq = " & seq)
            End If
        End If
    End Sub
    Public Sub manageNg()
        If defectDetailng.types = "FG" Then
            Dim rs = ckInputqtyaddjust(tbAddjust.Text, lbMax.Text)
            If rs Then
                Dim dfHome As New defectHome
                Dim wi
                Dim lot
                Dim seq
                Dim dfType
                Dim dtCode
                Dim dtName
                If dfHome.dtType = "NC" Then
                    wi = dfDetailsnc.dtWino
                    lot = dfDetailsnc.dtLotNo
                    If MainFrm.chk_spec_line = "2" Then
                        seq = dtSeqno
                    Else
                        seq = Working_Pro.seqNo 'dfDetailsnc.dtSeqno
                    End If
                    dfType = dfDetailsnc.dtType
                    dtCode = dfDetailsnc.dtCode
                    Dim rsCheck = ckInputqtyaddjust(tbAddjust.Text, lbMax.Text)
                    If rsCheck Then
                        setValuenc(actQty, nc, ng, sNc, tbAddjust.Text)
                        updateAddjustqty(wi, lot, seq, dfType, dtCode, dfDetailsnc.dtName, mainCP, source_code_supplier)
                        Working_Pro.ResetRed()
                        Working_Pro.Enabled = True
                        Working_Pro.cal_eff()
                        Working_Pro.ResetRed()
                        Me.Close()
                    Else
                        MsgBox("Please Check QTY.")
                    End If
                ElseIf dfHome.dtType = "NG" Then
                    Dim rsCheck = ckInputqtyaddjust(tbAddjust.Text, lbMax.Text)
                    wi = dfDetailsng.dtWino
                    lot = dfDetailsng.dtLotNo
                    If MainFrm.chk_spec_line = "2" Then
                        seq = dtSeqno
                    Else
                        seq = Working_Pro.seqNo 'dfDetailsnc.dtSeqno
                    End If
                    dfType = dfDetailsng.dtType
                    dtCode = dfDetailsng.dtCode
                    If rsCheck Then
                        setValueng(actQty, nc, ng, sNg, tbAddjust.Text)
                        updateAddjustqty(wi, lot, seq, dfType, dtCode, dfDetailsng.dtName, mainCP, source_code_supplier)
                        Working_Pro.ResetRed()
                        Working_Pro.Enabled = True
                        Working_Pro.cal_eff()
                        Working_Pro.ResetRed()
                        Me.Close()
                    Else
                        MsgBox("Please Check QTY.")
                    End If


                End If
            Else
                MsgBox("Please Check QTY.")
            End If
        Else ' Child Part
            Dim LdfDetailsnc As New defectDetailnc()
            Dim LdfDetailsng As New defectDetailng()
            Dim dfHome As New defectHome
            Dim wi
            Dim lot
            Dim seq
            Dim dfType
            Dim dtCode
            Dim dtName
            If dfHome.dtType = "NC" Then
                wi = LdfDetailsnc.dtWino
                lot = LdfDetailsnc.dtLotNo
                If MainFrm.chk_spec_line = "2" Then
                    seq = dtSeqno
                Else
                    seq = Working_Pro.seqNo 'dfDetailsnc.dtSeqno
                End If
                dfType = LdfDetailsnc.dtType
                dtCode = LdfDetailsnc.dtCode
                dtName = LdfDetailsnc.dtName
                Dim rs = ckInputqtyaddjust(tbAddjust.Text, lbMax.Text)
                If rs Then
                    setValuenc(actQty, Working_Pro.lb_nc_child_part.Text, Working_Pro.lb_ng_child_part.Text, sNc, tbAddjust.Text)
                    updateAddjustqty(wi, lot, seq, dfType, dtCode, dtName, mainCP, source_code_supplier)
                    Working_Pro.cal_eff()
                    Working_Pro.ResetRed()
                    Working_Pro.Enabled = True
                    Me.Close()
                Else
                    MsgBox("Please Check QTY.")
                End If
            ElseIf dfHome.dtType = "NG" Then
                wi = LdfDetailsng.dtWino
                lot = LdfDetailsng.dtLotNo
                If MainFrm.chk_spec_line = "2" Then
                    seq = dtSeqno
                Else
                    seq = Working_Pro.seqNo 'dfDetailsnc.dtSeqno
                End If
                dfType = LdfDetailsng.dtType
                dtCode = LdfDetailsng.dtCode
                dtName = LdfDetailsng.dtName
                Dim rs = ckInputqtyaddjust(tbAddjust.Text, lbMax.Text)
                If rs Then
                    setValueng(actQty, Working_Pro.lb_nc_child_part.Text, Working_Pro.lb_ng_child_part.Text, sNg, tbAddjust.Text)
                    updateAddjustqty(wi, lot, seq, dfType, dtCode, dtName, mainCP, source_code_supplier)
                    Working_Pro.cal_eff()
                    Working_Pro.ResetRed()
                    Working_Pro.Enabled = True
                    Me.Close()
                Else
                    MsgBox("Please Check QTY.")
                End If
            End If
        End If
    End Sub

    Public Shared ReadOnly Property Instance As defectNumpadadjust
        Get
            If _instance Is Nothing Then
                _instance = New defectNumpadadjust()
            End If
            Return _instance
        End Get
    End Property
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If tbAddjust.Text = "" Then
            tbAddjust.Text = 0
        End If
        If defectHome.dtType = "NC" Then
            manageNc()
        Else


            If Working_Pro.slm_flg_qr_prod = "1" Then ' scan QR 
                If defectDetailng.types = "FG" Then
                    Dim rsCheck = ckInputqtyaddjust(tbAddjust.Text, lbMax.Text)
                    If rsCheck Then
                        RemainScanDmcDefect = 0
                        Dim rs As Integer
                        If CDbl(Val(dtQty)) > CDbl(Val(tbAddjust.Text)) Then
                            rs = CDbl(Val(dtQty)) - CDbl(Val(tbAddjust.Text))
                            ScanQRprod.Visible = True
                            ScanQRprod.lbTopices.Text = "Cut Qr Product"
                            ScanQRprod.G_StatusAddCutDefect = "0"
                            Dim rsScan = Math.Abs(rs)
                            Working_Pro.RemainScanDmcDefect = rsScan
                            ScanQRprod.ManageQrScanFA(5, rsScan)
                            ScanQRprod.Show()
                        ElseIf CDbl(Val(dtQty)) < CDbl(Val(tbAddjust.Text)) Then
                            rs = CDbl(Val(tbAddjust.Text)) - CDbl(Val(dtQty))
                            ScanQRprod.lbTopices.Text = "Add Qr Product"
                            ScanQRprod.G_StatusAddCutDefect = "1"
                            ScanQRprod.Visible = True
                            Dim rsScan = Math.Abs(rs)
                            Working_Pro.RemainScanDmcDefect = rsScan
                            ScanQRprod.ManageQrScanFA(5, rsScan)
                            ScanQRprod.Show()
                        ElseIf CDbl(Val(dtQty)) = CDbl(Val(tbAddjust.Text)) Then
                            '  rs = CDbl(Val(tbAddjust.Text))
                            '  ScanQRprod.lbTopices.Text = "Add Qr Product"
                            '  ScanQRprod.Visible = True
                            '  ScanQRprod.G_StatusAddCutDefect = "1"
                            MsgBox("Please Check QTY")
                        End If
                        ' MsgBox(dtQty & " = " & CDbl(Val(tbAddjust.Text)))
                    Else
                        MsgBox("Please Check QTY")
                    End If
                Else
                    manageNg()
                End If
            Else
                manageNg()
                End If
            End If
        '  Me.Close()
    End Sub
    Public Shared Function setValuenc(Act As String, nc As String, ng As String, sNc As String, ipQty As Integer)
        Dim total = (nc - sNc) + ipQty
        If defectDetailnc.Types = "FG" Then
            Working_Pro.lb_nc_qty.Text = total
            ' MsgBox("CDbl(Val(sNc))===>" & CDbl(Val(sNc)))
            ' MsgBox("CDbl(Val(ipQty))===>" & CDbl(Val(ipQty)))
            Dim t
            If ipQty >= CDbl(Val(sNc)) Then
                Dim rs = ipQty - CDbl(Val(sNc))
                t = (CDbl(Val(Working_Pro.lb_good.Text)) - rs)
            Else
                If ipQty = 0 Then
                    ipQty = sNc
                End If
                Dim rs = sNc - ipQty
                If rs = 0 Then
                    rs = sNc
                End If
                t = (CDbl(Val(Working_Pro.lb_good.Text)) + rs)
            End If
            Working_Pro.lb_good.Text = t
        Else
            Working_Pro.lb_nc_child_part.Text = total
        End If
    End Function
    Public Shared Function setValueng(Act As String, nc As String, ng As String, sNg As String, ipQty As Integer)
        Dim total = (ng - sNg) + ipQty
        If defectDetailng.types = "FG" Then
            Working_Pro.lb_ng_qty.Text = total
            Dim t
            If ipQty >= CDbl(Val(sNg)) Then
                Dim rs = ipQty - CDbl(Val(sNg))
                t = (CDbl(Val(Working_Pro.lb_good.Text)) - rs)
            Else
                If ipQty = 0 Then
                    ipQty = sNg
                End If
                Dim rs = sNg - ipQty
                If rs = 0 Then
                    rs = sNg
                End If
                t = (CDbl(Val(Working_Pro.lb_good.Text)) + rs)
            End If
            Working_Pro.lb_good.Text = t
        Else
            Working_Pro.lb_ng_child_part.Text = total
        End If
    End Function
    Public Function updateAddjustqty(dtWino As String, dtLotNo As String, dtSeqno As String, dtType As String, dtCode As String, dfName As String, mainCP As String, source_code_supplier As String)
        Dim md As New modelDefect()
        Dim mdSqlite As New ModelSqliteDefect()
        Dim itemType As String = ""
        If defectHome.dtType = "NC" Then
            If defectDetailnc.Types = "FG" Then
                itemType = "1"
            Else
                itemType = "2"
            End If
        Else
            If defectDetailng.types = "FG" Then
                itemType = "1"
            Else
                itemType = "2"
            End If
        End If
        ' MsgBox("NG ITEM TYHPE ===>" & itemType)
        ' Dim rs = md.mUpdateaddjust(dtWino, dtLotNo, dtSeqno, dtType, dtCode, itemType, lbPart.Text)
        Dim rsSqlite = mdSqlite.mUpdateaddjust(dtWino, dtLotNo, dtSeqno, dtType, dtCode, itemType, lbPart.Text, source_code_supplier)
        'If rs Then
        If rsSqlite Then
            Dim dfRegister As New defectRegister()
            Dim pwi_id As String
            If MainFrm.chk_spec_line = "2" Then
                If defectHome.dtType = "NC" Then
                    pwi_id = defectDetailnc.dtpwi_id
                Else
                    pwi_id = defectDetailng.dtpwi_id
                End If
            Else
                pwi_id = Working_Pro.pwi_id
            End If
            dfRegister.insertDefectregister(dtWino, MainFrm.Label4.Text, dtItemcd, itemType, dtLotNo, dtSeqno, dtType, dtCode, tbAddjust.Text, dtMenu, dtActualdate, pwi_id, dfName, mainCP, source_code_supplier, defectHome.leaderConfrime)
        Else
            MsgBox("Update Status Fiall Function updateAddjustqty in defectNumpadadjust.vb")
            ' MsgBox("mUpdateaddjust rs===>" & rs)
        End If
        Return 0
    End Function
    Public Function calNumpadadjustNc(Act As Integer, nc As Integer, ng As Integer, sNc As Integer)
        Dim totals = (Act - (nc + ng)) + sNc
        '  MsgBox("total = " & totals)
        'MsgBox("(" & Act & " - (" & nc & " + " & ng & ")) + " & sNc)
        Return totals
    End Function
    Public Function calNumpadadjustNg(Act As Integer, nc As Integer, ng As Integer, sNg As Integer)
        Dim totals = (Act - (nc + ng)) + sNg
        'MsgBox(Act & "-" & nc & "+" & ng & "-" & sNg)
        Return totals
    End Function
    Public Function ckInputqtyaddjust(ipQty As Integer, maxQty As Integer)
        If ipQty <= maxQty And ipQty >= 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class