using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
using Newtonsoft.Json.Linq;
using BL.BIZINVOICING.BusinessEntities.Common;
using BL.BIZINVOICING.BusinessEntities.Masters;
using BL.BIZINVOICING.BusinessEntities.ConstantFile;

namespace BIZINVOICING.Controllers
{
    public class ZReportController : Controller
    {
        CompanyBankMaster com = new CompanyBankMaster();
        TRARegistration tra = new TRARegistration();
        APIReg areg = new APIReg();
        INVOICE inv = new INVOICE();
        string f_Path = System.Web.Configuration.WebConfigurationManager.AppSettings["FileP"].ToString();
        private readonly dynamic returnNull = null;
        // GET: ZReport
        public ActionResult ZReport()
        {
            return View();
        }
        public ActionResult GetRegi()
        {
            try
            {
                var result = com.GetCompany();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return null;
        }
        [HttpPost]
        public ActionResult GenReport(long csno)
        {

            try
            {
                string dtot = "0.00";
                string gtot = "0.00";
                string namount = "0.00";
                string vamount = "0.00";
                string a1 = string.Empty;
                string a2 = "0.00";
                string a3 = "0.00";
                string b1 = string.Empty;
                string b2 = "0.00";
                string b3 = "0.00";
                string c1 = string.Empty;
                string c2 = "0.00";
                string c3 = "0.00";
                string d1 = string.Empty;
                string d2 = "0.00";
                string d3 = "0.00";
                string e1 = string.Empty;
                string e2 = "0.00";
                string e3 = "0.00";

                var dTotal = inv.GetTotal();
                var dgTotal = inv.GetGTotal();
                int daiC = 0;

                if(dTotal != null)
                {
                    dtot = dTotal.Sum(a => a.Item_Total_Amount).ToString();
                    namount = dTotal.Sum(a => a.Total_Without_Vt).ToString();
                    vamount = dTotal.Sum(a => a.Vat_Amount).ToString();
                    daiC = dTotal.Count;
                }
                else
                {
                    dtot = "0.00";
                    namount = "0.00";
                    vamount = "0.00";
                    daiC = 0;
                }
                if(dgTotal != null)
                {
                    gtot = dgTotal.Sum(a => a.Item_Total_Amount).ToString();
                    
                }
                else
                {
                    gtot = "0.00";
                }
                var vType = inv.GetVATTotal("A");
                if(vType != null)
                {
                    a1 = "A-" + vType[0].Vat_Percentage;
                    a2 = vType.Sum(a => a.Item_Without_vat).ToString();
                    a3 = vType.Sum(a => a.Vat_Amount).ToString();
                }
                else
                {
                    a1 = "A-18.00";
                }
                vType = inv.GetVATTotal("B");
                if (vType != null)
                {
                    b1 = "B-" + vType[0].Vat_Percentage;
                    b2 = vType.Sum(a => a.Item_Without_vat).ToString();
                    b3 = vType.Sum(a => a.Vat_Amount).ToString();
                }
                else
                {
                    b1 = "B-0.00";
                }
                vType = inv.GetVATTotal("C");
                if (vType != null)
                {
                    c1 = "C-" + vType[0].Vat_Percentage;
                    c2 = vType.Sum(a => a.Item_Without_vat).ToString();
                    c3 = vType.Sum(a => a.Vat_Amount).ToString();
                }
                else
                {
                    c1 = "C-0.00";
                }
                vType = inv.GetVATTotal("D");
                if (vType != null)
                {
                    d1 = "D-" + vType[0].Vat_Percentage;
                    d2 = vType.Sum(a => a.Item_Without_vat).ToString();
                    d3 = vType.Sum(a => a.Vat_Amount).ToString();
                }
                else
                {
                    d1 = "D-0.00";
                }
                vType = inv.GetVATTotal("E");
                if (vType != null)
                {
                    e1 = "E-" + vType[0].Vat_Percentage;
                    e2 = vType.Sum(a => a.Item_Without_vat).ToString();
                    e3 = vType.Sum(a => a.Vat_Amount).ToString();
                }
                else
                {
                    e1 = "E-0.00";
                }
                var gTin = com.EditCompany(csno);
                var gAPI = areg.getAPI();
                string tin = string.Empty;
                tin = gTin.TinNo;
                var regD = tra.GetData(Convert.ToInt32(tin));
                string Rand = "12345678";
                Random random = new Random();
                string combination = "1234567890";
                StringBuilder captcha = new StringBuilder();
                string pDate = regD.posted_date.ToString("yyyy-MM-dd");
                for (int i = 0; i < 6; i++)
                    captcha.Append(combination[random.Next(combination.Length)]);
                Rand = captcha.ToString();
                string fileLoc1 = @f_Path + "/XML_Sub/" + Rand + "_Report.xml";
                string fileLoc2 = @f_Path + "/XML_Res/" + Rand + "_Report.xml";
                string xml1 = "<ZREPORT><DATE>" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "</DATE><TIME>" + DateTime.Now.ToString("HH:mm:ss") + "</TIME>";
                xml1 = xml1 + "<HEADER>";
                xml1 = xml1 + "<LINE>" + regD.company_name + "</LINE>";
                xml1 = xml1 + "<LINE>" + regD.street + "</LINE>";
                xml1 = xml1 + "<LINE>" + regD.address + "</LINE>";
                xml1 = xml1 + "<LINE>" + regD.country + "</LINE>";
                xml1 = xml1 + "</HEADER>";
                xml1 = xml1 + "<VRN>" + regD.vrn + "</VRN>";
                xml1 = xml1 + "<TIN>" + regD.tin_no + "</TIN>";
                xml1 = xml1 + "<TAXOFFICE>" + regD.taxoffice + "</TAXOFFICE>";
                xml1 = xml1 + "<REGID>" + regD.regid + "</REGID>";
                xml1 = xml1 + "<ZNUMBER>" + DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + "</ZNUMBER>";
                xml1 = xml1 + "<EFDSERIAL>" + regD.serial + "</EFDSERIAL>";
                xml1 = xml1 + "<REGISTRATIONDATE>" + pDate + "</REGISTRATIONDATE>";
                xml1 = xml1 + "<USER>" + regD.uin + "</USER>";
                xml1 = xml1 + "<SIMIMSI>" + "WEBAPI" + "</SIMIMSI>";

                xml1 = xml1 + "<TOTALS>";
                xml1 = xml1 + "<DAILYTOTALAMOUNT>" + dtot + "</DAILYTOTALAMOUNT>";
                xml1 = xml1 + "<GROSS>" + gtot + "</GROSS>";
                xml1 = xml1 + "<CORRECTIONS>" + "0.00" + "</CORRECTIONS>";
                xml1 = xml1 + "<DISCOUNTS>" + "0.00" + "</DISCOUNTS>";
                xml1 = xml1 + "<SURCHARGES>" + "0.00" + "</SURCHARGES>";
                xml1 = xml1 + "<TICKETSVOID>" + "0" + "</TICKETSVOID>";
                xml1 = xml1 + "<TICKETSVOIDTOTAL>" + "0.00" + "</TICKETSVOIDTOTAL>";
                xml1 = xml1 + "<TICKETSFISCAL>" + daiC + "</TICKETSFISCAL>";
                xml1 = xml1 + "<TICKETSNONFISCAL>" + "0" + "</TICKETSNONFISCAL>";
                xml1 = xml1 + "</TOTALS>";

                xml1 = xml1 + "<VATTOTALS>";
                xml1 = xml1 + "<VATRATE>" + a1 + "</VATRATE>";
                xml1 = xml1 + "<NETTAMOUNT>" + a2 + "</NETTAMOUNT>";
                xml1 = xml1 + "<TAXAMOUNT>" + a3 + "</TAXAMOUNT>";
                xml1 = xml1 + "<VATRATE>" + b1 + "</VATRATE>";
                xml1 = xml1 + "<NETTAMOUNT>" + b2 + "</NETTAMOUNT>";
                xml1 = xml1 + "<TAXAMOUNT>" + b3 + "</TAXAMOUNT>";
                xml1 = xml1 + "<VATRATE>" + c1 + "</VATRATE>";
                xml1 = xml1 + "<NETTAMOUNT>" + c2 + "</NETTAMOUNT>";
                xml1 = xml1 + "<TAXAMOUNT>" + c3 + "</TAXAMOUNT>";
                xml1 = xml1 + "<VATRATE>" + d1 + "</VATRATE>";
                xml1 = xml1 + "<NETTAMOUNT>" + d2 + "</NETTAMOUNT>";
                xml1 = xml1 + "<TAXAMOUNT>" + d3 + "</TAXAMOUNT>";
                xml1 = xml1 + "<VATRATE>" + e1 + "</VATRATE>";
                xml1 = xml1 + "<NETTAMOUNT>" + e2 + "</NETTAMOUNT>";
                xml1 = xml1 + "<TAXAMOUNT>" + e3 + "</TAXAMOUNT>";
                xml1 = xml1 + "</VATTOTALS>";

                xml1 = xml1 + "<PAYMENTS>";
                xml1 = xml1 + "<PMTTYPE>" + "CASH" + "</PMTTYPE>";
                xml1 = xml1 + "<PMTAMOUNT>" + "0.00" + "</PMTAMOUNT>";
                xml1 = xml1 + "<PMTTYPE>" + "CHEQUE" + "</PMTTYPE>";
                xml1 = xml1 + "<PMTAMOUNT>" + "0.00" + "</PMTAMOUNT>";
                xml1 = xml1 + "<PMTTYPE>" + "CCARD" + "</PMTTYPE>";
                xml1 = xml1 + "<PMTAMOUNT>" + "0.00" + "</PMTAMOUNT>";
                xml1 = xml1 + "<PMTTYPE>" + "EMONEY" + "</PMTTYPE>";
                xml1 = xml1 + "<PMTAMOUNT>" + "0.00" + "</PMTAMOUNT>";
                xml1 = xml1 + "<PMTTYPE>" + "INVOICE" + "</PMTTYPE>";
                xml1 = xml1 + "<PMTAMOUNT>" + dtot + "</PMTAMOUNT>";
                xml1 = xml1 + "</PAYMENTS>";

                xml1 = xml1 + "<CHANGES>";
                xml1 = xml1 + "<VATCHANGENUM>" + "0" + "</VATCHANGENUM>";
                xml1 = xml1 + "<HEADCHANGENUM>" + "0" + "</HEADCHANGENUM>";
                xml1 = xml1 + "</CHANGES>";
                xml1 = xml1 + "<ERRORS></ERRORS>";
                xml1 = xml1 + "<FWVERSION>3.0</FWVERSION>";
                xml1 = xml1 + "<FWCHECKSUM>WEBAPI</FWCHECKSUM>";


                xml1 = xml1 + "</ZREPORT>";
                byte[] signature = Sign(xml1, "CN=" + gAPI.Test_CN);
                string sign = Convert.ToBase64String(signature);
                //string sign = inv.generateSignature(xml1);

                string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                xml = xml + "<EFDMS>";
                xml = xml + xml1;
                xml = xml + "<EFDMSSIGNATURE>" + sign + "</EFDMSSIGNATURE>";
                xml = xml + "</EFDMS>";
                var postData = new List<KeyValuePair<string, string>>();
                FileStream fs1 = null;
                if (!System.IO.File.Exists(fileLoc1))
                {
                    using (fs1 = System.IO.File.Create(fileLoc1))
                    {
                        //File.WriteAllText(fileLoc, rdata);
                    }
                    System.IO.File.WriteAllText(fileLoc1, xml);
                    fs1.Close();
                }

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(gAPI.Res1);
                XmlDocument doc = new XmlDocument();
                //postData.Add(new KeyValuePair<string, string>("XML", xmlRequest.ToString()));
                //HttpContent content = new FormUrlEncodedContent(postData);

                // indicate what we are posting in the request
                request.Method = "POST";
                request.ContentType = "application/xml";
                request.Accept = "application/xml";
                request.Headers["ContentType"] = "Application/xml";
                request.Headers["Routing-Key"] = "vfdzreport";
                request.Headers["Cert-Serial"] = Convert.ToBase64String(Encoding.Default.GetBytes(gAPI.Cert_Serial));
                request.Headers["Authorization"] = "bearer " + gToken(csno);

                byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(xml);
                request.ContentLength = requestBytes.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Close();


                HttpWebResponse res1 = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(res1.GetResponseStream(), System.Text.Encoding.Default);
                string backstr = sr.ReadToEnd();
                doc.LoadXml(backstr);
                sr.Close();
                res1.Close();
                FileStream fs2 = null;
                if (!System.IO.File.Exists(fileLoc2))
                {
                    using (fs2 = System.IO.File.Create(fileLoc2))
                    {
                        //File.WriteAllText(fileLoc, rdata);
                    }
                    System.IO.File.WriteAllText(fileLoc2, backstr);
                    fs2.Close();
                }
                return Json("Success", JsonRequestBehavior.AllowGet);
                /*if (name == true)
                {
                    var retur = new { name, dname = dd.District_Name };
                    return Json(retur, JsonRequestBehavior.AllowGet);
                }*/

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }
        public byte[] Sign(string text, string certSubject)
        {

            string f_Path = System.Web.Configuration.WebConfigurationManager.AppSettings["FilePath"].ToString();
            X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            X509Certificate2 certificate2 = new X509Certificate2(f_Path, "b!zL0gic21");
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
        public string gToken(long csno)
        {
            var gTin = com.EditCompany(csno);
            var gAPI = areg.getAPI();
            string tin = string.Empty;
            tin = gTin.TinNo;
            var regD = tra.GetData(Convert.ToInt32(tin));
            string Rand = "12345678";
            Random random = new Random();
            string combination = "1234567890";
            StringBuilder captcha = new StringBuilder();
            for (int i = 0; i < 6; i++)
                captcha.Append(combination[random.Next(combination.Length)]);
            Rand = captcha.ToString();
            string f_Path = System.Web.Configuration.WebConfigurationManager.AppSettings["FileP"].ToString();
            string fileLoc = @f_Path + "/XML_Token/" + Rand + "_res.txt";
            //string fileLoc1 = @f_Path + "/XML_Token/" + Rand + "_res.xml";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string url = gAPI.TokenIP_Test;
            //string url = "https://virtual.tra.go.tz/efdmsRctApi/vfdtoken?Username=" + WebUtility.UrlEncode("babaafhg8490lkdn") + "&Password=" + WebUtility.UrlEncode("a6t4B2Q@76a7B2w{") + "&grant_type=password";
            WebRequest request = WebRequest.Create(url);
            string data = "Username=" + WebUtility.UrlEncode(regD.username) + "&Password=" + WebUtility.UrlEncode(regD.password) + "&grant_type=password";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["ContentType"] = "application/x-www-form-urlencoded";
            request.Method = "POST";
            byte[] postBytes = Encoding.ASCII.GetBytes(data);
            request.ContentLength = postBytes.Length;
            Stream ReqStrm;
            ReqStrm = request.GetRequestStream();
            ReqStrm.Write(postBytes, 0, postBytes.Length);
            ReqStrm.Close();
            //request.ContentLength = 0;
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = Encoding.GetEncoding("utf-8");
            StreamReader sr = new StreamReader(stream, encode);
            string backstr = sr.ReadToEnd();
            sr.Close();
            //res.Close();
            FileStream fs2 = null;
            if (!System.IO.File.Exists(fileLoc))
            {
                using (fs2 = System.IO.File.Create(fileLoc))
                {
                    //File.WriteAllText(fileLoc, rdata);
                }
                System.IO.File.WriteAllText(fileLoc, backstr);
                fs2.Close();
            }
            JObject jObject = JObject.Parse(backstr);
            string token = string.Empty;
            return token = jObject["access_token"].ToString();
            //Response.Write(token);
        }
    }
}