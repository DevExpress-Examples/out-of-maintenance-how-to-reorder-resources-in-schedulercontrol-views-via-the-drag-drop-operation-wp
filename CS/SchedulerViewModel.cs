using System;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Core.Commands;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SchedulerBindToObservableCollectionWpf {
    public class SchedulerViewModel {
        public ObservableCollection<ModelAppointment> Appointments { get; private set; }
        public ObservableCollection<ModelResource> Resources { get; private set; }

        public SchedulerViewModel() {
            Appointments = new ObservableCollection<ModelAppointment>();
            Resources = new ObservableCollection<ModelResource>();
            AddTestData();
        }

        private void AddTestData() {
            Resources.Add(CreateCustomResource(1, "Max Fowler", ToRgb(Color.PowderBlue), 3));
            Resources.Add(CreateCustomResource(2, "Nancy Drewmore", ToRgb(Color.PaleVioletRed), 1));
            Resources.Add(CreateCustomResource(3, "Pak Jang", ToRgb(Color.PeachPuff), 2));

            foreach(ModelResource resource in Resources) {
                string subjPrefix = resource.Name + "'s ";
                Appointments.Add(CreateEvent(subjPrefix + "meeting", resource.Id, 2, 5));
                Appointments.Add(CreateEvent(subjPrefix + "travel", resource.Id, 3, 6));
                Appointments.Add(CreateEvent(subjPrefix + "phone call", resource.Id, 0, 10));                
            }
        }

        private ModelResource CreateCustomResource(int res_id, string caption, int ResColor, int sortOrder) {
            ModelResource mRes = new ModelResource();
            mRes.Id = res_id;
            mRes.Name = caption;
            mRes.Color = ResColor;
            mRes.ResSortOrder = sortOrder;
            return mRes;
        }

        private ModelAppointment CreateEvent(string subject, object resourceId, int status, int label) {
            ModelAppointment apt = new ModelAppointment();
            apt.Subject = subject;
            apt.ResourceId = resourceId;
            Random rnd = new Random(); ;
            int rangeInMinutes = 60 * 24;
            apt.StartTime = DateTime.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes));
            apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes / 4));
            apt.Status = status;
            apt.Label = label;
            return apt;
        }

        private int ToRgb(System.Drawing.Color color) {
            return color.B << 16 | color.G << 8 | color.R;
        }
    }
}