Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Net
Public Class ScanQRprod
    Dim tclient As New WebClient
    Dim datTimeproduction As DateTime
    Dim G_StatusAction As Integer = 0
    Dim G_loopFromScanFA As Integer = 0
    Public Shared G_StatusAddCutDefect As Integer = 0
    Private Sub tb_QrProm_KeyDown(sender As Object, e As KeyEventArgs) Handles tb_QrProm.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                    If tb_QrProm.Text = "slog" Then
                        Working_Pro.Panel_log_scan_qr_product.Visible = True
                    ElseIf tb_QrProm.Text = "clog" Then
                        Working_Pro.Panel_log_scan_qr_product.Visible = False
                    ElseIf G_StatusAction = 1 Then
                        checkScanQrFA(G_loopFromScanFA, G_StatusAction)
                    ElseIf G_StatusAction = 2 Then
                        checkScanQrFAManual(G_loopFromScanFA, G_StatusAction)
                    ElseIf G_StatusAction = 3 Then
                        checkScanQrFADesc(G_loopFromScanFA, G_StatusAction)
                    ElseIf G_StatusAction = 4 Then
                        checkScanQrFADefectRegister(G_loopFromScanFA, G_StatusAction)
                    ElseIf G_StatusAction = 5 Then
                        checkScanQrFADefectAdjust(G_loopFromScanFA, G_StatusAction)
                    ElseIf G_StatusAction = 6 Then
                        G_StatusAction = "1" ' Form close lot 
                        checkScanQrFACloseLot(G_loopFromScanFA, G_StatusAction)
                    End If
                Else
                    load_show.Show()
                    'MsgBox("No Internet")
                End If
            Catch ex As Exception
                load_show.Show()
            End Try
        End If
    End Sub
    Public Async Sub ManageQrScanFA(statusAction As Integer, loopFrom As Integer)
        If statusAction = 1 Then
            lbTopices.Visible = False
            If Working_Pro.RemainScanDmc < 1 Then
                btn_close.Visible = True
            Else
                btn_close.Visible = False
            End If
            lb_Remain.Text = Working_Pro.RemainScanDmc
        ElseIf statusAction = 2 Then
            btn_close.Visible = True
            lbTopices.Visible = False
            lb_Remain.Text = Working_Pro.RemainScanDmcAddQty
        ElseIf statusAction = 3 Then
            '  btn_close.Visible = True
            lbTopices.Visible = False
            lb_Remain.Text = Working_Pro.RemainScanDmcDelQty
        ElseIf statusAction = 4 Then '  / 4 = register Defectss 
            lbTopices.Visible = False
            lb_Remain.Text = Working_Pro.RemainScanDmcDefect
            ' btn_close.Visible = False
        ElseIf statusAction = 5 Then ' / 5 = Adjust Defectss 
            lbTopices.Visible = True
            lb_Remain.Text = Working_Pro.RemainScanDmcDefect
            ' btn_close.Visible = False
        ElseIf statusAction = 6 Then ' / 6 = close lot 
            clear_remain.Visible = True
            lbTopices.Visible = False
            lb_Remain.Text = Working_Pro.RemainScanDmc
            ' btn_close.Visible = False
        End If
        G_StatusAction = statusAction
        G_loopFromScanFA = loopFrom
    End Sub
    Public Async Function checkScanQrFA(numberLoop As Integer, L_statusAction As Integer) As Task
        'For i As Integer = 1 To numberLoop
        Try
            Dim api_Scan = New Api_Scan_FA
            Dim line_cd As String = MainFrm.Label4.Text
            Dim idaid_production_date As String = Working_Pro.lvRemain.Items(Working_Pro.RemainScanDmc - 1).SubItems(1).Text ' ค่าของ Column ที่ 2
            Dim statusAction As String = L_statusAction
            Dim part_no As String = Working_Pro.Label3.Text
            Dim iqp_status = statusAction
            Dim iqp_defect = "0"
            Dim statusGroup = 1
            Dim rsCheckcondition = api_Scan.M_CheckCondionFA_Scan(line_cd, part_no, tb_QrProm.Text, iqp_status, iqp_defect, statusGroup, Working_Pro.pwi_id)
            If rsCheckcondition <> "0" Then
                Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsCheckcondition)
                Dim Alertstatus As Integer = 0
                Dim Alerttopices As String = ""
                Dim AlertFunction As String = ""
                Dim Alerttitle As String = ""
                Dim Alertpath_pic As String = ""
                Dim Alertpath_picIcon As String = ""
                Dim ALertdetail As String = ""
                Try
                    For Each item As Object In dict2
                        Alertstatus = item("status").ToString '' keep status
                    Next
                    For Each item As Object In dict2
                        Alerttopices = item("topices").ToString
                        AlertFunction = item("Function").ToString
                        Alerttitle = item("title").ToString
                        Alertpath_pic = item("path_pic").ToString
                        ALertdetail = item("detail").ToString
                        Alertpath_picIcon = item("path_pic_Icon").ToString

                    Next
                Catch ex As Exception

                End Try
                If Alertstatus = "1" Then
                    connect_counter_qty()
                    If api_Scan.M_checkStatus_info_qr(tb_QrProm.Text) = "3" Then  ' Case ที่ลบ มีข้อมูล DMC
                        Dim loop_qrProm As String = tb_QrProm.Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                        Dim loop_PK_iqp_id = api_Scan.M_Get_iqp_id(loop_qrProm)
                        Dim loop_statusAction As String = "1" ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                        Dim statusAddlog As String = "NO DATA"
                        Dim rsUpdate = api_Scan.M_Updated_status_info_qr(loop_PK_iqp_id, loop_statusAction, iqp_status, Working_Pro.pwi_id)
                        For i As Integer = 0 To Working_Pro.loglvQrProduct.Items.Count - 1
                            Dim logValue As ListViewItem = Working_Pro.loglvQrProduct.Items(i)
                            If logValue.SubItems(1).Text = tb_QrProm.Text Then
                                logValue.SubItems(3).Text = "Signal"
                                statusAddlog = "Signal"
                            End If
                        Next
                        If statusAddlog = "NO DATA" Then
                            Dim Load_Gpwi_id As String = Working_Pro.pwi_id ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                            Dim Load_loop_qrProm As String = tb_QrProm.Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                            Dim Load_loop_statusAction As String = "1" ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                            Dim load_pk_iqp_id = loop_PK_iqp_id
                            Dim newItem2 As New ListViewItem(Load_Gpwi_id)
                            newItem2.SubItems.Add(Load_loop_qrProm)
                            newItem2.SubItems.Add(load_pk_iqp_id)
                            newItem2.SubItems.Add("Add QTY")
                            Working_Pro.loglvQrProduct.Items.Add(newItem2)

                        End If
                    Else ' Case ที่ไม่เคยลบ ไม่มีข้อมูล DMC 
                        Dim pad_id = Working_Pro.PK_pad_id
                        Dim pwi_id As String = Working_Pro.pwi_id
                        Dim PK_iqp_id = api_Scan.M_Insert_QR_Product(pwi_id, tb_QrProm.Text, line_cd, idaid_production_date, statusAction)
                        Dim newItem As New ListViewItem(pwi_id.ToString())
                        newItem.SubItems.Add(tb_QrProm.Text)
                        newItem.SubItems.Add(PK_iqp_id)
                        newItem.SubItems.Add("1")
                        Working_Pro.lvQrProduct.Items.Add(newItem)
                        Dim newItem2 As New ListViewItem(pwi_id.ToString())
                        newItem2.SubItems.Add(tb_QrProm.Text)
                        newItem2.SubItems.Add(PK_iqp_id)
                        newItem2.SubItems.Add("Signal")
                        Working_Pro.loglvQrProduct.Items.Add(newItem2)
                    End If
                    Working_Pro.RemainScanDmc = Working_Pro.RemainScanDmc - 1
                    Dim index = Working_Pro.RemainScanDmc
                    Working_Pro.lvRemain.Items.RemoveAt(index)
                    lb_Remain.Text = Working_Pro.RemainScanDmc
                    tb_QrProm.Text = ""
                    If Working_Pro.RemainScanDmc = 0 Then
                        Working_Pro.lvQrProductDefect.Items.Clear()
                        Working_Pro.lvQrProduct.Items.Clear()
                        Working_Pro.cal_eff()
                        Working_Pro.Enabled = True
                        Me.Close()
                    End If
                Else
                    Try
                        Using client As New WebClient()
                            Dim data As Byte() = client.DownloadData(Alertpath_pic)
                            Using stream As New MemoryStream(data)
                                Dim tImage As Bitmap = Bitmap.FromStream(stream)
                                ' Alert_scan_qr_product.picStatusAlert.Image = tImage
                            End Using
                            Dim tImage2 As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(Alertpath_picIcon)))
                            Alert_scan_qr_product.pcIcon.Image = tImage2
                            Alert_scan_qr_product.lbTitle.Text = Alerttitle
                            Alert_scan_qr_product.lbDetail.Text = ALertdetail
                            Alert_scan_qr_product.pcIcon.BringToFront()
                            Me.Enabled = False
                            tb_QrProm.Text = ""
                            Dim result As DialogResult = Alert_scan_qr_product.ShowDialog()
                            If result = DialogResult.OK Then
                                Me.Enabled = True ' เปิดใช้งานฟอร์มต่อ
                            End If
                        End Using
                    Catch ex As Exception
                        MsgBox("Error downloading the image: " & ex.Message)
                    End Try
                    GoTo Outloop
                End If
            End If
        Catch ex As Exception

        End Try
        ' Next
