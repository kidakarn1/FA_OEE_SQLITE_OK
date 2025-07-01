Public Class Settinglamp
    Public Shared arr_list_id_lamp As List(Of String) = New List(Of String)
    Public ID As Integer = 0
    Public comportTowerLamp As String = "COM6"
    Private Sub Settinglamp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.Text = "7"
        TextBox1.Text = "0"
        Load_Mst_lamp()
        For i As Integer = 1 To 25
            TowerLamp(TextBox2.Text, TextBox1.Text)

        Next
    End Sub
    Public Sub Load_Mst_lamp()
        'TowerLamp(8, 2000) 'Yellow
        TowerLamp(5, 1000)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '   Dim dNGId = confix.GetId(cbNG.Text)
        '   Dim dNCId = confix.GetId(cbNC.Text)
        '   Dim dNCMaxId = confix.GetId(cbNCMax.Text)
        '   Dim dRunId = confix.GetId(cbRun.Text)
        '   Dim dStopId = confix.GetId(cbStop.Text)
        '   Dim dLossId = confix.GetId(cbLoss.Text)
        '   Dim cf = New confix
        '   cf.UpdateLamp(ID, dNGId, dNCId, dNCMaxId, dRunId, dStopId, dLossId)
        '   Me.Close()
        TowerLamp(TextBox2.Text, TextBox1.Text)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
    Public Sub TowerLamp(DataBits As Integer, WriteLine As Integer)
        If SerialPort1.IsOpen() Then
            SerialPort1.Close()
        End If
        Try
            SerialPort1.PortName = comportTowerLamp
            SerialPort1.BaudRate = 9600
            SerialPort1.Parity = IO.Ports.Parity.None
            SerialPort1.StopBits = IO.Ports.StopBits.One
            SerialPort1.DataBits = DataBits
            SerialPort1.Open()
            SerialPort1.WriteLine(WriteLine)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ' SerialPort1.PortName = comportTowerLamp
        ' SerialPort1.BaudRate = 9600
        ' SerialPort1.Parity = IO.Ports.Parity.None
        ' SerialPort1.StopBits = IO.Ports.StopBits.One
        '  SerialPort1.DataBits = 7

        ' เปิดการเชื่อมต่อ SerialPort
        'SerialPort1.Open()
        'ControlTowerLamp("1") ' เปิดไฟหรือเปลี่ยนสีของ Tower Lamp
        ''Threading.Thread.Sleep(1000) ' รอ 1 วินาที (ในกรณีที่มีการส่งคำสั่งต่อเนื่องอาจต้องปรับเวลารอให้เหมาะสม)
        'ControlTowerLamp("0") ' ให้ไฟยังคงติด

    End Sub
    ' ฟังก์ชันสำหรับควบคุมสถานะของ Tower Lamp
    Sub ControlTowerLamp(ByVal command As String)
        SerialPort1.WriteLine(command)
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If SerialPort1.IsOpen() Then
            SerialPort1.Close()
        End If
        SerialPort1.PortName = comportTowerLamp
        SerialPort1.BaudRate = 9600
        SerialPort1.Parity = IO.Ports.Parity.None
        SerialPort1.StopBits = IO.Ports.StopBits.One
        SerialPort1.DataBits = TextBox2.Text
        SerialPort1.Open()
        SerialPort1.WriteLine(TextBox1.Text)
        Console.ReadLine()
        'TowerLamp(7, 17) green off

        'TowerLamp(5, 11)
    End Sub
    Public Sub ResetRed()
        If SerialPort1.IsOpen() Then
            SerialPort1.Close()
        End If
        SerialPort1.PortName = comportTowerLamp
        SerialPort1.BaudRate = 9600
        SerialPort1.Parity = IO.Ports.Parity.None
        SerialPort1.StopBits = IO.Ports.StopBits.One
        SerialPort1.DataBits = 5
        SerialPort1.Open()
        SerialPort1.WriteLine(9999)
        Console.ReadLine()

        If SerialPort1.IsOpen() Then
            SerialPort1.Close()
        End If
        SerialPort1.PortName = comportTowerLamp
        SerialPort1.BaudRate = 9600
        SerialPort1.Parity = IO.Ports.Parity.None
        SerialPort1.StopBits = IO.Ports.StopBits.One
        SerialPort1.DataBits = 6
        SerialPort1.Open()
        SerialPort1.WriteLine(11)
        Console.ReadLine()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        ResetRed()
    End Sub

    Private Sub cbRun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbRun.SelectedIndexChanged

    End Sub
End Class