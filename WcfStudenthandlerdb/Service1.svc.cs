using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfStudenthandlerdb
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private const string ConnectionString = "Server=tcp:studentdb.database.windows.net,1433;Initial Catalog=StudentDB;Persist Security Info=False;User ID=magnus;Password=Password1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public int AddStudent(string navn, int id)
        {
            const string insertStudent = "insert into Students (Id, Navn) values (@id, @navn)";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertStudent, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@id", id);
                    insertCommand.Parameters.AddWithValue("@navn",navn);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        public IList<Student> GetAllStudents()
        {
            const string selectAllStudents = "Select * from Students order by Id";


            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using(SqlCommand selectCommand = new SqlCommand(selectAllStudents, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Student> studentList = new List<Student>();
                        while (reader.Read())
                        {
                            Student student = ReadStudent(reader);
                            studentList.Add(student);
                        }
                        return studentList;
                    }
                }
            }
        }

        private static Student ReadStudent(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string navn = reader.GetString(1);
            Student student = new Student
            {
                Id = id,
                Navn = navn,

            };
            return student;
        }

        public Student GetStudentById(int id)
        {
            const string selectStudent = "select * from Students where Id=@id";
            using(SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using(SqlCommand selectCommand = new SqlCommand(selectStudent, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    using(SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }
                        reader.Read();
                        Student student = ReadStudent(reader);
                        return student;
                    }
                }
            }
        }

        public IList<Student> GetStudentsByName(string navn)
        {
            string selectStr = "select * from Students where Navn LIKE @Navn";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectStr, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@navn", "%" + navn + "%");
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        IList<Student> studentList = new List<Student>();
                        while (reader.Read())
                        {
                            Student st = ReadStudent(reader);
                            studentList.Add(st);
                        }
                        return studentList;
                    }
                }
            }
        }
    }
}
