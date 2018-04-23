Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Native

Namespace SchedulerBindToObservableCollectionWpf
    Public Class DragDropResourcesHelper
        Private CurrentScheduler As SchedulerControl
        Private CustomFieldName As String

        Private downHitInfo As ISchedulerHitInfo
        Private downHitPoint As Point


        Public Sub New(ByVal scheduler As SchedulerControl, ByVal fieldName As String)
            CurrentScheduler = scheduler
            CustomFieldName = fieldName

            AddHandler CurrentScheduler.Storage.ResourceCollectionLoaded, AddressOf Storage_ResourceCollectionLoaded
            CurrentScheduler.Storage.RefreshData()
            AddHandler CurrentScheduler.PreviewMouseDown, AddressOf CurrentScheduler_PreviewMouseDown
            AddHandler CurrentScheduler.PreviewMouseMove, AddressOf CurrentScheduler_PreviewMouseMove
            AddHandler CurrentScheduler.Drop, AddressOf CurrentScheduler_Drop
        End Sub

        Private Sub CurrentScheduler_Drop(ByVal sender As Object, ByVal e As System.Windows.DragEventArgs)
            If downHitInfo Is Nothing Then
                Return
            End If
            Dim dropHitInfo As ISchedulerHitInfo = SchedulerHitInfo.CreateSchedulerHitInfo(CurrentScheduler, e.GetPosition(CurrentScheduler))
            If dropHitInfo.HitTest = DevExpress.XtraScheduler.Drawing.SchedulerHitTest.ResourceHeader Then
                Dim targetResource As Resource = dropHitInfo.ViewInfo.Resource
                Dim sourceResource As Resource = downHitInfo.ViewInfo.Resource
                If targetResource IsNot sourceResource Then
                    Dim sourceResourceSortOrder As Integer = Convert.ToInt32(sourceResource.CustomFields(CustomFieldName))
                    sourceResource.CustomFields(CustomFieldName) = targetResource.CustomFields(CustomFieldName)
                    targetResource.CustomFields(CustomFieldName) = sourceResourceSortOrder
                    ApplySorting()
                End If
            End If
        End Sub

        Private Sub CurrentScheduler_PreviewMouseMove(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs)
            If e.LeftButton = System.Windows.Input.MouseButtonState.Pressed AndAlso downHitInfo IsNot Nothing Then
                Dim dragSize As Size = System.Windows.Forms.SystemInformation.DragSize
                Dim dragRect As New Rectangle(New Point(downHitPoint.X - dragSize.Width \ 2, downHitPoint.Y - dragSize.Height \ 2), dragSize)
                Dim eventPoint As System.Windows.Point = e.GetPosition(CurrentScheduler)
                If Not dragRect.Contains(New Point(CInt((eventPoint.X)), CInt((eventPoint.Y)))) Then
                    System.Windows.DragDrop.DoDragDrop(CurrentScheduler, downHitInfo.ViewInfo.Resource, System.Windows.DragDropEffects.Move)
                    downHitInfo = Nothing
                    downHitPoint = Point.Empty
                End If
            End If
        End Sub

        Private Sub CurrentScheduler_PreviewMouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
            downHitInfo = Nothing
            downHitPoint = Point.Empty
            Dim eventPoint As System.Windows.Point = e.GetPosition(CurrentScheduler)
            Dim hitInfo As ISchedulerHitInfo = SchedulerHitInfo.CreateSchedulerHitInfo(CurrentScheduler, eventPoint)
            If hitInfo.HitTest = DevExpress.XtraScheduler.Drawing.SchedulerHitTest.ResourceHeader Then
                downHitInfo = hitInfo

                downHitPoint = New Point(CInt((eventPoint.X)), CInt((eventPoint.Y)))
                e.Handled = True
            End If
        End Sub

        Private Sub Storage_ResourceCollectionLoaded(ByVal sender As Object, ByVal e As EventArgs)
            ApplySorting()
        End Sub

        Private Sub ApplySorting()
            CurrentScheduler.Storage.ResourceStorage.Items.Sort(New ResourceCustomFieldComparer("SortOrder"))
            CurrentScheduler.ActiveView.LayoutChanged()
        End Sub
    End Class

    ' RESOURECE COMPARER
    Public MustInherit Class ResourceBaseComparer
        Implements IComparer(Of Resource), IComparer

        #Region "IComparer Members"
        Private Function IComparer_Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Return CompareCore(x, y)
        End Function
        Public Function Compare(ByVal x As Resource, ByVal y As Resource) As Integer Implements IComparer(Of Resource).Compare
            Return CompareCore(x, y)
        End Function
        #End Region

        Protected Overridable Function CompareCore(ByVal x As Object, ByVal y As Object) As Integer
            Dim xRes As Resource = DirectCast(x, Resource)
            Dim yRes As Resource = DirectCast(y, Resource)

            If xRes Is Nothing OrElse yRes Is Nothing Then
                Return 0
            End If
            If ResourceEmpty.Resource.Equals(xRes) OrElse ResourceEmpty.Resource.Equals(yRes) Then
                Return 0
            End If

            Return CompareResources(xRes, yRes)
        End Function

        Protected MustOverride Function CompareResources(ByVal xRes As Resource, ByVal yRes As Resource) As Integer
    End Class

    Public Class ResourceCustomFieldComparer
        Inherits ResourceBaseComparer

        Private customFieldValue As String = ""
        Public Sub New(ByVal parCustomField As String)
            customFieldValue = parCustomField
        End Sub

        Protected Overrides Function CompareResources(ByVal xRes As Resource, ByVal yRes As Resource) As Integer
            Return Convert.ToInt32(xRes.CustomFields(customFieldValue)).CompareTo(Convert.ToInt32(yRes.CustomFields(customFieldValue)))
        End Function
    End Class
End Namespace
