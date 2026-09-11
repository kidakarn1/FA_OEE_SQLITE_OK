Imports System.Drawing.Printing
Imports System.Drawing.Text
Imports ZXing
Public Class TEST_PRINTLABEL
    Private WithEvents printDocument1 As New PrintDocument()
    Private WithEvents printDocument2 As New PrintDocument()
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' กำหนดค่า PrinterSettings สำหรับเครื่องปริ้นที่หนึ่ง (เปลี่ยน "Printer1" เป็นชื่อจริงของเครื่องปริ้นที่ 1)
        printDocument1.PrinterSettings.PrinterName = "Citizen CL-S400DT TAG FA"
        ' กำหนดค่า PrinterSettings สำหรับเครื่องปริ้นที่สอง (เปลี่ยน "Printer2" เป็นชื่อจริงของเครื่องปริ้นที่ 2)
        printDocument2.PrinterSettings.PrinterName = "Citizen Label"
        Dim installedFonts As New InstalledFontCollection()
        For Each fontFam As FontFamily In installedFonts.Families
            comboFontPart.Items.Add(fontFam.Name)
            ComboFontModel.Items.Add(fontFam.Name)
            ComboBoxFontFooter.Items.Add(fontFam.Name)
            comboFontPart.Items.Add(fontFam.Name)
        Next
    End Sub
    Private Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)
        ' แทนที่ด้วยคำสั่งหรือเนื้อหาการพิมพ์จริง ๆ ของคุณ
        Dim val As String = RichTextBoxPart.Text
        e.Graphics.DrawString(val, Label1.Font, Brushes.Black, WidthPart.Text, HeightPart.Text)
    End Sub
    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        ' เริ่มกระบวนการการพิมพ์สำหรับเครื่องปริ้นที่หนึ่ง
        'printDocument1.Print()

        ' เริ่มกระบวนการการพิมพ์สำหรับเครื่องปริ้นที่สอง
        printDocument2.Print()
    End Sub
    Private Sub PrintDocument31_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDocument1.PrintPage
        '  Dim val As String = RichTextBox1.Text
        '  e.Graphics.DrawString(val, Label1.Font, Brushes.Black, widthText.Text, HeightText.Text)
    End Sub
    Private Sub PrintDocument32_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDocument2.PrintPage
        Dim RBP As String = RichTextBoxPart.Text
        Dim RBM As String = RichTextBoxModel.Text
        Dim RBF As String = RichTextBoxFooter.Text
        Dim RBC As String = RichTextBoxBarcode.Text
        ' Load the image you want to print
        ' Set the position and size where you want to draw the image on the page
        Dim widthP As Integer = WidthPart.Text
        Dim heightP As Integer = HeightPart.Text

        Dim WidthM As Integer = WidthModel.Text
        Dim HidthM As Integer = HeightModel.Text

        Dim WidthF As Integer = WidthFooter.Text
        Dim HidthF As Integer = HeightFooter.Text

        Dim WidthB As Integer = WidthBarcode.Text
        Dim HidthB As Integer = HeightBarcode.Text

        Dim WidthB2 As Integer = WidthBarcode2.Text
        Dim HidthB2 As Integer = HeightBarcode2.Text

        Dim barcodeWriter As New BarcodeWriter()
        barcodeWriter.Format = BarcodeFormat.CODE_128
        Dim barcodeBitmap As Bitmap = barcodeWriter.Write(RBC) ' Set your barcode data here
        picBarcode.Image = barcodeBitmap
        ' Draw the image on the page
        e.Graphics.DrawImage(picBarcode.Image, WidthB2, HidthB2, WidthB, HidthB)
        Dim x As Integer = 8
        Dim y As Integer = 20
        Dim width2 As Integer = 32
        Dim height2 As Integer = 27
        Dim fs As Integer = fontSizePart.Text
        ' Draw the image on the page
        Dim PP As FontStyle ' ประกาศตัวแปร PP เพื่อใช้เก็บ FontStyle ของตัวหนา (Bold)
        Dim FP As FontStyle ' ประกาศตัวแปร FP เพื่อใช้เก็บ FontStyle ของตัวหนังสือครั้ง (Regular)

        ' ตรวจสอบและกำหนด FontStyle ของตัวหนา (Bold)
        If ComboBox1.Text = "B" Then
            PP = FontStyle.Bold
        Else
            PP = FontStyle.Regular
        End If

        ' ตรวจสอบและกำหนด FontStyle ของตัวหนังสือครั้ง (Regular)
        If ComboBox2.Text = "B" Then
            FP = FontStyle.Bold
        Else
            FP = FontStyle.Regular
        End If
        ' สร้าง Font โดยใช้ FontStyle ที่กำหนดใน ComboBox และ TextBox
        Dim fontPart As New Font(comboFontPart.Text, CSng(fontSizePart.Text), FontStyle.Bold)
        Dim fontModel As New Font(ComboFontModel.Text, CSng(fontSizeModel.Text), FontStyle.Bold)
        Dim fontFooter As New Font(ComboBoxFontFooter.Text, CSng(fontSizeFooter.Text), FontStyle.Bold)
        e.Graphics.DrawString(RBP, fontPart, Brushes.Black, widthP, heightP)
        e.Graphics.DrawString(RBM, fontModel, Brushes.Black, WidthM, HidthM)
        e.Graphics.DrawString(RBF, fontFooter, Brushes.Black, WidthF, HidthF)
        ' e.Graphics.DrawString("BARCODE", lbQrCode.Font, Brushes.Black, 10, 6)
        ' e.Graphics.DrawString("TBKK Co.,Ltd.", lbButton.Font, Brushes.Black, 35, 105)
        'e.Graphics.DrawString("DA17", lbButton.Font, Brushes.Black, 161, 105)
        'e.Graphics.DrawString("002", lbButton.Font, Brushes.Black, 205, 105)
        'e.Graphics.DrawString("0013", lbButton.Font, Brushes.Black, 238, 105)
    End Sub
End Class
