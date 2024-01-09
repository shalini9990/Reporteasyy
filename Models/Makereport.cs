using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporteasyy.Models
{
    public class Makereport
    {
        public string MRID { get; set; }
        public string DBUserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Categories { get; set; }
        public DateTime ReportTime { get; set; }
        public string UnitNumber { get; set; }
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
        public string Urgency { get; set; }
        public string Media { get; set; }   
    }
}
