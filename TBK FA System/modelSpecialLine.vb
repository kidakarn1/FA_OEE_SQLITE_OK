Public Class modelSpecialLine
    Public Shared Function LoadDataLable(wi As String)
        Try
            Dim api = New api()
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/apiShopfloor/index.php/getDatadefect/getDefectPartNo?dfWi=" & dtWino & "&dfSeq=" & dtSeq & "&dfLot=" & dtLot & "&dfType=" & Type & "&PartNo=" & PartNo)
            Dim rsData As String = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/GET_DATA_NEW_FA/GetLabel?wi=" & wi)
            If rsData <> "0" Then
                Return rsData
            Else
                Return 0
            End If
        Catch ex As Exception
            MsgBox("connect Api Faill Please check modelSpecialLine in Function LoadDataLable = " & ex.Message)
            Return False
        End Try
    End Function
End Class
