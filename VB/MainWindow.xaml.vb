Imports System
Imports System.Collections.ObjectModel
Imports System.Windows
Imports DevExpress.Services
Imports DevExpress.XtraScheduler.Native
Imports DevExpress.XtraScheduler.Services.Internal

Namespace SchedulerBindToObservableCollectionWpf
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
            schedulerControl1.Start = Date.Today
            schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Timeline
            Dim helper As New DragDropResourcesHelper(schedulerControl1, "SortOrder")
        End Sub
    End Class
End Namespace
