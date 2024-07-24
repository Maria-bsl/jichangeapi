using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InvoiceController : SetupBaseController
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
        CompanyBankMaster sbm = new CompanyBankMaster();

        string pwd, drt;
        //string strConnString = ConfigurationManager.ConnectionStrings["SchCon"].ToString();
        //string f_Path = System.Web.Configuration.WebConfigurationManager.AppSettings["FileP"].ToString();
        public string ack = string.Empty;
        public string ackm = string.Empty;
        private string strConnString;


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
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                    }

                }
                catch (Exception Ex)
                {
                    // Catch Log here for exception thrown

                    Utilites.logfile("GetchDetails", "0", Ex.ToString());
                    pay.Error_Text = Ex.ToString();
                    pay.AddErrorLogs(pay);


                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            //return null;


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
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                    }

                }
                catch (Exception Ex)
                {
                    // Catch Log here for exception thrown

                    Utilites.logfile("GetchDetails_A", "0", Ex.ToString());
                    pay.Error_Text = Ex.ToString();
                    pay.AddErrorLogs(pay);
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }

            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            //return null;
        }

        [HttpPost]
        public HttpResponseMessage GetchDetails_P(SingletonComp s)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var result = inv.GetINVOICEMas().Where(x => x.approval_status == "2" && x.approval_status != "Cancel");
                    var result = inv.GetINVOICEMas_D(long.Parse(s.compid.ToString()));
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                    }

                }
                catch (Exception Ex)
                {
                    // Catch Log here for exception thrown

                    Utilites.logfile("GetchDetails_P", "0", Ex.ToString());
                    pay.Error_Text = Ex.ToString();
                    pay.AddErrorLogs(pay);
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            //return null;
        }

        [HttpPost]
        public HttpResponseMessage GetchDetails_Pen(SingletonComp s)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //var result = inv.GetINVOICEMas().Where(x => x.approval_status == "2" && x.approval_status != "Cancel");
                    var result = inv.GetINVOICEMas_Pen(long.Parse(s.compid.ToString()));
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                    }

                }
                catch (Exception Ex)
                {
                    // Catch Log here for exception thrown

                    Utilites.logfile("GetchDetails_Pen", "0", Ex.ToString());
                    pay.Error_Text = Ex.ToString();
                    pay.AddErrorLogs(pay);
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            //return null;
        }

        [HttpPost]
        public HttpResponseMessage GetchDetails_Lat(SingletonComp s)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var result = inv.GetINVOICEMas().Where(x => x.approval_status == "2" && x.approval_status != "Cancel");
                    var result = inv.GetINVOICEMas_Lat(long.Parse(s.compid.ToString()));
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                    }

                }
                catch (Exception Ex)
                {
                    // Catch Log here for exception thrown

                    Utilites.logfile("GetchDetails_Lat", "0", Ex.ToString());
                    pay.Error_Text = Ex.ToString();
                    pay.AddErrorLogs(pay);
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            //return null;
        }
        #endregion

        #region Get Signed Invoices
        [HttpPost]
        public HttpResponseMessage GetSignedDetails(SingletonComp s)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                List<INVOICE> result = inv.GetINVOICEMas(long.Parse(s.compid.ToString())).Where(x => x.approval_status == "2").ToList();
                return this.GetList<List<INVOICE>, INVOICE>(result);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
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
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                    }

                }
                catch (Exception Ex)
                {
                    // Catch Log here for exception thrown

                    Utilites.logfile("GetSignedInvoiceById", "0", Ex.ToString());
                    pay.Error_Text = Ex.ToString();
                    pay.AddErrorLogs(pay);
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            //return null;
        }

        #endregion


        #region Get invoice by Id
        [HttpPost]
        public HttpResponseMessage GetInvoiceDetailsbyid(SingletonCompInvid s)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var result = inv.GetINVOICEMas(long.Parse(s.compid.ToString())).Where(x => x.Inv_Mas_Sno == s.invid).FirstOrDefault();
                    var result = inv.GetINVOICEMas1(long.Parse(s.compid.ToString()), s.invid);

                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                    }

                }
                catch (Exception Ex)
                {
                    // Catch Log here for exception thrown

                    Utilites.logfile("GetInvoiceDetailsbyid", "0", Ex.ToString());
                    pay.Error_Text = Ex.ToString();
                    pay.AddErrorLogs(pay);
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            //return null;
        }


        [HttpPost]
        public HttpResponseMessage GetInvoiceInvoicedetails(SingletonInv i)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var result = inv.GetInvoiceDetails(i.invid);
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                    }

                }
                catch (Exception Ex)
                {
                    // Catch Log here for exception thrown

                    Utilites.logfile("GetInvoicedetails", "0", Ex.ToString());
                    pay.Error_Text = Ex.ToString();
                    pay.AddErrorLogs(pay);
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            //return null;
        }

        #endregion

        #region     Get Drodown Master Values

        [HttpPost]
        public HttpResponseMessage Getcompany()
        {
            try
            {
                var result = cp.GetCompanyMas();

                return Request.CreateResponse(new { response = result, message = new List<string> { } });
            }
            catch (Exception Ex)
            {
                // Catch Log here for exception thrown

                Utilites.logfile("GetCompany", "0", Ex.ToString());
                pay.Error_Text = Ex.ToString();
                pay.AddErrorLogs(pay);
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }

            //return null;
        }

        [HttpPost]
        public HttpResponseMessage GetcompanyS(SingletonComp c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    long cno = long.Parse(c.compid.ToString());
                    var result = cp.GetCompanyS(cno);
                    if (result != null)
                    {

                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });

                }
                catch (Exception Ex)
                {
                    // Catch Log here for exception thrown

                    Utilites.logfile("GetcompanyS", "0", Ex.ToString());
                    pay.Error_Text = Ex.ToString();
                    pay.AddErrorLogs(pay);
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            //return null;
        }
        [HttpPost]
        public HttpResponseMessage GetMAccount(SingletonComp c)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    long cno = long.Parse(c.compid.ToString());
                    var result = cd.GetMAccount(cno);
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {

                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                    }
                }
                catch (Exception Ex)
                {
                    // Catch Log here for exception thrown

                    Utilites.logfile("GetMAccount", "0", Ex.ToString());
                    pay.Error_Text = Ex.ToString();
                    pay.AddErrorLogs(pay);
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            //return null;
        }

        [HttpPost]
        public HttpResponseMessage GetCustomersS(SingletonComp c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    long cno = long.Parse(c.compid.ToString());
                    var result = cu.GetCustomersS(cno);
                    if (result != null)
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });

                    else
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                }
                catch (Exception Ex)
                {
                    // Catch Log here for exception thrown

                    Utilites.logfile("GetCustomersS", "0", Ex.ToString());
                    pay.Error_Text = Ex.ToString();
                    pay.AddErrorLogs(pay);
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            //return null;
        }

        [HttpPost]
        public HttpResponseMessage GetVatPer()
        {
            try
            {
                var result = vatpercetage.GetVatPercentage();

                return Request.CreateResponse(new { response = result, message = new List<string> { } });
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }

            //return null;
        }


        [HttpPost]
        public HttpResponseMessage GetCustomers()
        {
            try
            {
                var result = cu.GetCustomers();

                return Request.CreateResponse(new { response = result, message = new List<string> { } });
            }
            catch (Exception Ex)
            {
                // Catch Log here for exception thrown

                Utilites.logfile("GetCustomers", "0", Ex.ToString());
                pay.Error_Text = Ex.ToString();
                pay.AddErrorLogs(pay);
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }

            //return null;
        }

        
        public HttpResponseMessage GetCurrency()
        {
            try
            {
                var result = cy.GetCURRENCY();

                return Request.CreateResponse(new { response = result, message = new List<string> { } });
            }
            catch (Exception Ex)
            {
                // Catch Log here for exception thrown

                Utilites.logfile("GetCurrency", "0", Ex.ToString());
                pay.Error_Text = Ex.ToString();
                pay.AddErrorLogs(pay);
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }

            //return null;
        }
        #endregion



        public HttpResponseMessage GetInvNo(SingletonInvComp c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var result = inv.GetINVOICEMas().Where(x=>x.Invoice_No== invno && x.Com_Mas_Sno == long.Parse(compid.ToString())).FirstOrDefault();
                    if (inv.ValidateNo(c.invno, long.Parse(c.compid.ToString())))
                    {
                        return Request.CreateResponse(new { response = "EXIST", message = new List<string> { } });
                        //return Json("EXIST", JsonRequestBehavior.AllowGet);
                    }

                    //return Request.CreateResponse(new { response = result, message = new List<string> { }});
                }
                catch (Exception Ex)
                {
                    // Catch Log here for exception thrown

                    Utilites.logfile("GetInvNo", "0", Ex.ToString());
                    pay.Error_Text = Ex.ToString();
                    pay.AddErrorLogs(pay);
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            return null;
        }

        private INVOICE CreateInvoice(InvoiceForm invoiceForm)
        {
            CompanyBankMaster companyBankMaster = new CompanyBankMaster();
            INVOICE invoice = new INVOICE();
            CompanyBankMaster approvedCompany = companyBankMaster.GetCompany_MStatus((long)invoiceForm.compid);
            if (approvedCompany.Status.ToLower().ToString().Equals("no"))
            {
                string invoiceSno = invoiceForm.sno.ToString().PadLeft(8, '0');
                invoice.goods_status = "Approved";
                invoice.Control_No = "T" + invoiceSno;
                invoice.approval_status = "2";
                invoice.approval_date = DateTime.Now;
            }
            else
            {
                invoice.Control_No = "";
                invoice.goods_status = "Pending";
                invoice.approval_status = "1";
            }
            invoice.Invoice_No = invoiceForm.invno;
            invoice.Invoice_Date = DateTime.Parse(invoiceForm.date);
            invoice.Due_Date = DateTime.Parse(invoiceForm.edate);
            invoice.Invoice_Expired_Date = DateTime.Parse(invoiceForm.iedate);
            invoice.Payment_Type = invoiceForm.ptype;
            invoice.Com_Mas_Sno = (long)invoiceForm.compid;
            invoice.Chus_Mas_No = (long) invoiceForm.chus;
            invoice.Currency_Code = invoiceForm.ccode;
            invoice.Total_Without_Vt = Decimal.Parse(invoiceForm.twvat);
            invoice.Vat_Amount = Decimal.Parse(invoiceForm.vtamou);
            invoice.Total = Decimal.Parse(invoiceForm.total);
            invoice.Inv_Remarks = invoiceForm.Inv_remark;
            invoice.warrenty = invoiceForm.warrenty;
            invoice.delivery_status = invoiceForm.delivery_status;
            invoice.AuditBy = invoiceForm.userid.ToString();
            return invoice;
        }

        private INVOICE CreateEditInvoice(InvoiceForm invoiceForm)
        {
            INVOICE invoice = new INVOICE();
            INVOICE found = invoice.GetINVOICEMas1((long)invoiceForm.compid, (long)invoiceForm.sno);
            if (found == null) return null;
            found.Due_Date = DateTime.Parse(invoiceForm.edate);
            found.Invoice_Expired_Date = DateTime.Parse(invoiceForm.edate);
            found.Payment_Type = invoiceForm.ptype;
            found.Inv_Remarks = invoiceForm.Inv_remark;
            return found;
        }

        private void InsertInvoiceDetails(List<INVOICE> details,long invoiceSno)
        {
            for (int i = 0; i < details.Count(); i++)
            {
                INVOICE detail = details[i];
                if (detail.Inv_Mas_Sno == 0)
                {
                    INVOICE invoice = new INVOICE();
                    invoice.Inv_Mas_Sno = invoiceSno;
                    invoice.Item_Description = detail.Item_Description;
                    invoice.Item_Qty = detail.Item_Qty;
                    invoice.Item_Unit_Price = detail.Item_Unit_Price;
                    invoice.Item_Total_Amount = detail.Item_Total_Amount;
                    invoice.Vat_Percentage = detail.Vat_Percentage;
                    invoice.Vat_Amount = detail.Vat_Amount;
                    invoice.Item_Without_vat = detail.Item_Without_vat;
                    invoice.Remarks = detail.Remarks;
                    invoice.vat_category = detail.vat_category;
                    invoice.Vat_Type = detail.Vat_Type;
                    invoice.AddInvoiceDetails(invoice);
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage FindInvoice(long companySno,long invoiceSno)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                INVOICE found = invoice.GetINVOICEMas1(companySno, invoiceSno);
                if (found == null) { return this.GetNotFoundResponse();  }
                List<INVOICE> details = invoice.GetInvoiceDetails(invoiceSno);
                string jsonString = JsonSerializer.Serialize(found);
                JsonObject jsonObject = JsonNode.Parse(jsonString).AsObject();
                var detailsArray = new JsonArray();
                for (int i = 0; i < details.Count(); i++)
                {
                    var json = JsonSerializer.Serialize(details[i]);
                    JsonObject detail = JsonNode.Parse(json).AsObject();
                    detailsArray.Add(detail);
                }
                jsonObject.Add("details", detailsArray);
                return this.SuccessJsonResponse(jsonObject);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private HttpResponseMessage InsertInvoice(InvoiceForm invoiceForm)
        {
            try
            {
                INVOICE invoice = CreateInvoice(invoiceForm);
                bool isExistInvoiceNo = invoice.ValidateNo(invoiceForm.invno, (long) invoiceForm.compid); 
                if (isExistInvoiceNo)
                {
                    var messages = new List<string> { "Invoice number exists" };
                    return this.GetCustomErrorMessageResponse(messages);
                }
                bool isExistControlNo = invoice.ValidateControl(invoice.Control_No);
                if (isExistControlNo)
                {
                    var messages = new List<string> { "Control number exists" };
                    return this.GetCustomErrorMessageResponse(messages);
                }
                long invoiceSno = invoice.Addinvoi(invoice);
                InsertInvoiceDetails(invoiceForm.details,invoiceSno);
                return FindInvoice((long) invoiceForm.compid, invoiceSno);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private HttpResponseMessage ApproveInvoice(InvoiceForm invoiceForm)
        {
            try
            {
                INVOICE invoice = CreateInvoice(invoiceForm);
                invoice.Inv_Mas_Sno = invoiceForm.sno;
                invoice.goods_status = "Approved";
                string controlNumber = invoiceForm.sno.ToString().PadLeft(8, '0');
                invoice.Control_No = "T" + controlNumber;
                invoice.UpdateInvoiMasForTRA1(invoice);
                invoice.approval_status = "2";
                invoice.approval_date = System.DateTime.Now;
                invoice.UpdateInvoice(invoice);
                return FindInvoice((long)invoiceForm.compid, invoiceForm.sno);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private HttpResponseMessage UpdateInvoice(InvoiceForm invoiceForm)
        {
            try
            {
                INVOICE invoice = CreateEditInvoice(invoiceForm);
                if (invoice == null) { return this.GetNotFoundResponse(); }
                invoice.UpdateInvoiMas(invoice);
                invoice.DeleteInvoicedet(invoice);
                InsertInvoiceDetails(invoiceForm.details, invoice.Inv_Mas_Sno);
                return FindInvoice((long)invoiceForm.compid, invoice.Inv_Mas_Sno);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddInvoice(InvoiceForm invoiceForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                if (invoiceForm.sno == 0) { return InsertInvoice(invoiceForm); }
                else if (invoiceForm.sno > 0 && invoiceForm.goods_status == "Approve") { return ApproveInvoice(invoiceForm); }
                else return UpdateInvoice(invoiceForm);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        #region Save Update Invoice
        /*[HttpPost]
        public HttpResponseMessage AddInvoice(InvoiceForm o)
        {
            if (ModelState.IsValid) { 
                try
            {

                   // Check for Userid for created invoice
                   var cb = sbm.GetCompany_MStatus((long)o.compid);
                   if (cb.Status.ToLower().ToString().Equals("no"))
                   {
                       // Automatic Approve Invoice and Generated Control number
                       string cno = string.Empty;
                       cno = o.sno.ToString().PadLeft(8, '0');

                       inv.goods_status = "Approved";
                       inv.Control_No = "T" + cno;
                       inv.approval_status = "2";
                       inv.approval_date = System.DateTime.Now;
                       //inv.UpdateInvoice(inv);


                   }
                   else
                   {
                       inv.Control_No = "";
                       //inv.approval_date = ;
                       inv.goods_status = "Pending";
                       inv.approval_status = "1";
                   }

                DateTime dates = DateTime.Now;
                DateTime dates1 = DateTime.Now;
                DateTime dates2 = DateTime.Now;
                //dates = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                dates = DateTime.Parse(o.date);
                if (!string.IsNullOrEmpty(o.edate))
                {
                    //dates1 = DateTime.ParseExact(edate, "dd/MM/yyyy", null);
                    dates1 = DateTime.Parse(o.edate);
                }
                //dates2 = DateTime.ParseExact(iedate, "dd/MM/yyyy", null);
                dates2 = DateTime.Parse(o.iedate);

                inv.Invoice_No = o.invno;
                inv.Invoice_Date = dates;
                if (!string.IsNullOrEmpty(o.edate))
                {
                    inv.Due_Date = Convert.ToDateTime(o.edate);
                }
                inv.Invoice_Expired_Date = Convert.ToDateTime(o.iedate);
                inv.Payment_Type = o.ptype;
                inv.Com_Mas_Sno = long.Parse(o.compid.ToString());
                inv.Chus_Mas_No = o.chus;
                inv.Currency_Code = o.ccode;
                inv.Total_Without_Vt = Decimal.Parse(o.twvat);
                inv.Vat_Amount = Decimal.Parse(o.vtamou);
                inv.Total = Decimal.Parse(o.total);
                inv.Inv_Remarks = o.Inv_remark;
                inv.warrenty = o.warrenty;
                //inv.goods_status = "Pending";
                inv.delivery_status = o.delivery_status;
                //inv.Customer_ID_Type = string.Empty;
                //inv.Customer_ID_No = cino;
                inv.AuditBy = o.userid.ToString();
                long ssno = 0;

                if (o.sno == 0)
                {
                       if (inv.ValidateNo(o.invno, long.Parse(o.compid.ToString())))
                       {
                           return Request.CreateResponse(new {response = "EXIST", message = "Failed" });
                       }
                        ssno = inv.Addinvoi(inv);
                        for (int i = 0; i < o.details.Count; i++)
                        {
                            if (o.details[i].Inv_Mas_Sno == 0)
                            {
                                inv.Inv_Mas_Sno = ssno;
                                inv.Item_Description = o.details[i].Item_Description;
                                inv.Item_Qty = o.details[i].Item_Qty;
                                inv.Item_Unit_Price = o.details[i].Item_Unit_Price;
                                inv.Item_Total_Amount = o.details[i].Item_Total_Amount;
                                inv.Vat_Percentage = o.details[i].Vat_Percentage;
                                inv.Vat_Amount = o.details[i].Vat_Amount;
                                inv.Item_Without_vat = o.details[i].Item_Without_vat;
                                inv.Remarks = o.details[i].Remarks;
                                inv.vat_category = o.details[i].vat_category;
                                inv.Vat_Type = o.details[i].Vat_Type;
                                inv.AddInvoiceDetails(inv);
                            }
                        }

                }
                else if (o.sno > 0 && o.goods_status == "Approve")
                {
                    string cno = string.Empty;
                    cno = o.sno.ToString().PadLeft(8, '0');

                    inv.Inv_Mas_Sno = o.sno;
                    inv.goods_status = "Approved";
                    //inv.daily_count = 0;// (int)daiC;
                    //inv.grand_count = 0;// (int)graC;
                    inv.Control_No = "T" + cno;
                    inv.UpdateInvoiMasForTRA1(inv);
                    inv.approval_status = "2";
                    inv.approval_date = System.DateTime.Now;
                    inv.UpdateInvoice(inv);
                    ssno = o.sno;
                }
                else if (o.sno > 0)
                {
                    var getI = inv.EditINVOICEMas(o.sno);
                    bool flag = true;
                    if (getI.Invoice_No == o.invno)
                    {
                        flag = false;
                    }
                   *//* if (inv.ValidateNo(o.invno, long.Parse(o.compid.ToString())) && flag == true)
                    {
                        return Request.CreateResponse(new { response = "EXIST", message = "Failed" });
                    }*//*
                    inv.Inv_Mas_Sno = o.sno;
                    inv.UpdateInvoiMas(inv);
                    inv.DeleteInvoicedet(inv);
                    for (int i = 0; i < o.details.Count; i++)
                    {
                        if (o.details[i].Inv_Mas_Sno == 0)
                        {
                            inv.Inv_Mas_Sno = o.sno;
                            inv.Item_Description = o.details[i].Item_Description;
                            inv.Item_Qty = o.details[i].Item_Qty;
                            inv.Item_Unit_Price = o.details[i].Item_Unit_Price;
                            inv.Item_Total_Amount = o.details[i].Item_Total_Amount;
                            inv.Vat_Percentage = o.details[i].Vat_Percentage;
                            inv.Vat_Amount = o.details[i].Vat_Amount;
                            inv.Item_Without_vat = o.details[i].Item_Without_vat;
                            inv.Remarks = o.details[i].Remarks;
                            inv.vat_category = o.details[i].vat_category;
                            inv.AddInvoiceDetails(inv);
                        }
                    }
                    ssno = o.sno;
                }
                var result1 = ssno;
                return Request.CreateResponse(new { response = result1, message = new List<string> { } });
            }
            catch (Exception Ex)
            {
                // Catch Log here for exception thrown

                Utilites.logfile("Add Invoice", "0", Ex.ToString());
                pay.Error_Text = Ex.ToString();
                pay.AddErrorLogs(pay);
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
                //return null;
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        }*/



        [HttpPost]
         public HttpResponseMessage AddAmend(AddAmendForm f)
         {
             try
             {
                 DateTime dates = DateTime.Now;
                 DateTime dates1 = DateTime.Now;
                 DateTime dates2 = DateTime.Now;
                 //string[] sdate = date.Split('/');

                 //date = sdate[0] + "/" + sdate[1] + "/" + sdate[2];
                 dates = DateTime.ParseExact(f.date, "dd/MM/yyyy", null);
                 if (!string.IsNullOrEmpty(f.edate))
                 {
                     //string[] sdate1 = edate.Split('/');
                     //edate = sdate1[0] + "-" + sdate1[1] + "-" + sdate1[2];
                     dates1 = DateTime.ParseExact(f.edate, "dd/MM/yyyy", null);
                 }
                 //string[] sdate2 = iedate.Split('/');
                 dates2 = DateTime.ParseExact(f.iedate, "dd/MM/yyyy", null);
                 //iedate = sdate2[0] + "-" + sdate2[1] + "-" + sdate2[2];


                 inv.Invoice_No = f.invno;
                 inv.Invoice_Date = dates;
                 if (!string.IsNullOrEmpty(f.edate))
                 {
                     inv.Due_Date = Convert.ToDateTime(f.edate);
                 }
                 inv.Invoice_Expired_Date = Convert.ToDateTime(f.iedate);
                 inv.Payment_Type = f.ptype;
                 inv.Com_Mas_Sno = long.Parse(f.compid.ToString());
                 inv.Chus_Mas_No = f.chus;
                 inv.Currency_Code = f.ccode;
                 inv.Total_Without_Vt = Decimal.Parse(f.twvat);
                 inv.Vat_Amount = Decimal.Parse(f.vtamou);
                 inv.Total = Decimal.Parse(f.total);
                 inv.Inv_Remarks = f.Inv_remark;
                 inv.warrenty = f.warrenty;
                 inv.goods_status = f.goods_status;
                 inv.delivery_status = f.delivery_status;
                 //inv.Customer_ID_Type = string.Empty;
                 //inv.Customer_ID_No = cino;
                 inv.AuditBy = f.userid.ToString();
                 long ssno = 0;

                 if (f.sno > 0)
                 {
                     var getI = inv.GetINVOICEpdf(f.sno);
                     if (getI != null)
                     {
                         if (Decimal.Parse(f.total) == getI.Item_Total_Amount)
                         {
                             return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                         }
                         ic.Control_No = getI.Control_No;
                         ic.Com_Mas_Sno = getI.CompanySno;
                         ic.Cust_Mas_No = getI.Cust_Sno;
                         ic.Inv_Mas_Sno = f.sno;
                         ic.Invoice_Amount = getI.Item_Total_Amount;
                         ic.Payment_Type = f.ptype;
                         ic.Amment_Amount = Decimal.Parse(f.total);
                         if (!string.IsNullOrEmpty(f.edate))
                         {
                             ic.Due_Date = Convert.ToDateTime(f.edate);
                         }
                         ic.Invoice_Expired_Date = Convert.ToDateTime(f.iedate);
                         ic.Reason = f.reason;
                         ic.AuditBy = f.userid.ToString();
                         ic.AddAmmend(ic);
                     }



                     inv.Inv_Mas_Sno = f.sno;
                     inv.UpdateInvoiMas(inv);
                     inv.DeleteInvoicedet(inv);
                     for (int i = 0; i < f.details.Count; i++)
                     {
                         if (f.details[i].Inv_Mas_Sno == 0)
                         {
                             inv.Inv_Mas_Sno = f.sno;
                             inv.Item_Description = f.details[i].Item_Description;
                             inv.Item_Qty = f.details[i].Item_Qty;
                             inv.Item_Unit_Price = f.details[i].Item_Unit_Price;
                             inv.Item_Total_Amount = f.details[i].Item_Total_Amount;
                             inv.Vat_Percentage = f.details[i].Vat_Percentage;
                             inv.Vat_Amount = f.details[i].Vat_Amount;
                             inv.Item_Without_vat = f.details[i].Item_Without_vat;
                             inv.Remarks = f.details[i].Remarks;
                             inv.vat_category = f.details[i].vat_category;
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
                         body += "New Invoice Amount: " + String.Format("{0:n0}", f.total) + "<br /><br />";
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

                        Utilites.logfile("Add Ammend - email", "0", ex.ToString());
                        pay.Error_Text = ex.ToString();
                        pay.AddErrorLogs(pay);
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", ex.ToString() } });
                    }
                    try
                    {
                        var getD = cum.get_Cust(getI.Cust_Sno);
                        SqlConnection cn = new SqlConnection(strConnString);
                        cn.Open();
                        string txtM = string.Empty;
                        txtM = "Your Control no ammended, Old invoice amount: " + String.Format("{0:n0}", getI.Item_Total_Amount) + Environment.NewLine + "New amount " + String.Format("{0:n0}", f.total) + Environment.NewLine;// + "TZS " + String.Format("{0:n0}", fee) + " imelipwa kwa ajili ya " + chkFee.Fee_Type + " kupitia ";
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
                        /*  if (AccNo != null)
                          {
                              rs2["AccountNumber"] = AccNo.Fee_Acc_No;
                          }
                          else
                          {
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
                          }*/
                    }
                    catch (Exception ex)
                    {

                        Utilites.logfile("Add Ammend - sms", "0", ex.ToString());
                        pay.Error_Text = ex.ToString();
                        pay.AddErrorLogs(pay);
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", ex.ToString() } });
                    }
                     ssno = f.sno;
                 }

                 var result1 = ssno;
                 return Request.CreateResponse(new { response = result1, message = new List<string> { } });
             }
             catch (Exception Ex)
             {
                 return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
             }
             //return null;
         }


         [HttpPost]
         public HttpResponseMessage AddCancel(AddAmendForm ac)
         {
            /*string invno, string auname, string date, string edate, string iedate, string ptype, long chus, long comno, string ccode, string ctype, string cino,
            string twvat, string vtamou, string total, string Inv_remark, int lastrow, List< INVOICE > details, long sno, string warrenty, string goods_status, string delivery_status, string reason*/
             try
             {
                 DateTime dates = DateTime.Now;
                 DateTime dates1 = DateTime.Now;
                 DateTime dates2 = DateTime.Now;

                 dates = DateTime.ParseExact(ac.date, "dd/MM/yyyy", null);
                 if (!string.IsNullOrEmpty(ac.edate))
                 {
                     dates1 = DateTime.ParseExact(ac.edate, "dd/MM/yyyy", null);
                 }
                 dates2 = DateTime.ParseExact(ac.iedate, "dd/MM/yyyy", null);


                 inv.Invoice_No = ac.invno;
                 inv.Invoice_Date = dates;
                 if (!string.IsNullOrEmpty(ac.edate))
                 {
                     inv.Due_Date = Convert.ToDateTime(ac.edate);
                 }
                 inv.Invoice_Expired_Date = Convert.ToDateTime(ac.iedate);
                 inv.Payment_Type = ac.ptype;
                 inv.Com_Mas_Sno = long.Parse(ac.compid.ToString());
                 inv.Chus_Mas_No = ac.chus;
                 inv.Currency_Code = ac.ccode;
                 inv.Total_Without_Vt = Decimal.Parse(ac.twvat);
                 inv.Vat_Amount = Decimal.Parse(ac.vtamou);
                 inv.Total = Decimal.Parse(ac.total);
                 inv.Inv_Remarks = ac.Inv_remark;
                 inv.warrenty = ac.warrenty;
                 inv.goods_status = ac.goods_status;
                 inv.delivery_status = ac.delivery_status;
                 inv.AuditBy = ac.userid.ToString();
                 long ssno = 0;

                 if (ac.sno > 0)
                 {
                     var getI = inv.GetINVOICEpdf(ac.sno);
                     if (getI != null)
                     {
                         if (pay.Validate_Invoice(getI.Control_No, getI.CompanySno))
                         {
                             return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                         }
                         ic.Control_No = getI.Control_No;
                         ic.Com_Mas_Sno = getI.CompanySno;
                         ic.Cust_Mas_No = getI.Cust_Sno;
                         ic.Inv_Mas_Sno = ac.sno;
                         ic.Invoice_Amount = getI.Item_Total_Amount;
                         ic.Reason = ac.reason;
                         ic.AuditBy = ac.userid.ToString();
                         ic.AddCancel(ic);
                     }



                     inv.Inv_Mas_Sno = ac.sno;
                     inv.approval_status = "Cancel";
                     inv.UpdateStatus(inv);
                     inv.DeleteInvoicedet(inv);
                     for (int i = 0; i < ac.details.Count; i++)
                     {
                         if (ac.details[i].Inv_Mas_Sno == 0)
                         {
                             inv.Inv_Mas_Sno = ac.sno;
                             inv.Item_Description = ac.details[i].Item_Description;
                             inv.Item_Qty = ac.details[i].Item_Qty;
                             inv.Item_Unit_Price = ac.details[i].Item_Unit_Price;
                             inv.Item_Total_Amount = ac.details[i].Item_Total_Amount;
                             inv.Vat_Percentage = ac.details[i].Vat_Percentage;
                             inv.Vat_Amount = ac.details[i].Vat_Amount;
                             inv.Item_Without_vat = ac.details[i].Item_Without_vat;
                             inv.Remarks = ac.details[i].Remarks;
                             inv.vat_category = ac.details[i].vat_category;
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

                        Utilites.logfile("Add Cancel - email", "0", ex.ToString());
                        pay.Error_Text = ex.ToString();
                        pay.AddErrorLogs(pay);
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", ex.ToString() } });
                    }
                    try
                    {
                        /*  var getD = cum.get_Cust(getI.Cust_Sno);
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
                        if (AccNo != null)
                         {
                             rs2["AccountNumber"] = AccNo.Fee_Acc_No;
                         }
                         else
                         {
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
                         }*/
                    }
                    catch (Exception ex)
                    {

                        Utilites.logfile("Add Cancel - sms", "0", ex.ToString());
                        pay.Error_Text = ex.ToString();
                        pay.AddErrorLogs(pay);
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", ex.ToString() } });
                    }
                     ssno = ac.sno;
                 }
                 var result1 = ssno;
                 return Request.CreateResponse(new { response = result1, message = new List<string> { } });
             }
             catch (Exception Ex)
             {
                 // Catch Log here for exception thrown

                 Utilites.logfile("Add Cancel", "0", Ex.ToString());
                 pay.Error_Text = Ex.ToString();
                 pay.AddErrorLogs(pay);
                 return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
             }
             //return null;
         }

         [HttpPost]
         public HttpResponseMessage GetControl(SingletonControl c)
         {
             if (ModelState.IsValid) { 

                     try
                     {
                         decimal pamount = 0;
                         decimal bamount = 0;
                         var getC = inv.GetControl_A(c.control);
                         if (getC != null)
                         {
                             var getP = pay.GetPayment_Paid(c.control);
                             if (getP != null)
                             {
                                 pamount = getP.Sum(a => a.Amount);
                             }
                             bamount = getC.Item_Total_Amount - pamount;
                             var result = new { Control_No = getC.Control_No, Cust_Name = getC.Cust_Name, Payment_Type = getC.Payment_Type, Item_Total_Amount = getC.Item_Total_Amount, Balance = bamount };
                             return Request.CreateResponse(new { response = result, message = new List<string> { } });
                             //return Json(getC, JsonRequestBehavior.AllowGet);
                         }
                         else
                         {
                             return Request.CreateResponse(new { response = false, message = new List<string> { "Failed to get control number details"} });
                         }


                     }
                     catch (Exception Ex)
                     {
                         // Catch Log here for exception thrown

                         Utilites.logfile("Get Control", "0", Ex.ToString());
                         pay.Error_Text = Ex.ToString();
                         pay.AddErrorLogs(pay);
                         return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                     }
                 //return null;
             }
             else
             {
                 var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                 return Request.CreateResponse(new { response = 0, message = errorMessages });
             }
         }


         [HttpPost]
         public HttpResponseMessage GetAmendReport(CancelRepModel c)
         { 
             if (ModelState.IsValid) { 
                 try
                 {
                     long cno = long.Parse(c.compid.ToString());
                     var result = ic.GetAmendRep(cno, c.invno, c.stdate, c.enddate, c.cust);
                     if (result != null)
                     {

                         return Request.CreateResponse(new { response = result, message = new List<string> { } });
                     }
                     else
                     {
                         return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                     }


                 }
                 catch (Exception Ex)
                 {
                     // Catch Log here for exception thrown

                     Utilites.logfile("GetAmendReport", "0", Ex.ToString());
                     pay.Error_Text = Ex.ToString();
                     pay.AddErrorLogs(pay);
                     return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                 }
             }
             else
             {
                 var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                 return Request.CreateResponse(new { response = 0, message = errorMessages });
             }

             //return null;
         }


         [HttpPost]
         public HttpResponseMessage GetPaymentReport(CancelRepModel c)
         {
             if (ModelState.IsValid) { 
                 try
                 {
                     long cno = long.Parse(c.compid.ToString());
                     var result = pay.GetReport(cno, c.invno, c.stdate, c.enddate, c.cust);
                     if (result != null)
                     {

                         return Request.CreateResponse(new { response = result, message = new List<string> { } });
                     }
                     else
                     {
                         return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                     }


                 }
                 catch (Exception Ex)
                 {
                     // Catch Log here for exception thrown

                     Utilites.logfile("GetPaymentReport", "0", Ex.ToString());
                     pay.Error_Text = Ex.ToString();
                     pay.AddErrorLogs(pay);
                     return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                 }
             }
             else
             {
                 var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                 return Request.CreateResponse(new { response = 0, message = errorMessages });
             }
             //return null;
         }


         [HttpPost]
         public HttpResponseMessage GetCancelReport(CancelRepModel p)
         {
             if (ModelState.IsValid) { 

                 try
                 {
                     long cno = long.Parse(p.compid.ToString());
                     var result = ic.GetCancelRep(cno, p.invno, p.stdate, p.enddate, p.cust);
                     if (result != null)
                     {

                         return Request.CreateResponse(new { response = result, message = new List<string> { } });
                     }
                     else
                     {
                         return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed" } });
                     }


                 }
                 catch (Exception Ex)
                 {
                     // Catch Log here for exception thrown

                     Utilites.logfile("GetCancelReport", "0", Ex.ToString());
                     pay.Error_Text = Ex.ToString();
                     pay.AddErrorLogs(pay);
                     return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                     // Utilites.logfile("Instituion user", drt, Ex.ToString());
                 }
             }
             else
             {
                 var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                 return Request.CreateResponse(new { response = 0, message = errorMessages });
             }
             //return null;
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
                     /*var urlBuilder =
                        new System.UriBuilder(Request.Url.AbsoluteUri)
                        {
                            Path = Url.Action("Loginnew", "Loginnew"),
                            Query = null,
                        };*/

                     //Uri uri = urlBuilder.Uri;
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

         }

         #endregion


        #endregion

    }

}