Imports System.Net
Imports System.Web.Script.Serialization
Friend Class defectSelectcode
    Shared Type = ""
    Shared Partfg = ""
    Shared datlvDefectcode As ListViewItem
    Shared mv = New manageVariable()
    Public Shared sPart As String = ""
    Public Shared swi As String = ""
    Public Shared sDefectcode As String = ""
    Public Shared sSeqSpc = "NO DATA"
    Public Shared sPwiSpc = "NO DATA"
    Public Shared mainCp = "NO DATA"
    Public Shared sDefectdetail As String = ""
    Public Shared dSelecttype As New defectSelecttype()
    Public Shared dSelecttypeSpc As New defectSpecialSelectFG()
    Public Shared S_index As Integer = 0
    Public Sub defectSelectcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dfHome As New defectHome
        If dfHome.dtType = "NC" Then
            '  lvDefectcode.BackColor = Color.Peru
        ElseIf dfHome.dtType = "NG" Then
            'lvDefectcode.BackColor = Color.Tomato
        End If
        If MainFrm.chk_spec_line = "2" Then
            If dSelecttype.type = "1" Then
                sPart = dSelecttypeSpc.dtItemcd
                lbPartfg.Text = sPart
                dSelecttype.sPart = dSelecttypeSpc.dtItemcd
            Else
                sPart = dSelecttype.sPart
                lbPartfg.Text = sPart
                dSelecttype.sPart = sPart
            End If
            lbPartfg.Text = dfHome.dtType
            lbType.Text = dfHome.dtType
            lbPartfg.Text = sPart
            getDefectcode()
        Else
            lbPartfg.Text = dfHome.dtType
            sPart = dSelecttype.sPart
            lbPartfg.Text = sPart
            lbType.Text = dfHome.dtType
            getDefectcode()
        End If
    End Sub
    Public Sub getDefectcode()
        Try
            If My.Computer.Network.Ping(Backoffice_model.svp_ping) Then
                Dim md = New modelDefect()
                Dim rsData = md.mGetdefectcode(MainFrm.Label4.Text)
                If rsData <> "0" Then
                    Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rsData)
                    Dim i As Integer = 1
                    For Each item As Object In dcResultdata
                        'MsgBox("==============>" & item("defect_cd").ToString())
                        datlvDefectcode = New ListViewItem(item("defect_cd").ToString())
                        datlvDefectcode.SubItems.Add(item("defect_name").ToString())
                        lvDefectcode.Items.Add(datlvDefectcode)
                        i += 1
                    Next
                    lvDefectcode.Items(0).Selected = True
                Else
                    Button1.Enabled = False
                    Button1.Visible = False
                End If
            Else
                load_show.Show()
            End If
        Catch ex As Exception
            load_show.Show()
        End Try
    End Sub
    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        tbnUp()
    End Sub
    Public Sub tbnUp()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvDefectcode.Items.Count - 1))) Then
            S_index = CDbl(Val((lvDefectcode.Items.Count - 1)))
        End If
        Try
            lvDefectcode.Items(S_index).Selected = False
            S_index -= 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf lvDefectcode.Items.Count > S_index Then
                '   S_index = CDbl(Val((lvDefectcode.Items.Count - 1)))
            End If
            lvDefectcode.Items(S_index).Selected = True
            lvDefectcode.Items(S_index).EnsureVisible()
            lvDefectcode.Select()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub tbnDown()
        If S_index < 0 Then
            S_index = 0
        ElseIf S_index > CDbl(Val((lvDefectcode.Items.Count - 1))) Then
            S_index = CDbl(Val((lvDefectcode.Items.Count - 1)))
        End If
        Try
            lvDefectcode.Items(S_index).Selected = False
            S_index += 1
            If S_index < 0 Then
                S_index = 0
                'ElseIf lvDefectcode.Items.Count > S_index Then
                '   S_index = CDbl(Val((lvDefectcode.Items.Count - 1)))
            End If
            lvDefectcode.Items(S_index).Selected = True
            lvDefectcode.Items(S_index).EnsureVisible()
            lvDefectcode.Select()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If MainFrm.chk_spec_line = "2" Then
            If dSelecttype.type = "1" Then
                'Dim objdSelectFGSPC As New defectSpecialSelectFG()
                'objdSelectFGSPC.Show()
                'Me.Close()
                Dim objdSelecttype As New defectSelecttype()
                objdSelecttype.Show()
                Me.Close()
            Else
                Dim objdSelecttype As New defectSelecttype()
                objdSelecttype.Show()
                Me.Close()
            End If
        Else
            Dim objdSelecttype As New defectSelecttype()
            objdSelecttype.Show()
            Me.Close()
        End If
    End Sub
    Public Shared Function checkDefectCodeSupplier(dfCode As String)
        If dfCode.ToString.Substring(0, 1) = "0" Then
            Return "1"
        Else
            Return "0"
        End If
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            For Each lvItem As ListViewItem In lvDefectcode.SelectedItems
                Me.sDefectcode = lvDefectcode.Items(lvItem.Index).SubItems(0).Text
                Me.sDefectdetail = lvDefectcode.Items(lvItem.Index).SubItems(1).Text
            Next
            If dSelecttype.type = "2" Then
                Dim rss = checkDefectCodeSupplier(Me.sDefectcode)
                If rss = "1" Then
                    Dim rs = OEE.OEE_EXP_CHECK_SUPP(defectSelectcode.sPart)
                    If rs <> "0" Then
                        Dim dcResultdata As Object = New JavaScriptSerializer().Deserialize(Of List(Of Object))(rs)
                        If CDbl(Val(dcResultdata(0)("OUTSIDE_TYP").ToString())) = 2 Then
                            Dim dfSupplier = New defectSelectSupplier
                            dfSupplier.show()
                            Me.Hide()
                        Else
                            Dim dfRegister = New defectRegister
                            dfRegister.GdfRegister = dfRegister
                            dfRegister.swi = swi
                            dfRegister.source_cd_supplier = ""
                            dfRegister.SeqSpc = sSeqSpc
                            dfRegister.PwiSpc = sPwiSpc
                            dfRegister.mainCP = mainCp
                            dfRegister.Show()
                            Me.Hide()
                        End If
                    End If
                Else
                    Dim dfRegister = New defectRegister
                    dfRegister.GdfRegister = dfRegister
                    dfRegister.swi = swi
                    dfRegister.SeqSpc = sSeqSpc
                    dfRegister.PwiSpc = sPwiSpc
                    dfRegister.mainCP = mainCp
                    dfRegister.source_cd_supplier = ""
                    dfRegister.Show()
                    Me.Hide()
                End If
            Else
                Dim dfRegister = New defectRegister
                dfRegister.GdfRegister = dfRegister
                dfRegister.swi = swi
                dfRegister.SeqSpc = sSeqSpc
                dfRegister.PwiSpc = sPwiSpc
                dfRegister.mainCP = mainCp
                dfRegister.source_cd_supplier = ""
                dfRegister.Show()
                Me.Hide()
            End If
        Catch ex As Exception
            '
        End Try
    End Sub
    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        tbnDown()
    End Sub
End Class