Outloop:
    End Function
    Public Async Function checkScanQrFACloseLot(numberLoop As Integer, L_statusAction As Integer) As Task
        'For i As Integer = 1 To numberLoop
        Try
            Dim api_Scan = New Api_Scan_FA
            Dim line_cd As String = MainFrm.Label4.Text
            Dim idaid_production_date As String = Working_Pro.lvRemain.Items(Working_Pro.RemainScanDmc - 1).SubItems(1).Text ' ค่าของ Column ที่ 2
            Dim statusAction As String = L_statusAction
            Dim part_no As String = Working_Pro.Label3.Text
            Dim iqp_status = statusAction
            Dim iqp_defect = "0"
            Dim statusGroup = 1
            Dim rsCheckcondition = api_Scan.M_CheckCondionFA_Scan(line_cd, part_no, tb_QrProm.Text, iqp_status, iqp_defect, statusGroup, Working_Pro.pwi_id)
            If rsCheckcondition <> "0" Then
                Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsCheckcondition)
                Dim Alertstatus As Integer = 0
                Dim Alerttopices As String = ""
                Dim AlertFunction As String = ""
                Dim Alerttitle As String = ""
                Dim Alertpath_pic As String = ""
                Dim Alertpath_picIcon As String = ""
                Dim ALertdetail As String = ""
                Try
                    For Each item As Object In dict2
                        Alertstatus = item("status").ToString '' keep status
                    Next
                    For Each item As Object In dict2
                        Alerttopices = item("topices").ToString
                        AlertFunction = item("Function").ToString
                        Alerttitle = item("title").ToString
                        Alertpath_pic = item("path_pic").ToString
                        ALertdetail = item("detail").ToString
                        Alertpath_picIcon = item("path_pic_Icon").ToString

                    Next
                Catch ex As Exception

                End Try
                If Alertstatus = "1" Then
                    connect_counter_qty()
                    If api_Scan.M_checkStatus_info_qr(tb_QrProm.Text) = "3" Then  ' Case ที่ลบ มีข้อมูล DMC
                        Dim loop_qrProm As String = tb_QrProm.Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                        Dim loop_PK_iqp_id = api_Scan.M_Get_iqp_id(loop_qrProm)
                        Dim loop_statusAction As String = "1" ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                        Dim statusAddlog As String = "NO DATA"
                        Dim rsUpdate = api_Scan.M_Updated_status_info_qr(loop_PK_iqp_id, loop_statusAction, iqp_status, Working_Pro.pwi_id)
                        For i As Integer = 0 To Working_Pro.loglvQrProduct.Items.Count - 1
                            Dim logValue As ListViewItem = Working_Pro.loglvQrProduct.Items(i)
                            If logValue.SubItems(1).Text = tb_QrProm.Text Then
                                logValue.SubItems(3).Text = "Signal"
                                statusAddlog = "Signal"
                            End If
                        Next
                        If statusAddlog = "NO DATA" Then
                            Dim Load_Gpwi_id As String = Working_Pro.pwi_id ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                            Dim Load_loop_qrProm As String = tb_QrProm.Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                            Dim Load_loop_statusAction As String = "1" ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                            Dim load_pk_iqp_id = loop_PK_iqp_id
                            Dim newItem2 As New ListViewItem(Load_Gpwi_id)
                            newItem2.SubItems.Add(Load_loop_qrProm)
                            newItem2.SubItems.Add(load_pk_iqp_id)
                            newItem2.SubItems.Add("Add QTY")
                            Working_Pro.loglvQrProduct.Items.Add(newItem2)

                        End If
                    Else ' Case ที่ไม่เคยลบ ไม่มีข้อมูล DMC 
                        Dim pad_id = Working_Pro.PK_pad_id
                        Dim pwi_id As String = Working_Pro.pwi_id
                        Dim PK_iqp_id = api_Scan.M_Insert_QR_Product(pwi_id, tb_QrProm.Text, line_cd, idaid_production_date, statusAction)
                        Dim newItem As New ListViewItem(pwi_id.ToString())
                        newItem.SubItems.Add(tb_QrProm.Text)
                        newItem.SubItems.Add(PK_iqp_id)
                        newItem.SubItems.Add("1")
                        Working_Pro.lvQrProduct.Items.Add(newItem)
                        Dim newItem2 As New ListViewItem(pwi_id.ToString())
                        newItem2.SubItems.Add(tb_QrProm.Text)
                        newItem2.SubItems.Add(PK_iqp_id)
                        newItem2.SubItems.Add("Signal")
                        Working_Pro.loglvQrProduct.Items.Add(newItem2)
                    End If
                    Working_Pro.RemainScanDmc = Working_Pro.RemainScanDmc - 1
                    Dim index = Working_Pro.RemainScanDmc
                    Working_Pro.lvRemain.Items.RemoveAt(index)
                    lb_Remain.Text = Working_Pro.RemainScanDmc
                    tb_QrProm.Text = ""
                    If Working_Pro.RemainScanDmc = 0 Then
                        Working_Pro.lvQrProductDefect.Items.Clear()
                        Working_Pro.lvQrProduct.Items.Clear()
                        Working_Pro.cal_eff()
                        closeLotsummary.setVariable()
                        closeLotsummary.Enabled = True
                        Me.Close()
                    End If
                Else
                    Try
                        Using client As New WebClient()
                            Dim data As Byte() = client.DownloadData(Alertpath_pic)
                            Using stream As New MemoryStream(data)
                                Dim tImage As Bitmap = Bitmap.FromStream(stream)
                                ' Alert_scan_qr_product.picStatusAlert.Image = tImage
                            End Using
                            Dim tImage2 As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(Alertpath_picIcon)))
                            Alert_scan_qr_product.pcIcon.Image = tImage2
                            Alert_scan_qr_product.lbTitle.Text = Alerttitle
                            Alert_scan_qr_product.lbDetail.Text = ALertdetail
                            Alert_scan_qr_product.pcIcon.BringToFront()
                            Me.Enabled = False
                            tb_QrProm.Text = ""
                            Dim result As DialogResult = Alert_scan_qr_product.ShowDialog()
                            If result = DialogResult.OK Then
                                Me.Enabled = True ' เปิดใช้งานฟอร์มต่อ
                            End If
                        End Using
                    Catch ex As Exception
                        MsgBox("Error downloading the image: " & ex.Message)
                    End Try
                    GoTo Outloop
                End If
            End If
        Catch ex As Exception

        End Try
        ' Next
