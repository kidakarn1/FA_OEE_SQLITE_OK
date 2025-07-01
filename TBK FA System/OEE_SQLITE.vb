Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports NodaTime
Public Class OEE_SQLITE
    Public Shared Function GetTimeWorkingByModel(st_shift As String, line_cd As String, date_start As String, dateCurr As String, TimeCurr As String)
        Dim api As New api
        Dim dateTimestart = date_start + " " + st_shift + ":00"
        Dim dateTimeEnd = dateCurr + " " + TimeCurr
        Try
            Dim Sql = "SELECT top 1 * from production_actual_detail where st_time between '" & dateTimestart & "' and '" & dateTimeEnd & "' and item_cd = '" & line_cd & "'"
            Console.WriteLine(Sql)
            Dim jsonData As String = api.Load_dataSQLite(Sql)
            load_show_OEE.Close()
            Return jsonData
        Catch ex As Exception
            '' MsgBox("Error Files OEE_SQLITE In Function GetTimeWorkingByModel")
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    load_show_OEE.Show()
                Else
                    load_show.Show()
                End If
            Catch ex2 As Exception
                load_show_OEE.Close()
                load_show.Show()
            End Try
        End Try
    End Function
    Public Shared Function mas_OEE_GET_Data_AccTarget(st_shift As String, std_ct As String)
        Try
            Dim api = New api()
            Dim date_now_date As Date = DateTime.Now.ToString("yyyy-MM-dd")
            ' Dim time As Date = TimeOfDay.ToString("HH:mm:ss") 'DateTime.Now.ToString("HH:mm:ss")
            Dim time As String = DateTime.Now.ToString("HH:mm:ss")
            Dim date_st = DateTime.Now.ToString("yyyy-MM-dd")
            Dim date_end = DateTime.Now.ToString("yyyy-MM-dd")
            Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
            If time_now >= "00:00:00 AM" And time_now <= "08:00:00 AM" Then
                date_st = date_now_date.AddDays(-1)
            End If
            date_st = date_st
            Dim convertDateStart = Convert.ToDateTime(date_st).ToString("yyyy-MM-dd")
            Dim date_start As String = convertDateStart & " " & st_shift & ":00"
            Dim dateTimeCurr As String = date_end & " " & time
            Dim diffdatesec As Integer = DateDiff(DateInterval.Second, DateTime.Parse(date_start), DateTime.Parse(dateTimeCurr))
            Try
                If std_ct = 0 Then
                    ' Do something if value is 0
                    std_ct = 1
                End If
            Catch ex As Exception
                ' Handle any errors that occur in the try block
                std_ct = 1
            End Try
            ' Calculate and send the result
            Dim results As Integer = CInt((diffdatesec / std_ct).ToString("F0"))
            ' Assuming you're sending this as JSON response:
            load_show_OEE.Close()
            Return results
        Catch ex As Exception
            'MsgBox("ERROR OEE FUNCTION OEE_GET_Data_AccTarget Please Check API")
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    load_show_OEE.Show()
                Else
                    load_show.Show()
                End If
            Catch ex2 As Exception
                load_show_OEE.Close()
                load_show.Show()
            End Try
        End Try
    End Function
    Public Shared Function mas_GetDataProgressbarA(st_shift As String, end_shift As String, line_cd As String, dateTimeswmodel As String, statusSwitchModel As String, IsOnlyone As String, SpecFlgLine As String)
        Try
            ' MsgBox("special  ==>" & SpecFlgLine)
            Console.WriteLine("dateTimeswmodel= ==>" & dateTimeswmodel)
            Console.WriteLine("IsOnlyone ===>" & IsOnlyone)
            Console.WriteLine("statusSwitchModel ===>" & statusSwitchModel)
            Dim api = New api()
            Dim date_now_date As Date = DateTime.Now.ToString("yyyy-MM-dd")
            ' Dim time As Date = TimeOfDay.ToString("HH:mm:ss") 'DateTime.Now.ToString("HH:mm:ss")
            Dim time As String = DateTime.Now.ToString("HH:mm:ss")
            Dim date_st = DateTime.Now.ToString("yyyy-MM-dd")
            Dim date_end = DateTime.Now.ToString("yyyy-MM-dd")
            Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
            If time_now >= "00:00:00 AM" And time_now <= "08:00:00 AM" Then
                date_st = date_now_date.AddDays(-1)
            End If
            date_st = date_st
            Dim dateTimeend As String
            Dim date_start = Convert.ToDateTime(date_st).ToString("yyyy-MM-dd")
            Dim date_Crr = Convert.ToDateTime(date_now_date).ToString("yyyy-MM-dd")
            Dim timeCurr As Integer = DateTime.Now.Hour
            Dim dateTimestart As String
            If statusSwitchModel = 0 Then
                dateTimestart = date_start & " " & st_shift & ":00"
            ElseIf statusSwitchModel = 1 OrElse statusSwitchModel = 2 Then
                If IsOnlyone = "1" Then
                    dateTimestart = date_start & " " & st_shift & ":00"
                Else
                    dateTimestart = dateTimeswmodel
                End If
            End If
            If SpecFlgLine = 2 Then ' for K1M083
                dateTimestart = date_st & " " & st_shift & ":00"
                '     MsgBox("special dateTimestart==>" & dateTimestart)
            End If
            Try ' สำคัญ
                dateTimestart = ConvertFormatDate(dateTimestart) 'Add เพราะ Format Date Query ไม่ได้ '28/03/25 20:00:00 สำคัญ
            Catch ex As Exception ' สำคัญ
            End Try
            Console.WriteLine("F mas_GetDataProgressbarA dateTimestart===>" & dateTimestart)
            date_Crr = Convert.ToDateTime(date_Crr).ToString("yyyy-MM-dd")
            dateTimeend = date_Crr & " " & end_shift & ":00"
            If String.Compare(end_shift, "00:00") >= 0 AndAlso String.Compare(end_shift, "08:00") <= 0 Then
                Dim convertDateStart As DateTime = DateTime.Parse(date_start)
                convertDateStart = convertDateStart.AddDays(1)
                Dim newDateStr As String = convertDateStart.ToString("yyyy-MM-dd")
                dateTimeend = newDateStr & " " & end_shift & ":00"
            End If
            dateTimeend = DateTime.ParseExact(dateTimeend, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss")
            Dim diffdatesec As Long = DateDiff(DateInterval.Second, DateTime.Parse(dateTimestart), DateTime.Parse(dateTimeend))
            Dim breakTime = 20
            Dim lunch = 0
            Dim Dinnerbrak = 0
            If diffdatesec >= 3600 Then ' > 1 Hr
                If timeCurr >= 12 OrElse timeCurr >= 24 Then
                    lunch = 40
                End If
                If end_shift = "20:00" OrElse end_shift = "08:00" Then
                    If timeCurr >= 12 OrElse timeCurr >= 24 Then
                        Dinnerbrak = 30
                    End If
                End If
            Else
                lunch = 0
                Dinnerbrak = 0
            End If
            Dim Allbreak = breakTime + lunch + Dinnerbrak
            Dim diffdatemin = (diffdatesec / 60) - Allbreak
            Dim loadingTime As Integer
            Dim SqlString As String
            If SpecFlgLine <> 2 Then
                SqlString = "SELECT 
                            COALESCE(SUM(la.loss_time), 0) AS totalLoss
                        FROM 
                            loss_actual AS la
                        JOIN 
                            sys_loss_mst AS slm
                            ON la.loss_cd_id = slm.id
                        WHERE
                            la.line_cd = '" & line_cd & "'
                            AND la.start_loss BETWEEN '" & dateTimestart & "' AND'" & dateTimeend & "'
                            AND slm.loss_cd IN ('A', 'B', 'T', 'D', 'P1', 'X' , 'E1');"
            Else
                SqlString = "SELECT 
                            COALESCE(SUM(la.loss_time), 0) / 5 AS totalLoss
                        FROM 
                            loss_actual AS la
                        JOIN 
                            sys_loss_mst AS slm
                            ON la.loss_cd_id = slm.id
                        WHERE
                            la.line_cd = '" & line_cd & "'
                            AND la.start_loss BETWEEN '" & dateTimestart & "' AND'" & dateTimeend & "'
                            AND slm.loss_cd IN ('A', 'B', 'T', 'D', 'P1', 'X' , 'E1');"
            End If
            Console.WriteLine(SqlString)
            Dim jsonData As String = api.Load_dataSQLite(SqlString)
            '  If jsonData <> "0" Then
            Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(jsonData)
            For Each items As Object In dcResultdata
                loadingTime = diffdatemin - CDbl(Val(items("totalLoss").ToString()))
            Next
            '   End If
            Dim SqlString2 As String
            If SpecFlgLine <> 2 Then
                SqlString2 = "SELECT 
                            COALESCE(SUM(la.loss_time), 0) AS totalLoss
                        FROM
                            loss_actual AS la
                        JOIN
                            sys_loss_mst AS slm 
                            ON la.loss_cd_id = slm.id
                        WHERE 
                            la.line_cd = '" & line_cd & "'
                            AND la.start_loss BETWEEN '" & dateTimestart & "' AND'" & dateTimeend & "'
                            AND slm.loss_cd NOT IN ('A', 'B', 'T', 'D', 'P1', 'X' , 'E1')
                            AND la.flg_control = '1';"

            Else
                SqlString2 = "SELECT 
                            COALESCE(SUM(la.loss_time), 0) / 5  AS totalLoss
                        FROM
                            loss_actual AS la
                        JOIN 
                            sys_loss_mst AS slm
                            ON la.loss_cd_id = slm.id
                        WHERE 
                            la.line_cd = '" & line_cd & "'
                            AND la.start_loss BETWEEN '" & dateTimestart & "' AND'" & dateTimeend & "'
                            AND slm.loss_cd NOT IN ('A', 'B', 'T', 'D', 'P1', 'X', 'E1')
                            AND la.flg_control = '1';"
            End If
            Console.WriteLine(SqlString2)
            Dim jsonData2 As String = api.Load_dataSQLite(SqlString2)
            Dim OperatingTime As Integer = 0
            ' If jsonData2 <> "0" Then
            Dim dcResultdata2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(jsonData2)
            For Each items As Object In dcResultdata2
                'MsgBox("loadingTime ===>" & loadingTime)
                'MsgBox("totalLoss ===>" & items("totalLoss").ToString())
                OperatingTime = loadingTime - items("totalLoss").ToString()
            Next
            ' End If
            Dim rs As Double
            Dim result As Double = OperatingTime / loadingTime
            If Double.IsNaN(result) Then
                rs = Math.Floor(1 * 100)
            Else
                rs = Math.Floor(result * 100)
            End If
            Dim rsTotal = Math.Floor(rs)
            load_show_OEE.Close()
            Return rsTotal
        Catch ex As Exception
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    load_show_OEE.Show()
                Else
                    load_show.Show()
                End If
            Catch ex2 As Exception
                load_show_OEE.Close()
                load_show.Show()
            End Try
        End Try
    End Function
    Public Shared Function ConvertFormatDate(dateFormat As String)
        Dim ConvertDate = DateTime.ParseExact(dateFormat, "dd/MM/yy HH:mm:ss", Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss")
        Console.WriteLine("ConvertFormatDate ====>" & ConvertDate)
        Return ConvertDate
    End Function
    Public Shared Function OEE_GET_Data_LOSS(line_cd As String, lot_no As String, shift As String, dateStart As String, dateTimeswModel As String, statusSwitchModel As String, IsOnlyone As String, SpecFlgLine As String)
        Try
            Dim start_dateTime As String
            Dim date_now_date As Date = DateTime.Now.ToString("yyyy-MM-dd")
            Dim st_shift = Prd_detail.Label12.Text.Substring(3, 5) & ":00"
            Dim dateEnd = DateTime.Now.ToString("yyyy-MM-dd")
            Dim end_shift As String = DateTime.Now.ToString("HH:mm:ss")
            Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
            If time_now >= "00:00:00 AM" And time_now <= "08:00:00 AM" Then
                dateStart = date_now_date.AddDays(-1)
            End If
            Console.WriteLine("OEE_GET_Data_LOSS > statusSwitchModel===>" & statusSwitchModel)
            If statusSwitchModel = 0 Then
                start_dateTime = dateStart & " " & st_shift
            ElseIf statusSwitchModel = 1 Or statusSwitchModel = 2 Then
                If IsOnlyone = "1" Then
                    start_dateTime = dateStart & " " & st_shift
                Else
                    start_dateTime = dateTimeswModel
                End If
            End If
            Console.WriteLine("ready format date ")
            If SpecFlgLine = 2 Then 'for K1M083
                start_dateTime = dateStart & " " & st_shift
            End If
            Try ' สำคัญ
                start_dateTime = ConvertFormatDate(start_dateTime) 'Add เพราะ Format Date Query ไม่ได้ '28/03/25 20:00:00 สำคัญ
            Catch ex As Exception ' สำคัญ
            End Try
            Dim end_dateTime As String = dateEnd & " " & end_shift
            Dim result As DataTable
            Dim chk_spec_line As Integer = SpecFlgLine
            Dim sqlQuery As String = ""
            If chk_spec_line = 0 Or chk_spec_line = 1 Then
                sqlQuery = "SELECT slm.loss_cd,
       SUM(la.loss_time) AS lossTime,
       (SELECT SUM(la2.loss_time)
        FROM loss_actual AS la2
             INNER JOIN production_working_info AS pwi2 ON pwi2.pwi_id = la2.pwi_id
                      WHERE pwi2.pwi_lot_no = '" & lot_no & "'
                        AND la2.line_cd = '" & line_cd & "'
                        AND la2.start_loss BETWEEN '" & start_dateTime & "' AND '" & end_dateTime & "'
                        AND la2.loss_cd_id != '36'
                        AND la2.flg_control = '1'
                        AND la2.loss_cd_id != '35') AS AllLossTime
                    FROM loss_actual AS la
                         INNER JOIN production_working_info AS pwi ON pwi.pwi_id = la.pwi_id
                         INNER JOIN sys_loss_mst AS slm ON la.loss_cd_id = slm.id
                            WHERE pwi.pwi_lot_no =  '" & lot_no & "'
                              AND la.line_cd ='" & line_cd & "'
                              AND la.start_loss BETWEEN  '" & start_dateTime & "' AND '" & end_dateTime & "'
                      AND la.loss_cd_id != '36'
                      AND la.flg_control = '1'
                      AND la.loss_cd_id != '35'
                    GROUP BY slm.loss_cd
                    ORDER BY lossTime DESC
                    LIMIT 3;"
                ' Normal case
            Else
                ' Special case
                ' start_dateTime = dateStart & " " & st_shift
                sqlQuery = "SELECT slm.loss_cd,
               SUM(la.loss_time) / 5  AS lossTime,
               (SELECT SUM(la2.loss_time) / 5 
                FROM loss_actual AS la2
                     INNER JOIN production_working_info AS pwi2 ON pwi2.pwi_id = la2.pwi_id
                              WHERE pwi2.pwi_lot_no = '" & lot_no & "'
                                AND la2.line_cd = '" & line_cd & "'
                                AND la2.start_loss BETWEEN '" & start_dateTime & "' AND '" & end_dateTime & "'
                                AND la2.loss_cd_id != '36'
                                AND la2.flg_control = '1'
                                AND la2.loss_cd_id != '35') AS AllLossTime
        FROM loss_actual AS la
             INNER JOIN production_working_info AS pwi ON pwi.pwi_id = la.pwi_id
             INNER JOIN sys_loss_mst AS slm ON la.loss_cd_id = slm.id
                WHERE pwi.pwi_lot_no =  '" & lot_no & "'
                  AND la.line_cd ='" & line_cd & "'
                  AND la.start_loss BETWEEN  '" & start_dateTime & "' AND '" & end_dateTime & "'
          AND la.loss_cd_id != '36'
          AND la.flg_control = '1'
          AND la.loss_cd_id != '35'
        GROUP BY slm.loss_cd
        ORDER BY lossTime DESC
        LIMIT 3;"
            End If
            Dim api = New api()
            Console.WriteLine(sqlQuery)
            Dim jsonData2 As String = api.Load_dataSQLite(sqlQuery)
            ' If jsonData2 <> "0" Then
            load_show_OEE.Close()
            Return jsonData2
        Catch ex As Exception
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    load_show_OEE.Show()
                Else
                    load_show.Show()
                End If
            Catch ex2 As Exception
                load_show_OEE.Close()
                load_show.Show()
            End Try
        End Try
    End Function
    Public Shared Function mas_OEE_getDateTimeStart(st_shift As String, line_cd As String)
        Try
            Dim api = New api()
            Dim date_now_date As Date = DateTime.Now.ToString("yyyy-MM-dd")
            ' Dim time As Date = TimeOfDay.ToString("HH:mm:ss") 'DateTime.Now.ToString("HH:mm:ss")
            Dim time As String = DateTime.Now.ToString("HH:mm:ss")
            Dim date_st = DateTime.Now.ToString("yyyy-MM-dd")
            Dim date_end = DateTime.Now.ToString("yyyy-MM-dd")
            Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
            If time_now >= "00:00:00 AM" And time_now <= "08:00:00 AM" Then
                date_st = date_now_date.AddDays(-1)
            End If
            date_st = date_st
            Dim convertDateStart = Convert.ToDateTime(date_st).ToString("yyyy-MM-dd")
            Dim date_start = convertDateStart
            Dim now As DateTime = DateTime.Now
            Dim dateTimeCurr = date_end & " " & time
            Dim dateStart As String = date_start & " " & st_shift & ":00"
            Dim sqlString = "SELECT IFNULL(
                         (SELECT strftime('%Y-%m-%d %H:%M:%S', st_time ) 
                         FROM act_ins 
                         WHERE st_time BETWEEN '" & dateStart & "' AND '" & dateTimeCurr & "'
                         AND line_cd = '" & line_cd & "'
                         ORDER BY id ASC
                         LIMIT 1), 
                         strftime('%Y-%m-%d %H:%M:%S', 'now', '+7 hours')
                    ) AS result_time;"
            Console.WriteLine("mas_OEE_getDateTimeStart sqlString====>" & sqlString)
            Dim jsonData3 As String = api.Load_dataSQLite(sqlString)
            Dim dcResultdata3 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(jsonData3)
            Dim rs As String = ""
            For Each items As Object In dcResultdata3
                rs = items("result_time").ToString()
            Next
            load_show_OEE.Close()
            Return rs
            Console.WriteLine("jsonData3 =====>" & jsonData3)
        Catch ex As Exception
            ' GoTo ReConnect
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    load_show_OEE.Show()
                Else
                    load_show.Show()
                End If
            Catch ex2 As Exception
                load_show_OEE.Close()
                load_show.Show()
            End Try
        End Try
    End Function
    Public Shared Async Function mas_OEE_getDataGetWorkingTimeModel(st_shift As String, line_cd As String, item_cd As String) As Task(Of Dictionary(Of String, Object))
        Try
            Dim api = New api()
            Dim date_now_date As Date = DateTime.Now
            Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
            Dim date_st As String = DateTime.Now.ToString("yyyy-MM-dd")
            ' Adjust date_st for times between midnight and 8 AM
            If time_now >= "00:00:00 AM" AndAlso time_now <= "08:00:00 AM" Then
                date_st = date_now_date.AddDays(-1).ToString("yyyy-MM-dd")
            End If
            ' Format date and time strings
            Dim date_start As String = $"{date_st} {st_shift}:00"
            Dim date_end As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

            ' SQL query
            Dim sqlString As String = "SELECT item_cd, 
                               strftime('%Y-%m-%d %H:%M:%S', min(updated_date)) AS st_time, 
                               seq_no, 
                               wi_plan, 
                               pwi_id
                        FROM act_ins
                        WHERE st_time BETWEEN '{date_start}' AND '{date_end}'
                        AND line_cd = '{line_cd}'
                        GROUP BY item_cd, seq_no, wi_plan, pwi_id
                        ORDER BY st_time DESC;"
            Console.WriteLine("mas_OEE_getDataGetWorkingTimeModel sqlString===>" & sqlString)

            Try
                Dim jsonData3 As String = api.Load_dataSQLite(sqlString)
                Dim dcResultdata3 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(jsonData3)

                ' Parse the results
                Dim result As New List(Of Dictionary(Of String, Object))()
                For Each items As Object In dcResultdata3
                    Dim row As New Dictionary(Of String, Object)()
                    row("item_cd") = items("item_cd")
                    row("st_time") = items("st_time")
                    row("seq_no") = items("seq_no")
                    row("wi_plan") = items("wi_plan")
                    row("pwi_id") = items("pwi_id")
                    result.Add(row)
                Next

                ' Initialize default values
                Dim dateTimeModel As String = date_end
                Dim status As String = "0"
                Dim IsOnlyone As String = "0"
                Dim tmpItem_cd As String = "Switch_Model"

                ' Evaluate the result
                For Each item As Dictionary(Of String, Object) In result
                    If item("item_cd").ToString() = item_cd Then
                        tmpItem_cd = item("item_cd").ToString()
                        dateTimeModel = item("st_time").ToString()
                        IsOnlyone = "1"
                    Else
                        IsOnlyone = "0"
                        Exit For
                    End If
                Next

                ' Determine status
                status = If(tmpItem_cd = "Switch_Model", "1", "2")
                load_show_OEE.Close()
                ' Return result dictionary
                Return New Dictionary(Of String, Object) From {
                    {"rs", dateTimeModel},
                    {"status", status},
                    {"item_cd", tmpItem_cd},
                    {"IsOnlyone", IsOnlyone}
                }
            Catch ex As Exception
                load_show_OEE.Close()
                ' Return fallback result in case of error
                Return New Dictionary(Of String, Object) From {
                    {"rs", date_end},
                    {"status", "0"},
                    {"item_cd", "New_Model"},
                    {"IsOnlyone", "1"}
                }
            End Try
        Catch ex As Exception
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    load_show_OEE.Show()
                Else
                    load_show.Show()
                End If
            Catch ex2 As Exception
                load_show_OEE.Close()
                load_show.Show()
            End Try
        End Try
    End Function
    Public Shared Function mas_OEE_getDataProductionActual(st_shift As String, line_cd As String, item_cd As String)
        Try
            Dim prd_end_date
            Try
                Dim api = New api()
                Dim date_now_date As Date = DateTime.Now.ToString("yyyy-MM-dd")
                ' Dim time As Date = TimeOfDay.ToString("HH:mm:ss") 'DateTime.Now.ToString("HH:mm:ss")
                Dim time As String = DateTime.Now.ToString("HH:mm:ss")
                Dim date_st = DateTime.Now.ToString("yyyy-MM-dd")
                Dim date_end = DateTime.Now.ToString("yyyy-MM-dd")
                Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
                If time_now >= "00:00:00 AM" And time_now <= "08:00:00 AM" Then
                    date_st = date_now_date.AddDays(-1)
                End If
                date_st = date_st
                Dim convertDateStart = Convert.ToDateTime(date_st).ToString("yyyy-MM-dd")
                Dim dates_start = convertDateStart
                Dim date_start = dates_start & " " & st_shift & ":00"
                Dim dateTimeCurr = date_end & " " & time
                Dim result = "Select top 1 prd_end_date as prd_end_date  from production_actual where line_cd =  '" & line_cd & "' and prd_st_date between  '" & date_start & "' and  '" & dateTimeCurr & "' order by seq_no desc"
                If result IsNot Nothing AndAlso result.recordset IsNot Nothing AndAlso result.recordset.Count > 0 Then
                    Dim dateValue As DateTime = DateTime.Parse(result.recordset(0).prd_end_date)
                    Dim formattedDate As String = dateValue.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")
                    prd_end_date = formattedDate
                Else
                    Dim today As DateTime = DateTime.Now
                    Dim formattedToday As String = today.ToString("yyyy-MM-dd HH:mm") & ":00"
                    prd_end_date = formattedToday
                End If
                ' Dim data As New With {
                '  .prd_end_date = prd_end_date
                '  }
                'Dim jsonResponse As String = Newtonsoft.Json.JsonConvert.SerializeObject(data)
                load_show_OEE.Close()
                Return prd_end_date
            Catch ex As Exception
                Dim today As DateTime = DateTime.Now
                Dim formattedToday As String = today.ToString("yyyy-MM-dd HH:mm") & ":00"
                prd_end_date = formattedToday
                '   Dim data As New With {
                '    .prd_end_date = prd_end_date
                '    }
                '    Dim jsonResponse As String = Newtonsoft.Json.JsonConvert.SerializeObject(Data)
                load_show_OEE.Close()
                Return prd_end_date
            End Try
        Catch ex As Exception
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    load_show_OEE.Show()
                Else
                    load_show.Show()
                End If
            Catch ex2 As Exception
                load_show_OEE.Close()
                load_show.Show()
            End Try
        End Try
    End Function
    Public Shared Function mas_OEE_GetLossByHouseP1(line_cd As String)
        Try
            Dim api = New api()
            Dim rs As Integer = 0
            load_show_OEE.Close()
            Return rs
        Catch ex As Exception
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    load_show_OEE.Show()
                Else
                    load_show.Show()
                End If
            Catch ex2 As Exception
                load_show_OEE.Close()
                load_show.Show()
            End Try
        End Try
    End Function
    Public Shared Function mas_GetWorkingTime(line_cd As String, Timeshift As String)
        Try
            Dim api = New api()
            Dim date_now_date As Date = DateTime.Now.ToString("yyyy-MM-dd")
            ' Dim time As Date = TimeOfDay.ToString("HH:mm:ss") 'DateTime.Now.ToString("HH:mm:ss")
            Dim time As String = DateTime.Now.ToString("HH:mm:ss")
            Dim date_st = DateTime.Now.ToString("yyyy-MM-dd")
            Dim date_end = DateTime.Now.ToString("yyyy-MM-dd")
            Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
            If time_now >= "00:00:00 AM" And time_now <= "08:00:00 AM" Then
                date_st = date_now_date.AddDays(-1)
            End If
            Dim convertDateStart = Convert.ToDateTime(date_st).ToString("yyyy-MM-dd")
            Dim date_start As String = convertDateStart & " " & Timeshift & ":00"
            Dim dateTimeCurr As String = date_end & " " & time
            Dim rs As Double = 0
            Dim sql = "SELECT SUM(loss_time) / 60 AS loss_hour  FROM loss_actual  WHERE start_loss BETWEEN '" & date_start & "' AND '" & dateTimeCurr & "'  AND flg_control = '1'  AND line_cd = '" & line_cd & "' LIMIT 1;"
            Dim jsonData3 As String = api.Load_dataSQLite(sql)
            Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(jsonData3)
            Dim loss_hour As Integer = 0
            For Each item As Object In dict2
                loss_hour = item("loss_hour")
                Dim productionTime = DateDiff(DateInterval.Second, DateTime.Parse(date_start), DateTime.Parse(dateTimeCurr))
                Dim productionTimemin = productionTime / 60
                Dim productionTimehour = productionTimemin / 60
                rs = productionTimehour - loss_hour
            Next
            Dim Data As New With {
        .rs = rs.ToString()
    }
            load_show_OEE.Close()
            Return rs
        Catch ex As Exception
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    load_show_OEE.Show()
                Else
                    load_show.Show()
                End If
            Catch ex2 As Exception
                load_show_OEE.Close()
                load_show.Show()
            End Try
        End Try
    End Function
    Public Shared Function mas_OEE_getProduction_actual_detailByShift(line_cd As String, shift As String)
        Try
            Dim api = New api()
            Dim date_now_date As Date = DateTime.Now.ToString("yyyy-MM-dd")
            ' Dim time As Date = TimeOfDay.ToString("HH:mm:ss") 'DateTime.Now.ToString("HH:mm:ss")
            Dim time As String = DateTime.Now.ToString("HH:mm:ss")
            Dim date_st = DateTime.Now.ToString("yyyy-MM-dd")
            Dim date_end = DateTime.Now.ToString("yyyy-MM-dd")
            Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
            If time_now >= "00:00:00 AM" And time_now <= "08:00:00 AM" Then
                date_st = date_now_date.AddDays(-1)
            Else
                ' If time_now > "20:00:00 AM" Then
                If shift = "S" Or shift = "Q" Or shift = "B" Then
                    date_end = date_now_date.AddDays(1)
                    date_end = Convert.ToDateTime(date_end).ToString("yyyy-MM-dd")
                End If
            End If
            date_st = date_st
            Dim dateTimeend As Date
            dateTimeend = date_end & " " & time
            Dim newDateMinutes As DateTime = dateTimeend.AddMinutes(-60)
            Dim convertDateStart = Convert.ToDateTime(date_st).ToString("yyyy-MM-dd")
            Dim convertDateCrr = Convert.ToDateTime(date_now_date).ToString("yyyy-MM-dd")
            Dim convertDateEnd = Convert.ToDateTime(date_end).ToString("yyyy-MM-dd")
            Dim convertnewDateMinutes = Convert.ToDateTime(newDateMinutes).ToString("yyyy-MM-dd HH:mm:ss")
            '  MsgBox("convertDateCrr======>" & convertDateCrr)
            '  MsgBox("convertnewDateMinutes======>" & convertnewDateMinutes)
            Dim st_time = Prd_detail.Label12.Text.Substring(3, 5) & ":00"
            Dim end_time = Prd_detail.Label12.Text.Substring(11, 5) & ":00"
            Dim convertdateTimestart = convertDateStart + " " + st_time
            Dim convertdateTimeend = convertDateEnd + " " + end_time
            If shift = "S" Then ' Shift S
                Dim date2 As DateTime = DateTime.Parse(date_end)
                date2 = date2.AddDays(1)
                ' Format the date as YYYY-MM-DD
                convertdateTimeend = DateTime.Now.ToString("yyyy-MM-dd") & end_time
            End If
            Dim sql = "SELECT COALESCE(SUM(qty), 0) AS TotalActualDetail
                     FROM act_ins
                     WHERE line_cd = '" & line_cd & "' AND st_time BETWEEN '" & convertdateTimestart & "' AND '" & convertdateTimeend & "'"
            Dim jsonString = api.Load_dataSQLite(sql)
            Dim dict3 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(jsonString)
            Dim rs As Integer
            For Each item As Object In dict3
                rs = item("TotalActualDetail").ToString
            Next
            ' Access the value
            load_show_OEE.Close()
            Return rs
        Catch ex As Exception
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    load_show_OEE.Show()
                Else
                    load_show.Show()
                End If
            Catch ex2 As Exception
                load_show_OEE.Close()
                load_show.Show()
            End Try
        End Try
    End Function
    Public Shared Function mas_getProduction_actual_detailByHour(line_cd As String, minSwitchModel As Integer, start_date As String, partNo As String, special_line As String)
        ' Try
        Dim api = New api()
            Dim date_now_date As Date = DateTime.Now.ToString("yyyy-MM-dd")
            ' Dim time As Date = TimeOfDay.ToString("HH:mm:ss") 'DateTime.Now.ToString("HH:mm:ss")
            Dim time As String = DateTime.Now.ToString("HH:mm:ss")
            Dim date_st = DateTime.Now.ToString("yyyy-MM-dd")
            Dim date_end = DateTime.Now.ToString("yyyy-MM-dd")
            Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
            If time_now >= "00:00:00 AM" And time_now <= "08:00:00 AM" Then
                date_st = date_now_date.AddDays(-1)
            End If
            date_st = date_st
            Dim dateTimeend As Date
            dateTimeend = date_end & " " & time
            '  Dim newDateMinutes As DateTime = dateTimeend.AddMinutes(-60)
            Dim newDateMinutes As DateTime = start_date 'dateTimeend.AddMinutes(-minSwitchModel)
            'MsgBox("start Date ======>" & start_date)
            Dim convertDateStart = Convert.ToDateTime(date_st).ToString("yyyy-MM-dd")
            Dim convertDateCrr = Convert.ToDateTime(date_now_date).ToString("yyyy-MM-dd")
            Dim convertnewDateMinutes = Convert.ToDateTime(newDateMinutes).ToString("yyyy-MM-dd HH:mm:ss")
            Dim datebefores As String = convertnewDateMinutes.Split(" "c)(0)
            Dim timebefores As String = convertnewDateMinutes.Split(" "c)(1)
            Dim date_crr = convertDateCrr
        Dim dateTimeString As String
        Dim time_crr As String = DateTime.Now.ToString("HH:mm:ss")
        If time_crr >= "09:00:00" Then
            ' Morning shift after 09:00
            Dim hours As String = timebefores.Split(":"c)(0)
            Dim minutes As String = timebefores.Split(":"c)(1)
            Dim timeWithoutSeconds As String = $"{hours}:{minutes}"
            dateTimeString = $"{datebefores} {timeWithoutSeconds}:00"
        ElseIf time_crr >= "00:00:00" AndAlso time_crr < "08:00:00" Then
            ' Early morning shift
            Dim hours As String = timebefores.Split(":"c)(0)
            Dim minutes As String = timebefores.Split(":"c)(1)
            Dim timeWithoutSeconds As String = $"{hours}:{minutes}"
            dateTimeString = $"{datebefores} {timeWithoutSeconds}:00"
        Else
            ' Morning shift before 09:00
            dateTimeString = $"{date_crr} 08:00:00"
            End If
            Dim sqlRsgetBreak = ""
            Dim dateCrr = convertDateCrr & " " & time_now
            Dim sqlGetDataByHore As String = ""
        Dim TotalByHour As String = ""

        Dim dtConvertCrr As DateTime
        Try
            dtConvertCrr = DateTime.ParseExact(dateCrr, "yyyy-MM-dd hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture)
        Catch ex As Exception
            dtConvertCrr = dateCrr
        End Try
        Dim rsdtConvertCrr = dtConvertCrr.ToString("yyyy-MM-dd HH:mm:ss")
        If special_line = 0 Then
            sqlGetDataByHore = "SELECT COALESCE(SUM(qty), 0) AS TotalByHour FROM act_ins WHERE line_cd = '" & line_cd & "' AND st_time BETWEEN '" & dateTimeString & "' AND '" & rsdtConvertCrr & "'"
        ElseIf special_line = 1 Then
            sqlGetDataByHore = "SELECT COALESCE(SUM(qty), 0) AS TotalByHour FROM act_ins WHERE line_cd = '" & line_cd & "' AND st_time BETWEEN  '" & dateTimeString & "' AND '" & rsdtConvertCrr & "' AND item_cd = '" & partNo & "'"
        ElseIf special_line = 2 Then 'for K1m083
            Dim dateTimeString_special2
            If time_crr >= "00:00:00" AndAlso time_crr < "08:00:00" Then
                Dim hours As String = timebefores.Split(":"c)(0)
                Dim minutes As String = timebefores.Split(":"c)(1)
                Dim timeWithoutSeconds As String = $"{hours}:{minutes}"
                ' แก้จุดสำคัญตรงนี้
                Dim date_st_special As String = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd")
                ' ประกอบ datetime string
                dateTimeString_special2 = $"{date_st_special} {timeWithoutSeconds}:00"
                Console.WriteLine("Special datetime string: " & dateTimeString)
            Else ' Normal 
                dateTimeString_special2 = dateTimeString
            End If
            sqlGetDataByHore = "SELECT COALESCE(SUM(qty), 0) / 5 AS TotalByHour FROM act_ins WHERE line_cd = '" & line_cd & "' AND st_time BETWEEN '" & dateTimeString_special2 & "' AND '" & rsdtConvertCrr & "'"
        End If
            Console.WriteLine("sqlGetDataByHore =====>" & sqlGetDataByHore)
        Dim jsonData4 As String = api.Load_dataSQLite(sqlGetDataByHore)
            Dim dict3 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(jsonData4)
            For Each item As Object In dict3
            TotalByHour = item("TotalByHour")
            Console.WriteLine("actual P TotalByHour===>" & TotalByHour)
        Next
            Return TotalByHour
            'Catch ex As Exception
        '    Try
        '    If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
        '    load_show_OEE.Show()
        '    Else
        '    load_show.Show()
        '    End If
        'Catch ex2 As Exception
        '    load_show_OEE.Close()
        '    load_show.Show()
        'End Try
        ' End Try
    End Function
    Public Function mas_getDataTargetHrTimeLine(line_cd As String, shift As String, special_line As String)
        If special_line = "2" Then
            If shift = "A" Or shift = "P" Then

            End If
        End If

    End Function
End Class