using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InvoiceController : ApiController
    {
        #region Global Declrations
        // GET: Invoice
        INVOICE inv = new INVOICE();
        InvoiceC ic = new InvoiceC();
        Company cp = new Company();
        Customers cu = new Customers();
        CustomerMaster cum = new CustomerMaster();
        CURRENCY cy = new CURRENCY();
        VatPercentage vatpercetage = new VatPercentage();
        TRARegistration tra = new TRARegistration();
        APIReg areg = new APIReg();
        Payment pay = new Payment();
        EMAIL em = new EMAIL();
        S_SMTP ss = new S_SMTP();
        C_Deposit cd = new C_Deposit();

        string pwd, drt;
        //string strConnString = ConfigurationManager.ConnectionStrings["SchCon"].ToString();
        //string f_Path = System.Web.Configuration.WebConfigurationManager.AppSettings["FileP"].ToString();
        public string ack = string.Empty;
        public string ackm = string.Empty;


        #region Get Invoice Details
        [HttpPost]
        public HttpResponseMessage GetchDetails(SingletonComp s)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = inv.GetINVOICEMas(long.Parse(s.compid.ToString())).Where(x => x.approval_status != "2" && x.approval_status != "Cancel");
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = "Success" });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = "Failed" });
                    }

                }
                catch (Exception Ex)
                {
                    // Catch Log here for exception thrown
                    Ex.ToString();
                }
            }else 
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            return null;
      

        }
        [HttpPost]
        public HttpResponseMessage GetchDetails_A(SingletonComp s)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var result = inv.GetINVOICEMas().Where(x => x.approval_status == "2" && x.approval_status != "Cancel");
                    var result = inv.GetINVOICEMasE(long.Parse(s.compid.ToString()));
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = "Success" });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = "Failed" });
                    }

                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
   
                }else 
                    {
                        var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                        return Request.CreateResponse(new { response = 0, message = errorMessages
                    });
                    }

            return null;
        }

        [HttpPost]
        public HttpResponseMessage GetchDetails_P(SingletonComp s)
        {
            if (ModelState.IsValid) { 
            try
            {
                //var result = inv.GetINVOICEMas().Where(x => x.approval_status == "2" && x.approval_status != "Cancel");
                var result = inv.GetINVOICEMas_D(long.Parse(s.compid.ToString()));
                if (result != null)
                {
                    return Request.CreateResponse(new { response = result, message = "Success" });
                }
                else
                {
                    return Request.CreateResponse(new { response = 0, message = "Failed" });
                }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            return null;
        }

        [HttpPost]
        public HttpResponseMessage GetchDetails_Pen(SingletonComp s)
        {

            if (ModelState.IsValid) { 
            try
            {
                //var result = inv.GetINVOICEMas().Where(x => x.approval_status == "2" && x.approval_status != "Cancel");
                var result = inv.GetINVOICEMas_Pen(long.Parse(s.compid.ToString()));
                if (result != null)
                {
                    return Request.CreateResponse(new { response = result, message = "Success" });
                }
                else
                {
                    return Request.CreateResponse(new { response = 0, message = "Failed" });
                }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            return null;
        }

        [HttpPost]
        public HttpResponseMessage GetchDetails_Lat(SingletonComp s)
        {
            if (ModelState.IsValid) { 
            try
            {
                //var result = inv.GetINVOICEMas().Where(x => x.approval_status == "2" && x.approval_status != "Cancel");
                var result = inv.GetINVOICEMas_Lat(long.Parse(s.compid.ToString()));
                if (result != null)
                {
                    return Request.CreateResponse(new { response = result, message = "Success" });
                }
                else
                {
                    return Request.CreateResponse(new { response = 0, message = "Failed" });
                }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            return null;
        }
        #endregion

        #region Get Signed Invoices
        [HttpPost]
        public HttpResponseMessage GetSignedDetails(SingletonComp s)
        {
            if (ModelState.IsValid) { 
                try
                {
                    var result = inv.GetINVOICEMas(long.Parse(s.compid.ToString())).Where(x => x.approval_status == "2");
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = "Success" });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = "Failed" });
                    }

                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            return null;
        }

        [HttpPost]
        public HttpResponseMessage GetSignedInvoiceById(SingletonCompInvid s)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = inv.GetINVOICEMas2(long.Parse(s.compid.ToString()), s.invid);
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = "Success" });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = "Failed" });
                    }

                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            return null;
        }

        #endregion


        #region Get invoice by Id
        [HttpPost]
        public HttpResponseMessage GetInvoiceDetailsbyid(SingletonCompInvid s)
        {
            if (ModelState.IsValid) {    
                try
                {
                    //var result = inv.GetINVOICEMas(long.Parse(s.compid.ToString())).Where(x => x.Inv_Mas_Sno == s.invid).FirstOrDefault();
                    var result = inv.GetINVOICEMas1(long.Parse(s.compid.ToString()), s.invid);

                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = "Success" });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = "Failed" });
                    }

                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
            }else 
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages  });
            }

                return null;
        }


        [HttpPost]
        public HttpResponseMessage GetInvoiceInvoicedetails(SingletonInv i)
        {

            if (ModelState.IsValid) { 
                    try
                    {
                        var result = inv.GetInvoiceDetails(i.invid);
                        if (result != null)
                        {
                            return Request.CreateResponse(new { response = result, message = "Success" });
                        }
                        else
                        {
                            return Request.CreateResponse(new { response = 0, message = "Failed" });
                        }

                    }
                    catch (Exception Ex)
                    {
                        Ex.ToString();
                    }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            return null;
        }

        #endregion

        #region     Get Drodown Master Values

        [HttpPost]
        public HttpResponseMessage Getcompany()
        {
            try
            {
                var result = cp.GetCompanyMas();

                return Request.CreateResponse(new { response = result, message = "Success" });
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }

        [HttpPost]
        public HttpResponseMessage GetcompanyS(SingletonComp c)
        {
            if (ModelState.IsValid) {  
                try
                {
                    long cno = long.Parse(c.compid.ToString());
                    var result = cp.GetCompanyS(cno);
                    if (result != null)
                    {

                        return Request.CreateResponse(new { response = result, message = "Success" });
                    }
                    else
                        return Request.CreateResponse(new {response=0, message = "Failed" });

                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            return null;
        }
        [HttpPost]
        public HttpResponseMessage GetMAccount(SingletonComp c)
        {

            if (ModelState.IsValid) {
                try
                {
                    long cno = long.Parse(c.compid.ToString());
                    var result = cd.GetMAccount(cno);
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = "Success" });
                    }
                    else
                    {

                        return Request.CreateResponse(new { response = 0, message = "Failed" });
                    }
                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            return null;
        }

        [HttpPost]
        public HttpResponseMessage GetCustomersS(SingletonComp c)
        {
            if (ModelState.IsValid) { 
                try
                {
                    long cno = long.Parse(c.compid.ToString());
                    var result = cu.GetCustomersS(cno);
                    if (result != null)
                        return Request.CreateResponse(new { response = result, message = "Success" });

                    else
                        return Request.CreateResponse(new {response = 0, message = "Failed" });
                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            return null;
        }

        [HttpPost]
        public HttpResponseMessage GetVatPer()
        {
            try
            {
                var result = vatpercetage.GetVatPercentage();

                return Request.CreateResponse(new { response = result, message = "Success" });
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }


        [HttpPost]
        public HttpResponseMessage GetCustomers()
        {
            try
            {
                var result = cu.GetCustomers();

                return Request.CreateResponse(new { response = result, message = "Success" });
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        [HttpPost]
        public HttpResponseMessage GetCurrency()
        {
            try
            {
                var result = cy.GetCURRENCY();

                return Request.CreateResponse(new { response = result, message = "Success" });
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        #endregion


/*
        public HttpResponseMessage GetInvNo(SingletonInvMas c)
        {
            try
            {
                //var result = inv.GetINVOICEMas().Where(x=>x.Invoice_No== invno && x.Com_Mas_Sno == long.Parse(compid.ToString())).FirstOrDefault();
                if (inv.ValidateNo(c.invno, long.Parse(c.compid.ToString())))
                {
                    return Request.CreateResponse(new {response="EXIST", message = "Success" });
                   //return Json("EXIST", JsonRequestBehavior.AllowGet);
                }

                //return Request.CreateResponse(new { response = result, message = "Success"});
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }

        #region Save Update Invoice
        [HttpPost]
        public HttpResponseMessage AddInvoice(InvoiceForm v)
        {
            try
            {

                DateTime dates = DateTime.Now;
                DateTime dates1 = DateTime.Now;
                DateTime dates2 = DateTime.Now;
                //dates = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                dates = DateTime.Parse(v.date);
                if (!string.IsNullOrEmpty(edate))
                {
                    //dates1 = DateTime.ParseExact(edate, "dd/MM/yyyy", null);
                    dates1 = DateTime.Parse(edate);
                }
                //dates2 = DateTime.ParseExact(iedate, "dd/MM/yyyy", null);
                dates2 = DateTime.Parse(iedate);

                inv.Invoice_No = invno;
                inv.Invoice_Date = dates;
                if (!string.IsNullOrEmpty(edate))
                {
                    inv.Due_Date = Convert.ToDateTime(edate);
                }
                inv.Invoice_Expired_Date = Convert.ToDateTime(iedate);
                inv.Payment_Type = ptype;
                inv.Com_Mas_Sno = long.Parse(compid.ToString());
                inv.Chus_Mas_No = chus;
                inv.Currency_Code = ccode;
                inv.Total_Without_Vt = Decimal.Parse(twvat);
                inv.Vat_Amount = Decimal.Parse(vtamou);
                inv.Total = Decimal.Parse(total);
                inv.Inv_Remarks = Inv_remark;
                inv.warrenty = warrenty;
                inv.goods_status = goods_status;
                inv.delivery_status = delivery_status;
                //inv.Customer_ID_Type = string.Empty;
                //inv.Customer_ID_No = cino;
                inv.AuditBy = userid.ToString();
                long ssno = 0;

                if (sno == 0)
                {
                    if (inv.ValidateNo(invno, long.Parse(compid.ToString())))
                    {
                        return Json("EXIST", JsonRequestBehavior.AllowGet);
                    }
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
                else if (sno > 0 && goods_status == "Approve")
                {
                    string cno = string.Empty;
                    cno = sno.ToString().PadLeft(8, '0');

                    inv.Inv_Mas_Sno = sno;
                    inv.daily_count = 0;// (int)daiC;
                    inv.grand_count = 0;// (int)graC;
                    inv.Control_No = "T" + cno;
                    inv.UpdateInvoiMasForTRA1(inv);
                    inv.approval_status = "2";
                    inv.approval_date = System.DateTime.Now;
                    inv.UpdateInvoice(inv);
                    ssno = sno;
                }
                else if (sno > 0)
                {
                    var getI = inv.EditINVOICEMas(sno);
                    bool flag = true;
                    if (getI.Invoice_No == invno)
                    {
                        flag = false;
                    }
                    if (inv.ValidateNo(invno, long.Parse(compid.ToString())) && flag == true)
                    {
                        return Json("EXIST", JsonRequestBehavior.AllowGet);
                    }
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
                return Request.CreateResponse(new { response = result1, message = "Success" });
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return null;
        }
        [HttpPost]
        public HttpResponseMessage AddAmend(string invno, string auname, string date, string edate, string iedate, string ptype, long chus, long comno, string ccode, string ctype, string cino,
           string twvat, string vtamou, string total, string Inv_remark, int lastrow, List<INVOICE> details, long sno, string warrenty, string goods_status, string delivery_status, string reason)
        {
            try
            {
                DateTime dates = DateTime.Now;
                DateTime dates1 = DateTime.Now;
                DateTime dates2 = DateTime.Now;
                //string[] sdate = date.Split('/');

                //date = sdate[0] + "/" + sdate[1] + "/" + sdate[2];
                dates = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                if (!string.IsNullOrEmpty(edate))
                {
                    //string[] sdate1 = edate.Split('/');
                    //edate = sdate1[0] + "-" + sdate1[1] + "-" + sdate1[2];
                    dates1 = DateTime.ParseExact(edate, "dd/MM/yyyy", null);
                }
                //string[] sdate2 = iedate.Split('/');
                dates2 = DateTime.ParseExact(iedate, "dd/MM/yyyy", null);
                //iedate = sdate2[0] + "-" + sdate2[1] + "-" + sdate2[2];


                inv.Invoice_No = invno;
                inv.Invoice_Date = dates;
                if (!string.IsNullOrEmpty(edate))
                {
                    inv.Due_Date = Convert.ToDateTime(edate);
                }
                inv.Invoice_Expired_Date = Convert.ToDateTime(iedate);
                inv.Payment_Type = ptype;
                inv.Com_Mas_Sno = long.Parse(compid.ToString());
                inv.Chus_Mas_No = chus;
                inv.Currency_Code = ccode;
                inv.Total_Without_Vt = Decimal.Parse(twvat);
                inv.Vat_Amount = Decimal.Parse(vtamou);
                inv.Total = Decimal.Parse(total);
                inv.Inv_Remarks = Inv_remark;
                inv.warrenty = warrenty;
                inv.goods_status = goods_status;
                inv.delivery_status = delivery_status;
                //inv.Customer_ID_Type = string.Empty;
                //inv.Customer_ID_No = cino;
                inv.AuditBy = userid.ToString();
                long ssno = 0;

                if (sno > 0)
                {
                    var getI = inv.GetINVOICEpdf(sno);
                    if (getI != null)
                    {
                        if (Decimal.Parse(total) == getI.Item_Total_Amount)
                        {
                            return Request.CreateResponse(new { response = 0, message = "Failed" });
                        }
                        ic.Control_No = getI.Control_No;
                        ic.Com_Mas_Sno = getI.CompanySno;
                        ic.Cust_Mas_No = getI.Cust_Sno;
                        ic.Inv_Mas_Sno = sno;
                        ic.Invoice_Amount = getI.Item_Total_Amount;
                        ic.Payment_Type = ptype;
                        ic.Amment_Amount = Decimal.Parse(total);
                        if (!string.IsNullOrEmpty(edate))
                        {
                            ic.Due_Date = Convert.ToDateTime(edate);
                        }
                        ic.Invoice_Expired_Date = Convert.ToDateTime(iedate);
                        ic.Reason = reason;
                        ic.AuditBy = userid.ToString();
                        ic.AddAmmend(ic);
                    }



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
                    try
                    {
                        var getD = cum.get_Cust(getI.Cust_Sno);
                        //EText emt = new EText();
                        string to = getD.Email;
                        //string attach = f_Path + "/Receipts/" + vp.Permit_Application_No + ".pdf";
                        string from = "";
                        SmtpClient client = new SmtpClient();
                        int port = 0;
                        MailMessage message1 = new MailMessage();
                        string subject = "Control No : " + getI.Control_No + " Amended";//"Your ePermit payment receipt";
                        string btext = "Dear " + ", <br><br>";
                        //btext = btext + "Please find attachement of your payment receipt <br><br> Regards,<br>Schools<br />";
                        btext = btext + "<br><br> Regards,<br>Schools<br />";
                        //string body = btext;
                        string body = "Dear: " + getD.Cust_Name + "<br><br>";
                        body += "Your control no amended for following details " + "<br /><br />";
                        body += "Old Invoice Amount: " + String.Format("{0:n0}", getI.Item_Total_Amount) + "<br /><br />";
                        body += "New Invoice Amount: " + String.Format("{0:n0}", total) + "<br /><br />";
                        body += "Thanks," + "<br /><br /> JICHANGE";
                        var esmtp = ss.getSMTPText();
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
                    catch (Exception ex)
                    {
                        pay.Error_Text = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                    try
                    {
                        var getD = cum.get_Cust(getI.Cust_Sno);
                        SqlConnection cn = new SqlConnection(strConnString);
                        cn.Open();
                        string txtM = string.Empty;
                        txtM = "Your Control no ammended, Old invoice amount: " + String.Format("{0:n0}", getI.Item_Total_Amount) + Environment.NewLine + "New amount " + String.Format("{0:n0}", total) + Environment.NewLine;// + "TZS " + String.Format("{0:n0}", fee) + " imelipwa kwa ajili ya " + chkFee.Fee_Type + " kupitia ";
                        //txtM = txtM + servicePaymentDetails.transactionChannel + Environment.NewLine + "Namba ya muamala: " + servicePaymentDetails.paymentReference + " Namba ya stakabadhi: " + servicePaymentDetails.transactionRef + Environment.NewLine + "Kiasi unachodaiwa ni TZS " + String.Format("{0:n0}", bamount) + Environment.NewLine + "Ahsante, " + chkFee.Facility_Name;
                        DataSet ds2 = new DataSet();
                        SqlDataAdapter da2 = new SqlDataAdapter(new SqlCommand("select * from tbMessages_sms", cn));
                        SqlCommandBuilder cb2 = new SqlCommandBuilder(da2);
                        da2.FillSchema(ds2, SchemaType.Source);
                        DataTable dt2 = ds2.Tables["Table"];
                        dt2.TableName = "tbMessages_sms";
                        DataRow rs2 = ds2.Tables["tbMessages_sms"].NewRow();

                        rs2["msg_date"] = DateTime.Now;
                        rs2["PhoneNumber"] = "255" + getD.Phone;
                        rs2["TransactionNo"] = getI.Control_No;
                        *//*if (AccNo != null)
                        {
                            rs2["AccountNumber"] = AccNo.Fee_Acc_No;
                        }
                        else
                        {*//*
                        rs2["AccountNumber"] = "0";
                        //}
                        rs2["Message"] = txtM;
                        rs2["trials"] = 0;
                        rs2["deliveryCode"] = DBNull.Value;
                        rs2["sent"] = 0;
                        rs2["delivered"] = 0;
                        rs2["txn_type"] = "SCHOOL";
                        rs2["Msg_Date_Time"] = DateTime.Now;
                        ds2.Tables["tbMessages_sms"].Rows.Add(rs2);
                        da2.Update(ds2, "tbMessages_sms");
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        pay.Error_Text = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                    ssno = sno;
                }
                var result1 = ssno;
                return Request.CreateResponse(new { response = result1, message = "Success" });
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return null;
        }
        [HttpPost]
        public HttpResponseMessage AddCancel(string invno, string auname, string date, string edate, string iedate, string ptype, long chus, long comno, string ccode, string ctype, string cino,
           string twvat, string vtamou, string total, string Inv_remark, int lastrow, List<INVOICE> details, long sno, string warrenty, string goods_status, string delivery_status, string reason)
        {
            try
            {
                *//*DateTime dates = DateTime.Now;
                DateTime dates1 = DateTime.Now;
                DateTime dates2 = DateTime.Now;
          
                dates = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                if (!string.IsNullOrEmpty(edate))
                {
                    dates1 = DateTime.ParseExact(edate, "dd/MM/yyyy", null);
                }
                dates2 = DateTime.ParseExact(iedate, "dd/MM/yyyy", null);
               

                inv.Invoice_No = invno;
                inv.Invoice_Date = dates;
                if (!string.IsNullOrEmpty(edate))
                {
                    inv.Due_Date = Convert.ToDateTime(edate);
                }
                inv.Invoice_Expired_Date = Convert.ToDateTime(iedate);
                inv.Payment_Type = ptype;
                inv.Com_Mas_Sno = long.Parse(compid.ToString());
                inv.Chus_Mas_No = chus;
                inv.Currency_Code = ccode;
                inv.Total_Without_Vt = Decimal.Parse(twvat);
                inv.Vat_Amount = Decimal.Parse(vtamou);
                inv.Total = Decimal.Parse(total);
                inv.Inv_Remarks = Inv_remark;
                inv.warrenty = warrenty;
                inv.goods_status = goods_status;
                inv.delivery_status = delivery_status;
                inv.AuditBy = userid.ToString();*//*
                long ssno = 0;

                if (sno > 0)
                {
                    var getI = inv.GetINVOICEpdf(sno);
                    if (getI != null)
                    {
                        if (pay.Validate_Invoice(getI.Control_No, getI.CompanySno))
                        {
                            return Request.CreateResponse(new { response = 0, message = "Failed" });
                        }
                        ic.Control_No = getI.Control_No;
                        ic.Com_Mas_Sno = getI.CompanySno;
                        ic.Cust_Mas_No = getI.Cust_Sno;
                        ic.Inv_Mas_Sno = sno;
                        ic.Invoice_Amount = getI.Item_Total_Amount;
                        ic.Reason = reason;
                        ic.AuditBy = userid.ToString();
                        ic.AddCancel(ic);
                    }



                    inv.Inv_Mas_Sno = sno;
                    inv.approval_status = "Cancel";
                    inv.UpdateStatus(inv);
                    *//*inv.DeleteInvoicedet(inv);
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
                    }*//*
                    try
                    {
                        var getD = cum.get_Cust(getI.Cust_Sno);
                        //EText emt = new EText();
                        string to = getD.Email;
                        //string attach = f_Path + "/Receipts/" + vp.Permit_Application_No + ".pdf";
                        string from = "";
                        SmtpClient client = new SmtpClient();
                        int port = 0;
                        MailMessage message1 = new MailMessage();
                        string subject = "Control No : " + getI.Control_No + " Cancelled";//"Your ePermit payment receipt";
                        string btext = "Dear " + ", <br><br>";
                        //btext = btext + "Please find attachement of your payment receipt <br><br> Regards,<br>Schools<br />";
                        btext = btext + "<br><br> Regards,<br>Schools<br />";
                        //string body = btext;
                        string body = "Dear: " + getD.Cust_Name + "<br><br>";
                        body += "Your control no has been cancelled for following details " + "<br /><br />";
                        body += "Invoice Amount: " + String.Format("{0:n0}", getI.Item_Total_Amount) + "<br /><br />";
                        //body += "New Invoice Amount: " + String.Format("{0:n0}", total) + "<br /><br />";
                        body += "Thanks," + "<br /><br /> JICHANGE";
                        var esmtp = ss.getSMTPText();
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
                    catch (Exception ex)
                    {
                        pay.Error_Text = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                    try
                    {
                        var getD = cum.get_Cust(getI.Cust_Sno);
                        SqlConnection cn = new SqlConnection(strConnString);
                        cn.Open();
                        string txtM = string.Empty;
                        txtM = "Your Control no has been cancelled, Invoice amount: " + String.Format("{0:n0}", getI.Item_Total_Amount) + Environment.NewLine;// + "TZS " + String.Format("{0:n0}", fee) + " imelipwa kwa ajili ya " + chkFee.Fee_Type + " kupitia ";
                        //txtM = txtM + servicePaymentDetails.transactionChannel + Environment.NewLine + "Namba ya muamala: " + servicePaymentDetails.paymentReference + " Namba ya stakabadhi: " + servicePaymentDetails.transactionRef + Environment.NewLine + "Kiasi unachodaiwa ni TZS " + String.Format("{0:n0}", bamount) + Environment.NewLine + "Ahsante, " + chkFee.Facility_Name;
                        DataSet ds2 = new DataSet();
                        SqlDataAdapter da2 = new SqlDataAdapter(new SqlCommand("select * from tbMessages_sms", cn));
                        SqlCommandBuilder cb2 = new SqlCommandBuilder(da2);
                        da2.FillSchema(ds2, SchemaType.Source);
                        DataTable dt2 = ds2.Tables["Table"];
                        dt2.TableName = "tbMessages_sms";
                        DataRow rs2 = ds2.Tables["tbMessages_sms"].NewRow();

                        rs2["msg_date"] = DateTime.Now;
                        rs2["PhoneNumber"] = "255" + getD.Phone;
                        rs2["TransactionNo"] = getI.Control_No;
                        *//*if (AccNo != null)
                        {
                            rs2["AccountNumber"] = AccNo.Fee_Acc_No;
                        }
                        else
                        {*//*
                        rs2["AccountNumber"] = "0";
                        //}
                        rs2["Message"] = txtM;
                        rs2["trials"] = 0;
                        rs2["deliveryCode"] = DBNull.Value;
                        rs2["sent"] = 0;
                        rs2["delivered"] = 0;
                        rs2["txn_type"] = "SCHOOL";
                        rs2["Msg_Date_Time"] = DateTime.Now;
                        ds2.Tables["tbMessages_sms"].Rows.Add(rs2);
                        da2.Update(ds2, "tbMessages_sms");
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        pay.Error_Text = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                    ssno = sno;
                }
                var result1 = ssno;
                return Request.CreateResponse(new { response = result1, message = "Success" });
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return null;
        }

        [HttpPost]
        public HttpResponseMessage getControl(string control)
        {
            try
            {
                decimal pamount = 0;
                decimal bamount = 0;
                var getC = inv.GetControl_A(control);
                if (getC != null)
                {
                    var getP = pay.GetPayment_Paid(control);
                    if (getP != null)
                    {
                        pamount = getP.Sum(a => a.Amount);
                    }
                    bamount = getC.Item_Total_Amount - pamount;
                    var result = new { Control_No = getC.Control_No, Cust_Name = getC.Cust_Name, Payment_Type = getC.Payment_Type, Item_Total_Amount = getC.Item_Total_Amount, Balance = bamount };
                    return Request.CreateResponse(new { response = result, message = "Success" });
                    //return Json(getC, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return null;
        }
        [HttpPost]
        public HttpResponseMessage GetAmendReport(string invno, string stdate, string enddate, long cust)
        {

            try
            {
                long cno = long.Parse(compid.ToString());
                var result = ic.GetAmendRep(cno, invno, stdate, enddate, cust);
                if (result != null)
                {

                    return Request.CreateResponse(new { response = result, message = "Success" });
                }
                else
                {
                    return Request.CreateResponse(new { response = 0, message = "Failed" });
                }


            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        [HttpPost]
        public HttpResponseMessage GetPaymentReport(string invno, string stdate, string enddate, long cust)
        {

            try
            {
                long cno = long.Parse(compid.ToString());
                var result = pay.GetReport(cno, invno, stdate, enddate, cust);
                if (result != null)
                {

                    return Request.CreateResponse(new { response = result, message = "Success" });
                }
                else
                {
                    return Request.CreateResponse(new { response = 0, message = "Failed" });
                }


            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        public HttpResponseMessage GetCancelReport(string invno, string stdate, string enddate, long cust)
        {

            try
            {
                long cno = long.Parse(compid.ToString());
                var result = ic.GetCancelRep(cno, invno, stdate, enddate, cust);
                if (result != null)
                {

                    return Request.CreateResponse(new { response = result, message = "Success" });
                }
                else
                {
                    return Request.CreateResponse(new { response = 0, message = "Failed" });
                }


            }
            catch (Exception Ex)
            {
                Ex.ToString();
                // Utilites.logfile("Instituion user", drt, Ex.ToString());
            }

            return null;
        }
        private void SendActivationEmail(String email, String auname, String pwd, String uname)
        {
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();

                using (MailMessage mm = new MailMessage())
                {
                    var m = ss.getSMTPText();
                    var data = em.getEMAILst("1");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Local_subject;
                    drt = data.Local_subject;
                    var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Loginnew", "Loginnew"),
                       Query = null,
                   };

                    Uri uri = urlBuilder.Uri;
                    //string url = "web_url";
                    string weburl = System.Web.Configuration.WebConfigurationManager.AppSettings["web_url"].ToString();
                    string url = "<a href='" + weburl + "' target='_blank'>" + weburl + "</a>";
                    //location.href = '/Loginnew/Loginnew';
                    String body = data.Local_Text.Replace("}+cName+{", uname).Replace("}+uname+{", auname).Replace("}+pwd+{", pwd).Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");
                    //m1(weburl);
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    if (string.IsNullOrEmpty(m.SMTP_UName))
                    {
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.Host = m.SMTP_Address;
                    }
                    else
                    {
                        smtp.Host = m.SMTP_Address;
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.EnableSsl = Convert.ToBoolean(m.SSL_Enable);
                        NetworkCredential NetworkCred = new NetworkCredential(m.SMTP_UName, Utilites.DecodeFrom64(m.SMTP_Password));
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                    }
                    smtp.Send(mm);
                }
            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                // Utilites.logfile("Instituion user", drt, Ex.ToString());
            }

        }*/
        #endregion

    }

}
