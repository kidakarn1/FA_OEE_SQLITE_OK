Imports System.IO
Imports System.Net

Public Class SetingMaching

    ' ปุ่ม Back
    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim line_cd = MainFrm.Label4.Text
        Dim start_loss = StartLoss.Text
        Dim end_loss = EndLoss.Text
        Dim created_by = EmpCodeLeader.Text
        Dim updated_by = EmpCodeLeader.Text
        Dim status_flg = 2
        ' sentParameterLossIO(False)
        Backoffice_model.updated_info_loss_setting_machine(line_cd, start_loss, end_loss, created_by, updated_by, Working_Pro.pwi_id, status_flg, LossCD.Text)
        Me.Close()
    End Sub

    ' ปุ่ม Save (ตอนนี้ยังไม่ได้เขียน Logic)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ' TODO: Save logic here
        Backoffice_model.statusActionSetingMachine = 1 ' Action Confrime Step 2 
        CheckSetingMachine.ShowDialog()
    End Sub

    ' เมื่อฟอร์มโหลด
    Private Sub SetingMaching_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' อ่านรหัสพนักงานจากคอนโทรลบนฟอร์ม (เช่น Label/TextBox ชื่อ EmpCodeLeader)
        Dim empCode As String = EmpCodeLeader.Text.Trim()

        ' ถ้าไม่มีรหัส → ใช้ no_user ไปเลย
        If String.IsNullOrWhiteSpace(empCode) Then
            empCode = "no_user"
        End If

        ' โหลดรูปเข้า PictureBox pcLeader
        LoadEmployeeImage(empCode, pcLeader)

    End Sub

    ' -------------------------------------------------------------
    ' Helper: โหลดรูปจาก URL → Return Image
    ' -------------------------------------------------------------
    Private Function DownloadImageFromUrl(url As String) As Image
        Using wc As New WebClient()
            Dim data As Byte() = wc.DownloadData(url)
            Using ms As New MemoryStream(data)
                Return Image.FromStream(ms)
            End Using
        End Using
    End Function

    ' -------------------------------------------------------------
    ' Main: โหลดรูป user → ถ้าไม่ได้ให้โหลด no_user แทน
    ' -------------------------------------------------------------
    Public Sub LoadEmployeeImage(emp_cd As String, targetPic As PictureBox)

        Dim baseUrl As String = "http://" & Backoffice_model.svApi &
                                "/tbkk_shopfloor_sys/asset/img_emp/"

        Dim userUrl As String = baseUrl & emp_cd & ".jpg"
        Dim fallbackUrl As String = baseUrl & "no_user.jpg"

        ' ให้รูปยืดเต็มกรอบ PictureBox
        targetPic.SizeMode = PictureBoxSizeMode.StretchImage

        Dim finalImage As Image = Nothing

        ' 1) พยายามโหลดรูป user ก่อน
        Try
            finalImage = DownloadImageFromUrl(userUrl)

        Catch ex As Exception
            ' 2) ถ้าโหลด user ไม่ได้ → ลองโหลด no_user แทน
            Try
                finalImage = DownloadImageFromUrl(fallbackUrl)
            Catch ex2 As Exception
                MessageBox.Show("ไม่สามารถโหลดรูป user และ no_user ได้: " & ex2.Message,
                                "Image Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
                Exit Sub
            End Try
        End Try

        ' 3) ถ้ามีรูปเก่าอยู่แล้ว ให้ Dispose ทิ้งเพื่อกัน Memory Leak
        If targetPic.Image IsNot Nothing Then
            targetPic.Image.Dispose()
        End If

        ' 4) เซตรูปใหม่เข้ากรอบ
        targetPic.Image = finalImage

    End Sub

End Class
