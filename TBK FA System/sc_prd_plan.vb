Imports System
Imports System.ComponentModel
Imports System.Threading
Imports System.IO.Ports
Public Class sc_prd_plan
    Dim myPort As Array
    Delegate Sub SetTextCallback(ByVal [text] As String)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = ""
        Insert_list.Enabled = True
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text.Length = 73 Then
                Insert_list.ListView1.Items.Clear()
                'MsgBox("OK")
                Dim line_cd As String = MainFrm.Label4.Text
                Dim wi_cd As String = TextBox1.Text.Substring(63, 10)
                Dim LoadSQL = Backoffice_model.get_prd_plan_fromsc(line_cd, wi_cd)

                Dim numberOfindex As Integer = 0
                While LoadSQL.Read()

                    'MsgBox(LoadSQL("WI").ToString())
                    'MsgBox(LoadSQL("prd_flg").ToString())
                    If LoadSQL("prd_flg").ToString() = 1 Then
                        'MsgBox("Red")
                        Insert_list.ListView1.ForeColor = Color.Red
                        Insert_list.ListView1.Items.Add(LoadSQL("WI").ToString()).SubItems.AddRange(New String() {LoadSQL("ITEM_CD").ToString(), LoadSQL("ITEM_NAME").ToString(), LoadSQL("QTY").ToString(), LoadSQL("remain_qty").ToString()})
                        Insert_list.ListView1.Items(numberOfindex).ForeColor = Color.Red
                    Else
                        'MsgBox("Blue")
                        Insert_list.ListView1.ForeColor = Color.Blue
                        Insert_list.ListView1.Items.Add(LoadSQL("WI").ToString()).SubItems.AddRange(New String() {LoadSQL("ITEM_CD").ToString(), LoadSQL("ITEM_NAME").ToString(), LoadSQL("QTY").ToString(), LoadSQL("remain_qty").ToString()})
                        Insert_list.ListView1.Items(numberOfindex).ForeColor = Color.Blue
                    End If
                    Insert_list.ListBox1.Items.Add(LoadSQL("PS_UNIT_NUMERATOR"))
                    Insert_list.ListBox2.Items.Add(LoadSQL("CT"))
                    Insert_list.ListBox3.Items.Add(LoadSQL("seq_count"))
                    Insert_list.lbx_dlv_date.Items.Add(LoadSQL("DLV_DATE"))
                    Insert_list.lbx_location.Items.Add(LoadSQL("LOCATION_PART"))
                    Insert_list.lbx_model.Items.Add(LoadSQL("MODEL"))
                    Insert_list.lbx_prd_type.Items.Add(LoadSQL("PRODUCT_TYP"))
                    numberOfindex = numberOfindex + 1
                    'Insert_list.ListView1.Items(0).ForeColor = Color.Red
                    'Insert_list.ListView1.ForeColor = Color.Red
                    'Insert_list.ListView1.Items.Add(LoadSQL("WI").ToString()).SubItems.AddRange(New String() {LoadSQL("ITEM_CD").ToString(), LoadSQL("ITEM_NAME").ToString(), LoadSQL("QTY").ToString(), LoadSQL("remain_qty").ToString()})
                    'line_id.Text = LoadSQL("line_id").ToString()
                End While
                'MsgBox(numberOfindex)

                If numberOfindex > 0 Then
                    Insert_list.ListView1.Items(0).Selected = True



                    'Working_Pro.Button1.Text = MainFrm.cavity.Text & " Qty."

                    Working_Pro.Label18.Text = Prd_detail.Label6.Text
                    Working_Pro.Label29.Text = Prd_detail.Label2.Text
                    Working_Pro.Label14.Text = Prd_detail.Label12.Text.Substring(0, 1)

                    'MsgBox(Prd_detail.Label12.Text.Substring(0, 1))
                    Dim numOfindex As Integer = Insert_list.ListView1.SelectedIndices(0)
                    'MsgBox(ListView1.SelectedIndices(0))
                    'MsgBox(ListView1.Items(10).Text.ToString)
                    'MsgBox(ListView1.Items(10).SubItems(1).Text.ToString)
                    'MsgBox(ListView1.Items(10).SubItems(2).Text.ToString)
                    'MsgBox(ListView1.Items(numOfindex).SubItems(0).Text.ToString)
                    Working_Pro.wi_no.Text = Insert_list.ListView1.Items(numOfindex).SubItems(0).Text.ToString
                    Working_Pro.Label3.Text = Insert_list.ListView1.Items(numOfindex).SubItems(1).Text.ToString
                    Working_Pro.Label12.Text = Insert_list.ListView1.Items(numOfindex).SubItems(2).Text.ToString
                    Working_Pro.Label8.Text = Insert_list.ListView1.Items(numOfindex).SubItems(3).Text.ToString
                    'Working_Pro.Label6.Text = ListView1.Items(numOfindex).SubItems(4).Text.ToString

                    'SNP
                    Working_Pro.Label27.Text = Insert_list.ListBox1.Items(numOfindex)

                    Dim cavi_ty As Integer = MainFrm.cavity.Text

                    'CT = ListBox2
                    Working_Pro.CycleTime.Text = Insert_list.ListBox2.Items(numOfindex)
                    Dim time_req As Double = Insert_list.ListBox2.Items(numOfindex) * Insert_list.ListView1.Items(numOfindex).SubItems(4).Text
                    Working_Pro.Label34.Text = time_req
                    Working_Pro.Label38.Text = Format((Insert_list.ListBox2.Items(numOfindex) * 60) * cavi_ty, "0.0")


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
                    If Insert_list.ListBox3.Items(numOfindex) < 10 Then
                        Working_Pro.Label22.Text = "0" & Insert_list.ListBox3.Items(numOfindex) + 1
                    Else
                        Working_Pro.Label22.Text = Insert_list.ListBox3.Items(numOfindex) + 1
                    End If

                    'DLV DATE
                    Working_Pro.lb_dlv_date.Text = Insert_list.lbx_dlv_date.Items(numOfindex)

                    'MODEL
                    Working_Pro.lb_model.Text = Insert_list.lbx_model.Items(numOfindex)

                    'LOCATION
                    Working_Pro.lb_location.Text = Insert_list.lbx_location.Items(numOfindex)

                    'PRODUCT_TYPE
                    'Working_Pro.lb_prd_type.Text = Insert_list.lbx_prd_type.Items(numOfindex)
                    Try
                        Working_Pro.lb_prd_type.Text = Insert_list.lbx_prd_type.Items(numOfindex)
                    Catch ex As Exception
                        Working_Pro.lb_prd_type.Text = "30"
                    End Try



                    Dim sum_progress As Integer = (Insert_list.ListView1.Items(numOfindex).SubItems(4).Text * 100) / Insert_list.ListView1.Items(numOfindex).SubItems(3).Text
                    sum_progress = 100 - sum_progress
                    'MsgBox(sum_progress)

                    Dim sum_act As Integer = Insert_list.ListView1.Items(numOfindex).SubItems(3).Text - Insert_list.ListView1.Items(numOfindex).SubItems(4).Text
                    Working_Pro.Label6.Text = sum_act

                    Dim sum_diff As String = Insert_list.ListView1.Items(numOfindex).SubItems(4).Text
                    sum_diff = "-" & sum_diff
                    Working_Pro.Label10.Text = sum_diff



                    Working_Pro.Label33.Text = Insert_list.ListView1.Items(numOfindex).SubItems(4).Text

                    Working_Pro.CircularProgressBar1.Text = sum_progress & "%"
                    Working_Pro.CircularProgressBar1.Value = sum_progress

                    Working_Pro.CircularProgressBar2.Text = 0 & "%"
                    Working_Pro.CircularProgressBar2.Value = 0

                    Working_Pro.Panel1.BackColor = Color.Red
                    Working_Pro.Label30.Text = "STOPPED"
                    Working_Pro.btn_start.Visible = True
                    Working_Pro.btn_stop.Visible = False
                    Insert_list.Enabled = True
                    Prd_detail.Enabled = True
                    Working_Pro.Show()
                    Me.Close()
                Else

                    Insert_list.ListView1.View = View.Details

                    'Dim line_cd As String = MainFrm.Label4.Text
                    Dim LoadSQL2 = Backoffice_model.get_prd_plan(line_cd)



                    Dim numberOfindex1 As Integer = 0
                    While LoadSQL2.Read()
                        'MsgBox(LoadSQL("WI").ToString().Length)
                        'MsgBox(LoadSQL("prd_flg").ToString())
                        If LoadSQL2("prd_flg").ToString() = 1 Then
                            'MsgBox("Red")
                            Insert_list.ListView1.ForeColor = Color.Red
                            Insert_list.ListView1.Items.Add(LoadSQL2("WI").ToString()).SubItems.AddRange(New String() {LoadSQL2("ITEM_CD").ToString(), LoadSQL2("ITEM_NAME").ToString(), LoadSQL2("QTY").ToString(), LoadSQL2("remain_qty").ToString()})
                            Insert_list.ListView1.Items(numberOfindex).ForeColor = Color.Red
                        Else
                            'MsgBox("Blue")
                            Insert_list.ListView1.ForeColor = Color.Blue
                            Insert_list.ListView1.Items.Add(LoadSQL2("WI").ToString()).SubItems.AddRange(New String() {LoadSQL2("ITEM_CD").ToString(), LoadSQL2("ITEM_NAME").ToString(), LoadSQL2("QTY").ToString(), LoadSQL2("remain_qty").ToString()})
                            Insert_list.ListView1.Items(numberOfindex).ForeColor = Color.Blue
                        End If
                        'Insert_list.ListBox1.Items.Add(LoadSQL2("PS_UNIT_NUMERATOR"))
                        'Insert_list.ListBox2.Items.Add(LoadSQL2("CT"))
                        'Insert_list.ListBox3.Items.Add(LoadSQL2("seq_count"))
                        'Insert_list.lbx_dlv_date.Items.Add(LoadSQL2("DLV_DATE"))
                        'Insert_list.lbx_location.Items.Add(LoadSQL2("LOCATION_PART"))
                        'Insert_list.lbx_model.Items.Add(LoadSQL2("MODEL"))
                        'Insert_list.lbx_prd_type.Items.Add(LoadSQL2("PRODUCT_TYP"))
                        numberOfindex1 = numberOfindex1 + 1
                        'Insert_list.ListView1.Items(0).ForeColor = Color.Red
                        'Insert_list.ListView1.ForeColor = Color.Red
                        'Insert_list.ListView1.Items.Add(LoadSQL("WI").ToString()).SubItems.AddRange(New String() {LoadSQL("ITEM_CD").ToString(), LoadSQL("ITEM_NAME").ToString(), LoadSQL("QTY").ToString(), LoadSQL("remain_qty").ToString()})
                        'line_id.Text = LoadSQL("line_id").ToString()
                    End While

                    Insert_list.Enabled = True
                    MsgBox("This production plan not found in the system.")
                    TextBox1.Text = ""
                End If








            Else
                Insert_list.Enabled = True
                MsgBox("Please Scan QR code on Instruction tag card only!")
                TextBox1.Text = ""

            End If


        End If
    End Sub

    Private Sub sc_prd_plan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        myPort = IO.Ports.SerialPort.GetPortNames()

        Dim sc_type As String = MainFrm.lb_scanner_port.Text

        If sc_type = "USB" Then
            'MsgBox("Data")
        Else
            If MainFrm.lb_ctrl_sc_flg.Text = "prdlist" Then
                Try
                    'MsgBox("sc prd")
                    SerialPort1.PortName = sc_type
                    SerialPort1.BaudRate = 9600
                    SerialPort1.Parity = IO.Ports.Parity.None
                    SerialPort1.StopBits = IO.Ports.StopBits.One
                    SerialPort1.DataBits = 8
                    SerialPort1.Open()
                Catch ex As Exception

                End Try
            End If


        End If
    End Sub

    Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        'If MainFrm.lb_ctrl_sc_flg.Text = "prdlist" Then
        'MsgBox("prdlist")
        ReceivedText(SerialPort1.ReadExisting())
        'End If
        'SerialPort1.Close()

    End Sub

    Private Sub ReceivedText(ByVal [text] As String)
        'MsgBox([text])
        Dim countss As Integer = 0
        If Me.TextBox1.InvokeRequired Then
            Dim x As New SetTextCallback(AddressOf ReceivedText)
            Me.Invoke(x, New Object() {(text)})
        Else
            Me.TextBox1.Text &= [text]
            'MsgBox(MainFrm.lb_ctrl_sc_flg.Text)
            Dim temp_str As String = [text]

            lb_text_buffer.Text = lb_text_buffer.Text + temp_str
            TextBox1.Text = lb_text_buffer.Text
            'lb_sc_count.Text = "1"

            If MainFrm.lb_ctrl_sc_flg.Text = "prdlist" Then

                If lb_text_buffer.Text.Length > 70 Then
                    'MsgBox("jing")

                    If TextBox1.Text.Length = "74" Then
                        'MsgBox("Tag jing")
                        Dim wi_cd As String = TextBox1.Text.Substring(63, 10)
                        Dim line_cd As String = MainFrm.Label4.Text
                        'MsgBox(wi_cd)
                        'MsgBox(line_cd)
                        Dim LoadSQL = Backoffice_model.get_prd_plan_fromsc(line_cd, wi_cd)

                        Dim numberOfindex As Integer = 0
                        While LoadSQL.Read()

                            MsgBox(LoadSQL("WI").ToString())
                            MsgBox(LoadSQL("prd_flg").ToString())

                            If LoadSQL("prd_flg").ToString() = 1 Then
                                'MsgBox("Red")
                                Insert_list.ListView1.ForeColor = Color.Red
                                Insert_list.ListView1.Items.Add(LoadSQL("WI").ToString()).SubItems.AddRange(New String() {LoadSQL("ITEM_CD").ToString(), LoadSQL("ITEM_NAME").ToString(), LoadSQL("QTY").ToString(), LoadSQL("remain_qty").ToString()})
                                Insert_list.ListView1.Items(numberOfindex).ForeColor = Color.Red
                            Else
                                'MsgBox("Blue")
                                Insert_list.ListView1.ForeColor = Color.Blue
                                Insert_list.ListView1.Items.Add(LoadSQL("WI").ToString()).SubItems.AddRange(New String() {LoadSQL("ITEM_CD").ToString(), LoadSQL("ITEM_NAME").ToString(), LoadSQL("QTY").ToString(), LoadSQL("remain_qty").ToString()})
                                Insert_list.ListView1.Items(numberOfindex).ForeColor = Color.Blue
                            End If
                            Insert_list.ListBox1.Items.Add(LoadSQL("PS_UNIT_NUMERATOR"))
                            Insert_list.ListBox2.Items.Add(LoadSQL("CT"))
                            Insert_list.ListBox3.Items.Add(LoadSQL("seq_count"))
                            Insert_list.lbx_dlv_date.Items.Add(LoadSQL("DLV_DATE"))
                            Insert_list.lbx_location.Items.Add(LoadSQL("LOCATION_PART"))
                            Insert_list.lbx_model.Items.Add(LoadSQL("MODEL"))
                            Insert_list.lbx_prd_type.Items.Add(LoadSQL("PRODUCT_TYP"))
                            numberOfindex = numberOfindex + 1
                            'Insert_list.ListView1.Items(0).ForeColor = Color.Red
                            'Insert_list.ListView1.ForeColor = Color.Red
                            'Insert_list.ListView1.Items.Add(LoadSQL("WI").ToString()).SubItems.AddRange(New String() {LoadSQL("ITEM_CD").ToString(), LoadSQL("ITEM_NAME").ToString(), LoadSQL("QTY").ToString(), LoadSQL("remain_qty").ToString()})
                            'line_id.Text = LoadSQL("line_id").ToString()
                        End While

                        If numberOfindex > 0 Then
                            Insert_list.ListView1.Items(0).Selected = True



                            'Working_Pro.Button1.Text = MainFrm.cavity.Text & " Qty."

                            Working_Pro.Label18.Text = Prd_detail.Label6.Text
                            Working_Pro.Label29.Text = Prd_detail.Label2.Text
                            Working_Pro.Label14.Text = Prd_detail.Label12.Text.Substring(0, 1)

                            'MsgBox(Prd_detail.Label12.Text.Substring(0, 1))
                            Dim numOfindex As Integer = Insert_list.ListView1.SelectedIndices(0)
                            'MsgBox(ListView1.SelectedIndices(0))
                            'MsgBox(ListView1.Items(10).Text.ToString)
                            'MsgBox(ListView1.Items(10).SubItems(1).Text.ToString)
                            'MsgBox(ListView1.Items(10).SubItems(2).Text.ToString)
                            'MsgBox(ListView1.Items(numOfindex).SubItems(0).Text.ToString)
                            Working_Pro.wi_no.Text = Insert_list.ListView1.Items(numOfindex).SubItems(0).Text.ToString
                            Working_Pro.Label3.Text = Insert_list.ListView1.Items(numOfindex).SubItems(1).Text.ToString
                            Working_Pro.Label12.Text = Insert_list.ListView1.Items(numOfindex).SubItems(2).Text.ToString
                            Working_Pro.Label8.Text = Insert_list.ListView1.Items(numOfindex).SubItems(3).Text.ToString
                            'Working_Pro.Label6.Text = ListView1.Items(numOfindex).SubItems(4).Text.ToString

                            'SNP
                            Working_Pro.Label27.Text = Insert_list.ListBox1.Items(numOfindex)

                            Dim cavi_ty As Integer = MainFrm.cavity.Text

                            'CT = ListBox2
                            Working_Pro.CycleTime.Text = Insert_list.ListBox2.Items(numOfindex)
                            Dim time_req As Double = Insert_list.ListBox2.Items(numOfindex) * Insert_list.ListView1.Items(numOfindex).SubItems(4).Text
                            Working_Pro.Label34.Text = time_req
                            Working_Pro.Label38.Text = Format((Insert_list.ListBox2.Items(numOfindex) * 60) * cavi_ty, "0.0")


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
                            If Insert_list.ListBox3.Items(numOfindex) < 10 Then
                                Working_Pro.Label22.Text = "0" & Insert_list.ListBox3.Items(numOfindex) + 1
                            Else
                                Working_Pro.Label22.Text = Insert_list.ListBox3.Items(numOfindex) + 1
                            End If

                            'DLV DATE
                            Working_Pro.lb_dlv_date.Text = Insert_list.lbx_dlv_date.Items(numOfindex)

                            'MODEL
                            Working_Pro.lb_model.Text = Insert_list.lbx_model.Items(numOfindex)

                            'LOCATION
                            Working_Pro.lb_location.Text = Insert_list.lbx_location.Items(numOfindex)

                            'PRODUCT_TYPE
                            'Working_Pro.lb_prd_type.Text = Insert_list.lbx_prd_type.Items(numOfindex)

                            Try
                                Working_Pro.lb_prd_type.Text = Insert_list.lbx_prd_type.Items(numOfindex)
                            Catch ex As Exception
                                Working_Pro.lb_prd_type.Text = "30"
                            End Try



                            Dim sum_progress As Integer = (Insert_list.ListView1.Items(numOfindex).SubItems(4).Text * 100) / Insert_list.ListView1.Items(numOfindex).SubItems(3).Text
                            sum_progress = 100 - sum_progress
                            'MsgBox(sum_progress)

                            Dim sum_act As Integer = Insert_list.ListView1.Items(numOfindex).SubItems(3).Text - Insert_list.ListView1.Items(numOfindex).SubItems(4).Text
                            Working_Pro.Label6.Text = sum_act

                            Dim sum_diff As String = Insert_list.ListView1.Items(numOfindex).SubItems(4).Text
                            sum_diff = "-" & sum_diff
                            Working_Pro.Label10.Text = sum_diff



                            Working_Pro.Label33.Text = Insert_list.ListView1.Items(numOfindex).SubItems(4).Text

                            Working_Pro.CircularProgressBar1.Text = sum_progress & "%"
                            Working_Pro.CircularProgressBar1.Value = sum_progress

                            Working_Pro.CircularProgressBar2.Text = 0 & "%"
                            Working_Pro.CircularProgressBar2.Value = 0

                            Working_Pro.Panel1.BackColor = Color.Red
                            Working_Pro.Label30.Text = "STOPPED"
                            Working_Pro.btn_start.Visible = True
                            Working_Pro.btn_stop.Visible = False
                            Insert_list.Enabled = True


                            Working_Pro.Show()

                            SerialPort1.Close()
                            Me.Close()


                        Else

                            Insert_list.ListView1.View = View.Details

                            'Dim line_cd As String = MainFrm.Label4.Text
                            Dim LoadSQL2 = Backoffice_model.get_prd_plan(line_cd)



                            Dim numberOfindex1 As Integer = 0
                            While LoadSQL2.Read()
                                'MsgBox(LoadSQL("WI").ToString().Length)
                                'MsgBox(LoadSQL("prd_flg").ToString())
                                If LoadSQL2("prd_flg").ToString() = 1 Then
                                    'MsgBox("Red")
                                    Insert_list.ListView1.ForeColor = Color.Red
                                    Insert_list.ListView1.Items.Add(LoadSQL2("WI").ToString()).SubItems.AddRange(New String() {LoadSQL2("ITEM_CD").ToString(), LoadSQL2("ITEM_NAME").ToString(), LoadSQL2("QTY").ToString(), LoadSQL2("remain_qty").ToString()})
                                    Insert_list.ListView1.Items(numberOfindex).ForeColor = Color.Red
                                Else
                                    'MsgBox("Blue")
                                    Insert_list.ListView1.ForeColor = Color.Blue
                                    Insert_list.ListView1.Items.Add(LoadSQL2("WI").ToString()).SubItems.AddRange(New String() {LoadSQL2("ITEM_CD").ToString(), LoadSQL2("ITEM_NAME").ToString(), LoadSQL2("QTY").ToString(), LoadSQL2("remain_qty").ToString()})
                                    Insert_list.ListView1.Items(numberOfindex).ForeColor = Color.Blue
                                End If
                                'Insert_list.ListBox1.Items.Add(LoadSQL2("PS_UNIT_NUMERATOR"))
                                'Insert_list.ListBox2.Items.Add(LoadSQL2("CT"))
                                'Insert_list.ListBox3.Items.Add(LoadSQL2("seq_count"))
                                'Insert_list.lbx_dlv_date.Items.Add(LoadSQL2("DLV_DATE"))
                                'Insert_list.lbx_location.Items.Add(LoadSQL2("LOCATION_PART"))
                                'Insert_list.lbx_model.Items.Add(LoadSQL2("MODEL"))
                                'Insert_list.lbx_prd_type.Items.Add(LoadSQL2("PRODUCT_TYP"))
                                numberOfindex1 = numberOfindex1 + 1
                                'Insert_list.ListView1.Items(0).ForeColor = Color.Red
                                'Insert_list.ListView1.ForeColor = Color.Red
                                'Insert_list.ListView1.Items.Add(LoadSQL("WI").ToString()).SubItems.AddRange(New String() {LoadSQL("ITEM_CD").ToString(), LoadSQL("ITEM_NAME").ToString(), LoadSQL("QTY").ToString(), LoadSQL("remain_qty").ToString()})
                                'line_id.Text = LoadSQL("line_id").ToString()
                            End While

                            Insert_list.Enabled = True
                            MsgBox("This production plan not found in the system.")
                            TextBox1.Text = ""
                        End If
                    ElseIf TextBox1.Text.Length = "14" Then
                        'MsgBox("14")
                    Else
                        'Insert_list.Enabled = True
                        MsgBox("Please Scan QR code on Instruction tag card only!")
                        TextBox1.Text = ""

                    End If

                End If

            End If



        End If

    End Sub


End Class