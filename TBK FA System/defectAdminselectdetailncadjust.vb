Imports System.Web.Script.Serialization
Public Class defectAdminselectdetailncadjust
    Public Shared sSEQ As String = ""
    Public Shared sWi As String = ""
    Public Shared sLot As String = ""
    Public Shared sPart As String = ""
    Public Shared S_index As Integer = 0
    Public Shared Apwi_id As String = ""

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If lvAction.SelectedItems.Count > 0 Then
            For Each lvItem As ListViewItem In lvAction.SelectedItems
                sPart = lvAction.Items(lvItem.Index).SubItems(0).Text
                sWi = lvAction.Items(lvItem.Index).SubItems(1).Text
                sSEQ = lvAction.Items(lvItem.Index).SubItems(2).Text
                sLot = lvAction.Items(lvItem.Index).SubItems(3).Text
                sLot = lvAction.Items(lvItem.Index).SubItems(3).Text
                Apwi_id = lvAction.Items(lvItem.Index).SubItems(4).Text
            Next
            ' Dim obj As New defectAdminAdjustdetailnc
            If defectAdminhome.dfType = "NC" Then
                defectAdminAdjustdetail.backgroundNg.Visible = False
            Else
                defectAdminAdjustdetail.backgroundNg.Visible = True
            End If
            defectAdminAdjustdetail.Show()
            Me.Hide()
        Else
            MsgBox("PLEASE SELECT ROW.")
        End If

    End Sub
    Private Sub defectAdminselectdetailncadjust_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbLine.Text = MainFrm.Label4.Text
        loadData()
    End Sub
    Public Sub loadData()
        lvAction.Items.Clear()
        Dim md As New modelDefect
        Dim objDefectHome As defectAdminhome
        Dim dfType As String = ""
        If objDefectHome.dfType = "NG" Then
            dfType = "1"
        ElseIf objDefectHome.dfType = "NC" Then
            dfType = "2"
        End If
        Dim rs = md.mGetPartBySelectDate(lbLine.Text, defectAdminsearch.dateCheck, defectAdminsearch.Pshift, dfType)
        If dfType = "1" Then
            lvAction.BackColor = Color.Tomato
        ElseIf dfType = "2" Then
            lvAction.BackColor = Color.Peru
        End If
        If rs <> "0" Then
            Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
            Dim i As Integer = 1
            For Each item As Object In dcResultdata
                cListview += 1
                datlvDefectsumary = New ListViewItem(item("da_item_cd").ToString())
                datlvDefectsumary.SubItems.Add(item("da_wi_no").ToString())
                datlvDefectsumary.SubItems.Add(item("da_seq_no").ToString())
                datlvDefectsumary.SubItems.Add(item("da_lot_no").ToString())
                datlvDefectsumary.SubItems.Add(item("pwi_id").ToString())
                'datlvDefectsumary.SubItems.Add(item("SEQ_NO").ToString())
                lvAction.Items.Add(datlvDefectsumary)
            Next
            Button1.Visible = True
            Try
                lvAction.Items(0).Selected = True
                lvAction.Items(0).EnsureVisible()
                lvAction.Select()
            Catch ex As Exception

            End Try
        Else
            Button1.Visible = False
        End If
    End Sub


    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        defectAdminhome.Show()
        Me.Close()
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        BTNUP1()
    End Sub
    Public Sub BTNUP1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvAction.Items.Count - 1))) Then
            S_index = CDbl(Val((lvAction.Items.Count - 1)))
        End If
        Try
            lvAction.Items(S_index).Selected = False
            S_index -= 1
            If S_index < 0 Then
                S_index = 0
                ' ElseIf lvAction.Items.Count > S_index Then
                '   S_index = CDbl(Val((lvAction.Items.Count - 1)))
            End If
            lvAction.Items(S_index).Selected = True
            lvAction.Items(S_index).EnsureVisible()
            lvAction.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BTNDOWN1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvAction.Items.Count - 1))) Then
            S_index = CDbl(Val((lvAction.Items.Count - 1)))
        End If
        Try
            lvAction.Items(S_index).Selected = False
            S_index += 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf lvAction.Items.Count > S_index Then
                ' S_index = CDbl(Val((lvAction.Items.Count - 1)))
            End If
            lvAction.Items(S_index).Selected = True
            lvAction.Items(S_index).EnsureVisible()
            lvAction.Select()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        BTNDOWN1()
    End Sub
End Class