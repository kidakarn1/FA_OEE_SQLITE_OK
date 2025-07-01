Imports System.Data.SqlClient
Public Class Controller
	Public myConnection As New SqlConnection  'ตัวแปรสำหรับติดต่อฐานข้อมูล
	Public sConnect As String 'ตัวแปรสำหรับคำสั่งต่อฐานข้อมูล
	Public sSql As String 'ตัวแปรคำสั่ง sql
	Public Emp As List_Emp
	Public Scan As Sc

	Public Sub ConnectDB() ' ฟังชั่นก์ไว้สำหรับติดต่อฐานข้อมูลเมื่อต้องการใช้งาน
        Try
            'sConnect = "Server=192.168.1.34 Catalog=Datadase;User ID=sa;Password=Password;"
            sConnect = "Server=" & Backoffice_model.svDatabase & "\PCSDBSV;Initial Catalog=test_new_fa01;User ID=sa;Password=Te@m1nw;"
            myConnection.ConnectionString = sConnect ' คำสั่งติดต่อฐานข้อมูล
            myConnection.Open()
            'MsgBox("Database connect successfully")
        Catch ex As Exception
            MsgBox("Database connect failed. Please contact PC System")
			myConnection.Close()
			Application.Exit()
		End Try
	End Sub
End Class
