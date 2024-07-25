using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using JichangeApi.Services;
using JichangeApi.Services.setup;
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
    public class CompanyController : SetupBaseController
    {
        private readonly CompanyBankService companyBankService = new CompanyBankService();
        private readonly RegionService regionService = new RegionService();
        private readonly DistrictService districtService = new DistrictService();
        private readonly WardService wardService = new WardService();
        // GET: Company


        #region  Companys and S

        /*  [HttpPost]
          public HttpResponseMessage GetCompanys(string stat)
          {
              try
              {
                  *//*var result = c.GetCompany();
                  if (result != null)
                  {
                      return Request.CreateResponse(new { response = result, message = new List<string> { } });
                  }
                  else
                  {
                      var d = 0;
                      return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
                  }*/
        /*string stat = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["stat"]))
                stat = Request.QueryString["stat"].ToString();
            else
                stat = string.Empty;
        }
        catch
        {
            stat = string.Empty;
        }*//*
        if (stat == "app")
        {
            stat = "Approved";
        }
        else if (stat == "pen")
        {
            stat = "Pending";
        }
        else
        {
            stat = string.Empty;
        }
        string company = stat;
        var result = c.GetCompany1();
        if (Session["desig"].ToString() == "Administrator" && company == "")
        {
            result = c.GetCompany1_A();
        }
        else if (Session["desig"].ToString() == "Administrator" && company != "")
        {
            result = c.GetCompany1_A_Q(company);
        }
        else
        {
            if (company == "")
            {
                result = c.GetCompany1_Branch_A(long.Parse(branch.ToString()));
            }
            else
            {
                result = c.GetCompany1_Branch_A_Q(long.Parse(branch.ToString()), company);
            }
        }
        if (result != null)
        {
            return Request.CreateResponse(new { response = result, message = new List<string> { } });
        }
        else
        {
            var d = 0;
            return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
        }
    }
    catch (Exception Ex)
    {
        Ex.ToString();
    }
    return returnNull;
}
[HttpPost]
public HttpResponseMessage GetApp()
{
    try
    {


        var result = c.GetCompany1();
        if (Session["desig"].ToString() == "Administrator")
        {
            result = c.GetCompany1_D();
        }
        else
        {

            result = c.GetCompany1_Branch_D(long.Parse(branch.ToString()));


        }
        if (result != null)
        {
            return Request.CreateResponse(new { response = result, message = new List<string> { } });
        }
        else
        {
            var d = 0;
            return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
        }
    }
    catch (Exception Ex)
    {
        Ex.ToString();
    }
    return returnNull;
}
*/


        #endregion

        [HttpPost]
        public HttpResponseMessage GetCompanys_S()
        {
            CompanyBankService companyBankService = new CompanyBankService();
            try
            {
                List < CompanyBankMaster > companies = companyBankService.GetCompaniesList();
                return GetSuccessResponse(companies);
            }
            catch (Exception Ex)
            {
                return GetServerErrorResponse(Ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetCompanys_A()
        {
            try
            {
                string status = "Approved";
                List<CompanyBankMaster> companies = companyBankService.GetCompanyListWithStatus(status);
                return GetSuccessResponse(companies);
            }
            catch (Exception Ex)
            {
                return GetServerErrorResponse(Ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetCompanys_SU(SingletonComp com)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                List<CompanyBankMaster> companies = companyBankService.GetCompanyListWithSuspenseAccountIncluded((long) com.compid);
                return GetSuccessResponse(companies);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetCompanys_SUS()
        {
            try
            {
                List<CompanyBankMaster> companies = companyBankService.GetCompanyListSuspnseAccounts();
                return GetSuccessResponse(companies);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetAccount(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                List<CompanyBankMaster> companies = companyBankService.GetCompanyListBankAccounts((long)singletonComp.compid);
                return GetSuccessResponse(companies);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetBanks(SingletonSno singletonSno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                List<CompanyBankMaster> companies = companyBankService.GetCompanyListBankAccountDetails((long)singletonSno.Sno);
                return GetSuccessResponse(companies);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage CheckAccount(SingletonAcc singletonAcc)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                bool exists = companyBankService.IsExistAccountNumber(singletonAcc.acc);
                return GetSuccessResponse(exists);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetDetailsindivi(SingletonSno singletonSno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                CompanyBankMaster company = companyBankService.GetCompanyDetail((long) singletonSno.Sno);
                if (company == null) { return GetNotFoundResponse();  }
                return GetSuccessResponse(company);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage FindCompany(long sno)
        {
            try
            {
                SingletonSno singletonSno = new SingletonSno();
                singletonSno.Sno = sno;
                return GetDetailsindivi(singletonSno);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteCompanyBank(long sno)
        {
            try
            {
                bool isExist = companyBankService.IsExistCompany(sno);
                if (!isExist) { return GetNotFoundResponse(); }
                bool deleted = companyBankService.DeleteCompany(sno);
                if (deleted) { return GetSuccessResponse(sno); }
                var messages = new List<string> { "Failed to delete company" };
                return GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetRegionDetails()
        {
            try
            {
                List<REGION> results = regionService.GetRegionsList();
                return GetSuccessResponse(results);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetDistDetails(SingletonSno singletonSno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                List<DISTRICTS> results = districtService.GetActiveDistrict( (long) singletonSno.Sno);
                return GetSuccessResponse(results);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetWard(long sno)
        {
            try
            {
                List<WARD> wards = wardService.GetActiveWard(sno);
                return GetSuccessResponse(wards);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddCompanyBank(CompanyBankAddModel companyBankAddModel)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            CompanyBankService companyBankService = new CompanyBankService();
            try
            {
                if (companyBankAddModel.compsno == 0)
                {
                    long insertedCompany = companyBankService.InsertCompanyBankAndReturnId(companyBankAddModel);
                    return FindCompany(insertedCompany);
                }
                else
                {
                    companyBankService.UpdateCompanyBank(companyBankAddModel);
                    return FindCompany(companyBankAddModel.compsno);
                }
            }
            catch (Exception ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddCompanyBankL(AddCompanyBankL addCompanyBankL)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            CompanyBankService companyBankService = new CompanyBankService();
            try
            {
                if (addCompanyBankL.compsno == 0)
                {
                    long insertedCompany = companyBankService.InsertCompanyBankLAndReturnId(addCompanyBankL);
                    return FindCompany(insertedCompany);
                }
                else
                {
                    companyBankService.UpdateCompanyBankL(addCompanyBankL);
                    return FindCompany(addCompanyBankL.compsno);
                }

            }
            catch (Exception ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
        }
    }
}
