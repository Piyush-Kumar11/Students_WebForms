using System;  
using System.Data; 
using System.Web.UI.WebControls; 

namespace StudentInformationWebForms
{
    public partial class Students : System.Web.UI.Page
    {
        private StudentDAL studentDAL = new StudentDAL();

        // Event that runs when the page loads.
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if this is the first time the page is loading (not a postback)
            if (!IsPostBack)
            {
                LoadStudents();  // Load student records into the GridView.
            }
        }

        // Method to load students from the database into the GridView.
        private void LoadStudents()
        {
            gvStudents.DataSource = studentDAL.GetAllStudents(); // Fetch data from DB.
            gvStudents.DataBind(); // Bind the data to the GridView for display.
        }

        // Event handler for the "Add New Student" button click.
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlStudentForm.Visible = true;  // Show the student form panel.
            lblFormTitle.Text = "Add New Student";  // Set form title.
            btnSave.Text = "Save";  // Set button text to "Save".
            hfStudentId.Value = "";  // Clear the hidden field for student ID.
            ClearFields();  // Clear all input fields.
        }

        // Event handler for Save/Update Student button click.
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Check if validation controls have errors
            if (!Page.IsValid)
            {
                return; // Stop processing if input is invalid
            }

            // Convert values
            int? studentId = string.IsNullOrEmpty(hfStudentId.Value) ? (int?)null : Convert.ToInt32(hfStudentId.Value);
            string name = txtName.Text.Trim();
            int age = Convert.ToInt32(txtAge.Text.Trim());
            string email = txtEmail.Text.Trim();

            // Additional Server-side Validation (Backup for Security)
            if (name.Length < 2)
            {
                lblMessage.Text = "Name must be at least 2 characters.";
                lblMessage.CssClass = "text-danger";
                return;
            }
            if (age <= 0)
            {
                lblMessage.Text = "Age must be greater than 0.";
                lblMessage.CssClass = "text-danger";
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[\w\.-]+@[\w\.-]+\.\w{2,4}$"))
            {
                lblMessage.Text = "Invalid email format.";
                lblMessage.CssClass = "text-danger";
                return;
            }

            // Save Student (Only if validation passes)
            bool success = studentDAL.SaveStudent(studentId, name, age, email);

            if (success)
            {
                lblMessage.Text = "Student saved successfully!";
                lblMessage.CssClass = "text-success";
                LoadStudents();
                pnlStudentForm.Visible = false;
            }
            else
            {
                lblMessage.Text = "Error saving student. Please try again.";
                lblMessage.CssClass = "text-danger";
            }
        }


        // Handles edit and delete actions in the GridView.
        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditStudent") // If the "Edit" button is clicked
            {
                int studentId = Convert.ToInt32(e.CommandArgument);
                LoadStudentById(studentId);
            }
            else if (e.CommandName == "DeleteStudent") // If the "Delete" button is clicked
            {
                int studentId = Convert.ToInt32(e.CommandArgument);
                studentDAL.DeleteStudent(studentId);
                LoadStudents();
            }
        }

        // Loads a student's details into the form for editing.
        private void LoadStudentById(int studentId)
        {
            DataRow student = studentDAL.GetStudentById(studentId); // Fetch student details.
            if (student != null)
            {
                hfStudentId.Value = student["StudentID"].ToString(); // Store student ID in hidden field.
                txtName.Text = student["Name"].ToString();
                txtAge.Text = student["Age"].ToString(); 
                txtEmail.Text = student["Email"].ToString();
                lblFormTitle.Text = "Edit Student";
                btnSave.Text = "Update"; 
                pnlStudentForm.Visible = true; // Show the form panel.
            }
        }

        // Event handler for the Cancel button click.
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlStudentForm.Visible = false; // Hide the student form panel.
            ClearFields(); // Clear input fields.
        }

        // Clears the input fields in the form.
        private void ClearFields()
        {
            txtName.Text = "";
            txtAge.Text = ""; 
            txtEmail.Text = ""; 
        }
    }
}
