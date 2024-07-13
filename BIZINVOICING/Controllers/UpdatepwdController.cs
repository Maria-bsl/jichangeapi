using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Configuration;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class UpdatepwdController : AdminBaseController
    {
        // GET: Updatepwd
        REG_QSTN q = new REG_QSTN();
        EMP_DET Emp = new EMP_DET();
        CompanyUsers cu = new CompanyUsers();
        private readonly dynamic returnNull = null;
        public ActionResult Updatepwd()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }
        [HttpGet]
        public ActionResult GetqDetails()
        {
            try
            {
                var result = q.GetQSTNActive();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }

        [HttpPost]
        
        public ActionResult Addpwd(String pwd, int qustSno, String qust, String ansr, String posted_by, String type, long? Instid, long? Usersno)
        {
            try
            {
                if (type == "Emp")
                {
                    var check = Emp.Validatepwdbank(GetEncryptedData(pwd), long.Parse(Session["UserID"].ToString()));
                    
                    if (check == false)
                    {
                        Emp.Password = GetEncryptedData(pwd);
                        Emp.SNO = qustSno;
                        Emp.Q_Name = qust;
                        Emp.Q_Ans = ansr;
                        Emp.F_Login = "true";
                        Emp.AuditBy = posted_by;
                        Emp.Detail_Id = (long)Usersno;
                        Emp.UpdateQuestionEMP(Emp);
                        return Json(Usersno, JsonRequestBehavior.AllowGet);
                    }

                    else
                    {
                        return Json(check, JsonRequestBehavior.AllowGet);
                    }
                }
                else {
                    var check1 = cu.Validatepwdbank(GetEncryptedData(pwd), long.Parse(Session["UserID"].ToString()));
                    if (check1 == false)
                    {
                        cu.Password = GetEncryptedData(pwd);
                        cu.Sno = qustSno;
                        cu.Qname = qust;
                        cu.Qans = ansr;
                        cu.Flogin = "true";
                        cu.PostedBy = posted_by;
                        cu.CompuserSno = (long)Usersno;
                        cu.UpdateQuestionEMP(cu);
                        return Json(Usersno, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(check1, JsonRequestBehavior.AllowGet);
                    }






                }

            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return returnNull;
        }

        public static string GetEncryptedData(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

    }
}