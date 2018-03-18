using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorsHelper.Models
{
    public class Doctor
    {
        public string DoctorId { get; set; }
        [Required(ErrorMessage = "Please enter your login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Please enter your password")] 
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+",
        ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter your speciality")]
        public string Speciality { get; set; } // Make enum
    }
}