Outloop:
    End Function
    Public Async Function checkScanQrFAManual(numberLoop As Integer, L_statusAction As Integer) As Task
        'For i As Integer = 1 To numberLoop
        Try
            Dim api_Scan = New Api_Scan_FA
            Dim line_cd As String = MainFrm.Label4.Text
            Dim index As Integer = CDbl(Val(Working_Pro.RemainScanDmcAddQty - 1))
            Dim idaid_production_date As String = Working_Pro.lvRemainmanual.Items(index).SubItems(1).Text ' ค่าของ Column ที่ 2
            Dim statusAction As String = L_statusAction
            Dim part_no As String = Working_Pro.Label3.Text
            Dim iqp_status = statusAction
            Dim iqp_defect = "0"
            Dim statusGroup = 1
            Dim rsCheckcondition = api_Scan.M_CheckCondionFA_Scan(line_cd, part_no, tb_QrProm.Text, iqp_status, iqp_defect, statusGroup, Working_Pro.pwi_id)
            If rsCheckcondition <> "0" Then
                Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsCheckcondition)
                Dim Alertstatus As Integer = 0
                Dim Alerttopices As String = ""
                Dim AlertFunction As String = ""
                Dim Alerttitle As String = ""
                Dim Alertpath_pic As String = ""
                Dim Alertpath_picIcon As String = ""
                Dim ALertdetail As String = ""
                Try
                    For Each item As Object In dict2
                        Alertstatus = item("status").ToString '' keep status
                    Next
                    For Each item As Object In dict2
                        Alerttopices = item("topices").ToString
                        AlertFunction = item("Function").ToString
                        Alerttitle = item("title").ToString
                        Alertpath_pic = item("path_pic").ToString
                        ALertdetail = item("detail").ToString
                        Alertpath_picIcon = item("path_pic_Icon").ToString
                    Next
                Catch ex As Exception

                End Try
                If Alertstatus = "1" Then
                    'connect_counter_qty()
                    ' Dim pad_id = Working_Pro.PK_pad_id
                    'Dim PK_iqp_id = api_Scan.M_Insert_QR_Product(pad_id, tb_QrProm.Text, line_cd, idaid_production_date, statusAction)
                    ' Dim newItem As New ListViewItem(pad_id.ToString())
                    ' newItem.SubItems.Add(tb_QrProm.Text)
                    ' newItem.SubItems.Add("")
                    ' newItem.SubItems.Add("2")
                    ' Working_Pro.lvQrProduct.Items.Add(newItem)
                    'Working_Pro.RemainScanDmcAddQty = Working_Pro.RemainScanDmcAddQty - 1
                    'Dim index = Working_Pro.RemainScanDmcAddQty
                    'Working_Pro.lvRemainmanual.Items.RemoveAt(index)
                    'lb_Remain.Text = Working_Pro.RemainScanDmcAddQty
                    'tb_QrProm.Text = ""

                    For Each item As ListViewItem In Working_Pro.lvQrProduct.Items
                        If item.SubItems(1).Text = tb_QrProm.Text Then
                            Dim api = New api
                            Dim tImage2 As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/API_NEW_FA/images/Alert-unscreen.gif")))
                            Alert_scan_qr_product.pcIcon.Image = tImage2
                            Alert_scan_qr_product.lbTitle.Text = "Please verify the QR Code for this Part."
                            Alert_scan_qr_product.lbDetail.Text = "QR Code previously scanned on"
                            Alert_scan_qr_product.pcIcon.BringToFront()
                            Me.Enabled = False
                            checkDup = 1
                            tb_QrProm.Text = ""
                            Dim result As DialogResult = Alert_scan_qr_product.ShowDialog()
                            If result = DialogResult.OK Then
                                Me.Enabled = True ' เปิดใช้งานฟอร์มต่อ
                            End If
                            GoTo Outloop
                        End If
                    Next
                    Dim statusDefect = "1"
                    'Dim PK_iqp_id = api_Scan.M_getDatainfo_qr_product(tb_QrProm.Text)
                    Dim newItem As New ListViewItem(Working_Pro.pwi_id)
                    newItem.SubItems.Add(tb_QrProm.Text)
                        newItem.SubItems.Add("NO DATA")
                        newItem.SubItems.Add("2")
                        Working_Pro.lvQrProduct.Items.Add(newItem)
                    Dim rs As Integer
                    If Working_Pro.RemainScanDmcAddQty > 0 Then
                        rs = (CDbl(Val(Working_Pro.RemainScanDmcAddQty)) - 1)
                        Working_Pro.RemainScanDmcAddQty = rs
                    End If
                    lb_Remain.Text = rs
                    tb_QrProm.Text = ""
                    If Working_Pro.RemainScanDmcAddQty = 0 Then
                        ins_qty.insert_qty(Working_Pro.lvQrProduct.Items.Count)
                        For Each item As ListViewItem In Working_Pro.lvQrProduct.Items
                            ' อ่านค่าจากแต่ละคอลัมน์ของรายการ
                            If api_Scan.M_checkStatus_info_qr(item.SubItems(1).Text) = "3" Then  ' Case ที่ลบ มีข้อมูล DMC
                                Dim loop_qrProm As String = item.SubItems(1).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                                Dim loop_PK_iqp_id = api_Scan.M_Get_iqp_id(loop_qrProm)
                                Dim loop_statusAction As String = item.SubItems(3).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                                Dim rsUpdate = api_Scan.M_Updated_status_info_qr(loop_PK_iqp_id, loop_statusAction, iqp_status, Working_Pro.pwi_id)
                                Dim statusAddlog As String = "NO DATA"
                                For i As Integer = 0 To Working_Pro.loglvQrProduct.Items.Count - 1
                                    Dim logValue As ListViewItem = Working_Pro.loglvQrProduct.Items(i)
                                    If logValue.SubItems(1).Text = item.SubItems(1).Text Then
                                        logValue.SubItems(3).Text = "Add QTY"
                                        statusAddlog = "ADD OK"
                                    End If
                                Next
                                If statusAddlog = "NO DATA" Then
                                    Dim Load_Gpwi_id As String = Working_Pro.pwi_id ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                                    Dim Load_loop_qrProm As String = item.SubItems(1).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                                    Dim Load_loop_statusAction As String = item.SubItems(3).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                                    Dim load_pk_iqp_id = loop_PK_iqp_id
                                    Dim newItem2 As New ListViewItem(Load_Gpwi_id)
                                    newItem2.SubItems.Add(Load_loop_qrProm)
                                    newItem2.SubItems.Add(load_pk_iqp_id)
                                    newItem2.SubItems.Add("Add QTY")
                                    Working_Pro.loglvQrProduct.Items.Add(newItem2)
                                End If
                            Else ' Case ที่ไม่เคยลบ ไม่มีข้อมูล DMC 
                                Dim Gpwi_id As String = Working_Pro.pwi_id ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                                Dim loop_qrProm As String = item.SubItems(1).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                                Dim loop_statusAction As String = item.SubItems(3).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                                Dim rs_PK_iqp_id = api_Scan.M_Insert_QR_Product(Gpwi_id, loop_qrProm, line_cd, idaid_production_date, loop_statusAction)
                                Dim newItem2 As New ListViewItem(Gpwi_id)
                                newItem2.SubItems.Add(loop_qrProm)
                                newItem2.SubItems.Add(rs_PK_iqp_id)
                                newItem2.SubItems.Add("Add QTY")
                                Working_Pro.loglvQrProduct.Items.Add(newItem2)
                            End If
                        Next
                        Working_Pro.lvRemainmanual.Items.Clear()
                        Working_Pro.lvQrProduct.Items.Clear()
                        Working_Pro.RemainScanDmc = 0
                        Working_Pro.RemainScanDmcAddQty = 0
                        Working_Pro.cal_eff()
                        Working_Pro.Enabled = True
                        Me.Close()
                    End If
                Else
                    Try
                        Using client As New WebClient()
                            Dim data As Byte() = client.DownloadData(Alertpath_pic)
                            Using stream As New MemoryStream(data)
                                Dim tImage As Bitmap = Bitmap.FromStream(stream)
                                'Alert_scan_qr_product.picStatusAlert.Image = tImage
                            End Using
                            Dim tImage2 As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(Alertpath_picIcon)))
                            Alert_scan_qr_product.pcIcon.Image = tImage2
                            Alert_scan_qr_product.lbTitle.Text = Alerttitle
                            Alert_scan_qr_product.lbDetail.Text = ALertdetail
                            Alert_scan_qr_product.pcIcon.BringToFront()
                            Me.Enabled = False
                            tb_QrProm.Text = ""
                            Dim result As DialogResult = Alert_scan_qr_product.ShowDialog()
                            If result = DialogResult.OK Then
                                Me.Enabled = True ' เปิดใช้งานฟอร์มต่อ
                            End If
                        End Using
                    Catch ex As Exception
                        MsgBox("Error downloading the image: " & ex.Message)
                    End Try
                    GoTo Outloop
                End If
            End If
        Catch ex As Exception

        End Try
        ' Next
