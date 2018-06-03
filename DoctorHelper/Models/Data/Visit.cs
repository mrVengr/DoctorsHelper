using System;
using System.Collections.Generic;

public class Visit
{
    public string VisitId;
    public string PatientId;
    public string DoctorId;
    public DateTime VisitDate;
    public string Diagnosis;
    public string Commentary;
    public List<Medicine> PrescribedMedicines;
}