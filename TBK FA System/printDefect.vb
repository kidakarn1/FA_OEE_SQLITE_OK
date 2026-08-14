Imports System.Globalization
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Web.Script.Serialization
Imports QRCoder
Public Class printDefect
    Dim lPartno As String = "NO DATA"
    Dim lPartname As String = "NO DATA"
    Dim lModel As String = "NO DATA"
    Dim lLine As String = "NO DATA"
    Dim lActualdate As String = "NO DATA"
    Dim lLocation As String = "NO DATA"
    Dim lShift As String = "NO DATA"
    Dim lPhase As String = "NO DATA"
    Dim lLot As String = "NO DATA"
    Dim lQtydefect As String = "NO DATA"
    Dim lSeq As String = "NO DATA"
    Dim lwi As String = "NO DATA"
    Dim QR_Generator As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
    Dim Defect_LB_STATUS As String = "NC" 'ชั่วคราว
    Dim qrDefectinfo As String = ""
    Dim qrDefectcodedetails As String = ""
    Dim sDefect As String = ""
    Dim lBoxno As String = "001"
    Dim pCd As String = ""
    Dim lItemtype As String = ""
    Dim TypeMenu As String = ""
    Private defectDataList As List(Of Object)
    Private printReady As Boolean = False
    Dim rs As String = "0"
    Private currentIndex As Integer = 0
    Private Sub printDefect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '  MsgBox("load pd") ' ห้ามลบ
        '  PrintDocument1.Print()
        'PrintPreviewDialog1.ShowDialog()
    End Sub
    Public Async Function WaitForNetworkWithPopup() As Task
        Do While Not IsNetworkAvailable() OrElse Not My.Computer.Network.Ping(Backoffice_model.svp_ping)
            If Not load_show.Visible Then
                load_show.Show()
            End If
            'Console.WriteLine("⛔ ยังไม่มี Network หรือ Ping ไม่ผ่าน... รอ 1 วินาที")
            Await Task.Delay(3000)
        Loop
        If load_show.Visible Then
            load_show.Hide()
        End If
    End Function
    Private Function IsNetworkAvailable() As Boolean
        Return NetworkInterface.GetIsNetworkAvailable()
    End Function
    '    Public Async Sub Set_parameter_print(pNo As String, pName As String, Model As String, Line As String, atDate As Date, Location As String, Shift As String, Phase As String, lot As String, qtyDefect As String, seqQty As String, wi As String, itemType As String, dfType As String, menu As String)
    Public Async Function Set_parameter_print(pNo As String, pName As String, Model As String, Line As String, atDate As Date, Location As String, Shift As String, Phase As String, lot As String, qtyDefect As String, seqQty As String, wi As String, itemType As String, dfType As String, menu As String) As Task
        Await WaitForNetworkWithPopup()
        lPartno = pNo
        lPartname = pName
        lModel = Model
        lLine = Line
        Dim rsDate As String = atDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
        lActualdate = rsDate
        lLocation = Location
        lShift = Shift
        lPhase = Phase
        pCd = getPlant(lPhase)
        lLot = lot
        Dim plan_seq As String
        Dim num_char_seq As Integer
        num_char_seq = seqQty.Length
        If num_char_seq = 1 Then
            plan_seq = "00" & seqQty
        ElseIf num_char_seq = 2 Then
            plan_seq = "0" & seqQty
        Else
            plan_seq = seqQty
        End If
        lSeq = plan_seq
        lwi = wi
        sDefect = Trim(dfType) '"2" 'da_type
        lItemtype = itemType
        TypeMenu = menu
        ' ✅ โหลด defect code
        Dim md = New modelDefect()
        Dim mdsqlite = New model_api_sqlite
        ' Dim rs = md.mGetDatadefectcodeprint(lwi, lLot, lSeq, lPartno, sDefect)
        Dim retryCount As Integer = 0
        Dim maxRetries As Integer = 1000
        Dim success As Boolean = False
        Do
            rs = Await mdsqlite.mGetDatadefectcodeprint(lwi, lLot, lSeq, lPartno, sDefect)
            If rs <> "0" Then
                success = True
                Exit Do
            End If
            retryCount += 1
            ' Threading.Thread.Sleep(1000) ' รอ 1 วินาทีแล้วลองใหม่
            Await Task.Delay(1000)

        Loop While retryCount < maxRetries
        If Not success Then
            ' 'msgBox("❌ ไม่พบข้อมูล defect หรือ network ไม่พร้อมหลังจากพยายาม " & maxRetries & " ครั้ง", 'msgBoxStyle.Critical)
            printReady = False
            Exit Function
        End If
        ' ✅ เตรียมข้อมูล defect ล่วงหน้า
        Console.WriteLine("rs==>" & rs) ' ห้ามลบ
        Console.WriteLine("printding")
        defectDataList = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
        printReady = True
        ' ✅ พิมพ์
        Await WaitForNetworkWithPopup()
        Console.WriteLine("LOAD")
        PrintDocument1.Print()
        Console.WriteLine("LOAD end")
    End Function
    Public Function getPlant(phase)
        Dim plant As String = "NO_DATA"
        If phase = "10" Then
            plant = "51"
        ElseIf phase = "8" Then
            plant = "52"
        Else
            plant = "NO PLANT"
        End If
        Return plant
    End Function
    ' วางฟังก์ชันนี้แทนของเดิมทั้งหมด
    ' -------- Printing --------
    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ' 1) Guard: data ready?
        Console.WriteLine("D0")

        If Not printReady OrElse defectDataList Is Nothing OrElse defectDataList.Count = 0 Then
            Console.WriteLine("D0 IF")

            e.Cancel = True
            Return
        End If

        Using aPen As New Pen(Color.Black)
            Console.WriteLine("D1")
            aPen.Width = 3.0F

            ' ===== Layout lines (your original coordinates) =====
            ' vertical
            e.Graphics.DrawLine(aPen, 9, 5, 9, 280)
            e.Graphics.DrawLine(aPen, 120, 5, 120, 230)
            e.Graphics.DrawLine(aPen, 560, 5, 560, 192)
            e.Graphics.DrawLine(aPen, 425, 100, 425, 192)
            e.Graphics.DrawLine(aPen, 320, 190, 320, 230)
            e.Graphics.DrawLine(aPen, 460, 190, 460, 230)
            e.Graphics.DrawLine(aPen, 585, 190, 585, 280)
            e.Graphics.DrawLine(aPen, 680, 5, 680, 280)
            ' horizontal
            e.Graphics.DrawLine(aPen, 8, 5, 681, 5)
            e.Graphics.DrawLine(aPen, 120, 55, 560, 55)   ' part no
            e.Graphics.DrawLine(aPen, 120, 100, 681, 100) ' part name
            e.Graphics.DrawLine(aPen, 120, 145, 681, 145) ' model
            e.Graphics.DrawLine(aPen, 120, 190, 681, 190) ' actual date
            e.Graphics.DrawLine(aPen, 8, 230, 587, 230)   ' defect header
            e.Graphics.DrawLine(aPen, 8, 280, 681, 280)   ' bottom
            Console.WriteLine("D2")

            ' black headers
            e.Graphics.FillRectangle(Brushes.Black, 10, 100, 110, 20)
            e.Graphics.DrawString("INFO.", IN_FO.Font, Brushes.White, 46, 101)
            e.Graphics.FillRectangle(Brushes.Black, 10, 210, 110, 20)
            e.Graphics.DrawString("DEFECT QR.", IN_FO.Font, Brushes.White, 16, 214)

            ' NG / NC flag (top-right)
            If sDefect = "1" Then
                e.Graphics.FillRectangle(Brushes.Black, 560, 4, 121, 97)
                e.Graphics.DrawString("NG", Label14.Font, Brushes.White, 548, 1)
            Else
                e.Graphics.DrawString("NC", Label14.Font, Brushes.Black, 548, 1)
            End If
            Console.WriteLine("D3")

            ' ===== Static info =====
            e.Graphics.DrawString("PART NO:", title.Font, Brushes.Black, 130, 10)
            e.Graphics.DrawString(lPartno, values.Font, Brushes.Black, 150, 31)
            e.Graphics.DrawString("PART NAME:", title.Font, Brushes.Black, 130, 60)
            e.Graphics.DrawString(lPartname, values.Font, Brushes.Black, 150, 78)
            e.Graphics.DrawString("MODEL:", title.Font, Brushes.Black, 130, 105)
            e.Graphics.DrawString(lModel, values.Font, Brushes.Black, 150, 122)
            e.Graphics.DrawString("LINE:", title.Font, Brushes.Black, 430, 105)
            e.Graphics.DrawString(lLine, values.Font, Brushes.Black, 460, 122)
            e.Graphics.DrawString("LOT NO:", title.Font, Brushes.Black, 570, 105)
            e.Graphics.DrawString(lLot, values.Font, Brushes.Black, 610, 122)

            e.Graphics.DrawString("ACTUAL DATE : ", title.Font, Brushes.Black, 130, 150)
            e.Graphics.DrawString(lActualdate, values.Font, Brushes.Black, 150, 167)
            e.Graphics.DrawString("LOCATION :", title.Font, Brushes.Black, 430, 150)
            e.Graphics.DrawString(lLocation, values.Font, Brushes.Black, 445, 167)

            e.Graphics.DrawString("SHIFT : ", title.Font, Brushes.Black, 130, 197)
            e.Graphics.DrawString(lShift, values.Font, Brushes.Black, 191, 205)
            e.Graphics.DrawString("PHASE :", title.Font, Brushes.Black, 325, 197)
            e.Graphics.DrawString(lPhase, values.Font, Brushes.Black, 390, 205)
            e.Graphics.DrawString("BOX NO :", title.Font, Brushes.Black, 470, 197)
            e.Graphics.DrawString("001", values.Font, Brushes.Black, 510, 207)
            Console.WriteLine("D4")

            ' DEFECT header
            e.Graphics.DrawString("DEFECT CODE :", detail_code.Font, Brushes.Black, 15, 236)

            ' ===== Total QTY =====
            Dim totalDefectAll As Integer = 0
            For Each d In defectDataList
                Dim n As Integer = 0
                Integer.TryParse(Convert.ToString(d("total_defect")), n)
                totalDefectAll += n
            Next

            e.Graphics.DrawString("QTY :", title.Font, Brushes.Black, 570, 150)
            e.Graphics.DrawString(totalDefectAll.ToString(), values.Font, Brushes.Black, 610, 167)

            ' pad 5 digits
            If totalDefectAll < 0 Then
                lQtydefect = "00000"
            ElseIf totalDefectAll < 10 Then
                lQtydefect = "0000" & totalDefectAll.ToString()
            ElseIf totalDefectAll < 100 Then
                lQtydefect = "000" & totalDefectAll.ToString()
            ElseIf totalDefectAll < 1000 Then
                lQtydefect = "00" & totalDefectAll.ToString()
            ElseIf totalDefectAll < 10000 Then
                lQtydefect = "0" & totalDefectAll.ToString()
            Else
                lQtydefect = totalDefectAll.ToString()
            End If
            Console.WriteLine("D5")

            ' ===== Paginated defect list =====
            Dim startX As Single = 15.0F
            Dim startY As Single = 250.0F
            Dim leftX As Single = startX
            Dim curY As Single = startY
            Dim rightLimit As Single = 585.0F   ' before 585 line
            Dim bottomLimit As Single = 280.0F  ' before 280 line
            Dim f As Font = detail_code.Font
            Dim qrDetailsBuilder As New System.Text.StringBuilder()
            Dim i As Integer = currentIndex
            Dim itemPad As Single = 4.0F    ' ระยะช่องไฟระหว่างชิ้น ต่อจาก measurestring (จากเดิม 15)
            While i < defectDataList.Count
                Dim item = defectDataList(i)
                Dim code As String = Convert.ToString(item("da_code")).Trim()
                Dim qtyTxt As String = Convert.ToString(item("total_defect")).Trim()

                ' แสดงบนสติกเกอร์: ต่อ " | " ถ้าไม่ใช่รายการสุดท้ายของทั้งชุด
                Dim isLastOverall As Boolean = (i = defectDataList.Count - 1)
                Dim piece As String = $"{code} = {qtyTxt}{If(isLastOverall, "", "  |")}"

                ' สำหรับ QR: เก็บรายละเอียดด้วย " | " คั่นเหมือนเดิม
                If qrDetailsBuilder.Length > 0 Then qrDetailsBuilder.Append(" | ")
                qrDetailsBuilder.Append($"{code} = {qtyTxt}")

                ' วัดขนาด/ตัดบรรทัด
                Dim size As SizeF = e.Graphics.MeasureString(piece, f)
                Dim lineHeight As Single = size.Height

                If leftX + size.Width > rightLimit Then
                    leftX = size.Width + itemPad
                    curY += lineHeight
                End If

                If curY + lineHeight > bottomLimit Then
                    qrDefectcodedetails = qrDetailsBuilder.ToString()
                    qrDefectinfo = $"DF {sDefect} {lLine} {lwi} {lSeq} {lLot} {pCd} {lBoxno} {lQtydefect} {lPartno}"

                    PictureBox1.Image = QR_Generator.Encode(qrDefectinfo)
                    e.Graphics.DrawImage(PictureBox1.Image, 20, 10, 85, 85)
                    e.Graphics.DrawImage(PictureBox1.Image, 592, 195, 80, 80)

                    PictureBox1.Image = QR_Generator.Encode(qrDefectcodedetails)
                    e.Graphics.DrawImage(PictureBox1.Image, 20, 125, 85, 85)

                    currentIndex = i
                    e.HasMorePages = True
                    Return
                End If

                e.Graphics.DrawString(piece, f, Brushes.Black, leftX, curY)
                leftX += size.Width
                i += 1
            End While
            Console.WriteLine("D6")

            ' finished all items in this page
            currentIndex = 0
            e.HasMorePages = False

            ' final QR strings (all items)
            Console.WriteLine("D6_1")
            qrDefectcodedetails = qrDetailsBuilder.ToString()
            Console.WriteLine("qrDefectcodedetails LENGTH = " & qrDefectcodedetails.Length)
            Console.WriteLine("qrDefectcodedetails VALUE  = " & qrDefectcodedetails)
            Console.WriteLine("D6_2")

            qrDefectinfo = $"DF {sDefect} {lLine} {lwi} {lSeq} {lLot} {pCd} {lBoxno} {lQtydefect} {lPartno}"
            Console.WriteLine("qrDefectinfo LENGTH = " & qrDefectinfo.Length)
            Console.WriteLine("qrDefectinfo VALUE  = " & qrDefectinfo)
            Console.WriteLine("D6_3")

            Try
                Console.WriteLine("QR1 START")

                ' QR1 = ใช้ตัวเดิม
                Dim qrInfoImage As Image = QR_Generator.Encode(qrDefectinfo)
                PictureBox1.Image = qrInfoImage
                e.Graphics.DrawImage(qrInfoImage, 20, 10, 85, 85)      ' top-left
                e.Graphics.DrawImage(qrInfoImage, 592, 195, 80, 80)    ' bottom-right

                Console.WriteLine("QR1 OK")
                Console.WriteLine("QR2 START")

                ' QR2 = ใช้ QRCoder
                Dim qrDetailImage As Image = GenerateQrImageByQRCoder(qrDefectcodedetails)
                PictureBox1.Image = qrDetailImage
                e.Graphics.DrawImage(qrDetailImage, 20, 125, 85, 85)   ' bottom-left

                Console.WriteLine("QR2 OK")

            Catch ex As Exception
                Console.WriteLine("QR ERROR TYPE = " & ex.GetType().FullName)
                Console.WriteLine("QR ERROR MSG  = " & ex.Message)
                Console.WriteLine("QR INFO       = " & qrDefectinfo)
                Console.WriteLine("QR DETAILS    = " & qrDefectcodedetails)

                e.HasMorePages = False
                e.Cancel = True
                Return
            End Try

            Console.WriteLine("D6_4")

            ' insert tag defect
            Try
                Dim date_now As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                Dim md = New modelDefect()
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    Dim statusTrasnffer As Integer = 1
                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}",
                  lwi, lLine, lPartno, lItemtype, lLot, lSeq, sDefect,
                  CDbl(Val(lQtydefect)), TypeMenu, "001",
                  qrDefectinfo, qrDefectcodedetails, lItemtype,
                  date_now, lLine, date_now, lLine, Working_Pro.pwi_id)

                    Console.WriteLine(String.Format(
    "mas_mInserttagDefect => lwi={0}, lLine={1}, lPartno={2}, lItemtype={3}, lLot={4}, lSeq={5}, sDefect={6}, QtyRaw={7}, QtyConvert={8}, TypeMenu={9}, Code=001, qrDefectinfo={10}, qrDefectcodedetails={11}, date_now={12}, pwi_id={13}, statusTrasnffer={14}",
    lwi,
    lLine,
    lPartno,
    lItemtype,
    lLot,
    lSeq,
    sDefect,
    lQtydefect,
    CDbl(Val(lQtydefect)),
    TypeMenu,
    qrDefectinfo,
    qrDefectcodedetails,
    date_now,
    Working_Pro.pwi_id,
    statusTrasnffer
))
                    Dim rsInserttagdefect = md.mInserttagdefect(lwi, lLine, lPartno, lItemtype, lLot, lSeq, sDefect,
                                                                CDbl(Val(lQtydefect)), TypeMenu, "001",
                                                                qrDefectinfo, qrDefectcodedetails, lItemtype,
                                                                date_now, lLine, date_now, lLine, Working_Pro.pwi_id)
                    Dim sqlitersInserttagdefect = model_api_sqlite.mas_mInserttagDefect(lwi, lLine, lPartno, lItemtype, lLot, lSeq, sDefect,
                                                                                         CDbl(Val(lQtydefect)), TypeMenu, "001",
                                                                                         qrDefectinfo, qrDefectcodedetails, lItemtype,
                                                                                         date_now, lLine, date_now, lLine, Working_Pro.pwi_id, statusTrasnffer)
                Else
                    Dim statusTrasnffer As Integer = 0
                    Dim sqlitersInserttagdefect = model_api_sqlite.mas_mInserttagDefect(lwi, lLine, lPartno, lItemtype, lLot, lSeq, sDefect,
                                                                                         CDbl(Val(lQtydefect)), TypeMenu, "001",
                                                                                         qrDefectinfo, qrDefectcodedetails, lItemtype,
                                                                                         date_now, lLine, date_now, lLine, Working_Pro.pwi_id, statusTrasnffer)
                End If
            Catch ex As Exception
                ' fallback to sqlite
                Dim date_now As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                Dim statusTrasnffer As Integer = 0
                Dim sqlitersInserttagdefect = model_api_sqlite.mas_mInserttagDefect(lwi, lLine, lPartno, lItemtype, lLot, lSeq, sDefect,
                                                                                     CDbl(Val(lQtydefect)), TypeMenu, "001",
                                                                                     qrDefectinfo, qrDefectcodedetails, lItemtype,
                                                                                     date_now, lLine, date_now, lLine, Working_Pro.pwi_id, statusTrasnffer)
            End Try
        End Using
        Console.WriteLine("D7")

    End Sub
    Private Function GenerateQrImageByQRCoder(qrText As String) As Image
        Dim qrGenerator As New QRCoder.QRCodeGenerator()
        Dim qrData As QRCoder.QRCodeData =
        qrGenerator.CreateQrCode(qrText, QRCoder.QRCodeGenerator.ECCLevel.L)

        Dim qrCode As New QRCoder.QRCode(qrData)
        Return qrCode.GetGraphic(5)
    End Function
End Class