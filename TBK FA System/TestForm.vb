Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization
Imports NationalInstruments.DAQmx

Public Class TestForm
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.FromArgb(12, 59, 99)
        CreateCenteredPictureBoxes()
    End Sub

    Private Sub CreateCenteredPictureBoxes()
        ' ===== ลบ PictureBox เดิมก่อน (Reset) =====
        For i As Integer = Me.Controls.Count - 1 To 0 Step -1
            If TypeOf Me.Controls(i) Is PictureBox Then
                Me.Controls.RemoveAt(i)
            End If
        Next

        Dim boxWidth As Integer = 318
        Dim boxHeight As Integer = 148
        Dim gapX As Integer = 30
        Dim gapY As Integer = 30
        Dim totalWidth As Integer = (2 * boxWidth) + gapX
        Dim totalHeight As Integer = (2 * boxHeight) + gapY
        Dim offsetY As Integer = 40
        Dim startX As Integer = (Me.ClientSize.Width - totalWidth) \ 2
        Dim startY As Integer = ((Me.ClientSize.Height - totalHeight) \ 2) + offsetY

        ' ดึงข้อมูลจาก API
        Dim rsLoadMenuDefect = Backoffice_model.GetManageDefectMenu("K1A003")
        Dim imagePaths As New List(Of String)
        Dim actionList As New List(Of String)

        If rsLoadMenuDefect <> "0" Then
            Try
                Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsLoadMenuDefect)
                For Each item As Object In dict2
                    Dim dict As Dictionary(Of String, Object) = CType(item, Dictionary(Of String, Object))
                    imagePaths.Add(dict("smc_path_pic").ToString())
                    actionList.Add(dict("smc_function").ToString())
                Next
            Catch ex As Exception
                MessageBox.Show("Error parsing JSON: " & ex.Message)
                Return
            End Try
        End If

        For i As Integer = 0 To imagePaths.Count - 1
            Dim pic As New PictureBox()
            pic.Name = "pic" & (i + 1).ToString()
            pic.Width = boxWidth
            pic.Height = boxHeight
            ' pic.BorderStyle = BorderStyle.FixedSingle
            pic.BorderStyle = BorderStyle.None
            'pic.BackColor = Me.BackColor ' หรือใช้ Color.Transparent ถ้าไม่มีปัญหา render
            pic.SizeMode = PictureBoxSizeMode.Zoom

            ' fallback สีพื้น
            Dim fallbackBmp As New Bitmap(boxWidth, boxHeight)
            Using g As Graphics = Graphics.FromImage(fallbackBmp)
                g.Clear(Color.WhiteSmoke)
            End Using

            ' โหลดภาพจาก URL
            If i < imagePaths.Count Then
                Dim url As String = imagePaths(i)
                Try
                    Dim wc As New WebClient()
                    Dim imgData As Byte() = wc.DownloadData(url)
                    Using ms As New MemoryStream(imgData)
                        pic.Image = Image.FromStream(ms)
                    End Using
                Catch ex As Exception
                    pic.Image = fallbackBmp
                End Try
            Else
                pic.Image = fallbackBmp
            End If

            ' ใส่ action name ไว้ใน Tag
            If i < actionList.Count Then
                pic.Tag = actionList(i)
            Else
                pic.Tag = ""
            End If

            ' จัดตำแหน่ง
            Dim row As Integer = i \ 2
            Dim col As Integer = i Mod 2
            pic.Left = startX + col * (boxWidth + gapX)
            pic.Top = startY + row * (boxHeight + gapY)
            AddHandler pic.Click, AddressOf PictureBox_Click
            Me.Controls.Add(pic)
        Next
    End Sub

    Private Sub PictureBox_Click(sender As Object, e As EventArgs)
        Dim clickedPic As PictureBox = DirectCast(sender, PictureBox)
        Dim actionName As String = clickedPic.Tag.ToString()

        Select Case actionName
            Case "RegisterNC"
                RegisterNC()
            Case "RegisterNG"
                RegisterNG()
            Case "AdjustNC"
                AdjustNC()
            Case "AdjustNG"
                AdjustNG()
            Case Else
                MessageBox.Show("ยังไม่ได้กำหนด Action สำหรับ: " & actionName, "Info")
        End Select
    End Sub
    ' ===== ฟังก์ชันแต่ละปุ่ม =====
    Private Sub RegisterNC()
        MessageBox.Show("เรียกเมนู NC", "NC Action")
    End Sub
    Private Sub RegisterNG()
        MessageBox.Show("เรียกเมนู NG", "NG Action")
    End Sub
    Private Sub AdjustNC()
        MessageBox.Show("เปิดหน้าปรับ NC", "Adjust NC")
    End Sub
    Private Sub AdjustNG()
        MessageBox.Show("เปิดหน้าปรับ NG", "Adjust NG")
    End Sub
    ' ===== ปุ่มรีเฟรชเมนูใหม่ =====
    Private Sub ReButton_Click(sender As Object, e As EventArgs) Handles sentParameter.Click
        'CreateCenteredPictureBoxes()
        'สร้าง Task, Channel และเขียนค่า Output

    End Sub
End Class
