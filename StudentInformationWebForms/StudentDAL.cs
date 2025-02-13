using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StudentInformationWebForms
{
    // Data Access Layer (DAL) for managing database operations related to students
    public class StudentDAL
    {
        private readonly string connectionString; // Connection string to the database

        // Constructor to initialize the connection string from Web.config
        public StudentDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        }

        // returns DataTable containing all students.
        public DataTable GetAllStudents()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_getAllStudents", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd); // Bridge between DB and DataTable
                    DataTable dt = new DataTable();
                    da.Fill(dt); // executes the given SQL command or stored procedure, fetches the result, and stores it in the DataTable.
                    return dt;
                }
            }
        }

        // Returns DataRow containing student details or null if not found.
        public DataRow GetStudentById(int studentId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_getStudentById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd); // Bridge between DB and DataTable
                    DataTable dt = new DataTable();
                    da.Fill(dt); // executes the given SQL command or stored procedure, fetches the result, and stores it in the DataTable.

                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }

        // Returns True if operation succeeds, otherwise false.
        // Using int? allows us to pass null when inserting and an actual int when updating.
        public bool SaveStudent(int? studentId, string name, int age, string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(studentId == null ? "sp_insertStudent" : "sp_updateStudent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // If studentId is provided, add it for update operation
                    if (studentId != null)
                        cmd.Parameters.AddWithValue("@studentId", studentId);

                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@email", email);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Returns true if insert/update was successful
                }
            }
        }

        // returns True if deletion was successful, otherwise false.
        public bool DeleteStudent(int studentId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_deleteStudent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studentId", studentId);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Returns true if delete was successful
                }
            }
        }
    }
}
