using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsHelper.Models
{
    public class Visit
    {
        public string VisitId { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; }
        public string Commentary { get; set; }
    }
}