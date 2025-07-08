Imports System.Globalization
Imports System.Net.NetworkInformation
Imports System.Web.Script.Serialization
Public Class PrintDefectAdmin
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
    Private Sub PrintDefectAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PrintDocument1.Print()
    End Sub
    Public Async Function WaitForNetworkWithPopup() As Task
        Do While Not IsNetworkAvailable() OrElse Not My.Computer.Network.Ping(Backoffice_model.svp_ping)
            If Not load_show.Visible Then
                load_show.Show()
            End If
            Console.WriteLine("⛔ ยังไม่มี Network หรือ Ping ไม่ผ่าน... รอ 1 วินาที")
            Await Task.Delay(3000)
        Loop
        If load_show.Visible Then
            load_show.Hide()
        End If
    End Function
    Private Function IsNetworkAvailable() As Boolean
        Return NetworkInterface.GetIsNetworkAvailable()
    End Function
    Public Async Sub Set_parameter_print(pNo As String, pName As String, Model As String, Line As String, atDate As Date, Location As String, Shift As String, Phase As String, lot As String, qtyDefect As String, seqQty As String, wi As String, itemType As String, dfType As String, menu As String)
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
            rs = md.mGetDatadefectcodeprint(lwi, lLot, lSeq, lPartno, sDefect)
            If rs <> "0" Then
                success = True
                Exit Do
            End If
            retryCount += 1
            Threading.Thread.Sleep(1000) ' รอ 1 วินาทีแล้วลองใหม่
        Loop While retryCount < maxRetries
        If Not success Then
            ' MsgBox("❌ ไม่พบข้อมูล defect หรือ network ไม่พร้อมหลังจากพยายาม " & maxRetries & " ครั้ง", MsgBoxStyle.Critical)
            printReady = False
            Exit Sub
        End If
        ' ✅ เตรียมข้อมูล defect ล่วงหน้า
        defectDataList = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
        printReady = True
        ' ✅ พิมพ์
        Await WaitForNetworkWithPopup()
        PrintDocument1.Print()
    End Sub
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
    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        If Not printReady OrElse defectDataList Is Nothing OrElse defectDataList.Count = 0 Then
            ' MsgBox("⚠️ ข้อมูล defect ยังไม่พร้อม หรือ network ยังไม่มา", MsgBoxStyle.Exclamation)
            Console.WriteLine("⚠️ ข้อมูล defect ยังไม่พร้อม หรือ network ยังไม่มา", MsgBoxStyle.Exclamation)
            '  MsgBox("⚠️ ข้อมูล defect ยังไม่พร้อม หรือ network ยังไม่มา", MsgBoxStyle.Exclamation)
            ' MsgBox("⚠️ ข้อมูล defect ยังไม่พร้อม หรือ network ยังไม่มา", MsgBoxStyle.Exclamation)
            e.Cancel = True 'สั่งให้ยกเลิกการพิมพ์หน้านี้
            Return ' ออกจากฟังก์ชันทันที
        End If
        '  Try
        Dim md = New modelDefect()
        Dim mdsqlite = New model_api_sqlite()
        'MsgBox("lwi ==>" & lwi & "===seq====>" & lSeq)
        ' Dim rs = md.mGetDatadefectcodeprint(lwi, lLot, lSeq, lPartno, sDefect)
        ' MsgBox("rs====>" & rs)
        If rs <> "0" Then
            Dim aPen = New Pen(Color.Black)
            e.Graphics.DrawLine(Pens.Azure, 10, 10, 20, 20)
            aPen.Width = 3.0F  'border 
            ' e.Graphics.FillRectangle(Brushes.Black, 50, 100, 200, 150) ' background back
            ' e.Graphics.DrawString(Defect_LB_STATUS, Label13.Font, Brushes.Black, 5, 5) 
            'TAG LAYOUT แนวตั้ง
            e.Graphics.DrawLine(aPen, 9, 5, 9, 280) 'แก้ตำแหน่งที่ 1 , 3เส้นเปิด  NC/NG


            e.Graphics.DrawLine(aPen, 120, 5, 120, 230) 'แก้ตำแหน่งที่ 1 , 3เส้นเปิด  NC/NG


            e.Graphics.DrawLine(aPen, 680, 5, 680, 250) 'แก้ตำแหน่งที่ 1 , 3 เส้นปิด  NC/NG


            e.Graphics.DrawLine(aPen, 560, 5, 560, 192) 'แก้ตำแหน่งที่ 1 , 3 เส้นปิด  NC/NG


            e.Graphics.DrawLine(aPen, 425, 100, 425, 192) 'แก้ตำแหน่งที่ 1 , 3 เส้นปิด  NC/NG


            e.Graphics.DrawLine(aPen, 320, 190, 320, 230) 'แก้ตำแหน่งที่ 1 , 3 เส้นปิด  NC/NG


            e.Graphics.DrawLine(aPen, 460, 190, 460, 230) 'แก้ตำแหน่งที่ 1 , 3 เส้นปิด  NC/NG


            e.Graphics.DrawLine(aPen, 585, 190, 585, 280) 'แก้ตำแหน่งที่ 1 , 3 เส้นปิด  NC/NG

            e.Graphics.DrawLine(aPen, 680, 5, 680, 280) 'แก้ตำแหน่งที่ 1 , 3 เส้นปิด  NC/NG

            'Horizontal แนวนอน

            e.Graphics.DrawLine(aPen, 8, 5, 681, 5) 'แก้ตำแหน่งที่ 2 , 4

            e.Graphics.DrawLine(aPen, 120, 55, 560, 55) 'แก้ตำแหน่งที่ 2 , 4 part no

            e.Graphics.DrawLine(aPen, 120, 100, 681, 100) 'แก้ตำแหน่งที่ 2 , 4 part name

            e.Graphics.DrawLine(aPen, 120, 145, 681, 145) 'แก้ตำแหน่งที่ 2 , 4 model

            e.Graphics.DrawLine(aPen, 120, 190, 681, 190) 'แก้ตำแหน่งที่ 2 , 4 Actual Date

            e.Graphics.FillRectangle(Brushes.Black, 10, 100, 110, 20) ' background back
            e.Graphics.DrawString("INFO.", IN_FO.Font, Brushes.White, 46, 101)
            e.Graphics.FillRectangle(Brushes.Black, 10, 210, 110, 20) ' background back
            e.Graphics.DrawString("DEFECT QR.", IN_FO.Font, Brushes.White, 16, 214)
            e.Graphics.DrawLine(aPen, 8, 230, 587, 230) 'แก้ตำแหน่งที่ 2 , 4
            If sDefect = "1" Then 'NG
                e.Graphics.FillRectangle(Brushes.Black, 560, 4, 121, 97) ' NG/NC BACKGROUD Black
                e.Graphics.DrawString("NG", Label14.Font, Brushes.White, 548, 1) ' left top
            ElseIf sDefect = "2" Then ' NC
                e.Graphics.DrawString("NC", Label14.Font, Brushes.Black, 548, 1) ' left top
            End If
            e.Graphics.DrawLine(aPen, 8, 280, 681, 280) 'แก้ตำแหน่งที่ 2 , 4

            'Details'
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
            e.Graphics.DrawString("DEFECT CODE :", detail_code.Font, Brushes.Black, 15, 236)
            Dim Cdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
            Dim i As Integer = 1
            Dim cNumber As Integer = 1
            Dim dataDefect As String = " " '
            Dim dataQrdefectcodedetails As String = ""
            Dim mgtop As Integer = 250
            Dim mgleft As Integer = 15
            Dim tDefect As String = ""
            ' For Each item As Object In Cdata
            For Each item As Object In defectDataList
                tDefect = item("total_defect_all").ToString()
                dataDefect = Trim(item("da_code").ToString()) & " = " & Trim(item("total_defect").ToString())
                dataQrdefectcodedetails += Trim(item("da_code").ToString()) & " = " & Trim(item("total_defect").ToString())
                If cNumber < Cdata.count Then
                    dataDefect += " | "
                    dataQrdefectcodedetails += " | "
                End If
                If cNumber > 20 Then
                    dataDefect = "..."
                End If
                e.Graphics.DrawString(dataDefect, detail_code.Font, Brushes.Black, mgleft, mgtop)
                ' If cNumber > 20 Then
                ' GoTo outloop
                ' End If
                mgleft += 52
                If i Mod 10 = 0 And cNumber < 20 Then
                    mgtop = mgtop + 15
                    mgleft = 15
                    i = 0
                End If
                i += 1
                cNumber += 1
            Next
            e.Graphics.DrawString("QTY :", title.Font, Brushes.Black, 570, 150)
            e.Graphics.DrawString(tDefect, values.Font, Brushes.Black, 610, 167)
            If Len(tDefect) = 1 Then
                lQtydefect = "0000" & tDefect
            ElseIf Len(tDefect) = 2 Then
                lQtydefect = "000" & tDefect
            ElseIf Len(tDefect) = 3 Then
                lQtydefect = "00" & tDefect
            ElseIf Len(tDefect) = 4 Then
                lQtydefect = "0" & tDefect
            ElseIf Len(tDefect) = 5 Then
                lQtydefect = tDefect
            End If
            qrDefectinfo = "DF" & " " & sDefect & " " & lLine & " " & lwi & " " & lSeq & " " & lLot & " " & pCd & " " & lBoxno & " " & lQtydefect & " " & lPartno
            qrDefectcodedetails = dataQrdefectcodedetails
outloop:
            PictureBox1.Image = QR_Generator.Encode(qrDefectinfo)
            e.Graphics.DrawImage(PictureBox1.Image, 20, 10, 85, 85) 'top left'
            e.Graphics.DrawImage(PictureBox1.Image, 592, 195, 80, 80) 'buttom right'
            PictureBox1.Image = QR_Generator.Encode(qrDefectcodedetails)
            e.Graphics.DrawImage(PictureBox1.Image, 20, 125, 85, 85) 'bottom left'
            Dim date_now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            Dim dti_status_flg = "" 'FG = 1 , 2 = CP
            'If Backoffice_model.printedTags.Contains(qrDefectinfo) Then
            '  MsgBox("Tag นี้พิมพ์แล้ว ไม่สามารถพิมพ์ซ้ำได้", MsgBoxStyle.Exclamation)
            'Exit Sub
            'Else
            '   Backoffice_model.printedTags.Add(qrDefectinfo)

            '  End If
        End If
        ' Catch ex As Exception
        '  load_show.Show()
        '  End Try
    End Sub
End Class