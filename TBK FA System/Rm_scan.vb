Imports System.Globalization

Public Class Rm_scan
    Dim x As ListViewItem
    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Me.Hide()
    End Sub
    Public Sub set_child_wi(wi)
        'ListView_material.Items.Clear()
        Dim api = New api()
        Dim i As Integer = 1
        Dim reader = Backoffice_model.get_list_rm_scan(wi)
        While reader.read()
            x = New ListViewItem(i)
            ' If CDbl(Val(Module1.get_Remain_PLAN())) <> "0" Then
            x.SubItems.Add((reader("ITEM_CD").ToString()))
            x.SubItems.Add((reader("ITEM_NAME").ToString()))
            x.SubItems.Add((reader("QTY").ToString()))
            'Else
            ' x.SubItems.Add(reader("QTY").ToString())
            ' End If
            'x.SubItems.Add("0")
            'ListView_material.Items.Add(x)
            i += 1
        End While
        reader.close()
    End Sub
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        set_child_wi(Prd_detail.lb_wi.Text)
    End Sub

    Public Sub check_qr_code_picking()
        Dim arr_ITEM_CD = scan_item_cd.Text.Split(" ")
        Dim Get_data_picking As String = Backoffice_model.Get_data_picking(arr_ITEM_CD(21))
        Dim date_st As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim WI = Prd_detail.lb_wi.Text
        Dim ITEM_CD2 = arr_ITEM_CD(3)
        Dim LOT_PO = Get_data_picking.Substring(2, 10)
        Dim SEQ = Get_data_picking.Substring(59, 3)
        Dim SHIFT = Prd_detail.Label12.Text
        Dim Rm_created_date = date_st
        Dim Rm_created_by = " "
        Dim Rm_Updated_date = date_st
        Dim Rm_updated_by = " "
        Dim Rm_line_cd = Prd_detail.Label3.Text
        Dim Rm_QR_code = scan_item_cd.Text
        'MsgBox("arr_ITEM_CD(21) = " & arr_ITEM_CD(21))
        Backoffice_model.Insert_Rm_Scan(WI, ITEM_CD2, LOT_PO, SEQ, SHIFT, Rm_created_date, Rm_created_by, Rm_Updated_date, Rm_updated_by, Rm_line_cd, Rm_QR_code, arr_ITEM_CD(21))
        MsgBox("OK")
    End Sub
    Public Sub check_qr_code()
        insertMutidigit(scan_item_cd.Text)
    End Sub
    Public Sub insertMutidigit(dataTag As String)
        If scan_item_cd.Text <> "" Then
            Dim dataRM = model_rm_scan.GetDataRM_DATABASE_FA(dataTag)
            Dim date_st As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s", CultureInfo.InvariantCulture)
            Dim result_date_start As Date = Date.Parse(date_st)
            date_st = result_date_start.ToString("yyyy/MM/dd H:m:s", CultureInfo.InvariantCulture)
            Dim WI = Prd_detail.lb_wi.Text
            Dim ITEM_CD2 = Prd_detail.lb_item_cd.Text
            Dim LOT_PO = Prd_detail.Label6.Text
            Dim SEQ = "-" 'dataTag.Substring(86, 3)
            Dim SHIFT = Prd_detail.Label12.Text
            Dim Rm_created_date = date_st
            Dim Rm_created_by = Prd_detail.Label3.Text
            Dim Rm_Updated_date = ""
            Dim Rm_updated_by = ""
            Dim Rm_line_cd = Prd_detail.Label3.Text
            Dim Rm_QR_code = dataTag
            'MsgBox("arr_ITEM_CD(21) = " & arr_ITEM_CD(21))
            Backoffice_model.Insert_Rm_Scan(WI, ITEM_CD2, LOT_PO, SEQ, SHIFT, Rm_created_date, Rm_created_by, Rm_Updated_date, Rm_updated_by, Rm_line_cd, Rm_QR_code, "-")
            MsgBox("OK")
            keyboardRm_Scan.Close()
        Else
            MsgBox("Plase Check Tag.")
        End If
        scan_item_cd.Text = ""
        scan_item_cd.Focus()
    End Sub
    Public Sub Insert_TAG_103(dataTag As String)
        Dim arr_ITEM_CD = scan_item_cd.Text.Split(" ")
        Dim dataRM = model_rm_scan.GetDataRM_DATABASE_FA(dataTag)
        Dim date_st As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s", CultureInfo.InvariantCulture)
        Dim result_date_start As Date = Date.Parse(date_st)
        date_st = result_date_start.ToString("yyyy/MM/dd H:m:s", CultureInfo.InvariantCulture)
        Dim WI = Prd_detail.lb_wi.Text
        Dim ITEM_CD2 = Prd_detail.lb_item_cd.Text
        Dim LOT_PO = Prd_detail.Label6.Text
        Dim SEQ = " - " 'dataTag.Substring(86, 3)
        Dim SHIFT = Prd_detail.Label12.Text
        Dim Rm_created_date = date_st
        Dim Rm_created_by = " "
        Dim Rm_Updated_date = date_st
        Dim Rm_updated_by = " "
        Dim Rm_line_cd = Prd_detail.Label3.Text
        Dim Rm_QR_code = scan_item_cd.Text
        'MsgBox("arr_ITEM_CD(21) = " & arr_ITEM_CD(21))
        Backoffice_model.Insert_Rm_Scan(WI, ITEM_CD2, LOT_PO, SEQ, SHIFT, Rm_created_date, Rm_created_by, Rm_Updated_date, Rm_updated_by, Rm_line_cd, Rm_QR_code, arr_ITEM_CD(21))
        MsgBox("OK")
    End Sub
    Public Sub Insert_TAG_62(dataTag As String)
        Dim arr_ITEM_CD = scan_item_cd.Text.Split(" ")
        Dim dataRM = model_rm_scan.GetDataRM_DATABASE_FA(dataTag)
        Dim date_st As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
        Dim WI = Prd_detail.lb_wi.Text
        Dim ITEM_CD2 = Prd_detail.lb_item_cd.Text
        Dim LOT_PO = Prd_detail.Label6.Text
        Dim SEQ = " - "
        Dim SHIFT = Prd_detail.Label12.Text
        Dim Rm_created_date = date_st
        Dim Rm_created_by = " "
        Dim Rm_Updated_date = date_st
        Dim Rm_updated_by = " "
        Dim Rm_line_cd = Prd_detail.Label3.Text
        Dim Rm_QR_code = scan_item_cd.Text
        'MsgBox("arr_ITEM_CD(21) = " & arr_ITEM_CD(21))
        Backoffice_model.Insert_Rm_Scan(WI, ITEM_CD2, LOT_PO, SEQ, SHIFT, Rm_created_date, Rm_created_by, Rm_Updated_date, Rm_updated_by, Rm_line_cd, Rm_QR_code, arr_ITEM_CD(21))
        MsgBox("OK")
    End Sub
    Private Sub scan_item_cd_KeyDown(sender As Object, e As KeyEventArgs) Handles scan_item_cd.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Enter
                Try
                    'Dim arr_ITEM_CD = scan_item_cd.Text.Split(" ")
                    ' If arr_ITEM_CD(2) = Prd_detail.lb_wi.Text Then
                    check_qr_code()
                    '  Else
                    ' MsgBox("กรุณาตรวจสอบ WI ของท่านที่นำมาสแกน")
                    ' scan_item_cd.Text = ""
                    '  End If
                    'If scan_item_cd.Text.Length = 62 Then
                    'Dim ITEM_CD As String = scan_item_cd.Text.Substring(12)
                    'Dim arr_ITEM_CD = ITEM_CD.Split(" ")
                    'Dim reader = Backoffice_model.get_list_rm_scan(Prd_detail.lb_wi.Text)
                    'Dim check_data As Integer = 0
                    'While reader.read()
                    'MsgBox("ITEM CD ====>" & reader("ITEM_CD").ToString())
                    'If reader("ITEM_CD").ToString() = arr_ITEM_CD(0) Then
                    'check_data = 1
                    '	GoTo break_loop
                    'Else
                    'check_data = 0
                    'End If
                    'End While
                    'break_loop:
                    '	If check_data = 1 Then
                    'Dim date_st As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
                    'Dim WI = Prd_detail.lb_wi.Text
                    'Dim ITEM_CD2 = arr_ITEM_CD(0)
                    'Dim LOT_PO = scan_item_cd.Text.Substring(2, 10)
                    'Dim SEQ = scan_item_cd.Text.Substring(59, 3)
                    'Dim SHIFT = Prd_detail.Label12.Text
                    'Dim Rm_created_date = date_st
                    'Dim Rm_created_by = " "
                    'Dim Rm_Updated_date = date_st
                    'Dim Rm_updated_by = " "
                    'Dim Rm_line_cd = Prd_detail.Label3.Text
                    'Dim Rm_QR_code = scan_item_cd.Text
                    'Backoffice_model.Insert_Rm_Scan(WI, ITEM_CD2, LOT_PO, SEQ, SHIFT, Rm_created_date, Rm_created_by, Rm_Updated_date, Rm_updated_by, Rm_line_cd, Rm_QR_code)
                    'MsgBox("OK")
                    'scan_item_cd.Text = ""
                    'scan_item_cd.Focus()
                    'Else
                    'MsgBox("กรุณา นำ PART / FW มาผลิตให้ถูกต้อง")
                    'scan_item_cd.Text = ""
                    'scan_item_cd.Focus()
                    'End If
                    'ElseIf scan_item_cd.Text.Length = 103 Then
                    'Dim ITEM_CD As String = scan_item_cd.Text.Substring(19)
                    'Dim arr_ITEM_CD = ITEM_CD.Split(" ")
                    'Dim reader = Backoffice_model.get_list_rm_scan(Prd_detail.lb_wi.Text)
                    'Dim check_data As Integer = 0
                    'While reader.read
                    'If reader("ITEM_CD").ToString() = arr_ITEM_CD(0) Then
                    'check_data = 1
                    'GoTo break_loop1
                    'Else
                    'check_data = 0
                    'End If
                    'End While
                    'break_loop1:
                    'If check_data = 1 Then
                    'Dim date_st As String = DateTime.Now.ToString("yyyy/MM/dd H:m:s")
                    'Dim WI = Prd_detail.lb_wi.Text
                    '	Dim ITEM_CD2 = Prd_detail.lb_item_cd.Text
                    '	Dim LOT_PO = scan_item_cd.Text.Substring(58, 4)
                    '	Dim SEQ = scan_item_cd.Text.Substring(100, 3)
                    '	Dim SHIFT = Prd_detail.Label12.Text
                    '	Dim Rm_created_date = date_st
                    '	Dim Rm_created_by = " "
                    '	Dim Rm_Updated_date = date_st
                    '	Dim Rm_updated_by = " "
                    '	Dim Rm_line_cd = Prd_detail.Label3.Text
                    '	Dim Rm_QR_code = scan_item_cd.Text
                    '	Backoffice_model.Insert_Rm_Scan(WI, ITEM_CD2, LOT_PO, SEQ, SHIFT, Rm_created_date, Rm_created_by, Rm_Updated_date, Rm_updated_by, Rm_line_cd, Rm_QR_code)
                    '	MsgBox("OK")
                    '	scan_item_cd.Text = ""
                    '	Else
                    '	MsgBox("กรุณา นำ PART / FW มาผลิตให้ถูกต้อง")
                    '	scan_item_cd.Text = ""
                    '	scan_item_cd.Focus()
                    '	End If
                    '	Else
                    '	MsgBox("กรุณากรอก QR CODE ให้ถูกต้อง")
                    '	scan_item_cd.Text = ""
                    '	End If
                Catch ex As Exception
                    MsgBox("กรุณากรอก QR CODE ให้ถูกต้อง")
                    scan_item_cd.Text = ""
                End Try
        End Select
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        keyboardRm_Scan.showDialog
    End Sub
End Class
