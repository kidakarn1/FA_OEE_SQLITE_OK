Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Windows.Form
Imports TBK_FA_System
<TestClass()> Public Class UnitTest1
    <TestMethod()> Public Sub TestMethod1()
        Try
            '  Dim md = New modelDefect()
            '  Dim OEE_LOCAL = New OEE_SQLITE()
            '  Dim st_shift As String = "08:00:00"
            '  Dim end_shift As String = "20:00:00"
            '  Dim line_cd As String = "K1A006"
            '  Dim dateTimeswmodel As String = "2024-12-11 08:00:00"
            '  Dim statusSwitchModel As String = "0"
            '  Dim IsOnlyone As String = "1"
            '  Dim SpecFlgLine As String = "0"
            '  Dim rs = OEE_LOCAL.mas_GetDataProgressbarA(st_shift, end_shift, line_cd, dateTimeswmodel, statusSwitchModel, IsOnlyone, SpecFlgLine)
            '  Assert.AreNotEqual("0", rs)
        Catch ex As Exception
            Console.WriteLine("error function mas_GetDataProgressbarA File OEE_SQLITE")
        End Try
    End Sub
End Class