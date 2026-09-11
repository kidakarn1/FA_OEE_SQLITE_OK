Imports System.Threading
Imports System.IO
Imports System.Windows.Forms

Public Module AppGuard
    Private _mx As Mutex
    Private Const MUTEX_NAME As String = "Global\TBK_FA_System_20250918" ' ชื่อไม่ซ้ำ

    Public Sub Init()
        ' --- กันเปิดซ้อน ---
        Dim created As Boolean = False
        _mx = New Mutex(True, MUTEX_NAME, created)
        If Not created Then
            ' มีโปรแกรมรันอยู่แล้ว → ปิดตัวที่สอง
            Environment.Exit(0)
        End If

        ' --- กันเด้งระดับแอป ---
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
        AddHandler Application.ThreadException, AddressOf OnUiThreadException
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf OnDomainUnhandled
        AddHandler TaskScheduler.UnobservedTaskException, AddressOf OnUnobservedTask
    End Sub

    Public Sub Release()
        Try
            _mx?.ReleaseMutex()
            _mx?.Dispose()
        Catch
        End Try
    End Sub

    ' ===== Handlers (ย่อ) =====
    Private Sub OnUiThreadException(sender As Object, e As Threading.ThreadExceptionEventArgs)
        Log("[UI] " & e.Exception.ToString())
        ' ไม่ปิดแอป
    End Sub

    Private Sub OnDomainUnhandled(sender As Object, e As UnhandledExceptionEventArgs)
        Log("[FATAL] " & e.ExceptionObject.ToString())
        ' อาจถูกปิดโดย CLR ถ้ารุนแรงมาก
    End Sub

    Private Sub OnUnobservedTask(sender As Object, e As UnobservedTaskExceptionEventArgs)
        Log("[TASK] " & e.Exception.ToString())
        e.SetObserved()
    End Sub

    Private Sub Log(s As String)
        Try
            Dim dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TBK FA System", "logs")
            Directory.CreateDirectory(dir)
            Dim f = Path.Combine(dir, DateTime.Now.ToString("yyyyMMdd") & ".log")
            File.AppendAllText(f, $"{DateTime.Now:HH:mm:ss} {s}{Environment.NewLine}")
        Catch
        End Try
    End Sub
End Module
