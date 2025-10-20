Imports System.Web.Script.Serialization

Public Class defectAdminselectcodenc
    Shared Type = ""
    Shared Partfg = ""
    Shared datlvDefectcode As ListViewItem
    Shared mv = New manageVariable()
    Public Shared sPart As String = ""
    Public Shared sDefectcode As String = ""
    Public Shared sDefectdetail As String = ""
    Public Shared S_index As Integer = 0
    Public Sub defectSelectcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dAdminselecttype As New defectAdminselecttypenc()
        lbPartfg.Text = "NC"
        sPart = dAdminselecttype.sPart
        lbPartfg.Text = sPart
        getDefectcode()
    End Sub
    Public Sub getDefectcode()
        Dim md = New modelDefect()
        Dim rsData = md.mGetdefectcode(MainFrm.Label4.Text)
        If rsData <> "0" Then
            Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsData)
            Dim i As Integer = 1
            For Each item As Object In dcResultdata
                datlvDefectcode = New ListViewItem(item("defect_cd").ToString())
                datlvDefectcode.SubItems.Add(item("defect_name").ToString())
                lvDefectcode.Items.Add(datlvDefectcode)
                i += 1
            Next
            lvDefectcode.Items(0).Selected = True
        End If
    End Sub



    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim dAdminselecttype As New defectAdminselecttypenc()
        dAdminselecttype.Show()
        Me.Close()
    End Sub

    Private Sub lvDefectcode_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub lvDefectcode_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles lvDefectcode.SelectedIndexChanged

    End Sub

    ' Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, btnDown.Click
    ' For Each lvItem As ListViewItem In lvDefectcode.SelectedItems
    ' Me.sDefectcode = lvDefectcode.Items(lvItem.Index).SubItems(0).Text
    ' Me.sDefectdetail = lvDefectcode.Items(lvItem.Index).SubItems(1).Text
    ' Next
    ' '' Dim dfAdminregister = New defectAdminregister
    '    defectAdminregister.Show()
    ' Me.Hide()
    ' End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles  PictureBox2.Click
        BTNDOWN1()
    End Sub

    Private Sub lbPartfg_Click(sender As Object, e As EventArgs) Handles lbPartfg.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) 

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) 

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) 

    End Sub

    Private Sub lbType_Click(sender As Object, e As EventArgs) 

    End Sub


    Public Sub BTNUP1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvDefectcode.Items.Count - 1))) Then
            S_index = CDbl(Val((lvDefectcode.Items.Count - 1)))
        End If
        Try
            lvDefectcode.Items(S_index).Selected = False
            S_index -= 1
            If S_index < 0 Then
                S_index = 0
                ' ElseIf lvDefectcode.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvDefectcode.Items.Count - 1)))
            End If
            lvDefectcode.Items(S_index).Selected = True
            lvDefectcode.Items(S_index).EnsureVisible()
            lvDefectcode.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BTNDOWN1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvDefectcode.Items.Count - 1))) Then
            S_index = CDbl(Val((lvDefectcode.Items.Count - 1)))
        End If
        Try
            lvDefectcode.Items(S_index).Selected = False
            S_index += 1
            If S_index < 0 Then
                S_index = 0
                '  ElseIf lvDefectcode.Items.Count > S_index Then
                '     S_index = CDbl(Val((lvDefectcode.Items.Count - 1)))
            End If
            lvDefectcode.Items(S_index).Selected = True
            lvDefectcode.Items(S_index).EnsureVisible()
            lvDefectcode.Select()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        BTNUP1()
    End Sub

    Private Sub btnDown_Click_1(sender As Object, e As EventArgs) Handles btnDown.Click
        If lvDefectcode.SelectedItems.Count > 0 Then
            For Each lvItem As ListViewItem In lvDefectcode.SelectedItems
                Me.sDefectcode = lvDefectcode.Items(lvItem.Index).SubItems(0).Text
                Me.sDefectdetail = lvDefectcode.Items(lvItem.Index).SubItems(1).Text
            Next
            '' Dim dfAdminregister = New defectAdminregister
            defectAdminregister.Show()
            Me.Hide()
        Else
            MsgBox("PLEASE SELECT ROW.")
        End If
    End Sub
End Class