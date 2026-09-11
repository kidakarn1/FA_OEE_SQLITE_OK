Public Class Chang_Admin_Shift
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        defectAdminsearch.LbShift.Text = "A (08:00 - 17:00)"
        defectAdminsearch.Enabled = True
        'defectAdminsearchnc.Show()
        Me.Close()
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        defectAdminsearch.LbShift.Text = "B (20:00 - 05:00)"
        defectAdminsearch.Enabled = True
        'defectAdminsearchnc.Show()
        Me.Close()
        'defectAdminsearchnc.Show()
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        defectAdminsearch.LbShift.Text = "P (08:00 - 20:00)"
        defectAdminsearch.Enabled = True
        defectAdminsearch.Show()
        Me.Close()
    End Sub

    Private Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        defectAdminsearch.LbShift.Text = "Q (20:00 - 08:00)"
        defectAdminsearch.Enabled = True
        'defectAdminsearchnc.Show()
        Me.Close()
    End Sub

    Private Sub Button55_Click(sender As Object, e As EventArgs) Handles Button55.Click
        defectAdminsearch.LbShift.Text = "M (17:00 - 20:00)"
        defectAdminsearch.Enabled = True
        'defectAdminsearchnc.Show()
        Me.Close()
    End Sub

    Private Sub Button66_Click(sender As Object, e As EventArgs) Handles Button66.Click
        'Dim defectAdminsearchnc As New defectAdminsearchnc
        defectAdminsearch.LbShift.Text = "N (05:00 - 08:00)"
        defectAdminsearch.Enabled = True
        'defectAdminsearchnc.Show()
        Me.Close()
    End Sub

    Private Sub Button77_Click(sender As Object, e As EventArgs) Handles Button77.Click
        ' Dim defectAdminsearchnc As New defectAdminsearchnc
        defectAdminsearch.LbShift.Text = "S (17:00 - 02:00)"
        defectAdminsearch.Enabled = True
        'defectAdminsearchnc.Show()
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' check_shift()
    End Sub
    Public Sub check_shift()
        Dim timeShift As String = DateTime.Now.ToString("HH")
        Dim time_now As String = DateTime.Now.ToString("HH:mm:ss tt")
        Dim defaultShift As String = ""
        If timeShift = "05" Or timeShift = "06" Or timeShift = "07" Then
            Button44.Visible = True
            Button44_hide.Visible = False
            Button66.Visible = True
            Button66_hide.Visible = False

            Button11.Visible = False
            Button11_hide.Visible = True
            Button22.Visible = False
            Button22_hide.Visible = True
            Button33.Visible = False
            Button33_hide.Visible = True
            Button55.Visible = False
            Button55_hide.Visible = True
            Button77.Visible = False
            Button77_hide.Visible = True
        End If
        If timeShift = "08" Or timeShift = "09" Or timeShift = "10" Or timeShift = "11" Or timeShift = "12" Or timeShift = "13" Or timeShift = "14" Or timeShift = "15" Or timeShift = "16" Then
            Button11.Visible = True
            Button11_hide.Visible = False
            Button33.Visible = True
            Button33_hide.Visible = False

            Button44.Visible = False
            Button44_hide.Visible = True
            Button22.Visible = False
            Button22_hide.Visible = True
            Button66.Visible = False
            Button66_hide.Visible = True
            Button55.Visible = False
            Button55_hide.Visible = True
            Button77.Visible = False
            Button77_hide.Visible = True
        End If

        If timeShift = "17" Or timeShift = "18" Or timeShift = "19" Then
            Button77.Visible = True
            Button77_hide.Visible = False
            Button33.Visible = True
            Button33_hide.Visible = False

            Button44.Visible = False
            Button44_hide.Visible = True
            Button22.Visible = False
            Button22_hide.Visible = True
            Button11.Visible = False
            Button11_hide.Visible = True
            Button55.Visible = False
            Button55_hide.Visible = True
            Button66.Visible = False
            Button66_hide.Visible = True
        End If
        If timeShift = "02" Or timeShift = "03" Or timeShift = "04" Then
            Button22.Visible = True
            Button22_hide.Visible = False
            Button44.Visible = True
            Button44_hide.Visible = False
            Button77.Visible = False
            Button77_hide.Visible = True
            Button33.Visible = False
            Button33_hide.Visible = True
            Button66.Visible = False
            Button66_hide.Visible = True
            Button11.Visible = False
            Button11_hide.Visible = True
            Button55.Visible = False
            Button55_hide.Visible = True
        End If
        If timeShift = "20" Or timeShift = "21" Or timeShift = "22" Or timeShift = "23" Or timeShift = "24" Or timeShift = "00" Or timeShift = "01" Then
            Button44.Visible = True
            Button44_hide.Visible = False
            Button22.Visible = True
            Button22_hide.Visible = False
            Button77.Visible = True
            Button77_hide.Visible = False

            Button66.Visible = False
            Button66_hide.Visible = True
            Button55.Visible = False
            Button55_hide.Visible = True
            Button11.Visible = False
            Button11_hide.Visible = True
            Button33.Visible = False
            Button33_hide.Visible = True
        End If
    End Sub
    Private Sub Chang_sh_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        check_shift()
    End Sub

End Class