using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BIZINVOICEWEBAPI.Controllers
{
    public class InvoiceReqController : ApiController
    {

        [HttpGet]
        public async Task<bool> PostInvoice()

        {


            var dmmy = new StringContent("<EFDMS><REGDATA><TIN> 216273612</TIN><CERTKEY>10TZ162172 </CERTKEY>  </REGDATA> <EFDMSSIGNATURE>0yESKN20S6EWE5PJSOkCfM314rppI2WGb6WyhRnpofUpadBr2v80qEYwFS5gQ4N8LQ+xLa1JthOH6iUZipYI2affX3tahVfTxET6XsVkeW1LAk2qPQxfz7/DFfH+XCWR9YDwXDhM5/H5+VkmQCSuD+gJTT1b/neoIgwqvU0nQCZ41c+h+ze1yubfz7G9oErav/sXWn7AIgC+GLqiQFIJ8+t1peerECCiighj/6GDjDixoqd885Q1HtuC5e4Xn62ZCbEFrSXowODD4k7snhnYv/o0JPwbqmxEdrLlCOLulDhiaQRrV53X85P1KfwPixlrfM5whd1d38gDkktOZHqk4g==</EFDMSSIGNATURE>  </EFDMS>",
                                Encoding.UTF8, "Application/xml");

            string sign = "<REGDATA><TIN>" + "104043151" + "</TIN><CERTKEY>" + "10TZ100576" + "</CERTKEY></REGDATA>"; 
            byte[] signature = Sign(sign, "CN=TRAEFDTEST01");


            


            string base64String = Convert.ToBase64String(signature);

            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            xml = xml + "<EFDMS>";
            xml = xml + sign;
            xml = xml + "<EFDMSSIGNATURE>" + base64String + "</EFDMSSIGNATURE>";
            xml = xml + "</EFDMS>";


            HttpWebRequest request = null;
            try
            {

               

                // var _authkey = configurationManger.APPSetting["userkey"];
                request = (HttpWebRequest)WebRequest.Create("https://virtual.tra.go.tz/efdmsRctApi/api/vfdRegReq");
                //request.Method = "Post";
                //request.ContentType = "Application/xml;encoding='utf-8' ";
                //request.Headers.Add("Cert-Serial", "747d90b923284aa2410e1086a9a6f947");
                //request.Headers.Add("Client", "webapi");
                var s = Convert.ToBase64String(Encoding.Default.GetBytes("747d90b923284aa2410e1086a9a6f947"));
                request.Method = "POST";
                request.ContentType = "application/xml";
                request.Accept = "application/xml";
                request.Headers["ContentType"] = "Application/xml";
                request.Headers["Cert-Serial"] = Convert.ToBase64String(Encoding.Default.GetBytes("747d90b923284aa2410e1086a9a6f947"));
                request.Headers["Client"] = "webapi";
                byte[] bytes;


                bytes = System.Text.Encoding.ASCII.GetBytes(xml);
                Stream requestStream = request.GetRequestStream();
                 requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                   // return responseStr;
                }
            }
            catch (Exception ex)
            {
            }

            #region not used
            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Username", "babaafhg8490lkdn");
            //client.DefaultRequestHeaders.Add("Password", "a6t4B2Q@76a7B2w{");
            //client.DefaultRequestHeaders.Add("grant_type", "password");
            //client.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");

            //var content = new StringContent(xml, Encoding.UTF8, "application/x-www-form-urlencoded");


            //var result = client.PostAsync("https://virtual.tra.go.tz/efdmsRctApi/vfdtoken", content).Result;


            //if (result.IsSuccessStatusCode)
            //{
            //    var rslt = result.Content.ReadAsStringAsync().Result;
            //}

            #endregion


            #region For TOken
            var client = new HttpClient();
            var dict = new Dictionary<string, string>();
            //dict.Add("Content-Type", "application/x-www-form-urlencoded");
            dict.Add("Username", "babaafhg8490lkdn");
            dict.Add("Password", "a6t4B2Q@76a7B2w{");
            dict.Add("grant_type", "password");

            string JSON_CONTENT = JsonConvert.SerializeObject(dict);
            var encodedContent = new FormUrlEncodedContent(dict);
            var responsess = await client.PostAsync("https://virtual.tra.go.tz/efdmsRctApi/vfdtoken", encodedContent).ConfigureAwait(false);
            if (responsess.StatusCode == HttpStatusCode.OK)
            {
                string resultJsons = responsess.Content.ReadAsStringAsync().Result;
                // var responseContent = await response.Content.ReadAsStringAsync ().ConfigureAwait (false);
            }
            #endregion


            try
            {
                HttpWebRequest requestnew = null;

                string InvoiceDatas = "<RCT><DATE>"+ "2021-04-16" + "</DATE><TIME>"+ "13:53:20" + "</TIME><TIN>" + "104043151" + "</TIN><REGID>"+ "TZ0100552941" + "</REGID><EFDSERIAL>"+ "10TZ100576" + "</EFDSERIAL><CUSTIDTYPE>"+"1"+ "</CUSTIDTYPE><CUSTID>"+ "104043151" + "</CUSTID><CUSTNAME>"+"Test"+ "</CUSTNAME><MOBILENUM>"+ "255123456789" + "</MOBILENUM><RCTNUM>"+ "2" + "</RCTNUM><DC>"+"2"+ "</DC><GC>"+ "2" + "</GC><ZNUM>"+ "20210419" + "</ZNUM><RCTVNUM>"+ "F6024B2" + "</RCTVNUM><ITEMS><ITEM><ID>" + "1"+ "</ID><DESC>"+"test"+ "</DESC><QTY>"+"1"+ "</QTY><TAXCODE>"+"1"+ "</TAXCODE><AMT>"+ "20" + "</AMT></ITEM></ITEMS><TOTALS><TOTALTAXEXCL>"+ "16.95" + "</TOTALTAXEXCL><TOTALTAXINCL>"+"20"+ "</TOTALTAXINCL><DISCOUNT>"+ "0.00" + "</DISCOUNT></TOTALS><PAYMENTS><PMTTYPE>" + "INVOICE" + "</PMTTYPE><PMTAMOUNT>"+"20"+ "</PMTAMOUNT></PAYMENTS><VATOTALS><VATRATE>"+"A"+ "</VATRATE><NETTAMOUNT>"+ "16.95" + "</NETTAMOUNT><TAXAMOUNT>"+ "3.05"+ "</TAXAMOUNT></VATOTALS></RCT>";

                string InvoiceData = "<RCT><DATE>" + "2021-04-16" + "</DATE><TIME>" + "13:53:20" + "</TIME><TIN>" + "104043151" + "</TIN><REGID>" + "TZ0100552941" + "</REGID><EFDSERIAL>" + "10TZ100576" + "</EFDSERIAL><CUSTIDTYPE>" + "1" + "</CUSTIDTYPE><CUSTID>" + "104043151" + "</CUSTID><CUSTNAME>" + "SATISH KUMAR" + "</CUSTNAME><MOBILENUM>" + "255123456789" + "</MOBILENUM><RCTNUM>" + "2" + "</RCTNUM><DC>" + "2" + "</DC><GC>" + "2" + "</GC><ZNUM>" + "20210419" + "</ZNUM><RCTVNUM>" + "F6024B2" + "</RCTVNUM><ITEMS><ITEM><ID>" + "1" + "</ID><DESC>" + "test" + "</DESC><QTY>" + "1" + "</QTY><TAXCODE>" + "1" + "</TAXCODE><AMT>" + "200.00" + "</AMT></ITEM></ITEMS><TOTALS><TOTALTAXEXCL>" + "16.95" + "</TOTALTAXEXCL><TOTALTAXINCL>" + "20.09" + "</TOTALTAXINCL><DISCOUNT>" + "0.00" + "</DISCOUNT></TOTALS><PAYMENTS><PMTTYPE>" + "INVOICE" + "</PMTTYPE><PMTAMOUNT>" + "20.00" + "</PMTAMOUNT></PAYMENTS><VATOTALS><VATRATE>" + "A" + "</VATRATE><NETTAMOUNT>" + "16.95" + "</NETTAMOUNT><TAXAMOUNT>" + "3.05" + "</TAXAMOUNT></VATOTALS></RCT>";

                string xml1 = "<RCT><DATE>" + DateTime.Now.ToString("yyyy-MM-dd") + "</DATE><TIME>" + DateTime.Now.ToString("HH:mm:ss") + "</TIME>";
                xml1 = xml1 + "<TIN>" + "104043151" + "</TIN>";
                xml1 = xml1 + "<REGID>" + "TZ0100552941" + "</REGID>";
                xml1 = xml1 + "<EFDSERIAL>" + "10TZ100576" + "</EFDSERIAL>";
                xml1 = xml1 + "<CUSTIDTYPE>" + "1" + "</CUSTIDTYPE><CUSTID>104043151</CUSTID>";
                xml1 = xml1 + "<CUSTNAME>" + "SATISH KUMAR" + "</CUSTNAME>";
                xml1 = xml1 + "<MOBILENUM>" + "255123456789" + "</MOBILENUM>";
                xml1 = xml1 + "<RCTNUM>" + "2" + "</RCTNUM>";
                xml1 = xml1 + "<DC>" + "2" + "</DC>";
                xml1 = xml1 + "<GC>" + "2" + "</GC>";
                xml1 = xml1 + "<ZNUM>" + DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + "</ZNUM>";
                xml1 = xml1 + "<RCTVNUM>" + "F6024B" + "2" + "</RCTVNUM>";
                xml1 = xml1 + "<ITEMS><ITEM><ID>" + "1" + "</ID>";
                xml1 = xml1 + "<DESC>" + "invoice generation" + "</DESC>";
                xml1 = xml1 + "<QTY>" + "1" + "</QTY>";
                xml1 = xml1 + "<TAXCODE>" + "1" + "</TAXCODE>";
                xml1 = xml1 + "<AMT>" + "200.00" + "</AMT>";
                xml1 = xml1 + "</ITEM></ITEMS>";
                xml1 = xml1 + "<TOTALS><TOTALTAXEXCL>" + "169.49" + "</TOTALTAXEXCL>";
                xml1 = xml1 + "<TOTALTAXINCL>" + "200.00" + "</TOTALTAXINCL>";
                xml1 = xml1 + "<DISCOUNT>" + "0.00" + "</DISCOUNT>";
                xml1 = xml1 + "</TOTALS>";
                xml1 = xml1 + "<PAYMENTS><PMTTYPE>" + "INVOICE" + "</PMTTYPE>";
                xml1 = xml1 + "<PMTAMOUNT>" + "200.00" + "</PMTAMOUNT>";
                xml1 = xml1 + "</PAYMENTS>";
                xml1 = xml1 + "<VATTOTALS><VATRATE>" + "A" + "</VATRATE>";
                xml1 = xml1 + "<NETTAMOUNT>" + "169.49" + "</NETTAMOUNT>";
                xml1 = xml1 + "<TAXAMOUNT>" + "30.51" + "</TAXAMOUNT>";
                xml1 = xml1 + "</VATTOTALS>";
                xml1 = xml1 + "</RCT>";

                byte[] signatures = Sign(xml1, "CN=TRAEFDTEST01");

                string base64StringInvoice = Convert.ToBase64String(signatures);

                string xmlInvoice = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                xmlInvoice = xmlInvoice + "<EFDMS>";
                xmlInvoice = xmlInvoice + xml1;
                xmlInvoice = xmlInvoice + "<EFDMSSIGNATURE>" + base64StringInvoice + "</EFDMSSIGNATURE>";
                xmlInvoice = xmlInvoice + "</EFDMS>";



                // var _authkey = configurationManger.APPSetting["userkey"];
                requestnew = (HttpWebRequest)WebRequest.Create("https://virtual.tra.go.tz/efdmsRctApi/api/efdmsRctInfo");

                //var s = Convert.ToBase64String(Encoding.Default.GetBytes("747d90b923284aa2410e1086a9a6f947"));
                requestnew.Method = "POST";
                requestnew.ContentType = "application/xml";
                requestnew.Accept = "application/xml";
                requestnew.Headers["ContentType"] = "Application/xml";
                requestnew.Headers["Routing-Key"] = "vfdrct";

                requestnew.Headers["Cert-Serial"] = Convert.ToBase64String(Encoding.Default.GetBytes("747d90b923284aa2410e1086a9a6f947"));
                requestnew.Headers["Authorization"] = "bearer sMqYMEXFPatRWaY7oMii8BbMxkjDv0-QyJanRDL7V_mMJ2YHrIYbRhnWCOnuGs4ltIny9KuJDONXx9nDVwFqS_UaA8Ujr1wThUBXUiSmQ8X4Dy9kPawafc84J5p_8TtkhPcKBlKdwbX2IKhpUFw4QQ4isqO0czqzc4TfcQfn7QxO-SY7jwnVz5OPW0YkQQ9-Cz5pMhy3ipY5eMryGRe_5wmB52OeXrenM64FGOcL4xeTmMSuJmLDkwa6I7ABmbS1ZX7O5A24t3PvZ4DGTJ5zvA28ugSUT_uMZZFP30Ex_lquKy44WD5XBE6bgr9_Uh1xO87LPACrRlbW1cpyt67voEY54HFopK1qJ0so_KS4hjRCaH0TV7Cz7_1DPGnRq4qSCGfM5b1dOIiuYyNcWIyjAFpDVcIsqo22juH0EmskJEc";
                byte[] bytesinvoice;


                bytesinvoice = System.Text.Encoding.ASCII.GetBytes(xmlInvoice);
                Stream requestStreamnew = requestnew.GetRequestStream();
                requestStreamnew.Write(bytesinvoice, 0, bytesinvoice.Length);
                requestStreamnew.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)requestnew.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    // return responseStr;
                }
            }
            catch (Exception ex)
            {
            }

 
            return true;
        }

 



        #region Sign

        public byte[] Sign(string text, string certSubject)
        {

            string f_Path = System.Web.Configuration.WebConfigurationManager.AppSettings["FilePath"].ToString();
            X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            X509Certificate2 certificate2 = new X509Certificate2(f_Path  , "b!zL0gic21");
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
