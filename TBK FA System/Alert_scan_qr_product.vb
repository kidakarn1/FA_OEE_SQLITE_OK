Public Class Alert_scan_qr_product
    Private Sub btn_close_Click(sender As Object, e As EventArgs) Handles btn_close.Click
        Me.DialogResult = DialogResult.OK
        ScanQRprod.Enabled = True
        Me.Close()
    End Sub
End Class