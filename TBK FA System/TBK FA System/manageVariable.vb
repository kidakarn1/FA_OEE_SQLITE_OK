Public Class manageVariable
    Public Shared sPart As String = ""
    Public Shared Sub setSelectpartdefect(data)
        'msgBox("set data - =" & data)
        sPart = data
    End Sub
    Public Shared Function getSelectpartdefect()
        'msgBox("spar = " & sPart)
        Return sPart
    End Function
End Class
