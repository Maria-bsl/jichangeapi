using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.smsservices;
using JichangeApi.Enums;
using JichangeApi.Models;
using JichangeApi.Utilities;
using QRCoder.Extensions;
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
                    /*case "administrator":
                        List<CompanyBankMaster> pendingCompanies = companyBankMaster.GetCompany1();
                        return pendingCompanies != null ? pendingCompanies : new List<CompanyBankMaster>();*/
                    default:
                        long branch = long.Parse(desibraid.braid.ToString());
                        List<CompanyBankMaster> branchCompanies = companyBankMaster.GetApprovedCompaniesByBranch(branch,"pending");
                        return branchCompanies != null ? branchCompanies : new List<CompanyBankMaster>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public C_Deposit ApproveCompany(AddCompanyApproveModel addCompanyApproveModel)
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster
                {
                    CompSno = addCompanyApproveModel.compsno,
                    Postedby = addCompanyApproveModel.userid.ToString(),
                    Status = GeneralStates.StatusType.Approved.ToString(),
                    Sus_Ac_SNo = addCompanyApproveModel.suspenseAccSno
                };
                var checkSuspenseAccount = companyBankMaster.Check_Suspense_Acc(companyBankMaster.Sus_Ac_SNo);

                if(checkSuspenseAccount != null)
                {
                    companyBankMaster.UpdateCompanysta(companyBankMaster);

                }
                else
                {
                    companyBankMaster.UpdateCompanystatus(companyBankMaster);
                }
               
                C_Deposit companyDeposit = new C_Deposit
                {
                    Deposit_Acc_No = addCompanyApproveModel.depositAccNo,
                    Comp_Mas_Sno = addCompanyApproveModel.compsno,
                    Reason = "Account Mapping Processed",
                    AuditBy = addCompanyApproveModel.userid.ToString()
                };
                companyDeposit.AddAccount(companyDeposit);
                var companydetails = new CompanyUsers().GetCompanyUsers(companyBankMaster.CompSno);

                string decodedPassword = Utilities.PasswordGeneratorUtil.DecodeFrom64(companydetails.Password);
                new SmsService().SendWelcomeSmsToNewUser(companydetails.Mobile, decodedPassword, companydetails.Mobile);
                EmailUtils.SendActivationEmail(companydetails.Email, companydetails.Username, decodedPassword, companydetails.Username);


                return companyDeposit;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
