Imports System.Drawing.Printing
Imports ZXing
Public Class PrintLabelBreak
    Private WithEvents printDocument1 As New PrintDocument()
    Private WithEvents printDocument2 As New PrintDocument()
    Dim G_partNumber As String = ""
    Dim G_breakName As String = ""
    Dim G_lotNo As String = ""
    Dim G_seq As String = ""
    Dim G_countqty As String = ""
    Public Sub loadData(partNumber As String, breakName As String, lotNo As String, seq As String, countqty As String)
        Try
            Dim plan_seq As String = ""
            Dim qty As String = ""
            Dim num_char_seq As Integer = 0
            Dim num_char_qty As Integer = 0
            num_char_seq = seq.Length
            num_char_qty = countqty.Length
            If num_char_seq = 1 Then
                plan_seq = "00" & seq
            ElseIf num_char_seq = 2 Then
                plan_seq = "0" & seq
            Else
                plan_seq = seq
            End If

            If num_char_qty = 1 Then
                qty = "000" & countqty
            ElseIf num_char_qty = 2 Then
                qty = "00" & countqty
            ElseIf num_char_qty = 3 Then
                qty = "0" & countqty
            Else
                qty = countqty
            End If
            G_partNumber = partNumber
            G_breakName = breakName
            G_lotNo = lotNo
            G_seq = plan_seq
            G_countqty = qty
            printDocument1 = New PrintDocument()
            printDocument2 = New PrintDocument()
            printDocument2.PrinterSettings.PrinterName = "Citizen Label"
            printDocument2.Print()
        Catch ex As Exception
            MsgBox("Please Check Printer Label. = >")
        End Try
    End Sub
    Private Sub PrintDocument31_PrintPage_1(sender As Object, e As PrintPageEventArgs) Handles printDocument2.PrintPage
        Try
            Dim x As Integer = 8
            Dim y As Integer = 98
            Dim width As Integer = 32
            Dim height As Integer = 27
            Dim barcodeWriter As New BarcodeWriter()
            barcodeWriter.Format = ZXing.BarcodeFormat.CODE_128 ' Set the correct barcode format, e.g., CODE_128
            Dim barcodeOptions As New ZXing.Common.EncodingOptions()
            barcodeWriter.Options = barcodeOptions
            Dim barcodeBitmap As Bitmap = barcodeWriter.Write(G_partNumber & "    ") ' Set your barcode data here
            picBarcode.Image = barcodeBitmap
            ' Draw the image on the page
            e.Graphics.DrawImage(picBarcode.Image, 15, 50, 245, 45)
            e.Graphics.DrawImage(logoLabel.Image, x, y, width, height)
            e.Graphics.DrawString(G_partNumber, lbPartNumber.Font, Brushes.Black, 24, 20)
            e.Graphics.DrawString(G_breakName, lbPartMmodelBreak.Font, Brushes.Black, 190, 20)
            e.Graphics.DrawString("TBKK Co.,Ltd.", lbButton.Font, Brushes.Black, 35, 105)
            e.Graphics.DrawString(G_lotNo, lbButton.Font, Brushes.Black, 156, 105)
            e.Graphics.DrawString(G_seq, lbButton.Font, Brushes.Black, 199, 105)
            e.Graphics.DrawString(G_countqty, lbButton.Font, Brushes.Black, 232, 105)
            Me.Close()
        Catch ex As Exception
            MsgBox("error ===>" & ex.Message)
        End Try
    End Sub
End Class