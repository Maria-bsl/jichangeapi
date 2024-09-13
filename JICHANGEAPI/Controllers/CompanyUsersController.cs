using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using JichangeApi.Models.form;
using JichangeApi.Services;
using JichangeApi.Services.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CompanyUsersController : SetupBaseController
    {
        private readonly CompanyUsersService companyUsersService = new CompanyUsersService();
        private readonly Payment pay = new Payment();
       
        [HttpPost]
        public HttpResponseMessage GetCompanyUserss(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<CompanyUsers> companyUsers = companyUsersService.GetCompanyUsersList(singletonComp);
                return GetSuccessResponse(companyUsers);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage EditCompanyUserss(SingletonSno Sno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                CompanyUsers user = companyUsersService.EditCompanyUser((long)Sno.Sno);
                if (user != null) { return GetSuccessResponse(user); }
                return GetNotFoundResponse();
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage ResendCredentials(ResendUserCredentialsForm form)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                var found = companyUsersService.ResendUserCredentials(form.resendCredentials, (long)form.companyUserId);
                return GetSuccessResponse(found);
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
        public HttpResponseMessage AddCompanyUser(AddCompanyUserForm addCompanyUserForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                if ((long) addCompanyUserForm.sno == 0)
                {
                    CompanyUsers user = companyUsersService.InsertCompanyUser(addCompanyUserForm);
                    return GetSuccessResponse(user);
                }
                else
                {
                    CompanyUsers user = companyUsersService.UpdateCompanyUser(addCompanyUserForm);
                    return GetSuccessResponse(user);
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

                return GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage CheckdupliacteEmail(String name)
        {
            try
            {
                bool isDuplicateEmail = companyUsersService.IsDuplicateEmail(name);
                return GetSuccessResponse(isDuplicateEmail);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage Checkdupliacte(String name)
        {
            try
            {
                bool isDuplicateUser = companyUsersService.IsDuplicateUser(name);
                return GetSuccessResponse(isDuplicateUser);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteCompanyUser(DeleteCompanyUserForm deleteCompanyUserForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                long removedSno = companyUsersService.RemoveCompanyUser(deleteCompanyUserForm);
                return GetSuccessResponse(removedSno);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }
    }
}
