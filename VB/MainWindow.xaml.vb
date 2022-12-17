Imports System.Windows

Namespace SchedulerBindToObservableCollectionWpf

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            Me.schedulerControl1.Start = Date.Today
            Me.schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Timeline
            Dim helper As DragDropResourcesHelper = New DragDropResourcesHelper(Me.schedulerControl1, "SortOrder")
        End Sub
    End Class
End Namespace
