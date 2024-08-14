using BL.BIZINVOICING.BusinessEntities.Common;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Controllers.smsservices;
using JichangeApi.Models;
using JichangeApi.Models.form;
using JichangeApi.Services.Companies;
using JichangeApi.Utilities;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace JichangeApi.Services
{
    public class InvoiceService
    {
        private CompanyBankService companyBankService = new CompanyBankService();

        Payment pay = new Payment();
        
        private INVOICE CreateInvoice(InvoiceForm invoiceForm)
        {
            CompanyBankMaster companyBankMaster = new CompanyBankMaster();
            INVOICE invoice = new INVOICE();
            CompanyBankMaster approvedCompany = companyBankMaster.GetCompany_MStatus((long)invoiceForm.compid);
            if (approvedCompany != null && !approvedCompany.Checker.ToLower().ToString().Equals("no"))
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
            invoice.Chus_Mas_No = (long)invoiceForm.chus;
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
        private INVOICE CreateAmendInvoice(AddAmendForm addAmendForm)
        {
            INVOICE invoice = new INVOICE();
            invoice.Invoice_No = addAmendForm.invno;
            invoice.Invoice_Date = DateTime.Parse(addAmendForm.date); //DateTime.ParseExact(addAmendForm.date, "yyyy-MM-dd", null); ;
            if (!string.IsNullOrEmpty(addAmendForm.edate))
            {
                invoice.Due_Date = DateTime.Parse(addAmendForm.edate); //DateTime.ParseExact(addAmendForm.edate, "yyyy-MM-dd", null);
            }
            invoice.Invoice_Expired_Date = DateTime.Parse(addAmendForm.iedate); //DateTime.ParseExact(addAmendForm.iedate, "yyyy-MM-dd", null);
            invoice.Payment_Type = addAmendForm.ptype;
            invoice.Com_Mas_Sno = (long)addAmendForm.compid;
            invoice.Chus_Mas_No = addAmendForm.chus;
            invoice.Currency_Code = addAmendForm.ccode;
            invoice.Total_Without_Vt = Decimal.Parse(addAmendForm.twvat);
            invoice.Vat_Amount = Decimal.Parse(addAmendForm.vtamou);
            invoice.Total = Decimal.Parse(addAmendForm.total);
            invoice.Inv_Remarks = addAmendForm.Inv_remark;
            invoice.warrenty = addAmendForm.warrenty;
            invoice.goods_status = addAmendForm.goods_status;
            invoice.delivery_status = addAmendForm.delivery_status;
            invoice.AuditBy = addAmendForm.userid.ToString();
            return invoice;
        }
        private INVOICE CreateCancelInvoice(AddAmendForm addAmendForm)
        {
            return CreateAmendInvoice(addAmendForm);
        }
        private void InsertInvoiceDetails(List<INVOICE> details, long invoiceSno)
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
        private InvoiceC AddInvoiceAmendment(AddAmendForm addAmendForm,InvoicePDfData invoicePDfData)
        {
            InvoiceC invoiceC = new InvoiceC();
            //bool isSameAmount = Decimal.Parse(addAmendForm.total) == invoicePDfData.Item_Total_Amount;
            //if (isSameAmount) throw new ArgumentException("Modify items for amendments");
            invoiceC.Control_No = invoicePDfData.Control_No;
            invoiceC.Com_Mas_Sno = invoicePDfData.CompanySno;
            invoiceC.Cust_Mas_No = invoicePDfData.Cust_Sno;
            invoiceC.Inv_Mas_Sno = addAmendForm.sno;
            invoiceC.Invoice_Amount = invoicePDfData.Item_Total_Amount;
            invoiceC.Payment_Type = addAmendForm.ptype;
            invoiceC.Amment_Amount = Decimal.Parse(addAmendForm.total);
            if (!string.IsNullOrEmpty(addAmendForm.edate))
            {
                invoiceC.Due_Date = DateTime.Parse(addAmendForm.edate); //DateTime.ParseExact(addAmendForm.edate, "dd/MM/yyyy", null); 
            }
            invoiceC.Invoice_Expired_Date = DateTime.Parse(addAmendForm.iedate); //DateTime.ParseExact(addAmendForm.iedate, "dd/MM/yyyy", null);
            invoiceC.Reason = addAmendForm.reason;
            invoiceC.AuditBy = addAmendForm.userid.ToString();
            invoiceC.AddAmmend(invoiceC);
            return invoiceC;
        }
        private InvoiceC AttemptCancelInvoice(AddAmendForm addAmendForm, InvoicePDfData invoicePDfData)
        {
            Payment pay = new Payment();
            if (pay.Validate_Invoice(invoicePDfData.Control_No, invoicePDfData.CompanySno))
            {
                throw new ArgumentException("You cannot cancel a paid invoice");
            }
            InvoiceC invoiceC = new InvoiceC();
            invoiceC.Control_No = invoicePDfData.Control_No;
            invoiceC.Com_Mas_Sno = invoicePDfData.CompanySno;
            invoiceC.Cust_Mas_No = invoicePDfData.Cust_Sno;
            invoiceC.Inv_Mas_Sno = addAmendForm.sno;
            invoiceC.Invoice_Amount = invoicePDfData.Item_Total_Amount;
            invoiceC.Reason = addAmendForm.reason;
            invoiceC.AuditBy = addAmendForm.userid.ToString();
            invoiceC.AddCancel(invoiceC);
            return invoiceC;
        }
        private void CreateAmendInvoiceEmailContent(long customerId,InvoicePDfData invoicePDfData,AddAmendForm addAmendForm)
        {
            try
            {
                CustomerMaster customer = new CustomerMaster().get_Cust(customerId);
                string subject = "Control No : " + invoicePDfData.Control_No + " Amended";
                string btext = "Dear " + ", <br><br>";
                btext = btext + "<br><br> Regards,<br>Schools<br />";
                string body = "Dear: " + customer.Cust_Name + "<br><br>";
                body += "Your control no amended for following details " + "<br /><br />";
                body += "Old Invoice Amount: " + String.Format("{0:n0}", invoicePDfData.Item_Total_Amount) + "<br /><br />";
                body += "New Invoice Amount: " + String.Format("{0:n0}", addAmendForm.total) + "<br /><br />";
                body += "Thanks," + "<br /><br /> JICHANGE";
                EmailUtils.SendSubjectTextBodyEmail(customer.Email, subject, btext, body);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        private void CreateCancelInvoiceEmailContent(long customerId, InvoicePDfData invoicePDfData, AddAmendForm addAmendForm)
        {
            try
            {
                CustomerMaster customer = new CustomerMaster().get_Cust(customerId);
                string subject = "Control No : " + invoicePDfData.Control_No + " Cancelled";//"Your ePermit payment receipt";
                string btext = "Dear " + ", <br><br>";
                //btext = btext + "Please find attachement of your payment receipt <br><br> Regards,<br>Schools<br />";
                btext = btext + "<br><br> Regards,<br>Jichange<br />";
                //string body = btext;
                string body = "Dear: " + customer.Cust_Name + "<br><br>";
                body += "Your control no has been cancelled for following details " + "<br /><br />";
                body += "Invoice Amount: " + String.Format("{0:n0}", invoicePDfData.Item_Total_Amount) + "<br /><br />";
                //body += "New Invoice Amount: " + String.Format("{0:n0}", total) + "<br /><br />";
                body += "Thanks," + "<br /><br /> JICHANGE";
                EmailUtils.SendSubjectTextBodyEmail(customer.Email, subject, btext, body);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);


                throw new Exception(ex.Message);
            }
        }
        private void UpdateControlNumber(INVOICE invoice,long invoiceSno)
        {
            invoice.Inv_Mas_Sno = invoiceSno;
            CompanyBankMaster approvedCompany = new CompanyBankMaster().GetCompany_MStatus(invoice.Com_Mas_Sno);
            if (approvedCompany != null && approvedCompany.Checker.ToLower().ToString().Equals("no"))
            {
                string control = invoice.Inv_Mas_Sno.ToString().PadLeft(8, '0');
                invoice.goods_status = "Approved";
                invoice.Control_No = "T" + control;
                invoice.approval_status = "2";
                invoice.approval_date = DateTime.Now;
                invoice.UpdateInvoice(invoice);
            }
        }
        public List<INVOICE> GetSignedDetails(SingletonComp singletonComp,int? page,int? limit)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var invoices = invoice.GetINVOICEMas((long)singletonComp.compid,page,limit).Where(x => x.approval_status == "2").ToList();
                return invoices != null ? invoices : new List<INVOICE>();
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public INVOICE GetSignedInvoiceById(SingletonCompInvid singletonCompInvid)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetINVOICEMas2((long) singletonCompInvid.compid, (long) singletonCompInvid.invid);
                if (result == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                return result;
            }
            catch (ArgumentException ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public INVOICE GetInvoiceDetailById(SingletonCompInvid singletonCompInvid )
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetINVOICEMas1((long)singletonCompInvid.compid, (long)singletonCompInvid.invid);
                if (result == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                return result;
            }
            catch (ArgumentException ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<INVOICE> GetInvoiceDetails(SingletonInv singletonInv)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetInvoiceDetails((long) singletonInv.invid);
                return result != null ? result : new List<INVOICE>();
            }
            catch (ArgumentException ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public bool IsExistCompanyInvoiceNumber(SingletonInvComp singletonInvComp)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                bool exists = invoice.ValidateNo(singletonInvComp.invno, (long)singletonInvComp.compid);
                return exists;
            }
            catch (ArgumentException ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public JsonObject FindInvoice(long companySno, long invoiceSno)
        {
            try
            {
                INVOICE found = new INVOICE().GetINVOICEMas1(companySno, invoiceSno);
                if (found == null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
                List<INVOICE> details = found.GetInvoiceDetails(invoiceSno);
                string jsonString = JsonSerializer.Serialize(found);
                JsonObject jsonObject = JsonNode.Parse(jsonString).AsObject();
                var detailsArray = new JsonArray();
                if (details != null)
                {
                    for (int i = 0; i < details.Count(); i++)
                    {
                        var json = JsonSerializer.Serialize(details[i]);
                        JsonObject detail = JsonNode.Parse(json).AsObject();
                        detailsArray.Add(detail);
                    }
                }
                jsonObject.Add("details", detailsArray);
                return jsonObject;
            }
            catch (ArgumentException ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public JsonObject InsertInvoice(InvoiceForm invoiceForm)
        {
            try
            {
                INVOICE invoice = CreateInvoice(invoiceForm);
                bool isExistInvoiceNo = invoice.ValidateNo(invoiceForm.invno, (long)invoiceForm.compid);
                if (isExistInvoiceNo) throw new ArgumentException("Invoice number exists");
                bool isExistControlNo = invoice.ValidateControl(invoice.Control_No);
                if (isExistControlNo) throw new ArgumentException("Control number exists");
                long invoiceSno = invoice.Addinvoi(invoice);
                UpdateControlNumber(invoice,invoiceSno);
                InsertInvoiceDetails(invoiceForm.details, invoiceSno);
                return FindInvoice((long)invoiceForm.compid, invoiceSno);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public JsonObject UpdateInvoice(InvoiceForm invoiceForm)
        {
            try
            {
                INVOICE invoice = CreateEditInvoice(invoiceForm);
                if (invoice == null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
                invoice.UpdateInvoiMas(invoice);
                invoice.DeleteInvoicedet(invoice);
                InsertInvoiceDetails(invoiceForm.details, invoice.Inv_Mas_Sno);
                return FindInvoice((long)invoiceForm.compid, invoice.Inv_Mas_Sno);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public JsonObject ApproveInvoice(InvoiceForm invoiceForm)
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
            catch (ArgumentException ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public JsonObject AmendInvoice(AddAmendForm addAmendForm)
        {
            try
            {
                INVOICE invoice = CreateAmendInvoice(addAmendForm);
                if (addAmendForm.sno <= 0) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                InvoicePDfData invoicePdfData = invoice.GetINVOICEpdf(addAmendForm.sno);
                if (invoicePdfData != null)
                {
                    AddInvoiceAmendment(addAmendForm, invoicePdfData);
                }
                invoice.Inv_Mas_Sno = addAmendForm.sno;
                invoice.UpdateInvoiMas(invoice);
                invoice.DeleteInvoicedet(invoice);
                InsertInvoiceDetails(addAmendForm.details, addAmendForm.sno);
                //CreateAmendInvoiceEmailContent(invoicePdfData.Cust_Sno, invoicePdfData,addAmendForm);
                return FindInvoice((long)addAmendForm.compid, addAmendForm.sno);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public JsonObject CancelInvoice(AddAmendForm addAmendForm)
        {
            try
            {
                INVOICE invoice = CreateCancelInvoice(addAmendForm);
                if (addAmendForm.sno <= 0) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                InvoicePDfData invoicePdfData = invoice.GetINVOICEpdf(addAmendForm.sno);
                if (invoicePdfData != null)
                {
                    AttemptCancelInvoice(addAmendForm, invoicePdfData);
                }
                invoice.Inv_Mas_Sno = addAmendForm.sno;
                invoice.approval_status = "Cancel";
                invoice.UpdateStatus(invoice);
                invoice.DeleteInvoicedet(invoice);
                InsertInvoiceDetails(addAmendForm.details, addAmendForm.sno);
                //CreateCancelInvoiceEmailContent(invoicePdfData.Cust_Sno, invoicePdfData, addAmendForm);
                return FindInvoice((long)addAmendForm.compid, addAmendForm.sno);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.ToString());
            }
        }
        public JsonObject GetBriefPaymentDetail(SingletonControl singletonControl)
        {
            try
            {
                decimal paymentAmount = 0;
                decimal balance = 0;
                InvoicePDfData invoicePDfData = new INVOICE().GetControl_A(singletonControl.control);
                if (invoicePDfData == null) throw new ArgumentException("Failed to get control number details");
                List<Payment> paymentDetail = new Payment().GetPayment_Paid(singletonControl.control);
                if (paymentDetail != null)
                {
                    paymentAmount = paymentDetail.Sum(a => a.Amount);
                }
                balance = invoicePDfData.Item_Total_Amount - paymentAmount;
                JsonObject response = new JsonObject
                {
                    { "Control_No", invoicePDfData.Control_No },
                    { "Cust_Name", invoicePDfData.Cust_Name },
                    { "Payment_Type", invoicePDfData.Payment_Type },
                    { "Item_Total_Amount", invoicePDfData.Item_Total_Amount },
                    { "Balance", balance }
                };
                return response;
            }
            catch (ArgumentException ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }


        public List<InvoiceC> GetAmendmentReports(CancelRepModel cancelRepModel)
        {
            try
            {
                InvoiceC invoiceC = new InvoiceC();
                var results = invoiceC.GetAmendRep((long)cancelRepModel.compid, cancelRepModel.invno, cancelRepModel.stdate, cancelRepModel.enddate, (long) cancelRepModel.cust);
                return results != null ? results : new List<InvoiceC>();
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public List<InvoiceC> GetAmendmentReports(InvoiceReportDetailsForm invoiceReportDetailsForm)
        {
            try
            {
                var results = new InvoiceC().GetAmendRep(invoiceReportDetailsForm.companyIds, invoiceReportDetailsForm.customerIds, invoiceReportDetailsForm.invoiceIds, invoiceReportDetailsForm.stdate, invoiceReportDetailsForm.enddate);
                return results != null ? results : new List<InvoiceC>();
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public List<InvoiceC> GetCancelledInvoicesReport(CancelRepModel cancelRepModel)
        {
            try
            {
                InvoiceC invoiceC = new InvoiceC();
                var results = invoiceC.GetCancelRep((long)cancelRepModel.compid, cancelRepModel.invno, cancelRepModel.stdate, cancelRepModel.enddate, (long) cancelRepModel.cust);
                return results != null ? results : new List<InvoiceC>();
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public List<InvoiceC> GetCancelledInvoicesReport(InvoiceReportDetailsForm invoiceReportDetailsForm)
        {
            try
            {
                var results = new InvoiceC().GetCancelRep(invoiceReportDetailsForm.companyIds, invoiceReportDetailsForm.customerIds, invoiceReportDetailsForm.invoiceIds, invoiceReportDetailsForm.stdate, invoiceReportDetailsForm.enddate);
                return results != null ? results : new List<InvoiceC>();
            }
            catch (ArgumentException ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            } 
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public List<INVOICE> GetchDetails(SingletonComp singletonComp)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var results = invoice.GetINVOICEMas((long) singletonComp.compid).Where(x => x.approval_status != "2" && x.approval_status != "Cancel");
                return results != null ? results.ToList() : new List<INVOICE>();
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public List<INVOICE> GetchDetails_A(SingletonComp singletonComp)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetINVOICEMasE((long)singletonComp.compid);
                return result != null ? result : new List<INVOICE>();
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<INVOICE> GetchDetails_P(SingletonComp singletonComp)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetINVOICEMas_D((long) singletonComp.compid);
                return result != null ? result : new List<INVOICE>();
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<INVOICE> GetchDetails_Pen(SingletonComp singletonComp)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetINVOICEMas_Pen((long) singletonComp.compid);
                return result != null ? result : new List<INVOICE>();
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<INVOICE> GetchDetails_Lat(SingletonComp singletonComp)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetINVOICEMas_Lat((long) singletonComp.compid);
                return result != null ? result : new List<INVOICE>();
            }
            catch (Exception ex)
            {

                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }



        public List<InvoiceC> GetConsolidatedReports(ReportDates report)
        {
            try
            {
                InvoiceC invoiceC = new InvoiceC();
                var results = invoiceC.InvoiceConsolidatedReport(report.stdate, report.enddate);
                return results != null ? results : new List<InvoiceC>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public List<InvoiceC> GetPaymentConsolidatedReports(ReportDates report)
        {
            try
            {
                InvoiceC invoiceC = new InvoiceC();
                var results = invoiceC.PaymentConsolidatedReport(report.stdate, report.enddate);
                return results ?? new List<InvoiceC>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);

            }
        }


        public List<Payment> GetPaymentTransactReports(TransactBankModel transact)
        {
            try
            {
                Payment payment = new Payment();
                var compid = transact.Compid.ToString() == "all" ? "0" : transact.Compid;
                var branch = compid.Equals("0") ? 0 :companyBankService.GetCompanyDetail(long.Parse(compid)).Branch_Sno;
                var cusid = transact.cusid.ToLower() == "all" ? "0" : transact.cusid;
                var results = payment.GetTransactionsReport(long.Parse(compid), transact.stdate, transact.enddate, long.Parse(cusid), (long)branch);
                return results ?? results ;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public List<Payment> GetPaymentTransactReports(InvoiceDetailsForm invoiceDetailsForm)
        {
            try
            {
                List<Payment> payments = new Payment().GetTransactionsReport(invoiceDetailsForm.companyIds, invoiceDetailsForm.customerIds, invoiceDetailsForm.stdate, invoiceDetailsForm.enddate);
                return payments ?? new List<Payment>();
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }


        public List<Payment> GetPaymentTransactInvoiceDetailsReports(TransactInvoiceNo transact)
        {
            try
            {
                Payment payment = new Payment();

                var results = payment.GetTransactionsinvoiceDetailsReport(transact.invoice_sno);
                return results ?? results;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        
        //Add delivery code and Confirm Del
        public JsonObject MarkInvoiceDelivery(long sno,long userid)
        {
            try
            {
                INVOICE getinvoicedata = new INVOICE().GetInvoiceCDetails(sno);
                if (getinvoicedata == null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
                INVOICE invoice = new INVOICE();
                var otp = Services.OTP.GenerateOTP(6);
                invoice.Inv_Mas_Sno = getinvoicedata.Inv_Mas_Sno;
                invoice.AuditBy = userid.ToString();
                invoice.delivery_status = "Pending";
                invoice.grand_count = (int?)Int64.Parse(otp);
                invoice.UpdateInvoiceDeliveryCode(invoice);
                SmsService sms = new SmsService();
                sms.SendCustomerDeliveryCode(getinvoicedata.Mobile, otp);
                if (!string.IsNullOrEmpty(getinvoicedata.Email)) { 
                    EmailUtils.SendCustomerDeliveryCodeEmail(getinvoicedata.Email, otp, getinvoicedata.Mobile); 
                }
                return FindInvoice(getinvoicedata.Com_Mas_Sno, getinvoicedata.Inv_Mas_Sno);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        


    }
}
