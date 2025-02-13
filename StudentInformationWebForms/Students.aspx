<%@ Page Title="Students" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="StudentInformationWebForms.Students" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Student Management</h2>

    <!-- GridView to display student records -->
    <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False" DataKeyNames="StudentID"
        CssClass="table table-bordered" OnRowCommand="gvStudents_RowCommand">
        <Columns>
            <asp:BoundField DataField="StudentID" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Age" HeaderText="Age" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button runat="server" CssClass="btn btn-primary btn-sm" CommandName="EditStudent" CommandArgument='<%# Eval("StudentID") %>' Text="Edit" />
                    <asp:Button runat="server" CssClass="btn btn-danger btn-sm" CommandName="DeleteStudent" CommandArgument='<%# Eval("StudentID") %>' Text="Delete" OnClientClick="return confirm('Are you sure?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br />
    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add New Student" OnClick="btnAdd_Click" />

    <br /><br />

    <!-- Student Form Panel (Hidden by Default) -->
    <asp:Panel ID="pnlStudentForm" runat="server" CssClass="border p-3" Visible="False">
        <h3><asp:Label ID="lblFormTitle" runat="server" Text="Add New Student"></asp:Label></h3>
        
        <asp:HiddenField ID="hfStudentId" runat="server" />
        
        <div class="mb-3">
            <label>Name:</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Enter Name"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label>Age:</label>
            <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" Placeholder="Enter Age" TextMode="Number"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label>Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Enter Email"></asp:TextBox>
        </div>

        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="Cancel" OnClick="btnCancel_Click" />
    </asp:Panel>

</asp:Content>
