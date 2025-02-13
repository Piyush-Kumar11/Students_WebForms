<%@ Page Title="Students" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Students.aspx.cs" Inherits="StudentInformationWebForms.Students" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h2>Student Management</h2>--%>

    <%-- GridView to display student records --%>
    <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False" DataKeyNames="StudentID"
     CssClass="table table-bordered" OnRowCommand="gvStudents_RowCommand">
    <Columns>
        <%-- Display Student ID (Read-Only) --%>
        <asp:BoundField DataField="StudentID" HeaderText="ID" ReadOnly="True" />

        <%-- Display Student Name --%>
        <asp:BoundField DataField="Name" HeaderText="Name" />

        <%-- Display Student Age --%>
        <asp:BoundField DataField="Age" HeaderText="Age" />

        <%-- Display Student Email --%>
        <asp:BoundField DataField="Email" HeaderText="Email" />

        <%-- Display Enrollment Date (Formatted as yyyy-MM-dd) --%>
        <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Date" DataFormatString="{0:yyyy-MM-dd}" />

        <%-- Actions Column for Edit and Delete Buttons --%>
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <%-- Edit Button (Passes StudentID as CommandArgument) --%>
                <asp:Button runat="server" CssClass="btn btn-primary btn-sm" 
                    CommandName="EditStudent" CommandArgument='<%# Eval("StudentID") %>' Text="Edit" />

                <%-- Delete Button (Passes StudentID as CommandArgument) --%>
                <asp:Button runat="server" CssClass="btn btn-danger btn-sm" 
                    CommandName="DeleteStudent" CommandArgument='<%# Eval("StudentID") %>' 
                    Text="Delete" OnClientClick="return confirm('Are you sure?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

    <br />

    <%-- Button to Show the Add Student Form --%>
    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add New Student" 
        OnClick="btnAdd_Click" />

    <br /><br />

        <%-- Student Form Panel (Hidden by Default) --%>
    <asp:Panel ID="pnlStudentForm" runat="server" CssClass="border p-3" Visible="False">
        
        <%-- Form Title (Dynamically Changes to 'Add' or 'Edit' Mode) --%>
        <h3><asp:Label ID="lblFormTitle" runat="server" Text="Add New Student"></asp:Label></h3>
        
        <%-- Hidden Field to Store Student ID (Used for Edit Mode) --%>
        <asp:HiddenField ID="hfStudentId" runat="server" />
        
        <%--Name Input Field with Validation--%>
        <div class="mb-3">
            <label>Name:</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Enter Name"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                ErrorMessage="Name is required." CssClass="text-danger" Display="Dynamic" />
            <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName"
                ValidationExpression="^[A-Za-z\s]{2,50}$" ErrorMessage="Name must be at least 2 letters." 
                CssClass="text-danger" Display="Dynamic" />
        </div>

        <%--Age Input Field with Validation--%>
        <div class="mb-3">
            <label>Age:</label>
            <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" Placeholder="Enter Age" TextMode="Number"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtAge"
                ErrorMessage="Age is required." CssClass="text-danger" Display="Dynamic" />
            <asp:RangeValidator ID="rvAge" runat="server" ControlToValidate="txtAge" MinimumValue="1" MaximumValue="100"
                Type="Integer" ErrorMessage="Age must be between 1 and 100." CssClass="text-danger" Display="Dynamic" />
        </div>

        <%--Email Input Field with Validation--%>
        <div class="mb-3">
            <label>Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Enter Email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w{2,4}$" ErrorMessage="Enter a valid email format (e.g. example@mail.com)."
                CssClass="text-danger" Display="Dynamic" />
        </div>

        <%--Error/Success Message Label (Shows validation or success messages)--%>
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />

        <%--Save Button (With Validation)--%>
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save"
            OnClick="btnSave_Click" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" DisplayMode="BulletList" />


        <%-- Cancel Button (Hides the Form) --%>
        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="Cancel" 
            OnClick="btnCancel_Click" />
    </asp:Panel>

</asp:Content>
