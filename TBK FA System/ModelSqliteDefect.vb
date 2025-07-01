Imports System.Data.SQLite
Imports System.Globalization
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class ModelSqliteDefect
    Public Shared Function cancelCloselotStatusUpdate(status As String)
        Dim api As New api
        Try
            Dim Sql = " Update defect_transactions set dt_status_close_lot = '" & status & "' where dt_status_close_lot = '0'"
            Dim jsonData As String = api.Load_dataSQLite(Sql)
            Return jsonData
        Catch ex As Exception
            MsgBox("Error Files ModelSqliteDefect In Function cancelCloselotStatusUpdate")
        End Try
    End Function
    Public Shared Function UpdateStatusCloselotSqlite(status As String, pwi_id As String)
        Dim api As New api
        Try
            Dim Sql = " Update defect_transactions set dt_status_close_lot = '" & status & "' where pwi_id = '" & pwi_id & "'"
            Dim jsonData As String = api.Load_dataSQLite(Sql)
            Return jsonData
        Catch ex As Exception
            MsgBox("Error Files ModelSqliteDefect In Function UpdateStatusCloselotSqlite")
        End Try
    End Function
    Public Shared Function mSqliteGetDataQuality(line_cd As String, lot_no As String, startTimeFrith As String)
        Dim api As New api
        startTimeFrith = Convert.ToDateTime(startTimeFrith).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
        'Console.WriteLine("startTimeFrith ===>" & startTimeFrith)
        Try
            Dim Sql = "Select dt_code , sum(dt_qty) AS TotalQ,
                        (SELECT SUM(dt_qty)
                             FROM defect_transactions
                             I where 
		                        dt_line_cd ='" & line_cd & "' and dt_lot_no ='" & lot_no & "' and dt_main_cp = '1' and dt_type ='1' and dt_status_flg = '1' and dt_created_date >= '" & startTimeFrith & "'  and dt_status_close_lot != '2'
	                         ) AS AllDefect
                         from defect_transactions where dt_line_cd ='" & line_cd & "' and dt_lot_no ='" & lot_no & "' and dt_main_cp = '1' and dt_type ='1' and dt_status_flg = '1' and dt_created_date >= '" & startTimeFrith & "' and dt_status_close_lot != '2'  
                        GROUP by dt_code order by sum(dt_qty) Desc LIMIT 3;"
            'Console.WriteLine(Sql)
            Dim jsonData As String = api.Load_dataSQLite(Sql)
            Return jsonData
        Catch ex As Exception
            MsgBox("Error Files ModelSqliteDefect In Function mSqliteGetDataQuality")
        End Try
    End Function
    Public Shared Function mSqliteGetDataQualityOverAllNG(line_cd As String, lot_no As String, startTimeFrith As String)
        Dim api As New api
        startTimeFrith = Convert.ToDateTime(startTimeFrith).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
        'Console.WriteLine("startTimeFrith ===>" & startTimeFrith)
        Try
            Dim Sql = "Select dt_code , sum(dt_qty) AS TotalQ,
                        (SELECT SUM(dt_qty)
                             FROM defect_transactions
                             I where 
		                        dt_line_cd ='" & line_cd & "' and dt_lot_no ='" & lot_no & "'  and dt_type ='1' and dt_status_flg = '1' and dt_created_date >= '" & startTimeFrith & "'  and dt_status_close_lot != '2'
	                         ) AS AllDefect
                         from defect_transactions where dt_line_cd ='" & line_cd & "' and dt_lot_no ='" & lot_no & "' and  dt_type ='1' and dt_status_flg = '1' and dt_created_date >= '" & startTimeFrith & "' and dt_status_close_lot != '2' 
                        GROUP by dt_code order by sum(dt_qty) Desc LIMIT 3;"
            Console.WriteLine(Sql)
            Dim jsonData As String = api.Load_dataSQLite(Sql)
            Return jsonData
        Catch ex As Exception
            MsgBox("Error Files ModelSqliteDefect In Function mSqliteGetDataQuality")
        End Try
    End Function

    Public Shared Function mSqlieGetDataNGbyWILot(line_cd As String, lot_no As String, startFrist As String, wi As String)
        Dim api As New api
        Dim dt As DateTime = DateTime.Now
        Dim dateTimeCurr = dt.ToString("yyyy-MM-dd HH") & ":00:00"
        Dim dateTimeEND = dt.ToString("yyyy-MM-dd HH") & ":59:59"
        Try
            Dim Sql = "SELECT IFNULL(SUM(dt_qty), 0) AS total_qty
                        FROM defect_transactions
                        WHERE dt_wi_no = '" & wi & "' 
                          AND dt_lot_no = '" & lot_no & "' 
                          AND dt_created_date >= '" & startFrist & "' 
                          AND dt_item_type = '1' 
                          AND dt_type  = '1';"
            'Console.WriteLine(Sql)
            Dim jsonData As String = api.Load_dataSQLite(Sql)
            If jsonData <> "0" Then
                Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(jsonData)
                For Each items As Object In dcResultdata
                    Return items("total_qty").ToString
                Next
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("Error Files ModelSqliteDefect In Function mSqlieGetDataNGbyWILot")
        End Try
    End Function
    Public Shared Function mSqliteGetDataNG_BYHOUR(line_cd As String, lot_no As String)
        Dim api As New api
        Dim dt As DateTime = DateTime.Now
        Dim dateTimeCurr = dt.ToString("yyyy-MM-dd HH") & ":00:00"
        Dim dateTimeEND = dt.ToString("yyyy-MM-dd HH") & ":59:59"
        Try
            Dim Sql = "Select   sum(dt_qty) AS TotalNGByHour
                         from defect_transactions where dt_line_cd ='" & line_cd & "' and dt_lot_no ='" & lot_no & "' and dt_item_type = '1' and dt_type ='1' and dt_status_flg = '1' and dt_created_date between '" & dateTimeCurr & "'and '" & dateTimeEND & "'"
            'Console.WriteLine(Sql)
            Dim jsonData As String = api.Load_dataSQLite(Sql)
            Return jsonData

        Catch ex As Exception
            MsgBox("Error Files ModelSqliteDefect In Function mSqliteGetDataNG_BYHOUR")
        End Try
    End Function
    Public Function mSqliteGetdatachildpartsummaryfg(dtWino As String, dtSeq As String, dtLot As String)
        Dim api As New api
        Try
            Dim Sql = " 
                SELECT
						dt.dt_item_cd,
						dt.dt_code,
						SUM ( dt.dt_qty ) AS total_nc,
						dt.dt_item_type ,
						dt.dt_name_en AS description ,
						dt.dt_type,
						dt.dt_name_en AS defect_name,
                        dt.pwi_id
					FROM
						defect_transactions AS dt
					WHERE
						dt.dt_wi_no = '" & dtWino & "' 
						AND dt.dt_seq_no = '" & dtSeq & "' 
						AND dt.dt_lot_no = '" & dtLot & "' 
						AND dt.dt_status_flg = '1' 
						AND dt.dt_item_type = '1'
						AND dt.dt_qty <> '0'
					GROUP BY
						dt.dt_item_cd,
						dt.dt_code,
						dt.dt_item_type,
						dt.dt_name_en , 
						dt.dt_type,
						dt.dt_name_en,
                        dt.pwi_id
					ORDER BY
						dt.dt_item_type ASC
