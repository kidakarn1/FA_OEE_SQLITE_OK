Imports System.Web.Script.Serialization
Public Class ManagePlan
    Public Shared MpartsNo As String = ""
    Public Shared Mqty As String = ""
    Public Shared Mdates As String = ""
    Public Shared Mseq As String = ""
    Public Shared Mline As String = ""
    Public Shared MkeyInfo As String = ""
    Public Shared MplantCode As String = ""
    Public Shared Mwi As String = ""
    Private Sub ManagePlan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ManagePlan()
        If MainFrm.rsCheckCriticalFlg = "1" Then
            pbQrCode.Visible = True
        Else
            pbQrCode.Visible = False
        End If
    End Sub
    Public Sub ManagePlan()
        Dim rs = Backoffice_model.Get_plan_production_critical()
        ListView1.Items.Clear()
        If rs <> " " Then
            Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
            Dim num As Integer = 0
            For Each item As Object In dict
                lv = New ListViewItem(num + 1)
                lv.SubItems.Add(item("WI").ToString())
                lv.SubItems.Add(item("ITEM_CD").ToString())
                Dim QTY_NC As Integer = 0
                Dim QTY_NG As Integer = 0
                Try
                    QTY_NC = item("QTY_NC").ToString()
                    QTY_NG = item("QTY_NG").ToString()
                Catch ex As Exception
                    QTY_NC = 0
                    QTY_NG = 0
                End Try
                lv.SubItems.Add(CDbl(Val(item("QTY").ToString())) - CDbl(Val(item("prd_qty_sum").ToString())) - QTY_NC - QTY_NG)
                ListView1.Items.Add(lv)
                num += 1
            Next
            SelectRow(0)
        End If
    End Sub
    Private Sub SelectRow(ByVal rowIndex As Integer)
        ' Clear any existing selections
        ListView1.SelectedItems.Clear()

        ' Check if the rowIndex is within valid range
        If rowIndex >= 0 AndAlso rowIndex < ListView1.Items.Count Then
            ' Select the specified row
            ListView1.Items(rowIndex).Selected = True

            ' Ensure the selected item is visible
            ListView1.EnsureVisible(rowIndex)
        End If
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub pbQrCode_Click(sender As Object, e As EventArgs) Handles pbQrCode.Click
        Me.Enabled = False
        ScanTagPlan.Show()
    End Sub
    Private Sub btnBack_Click_1(sender As Object, e As EventArgs) Handles btnBack.Click
        MainFrm.Enabled = True
        MainFrm.Show()
        Me.Close()
    End Sub
    Public Sub SelectPlan(wi)
        LoadSQL_prd_plan = Backoffice_model.GetDataPlanCritical(wi)
        Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(LoadSQL_prd_plan)
        If LoadSQL_prd_plan <> " " Then
            For Each item As Object In dict
                Working_Pro.Label27.Text = item("PS_UNIT_NUMERATOR").ToString()
                Prd_detail.lb_snp.Text = item("PS_UNIT_NUMERATOR").ToString()
                Prd_detail.lb_item_cd.Text = item("ITEM_CD").ToString()
                Prd_detail.lb_item_name.Text = CStr(item("ITEM_NAME").ToString())
                Prd_detail.lb_model.Text = item("MODEL").ToString()
                Prd_detail.lb_plan_qty.Text = item("QTY").ToString()
                Prd_detail.lb_remain_qty.Text = (item("QTY").ToString() - item("prd_qty_sum").ToString())
                Prd_detail.lb_wi.Text = item("WI").ToString()
                Prd_detail.LB_PLAN_DATE.Text = item("WORK_ODR_DLV_DATE").ToString().Substring(0, 10)
                MainFrm.ArrayDataPlan.Add(New DataPlan With {.IND_ROW = item("IND_ROW").ToString(), .PS_UNIT_NUMERATOR = "PS_UNIT_NUMERATOR", .CT = item("CT").ToString(), .seq_no = item("seq_no").ToString(), .WORK_ODR_DLV_DATE = item("WORK_ODR_DLV_DATE").ToString(), .LOCATION_PART = item("LOCATION_PART").ToString(), .MODEL = item("MODEL").ToString(), .PRODUCT_TYP = item("PRODUCT_TYP").ToString(), .wi = item("WI").ToString(), .item_cd = item("ITEM_CD").ToString(), .item_name = item("ITEM_NAME").ToString()})
                Dim seqPlan As String = ""
                If Len(item("order_flg")) = 1 Then
                    seqPlan = "00" & item("order_flg").ToString
                ElseIf Len(item("order_flg")) = 2 Then
                    seqPlan = "0" & item("order_flg").ToString
                Else
                    seqPlan = item("order_flg").ToString
                End If
                Mseq = seqPlan
                Mline = item("LINE_CD").ToString
                MkeyInfo = seqPlan
                MplantCode = item("PLANT").ToString
                Prd_detail.Show()
                ScanTagPlan.Close()
                Me.Close()
            Next
        Else
            ScanTagPlan.PictureBox9.Show()
            ScanTagPlan.Panel3.Show()
            ScanTagPlan.Label5.Show()
            ScanTagPlan.PictureBox11.Show()
            Dim listdetail = "Not have production plan !"
            ScanTagPlan.tbScanQr.Text = ""
            ScanTagPlan.PictureBox9.BringToFront()
            ScanTagPlan.PictureBox9.Show()
            ScanTagPlan.PictureBox11.BringToFront()
            ScanTagPlan.PictureBox11.Show()
            ScanTagPlan.Panel3.BringToFront()
            ScanTagPlan.Panel3.Show()
            ScanTagPlan.Label5.Text = listdetail
            ScanTagPlan.Label5.BringToFront()
            ScanTagPlan.Label5.Show()
            ScanTagPlan.Enabled = True
        End If
    End Sub
    Private Sub pbNext_Click(sender As Object, e As EventArgs) Handles pbNext.Click
        Try
            Dim sel_cd As Integer = ListView1.SelectedIndices(0)
            MpartsNo = ListView1.Items(sel_cd).SubItems(2).Text
            Mqty = ListView1.Items(sel_cd).SubItems(3).Text
            Mdates = ListView1.Items(sel_cd).SubItems(0).Text
            Mwi = ListView1.Items(sel_cd).SubItems(1).Text
            SelectPlan(Mwi)
        Catch ex As Exception
            MsgBox("Please Check Plan.")
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class