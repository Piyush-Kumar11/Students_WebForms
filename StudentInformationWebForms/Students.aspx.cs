using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace StudentInformationWebForms
{
    public partial class Students : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStudents();
            }
        }

        // Load Students into GridView
        private void LoadStudents()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_getAllStudents", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvStudents.DataSource = dt;
                    gvStudents.DataBind();
                }
            }
        }

        // Add New Student Button Click
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlStudentForm.Visible = true;
            lblFormTitle.Text = "Add New Student";
            btnSave.Text = "Save";
            hfStudentId.Value = "";
            ClearFields();
        }

        // Save or Update Student
        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(hfStudentId.Value == "" ? "sp_insertStudent" : "sp_updateStudent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (hfStudentId.Value != "")
                        cmd.Parameters.AddWithValue("@studentId", Convert.ToInt32(hfStudentId.Value));

                    cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@age", Convert.ToInt32(txtAge.Text.Trim()));
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());

                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }

            LoadStudents();
            pnlStudentForm.Visible = false;
        }

        // Edit and Delete Actions
        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditStudent")
            {
                int studentId = Convert.ToInt32(e.CommandArgument);
                LoadStudentById(studentId);
            }
            else if (e.CommandName == "DeleteStudent")
            {
                int studentId = Convert.ToInt32(e.CommandArgument);
                DeleteStudent(studentId);
            }
        }

        // Load Student for Editing
        private void LoadStudentById(int studentId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_getStudentById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        hfStudentId.Value = dr["StudentID"].ToString();
                        txtName.Text = dr["Name"].ToString();
                        txtAge.Text = dr["Age"].ToString();
                        txtEmail.Text = dr["Email"].ToString();
                        lblFormTitle.Text = "Edit Student";
                        btnSave.Text = "Update";
                        pnlStudentForm.Visible = true;
                    }
                    con.Close();
                }
            }
        }

        // Delete Student
        private void DeleteStudent(int studentId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_deleteStudent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            LoadStudents();
        }

        // Cancel Form
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlStudentForm.Visible = false;
            ClearFields();
        }

        // Clear Input Fields
        private void ClearFields()
        {
            txtName.Text = "";
            txtAge.Text = "";
            txtEmail.Text = "";
        }
    }
}
