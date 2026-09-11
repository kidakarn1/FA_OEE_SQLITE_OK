# Offline tests of the actual packaging/latch methods, compiled in an isolated
# host. Never load Working_Pro itself: its static initializer performs HTTP I/O.
$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path $PSScriptRoot -Parent
$workingSource = Get-Content -LiteralPath (Join-Path $projectRoot 'TBK FA System\Working_Pro.vb') -Raw
$modelSource = Get-Content -LiteralPath (Join-Path $projectRoot 'TBK FA System\Backoffice_model.vb') -Raw
$contextSource = Get-Content -LiteralPath (Join-Path $projectRoot 'TBK FA System\ProductionResumeContext.vb') -Raw
# The framework CodeDom compiler predates NameOf; preserve its constant value.
$contextSource = $contextSource.Replace('NameOf(box)', '"box"')
$dtoSource = $modelSource.Substring($modelSource.IndexOf('Public Class IncompleteTransferApiRecord'))
$dtoSource = $dtoSource.Substring(0, $dtoSource.IndexOf('Public Class Backoffice_model'))
$methodNames = @('IsOldActiveRecoveryFull', 'IsSelectedOldActiveRecoveryBox', 'PrepareOldRecoveryFull',
    'CompleteOldRecoveryFullHandoff', 'GetLiveCurrentBoxQuantity', 'ApplyNormalPackagingRebase',
    'BeginNormalPackagingRebaseAfterSourceCompletion', 'IsNormalPackagingRebaseForCurrentWi',
    'ClearNormalPackagingRebase', 'GetPackagingQuantity', 'IsNormalNewBoxPackagingBaselineForCurrentContext',
    'ClearNormalNewBoxPackagingBaseline', 'GetResumePackagingGoodQuantity', 'GetResumePackagingActualQuantity',
    'IsResumeContextForCurrentWi', 'AreEquivalentNumericIdentity', 'DeferResumeSourceTagUntilActualPersistence',
    'MarkDeferredSourceActualAccepted', 'CommitDeferredSourceTagAfterActualPersistence',
    'ProcessDeferredNormalOverflowAfterSourceCommit', 'ClearDeferredSourceTagAfterActual',
    'GetCompletedTagQuantities', 'CanAcceptContinueCounterInput')
$methods = foreach ($methodName in $methodNames) {
    $pattern = '(?ms)^    (?:Public|Private) (?:Function|Sub) ' + [regex]::Escape($methodName) + '\(.*?^    End (?:Function|Sub)'
    $match = [regex]::Match($workingSource, $pattern)
    if (-not $match.Success) { throw "Missing production method: $methodName" }
    $match.Value
}
$fieldPattern = '(?m)^    Private (?:ReadOnly )?_(?:resumeContext|oldRecovery\w*|normalPackaging\w*|normalAcceptedReworkPackagingQuantity|normalNewBox\w*|resumeNormalBoxQuantityAtRebase|deferred\w*|continueTagPersistenceFailed|pendingContinueTagPersistence)\b[^\r\n]*'
$fields = [regex]::Matches($workingSource, $fieldPattern) | ForEach-Object { $_.Value }
$requestClass = [regex]::Match($workingSource, '(?ms)^    Private NotInheritable Class ContinueTagPersistenceRequest.*?^    End Class').Value
$persistenceBranch = [regex]::Match($workingSource, '(?ms)^                If _oldRecoveryFullPending Then\r?\n                    If Not IsOldActiveRecoveryFull\(\).*?(?=^                ElseIf isFullSourceReplacement AndAlso isDurableContinue Then)').Value
if (-not $persistenceBranch) { throw 'Recovery /complete branch not found' }
$persistenceMethod = "    Private Sub RunRecoveryPersistenceOnly()`n        Dim plan_seq As String = Label22.Text.PadLeft(3, ""0""c)`n        Dim qr_detailsss As String = CurrentPayload`n" + $persistenceBranch + "`n                End If`n    End Sub`n"
$hostPrefix = @'
Option Infer On
Imports System
Imports Microsoft.VisualBasic
Imports System.Linq
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Forms
Public Class show_detail_production
    Public Sub RefreshOldIncompleteResumeDisplay()
    End Sub
