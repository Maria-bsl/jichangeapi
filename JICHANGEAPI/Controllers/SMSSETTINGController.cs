using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JichangeApi.Controllers
{
    public class SMSSETTINGController : SetupBaseController
    {
        // GET: SMSSETNGS
        SMS_SETTING smtp = new SMS_SETTING();
        EMP_DET ed = new EMP_DET();
        private readonly dynamic returnNull = null;
        //Arights act = new Arights();
      


        [HttpPost]
        public HttpResponseMessage AddSMTP(AddSmtpModel addsm)
        {
            try
            {
                smtp.From_Address = addsm.from_address;
                smtp.USER_Name = addsm.smtp_uname;
                smtp.Password = Utilites.GetEncryptedData(addsm.smtp_pwd);
                smtp.Mobile_Service = addsm.smtp_mob;
                smtp.AuditBy = addsm.userid.ToString();
                smtp.SNO = addsm.sno;
                long ssno = 0;
                if (addsm.sno == 0)
                {
                    ssno = smtp.AddSMS(smtp);
                    return Request.CreateResponse(new { response = ssno, message = new List<string> { } });

                }
                else if (addsm.sno > 0)
                {
                    smtp.UpdateSMS(smtp);
                    ssno = addsm.sno;
                    return Request.CreateResponse(new { response = ssno, message = new List<string> { } });
                }


            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public HttpResponseMessage DeleteSMTP(long sno)
        {
            try
            {

                smtp.SNO = sno;
                if (sno > 0)
                {
                    smtp.DeleteSMS(sno);
                }
                var result = sno;

                return Request.CreateResponse(new { response = result, message = new List<string> { } });
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public HttpResponseMessage GetSMTPDetails()
        {
            try
            {
                var result = smtp.GetSMS();
                if (result != null)
                {
                    return Request.CreateResponse(new { response = result, message = new List<string> { } });
                }
                else
                {
                    var d = 0;
                    return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return returnNull;
        }


    }
}
