using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporteasyy.Models;
using Reporteasyy.Services;

namespace Reporteasyy.ViewModels
{
    public class viewrreportViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Makereport> reports;

        public ObservableCollection<Makereport> Reports
        {
            get => reports;
            set
            {
                reports = value;
                RaisePropertyChanged(nameof(Reports));
            }
        }

        public viewrreportViewModel()
        {
            GetAllReport();
        }

        public async Task GetAllReport()
        {
            List<Makereport> reportsList = await new FirebaseHelper().GetAllReportByDBUserID(Preferences.Get("RTDBUserID", "NoRTDBUserID"));
            reportsList.Sort((x, y) => x.ReportTime.CompareTo(y.ReportTime));

            ObservableCollection<Makereport> reportsOC = new ObservableCollection<Makereport>();
            foreach(Makereport r in reportsList)
            {
                reportsOC.Add(r);
            }

            Reports = reportsOC;
        }

        public void RaisePropertyChanged(string  propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
