using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;

public class DataBaseController
{
    public void InsertNewDoctor(Doctor doctor)
    {
        OracleConnection conn = new OracleConnection(Constants.ConnString);
        conn.Open();
        OracleCommand cmd = new OracleCommand
        {
            Connection = conn,
            CommandText = "Insert into Doctors (Doctor_Id,Name,Surname,Email,Password,Speciality,License) " + 
                "values (:id,:name,:surname,:email,:pass,:spec,:lic)",
            CommandType = CommandType.Text
        };

        cmd.Parameters.AddRange(new OracleParameter[]
            {
                new OracleParameter("id", doctor.DoctorId),
                new OracleParameter("name", doctor.Name),
                new OracleParameter("surname", doctor.Surname),
                new OracleParameter("email", doctor.Email),
                new OracleParameter("pass", doctor.Password),
                new OracleParameter("spec", doctor.Speciality),
                new OracleParameter("lic", doctor.License)
            });

        cmd.ExecuteNonQuery();
        conn.Dispose();
    }

    public void InsertNewPatient(Patient patient)
    {
        OracleConnection conn = new OracleConnection(Constants.ConnString);
        conn.Open();
        OracleCommand cmd = new OracleCommand
        {
            Connection = conn,
            CommandText = "Insert into Patients (Patient_Id,Name,Surname,Gender,BirthDate,Address) " +
                "values (:id,:name,:surname,:gender,:birthdate,:address,:story)",
            CommandType = CommandType.Text
        };

        cmd.Parameters.AddRange(new OracleParameter[]
            {
                new OracleParameter("id", patient.PatientId),
                new OracleParameter("name", patient.Name),
                new OracleParameter("surname", patient.Surname),
                new OracleParameter("gender", patient.Gender),
                new OracleParameter("birthdate", patient.BirthDate),
                new OracleParameter("address", patient.Address),
                new OracleParameter("story", patient.Story),

            });

        cmd.ExecuteNonQuery();
        conn.Dispose();
    }

    public Doctor Login(string email, string password)
    {
        OracleConnection conn = new OracleConnection(Constants.ConnString);
        conn.Open();
        OracleCommand cmd = new OracleCommand
        {
            Connection = conn,
            CommandText = "select * from Doctors where Email=:email AND Password=:pass",
            CommandType = CommandType.Text
        };

        cmd.Parameters.AddRange(new OracleParameter[]
           {
                new OracleParameter("email", email),
                new OracleParameter("pass", password)
           });

        OracleDataReader dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            Doctor doctor = new Doctor()
            {
                DoctorId = dr.GetString(0),
                Name = dr.GetString(1),
                Surname = dr.GetString(2),
                Email = email,
                Speciality = (Speciality)dr.GetInt32(5),
                License = dr.GetString(6)
            };
            
            conn.Dispose();
            return doctor;
        }
        else
        {
            conn.Dispose();
            return null;
        };
    }

    public List<Patient> GetDoctorsPatients(string doctorId)
    {
        OracleConnection conn = new OracleConnection(Constants.ConnString);
        conn.Open();
        OracleCommand cmd = new OracleCommand
        {
            Connection = conn,
            CommandText = "select (PatientId, Name, Surname, Gender, BirthDate, Address, Story) " +
                "from Patients p, Visit v where p.PatientId=v.PatientId AND v.DoctorId=:docId",
            CommandType = CommandType.Text
        };

        cmd.Parameters.Add(new OracleParameter("docId", doctorId));

        OracleDataReader dr = cmd.ExecuteReader();

        List<Patient> patients = new List<Patient>();
        while (dr.Read())
        {
            patients.Add(new Patient()
            {
                PatientId = dr.GetString(0),
                Name = dr.GetString(1),
                Surname = dr.GetString(2),
                Gender = (Gender)dr.GetInt32(3),
                BirthDate = dr.GetDateTime(4),
                Address = dr.GetString(5),
                Story = dr.GetString(6)
            });
        }

        return patients;
    }

    public List<Visit> GetDoctorsVisits(string doctorId)
    {
        OracleConnection conn = new OracleConnection(Constants.ConnString);
        conn.Open();
        OracleCommand cmd = new OracleCommand
        {
            Connection = conn,
            CommandText = "select (PatientId, Name, Surname, Gender, BirthDate, Address, Story, VisitId, VisitDate, Diagnosis) " +
                "from Patients p, Visit v where p.PatientId=v.PatientId AND v.DoctorId=:docId",
            CommandType = CommandType.Text
        };

        cmd.Parameters.Add(new OracleParameter("docId", doctorId));

        OracleDataReader dr = cmd.ExecuteReader();
    }

    public List<Medicine> GetAllMedicines(string doctorId)
    {
        OracleConnection conn = new OracleConnection(Constants.ConnString);
        conn.Open();
        OracleCommand cmd = new OracleCommand
        {
            Connection = conn,
            CommandText = "select * from Medicines",
            CommandType = CommandType.Text
        };

        OracleDataReader dr = cmd.ExecuteReader();

        List<Medicine> medicines = new List<Medicine>();
        while (dr.Read())
        {
            medicines.Add(new Medicine()
            {
                MedicineId = dr.GetString(0),
                Name = dr.GetString(1),
                Brand = dr.GetString(2),
                Description = dr.GetString(3),
                SideEffects = dr.GetString(4)
            });
        }

        return medicines;
    }
}