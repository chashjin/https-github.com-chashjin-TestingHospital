﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StaffUpdate.aspx.cs" Inherits="StaffManagement.StaffUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style type="text/css">

        .auto-style1 {
            width: 158px;
        }

        .auto-style2 {
            width: 158px;
            height: 30px;
        }
        .auto-style3 {
            height: 30px;
        }

        </style>

    <div class="background">
        
        <div>
            <h1 style="color:darkblue">Staff Update Details</h1>
        </div>
        <div>
            <table style="width:100%;">
                <tr>
                    <td class="auto-style1">Staff ID :</td>
                    <td>
                        <asp:TextBox ID="tbStaffId" runat="server" Enabled="False" BackColor="#99CCFF"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Name :</td>
                    <td>
                        <asp:TextBox ID="tbName" runat="server" Enabled="False" BackColor="#99CCFF"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">IC Number :</td>
                    <td>
                        <asp:TextBox ID="tbIC" runat="server" Enabled="False" BackColor="#99CCFF"></asp:TextBox>
                        &nbsp;&nbsp;
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Gender :</td>
                    <td>
                        <asp:TextBox ID="tbGender" runat="server" Enabled="False" BackColor="#99CCFF"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Home Address :</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tbAddress" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbAddress" ErrorMessage="Please insert Address" ForeColor="Red" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Contact Number :</td>
                    <td>
                        <asp:TextBox ID="tbContactNum" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbContactNum" ErrorMessage="Please insert Contact Number" ForeColor="Red" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbContactNum" ErrorMessage="Invalid Contact Number. (exp: 0168528999)" ForeColor="Red" ValidationExpression="\d{10}" ValidationGroup="Submit">*</asp:RegularExpressionValidator>
                    
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Email Address :</td>
                    <td>
                        <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbEmail" ErrorMessage="Please insert Email" ForeColor="Red" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbEmail" ErrorMessage="Invalid Email Address. (exp: abc@hotmail.com)" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Submit">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1">Department and Position</td>
                    <td></td>
                </tr><tr>
                    <td class="auto-style1">Department :</td>
                    <td>
                        <asp:DropDownList ID="ddlDepartment" runat="server">
                            <asp:ListItem>--Please Choose One--</asp:ListItem>
                            <asp:ListItem>Human Resource</asp:ListItem>
                            <asp:ListItem>Maternity Department</asp:ListItem>
                            <asp:ListItem>General Disease</asp:ListItem>
                            <asp:ListItem>Pharmacy</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDepartment" ErrorMessage="Please Select a Department" ForeColor="Red" InitialValue="--Please Choose One--" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </td>
                </tr><tr>
                    <td class="auto-style1">Position :</td>
                    <td>
                        <asp:DropDownList ID="ddlPosition" runat="server">
                            <asp:ListItem Value="0">--Please Choose One--</asp:ListItem>
                            <asp:ListItem Value="Doctor">Doctor</asp:ListItem>
                            <asp:ListItem Value="Nurse">Nurse</asp:ListItem>
                            <asp:ListItem>Manager</asp:ListItem>
                            <asp:ListItem>Clerk</asp:ListItem>
                            <asp:ListItem>Admin</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlPosition" ErrorMessage="Please Select Position" ForeColor="Red" InitialValue="0" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Salary :</td>
                    <td>
                        <asp:TextBox ID="tbSalary" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tbSalary" ErrorMessage="Please insert Salary" ForeColor="Red" ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                    
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="tbSalary" ErrorMessage="Salary must be in digit number." ForeColor="Red" ValidationExpression="[0-9]*\.?[0-9]*" ValidationGroup="Submit">*</asp:RegularExpressionValidator>
                    
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style1"></td>
                    <td>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                    </td>
                </tr>
            </table>
        </div>
    
    </div>
                        <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server" HeaderText="Please Check Again:" ValidationGroup="Submit" />
</asp:Content>