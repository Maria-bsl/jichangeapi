using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using JichangeApi.Services;
using JichangeApi.Services.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CompanyInboxController : SetupBaseController
    {
        private readonly CompanyInboxService companyInboxService = new CompanyInboxService();
      

        [HttpPost]
        public HttpResponseMessage GetCompanys(Desibraid desibraid)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<CompanyBankMaster> companies = companyInboxService.GetDesingationBranchCompanyList(desibraid);
                return GetSuccessResponse(companies);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddCompanyBank(AddCompanyApproveModel apa)
        {
            try
            {
                C_Deposit cd = new C_Deposit();
                CompanyBankMaster c = new CompanyBankMaster();
                c.CompSno = apa.compsno;
                c.Postedby = apa.userid.ToString();
                c.Status = "Approved";
                c.Sus_Ac_SNo = apa.ssno;

                cd.Deposit_Acc_No = apa.pfx;
                cd.Comp_Mas_Sno = apa.compsno;
                //cd.Reason = 
                cd.AuditBy = apa.userid.ToString();

                c.UpdateCompanysta(c);
                cd.AddAccount(cd);
                return GetSuccessResponse(cd);
            }
            catch (Exception Ex)
            {
                return GetServerErrorResponse(Ex.Message);
            }
        }
    }
}
