using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services.Companies
{
    public class CompanyInboxService
    {
        public List<CompanyBankMaster> GetDesingationBranchCompanyList(Desibraid desibraid)
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                switch (desibraid.design.ToLower()) 
                {
                    case "administrator":
                        List<CompanyBankMaster> pendingCompanies = companyBankMaster.GetCompany1();
                        return pendingCompanies != null ? pendingCompanies : new List<CompanyBankMaster>();
                    default:
                        List<CompanyBankMaster> branchCompanies = companyBankMaster.GetCompany1_Branch(long.Parse(desibraid.braid.ToString()));
                        return branchCompanies != null ? branchCompanies : new List<CompanyBankMaster>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
