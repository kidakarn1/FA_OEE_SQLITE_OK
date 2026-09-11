Imports System
Imports System.Net
Imports System.IO
Imports System.Windows.Forms.Form
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Web.Script.Serialization
Imports System.Text
Imports System.Data.SQLite

Public Class api
    Public Function Load_data(ByVal _URL As String, Optional timeoutMilliseconds As Integer = 20000) As String
        Dim re_data = "NO_DATA"
        Try
            Dim _HttpWebRequest As System.Net.HttpWebRequest = CType(System.Net.HttpWebRequest.Create(_URL), System.Net.HttpWebRequest)

            _HttpWebRequest.AllowWriteStreamBuffering = True
            _HttpWebRequest.Timeout = timeoutMilliseconds
            _HttpWebRequest.ReadWriteTimeout = timeoutMilliseconds

            Using _WebResponse As System.Net.WebResponse = _HttpWebRequest.GetResponse()
                Using responseStream As Stream = _WebResponse.GetResponseStream()
                    Using data As New StreamReader(responseStream)
                        re_data = data.ReadToEnd()
                    End Using
                End Using
            End Using
        Catch _Exception As Exception
            ''msgBox("FALL WOW")
            Return Nothing
        End Try
        Return re_data
    End Function

    ' OEE calls are polled frequently; fail fast when the OEE service is unavailable
    ' instead of blocking the production UI for the general API timeout.
    Public Function Load_dataOEE(ByVal _URL As String) As String
        Return Load_data(_URL, 5000)
    End Function

    Public Async Function Load_dataAsync(ByVal _URL As String, Optional timeoutMilliseconds As Integer = 20000) As Task(Of String)
        Dim request As HttpWebRequest = Nothing
        Try
            request = CType(WebRequest.Create(_URL), HttpWebRequest)
            request.AllowWriteStreamBuffering = True
            request.Timeout = timeoutMilliseconds
            request.ReadWriteTimeout = timeoutMilliseconds

            Using timeoutCts As New Threading.CancellationTokenSource()
                Dim responseTask As Task(Of WebResponse) = request.GetResponseAsync()
                Dim timeoutTask As Task = Task.Delay(timeoutMilliseconds, timeoutCts.Token)
                Dim completedTask As Task = Await Task.WhenAny(responseTask, timeoutTask).ConfigureAwait(False)
                If completedTask IsNot responseTask Then
                    request.Abort()
                    Try
                        Await responseTask.ConfigureAwait(False)
                    Catch
                    End Try
                    Return Nothing
                End If

                timeoutCts.Cancel()
                Using response As WebResponse = Await responseTask.ConfigureAwait(False)
                    Using responseStream As Stream = response.GetResponseStream()
                        Using data As New StreamReader(responseStream)
                            Return Await data.ReadToEndAsync().ConfigureAwait(False)
                        End Using
                    End Using
                End Using
            End Using
        Catch ex As Exception
            If request IsNot Nothing Then
                Try
                    request.Abort()
                Catch
                End Try
            End If
            Return Nothing
        End Try
    End Function

    Public Function Load_dataOEEAsync(ByVal _URL As String) As Task(Of String)
        Return Load_dataAsync(_URL, 5000)
    End Function

    Private Shared ReadOnly sqliteLock As New Object()
    Private Shared ReadOnly sqliteInitLock As New Object()
    Private Shared sqliteWalConnectionString As String = Nothing

    Public Sub InitSQLiteWAL()
        Dim connStr As String = Backoffice_model.sqliteConnect & ";Default Timeout=5;"

        SyncLock sqliteInitLock
            If String.Equals(sqliteWalConnectionString, connStr, StringComparison.Ordinal) Then Return

            Try
                Using connection As New SQLiteConnection(connStr)
                    connection.Open()
                    Using cmd As New SQLiteCommand("PRAGMA journal_mode=WAL;", connection)
                        cmd.CommandTimeout = 5
                        Dim journalMode As String = Convert.ToString(cmd.ExecuteScalar())
                        If String.Equals(journalMode, "wal", StringComparison.OrdinalIgnoreCase) Then
                            sqliteWalConnectionString = connStr
                        End If
                    End Using
                End Using
            Catch ex As Exception
                ' Leave the marker unset so a later call can retry after a transient lock.
                'Console.WriteLine("Failed to enable WAL mode: " & ex.Message)
            End Try
        End SyncLock
    End Sub
    Public Async Function Load_dataSQLiteAsyncLoaddata(ByVal Sql As String) As Task(Of String)
        Return Await Task.Run(Function()
                                  Return Load_dataSQLite(Sql)
                              End Function).ConfigureAwait(False)
    End Function
    Public Async Function Load_dataSQLiteAsync(ByVal sql As String) As Task(Of String)
        Try
            Using conn As New SQLiteConnection(Backoffice_model.sqliteConnect & ";Default Timeout=5;")
                Await conn.OpenAsync()

                Using cmd As New SQLiteCommand(sql, conn)
                    cmd.CommandTimeout = 5
                    Dim affected As Integer = Await cmd.ExecuteNonQueryAsync()
                    'Console.WriteLine($"✅ SQLite Executed: {sql} => {affected} row(s) affected")
                    Return affected.ToString()
                End Using
            End Using

        Catch ex As Exception
            Dim functionName = New StackTrace().GetFrame(0).GetMethod().Name
            'Console.WriteLine($"❌ Error in {functionName}: {ex.Message}")
            Return "0"
        End Try
    End Function
    Public Function Load_dataSQLite(ByVal Sql As String) As String
        SyncLock sqliteLock
            Try
                Dim connStr As String = Backoffice_model.sqliteConnect & ";Default Timeout=5;"
                Using connection As New SQLiteConnection(connStr)
                    connection.Open()
                    ' ตัด PRAGMA ออก เพราะมันถูกตั้งแล้วตอนเริ่มต้น
                    Using cmd As New SQLiteCommand(Sql, connection)
                        cmd.CommandTimeout = 5
                        Using reader As SQLiteDataReader = cmd.ExecuteReader()
                            Dim dataTable As New DataTable()
                            dataTable.Load(reader)
                            If dataTable.Rows.Count = 0 Then
                                Return "0"
                            Else
                                Return JsonConvert.SerializeObject(dataTable)
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                ' Preserve the existing error signal without exposing complete SQL
                ' text (which may contain production values) in Release output.
