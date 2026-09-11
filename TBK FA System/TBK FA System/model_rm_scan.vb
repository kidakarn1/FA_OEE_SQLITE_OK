Public Class model_rm_scan
    Public Shared Function GetDataRM_DATABASE_FA(TagFG As String)
        Dim api = New api()
        Dim result_api_checkper = api.Load_data("http://" & Backoffice_model.svApi & "/linenotify_fa/Alert_loss_fa/notify_fa?wi=" & wi_plan & "&flg_control=" & flg_control & "&pd=" & pd & "&loss_id=" & loss_id)
    End Function
End Class
