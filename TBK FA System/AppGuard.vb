Imports System.Threading
Imports System.IO
Imports System.Windows.Forms

Public Module AppGuard
    Private _mx As Mutex
    ' ใช้ชื่อเดียวกับโปรแกรม TBK FA System แต่แปลง space → _
    Private Const MUTEX_NAME As String = "Global\TBK_FA_System"
    Public Const SILENT As Boolean = True   ' True = ไม่โชว์ MsgBox, log อย่างเดียว

    Public Sub Init()
        ' ===== กันเปิดซ้อน =====
        Dim created As Boolean = False
        _mx = New Mutex(True, MUTEX_NAME, created)
        If Not created Then
            ' มี instance รันอยู่แล้ว → ปิดตัวใหม่นี้
            Environment.Exit(0)
        End If

        ' ===== กันเด้ง (Global Exception Handlers) =====
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

    ' ===== Handlers =====
    Private Sub OnUiThreadException(sender As Object, e As Threading.ThreadExceptionEventArgs)
        Log("[UI] " & e.Exception.ToString())
        If Not SILENT Then
            MessageBox.Show("เกิดข้อผิดพลาดในหน้าจอ: " & e.Exception.Message, "TBK FA System", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub OnDomainUnhandled(sender As Object, e As UnhandledExceptionEventArgs)
        Log("[FATAL] " & e.ExceptionObject.ToString())
        ' CLR อาจปิดแอปถ้า exception รุนแรงมาก
    End Sub

    Private Sub OnUnobservedTask(sender As Object, e As UnobservedTaskExceptionEventArgs)
        Log("[TASK] " & e.Exception.ToString())
        e.SetObserved()
    End Sub

    ' ===== Logging =====
    Private Sub Log(msg As String)
        Try
            Dim dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TBK FA System", "logs")
            Directory.CreateDirectory(dir)
            Dim f = Path.Combine(dir, DateTime.Now.ToString("yyyyMMdd") & ".log")
            File.AppendAllText(f, $"{DateTime.Now:HH:mm:ss} {msg}{Environment.NewLine}")
        Catch
        End Try
    End Sub
End Module
