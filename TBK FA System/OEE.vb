Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.Globalization
Imports System.Data
Public Class OEE
    Public Shared Function OEE_EXP_CHECK_SUPP(item_cd As String)
        Dim api = New api()
        Dim TarGet = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/EXP_CHECK_SUPP?item_cd=" & item_cd)
        'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/EXP_CHECK_SUPP?item_cd=" & item_cd)
        Return TarGet
    End Function
    Public Shared Function OEE_GET_TARGET(shift As String, WI As String, actual As String)
        Dim api = New api()
        Dim TarGet = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/GET_TARGET?shift=" & shift & "&WI=" & WI & "&actual=" & actual)
        Return TarGet
    End Function
    Public Shared Function OEE_GET_NEW_TARGET(st_shift As String, end_shift As String, std_ct As String)
        Dim api = New api()
        Dim TarGet = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/NEW_GET_TARGET?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct)
        'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/NEW_GET_TARGET?st_shift=" & st_shift & "&end_shift=" & end_shift & "&std_ct=" & std_ct)
        Return TarGet
    End Function
    Public Shared Function OEE_GET_Hour(shift As String)
        Dim api = New api()
        Dim TarGet = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/GET_HOUR?shift=" & shift)
        Return TarGet
    End Function
    Public Shared Function OEE_getProduction_actual_detailByHour(line_cd As String)
        Dim api = New api()
        Dim rs = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/getProduction_actual_detailByHour?line_cd=" & line_cd)
        Return rs
    End Function
    Public Shared Function GetDataProgressbarA(st_shift As String, end_shift As String, line_cd As String)
        Dim api = New api()
        Dim rs = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/progressbarA?st_shift=" & st_shift & "&end_shift=" & end_shift & "&line_cd=" & line_cd)
        'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/progressbarA?st_shift=" & st_shift & "&end_shift=" & end_shift & "&line_cd=" & line_cd)
        Return rs
    End Function


    Public Shared Function OEE_GET_Data_Progressbar(line_cd As String, shift As String)
        Dim api = New api()
        Dim TarGet = api.Load_data("http://192.168.161.219/test_ci/table_taget.php?line_cd=" & line_cd & "&shift=" & shift)
        Return TarGet
    End Function
    Public Shared Function OEE_GetLossByHouseP1(line_cd As String)
        Dim api = New api()
        Dim rs = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/GetLossByHouse?line_cd=" & line_cd)
        Return rs
    End Function
    Public Shared Function OEE_GET_Data_LOSS(line_cd As String, lot_no As String, shift As String, dateStart As String)
        Dim api = New api()
        Dim TarGet = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/GetDataAvailabillty?line_cd=" & line_cd & "&lot_no=" & lot_no & "&shift=" & shift & "&dateStart=" & dateStart)
        'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/GetDataAvailabillty?line_cd=" & line_cd & "&lot_no=" & lot_no & "&shift=" & shift & "&dateStart=" & dateStart)
        Return TarGet
    End Function
    Public Shared Function OEE_GET_Data_AccTarget(st_shift As String, std_ct As String)
        Dim api = New api()
        Dim AccTarGet = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/getAccTarget?st_shift=" & st_shift & "&std_ct=" & std_ct)
        Return AccTarGet
    End Function
    Public Shared Function OEE_getSpeedLoss(NG As String, Good As String, Timeshift As String, std_cd As String)
        Dim api = New api()
        Dim AccTarGet = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/getSpeedLoss?NG=" & NG & "&Good=" & Good & "&Timeshift=" & Timeshift & "&std_cd=" & std_cd)
        'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/getSpeedLoss?NG=" & NG & "&Good=" & Good & "&Timeshift=" & Timeshift & "&std_cd=" & std_cd)
        Return AccTarGet
    End Function
    Public Shared Function OEE_getWorkingTime(line_cd As String, Timeshift As String)
        Dim api = New api()
        Dim rs = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/GetWorkingTime?line_cd=" & line_cd & "&st_shift=" & Timeshift)
        'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/GetWorkingTime?line_cd=" & line_cd & "&st_shift=" & Timeshift)
        Return rs
    End Function
    Public Shared Async Function OEE_getDateTimeStart(st_shift As String, line_cd As String) As Task(Of String)
        Dim api = New api()
        Dim url As String = "http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_OEE/getDateTimeStart?st_shift=" & st_shift & "&line_cd=" & line_cd
        'Console.WriteLine(url)
        ' Assuming Load_data is synchronous, use Task.Run to make it asynchronous
        Dim rs As String = Await Task.Run(Function() api.Load_data(url))
        Return rs
    End Function
End Class
