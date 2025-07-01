Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.Globalization
Imports System.Data
Imports System.Web.Script.Serialization
Public Class OEE_NODE
    Public Shared Function OEE_LOAD_MSTOEE(line_cd As String)
        Try
            Dim api = New api()
            'Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataGetmstOEE?line_cd=" & line_cd)
            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataGetmstOEE?line_cd=" & line_cd)
            Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(jsonString)
            Dim i As Integer = 1
            load_show_OEE.Close()
            Return dcResultdata
        Catch ex As Exception
            MsgBox("Please Check Master OEE In Table line_mst")
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
    Public Shared Function OEE_LOAD_MSTOEEColor(line_cd As String)
        Try
            Dim api = New api()
            Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataGetOEEColor?line_cd=" & line_cd)
            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataGetOEEColor?line_cd=" & line_cd)
            Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(jsonString)
            Dim i As Integer = 1
            load_show_OEE.Close()
            Return dcResultdata
        Catch ex As Exception
            MsgBox("Please Check Function : OEE_LOAD_MSTOEEColor Or Check Master OEE In Table line_mst")
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
    Public Shared Function OEE_GET_NEW_TARGET(st_shift As String, end_shift As String, std_ct As String, shift As String)
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
                If shift = "S" Or shift = "Q" Or shift = "B" Then
                    date_end = date_now_date.AddDays(1)
                    date_end = Convert.ToDateTime(date_end).ToString("yyyy-MM-dd")
                End If
                '   If shift = "S" Or shift = "Q" Then
                ' date_end = date_now_date.AddDays(1)
                ' date_end = Convert.ToDateTime(date_end).ToString("yyyy-MM-dd")
                'End If
            End If
            date_st = date_st

            Dim convertDateStart = Convert.ToDateTime(date_st).ToString("yyyy-MM-dd")
            ' Dim TarGet = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/NEW_GET_TARGET?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct)
            '   'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/NEW_GET_TARGET?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct)
            'Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataGettarget?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct & "&date_start=" & convertDateStart & "&date_end=" & date_end)
            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataGettarget?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct & "&date_start=" & convertDateStart & "&date_end=" & date_end)
            Dim jsSerializer As New JavaScriptSerializer()
            ' Deserialize the JSON string to a Dictionary
            Dim data As Dictionary(Of String, Object) = jsSerializer.Deserialize(Of Dictionary(Of String, Object))(jsonString)
            ' Access the value
            Dim TarGet As Integer = data("Target").ToString
            ''Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/NEW_GET_TARGET?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct)
            load_show_OEE.Close()
            Return TarGet
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
    Public Shared Function OEE_GET_NEW_TARGET_PERCEN(st_shift As String, end_shift As String, std_ct As String, shift As String, line_cd As String)
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
                If shift = "S" Or shift = "Q" Or shift = "B" Then
                    date_end = date_now_date.AddDays(1)
                    date_end = Convert.ToDateTime(date_end).ToString("yyyy-MM-dd")
                End If
                '   If shift = "S" Or shift = "Q" Then
                ' date_end = date_now_date.AddDays(1)
                ' date_end = Convert.ToDateTime(date_end).ToString("yyyy-MM-dd")
                'End If
            End If
            date_st = date_st
            Dim convertDateStart = Convert.ToDateTime(date_st).ToString("yyyy-MM-dd")
            ' Dim TarGet = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/NEW_GET_TARGET?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct)
            '   'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/NEW_GET_TARGET?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct)
            Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataGettargetPercen?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct & "&date_start=" & convertDateStart & "&date_end=" & date_end & "&line_cd=" & line_cd)
            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataGettargetPercen?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct & "&date_start=" & convertDateStart & "&date_end=" & date_end & "&line_cd=" & line_cd)
            Dim jsSerializer As New JavaScriptSerializer()
            ' Deserialize the JSON string to a Dictionary
            Dim data As Dictionary(Of String, Object) = jsSerializer.Deserialize(Of Dictionary(Of String, Object))(jsonString)
            ' Access the value
            Dim TarGet As Integer = data("Target").ToString
            ''Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/NEW_GET_TARGET?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct)
            load_show_OEE.Close()
            Return TarGet
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
    Public Shared Function OEE_GET_Hour(shift As String)
        Try
            Dim api = New api()
            ' Dim TarGet = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/NEW_GET_TARGET?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct)
            '   'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/NEW_GET_TARGET?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct)
            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataGetHour?shift=" & shift)
            Dim jsSerializer As New JavaScriptSerializer()
            ' Deserialize the JSON string to a Dictionary
            Dim data As Dictionary(Of String, Object) = jsSerializer.Deserialize(Of Dictionary(Of String, Object))(jsonString)
            ' Access the value
            Dim TarGet As Double = data("WorkHour").ToString
            'Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataGetHour?shift=" & shift)
            ''Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/NEW_GET_TARGET?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct)
            load_show_OEE.Close()
            Return TarGet
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
    Public Shared Function OEE_getProduction_actual_detailByHour(line_cd As String, minSwitchModel As Integer, start_date As String, partNo As String)
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
            Dim dateTimeend As Date
            dateTimeend = date_end & " " & time
            '  Dim newDateMinutes As DateTime = dateTimeend.AddMinutes(-60)
            Dim newDateMinutes As DateTime = start_date 'dateTimeend.AddMinutes(-minSwitchModel)
            'MsgBox("start Date ======>" & start_date)
            Dim convertDateStart = Convert.ToDateTime(date_st).ToString("yyyy-MM-dd")
            Dim convertDateCrr = Convert.ToDateTime(date_now_date).ToString("yyyy-MM-dd")
            Dim convertnewDateMinutes = Convert.ToDateTime(newDateMinutes).ToString("yyyy-MM-dd HH:mm:ss")
            ' MsgBox("convertDateStart======>" & convertDateStart)
            '  MsgBox("convertnewDateMinutes======>" & convertnewDateMinutes)
            'Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataDetailByHouse?line_cd=" & line_cd & "&date_crr=" & convertDateCrr & "&time_crr=" & time & "&convertnewDateMinutes=" & convertnewDateMinutes & "&partNo=" & partNo)
            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataDetailByHouse?line_cd=" & line_cd & "&date_crr=" & convertDateCrr & "&time_crr=" & time & "&convertnewDateMinutes=" & convertnewDateMinutes & "&partNo=" & partNo)
            Dim jsSerializer As New JavaScriptSerializer()
            ' Deserialize the JSON string to a Dictionary
            Dim data As Dictionary(Of String, Object) = jsSerializer.Deserialize(Of Dictionary(Of String, Object))(jsonString)
            ' Access the value
            Dim rs As Integer = data("TotalByHour").ToString
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
    Public Shared Function OEE_getProduction_actual_detailByShift(line_cd As String, shift As String)
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
            'Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataDetailByShift?line_cd=" & line_cd & "&date_start=" & convertDateStart & "&date_end=" & convertDateEnd & "&st_shift=" & st_time & "&end_shift=" & end_time)
            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataDetailByShift?line_cd=" & line_cd & "&date_start=" & convertDateStart & "&date_end=" & convertDateEnd & "&st_shift=" & st_time & "&end_shift=" & end_time)
            Dim jsSerializer As New JavaScriptSerializer()
            ' Deserialize the JSON string to a Dictionary
            Dim data As Dictionary(Of String, Object) = jsSerializer.Deserialize(Of Dictionary(Of String, Object))(jsonString)
            ' Access the value
            Dim rs As Integer = data("TotalActualDetail").ToString
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
    Public Shared Function GetDataProgressbarA(st_shift As String, end_shift As String, line_cd As String, dateTimeswmodel As String, statusSwitchModel As String, IsOnlyone As String)
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
            Dim dateTimeend As Date
            dateTimeend = date_end & " " & time
            Dim convertDateStart = Convert.ToDateTime(date_st).ToString("yyyy-MM-dd")
            Dim convertDateCrr = Convert.ToDateTime(date_now_date).ToString("yyyy-MM-dd")
            'Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataProgressA?st_shift=" & st_shift & "&end_shift=" & end_shift & "&line_cd=" & line_cd & "&date_start=" & convertDateStart & "&date_Crr=" & convertDateCrr & "&TimeCrr=" & time & "&dateTimeswmodel=" & dateTimeswmodel & "&statusSwitchModel=" & statusSwitchModel & "&IsOnlyone=" & IsOnlyone)
            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataProgressA?st_shift=" & st_shift & "&end_shift=" & end_shift & "&line_cd=" & line_cd & "&date_start=" & convertDateStart & "&date_Crr=" & convertDateCrr & "&TimeCrr=" & time & "&dateTimeswmodel=" & dateTimeswmodel & "&statusSwitchModel=" & statusSwitchModel & "&IsOnlyone=" & IsOnlyone)
            Dim jsSerializer As New JavaScriptSerializer()
            ' Deserialize the JSON string to a Dictionary
            Dim data As Dictionary(Of String, Object) = jsSerializer.Deserialize(Of Dictionary(Of String, Object))(jsonString)
            ' Access the value
            Dim rs As Integer = data("PercentA").ToString
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
    Public Shared Function OEE_GetLossByHouseP1(line_cd As String)
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
            Dim dateTimeend As Date
            dateTimeend = date_end & " " & time
            Dim newDateMinutes As DateTime = dateTimeend.AddMinutes(-60)
            Dim convertDateStart = Convert.ToDateTime(date_st).ToString("yyyy-MM-dd")
            Dim convertnewDateMinutes = Convert.ToDateTime(newDateMinutes).ToString("yyyy-MM-dd HH:mm:ss")

            ' MsgBox("convertnewDateMinutes ===>" & convertnewDateMinutes)
            'Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataGetlossbyhouse?line_cd=" & line_cd & "&date_start=" & date_st & "&date_end=" & date_end & "&time_crr=" & time & "&convertnewDateMinutes=" & convertnewDateMinutes)
            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataGetlossbyhouse?line_cd=" & line_cd & "&date_start=" & date_st & "&date_end=" & date_end & "&time_crr=" & time & "&convertnewDateMinutes=" & convertnewDateMinutes)
            ' Deserialize the JSON string to a Dictionary
            Dim jsSerializer As New JavaScriptSerializer()
            Dim data As List(Of Dictionary(Of String, Object)) = jsSerializer.Deserialize(Of List(Of Dictionary(Of String, Object)))(jsonString)
            ' Access the integer value from the first dictionary in the list
            Dim targetValue As Integer = Convert.ToInt32(data(0)("TotalLoss"))
            ' Retrieve the integer value from the dictionary
            ' Access the value
            Dim rs As Integer = targetValue
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
    Public Shared Function OEE_GET_Data_LOSS(line_cd As String, lot_no As String, shift As String, dateStart As String, dateTimeswModel As String, statusSwitchModel As String, IsOnlyone As String)
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
            'MsgBox("date_st ===>" & convertDateStart)
            'MsgBox("dateTime_end ===>" & date_end)
            'MsgBox("time = " & time)
            Dim st_time = Prd_detail.Label12.Text.Substring(3, 5) & ":00"
            'MsgBox("File OEE Time ST shift -= " & st_time)
            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataGetDataAvailabillty?line_cd=" & line_cd & "&lot_no=" & lot_no & "&shift=" & shift & "&dateStart=" & convertDateStart & "&dateEnd=" & date_end & "&st_shift=" & st_time & "&end_shift=" & time & "&dateTimeswModel=" & dateTimeswModel & "&statusSwitchModel=" & statusSwitchModel & "&IsOnlyone=" & IsOnlyone)
            Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataGetDataAvailabillty?line_cd=" & line_cd & "&lot_no=" & lot_no & "&shift=" & shift & "&dateStart=" & convertDateStart & "&dateEnd=" & date_end & "&st_shift=" & st_time & "&end_shift=" & time & "&dateTimeswModel=" & dateTimeswModel & "&statusSwitchModel=" & statusSwitchModel & "&IsOnlyone=" & IsOnlyone)
            'Console.WriteLine(jsonString)
            load_show_OEE.Close()
            Return jsonString
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
    Public Shared Function OEE_GET_Data_AccTarget(st_shift As String, std_ct As String)
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

            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/datagetAccTarget?st_shift=" & st_shift & "&std_ct=" & std_ct & "&dateStart=" & convertDateStart & "&dateEnd=" & date_end & "&end_shift=" & time)
            'Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/datagetAccTarget?st_shift=" & st_shift & "&std_ct=" & std_ct & "&dateStart=" & convertDateStart & "&dateEnd=" & date_end & "&end_shift=" & time)
            Dim jsSerializer As New JavaScriptSerializer()
            ' Deserialize the JSON string to a Dictionary
            Dim data As Dictionary(Of String, Object) = jsSerializer.Deserialize(Of Dictionary(Of String, Object))(jsonString)
            ' Access the value
            Dim rs As Integer = data("ActualTarget").ToString
            'Console.WriteLine("http: //192.168.161.78:6100/api/datagetAccTarget?st_shift=" & st_shift & "&std_ct=" & std_ct)
            load_show_OEE.Close()
            Return rs
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
    Public Shared Function OEE_getSpeedLoss(NG As String, Good As String, Timeshift As String, std_cd As String)
        Try
            Dim api = New api()

            Dim jsonString = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/getSpeedLoss?NG=" & NG & "&Good=" & Good & "&Timeshift=" & Timeshift & "&std_cd=" & std_cd)
            Dim jsSerializer As New JavaScriptSerializer()
            ' Deserialize the JSON string to a Dictionary
            Dim data As Dictionary(Of String, Object) = jsSerializer.Deserialize(Of Dictionary(Of String, Object))(jsonString)
            ' Access the value
            Dim rs As Integer = data("ActualTarget")
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/getSpeedLoss?NG=" & NG & "&Good=" & Good & "&Timeshift=" & Timeshift & "&std_cd=" & std_cd)
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
                load_show.Show()
            End Try
        End Try
    End Function
    Public Shared Function OEE_getWorkingTime(line_cd As String, Timeshift As String)
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
            'Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataGetWorkingTime?line_cd=" & line_cd & "&st_shift=" & Timeshift & "&date_crr=" & date_end & "&time_crr=" & time & "&dates_start=" & convertDateStart)
            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataGetWorkingTime?line_cd=" & line_cd & "&st_shift=" & Timeshift & "&date_crr=" & date_end & "&time_crr=" & time & "&dates_start=" & convertDateStart)
            Dim jsSerializer As New JavaScriptSerializer()
            ' Deserialize the JSON string to a Dictionary
            Dim data As Dictionary(Of String, Object) = jsSerializer.Deserialize(Of Dictionary(Of String, Object))(jsonString)
            ' Access the value
            Dim rs As Integer = data("rs").ToString
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
    Public Shared Function OEE_getDateTimeStart(st_shift As String, line_cd As String)
        Try
