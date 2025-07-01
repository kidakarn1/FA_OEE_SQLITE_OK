Imports System.Globalization
Imports System.Web.Script.Serialization
Public Class print_back
	Dim QR_Generator As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
	Private Sub print_back_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		print()
	End Sub
	Public Function print()
        Dim api = New api()
        PrintDocument1.Print()
    End Function
    Public Function check_tagprint_main()
        Dim result_snp As Integer = CDbl(Val(Working_Pro.Label6.Text)) Mod CDbl(Val(Working_Pro.Label27.Text))
        Dim flg_control As Integer = 0
        If result_snp = "0" Then
            flg_control = 1
        End If
        Return flg_control
    End Function
    Public Sub oldM83_batch(e)
        Dim aPen = New Pen(Color.Black)
        aPen.Width = 2.0F
        'vertical ตรง
        e.Graphics.DrawLine(aPen, 10, 10, 10, 290)

        e.Graphics.DrawLine(aPen, 330, 58, 330, 95)

        e.Graphics.DrawLine(aPen, 460, 10, 460, 290)

        e.Graphics.DrawLine(aPen, 580, 10, 580, 290) 'QTY

        e.Graphics.DrawLine(aPen, 110, 10, 110, 290) 'QR right top

        e.Graphics.DrawLine(aPen, 700, 10, 700, 290)
        'Horizontal นอน

        e.Graphics.DrawLine(aPen, 110, 58, 580, 58)

        e.Graphics.DrawLine(aPen, 110, 95, 700, 95)

        e.Graphics.DrawLine(aPen, 10, 11, 700, 11)

        e.Graphics.DrawLine(aPen, 10, 110, 110, 110)

        e.Graphics.DrawLine(aPen, 10, 220, 110, 220)

        e.Graphics.DrawLine(aPen, 580, 67, 700, 67)

        e.Graphics.DrawLine(aPen, 460, 140, 700, 140)

        e.Graphics.DrawLine(aPen, 460, 190, 700, 190)

        e.Graphics.DrawLine(aPen, 460, 240, 580, 240)

        e.Graphics.DrawLine(aPen, 10, 289, 700, 289)
        'DATA
        e.Graphics.DrawString("TBKK", lb_font1.Font, Brushes.Black, 19, 15)
        e.Graphics.DrawString("WI : " & Prd_detail.lb_wi.Text, Label_wi_type.Font, Brushes.Black, 15, 60)
        If Working_Pro.lb_prd_type.Text = "10" Then
            prdtype = "PART TYPE : FG"
        ElseIf Working_Pro.lb_prd_type.Text = "40" Then
            prdtype = "PART TYPE : Parts"
        Else
            prdtype = "PART TYPE : FW"
        End If
        e.Graphics.DrawString(prdtype, Label_wi_type.Font, Brushes.Black, 15, 85)
        e.Graphics.DrawString("Instr. Code", lb_font5.Font, Brushes.Black, 120, 15)
        e.Graphics.DrawString(Working_Pro.Label3.Text & "     " & Working_Pro.Label12.Text, lb_font4_B.Font, Brushes.Black, 120, 35)
        Dim result_snp As Integer = CDbl(Val(Working_Pro.Label6.Text)) Mod CDbl(Val(Working_Pro.Label27.Text))
        If Working_Pro.V_check_line_reprint = "0" Then
            If result_snp = "0" Then
                result_snp = Working_Pro.Label27.Text
            End If
        End If
        e.Graphics.DrawString("QTY.", lb_font3.Font, Brushes.Black, 465, 15)
        e.Graphics.DrawString(result_snp, LB_QTY.Font, Brushes.Black, 495, 28)
        e.Graphics.DrawString("MODEL", lb_font5.Font, Brushes.Black, 120, 60)
        e.Graphics.DrawString(Working_Pro.lb_model.Text, lb_font4_B.Font, Brushes.Black, 140, 75)
        e.Graphics.DrawString("NEXT PROCESS", lb_font5.Font, Brushes.Black, 340, 60)
        e.Graphics.DrawString("ISUZU", lb_font4_B.Font, Brushes.Black, 360, 75)
        e.Graphics.DrawString("LOCATION", lb_font5.Font, Brushes.Black, 465, 60)
        e.Graphics.DrawString("D4U10A1", lb_font4_B.Font, Brushes.Black, 485, 75)
        Dim num_box_seq As Integer
        num_box_seq = Working_Pro.lb_box_count.Text.Length
        Dim plan_seq As String
        Dim num_char_seq As Integer
        num_char_seq = Working_Pro.Label22.Text.Length
        If num_char_seq = 1 Then
            plan_seq = "00" & Working_Pro.Label22.Text
        ElseIf num_char_seq = 2 Then
            plan_seq = "0" & Working_Pro.Label22.Text
        Else
            plan_seq = Working_Pro.Label22.Text
        End If
        e.Graphics.DrawString("SHIFT", lb_font5.Font, Brushes.Black, 585, 69)
        e.Graphics.DrawString(Working_Pro.Label14.Text, lb_font4_B.Font, Brushes.Black, 625, 75)
        e.Graphics.DrawString("LINE", lb_font5.Font, Brushes.Black, 467, 98)
        e.Graphics.DrawString(MainFrm.Label4.Text, lb_font4_B.Font, Brushes.Black, 470, 115)
        e.Graphics.DrawString("PRO SEQ", lb_font5.Font, Brushes.Black, 585, 98)
        e.Graphics.DrawString(plan_seq, lb_font4_B.Font, Brushes.Black, 610, 115)
        Dim api = New api()
        Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(Working_Pro.load_data)
        Dim plan_date As String = ""
        For Each item As Object In dict
            Dim da As Date = item("WORK_ODR_DLV_DATE").substring(0, 10)
            Dim f As Date = Date.ParseExact(item("WORK_ODR_DLV_DATE").substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture)
            result_plan_date = f.ToString("dd/MM/yyyy")
            plan_date = result_plan_date
        Next
        Dim the_Label_bach As String
        If Trim(Len(Working_Pro.Label_bach.Text)) = 1 Then
            the_Label_bach = "00" & Working_Pro.Label_bach.Text
        ElseIf Trim(Len(Working_Pro.Label_bach.Text)) = 2 Then
            the_Label_bach = "0" & Working_Pro.Label_bach.Text
        Else
            the_Label_bach = Working_Pro.Label_bach.Text
        End If
        e.Graphics.DrawString("PLAN DATE", lb_font5.Font, Brushes.Black, 465, 145)
        e.Graphics.DrawString(plan_date, lb_font4_B.Font, Brushes.Black, 470, 162)
        e.Graphics.DrawString("BATCH NO", lb_font5.Font, Brushes.Black, 585, 145)
        e.Graphics.DrawString(the_Label_bach, lb_font4_B.Font, Brushes.Black, 610, 162)
        e.Graphics.DrawString("ACTUAL DATE", lb_font5.Font, Brushes.Black, 465, 195)
        e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), lb_font4_B.Font, Brushes.Black, 470, 215)
        e.Graphics.DrawString("LOT NO", lb_font5.Font, Brushes.Black, 465, 245)
        e.Graphics.DrawString(Working_Pro.Label18.Text, lb_font4_B.Font, Brushes.Black, 485, 265)
        e.Graphics.DrawString("NO.", lb_font5.Font, Brushes.Black, 120, 105)
        e.Graphics.DrawString("Part No.", lb_font5.Font, Brushes.Black, 160, 105)
        e.Graphics.DrawString("Part Name.", lb_font5.Font, Brushes.Black, 250, 105)
        e.Graphics.DrawString("QTY", lb_font5.Font, Brushes.Black, 420, 105)
        e.Graphics.DrawLine(aPen, 110, 123, 460, 123)
        e.Graphics.DrawLine(aPen, 110, 210, 460, 210)
        Dim margin_left_no = 125
        Dim margin_top_no = 130
        Dim margin_left_item_cd = 160
        Dim margin_left_part_name = 250
        Dim margin_left_QTY = 425
        Dim arr_item_cd() As String = {"898244-6240", "898244-6250", "898244-6260", "898244-6270", "898244-6280"}
        Dim arr_qr_code_sub() As String = {"01", "02", "03", "04", "05"}
        For i = 1 To 5 Step 1
            e.Graphics.DrawString(i, lb_font5.Font, Brushes.Black, margin_left_no, margin_top_no)
            e.Graphics.DrawString(arr_item_cd(i - 1), lb_font5.Font, Brushes.Black, margin_left_item_cd, margin_top_no)
            e.Graphics.DrawString("BRACKET ASM;CAM NO" & i, lb_font5.Font, Brushes.Black, margin_left_part_name, margin_top_no)
            e.Graphics.DrawString(result_snp.ToString(), lb_font5.Font, Brushes.Black, margin_left_QTY, margin_top_no)
            margin_top_no += 15
        Next
        Dim qrcode As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
        qrcode.QRCodeScale = 10
        Dim bitmap_qr_box As Bitmap = qrcode.Encode("TEST")
        Dim qr_by_model = 120
        Dim qr_by_model_left = 118
        Dim iden_cd As String
        'If MainFrm.Label6.Text = "K1PD01" Then
        iden_cd = "GB"
        ' Else
        'iden_cd = "GB"
        ' End If
        Dim part_no_res1 As String
        Dim part_no_res As String
        Dim part_numm As Integer = 0
        Dim act_date As String
        Dim actdateConv As Date = DateTime.Now.ToString("dd/MM/yyyy")
        act_date = Format(actdateConv, "yyyyMMdd")
        Dim qty_num As String
        Dim num_char_qty As Integer
        num_char_qty = Len(Trim(result_snp)) 'Label27.Text.Length 'lb_qty_for_box.Text.Length
        If num_char_qty = 1 Then
            qty_num = "     " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 2 Then
            qty_num = "    " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 3 Then
            qty_num = "   " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 4 Then
            qty_num = "  " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 5 Then
            qty_num = " " & result_snp 'lb_qty_for_box.Text
        Else
            qty_num = result_snp 'lb_qty_for_box.Text
        End If
        Dim cus_part_no As String = "                         "
        Dim plan_cd As String
        Dim factory_cd As String
        If MainFrm.Label6.Text = "K2PD06" Then
            factory_cd = "Phase8"
            plan_cd = "52"
        Else
            factory_cd = "Phase10"
            plan_cd = "51"
        End If
        Dim box_no As String
        Dim num_char_box As Integer
        num_char_box = Working_Pro.lb_box_count.Text.Length
        If num_char_box = 1 Then
            box_no = "00" & Working_Pro.lb_box_count.Text
        ElseIf num_char_box = 2 Then
            box_no = "0" & Working_Pro.lb_box_count.Text
        Else
            box_no = Working_Pro.lb_box_count.Text
        End If
        plan_date = plan_date.Substring(6, 4) & plan_date.Substring(3, 2) & plan_date.Substring(0, 2)
        For j = 1 To 5 Step 1
            part_no_res = ""
            Dim part_no_sub As String = arr_item_cd(j - 1)
            For part_numm = part_no_sub.Length To 24
                part_no_res = part_no_res & " "
            Next part_numm
            part_no_res1 = arr_item_cd(j - 1) & part_no_res
            bitmap_qr_box = QR_Generator.Encode(iden_cd & Working_Pro.Label24.Text & plan_date & plan_seq & part_no_res1 & act_date & qty_num & Working_Pro.Label18.Text & cus_part_no & act_date & plan_seq & plan_cd & the_Label_bach)
            e.Graphics.DrawString("QR No ." & j, lb_font5.Font, Brushes.Black, qr_by_model_left, 215)
            e.Graphics.DrawImage(bitmap_qr_box, qr_by_model, 233, 50, 50) 'Right top
            qr_by_model += 70
            margin_top_no += 15
            qr_by_model_left += 70
            arr_qr_code_sub(j - 1) = iden_cd & Working_Pro.Label24.Text & plan_date & plan_seq & part_no_res1 & act_date & qty_num & Working_Pro.Label18.Text & cus_part_no & act_date & plan_seq & plan_cd & the_Label_bach
        Next
        part_no_res = ""
        For part_numm = Working_Pro.Label3.Text.Length To 24
            part_no_res = part_no_res & " "
        Next part_numm
        part_no_res1 = Working_Pro.Label3.Text & part_no_res
        Dim qr_code As String = iden_cd & Working_Pro.Label24.Text & plan_date & plan_seq & part_no_res1 & act_date & qty_num & Working_Pro.Label18.Text & cus_part_no & act_date & plan_seq & plan_cd & the_Label_bach
        bitmap_qr_box = QR_Generator.Encode(qr_code)
        e.Graphics.DrawImage(bitmap_qr_box, 15, 120, 90, 90) 'left
        e.Graphics.DrawImage(bitmap_qr_box, 615, 15, 45, 45) 'Right top
        e.Graphics.DrawString("FACTORY", lb_font3.Font, Brushes.Black, 15, 230)
        e.Graphics.DrawString("Phase10", lb_font3.Font, Brushes.Black, 33, 250)
        e.Graphics.DrawImage(bitmap_qr_box, 600, 205, 75, 75) 'Right top
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Backoffice_model.Insert_tag_print_main(Working_Pro.wi_no.Text, qr_code, the_Label_bach, 1, plan_seq, Working_Pro.Label14.Text, check_tagprint_main(), Working_Pro.Label3.Text, Working_Pro.pwi_id, Working_Pro.tag_group_no)
                model_api_sqlite.mas_Insert_tag_print_main(Working_Pro.wi_no.Text, qr_code, the_Label_bach, 1, plan_seq, Working_Pro.Label14.Text, check_tagprint_main(), Working_Pro.Label3.Text, Working_Pro.pwi_id, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "0", Working_Pro.Label18.Text)
                Dim id_tag = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_ID_PRINT_DETAIL_MAIN?qr_code=" & qr_code)
                For K = 1 To 5 Step 1
                    Backoffice_model.Insert_tag_print_sub(id_tag, MainFrm.Label4.Text, arr_qr_code_sub(K - 1), Working_Pro.wi_no.Text, Working_Pro.tag_group_no)
                    model_api_sqlite.mas_Insert_tag_print_sub(id_tag, MainFrm.Label4.Text, arr_qr_code_sub(K - 1), Working_Pro.wi_no.Text, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "0", Working_Pro.Label18.Text)
                Next
            Else
                Dim id_tag = model_api_sqlite.mas_get_tag_print_detail_main(qr_code)
                model_api_sqlite.mas_Insert_tag_print_main(Working_Pro.wi_no.Text, qr_code, the_Label_bach, 1, plan_seq, Working_Pro.Label14.Text, check_tagprint_main(), Working_Pro.Label3.Text, Working_Pro.pwi_id, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "0", Working_Pro.Label18.Text)
                For K = 1 To 5 Step 1
                    model_api_sqlite.mas_Insert_tag_print_sub(id_tag, MainFrm.Label4.Text, arr_qr_code_sub(K - 1), Working_Pro.wi_no.Text, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "0", Working_Pro.Label18.Text)
                Next
            End If
        Catch ex As Exception
            Dim id_tag = model_api_sqlite.mas_get_tag_print_detail_main(qr_code)
            model_api_sqlite.mas_Insert_tag_print_main(Working_Pro.wi_no.Text, qr_code, the_Label_bach, 1, plan_seq, Working_Pro.Label14.Text, check_tagprint_main(), Working_Pro.Label3.Text, Working_Pro.pwi_id, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "0", Working_Pro.Label18.Text)
            For K = 1 To 5 Step 1
                model_api_sqlite.mas_Insert_tag_print_sub(id_tag, MainFrm.Label4.Text, arr_qr_code_sub(K - 1), Working_Pro.wi_no.Text, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "0", Working_Pro.Label18.Text)
            Next
        End Try
    End Sub
    Public Async Function newM83_batch(e) As Task
        Dim hasError As Boolean = False
        Dim aPen = New Pen(Color.Black)
        aPen.Width = 2.0F
        'vertical ตรง
        e.Graphics.DrawLine(aPen, 10, 10, 10, 290)
        e.Graphics.DrawLine(aPen, 280, 58, 280, 116) ' model
        e.Graphics.DrawLine(aPen, 460, 10, 460, 58) ' line qr
        e.Graphics.DrawLine(aPen, 420, 58, 420, 116) ' nextprocess
        e.Graphics.DrawLine(aPen, 490, 116, 490, 214) ' QTY Title

        e.Graphics.DrawLine(aPen, 310, 10, 310, 58) 'QTY
        e.Graphics.DrawLine(aPen, 595, 10, 595, 213) 'shift

        e.Graphics.DrawLine(aPen, 110, 10, 110, 116) 'Back TBKK
        e.Graphics.DrawLine(aPen, 110, 288, 110, 213) 'Factory

        e.Graphics.DrawLine(aPen, 700, 10, 700, 290)
        'Horizontal นอน

        e.Graphics.DrawLine(aPen, 110, 58, 700, 58)
        ' e.Graphics.DrawLine(aPen, 110, 95, 700, 95)
        e.Graphics.DrawLine(aPen, 10, 11, 700, 11)
        '  e.Graphics.DrawLine(aPen, 10, 220, 110, 220) 'factory
        'e.Graphics.DrawLine(aPen, 595, 142, 700, 142) 'line
        e.Graphics.DrawLine(aPen, 490, 166, 700, 166) 'line ยาว
        '  e.Graphics.DrawLine(aPen, 595, 235, 700, 235) 'actual
        e.Graphics.DrawLine(aPen, 10, 289, 700, 289)
        'DATA
        Dim LotNo As String = Working_Pro.Label18.Text
        e.Graphics.DrawString("TBKK", lb_font1.Font, Brushes.Black, 19, 15)
        e.Graphics.DrawString("(Thailand) Co.,Ltd. ", Label_wi_type.Font, Brushes.Black, 12, 50)
        Dim result_snp
        If Working_Pro.statusPrint = "CloseLot" Then
            result_snp = CDbl(Val(Working_Pro.lb_good.Text)) Mod CDbl(Val(Working_Pro.Label27.Text))
        ElseIf Working_Pro.statusPrint = "Normal" Then ' manual
            result_snp = CDbl(Val(Working_Pro.GoodQty)) Mod CDbl(Val(Working_Pro.Label27.Text))
        ElseIf Working_Pro.statusPrint = "Normal_contect_DIO" Or Working_Pro.statusPrint = "Normal_NI_MAX" Or Working_Pro.statusPrint = "Normal_DIO_RS232" Then ' auto counter qty
            result_snp = CDbl(Val(Working_Pro.GoodQty)) Mod CDbl(Val(Working_Pro.Label27.Text))
        End If
        Dim status_tag As String = "[ Incomplete Tag ]"
        If result_snp = "0" Then
            result_snp = Working_Pro.Label27.Text    'total
            status_tag = " "
        Else
            'result_snp = CDbl(Val(Working_Pro.GoodQty)) Mod CDbl(Val(Working_Pro.Label27.Text)) 'LB_COUNTER_SEQ.Text 
            status_tag = "[ Incomplete Tag ]"
        End If
        e.Graphics.DrawString("FA System", Label_wi_type.Font, Brushes.Black, 12, 78)
        If status_tag = "[ Incomplete Tag ]" Then
            e.Graphics.FillRectangle(Brushes.Black, 15, 65, 91, 14) ' Incomplete Tag BACKGROUD Black ' ห่างจากข้างหน้า, ความสูง, ความกว้าง,ขึ้นลง
            e.Graphics.DrawString(status_tag, lb_incomp.Font, Brushes.White, 15, 66) ' left top
        End If
        If Working_Pro.lb_prd_type.Text = "10" Then
            prdtype = "PART TYPE : FG"
        ElseIf Working_Pro.lb_prd_type.Text = "40" Then
            prdtype = "PART TYPE : Parts"
        Else
            prdtype = "PART TYPE : FW"
        End If
        e.Graphics.DrawString(prdtype, Label_wi_type.Font, Brushes.Black, 12, 99)
        e.Graphics.DrawString("Instr. Code", lb_font5.Font, Brushes.Black, 120, 15)
        e.Graphics.DrawString("BRACKET CAM ASSY ", lb_font4_B.Font, Brushes.Black, 120, 35)
        'Dim result_snp As Integer = CDbl(Val(Working_Pro.Label6.Text)) Mod CDbl(Val(Working_Pro.Label27.Text))
        e.Graphics.DrawString("QTY.", lb_font3.Font, Brushes.Black, 316, 15)
        e.Graphics.DrawString(result_snp, LB_QTY.Font, Brushes.Black, 363, 30)
        e.Graphics.DrawString("MODEL", lb_font5.Font, Brushes.Black, 120, 60)
        e.Graphics.DrawString("EJ40", batchModel.Font, Brushes.Black, 140, 75)
        e.Graphics.DrawString("NEXT PROCESS", lb_font5.Font, Brushes.Black, 282, 60)
        e.Graphics.DrawString("ISUZU", batchModel.Font, Brushes.Black, 305, 75)
        e.Graphics.DrawString("LOCATION", lb_font5.Font, Brushes.Black, 430, 60)
        e.Graphics.DrawString("D4U10A1", batchModel.Font, Brushes.Black, 445, 75)
        Dim num_box_seq As Integer
        num_box_seq = Working_Pro.lb_box_count.Text.Length
        Dim plan_seq As String
        Dim num_char_seq As Integer
        num_char_seq = Working_Pro.Label22.Text.Length
        If num_char_seq = 1 Then
            plan_seq = "00" & Working_Pro.Label22.Text
        ElseIf num_char_seq = 2 Then
            plan_seq = "0" & Working_Pro.Label22.Text
        Else
            plan_seq = Working_Pro.Label22.Text
        End If
        e.Graphics.DrawString("SHIFT", lb_font3.Font, Brushes.Black, 460, 15)
        e.Graphics.DrawString(Working_Pro.Label14.Text, LB_QTY.Font, Brushes.Black, 520, 30)
        e.Graphics.DrawString("LINE", lb_font5.Font, Brushes.Black, 495, 124)
        e.Graphics.DrawString(MainFrm.Label4.Text, lb_font4_B.Font, Brushes.Black, 517, 140)
        e.Graphics.DrawString("PRO SEQ", lb_font5.Font, Brushes.Black, 600, 60)
        e.Graphics.DrawString(plan_seq, batchModel.Font, Brushes.Black, 620, 75)
        e.Graphics.DrawString("LOT NO", lb_font3.Font, Brushes.Black, 596, 15)
        e.Graphics.DrawString(Working_Pro.Label18.Text, LB_QTY.Font, Brushes.Black, 622, 30)
        Dim api = New api()
        Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(Working_Pro.load_data)
        Dim plan_date As String = ""
        For Each item As Object In dict
            Dim da As Date = item("WORK_ODR_DLV_DATE").substring(0, 10)
            Dim f As Date = Date.ParseExact(item("WORK_ODR_DLV_DATE").substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture)
            result_plan_date = f.ToString("dd/MM/yyyy")
            plan_date = result_plan_date
        Next
        Dim the_Label_bach As String
        If Trim(Len(Working_Pro.Label_bach.Text)) = 1 Then
            the_Label_bach = "00" & Working_Pro.Label_bach.Text
        ElseIf Trim(Len(Working_Pro.Label_bach.Text)) = 2 Then
            the_Label_bach = "0" & Working_Pro.Label_bach.Text
        Else
            the_Label_bach = Working_Pro.Label_bach.Text
        End If
        e.Graphics.DrawString("PLAN DATE", lb_font5.Font, Brushes.Black, 495, 170)
        e.Graphics.DrawString(plan_date, lb_font4_B.Font, Brushes.Black, 500, 188)
        e.Graphics.DrawString("BATCH NO", lb_font5.Font, Brushes.Black, 605, 124)
        e.Graphics.DrawString(the_Label_bach, lb_font4_B.Font, Brushes.Black, 630, 140)
        e.Graphics.DrawString("ACTUAL DATE", lb_font5.Font, Brushes.Black, 605, 170)
        e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), lb_font4_B.Font, Brushes.Black, 606, 188)
        e.Graphics.DrawString("NO.", lb_font5.Font, Brushes.Black, 15, 119)
        e.Graphics.DrawString("WI No.", lb_font5.Font, Brushes.Black, 85, 119)
        e.Graphics.DrawString("Part No.", lb_font5.Font, Brushes.Black, 180, 119)
        e.Graphics.DrawString("Part Name.", lb_font5.Font, Brushes.Black, 300, 119)
        e.Graphics.DrawString("QTY", lb_font5.Font, Brushes.Black, 430, 119)
        e.Graphics.DrawLine(aPen, 10, 117, 700, 117) ' start line Title
        e.Graphics.DrawLine(aPen, 10, 134, 490, 134) ' end line Title
        e.Graphics.DrawLine(aPen, 10, 213, 700, 213)
        Dim margin_left_no = 18
        Dim margin_top_no = 136
        Dim margin_top_wi = 158
        Dim margin_left_wi = 75
        Dim margin_left_item_cd = 162
        Dim margin_left_part_name = 275
        Dim margin_left_QTY = 435
        ' Dim arr_item_cd() As String = {"898244-6240", "898244-6250", "898244-6260", "898244-6270", "898244-6280"}
        Dim arr_qr_code_sub() As String = {"01", "02", "03", "04", "05"}
        Dim i As Integer = 1
        For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
            Dim special_wi As String = itemPlanData.wi
            Dim special_item_cd As String = itemPlanData.item_cd
            Dim special_item_name As String = itemPlanData.item_name
            e.Graphics.DrawString(i, lb_font5.Font, Brushes.Black, margin_left_no, margin_top_no)
            e.Graphics.DrawString(special_wi, lb_font5.Font, Brushes.Black, margin_left_wi, margin_top_no)
            e.Graphics.DrawString(special_item_cd, lb_font5.Font, Brushes.Black, margin_left_item_cd, margin_top_no)
            e.Graphics.DrawString(special_item_name, lb_font5.Font, Brushes.Black, margin_left_part_name, margin_top_no)
            e.Graphics.DrawString(result_snp.ToString(), lb_font5.Font, Brushes.Black, margin_left_QTY, margin_top_no)
            margin_top_no += 15
            i = i + 1
        Next
        Dim qrcode As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
        qrcode.QRCodeScale = 10
        Dim bitmap_qr_box As Bitmap = qrcode.Encode("TEST")
        Dim qr_by_model = 158
        Dim qr_by_model_left = 118
        Dim iden_cd As String
        If MainFrm.Label6.Text = "K1PD01" Then
            iden_cd = "GB"
        Else
            iden_cd = "GB"
        End If
        Dim part_no_res1 As String
        Dim part_no_res As String
        Dim part_numm As Integer = 0
        Dim act_date As String
        Dim actdateConv As Date = DateTime.Now.ToString("dd/MM/yyyy")
        act_date = Format(actdateConv, "yyyyMMdd")
        Dim qty_num As String
        Dim num_char_qty As Integer
        num_char_qty = Len(Trim(result_snp)) 'Label27.Text.Length 'lb_qty_for_box.Text.Length
        If num_char_qty = 1 Then
            qty_num = "     " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 2 Then
            qty_num = "    " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 3 Then
            qty_num = "   " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 4 Then
            qty_num = "  " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 5 Then
            qty_num = " " & result_snp 'lb_qty_for_box.Text
        Else
            qty_num = result_snp 'lb_qty_for_box.Text
        End If
        Dim cus_part_no As String = "                         "
        Dim plan_cd As String
        Dim factory_cd As String
        If MainFrm.Label6.Text = "K2PD06" Then
            factory_cd = "Phase8"
            plan_cd = "52"
        Else
            factory_cd = "Phase10"
            plan_cd = "51"
        End If
        Dim box_no As String
        Dim num_char_box As Integer
        num_char_box = Working_Pro.lb_box_count.Text.Length
        If num_char_box = 1 Then
            box_no = "00" & Working_Pro.lb_box_count.Text
        ElseIf num_char_box = 2 Then
            box_no = "0" & Working_Pro.lb_box_count.Text
        Else
            box_no = Working_Pro.lb_box_count.Text
        End If
        plan_date = plan_date.Substring(6, 4) & plan_date.Substring(3, 2) & plan_date.Substring(0, 2)
        Dim j = 1
        For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
            part_no_res = ""
            Dim part_no_sub As String = itemPlanData.item_cd
            For part_numm = part_no_sub.Length To 24
                part_no_res = part_no_res & " "
            Next part_numm
            part_no_res1 = itemPlanData.item_cd & part_no_res
            bitmap_qr_box = QR_Generator.Encode(iden_cd & Working_Pro.Label24.Text & plan_date & plan_seq & part_no_res1 & act_date & qty_num & Working_Pro.Label18.Text & cus_part_no & act_date & plan_seq & plan_cd & the_Label_bach)
            e.Graphics.DrawString("QR No ." & j, BTitle.Font, Brushes.Black, qr_by_model_left, 215)
            e.Graphics.DrawImage(bitmap_qr_box, qr_by_model, 226, 68, 60) 'Right top
            qr_by_model += 116
            margin_top_no += 15
            qr_by_model_left += 114
            arr_qr_code_sub(j - 1) = iden_cd & Working_Pro.Label24.Text & plan_date & plan_seq & part_no_res1 & act_date & qty_num & Working_Pro.Label18.Text & cus_part_no & act_date & plan_seq & plan_cd & the_Label_bach
            j = j + 1
        Next
        part_no_res = ""
        For part_numm = Working_Pro.Label3.Text.Length To 24
            part_no_res = part_no_res & " "
        Next part_numm
        part_no_res1 = Working_Pro.Label3.Text & part_no_res
        Dim qr_code As String = iden_cd & Working_Pro.Label24.Text & plan_date & plan_seq & part_no_res1 & act_date & qty_num & Working_Pro.Label18.Text & cus_part_no & act_date & plan_seq & plan_cd & the_Label_bach
        bitmap_qr_box = QR_Generator.Encode(qr_code)
        ' e.Graphics.DrawImage(bitmap_qr_box, 15, 120, 90, 90) 'left
        'e.Graphics.DrawImage(bitmap_qr_box, 615, 15, 45, 45) 'Right top
        e.Graphics.DrawString("FACTORY", lb_font3.Font, Brushes.Black, 15, 230)
        e.Graphics.DrawString("Phase10", lb_font3.Font, Brushes.Black, 33, 250)
        '  e.Graphics.DrawImage(bitmap_qr_box, 600, 205, 75, 75) 'Right top
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim rs = Backoffice_model.Insert_tag_print_main(Working_Pro.wi_no.Text, qr_code, the_Label_bach, 1, plan_seq, Working_Pro.Label14.Text, check_tagprint_main(), Working_Pro.Label3.Text, Working_Pro.pwi_id, Working_Pro.tag_group_no)
                Dim rs1 = Await model_api_sqlite.mas_Insert_tag_print_main(Working_Pro.wi_no.Text, qr_code, the_Label_bach, 1, plan_seq, Working_Pro.Label14.Text, check_tagprint_main(), Working_Pro.Label3.Text, Working_Pro.pwi_id, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "1", Working_Pro.Label18.Text)
                ' Dim id_tag = Await api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_ID_PRINT_DETAIL_MAIN?qr_code=" & qr_code)
                Dim url As String = "http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GET_ID_PRINT_DETAIL_MAIN?qr_code=" & qr_code
                Console.WriteLine("Calling API GetPercenPlanned_OEE URL: " & url)
                ' ✅ แปลงให้ async โดยรันบน background thread
                Dim id_tag As String = Await Task.Run(Function() api.Load_data(url))
                'For K = 1 To MainFrm.ArrayDataPlan.ToArray.Length Step 1
                'Dim rs3 = Backoffice_model.Insert_tag_print_sub(id_tag, MainFrm.Label4.Text, arr_qr_code_sub(K - 1), Working_Pro.wi_no.Text, Working_Pro.tag_group_no)
                'Dim rs4 = Await model_api_sqlite.mas_Insert_tag_print_sub(id_tag, MainFrm.Label4.Text, arr_qr_code_sub(K - 1), Working_Pro.wi_no.Text, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "1", Working_Pro.Label18.Text)
                'Next
                Dim index As Integer = 1
                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                    Dim special_wi As String = itemPlanData.wi
                    Dim rs3 = Backoffice_model.Insert_tag_print_sub(id_tag, MainFrm.Label4.Text, arr_qr_code_sub(index - 1), special_wi, Working_Pro.tag_group_no)
                    Dim rs4 = Await model_api_sqlite.mas_Insert_tag_print_sub(id_tag, MainFrm.Label4.Text, arr_qr_code_sub(index - 1), special_wi, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "1", LotNo)
                    index = index + 1
                Next
            Else
                Dim indexelse As Integer = 1
                Dim rs5 = Await model_api_sqlite.mas_Insert_tag_print_main(Working_Pro.wi_no.Text, qr_code, the_Label_bach, 1, plan_seq, Working_Pro.Label14.Text, check_tagprint_main(), Working_Pro.Label3.Text, Working_Pro.pwi_id, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "0", Working_Pro.Label18.Text)
                Dim id_tag = Await model_api_sqlite.mas_get_tag_print_detail_main(qr_code)
                'For K = 1 To 5 Step 1
                ' Dim rs6 = Await model_api_sqlite.mas_Insert_tag_print_sub(id_tag, MainFrm.Label4.Text, arr_qr_code_sub(K - 1), Working_Pro.wi_no.Text, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "0", Working_Pro.Label18.Text)
                ' Next
                For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                    Dim special_wi As String = itemPlanData.wi
                    Dim rs6 = Await model_api_sqlite.mas_Insert_tag_print_sub(id_tag, MainFrm.Label4.Text, arr_qr_code_sub(indexelse - 1), special_wi, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "0", LotNo)
                    indexelse = indexelse + 1
                Next
            End If
        Catch ex As Exception
            hasError = True
        End Try
        If hasError = True Then
            Dim rs7 = Await model_api_sqlite.mas_Insert_tag_print_main(Working_Pro.wi_no.Text, qr_code, the_Label_bach, 1, plan_seq, Working_Pro.Label14.Text, check_tagprint_main(), Working_Pro.Label3.Text, Working_Pro.pwi_id, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "0", Working_Pro.Label18.Text)
            Dim id_tag = Await model_api_sqlite.mas_get_tag_print_detail_main(qr_code)
            Dim indexcatch As Integer = 1
            For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
                Dim special_wi As String = itemPlanData.wi
                Dim rs8 = Await model_api_sqlite.mas_Insert_tag_print_sub(id_tag, MainFrm.Label4.Text, arr_qr_code_sub(indexcatch - 1), special_wi, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "0", LotNo)
                indexcatch = indexcatch + 1
            Next
            ' For K = 1 To 5 Step 1
            ' Dim rs8 = Await model_api_sqlite.mas_Insert_tag_print_sub(id_tag, MainFrm.Label4.Text, arr_qr_code_sub(K - 1), Working_Pro.wi_no.Text, Working_Pro.tag_group_no, Working_Pro.Gobal_NEXT_PROCESS, "0", Working_Pro.Label18.Text)
            ' Next
        End If
    End Function
    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'oldM83_batch(e)
        newM83_batch(e) 'return data ต้องเอา result มาเก็บไม่งั้น ERROR
    End Sub
    Public Sub oldM83_single(e)
        Dim aPen = New Pen(Color.Black)
        aPen.Width = 2.0F
        'MsgBox(Label10.Text)
        'vertical ตรง
        e.Graphics.DrawLine(aPen, 10, 10, 10, 290)

        e.Graphics.DrawLine(aPen, 330, 58, 330, 95)

        e.Graphics.DrawLine(aPen, 460, 10, 460, 290)

        e.Graphics.DrawLine(aPen, 580, 10, 580, 290) 'QTY


        e.Graphics.DrawLine(aPen, 110, 10, 110, 290) 'QR right top


        e.Graphics.DrawLine(aPen, 700, 10, 700, 290)
        'Horizontal นอน

        e.Graphics.DrawLine(aPen, 110, 58, 580, 58)

        e.Graphics.DrawLine(aPen, 110, 95, 700, 95)


        e.Graphics.DrawLine(aPen, 10, 11, 700, 11)

        e.Graphics.DrawLine(aPen, 10, 110, 110, 110)

        e.Graphics.DrawLine(aPen, 10, 220, 110, 220)


        e.Graphics.DrawLine(aPen, 580, 67, 700, 67)

        e.Graphics.DrawLine(aPen, 460, 140, 700, 140)

        e.Graphics.DrawLine(aPen, 460, 190, 700, 190)

        e.Graphics.DrawLine(aPen, 460, 240, 580, 240)


        e.Graphics.DrawLine(aPen, 10, 289, 700, 289)
        'DATA
        e.Graphics.DrawString("TBKK", lb_font1.Font, Brushes.Black, 19, 15)
        e.Graphics.DrawString("WI : " & Prd_detail.lb_wi.Text, Label_wi_type.Font, Brushes.Black, 15, 60)
        If Working_Pro.lb_prd_type.Text = "10" Then
            prdtype = "PART TYPE : FG"
        ElseIf Working_Pro.lb_prd_type.Text = "40" Then
            prdtype = "PART TYPE : Parts"
        Else
            prdtype = "PART TYPE : FW"
        End If
        e.Graphics.DrawString(prdtype, Label_wi_type.Font, Brushes.Black, 15, 85)
        e.Graphics.DrawString("Instr. Code", lb_font5.Font, Brushes.Black, 120, 15)
        e.Graphics.DrawString(Working_Pro.Label3.Text & "     " & Working_Pro.Label12.Text, lb_font4_B.Font, Brushes.Black, 120, 35)
        Dim result_snp As Integer = 1 'CDbl(Val(Working_Pro.Label6.Text)) Mod CDbl(Val(Working_Pro.Label27.Text))
        'If Backoffice_model.check_line_reprint() = "0" Then
        '	If result_snp = "0" Then
        '	result_snp = Working_Pro.Label27.Text
        '	End If
        '	End If
        e.Graphics.DrawString("QTY.", lb_font3.Font, Brushes.Black, 465, 15)
        e.Graphics.DrawString(result_snp, LB_QTY.Font, Brushes.Black, 495, 28)
        e.Graphics.DrawString("MODEL", lb_font5.Font, Brushes.Black, 120, 60)
        e.Graphics.DrawString(Working_Pro.lb_model.Text, lb_font4_B.Font, Brushes.Black, 140, 75)
        e.Graphics.DrawString("NEXT PROCESS", lb_font5.Font, Brushes.Black, 340, 60)
        e.Graphics.DrawString("ISUZU", lb_font4_B.Font, Brushes.Black, 360, 75)
        e.Graphics.DrawString("LOCATION", lb_font5.Font, Brushes.Black, 465, 60)
        e.Graphics.DrawString("D4U10A1", lb_font4_B.Font, Brushes.Black, 485, 75)
        Dim num_box_seq As Integer
        num_box_seq = Working_Pro.lb_box_count.Text.Length
        Dim plan_seq As String
        Dim num_char_seq As Integer
        num_char_seq = Working_Pro.Label22.Text.Length
        If num_char_seq = 1 Then
            plan_seq = "00" & Working_Pro.Label22.Text
        ElseIf num_char_seq = 2 Then
            plan_seq = "0" & Working_Pro.Label22.Text
        Else
            plan_seq = Working_Pro.Label22.Text
        End If
        e.Graphics.DrawString("SHIFT", lb_font5.Font, Brushes.Black, 585, 69)
        e.Graphics.DrawString(Working_Pro.Label14.Text, lb_font4_B.Font, Brushes.Black, 625, 75)
        e.Graphics.DrawString("LINE", lb_font5.Font, Brushes.Black, 467, 98)
        e.Graphics.DrawString(MainFrm.Label4.Text, lb_font4_B.Font, Brushes.Black, 470, 115)

        e.Graphics.DrawString("PRO SEQ", lb_font5.Font, Brushes.Black, 585, 98)
        e.Graphics.DrawString(plan_seq, lb_font4_B.Font, Brushes.Black, 610, 115)
        Dim api = New api()
        Dim load_data = Working_Pro.load_data
        Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(load_data)
        Dim plan_date As String = ""
        For Each item As Object In dict
            Dim da As Date = item("WORK_ODR_DLV_DATE").substring(0, 10)
            Dim f As Date = Date.ParseExact(item("WORK_ODR_DLV_DATE").substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture)
            result_plan_date = f.ToString("dd/MM/yyyy")
            plan_date = result_plan_date
        Next
        Dim the_box_seq As String
        If num_box_seq = 1 Then
            the_box_seq = "00" & Working_Pro.lb_box_count.Text
        ElseIf num_box_seq = 2 Then
            the_box_seq = "0" & Working_Pro.lb_box_count.Text
        Else
            the_box_seq = Working_Pro.lb_box_count.Text
        End If
        e.Graphics.DrawString("PLAN DATE", lb_font5.Font, Brushes.Black, 465, 145)
        e.Graphics.DrawString(plan_date, lb_font4_B.Font, Brushes.Black, 470, 162)

        e.Graphics.DrawString("BOX NO", lb_font5.Font, Brushes.Black, 585, 145)
        e.Graphics.DrawString(the_box_seq, lb_font4_B.Font, Brushes.Black, 610, 162)

        e.Graphics.DrawString("ACTUAL DATE", lb_font5.Font, Brushes.Black, 465, 195)
        e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), lb_font4_B.Font, Brushes.Black, 470, 215)

        e.Graphics.DrawString("LOT NO", lb_font5.Font, Brushes.Black, 465, 245)
        e.Graphics.DrawString(Working_Pro.Label18.Text, lb_font4_B.Font, Brushes.Black, 485, 265)

        e.Graphics.DrawString("NO.", lb_font5.Font, Brushes.Black, 120, 105)
        e.Graphics.DrawString("Part No.", lb_font5.Font, Brushes.Black, 160, 105)
        e.Graphics.DrawString("Part Name.", lb_font5.Font, Brushes.Black, 250, 105)
        e.Graphics.DrawString("QTY", lb_font5.Font, Brushes.Black, 420, 105)

        e.Graphics.DrawLine(aPen, 110, 123, 460, 123)
        'e.Graphics.DrawLine(aPen, 110, 210, 460, 210)
        Dim margin_left_no = 125
        Dim margin_top_no = 140
        Dim margin_left_item_cd = 160
        Dim margin_left_part_name = 250
        Dim margin_left_QTY = 425
        Dim arr_item_cd() As String = {"898244-6240", "898244-6250", "898244-6260", "898244-6270", "898244-6280"}
        Dim arr_qr_code_sub() As String = {"01", "02", "03", "04", "05"}
        For i = 1 To MainFrm.ArrayDataPlan.ToArray.Length Step 1
            e.Graphics.DrawString(i, LB_FONT_DATA.Font, Brushes.Black, margin_left_no, margin_top_no)
            e.Graphics.DrawString(arr_item_cd(i - 1), LB_FONT_DATA.Font, Brushes.Black, margin_left_item_cd, margin_top_no)
            e.Graphics.DrawString("BRACKET ASM;CAM NO" & i, LB_FONT_DATA.Font, Brushes.Black, margin_left_part_name, margin_top_no)
            e.Graphics.DrawString(result_snp.ToString(), LB_FONT_DATA.Font, Brushes.Black, margin_left_QTY, margin_top_no)
            margin_top_no += 30
        Next
        Dim qrcode As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
        qrcode.QRCodeScale = 10
        Dim bitmap_qr_box As Bitmap = qrcode.Encode("TEST")
        Dim qr_by_model = 120
        Dim qr_by_model_left = 118
        Dim iden_cd As String
        '  If MainFrm.Label6.Text = "K1PD01" Then
        iden_cd = "GB"
        ' Else
        ' iden_cd = "GB"
        '  End If
        Dim part_no_res1 As String
        Dim part_no_res As String
        Dim part_numm As Integer = 0
        Dim act_date As String
        Dim actdateConv As Date = DateTime.Now.ToString("dd/MM/yyyy")
        act_date = Format(actdateConv, "yyyyMMdd")
        Dim qty_num As String
        Dim num_char_qty As Integer
        num_char_qty = Len(Trim(result_snp)) 'Label27.Text.Length 'lb_qty_for_box.Text.Length
        If num_char_qty = 1 Then
            qty_num = "     " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 2 Then
            qty_num = "    " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 3 Then
            qty_num = "   " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 4 Then
            qty_num = "  " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 5 Then
            qty_num = " " & result_snp 'lb_qty_for_box.Text
        Else
            qty_num = result_snp 'lb_qty_for_box.Text
        End If
        Dim cus_part_no As String = "                         "
        Dim plan_cd As String
        Dim factory_cd As String
        If MainFrm.Label6.Text = "K2PD06" Then
            factory_cd = "Phase8"
            plan_cd = "52"
        Else
            factory_cd = "Phase10"
            plan_cd = "51"
        End If
        Dim box_no As String
        Dim num_char_box As Integer
        num_char_box = Working_Pro.lb_box_count.Text.Length
        If num_char_box = 1 Then
            box_no = "00" & Working_Pro.lb_box_count.Text
        ElseIf num_char_box = 2 Then
            box_no = "0" & Working_Pro.lb_box_count.Text
        Else
            box_no = Working_Pro.lb_box_count.Text
        End If
        For part_numm = Working_Pro.Label3.Text.Length To 24
            part_no_res = part_no_res & " "
        Next part_numm
        plan_date = plan_date.Substring(6, 4) & plan_date.Substring(3, 2) & plan_date.Substring(0, 2)
        part_no_res1 = Working_Pro.Label3.Text & part_no_res
        Dim qr_code As String = iden_cd & Working_Pro.Label24.Text & plan_date & plan_seq & part_no_res1 & act_date & qty_num & Working_Pro.Label18.Text & cus_part_no & act_date & plan_seq & plan_cd & box_no
        bitmap_qr_box = QR_Generator.Encode(qr_code)
        e.Graphics.DrawImage(bitmap_qr_box, 15, 120, 90, 90) 'left
        e.Graphics.DrawImage(bitmap_qr_box, 615, 15, 45, 45) 'Right top
        e.Graphics.DrawString("FACTORY", lb_font3.Font, Brushes.Black, 15, 230)
        e.Graphics.DrawString("Phase10", lb_font3.Font, Brushes.Black, 33, 250)
        e.Graphics.DrawImage(bitmap_qr_box, 600, 205, 75, 75) 'Right top
        Dim tr_status As Integer = 0
        If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
            tr_status = 1
            Backoffice_model.Insert_tag_print(Working_Pro.wi_no.Text, qr_code, box_no, 1, plan_seq, Working_Pro.Label14.Text, Working_Pro.check_tagprint(), Working_Pro.Label3.Text, Working_Pro.pwi_id, "1", Working_Pro.GoodQty, Working_Pro.Gobal_NEXT_PROCESS, tr_status)
            'model_api_sqlite.mas_Insert_tag_print(Working_Pro.wi_no.Text, qr_code, box_no, 1, plan_seq, Working_Pro.Label14.Text, Working_Pro.check_tagprint(), Working_Pro.Label3.Text, Working_Pro.pwi_id, "1", Working_Pro.GoodQty, Working_Pro.Gobal_NEXT_PROCESS, tr_status)
            ' Else
            '  tr_status = 0
            '    model_api_sqlite.mas_Insert_tag_print(Working_Pro.wi_no.Text, qr_code, box_no, 1, plan_seq, Working_Pro.Label14.Text, Working_Pro.check_tagprint(), Working_Pro.Label3.Text, Working_Pro.pwi_id, Working_Pro.tag_group_no, Working_Pro.GoodQty, Working_Pro.Gobal_NEXT_PROCESS, tr_status)
        End If
    End Sub

    Private Async Sub PrintDocument2_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage
        ' MsgBox("single")
        ' oldM83_single(e)
        Await newM83_single(e)
    End Sub
    Public Async Function newM83_single(e) As Task
        Dim aPen = New Pen(Color.Black)
        Dim hasError As Boolean = False

        aPen.Width = 2.0F
        'MsgBox(Label10.Text)
        'vertical ตรง
        e.Graphics.DrawLine(aPen, 10, 10, 10, 290)

        e.Graphics.DrawLine(aPen, 280, 58, 280, 95) ' model

        e.Graphics.DrawLine(aPen, 460, 10, 460, 58) ' line qr
        e.Graphics.DrawLine(aPen, 420, 58, 420, 95) ' nextprocess


        e.Graphics.DrawLine(aPen, 310, 10, 310, 58) 'QTY
        e.Graphics.DrawLine(aPen, 595, 10, 595, 290) 'shift

        e.Graphics.DrawLine(aPen, 110, 10, 110, 290) 'QR right top


        e.Graphics.DrawLine(aPen, 700, 10, 700, 290)
        'Horizontal นอน

        e.Graphics.DrawLine(aPen, 110, 58, 700, 58)

        e.Graphics.DrawLine(aPen, 110, 95, 700, 95)


        e.Graphics.DrawLine(aPen, 10, 11, 700, 11)
        e.Graphics.DrawLine(aPen, 10, 220, 110, 220) 'factory


        e.Graphics.DrawLine(aPen, 595, 142, 700, 142) 'line

        e.Graphics.DrawLine(aPen, 595, 183, 700, 183) 'plan

        e.Graphics.DrawLine(aPen, 595, 235, 700, 235) 'actual


        e.Graphics.DrawLine(aPen, 10, 289, 700, 289)
        'DATA
        e.Graphics.DrawString("TBKK", lb_font1.Font, Brushes.Black, 19, 15)
        e.Graphics.DrawString("(Thailand) Co.,Ltd. ", Label_wi_type.Font, Brushes.Black, 12, 50)
        e.Graphics.DrawString("FA System", Label_wi_type.Font, Brushes.Black, 12, 78)
        If Working_Pro.lb_prd_type.Text = "10" Then
            prdtype = "PART TYPE : FG"
        ElseIf Working_Pro.lb_prd_type.Text = "40" Then
            prdtype = "PART TYPE : Parts"
        Else
            prdtype = "PART TYPE : FW"
        End If
        e.Graphics.DrawString(prdtype, Label_wi_type.Font, Brushes.Black, 12, 105)
        e.Graphics.DrawString("Instr. Code", lb_font5.Font, Brushes.Black, 120, 15)
        e.Graphics.DrawString("BRACKET CAM ASSY ", lb_font4_B.Font, Brushes.Black, 120, 35)
        Dim result_snp As Integer = 1 'CDbl(Val(Working_Pro.Label6.Text)) Mod CDbl(Val(Working_Pro.Label27.Text))
        'If Backoffice_model.check_line_reprint() = "0" Then
        '	If result_snp = "0" Then
        '	result_snp = Working_Pro.Label27.Text
        '	End If
        '	End If
        e.Graphics.DrawString("QTY.", lb_font3.Font, Brushes.Black, 316, 15)
        e.Graphics.DrawString(result_snp, LB_QTY.Font, Brushes.Black, 363, 30)
        e.Graphics.DrawString("MODEL", lb_font5.Font, Brushes.Black, 120, 60)
        e.Graphics.DrawString("EJ40", lb_font4_B.Font, Brushes.Black, 140, 75)
        e.Graphics.DrawString("NEXT PROCESS", lb_font5.Font, Brushes.Black, 282, 60)
        e.Graphics.DrawString("ISUZU", lb_font4_B.Font, Brushes.Black, 320, 75)
        e.Graphics.DrawString("LOCATION", lb_font5.Font, Brushes.Black, 430, 60)
        e.Graphics.DrawString("D4U10A1", lb_font4_B.Font, Brushes.Black, 460, 75)
        Dim num_box_seq As Integer
        num_box_seq = Working_Pro.lb_box_count.Text.Length
        Dim plan_seq As String
        Dim num_char_seq As Integer
        num_char_seq = Working_Pro.Label22.Text.Length
        If num_char_seq = 1 Then
            plan_seq = "00" & Working_Pro.Label22.Text
        ElseIf num_char_seq = 2 Then
            plan_seq = "0" & Working_Pro.Label22.Text
        Else
            plan_seq = Working_Pro.Label22.Text
        End If
        e.Graphics.DrawString("SHIFT", lb_font3.Font, Brushes.Black, 460, 15)
        e.Graphics.DrawString(Working_Pro.Label14.Text, LB_QTY.Font, Brushes.Black, 520, 30)
        e.Graphics.DrawString("LINE", lb_font5.Font, Brushes.Black, 600, 98)
        e.Graphics.DrawString(MainFrm.Label4.Text, lb_font4_B.Font, Brushes.Black, 620, 115)
        e.Graphics.DrawString("PRO SEQ", lb_font5.Font, Brushes.Black, 600, 60)
        e.Graphics.DrawString(plan_seq, lb_font4_B.Font, Brushes.Black, 640, 75)
        e.Graphics.DrawString("LOT NO", lb_font3.Font, Brushes.Black, 596, 15)
        e.Graphics.DrawString(Working_Pro.Label18.Text, LB_QTY.Font, Brushes.Black, 622, 30)
        Dim api = New api()
        Dim load_data = Working_Pro.load_data
        Dim dict As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(load_data)
        Dim plan_date As String = ""
        For Each item As Object In dict
            Dim da As Date = item("WORK_ODR_DLV_DATE").substring(0, 10)
            Dim f As Date = Date.ParseExact(item("WORK_ODR_DLV_DATE").substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture)
            result_plan_date = f.ToString("dd/MM/yyyy")
            plan_date = result_plan_date
        Next
        Dim the_box_seq As String
        If num_box_seq = 1 Then
            the_box_seq = "00" & Working_Pro.lb_box_count.Text
        ElseIf num_box_seq = 2 Then
            the_box_seq = "0" & Working_Pro.lb_box_count.Text
        Else
            the_box_seq = Working_Pro.lb_box_count.Text
        End If
        e.Graphics.DrawString("PLAN DATE", lb_font5.Font, Brushes.Black, 600, 187)
        e.Graphics.DrawString(plan_date, lb_font4_B.Font, Brushes.Black, 605, 205)
        e.Graphics.DrawString("BOX NO", lb_font5.Font, Brushes.Black, 600, 148)
        e.Graphics.DrawString(the_box_seq, lb_font4_B.Font, Brushes.Black, 640, 159)
        e.Graphics.DrawString("ACTUAL DATE", lb_font5.Font, Brushes.Black, 600, 243)
        e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), lb_font4_B.Font, Brushes.Black, 605, 258)
        e.Graphics.DrawString("NO.", lb_font5.Font, Brushes.Black, 120, 105)
        e.Graphics.DrawString("WI.", lb_font5.Font, Brushes.Black, 180, 105)
        e.Graphics.DrawString("Part No.", lb_font5.Font, Brushes.Black, 270, 105)
        e.Graphics.DrawString("Part Name.", lb_font5.Font, Brushes.Black, 400, 105)
        e.Graphics.DrawString("QTY", lb_font5.Font, Brushes.Black, 547, 105)
        e.Graphics.DrawLine(aPen, 110, 123, 595, 123)
        'e.Graphics.DrawLine(aPen, 110, 210, 460, 210)
        Dim margin_left_no = 125
        Dim margin_top_no = 140
        Dim margin_left_wi = 160
        Dim margin_left_item_cd = 250
        Dim margin_left_part_name = 357
        Dim margin_left_QTY = 553
        Dim arr_item_cd() As String = {"898244-6240", "898244-6250", "898244-6260", "898244-6270", "898244-6280"}
        Dim arr_qr_code_sub() As String = {"01", "02", "03", "04", "05"}
        ' For i = 1 To 5 Step 1
        Dim Iseq As Integer = 0
        Dim i As Integer = 1
        For Each itemPlanData As DataPlan In Confrime_work_production.ArrayDataPlan
            Iseq += 1
            Dim special_wi As String = itemPlanData.wi
            Dim special_item_cd As String = itemPlanData.item_cd
            Dim special_item_name As String = itemPlanData.item_name
            e.Graphics.DrawString(i, LB_FONT_DATA.Font, Brushes.Black, margin_left_no, margin_top_no)
            e.Graphics.DrawString(special_wi, LB_FONT_DATA.Font, Brushes.Black, margin_left_wi, margin_top_no)
            e.Graphics.DrawString(special_item_cd, LB_FONT_DATA.Font, Brushes.Black, margin_left_item_cd, margin_top_no)
            e.Graphics.DrawString(special_item_name, LB_FONT_DATA.Font, Brushes.Black, margin_left_part_name, margin_top_no)
            e.Graphics.DrawString(result_snp.ToString(), LB_FONT_DATA.Font, Brushes.Black, margin_left_QTY, margin_top_no)
            margin_top_no += 30
            i = i + 1
        Next
        Dim qrcode As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
        qrcode.QRCodeScale = 10
        Dim bitmap_qr_box As Bitmap = qrcode.Encode("TEST")
        Dim qr_by_model = 120
        Dim qr_by_model_left = 118
        Dim iden_cd As String
        '  If MainFrm.Label6.Text = "K1PD01" Then
        iden_cd = "GB"
        ' Else
        ' iden_cd = "GB"
        '  End If
        Dim part_no_res1 As String
        Dim part_no_res As String
        Dim part_numm As Integer = 0
        Dim act_date As String
        Dim actdateConv As Date = DateTime.Now.ToString("dd/MM/yyyy")
        act_date = Format(actdateConv, "yyyyMMdd")
        Dim qty_num As String
        Dim num_char_qty As Integer
        num_char_qty = Len(Trim(result_snp)) 'Label27.Text.Length 'lb_qty_for_box.Text.Length
        If num_char_qty = 1 Then
            qty_num = "     " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 2 Then
            qty_num = "    " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 3 Then
            qty_num = "   " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 4 Then
            qty_num = "  " & result_snp 'lb_qty_for_box.Text
        ElseIf num_char_qty = 5 Then
            qty_num = " " & result_snp 'lb_qty_for_box.Text
        Else
            qty_num = result_snp 'lb_qty_for_box.Text
        End If
        Dim cus_part_no As String = "                         "
        Dim plan_cd As String
        Dim factory_cd As String
        If MainFrm.Label6.Text = "K2PD06" Then
            factory_cd = "Phase8"
            plan_cd = "52"
        Else
            factory_cd = "Phase10"
            plan_cd = "51"
        End If
        Dim box_no As String
        Dim num_char_box As Integer
        num_char_box = Working_Pro.lb_box_count.Text.Length
        If num_char_box = 1 Then
            box_no = "00" & Working_Pro.lb_box_count.Text
        ElseIf num_char_box = 2 Then
            box_no = "0" & Working_Pro.lb_box_count.Text
        Else
            box_no = Working_Pro.lb_box_count.Text
        End If
        For part_numm = Working_Pro.Label3.Text.Length To 24
            part_no_res = part_no_res & " "
        Next part_numm
        plan_date = plan_date.Substring(6, 4) & plan_date.Substring(3, 2) & plan_date.Substring(0, 2)
        part_no_res1 = Working_Pro.Label3.Text & part_no_res
        Dim qr_code As String = iden_cd & Working_Pro.Label24.Text & plan_date & plan_seq & part_no_res1 & act_date & qty_num & Working_Pro.Label18.Text & cus_part_no & act_date & plan_seq & plan_cd & box_no
        bitmap_qr_box = QR_Generator.Encode(qr_code)
        'e.Graphics.DrawImage(bitmap_qr_box, 15, 120, 90, 90) 'left
        ' e.Graphics.DrawImage(bitmap_qr_box, 615, 15, 45, 45) 'Right top
        e.Graphics.DrawString("FACTORY", lb_font3.Font, Brushes.Black, 15, 230)
        e.Graphics.DrawString("Phase10", lb_font3.Font, Brushes.Black, 33, 250)
        '  e.Graphics.DrawImage(bitmap_qr_box, 600, 205, 75, 75) 'Right top
        Dim tr_status As Integer = 0
        Dim tag_grop_no As Integer = 1
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                tr_status = 1 '
                Backoffice_model.Insert_tag_print(Working_Pro.wi_no.Text, qr_code, box_no, 1, plan_seq, Working_Pro.Label14.Text, Working_Pro.check_tagprint(), Working_Pro.Label3.Text, Working_Pro.pwi_id, tag_grop_no, Working_Pro.GoodQty, Working_Pro.Gobal_NEXT_PROCESS, tr_status)
                '   model_api_sqlite.mas_Insert_tag_print(Working_Pro.wi_no.Text, qr_code, box_no, 1, plan_seq, Working_Pro.Label14.Text, Working_Pro.check_tagprint(), Working_Pro.Label3.Text, Working_Pro.pwi_id, tag_grop_no, Working_Pro.GoodQty, Working_Pro.Gobal_NEXT_PROCESS, tr_status)
            Else
                tr_status = 0
                Await model_api_sqlite.mas_Insert_tag_print(Working_Pro.wi_no.Text, qr_code, box_no, 1, plan_seq, Working_Pro.Label14.Text, Working_Pro.check_tagprint(), Working_Pro.Label3.Text, Working_Pro.pwi_id, tag_grop_no, Working_Pro.GoodQty, Working_Pro.Gobal_NEXT_PROCESS, tr_status)
            End If
        Catch ex As Exception
            hasError = True
            tr_status = 0
        End Try
        If hasError Then
            tr_status = 0
            Await model_api_sqlite.mas_Insert_tag_print(Working_Pro.wi_no.Text, qr_code, box_no, 1, plan_seq, Working_Pro.Label14.Text, Working_Pro.check_tagprint(), Working_Pro.Label3.Text, Working_Pro.pwi_id, tag_grop_no, Working_Pro.GoodQty, Working_Pro.Gobal_NEXT_PROCESS, tr_status)
        End If
    End Function
End Class