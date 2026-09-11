Imports System.Web.Script.Serialization

Public Class ManagePrintDefectAdmin
    Private isLoaded As Boolean = False
    Private defectJsonData As List(Of Dictionary(Of String, Object)) = Nothing
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub
    Private Sub ManagePrintDefectAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        combodfType()
        isLoaded = True
        loadDataDefect(1, Show_reprint_wi.hide_wi_select.Text, Scan_reprint.date_now_start, Scan_reprint.date_now_end)
    End Sub
    Public Sub combodfType()
        Dim myItems As New List(Of KeyValuePair(Of String, Integer)) From {
            New KeyValuePair(Of String, Integer)("NG", 1),
            New KeyValuePair(Of String, Integer)("NC", 2)
        }
        comboxitemtype.DataSource = New BindingSource(myItems, Nothing)
        comboxitemtype.DisplayMember = "Key"
        comboxitemtype.ValueMember = "Value"
        comboxitemtype.SelectedIndex = 0
    End Sub

    Public Sub loadDataDefect(df_item_type As String, df_wi As String, DateStart As String, DateEnd As String)
        Dim rsData = modelDefect.LoadDataTagDefect(df_item_type, df_wi, DateStart, DateEnd)
        If rsData <> "0" Then
            defectJsonData = New JavaScriptSerializer().Deserialize(Of List(Of Dictionary(Of String, Object)))(rsData)
            lvShowDataDefect.BeginUpdate()
            lvShowDataDefect.Items.Clear()
            lvShowDataDefect.Columns.Clear()
            lvShowDataDefect.View = View.Details
            lvShowDataDefect.Columns.Add("NO", 60)
            lvShowDataDefect.Columns.Add("Part NO", 190)
            lvShowDataDefect.Columns.Add("Lot No", 100)
            lvShowDataDefect.Columns.Add("SEQ", 80)
            lvShowDataDefect.Columns.Add("QTY", 80)
            lvShowDataDefect.Columns.Add("BOX", 80)
            lvShowDataDefect.Columns.Add("Shift", 80)
            Dim No As Integer = 0
            For Each item In defectJsonData
                No += 1
                Dim row As New ListViewItem(No.ToString())
                row.SubItems.Add(GetSafe(item, "dti_item_cd"))
                row.SubItems.Add(GetSafe(item, "dti_lot_no"))
                row.SubItems.Add(GetSafe(item, "dti_seq_no"))
                row.SubItems.Add(GetSafe(item, "dti_sum_qty"))
                row.SubItems.Add(GetSafe(item, "dti_box_no"))
                row.SubItems.Add(GetSafe(item, "pwi_shift"))
                lvShowDataDefect.Items.Add(row)
            Next
            lvShowDataDefect.EndUpdate()
        Else
            lvShowDataDefect.BeginUpdate()
            lvShowDataDefect.Items.Clear()
            lvShowDataDefect.EndUpdate()
            MessageBox.Show("No data found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub comboxitemtype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboxitemtype.SelectedIndexChanged
        If Not isLoaded Then Exit Sub

        If comboxitemtype.SelectedValue IsNot Nothing AndAlso TypeOf comboxitemtype.SelectedValue Is Integer Then
            Dim selectedValue As Integer = Convert.ToInt32(comboxitemtype.SelectedValue)
            loadDataDefect(selectedValue, Show_reprint_wi.hide_wi_select.Text, Scan_reprint.date_now_start, Scan_reprint.date_now_end)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If lvShowDataDefect.SelectedItems.Count = 0 Then
            MessageBox.Show("กรุณาเลือกข้อมูลที่ต้องการพิมพ์ก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim selectedIndex As Integer = lvShowDataDefect.SelectedIndices(0)
        If defectJsonData Is Nothing OrElse selectedIndex >= defectJsonData.Count Then
            MessageBox.Show("ไม่พบข้อมูล JSON ที่เกี่ยวข้องกับรายการนี้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim itemd = defectJsonData(selectedIndex)

        ' ดึงข้อมูลจาก JSON โดยตรง
        Dim dti_id = GetSafe(itemd, "dti_id")
        Dim item_cd = GetSafe(itemd, "dti_item_cd")
        Dim item_name = GetSafe(itemd, "ITEM_NAME")
        Dim model = GetSafe(itemd, "MODEL")
        Dim location_part = GetSafe(itemd, "LOCATION_PART")
        Dim sLine = GetSafe(itemd, "dti_line_cd")
        Dim stDatetime = GetSafe(itemd, "dti_created_date")
        Dim sShift = GetSafe(itemd, "pwi_shift")
        Dim factory_cd = GetSafe(itemd, "PLANT") ' หรือใส่เป็น "TBKK"
        Dim sLot = GetSafe(itemd, "dti_lot_no")
        Dim total_nc = GetSafe(itemd, "dti_sum_qty")
        Dim SEQ = GetSafe(itemd, "dti_seq_no")
        Dim wi = GetSafe(itemd, "dti_wi_no")
        Dim itemType = GetSafe(itemd, "dti_item_type")
        Dim dfType = comboxitemtype.SelectedValue.ToString()
        Dim Menu = GetSafe(itemd, "dti_menu")
        ' เรียกพิมพ์
        PrintDefectAdmin.Set_parameter_print(
            item_cd,
            item_name,
            model,
            sLine,
            stDatetime,
            location_part,
            sShift,
            factory_cd,
            sLot,
            total_nc,
            SEQ,
            wi,
            itemType,
            dfType,
            Menu
        )
        Dim rs = modelDefect.mUpdatePrintCountDefect(dti_id)
    End Sub

    Private Function GetSafe(dict As Dictionary(Of String, Object), key As String) As String
        If dict.ContainsKey(key) AndAlso dict(key) IsNot Nothing Then
            Return dict(key).ToString()
        Else
            Return ""
        End If
    End Function

End Class
