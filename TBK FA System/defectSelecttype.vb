Imports System.Net
Imports System.Web.Script.Serialization
Friend Class defectSelecttype
    Public Shared type
    Shared pFG
    Shared wi
    Public Shared dtType
    Public Shared dt_menu
    Public Shared datlvChildpart As ListViewItem
    Public Shared sPart As String = ""
    Public Shared actTotal As Integer = Working_Pro.LB_COUNTER_SEQ.Text 'Working_Pro.Label6.Text
    Public Shared ncTotal As Integer = Working_Pro.lb_nc_qty.Text
    Public Shared ngTotal As Integer = Working_Pro.lb_ng_qty.Text
    Public Shared mv = New manageVariable()
    Public Shared S_index As Integer = 0
    Dim SelectSpcSeq = "NO DATA"
    Public Shared maincp = "NO DATA"
    Dim SelectSpcPWI_ID = "NO DATA"
    Public Shared Sub setVariable()
        actTotal = Working_Pro.LB_COUNTER_SEQ.Text
        ncTotal = Working_Pro.lb_nc_qty.Text
        ngTotal = Working_Pro.lb_ng_qty.Text
        Dim pd As New Working_Pro()
        Dim dfHome As New defectHome
        type = dfHome.dtType
        pFG = pd.pFg
        wi = pd.wiNo '"5100287204"
        sPart = ""
    End Sub
    Private Sub defectSelecttype_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim mdDefect = New modelDefect
        If mdDefect.mGetDataEnableFGPart(MainFrm.Label4.Text) = "0" Then
            btnPartfg.Enabled = False
            ' btnPartfg.Visible = False
        Else
            btnPartfg.Enabled = True
            'btnPartfg.Visible = True
        End If
        Dim dfHpme As New defectHome()
        If dfHpme.dtType = "NC" Then
            'lvChildpart.BackColor = Color.Peru
        ElseIf dfHpme.dtType = "NG" Then
            'lvChildpart.BackColor = Color.Tomato
        End If
        lvChildpart.Scrollable = True
        setVariable()
        Dim dfHome As New defectHome
        lbType.Text = dfHome.dtType
        If MainFrm.chk_spec_line = "2" Then
            btnPartfg.Text = "SELECT FG"
        Else
            btnPartfg.Text = pFG
        End If

        Dim reData = getChildpart(wi)
    End Sub
    Public Function getChildpart(wi)
        Dim md = New modelDefect()
        Dim rsData
        If MainFrm.chk_spec_line = "2" Then
            Dim arrayWI As List(Of String) = New List(Of String)
            For Each itemPlanData As DataPlan In MainFrm.ArrayDataPlan
                arrayWI.Add(itemPlanData.wi)
            Next
            rsData = md.mGetchildpartSpc(arrayWI.ToArray)
        Else
            rsData = md.mGetchildpart(wi)
        End If
        If rsData <> "0" Then
            Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsData) 'error
            Dim i As Integer = 1
            Dim index As Integer = 0
            Dim checkRs As Integer = 0
            Dim GenSEQ As Integer = CInt(Working_Pro.Label22.Text) - MainFrm.ArrayDataPlan.ToArray().Length
            Dim Iseq = GenSEQ
            Dim tmpWi = ""
            For Each item As Object In dcResultdata
                datlvChildpart = New ListViewItem(i)
                datlvChildpart.SubItems.Add(item("ITEM_CD").ToString())
                datlvChildpart.SubItems.Add(item("ITEM_NAME").ToString())
                datlvChildpart.SubItems.Add(item("WI").ToString())
                If MainFrm.chk_spec_line = "2" Then
                    If tmpWi <> item("WI").ToString() Then
                        tmpWi = item("WI").ToString()
                        Iseq += 1
                        index += 1
                    Else
                        checkRs = 0
                    End If
                    datlvChildpart.SubItems.Add(Iseq)
                    ' If checkRs = 1 Then
                    '
                    'MsgBox("index ===>" & index)
                    'End If
                    datlvChildpart.SubItems.Add(Working_Pro.Spwi_id(index - 1))
                Else
                    datlvChildpart.SubItems.Add(Working_Pro.seqNo)
                    datlvChildpart.SubItems.Add(Working_Pro.pwi_id)
                End If
                ' datlvChildpart.SubItems.Add(item("ODR_SEQ").ToString())
                datlvChildpart.SubItems.Add("1")
                lvChildpart.Items.Add(datlvChildpart)
                i += 1
            Next
        End If
    End Function
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim dfHome As New defectHome
        dfHome.Show()
        Me.Close()
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        'Me.Close()
        'Dim dfHome = New defectHome()
        'dfHome.show()
        tbnUp()
    End Sub
    Public Sub tbnUp()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvChildpart.Items.Count - 1))) Then
            S_index = CDbl(Val((lvChildpart.Items.Count - 1)))
        End If
        Try
            lvChildpart.Items(S_index).Selected = False
            S_index -= 1
            If S_index < 0 Then
                S_index = 0
                ' ElseIf lvChildpart.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvCvhildpart.Items.Count - 1)))
            End If
            lvChildpart.Items(S_index).Selected = True
            lvChildpart.Items(S_index).EnsureVisible()
            lvChildpart.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub tbnDown()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvChildpart.Items.Count - 1))) Then
            S_index = CDbl(Val((lvChildpart.Items.Count - 1)))
        End If
        Try
            lvChildpart.Items(S_index).Selected = False
            S_index += 1
            If S_index < 0 Then
                S_index = 0
                ' ElseIf lvChildpart.Items.Count > S_index Then
                '    S_index = CDbl(Val((lvChildpart.Items.Count - 1)))
            End If
            lvChildpart.Items(S_index).Selected = True
            lvChildpart.Items(S_index).EnsureVisible()
            lvChildpart.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub lvChildpart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvChildpart.SelectedIndexChanged
        For Each lvItem As ListViewItem In lvChildpart.SelectedItems
            Me.sPart = lvChildpart.Items(lvItem.Index).SubItems(1).Text
            SelectSpcSeq = lvChildpart.Items(lvItem.Index).SubItems(4).Text
            SelectSpcPWI_ID = lvChildpart.Items(lvItem.Index).SubItems(5).Text
            wi = lvChildpart.Items(lvItem.Index).SubItems(3).Text
            'maincp = lvChildpart.Items(lvItem.Index).SubItems(6).Text
        Next
        dt_menu = "1"
        If type = "NG" Then
            dtType = "1"
        ElseIf type = "NC" Then
            dtType = "2"
        End If
        type = "2"
        ' mv.setSelectpartdefect("TEST OK")
    End Sub
    Private Sub btnPartfg_Click(sender As Object, e As EventArgs) Handles btnPartfg.Click
        If type = "NG" Then
            dtType = "1"
        ElseIf type = "NC" Then
            dtType = "2"
        End If
        type = "1"
        dt_menu = "1"
        If MainFrm.chk_spec_line = "2" Then
            Dim mdDefect = New modelDefect
            If mdDefect.mGetDataEnableFGPart(MainFrm.Label4.Text) = "1" Then
                Dim dfSS As New defectSpecialSelectFG()
                ' Me.dfSS = btnPartfg.Text
                dfSS.Show()
                Me.Hide()
            Else
                MsgBox("Please Enable Line Special.")
                'Dim sDefectcode As New defectSelectcode()
                'Me.sPart = btnPartfg.Text
                'lvChildpart.Items(0).Selected = True
                'lvChildpart.Select()
            End If
        Else
            Dim sDefectcode As New defectSelectcode()
            Me.sPart = btnPartfg.Text
            sDefectcode.Show()
            Me.Hide()
        End If
    End Sub
    Private Sub HScrollBar1_Scroll(sender As Object, e As ScrollEventArgs)
        'create two scroll bars
        Dim hs As HScrollBar
        Dim vs As VScrollBar
        hs = New HScrollBar()
        vs = New VScrollBar()

        'set properties
        hs.Location = New Point(10, 200)
        hs.Size = New Size(175, 15)
        hs.Value = 50
        vs.Location = New Point(200, 30)
        vs.Size = New Size(15, 175)
        hs.Value = 50

        'adding the scroll bars to the form
        Me.Controls.Add(hs)
        Me.Controls.Add(vs)
        ' Set the caption bar text of the form.  
        Me.Text = "tutorialspoint.com"
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub lbType_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        tbnDown()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        If lvChildpart.SelectedItems.Count > 0 Then
            Dim sDefectcode As New defectSelectcode()
            sDefectcode.sSeqSpc = SelectSpcSeq
            sDefectcode.sPwiSpc = SelectSpcPWI_ID
            sDefectcode.swi = wi
            maincp = modelDefect.mGetCalPartOEE(MainFrm.Label4.Text, btnPartfg.Text, Me.sPart, type, MainFrm.chk_spec_line, MainFrm.Label6.Text)
            sDefectcode.mainCp = maincp
            sDefectcode.Show()
            Me.Hide()
        Else
            MsgBox("PLEASE SELECT ROW. ")
        End If
    End Sub
End Class