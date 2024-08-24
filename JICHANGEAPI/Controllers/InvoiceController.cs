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
using JichangeApi.Services;
using JichangeApi.Services.setup;
using JichangeApi.Services.Companies;
using JichangeApi.Controllers.smsservices;
using JichangeApi.Utilities;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InvoiceController : SetupBaseController
    {
        #region Global Declrations
        private readonly InvoiceService invoiceService = new InvoiceService();
        private readonly PaymentService paymentService = new PaymentService();
        private readonly CompanyService companyService = new CompanyService();
        private readonly CompanyDepositService companyDepositService = new CompanyDepositService();
        private readonly CustomerService customerService = new CustomerService();
        private readonly CurrencyService currencyService = new CurrencyService();
        private readonly Payment pay = new Payment();



        #region Get Invoice Details
        [HttpPost]
        public HttpResponseMessage GetchDetails(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> invoices = invoiceService.GetchDetails(singletonComp);
                return GetSuccessResponse(invoices);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetName(string name)
        {
            return GetSuccessResponse(name);
        }

        [HttpPost]
        public HttpResponseMessage GetchDetails_A(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> invoices = invoiceService.GetchDetails_A(singletonComp);
                return GetSuccessResponse(invoices);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetchDetails_P(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> invoices = invoiceService.GetchDetails_P(singletonComp);
                return GetSuccessResponse(invoices);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetchDetails_Pen(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> invoices = invoiceService.GetchDetails_Pen(singletonComp);
                return GetSuccessResponse(invoices);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetchDetails_Lat(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> invoices = invoiceService.GetchDetails_Lat(singletonComp);
                return GetSuccessResponse(invoices);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }
        #endregion

        #region Get Signed Invoices
        [HttpPost]
        public HttpResponseMessage GetSignedDetails(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> invoices = invoiceService.GetSignedDetails(singletonComp);
                return GetSuccessResponse(invoices);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetSignedInvoiceById(SingletonCompInvid singletonCompInvid)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                INVOICE invoice = invoiceService.GetSignedInvoiceById(singletonCompInvid);
                return GetSuccessResponse(invoice);

            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                //Utilites.logfile("GetSignedInvoiceById", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage FindInvoice(long compid, long inv)
        {
            try
            {
                JsonObject invoice = invoiceService.FindInvoice(compid, inv);
                return SuccessJsonResponse(invoice);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        #endregion


        #region Get invoice by Id
        [HttpPost]
        public HttpResponseMessage GetInvoiceDetailsbyid(SingletonCompInvid singletonCompInvid)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                INVOICE invoice = invoiceService.GetSignedInvoiceById(singletonCompInvid);
                return GetSuccessResponse(invoice);

            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                //Utilites.logfile("GetInvoiceDetailsbyid", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetInvoiceInvoicedetails(SingletonInv singletonInv)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> invoice = invoiceService.GetInvoiceDetails(singletonInv);
                return GetSuccessResponse(invoice);
            }
            catch (Exception Ex)
            {
                // Catch Log here for exception thrown

                //Utilites.logfile("GetInvoicedetails", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }
        #endregion

        #region     Get Drodown Master Values

        [HttpPost]
        public HttpResponseMessage Getcompany()
        {
            try
            {
                List<Company> companies = companyService.GetCompanyList();
                return GetSuccessResponse(companies);
            }
            catch (Exception Ex)
            {
                //Utilites.logfile("GetCompany", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetcompanyS(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                Company company = companyService.GetCompanyById((long)singletonComp.compid);
                return GetSuccessResponse(company);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                //Utilites.logfile("GetSignedInvoiceById", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetMAccount(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                C_Deposit c_Deposit = companyDepositService.GetCompanyDepositAccount((long)singletonComp.compid);
                return GetSuccessResponse(c_Deposit);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                //Utilites.logfile("GetSignedInvoiceById", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetCustomersS(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<Customers> customers = customerService.GetCustomersS((long)singletonComp.compid);
                return GetSuccessResponse(customers);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetVatPer()
        {
            try
            {
                VatPercentage vatpercetage = new VatPercentage();
                var result = vatpercetage.GetVatPercentage();
                if (result == null) return GetSuccessResponse(new List<VatPercentage>());
                return GetSuccessResponse(result);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetCustomers()
        {
            try
            {
                var customers = customerService.GetCutomers();
                return GetSuccessResponse(customers);
            }
            catch (Exception Ex)
            {
                // Catch Log here for exception thrown
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                Utilites.logfile("GetCustomers", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
                return GetServerErrorResponse(Ex.Message);
            }

            //return null;
        }


        public HttpResponseMessage GetCurrency()
        {
            try
            {
                var currencies = currencyService.GetCurrenciesList();
                return GetSuccessResponse(currencies);
            }
            catch (Exception Ex)
            {
                // Catch Log here for exception thrown
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                Utilites.logfile("GetCurrency", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
                return GetServerErrorResponse(Ex.Message);
            }

            //return null;
        }
        #endregion


        [HttpPost]
        public HttpResponseMessage GetInvNo(SingletonInvComp singletonInvComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                bool exists = invoiceService.IsExistCompanyInvoiceNumber(singletonInvComp);
                return GetSuccessResponse(exists);
            }
            catch (Exception Ex)
            {
                // Catch Log here for exception thrown

                //Utilites.logfile("GetInvNo", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddInvoice(InvoiceForm invoiceForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                if (invoiceForm.sno == 0)
                {
                    var invoice = invoiceService.InsertInvoice(invoiceForm);
                    return SuccessJsonResponse(invoice);
                }
                else if (invoiceForm.sno > 0 && !string.IsNullOrEmpty(invoiceForm.goods_status) && invoiceForm.goods_status.ToLower().Equals("approved"))
                {
                    var invoice = invoiceService.ApproveInvoice(invoiceForm);
                    return SuccessJsonResponse(invoice);
                }
                else
                {
                    var invoice = invoiceService.UpdateInvoice(invoiceForm);
                    return SuccessJsonResponse(invoice);
                }
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        #region Save Update Invoice

        [HttpPost]
        public HttpResponseMessage AddAmend(AddAmendForm addAmendForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                var amendedInvoice = invoiceService.AmendInvoice(addAmendForm);
                return SuccessJsonResponse(amendedInvoice);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddCancel(AddAmendForm addAmendForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                var cancelledInvoice = invoiceService.CancelInvoice(addAmendForm);
                return SuccessJsonResponse(cancelledInvoice);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage GetControl(SingletonControl singletonControl)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                var details = invoiceService.GetBriefPaymentDetail(singletonControl);
                return SuccessJsonResponse(details);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetAmendReport(InvoiceReportDetailsForm invoiceReportDetailsForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<InvoiceC> invoiceCs = invoiceService.GetAmendmentReports(invoiceReportDetailsForm);
                return GetSuccessResponse(invoiceCs);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetPaymentReport(InvoiceReportDetailsForm invoiceReportDetailsForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<Payment> payments = paymentService.GetPaymentReport(invoiceReportDetailsForm);
                return GetSuccessResponse(payments);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetCancelReport(InvoiceReportDetailsForm invoiceReportDetailsForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<InvoiceC> invoices = invoiceService.GetCancelledInvoicesReport(invoiceReportDetailsForm);
                return GetSuccessResponse(invoices);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.Message);
            }
        }

        #endregion
        #endregion


        #region Consolidated Reports

        [HttpPost]
        public HttpResponseMessage GetConsoReport(ReportDates reportDates)
        {

            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<InvoiceC> invoiceConsolidated = invoiceService.GetConsolidatedReports(reportDates);
                return GetSuccessResponse(invoiceConsolidated);

            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.Message);
            }

        }


        [HttpPost]
        public HttpResponseMessage GetConsoPayment(ReportDates reportDates)
        {

            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<InvoiceC> payConsolidated = invoiceService.GetPaymentConsolidatedReports(reportDates);
                return GetSuccessResponse(payConsolidated);

            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.Message);
            }

        }


        /*[HttpPost]
        public HttpResponseMessage GetchTransact_B(TransactBankModel transact)
        {

            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<Payment> getTransaction = invoiceService.GetPaymentTransactReports(transact);
                return GetSuccessResponse(getTransaction);

            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }


        }*/

        [HttpPost]
        public HttpResponseMessage GetchTransact_B(InvoiceDetailsForm invoiceDetailsForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<Payment> getTransaction = invoiceService.GetPaymentTransactReports(invoiceDetailsForm);
                return GetSuccessResponse(getTransaction);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.Message);
            }
        }

        #endregion


        [HttpPost]
        public HttpResponseMessage GetchTransact_Inv(TransactInvoiceNo transact)
        {

            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<Payment> getTransactionInvoiceDetails = invoiceService.GetPaymentTransactInvoiceDetailsReports(transact);
                return GetSuccessResponse(getTransactionInvoiceDetails);

            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.Message);
            }

        }


        [HttpPost]
        public HttpResponseMessage AddDCode(SingletonAddCode singleton)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                /*List<Payment> getTransactionInvoiceDetails = invoiceService.AddDeliveryCode(singleton);
                return GetSuccessResponse(getTransactionInvoiceDetails);*/

                /*INVOICE getinvoicedata = new INVOICE().GetInvoiceCDetails((long)singleton.sno);
                INVOICE invoice = new INVOICE();

                if (getinvoicedata != null)
                {
                    var otp = Services.OTP.GenerateOTP(6);

                    invoice.Inv_Mas_Sno = getinvoicedata.Inv_Mas_Sno;
                    invoice.AuditBy = singleton.user_id.ToString();
                    invoice.delivery_status = "Pending";
                    invoice.grand_count = (int?)Int64.Parse(otp);
                    invoice.UpdateInvoiceDeliveryCode(invoice);
                    SmsService sms = new SmsService();
                    sms.SendCustomerDeliveryCode(getinvoicedata.Mobile, otp);
                    if(!string.IsNullOrEmpty(getinvoicedata.Email)) { EmailUtils.SendCustomerDeliveryCodeEmail(getinvoicedata.Email , otp, getinvoicedata.Mobile);}
                    
                    return GetSuccessResponse(invoice);

                }
                    return GetNoDataFoundResponse();*/
                var invoice = invoiceService.MarkInvoiceDelivery((long)singleton.sno, (long)singleton.user_id);
                return SuccessJsonResponse(invoice);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage ConfirmDel(SingletonDeliveryCode singleton)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                /*List<Payment> getTransactionInvoiceDetails = invoiceService.ConfirmDelivery(singleton);
                return GetSuccessResponse(getTransactionInvoiceDetails);*/

                INVOICE getinvoicedata = new INVOICE().GetInvoiceCodeDetails((long)singleton.code);
                INVOICE invoice = new INVOICE();

                if (getinvoicedata != null)
                {

                    invoice.Inv_Mas_Sno = getinvoicedata.Inv_Mas_Sno;
                    invoice.delivery_status = "Delivered";
                    invoice.UpdateInvoiceStatusDeliveryCode(invoice);


                    return GetSuccessResponse(invoice);

                }
                return GetNoDataFoundResponse();

            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage IsExistInvoice(long compid,string invno)
        {
            try
            {
                bool exists = invoiceService.IsExistInvoice(compid, invno);
                return GetSuccessResponse(exists);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);
                return GetServerErrorResponse(ex.Message);
            } 
        }
    }

}