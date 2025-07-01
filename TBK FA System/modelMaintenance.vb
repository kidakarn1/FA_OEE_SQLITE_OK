Imports System.Data.SqlClient
Imports System.Data.SQLite
    Imports System.Globalization
    Imports System.Data
    Imports Newtonsoft.Json.Linq
    Public Class modelMaintenance
        Public Shared bf = New Backoffice_model()
    Public Shared Function getDataMN(line_cd As String)
        Try
            Dim api = New api()
            Dim rsData = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_maintenance/getDataMN?line_cd=" & line_cd)
            Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_maintenance/getDataMN?line_cd=" & line_cd)
            Return rsData
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelMaintenance in Function getDataMN = " & ex.Message)
            Return 0
        End Try
    End Function
End Class