Outloop:
    End Function
    Public Shared Function getFormByName(formName As String) As Form
        ' ตรวจสอบว่าฟอร์มเปิดอยู่หรือไม่
        For Each frm As Form In Application.OpenForms
            If frm.Name = formName Then
                Return frm
            End If
        Next

        ' ถ้าไม่พบ ให้ลองสร้างฟอร์มใหม่
        Dim formType As Type = System.Reflection.Assembly.GetExecutingAssembly().GetTypes() _
        .Where(Function(T) (T.BaseType Is GetType(Form)) AndAlso (T.Name = formName)) _
        .FirstOrDefault()

        Return If(formType Is Nothing, Nothing, DirectCast(Activator.CreateInstance(formType), Form))
    End Function
    Public Async Function checkScanQrFADefectRegister(numberLoop As Integer, L_statusAction As Integer) As Task
        'For i As Integer = 1 To numberLoop
        Try
            Dim api_Scan = New Api_Scan_FA
            Dim line_cd As String = MainFrm.Label4.Text
            Dim statusAction As String = L_statusAction
            Dim part_no As String = Working_Pro.Label3.Text
            Dim iqp_status = statusAction
            Dim iqp_defect = "0"
            Dim statusGroup = "3"
            Dim rsCheckcondition = api_Scan.M_CheckCondionFA_Scan(line_cd, part_no, tb_QrProm.Text, iqp_status, iqp_defect, statusGroup, Working_Pro.pwi_id)
            If rsCheckcondition <> "0" Then
                Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsCheckcondition)
                Dim Alertstatus As Integer = 0
                Dim Alerttopices As String = ""
                Dim AlertFunction As String = ""
                Dim Alerttitle As String = ""
                Dim Alertpath_pic As String = ""
                Dim Alertpath_picIcon As String = ""
                Dim ALertdetail As String = ""
                Try
                    For Each item As Object In dict2
                        Alertstatus = item("status").ToString '' keep status
                    Next
                    For Each item As Object In dict2
                        Alerttopices = item("topices").ToString
                        AlertFunction = item("Function").ToString
                        Alerttitle = item("title").ToString
                        Alertpath_pic = item("path_pic").ToString
                        ALertdetail = item("detail").ToString
                        Alertpath_picIcon = item("path_pic_Icon").ToString
                    Next
                Catch ex As Exception

                End Try
                If Alertstatus = "1" Then
                    For Each item As ListViewItem In Working_Pro.lvQrProductDefect.Items
                        If item.SubItems(1).Text = tb_QrProm.Text Then
                            Dim api = New api
                            Dim tImage2 As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/API_NEW_FA/images/Alert-unscreen.gif")))
                            Alert_scan_qr_product.pcIcon.Image = tImage2
                            Alert_scan_qr_product.lbTitle.Text = "Please verify the QR Code for this Part."
                            Alert_scan_qr_product.lbDetail.Text = "QR Code previously scanned on"
                            Alert_scan_qr_product.pcIcon.BringToFront()
                            tb_QrProm.Text = ""
                            Dim result As DialogResult = Alert_scan_qr_product.ShowDialog()
                            If result = DialogResult.OK Then
                                Me.Enabled = True ' เปิดใช้งานฟอร์มต่อ
                            End If
                            GoTo Outloop
                        End If
                    Next
                    Dim statusDefect = "1"
                    Dim PK_iqp_id = api_Scan.M_getDatainfo_qr_product(tb_QrProm.Text)
                    Dim newItem As New ListViewItem("NO")
                    newItem.SubItems.Add(tb_QrProm.Text)
                    newItem.SubItems.Add(PK_iqp_id)
                    newItem.SubItems.Add("4")
                    Working_Pro.lvQrProductDefect.Items.Add(newItem)
                    Working_Pro.RemainScanDmcDefect = Working_Pro.RemainScanDmcDefect - 1
                    lb_Remain.Text = Working_Pro.RemainScanDmcDefect
                    If Working_Pro.RemainScanDmcDefect = 0 Then
                        For Each item As ListViewItem In Working_Pro.lvQrProductDefect.Items
                            ' อ่านค่าจากแต่ละคอลัมน์ของรายการ
                            Dim loop_qrProm As String = item.SubItems(1).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                            Dim loop_PK_iqp_id As String = item.SubItems(2).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                            Dim loop_statusAction As String = item.SubItems(3).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                            Dim rsUpdate = api_Scan.M_Updated_QR_Product(loop_PK_iqp_id, loop_statusAction, "1", Working_Pro.pwi_id)
                            For i As Integer = 0 To Working_Pro.loglvQrProduct.Items.Count - 1
                                Dim logValue As ListViewItem = Working_Pro.loglvQrProduct.Items(i)
                                If logValue.SubItems(1).Text = item.SubItems(1).Text Then
                                    logValue.SubItems(3).Text = "Register Defect"
                                End If
                            Next
                        Next
                        Working_Pro.lvQrProductDefect.Items.Clear()
                        Dim fromRegis = defectRegister.GdfRegister
                        Dim defectForm As Form = getFormByName("defectRegister")
                        If defectRegister.tbQtydefectnc.Text = 0 Then ' plus
                            defectRegister.tbQtydefectnc.Text = fromRegis.GdfRegister.tbQtydefectnc.text
                            defectRegister.dfQty = defectRegister.tbQtydefectnc.Text
                            fromRegis.calFG()
                        Else ' numpad ไม่ต้อง Set ตัวแปร ใหม่ 
                            defectRegister.CalFG()
                        End If
                        If defectForm IsNot Nothing Then
                            defectForm.Close()
                            defectForm.Dispose()
                            Me.Close()
                        End If
                    End If
                    tb_QrProm.Text = ""
                Else
                    Try
                        Using client As New WebClient()
                            Dim data As Byte() = client.DownloadData(Alertpath_pic)
                            Using stream As New MemoryStream(data)
                                Dim tImage As Bitmap = Bitmap.FromStream(stream)
                                ''Alert_scan_qr_product.picStatusAlert.Image = tImage
                            End Using
                            Dim tImage2 As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(Alertpath_picIcon)))
                            Alert_scan_qr_product.pcIcon.Image = tImage2
                            Alert_scan_qr_product.lbTitle.Text = Alerttitle
                            Alert_scan_qr_product.lbDetail.Text = ALertdetail
                            Alert_scan_qr_product.pcIcon.BringToFront()
                            Me.Enabled = False
                            tb_QrProm.Text = ""
                            Dim result As DialogResult = Alert_scan_qr_product.ShowDialog()
                            If result = DialogResult.OK Then
                                Me.Enabled = True ' เปิดใช้งานฟอร์มต่อ
                            End If
                        End Using
                    Catch ex As Exception
                        MsgBox("Error downloading the image: " & ex.Message)
                    End Try
                    GoTo Outloop
                End If
            End If
        Catch ex As Exception
        End Try
        ' Next
