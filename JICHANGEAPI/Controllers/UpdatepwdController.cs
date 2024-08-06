using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UpdatepwdController : SetupBaseController
    {
        // GET: Updatepwd
        REG_QSTN q = new REG_QSTN();
        EMP_DET Emp = new EMP_DET();
        CompanyUsers cu = new CompanyUsers();
        private readonly dynamic returnNull = null;


        [HttpPost]
        public HttpResponseMessage UpdatePwd(UpdatePassModel updatePassModel)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }

            try
            {

                if (updatePassModel.pwd == updatePassModel.confirmPwd)
                {
                    return GetCustomErrorMessageResponse(new List<string> {"Password does not match."});
                }

                if (updatePassModel.type == "Emp")
                {
                    var check = Emp.Validatepwdbank(GetEncryptedData(updatePassModel.pwd), long.Parse(updatePassModel.userid.ToString()));

                    if (check == false)
                    {
                        Emp.Password = GetEncryptedData(updatePassModel.pwd);
                        Emp.F_Login = "true";
                        Emp.AuditBy = updatePassModel.userid.ToString();
                        Emp.Detail_Id = (long)updatePassModel.userid;
                        Emp.UpdateQuestionEMP(Emp);
                        return GetSuccessResponse(updatePassModel.userid);
                    }

                    else
                    {
                        return GetNotFoundResponse();
                    }
                }
                else
                {
                    var check1 = cu.Validatepwdbank(GetEncryptedData(updatePassModel.pwd), long.Parse(updatePassModel.userid.ToString()));
                    if (check1 == false)
                    {
                        cu.Password = GetEncryptedData(updatePassModel.pwd);
                        cu.Flogin = "true";
                        cu.PostedBy = updatePassModel.userid.ToString();
                        cu.CompuserSno = (long)updatePassModel.userid;
                        cu.UpdateQuestionEMP(cu);

                        return GetSuccessResponse(updatePassModel.userid);
                    }
                    else
                    {
                        return GetNotFoundResponse();
                    }


                }


            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.ToString());
            }

        }
      

        [HttpGet]
        public HttpResponseMessage GetqDetails()
        {
            try
            {
                var result = q.GetQSTNActive();

                return GetSuccessResponse(result);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }

        [HttpPost]
        public HttpResponseMessage Addpwd(String pwd, int qustSno, String qust, String ansr, String posted_by, String type,string userid, long? Instid, long? Usersno)
        {
            try
            {
                if (type == "Emp")
                {
                    var check = Emp.Validatepwdbank(GetEncryptedData(pwd), long.Parse(userid.ToString()));

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
                        return GetSuccessResponse(Usersno);
                    }

                    else
                    {
                        return GetNotFoundResponse();
                    }
                }
                else
                {
                    var check1 = cu.Validatepwdbank(GetEncryptedData(pwd), long.Parse(userid.ToString()));
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
                       
                        return GetSuccessResponse(Usersno);
                    }
                    else
                    {
                      return GetNotFoundResponse();
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
