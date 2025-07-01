Imports System.IO.Ports
Imports System.Threading
Module SensorReader
    Dim WithEvents serialPort As New SerialPort()
    Sub Main()
        ' กำหนดค่าพอร์ต COM และอื่น ๆ ตามที่ต้องการ
        serialPort.PortName = "COM3" ' ตั้งค่าตามที่เซนเซอร์ของคุณต่อ
        serialPort.BaudRate = 9600 ' ตั้งค่าตามที่เซนเซอร์ของคุณต้องการ
        ' อื่น ๆ ตามที่จำเป็น
        ' เปิดพอร์ต COM
        Try
            serialPort.Open()
        Catch ex As Exception
            'Console.WriteLine("Error opening serial port: " & ex.Message)
            Return
        End Try
        ' อ่านข้อมูลจากเซนเซอร์
        While True
            Try
                ' อ่านข้อมูลจากพอร์ต COM
                Dim sensorData As String = serialPort.ReadLine()
                ' ประมวลผลข้อมูลตามที่ต้องการ
                'Console.WriteLine("Sensor Data: " & sensorData)
                ' สามารถทำประมวลผลเพิ่มเติมหรือส่งข้อมูลไปยังส่วนอื่น ๆ ต่อไปได้
            Catch ex As Exception
                'Console.WriteLine("Error reading from serial port: " & ex.Message)
            End Try
            ' หน่วงเวลาเพื่อไม่ให้โปรแกรมทำงานมากเกินไป
            Thread.Sleep(1000)
        End While
    End Sub
    ' ปิดพอร์ต COM เมื่อโปรแกรมจบการทำงาน
    Private Sub SensorReader_FormClosing(sender As Object, e As EventArgs)
        If serialPort.IsOpen Then
            serialPort.Close()
        End If
    End Sub
End Module
