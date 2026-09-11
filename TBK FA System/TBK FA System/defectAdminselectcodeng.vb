Imports System.Web.Script.Serialization

Public Class defectAdminselectcodeng
    Shared Type = ""
    Shared Partfg = ""
    Shared datlvDefectcode As ListViewItem
    Shared mv = New manageVariable()
    Public Shared sPart As String = ""
    Public Shared sDefectcode As String = ""
    Public Shared sDefectdetail As String = ""
    Public Shared S_index As Integer = 0
    Public Sub defectSelectcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dAdminselecttype As New defectAdminselecttypeng()
        lbPartfg.Text = "NG"
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
        Else
            btnDown.Enabled = False
            btnDown.Visible = False
        End If
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim dAdminselecttype As New defectAdminselecttypeng()
        dAdminselecttype.Show()
        Me.Close()
    End Sub

    Private Sub lvDefectcode_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub lvDefectcode_SelectedIndexChanged_1(sender As Object, e As EventArgs)

    End Sub
    Public Shared Function checkDefectCodeSupplier(dfCode As String)
        If dfCode.ToString.Substring(0, 1) = "0" Then
            Return "1"
        Else
            Return "0"
        End If
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        Try
            For Each lvItem As ListViewItem In lvDefectcode.SelectedItems
                Me.sDefectcode = lvDefectcode.Items(lvItem.Index).SubItems(0).Text
                Me.sDefectdetail = lvDefectcode.Items(lvItem.Index).SubItems(1).Text
            Next
            swi = defectAdmindetailng.sWi
            sSeqSpc = defectAdmindetailng.dSeq
            mainCp = "1"
            sPwiSpc = defectAdmindetailng.Apwi_id
            If defectAdminselecttypeng.type = "2" Then
                Dim rss = checkDefectCodeSupplier(Me.sDefectcode)
                If rss = "1" Then
                    Dim rs = OEE.OEE_EXP_CHECK_SUPP(sPart)
                    If rs <> "0" Then
                        Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
                        If CDbl(Val(dcResultdata(0)("OUTSIDE_TYP").ToString())) = 2 Then
                            Dim dfSupplier = New AdmindefectSelectSupplier
                            dfSupplier.show()
                            Me.Hide()
                        Else
                            defectAdminregister.swi = swi
                            defectAdminregister.source_cd_supplier = ""
                            defectAdminregister.SeqSpc = sSeqSpc
                            defectAdminregister.PwiSpc = sPwiSpc
                            defectAdminregister.mainCP = mainCp
                            defectAdminregister.Show()
                            Me.Hide()
                        End If
                    End If
                Else
                    defectAdminregister.swi = swi
                    defectAdminregister.SeqSpc = sSeqSpc
                    defectAdminregister.PwiSpc = sPwiSpc
                    defectAdminregister.mainCP = mainCp
                    defectAdminregister.source_cd_supplier = ""
                    defectAdminregister.Show()
                    Me.Hide()
                End If
            Else
                defectAdminregister.swi = swi
                defectAdminregister.SeqSpc = sSeqSpc
                defectAdminregister.PwiSpc = sPwiSpc
                defectAdminregister.mainCP = mainCp
                defectAdminregister.source_cd_supplier = ""
                defectAdminregister.Show()
                Me.Hide()
            End If
        Catch ex As Exception
            MsgBox("PLEASE SELECT ROW.")
        End Try
    End Sub
    Private Sub btnDown_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub lbPartfg_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub lbType_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnDown_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnUp_Click_1(sender As Object, e As EventArgs) Handles btnUp.Click
        BTNUP1()
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
                'ElseIf lvDefectcode.Items.Count > S_index Then
                '  S_index = CDbl(Val((lvDefectcode.Items.Count - 1)))
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
                'ElseIf lvDefectcode.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvDefectcode.Items.Count - 1)))
            End If
            lvDefectcode.Items(S_index).Selected = True
            lvDefectcode.Items(S_index).EnsureVisible()
            lvDefectcode.Select()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        BTNDOWN1()
    End Sub
End Class
