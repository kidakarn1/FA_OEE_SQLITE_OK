Public Enum ProductionStartMode
    None = 0
    NewBox = 1
    ContinueExistingBox = 2
    ReprintIncompleteTag = 3
End Enum

' Carries the operator's choice from the Main production menu until the
' production-plan context (Part No., SNP, WI and next process) is available.
Public Module ProductionStartFlowState
    Public Property SelectedMode As ProductionStartMode = ProductionStartMode.None
    Public Property SelectedBox As IncompleteBoxRecord
    ' Separate from normal Continue selection.  It is an in-memory hand-off
    ' only; selecting recovery never claims, releases, or changes the HBL.
    Public Property SelectedOldActiveRecovery As IncompleteTransferCrashRecoveryRecord
    Public Property OldActiveRecoverySeedApplied As Boolean

    Public Sub Reset(Optional releaseClaim As Boolean = False)
        ' Continue Existing Box no longer creates a flg_control = 2 claim.
        ' Keep the parameter for existing callers, but never rewrite a legacy
        ' status while clearing only this in-memory hand-off state.
        SelectedMode = ProductionStartMode.None
        SelectedBox = Nothing
        SelectedOldActiveRecovery = Nothing
        OldActiveRecoverySeedApplied = False
    End Sub
End Module

Public Class IncompleteBoxRecord
    Public Property TagId As Integer
    Public Property Wi As String
    Public Property PwiId As String
    Public Property LineCode As String
    Public Property PartNo As String
    Public Property PartName As String
    Public Property Model As String
    Public Property LotNo As String
    Public Property SeqNo As String
    Public Property BoxNo As Integer
    Public Property Quantity As Integer
    Public Property Snp As Integer
    Public Property Shift As String
    Public Property NextProcess As String
    ' Read-only source status from tag_print_detail.  It is used only to make
    ' NEW Continue eligibility observable; recovery uses its durable history
    ' owner and does not depend on this display value.
    Public Property FlgControl As String
    Public Property QrDetail As String
    Public Property CreatedDate As DateTime
    Public Property PrintCount As Integer

    Public ReadOnly Property RemainingQuantity As Integer
        Get
            If Snp <= 0 Then Return 0
            Return Math.Max(0, Snp - Quantity)
        End Get
    End Property
End Class

' Keeps packaging state separate from production actual state.
' Phase 1 only introduces the state holder. Counter logic must opt in explicitly.
Public NotInheritable Class ProductionResumeContext
    Public Property Mode As ProductionStartMode = ProductionStartMode.None
    Public Property SelectedBox As IncompleteBoxRecord
    Public Property ProductionActualAtResume As Integer
    Public Property ProductionGoodAtResume As Integer
    ' The Current production WI is distinct from the selected historical source.
    Public Property CurrentWiAtResume As String
    ' Immutable source identity captured from the selected TagId after its
    ' existing server revalidation. This is display/trace state only.
    Public Property SourceWiAtResume As String
    ' Option A keeps historical source-box identity separate from the Current
    ' production-box identity. CurrentBoxNo is state preparation only until a
    ' later persistence contract explicitly supports it.
    Public Property SourceBoxNo As Integer
    Public Property CurrentBoxNo As Integer
    Public Property CurrentPwiAtResume As String
    Public Property CurrentSeqAtResume As String
    Public Property BackendTransferId As Integer
    Public Property DurableActiveConfirmed As Boolean
    ' Accepted rework is physically packaged but is not added to the legacy
    ' lb_good counter.  Keep only the post-resume contribution here so it never
    ' changes Production Actual or historical Good.
    Public Property AcceptedReworkPackagingQuantity As Integer
    ' The normal Current-WI sequence is kept separately while a historical box
    ' number is temporarily reused for Continue Existing Box.
    Public Property NormalBoxCounterBeforeResume As Integer
    Public Property NormalBatchCounterBeforeResume As Integer
    ' Position of the Current-WI normal box before its counter is temporarily
    ' seeded with the selected historical source box number.
    Public Property NormalBoxQuantityBeforeResume As Integer
    Public Property SourceBoxCompleted As Boolean
    Public Property IsServerValidated As Boolean
    Public Property IsClaimed As Boolean

    Public ReadOnly Property IsResumeActive As Boolean
        Get
            Return Mode = ProductionStartMode.ContinueExistingBox AndAlso
                   SelectedBox IsNot Nothing AndAlso
                   IsServerValidated
        End Get
    End Property

    Public ReadOnly Property BaseBoxQuantity As Integer
        Get
            If SelectedBox Is Nothing Then Return 0
            Return Math.Max(0, SelectedBox.Quantity)
        End Get
    End Property

    Public Sub Reset()
        Mode = ProductionStartMode.None
        SelectedBox = Nothing
        ProductionActualAtResume = 0
        ProductionGoodAtResume = 0
        CurrentWiAtResume = String.Empty
        SourceWiAtResume = String.Empty
        SourceBoxNo = 0
        CurrentBoxNo = 0
        CurrentPwiAtResume = String.Empty
        CurrentSeqAtResume = String.Empty
        BackendTransferId = 0
        DurableActiveConfirmed = False
        AcceptedReworkPackagingQuantity = 0
        NormalBoxCounterBeforeResume = 0
        NormalBatchCounterBeforeResume = 0
        NormalBoxQuantityBeforeResume = 0
        SourceBoxCompleted = False
        IsServerValidated = False
        IsClaimed = False
    End Sub

    Public Sub BeginResume(box As IncompleteBoxRecord,
                           productionActual As Integer,
                           productionGood As Integer,
                           serverValidated As Boolean,
                           claimed As Boolean,
                           Optional currentWi As String = "",
                           Optional normalBoxCounter As Integer = 0,
                           Optional normalBatchCounter As Integer = 0,
                           Optional normalBoxQuantity As Integer = 0)
        If box Is Nothing Then Throw New ArgumentNullException(NameOf(box))

        Mode = ProductionStartMode.ContinueExistingBox
        SelectedBox = box
        ProductionActualAtResume = Math.Max(0, productionActual)
        ProductionGoodAtResume = Math.Max(0, productionGood)
        CurrentWiAtResume = If(currentWi, String.Empty).Trim()
        SourceWiAtResume = If(box.Wi, String.Empty).Trim()
        SourceBoxNo = Math.Max(0, box.BoxNo)
        CurrentBoxNo = 1
        CurrentPwiAtResume = String.Empty
        CurrentSeqAtResume = String.Empty
        BackendTransferId = 0
        DurableActiveConfirmed = False
        AcceptedReworkPackagingQuantity = 0
        NormalBoxCounterBeforeResume = Math.Max(0, normalBoxCounter)
        NormalBatchCounterBeforeResume = Math.Max(0, normalBatchCounter)
        NormalBoxQuantityBeforeResume = Math.Max(0, normalBoxQuantity)
        SourceBoxCompleted = False
        IsServerValidated = serverValidated
        IsClaimed = claimed
    End Sub

    Public Sub MarkSourceBoxCompleted()
        SourceBoxCompleted = True
    End Sub

    Public Sub AddAcceptedReworkPackagingQuantity(quantity As Integer)
        If quantity <= 0 Then Return
        AcceptedReworkPackagingQuantity += quantity
    End Sub
End Class