End Class
Public Class CounterLabel
    Public Property Text As String = "0"
End Class
Public Class Backoffice_model
    Public Shared Reject As Boolean
    Public Shared LastQr As String
    Public Shared LastHbl As Integer
    Public Shared LastSource As Integer
    Public Shared LastQty As Integer
    Public Shared Function CompleteIncompleteTransfer(hbl As Integer, source As Integer, wi As String,
        pwi As String, seq As String, box As Integer, snp As Integer, qr As String, shift As String,
        flag As Integer, item As String, group As String, qty As Integer, nextProc As String,
        ByRef tagId As Integer, ByRef reason As String, ByRef already As Boolean) As Boolean
        LastQr = qr : LastHbl = hbl : LastSource = source : LastQty = qty
        If box <> 1 OrElse snp <> 480 OrElse pwi <> "2689" OrElse CInt(seq) <> 5 Then Throw New Exception("wrong completion identity")
        If Reject Then
            reason = "simulated rejection"
            Return False
        End If
        tagId = 77
        Return True
    End Function
End Class
Public Class HandoffTestHost
    Private Enum PackagingQuantityBasis
        TotalActual = 0
        GoodActual = 1
    End Enum
    Public check_tag_type As String = "1"
    Public pwi_id As String = "2689"
    Public GoodQty As Integer
    Public Label6 As New CounterLabel, lb_good As New CounterLabel, Label22 As New CounterLabel
    Public Label27 As New CounterLabel, Label3 As New CounterLabel, Label18 As New CounterLabel
    Public Label14 As New CounterLabel
    Public tag_group_no As String = "1", Gobal_NEXT_PROCESS As String = "next"
    Public CurrentPayload As String = "retained-current-full-tag-payload"
    Public wi_no As New CounterLabel, lb_box_count As New CounterLabel, Label_bach As New CounterLabel
    Public lb_qty_for_box As New CounterLabel
    Public RejectComplete As Boolean
    Public CompletionAttempts As Integer
    Public Printed As Integer
    Public NormalPrinted As Integer
    Private Function IsDurableContinueFullCompletionPending() As Boolean
        Return False
    End Function
    Private Function ResolveDurableContinueFullCompletion() As Boolean
        Throw New Exception("Unexpected normal Continue retry")
    End Function
    Private Function HasPendingContinuePersistenceForCurrentSource() As Boolean
        Return False
    End Function
    Private Function ResolvePendingContinueTagPersistence() As Boolean
        Throw New Exception("Unexpected normal replacement retry")
    End Function
    Private Sub ClearPendingContinueTagPersistence()
        _continueTagPersistenceFailed = False
    End Sub
    ' Only the HTTP/printer boundary is simulated. Eligibility, acceptance gate,
    ' source/tail split, failure retention, rebase, and overflow are real methods.
    Private Sub tag_print()
        If Not _oldRecoveryFullPending Then
            NormalPrinted += 1
            Return
        End If
        CompletionAttempts += 1
        Backoffice_model.Reject = RejectComplete
        RunRecoveryPersistenceOnly()
        If _continueTagPersistenceFailed OrElse _oldRecoveryFullTagId <= 0 Then Return
        Printed += 1
        CompleteOldRecoveryFullHandoff()
    End Sub
    Private Sub FailOldRecoveryFull(reason As String)
        _continueTagPersistenceFailed = True
    End Sub
