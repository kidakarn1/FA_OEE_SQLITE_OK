Public Class Api_Scan_FA
    Public Shared Function M_Insert_QR_Product(pwi_id As Integer, qrProduct As String, line_cd As String, idaid_production_date As String, statusAction As String)
        Try
            Dim api = New api()
            Dim GetData = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/Insert_QR_Product?pwi_id=" & pwi_id & "&qrProduct=" & qrProduct & "&line_cd=" & line_cd & "&idaid_production_date=" & idaid_production_date & "&statusAction=" & statusAction)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/Insert_QR_Product?pwi_id=" & pwi_id & "&qrProduct=" & qrProduct & "&line_cd=" & line_cd & "&idaid_production_date=" & idaid_production_date & "&statusAction=" & statusAction)
            If GetData <> "0" Then
                Return GetData
            Else
                MsgBox("Insert Faill")
                Return 0
            End If
        Catch ex As Exception
            MsgBox("Error Function M_Insert_QR_Product In Api_Scan_FA")
        End Try
    End Function
    Public Shared Function M_Updated_QR_Product(iqp_id As Integer, qr_product As String, statusDefect As String, pwi_id As String)
        Try
            Dim api = New api()
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/Update_status_defect_qr_product?iqp_id=" & iqp_id & "&qrProduct=" & qr_product & "&statusDefect=" & statusDefect & "&pwi_id=" & pwi_id)
            Dim GetData = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/Update_status_defect_qr_product?iqp_id=" & iqp_id & "&qrProduct=" & qr_product & "&statusDefect=" & statusDefect & "&pwi_id=" & pwi_id)
            If GetData <> "0" Then
                Return GetData
            Else
                MsgBox("Insert Faill")
                Return 0
            End If
        Catch ex As Exception
            MsgBox("Error Function M_Updated_QR_Product In Api_Scan_FA")
        End Try
    End Function
    Public Shared Function M_Updated_status_info_qr(iqp_id As Integer, qr_product As String, statusAction As String, pwi_id As String)
        Try
            Dim api = New api()
            '  Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/Updated_status_info_qr?iqp_id=" & iqp_id & "&qrProduct=" & qr_product & "&statusAction=" & statusAction & "&pwi_id=" & pwi_id)
            Dim GetData = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/Updated_status_info_qr?iqp_id=" & iqp_id & "&qrProduct=" & qr_product & "&statusAction=" & statusAction & "&pwi_id=" & pwi_id)
            If GetData <> "0" Then
                Return GetData
            Else
                MsgBox("Insert Faill")
                Return 0
            End If
        Catch ex As Exception
            MsgBox("Error Function M_Updated_status_info_qr In Api_Scan_FA")
        End Try
    End Function

    Public Shared Function M_CheckCondionFA_Scan(line_cd As String, part_no As String, qrProduct As String, iqp_status As String, iqp_defect As String, statusGroup As Integer, pwi_id As String)
        Try
            Dim api = New api()
            Dim rsCheckCondition = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/CheckCondionFA_Scan?line_cd=" & line_cd & "&part_no=" & part_no & "&qrProduct=" & qrProduct & "&iqp_status=" & iqp_status & "&iqp_defect=" & iqp_defect & "&statusGroup=" & statusGroup & "&pwi_id=" & pwi_id)
            ' Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/CheckCondionFA_Scan?line_cd=" & line_cd & "&part_no=" & part_no & "&qrProduct=" & qrProduct & "&iqp_status=" & iqp_status & "&iqp_defect=" & iqp_defect & "&statusGroup=" & statusGroup & "&pwi_id=" & pwi_id)
            Return rsCheckCondition
        Catch ex As Exception
            MsgBox("Error Function M_CheckCondionFA_Scan In Api_Scan_FA")
        End Try
    End Function
    Public Shared Function M_getDatainfo_qr_product(qrProduct As String)
        Try
            Dim api = New api()
            Dim rs = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/getDatainfo_qr_product?qrProduct=" & qrProduct)
            'Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/getDatainfo_qr_product?qrProduct=" & qrProduct)
            Return rs
        Catch ex As Exception
            MsgBox("Error Function M_CheckCondionFA_Scan In Api_Scan_FA")
        End Try
    End Function
    Public Shared Function M_checkStatus_info_qr(qrProduct As String)
        Try
            Dim api = New api()
            Dim rs = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/checkStatus_info_qr?qrProduct=" & qrProduct)
            ' Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/checkStatus_info_qr?qrProduct=" & qrProduct)
            Return rs
        Catch ex As Exception
            MsgBox("Error Function M_checkStatus_info_qr In Api_Scan_FA")
        End Try
    End Function
    Public Shared Function M_Get_iqp_id(qrProduct As String)
        Try
            Dim api = New api()
            Dim rs = api.Load_data("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/Get_iqp_id?qrProduct=" & qrProduct)
            '  Console.WriteLine("http://" & Backoffice_model.svApi & "/API_NEW_FA/index.php/Api_Scan_QR_Product/Get_iqp_id?qrProduct=" & qrProduct)
            Return rs
        Catch ex As Exception
            MsgBox("Error Function M_Get_iqp_id In Api_Scan_FA")
        End Try
    End Function
End Class