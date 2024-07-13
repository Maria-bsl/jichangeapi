using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Xml;
using System.Data;
namespace BIZINVOICING.Controllers
{
    public class CompanyInboxController : AdminBaseController
    {
        // GET: CompanyInbox
        string f_Path = System.Web.Configuration.WebConfigurationManager.AppSettings["FileP"].ToString();
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
        public ActionResult CompanyInbox()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }
        [HttpPost]
        public ActionResult GetCompanys()
        {
            try
            {
                var result = c.GetCompany1();
                if (Session["desig"].ToString() == "Administrator")
                {
                    result = c.GetCompany1();
                }
                else
                {
                    result = c.GetCompany1_Branch(long.Parse(Session["BRAID"].ToString()));
                }
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var d = 0;
                    return Json(d, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }

        [HttpPost]
        public ActionResult AddCompanyBank(long compsno, string pfx, String pwd, long ssno)
        {
            try
            {
                C_Deposit cd = new C_Deposit();
                c.CompSno = compsno;
                c.Postedby = Session["UserID"].ToString();
                c.Status = "Approved";
                c.Sus_Ac_SNo = ssno;

                cd.Deposit_Acc_No = pfx;
                cd.Comp_Mas_Sno = compsno;
                //cd.Reason = 
                cd.AuditBy = Session["UserID"].ToString();
                
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
                            ex.ToString();
                        }
                    }
                }*/

                c.UpdateCompanysta(c);
                cd.AddAccount(cd);
                return Json(compsno, JsonRequestBehavior.AllowGet);
               
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }

        #region  dt xml
        public DataSet DtXml(string xmlData)
        {
            StringReader theReader = new StringReader(xmlData);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);

            return theDataSet;
        }

        #endregion
        #region Save Reg Data

        public long AddRegData(DataSet ds, long cno, string pfx, string pwd)
        {
            long result1 = 0;
            long ssno = cno;
            try
            {

                TRARegistration objtra = new TRARegistration();
                DataRow row = ds.Tables[1].Rows[0];
                objtra.ack_code = Convert.ToInt32(row["ACKCODE"]);
                objtra.ack_message = row["ACKMSG"].ToString();
                objtra.regid = row["REGID"].ToString();
                objtra.serial = row["SERIAL"].ToString();
                objtra.uin = row["UIN"].ToString();
                objtra.tin_no = Convert.ToInt32(row["TIN"]);
                objtra.vrn = row["VRN"].ToString();
                objtra.mobile_no = row["MOBILE"].ToString();
                objtra.street = row["STREET"].ToString();
                objtra.city = row["STREET"].ToString();
                objtra.address = row["ADDRESS"].ToString();
                objtra.country = row["COUNTRY"].ToString();
                objtra.company_name = row["NAME"].ToString();
                objtra.receiptcode = row["RECEIPTCODE"].ToString();
                objtra.region = row["REGION"].ToString();
                objtra.gc = Convert.ToInt32(row["GC"]);
                objtra.taxoffice = row["TAXOFFICE"].ToString();
                objtra.username = row["USERNAME"].ToString();
                objtra.password = row["PASSWORD"].ToString();
                objtra.tokenpath = row["TOKENPATH"].ToString();
                objtra.posted_by = "1";
                objtra.posted_date = DateTime.Now;
                objtra.Cert_Serial = pfx;
                objtra.Pfx_Pass = pwd;
                objtra.Certi_Path = @f_Path + "/Certificates";
                objtra.Inv_Path = @f_Path + "/Invoices";
                objtra.Log_Path = @f_Path ;
                if (ssno == 0)
                {
                    ssno = tra.AddTRARegistration(objtra);//add data
                    foreach (DataRow detailsRow in ds.Tables[2].Rows)
                    {
                        TRARegistration trdetails = new TRARegistration();
                        trdetails.reg_ack_sno = ssno;
                        trdetails.tax_code = detailsRow["CODEA"].ToString();
                        trdetails.tax_percentage = Convert.ToInt32(detailsRow["CODEA"]);
                        tra.AddregistrationackDetails(trdetails);
                    }
                }
                if (ssno > 0)
                {
                    objtra.reg_ack_sno = ssno;
                    tra.UpdateRegistration(objtra);//update data
                    tra.DeleteRegDet(objtra);
                    foreach (DataRow detailsRow in ds.Tables[2].Rows)
                    {
                        TRARegistration trdetails = new TRARegistration();
                        trdetails.reg_ack_sno = ssno;
                        trdetails.tax_code = detailsRow["CODEA"].ToString();
                        trdetails.tax_percentage = Convert.ToInt32(detailsRow["CODEA"]);
                        tra.AddregistrationackDetails(trdetails);
                    }
                }
                result1 = ssno;
                return result1;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return result1;
        }

        #endregion
        #region Sign

        public byte[] Sign(string text, string certSubject)
        {

            string f_Path = System.Web.Configuration.WebConfigurationManager.AppSettings["FilePath"].ToString();
            X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            X509Certificate2 certificate2 = new X509Certificate2(f_Path, "Kimara20");
            //X509Certificate2 certificate2 = new X509Certificate2(f_Path, "bizl0g*");
            my.Open(OpenFlags.ReadWrite);
            my.Add(certificate2);

            // Find the certificate we'll use to sign

            RSACryptoServiceProvider csp = null;

            foreach (X509Certificate2 cert in my.Certificates)
            {

                if (cert.Subject.Contains(certSubject))
                {


                    csp = (RSACryptoServiceProvider)cert.PrivateKey;

                }

            }

            if (csp == null)
            {

                throw new Exception("No valid cert was found");

            }



            SHA1Managed sha1 = new SHA1Managed();

            UnicodeEncoding encoding = new UnicodeEncoding();

            byte[] data = System.Text.Encoding.ASCII.GetBytes(text);

            byte[] hash = sha1.ComputeHash(data);



            return csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));

        }

        #endregion

    }
}