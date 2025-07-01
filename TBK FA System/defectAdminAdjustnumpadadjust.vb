Imports System.Globalization
Public Class defectAdminAdjustnumpadadjust
    Shared dfDetailsnc As New defectDetailnc()
    Shared dfDetailsng As New defectDetailng()
    Shared maxQty As Integer = 0
    Shared sPart As String = ""
    Shared actQty As Integer = 0
    Public Shared dtWino
    Public Shared dtLineno
    Public Shared dtItemcd
    Public Shared dtItemtype
    Public Shared dtLotNo
    Public Shared dtSeqno
    Public Shared dtType
    Public Shared dtCode
    Public Shared dtQty
    Public Shared dtMenu
    Public Shared dtActualdate
    Public Shared pd As New Confrime_work_production()
    Public Shared sNc As Integer = 0
    Public Shared sNg As Integer = 0
    Public Shared NC As Integer = 0
    Public Shared NG As Integer = 0
    Public Shared GmaxQty As Integer = 0
    Public Shared GuseQty As Integer = 0
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
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim dfHome As defectHome
        If dfHome.dtType = "NC" Then
            defectAdminAdjustdetail.Show()
        ElseIf dfHome.dtType = "NG" Then
            ' defectAdminAdjustdetailng.Show()
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
        Dim dfHome As defectAdminhome
        If dfHome.dfType = "NC" Then
            Dim objDefectdetailnc As New defectAdminAdjustdetail
            Dim objSelectDefectdetailnc As New defectAdminselectdetailncadjust
            If defectAdminAdjustdetail.Types = "FG" Then
                actQty = objDefectdetailnc.ACTUAL
            ElseIf defectAdminAdjustdetail.Types = "CP" Then
                actQty = objDefectdetailnc.SNC
            End If

            sPart = objDefectdetailnc.SPART '"J107-11820-RM" 'pd.pFg
            dtWino = objSelectDefectdetailnc.sWi
            dtLineno = MainFrm.Label4.Text 'objDefectdetailnc.dtLineno
            dtItemcd = objSelectDefectdetailnc.sPart
            '   Dim dfAdminAdjust As New defectAdminAdjustdetailnc
            If defectAdminAdjustdetail.SItemType = "FG" Then
                dtItemtype = "1"
            Else
                dtItemtype = "2"
            End If
            dtLotNo = objSelectDefectdetailnc.sLot
            dtSeqno = objSelectDefectdetailnc.sSEQ
            dtType = "2" 'objDefectdetailnc.dtType 'NC/NG
            dtCode = objDefectdetailnc.SDEFECT_CODE
            dtQty = objDefectdetailnc.SNC
            dtMenu = "2"
            dtActualdate = DateTime.Now.ToString("yyyy-MM-dd H:m:s") 'defectAdminsearch.dateCheck
            sNc = objDefectdetailnc.SNC
        ElseIf dfHome.dfType = "NG" Then
            Dim objDefectdetailnc As New defectAdminAdjustdetail
            Dim objSelectDefectdetailnc As New defectAdminselectdetailncadjust
            If defectAdminAdjustdetail.Types = "FG" Then
                actQty = objDefectdetailnc.ACTUAL
            ElseIf defectAdminAdjustdetail.Types = "CP" Then
                actQty = objDefectdetailnc.SNC
            End If
                sPart = objDefectdetailnc.SPART '"J107-11820-RM" 'pd.pFg
            dtWino = objSelectDefectdetailnc.sWi
            dtLineno = MainFrm.Label4.Text 'objDefectdetailnc.dtLineno
            dtItemcd = objSelectDefectdetailnc.sPart
            If defectAdminAdjustdetail.SItemType = "FG" Then
                dtItemtype = "1"
            Else
                dtItemtype = "2"
            End If
            dtLotNo = objSelectDefectdetailnc.sLot
            dtSeqno = objSelectDefectdetailnc.sSEQ
            dtType = "1" 'objDefectdetailnc.dtType 'NC/NG
            dtCode = objDefectdetailnc.SDEFECT_CODE
            dtQty = objDefectdetailnc.SNC
            dtMenu = "2"
            dtActualdate = DateTime.Now.ToString("yyyy-MM-dd H:m:s") 'defectAdminsearch.dateCheck
            sNg = objDefectdetailnc.SNC
        End If
    End Sub
    Private Sub defectNumpadadjust_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setVariable()
        lbAct.Text = actQty
        lbMax.Text = ""
        lbPart.Text = sPart
        Dim objDefect As New defectAdminAdjustdetail
        NC = objDefect.NC
        NG = objDefect.NG
        Dim dfHome As defectAdminhome
        If dfHome.dfType = "NC" Then
            If defectAdminAdjustdetail.Types = "FG" Then
                lbMax.Text = calNumpadadjustNc(actQty, NC, NG, sNc)
            Else
                Dim md = New modelDefect
                ' Dim UseQty = md.mGetdefectdetailncPartno(dtWino, dtSeqno, dtLotNo, dtType, lbPart.Text)
                Dim UseQty = md.mGetdefectdetailPartno(dtWino, dtSeqno, dtLotNo, dtType, lbPart.Text)
                maxQty = (999 - Convert.ToInt32(UseQty)) + sNc
                lbMax.Text = maxQty
            End If
        ElseIf dfHome.dfType = "NG" Then
            If defectAdminAdjustdetail.Types = "FG" Then
                lbMax.Text = calNumpadadjustNg(actQty, NC, NG, sNg)
            Else
                Dim md = New modelDefect
                'Dim UseQty = md.mGetdefectdetailncPartno(dtWino, dtSeqno, dtLotNo, dtType, lbPart.Text)
                Dim UseQty = md.mGetdefectdetailPartno(dtWino, dtSeqno, dtLotNo, dtType, lbPart.Text)
                maxQty = (999 - Convert.ToInt32(UseQty)) + sNg
                lbMax.Text = maxQty
            End If
        End If
    End Sub
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If tbAddjust.Text = "" Then
            tbAddjust.Text = 0
        End If
        Dim rs = ckInputqtyaddjust(tbAddjust.Text, lbMax.Text)
        If rs Then
            GmaxQty = lbMax.Text
            GuseQty = tbAddjust.Text
            Dim dfAlert As New defectAlertsuredefect
            dfAlert.Show()
            '  updateAddjustqty(dtWino, dtLotNo, dtSeqno, dtType, dtCode)
            '  Dim obj As New defectAdminAdjustdetailnc
            '  obj.Show()
            '  Me.Close()
        Else
            MsgBox("Please Check QTY.")
        End If
    End Sub
    Public Function updateAddjustqty(dtWino As String, dtLotNo As String, dtSeqno As String, dtType As String, dtCode As String, dtItemCd As String, pwi_id As String)
        Dim md As New modelDefect()
        Dim mdSqlite As New ModelSqliteDefect()
        'Dim rs = md.mUpdateaddjust(dtWino, dtLotNo, dtSeqno, dtType, dtCode, dtItemtype, dtItemCd)
        Dim rs = mdSqlite.mUpdateaddjust(dtWino, dtLotNo, dtSeqno, dtType, dtCode, dtItemtype, dtItemCd, "SupplierCode")
        If rs Then
            Dim objDefectdetailnc As New defectAdminAdjustdetail
            Dim rsActualDefect = md.mUpdatedefectactualAdmin(dtWino, dtLotNo, dtSeqno, dtType, dtCode, dtItemtype, objDefectdetailnc.SPART)
            If rsActualDefect Then
                Dim dfRegister As New defectAdminregister()
                dfRegister.insertDefectregister(dtWino, dtLineno, objDefectdetailnc.SPART, dtItemtype, dtLotNo, dtSeqno, dtType, dtCode, tbAddjust.Text, dtMenu, dtActualdate, pwi_id)
                Dim rsInsertActual = md.mInsertdefectactual(dtWino, dtLineno, objDefectdetailnc.SPART, dtItemtype, dtLotNo, dtSeqno, dtType, dtCode, tbAddjust.Text, "2", dtActualdate, pwi_id)
            Else
                MsgBox("Update Status Fiall Function mUpdatedefectactual in defectNumpadadjust.vb")
            End If
        Else
            MsgBox("Update Status Fiall Function updateAddjustqty in defectNumpadadjust.vb")
        End If
        Return 0
    End Function
    Public Function calNumpadadjustNc(Act As Integer, nc As Integer, ng As Integer, sNc As Integer)
        Dim total = (Act - (nc + ng)) + sNc
        'MsgBox("(" & Act & "-" & "(" & nc & "+" & ng & ")) + " & sNc & " = " & total)
        Return total
    End Function
    Public Function calNumpadadjustNg(Act As Integer, nc As Integer, ng As Integer, sNg As Integer)
        Dim total = (Act - (nc + ng)) + sNg
        'MsgBox("(" & Act & "-" & "(" & nc & "+" & ng & ")) + " & sNg & " = " & total)
        Return total
    End Function
    Public Function ckInputqtyaddjust(ipQty As Integer, maxQty As Integer)
        If ipQty <= maxQty And ipQty >= 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
