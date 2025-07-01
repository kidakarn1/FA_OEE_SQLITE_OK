Public Class manageVariable
    Public Shared sPart As String = ""
    Public Shared Sub setSelectpartdefect(data)
        MsgBox("set data - =" & data)
        sPart = data
    End Sub
    Public Shared Function getSelectpartdefect()
        MsgBox("spar = " & sPart)
        Return sPart
    End Function
End Class
