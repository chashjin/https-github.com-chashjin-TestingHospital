﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace HMS
{
    public partial class AddAppointment2 : System.Web.UI.Page
    {
        static string UserName = null;

        static string AppointmentType = null;
        static string DoctorName = null;

        //getAppoinmentID
        static int count = 0;
        static string appointmentID = null;
        //getStaffID
        static string staffName = null;
        //getPatientID
        static string patientID = null;

        static string status = "Pending";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginID"] == null)
            {
                Response.Redirect("~/TanAngie/LoginPage.aspx");
            }
            UserName = Session["LoginID"].ToString();
            lblUserName.Text = UserName;
            Session["LoginID"] = Session["LoginID"].ToString();

            DoctorName = Session["DoctorName"].ToString();
            lblDoctorName.Text = DoctorName;
            Session["DoctorName"] = Session["DoctorName"].ToString();

            AppointmentType = Session["AppointmentType"].ToString();
            //appType = AppointmentType;
            Session["AppointmentType"] = Session["AppointmentType"].ToString();

            //if (PreviousPage != null && PreviousPage.IsCrossPagePostBack)
            //{
            //    System.Web.UI.WebControls.Label name = PreviousPage.FindControl("lblDoctorName") as System.Web.UI.WebControls.Label;
            //    //System.Web.UI.WebControls.HiddenField hiddenDoctor = PreviousPage.FindControl("hiddenDoctor") as System.Web.UI.WebControls.HiddenField;
            //    DropDownList type = PreviousPage.FindControl("ddlType") as DropDownList;
            //    //System.Web.UI.WebControls.HiddenField DName = PreviousPage.FindControl("hiddenDoctorName") as System.Web.UI.WebControls.HiddenField;
            //    //doctorName = DName.Value;
            //    doctorName = name.Text;
                
            //    appType = type.SelectedItem.ToString();
            //}

            //Step 1: Create and Open Connection
            SqlConnection conHMS;
            String connStr = ConfigurationManager.ConnectionStrings["HMS"].ConnectionString;
            conHMS = new SqlConnection(connStr);
            conHMS.Open();

            String strSchedule, strDate, strTime;
            SqlCommand cmdSchedule, cmdDate, cmdTime;
            SqlDataReader dtr, dtr2, dtr3;

            strSchedule = "SELECT DISTINCT WS.WorkDay AS 'Work Day', T.ShiftTime AS 'Shift Time' FROM WorkingSchedule WS , Time T, Staff S " +
                           "WHERE T.TimeID = WS.TimeID AND " +
                           "WS.StaffID = S.StaffID AND " +
                           "S.StaffName = '" + DoctorName + "'";

            //strDate = "SELECT DISTINCT WS.WorkDay AS 'Work Day' FROM WorkingSchedule WS , Time T, Staff S " +
            //               "WHERE WS.StaffID = S.StaffID AND " +
            //               "S.StaffName = '" + lblDoctorName.Text + "'";

            strTime = "SELECT DISTINCT WS.WorkDay AS 'Work Day', T.ShiftTime AS 'Shift Time' FROM WorkingSchedule WS , Time T, Staff S " +
                           "WHERE T.TimeID = WS.TimeID AND " +
                           "WS.StaffID = S.StaffID AND " +
                           "S.StaffName = '" + DoctorName + "'";

            cmdSchedule = new SqlCommand(strSchedule, conHMS);
            dtr = cmdSchedule.ExecuteReader();
            ScheduleTime.DataSource = dtr;
            ScheduleTime.DataBind();
            dtr.Close();

            //cmdDate = new SqlCommand(strDate, conHMS);
            //dtr2 = cmdDate.ExecuteReader();
            //ddlDay.DataSource = dtr2;
            //ddlDay.Items.Add("");
            //ddlDay.DataValueField = "Work Day";
            //ddlDay.DataTextField = "Work Day";
            //ddlDay.DataBind();
            //dtr2.Close();

            cmdTime = new SqlCommand(strTime, conHMS);
            if (!IsPostBack)
            {
                dtr3 = cmdTime.ExecuteReader();
                ddlWSTime.DataSource = dtr3;
                ddlWSTime.Items.Add("");
                //ddlTime.DataValueField = "Work Day";
                ddlWSTime.DataTextField = "Shift Time";
                ddlWSTime.DataBind();
                dtr3.Close();
            }

            cmdTime = new SqlCommand(strTime, conHMS);
            if (!IsPostBack)
            {
                dtr3 = cmdTime.ExecuteReader();
                ddlWSDay.DataSource = dtr3;
                ddlWSDay.Items.Add("");
                //ddlTime.DataValueField = "Work Day";
                ddlWSDay.DataTextField = "Work Day";
                ddlWSDay.DataBind();
                dtr3.Close();
            }
            conHMS.Close();

            if (ScheduleTime.Rows.Count == 0)
            {
                Calendar1.Enabled = false;
                txtDate.Enabled = false;
                ddlTime.Enabled = false;
                btnConfirm.Enabled = false;
            }
        }


        //disable previous date
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now.Date)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray;
            }
            if (e.Day.IsWeekend)
            {
                e.Day.IsSelectable = false;
            }

            //if (e.Day.Equals(ddlWSDay.Text))
            //{
            //    e.Day.IsSelectable = true;
            //}

            if (e.Day.Date > (System.DateTime.Now.AddDays(30)))
            {
                e.Day.IsSelectable = false;
                e.Cell.Font.Strikeout = true;
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtDate.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");

            TextBox1.Text = Calendar1.SelectedDate.DayOfWeek.ToString();

            //Step 1: Create and Open Connection
            SqlConnection conHMS;
            String connStr = ConfigurationManager.ConnectionStrings["HMS"].ConnectionString;
            conHMS = new SqlConnection(connStr);
            conHMS.Open();

            String strTime;
            SqlCommand cmdTime;
            SqlDataReader dtr;

            strTime = "SELECT DISTINCT WS.WorkDay AS 'Work Day', T.ShiftTime AS 'Shift Time' FROM WorkingSchedule WS , Time T, Staff S " +
                           "WHERE T.TimeID = WS.TimeID AND " +
                           "WS.StaffID = S.StaffID AND " +
                           "WS.WorkDay = '" + TextBox1.Text + "'" + " AND " +
                           "S.StaffName = '" + lblDoctorName.Text + "'";

            cmdTime = new SqlCommand(strTime, conHMS);
            //if (!IsPostBack)
            //{
                dtr = cmdTime.ExecuteReader();
                ddlTime.DataSource = dtr;
                ddlTime.Items.Add("");
                //ddlTime.DataValueField = "Work Day";
                ddlTime.DataTextField = "Shift Time";
                ddlTime.DataBind();
                dtr.Close();
            //}
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            
            DialogResult dialogResult = MessageBox.Show("Are you sure want to make the appointment?", "New Appointment", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //Step 1: Create and Open Connection
                SqlConnection conHMS;
                String connStr = ConfigurationManager.ConnectionStrings["HMS"].ConnectionString;
                conHMS = new SqlConnection(connStr);
                conHMS.Open();

                String strAppointmentID, strStaffID, strPatientID, strInsert;
                SqlCommand cmdAppointmentID, cmdStaffID, cmdPatientID, cmdInsert;
                SqlDataReader dtr1, dtr2, dtr3, dtr4;

                //getAppointmentID
                strAppointmentID = "Select AppointmentID From Appointment";
                cmdAppointmentID = new SqlCommand(strAppointmentID, conHMS);
                dtr1 = cmdAppointmentID.ExecuteReader();
                if (dtr1.HasRows)
                {
                    while (dtr1.Read())
                    {
                        count += 1;
                        //appointmentID = dtr["AppointmentID"].ToString();
                    }
                }
                string digitFormat = string.Format("{0:D4}", count + 1);
                appointmentID = "AP" + digitFormat;
                dtr1.Close();
                //endOfAppointmentID

                //getStaffID
                strStaffID = "Select StaffID From Staff Where StaffName = @StaffName";
                cmdStaffID = new SqlCommand(strStaffID, conHMS);
                cmdStaffID.Parameters.AddWithValue("@StaffName", DoctorName);
                dtr2 = cmdStaffID.ExecuteReader();
                if (dtr2.HasRows)
                {
                    if (dtr2.Read())
                    {
                        staffName = dtr2["StaffID"].ToString();
                    }
                }
                dtr2.Close();
                //endOfStaffID

                //getPatientID
                strPatientID = "Select PatientID From Patient Where LoginID = @LoginID";
                cmdPatientID = new SqlCommand(strPatientID, conHMS);
                cmdPatientID.Parameters.AddWithValue("@LoginID", UserName);
                dtr3 = cmdPatientID.ExecuteReader();
                if (dtr3.HasRows)
                {
                    if (dtr3.Read())
                    {
                        patientID = dtr3["PatientID"].ToString();
                    }
                }
                dtr3.Close();
                //endOfPatientID

                //Insert Record to Appointment Table
                strInsert = "Insert Into Appointment (AppointmentID, AppointmentType, AppointmentDate, AppointmentTime, AppointmentStatus, StaffID, PatientID) " +
                            "Values (@AppointmentID, @AppointmentType, @AppointmentDate, @AppointmentTime, @AppointmentStatus, @StaffID, @PatientID) ";

                cmdInsert = new SqlCommand(strInsert, conHMS);
                cmdInsert.Parameters.AddWithValue("@AppointmentID", appointmentID);
                cmdInsert.Parameters.AddWithValue("@AppointmentType", AppointmentType);
                cmdInsert.Parameters.AddWithValue("@AppointmentDate", txtDate.Text);
                cmdInsert.Parameters.AddWithValue("@AppointmentTime", ddlTime.SelectedItem.Value.ToString());
                cmdInsert.Parameters.AddWithValue("@AppointmentStatus", status);
                cmdInsert.Parameters.AddWithValue("@StaffID", staffName);
                cmdInsert.Parameters.AddWithValue("@PatientID", patientID);

                /*Step 3: Execute command to insert*/
                //int n = cmdInsert.ExecuteNonQuery();

                /*Display insert status*/
                //if (n > 0)
                //    System.Windows.Forms.MessageBox.Show("New Appointment details added.");
                //else
                //    System.Windows.Forms.MessageBox.Show("Sorry, insertion failed.");


                dtr4 = cmdInsert.ExecuteReader();
                dtr4.Close();
                conHMS.Close();
                MessageBox.Show("New Appointment details added.");
                Response.Redirect("~/TanDingKang/AppointmentHomePage.aspx");
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Sorry, insertion failed.");
                Response.Redirect("~/TanDingKang/AppointmentHomePage.aspx");
            }
        }
    }
}