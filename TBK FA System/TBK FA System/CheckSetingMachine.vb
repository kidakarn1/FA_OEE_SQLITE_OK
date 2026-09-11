Imports System.IO.Ports
Imports System.Web.Script.Serialization
Imports NationalInstruments.DAQmx

Public Class CheckSetingMachine
    Private digitalTask As Task
    Private writer As DigitalSingleChannelWriter
    Private serialPort As SerialPort
    Private OpenLED As Boolean
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        tbEmpCode.Text = ""
        Me.Close()
    End Sub
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        KeyboardsLeaderSetMachine.ShowDialog()
    End Sub
    Public Sub sentParameterLossIO(Boolean_value As Boolean)
        Try
            ' สร้าง Task
            digitalTask = New NationalInstruments.DAQmx.Task()

            digitalTask.DOChannels.CreateChannel(
            "Dev1/port2/line0",       ' พอร์ตที่จะสั่งงาน
            "",
            ChannelLineGrouping.OneChannelForAllLines
        )

            writer = New DigitalSingleChannelWriter(digitalTask.Stream)

            ' ส่งค่า True/False ออกพอร์ต
            writer.WriteSingleSampleSingleLine(True, Boolean_value)

            OpenLED = Boolean_value

            Console.WriteLine("LED (P1.0)")

        Catch ex As DaqException
            MessageBox.Show("DAQ Error: " & ex.Message)

            'If SerialPort.IsOpen Then
            'serialPort.Close()
            'End If
        End Try
    End Sub

    Public Sub checkPermissionLeader(statusAction As Integer)
        Dim tempp As String = ""
        Dim emp_cd As String = ""
        Dim name_sur As String = ""
        If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
            If tbEmpCode.Text.Length = 5 Then
                Dim LoadSQL = Backoffice_model.chk_user_skill_line(Trim(tbEmpCode.Text), MainFrm.Label4.Text)
                Dim mp_adm_control_flg As String = ""
                Dim mp_prod_control_flg As String = ""
                ' While LoadSQL.Read()
                'tempp = LoadSQL("sug_id").ToString()
                'emp_cd = LoadSQL("emp_id").ToString()
                'name_sur = LoadSQL("fname").ToString() & " " & LoadSQL("lname").ToString()
                'End While
                If LoadSQL <> "0" Then
                    Dim dict3 As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(LoadSQL)
                    For Each item As Object In dict3
                        mp_adm_control_flg = item("mp_adm_control_flg").ToString()
                        mp_prod_control_flg = item("mp_prod_control_flg").ToString()
                        Backoffice_model.user_pd = item("dep_cd").ToString()
                        emp_cd = item("sau_username").ToString()
                        emp_name = item("sau_fname").ToString() & " " & item("sau_lname").ToString()
                    Next
                End If
                If mp_adm_control_flg = "1" Then
                    If statusAction = 0 Then ' หมายถึง Confrime หน้า แรก
                        SetingMaching.EmpCodeLeader.Text = emp_cd
                        SetingMaching.LossCode.Text = Loss_reg.Label7.Text
                        SetingMaching.StartLoss.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        SetingMaching.EndLoss.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        SetingMaching.LossCD.Text = Loss_reg.loss_cd.Text
                        Dim line_cd = MainFrm.Label4.Text
                        start_loss = SetingMaching.StartLoss.Text
                        end_loss = SetingMaching.EndLoss.Text
                        created_by = emp_cd
                        updated_by = emp_cd
                        status_flg = 0
                        sentParameterLossIO(True)
                        Backoffice_model.insert_info_loss_setting_machine(line_cd, start_loss, end_loss, created_by, updated_by, Working_Pro.pwi_id, status_flg, SetingMaching.LossCD.Text)
                        SetingMaching.Show()
                    Else ' หมายถึง Confrime หน้า Step 2 
                        Dim line_cd = MainFrm.Label4.Text
                        start_loss = SetingMaching.StartLoss.Text
                        end_loss = SetingMaching.EndLoss.Text
                        created_by = emp_cd
                        updated_by = emp_cd
                        status_flg = 1
                        sentParameterLossIO(False)
                        Backoffice_model.updated_info_loss_setting_machine(line_cd, start_loss, end_loss, created_by, updated_by, Working_Pro.pwi_id, status_flg, SetingMaching.LossCD.Text)
                        SetingMaching.Close()
                    End If
                    Me.Close()
                Else
                    MsgBox("Not Permission")
                    tbEmpCode.Text = ""
                    tbEmpCode.Focus()
                    'TextBox1.Select()
                End If
            Else
                MsgBox("Not Permission")
                'msgBox("Can't to login! Please scan your employee card.")
                tbEmpCode.Text = ""
                tbEmpCode.Focus()
                tbEmpCode.Select()
                ' Me.Close()
            End If
        Else
            load_show.Show()
        End If
    End Sub
    Private Sub tbEmpCode_KeyDown(sender As Object, e As KeyEventArgs) Handles tbEmpCode.KeyDown
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                If e.KeyCode = Keys.Enter Then
                    checkPermissionLeader(Backoffice_model.statusActionSetingMachine)
                End If
            Else
                load_show.Show()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class