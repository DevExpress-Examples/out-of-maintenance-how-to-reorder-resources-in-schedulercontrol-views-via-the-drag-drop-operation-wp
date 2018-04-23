using System;
using System.Collections.ObjectModel;
using System.Windows;
using DevExpress.Services;
using DevExpress.XtraScheduler.Native;
using DevExpress.XtraScheduler.Services.Internal;

namespace SchedulerBindToObservableCollectionWpf {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            schedulerControl1.Start = DateTime.Today;
            schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Timeline;
            DragDropResourcesHelper helper = new DragDropResourcesHelper(schedulerControl1, "SortOrder");
        }
    }
}