'@
$hostTests = @'
    Private Shared Sub Check(condition As Boolean, name As String)
        If Not condition Then Throw New Exception("FAIL: " & name)
    End Sub
    Private Shared Function Fresh() As HandoffTestHost
        Dim h As New HandoffTestHost
        h.Label22.Text = "05" : h.Label27.Text = "480" : h.wi_no.Text = "5100427641"
        ProductionStartFlowState.Reset()
        ProductionStartFlowState.SelectedMode = ProductionStartMode.NewBox
        ProductionStartFlowState.OldActiveRecoverySeedApplied = True
        ProductionStartFlowState.SelectedOldActiveRecovery = New IncompleteTransferCrashRecoveryRecord With {
            .DetailQuerySucceeded = True, .Transfer = New IncompleteTransferApiRecord With {
            .TransferId = 6, .SourceTagId = 99, .BaseQty = 475, .CurrentWi = h.wi_no.Text,
            .CurrentPwi = h.pwi_id, .CurrentSeq = "05", .CurrentSnp = 480, .CurrentBoxNo = 1, .Flag = 0}}
        Return h
    End Function
    Public Shared Function Run() As String
        Dim h = Fresh()
        h.lb_qty_for_box.Text = "480"
        Check(h.IsOldActiveRecoveryFull(), "valid rolled anchor eligible")
        ProductionStartFlowState.SelectedOldActiveRecovery.Transfer.Flag = 1
        Check(Not h.IsOldActiveRecoveryFull(), "completed anchor excluded")
        ProductionStartFlowState.SelectedOldActiveRecovery.Transfer.Flag = 0
        h.wi_no.Text = "different"
        Check(Not h.IsOldActiveRecoveryFull(), "wrong WI excluded")
        h.wi_no.Text = "5100427641" : h.Label22.Text = "06"
        Check(Not h.IsOldActiveRecoveryFull(), "wrong sequence excluded")
        h.Label22.Text = "05" : h.lb_box_count.Text = "2"
        Check(Not h.IsOldActiveRecoveryFull(), "BOX002 excluded")

        For Each overflow As Integer In New Integer() {0, 5}
            h = Fresh()
            h.lb_qty_for_box.Text = (480 + overflow).ToString()
            Check(h.GetCompletedTagQuantities(0, 5 + overflow, 480).SequenceEqual(New Integer() {480}), "one source boundary")
            h.DeferResumeSourceTagUntilActualPersistence(480, 5 + overflow, 5 + overflow, overflow, False)
            Check(h.lb_qty_for_box.Text = "480" AndAlso h._oldRecoveryFullOverflow = overflow, "split source and tail")
            h.CommitDeferredSourceTagAfterActualPersistence()
            Check(h.CompletionAttempts = 0, "wait for accepted counters")
            h.Label6.Text = (5 + overflow).ToString() : h.lb_good.Text = h.Label6.Text
            h.CommitDeferredSourceTagAfterActualPersistence()
            Check(h.Printed = 1 AndAlso h.lb_box_count.Text = "1", "one committed BOX001")
            Check(ProductionStartFlowState.SelectedOldActiveRecovery Is Nothing AndAlso Not ProductionStartFlowState.OldActiveRecoverySeedApplied, "recovery cleared")
            Check(h.GetLiveCurrentBoxQuantity() = overflow, "normal tail retained")
            Check(CInt(h.Label6.Text) = 5 + overflow, "Actual contains new movement only")
            Check(Not h._resumeContext.IsResumeActive AndAlso ProductionStartFlowState.SelectedMode = ProductionStartMode.NewBox, "no fabricated Continue")
            h.Label6.Text = (15 + overflow).ToString() : h.lb_good.Text = h.Label6.Text
            h.lb_qty_for_box.Text = (10 + overflow).ToString()
            Check(h.GetResumePackagingGoodQuantity(CInt(h.lb_good.Text)) = 10 + overflow, "next normal production")
        Next

        h = Fresh() : h.lb_qty_for_box.Text = "485" : h.RejectComplete = True
        h.Label6.Text = "10" : h.lb_good.Text = "10"
        h.DeferResumeSourceTagUntilActualPersistence(480, 10, 10, 5, True)
        h.CommitDeferredSourceTagAfterActualPersistence()
        Check(h.CompletionAttempts = 0, "manual explicit acceptance required")
        h.MarkDeferredSourceActualAccepted() : h.CommitDeferredSourceTagAfterActualPersistence()
        Dim retainedQr As String = Backoffice_model.LastQr
        Check(h.Printed = 0 AndAlso h.lb_box_count.Text = "0" AndAlso h.lb_qty_for_box.Text = "480", "failure neither prints nor advances")
        Check(h._oldRecoveryFullPending AndAlso ProductionStartFlowState.SelectedOldActiveRecovery IsNot Nothing, "failure retains recovery")
        Check(Not h.CanAcceptContinueCounterInput(), "failure blocks new input")
        h.CurrentPayload = "a newly generated payload must not replace the first request"
        h.RejectComplete = False
        Check(h.CanAcceptContinueCounterInput() AndAlso h.Printed = 1 AndAlso h.lb_qty_for_box.Text = "5", "retry hands accepted tail off once")
        Check(Backoffice_model.LastQr = retainedQr AndAlso Backoffice_model.LastHbl = 6 AndAlso Backoffice_model.LastSource = 99 AndAlso Backoffice_model.LastQty = 480, "exact payload and same HBL on retry")

        h = Fresh() : h.lb_qty_for_box.Text = "485" : h.Label6.Text = "10" : h.lb_good.Text = "0"
        h.DeferResumeSourceTagUntilActualPersistence(480, 10, 0, 5, False)
        h.CommitDeferredSourceTagAfterActualPersistence()
        Check(h.GetResumePackagingGoodQuantity(0) = 5, "accepted rework tail independent of Good")

        h = Fresh() : h.lb_qty_for_box.Text = "965" : h.Label6.Text = "490" : h.lb_good.Text = "490"
        h.DeferResumeSourceTagUntilActualPersistence(480, 490, 490, 485, False)
        h.CommitDeferredSourceTagAfterActualPersistence()
        Check(h.Printed = 1 AndAlso h.NormalPrinted = 1 AndAlso h.lb_box_count.Text = "2" AndAlso h.lb_qty_for_box.Text = "5", "multi-boundary normal overflow")

        h = Fresh() : ProductionStartFlowState.Reset()
        Check(Not h.IsOldActiveRecoveryFull() AndAlso h.CanAcceptContinueCounterInput(), "normal NewBox unaffected")
        h.Label6.Text = "7" : h.lb_good.Text = "7"
        Check(h.GetResumePackagingActualQuantity(7) = 7, "normal quantity unchanged")
        h._resumeContext.BeginResume(New IncompleteBoxRecord With {.TagId = 1, .Quantity = 475, .Snp = 480, .PartNo = "p"}, 0, 0, True, False, h.wi_no.Text)
        h.Label3.Text = "p" : h.Label6.Text = "10" : h.lb_good.Text = "10"
        h.BeginNormalPackagingRebaseAfterSourceCompletion()
        h._resumeContext.Reset()
        Check(h.GetResumePackagingGoodQuantity(10) = 5, "normal Continue shared rebase unchanged")
        Return "PASS: eligibility, exact full, overflow, acceptance, failure latch/retry, rework tail, NewBox, normal Continue rebase"
    End Function
End Class
'@
$source = $hostPrefix + "`n" + $requestClass + "`n" + $persistenceMethod + "`n" + ($fields -join "`n") + "`n" + ($methods -join "`n") + "`n" + $hostTests + "`n" + $dtoSource + "`n" + $contextSource
$provider = New-Object Microsoft.VisualBasic.VBCodeProvider
$parameters = New-Object System.CodeDom.Compiler.CompilerParameters
$parameters.GenerateInMemory = $true
@('System.dll','System.Core.dll','System.Windows.Forms.dll','System.Drawing.dll','Microsoft.VisualBasic.dll') | ForEach-Object { [void]$parameters.ReferencedAssemblies.Add($_) }
$compiled = $provider.CompileAssemblyFromSource($parameters, $source)
$errors = @($compiled.Errors | Where-Object { -not $_.IsWarning })
if ($errors.Count) { throw ($errors -join "`n") }
$compiled.CompiledAssembly.GetType('HandoffTestHost').GetMethod('Run').Invoke($null, @())
