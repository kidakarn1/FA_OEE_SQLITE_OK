Public Class Sel_prod_start
    Public Property SelectedMode As ProductionStartMode = ProductionStartMode.None

    Private Sub btnContinueBox_Click(sender As Object, e As EventArgs) Handles btnContinueBox.Click
        SelectedMode = ProductionStartMode.ContinueExistingBox
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnNewBox_Click(sender As Object, e As EventArgs) Handles btnNewBox.Click
        SelectedMode = ProductionStartMode.NewBox
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        SelectedMode = ProductionStartMode.None
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
End Class