Outloop:
    End Function
    Public Async Function checkScanQrFADefectAdjust(numberLoop As Integer, L_statusAction As Integer) As Task
        'For i As Integer = 1 To numberLoop
        Try
            Dim api_Scan = New Api_Scan_FA
            Dim line_cd As String = MainFrm.Label4.Text
            Dim statusAction As String = L_statusAction
            Dim part_no As String = Working_Pro.Label3.Text
            Dim iqp_status = statusAction
            Dim iqp_defect = "0"
            Dim statusGroup = "4"
            Dim statusDefectUpdate As Integer = 0
            Dim CheckCmd As String = ""
            If G_StatusAddCutDefect = "1" Then
                statusGroup = "4"
                statusDefectUpdate = 1
                CheckCmd = "Register Defect"
            Else
                statusGroup = "5"
                CheckCmd = "Add QTY"
                statusDefectUpdate = 0
            End If
            Dim rsCheckcondition = api_Scan.M_CheckCondionFA_Scan(line_cd, part_no, tb_QrProm.Text, iqp_status, iqp_defect, statusGroup, Working_Pro.pwi_id)
            If rsCheckcondition <> "0" Then
                Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsCheckcondition)
                Dim Alertstatus As Integer = 0
                Dim Alerttopices As String = ""
                Dim AlertFunction As String = ""
                Dim Alerttitle As String = ""
                Dim Alertpath_pic As String = ""
                Dim Alertpath_picIcon As String = ""
                Dim ALertdetail As String = ""
                Try
                    For Each item As Object In dict2
                        Alertstatus = item("status").ToString '' keep status
                    Next
                    For Each item As Object In dict2
                        Alerttopices = item("topices").ToString
                        AlertFunction = item("Function").ToString
                        Alerttitle = item("title").ToString
                        Alertpath_pic = item("path_pic").ToString
                        ALertdetail = item("detail").ToString
                        Alertpath_picIcon = item("path_pic_Icon").ToString
                    Next
                Catch ex As Exception
                End Try
                If Alertstatus = "1" Then
                    For Each item As ListViewItem In Working_Pro.lvQrProductDefect.Items
                        If item.SubItems(1).Text = tb_QrProm.Text Then
                            Dim api = New api
                            Dim tImage2 As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/API_NEW_FA/images/Alert-unscreen.gif")))
                            Alert_scan_qr_product.pcIcon.Image = tImage2
                            Alert_scan_qr_product.lbTitle.Text = "Please verify the QR Code for this Part."
                            Alert_scan_qr_product.lbDetail.Text = "QR Code previously scanned on"
                            Alert_scan_qr_product.pcIcon.BringToFront()
                            tb_QrProm.Text = ""
                            Dim result As DialogResult = Alert_scan_qr_product.ShowDialog()
                            If result = DialogResult.OK Then
                                Me.Enabled = True ' เปิดใช้งานฟอร์มต่อ
                            End If
                            GoTo Outloop
                        End If
                    Next
                    Dim statusDefect = "1"
                    Dim PK_iqp_id = api_Scan.M_getDatainfo_qr_product(tb_QrProm.Text)
                    Dim newItem As New ListViewItem("NO")
                    newItem.SubItems.Add(tb_QrProm.Text)
                    newItem.SubItems.Add(PK_iqp_id)
                    newItem.SubItems.Add("5")
                    Working_Pro.lvQrProductDefect.Items.Add(newItem)
                    Working_Pro.RemainScanDmcDefect = Working_Pro.RemainScanDmcDefect - 1
                    lb_Remain.Text = Working_Pro.RemainScanDmcDefect
                    If Working_Pro.RemainScanDmcDefect = 0 Then
                        For Each item As ListViewItem In Working_Pro.lvQrProductDefect.Items
                            ' อ่านค่าจากแต่ละคอลัมน์ของรายการ
                            Dim loop_qrProm As String = item.SubItems(1).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                            Dim loop_PK_iqp_id As String = item.SubItems(2).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                            Dim loop_statusAction As String = item.SubItems(3).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                            Dim rsUpdate = api_Scan.M_Updated_QR_Product(loop_PK_iqp_id, loop_statusAction, statusDefectUpdate, Working_Pro.pwi_id)
                            For i As Integer = 0 To Working_Pro.loglvQrProduct.Items.Count - 1
                                Dim logValue As ListViewItem = Working_Pro.loglvQrProduct.Items(i)
                                If logValue.SubItems(1).Text = item.SubItems(1).Text Then
                                    logValue.SubItems(3).Text = CheckCmd
                                End If
                            Next
                        Next
                        Working_Pro.lvQrProductDefect.Items.Clear()
                        Dim defectForm As Form = getFormByName("defectNumpadadjust")
                        Dim fromRegis = defectNumpadadjust.GdfNumpadafjust
                        fromRegis.manageNg()
                        If defectForm IsNot Nothing Then
                            defectForm.Close()
                            defectForm.Dispose()
                            Me.Close()
                        End If
                    End If
                    tb_QrProm.Text = ""
                Else
                    Try
                        Using client As New WebClient()
                            Dim data As Byte() = client.DownloadData(Alertpath_pic)
                            Using stream As New MemoryStream(data)
                                Dim tImage As Bitmap = Bitmap.FromStream(stream)
                                'Alert_scan_qr_product.picStatusAlert.Image = tImage
                            End Using
                            Dim tImage2 As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(Alertpath_picIcon)))
                            Alert_scan_qr_product.pcIcon.Image = tImage2
                            Alert_scan_qr_product.lbTitle.Text = Alerttitle
                            Alert_scan_qr_product.lbDetail.Text = ALertdetail
                            Alert_scan_qr_product.pcIcon.BringToFront()
                            Me.Enabled = False
                            tb_QrProm.Text = ""
                            Dim result As DialogResult = Alert_scan_qr_product.ShowDialog()
                            If result = DialogResult.OK Then
                                Me.Enabled = True ' เปิดใช้งานฟอร์มต่อ
                            End If
                        End Using
                    Catch ex As Exception
                        MsgBox("Error downloading the image: " & ex.Message)
                    End Try
                    GoTo Outloop
                End If
            End If
        Catch ex As Exception
        End Try
        ' Next
