Public Class Show_reprint_wi

    Public g_index As Integer = 0
    Public Sub ListView_WI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView_WI.SelectedIndexChanged
        If IsNothing(Me.ListView_WI.FocusedItem) Then
        ElseIf ListView_WI.FocusedItem.Index >= 0 Then
            If ListView_WI.Items.Count > 0 Then
                g_index = 0
                Dim index As Integer = ListView_WI.FocusedItem.Index
                g_index = index
                hide_wi_select.Text = ListView_WI.Items(g_index).SubItems(1).Text
            End If
        Else
            MessageBox.Show("An Error has halted thid process")
        End If
    End Sub
    Private Sub load_wi()
        Dim reader = Backoffice_model.get_data_wi_reprint(Scan_reprint.date_now_start, Scan_reprint.date_now_end, MainFrm.Label4.Text)
        Dim i As Integer = 1
        Try
            While reader.read()
                x = New ListViewItem(i)
                x.SubItems.Add(reader("WI").ToString)
                x.SubItems.Add(reader("UD").ToString)
                ListView_WI.Items.Add(x)
                i += 1
            End While
        Catch ex As Exception
            MsgBox("NO DATA REPRINT")
        End Try
    End Sub
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles pcBack.Click
        Adm_manage.Show()
        Me.Close()
    End Sub
    Private Sub Show_reprint_wi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        hide_wi_select.Visible = False
        load_wi()
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles pcOK.Click
        'load_data_show_of()
        MenuReprint.Show()
        Me.Hide()
    End Sub
    Public Sub load_data_show_of()
        tag_reprint_new.ListView1.Items.Clear()
        tag_reprint_new.ListBox2.Items.Clear()
        Dim num As Integer = 0
        Dim reader
        Dim check_format_tag As String = Backoffice_model.B_check_format_tag()
        If check_format_tag = "1" Then ' for tag_type = '2' and tag_issue_flg = '2'  OR K1M183
            Dim LoadSQL_BATCH = Backoffice_model.get_tag_reprint_batch(hide_wi_select.Text) ' by batch for k1m083
            While LoadSQL_BATCH.Read()
                num = num + 1
                tag_reprint_new.ListView1.View = View.Details
                tag_reprint_new.ListView1.Items.Add(num).SubItems.AddRange(New String() {"(BATCH)" & LoadSQL_BATCH("updated_date").ToString(), LoadSQL_BATCH("tag_qr_detail").ToString().Substring(95, 3), LoadSQL_BATCH("shift").ToString(), CDbl(Val(LoadSQL_BATCH("tag_qr_detail").ToString().Substring(100, 3))), LoadSQL_BATCH("next_proc").ToString()})
                tag_reprint_new.ListBox1.Items.Add(LoadSQL_BATCH("tag_qr_detail").ToString())
                tag_reprint_new.ListBox2.Items.Add(LoadSQL_BATCH("shift").ToString())
                tag_reprint_new.ListBox2.Items.Add(LoadSQL_BATCH("tag_qr_detail").ToString().Substring(100, 3))
            End While
            LoadSQL_BATCH.close()
            reader = Backoffice_model.get_tag_reprint_spaceial(hide_wi_select.Text) ' by box for k1m083 
        Else
            reader = Backoffice_model.get_tag_reprint_detail(hide_wi_select.Text)
        End If
        'Dim reader_sum = Backoffice_model.get_tag_reprint_sum_detail(hide_wi_select.Text)
        Dim reader_qr_detail As String = ""
        Dim reader_updated_date As String = ""
        Dim reader_seq_no As String = ""
        Dim reader_shift As String = ""
        Dim reader_box_no As String = ""
        Dim sum_qty As String = ""
        'If Backoffice_model.check_line_reprint() = "0" Then
        While reader.Read()
            num = num + 1
            'tag_reprint_new.ListView1.View = View.Details

            Dim date_act As String = reader("qr_detail").Substring(44, 8)
            Dim actual_date = (Trim(date_act.Substring(0, 4))) & "-" & (Trim(date_act.Substring(4, 2))) & "-" & (Trim(date_act.Substring(6)))
            tag_reprint_new.ListView1.Items.Add(num).SubItems.AddRange(New String() {actual_date, reader("seq_no").ToString(), reader("shift").ToString(), reader("box_no").ToString(), reader("next_proc").ToString()})
            tag_reprint_new.ListBox1.Items.Add(reader("qr_detail"))
            tag_reprint_new.ListBox2.Items.Add(reader("shift"))
            tag_reprint_new.ListBox2.Items.Add(reader("box_no"))
        End While
        reader.close()
        'Else
        '	While reader.Read()
        '		If reader("box_no") > 0 Then
        '		sum_qty = CDbl(Val(sum_qty)) + 1'
        'reader_qr_detail = reader("qr_detail")
        'reader_updated_date = reader("updated_date").ToString()
        '	reader_seq_no = reader("seq_no").ToString()
        '		reader_shift = reader("shift").ToString()
        '		reader_box_no = "001" 'reader("box_no").ToString()

        '		End If
        '		End While
        '		reader.close()
        'Dim before_qr_code As String = reader_qr_detail.Substring(0, 52)
        'Dim after_qr_code As String = reader_qr_detail.Substring(58)
        'Dim new_qty As String = ""
        'If Len(sum_qty) = "1" Then
        'new_qty = "     " & sum_qty
        '	ElseIf Len(sum_qty) = "2" Then
        '		new_qty = "    " & sum_qty
        'ElseIf Len(sum_qty) = "3" Then
        'new_qty = "   " & sum_qty
        'ElseIf Len(sum_qty) = "4" Then
        'new_qty = "  " & sum_qty
        'ElseIf Len(sum_qty) = "5" Then
        'new_qty = " " & sum_qty
        'ElseIf Len(sum_qty) = "6" Then
        'new_qty = sum_qty
        'End If
        'Dim result_qty As String = before_qr_code & new_qty & after_qr_code
        'reader.close()
        'num = num + 1
        'tag_reprint_new.ListView1.View = View.Details
        'tag_reprint_new.ListView1.Items.Add(num).SubItems.AddRange(New String() {reader_updated_date, reader_seq_no, reader_shift, reader_box_no})
        'tag_reprint_new.ListBox1.Items.Add(result_qty)
        'tag_reprint_new.ListBox2.Items.Add(reader_shift)
        'tag_reprint_new.ListBox2.Items.Add(reader_box_no)
        'End If

        Dim reader1 = Backoffice_model.get_tag_reprint_detail_genarate(hide_wi_select.Text)
        While reader1.Read()
            num = num + 1
            tag_reprint_new.ListView1.View = View.Details
            tag_reprint_new.ListView1.Items.Add(num).SubItems.AddRange(New String() {reader1("updated_date").ToString(), reader1("seq_no").ToString(), reader1("shift").ToString(), reader1("box_no").ToString()})
            tag_reprint_new.ListView1.Items(num - 1).BackColor = Color.Red
            tag_reprint_new.ListBox1.Items.Add(reader1("new_qr_detail"))
            tag_reprint_new.ListBox2.Items.Add(reader1("shift"))
            tag_reprint_new.ListBox2.Items.Add(reader1("box_no"))
        End While
        tag_reprint_new.Show()
        Me.Hide()
    End Sub
End Class
