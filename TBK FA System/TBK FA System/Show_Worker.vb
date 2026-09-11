Imports System.IO
Imports System.Net

Public Class Show_Worker
    Public Shared width1 As Integer = 60
    Public Shared width3 As Integer = 60
    Public Shared height1 As Integer = 0
    Public Shared width2 As Integer = 0
    Public Shared height2 As Integer = 40
    Public Shared height3 As Integer = 50
    Public Shared k As Integer = 1
    Public Shared k2 As Integer = 1
    Dim Plus As Integer = 0
    Dim count_row As Integer = 1
    Public clcikBtnplus As Integer = 0
    Private Sub Show_Worker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadWorker()
    End Sub
    Public Sub loadWorker()
        width1 = 60
        width3 = 60
        height1 = 0
        width2 = 0
        height2 = 40
        height3 = 50
        k = 1
        k2 = 1
        Dim total = List_Emp.ListView1.Items.Count
        lbTotalWorker.Text = total
        For i As Integer = 1 To total ' วนลูป 5 รอบ
            Dim emp_cd As String = List_Emp.ListView1.Items(i - 1).Text '"K0070"
            ' Dim emp_cd As String = "K0070"
            CreateLBEmpCode(i, emp_cd)
            ' CreateLBPicture(i, emp_cd)
        Next i
    End Sub
    Public Sub CreateLBEmpCode(i As String, emp_cd As String)

        Dim label As New Label()
        Dim pictureBox As New PictureBox()
        Dim tclient As New WebClient
        label.Text = "Label " & i
        Dim url As String = ""
        Dim tImage As Bitmap
        Try
            url = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
            tImage = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
        Catch ex As Exception
            url = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg"
            tImage = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
        End Try
        pictureBox.Image = tImage
        If k Mod 5 = 0 Then
            count_row += 1
            label.Location = New Point(55, 60 + (i - 1) * 50) ' กำหนดตำแหน่ง
            pictureBox.Location = New Point(55, 90 + (i - 1) * 50)
            ' width1 = width1 + 60
            height2 = width1 + (i - 1) * 50
            k = 2
            'height1 = height1 +
        Else
            label.Location = New Point(55 + (k - 1) * 150, height2) ' กำหนดตำแหน่ง
            pictureBox.Location = New Point(55 + (k - 1) * 150, (height2 + 30))
            k = k + 1
        End If
        label.Font = New Font("Arial", 18) ' กำหนดตัวอักษรและขนาด
        label.AutoSize = True
        label.Text = emp_cd
        Panel_show_worker.Controls.Add(label)
        pictureBox.Size = New Size(110, 140) ' ขนาดของ PictureBox
        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage ' การปรับขนาดรูปภาพใน PictureBox
        Panel_show_worker.Controls.Add(pictureBox) ' เพิ่ม PictureBox เข้าไปใน Form
    End Sub
    Public Sub CreateLBPicture(i As String, emp_cd As String)
        Dim pictureBox As New PictureBox()
        Dim tclient As New WebClient
        'pictureBox.Image = Image.FromFile("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg") ' เปลี่ยนเส้นทางไปยังไฟล์รูปภาพของคุณ
        Dim url As String = "http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/" & emp_cd & ".jpg"
        Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(url)))
        pictureBox.Image = tImage
        If k2 Mod 5 = 0 Then
            pictureBox.Location = New Point(40, 60 + (i - 1) * 50) ' กำหนดตำแหน่ง
            ' width1 = width1 + 60
            height3 = width3 + (i - 1) * 50
            k2 = 2
            'height1 = height1 +
        Else
            pictureBox.Location = New Point(40 + (k2 - 1) * 150, height3) ' กำหนดตำแหน่ง
            k2 = k2 + 1
        End If
        pictureBox.Size = New Size(110, 140) ' ขนาดของ PictureBox
        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage ' การปรับขนาดรูปภาพใน PictureBox
        Panel_show_worker.Controls.Add(pictureBox) ' เพิ่ม PictureBox เข้าไปใน Form
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

    Private Sub btn_down_Click(sender As Object, e As EventArgs) Handles btn_down.Click
        clcikBtnplus += 1
        If clcikBtnplus <= count_row Then
            Plus += 300
        Else
            Plus = 300 * count_row
        End If
        Panel_show_worker.AutoScrollPosition = New Point(0, Plus)
    End Sub

    Private Sub btn_up_Click(sender As Object, e As EventArgs) Handles btn_up.Click
        clcikBtnplus -= 1
        If clcikBtnplus <= count_row Then
            clcikBtnplus = 0
        End If
        Plus -= 300
        If Plus < 0 Then
            Plus = 0
        End If
        Panel_show_worker.AutoScrollPosition = New Point(0, Plus)
    End Sub
End Class