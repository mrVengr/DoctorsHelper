using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsHelper.Models
{
    public class Medicine
    {
        public string MedicineId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string SideEffects { get; set; }
    }
}