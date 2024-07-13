using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL.BIZINVOICING.BusinessEntities.Common;
using BL.BIZINVOICING.BusinessEntities.Masters;
using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text;
/*using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;*/
using System.Security.Cryptography;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EPS.API.Models
{
    public class PaymentDetailsRepository : IPaymentDetailsRepository
    {
        /// <summary>
        /// Add payment details
        /// </summary>
        /// <param name="serviceUserDetails"></param>
        /// <returns></returns>
        INVOICE inv = new INVOICE();
        Company comp = new Company();
        CustomerMaster cm = new CustomerMaster();
        public long PostAddPayment(PaymentDetails servicePaymentDetails)
        {
            #region "Variables"
            Payment userDetails = null;
            //Permit userPermit = null;
            long sno = 0;
            long pno = 0;
            long pID=0;
            string f_Path = System.Web.Configuration.WebConfigurationManager.AppSettings["FilePath"].ToString();
            #endregion
            string loc5 = CreateMD5(servicePaymentDetails.paymentReference);
            string locsha = "3F59C0D0253EDF46BCF0D1A0195087A7" + loc5;
            string tosha = Hash(locsha);
            string strConnString = ConfigurationManager.ConnectionStrings["SchCon"].ToString();
            try
            {
                userDetails = new Payment();
                //FeeD fd = new FeeD();
                //StuReg sr = new StuReg();
                Payment pmt = new Payment();
                //API_Val apval = new API_Val();
                string abc = string.Empty;
                if (System.Web.HttpContext.Current != null)
                {
                    abc = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    abc = "0.0.0.0";
                }
                /*if ((servicePaymentDetails.token != ConfigurationManager.AppSettings["Token"]))
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Invalid Token";
                    userDetails.AddErrorLogs(userDetails);
                    return 5;
                }*/
                /*if (apval.ValidateToken(servicePaymentDetails.token))
                {

                }
                else
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + "Post Invalid Token " + servicePaymentDetails.token;
                    userDetails.AddErrorLogs(userDetails);
                    return 5;
                }*/
                /*if (apval.ValidateIT(abc, servicePaymentDetails.token))
                {

                }
                else
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + "Post Unknown Source " + servicePaymentDetails.token + " "+abc;
                    userDetails.AddErrorLogs(userDetails);
                    return 4;
                }*/
                if (!inv.ValidateControl(servicePaymentDetails.paymentReference))
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Payment reference number does not exists";
                    userDetails.AddErrorLogs(userDetails);
                    return 1;
                }
                
                
                //var vp = np.GePDetails(pID);

                if (pmt.ValidateRefno(servicePaymentDetails.transactionRef))
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Receipt no already exists";
                    userDetails.AddErrorLogs(userDetails);
                    return 2;
                }
                
                
                DateTime cDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                double usamount = 0;
                double tFee = 0;
                long check = 0;
                
                
                
             
                S_SMTP smtp = new S_SMTP();
                /*SText smst = new SText();
                S_SMS smss = new S_SMS();
                Allocation acation = new Allocation();*/

                var chkFee = inv.GetControl(servicePaymentDetails.paymentReference);
                //var srFee = sr.GetAdmission_Det_Service(chkFee.Admission_No);
                //var sFee = sr.GetAdmission_App_Conf(chkFee.Admission_No, chkFee.Facilit_Reg_Sno);
                bool flag = true;
                
                try
                {
                    sno = userDetails.GetLastInsertedId();
                    pno = userDetails.GetLastInsertedId();
                }
                catch
                {
                    sno = 34561200;
                    pno = 1;
                    flag = false;
                }
                if (flag == true)
                {
                    pno = pno + 1;
                }
                pno = 34561200 + pno;
                userDetails.Payment_SNo = pno.ToString();
                userDetails.Payment_Date = servicePaymentDetails.transactionDate;
                userDetails.Payment_Time = servicePaymentDetails.transactionDate;
                userDetails.Fee_Data_Sno = servicePaymentDetails.paymentReference;
                userDetails.Trans_Channel = servicePaymentDetails.transactionChannel;
                userDetails.Payment_Type = servicePaymentDetails.Payment_Type;
                userDetails.Payment_Desc = servicePaymentDetails.paymentDesc;
                userDetails.Payer_Id = servicePaymentDetails.payerID;
                userDetails.Institution_ID = servicePaymentDetails.institutionID;
                userDetails.Control_No = chkFee.Control_No;
                userDetails.Comp_Mas_Sno = chkFee.CompanySno;
                userDetails.Company_Name = chkFee.CompName;
                userDetails.Cust_Mas_Sno = chkFee.Cust_Sno;
                userDetails.Currency_Code = servicePaymentDetails.currency;
                userDetails.Token = servicePaymentDetails.token;
                userDetails.Chksum = servicePaymentDetails.checksum;
                userDetails.Item_Total_Amount = servicePaymentDetails.amount;
                userDetails.Payment_Trans_No = servicePaymentDetails.transactionRef;
                userDetails.Receipt_No = servicePaymentDetails.transactionRef;
                userDetails.Message = "Success from API";
                userDetails.Response_Code = "0";
                //userDetails.Merchant = merchant;
                userDetails.Card = "API";
                userDetails.Status = "Passed";
                userDetails.Posted_By = "0";
                userDetails.Payer_Name = servicePaymentDetails.payerName;
                userDetails.Amount_Type = servicePaymentDetails.amountType;
                
                userDetails.Amount30 = servicePaymentDetails.amount * 30 / 100;
                userDetails.Amount70 = servicePaymentDetails.amount * 70 / 100;
                userDetails.AddPayment(userDetails);

                /*string pamount = servicePaymentDetails.amount.ToString();
                string camount = pamount + servicePaymentDetails.transactionRef;
                userDetails.Col1 = Utilites.GetEncryptedData(servicePaymentDetails.transactionRef);
                userDetails.Col2 = Utilites.GetEncryptedData(pamount);
                userDetails.Col3 = Utilites.GetEncryptedData(camount);
                userDetails.AddOther(userDetails);*/

                userDetails.Error_Text = servicePaymentDetails.Fee_Data_Sno + " Payment Details Successfully Inserted";
                userDetails.AddErrorLogs(userDetails);

                try
                {
                    /*var gAmount = pmt.GetPayment_Paid(servicePaymentDetails.paymentReference);
                    long famount = 0;
                    if (gAmount != null)
                    {
                        famount = gAmount.Sum(a => a.Amount);
                    }
                    long tamount = chkFee.Requested_Amount;*/
                    
                    long fee = servicePaymentDetails.amount;
                    //long bamount = tamount - famount;
                    //var AccNo = acation.GetType(chkFee.Fee_Sno, chkFee.Facilit_Reg_Sno);
                    SqlConnection cn = new SqlConnection(strConnString);
                    cn.Open();
                    string txtM = string.Empty;
                    //txtM = "STAKABADHI YA MALIPO" + Environment.NewLine + "Mzazi/Mlezi wa " + sFee.Student_Full_Name + Environment.NewLine + "TZS " + String.Format("{0:n0}", fee) + " imelipwa kwa ajili ya " + chkFee.Fee_Type + " kupitia ";
                    //txtM = txtM + servicePaymentDetails.transactionChannel + Environment.NewLine + "Namba ya muamala: " + servicePaymentDetails.paymentReference + " Namba ya stakabadhi: " + servicePaymentDetails.transactionRef + Environment.NewLine + "Kiasi unachodaiwa ni TZS " + String.Format("{0:n0}", bamount) + Environment.NewLine + "Ahsante, " + chkFee.Facility_Name;
                    /*DataSet ds2 = new DataSet();
                    SqlDataAdapter da2 = new SqlDataAdapter(new SqlCommand("select * from tbMessages_sms", cn));
                    SqlCommandBuilder cb2 = new SqlCommandBuilder(da2);
                    da2.FillSchema(ds2, SchemaType.Source);
                    DataTable dt2 = ds2.Tables["Table"];
                    dt2.TableName = "tbMessages_sms";
                    DataRow rs2 = ds2.Tables["tbMessages_sms"].NewRow();

                    rs2["msg_date"] = servicePaymentDetails.transactionDate;
                    rs2["PhoneNumber"] = "255" + sFee.Parent_Mobile_No;
                    rs2["TransactionNo"] = servicePaymentDetails.paymentReference;
                    if (AccNo != null)
                    {
                        rs2["AccountNumber"] = AccNo.Fee_Acc_No;
                    }
                    else
                    {
                        rs2["AccountNumber"] = "0";
                    }
                    rs2["Message"] = txtM;
                    rs2["trials"] = 0;
                    rs2["deliveryCode"] = DBNull.Value;
                    rs2["sent"] = 0;
                    rs2["delivered"] = 0;
                    rs2["txn_type"] = "SCHOOL";
                    rs2["Msg_Date_Time"] = servicePaymentDetails.transactionDate;
                    ds2.Tables["tbMessages_sms"].Rows.Add(rs2);
                    da2.Update(ds2, "tbMessages_sms");*/
                    cn.Close();

                }
                catch (Exception ex)
                {
                    pmt.Error_Text = ex.ToString();
                    pmt.AddErrorLogs(pmt);
                }
                
                try
                {
                    //EText emt = new EText();
                    string to = chkFee.CustEmail;
                    //string attach = f_Path + "/Receipts/" + vp.Permit_Application_No + ".pdf";
                    string from = "";
                    SmtpClient client = new SmtpClient();
                    int port = 0;
                    MailMessage message1 = new MailMessage();
                    string subject = "Transaction No : " + chkFee.Control_No;//"Your ePermit payment receipt";
                    string btext = "Dear " + chkFee.Cust_Name + ", <br><br>";
                    //btext = btext + "Please find attachement of your payment receipt <br><br> Regards,<br>Schools<br />";
                    btext = btext + "<br><br> Regards,<br>Schools<br />";
                    //string body = btext;
                    string body = "";
                    /*var emtid = emt.GetEmail("5");
                    if (emtid != null)
                    {
                        body = emtid.E_Text;
                    }*/
                    var esmtp = smtp.getSMTPText();
                    if (esmtp != null)
                    {
                        if (string.IsNullOrEmpty(esmtp.SMTP_UName))
                        {
                            port = Int32.Parse(esmtp.SMTP_Port);
                            from = esmtp.From_Address;
                            message1 = new MailMessage(from, to, subject, body);
                            client = new SmtpClient(esmtp.SMTP_Address, port);
                            message1.IsBodyHtml = true;
                        }
                        else
                        {
                            port = Int32.Parse(esmtp.SMTP_Port);
                            from = esmtp.From_Address;
                            message1 = new MailMessage(from, to, subject, body);
                            //message1.Attachments.Add(new Attachment(attach));
                            message1.IsBodyHtml = true;
                            client = new SmtpClient(esmtp.SMTP_Address, port);
                            client.UseDefaultCredentials = false;
                            client.EnableSsl = Convert.ToBoolean(esmtp.SSL_Enable);
                            client.Credentials = new NetworkCredential(esmtp.SMTP_UName, Utilites.DecodeFrom64(esmtp.SMTP_Password));
                        }

                    }
                    
                    
                    client.Send(message1);
                    
                }
                catch//(Exception ch)
                {
                    //Response.Write(ch.ToString());
                    //Response.End();
                }
                /*try
                {
                    // Send SMS Code 
                    var empm = ud1.GetMobile("DSE");
                    var smsid = smst.GetSMST("15");
                    var smsv = smss.getSMSText();
                    if (smsv != null)
                    {
                        if (!String.IsNullOrEmpty(empm.Mobile_Number))
                        {
                            if (smst.ValidateSMSText("15"))
                            {
                                string username = smsv.UserName;//"bizlogicsolutions";
                                string password = Utility.DecodeFrom64(smsv.PWD);//"Bizlogic123";
                                string senderb = smsv.From_Address;//"Biz-Logic";
                                string mess = "Application No : " + vp.Permit_Application_No + " " + smsid.E_Text;
                                //mess = mess + AntiXssEncoder.HtmlEncode(txtComments.Text, true) + Environment.NewLine + "Regards," + Environment.NewLine + "DSE" + Environment.NewLine + "MWTC (Works)";
                                string gsm = empm.Mobile_Number; //"255713316177";
                                if (!gsm.Contains("255"))
                                {
                                    gsm = "255" + gsm;
                                }
                                string mobileid = smsv.Mobile_Service;
                                string response = SMSGateway.PushSMS(password, username, senderb, mobileid, mess, gsm);
                                
                            }
                        }
                    }
                    if (smsv != null)
                    {
                        var smsid1 = smst.GetSMST("18");
                        if (smst.ValidateSMSText("18"))
                        {
                            string username = smsv.UserName;//"bizlogicsolutions";
                            string password = Utility.DecodeFrom64(smsv.PWD);//"Bizlogic123";
                            string senderb = smsv.From_Address;//"Biz-Logic";
                            string mess = "Application No : " + vp.Permit_Application_No + " " + smsid.E_Text;
                            //mess = mess + AntiXssEncoder.HtmlEncode(txtComments.Text, true) + Environment.NewLine + "Regards," + Environment.NewLine + "DSE" + Environment.NewLine + "MWTC (Works)";
                            string gsm = details.Contact_Per_Home;
                            string isd = details.ISD_Code;
                            if (!gsm.Contains("255"))
                            {
                                if (!string.IsNullOrEmpty(isd))
                                {
                                    gsm = isd + gsm;
                                }
                                else
                                {
                                    gsm = "255" + gsm;
                                }
                            }
                            string mobileid = smsv.Mobile_Service;
                            string response = SMSGateway.PushSMS(password, username, senderb, mobileid, mess, gsm);
                           
                        }
                    }

                }
                catch//(Exception ch)
                {
                    //Response.Write(ch.ToString());
                    //Response.End();
                }*/
                return pno; 
            }
            catch (Exception ex)
            {
                //ApplicationError.MAPPErrorHandling(ex, "PostAddUser", "device");
                //HttpContext.Current.Response.Write(ex.ToString());
                //HttpContext.Current.Response.End();
                userDetails.Error_Text = servicePaymentDetails.paymentReference + ex.ToString();
                userDetails.AddErrorLogs(userDetails);
                return -1;
                //return -1;
            }
        }
       
        public long PostGetDetails(PaymentDetails servicePaymentDetails)
        {
            #region "Variables"
            Payment userDetails = null;
            //Permit userPermit = null;
            long sno = 0;
            long pno = 0;
            long pID = 0;
            string f_Path = System.Web.Configuration.WebConfigurationManager.AppSettings["FilePath"].ToString();
            #endregion
            string loc5 = CreateMD5(servicePaymentDetails.paymentReference);
            string locsha = "3F59C0D0253EDF46BCF0D1A0195087A7" + loc5;
            string tosha = Hash(locsha);
            try
            {
                userDetails = new Payment();
                //FeeD fd = new FeeD();
                //StuReg sr = new StuReg();
                Payment pmt = new Payment();
                //API_Val apval = new API_Val();
                decimal oamount = 0;
                decimal tamount = 0;
                decimal ramount = 0;
                string abc = string.Empty;
                if (System.Web.HttpContext.Current != null)
                {
                    abc = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    abc = "0.0.0.0";
                }
                //System.Web.HttpContext context = System.Web.HttpContext.Current;
                //string ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];//HTTP_X_FORWARDED_FOR
                //HttpContext.Current.Response.Write(ipAddress);
                //HttpContext.Current.Response.End();
                /*if ((servicePaymentDetails.token != ConfigurationManager.AppSettings["Token"]))
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Invalid Token";
                    userDetails.AddErrorLogs(userDetails);
                    return 5;
                }*/
                /*if (apval.ValidateToken(servicePaymentDetails.token))
                {

                }
                else
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Invalid Token " + servicePaymentDetails.token;
                    userDetails.AddErrorLogs(userDetails);
                    return 5;
                }
                if (apval.ValidateIT(abc, servicePaymentDetails.token))
                {

                }
                else
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Unknown Source " + servicePaymentDetails.token + " " + abc;
                    userDetails.AddErrorLogs(userDetails);
                    return 4;
                }*/
                if (!inv.ValidateControl(servicePaymentDetails.paymentReference))
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Payment reference number does not exists";
                    userDetails.AddErrorLogs(userDetails);
                    return 1;
                }
                var gAmount = pmt.GetPayment_Paid(servicePaymentDetails.paymentReference);
                var chkFee = inv.GetControl(servicePaymentDetails.paymentReference.ToString());
                if (gAmount != null)
                {
                    tamount = gAmount.Sum(z => z.Amount);
                    if (chkFee != null)
                    {
                        oamount = chkFee.Item_Total_Amount;
                    }
                    ramount = oamount - tamount;
                    if (ramount < 1)
                    {
                        return 3;
                    }
                    
                }
                //var vp = np.GePDetails(pID);

               /* if (string.IsNullOrEmpty(servicePaymentDetails.payerName))
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Payername is null or empty";
                    userDetails.AddErrorLogs(userDetails);
                    return 10;
                }
                if (string.IsNullOrEmpty(servicePaymentDetails.institutionID))
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Institution ID is null or empt";
                    userDetails.AddErrorLogs(userDetails);
                    return 2;
                }
                if (servicePaymentDetails.transactionDate.Year == 0001)
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Payment Date is null or empty";
                    userDetails.AddErrorLogs(userDetails);
                    return 3;
                }
                //if (string.IsNullOrEmpty(servicePaymentDetails.Amount.ToString()))
                //{
                if (servicePaymentDetails.amount < 1)
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Payment Amount is null or empty";
                    userDetails.AddErrorLogs(userDetails);
                    return 7;
                }
                //}
                if (string.IsNullOrEmpty(servicePaymentDetails.transactionRef.ToString()))
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Receipt No is null or empty";
                    userDetails.AddErrorLogs(userDetails);
                    return 4;
                }
                if (string.IsNullOrEmpty(servicePaymentDetails.transactionRef))
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Transaction Ref No is null or empty";
                    userDetails.AddErrorLogs(userDetails);
                    return 4;
                }

                if (string.IsNullOrEmpty(servicePaymentDetails.transactionChannel))
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Transaction Channel is null or empty";
                    userDetails.AddErrorLogs(userDetails);
                    return 6;
                }*/
                /*if (string.IsNullOrEmpty(servicePaymentDetails.checksum) || tosha != servicePaymentDetails.checksum)
                {
                    userDetails.Error_Text = servicePaymentDetails.paymentReference + " Checksum is not valid or empty";
                    userDetails.AddErrorLogs(userDetails);
                    return 9;
                }*/
                
                DateTime cDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                double usamount = 0;
                double tFee = 0;
                long check = 0;




                S_SMTP smtp = new S_SMTP();
                /*SText smst = new SText();
                S_SMS smss = new S_SMS();

                var chkFee1 = fd.GetFee_Service(servicePaymentDetails.paymentReference);
                var srFee = sr.GetAdmission_Det_Service(chkFee1.Admission_No);*/
                bool flag = true;




                
               
                return pno;
            }
            catch (Exception ex)
            {
                //ApplicationError.MAPPErrorHandling(ex, "PostAddUser", "device");
                //HttpContext.Current.Response.Write(ex.ToString());
                //HttpContext.Current.Response.End();
                userDetails.Error_Text = servicePaymentDetails.paymentReference + ex.ToString();
                userDetails.AddErrorLogs(userDetails);
                return -1;
                //return -1;
            }
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        static string Hash(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }
        public long GetIsAuthenticatedInvoice(long invNo, string token)
        {
            #region "Variables"
            //Payment py = new Payment();
            #endregion
            //FeeD fd = new FeeD();
            Payment py = new Payment();
            try
            {
                if (!inv.ValidateControl(invNo.ToString()))
                {
                    py.Error_Text = invNo + " Get Method Payment reference number does not exists";
                    py.AddErrorLogs(py);
                    return 0;
                }
                else if (token != ConfigurationManager.AppSettings["Token"])
                {
                    return 2;
                }
                else
                {
                    return 1;
                }

                
            }
            catch (Exception ex)
            {
                py.Error_Text = invNo + " GET method Error Invoice no " + ex.ToString();
                py.AddErrorLogs(py);   
                return -1;
            }
        }
        public InvoiceDetails GetInvoiceDetails(PaymentDetails servicePaymentDetails)
        {
            #region "Variables"
            InvoiceDetails serviceInvoice = null;
            //List<InvoiceDetails> lstInvoiceDetails = null;
            #endregion

            try
            {
                //FeeD fd = new FeeD();
                Payment pay = new Payment();
                //Allocation all = new Allocation();
                //StuReg sr = new StuReg();
                var chkFee = inv.GetControl(servicePaymentDetails.paymentReference.ToString());
                string aType = "";
                decimal oamount = 0;
                decimal tamount = 0;
                decimal ramount = 0;
                if (chkFee != null)
                {

                    //var sFee = sr.GetAdmission_App_Conf(chkFee.Admission_No, chkFee.Facilit_Reg_Sno);
                    //var gType = all.GetType(chkFee.Fee_Sno, chkFee.Facilit_Reg_Sno);
                    //var gAmount = pay.GetPayment_Paid(chkFee.Fee_Data_Sno);
                    var gAmount = pay.GetPayment_Paid(servicePaymentDetails.paymentReference);
                    serviceInvoice = new InvoiceDetails();
                    
                    //lstInvoiceDetails = new List<InvoiceDetails>();
                    serviceInvoice.payerName = chkFee.Cust_Name;
                    //serviceInvoice.payerName = "John S";
                    //HttpContext.Current.Response.Write("check");
                    //HttpContext.Current.Response.End();
                    if (gAmount != null)
                    {
                        
                        tamount = gAmount.Sum(z => z.Amount);
                        oamount = chkFee.Item_Total_Amount;
                        ramount = oamount - tamount;
                        if (ramount < 1)
                        {
                            serviceInvoice.amount = 0;
                        }
                        else
                        {
                            serviceInvoice.amount = ramount;
                        }
                    }
                    else
                    {
                        serviceInvoice.amount = chkFee.Item_Total_Amount;
                    }
                    //aType = gType.Amount_Type;
                    /*if (string.IsNullOrEmpty(aType))
                    {
                        serviceInvoice.amountType = "FIXED";
                    }
                    else
                    {*/
                    serviceInvoice.amountType = chkFee.Payment_Type; ;
                    //}
                    
                    serviceInvoice.currency = "TZS";
                    serviceInvoice.paymentReference = chkFee.Control_No.ToString();
                    /*if (gType.FType_ID != null)
                    {
                        //serviceInvoice.paymentType = gType.Fee_Acc_Bank_Code;
                        serviceInvoice.paymentType = gType.FType_ID.ToString();
                    }
                    else
                    {
                        serviceInvoice.paymentType = "";
                    }*/
                    serviceInvoice.paymentType = "";
                    serviceInvoice.paymentDesc = "";// chkFee.Fee_Type;
                    serviceInvoice.payerID = "";// chkFee.Admission_No;
                    
                    //lstInvoiceDetails.Add(serviceInvoice);
                }
                return serviceInvoice;
            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write(ex.ToString());
                //HttpContext.Current.Response.End();
                return null;
            }
        }
    }
    public class PaymentReceipt
    {
        public long receipt { get; set; }
    }
}