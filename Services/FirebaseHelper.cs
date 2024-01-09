using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporteasyy.Models;
using Firebase.Database.Query;
using Reporteasyy.Services;
using Firebase.Storage;

namespace Reporteasyy.Services
{
    public class FirebaseHelper
    {
        public FirebaseClient FBClient = new FirebaseClient(baseUrl: Settings.FirebaseRealTimeURL);

        // Add DBUser to real-time database, need to pass DBUser as argument (created first):
        public async Task<string> AddDBUser(DBUser dBUser)
        {
            FirebaseObject<DBUser> result = await FBClient.Child("DBUser").PostAsync(dBUser);
            return result.Key;
        }

        // Will get DBUser with DBUserName equals to name:
        public async Task<DBUser> GetDBUser(string name)
        {
            var result = await FBClient
                .Child("DBUser")
                .OrderBy("DBUserName")
                .EqualTo(name)
                .OnceAsync<DBUser>();

            FirebaseObject<DBUser> user = result.FirstOrDefault();
            return new DBUser
            {
                DBUserID = user.Key,
                DBUserName = user.Object.DBUserName,
                DBEmail = user.Object.DBEmail,
                DBFullname = user.Object.DBFullname,
                DBIdentificationNumber = user.Object.DBIdentificationNumber,
                DBPhoneNumber = user.Object.DBPhoneNumber
            };
        }

        // Get one DBUser with DBUserAuthID equals to authID (the userID in authentication database):
        public async Task<DBUser> GetDBUserByAuthID(string authID)
        {
            IReadOnlyCollection<FirebaseObject<DBUser>> result = await FBClient
                .Child("DBUser")
                .OrderBy("DBUserAuthID")
                .EqualTo(authID)
                .OnceAsync<DBUser>();

            FirebaseObject<DBUser> user = result.FirstOrDefault();

            return new DBUser
            {
                DBUserID = user.Key,
                DBUserName = user.Object.DBUserName,
                DBEmail = user.Object.DBEmail,
                DBFullname = user.Object.DBFullname,
                DBIdentificationNumber = user.Object.DBIdentificationNumber,
                DBPhoneNumber = user.Object.DBPhoneNumber
            };
        }

        // Get all DBUser from real-time database:
        public async Task<List<DBUser>> GetDBUser()
        {
            IReadOnlyCollection<FirebaseObject<DBUser>> result = await FBClient
                .Child("DBUser")
                .OnceAsync<DBUser>();

            return result.Select(user => new DBUser
            {
                DBUserID = user.Key,
                DBUserName = user.Object.DBUserName,
                DBEmail = user.Object.DBEmail,
                DBFullname = user.Object.DBFullname,
                DBIdentificationNumber = user.Object.DBIdentificationNumber,
                DBPhoneNumber = user.Object.DBPhoneNumber
            }).ToList();
        }

        public async Task<string> AddReport(Makereport report)
        {
            FirebaseObject<Makereport> result = await FBClient.Child("Report").PostAsync(report);
            return result.Key;
        }

        public async Task<string> AddReport(string dbUserID, string title, string description, string categories, DateTime reportTime, string unitNumber,
            string streetName, string postalCode, string latitude, string longitude, string urgency, string media)
        {
            return await AddReport(new Makereport
            {
                DBUserID = dbUserID,
                Title = title,
                Description = description,
                Categories = categories,
                ReportTime = reportTime,
                UnitNumber = unitNumber,
                StreetName = streetName,
                PostalCode = postalCode,
                Latitude = latitude,
                Longitude = longitude,
                Urgency = urgency,
                Media = media
            });
        }

        public async Task<Makereport> GetReportByIDAsync(string reportID)
        {
            IReadOnlyCollection<FirebaseObject<Makereport>> result = await FBClient
                .Child("Report")
                .OrderByKey()
                .EqualTo(reportID)
                .OnceAsync<Makereport>();

            FirebaseObject<Makereport> report = result.FirstOrDefault();
            return new Makereport
            {
                MRID = report.Key,
                DBUserID = report.Object.DBUserID,
                Title = report.Object.Title,
                Description = report.Object.Description,
                Categories = report.Object.Categories,
                ReportTime = report.Object.ReportTime,
                UnitNumber = report.Object.UnitNumber,
                StreetName = report.Object.StreetName,
                PostalCode = report.Object.PostalCode,
                Latitude = report.Object.Latitude,
                Longitude = report.Object.Longitude,
                Urgency = report.Object.Urgency,
                Media = report.Object.Media
            };
        }

        public async Task<List<Makereport>> GetAllReportByDBUserID(string dbUserID)
        {
            IReadOnlyCollection<FirebaseObject<Makereport>> result = await FBClient
                .Child("Report")
                .OrderBy("DBUserID")
                .EqualTo(dbUserID)
                .OnceAsync<Makereport>();

            return result.Select(report => new Makereport
            {
                MRID = report.Key,
                DBUserID = report.Object.DBUserID,
                Title = report.Object.Title,
                Description = report.Object.Description,
                Categories = report.Object.Categories,
                ReportTime = report.Object.ReportTime,
                UnitNumber = report.Object.UnitNumber,
                StreetName = report.Object.StreetName,
                PostalCode = report.Object.PostalCode,
                Latitude = report.Object.Latitude,
                Longitude = report.Object.Longitude,
                Urgency = report.Object.Urgency,
                Media = report.Object.Media
            }).ToList();
        }

        // For storage:
        public FirebaseStorage FirebaseStorage = new FirebaseStorage(Settings.FirebaseStorage);

        // Add files to firebase storage:
        public async Task<string> UploadFileToFirebaseStorage(string mediaPath)
        {
            FileStream stream = File.OpenRead(mediaPath);

            string fileName = Path.GetFileName(mediaPath);
            fileName = $"{DateTime.Now.ToString("yyyyMMdd_HHmmssfff")}_{fileName}";

            string url = await FirebaseStorage
                .Child("REFiles")
                .Child(fileName)
                .PutAsync(stream);

            stream.Dispose();

            return url;
        }
    }
}
