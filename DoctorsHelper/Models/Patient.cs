using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsHelper.Models
{
    public class Patient
    {
        public string PatientId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
    }
}