"
            Dim jsonData As String = api.Load_dataSQLite(Sql)
            If jsonData <> "0" Then
                Return jsonData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("Error Files ModelSqliteDefect In Function mSqliteGetdatachildpartsummaryfg")
        End Try
        ' No need to close the connection here; it's already closed after exiting the Using block
        Return 0
    End Function
    Public Function mSqliteGetdatachildpartsummarychild(dtWino As String, dtSeq As String, dtLot As String)
        Dim api As New api
        Try
            Dim Sql = " 
                SELECT
						dt.dt_item_cd,
						dt.dt_code,
						SUM ( dt.dt_qty ) AS total_nc,
						dt.dt_item_type ,
						dt.dt_name_en AS description ,
						dt.dt_type,
						dt.dt_name_en AS defect_name,
                        dt.pwi_id
					FROM
						defect_transactions AS dt
					WHERE
						dt.dt_wi_no = '" & dtWino & "' 
						AND dt.dt_seq_no = '" & dtSeq & "' 
						AND dt.dt_lot_no = '" & dtLot & "' 
						AND dt.dt_status_flg = '1' 
						AND dt.dt_item_type = '2'
						AND dt.dt_qty <> '0'
					GROUP BY
						dt.dt_item_cd,
						dt.dt_code,
						dt.dt_item_type,
						dt.dt_type,
                        dt.dt_name_en,
                        dt.pwi_id
					ORDER BY
						dt.dt_item_type ASC