Outloop:
    End Function
    Public Async Function checkScanQrFADesc(numberLoop As Integer, L_statusAction As Integer) As Task
        'For i As Integer = 1 To numberLoop
        Try
            Dim api_Scan = New Api_Scan_FA
            Dim line_cd As String = MainFrm.Label4.Text
            Dim statusAction As String = L_statusAction
            Dim part_no As String = Working_Pro.Label3.Text
            Dim iqp_status = statusAction
            Dim iqp_defect = "0"
            Dim statusGroup = "2"
            Dim statusDefectUpdate As Integer = 0
            Dim rsCheckcondition = api_Scan.M_CheckCondionFA_Scan(line_cd, part_no, tb_QrProm.Text, iqp_status, iqp_defect, statusGroup, Working_Pro.pwi_id)
            If rsCheckcondition <> "0" Then
                Dim dict2 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsCheckcondition)
                Dim Alertstatus As Integer = 0
                Dim Alerttopices As String = ""
                Dim AlertFunction As String = ""
                Dim Alerttitle As String = ""
                Dim Alertpath_pic As String = ""
                Dim Alertpath_picIcon As String = ""
                Dim ALertdetail As String = ""
                Try
                    For Each item As Object In dict2
                        Alertstatus = item("status").ToString '' keep status
                    Next
                    For Each item As Object In dict2
                        Alerttopices = item("topices").ToString
                        AlertFunction = item("Function").ToString
                        Alerttitle = item("title").ToString
                        Alertpath_pic = item("path_pic").ToString
                        ALertdetail = item("detail").ToString
                        Alertpath_picIcon = item("path_pic_Icon").ToString
                    Next
                Catch ex As Exception
                End Try
                If Alertstatus = "1" Then
                    For Each item As ListViewItem In Working_Pro.lvQrProduct.Items
                        If item.SubItems(1).Text = tb_QrProm.Text Then

                            Dim api = New api
                            Dim tImage2 As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/API_NEW_FA/images/Alert-unscreen.gif")))
                            Alert_scan_qr_product.pcIcon.Image = tImage2
                            Alert_scan_qr_product.lbTitle.Text = "Please verify the QR Code for this Part."
                            Alert_scan_qr_product.lbDetail.Text = "QR Code previously scanned on"
                            Alert_scan_qr_product.pcIcon.BringToFront()
                            Dim result As DialogResult = Alert_scan_qr_product.ShowDialog()
                            If result = DialogResult.OK Then
                                Me.Enabled = True ' เปิดใช้งานฟอร์มต่อ
                            End If
                            tb_QrProm.Text = ""
                            GoTo Outloop
                        End If
                    Next
                    Dim checkStatusQrProduct As Integer = 0
                    For Each item As ListViewItem In Working_Pro.loglvQrProduct.Items
                        If item.SubItems(1).Text = tb_QrProm.Text Then
                            If item.SubItems(3).Text <> "Register Defect" Then
                                checkStatusQrProduct = 1
                            Else
                                checkStatusQrProduct = 2
                            End If
                            GoTo nextStep
                            '  Dim tImage2 As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(Alertpath_picIcon)))
                            '  Alert_scan_qr_product.pcIcon.Image = tImage2
                            '  Alert_scan_qr_product.lbTitle.Text = Alerttitle
                            '  Alert_scan_qr_product.lbDetail.Text = ALertdetail
                            '  Alert_scan_qr_product.pcIcon.BringToFront()
                            '  Me.Enabled = False
                            '  tb_QrProm.Text = ""
                            '  Dim result As DialogResult = Alert_scan_qr_product.ShowDialog()
                            '  If result = DialogResult.OK Then
                            '  Me.Enabled = True ' เปิดใช้งานฟอร์มต่อ
                            'End If
                            '  MsgBox("QR DOUBLE")
                            '  tb_QrProm.Text = ""
                            '  GoTo Outloop
                        End If
                    Next
