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
    public class LoginnewController : AdminBaseController
    {
        // GET: Login
        EMP_DET emp = new EMP_DET();
        TRACK_DET dt = new TRACK_DET();
        CompanyUsers cu = new CompanyUsers();
        DESIGNATION des = new DESIGNATION();
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
                var company = cu.CheckLogin(uname, pwd1);
                if (empdata != null)
                {
                    Session["sessB"] = "BNk";
                    Session["UserID"] = empdata.Detail_Id;
                    Session["Username"] = empdata.User_name;
                    Session["UfullName"] = empdata.First_Name + " " + empdata.Last_name;
                    Session["admin1"] = "Bank";
                    Session["flogin"] = empdata.F_Login;
                    dt.Full_Name = empdata.Full_Name;
                    dt.Facility_Reg_No =0;
                    dt.Ipadd = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    //dt.Ipadd = System.Web.HttpContext.Current.Request.UserHostAddress;
                    dt.Email = empdata.Email_Address;
                    dt.Posted_by = Convert.ToString(empdata.Detail_Id);
                    dt.Login_Time = DateTime.Now;
                    dt.Description = "Biz";
                    dt.AddTrack(dt);
                    var getD = des.Editdesignation(empdata.Desg_Id);
                    Session["desig"] = getD.Desg_Name;
                    if (empdata.Branch_Sno != null)
                    {
                        Session["BRAID"] = empdata.Branch_Sno;
                    }
                    else
                    {
                        Session["BRAID"] = "0";
                    }
                    returnField = new { check = "Emp", flogin = empdata.F_Login, Usno = empdata.Detail_Id };
                    return Json(returnField, JsonRequestBehavior.AllowGet);
                }
                if (company != null)
                {
                    Session["sessComp"] = "Comp";
                    Session["UserID"] = company.CompuserSno;
                    Session["CompID"] = company.Compmassno;
                    Session["admin1"] = "Companys";
                    Session["flogin"] = company.Flogin;
                    Session["UfullName"] =company.Username;
                    //if (company.Usertype == "admin")
                    //{
                    //    Session["admin1"] = "Institution Admin";
                    //}
                    //else
                    //{
                    //    Session["admin1"] = company.Usertype;
                    //}
                    dt.Full_Name = company.Fullname;
                    dt.Facility_Reg_No = company.Compmassno;
                    dt.Ipadd = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    //dt.Ipadd = System.Web.HttpContext.Current.Request.UserHostAddress;
                    dt.Email = company.Email;
                    dt.Posted_by = Convert.ToString(company.CompuserSno);
                    dt.Login_Time = DateTime.Now;
                    dt.Description = "company";
                    dt.AddTrack(dt);
                    returnField = new { check = "company", flogin = company.Flogin, InstID = company.Compmassno, Usno = company.CompuserSno };
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
                var result = dt.EditTRACK(Session["UserID"].ToString());
                dt.SNO = result.SNO;
                dt.Posted_by = Session["UserID"].ToString();
                dt.UpdateTRACKEmp(dt);
            }
            //else
            //{
            //    //cu.CompuserSno = Convert.ToInt64(Session["UserID"].ToString());
            //    //// emp.UpdateOnlyflsg(emp);
            //    //var result = dt.EditTRACK(Session["UserID"].ToString());
            //    //dt.SNO = result.SNO;
            //    //dt.Posted_by = Session["UserID"].ToString();
            //    //dt.UpdateTRACKEmp(dt);
            //}
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
