Imports System.Web.Script.Serialization

Public Class Lot_History
    Public Shared S_index As Integer = 0
    Private Sub pbBack_Click(sender As Object, e As EventArgs) Handles pbBack.Click
        Me.Close()
    End Sub
    Public Sub BTNUP1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvShowData.Items.Count - 1))) Then
            S_index = CDbl(Val((lvShowData.Items.Count - 1)))
        End If
        Try
            lvShowData.Items(S_index).Selected = False
            S_index -= 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf lvDefectact.Items.Count > S_index Then
                'S_index = CDbl(Val((lvDefectact.Items.Count - 1)))
            End If
            lvShowData.Items(S_index).Selected = True
            lvShowData.Items(S_index).EnsureVisible()
            lvShowData.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BTNDOWN1()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvShowData.Items.Count - 1))) Then
            S_index = CDbl(Val((lvShowData.Items.Count - 1)))
        End If
        Try
            lvShowData.Items(S_index).Selected = False
            S_index += 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf S_index > lvDefectact.Items.Count Then
                '  S_index = CDbl(Val((lvDefectact.Items.Count - 1)))
            End If
            lvShowData.Items(S_index).Selected = True
            lvShowData.Items(S_index).EnsureVisible()
            lvShowData.Select()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        BTNDOWN1()
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        BTNUP1()
    End Sub
    Public Sub loadDataHistory()
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Chang_Loss.ListView2.View = View.Details
                'Chang_Loss.ListView2.Scrollable = Size()
                'List_Emp.ListBox2.Items.Add(Trim(TextBox2.Text))
                Loss_reg.Label2.Text = MainFrm.Label4.Text
                Dim rm_wi = Prd_detail.lb_wi.Text
                Dim api = New api()
                Dim rm_lot_po = Prd_detail.Label6.Text
                Dim rm_shift = Prd_detail.Label12.Text.Substring(0, 1)
                Dim i As Integer = 1
                Dim result_data As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/GET_DATA_NEW_FA/Getrm_scan?rm_wi=" & rm_wi & "&rm_lot_po=" & rm_lot_po & "&rm_shift=" & rm_shift)
                Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/GET_DATA_NEW_FA/Getrm_scan?rm_wi=" & rm_wi & "&rm_lot_po=" & rm_lot_po & "&rm_shift=" & rm_shift)
                If result_data <> "0" Then
                    Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(result_data)
                    For Each item As Object In dict2
                        datlvDefectsumary = New ListViewItem(i)
                        datlvDefectsumary.SubItems.Add(item("rm_QR_code").ToString())
                        datlvDefectsumary.SubItems.Add(item("rm_created_date").ToString())
                        lvShowData.Items.Add(datlvDefectsumary)
                        i = i + 1
                    Next
                End If
            Else
                load_show.Show()
            End If
        Catch ex As Exception
            load_show.Show()
        End Try
    End Sub
    Private Sub Lot_History_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadDataHistory()
    End Sub
End Class