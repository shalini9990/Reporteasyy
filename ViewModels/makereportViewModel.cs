using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporteasyy.Services;
using Reporteasyy.Models;
using Reporteasyy.Views;

namespace Reporteasyy.ViewModels
{
    public class makereportViewModel : INotifyPropertyChanged
    {
        private INavigation navigation;
        public string MediaPath { get; set; } = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public Command OnSubmitBtnOnClick { get; }

        private string _title;
        private string _description;
        private string _selectedCategory;
        private string _unitnumber;
        private string _streetname;
        private string _postalcode;
        private string _latitude;
        private string _longitude;
        private string _urgency;

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

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged(nameof(SelectedCategory));
            }
        }

       


        public string Unitnumber
        {
            get => _unitnumber;
            set
            {
                _unitnumber = value;
                RaisePropertyChanged(nameof(Unitnumber));
            }
        }

        public string Streetname
        {
            get => _streetname;
            set
            {
                _streetname = value;
                RaisePropertyChanged(nameof(Streetname));
            }
        }

        public string Postalcode
        {
            get => _postalcode;
            set
            {
                _postalcode = value;
                RaisePropertyChanged(nameof(Postalcode));
            }
        }

        public string Latitude
        {
            get => _latitude;
            set
            {
                _latitude = value;
                RaisePropertyChanged(nameof(Latitude));
            }
        }

        public string Longitude
        {
            get => _longitude;
            set
            {
                _longitude = value;
                RaisePropertyChanged(nameof(Longitude));
            }
        }

        public string Urgency
        {
            get => _urgency;
            set
            {
                _urgency = value;
                RaisePropertyChanged(nameof(Urgency));
            }
        }

        public makereportViewModel(INavigation nav)
        {
            navigation = nav;
            OnSubmitBtnOnClick = new Command(SubmitAsync);
        }

        private async void SubmitAsync(object obj)
        {
            string mediaURL = null;
            
            if (MediaPath != null)
            {
                mediaURL = await new FirebaseHelper().UploadFileToFirebaseStorage(MediaPath);
            }

            if (SelectedCategory == null)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Category can't be empty!", "OK");
                return;
            }

            string reportID = await new FirebaseHelper().AddReport(new Makereport
            {
                DBUserID = Preferences.Get("RTDBUserID", "NoRTDBUserID"),
                Title = Title,
                Description = Description,
                Categories = SelectedCategory,
                ReportTime = DateTime.Now,
                UnitNumber = Unitnumber,
                StreetName = Streetname,
                PostalCode = Postalcode,
                Latitude = Latitude,
                Longitude = Longitude,
                Urgency = Urgency,
                Media = mediaURL
            });

            await App.Current.MainPage.DisplayAlert("Completed!", $"New report added, ID: \"{reportID}\"", "View all reports");
            await navigation.PushAsync(new viewreport());
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
