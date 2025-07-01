Imports System.Data.SQLite
Imports System.Globalization

Public Class maintenance
    Public Shared Sub insMaintenance(lineCd As String, op As String, shift As String, brakTime_down As Date, wi As String, model As String)
        Dim api = New api()
        Dim st_datetime As String = brakTime_down.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
        Dim dt As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim sqliteConn As New SQLiteConnection(Backoffice_model.sqliteConnect)
        Try
recheck:
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "Insert into maintenance (mn_line , mn_op , mn_wi , mn_model , mn_breakdown_start , mn_shift , mn_create_date , mn_create_by , mn_status) 
                                values('" & lineCd & "' , '" & op & "' ,'" & wi & "' ,  '" & model & "' , '" & st_datetime & "' , '" & shift & "' , '" & dt & "' , '" & lineCd & "' , '0')"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            LoadSQL.Close()
        Catch ex As Exception
            MsgBox("SQLite Database connect failed. Please contact PC System [Function insMaintenance] In file maintenance" & ex.Message)
            sqliteConn.Close()
            GoTo recheck
        End Try
        '    Dim result_api_checkper = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/INSERT_DATA_NEW_FA/insDataLossByLine?LineCd=" & lineCd & "&op=" & op & "&shift=" & shift & "&brakTime_down=" & st_datetime2)
        'MsgBox(result_api_checkper)
    End Sub

    Public Shared Sub updMaintenanceSqlite()
        Dim api = New api()
        ' Dim st_datetime As String = brakTime_down.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
        'Dim dt As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim sqliteConn As New SQLiteConnection(Backoffice_model.sqliteConnect)
        Try
recheck:
            sqliteConn.Open()
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = "update maintenance set mn_status = '2' where mn_status = '1'"
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            LoadSQL.Close()
        Catch ex As Exception
            MsgBox("SQLite Database connect failed. Please contact PC System [Function updMaintenance] In file maintenance" & ex.Message)
            sqliteConn.Close()
            GoTo recheck
        End Try
    End Sub
    Public Shared Sub UpdateMaintenance(lineCd As String, op As String, shift As String, breakdown_total As String, restart_time As String, breakdown_start As Date)
        Dim api = New api()
        Dim st_datetime As String = breakdown_start.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
        Dim result_api_checkper = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/UPDATE_DATA/UpdateMaintenance?breakdown_total=" & breakdown_total & "&restart_time=" & restart_time & "&lineCd=" & lineCd & "&op=" & op & "&breakdown_start=" & st_datetime)
    End Sub
End Class