"
            'Console.WriteLine(Sql)
            Dim jsonData As String = api.Load_dataSQLite(Sql)
            If jsonData <> "0" Then
                Return jsonData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("Error Files ModelSqliteDefect In Function mSqliteGetdatachildpartsummarychild")
        End Try
        ' No need to close the connection here; it's already closed after exiting the Using block
        Return 0
    End Function
    Public Shared Function mSqliteGetdatachildpartsummaryfgSpc(arrayWI As Array, lengthDataPlan As Integer, dtLot As String, startseq As Integer)
        Try
            Dim api = New api()
            Dim tmpseq As Integer = startseq
            Dim condition As String = ""
            Dim conditionseq As String = ""
            Dim index As Integer = 0
            For Each item As String In arrayWI
                tmpseq += 1
                condition &= item
                conditionseq &= tmpseq
                index += 1
                If index < lengthDataPlan Then
                    condition &= ","
                    conditionseq &= ","
                End If
            Next
            Dim sql = "
                    SELECT
						dt.dt_item_cd,
						dt.dt_code,
						SUM ( dt.dt_qty ) AS total_nc,
						dt.dt_item_type ,
						dt.dt_name_en AS description ,
						dt.dt_type,
						dt.dt_name_en AS defect_name,
						dt.dt_wi_no
					FROM
						defect_transactions AS dt
					WHERE
						( 
                            dt.dt_wi_no  in (" & condition & ")
                        ) 
						AND 
						(
							dt.dt_seq_no in (" & conditionseq & ")
						)
						AND dt.dt_lot_no = '" & dtLot & "' 
						AND dt.dt_status_flg = '1' 
						AND dt.dt_item_type = '1'
						AND dt.dt_qty <> '0'
					GROUP BY
						dt.dt_item_cd,
						dt.dt_code,
						dt.dt_item_type,
						dt.dt_type,
						dt.dt_name_en,
						dt.dt_wi_no
					ORDER BY
						dt.dt_item_type ASC
            "
            'Console.WriteLine(sql)
            Dim jsonData As String = api.Load_dataSQLite(sql)
            If jsonData <> "0" Then
                Return jsonData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check ModelSqliteDefect in Function mSqliteGetdatachildpartsummaryfgSpc = " & ex.Message)
            Return "0"
        End Try
    End Function


    Public Shared Function mSqliteGetdatachildpartsummarychildSpc(arrayWI As Array, lengthDataPlan As Integer, dtLot As String, startseq As Integer)
        Try
            Dim api = New api()
            Dim tmpseq As Integer = startseq
            Dim condition As String = ""
            Dim conditionseq As String = ""
            Dim index As Integer = 0
            For Each item As String In arrayWI
                tmpseq += 1
                condition &= item
                conditionseq &= tmpseq
                index += 1
                If index < lengthDataPlan Then
                    condition &= ","
                    conditionseq &= ","
                End If
            Next
            Dim sql = "
                   SELECT
						dt.dt_item_cd,
						dt.dt_code,
						SUM ( dt.dt_qty ) AS total_nc,
						dt.dt_item_type ,
						dt.dt_name_en AS description ,
						dt.dt_type,
						dt.dt_name_en AS defect_name , 
						dt.dt_wi_no
					FROM
						defect_transactions AS dt
					WHERE
						( 
                            dt.dt_wi_no in (" & condition & ")
                        ) 
						AND 
						(
							dt.dt_seq_no in (" & conditionseq & ")
						)
						AND dt.dt_lot_no = '" & dtLot & "' 
						AND dt.dt_status_flg = '1' 
						AND dt.dt_item_type = '2'
						AND dt.dt_qty <> '0'
					GROUP BY
						dt.dt_item_cd,
						dt.dt_code,
						dt.dt_item_type,
						dt.dt_type,
						dt.dt_name_en,
						dt.dt_wi_no
					ORDER BY
						dt.dt_item_type ASC
            "
            'Console.WriteLine(sql)
            Dim jsonData As String = api.Load_dataSQLite(sql)
            If jsonData <> "0" Then
                Return jsonData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelSqliteDefect in Function mSqliteGetdatachildpartsummarychildSpc = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function


    Public Shared Function mSqlitegroupDataWiSpc(WI As String, seq As String, dtLot As String)
        Try
            Dim requestFunction As New JObject()
            Dim api = New api()
            Dim sql = "
                    SELECT 
				                    dt_wi_no, 
				                    dt_seq_no,
				                    (
					                    select IFNULL(sum(dt_qty) , 0) 
					                    from  defect_transactions 
					                    where 
						                    dt_status_flg = '1' AND
						                    dt_type = '2' AND 
						                    dt_lot_no ='" & dtLot & "' AND
						                    dt_wi_no in (" & WI & ") AND
						                    dt_seq_no in (" & seq & ")  AND
						                    dt_item_type ='1'
				                    ) AS TotaldfNC ,
				                    (
					                    select IFNULL(sum(dt_qty) , 0) 
					                    from  defect_transactions 
					                    where 
						                    dt_status_flg = '1' AND
						                    dt_type = '1' AND 
						                    dt_lot_no ='" & dtLot & "' AND
						                    dt_wi_no in (" & WI & ") AND
						                    dt_seq_no in (" & seq & ")  AND
						                    dt_item_type ='1'

				                    ) AS TotaldfNG ,
				                    sum( dt_qty ) AS totalDEFECT 
				                    FROM
					                    defect_transactions 
				                    WHERE
					                    dt_status_flg = '1' AND
					                    dt_lot_no ='" & dtLot & "' AND
					                    dt_wi_no in (" & WI & ") AND
					                    dt_seq_no in (" & seq & ")  AND
					                    dt_item_type ='1'
				                    GROUP BY
					                    dt_wi_no,
					                    dt_seq_no
            "
            'Console.WriteLine(sql)
            Dim jsonData As String = api.Load_dataSQLite(sql)
            If jsonData <> "0" Then
                Return jsonData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check ModelSqliteDefect in Function mSqlitegroupDataWiSpc = " & ex.Message)
            Return "0"
        End Try
        Return "0"
    End Function

    Public Shared Function mSqliteGetdefectdetailncPartno(dtWino As String, dtSeq As String, dtLot As String, Type As String, PartNo As String)
        Try
            Dim api = New api()
            Dim sql = "
            SELECT
				    IFNULL(SUM(dt.dt_qty) , 0) AS total_nc
				    FROM
					    defect_transactions as dt
			WHERE
				dt.dt_wi_no = '" & dtWino & "' 
				AND dt.dt_seq_no = '" & dtSeq & "' 
				AND dt.dt_lot_no = '" & dtLot & "' 
				AND dt.dt_type = '" & Type & "' 
				And dt.dt_status_flg = '1'
				AND dt.dt_item_cd = '" & PartNo & "'"
            'Console.WriteLine(sql)
            Dim jsonData As String = api.Load_dataSQLite(sql)
            If jsonData <> "0" Then
                Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(jsonData)
                For Each items As Object In dcResultdata
                    Return items("total_nc").ToString
                Next
            Else
                Return False
            End If
            ' No need to close the connection here; it's already closed after exiting the Using block
        Catch ex As Exception
            MsgBox("connect Api Faill Please check ModelSqliteDefect in Function mSqliteGetdefectdetailncPartno = " & ex.Message)
            Return False
        End Try
    End Function
    Public Function mSqliteInsertDefectTransection(dt_wi_no As String, dt_line_cd As String, dt_item_cd As String, dt_item_type As String, dt_lot_no As String, dt_seq_no As String, dt_type As String, dt_code As String, dt_qty As String, dtMenu As String, dtActualdate As String, pwi_id As String, dt_name_en As String, mainCP As String, source_cd_supplier As String, leaderConfrime As String)
        Dim sqliteConn As New SQLiteConnection(Backoffice_model.sqliteConnect)
        Backoffice_model.Check_connect_sqlite()
        Backoffice_model.Clear_sqlite()
        Try
            ' Dim created_date = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
            Dim created_date = Now.ToString("yyyy-MM-dd HH:mm:ss")
            sqliteConn.Open()
            Dim sva_ip_address As String = ""
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            If Convert.ToInt32(dt_qty) > 0 Then
                dt_status_flg = "1"
            Else
                dt_status_flg = "5"
            End If
            Dim created_by As String = ""
            Dim updated_by As String = ""
            If leaderConfrime <> "" Then
                created_by = leaderConfrime
                updated_by = leaderConfrime
            Else
                created_by = dt_line_cd
                updated_by = dt_line_cd
            End If
            cmd.CommandText = " 
                INSERT into defect_transactions (
					dt_wi_no,
					dt_line_cd,
					dt_item_cd,
					dt_item_type,
					dt_lot_no,
					dt_seq_no,
					dt_type,
					dt_code,
					dt_qty,
					dt_menu,
					dt_actual_date,
					dt_status_flg,
					dt_created_date,
					dt_created_by,
					dt_updated_date,
					dt_updated_by,
					pwi_id,
                    dt_status_close_lot,
                    dt_name_en,
                    dt_main_cp,
                    dt_supplier_code
					) Values(
					'" & dt_wi_no & "',
					'" & dt_line_cd & "',
					'" & dt_item_cd & "',
					'" & dt_item_type & "',
					'" & dt_lot_no & "',
					'" & dt_seq_no & "',
					'" & dt_type & "',
					'" & dt_code & "',
					'" & dt_qty & "',
					'" & dtMenu & "',
					'" & created_date & "',
					'" & dt_status_flg & "',
					 '" & created_date & "',
					'" & created_by & "',
					 '" & created_date & "',
					'" & updated_by & "',
					'" & pwi_id & "',
                    '0',
                    '" & dt_name_en & "' , 
                    '" & mainCP & "',
                    '" & source_cd_supplier & "')
            "
            Console.WriteLine(cmd.CommandText)
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            sqliteConn.Close()
            Return True
        Catch ex As Exception
            MsgBox("SQLite Database connect failed. Please contact System System [Function mInsertDefectTransection] In File ModelSqliteDefect.")
            sqliteConn.Close()
            Return False
        End Try
        Return 0
    End Function
    Public Shared Function mUpdateaddjust(dtWino As String, dtLotNo As String, dtSeqno As String, dtType As String, dtCode As String, ItemType As String, PartNo As String, supplier_cd As String)
        'Dim api = New api()
        ' Dim rsData As Boolean = api.Load_data("http://" & Backoffice_model.svApi & "/"/apiShopfloor/index.php/_test/updateDatadefect/updateDatadefectregister?dtWino=" & dtWino & "&dtLotNo=" & dtLotNo & "&dtSeqno=" & CDbl(Val(dtSeqno)) & "&dtType=" & dtType & "&dtCode=" & dtCode & "&dtItemType=" & ItemType & "&PartNo=" & PartNo)
        Dim sqliteConn As New SQLiteConnection(Backoffice_model.sqliteConnect)
        Backoffice_model.Check_connect_sqlite()
        Backoffice_model.Clear_sqlite()
        Try
            Dim created_date = DateTime.Now.ToString("yyyy-MM-dd H:m:s")
            sqliteConn.Open()
            Dim sva_ip_address As String = ""
            Dim cmd As New SQLiteCommand
            cmd.Connection = sqliteConn
            cmd.CommandText = " update defect_transactions set dt_status_flg = '5' where dt_wi_no='" & dtWino & "' and dt_lot_no = '" & dtLotNo & "' and dt_seq_no = '" & dtSeqno & "' and dt_type = '" & dtType & "' and dt_code = '" & dtCode & "' and dt_item_type = '" & ItemType & "' and dt_item_cd = '" & PartNo & "' and dt_supplier_code = '" & supplier_cd & "'"
            'Console.WriteLine(cmd.CommandText)
            Dim LoadSQL As SQLiteDataReader = cmd.ExecuteReader()
            sqliteConn.Close()
            Return True
        Catch ex As Exception
            '  MsgBox("SQLite Database connect failed. Please contact System System [Function mUpdateaddjust] In File ModelSqliteDefect.")
            Return False
        End Try
    End Function
    Public Shared Function mSqliteGetdefectdetailnc(dfWi As String, dfSeq As String, dfLot As String, dfType As String)
        Dim api As New api
        Try
            'Console.WriteLine("--------------------------->")
            Dim Sql = "SELECT dt.dt_item_cd, dt.dt_code, SUM(dt.dt_qty) AS total_nc, dt.dt_name_en ,  dt.dt_item_type, dt.dt_wi_no as dt_wi_no , dt.dt_main_cp as dt_main_cp , dt.dt_supplier_code as dt_supplier_code " &
          "FROM defect_transactions as dt " &
          "WHERE dt.dt_wi_no = '" & dfWi & "' " &
          "AND dt.dt_seq_no = '" & dfSeq & "' " &
          "AND dt.dt_lot_no = '" & dfLot & "' " &
          "AND dt.dt_type = '" & dfType & "' " &
          "AND dt.dt_status_flg = '1' " &
          "GROUP BY dt.dt_item_cd, dt.dt_name_en , dt.dt_code, dt.dt_item_type, dt.dt_wi_no  , dt.dt_main_cp  , dt.dt_supplier_code " &
          "ORDER BY dt.dt_item_type ASC"
            'Console.WriteLine(Sql)
            'Console.WriteLine("-------------77777777-------------->")
            Dim jsonData As String = api.Load_dataSQLite(Sql)
            Return jsonData
        Catch ex As Exception
            MsgBox("Error Files ModelSqliteDefect In Function  nc")
        End Try
        ' No need to close the connection here; it's already closed after exiting the Using block
        Return 0
    End Function
    Public Shared Async Function mSqliteGetdefectdetail(dfWi As String, dfSeq As String, dfLot As String, dfType As String, item_cd As String, dtCode As String) As Task(Of String)
        Dim api As New api
        Try
            'Console.WriteLine("--------------------------->")
            Dim Sql = "SELECT dt.dt_item_cd, dt.dt_code, SUM(dt.dt_qty) AS total_nc, dt.dt_name_en ,  dt.dt_item_type, dt.dt_wi_no as dt_wi_no , dt.dt_main_cp as dt_main_cp , dt.dt_supplier_code as dt_supplier_code " &
          "FROM defect_transactions as dt " &
          "WHERE dt.dt_wi_no = '" & dfWi & "' " &
          "AND dt.dt_seq_no = '" & dfSeq & "' " &
          "AND dt.dt_lot_no = '" & dfLot & "' " &
          "AND dt.dt_type = '" & dfType & "' " &
          "AND dt.dt_item_cd = '" & item_cd & "' " &
          "AND dt.dt_code = '" & dtCode & "' " &
          "AND dt.dt_status_flg = '1' " &
          "GROUP BY dt.dt_item_cd, dt.dt_name_en , dt.dt_code, dt.dt_item_type, dt.dt_wi_no  , dt.dt_main_cp  , dt.dt_supplier_code " &
          "ORDER BY dt.dt_item_type ASC"
            'Console.WriteLine(Sql)
            Dim jsonData As String = api.Load_dataSQLite(Sql)
            Return jsonData
        Catch ex As Exception
            MsgBox("Error Files ModelSqliteDefect In Function mSqliteGetdefectdetail")
        End Try
        ' No need to close the connection here; it's already closed after exiting the Using block
        Return 0
    End Function

    Public Shared Async Function mSqliteGetdefectdetailLeaderConFirm(dfWi As String, dfSeq As String, dfLot As String, dfType As String, item_cd As String, dtCode As String) As Task(Of String)
        Dim api As New api
        Try
            'Console.WriteLine("--------------------------->")
            Dim Sql = "SELECT dt.dt_line_cd , dt.dt_item_cd, dt.dt_code, SUM(dt.dt_qty) AS total_nc, dt.dt_name_en ,  dt.dt_item_type, dt.dt_wi_no as dt_wi_no , dt.dt_main_cp as dt_main_cp , dt.dt_supplier_code as dt_supplier_code  , dt.dt_created_by " &
          "FROM defect_transactions as dt " &
          "WHERE dt.dt_wi_no = '" & dfWi & "' " &
          "AND dt.dt_seq_no = '" & dfSeq & "' " &
          "AND dt.dt_lot_no = '" & dfLot & "' " &
          "AND dt.dt_type = '" & dfType & "' " &
          "AND dt.dt_item_cd = '" & item_cd & "' " &
          "AND dt.dt_code = '" & dtCode & "' " &
          "AND dt.dt_status_flg = '1' " &
          "GROUP BY dt.dt_line_cd , dt.dt_item_cd, dt.dt_name_en , dt.dt_code, dt.dt_item_type, dt.dt_wi_no  , dt.dt_main_cp  , dt.dt_supplier_code  , dt.dt_created_by " &
          " ORDER BY dt.dt_item_type ASC"
            Console.WriteLine("mSqliteGetdefectdetailLeaderConFirm===>" & Sql)
            Dim jsonData As String = api.Load_dataSQLite(Sql)
            Return jsonData
        Catch ex As Exception
            MsgBox("Error Files ModelSqliteDefect In Function mSqliteGetdefectdetailLeaderConFirm")
        End Try
        ' No need to close the connection here; it's already closed after exiting the Using block
        Return 0
    End Function
    Public Shared Function mSqliteGetdefectdetailncPartnoSpc(arrayWI As Array, lengthDataPlan As Integer, dtLot As String, Type As String, PartNo As String, dfWiSel As String, dfSeqSel As String)
        Try
            Dim api = New api()
            Dim requestFunction As New JObject()
            Dim jArrayWI As New JArray(arrayWI)
            Dim condition As String = ""
            Dim conditionseq As String = ""
            Dim index As Integer = 0
            Dim sql = "
