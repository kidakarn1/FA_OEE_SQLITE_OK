Imports System.Web.Script.Serialization

Public Class AdmindefectSelectSupplier
    Shared Source_cd_supplier As String = ""
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        defectAdminselectcodeng.Show()
        Me.Close()
    End Sub
    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        tbnDown()
    End Sub
    Public Sub tbnUp()
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
                'ElseIf lvDefectcode.Items.Count > S_index Then
                '   S_index = CDbl(Val((lvDefectcode.Items.Count - 1)))
            End If
            lvDefectact.Items(S_index).Selected = True
            lvDefectact.Items(S_index).EnsureVisible()
            lvDefectact.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub tbnDown()
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
                'ElseIf lvDefectcode.Items.Count > S_index Then
                '   S_index = CDbl(Val((lvDefectcode.Items.Count - 1)))
            End If
            lvDefectact.Items(S_index).Selected = True
            lvDefectact.Items(S_index).EnsureVisible()
            lvDefectact.Select()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        tbnUp()
    End Sub
    Public Sub getSupplier()
        Dim OEE = New OEE
        'MsgBox(defectSelectcode.sPart)
        Dim rs = OEE.OEE_EXP_CHECK_SUPP(defectAdminselectcodeng.sPart)
        'MsgBox(rs)
        If rs <> "0" Then
            Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
            Dim i As Integer = 1
            For Each item As Object In dcResultdata
                datlvDefectcode = New ListViewItem(i)
                '  datlvDefectcode.SubItems.Add(item("SOURCE_CD").ToString() & " | " & item("SOURCE_NAME").ToString())
                datlvDefectcode.SubItems.Add(item("SOURCE_CD").ToString())
                datlvDefectcode.SubItems.Add(item("SOURCE_NAME").ToString())
                lvDefectact.Items.Add(datlvDefectcode)
                i += 1
            Next
            lvDefectact.Items(0).Selected = True
        Else
            MsgBox("No Supplier")
        End If
    End Sub
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        For Each lvItem As ListViewItem In lvDefectact.SelectedItems
            Me.Source_cd_supplier = lvDefectact.Items(lvItem.Index).SubItems(1).Text
        Next
        Dim dfAdminregister = New defectAdminregister
        defectAdminregister.swi = defectAdminregister.dtWino
        defectAdminregister.SeqSpc = defectAdminregister.dtSeqno
        defectAdminregister.PwiSpc = "0"
        defectAdminregister.mainCP = "1"
        defectAdminregister.source_cd_supplier = Me.Source_cd_supplier
        defectAdminregister.Show()
        Me.Hide()
    End Sub

    Private Sub AdmindefectSelectSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getSupplier()
    End Sub
End Class