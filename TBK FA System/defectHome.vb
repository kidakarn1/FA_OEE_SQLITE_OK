Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization
Public Class defectHome
    Public arr_leader_confirme As New List(Of String)
    Public Shared dtType As String = "NO DATA"
    Public Shared leaderConfrime As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnRegisterngs.Click
        ' dtType = "NG"
        ' Dim dfSelecttype1 = New defectSelecttype()
        ' dfSelecttype1.show()
        ' Me.Close()
        RegisterNG()
    End Sub
    Public Sub RegisterNC()
        dtType = "NC"
        Dim dfSelecttype = New defectSelecttype()
        dfSelecttype.show()
        Me.Close()
        ' MsgBox("ไม่สามารถ ใส่ NC ได้ เนื่องจาก เมนูปิดแล้ว")
    End Sub
    Public Sub RegisterNG()
        dtType = "NG"
        Dim dfSelecttype1 = New defectSelecttype()
        dfSelecttype1.show()
        Me.Close()
    End Sub
    Public Sub AdjustNC()
        dtType = "NC"
        Dim dfDetailnc = New defectDetailnc()
        dfDetailnc.show()
        Me.Close()
    End Sub
    Public Sub AdjustNG()
        dtType = "NG"
        Dim dfDetailng = New defectDetailng()
        dfDetailng.show()
        Me.Close()
    End Sub
    Private Sub btnAdjustnc_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub btnAdjustng_Click(sender As Object, e As EventArgs) Handles btnAdjustngs.Click
        'dtType = "NG"
        'Dim dfDetailng = New defectDetailng()
        'dfDetailng.show()
        'Me.Close()
        AdjustNG()
    End Sub
    Private Sub defectHome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Working_Pro.TowerLamp(8, 0)
        CreateCenteredPictureBoxes()
        ' AddHandler btnBackDefectHome.Click, AddressOf btnBackDefectHome_Click
    End Sub
    Private Sub btnRegisterncs_Click(sender As Object, e As EventArgs) Handles btnRegisterncs.Click
        'dtType = "NC"
        'Dim dfSelecttype = New defectSelecttype()
        'dfSelecttype.show()
        'Me.Close()
        ' MsgBox("ไม่สามารถ ใส่ NC ได้ เนื่องจาก เมนูปิดแล้ว")
        RegisterNC()
    End Sub
    Private Sub btnAdjustncs_Click(sender As Object, e As EventArgs) Handles btnAdjustncs.Click
        AdjustNC()
        '  dtType = "NC"
        '  Dim dfDetailnc = New defectDetailnc()
        '  dfDetailnc.show()
        '  Me.Close()
        'MsgBox("ไม่สามารถ ปรับ NC ได้ เนื่องจาก เมนูปิดแล้ว")
    End Sub
    Public Sub CreateCenteredPictureBoxes()
        ' ===== ลบ PictureBox และ Control อื่น ๆ ที่ไม่จำเป็น (Reset) =====
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If TypeOf Me.Controls(i) Is PictureBox Then
                        Dim pic As PictureBox = CType(Me.Controls(i), PictureBox)
                        If pic.Name <> "btnBackDefectHome" AndAlso pic.Name <> "pbRefectData" Then
                            Me.Controls.RemoveAt(i)
                        End If
                    ElseIf TypeOf Me.Controls(i) Is Label AndAlso Me.Controls(i).BackColor = Color.White Then
                        Me.Controls.RemoveAt(i)
                    End If
                Next
                ' ===== กำหนดค่าขนาดและตำแหน่งของ PictureBox =====
                Dim boxWidth As Integer = 318
                Dim boxHeight As Integer = 148
                Dim gapX As Integer = 10  ' ลดช่องว่างแนวตั้ง
                Dim gapY As Integer = 20  ' ลดช่องว่างแนวนอน
                Dim totalWidth As Integer = (2 * boxWidth) + gapX
                Dim totalHeight As Integer = (2 * boxHeight) + gapY
                Dim offsetY As Integer = 40
                Dim startX As Integer = (Me.ClientSize.Width - totalWidth) \ 2
                Dim startY As Integer = ((Me.ClientSize.Height - totalHeight) \ 2) + offsetY
                ' ===== ดึงข้อมูลจาก API =====
                Dim rsLoadMenuDefect = Backoffice_model.GetManageDefectMenu(MainFrm.Label4.Text)
                Dim imagePaths As New List(Of String)
                Dim actionList As New List(Of String)
                arr_leader_confirme = New List(Of String)
                If rsLoadMenuDefect <> "0" Then
                    Try
                        Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsLoadMenuDefect)
                        For Each item As Object In dict2
                            Dim dict As Dictionary(Of String, Object) = CType(item, Dictionary(Of String, Object))
                            imagePaths.Add(dict("smc_path_pic").ToString())
                            actionList.Add(dict("smc_function").ToString())
                            arr_leader_confirme.Add(dict("smc_leader_confirme").ToString())
                        Next
                    Catch ex As Exception
                        ' MessageBox.Show("Error parsing JSON: " & ex.Message)
                        load_show.ShowDialog()
                        Return
                    End Try
                End If
                ' ===== กำหนดสีพื้นหลังฟอร์มให้เท่ากับพื้นหลัง UI เพื่อลดเส้นหลอกตา =====
                Me.BackColor = Color.DarkBlue
                ' ===== สร้าง PictureBox แต่ละรายการ =====
                For i As Integer = 0 To imagePaths.Count - 1
                    Dim pic As New PictureBox()
                    pic.Name = "pic" & (i + 1).ToString()
                    pic.Width = boxWidth
                    pic.Height = boxHeight
                    pic.BorderStyle = BorderStyle.None
                    pic.SizeMode = PictureBoxSizeMode.Zoom
                    pic.BackColor = Color.Transparent

                    ' fallback สีพื้นหากโหลดภาพไม่ได้
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

                    ' เก็บข้อมูล action และ leader confirm ลงใน Tag
                    Dim actionName As String = If(i < actionList.Count, actionList(i), "")
                    Dim leaderConfirm As String = If(i < arr_leader_confirme.Count, arr_leader_confirme(i), "")
                    pic.Tag = New With {
                    .ActionName = actionName,
                    .LeaderConfirm = leaderConfirm
                }

                    ' ===== คำนวณตำแหน่ง =====
                    Dim row As Integer = i \ 2
                    Dim col As Integer = i Mod 2
                    pic.Left = startX + col * (boxWidth + gapX)
                    pic.Top = startY + row * (boxHeight + gapY)

                    ' Event คลิก
                    AddHandler pic.Click, AddressOf PictureBox_Click

                    ' เพิ่ม PictureBox ลงในฟอร์ม
                    Me.Controls.Add(pic)
                Next
            Else
                load_show.ShowDialog()
            End If
        Catch ex As Exception
            load_show.ShowDialog()
        End Try
    End Sub

    Public Sub ManageMenuDefect(actionName As String)
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
    Private Sub PictureBox_Click(sender As Object, e As EventArgs)
        Dim clickedPic As PictureBox = DirectCast(sender, PictureBox)
        Dim actionName As String = ""
        Dim leaderConfirm As String = ""
        ' แยกค่าจาก Anonymous Object ที่เก็บไว้ใน Tag
        If clickedPic.Tag IsNot Nothing Then
            Dim tagData = clickedPic.Tag
            Dim t = tagData.GetType()
            actionName = t.GetProperty("ActionName").GetValue(tagData, Nothing).ToString()
            leaderConfirm = t.GetProperty("LeaderConfirm").GetValue(tagData, Nothing).ToString()
        End If
        ' แสดงค่า leaderConfirm ที่คลิก
        ' MessageBox.Show("LeaderConfirm: " & leaderConfirm, "Tag Info")
        ' กรณีต้องแสดงค่าทั้งหมดใน arr_leader_confirme (ถ้าจำเป็น)
        ' หากไม่จำเป็นสามารถลบส่วนนี้ออกได้
        ' เรียกฟังก์ชันตาม actionName
        If leaderConfirm = "1" Then
            Dim result As DialogResult = LeaderConfirmDefect.ShowDialog()
            If result = DialogResult.OK Then
                ManageMenuDefect(actionName)
            End If
        Else
            leaderConfrime = ""
            ManageMenuDefect(actionName)
        End If
    End Sub

    Private Sub ReDataButton_Click(sender As Object, e As EventArgs) Handles pbRefectData.Click
        CreateCenteredPictureBoxes()
        ' AddHandler btnBackDefectHome.Click, AddressOf btnBackDefectHome_Click
    End Sub
    Private Sub btnBackDefectHome_Click(sender As Object, e As EventArgs) Handles btnBackDefectHome.Click
        Working_Pro.ResetRed()
        Working_Pro.Enabled = True
        Me.Hide()
    End Sub
    ' ===== ฟังก์ชันแต่ละปุ่ม =====
End Class