SELECT
				 IFNULL(SUM(dt.dt_qty) , 0) AS total_nc
				 FROM
					 defect_transactions as dt
			WHERE
				dt.dt_wi_no = '" & dfWiSel & "' 
				AND dt.dt_seq_no = '" & dfSeqSel & "' 
				AND dt.dt_lot_no = '" & dtLot & "' 
				AND dt.dt_type = '" & Type & "' 
				And dt.dt_status_flg = '1'
				AND dt.dt_item_cd = '" & PartNo & "'"
            requestFunction("arrWi") = jArrayWI
            requestFunction("lengthDataPlan") = lengthDataPlan
            requestFunction("dfLot") = dtLot
            requestFunction("Type") = Type
            requestFunction("PartNo") = PartNo
            requestFunction("dfWiSel") = dfWiSel
            requestFunction("dfSeqSel") = dfSeqSel
            'Console.WriteLine(sql)
            Dim jsonData As String = api.Load_dataSQLite(sql)
            If jsonData <> "0" Then
                Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(jsonData)
                For Each items As Object In dcResultdata
                    Return items("total_nc").ToString
                Next
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mSqliteGetdefectdetailncPartnoSpc = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mSqliteGetdefectdetailncSpc(arrayWI As Array, lengthDataPlan As Integer, dtLot As String, Type As String, startSeq As Integer)
        Try
            Dim api = New api()
            Dim requestFunction As New JObject()
            Dim jArrayWI As New JArray(arrayWI)
            requestFunction("arrWi") = jArrayWI
            requestFunction("lengthDataPlan") = lengthDataPlan
            requestFunction("startseq") = startSeq
            requestFunction("dfLot") = dtLot
            requestFunction("dfType") = Type
            Dim tmpseq As Integer = startSeq
            Dim condition As String = ""
            Dim conditionseq As String = ""
            Dim index As Integer = 0
            For Each item As String In arrayWI
                tmpseq += 1
                condition &= item
                conditionseq &= tmpseq
                index += 1
                If index < lengthDataPlan Then
                    condition &= ","
                    conditionseq &= ","
                End If
            Next
            Dim Sql = " SELECT
				dt.dt_item_cd,
				dt.dt_code,
                dt.dt_name_en , 
				SUM (dt.dt_qty) AS total_nc,
				dt.dt_item_type ,
				dt.dt_name_en AS description,
				dt.dt_name_en AS defect_name,
				dt.dt_wi_no,
				dt.dt_seq_no,
				dt.pwi_id,
				dt.dt_main_cp,
				dt.dt_supplier_code
			FROM
				defect_transactions as dt
			WHERE
				(
					dt.dt_wi_no  in (" & condition & ") 
				) 
				AND (
					dt.dt_seq_no  in (" & conditionseq & ")
					) 
				AND dt.dt_lot_no = '" & dtLot & "' 
				AND dt.dt_type = '" & Type & "' 
				And dt.dt_status_flg = '1'
			GROUP BY
				dt.dt_item_cd,
				dt.dt_code,
				dt.dt_item_type ,
				dt.dt_wi_no,
				dt.dt_seq_no,
				dt.pwi_id,
                dt.dt_name_en,
				dt.dt_main_cp,
				dt.dt_supplier_code
				order by dt.dt_item_type asc"
            'Console.WriteLine(Sql)
            Dim jsonData As String = api.Load_dataSQLite(Sql)
            If jsonData <> "0" Then
                Return jsonData
            Else
                Return "0"
            End If

            ' 'Console.WriteLine(rsData)
            ' If rsData <> "0" Then
            ' Return rsData
            ' Else
            ' Return False
            '  End If
        Catch ex As Exception
            MsgBox("Error Files ModelSqliteDefect in Function mSqliteGetdefectdetailncSpc = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mSqliteUpdateaddjust(dtWino As String, dtLotNo As String, dtSeqno As String, dtType As String, dtCode As String, ItemType As String, PartNo As String)
        Try
            Dim api = New api()
            Dim sql = "update defect_transactions set dt_status_flg = '5' where dt_wi_no='" & dtWino & "' and dt_lot_no = '" & dtLotNo & "' and dt_seq_no = '" & dtSeqno & "' and dt_type = '" & dtType & "' and dt_code = '" & dtCode & "' and dt_item_type = '" & ItemType & "' and dt_item_cd = '" & PartNo & "'"
            'Console.WriteLine(sql)
            Dim jsonData As String = api.Load_dataSQLite(sql)
            Return jsonData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mSqliteUpdateaddjust = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mSqliteGetdatachildpartsummarychildgrouppart(dtWino As String, dtSeq As String, dtLot As String, dfType As String)
        Try
            Dim api = New api()
            Dim sql = "SELECT
						dt.dt_item_cd,
						SUM ( dt.dt_qty ) AS total_nc,
						dt.dt_item_type 
					FROM
						defect_transactions AS dt
					WHERE
						dt.dt_wi_no = '" & dtWino & "' 
						AND dt.dt_seq_no = '" & dtSeq & "' 
						AND dt.dt_lot_no = '" & dtLot & "' 
						AND dt.dt_status_flg = '1' 
						AND dt.dt_item_type = '2'
						AND dt.dt_type = '" & dfType & "'
						AND dt.dt_qty <> '0'
					GROUP BY
						dt.dt_item_cd,
						dt.dt_item_type
					ORDER BY
						dt.dt_item_type ASC"
            Console.WriteLine(sql)
            Dim jsonData As String = api.Load_dataSQLite(sql)
            If jsonData <> "0" Then
                Return jsonData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mSqliteGetdatachildpartsummarychildgrouppart = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mSqliteGetdatachildpartsummaryfggrouppart(dtWino As String, dtSeq As String, dtLot As String, dfType As String)
        Try
            Dim api = New api()
            Dim sql = "SELECT
						dt.dt_item_cd,
						SUM ( dt.dt_qty ) AS total_nc,
						dt.dt_item_type 
					FROM
						defect_transactions AS dt
					WHERE
						dt.dt_wi_no = '" & dtWino & "' 
						AND dt.dt_seq_no = '" & dtSeq & "' 
						AND dt.dt_lot_no = '" & dtLot & "' 
						AND dt.dt_status_flg = '1' 
						AND dt.dt_item_type = '1'
						AND dt.dt_type = '" & dfType & "'
						AND dt.dt_qty <> '0'
					GROUP BY
						dt.dt_item_cd,
						dt.dt_item_type
					ORDER BY
						dt.dt_item_type ASC"
            'Console.WriteLine(sql)
            Dim jsonData As String = api.Load_dataSQLite(sql)
            If jsonData <> "0" Then
                Return jsonData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelDefect in Function mSqliteGetdatachildpartsummaryfggrouppart = " & ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function mSqliteGetdatachildpartsummarychildgrouppartAdminAdjust(dfWi As String, dfSeq As String, dfLot As String, dfType As String, ItemCd As String)
        Try
            Dim api = New api()
            Dim sql = "SELECT
						dt.dt_item_cd,
						SUM ( dt.dt_qty ) AS total_nc,
						dt.dt_item_type 
					FROM
						defect_transactions AS dt
					WHERE
						dt.dt_wi_no = '" & dfWi & "' 
						AND dt.dt_seq_no = '" & dfSeq & "' 
						AND dt.dt_lot_no = '" & dfLot & "' 
						AND dt.dt_status_flg = '1' 
						AND dt.dt_item_type = '2'
						AND dt.dt_type = '" & dfType & "'
						AND dt.dt_qty <> '0'
						AND dt.dt_item_cd = '" & ItemCd & "' 
					GROUP BY
						dt.dt_item_cd,
						dt.dt_item_type
					ORDER BY
						dt.dt_item_type ASC"
            Dim jsonData As String = api.Load_dataSQLite(sql)
            If jsonData <> "0" Then
                Return jsonData
            Else
                Return "0"
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelSqliteDefect in Function mSqliteGetdatachildpartsummarychildgrouppartAdminAdjust = " & ex.Message)
            Return False
        End Try
    End Function
End Class