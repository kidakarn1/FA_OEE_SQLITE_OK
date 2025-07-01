Public Class Print_Defact
    Private part_no As String = "NO_DATA"
    Private PART_NAME As String = "NO_DATA"
    Private Model As String = "NO_DATA"
    Private LOT_NO As String = "NO_DATA"
    Private BOX_NO As Integer = 0
    Private SHIFT As String = "NO_DATA"
    Private QTY As String = "NO_DATA"
    Private LINE As String = "NO_DATA"
    Private CHECK_DATE As String = "NO_DATA"
    Private M_BOX As String = "NO_DATA"
    Private NEW_QR As String = "NO_DATA"
    Private box_seq As String = "NO_DATA"
    Private new_gen_qr As String = "NO_DATA"
    Private QR_PRODUCT As String = ""
    Private BOX_SEQ_NG As String = "NO_DATA"
    Private default_NG As String = "1000"
    Private WASHING_DATE_NG As String = "NO_DATA"
    Private G_STATUS_PRINT As String = "NO_DATA"
    Public Sub Set_parameter_print(LB_PART_NO As String, LB_PART_NAME As String, LB_MODEL As String, LB_LOT As String, LB_COUNTBOX As String, LB_SNP As String, LB_Hide_QR_FA_SCAN As String, max_box As String, QR_PRODUCT_SCAN As String, result_seq_ng As String, para_shift As String, LB_STATUS_PRINT As String)
        part_no = LB_PART_NO
        PART_NAME = LB_PART_NAME
        Model = LB_MODEL
        LOT_NO = LB_LOT
        BOX_NO = LB_COUNTBOX
        M_BOX = max_box
        SHIFT = para_shift
        QTY = LB_SNP
        LINE = MainFrm.Label4.Text
        G_STATUS_PRINT = LB_STATUS_PRINT
        'CHECK_DATE = "NO_DATA"
        'Dim year As String = LB_Hide_QR_FA_SCAN.Substring(8, 4)
        'Dim mouth As String = LB_Hide_QR_FA_SCAN.Substring(12, 2)
        'Dim day As String = LB_Hide_QR_FA_SCAN.Substring(14, 2)
        WASHING_DATE_NG = DateTime.Now.ToString("yyyy/MM/dd")
        'WASHING_DATE_NG = Year() & "/" & mouth & "/" & Day
        'MsgBox("default = " & LB_Hide_QR_FA_SCAN)
        Dim iden_cd As String
        If MainFrm.Label6.Text = "K1PD01" Then
            iden_cd = "GA"
        Else
            iden_cd = "GB"
        End If
        Dim act_date As String
        Dim actdateConv As Date = DateTime.Now.ToString("dd/MM/yyyy")
        act_date = Format(actdateConv, "yyyyMMdd")
        Dim part_no_res1 As String
        Dim part_no_res As String
        Dim part_numm As Integer = 0
        For part_numm = Working_Pro.Label3.Text.Length To 24
            part_no_res = part_no_res & " "
        Next part_numm
        part_no_res1 = Working_Pro.Label3.Text & part_no_res
        Dim qty_num As String
        Dim num_char_qty As Integer
        num_char_qty = Working_Pro.lb_qty_for_box.Text.Length
        If num_char_qty = 1 Then
            qty_num = "     " & LB_SNP
        ElseIf num_char_qty = 2 Then
            qty_num = "    " & LB_SNP
        ElseIf num_char_qty = 3 Then
            qty_num = "   " & LB_SNP
        ElseIf num_char_qty = 4 Then
            qty_num = "  " & LB_SNP
        ElseIf num_char_qty = 5 Then
            qty_num = " " & LB_SNP
        Else
            qty_num = LB_SNP
        End If
        Dim plan_cd As String
        Dim factory_cd As String
        If MainFrm.Label6.Text = "K2PD06" Then
            factory_cd = "Phase8"
            plan_cd = "52"
        Else
            factory_cd = "Phase10"
            plan_cd = "51"
        End If
        Dim cus_part_no As String = "                         "
        Dim seq_con As String
        If Len(Working_Pro.Label22.Text) = 1 Then
            seq_con = "00" & Working_Pro.Label22.Text
        ElseIf Len(Working_Pro.Label22.Text) = 2 Then
            seq_con = "0" & Working_Pro.Label22.Text
        ElseIf Len(Working_Pro.Label22.Text) = 3 Then
            seq_con = Working_Pro.Label22.Text
        End If
        Dim check_data = iden_cd & LINE & act_date & seq_con & part_no_res1 & act_date & qty_num & LOT_NO & cus_part_no & act_date & seq_con & plan_cd & BOX_NO
        NEW_QR = 'LB_Hide_QR_FA_SCAN.Substring(0, 100)
        BOX_SEQ_NG = result_seq_ng 'CDbl(Val(default_NG)) - CDbl(Val(LB_COUNTBOX))
        new_gen_qr = check_data 'NEW_QR & BOX_NO
        'QR_PRODUCT = QR_PRODUCT_SCAN
        PrintDocument1.Print()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim aPen = New Pen(Color.Black)
        aPen.Width = 3.0F  'border 

        'MsgBox(Label10.Text)

        'vertical

        e.Graphics.DrawLine(aPen, 80, 5, 80, 295)
        e.Graphics.DrawLine(aPen, 500, 5, 500, 200)
        e.Graphics.DrawLine(aPen, 400, 90, 400, 295)
        e.Graphics.DrawLine(aPen, 200, 200, 200, 295)
        'e.Graphics.DrawLine(aPen, 333, 200, 333, 295)


        e.Graphics.DrawLine(aPen, 540, 200, 540, 295)

        e.Graphics.DrawLine(aPen, 670, 5, 670, 295)
        'Horizontal
        e.Graphics.DrawLine(aPen, 80, 5, 670, 5)
        e.Graphics.DrawLine(aPen, 80, 40, 500, 40)
        e.Graphics.DrawLine(aPen, 80, 90, 500, 90)
        e.Graphics.DrawLine(aPen, 80, 140, 400, 140)
        e.Graphics.DrawLine(aPen, 500, 140, 670, 140) '
        e.Graphics.DrawLine(aPen, 80, 200, 670, 200)
        e.Graphics.DrawLine(aPen, 80, 250, 540, 250)
        e.Graphics.DrawLine(aPen, 80, 295, 670, 295)
        'TAG LAYOUT
        e.Graphics.DrawString("TBKK", Label5.Font, Brushes.Black, 10, 10)
        e.Graphics.DrawString("(Thailand) Co.,Ltd.", Label6.Font, Brushes.Black, 0, 40)
        e.Graphics.DrawString("New FA", Label7.Font, Brushes.Black, 10, 100)
        e.Graphics.DrawString("System", Label7.Font, Brushes.Black, 15, 120)
        e.Graphics.DrawString("To", Label1.Font, Brushes.Black, 102, 10)
        e.Graphics.DrawString("IHI Turbo (Thailand) Co._LTD", Label1.Font, Brushes.Black, 110, 25)
        e.Graphics.DrawString("PART NO.", Label1.Font, Brushes.Black, 90, 50)
        e.Graphics.DrawString(part_no, Label10.Font, Brushes.Black, 100, 60)
        e.Graphics.DrawString("PART NAME", Label1.Font, Brushes.Black, 90, 100)
        e.Graphics.DrawString(PART_NAME, Label1.Font, Brushes.Black, 102, 120)
        e.Graphics.DrawString("MODEL", Label1.Font, Brushes.Black, 90, 150)
        e.Graphics.DrawString(Model, Label1.Font, Brushes.Black, 102, 165)
        e.Graphics.DrawString("QTY", Label8.Font, Brushes.Black, 425, 100)
        e.Graphics.DrawString(QTY, Label12.Font, Brushes.Black, 420, 130)

        e.Graphics.DrawString("LOT NO.", Label1.Font, Brushes.Black, 90, 200)
        e.Graphics.DrawString(LOT_NO, Label8.Font, Brushes.Black, 100, 220)

        e.Graphics.DrawString("BOX NO.", Label1.Font, Brushes.Black, 200, 200)
        e.Graphics.DrawString(BOX_NO, Label8.Font, Brushes.Black, 235, 220)
        e.Graphics.DrawString("LINE", Label1.Font, Brushes.Black, 405, 200)
        e.Graphics.DrawString(LINE, Label8.Font, Brushes.Black, 415, 220)
        e.Graphics.DrawString("WASHING DATE", Label1.Font, Brushes.Black, 505, 145)
        e.Graphics.DrawString(WASHING_DATE_NG, Label8.Font, Brushes.Black, 505, 170)
        e.Graphics.DrawString("PHASE", Label1.Font, Brushes.Black, 90, 252)
        e.Graphics.DrawString("10", Label8.Font, Brushes.Black, 125, 265)
        e.Graphics.DrawString("SHIFT", Label1.Font, Brushes.Black, 200, 252)
        e.Graphics.DrawString(SHIFT, Label2.Font, Brushes.Black, 255, 265)
        e.Graphics.DrawString("PROCESS", Label1.Font, Brushes.Black, 405, 252)
        e.Graphics.DrawString("REWORK", Label9.Font, Brushes.Black, 420, 265)
        e.Graphics.DrawString("Info", Label1.Font, Brushes.Black, 545, 200)
        'e.Graphics.DrawString("K2M133", Label2.Font, Brushes.Black, 490, 265)
        Dim qrcode As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
        Dim qr = "TESTTTTT"
        qrcode.QRCodeScale = 10
        Dim bitmap_qr_box As Bitmap = qrcode.Encode(new_gen_qr)
        Dim bitmap_qr_product As Bitmap = qrcode.Encode(QR_PRODUCT)
        e.Graphics.DrawString(G_STATUS_PRINT, Label13.Font, Brushes.Black, 510, 20)
        ' e.Graphics.DrawImage(bitmap_qr_box, 532, 20, 115, 115) 'TOP
        e.Graphics.DrawImage(bitmap_qr_box, 0, 210, 75, 75) 'left
        e.Graphics.DrawImage(bitmap_qr_box, 570, 220, 70, 70) 'button right
    End Sub
    Private Sub Print_Defact_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PrintDocument1.Print()
    End Sub
End Class
