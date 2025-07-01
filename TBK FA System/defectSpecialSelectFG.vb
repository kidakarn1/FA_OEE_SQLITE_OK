Public Class defectSpecialSelectFG
    Public Shared dtItemcd As String = "NO DATA"
    Public Shared dtwi As String = "NO DATA"
    Public Shared dtseqSpc As String = "NO DATA"
    Public Shared dtpwiSpc As String = "NO DATA"
    Private Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        Dim objdSelecttype As New defectSelecttype()
        objdSelecttype.Show()
        Me.Close()
    End Sub
    Private Sub btn_next_Click(sender As Object, e As EventArgs) Handles btn_next.Click
        Try
            If lvDefectFGSpc.SelectedItems.Count > 0 Then
                For Each lvItem As ListViewItem In lvDefectFGSpc.SelectedItems
                    Me.dtItemcd = lvDefectFGSpc.Items(lvItem.Index).SubItems(2).Text
                    Me.dtwi = lvDefectFGSpc.Items(lvItem.Index).SubItems(1).Text
                    Me.dtseqSpc = lvDefectFGSpc.Items(lvItem.Index).SubItems(4).Text
                    Me.dtpwiSpc = lvDefectFGSpc.Items(lvItem.Index).SubItems(5).Text
                Next
                ' Dim dfNumpadafjust = New defectNumpadadjust
                'dfNumpadafjust.Show()
                Dim sDefectcode As New defectSelectcode()
                sDefectcode.sPart = dtItemcd
                sDefectcode.swi = Me.dtwi
                sDefectcode.sSeqSpc = Me.dtseqSpc
                sDefectcode.sPwiSpc = Me.dtpwiSpc
                sDefectcode.Show()
                Me.Close()
            Else
                MsgBox("Please Select Data")
            End If
        Catch ex As Exception
            MsgBox("Please Select Data")
        End Try
    End Sub
    Private Sub defectSpecialSelectFG_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim arrData0 As DataPlan = MainFrm.ArrayDataPlan(0)
        Dim arrData1 As DataPlan = MainFrm.ArrayDataPlan(1)
        Dim arrData2 As DataPlan = MainFrm.ArrayDataPlan(2)
        Dim arrData3 As DataPlan = MainFrm.ArrayDataPlan(3)
        Dim arrData4 As DataPlan = MainFrm.ArrayDataPlan(4)
        Dim i As Integer = 1
        Dim GenSEQ As Integer = CDbl(Val(Working_Pro.Label22.Text)) - MainFrm.ArrayDataPlan.ToArray.Length
        Dim Iseq = GenSEQ
        Dim j As Integer = 0
        For Each itemPlanData As DataPlan In MainFrm.ArrayDataPlan
            Iseq += 1
            datlvDefectdetails = New ListViewItem(i)
            datlvDefectdetails.SubItems.Add(itemPlanData.wi)
            datlvDefectdetails.SubItems.Add(itemPlanData.item_cd)
            datlvDefectdetails.SubItems.Add(itemPlanData.item_name)
            datlvDefectdetails.SubItems.Add(Iseq) 'seq
            datlvDefectdetails.SubItems.Add(Working_Pro.Spwi_id(j)) 'pwi_id
            lvDefectFGSpc.Items.Add(datlvDefectdetails)
            i = i + 1
            j = j + 1
        Next
        If lvDefectFGSpc.Items.Count > 0 Then
            lvDefectFGSpc.Items(0).Selected = True
        End If
    End Sub
End Class