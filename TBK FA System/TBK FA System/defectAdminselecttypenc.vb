Imports System.Web.Script.Serialization

Public Class defectAdminselecttypenc
    Public Shared type
    Shared sPartno
    Public Shared wi
    Public Shared dtType
    Public Shared dt_menu
    Shared datlvChildpart As ListViewItem
    Public Shared sPart As String = ""
    Public Shared actTotal As Integer = defectAdmindetailnc.sAct
    Public Shared ncTotal As Integer = defectAdmindetailnc.sNc
    Public Shared ngTotal As Integer = defectAdmindetailnc.sNg
    Public Shared Apwi_id As Integer = defectAdmindetailnc.Apwi_id
    Public Shared mv = New manageVariable()
    Public S_index As Integer = 0
    Public Shared Sub setVariable()
        actTotal = defectAdmindetailnc.sAct
        ncTotal = defectAdmindetailnc.sNc
        ngTotal = defectAdmindetailnc.sNg
        Dim dfAdmindetailnc As New defectAdmindetailnc()
        type = "NC"
        sPartno = dfAdmindetailnc.sPartno
        wi = dfAdmindetailnc.sWi
    End Sub
    Private Sub defectSelecttype_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setVariable()
        lbType.Text = "NC"
        btnPartfg.Text = sPartno
        Dim reData = getChildpart(wi)
        Try
            lvChildpart.Items(0).Selected = True
            lvChildpart.Items(0).EnsureVisible()
            lvChildpart.Select()
        Catch ex As Exception

        End Try
    End Sub

    Public Function getChildpart(wi)
        Dim md = New modelDefect()
        Dim rsData = md.mGetchildpart(wi)
        If rsData <> "0" Then
            Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsData)
            Dim i As Integer = 1
            For Each item As Object In dcResultdata
                datlvChildpart = New ListViewItem(i)
                datlvChildpart.SubItems.Add(item("ITEM_CD").ToString())
                lvChildpart.Items.Add(datlvChildpart)
                i += 1
            Next
        End If
    End Function
    Public Sub BTNUP1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvChildpart.Items.Count - 1))) Then
            S_index = CDbl(Val((lvChildpart.Items.Count - 1)))
        End If
        Try
            lvChildpart.Items(S_index).Selected = False
            S_index -= 1
            If S_index < 0 Then
                S_index = 0
                ' ElseIf lvChildpart.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvChildpart.Items.Count - 1)))
            End If
            lvChildpart.Items(S_index).Selected = True
            lvChildpart.Items(S_index).EnsureVisible()
            lvChildpart.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BTNDOWN1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvChildpart.Items.Count - 1))) Then
            S_index = CDbl(Val((lvChildpart.Items.Count - 1)))
        End If
        Try
            lvChildpart.Items(S_index).Selected = False
            S_index += 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf lvChildpart.Items.Count > S_index Then
                '  S_index = CDbl(Val((lvChildpart.Items.Count - 1)))
            End If
            lvChildpart.Items(S_index).Selected = True
            lvChildpart.Items(S_index).EnsureVisible()
            lvChildpart.Select()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        BTNUP1()
    End Sub
    Private Sub lvChildpart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvChildpart.SelectedIndexChanged

    End Sub
    Private Sub btnPartfg_Click(sender As Object, e As EventArgs) Handles btnPartfg.Click
        Dim mdDefect = New modelDefect
        If mdDefect.mGetDataEnableFGPart(MainFrm.Label4.Text) = "1" Then
            type = "1"
            dtType = "2"
            dt_menu = "1"
            Dim sDefectcode As New defectAdminselectcodenc()
            Me.sPart = btnPartfg.Text
            sDefectcode.Show()
            Me.Hide()
        End If
    End Sub
    Private Sub HScrollBar1_Scroll(sender As Object, e As ScrollEventArgs)
        'create two scroll bars
        Dim hs As HScrollBar
        Dim vs As VScrollBar
        hs = New HScrollBar()
        vs = New VScrollBar()
        'set properties
        hs.Location = New Point(10, 200)
        hs.Size = New Size(175, 15)
        hs.Value = 50
        vs.Location = New Point(200, 30)
        vs.Size = New Size(15, 175)
        hs.Value = 50
        'adding the scroll bars to the form
        Me.Controls.Add(hs)
        Me.Controls.Add(vs)
        ' Set the caption bar text of the form.  
        Me.Text = "tutorialspoint.com"
    End Sub

	Private Sub Label2_Click(sender As Object, e As EventArgs)

	End Sub

	Private Sub Label3_Click(sender As Object, e As EventArgs)

	End Sub

	Private Sub lbType_Click(sender As Object, e As EventArgs)

	End Sub

	Private Sub Label4_Click(sender As Object, e As EventArgs)

	End Sub

	Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim dfAdmindetailnc As New defectAdmindetailnc()
        dfAdmindetailnc.Show()
        Me.Close()
    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        BTNDOWN1()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        For Each lvItem As ListViewItem In lvChildpart.SelectedItems
            Me.sPart = lvChildpart.Items(lvItem.Index).SubItems(1).Text
        Next
        dt_menu = "1"
        dtType = "2"
        type = "2"
        ' mv.setSelectpartdefect("TEST OK")
        Dim sDefectcode As New defectAdminselectcodenc()
        sDefectcode.Show()
        Me.Hide()
    End Sub
End Class