nextStep:
                    If checkStatusQrProduct = 1 Then
                        Dim PK_iqp_id = api_Scan.M_getDatainfo_qr_product(tb_QrProm.Text)
                        Dim newItem As New ListViewItem("NO")
                        newItem.SubItems.Add(tb_QrProm.Text)
                        newItem.SubItems.Add(PK_iqp_id)
                        newItem.SubItems.Add("3")
                        Working_Pro.lvQrProduct.Items.Add(newItem)
                        Working_Pro.RemainScanDmcDelQty = Working_Pro.RemainScanDmcDelQty - 1
                        lb_Remain.Text = Working_Pro.RemainScanDmcDelQty
                        tb_QrProm.Text = ""
                        If Working_Pro.RemainScanDmcDelQty = 0 Then
                            Desc_act.set_data_del()
                            For Each item As ListViewItem In Working_Pro.lvQrProduct.Items
                                ' อ่านค่าจากแต่ละคอลัมน์ของรายการ
                                Dim loop_qrProm As String = item.SubItems(1).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                                Dim loop_PK_iqp_id As String = item.SubItems(2).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                                Dim loop_statusAction As String = item.SubItems(3).Text ' ค่าจากคอลัมน์ที่สอง (ถ้ามี)
                                Dim rsUpdate = api_Scan.M_Updated_status_info_qr(loop_PK_iqp_id, loop_statusAction, iqp_status, Working_Pro.pwi_id)
                                For i As Integer = 0 To Working_Pro.loglvQrProduct.Items.Count - 1
                                    Dim logValue As ListViewItem = Working_Pro.loglvQrProduct.Items(i)
                                    If logValue.SubItems(1).Text = item.SubItems(1).Text Then
                                        logValue.SubItems(3).Text = "Desc QTY"
                                    End If
                                Next
                            Next
                            Working_Pro.lvQrProduct.Items.Clear()
                            Desc_act.Close()
                            Me.Close()
                        End If
                    ElseIf checkStatusQrProduct = 2 Then
                        Dim api = New api
                        Dim tImage2 As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/API_NEW_FA/images/Alert-unscreen.gif")))
                        Alert_scan_qr_product.pcIcon.Image = tImage2
                        Alert_scan_qr_product.lbTitle.Text = "Please verify the QR Code for this Part."
                        Alert_scan_qr_product.lbDetail.Text = "QR Code marked as NG item in system."
                        Alert_scan_qr_product.pcIcon.BringToFront()
                        tb_QrProm.Text = ""
                        GoTo Outloop
                    Else
                        Dim api = New api
                        Dim tImage2 As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData("http://" & Backoffice_model.svApi & "/API_NEW_FA/images/Alert-unscreen.gif")))
                        Alert_scan_qr_product.pcIcon.Image = tImage2
                        Alert_scan_qr_product.lbTitle.Text = "Please verify the QR Code for this Part."
                        Alert_scan_qr_product.lbDetail.Text = "QR Code not in production sequence."
                        Alert_scan_qr_product.pcIcon.BringToFront()
                        tb_QrProm.Text = ""
                        GoTo Outloop
                    End If
                Else
                    Try
                        Using client As New WebClient()
                            Dim data As Byte() = client.DownloadData(Alertpath_pic)
                            Using stream As New MemoryStream(data)
                                Dim tImage As Bitmap = Bitmap.FromStream(stream)
                                'Alert_scan_qr_product.picStatusAlert.Image = tImage
                            End Using
                            Dim tImage2 As Bitmap = Bitmap.FromStream(New MemoryStream(tclient.DownloadData(Alertpath_picIcon)))
                            Alert_scan_qr_product.pcIcon.Image = tImage2
                            Alert_scan_qr_product.lbTitle.Text = Alerttitle
                            Alert_scan_qr_product.lbDetail.Text = ALertdetail
                            Alert_scan_qr_product.pcIcon.BringToFront()
                            Me.Enabled = False
                            tb_QrProm.Text = ""
                            Dim result As DialogResult = Alert_scan_qr_product.ShowDialog()
                            If result = DialogResult.OK Then
                                Me.Enabled = True ' เปิดใช้งานฟอร์มต่อ
                            End If
                        End Using
                    Catch ex As Exception
                        MsgBox("Error downloading the image: " & ex.Message)
                    End Try
                    GoTo Outloop
                End If
            End If
        Catch ex As Exception
        End Try
        ' Next
