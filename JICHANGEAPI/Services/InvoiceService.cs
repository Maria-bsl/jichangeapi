using BL.BIZINVOICING.BusinessEntities.Common;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Controllers.smsservices;
using JichangeApi.Models;
using JichangeApi.Models.form;
using JichangeApi.Services.Companies;
using JichangeApi.Utilities;
using Newtonsoft.Json.Linq;
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
        private static readonly string TABLE_NAME = "Invoice_Master";
        private static readonly List<string> TABLE_COLUMNS = new List<string> { "inv_mas_sno", "invoice_no", "invoice_date",
            "comp_mas_sno", "cust_mas_sno", "currency_code", "total_without_vat", "vat_amount", "total_amount", "inv_remarks",
            "posted_by", "posted_date", "warrenty", "goods_status", "delivery_status", "grand_count", "daily_count", "approval_status",
            "approval_date", "customer_id_type", "customer_id_no", "p_date", "control_no", "payment_type", "due_date", "invoice_expired" };

        private static readonly string INVOICE_DETAILS = "Invoice_Details";
        private static readonly List<string> INVOICE_DETAILS_COLUMNS = new List<string> { "inv_det_sno", "inv_mas_sno",
            "item_description", "item_qty", "item_unit_price", "item_total_amount", "vat_percentage", "vat_amount", "item_without_vat",
            "remarks", "vat_category", "vat_type" };

        private static readonly string INVOICE_CANCELLATION = "Invoice_Cancellation";
        private static readonly List<string> INVOICE_CANCELLATION_COLUMNS = new List<string> { "inv_can_sno", "control_no", "comp_mas_sno",
            "cust_mas_sno", "inv_mas_sno", "invoice_amount", "reason_for_cancel", "posted_by", "posted_date" };

        private static readonly string INVOICE_AMMENDMENT = "Invoice_Ammendment";
        private static readonly List<string> INVOICE_AMMENDMENT_COLUMNS = new List<string> { "inv_amm_sno", "control_no", "comp_mas_sno",
            "cust_mas_sno", "inv_mas_sno", "invoice_amount", "amment_amount", "payment_type", "reason_for_amm", "due_date", "expiry_date", 
            "posted_by", "posted_date" };



        private CompanyBankService companyBankService = new CompanyBankService();
        readonly Payment pay = new Payment();

        private void AppendInsertInvoiceAmmendmentsAuditTrail(long sno, InvoiceC invoice, long userid, long compid)
        {
            var values = new List<string> { sno.ToString(), invoice?.Control_No, invoice.Com_Mas_Sno.ToString(), invoice?.Cust_Mas_No.ToString(),
                invoice?.Inv_Mas_Sno.ToString(), invoice?.invoice_amount.ToString(), invoice?.Amment_Amount.ToString(), invoice?.Payment_Type, 
                invoice?.Reason, invoice?.Due_Date.ToString(), invoice?.Invoice_Expired_Date.ToString(), invoice?.AuditBy, DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, InvoiceService.INVOICE_AMMENDMENT, InvoiceService.INVOICE_AMMENDMENT_COLUMNS, compid);
        }


        private void AppendInsertInvoiceCancellationAuditTrail(long sno, InvoiceC invoice, long userid, long compid)
        {
            var values = new List<string> { sno.ToString(), invoice?.Control_No, invoice?.Com_Mas_Sno.ToString(), invoice?.Chus_Mas_No.ToString(),
                invoice?.Inv_Mas_Sno.ToString(), invoice?.Total.ToString(), invoice?.Reason, invoice?.AuditBy, DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, InvoiceService.INVOICE_CANCELLATION, InvoiceService.INVOICE_CANCELLATION_COLUMNS, compid);
        }

        private void AppendInsertInvoiceDetailsAuditTrail(long sno,INVOICE invoice,long userid,long compid)
        {
            var values = new List<string> { sno.ToString(), invoice?.Inv_Mas_Sno.ToString(), invoice?.Item_Description, 
                invoice?.Item_Qty.ToString(), invoice?.Item_Unit_Price.ToString(), invoice?.Total.ToString(), invoice?.Vat_Percentage.ToString(), 
                invoice?.Vat_Amount.ToString(), invoice?.Item_Without_vat.ToString(), invoice?.Remarks, invoice?.vat_category, invoice?.Vat_Type };
            Auditlog.InsertAuditTrail(values, userid, InvoiceService.INVOICE_DETAILS, InvoiceService.INVOICE_DETAILS_COLUMNS, compid);
        }

        private void AppendInvoiceDetailsDeleteAuditTrail(long sno, INVOICE invoice, long userid, long compid)
        {
            var values = new List<string> { sno.ToString(), invoice.Inv_Mas_Sno.ToString(), invoice?.Item_Description, invoice?.Item_Qty.ToString(),
                invoice?.Item_Unit_Price.ToString(), invoice?.Total.ToString(), invoice?.Vat_Percentage.ToString(), invoice?.Vat_Amount.ToString(),
                invoice?.Item_Without_vat.ToString(), invoice?.Remarks, invoice?.vat_category, invoice?.Vat_Type };
            Auditlog.deleteAuditTrail(values, userid, InvoiceService.INVOICE_DETAILS, InvoiceService.INVOICE_DETAILS_COLUMNS, compid);
        }

        private void AppendInsertAuditTrail(long sno, INVOICE invoice, long userid)
        {
            var values = new List<string> { sno.ToString(), invoice?.Invoice_No,invoice?.Invoice_Date.ToString(),
                invoice?.Com_Mas_Sno.ToString(), invoice?.Chus_Mas_No.ToString(), invoice?.Currency_Code, invoice?.Total_Without_Vt.ToString(),
                invoice?.Vat_Amount.ToString(), invoice?.Total.ToString(), invoice?.Inv_Remarks, userid.ToString(), DateTime.Now.ToString(), 
                invoice?.warrenty, invoice?.goods_status, invoice?.delivery_status, invoice.grand_count?.ToString(), invoice?.daily_count.ToString(), 
                invoice?.approval_status, invoice?.approval_date.ToString(), invoice?.Customer_ID_Type, invoice?.Customer_ID_No, DateTime.Now.ToString(),
                invoice?.Control_No, invoice?.Payment_Type, invoice?.Due_Date.ToString(), invoice?.Invoice_Expired_Date.ToString()};
            Auditlog.InsertAuditTrail(values, userid, InvoiceService.TABLE_NAME, InvoiceService.TABLE_COLUMNS,invoice.Com_Mas_Sno);
        }

        private void AppendUpdateAuditTrail(long sno, INVOICE oldInvoice, INVOICE newInvoice, long userid)
        {
            var oldValues = new List<string> { sno.ToString(), oldInvoice?.Invoice_No,oldInvoice?.Invoice_Date.ToString(),
                oldInvoice?.Com_Mas_Sno.ToString(), oldInvoice?.Chus_Mas_No.ToString(), oldInvoice?.Currency_Code, oldInvoice?.Total_Without_Vt.ToString(),
                oldInvoice?.Vat_Amount.ToString(), oldInvoice?.Total.ToString(), oldInvoice?.Inv_Remarks, userid.ToString(), DateTime.Now.ToString(),
                oldInvoice?.warrenty, oldInvoice?.goods_status, oldInvoice?.delivery_status, oldInvoice.grand_count?.ToString(), oldInvoice?.daily_count.ToString(),
                oldInvoice?.approval_status, oldInvoice?.approval_date.ToString(), oldInvoice?.Customer_ID_Type, oldInvoice?.Customer_ID_No, DateTime.Now.ToString(),
                oldInvoice?.Control_No, oldInvoice?.Payment_Type, oldInvoice?.Due_Date.ToString(), oldInvoice?.Invoice_Expired_Date.ToString()};

            var newValues = new List<string> { sno.ToString(), newInvoice?.Invoice_No,newInvoice?.Invoice_Date.ToString(),
                newInvoice?.Com_Mas_Sno.ToString(), newInvoice?.Chus_Mas_No.ToString(), newInvoice?.Currency_Code, newInvoice?.Total_Without_Vt.ToString(),
                newInvoice?.Vat_Amount.ToString(), newInvoice?.Total.ToString(), newInvoice?.Inv_Remarks, userid.ToString(), DateTime.Now.ToString(),
                newInvoice?.warrenty, newInvoice?.goods_status, newInvoice?.delivery_status, newInvoice.grand_count?.ToString(), newInvoice?.daily_count.ToString(),
                newInvoice?.approval_status, newInvoice?.approval_date.ToString(), newInvoice?.Customer_ID_Type, newInvoice?.Customer_ID_No, DateTime.Now.ToString(),
                newInvoice?.Control_No, newInvoice?.Payment_Type, newInvoice?.Due_Date.ToString(), newInvoice?.Invoice_Expired_Date.ToString()};

            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, InvoiceService.TABLE_NAME, InvoiceService.TABLE_COLUMNS, oldInvoice.Com_Mas_Sno);
        }

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
            //INVOICE invoice = new INVOICE();
            INVOICE invoice = new INVOICE().GetINVOICEMas1((long)invoiceForm.compid, (long)invoiceForm.sno);
            if (invoice == null) return null;
            invoice.Due_Date = DateTime.Parse(invoiceForm.edate);
            invoice.Invoice_Expired_Date = DateTime.Parse(invoiceForm.iedate);
            invoice.Payment_Type = invoiceForm.ptype;
            invoice.Currency_Code = invoiceForm.ccode;
            invoice.Total = Decimal.Parse(invoiceForm.total);
            invoice.Inv_Remarks = invoiceForm.Inv_remark;
            invoice.AuditBy = invoiceForm.userid.ToString();
            return invoice;
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
        private void InsertInvoiceDetails(List<INVOICE> details, long invoiceSno,long userid,long compid)
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
                    long invoiceDetailSno = invoice.AddInvoiceDetails(invoice);
                    AppendInsertInvoiceDetailsAuditTrail(invoiceDetailSno, invoice, userid,compid);
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
            long ammendedSno = invoiceC.AddAmmend(invoiceC);
            AppendInsertInvoiceAmmendmentsAuditTrail(ammendedSno, invoiceC, (long)addAmendForm.userid, (long)addAmendForm.compid);
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
            long cancelledSno = invoiceC.AddCancel(invoiceC);
            AppendInsertInvoiceCancellationAuditTrail(cancelledSno, invoiceC, (long)addAmendForm.userid, (long)addAmendForm.compid);
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

                CustomerMaster customer = new CustomerMaster();
                var customerdetails = customer.CustGetId( invoice.Com_Mas_Sno, invoice.Chus_Mas_No);
                var total = invoice.Total.ToString("N2") + " /= " + invoice.Currency_Code;
                // Send Approved Invoice to Customer EMAIL & SMS
                if (customerdetails.Phone != null)
                {
                    SmsService smsService = new SmsService();
                    try {
                    //if (customerdetails.Phone != null)
                        smsService.SendCustomerInvoiceSMS(customerdetails.Cust_Name, invoice.Invoice_No, invoice.Control_No, customerdetails.Company_Name, total.ToString(), customerdetails.Phone);
                    }
                    catch (Exception ex)
                    {
                        pay.Message = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                }
                if (customerdetails.Email != null && !customerdetails.Email.Equals("")) 
                {
                    try { 
                    
                        EmailUtils.SendCustomerNewInvoiceEmail(customerdetails.Email, customerdetails.Cust_Name, invoice.Invoice_No, invoice.Control_No, customerdetails.Company_Name, total.ToString());
                    }
                    catch (Exception ex)
                    {
                        pay.Message = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                }
            
            }
        }
        public List<INVOICE> GetSignedDetails(SingletonComp singletonComp)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var invoices = invoice.GetINVOICEMas((long)singletonComp.compid).Where(x => x.approval_status == "2");
                return invoices != null ? invoices.ToList() : new List<INVOICE>(); ;
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
                var inv = invoice;
                AppendInsertAuditTrail(invoiceSno, invoice, (long) invoiceForm.userid);
                UpdateControlNumber(invoice,invoiceSno);
                AppendUpdateAuditTrail(invoiceSno,inv,invoice, (long)invoiceForm.userid);
                InsertInvoiceDetails(invoiceForm.details, invoiceSno, (long) invoiceForm.userid, (long) invoiceForm.compid);

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
                INVOICE found = new INVOICE().GetINVOICEMas1((long) invoiceForm.compid, (long) invoiceForm.sno);
                if (found == null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
                INVOICE invoice = CreateEditInvoice(invoiceForm);
                invoice.UpdateInvoiMas(invoice);
                AppendUpdateAuditTrail(found.Inv_Mas_Sno, found, invoice, (long)invoiceForm.userid);
                List<INVOICE> details = invoice.GetInvoiceDetails(found.Inv_Mas_Sno);
                if (details != null && details.Count > 0)
                {
                    details.ForEach(detail => AppendInvoiceDetailsDeleteAuditTrail(found.Inv_Mas_Sno,detail,(long) invoiceForm.userid,found.Com_Mas_Sno));
                    invoice.DeleteInvoicedet(invoice);
                }
                InsertInvoiceDetails(invoiceForm.details, invoice.Inv_Mas_Sno, (long) invoiceForm.userid, (long) invoiceForm.compid);

                var customerdetails = new CustomerMaster().CustGetId(invoice.Com_Mas_Sno, invoice.Chus_Mas_No);
                var total = invoice.Total.ToString("N2") + " /= " + invoice.Currency_Code;
                // Send Updated Invoice to Customer EMAIL & SMS
                if (customerdetails.Phone != null)
                {
                    try { 
                    SmsService smsService = new SmsService();
                    smsService.SendCustomerInvoiceSMS(customerdetails.Cust_Name, invoice.Invoice_No, invoice.Control_No, customerdetails.Company_Name, total.ToString(), customerdetails.Phone);
                    }
                    catch (Exception ex)
                    {
                        pay.Message = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                }
                if (customerdetails.Email != null && !customerdetails.Email.Equals(""))
                {
                    try { 
                    EmailUtils.SendCustomerNewInvoiceEmail(customerdetails.Email, customerdetails.Cust_Name, invoice.Invoice_No, invoice.Control_No, customerdetails.Company_Name, total.ToString());
                    }
                    catch (Exception ex)
                    {
                        pay.Message = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                }


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
                INVOICE found = new INVOICE().GetINVOICEMas1((long)invoiceForm.compid, (long)invoiceForm.sno);
                if (found == null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
                INVOICE invoice = CreateInvoice(invoiceForm);
                invoice.Inv_Mas_Sno = invoiceForm.sno;
                invoice.goods_status = "Approved";
                string controlNumber = invoiceForm.sno.ToString().PadLeft(8, '0');
                invoice.Control_No = "T" + controlNumber;
                //invoice.UpdateInvoiMasForTRA1(invoice);
                invoice.approval_status = "2";
                invoice.approval_date = System.DateTime.Now;
                invoice.UpdateInvoice(invoice);
                AppendUpdateAuditTrail(found.Inv_Mas_Sno, found, invoice, (long)invoiceForm.userid);

                CustomerMaster customer = new CustomerMaster();
                var customerdetails = customer.CustGetId(invoice.Com_Mas_Sno, invoice.Chus_Mas_No );
                var total = invoice.Total.ToString("N2") + " /= " + invoice.Currency_Code;
                // Send Approved Invoice to Customer EMAIL & SMS
                if (customerdetails.Phone != null)
                {
                    try { 
                    SmsService smsService = new SmsService();
                    smsService.SendCustomerInvoiceSMS(customerdetails.Cust_Name, invoice.Invoice_No, invoice.Control_No, customerdetails.Company_Name, total.ToString(), customerdetails.Phone);
                    }
                    catch (Exception ex)
                    {
                        pay.Message = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                }
                if (customerdetails.Email != null && !customerdetails.Email.Equals(""))
                {
                    try { 
                    EmailUtils.SendCustomerNewInvoiceEmail(customerdetails.Email, customerdetails.Cust_Name, invoice.Invoice_No, invoice.Control_No, customerdetails.Company_Name, total.ToString());
                    }
                    catch (Exception ex)
                    {
                        pay.Message = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                }

    
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
                INVOICE found = new INVOICE().GetINVOICEMas1((long)addAmendForm.compid, (long)addAmendForm.sno);
                if (found == null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
                INVOICE invoice = CreateAmendInvoice(addAmendForm);
                if (addAmendForm.sno <= 0) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                InvoicePDfData invoicePdfData = invoice.GetINVOICEpdf(addAmendForm.sno);
                if (invoicePdfData != null)
                {
                    AddInvoiceAmendment(addAmendForm, invoicePdfData);
                }
                invoice.Inv_Mas_Sno = addAmendForm.sno;
                invoice.UpdateInvoiMas(invoice);
                AppendUpdateAuditTrail(addAmendForm.sno,found,invoice,(long) addAmendForm.userid);
                List<INVOICE> details = invoice.GetInvoiceDetails(found.Inv_Mas_Sno);
                if (details != null && details.Count > 0)
                {
                    details.ForEach(detail => AppendInvoiceDetailsDeleteAuditTrail(found.Inv_Mas_Sno, detail, (long)addAmendForm.userid, found.Com_Mas_Sno));
                    invoice.DeleteInvoicedet(invoice);
                }
                InsertInvoiceDetails(addAmendForm.details, addAmendForm.sno, (long) addAmendForm.userid, (long) addAmendForm.compid);

                
                CustomerMaster customer = new CustomerMaster();
                var customerdetails = customer.CustGetId(invoice.Com_Mas_Sno, invoice.Chus_Mas_No);
                var total = invoice.Total.ToString("N2") + " /= " + invoice.Currency_Code;
                // Send Amended Invoice to Customer EMAIL & SMS
                if (customerdetails.Phone != null)
                {
                    try { 
                        SmsService smsService = new SmsService();
                        smsService.SendCustomerInvoiceAmmendedSMS(customerdetails.Phone, customerdetails.Cust_Name, total.ToString(), invoice.Invoice_No, invoicePdfData.Control_No, customerdetails.Company_Name );
                    }catch(Exception ex)
                    {
                        pay.Message = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                }
                if (customerdetails.Email != null)
                {
                    try { 
                        EmailUtils.SendCustomerAmmendedInvoiceEmail(customerdetails.Email, customerdetails.Cust_Name, invoice.Invoice_No, invoicePdfData.Control_No, customerdetails.Company_Name, total.ToString());
                    }catch (Exception ex)
                    {
                        pay.Message = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                }

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
                INVOICE found = new INVOICE().GetINVOICEMas1((long)addAmendForm.compid, (long)addAmendForm.sno);
                if (found == null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
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
                AppendUpdateAuditTrail(invoice.Inv_Mas_Sno, found, invoice, (long)addAmendForm.userid);
                List<INVOICE> details = invoice.GetInvoiceDetails(found.Inv_Mas_Sno);
                if (details != null && details.Count > 0)
                {
                    details.ForEach(detail => AppendInvoiceDetailsDeleteAuditTrail(found.Inv_Mas_Sno, detail, (long)addAmendForm.userid, found.Com_Mas_Sno));
                    invoice.DeleteInvoicedet(invoice);
                }
                InsertInvoiceDetails(addAmendForm.details, addAmendForm.sno, (long) addAmendForm.userid, (long)addAmendForm.compid);
                


                CustomerMaster customer = new CustomerMaster();
                var customerdetails = customer.CustGetId(invoice.Com_Mas_Sno, invoice.Chus_Mas_No);
                var total = invoice.Total.ToString("N2") + " /= " + invoice.Currency_Code;
                // Send Cancelled Invoice to Customer EMAIL & SMS
                if (customerdetails.Phone != null)
                {
                    SmsService smsService = new SmsService();
                    try { 
                    smsService.SendCustomerCancelInvoiceSMS( customerdetails.Phone, customerdetails.Cust_Name, invoice.Invoice_No, invoicePdfData.Control_No, customerdetails.Company_Name);
                    }
                    catch (Exception ex)
                    {
                        pay.Message = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                }
                if (customerdetails.Email != null && !customerdetails.Email.Equals(""))
                {
                    try
                    {
                        EmailUtils.SendCustomerCancelledInvoiceEmail(customerdetails.Email, customerdetails.Cust_Name, invoice.Invoice_No, invoicePdfData.Control_No, customerdetails.Company_Name);
                    }
                    catch (Exception ex)
                    {
                        pay.Message = ex.ToString();
                        pay.AddErrorLogs(pay);
                    }
                }
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
                    { "Balance", balance },
                    { "Currency_Code",invoicePDfData.Currency_Code },
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
                return results.ToList(); //results != null ? results.ToList() : new List<INVOICE>();
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
                INVOICE found = new INVOICE().GetINVOICEMas1(getinvoicedata.Com_Mas_Sno, getinvoicedata.Inv_Mas_Sno);
                AppendUpdateAuditTrail(invoice.Inv_Mas_Sno, found, invoice, userid);
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
        
        public bool IsExistInvoice(long compid,string invno)
        {
            try
            {
                bool exists = new INVOICE().ValidateNo(invno, compid);
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

    }
}