ReConnect:
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
            'Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataDataTimestart?st_shift=" & st_shift & "&line_cd=" & line_cd & "&date_start=" & convertDateStart & "&dateCurr=" & date_end & "&TimeCurr=" & time)
            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataDataTimestart?st_shift=" & st_shift & "&line_cd=" & line_cd & "&date_start=" & convertDateStart & "&dateCurr=" & date_end & "&TimeCurr=" & time)
            Dim jsSerializer As New JavaScriptSerializer()
            Dim data As Dictionary(Of String, Object) = jsSerializer.Deserialize(Of Dictionary(Of String, Object))(jsonString)
            Dim rs As String = data("formattedDateTime").ToString
            ' MsgBox("OEE_getDateTimeStartn formattedDateTime====>" & rs)
            Return rs
        Catch ex As Exception
            GoTo ReConnect
        End Try
    End Function
    Public Shared Function OEE_getDataGetWorkingTimeModel(st_shift As String, line_cd As String, item_cd As String)
        Try
ReConnect:
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
            'Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataGetWorkingTimeModel?st_shift=" & st_shift & "&line_cd=" & line_cd & "&dates_start=" & convertDateStart & "&date_crr=" & date_end & "&time_crr=" & time & "&item_cd=" & item_cd)
            Dim jsonString = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataGetWorkingTimeModel?st_shift=" & st_shift & "&line_cd=" & line_cd & "&dates_start=" & convertDateStart & "&date_crr=" & date_end & "&time_crr=" & time & "&item_cd=" & item_cd)
            Dim jsSerializer As New JavaScriptSerializer()
            Dim data As Dictionary(Of String, Object) = jsSerializer.Deserialize(Of Dictionary(Of String, Object))(jsonString)
            Dim st_time As String = data("rs").ToString
            Return data
        Catch ex As Exception
            GoTo ReConnect
        End Try
    End Function
    Public Shared Function OEE_getDataProductionActual(st_shift As String, line_cd As String, item_cd As String) As String
        Try
            Dim api = New api()
            Dim date_now_date As Date = DateTime.Now
            Dim time As String = DateTime.Now.ToString("HH:mm:ss")
            Dim date_st As String = DateTime.Now.ToString("yyyy-MM-dd")
            Dim date_end As String = DateTime.Now.ToString("yyyy-MM-dd")
            Dim time_now As DateTime = DateTime.ParseExact(DateTime.Now.ToString("hh:mm:ss tt"), "hh:mm:ss tt", CultureInfo.InvariantCulture)
            Dim time_cutoff As DateTime = DateTime.ParseExact("08:00:00 AM", "hh:mm:ss tt", CultureInfo.InvariantCulture)

            If time_now <= time_cutoff Then
                date_st = date_now_date.AddDays(-1).ToString("yyyy-MM-dd")
            End If

            Console.WriteLine("http://" & Backoffice_model.svOEE & "/api/dataDataProductionActual?st_shift=" & st_shift & "&line_cd=" & line_cd & "&dates_start=" & date_st & "&date_crr=" & date_end & "&time_crr=" & time & "&item_cd=" & item_cd)

            Dim jsonString As String = api.Load_data("http://" & Backoffice_model.svOEE & "/api/dataDataProductionActual?st_shift=" & st_shift & "&line_cd=" & line_cd & "&dates_start=" & date_st & "&date_crr=" & date_end & "&time_crr=" & time & "&item_cd=" & item_cd)

            If String.IsNullOrEmpty(jsonString) Then Return "API ERROR"

            Dim jsSerializer As New JavaScriptSerializer()
            Dim data As Dictionary(Of String, Object)

            Try
                data = jsSerializer.Deserialize(Of Dictionary(Of String, Object))(jsonString)
            Catch ex As Exception
                Return "JSON PARSE ERROR"
            End Try

            If data IsNot Nothing AndAlso data.ContainsKey("prd_end_date") AndAlso Not String.IsNullOrEmpty(data("prd_end_date").ToString()) Then
                Dim prdEndDate As DateTime
                If DateTime.TryParseExact(data("prd_end_date").ToString(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, prdEndDate) Then
                    load_show_OEE.Close()
                    Return prdEndDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                Else
                    Return data("prd_end_date").ToString()
                End If
            Else
                Return "NO DATA"
            End If

        Catch ex As Exception
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    If load_show_OEE IsNot Nothing Then load_show_OEE.Show()
                Else
                    If load_show IsNot Nothing Then load_show.Show()
                End If
            Catch ex2 As Exception
                If load_show_OEE IsNot Nothing Then load_show_OEE.Close()
                If load_show IsNot Nothing Then load_show.Show()
            End Try
            Return "ERROR"
        End Try
    End Function

End Class