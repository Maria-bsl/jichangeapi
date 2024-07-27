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
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
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
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
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
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
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
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
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
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
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
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                //Utilites.logfile("GetSignedInvoiceById", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
                return GetServerErrorResponse(Ex.Message);
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
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                //Utilites.logfile("GetInvoiceDetailsbyid", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
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
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                //Utilites.logfile("GetSignedInvoiceById", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
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
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                //Utilites.logfile("GetSignedInvoiceById", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
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

                Utilites.logfile("GetCurrency", "0", Ex.ToString());
                //pay.Error_Text = Ex.ToString();
                //pay.AddErrorLogs(pay);
                return GetServerErrorResponse(Ex.Message);
            }

            //return null;
        }
        #endregion



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
                    return GetSuccessResponse(invoice);
                }
                else if (invoiceForm.sno > 0 && !string.IsNullOrEmpty(invoiceForm.goods_status) && invoiceForm.goods_status.ToLower().Equals("approve"))
                {
                    var invoice = invoiceService.ApproveInvoice(invoiceForm);
                    return GetSuccessResponse(invoice);
                }
                else
                {
                    var invoice = invoiceService.UpdateInvoice(invoiceForm);
                    return GetSuccessResponse(invoice);
                }
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
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
                return GetSuccessResponse(amendedInvoice);
            }
            catch(ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
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
                return GetSuccessResponse(cancelledInvoice);
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
        }
        [HttpPost]
        public HttpResponseMessage GetControl(SingletonControl singletonControl)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                var details = invoiceService.GetBriefPaymentDetail(singletonControl);
                return GetSuccessResponse(details);
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
        }
        [HttpPost]
        public HttpResponseMessage GetAmendReport(CancelRepModel cancelRepModel)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<InvoiceC> invoiceCs = invoiceService.GetAmendmentReports(cancelRepModel);
                return GetSuccessResponse(invoiceCs);
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
        }

         [HttpPost]
        public HttpResponseMessage GetPaymentReport(CancelRepModel cancelRepModel)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<Payment> payments = paymentService.GetPaymentReport(cancelRepModel);
                return GetSuccessResponse(payments);    
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
        }
         [HttpPost]
         public HttpResponseMessage GetCancelReport(CancelRepModel cancelRepModel)
         {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<InvoiceC> payments = invoiceService.GetCancelledInvoicesReport(cancelRepModel);
                return GetSuccessResponse(payments);

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
        }
         #endregion
        #endregion

    }

}