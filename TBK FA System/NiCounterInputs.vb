Imports System
Imports System.Collections.Generic

' Used only on the UI thread. Capture all edges before awaiting any database work.
Public Class NiCounterInputs
    Private ReadOnly channels As List(Of Integer)
    Private ReadOnly previousHigh(7) As Boolean
    Private ReadOnly lastEdge(7) As Long
    Private ReadOnly pending As New Queue(Of Integer)()
    Private currentContext As String = Nothing

    Public Sub New(config As String)
        channels = ParseChannels(config)
        For i As Integer = 0 To 7
            previousHigh(i) = True
            lastEdge(i) = Long.MinValue
        Next
    End Sub

    Public Shared Function ParseChannels(config As String) As List(Of Integer)
        If String.IsNullOrWhiteSpace(config) Then config = "0"
        Dim result As New List(Of Integer)()
        For Each token As String In config.Split(","c)
            Dim channel As Integer
            If Not Integer.TryParse(token.Trim(), channel) OrElse channel < 0 OrElse channel > 7 Then
                Throw New ArgumentException("Invalid dio_detail. Use channel numbers 0-7, for example: 0,1,2")
            End If
            If Not result.Contains(channel) Then result.Add(channel)
        Next
        Return result
    End Function

    Public Sub Capture(states As Boolean(), context As String, accepting As Boolean,
                       nowMs As Long, debounceMs As Integer)
        If states Is Nothing OrElse states.Length <> 8 Then Throw New ArgumentException("Expected eight NI input lines.")
        ' Establish the current electrical state on connect/reconnect. A line
        ' already held low is not a new pulse for the newly connected reader.
        If currentContext Is Nothing OrElse currentContext <> context Then
            pending.Clear()
            For i As Integer = 0 To 7
                previousHigh(i) = states(i)
                lastEdge(i) = Long.MinValue
            Next
        End If
        currentContext = context
        If Not accepting Then pending.Clear()
        For Each channel As Integer In channels
            Dim falling As Boolean = previousHigh(channel) AndAlso Not states(channel)
            previousHigh(channel) = states(channel)
            If accepting AndAlso falling AndAlso
               (lastEdge(channel) = Long.MinValue OrElse nowMs - lastEdge(channel) >= debounceMs) Then
                If pending.Count >= 10000 Then Throw New InvalidOperationException("NI counter queue is full. Stop and reconcile production quantity.")
                pending.Enqueue(channel)
                lastEdge(channel) = nowMs
            End If
        Next
    End Sub

    Public Function TryTake(context As String, ByRef channel As Integer) As Boolean
        If currentContext <> context Then
            pending.Clear()
            Return False
        End If
        If pending.Count = 0 Then Return False
        channel = pending.Dequeue()
        Return True
    End Function

    Public Sub Clear()
        pending.Clear()
    End Sub

    Public ReadOnly Property PendingCount As Integer
        Get
            Return pending.Count
        End Get
    End Property
End Class
