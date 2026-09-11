Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            ' Keep the machine's language/number settings, but always use Gregorian years.
            ' This prevents yyyy from becoming 2569 and prevents later parsing from
            ' shifting the same timestamp to 1483 or 3112 on Thai Windows.
            Dim appCulture As Global.System.Globalization.CultureInfo = CType(Global.System.Globalization.CultureInfo.CurrentCulture.Clone(), Global.System.Globalization.CultureInfo)
            appCulture.DateTimeFormat.Calendar = New Global.System.Globalization.GregorianCalendar()

            Global.System.Globalization.CultureInfo.DefaultThreadCurrentCulture = appCulture
            Global.System.Threading.Thread.CurrentThread.CurrentCulture = appCulture
        End Sub
    End Class
End Namespace
