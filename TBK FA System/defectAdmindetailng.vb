Imports System.Web.Script.Serialization
Public Class defectAdmindetailng
    Shared sLinecd = ""
    Shared sDate = DateTime.Now.ToString("yyyy-MM-dd")
    Shared eDate = DateTime.Now.ToString("yyyy-MM-dd")
    Public Shared sWi As String = ""
    Public Shared sPartno As String = ""
    Public Shared sshift As String = ""
    Public Shared sLot As String = ""
    Public Shared dSeq As String = ""
    Public Shared sAct As Integer = 0
    Public Shared sNc As Integer = 0
    Public Shared sNg As Integer = 0
    Public Shared S_index As Integer = 0
    Public Shared Apwi_id As String = ""
    Private Sub defectAdmindetailnc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setVariable()
        getDatadefect(sLinecd, sDate, eDate)
    End Sub
    Public Sub setVariable()
        sLinecd = MainFrm.Label4.Text
        lbLine.Text = sLinecd
    End Sub
    Public Sub getDatadefect(sLinecd As String, sDate As String, eDate As String)
        Dim md As New modelDefect()
        Dim rs = md.mGetDefectadmindetailng(sLinecd, sDate, eDate)
        If rs <> "0" Then
            Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
            Dim i As Integer = 1
            For Each item As Object In dcResultdata
                cListview += 1
                datlvDefectsumary = New ListViewItem(item("ITEM_CD").ToString())
                'datlvDefectsumary.SubItems.Add()
                datlvDefectsumary.SubItems.Add(item("WI").ToString())
                datlvDefectsumary.SubItems.Add(item("SHIFT").ToString())
                datlvDefectsumary.SubItems.Add(item("SEQ_NO").ToString())
                datlvDefectsumary.SubItems.Add(item("LOT_NO").ToString())
                datlvDefectsumary.SubItems.Add(item("ACT_QTY").ToString())
                datlvDefectsumary.SubItems.Add(item("GOOD_QTY").ToString())
                sAct = item("ACT_QTY").ToString()
                sNg = 0
                Try
                    sNg = item("DEF_QTY").ToString()
                    datlvDefectsumary.SubItems.Add(item("DEF_QTY").ToString())
                Catch ex As Exception
                    sNg = 0
                    datlvDefectsumary.SubItems.Add("")
                End Try
                datlvDefectsumary.SubItems.Add(item("TOTAL_DEFECT").ToString())
                If item("PWI_ID").ToString() <> "0" Then
                    datlvDefectsumary.SubItems.Add(item("PWI_ID").ToString())
                Else
                    Dim getPwi_id = md.mGetPwi_id(item("WI").ToString(), item("LOT_NO").ToString(), item("SEQ_NO").ToString(), item("SHIFT").ToString())
                    datlvDefectsumary.SubItems.Add(getPwi_id)
                End If
                lvDefectact.Items.Add(datlvDefectsumary)
                i += 1
            Next
            lvDefectact.Items(0).Selected = True
            btnOk.Visible = True
        Else
            btnOk.Visible = False
        End If
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        For Each lvItem As ListViewItem In lvDefectact.SelectedItems
            sPartno = lvDefectact.Items(lvItem.Index).SubItems(0).Text
            sWi = lvDefectact.Items(lvItem.Index).SubItems(1).Text
            sshift = lvDefectact.Items(lvItem.Index).SubItems(2).Text
            dSeq = lvDefectact.Items(lvItem.Index).SubItems(3).Text
            sLot = lvDefectact.Items(lvItem.Index).SubItems(4).Text
            sAct = lvDefectact.Items(lvItem.Index).SubItems(5).Text
            sNc = 0
            ' sNg = lvDefectact.Items(lvItem.Index).SubItems(8).Text
            sNg = lvDefectact.Items(lvItem.Index).SubItems(8).Text
            Apwi_id = lvDefectact.Items(lvItem.Index).SubItems(9).Text
        Next
        Try
            Dim dfAdminselecttype2 As New defectAdminselecttypeng()
            dfAdminselecttype2.Show()
            Me.Close()
        Catch ex As Exception
            MsgBox("Please Click Back.")
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim dfAdminhome As New defectAdminhome
        defectAdminhome.Show()
        Me.Close()
    End Sub

    Private Sub btnDown(sender As Object, e As EventArgs) Handles PictureBox1.Click
        BTNDOWN1()
    End Sub

    Private Sub lvDefectact_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Public Sub BTNUP1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvDefectact.Items.Count - 1))) Then
            S_index = CDbl(Val((lvDefectact.Items.Count - 1)))
        End If
        Try
            lvDefectact.Items(S_index).Selected = False
            S_index -= 1
            If S_index < 0 Then
                S_index = 0
            ElseIf lvDefectact.Items.Count >= S_index Then
                S_index = CDbl(Val((lvDefectact.Items.Count - 1)))
            End If
            lvDefectact.Items(S_index).Selected = True
            lvDefectact.Items(S_index).EnsureVisible()
            lvDefectact.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BTNDOWN1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvDefectact.Items.Count - 1))) Then
            S_index = CDbl(Val((lvDefectact.Items.Count - 1)))
        End If
        Try
            lvDefectact.Items(S_index).Selected = False
            S_index += 1

            If S_index < 0 Then
                S_index = 0
                ' ElseIf lvDefectact.Items.Count >= S_index Then
                '  S_index = CDbl(Val((lvDefectact.Items.Count - 1)))
            End If
            lvDefectact.Items(S_index).Selected = True
            lvDefectact.Items(S_index).EnsureVisible()
            lvDefectact.Select()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        BTNUP1()
    End Sub
End Class
