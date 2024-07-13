using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginUserController : ApiController
    {
       

        EMP_DET emp = new EMP_DET();
        TRACK_DET dt = new TRACK_DET();
        CompanyUsers cu = new CompanyUsers();
        CompanyBankMaster co = new CompanyBankMaster();
        DESIGNATION des = new DESIGNATION();
        // tr dt = new TRACK_DET();
        bool dp;

        [AllowAnonymous]       
        [HttpPost]
        public HttpResponseMessage AddLogins(AuthLog a)
        {
            if (ModelState.IsValid) { 
                try
                {
                    var returnField = (Object)null;
                    string pwd1 = GetEncryptedData(a.password);
                    var empdata = emp.CheckLogin(a.userName, pwd1);
                    var company = cu.CheckLogin(a.userName, pwd1);
                    if (empdata != null)
                    {
                        var role = "Bank";
                        var jwtToken = Authentication.Authentication.GenerateJWTAuthetication(a.userName, role);
                        var validUserName = Authentication.Authentication.ValidateToken(jwtToken);

                        var sessB = "BNk";
                        var userid = empdata.Detail_Id;
                        var Username = empdata.User_name;
                        var UfullName = empdata.First_Name + " " + empdata.Last_name;
                        var admin1 = "Bank";
                        var flogin = empdata.F_Login;
                        dt.Full_Name = empdata.Full_Name;
                        dt.Facility_Reg_No = 0;
                        dt.Ipadd = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        //dt.Ipadd = System.Web.HttpContext.Current.Request.UserHostAddress;
                        dt.Email = empdata.Email_Address;
                        dt.Posted_by = Convert.ToString(empdata.Detail_Id);
                        dt.Login_Time = DateTime.Now;
                        dt.Description = "Biz";
                        dt.AddTrack(dt);
                        var getD = des.Editdesignation(empdata.Desg_Id);
                        var desig = getD.Desg_Name;
                        var BRAID = "";
                        if (empdata.Branch_Sno != null)
                        {
                            BRAID = empdata.Branch_Sno.ToString();
                        }
                        else
                        {
                             BRAID = "0";
                        }
                        returnField = new { Token = jwtToken, check = "Emp", flogin = empdata.F_Login,desig = desig, braid = BRAID, Usno = empdata.Detail_Id, sessB = sessB,role = role, Uname = Username, fulname = UfullName, userid = userid };
                        return Request.CreateResponse(new{ response = returnField, message ="Success"} );
                    }
                    if (company != null)
                    {

                        var role = "Company";
                        var jwtToken = Authentication.Authentication.GenerateJWTAuthetication(a.userName, role);
                        var validUserName = Authentication.Authentication.ValidateToken(jwtToken);
                        var sessComp = "Comp";
                        var userid = company.CompuserSno;
                        var CompID = company.Compmassno;
                        var admin1 = "Companys";
                        var flogin = company.Flogin;
                        var UfullName = company.Username;
                        var braid = company.Sno;
                        //if (company.Usertype == "admin")
                        //{
                        //    var admin1"] = "Institution Admin";
                        //}
                        //else
                        //{
                        //    var admin1"] = company.Usertype;
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
                        returnField = new { Token = jwtToken, check = "company", flogin = company.Flogin, InstID = company.Compmassno,role = admin1, braid = braid, Usno = company.CompuserSno, desig = admin1, sessComp = sessComp, userid = userid, Uname = UfullName };
                        return Request.CreateResponse(new { response = returnField, message = "Success" });

                    }
                    else
                    {

                        var returnField0 = new { check = "Username or password is incorrect" };
                        return Request.CreateResponse(new { response = returnField0, message = "Failed"});
                    }

                }
                catch (Exception e)
                {
                    e.ToString();
                    // return Request.CreateErrorResponse(HttpResponseException);
                
                }
                
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            return null;
        }

        //   [NoCache]



        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Logout(MainForm m)
        {
            if (m.userid != null)
            {
                emp.Detail_Id = Convert.ToInt64(m.userid.ToString());
                // emp.UpdateOnlyflsg(emp);
                var result = dt.EditTRACK(m.userid.ToString());
                dt.SNO = result.SNO;
                dt.Posted_by = m.userid.ToString();
                dt.UpdateTRACKEmp(dt);
            }
            //else
            //{
            //    //cu.CompuserSno = Convert.ToInt64(userid.ToString());
            //    //// emp.UpdateOnlyflsg(emp);
            //    //var result = dt.EditTRACK(userid.ToString());
            //    //dt.SNO = result.SNO;
            //    //dt.Posted_by = userid.ToString();
            //    //dt.UpdateTRACKEmp(dt);
            //}
           /* Session.Clear();
            Session.Abandon();
            Session.RemoveAll();*/
            return Request.CreateResponse(new {response = "Successfully Log Out", message = "Success"});
        }

        private static string GetEncryptedData(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }



        private static string GetEncryptedData2(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        private static string DecodeFrom64(string password)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(password);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

    }
}
