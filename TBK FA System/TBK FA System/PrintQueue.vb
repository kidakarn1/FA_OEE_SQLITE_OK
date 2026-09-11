' ======================= PrintQueue.vb =======================
Imports System.Collections.Concurrent
Imports System.Drawing.Printing
Imports System.Threading
Imports System.Windows.Forms

Module PrintQueue
    Private ReadOnly _queue As New ConcurrentQueue(Of Func(Of Boolean))
    Private ReadOnly _gate As New SemaphoreSlim(1, 1)
    Private _processing As Integer = 0

    ' งานที่ต้องรอ EndPrint (owner = ฟอร์ม/คอนโทรลบน UI ของเอกสารนั้น)
    Public Sub Enqueue(owner As Control, doc As PrintDocument, assignPayloadAndPrint As Action)
        If owner Is Nothing OrElse doc Is Nothing OrElse assignPayloadAndPrint Is Nothing Then Exit Sub
        _queue.Enqueue(Function() RunAndWaitEndPrint(owner, doc, assignPayloadAndPrint))
        StartWorkerIfNeeded()
    End Sub

    ' งานทั่วไปที่ไม่แตะ UI โดยตรง
    Public Sub Enqueue(work As Action)
        If work Is Nothing Then Exit Sub
        _queue.Enqueue(Function()
                           work.Invoke()
                           Return True
                       End Function)
        StartWorkerIfNeeded()
    End Sub

    Private Async Sub StartWorkerIfNeeded()
        If Interlocked.CompareExchange(_processing, 1, 0) <> 0 Then Exit Sub
        Try
            Await _gate.WaitAsync().ConfigureAwait(False)
            Try
                Dim job As Func(Of Boolean)
                While _queue.TryDequeue(job)
                    Try
                        job.Invoke()
                    Catch ex As Exception
                        ' หลีกเลี่ยง MessageBox ข้ามเธรด (ให้ไปขึ้นบน UI เจ้าของงานทีหลัง)
                        Debug.WriteLine("PrintQueue worker error: " & ex.Message)
                    End Try
                End While
            Finally
                _gate.Release()
            End Try
        Finally
            Interlocked.Exchange(_processing, 0)
            If Not _queue.IsEmpty Then StartWorkerIfNeeded()
        End Try
    End Sub

    Private Function UiAlive(ctrl As Control) As Boolean
        Return (ctrl IsNot Nothing) AndAlso ctrl.IsHandleCreated AndAlso Not ctrl.IsDisposed
    End Function

    ' โพสต์ไป UI ของ owner เสมอ → ต่อ EndPrint + เรียก Print() บน UI, แล้วรอ EndPrint บน worker
    Private Function RunAndWaitEndPrint(owner As Control, doc As PrintDocument, doAssignAndPrint As Action) As Boolean
        Dim tcs As New Threading.Tasks.TaskCompletionSource(Of Boolean)(
            Threading.Tasks.TaskCreationOptions.RunContinuationsAsynchronously)

        ' ตรวจสภาพก่อนโพสต์
        If Not UiAlive(owner) Then
            Debug.WriteLine("RunAndWaitEndPrint: owner not alive -> cancel")
            Return False
        End If

        owner.BeginInvoke(Sub()
                              Try
                                  If Not UiAlive(owner) Then
                                      tcs.TrySetCanceled()
                                      Return
                                  End If

                                  Dim h As PrintEventHandler = Nothing
                                  h = Sub(sender, e)
                                          Try
                                              tcs.TrySetResult(True)
                                          Finally
                                              RemoveHandler doc.EndPrint, h
                                          End Try
                                      End Sub
                                  AddHandler doc.EndPrint, h

                                  ' ปิด popup
                                  doc.PrintController = New StandardPrintController()

                                  ' เรียกงานที่ภายในต้องมี doc.Print()
                                  doAssignAndPrint.Invoke()

                              Catch ex As Exception
                                  tcs.TrySetException(ex)
                              End Try
                          End Sub)

        Try
            tcs.Task.Wait() ' รอ EndPrint บน worker (ไม่บล็อก UI)
            Return True
        Catch ex As Exception
            Debug.WriteLine("RunAndWaitEndPrint: wait failed -> " & ex.Message)
            Return False
        End Try
    End Function
End Module
