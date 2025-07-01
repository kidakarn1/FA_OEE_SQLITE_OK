Imports System
Imports System.Runtime.InteropServices
Imports NationalInstruments.DAQmx
Public Class CheckWindow
    Public Shared digitalReadTask_new_dio As Task
    Public Shared reader_new_dio As DigitalSingleChannelReader
    Public Shared data_new_dio As UInt32

    Public Function Per_CheckCounter() As Boolean
        Try
            Dim rsWindows As Boolean = Working_Pro.CheckOs()
            Working_Pro.rsWindow = rsWindows
            Return rsWindows
        Catch ex As Exception
            MsgBox("[Per_CheckCounter] " & ex.Message)
            Return False
        End Try
    End Function
    Public Function count_NIMAX() As String
        Try
            If Working_Pro.rsWindow Then
                ' 🧹 เคลียร์ Task เดิมก่อน หากมีอยู่แล้ว
                If digitalReadTask_new_dio IsNot Nothing Then
                    Try
                        digitalReadTask_new_dio.Dispose()
                    Catch ex As Exception
                        Console.WriteLine("[Dispose Task Error] " & ex.Message)
                    End Try
                End If

                ' 🔁 สร้าง Task ใหม่
                digitalReadTask_new_dio = New Task()

                ' 🎯 เพิ่ม Channel ใหม่
                Try
                    digitalReadTask_new_dio.DIChannels.CreateChannel(
                        "Dev1/port0", "port0", ChannelLineGrouping.OneChannelForAllLines)
                Catch ex As Exception
                    '  MsgBox("[CreateChannel] " & ex.Message)
                    Return "Fail to create channel"
                End Try

                ' 🧠 Reader
                Try
                    reader_new_dio = New DigitalSingleChannelReader(digitalReadTask_new_dio.Stream)
                Catch ex As Exception
                    'MsgBox("[Reader Init] " & ex.Message)
                    Return "Fail to create reader"
                End Try
                ' ✅ เริ่ม Timer
                Working_Pro.Timer_new_dio.Start()
                Return "OK"
            Else
                Return "Not Supported on this OS"
            End If
        Catch ex As Exception
            '  MsgBox("[count_NIMAX] " & ex.Message)
            Return "Please Check USB DIO"
        End Try
    End Function
End Class
