Imports NationalInstruments.DAQmx

Public Class CheckWindow
    Public Shared digitalReadTask_new_dio As Task
    Public Shared digitalReadTask_new_dio_check_model As Task
    Public Shared reader_new_dio As DigitalSingleChannelReader
    Public Shared reader_new_dio_check_model As DigitalSingleChannelReader
    Public Shared data_new_dio As UInt32

    Public Const PART_A As String = "898248-1601"
    Public Const PART_B As String = "898248-1612"
    Public Function Per_CheckCounter() As Boolean
        Try
            Dim rsWindows As Boolean = Working_Pro.CheckOs()
            Working_Pro.rsWindow = rsWindows
            Return rsWindows
        Catch ex As Exception
            'msgBox("[Per_CheckCounter] " & ex.Message)
            Return False
        End Try
    End Function
    ' ========= ฟังก์ชันกลาง: สร้าง/รีคริเอต Task + Reader =========
    Private Function SetupReader(ByRef taskRef As Task,
                                 ByRef readerRef As DigitalSingleChannelReader,
                                 chanSpec As String) As String
        Try
            ' เคลียร์งานเดิม
            If taskRef IsNot Nothing Then
                Try : taskRef.Dispose() : Catch : End Try
            End If

            ' งานใหม่ + ช่องสัญญาณ
            taskRef = New Task()
            taskRef.DIChannels.CreateChannel(
                chanSpec, "ProductionLines",
                ChannelLineGrouping.OneChannelForAllLines)

            ' สร้าง reader
            readerRef = New DigitalSingleChannelReader(taskRef.Stream)

            ' (ไม่จำเป็นต้อง Start สำหรับ on-demand แต่ Start ก็ได้)
            'taskRef.Start()

            Return "OK"
        Catch ex As Exception
            Return "[SetupReader] " & ex.Message
        End Try
    End Function

    ' ========= เหมือนเดิม แต่เรียกใช้ฟังก์ชันกลาง =========
    Public Function count_NIMAX() As String
        Try
            If Not Working_Pro.rsWindow Then Return "Not Supported on this OS"

            Dim r = SetupReader(digitalReadTask_new_dio, reader_new_dio,
                                "Dev1/port0/line0:7")
            If r <> "OK" Then Return r

            Working_Pro.Timer_new_dio.Start()
            Return "OK"
        Catch
            Return "Please Check USB DIO"
        End Try
    End Function

    Public Function count_NIMAX_CheckModel() As String
        Try
            If Not Working_Pro.rsWindow Then Return "Not Supported on this OS"

            Dim r = SetupReader(digitalReadTask_new_dio_check_model, reader_new_dio_check_model,
                                "Dev1/port2/line0:3")
            If r <> "OK" Then Return r

            Return "OK"
        Catch
            Return "Please Check USB DIO"
        End Try
    End Function
End Class
