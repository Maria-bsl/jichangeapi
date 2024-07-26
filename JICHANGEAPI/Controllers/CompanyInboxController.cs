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
    public class CompanyInboxController : SetupBaseController
    {
        // GET: CompanyInbox
        //string f_Path = System.Web.Configuration.WebConfigurationManager.AppSettings["FileP"].ToString();
        CompanyBankMaster c = new CompanyBankMaster();
        REGION r = new REGION();
        CompanyUsers cu = new CompanyUsers();
        DISTRICTS d = new DISTRICTS();
        WARD w = new WARD();
        Auditlog ad = new Auditlog();
        TRARegistration tra = new TRARegistration();
        APIReg areg = new APIReg();
        private readonly dynamic returnNull = null;
        EMAIL em = new EMAIL();
        S_SMTP ss = new S_SMTP();
        langcompany lc = new langcompany();
      

        [HttpPost]
        public HttpResponseMessage GetCompanys(Desibraid d)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                var result = d.design.ToString().ToLower() == "administrator" ? c.GetCompany1() : c.GetCompany1_Branch(long.Parse(d.braid.ToString()));
                return this.GetList<List<CompanyBankMaster>, CompanyBankMaster>(result);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddCompanyBank(AddCompanyApproveModel apa)
        {
            try
            {
                C_Deposit cd = new C_Deposit();
                c.CompSno = apa.compsno;
                c.Postedby = apa.userid.ToString();
                c.Status = "Approved";
                c.Sus_Ac_SNo = apa.ssno;

                cd.Deposit_Acc_No = apa.pfx;
                cd.Comp_Mas_Sno = apa.compsno;
                //cd.Reason = 
                cd.AuditBy = apa.userid.ToString();

                #region
                /*var getTin = c.EditCompanyss(compsno);
                if(getTin != null)
                {
                    if (!tra.ValidateReg(Int32.Parse(getTin.TinNo)))
                    {
                        string Rand = "12345678";
                        Random random = new Random();
                        string combination = "1234567890";
                        StringBuilder captcha = new StringBuilder();
                        for (int i = 0; i < 6; i++)
                            captcha.Append(combination[random.Next(combination.Length)]);
                        Rand = captcha.ToString();
                        
                        string fileLoc1 = @f_Path + "/XML_Sub/" + Rand + "_Reg.xml";
                        string fileLoc2 = @f_Path + "/XML_Sub/" + Rand + "_INV.xml";
                        string fileLoc3 = @f_Path + "/XML_Res/" + Rand + "_Reg.xml";
                        string fileLoc4 = @f_Path + "/XML_Res/" + Rand + "_INV.xml";
                        string fileLoc5 = @f_Path + "/XML_Token/" + Rand + "_req.xml";
                        string fileLoc6 = @f_Path + "/XML_Token/" + Rand + "_res.xml";
                        var gAPI = areg.getAPI();
                        DataSet dsItems = new DataSet();
                        DataSet dsItemsInvoice = new DataSet();
                        //string sign = "<REGDATA><TIN>" + "104043151" + "</TIN><CERTKEY>" + gAPI.Cert_Key + "</CERTKEY></REGDATA>";
                        string sign = "<REGDATA><TIN>" + "104043151" + "</TIN><CERTKEY>" + gAPI.Cert_Key + "</CERTKEY></REGDATA>";
                        byte[] signature = Sign(sign, "CN=" + gAPI.Test_CN);
                        string base64String = Convert.ToBase64String(signature);
                        string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                        xml = xml + "<EFDMS>";
                        xml = xml + sign;
                        xml = xml + "<EFDMSSIGNATURE>" + base64String + "</EFDMSSIGNATURE>";
                        xml = xml + "</EFDMS>";
                        FileStream fs1 = null;
                        if (!System.IO.File.Exists(fileLoc1))
                        {
                            using (fs1 = System.IO.File.Create(fileLoc1))
                            {

                            }
                            System.IO.File.WriteAllText(fileLoc1, xml);
                            fs1.Close();
                        }
                        XmlDocument doc = new XmlDocument();
                        HttpWebRequest request = null;
                        try
                        {
                            request = (HttpWebRequest)WebRequest.Create(gAPI.RegIP_Test);
                            request.Method = "POST";
                            request.ContentType = "application/xml";
                            request.Accept = "application/xml";
                            request.Headers["ContentType"] = "Application/xml";
                            request.Headers["Cert-Serial"] = Convert.ToBase64String(Encoding.Default.GetBytes(gAPI.Cert_Serial));
                            request.Headers["Client"] = "webapi";
                            byte[] signbytes;


                            signbytes = System.Text.Encoding.ASCII.GetBytes(xml);
                            Stream requestStream = request.GetRequestStream();
                            requestStream.Write(signbytes, 0, signbytes.Length);
                            requestStream.Close();
                            HttpWebResponse response;
                            response = (HttpWebResponse)request.GetResponse();
                            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
                            string backstr = sr.ReadToEnd();
                            doc.LoadXml(backstr);
                            sr.Close();
                            FileStream fs2 = null;
                            if (!System.IO.File.Exists(fileLoc3))
                            {
                                using (fs2 = System.IO.File.Create(fileLoc3))
                                {
                                    //File.WriteAllText(fileLoc, rdata);
                                }
                                System.IO.File.WriteAllText(fileLoc3, backstr);
                                fs2.Close();
                            }
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                //Stream responseStream = response.GetResponseStream();
                                //string responseStr = new StreamReader(responseStream).ReadToEnd();
                                //dsItems = DtXml(responseStr);
                                dsItems = DtXml(backstr);
                                var resultreg = AddRegData(dsItems, 0, pfx, pwd); //Data saving to DB
                                //DataRow row = dsItems.Tables[1].Rows[0];

                            }


                        }
                        catch (Exception ex)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                        }
                    }
                }*/

                #endregion

                c.UpdateCompanysta(c);
                cd.AddAccount(cd);
                return Request.CreateResponse(new {response = apa.compsno, message ="Success"});

            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
            //return returnNull;
        }


    }
}
