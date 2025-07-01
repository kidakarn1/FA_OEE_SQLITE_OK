Public Class ManagePrintDefectAdmin

    ' Flag ใช้ป้องกันการทำงานของ SelectedIndexChanged ตอนกำลัง Load ComboBox
    Private isLoaded As Boolean = False

    ' กดปุ่ม Back
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

    ' ตอนโหลดฟอร์ม
    Private Sub ManagePrintDefectAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        combodfType()
        isLoaded = True ' เปิดให้ SelectedIndexChanged ทำงานหลังโหลดเสร็จ
    End Sub

    ' โหลดข้อมูลให้ ComboBox
    Public Sub combodfType()
        comboxitemtype.Items.Clear()

        ' Key = ชื่อ, Value = รหัส
        Dim myItems As New List(Of KeyValuePair(Of String, Integer)) From {
            New KeyValuePair(Of String, Integer)("NC", 1),
            New KeyValuePair(Of String, Integer)("NG", 2)
        }

        ' Binding
        comboxitemtype.DataSource = New BindingSource(myItems, Nothing)
        comboxitemtype.DisplayMember = "Key"     ' แสดงคำว่า NC / NG
        comboxitemtype.ValueMember = "Value"     ' ค่าเป็น 1 หรือ 2
        comboxitemtype.SelectedIndex = 0         ' เลือกรายการแรกไว้ก่อน
    End Sub

    ' โหลดข้อมูล defect ตามประเภท
    Public Sub loadDataDefect(df_item_type As String, df_wi As String, DateStart As String, DateEnd As String)
        ' สามารถเปิดใช้งานเมื่อมี model เชื่อม DB:
        ' Dim rsData = modelDefect.LoadDataTagDefect(df_item_type, df_wi, DateStart, DateEnd)
    End Sub

    ' เมื่อเลือกประเภท defect
    Private Sub comboxitemtype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboxitemtype.SelectedIndexChanged
        If Not isLoaded Then Exit Sub ' ยังไม่โหลดเสร็จ ไม่ต้องทำงาน

        ' ป้องกัน error ถ้า SelectedValue ยังไม่ถูกต้อง
        If comboxitemtype.SelectedValue IsNot Nothing AndAlso TypeOf comboxitemtype.SelectedValue Is Integer Then
            Dim selectedValue As Integer = Convert.ToInt32(comboxitemtype.SelectedValue)
            Dim selectedText As String = comboxitemtype.Text

            MessageBox.Show("คุณเลือก: " & selectedText & " (Key = " & selectedValue & ")")

            ' เรียกโหลดข้อมูล defect
            loadDataDefect(selectedText, Show_reprint_wi.hide_wi_select.Text, Scan_reprint.date_now_start, Scan_reprint.date_now_end)
        End If
    End Sub

    ' ปุ่มอื่น (ว่าง)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' กำหนดโค้ดตามต้องการ
    End Sub

End Class
