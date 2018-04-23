using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;

namespace SchedulerBindToObservableCollectionWpf {
    public class DragDropResourcesHelper {
        private SchedulerControl CurrentScheduler;
        private string CustomFieldName;

        ISchedulerHitInfo downHitInfo;
        Point downHitPoint;


        public DragDropResourcesHelper(SchedulerControl scheduler, string fieldName) {
            CurrentScheduler = scheduler;
            CustomFieldName = fieldName;

            CurrentScheduler.Storage.ResourceCollectionLoaded +=Storage_ResourceCollectionLoaded;
            CurrentScheduler.Storage.RefreshData();
            CurrentScheduler.PreviewMouseDown += CurrentScheduler_PreviewMouseDown;
            CurrentScheduler.PreviewMouseMove += CurrentScheduler_PreviewMouseMove;
            CurrentScheduler.Drop += CurrentScheduler_Drop;
        }

        void CurrentScheduler_Drop(object sender, System.Windows.DragEventArgs e) {
            if(downHitInfo == null) return;
            ISchedulerHitInfo dropHitInfo = SchedulerHitInfo.CreateSchedulerHitInfo(CurrentScheduler, e.GetPosition(CurrentScheduler));
            if(dropHitInfo.HitTest == DevExpress.XtraScheduler.Drawing.SchedulerHitTest.ResourceHeader) {
                Resource targetResource = dropHitInfo.ViewInfo.Resource;
                Resource sourceResource = downHitInfo.ViewInfo.Resource;
                if(targetResource != sourceResource) {
                    int sourceResourceSortOrder = Convert.ToInt32(sourceResource.CustomFields[CustomFieldName]);
                    sourceResource.CustomFields[CustomFieldName] = targetResource.CustomFields[CustomFieldName];
                    targetResource.CustomFields[CustomFieldName] = sourceResourceSortOrder;
                    ApplySorting();
                }
            }
        }

        void CurrentScheduler_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e) {
            if(e.LeftButton == System.Windows.Input.MouseButtonState.Pressed && downHitInfo != null) {
                Size dragSize = System.Windows.Forms.SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitPoint.X - dragSize.Width / 2, downHitPoint.Y - dragSize.Height / 2), dragSize);
                System.Windows.Point eventPoint = e.GetPosition(CurrentScheduler);
                if(!dragRect.Contains(new Point((int)eventPoint.X, (int)eventPoint.Y))) {
                    System.Windows.DragDrop.DoDragDrop(CurrentScheduler, downHitInfo.ViewInfo.Resource, System.Windows.DragDropEffects.Move);
                    downHitInfo = null;
                    downHitPoint = Point.Empty;
                }
            }            
        }

        void CurrentScheduler_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            downHitInfo = null;
            downHitPoint = Point.Empty;
            System.Windows.Point eventPoint = e.GetPosition(CurrentScheduler);
            ISchedulerHitInfo hitInfo = SchedulerHitInfo.CreateSchedulerHitInfo(CurrentScheduler, eventPoint);
            if(hitInfo.HitTest == DevExpress.XtraScheduler.Drawing.SchedulerHitTest.ResourceHeader) {
                downHitInfo = hitInfo;

                downHitPoint = new Point((int)eventPoint.X, (int)eventPoint.Y);
                e.Handled = true;
            }            
        }

        void Storage_ResourceCollectionLoaded(object sender, EventArgs e) {
            ApplySorting();
        }

        void ApplySorting() {
            CurrentScheduler.Storage.ResourceStorage.Items.Sort(new ResourceCustomFieldComparer("SortOrder"));
            CurrentScheduler.ActiveView.LayoutChanged();
        }
    }

    // RESOURECE COMPARER
    public abstract class ResourceBaseComparer : IComparer<Resource>, IComparer {
        #region IComparer Members
        int IComparer.Compare(object x, object y) {
            return CompareCore(x, y);
        }
        public int Compare(Resource x, Resource y) {
            return CompareCore(x, y);
        }
        #endregion

        protected virtual int CompareCore(object x, object y) {
            Resource xRes = (Resource)x;
            Resource yRes = (Resource)y;

            if(xRes == null || yRes == null)
                return 0;
            if(Resource.Empty.Equals(xRes) || Resource.Empty.Equals(yRes))
                return 0;

            return CompareResources(xRes, yRes);
        }

        protected abstract int CompareResources(Resource xRes, Resource yRes);
    }

    public class ResourceCustomFieldComparer : ResourceBaseComparer {
        string customFieldValue = "";
        public ResourceCustomFieldComparer(string parCustomField) {
            customFieldValue = parCustomField;
        }

        protected override int CompareResources(Resource xRes, Resource yRes) {
            return Convert.ToInt32(xRes.CustomFields[customFieldValue]).CompareTo(Convert.ToInt32(yRes.CustomFields[customFieldValue]));
        }
    }
}
