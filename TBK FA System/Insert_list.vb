Imports System.Net
Imports System.IO


Public Class Insert_list
    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Insert_list_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Prd_detail.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Dim client As New WebClient
        'Dim data As String = client.DownloadString("https://api.lifx.com/v1/lights/all")

        Dim tclient As New WebClient
        Dim i As Integer = Prd_detail.Label2.Text

        Dim temp_co_emp As Integer = List_Emp.ListView1.Items.Count

        For i = 0 To temp_co_emp - 1
            If i = 0 Then
                Try
                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                    Dim url As String = "Http://192.168.82.23/member/photo/" & emp_cd & ".jpg"
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))
                    Working_Pro.PictureBox2.Image = tImage
                    Working_Pro.lb_emp1.Visible = True
                    Working_Pro.lb_emp1.Text = emp_cd
                Catch ex As Exception
                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                    Working_Pro.PictureBox2.Image = tImage
                    Working_Pro.lb_emp1.Visible = True
                    Working_Pro.lb_emp1.Text = emp_cd
                End Try
            ElseIf i = 1 Then
                Try
                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                    Dim url As String = "Http://192.168.82.23/member/photo/" & emp_cd & ".jpg"
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))
                    Working_Pro.PictureBox3.Image = tImage
                    Working_Pro.lb_emp2.Visible = True
                    Working_Pro.lb_emp2.Text = emp_cd
                Catch ex As Exception
                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                    Working_Pro.PictureBox3.Image = tImage
                    Working_Pro.lb_emp2.Visible = True
                    Working_Pro.lb_emp2.Text = emp_cd
                End Try
            ElseIf i = 2 Then
                Try
                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                    Dim url As String = "Http://192.168.82.23/member/photo/" & emp_cd & ".jpg"
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))
                    Working_Pro.PictureBox4.Image = tImage
                    Working_Pro.lb_emp3.Visible = True
                    Working_Pro.lb_emp3.Text = emp_cd
                Catch ex As Exception
                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                    Working_Pro.PictureBox4.Image = tImage
                    Working_Pro.lb_emp3.Visible = True
                    Working_Pro.lb_emp3.Text = emp_cd
                End Try
            ElseIf i = 3 Then
                Try
                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                    Dim url As String = "Http://192.168.82.23/member/photo/" & emp_cd & ".jpg"
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))
                    Working_Pro.PictureBox5.Image = tImage
                    Working_Pro.lb_emp4.Visible = True
                    Working_Pro.lb_emp4.Text = emp_cd
                Catch ex As Exception
                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                    Working_Pro.PictureBox5.Image = tImage
                    Working_Pro.lb_emp4.Visible = True
                    Working_Pro.lb_emp4.Text = emp_cd
                End Try
            ElseIf i = 4 Then
                Try
                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                    Dim url As String = "Http://192.168.82.23/member/photo/" & emp_cd & ".jpg"
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))
                    Working_Pro.PictureBox6.Image = tImage
                    Working_Pro.lb_emp5.Visible = True
                    Working_Pro.lb_emp5.Text = emp_cd
                Catch ex As Exception
                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                    Working_Pro.PictureBox6.Image = tImage
                    Working_Pro.lb_emp5.Visible = True
                    Working_Pro.lb_emp5.Text = emp_cd
                End Try
            ElseIf i = 5 Then
                Try
                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                    Dim url As String = "Http://192.168.82.23/member/photo/" & emp_cd & ".jpg"
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))
                    Working_Pro.PictureBox7.Image = tImage
                    Working_Pro.lb_emp6.Visible = True
                    Working_Pro.lb_emp6.Text = emp_cd
                Catch ex As Exception
                    Dim emp_cd As String = List_Emp.ListView1.Items(i).Text
                    Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/tbkk_shopfloor_sys/asset/img_emp/no_user.jpg")))
                    Working_Pro.PictureBox7.Image = tImage
                    Working_Pro.lb_emp6.Visible = True
                    Working_Pro.lb_emp6.Text = emp_cd
                End Try
            End If

        Next



        'MsgBox(data)

        'Working_Pro.Button1.Text = MainFrm.cavity.Text & " Qty."

        Working_Pro.Label18.Text = Prd_detail.Label6.Text
        Working_Pro.Label29.Text = Prd_detail.Label2.Text
        Working_Pro.Label14.Text = Prd_detail.Label12.Text.Substring(0, 1)

        'MsgBox(Prd_detail.Label12.Text.Substring(0, 1))
        Dim numOfindex As Integer = ListView1.SelectedIndices(0)

        lb_temp_selected.Text = numOfindex
        Working_Pro.lb_temp.Text = numOfindex
        'MsgBox(ListView1.SelectedIndices(0))
        'MsgBox(ListView1.Items(10).Text.ToString)
        'MsgBox(ListView1.Items(10).SubItems(1).Text.ToString)
        'MsgBox(ListView1.Items(10).SubItems(2).Text.ToString)
        'MsgBox(ListView1.Items(numOfindex).SubItems(0).Text.ToString)
        Working_Pro.wi_no.Text = ListView1.Items(numOfindex).SubItems(0).Text.ToString
        Working_Pro.Label3.Text = ListView1.Items(numOfindex).SubItems(1).Text.ToString
        Working_Pro.Label12.Text = ListView1.Items(numOfindex).SubItems(2).Text.ToString
        Working_Pro.Label8.Text = ListView1.Items(numOfindex).SubItems(3).Text.ToString
        'Working_Pro.Label6.Text = ListView1.Items(numOfindex).SubItems(4).Text.ToString

        'SNP
        Working_Pro.Label27.Text = ListBox1.Items(numOfindex)
        Dim cavi_ty As Integer = MainFrm.cavity.Text

        'CT = ListBox2
        Working_Pro.CycleTime.Text = ListBox2.Items(numOfindex)
        Dim time_req As Double = ListBox2.Items(numOfindex) * ListView1.Items(numOfindex).SubItems(4).Text
        Dim emp_no As Integer = Convert.ToInt32(Prd_detail.Label2.Text)

        'time_req = time_req / emp_no


        'Working_Pro.Label34.Text = (time_req / emp_no)
        Working_Pro.Label34.Text = time_req
        Working_Pro.Label38.Text = Format((ListBox2.Items(numOfindex) * 60) * cavi_ty, "0.0")


        Working_Pro.Label37.Text = "0.0"


        'Dim TimeValue As Double = ListBox2.Items(numOfindex) * 60
        'Working_Pro.Label38.Text = TimeValue
        'Dim mins As Double
        'Dim secs As Double
        'Dim resss As String

        'mins = Fix(TimeValue / 60)
        'secs = TimeValue - (mins * 60)
        'If secs < 10 Then secs = "0" & secs
        'Dim resConv As String = secs.ToString()
        'resConv = resConv.Substring(0, 2)
        'resss = mins & "." & resConv
        'Working_Pro.Label38.Text = resss


        'SEQ = ListBox3
        If ListBox3.Items(numOfindex) < 10 Then
            Working_Pro.Label22.Text = "0" & ListBox3.Items(numOfindex) + 1
        Else
            Working_Pro.Label22.Text = ListBox3.Items(numOfindex) + 1
        End If

        'DLV DATE
        Working_Pro.lb_dlv_date.Text = lbx_dlv_date.Items(numOfindex)

        'MODEL
        Working_Pro.lb_model.Text = lbx_model.Items(numOfindex)

        'LOCATION
        Working_Pro.lb_location.Text = lbx_location.Items(numOfindex)

        'PRODUCT_TYPE
        Try
            Working_Pro.lb_prd_type.Text = lbx_prd_type.Items(numOfindex)
        Catch ex As Exception
            Working_Pro.lb_prd_type.Text = "30"
        End Try




        Dim sum_progress As Integer = (ListView1.Items(numOfindex).SubItems(4).Text * 100) / ListView1.Items(numOfindex).SubItems(3).Text
        sum_progress = 100 - sum_progress
        'MsgBox(sum_progress)

        Dim sum_act As Integer = ListView1.Items(numOfindex).SubItems(3).Text - ListView1.Items(numOfindex).SubItems(4).Text
        Working_Pro.Label6.Text = sum_act

        Dim sum_diff As String = ListView1.Items(numOfindex).SubItems(4).Text
        sum_diff = "-" & sum_diff
        Working_Pro.Label10.Text = sum_diff



        Working_Pro.Label33.Text = ListView1.Items(numOfindex).SubItems(4).Text

        Working_Pro.CircularProgressBar1.Text = sum_progress & "%"
        Working_Pro.CircularProgressBar1.Value = sum_progress

        Working_Pro.CircularProgressBar2.Text = 0 & "%"
        Working_Pro.CircularProgressBar2.Value = 0

        Working_Pro.Panel1.BackColor = Color.Red
        Working_Pro.Label30.Text = "STOPPED"
        Working_Pro.btn_start.Visible = True
        Working_Pro.btn_stop.Visible = False
        Working_Pro.Show()
        Me.Hide()
    End Sub



    Private Sub ListView1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count <= 0 Then
            Button3.Enabled = False
        ElseIf ListView1.SelectedItems.Count > 0 Then
            Button3.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Enabled = False
        sc_prd_plan.lb_text_buffer.Text = ""
        MainFrm.lb_ctrl_sc_flg.Text = "prdlist"
        sc_prd_plan.TextBox1.Select()
        sc_prd_plan.Show()
        'Me.Hide()
    End Sub
End Class