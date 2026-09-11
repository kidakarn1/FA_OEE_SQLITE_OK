Imports System
Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports System.Data.SQLite
Imports System.Diagnostics
Imports System.Threading
Imports System.Net
Imports System.IO.Compression

Module UpdateSqlite
    '===================== ปรับค่าตามระบบจริง =====================
    ' แหล่ง Master: ชี้ไปยัง ZIP ผ่าน HTTP
    Private ReadOnly MASTER_SOURCE As String = "http://192.168.161.77/Version_New_FA/MasterSqlite/FA_local_db.zip"
    ' เส้นทาง DB ที่เครื่องใช้งานจริง
    Private ReadOnly PATH_USE As String = "C:\sqlite3\FA_local_db.db3"
    '==============================================================
    Sub Main(args As String())
        Try
            If Not File.Exists(PATH_USE) Then
                Console.WriteLine("❌ ไม่พบไฟล์ปลายทาง: " & PATH_USE)
                Return
            End If

            ' 1) สำรอง DB ปลายทาง
            BackupDb(PATH_USE)

            Dim localMasterCopy As String = Nothing
            Try
                ' 2) ดาวน์โหลด + แตก ZIP → ได้ไฟล์ .db3 ชั่วคราว
                Console.WriteLine("กำลังดาวน์โหลด Master ZIP: " & MASTER_SOURCE)
                localMasterCopy = FetchMasterFromHttpZip(MASTER_SOURCE)
                Console.WriteLine("📥 Master SQLite (local temp): " & localMasterCopy)

                ' 3) ATTACH + Deep Schema Sync
                Using useConn As New SQLiteConnection("Data Source=" & PATH_USE & ";Version=3;")
                    useConn.Open()
                    ExecNonQuery(useConn, "PRAGMA journal_mode=WAL;")
                    ExecNonQuery(useConn, "PRAGMA foreign_keys=ON;")

                    AttachMaster(useConn, localMasterCopy)
                    PrintDatabaseList(useConn)

                    Dim mtcnt As Integer = CountMasterTables(useConn)
                    Console.WriteLine("📋 master มีตารางทั้งหมด: " & mtcnt.ToString())
                    If mtcnt = 0 Then Throw New Exception("ไม่พบตารางใน master (ไฟล์ ZIP/DB อาจเสีย)")

                    SyncSchemaDeep(useConn)

                    ExecNonQuery(useConn, "DETACH DATABASE master;")
                    useConn.Close()
                End Using

                Console.WriteLine("✅ เสร็จสิ้น Schema Sync (PATH_USE ← HTTP ZIP)")

            Finally
                ' ลบไฟล์ชั่วคราว
                If Not String.IsNullOrEmpty(localMasterCopy) Then
                    Try : File.Delete(localMasterCopy) : Catch : End Try
                End If
            End Try

        Catch ex As Exception
            Console.WriteLine("❌ ERROR: " & ex.Message)
        End Try
    End Sub

    '=========================== HTTP + ZIP ===========================
    Private Function FetchMasterFromHttpZip(url As String) As String
        ' ดาวน์โหลด ZIP ลง temp
        Dim tempDir As String = Path.Combine(Path.GetTempPath(), "FA_MasterCache")
        If Not Directory.Exists(tempDir) Then Directory.CreateDirectory(tempDir)

        Dim zipPath As String = Path.Combine(tempDir, "master.zip")
        Dim dbPath As String = Nothing

        ' ดาวน์โหลด (รองรับ intranet ธรรมดา ไม่มี auth)
        Dim wc As New WebClient()
        wc.DownloadFile(url, zipPath)

        ' ตรวจขนาด
        Dim fz As New FileInfo(zipPath)
        If Not fz.Exists OrElse fz.Length = 0 Then
            Throw New Exception("ไฟล์ ZIP ที่ดาวน์โหลดว่างหรือหาย: " & zipPath)
        End If

        ' แตก ZIP ไปโฟลเดอร์ย่อย
        Dim extractDir As String = Path.Combine(tempDir, "unzipped")
        If Directory.Exists(extractDir) Then
            Try : Directory.Delete(extractDir, True) : Catch : End Try
        End If
        ZipFile.ExtractToDirectory(zipPath, extractDir)

        ' หาไฟล์ .db3 (เลือกชื่อ FA_local_db.db3 ก่อน ถ้าไม่เจอค่อยเอาไฟล์ .db3 แรก)
        Dim preferred As String = Path.Combine(extractDir, "FA_local_db.db3")
        If File.Exists(preferred) Then
            dbPath = preferred
        Else
            Dim found As String() = Directory.GetFiles(extractDir, "*.db3", SearchOption.AllDirectories)
            If found Is Nothing OrElse found.Length = 0 Then
                ' เผื่อกรณีใช้ .db หรือ .sqlite
                found = Directory.GetFiles(extractDir, "*.db", SearchOption.AllDirectories)
            End If
            If found Is Nothing OrElse found.Length = 0 Then
                found = Directory.GetFiles(extractDir, "*.sqlite", SearchOption.AllDirectories)
            End If
            If found Is Nothing OrElse found.Length = 0 Then
                Throw New Exception("ไม่พบไฟล์ฐานข้อมูลใน ZIP")
            End If
            dbPath = found(0)
        End If

        ' สร้างสำเนาชื่อเดียวกันทุกครั้ง (เพื่อทาง Attach ง่าย)
        Dim localCopy As String = Path.Combine(tempDir, "FA_master_copy.db3")
        If File.Exists(localCopy) Then
            Try : File.Delete(localCopy) : Catch : End Try
        End If
        File.Copy(dbPath, localCopy, False)
        Return localCopy
    End Function

    '=========================== Utilities ===========================
    Private Sub BackupDb(dbPath As String)
        Dim dir As String = Path.GetDirectoryName(dbPath)
        Dim name As String = Path.GetFileNameWithoutExtension(dbPath)
        Dim ts As String = DateTime.Now.ToString("yyyyMMdd_HHmmss")
        Dim bak As String = Path.Combine(dir, name & "_" & ts & ".bak.db3")
        File.Copy(dbPath, bak, False)
        Console.WriteLine("🗂️ Backup: " & bak)
    End Sub

    Private Sub ExecNonQuery(conn As SQLiteConnection, sql As String)
        Using cmd As New SQLiteCommand(sql, conn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub SafeExec(conn As SQLiteConnection, sql As String)
        Try
            ExecNonQuery(conn, sql)
        Catch ex As Exception
            Console.WriteLine("❌ SQL FAIL: " & ex.Message & vbCrLf & "SQL> " & sql)
        End Try
    End Sub

    Private Sub AttachMaster(useConn As SQLiteConnection, masterPath As String)
        Dim safePath As String = masterPath.Replace("'", "''")
        ExecNonQuery(useConn, "ATTACH DATABASE '" & safePath & "' AS master;")
        Console.WriteLine("🔗 ATTACHED master -> " & masterPath)
    End Sub

    Private Sub PrintDatabaseList(conn As SQLiteConnection)
        Using cmd As New SQLiteCommand("PRAGMA database_list;", conn)
            Using rd = cmd.ExecuteReader()
                Console.WriteLine("== PRAGMA database_list ==")
                While rd.Read()
                    Console.WriteLine("seq=" & rd(0).ToString() & ", name=" & rd(1).ToString() & ", file=" & rd(2).ToString())
                End While
            End Using
        End Using
    End Sub

    Private Function CountMasterTables(conn As SQLiteConnection) As Integer
        Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM master.sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';", conn)
            Return Convert.ToInt32(cmd.ExecuteScalar())
        End Using
    End Function

    '===================== Deep Schema Sync (จาก master → use) =====================
    Private Sub SyncSchemaDeep(useConn As SQLiteConnection)
        Console.WriteLine("🛠️ เริ่ม Sync Schema (Deep) ...")

        Dim masterTables As HashSet(Of String) = GetTables(useConn, "master")
        Dim useTables As HashSet(Of String) = GetTables(useConn, "main")

        ' (0) ลบตารางที่เกินใน USE
        Dim t As String
        For Each t In useTables
            If Not masterTables.Contains(t) Then
                Console.WriteLine("🗑️ DROP TABLE (extra in USE): " & t)
                SafeExec(useConn, "DROP TABLE IF EXISTS [" & t & "];")
            End If
        Next

        ' (1) สร้างตารางที่ขาดใน USE
        For Each t In masterTables
            If Not useTables.Contains(t) Then
                Dim createSql As String = GetCreateSql(useConn, "master", t)
                If Not String.IsNullOrEmpty(createSql) Then
                    createSql = EnsureCreateTableHasIfNotExists(createSql)
                    Console.WriteLine("➕ CREATE TABLE (missing in USE): " & t)
                    SafeExec(useConn, createSql)
                End If
            End If
        Next

        ' (2) ตารางร่วมกัน → หาก schema ต่าง ให้รีบิลด์
        For Each t In masterTables
            If Not useTables.Contains(t) Then Continue For

            If Not TableStructEquals(useConn, t) Then
                Console.WriteLine("🧱 REBUILD TABLE (schema mismatch): " & t)
                RebuildTableToMatchMaster(useConn, t)
            End If

            ' Sync Index/Trigger ให้ตรง master
            SyncIndexesAndTriggers(useConn, t)
        Next

        Console.WriteLine("✅ จบ Sync Schema (Deep)")
    End Sub

    ' ===== โครงสร้างตาราง =====
    Private Class TableCol
        Public name As String
        Public coltype As String
        Public notnull As Integer
        Public dflt As String
        Public pk As Integer

        Public Function EqualsTo(other As TableCol) As Boolean
            If other Is Nothing Then Return False
            If String.Compare(Me.name, other.name, True) <> 0 Then Return False
            If UCase(Trim(Me.coltype)) <> UCase(Trim(other.coltype)) Then Return False
            If Me.notnull <> other.notnull Then Return False
            Dim a As String = If(Me.dflt, "")
            Dim b As String = If(other.dflt, "")
            If a <> b Then Return False
            If Me.pk <> other.pk Then Return False
            Return True
        End Function
    End Class

    Private Function TableStructEquals(conn As SQLiteConnection, table As String) As Boolean
        Dim mCols As List(Of TableCol) = ReadTableCols(conn, "master", table)
        Dim uCols As List(Of TableCol) = ReadTableCols(conn, "main", table)

        If mCols.Count <> uCols.Count Then Return False

        Dim i As Integer
        For i = 0 To mCols.Count - 1
            If Not mCols(i).EqualsTo(uCols(i)) Then Return False
        Next
        Return True
    End Function

    Private Function ReadTableCols(conn As SQLiteConnection, db As String, table As String) As List(Of TableCol)
        Dim list As New List(Of TableCol)
        Using cmd As New SQLiteCommand("PRAGMA " & db & ".table_info([" & table & "]);", conn)
            Using rd = cmd.ExecuteReader()
                While rd.Read()
                    Dim c As New TableCol()
                    c.name = Convert.ToString(rd("name"))
                    c.coltype = Convert.ToString(rd("type"))
                    c.notnull = Convert.ToInt32(If(rd("notnull"), 0))
                    c.dflt = If(rd("dflt_value") Is DBNull.Value, Nothing, Convert.ToString(rd("dflt_value")))
                    c.pk = Convert.ToInt32(If(rd("pk"), 0))
                    list.Add(c)
                End While
            End Using
        End Using
        Return list
    End Function

    Private Function EnsureCreateTableHasIfNotExists(sql As String) As String
        Dim s As String = sql.Trim()
        Dim up As String = s.ToUpperInvariant()
        If up.StartsWith("CREATE TABLE ") AndAlso up.IndexOf("IF NOT EXISTS") = -1 Then
            Return s.Replace("CREATE TABLE", "CREATE TABLE IF NOT EXISTS")
        End If
        If up.StartsWith("CREATE VIRTUAL TABLE ") AndAlso up.IndexOf("IF NOT EXISTS") = -1 Then
            Return s.Replace("CREATE VIRTUAL TABLE", "CREATE VIRTUAL TABLE IF NOT EXISTS")
        End If
        Return s
    End Function

    Private Function IntersectColumns(conn As SQLiteConnection, table As String) As List(Of String)
        Dim m As List(Of TableCol) = ReadTableCols(conn, "master", table)
        Dim u As List(Of TableCol) = ReadTableCols(conn, "main", table)

        Dim list As New List(Of String)
        Dim i As Integer, j As Integer
        For i = 0 To m.Count - 1
            Dim mn As String = m(i).name
            For j = 0 To u.Count - 1
                If String.Compare(mn, u(j).name, True) = 0 Then
                    list.Add(mn)
                    Exit For
                End If
            Next
        Next
        Return list
    End Function

    Private Sub RebuildTableToMatchMaster(conn As SQLiteConnection, table As String)
        Dim masterCreate As String = GetCreateSql(conn, "master", table)
        If String.IsNullOrEmpty(masterCreate) Then
            Console.WriteLine("⚠️ ไม่มี CREATE SQL สำหรับตาราง: " & table)
            Return
        End If

        Dim tmp As String = "__tmp_sync_" & table
        Dim commonCols As List(Of String) = IntersectColumns(conn, table)
        Dim colList As String = JoinBracket(commonCols)

        Using tx = conn.BeginTransaction()
            Try
                ExecNonQuery(conn, "PRAGMA foreign_keys=OFF;")
                SafeExec(conn, "DROP TABLE IF EXISTS [" & tmp & "];")

                Dim tmpCreate As String = masterCreate.Replace("CREATE TABLE [" & table & "]", "CREATE TABLE [" & tmp & "]") _
                                                      .Replace("CREATE TABLE " & table, "CREATE TABLE " & tmp)
                SafeExec(conn, tmpCreate)

                If commonCols.Count > 0 Then
                    Dim sqlIns As String = "INSERT INTO [" & tmp & "] (" & colList & ") SELECT " & colList & " FROM [" & table & "];"
                    SafeExec(conn, sqlIns)
                End If

                SafeExec(conn, "DROP TABLE IF EXISTS [" & table & "];")
                SafeExec(conn, "ALTER TABLE [" & tmp & "] RENAME TO [" & table & "];")

                DropIndexesAndTriggers(conn, table)
                SyncIndexesAndTriggers(conn, table)

                ExecNonQuery(conn, "PRAGMA foreign_keys=ON;")
                tx.Commit()
                Console.WriteLine("✅ REBUILD OK: " & table)

            Catch ex As Exception
                tx.Rollback()
                ExecNonQuery(conn, "PRAGMA foreign_keys=ON;")
                Console.WriteLine("❌ REBUILD FAIL (" & table & "): " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub DropIndexesAndTriggers(conn As SQLiteConnection, table As String)
        Using cmd As New SQLiteCommand("SELECT name, type FROM main.sqlite_master WHERE tbl_name=@t AND (type='index' OR type='trigger');", conn)
            cmd.Parameters.AddWithValue("@t", table)
            Using rd = cmd.ExecuteReader()
                While rd.Read()
                    Dim nm As String = Convert.ToString(rd("name"))
                    Dim tp As String = Convert.ToString(rd("type"))
                    If tp = "index" Then
                        SafeExec(conn, "DROP INDEX IF EXISTS [" & nm & "];")
                    ElseIf tp = "trigger" Then
                        SafeExec(conn, "DROP TRIGGER IF EXISTS [" & nm & "];")
                    End If
                End While
            End Using
        End Using
    End Sub

    Private Sub SyncIndexesAndTriggers(conn As SQLiteConnection, table As String)
        Using cmd As New SQLiteCommand("SELECT type, name, sql FROM master.sqlite_master WHERE tbl_name=@t AND sql IS NOT NULL AND (type='index' OR type='trigger');", conn)
            cmd.Parameters.AddWithValue("@t", table)
            Using rd = cmd.ExecuteReader()
                While rd.Read()
                    Dim sql As String = Convert.ToString(rd("sql"))
                    If String.IsNullOrEmpty(sql) Then Continue While
                    SafeExec(conn, sql)
                End While
            End Using
        End Using
    End Sub

    '=========================== Helpers =========================
    Private Function GetTables(conn As SQLiteConnection, db As String) As HashSet(Of String)
        Dim setNames As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Using cmd As New SQLiteCommand("SELECT name FROM " & db & ".sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';", conn)
            Using rd = cmd.ExecuteReader()
                While rd.Read()
                    setNames.Add(rd.GetString(0))
                End While
            End Using
        End Using
        Return setNames
    End Function

    Private Function GetCreateSql(conn As SQLiteConnection, db As String, table As String) As String
        Using cmd As New SQLiteCommand("SELECT sql FROM " & db & ".sqlite_master WHERE type='table' AND name=@t;", conn)
            cmd.Parameters.AddWithValue("@t", table)
            Dim o As Object = cmd.ExecuteScalar()
            If o Is Nothing OrElse o Is DBNull.Value Then Return Nothing
            Return Convert.ToString(o)
        End Using
    End Function

    Private Function JoinBracket(items As List(Of String)) As String
        Dim i As Integer
        Dim parts As New List(Of String)
        For i = 0 To items.Count - 1
            parts.Add("[" & items(i) & "]")
        Next
        Return String.Join(",", parts.ToArray())
    End Function

End Module
