using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporteasyy.Services;
using Reporteasyy.Models;

namespace Reporteasyy.ViewModels
{
    public class makereportViewModel : INotifyPropertyChanged
    {
        private INavigation navigation;

        public event PropertyChangedEventHandler PropertyChanged;

        public Command OnSubmitBtnOnClick { get; }

        private string _title;
        private string _description;
        private int _selectedCategoryIndex;
        private string _selectedCategory;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public int SelectedCategoryIndex
        {
            get => _selectedCategoryIndex;
            set
            {
                _selectedCategoryIndex = value;
                RaisePropertyChanged(nameof(SelectedCategoryIndex));
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged(nameof(SelectedCategory));
            }
        }

        public makereportViewModel(INavigation nav)
        {
            navigation = nav;
            OnSubmitBtnOnClick = new Command(SubmitAsync);
        }

        private async void SubmitAsync(object obj)
        {
            string reportID = await new FirebaseHelper().AddReport(new Makereport
            {
                DBUserID = Preferences.Get("RTDBUserID", "NoRTDBUserID"),
                Title = Title,
                Description = Description,
                Categories = SelectedCategory,
                ReportTime = DateTime.Now,
                UnitNumber = "UnitNumber",
                StreetName = "StreetName",
                PostalCode = "PostalCode",
                Urgency = "Urgency",
                Media = "Media"
            });
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
