Imports System.Drawing
Imports System.Collections.Generic

Public Class Chang_cavity
    Private closingAfterSelection As Boolean
    Private ReadOnly cavityButtons As New List(Of Button)()

    Private Sub Chang_cavity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildCavityButtons()
        SelectCurrentCavity()
    End Sub

    Private Sub BuildCavityButtons()
        For Each staleControl As Control In pnlOptions.Controls
            staleControl.Dispose()
        Next
        pnlOptions.Controls.Clear()
        cavityButtons.Clear()

        Const columnCount As Integer = 4
        Const buttonWidth As Integer = 111
        Const buttonHeight As Integer = 54
        Const gap As Integer = 1

        For value As Integer = 1 To 20
            Dim cavityButton As New Button With {
                .BackColor = Color.FromArgb(2, 10, 14),
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Catamaran", 30.0!, FontStyle.Bold),
                .ForeColor = Color.White,
                .Name = "btnCavity" & value.ToString(),
                .Size = New Size(buttonWidth, buttonHeight),
                .TabIndex = value - 1,
                .Tag = value,
                .Text = value.ToString(),
                .UseVisualStyleBackColor = False
            }
            cavityButton.FlatAppearance.BorderColor = Color.LightGray
            cavityButton.FlatAppearance.BorderSize = 1
            Dim column As Integer = (value - 1) Mod columnCount
            Dim row As Integer = (value - 1) \ columnCount
            cavityButton.Location = New Point(column * (buttonWidth + gap), row * (buttonHeight + gap))
            AddHandler cavityButton.Click, AddressOf CavityButton_Click
            cavityButtons.Add(cavityButton)
            pnlOptions.Controls.Add(cavityButton)
        Next
    End Sub

    Private Sub SelectCurrentCavity()
        Dim currentValue As Integer
        If Not Integer.TryParse(Trim(MainFrm.cavity.Text), currentValue) OrElse currentValue < 1 Then
            currentValue = 1
        End If
        SetSelectedValue(currentValue)
    End Sub

    Private Sub CavityButton_Click(sender As Object, e As EventArgs)
        Dim selectedButton = DirectCast(sender, Button)
        Dim selectedValue As Integer = Convert.ToInt32(selectedButton.Tag)
        SetSelectedValue(selectedValue)
        ApplySelectedValue(selectedValue)
    End Sub

    Private Sub SetSelectedValue(value As Integer)
        For Each cavityButton As Button In cavityButtons
            Dim isSelected = Convert.ToInt32(cavityButton.Tag) = value
            cavityButton.ForeColor = If(isSelected, Color.SpringGreen, Color.White)
            cavityButton.FlatAppearance.BorderColor = If(isSelected, Color.SpringGreen, Color.LightGray)
            cavityButton.FlatAppearance.BorderSize = If(isSelected, 2, 1)
        Next
        lblSelectedCavity.Text = value.ToString() & " PCS / CYCLE"
    End Sub

    Private Sub ApplySelectedValue(value As Integer)
        Try
            ' Use the existing Configuration/MainFrm cavity value and save path.
            MainFrm.cavity.Text = value.ToString()
            Backoffice_model.saveLineConfig(
                MainFrm.Label6.Text,
                MainFrm.Label4.Text,
                MainFrm.count_type.Text,
                value,
                MainFrm.lb_scanner_port.Text,
                MainFrm.lb_printer_port.Text,
                MainFrm.lb_dio_port.Text,
                "")

            'Use the same master-sync API as Configuration so the server-side
            'counter master is updated together with line_detail.cavity.
            Dim masterSyncResult As String = Line_conf.SyncControlMaster(value, False)
            If masterSyncResult Is Nothing Then
            MessageBox.Show("บันทึกค่าในเครื่องแล้ว แต่ยังเชื่อมต่อ Master ไม่สำเร็จ", "Setting", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            closingAfterSelection = True
            Prd_detail.RefreshCavityDisplay()
            Prd_detail.Enabled = True
            Prd_detail.Show()
            Prd_detail.Activate()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("ไม่สามารถบันทึกค่าได้: " & ex.Message, "Setting", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub Chang_cavity_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If Not closingAfterSelection Then
            Prd_detail.Enabled = True
            Prd_detail.RefreshCavityDisplay()
            Prd_detail.Show()
        End If
    End Sub
End Class
