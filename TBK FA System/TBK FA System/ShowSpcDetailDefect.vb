Imports System.Web.Script.Serialization
Public Class ShowSpcDetailDefect
    Shared aDefectcode As List(Of String) = New List(Of String)
    Shared aDefectQty As List(Of String) = New List(Of String)
    Shared checkstatusFG As String = "0"
    Shared index_FG As Integer = 0
    Shared index_CP As Integer = 0
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub
    Public Function getDefectdetail()
        Try
            'If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
            Dim md As New modelDefect()
                Dim mdSqlite As New ModelSqliteDefect()
                Dim arrayWI As List(Of String) = New List(Of String)
                For Each itemPlanData As DataPlan In MainFrm.ArrayDataPlan
                    arrayWI.Add(itemPlanData.wi)
                Next
                Dim GenSEQ As Integer = Working_Pro.Label22.Text - MainFrm.ArrayDataPlan.ToArray.Length
                Dim Iseq = GenSEQ
                'rsFg = md.mGetdatachildpartsummaryfgSpc(arrayWI.ToArray(), MainFrm.ArrayDataPlan.ToArray.Length, Working_Pro.Label18.Text, GenSEQ)
                rsFg = mdSqlite.mSqliteGetdatachildpartsummaryfgSpc(arrayWI.ToArray(), MainFrm.ArrayDataPlan.ToArray.Length, Working_Pro.Label18.Text, GenSEQ)
                '  rs = md.mGetdatachildpartsummarychildSpc(arrayWI.ToArray(), MainFrm.ArrayDataPlan.ToArray.Length, Working_Pro.Label18.Text, GenSEQ)
                rs = mdSqlite.mSqliteGetdatachildpartsummarychildSpc(arrayWI.ToArray(), MainFrm.ArrayDataPlan.ToArray.Length, Working_Pro.Label18.Text, GenSEQ)
                If rsFg <> "0" Then
                    Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsFg)
                    Dim i As Integer = 1
                    For Each item As Object In dcResultdata
                        If item("dt_type").ToString() = "1" Then
                            dt_item_type = "NG"
                        Else
                            dt_item_type = "NC"
                        End If
                        tmplvFG = New ListViewItem(item("dt_wi_no").ToString())
                        tmplvFG.SubItems.Add(item("dt_item_cd").ToString())
                        tmplvFG.SubItems.Add(item("dt_code").ToString())
                        tmplvFG.SubItems.Add(dt_item_type)
                        tmplvFG.SubItems.Add(item("total_nc").ToString())
                        lvFG.Items.Add(tmplvFG)
                    Next
                Else
                    tmplvFG = New ListViewItem("NO DATA")
                    tmplvFG.SubItems.Add("NO DATA")
                    tmplvFG.SubItems.Add("NO DATA")
                    tmplvFG.SubItems.Add("NO DATA")
                    tmplvFG.SubItems.Add("NO DATA")
                    lvFG.Items.Add(tmplvFG)
                End If
                If rs <> "0" Then
                    Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
                    Dim i As Integer = 1
                    For Each item As Object In dcResultdata
                        If item("dt_type").ToString() = "1" Then
                            dt_item_type = "NG"
                        Else
                            dt_item_type = "NC"
                        End If
                        tmplvcp = New ListViewItem(item("dt_wi_no").ToString())
                        tmplvcp.SubItems.Add(item("dt_item_cd").ToString())
                        tmplvcp.SubItems.Add(item("dt_code").ToString())
                        tmplvcp.SubItems.Add(dt_item_type)
                        tmplvcp.SubItems.Add(item("total_nc").ToString())
                        lvCp.Items.Add(tmplvcp)
                    Next
                Else
                    tmplvcp = New ListViewItem("NO DATA")
                    tmplvcp.SubItems.Add("NO DATA")
                    tmplvcp.SubItems.Add("NO DATA")
                    tmplvcp.SubItems.Add("NO DATA")
                    tmplvcp.SubItems.Add("NO DATA")
                    lvCp.Items.Add(tmplvcp)
                End If
            'End If
        Catch ex As Exception
            load_show.Show()
        End Try
    End Function

    Private Sub ShowSpcDetailDefect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getDefectdetail()
    End Sub

    Private Sub btnFGUp_Click(sender As Object, e As EventArgs) Handles btnFGUp.Click
        tbnUpFG()
    End Sub

    Public Sub tbnUpFG()
        If index_FG < 0 Then
            index_FG = 0
        ElseIf index_FG > CDbl(Val((lvFG.Items.Count - 1))) Then
            index_FG = CDbl(Val((lvFG.Items.Count - 1)))
        End If
        Try
            lvFG.Items(index_FG).Selected = False
            index_FG -= 1
            If index_FG < 0 Then
                index_FG = 0
                ' ElseIf lvChildpart.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvChildpart.Items.Count - 1)))
            End If
            lvFG.Items(index_FG).Selected = True
            lvFG.Items(index_FG).EnsureVisible()
            lvFG.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub tbnDownFG()
        If index_FG < 0 Then
            index_FG = 0
        ElseIf index_FG > CDbl(Val((lvFG.Items.Count - 1))) Then
            index_FG = CDbl(Val((lvFG.Items.Count - 1)))
        End If
        Try
            lvFG.Items(index_FG).Selected = False
            index_FG += 1
            If index_FG < 0 Then
                index_FG = 0
                ' ElseIf lvChildpart.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvChildpart.Items.Count - 1)))
            End If
            lvFG.Items(index_FG).Selected = True
            lvFG.Items(index_FG).EnsureVisible()
            lvFG.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub tbnUpCP()
        If index_CP < 0 Then
            index_CP = 0
        ElseIf index_CP > CDbl(Val((lvCp.Items.Count - 1))) Then
            index_CP = CDbl(Val((lvCp.Items.Count - 1)))
        End If
        Try
            lvCp.Items(index_CP).Selected = False
            index_CP -= 1
            If index_CP < 0 Then
                index_CP = 0
                ' ElseIf lvChildpart.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvChildpart.Items.Count - 1)))
            End If
            lvCp.Items(index_CP).Selected = True
            lvCp.Items(index_CP).EnsureVisible()
            lvCp.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub tbnDownCP()
        If index_CP < 0 Then
            index_CP = 0
        ElseIf index_CP > CDbl(Val((lvCp.Items.Count - 1))) Then
            index_CP = CDbl(Val((lvCp.Items.Count - 1)))
        End If
        Try
            lvCp.Items(index_CP).Selected = False
            index_CP += 1
            If index_CP < 0 Then
                index_CP = 0
                ' ElseIf lvChildpart.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvChildpart.Items.Count - 1)))
            End If
            lvCp.Items(index_CP).Selected = True
            lvCp.Items(index_CP).EnsureVisible()
            lvCp.Select()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnFGDown_Click(sender As Object, e As EventArgs) Handles btnFGDown.Click
        tbnDownFG()
    End Sub

    Private Sub btnCPUP_Click(sender As Object, e As EventArgs) Handles btnCPUP.Click
        tbnUpCP()
    End Sub

    Private Sub btnCPDOWN_Click(sender As Object, e As EventArgs) Handles btnCPDOWN.Click
        tbnDownCP()
    End Sub
End Class