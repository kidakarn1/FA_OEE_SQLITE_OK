Imports System.Web.Script.Serialization

Public Class ManagePrintLable

    Private isLoaded As Boolean = False
    Private defectJsonData As List(Of Dictionary(Of String, Object)) = Nothing

    ' สมมุติว่ามี POSITION1 และ POSITION2 ที่ต้องกำหนดค่าเอง หรือดึงจาก ListView
    Private GPOSITION1 As String = ""
    Private GPOSITION2 As String = ""

    Private Sub ManagePrintLable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isLoaded = True
        loadDataLable(Show_reprint_wi.hide_wi_select.Text)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If lvShowDataDefect.SelectedItems.Count = 0 Then
            MessageBox.Show("กรุณาเลือกแถวข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedIndex = lvShowDataDefect.SelectedIndices(0)
        If defectJsonData Is Nothing OrElse selectedIndex >= defectJsonData.Count Then
            MessageBox.Show("ไม่พบข้อมูล JSON ที่เกี่ยวข้อง", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim item = defectJsonData(selectedIndex)

        ' ข้อมูลจาก ListView
        Dim selectedRow = lvShowDataDefect.SelectedItems(0)
        Dim part_no = selectedRow.SubItems(1).Text
        Dim lotNo = selectedRow.SubItems(2).Text
        Dim seq = selectedRow.SubItems(3).Text
        GPOSITION1 = selectedRow.SubItems(4).Text
        GPOSITION2 = selectedRow.SubItems(5).Text
        Dim break = GPOSITION1 & " " & GPOSITION2

        ' กำหนด breakname หรือ logic อื่นๆ ได้ที่นี่
        Dim breakname = ""

        ' ✅ ดึง number_qty จาก JSON แทน
        Dim number_qty = GetSafe(item, "number_qty")

        ' เรียกหน้าจอพิมพ์
        Dim plb = New PrintLabelBreak()
        plb.loadData(part_no, break, lotNo, seq, number_qty)
    End Sub


    Public Sub loadDataLable(wi As String)
        Dim rsData = modelSpecialLine.LoadDataLable(wi)
        If rsData <> "0" Then
            defectJsonData = New JavaScriptSerializer().Deserialize(Of List(Of Dictionary(Of String, Object)))(rsData)
            lvShowDataDefect.BeginUpdate()
            lvShowDataDefect.Items.Clear()
            lvShowDataDefect.Columns.Clear()
            lvShowDataDefect.View = View.Details
            lvShowDataDefect.Columns.Add("NO", 55)
            lvShowDataDefect.Columns.Add("Part NO", 160)
            lvShowDataDefect.Columns.Add("Lot No", 90)
            lvShowDataDefect.Columns.Add("SEQ", 75)
            lvShowDataDefect.Columns.Add("POSITION1", 150)
            lvShowDataDefect.Columns.Add("POSITION2", 150)

            Dim No As Integer = 0
            For Each item In defectJsonData
                No += 1
                Dim row As New ListViewItem(GetSafe(item, "number_qty"))
                row.SubItems.Add(GetSafe(item, "item_cd"))
                row.SubItems.Add(GetSafe(item, "pwi_lot_no"))
                row.SubItems.Add(GetSafe(item, "seq_no"))
                row.SubItems.Add(GetSafe(item, "POSITION1"))
                row.SubItems.Add(GetSafe(item, "POSITION2"))
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

    Private Function GetSafe(dict As Dictionary(Of String, Object), key As String) As String
        If dict.ContainsKey(key) AndAlso dict(key) IsNot Nothing Then
            Return dict(key).ToString()
        Else
            Return ""
        End If
    End Function

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

End Class
