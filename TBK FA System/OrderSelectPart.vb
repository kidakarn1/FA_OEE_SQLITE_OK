Imports System.Web.Script.Serialization

Public Class OrderSelectPart
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub
    Private Sub btnSelect_Click(
    sender As Object,
    e As EventArgs
) Handles btnSelect.Click
        SelectCurrentPart()
        NumpadQtySet_Scan_Rm.Show()
    End Sub
    Private Sub btnUp_Click(
    sender As Object,
    e As EventArgs
) Handles btnUp.Click

        Try

            If lvOrderPartNo.Items.Count = 0 Then
                Exit Sub
            End If

            Dim currentIndex As Integer = 0

            'ถ้ามี Record ถูกเลือกอยู่
            If lvOrderPartNo.SelectedIndices.Count > 0 Then

                currentIndex =
                lvOrderPartNo.SelectedIndices(0)

            Else

                'ถ้ายังไม่ได้เลือก ให้เลือก Record แรก
                currentIndex = 0

            End If

            'เลื่อนขึ้น
            If currentIndex > 0 Then
                currentIndex -= 1
            End If


            'Clear Selection เดิม
            For Each item As ListViewItem In lvOrderPartNo.SelectedItems
                item.Selected = False
            Next


            'Select Record ใหม่
            lvOrderPartNo.Items(currentIndex).Selected = True
            lvOrderPartNo.Items(currentIndex).Focused = True

            'Scroll ให้เห็น Record
            lvOrderPartNo.Items(currentIndex).EnsureVisible()

            'Focus ListView
            lvOrderPartNo.Focus()

        Catch ex As Exception

            MsgBox(
            "Move Up Error : " &
            ex.Message
        )

        End Try

    End Sub
    Private Sub btnDown_Click(
    sender As Object,
    e As EventArgs
) Handles btnDown.Click

        Try

            If lvOrderPartNo.Items.Count = 0 Then
                Exit Sub
            End If

            Dim currentIndex As Integer = 0

            'ถ้ามี Record ถูกเลือก
            If lvOrderPartNo.SelectedIndices.Count > 0 Then

                currentIndex =
                lvOrderPartNo.SelectedIndices(0)

            Else

                currentIndex = 0

            End If


            'เลื่อนลง
            If currentIndex < lvOrderPartNo.Items.Count - 1 Then
                currentIndex += 1
            End If


            'Clear Selection เดิม
            For Each item As ListViewItem In lvOrderPartNo.SelectedItems
                item.Selected = False
            Next


            'Select Record ใหม่
            lvOrderPartNo.Items(currentIndex).Selected = True
            lvOrderPartNo.Items(currentIndex).Focused = True

            'Scroll ให้เห็น Record
            lvOrderPartNo.Items(currentIndex).EnsureVisible()

            'Focus
            lvOrderPartNo.Focus()

        Catch ex As Exception

            MsgBox(
            "Move Down Error : " &
            ex.Message
        )

        End Try

    End Sub
    '========================================================
    ' SELECT CURRENT RECORD
    '========================================================
    Public Sub SelectCurrentPart()
        Try
            If lvOrderPartNo.Items.Count = 0 Then
                MsgBox("Component Part Not Found.")
                Exit Sub
            End If

            'ถ้ายังไม่มีรายการถูกเลือก ให้เลือกแถวแรก
            If lvOrderPartNo.SelectedItems.Count = 0 Then

                lvOrderPartNo.Items(0).Selected = True
                lvOrderPartNo.Items(0).Focused = True
                lvOrderPartNo.Items(0).EnsureVisible()

            End If

            '========================================
            ' Column 1 = Part No.
            '========================================
            Dim partNo As String =
            lvOrderPartNo.SelectedItems(0).SubItems(1).Text.Trim()
            Dim partName As String =
            lvOrderPartNo.SelectedItems(0).SubItems(2).Text.Trim()
            Dim partModel As String =
            lvOrderPartNo.SelectedItems(0).SubItems(3).Text.Trim()
            'Set Part No ไปหน้า RM Scan
            NumpadQtySet_Scan_Rm.lbPartNumber.Text = partNo
            Rm_scan.lbPartNo.Text = partNo
            NumpadQtySet_Scan_Rm.lbPartNumber2.Text = partNo
            NumpadQtySet_Scan_Rm.lbPartName.Text = partName
            NumpadQtySet_Scan_Rm.lbModel.Text = partModel
            'Me.Close()

        Catch ex As Exception

            MsgBox(
            "Select Part Error : " &
            ex.Message
        )

        End Try

    End Sub

    Public Sub loadComponecePart()
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then

                '====================================================
                ' Setup ListView
                '====================================================
                lvOrderPartNo.Clear()

                lvOrderPartNo.View = View.Details
                lvOrderPartNo.FullRowSelect = True
                lvOrderPartNo.MultiSelect = False
                lvOrderPartNo.ForeColor = Color.Black

                '====================================================
                ' Hide Column Header
                '====================================================
                lvOrderPartNo.HeaderStyle = ColumnHeaderStyle.None

                '====================================================
                ' Create Columns
                ' Header ถูกกำหนดไว้ แต่ไม่แสดง
                '====================================================
                lvOrderPartNo.Columns.Add("No", 65)
                lvOrderPartNo.Columns.Add("Part No", 180)
                lvOrderPartNo.Columns.Add("Part Name", 300)
                lvOrderPartNo.Columns.Add("Model", 200)


                '====================================================
                ' API Object
                '====================================================
                Dim api = New api()


                '====================================================
                ' Call API
                '====================================================
                Dim result = Backoffice_model.GetCP(Prd_detail.lb_wi.Text)
                '====================================================
                ' Check Result
                '====================================================
                If String.IsNullOrWhiteSpace(result) Then

                    MsgBox("Component Part Not Found.")

                    Exit Sub

                End If


                '====================================================
                ' Convert JSON
                '====================================================
                Dim serializer As New JavaScriptSerializer()

                Dim rows As Object() =
                serializer.Deserialize(Of Object())(result)


                '====================================================
                ' For Each API Result
                '====================================================
                Dim No As Integer = 1
                For Each obj As Object In rows
                    Dim row As Dictionary(Of String, Object) =
                    DirectCast(
                        obj,
                        Dictionary(Of String, Object)
                    )


                    Dim itemCd As String = ""
                    Dim itemName As String = ""
                    Dim model As String = ""


                    '================================================
                    ' ITEM_CD
                    '================================================
                    If row.ContainsKey("ITEM_CD") AndAlso
                   row("ITEM_CD") IsNot Nothing Then

                        itemCd = row("ITEM_CD").ToString()

                    End If


                    '================================================
                    ' ITEM_NAME
                    '================================================
                    If row.ContainsKey("ITEM_NAME") AndAlso
                   row("ITEM_NAME") IsNot Nothing Then

                        itemName = row("ITEM_NAME").ToString()

                    End If


                    '================================================
                    ' MODEL
                    '================================================
                    If row.ContainsKey("MODEL") AndAlso
                   row("MODEL") IsNot Nothing Then

                        model = row("MODEL").ToString()

                    End If


                    '================================================
                    ' Add To ListView
                    '================================================
                    Dim item As New ListViewItem(No)
                    item.SubItems.Add(itemCd)
                    item.SubItems.Add(itemName)
                    item.SubItems.Add(model)
                    No = No + 1
                    'เก็บข้อมูล API ทั้ง Row
                    item.Tag = row

                    lvOrderPartNo.Items.Add(item)

                Next


                '====================================================
                ' Check No Data
                '====================================================
                If lvOrderPartNo.Items.Count = 0 Then

                    MsgBox("Component Part Not Found.")

                End If


            Else

                MsgBox("Cannot connect to server.")

            End If


        Catch ex As Exception

            MsgBox(
            "Load Component Part Error : " &
            ex.Message
        )

        End Try

    End Sub
    Private Sub OrderSelectPart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadComponecePart()
    End Sub
End Class