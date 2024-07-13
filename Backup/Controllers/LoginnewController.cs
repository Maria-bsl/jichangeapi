using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using System.Globalization;

namespace BIZINVOICING.Controllers
{
    public class LoginnewController : Controller
    {
        // GET: Login
        EMP_DET emp = new EMP_DET();
        TRACK_DET dt = new TRACK_DET();
        // tr dt = new TRACK_DET();
        bool dp;
        public ActionResult Loginnew()
        {

            return View();
        }
        [HttpPost]
        public ActionResult addLogin(string uname, string pwd)
        {
            try
            {
                var returnField = (Object)null;
                String pwd1 = GetEncryptedData(pwd);
                var empdata = emp.CheckLogin(uname, pwd1);
                if (empdata != null)
                {
                    Session["sessB"] = "BNk";
                    Session["UserID"] = empdata.Detail_Id;
                    Session["Username"] = empdata.User_name;
                    Session["UfullName"] = empdata.First_Name + " " + empdata.Last_name;
                    Session["admin1"] = "Bank";
                    Session["flogin"] = empdata.F_Login;
                    dt.Full_Name = empdata.Full_Name;
                    dt.Facility_Reg_No = Convert.ToString(empdata.Detail_Id);
                    dt.Ipadd = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    //dt.Ipadd = System.Web.HttpContext.Current.Request.UserHostAddress;
                    dt.Email = empdata.Email_Address;
                    dt.Posted_by = Convert.ToString(empdata.Detail_Id);
                    dt.Login_Time = DateTime.Now;
                    dt.Description = "BankStaff";
                    returnField = new { check = "Emp", flogin = empdata.F_Login, Usno = empdata.Detail_Id };
                    return Json(returnField, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var returnField0 = new { check = "Username or password is incorrect" };
                    return Json(returnField0, JsonRequestBehavior.AllowGet);
                }

            }
            catch(Exception e)
            {
                e.ToString();
            }
            return null;
        }

     //   [NoCache]
        public ActionResult Logout()
        {
            if (Session["UserID"] != null)
            {
                emp.Detail_Id = Convert.ToInt64(Session["UserID"].ToString());
               // emp.UpdateOnlyflsg(emp);
                //var result = dt.EditTRACK(Session["UserID"].ToString());
                //dt.SNO = result.SNO;
                //dt.Posted_by = Session["UserID"].ToString();
                //dt.UpdateTRACKEmp(dt);
            }
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Loginnew", "Loginnew");
        }
        public string GetEncryptedData(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
    }
}
