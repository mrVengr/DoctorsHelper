using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using DoctorsHelper.Models;

namespace DoctorsHelper
{
    public class OracleController
    {
        private const string ConnectionString = "User Id=medical;Password=medical;Data Source=medical";
        private OleDbConnection con = new OleDbConnection(ConnectionString);

        private OleDbCommand BuildDBQuery(string query)
        {
            OleDbCommand command = con.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();

            return command;
        }

        public List<Doctor> GetAllDoctors()
        {
            con.Open();
            OleDbCommand command = BuildDBQuery("Select * FROM Doctors");

            List<Doctor> doctors = new List<Doctor>();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                doctors.Add(new Doctor()
                {
                    DoctorId = reader["ID_DOCTOR"].ToString(),
                    Name = reader["D_Name"].ToString(),
                    Surname = reader["Surname"].ToString(),
                    Speciality = reader["Speciality"].ToString(),

                });
            }

            con.Close();

            return doctors;
        }
    }
}