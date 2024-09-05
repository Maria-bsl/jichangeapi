using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;
using JichangeApi.Services.setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers.setup
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SuspenseAController : SetupBaseController
    {
        private readonly SuspenseAService suspenseAService = new SuspenseAService();
        Payment pay = new Payment();
        [HttpPost]
        public HttpResponseMessage GetAccount()
        {
            try
            {
                var results = suspenseAService.GetAccounts();
                return GetSuccessResponse(results);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetAccount_Active()
        {
            S_Account suspenseAccount = new S_Account();
            try
            {
                var results = suspenseAService.GetActiveAccounts();
                return GetSuccessResponse(results);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        /*private S_Account CreateSuspenseAccount(AddSuspenseAccountForm addSuspenseAccountForm)
        {
            S_Account suspenseAccount = new S_Account();
            suspenseAccount.Sus_Acc_No = addSuspenseAccountForm.account;
            suspenseAccount.Status = addSuspenseAccountForm.status;
            suspenseAccount.AuditBy = addSuspenseAccountForm.userid.ToString();
            suspenseAccount.Sus_Acc_Sno = (long)addSuspenseAccountForm.sno;
            return suspenseAccount;
        }*/

        /*private HttpResponseMessage InsertSuspenseAccount(S_Account suspenseAccount,AddSuspenseAccountForm addSuspenseAccountForm)
        {
            try
            {
                bool isExist = suspenseAccount.ValidateAccount(suspenseAccount.Sus_Acc_No.ToLower());
                if (isExist) return this.GetAlreadyExistsErrorResponse();
                long addedSuspenseAccount = suspenseAccount.AddAccount(suspenseAccount);
                return FindSuspenseAccount(addedSuspenseAccount);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private HttpResponseMessage UpdateSuspenseAccount(S_Account suspenseAccount,AddSuspenseAccountForm addSuspenseAccountForm)
        {
            try
            {
                bool isExist = suspenseAccount.isExistSuspenseAccount((long) addSuspenseAccountForm.sno);
                if (!isExist) return this.GetNotFoundResponse();
                bool isDuplicate = suspenseAccount.isDuplicateAccountNumber(addSuspenseAccountForm.account, (long)addSuspenseAccountForm.sno);
                if (isDuplicate) { return this.GetAlreadyExistsErrorResponse(); }
                long updatedSno = suspenseAccount.UpdateAccount(suspenseAccount);
                return FindSuspenseAccount(updatedSno);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }*/

        [HttpPost]
        public HttpResponseMessage AddAccount(AddSuspenseAccountForm addSuspenseAccountForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                //S_Account suspenseAccount = CreateSuspenseAccount(addSuspenseAccountForm);
                if ((long) addSuspenseAccountForm.sno == 0) { 
                    var result = suspenseAService.InsertSuspenseAccount(addSuspenseAccountForm);
                    return GetSuccessResponse(result);
                }
                else {
                    var result = suspenseAService.UpdateSuspenseAccount(addSuspenseAccountForm);
                    return GetSuccessResponse(result);
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

        [HttpGet]
        public HttpResponseMessage FindSuspenseAccount(long sno)
        {
            try
            {
                var result = suspenseAService.FindSuspenseAccount(sno);
                return GetSuccessResponse(result);
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
        public HttpResponseMessage DeleteAccount(DeleteSuspenseAccountForm deleteSuspenseAccountForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                var result = suspenseAService.DeleteSuspenseAccount((long)deleteSuspenseAccountForm.sno, (long)deleteSuspenseAccountForm.userid);
                return GetSuccessResponse(result);
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
    }
}