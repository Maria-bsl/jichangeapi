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
using System.Data;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class InvoiceDashController : LangcoController
    {
        // GET: InvoiceDash
        INVOICE inv = new INVOICE();
        Company cp = new Company();
        Customers cu = new Customers();
        CURRENCY cy = new CURRENCY();
        VatPercentage vatpercetage = new VatPercentage();
        TRARegistration tra = new TRARegistration();
        APIReg areg = new APIReg();

        string f_Path = System.Web.Configuration.WebConfigurationManager.AppSettings["FileP"].ToString();
        public string ack = string.Empty;
        public string ackm = string.Empty;
        
        public ActionResult InvoiceDash()
        {
            if (Session["sessComp"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            #region TokenGen
            /*string Rand = "12345678";
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
            //Get token value
            var gAPI = areg.getAPI();
            var tokenvalue = string.Empty;
            var objtra = tra.GetDataU();
            if (objtra == null )
            {
                DataSet dsItems = new DataSet();
                DataSet dsItemsInvoice = new DataSet();
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
                    //var s = Convert.ToBase64String(Encoding.Default.GetBytes("747d90b923284aa2410e1086a9a6f947"));
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
                        var resultreg = AddRegData(dsItems, 0); //Data saving to DB


                    }


                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            try
            {
                var token = GetToken(objtra.username.ToString(), objtra.password.ToString());
                token.Wait();
                string abc = "";
                var resulttoken = token.Result;
                //ack = "8";
                //HttpContext.Response.Write(ack+": "+ackm);
                //HttpContext.Response.End();
                var serializer = new JavaScriptSerializer();
                dynamic obj = serializer.Deserialize(resulttoken, typeof(object));

                //var reslt= obj.GetType().GetProperty("value");



                foreach (var item in obj)
                {

                    tokenvalue = item.Value;
                    break;
                }
            }
            catch
            {

            }
            if (ack == "18")
            {
                string sign = "<REGDATA><TIN>" + objtra.tin_no + "</TIN><CERTKEY>" + gAPI.Cert_Key + "</CERTKEY></REGDATA>";
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
                DataSet dsItems = new DataSet();
                DataSet dsItemsInvoice = new DataSet();
                XmlDocument doc = new XmlDocument();
                HttpWebRequest request = null;
                try
                {
                    request = (HttpWebRequest)WebRequest.Create(gAPI.RegIP_Test);
                    //var s = Convert.ToBase64String(Encoding.Default.GetBytes("747d90b923284aa2410e1086a9a6f947"));
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
                        var resultreg = AddRegData(dsItems, objtra.reg_ack_sno); //Data saving to DB
                        try
                        {
                            var token = GetToken(objtra.username.ToString(), objtra.password.ToString());
                            token.Wait();
                            var resulttoken = token.Result;
                            //ack = "8";
                            //HttpContext.Response.Write(ack+": "+ackm);
                            //HttpContext.Response.End();
                            var serializer = new JavaScriptSerializer();
                            dynamic obj = serializer.Deserialize(resulttoken, typeof(object));

                            //var reslt= obj.GetType().GetProperty("value");



                            foreach (var item in obj)
                            {

                                tokenvalue = item.Value;
                                break;
                            }
                        }
                        catch
                        {

                        }

                    }
                    


                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }*/
            var gVRN = tra.GetDataU();
            if (gVRN != null)
            {
                ViewBag.VRN = gVRN.vrn;
            }
            #endregion
            return View();
        }

        #region For TOken
        public async Task<string> GetToken(string username, string pwd)
        {
            var gAPI = areg.getAPI();
            string Rand = "12345678";
            Random random = new Random();
            string combination = "1234567890";
            StringBuilder captcha = new StringBuilder();
            for (int i = 0; i < 6; i++)
                captcha.Append(combination[random.Next(combination.Length)]);
            Rand = captcha.ToString();
            string fileLoc1 = @f_Path + "/XML_Token/" + Rand + "_req.txt";
            string fileLoc2 = @f_Path + "/XML_Token/" + Rand + "_res.txt";
            string resultJsons = string.Empty;
            var client = new HttpClient();
            var dict = new Dictionary<string, string>();
            dict.Add("Username", username);
            dict.Add("Password", pwd);
            dict.Add("grant_type", "password");

            string JSON_CONTENT = JsonConvert.SerializeObject(dict);
            FileStream fs1 = null;
            if (!System.IO.File.Exists(fileLoc1))
            {
                using (fs1 = System.IO.File.Create(fileLoc1))
                {
                    //File.WriteAllText(fileLoc, rdata);
                }
                System.IO.File.WriteAllText(fileLoc1, JSON_CONTENT);
                fs1.Close();
            }
            var encodedContent = new FormUrlEncodedContent(dict);
            var responsess = await client.PostAsync(gAPI.TokenIP_Test, encodedContent).ConfigureAwait(false);
            StreamReader sr = new StreamReader(await responsess.Content.ReadAsStreamAsync(), System.Text.Encoding.Default);
            ack = responsess.Headers.GetValues("ACKCODE").FirstOrDefault();
            ackm = responsess.Headers.GetValues("ACKMSG").FirstOrDefault();
            //ViewBag.MSg = ackm;
            string backstr = sr.ReadToEnd();
            backstr = backstr + Environment.NewLine + ack + Environment.NewLine + ackm;
            sr.Close();
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
            if (responsess.StatusCode == HttpStatusCode.OK)
            {
                resultJsons = responsess.Content.ReadAsStringAsync().Result;
                var responseContent = await responsess.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            return resultJsons;


        }
        #endregion
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

        public long AddRegData(DataSet ds, long cno)
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

        #endregion
        public ActionResult Generatedinvoices()
        {
            if (Session["sessComp"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }

        #region Get Invoice Details
        public ActionResult GetchDetails()
        {
            try
            {
                var result = inv.GetINVOICEMas(long.Parse(Session["CompID"].ToString())).Where(x => x.approval_status != "2");
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        #endregion

        #region Get Signed Invoices
        public ActionResult GetSignedDetails()
        {
            try
            {
                var result = inv.GetINVOICEMas(long.Parse(Session["CompID"].ToString())).Where(x => x.approval_status == "2");
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        #endregion


        #region Get invoice by Id
        public ActionResult GetInvoiceDetailsbyid(int invid)
        {
            try
            {
                var result = inv.GetINVOICEMas(long.Parse(Session["CompID"].ToString())).Where(x => x.Inv_Mas_Sno == invid).FirstOrDefault();
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }

        public ActionResult GetInvoiceInvoicedetails(int invid)
        {
            try
            {
                var result = inv.GetInvoiceDetails(invid);
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }

        #endregion

        #region     Get Drodown Master Values
        public ActionResult Getcompany()
        {
            try
            {
                var result = cp.GetCompanyMas();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }

        public ActionResult GetVatPer()
        {
            try
            {
                var result = vatpercetage.GetVatPercentage();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }

        public ActionResult GetCustomers()
        {
            try
            {
                var result = cu.GetCustomers();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        public ActionResult GetCurrency()
        {
            try
            {
                var result = cy.GetCURRENCY();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        #endregion

        public ActionResult GetInvNo(string invno, int invmasno)
        {
            try
            {
                var result = inv.GetINVOICEMas(long.Parse(Session["CompID"].ToString())).Where(x => x.Invoice_No == invno && x.Inv_Mas_Sno != invmasno).FirstOrDefault();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }

        #region Save Update Invoice
        [HttpPost]
        public ActionResult AddInvoice(string invno, string auname, string date, long chus, long comno, string ccode, string ctype, string cino,
            string twvat, string vtamou, string total, string Inv_remark, int lastrow, List<INVOICE> details, long sno, string warrenty, string goods_status, string delivery_status)
        {
            try
            {
                DateTime dates;
                if (sno == 0)
                    dates = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                else
                    dates = DateTime.ParseExact(date, "MM/dd/yyyy", null);


                inv.Invoice_No = invno;
                inv.Invoice_Date = Convert.ToDateTime(dates);
                inv.Com_Mas_Sno = comno;
                inv.Chus_Mas_No = chus;
                inv.Currency_Code = ccode;
                inv.Total_Without_Vt = Decimal.Parse(twvat);
                inv.Vat_Amount = Decimal.Parse(vtamou);
                inv.Total = Decimal.Parse(total);
                inv.Inv_Remarks = Inv_remark;
                inv.warrenty = warrenty;
                inv.goods_status = goods_status;
                inv.delivery_status = delivery_status;
                inv.Customer_ID_Type = ctype;
                inv.Customer_ID_No = cino;
                inv.AuditBy = Session["UserID"].ToString();
                long ssno = 0;

                if (sno == 0)
                {

                    ssno = inv.Addinvoi(inv);
                    for (int i = 0; i < details.Count; i++)
                    {
                        if (details[i].Inv_Mas_Sno == 0)
                        {
                            inv.Inv_Mas_Sno = ssno;
                            inv.Item_Description = details[i].Item_Description;
                            inv.Item_Qty = details[i].Item_Qty;
                            inv.Item_Unit_Price = details[i].Item_Unit_Price;
                            inv.Item_Total_Amount = details[i].Item_Total_Amount;
                            inv.Vat_Percentage = details[i].Vat_Percentage;
                            inv.Vat_Amount = details[i].Vat_Amount;
                            inv.Item_Without_vat = details[i].Item_Without_vat;
                            inv.Remarks = details[i].Remarks;
                            inv.vat_category = details[i].vat_category;
                            inv.Vat_Type = details[i].Vat_Type;
                            inv.AddInvoiceDetails(inv);
                        }
                    }

                }

                else if (sno > 0)
                {

                    inv.Inv_Mas_Sno = sno;
                    inv.UpdateInvoiMas(inv);
                    inv.DeleteInvoicedet(inv);
                    for (int i = 0; i < details.Count; i++)
                    {
                        if (details[i].Inv_Mas_Sno == 0)
                        {
                            inv.Inv_Mas_Sno = sno;
                            inv.Item_Description = details[i].Item_Description;
                            inv.Item_Qty = details[i].Item_Qty;
                            inv.Item_Unit_Price = details[i].Item_Unit_Price;
                            inv.Item_Total_Amount = details[i].Item_Total_Amount;
                            inv.Vat_Percentage = details[i].Vat_Percentage;
                            inv.Vat_Amount = details[i].Vat_Amount;
                            inv.Item_Without_vat = details[i].Item_Without_vat;
                            inv.Remarks = details[i].Remarks;
                            inv.vat_category = details[i].vat_category;
                            inv.AddInvoiceDetails(inv);
                        }
                    }
                    ssno = sno;
                }
                var result1 = ssno;
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return null;
        }
        #endregion






    }
}