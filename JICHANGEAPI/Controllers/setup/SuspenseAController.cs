using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;
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
    public class SuspenseAController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetAccount()
        {
            var suspenseAccount = new S_Account();
            try
            {
                var results = suspenseAccount.GetAccounts();
                if (results != null)
                {
                    return Request.CreateResponse(new { response = results, message = new List<string>() });
                }
                else
                {
                    return Request.CreateResponse(new { response = new List<BranchM>(), message = new List<string> { "Failed to retrieve branch list" } });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }

        [HttpPost]
        public HttpResponseMessage GetAccount_Active()
        {
            var suspenseAccount = new S_Account();
            try
            {
                var results = suspenseAccount.GetAccounts_Active();
                if (results != null)
                {
                    return Request.CreateResponse(new { response = results, message = new List<string>() });
                }
                else
                {
                    return Request.CreateResponse(new { response = new List<BranchM>(), message = new List<string> { "Failed to retrieve branch list" } });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }

        [HttpPost]
        public HttpResponseMessage AddAccount(AddSuspenseAccountForm addSuspenseAccountForm)
        {
            if (ModelState.IsValid)
            {
                var suspenseAccount = new S_Account();
                try
                {
                    suspenseAccount.Sus_Acc_No = addSuspenseAccountForm.account;
                    suspenseAccount.Status = addSuspenseAccountForm.status;
                    suspenseAccount.AuditBy = addSuspenseAccountForm.userid.ToString();
                    suspenseAccount.Sus_Acc_Sno = (long) addSuspenseAccountForm.sno;
                    if (addSuspenseAccountForm.sno == 0)
                    {
                        var isExist = suspenseAccount.ValidateAccount(suspenseAccount.Sus_Acc_No.ToLower());
                        if (isExist)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Already exists." } });
                        }
                        var addedSuspenseAccount = suspenseAccount.AddAccount(suspenseAccount);
                        return Request.CreateResponse(new { response = addedSuspenseAccount, message = new List<string>() });
                    }
                    else
                    {
                        var account = suspenseAccount.getAccount((long) addSuspenseAccountForm.sno);
                        if (account == null)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Suspense Account not found" } });
                        }
                        suspenseAccount.UpdateAccount(suspenseAccount);
                        return Request.CreateResponse(new { response = (long)addSuspenseAccountForm.sno, message = new List<string>() });
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteAccount(DeleteSuspenseAccountForm deleteSuspenseAccountForm)
        {
            if (ModelState.IsValid)
            {
                var suspenseAccount = new S_Account();
                try
                {
                    var exists = suspenseAccount.isExistSuspenseAccount(deleteSuspenseAccountForm.sno);
                    if (exists)
                    {
                        suspenseAccount.DeleteAccount(deleteSuspenseAccountForm.sno);
                        return Request.CreateResponse(new { response = deleteSuspenseAccountForm.sno, message = new List<string>() });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Suspense account does not exist." } });
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        }

    }
}