#If DEBUG Then
                Console.WriteLine("Error in Load_dataSQLite: " & ex.Message & "===>" & Sql)
#Else
                Console.WriteLine("Error in Load_dataSQLite: " & ex.Message)
#End If
                ' Throw
            End Try
        End SyncLock
    End Function

    Public Function DownloadImage(ByVal _URL As String) As Image
        Dim _tmpImage As Image = Nothing

        Try
            ' Open a connection
            Dim _HttpWebRequest As System.Net.HttpWebRequest = CType(System.Net.HttpWebRequest.Create(_URL), System.Net.HttpWebRequest)

            _HttpWebRequest.AllowWriteStreamBuffering = True

            ' You can also specify additional header values like the user agent or the referer: (Optional)
            _HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)"
            _HttpWebRequest.Referer = "http://www.google.com/"

            ' set timeout for 20 seconds (Optional)
            _HttpWebRequest.Timeout = 20000
            _HttpWebRequest.ReadWriteTimeout = 20000

            Using _WebResponse As System.Net.WebResponse = _HttpWebRequest.GetResponse()
                Using _WebStream As System.IO.Stream = _WebResponse.GetResponseStream()
                    ' Clone the bitmap so the response stream can be released immediately.
                    Using loadedImage As New System.Drawing.Bitmap(_WebStream)
                        Return New System.Drawing.Bitmap(loadedImage)
                    End Using
                End Using
            End Using
        Catch _Exception As Exception
            ' Error
            ''Console.WriteLine("Exception caught in process: {0}", _Exception.ToString())
            Return Nothing
        End Try
    End Function
    Public Function Load_dataPOST(ByVal url As String, ByVal postData As JObject, Optional returnHttpErrorBody As Boolean = False,
                                 Optional ByRef httpStatusCode As Integer = 0) As String
        Dim rs As String = ""
        httpStatusCode = 0

        ' สร้าง HttpWebRequest
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)

        ' กำหนดเมธอดและตัวแปรที่จำเป็นต่อการส่งคำขอ
        request.Method = "POST"
        request.ContentType = "application/json"
        request.Timeout = 20000
        request.ReadWriteTimeout = 20000

        ' แปลงข้อมูล POST เป็นไบต์และกำหนดขนาดความยาวของข้อมูล
        Dim postDataBytes As Byte() = Encoding.UTF8.GetBytes(postData.ToString())
        request.ContentLength = postDataBytes.Length
        ' เขียนข้อมูล POST ลงใน Request Stream
        Try
            Using requestStream As Stream = request.GetRequestStream()
                requestStream.Write(postDataBytes, 0, postDataBytes.Length)
            End Using
            ' ส่งคำขอและรับตอบกลับ
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                httpStatusCode = CInt(response.StatusCode)
                ' อ่านข้อมูลจากเนื้อหาของการตอบกลับ
                Using streamReader As New StreamReader(response.GetResponseStream())
                    Dim responseData As String = streamReader.ReadToEnd()
                    rs = responseData
                End Using
            End Using
        Catch ex As WebException
            If returnHttpErrorBody AndAlso ex.Response IsNot Nothing Then
                Using errorResponse As HttpWebResponse = TryCast(ex.Response, HttpWebResponse)
                    If errorResponse IsNot Nothing Then
                        httpStatusCode = CInt(errorResponse.StatusCode)
                        Using streamReader As New StreamReader(errorResponse.GetResponseStream())
                            Return streamReader.ReadToEnd()
                        End Using
                    End If
                End Using
            End If
            Throw
        End Try
        Return rs
    End Function
End Class
