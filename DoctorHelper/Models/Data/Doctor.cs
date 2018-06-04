using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

public class Doctor
{
    public string DoctorId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Speciality Speciality { get; set; }
    public string License { get; set; }


}