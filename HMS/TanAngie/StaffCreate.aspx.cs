﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace StaffManagement
{
    public partial class StaffCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlPosition.SelectedItem.Value.Equals("Manager"))
            {
                int managerCheck = DepartmentManagerCheck(DepartmentCheck());
                buttonSubmit();
            }
            else
            {
                buttonSubmit();
            }

        }
        protected void buttonSubmit()
        {
            if (lblIcCheckMesg.Text.Equals("The Idendity Card is Valid."))
            {
                int idcheck = loginIDCheck();
                if (idcheck > 0)
                {
                    int check = PasswordCheck();
                    if (check > 0)
                    {
                        if (MessageBox.Show("Confirm to Create the Staff?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            DataSave();
                            if (ddlPosition.SelectedItem.Value.Equals("Doctor"))
                                Response.Redirect("~/TanAngie/StaffAddNewSchedule.aspx");
                            else
                            {
                                tbAddress.Text = "";  //Reset All The Things
                                tbContactNum.Text = "";
                                tbEmail.Text = "";
                                tbIC.Text = "";
                                tbLoginId.Text = "";
                                tbName.Text = "";
                                tbSalary.Text = "";
                                tbSecurityPw.Text = "";
                                lblIcCheckMesg.Text = "";
                                ddlDepartment.ClearSelection();
                                ddlPosition.ClearSelection();
                                ddlSecurityQuestion.ClearSelection();
                                rblGender.ClearSelection();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Check the Validation of Idendity Card.");
                }
            }
        }
        protected void DataSave()
        {
            String StaffID = StaffIdAssign();
            String StaffStatus = "Active";
            String departmentID = DepartmentCheck();

            //Data Save
            SqlConnection conDatabase;
            string connStr = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
            conDatabase = new SqlConnection(connStr);
            conDatabase.Open();

            SqlCommand cmdCreateStaff; //For Staff
            SqlCommand cmdCreateLogin; //For Login
            string strCreateStaff; //For Staff
            string strCreateLogin; //For Login

            //For Login Table
            strCreateLogin = "Insert Into Login (LoginID,LoginPW,SecurityQ,SecurityPW) Values" +
               " (@LoginID,@LoginPW,@SecurityQ,@SecurityPW)";
            cmdCreateLogin = new SqlCommand(strCreateLogin, conDatabase);
            cmdCreateLogin.Parameters.AddWithValue("@LoginID", tbLoginId.Text);
            cmdCreateLogin.Parameters.AddWithValue("@LoginPW", pwPassword.Value);
            cmdCreateLogin.Parameters.AddWithValue("@SecurityQ", ddlSecurityQuestion.SelectedItem.Value);
            cmdCreateLogin.Parameters.AddWithValue("@SecurityPW", tbSecurityPw.Text);
            cmdCreateLogin.ExecuteNonQuery();

            //For Staff Table
            strCreateStaff = "Insert Into Staff (StaffID,StaffIC,StaffName,StaffGender," +
                "StaffAddr,StaffContactNo,Position,Salary,StaffStatus,EmailID,EmailPW," +
                "DepartmentID,LoginID) Values (@StaffID,@StaffIC,@StaffName,@StaffGender," +
                "@StaffAddr,@StaffContactNo,@Position,@Salary,@StaffStatus,@EmailID,@EmailPW," +
                "@DepartmentID,@LoginID)";
            cmdCreateStaff = new SqlCommand(strCreateStaff, conDatabase);
            cmdCreateStaff.Parameters.AddWithValue("@StaffID", StaffID);
            cmdCreateStaff.Parameters.AddWithValue("@StaffIC", tbIC.Text);
            cmdCreateStaff.Parameters.AddWithValue("@StaffName", tbName.Text);
            cmdCreateStaff.Parameters.AddWithValue("@StaffGender", rblGender.SelectedItem.Value);
            cmdCreateStaff.Parameters.AddWithValue("@StaffAddr", tbAddress.Text);
            cmdCreateStaff.Parameters.AddWithValue("@StaffContactNo", tbContactNum.Text);
            cmdCreateStaff.Parameters.AddWithValue("@Position", ddlPosition.SelectedItem.Value);
            cmdCreateStaff.Parameters.AddWithValue("@Salary", tbSalary.Text);
            cmdCreateStaff.Parameters.AddWithValue("@StaffStatus", StaffStatus);
            cmdCreateStaff.Parameters.AddWithValue("@EmailID", tbEmail.Text);
            cmdCreateStaff.Parameters.AddWithValue("@EmailPW", pwEmail.Value);
            cmdCreateStaff.Parameters.AddWithValue("@DepartmentID", departmentID);
            cmdCreateStaff.Parameters.AddWithValue("@LoginID", tbLoginId.Text);
            int n = cmdCreateStaff.ExecuteNonQuery();
            if (n > 0)
            {
                MessageBox.Show("New Staff's Details had Saved Successfuly.\n\nStaff ID : "+StaffID);
                
            }
            else
                MessageBox.Show("Error, the Data Cannot be Saved.");

            conDatabase.Close();
        }
        protected String StaffIdAssign()
        {
            String lastId;
            String convertId;
            Int32 idAdd1;
            String newId = null;

            SqlConnection conDatabase;
            string connStr = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
            conDatabase = new SqlConnection(connStr);
            conDatabase.Open();
            string strCheck;
            SqlCommand cmdCheck;
            strCheck = "Select TOP 1 StaffID From dbo.Staff Where Position = @position Order By StaffID DESC";
            cmdCheck = new SqlCommand(strCheck, conDatabase);
            cmdCheck.Parameters.AddWithValue("@position",ddlPosition.SelectedItem.Value);
            SqlDataReader dtr;
            dtr = cmdCheck.ExecuteReader();
            if (dtr.Read())
            {
                MessageBox.Show(""+dtr["StaffID".ToString()]);
                lastId = "" + dtr["StaffID".ToString()];
                convertId = lastId.Substring(1);
                idAdd1 = Convert.ToInt32(convertId);
                idAdd1 += 1;
                if (ddlPosition.SelectedItem.Value.Equals("Doctor"))
                {
                    if (idAdd1 < 9)
                        newId = "D000" + Convert.ToString(idAdd1);
                    else if (idAdd1 < 99)
                        newId = "D00" + Convert.ToString(idAdd1);
                    else if (idAdd1 < 999)
                        newId = "D0" + Convert.ToString(idAdd1);
                    else if (idAdd1 < 9999)
                        newId = "D" + Convert.ToString(idAdd1);
                }
                else if (ddlPosition.SelectedItem.Value.Equals("Nurse"))
                {
                    if (idAdd1 < 9)
                        newId = "N000" + Convert.ToString(idAdd1);
                    else if (idAdd1 < 99)
                        newId = "N00" + Convert.ToString(idAdd1);
                    else if (idAdd1 < 999)
                        newId = "N0" + Convert.ToString(idAdd1);
                    else if (idAdd1 < 9999)
                        newId = "N" + Convert.ToString(idAdd1);
                }
                else if (ddlPosition.SelectedItem.Value.Equals("Clerk"))
                {
                    if (idAdd1 < 9)
                        newId = "S000" + Convert.ToString(idAdd1);
                    else if (idAdd1 < 99)
                        newId = "S00" + Convert.ToString(idAdd1);
                    else if (idAdd1 < 999)
                        newId = "S0" + Convert.ToString(idAdd1);
                    else if (idAdd1 < 9999)
                        newId = "S" + Convert.ToString(idAdd1);
                }
                else if (ddlPosition.SelectedItem.Value.Equals("Manager"))
                {
                    newId = "M000" + Convert.ToString(idAdd1);
                }
            }
            else
            {
                MessageBox.Show("data not found.");
            }
            conDatabase.Close();
            dtr.Close();
            return newId;
        }
        protected String DepartmentCheck()
        {
            String departmentId = null;
            if (ddlDepartment.SelectedItem.Value == "Human Resource")
            {
                departmentId = "DP001";
            }
            else if (ddlDepartment.SelectedItem.Value == "Maternity Department")
            {
                departmentId = "DP002";
            }
            else if (ddlDepartment.SelectedItem.Value == "General Disease")
            {
                departmentId = "DP003";
            }
            else if (ddlDepartment.SelectedItem.Value == "Pharmacy")
            {
                departmentId = "DP004";
            }
            return departmentId;
        }
        protected void btnReset_Click(object sender, EventArgs e) //Reset All The Things
        {
            tbAddress.Text = "";
            tbContactNum.Text = "";
            tbEmail.Text = "";
            tbIC.Text = "";
            tbLoginId.Text = "";
            tbName.Text = "";
            tbSalary.Text = "";
            tbSecurityPw.Text = "";
            lblIcCheckMesg.Text = "";
            ddlDepartment.ClearSelection();
            ddlPosition.ClearSelection();
            ddlSecurityQuestion.ClearSelection();
            rblGender.ClearSelection();
        }
        protected void btnCheck_Click(object sender, EventArgs e)//Check IC
        {
            SqlConnection conDatabase;
            string connStr = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
            conDatabase = new SqlConnection(connStr);
            conDatabase.Open();
            string strCheck;
            SqlCommand cmdCheck;
            strCheck = "Select * From dbo.Staff Where StaffIC = @IC And Position = '"+ddlPosition.SelectedItem.Value+"'";
            cmdCheck = new SqlCommand(strCheck, conDatabase);
            cmdCheck.Parameters.AddWithValue("@IC", tbIC.Text);
            SqlDataReader dtr;
            dtr = cmdCheck.ExecuteReader();
            if (dtr.Read())
            {
                lblIcCheckMesg.Text = "The Idendity Card had already exist. Please check from the staff details.";
            }
            else
            {
                lblIcCheckMesg.Text = "The Idendity Card is Valid.";
            } 
            conDatabase.Close();
            dtr.Close();
        }
        protected int PasswordCheck()
        {
            if (pwPassword.Value.Equals(pwConfirm.Value))
            {
                return 1;
            }
            else
            {
                MessageBox.Show("Please Check that Password must Same with Confirm Password.");
                return 0;
            }
        }
        protected int loginIDCheck()
        {
            SqlConnection conDatabase;
            string connStr = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
            conDatabase = new SqlConnection(connStr);
            conDatabase.Open();
            string strCheck;
            SqlCommand cmdCheck;
            strCheck = "Select * From dbo.Login Where LoginID = @LoginID";
            cmdCheck = new SqlCommand(strCheck, conDatabase);
            cmdCheck.Parameters.AddWithValue("@LoginID", tbLoginId.Text);
            SqlDataReader dtr;
            dtr = cmdCheck.ExecuteReader();
            if (dtr.Read())
            {
                MessageBox.Show("The LoginID had already exist.Please insert a new LoginID.");
                conDatabase.Close();
                dtr.Close();
                return 0;
            }
            else
            {
                conDatabase.Close();
                dtr.Close();
                return 1;
            }
        }
        protected int DepartmentManagerCheck(string departmentid)
        {
            SqlConnection conDatabase;
            string connStr = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
            conDatabase = new SqlConnection(connStr);
            conDatabase.Open();
            string strCheck;
            SqlCommand cmdCheck;
            strCheck = "Select * From dbo.Staff Where Position = 'Manager' AND StaffStatus = 'Active' AND DepartmentID = '"+departmentid+"'";
            cmdCheck = new SqlCommand(strCheck, conDatabase);
            SqlDataReader dtr;
            dtr = cmdCheck.ExecuteReader();
            if (dtr.Read())
            {
                MessageBox.Show("This department already have a Manager.\n");
                conDatabase.Close();
                dtr.Close();
                return 0;
            }
            else
            {
                conDatabase.Close();
                dtr.Close();
                return 1;
            }
        }
    }
}