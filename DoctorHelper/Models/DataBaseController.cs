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
            CommandText = "Insert into Doctor (DoctorId,Name,Surname,Email,Password,Speciality,License) " + 
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
                new OracleParameter("spec", (int)doctor.Speciality),
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
            CommandText = "Insert into Patient (PatientId,Name,Surname,Gender,BirthDate,Address,History) " +
                "values (:id,:name,:surname,:gender,:birthdate,:address,:story)",
            CommandType = CommandType.Text
        };

        cmd.Parameters.AddRange(new OracleParameter[]
            {
                new OracleParameter("id", patient.PatientId),
                new OracleParameter("name", patient.Name),
                new OracleParameter("surname", patient.Surname),
                new OracleParameter("gender", (int)patient.Gender),
                new OracleParameter("birthdate", patient.BirthDate),
                new OracleParameter("address", patient.Address),
                new OracleParameter("story", patient.Story),

            });

        cmd.ExecuteNonQuery();
        conn.Dispose();
    }

    public void InsertNewVisit(Visit visit)
    {
        OracleConnection conn = new OracleConnection(Constants.ConnString);
        conn.Open();
        OracleCommand cmd = new OracleCommand
        {
            Connection = conn,
            CommandText = "Insert into Visit (VisitId,DoctorId,PatientId,VisitDate,Diagnosis,Commentary) " +
                "values (:visId,:docId,:patId,:date,:diag,:comm)",
            CommandType = CommandType.Text
        };

        cmd.Parameters.AddRange(new OracleParameter[]
            {
                new OracleParameter("visId", visit.VisitId),
                new OracleParameter("docId", visit.DoctorId),
                new OracleParameter("patId", visit.PatientId),
                new OracleParameter("date", visit.VisitDate),
                new OracleParameter("diag", visit.Diagnosis),
                new OracleParameter("comm", visit.Commentary)
            });

        cmd.ExecuteNonQuery();

        foreach (Medicine med in visit.PrescribedMedicines)
        {
            cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "Insert into PrescribedMedicine (VisitId, MedicineId) values (:visId,:medId)",
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddRange(new OracleParameter[]
            {
                new OracleParameter("visId", visit.VisitId),
                new OracleParameter("docId", med.MedicineId)
            });

            cmd.ExecuteNonQuery();
        }
        
        conn.Dispose();
    }

    public Doctor Login(string email, string password)
    {
        OracleConnection conn = new OracleConnection(Constants.ConnString);
        conn.Open();
        OracleCommand cmd = new OracleCommand
        {
            Connection = conn,
            CommandText = "select * from Doctor where Email=:email AND Password=:pass",
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
                Password = password,
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
            CommandText = "select p.PatientId, p.Name, p.Surname, p.Gender, p.BirthDate, p.Address, p.History " +
                "from Patient p, Visit v where p.PatientId=v.PatientId AND v.DoctorId=:docId",
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

    public Dictionary<Visit, Patient> GetDoctorsVisits(string doctorId)
    {
        OracleConnection conn = new OracleConnection(Constants.ConnString);
        conn.Open();
        OracleCommand cmd = new OracleCommand
        {
            Connection = conn,
            CommandText = "select (PatientId, Name, Surname, Gender, BirthDate, Address, Story, VisitId, VisitDate, Diagnosis, Commentary) " +
                "from Patients p, Visit v where p.PatientId=v.PatientId AND v.DoctorId=:docId",
            CommandType = CommandType.Text
        };

        cmd.Parameters.Add(new OracleParameter("docId", doctorId));

        OracleDataReader dr = cmd.ExecuteReader();

        Dictionary<Visit, Patient> visits = new Dictionary<Visit, Patient>();
        while (dr.Read())
        {
            Patient p = new Patient()
            {
                PatientId = dr.GetString(0),
                Name = dr.GetString(1),
                Surname = dr.GetString(2),
                Gender = (Gender)dr.GetInt32(3),
                BirthDate = dr.GetDateTime(4),
                Address = dr.GetString(5),
                Story = dr.GetString(6)
            };

            Visit v = new Visit()
            {
                VisitId = dr.GetString(7),
                VisitDate = dr.GetDateTime(8),
                Diagnosis = dr.GetString(9),
                Commentary = dr.GetString(10)
            };

            visits.Add(v,p);
        }

        foreach (KeyValuePair<Visit, Patient> visit in visits)
        {
            cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "select (MedicineId, Name, Brand, Description, SideEffects) " +
                    "from PrescribedMedicine p, Medicine m where p.MedicineId=m.MedicineId AND p.VisitId=:visId",
                CommandType = CommandType.Text
            };

            cmd.Parameters.Add(new OracleParameter("visId", visit.Key.VisitId));

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                visit.Key.PrescribedMedicines.Add(new Medicine()
                {
                    MedicineId = dr.GetString(0),
                    Name = dr.GetString(1),
                    Brand = dr.GetString(2),
                    Description = dr.GetString(3),
                    SideEffects = dr.GetString(4)
                });
            }
        }

        return visits;
    }

    public List<Medicine> GetAllMedicines(string doctorId)
    {
        OracleConnection conn = new OracleConnection(Constants.ConnString);
        conn.Open();
        OracleCommand cmd = new OracleCommand
        {
            Connection = conn,
            CommandText = "select * from Medicine",
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