Outloop:
    End Function
    Public Sub connect_counter_qty()
        If Working_Pro.s_mecg_name = "DIO-3232LX-USB" Then
            Working_Pro.counter_contect_DIO()
        ElseIf Working_Pro.s_mecg_name = "RS232" Then
            Working_Pro.counter_contect_DIO_RS232()
        ElseIf Working_Pro.s_mecg_name = "NO DEVICE" Then

        Else ' NI MAX
            Working_Pro.counter_data_new_dio()
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_close.Click
        Working_Pro.lvQrProductDefect.Items.Clear()
        Working_Pro.lvQrProduct.Items.Clear()
        Working_Pro.lvRemainmanual.Items.Clear()
        Working_Pro.RemainScanDmcAddQty = 0
        Working_Pro.RemainScanDmcDelQty = 0
        If closeLotsummary.Visible Then
            Working_Pro.cal_eff()
            closeLotsummary.setVariable()
            closeLotsummary.Enabled = True
        Else
            Working_Pro.Enabled = True
        End If
        If ins_qty.Visible Then
            ins_qty.pb_ok.Visible = True
        End If
        Me.Close()
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub clear_remain_Click(sender As Object, e As EventArgs) Handles clear_remain.Click
        Working_Pro.RemainScanDmc = 0
        closeLotsummary.Enabled = True
        Me.Close()
    End Sub

    Private Sub btn_close_Click(sender As Object, e As EventArgs)

    End Sub
End Class