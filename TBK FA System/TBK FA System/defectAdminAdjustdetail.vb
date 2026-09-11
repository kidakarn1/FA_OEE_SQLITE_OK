Imports System.Web.Script.Serialization
Public Class defectAdminAdjustdetail
    Public Shared NC As Integer = 0
    Public Shared NG As Integer = 0
    Public Shared ACTUAL As Integer = 0
    Public Shared SNC As Integer = 0
    Public Shared SNG As Integer = 0
    Public Shared SDEFECT_CODE As String = ""
    Public Shared SPART As String = ""
    Public Shared SItemType As String = 0
    Public Shared S_index As Integer = 0
    Public Shared Types As String = ""
    Private Sub defectAdminAdjustdetailnc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbLine.Text = MainFrm.Label4.Text
        setData()
        setCombobox.SelectedIndex = 0
        loadData()
    End Sub
    Public Sub setData()
        setCombobox.Items.Add("FG")
        setCombobox.Items.Add("CP")
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        ClickOk()
    End Sub
    Public Sub ClickOk()
        SItemType = setCombobox.Text
        Types = setCombobox.Text
        For Each lvItem As ListViewItem In lvDefectact.SelectedItems
            SNC = lvDefectact.Items(lvItem.Index).SubItems(4).Text
            SDEFECT_CODE = lvDefectact.Items(lvItem.Index).SubItems(2).Text
            SPART = lvDefectact.Items(lvItem.Index).SubItems(1).Text
        Next

        defectAdminAdjustnumpadadjust.Show()
    End Sub
    Public Sub loadData()
        btnOk.Visible = False
        Dim objDefectHome As defectAdminhome
        Dim dfType As String = ""
        If objDefectHome.dfType = "NG" Then
            dfType = "1"
        ElseIf objDefectHome.dfType = "NC" Then
            dfType = "2"
        End If
        lvDefectact.Items.Clear()
        Dim md As New modelDefect()
        Dim objdef As New defectAdminselectdetailncadjust()
        If dfType = "1" Then
            lvDefectact.BackColor = Color.Tomato
        ElseIf dfType = "2" Then
            lvDefectact.BackColor = Color.Peru
        End If
        Dim rs = md.mGetDataAdminAdjustByWi(defectAdminsearch.lineCd, defectAdminsearch.dateCheck, defectAdminsearch.Pshift, objdef.sWi, setCombobox.Text, objdef.sSEQ, dfType)
        If rs <> "0" Then
            Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
            Dim i As Integer = 1

            For Each item As Object In dcResultdata
                If item("da_qty").ToString() <> "0" Then
                    cListview += 1
                    datlvDefectsumary = New ListViewItem(i)
                    datlvDefectsumary.SubItems.Add(item("da_item_cd").ToString())
                    datlvDefectsumary.SubItems.Add(item("da_code").ToString())
                    datlvDefectsumary.SubItems.Add(item("defect_name").ToString())
                    datlvDefectsumary.SubItems.Add(item("da_qty").ToString())
                    'datlvDefectsumary.SubItems.Add(item("SEQ_NO").ToStriang())
                    NC = CDbl(Val(item("TOTAL_NC").ToString()))
                    NG = CDbl(Val(item("TOTAL_NG").ToString()))
                    ACTUAL = item("TOTAL_ACTUAL").ToString()
                    lvDefectact.Items.Add(datlvDefectsumary)
                    i += 1
                End If
                btnOk.Visible = True
            Next
            Try
                lvDefectact.Items(0).Selected = True
                lvDefectact.Items(0).EnsureVisible()
                lvDefectact.Select()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        defectAdminselectdetailncadjust.Show()
        Me.Close()
    End Sub

    Private Sub setCombobox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles setCombobox.SelectedIndexChanged
        loadData()
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        BTNUP1()
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
                'ElseIf lvDefectact.Items.Count > S_index Then
                'S_index = CDbl(Val((lvDefectact.Items.Count - 1)))
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
                'ElseIf S_index > lvDefectact.Items.Count Then
                '  S_index = CDbl(Val((lvDefectact.Items.Count - 1)))
            End If
            lvDefectact.Items(S_index).Selected = True
            lvDefectact.Items(S_index).EnsureVisible()
            lvDefectact.Select()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        BTNDOWN1()
    End Sub
End Class