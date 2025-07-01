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
    Public Function Load_data(ByVal _URL As String) As String
        Dim _tmpImage As Image = Nothing
        Dim re_data = "NO_DATA"
        Try
            Dim _HttpWebRequest As System.Net.HttpWebRequest = CType(System.Net.HttpWebRequest.Create(_URL), System.Net.HttpWebRequest)

            _HttpWebRequest.AllowWriteStreamBuffering = True
            _HttpWebRequest.Timeout = 20000

            Dim _WebResponse As System.Net.WebResponse = _HttpWebRequest.GetResponse()
            Using data As New StreamReader(_WebResponse.GetResponseStream())
                re_data = data.ReadToEnd
                Dim JSONString As String = ""
                JSONString = JsonConvert.SerializeObject(re_data)
                'MsgBox("re_data" & re_data)
                'MsgBox(data.ReadToEnd)
                '  For Each key In data.ReadToEnd
                're_data &= key
                ' Next 'return to json '
            End Using

            _WebResponse.Close()
        Catch _Exception As Exception
            'MsgBox("FALL WOW")
            Return Nothing
        End Try
        Return re_data
    End Function
    Private Shared ReadOnly sqliteLock As New Object()
    Public Sub InitSQLiteWAL()
        Try
            Dim connStr As String = Backoffice_model.sqliteConnect & ";Default Timeout=5;"
            Using connection As New SQLiteConnection(connStr)
                connection.Open()
                Using cmd As New SQLiteCommand("PRAGMA journal_mode=WAL;", connection)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Failed to enable WAL mode: " & ex.Message)
        End Try
    End Sub
    Public Async Function Load_dataSQLiteAsyncLoaddata(ByVal Sql As String) As Task(Of String)
        Return Await Task.Run(Function()
                                  Return Load_dataSQLite(Sql)
                              End Function).ConfigureAwait(False)
    End Function
    Public Async Function Load_dataSQLiteAsync(ByVal sql As String) As Task(Of String)
        Try
            Using conn As New SQLiteConnection(Backoffice_model.sqliteConnect)
                Await conn.OpenAsync()

                Using cmd As New SQLiteCommand(sql, conn)
                    Dim affected As Integer = Await cmd.ExecuteNonQueryAsync()
                    Console.WriteLine($"✅ SQLite Executed: {sql} => {affected} row(s) affected")
                    Return affected.ToString()
                End Using
            End Using

        Catch ex As Exception
            Dim functionName = New StackTrace().GetFrame(0).GetMethod().Name
            Console.WriteLine($"❌ Error in {functionName}: {ex.Message}")
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
                Console.WriteLine("Error in Load_dataSQLite: " & ex.Message)
                Throw
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

            ' Request response:
            Dim _WebResponse As System.Net.WebResponse = _HttpWebRequest.GetResponse()

            ' Open data stream:
            Dim _WebStream As System.IO.Stream = _WebResponse.GetResponseStream()

            ' convert webstream to image
            _tmpImage = New System.Drawing.Bitmap(_WebStream)


            ' Cleanup
            _WebResponse.Close()
            _WebResponse.Close()
        Catch _Exception As Exception
            ' Error
            'Console.WriteLine("Exception caught in process: {0}", _Exception.ToString())
            Return Nothing
        End Try

        Return _tmpImage
    End Function
    Public Function Load_dataPOST(ByVal url As String, ByVal postData As JObject) As String
        Dim rs As String = ""

        ' สร้าง HttpWebRequest
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)

        ' กำหนดเมธอดและตัวแปรที่จำเป็นต่อการส่งคำขอ
        request.Method = "POST"
        request.ContentType = "application/json"

        ' แปลงข้อมูล POST เป็นไบต์และกำหนดขนาดความยาวของข้อมูล
        Dim postDataBytes As Byte() = Encoding.UTF8.GetBytes(postData.ToString())
        request.ContentLength = postDataBytes.Length
        ' เขียนข้อมูล POST ลงใน Request Stream
        Using requestStream As Stream = request.GetRequestStream()
            requestStream.Write(postDataBytes, 0, postDataBytes.Length)
        End Using
        ' ส่งคำขอและรับตอบกลับ
        Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            ' อ่านข้อมูลจากเนื้อหาของการตอบกลับ
            Using streamReader As New StreamReader(response.GetResponseStream())
                Dim responseData As String = streamReader.ReadToEnd()
                rs = responseData
            End Using
        End Using
        Return rs
    End Function
End Class
