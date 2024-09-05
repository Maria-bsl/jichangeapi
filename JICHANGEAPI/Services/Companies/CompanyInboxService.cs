using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Controllers.smsservices;
using JichangeApi.Enums;
using JichangeApi.Models;
using JichangeApi.Services.setup;
using JichangeApi.Utilities;
using QRCoder.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace JichangeApi.Services.Companies
{
    public class CompanyInboxService
    {
        private readonly CompanyBankService companyBankService = new CompanyBankService();
        readonly Payment pay = new Payment();

        private static readonly List<string> DEPOSIT_ACCOUNT_COLUMNS = new List<string> { "comp_dep_acc_sno", "comp_mas_sno", "deposit_acc_no", "reason", "posted_by", "posted_date" };
        private static readonly string DEPOSIT_ACCOUNT = "Company_Deposit_Account";
        private void AppendInsertDistrictAuditTrail(long sno, C_Deposit deposit, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), deposit.Comp_Mas_Sno.ToString(), deposit.Deposit_Acc_No, deposit.Reason, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, DEPOSIT_ACCOUNT, DEPOSIT_ACCOUNT_COLUMNS);
        }


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
                        return branchCompanies ?? new List<CompanyBankMaster>();
                }
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public C_Deposit ApproveCompany(AddCompanyApproveModel addCompanyApproveModel)
        {
            try
            {
                var found = companyBankService.GetCompanyDetail((long)addCompanyApproveModel.compsno);
                if (found == null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); };
                CompanyBankMaster companyBankMaster = new CompanyBankMaster
                {
                    CompSno = addCompanyApproveModel.compsno,
                    Postedby = addCompanyApproveModel.userid.ToString(),
                    Status = GeneralStates.StatusType.Approved.ToString(),
                    Sus_Ac_SNo = addCompanyApproveModel.suspenseAccSno
                };
                var checkSuspenseAccount = companyBankMaster.Check_Suspense_Acc(companyBankMaster.Sus_Ac_SNo);

                if (checkSuspenseAccount != null)
                {
                    companyBankMaster.UpdateCompanysta(companyBankMaster);
                }
                else
                {
                    companyBankMaster.UpdateCompanystatus(companyBankMaster);
                }
                CompanyBankService.AppendUpdateAuditTrail(addCompanyApproveModel.compsno, found, companyBankMaster, (long)addCompanyApproveModel.userid);

                C_Deposit companyDeposit = new C_Deposit
                {
                    Deposit_Acc_No = addCompanyApproveModel.depositAccNo,
                    Comp_Mas_Sno = addCompanyApproveModel.compsno,
                    Reason = "Account Mapping Processed",
                    AuditBy = addCompanyApproveModel.userid.ToString()
                };
                long addedAccount = companyDeposit.AddAccount(companyDeposit);
                AppendInsertDistrictAuditTrail(addedAccount, companyDeposit, (long)addCompanyApproveModel.userid);

                var companydetails = new CompanyUsers().GetCompanyUsers(companyBankMaster.CompSno);

                string decodedPassword = Utilities.PasswordGeneratorUtil.DecodeFrom64(companydetails.Password);
                new SmsService().SendWelcomeSmsToNewUser(companydetails.Mobile, decodedPassword, companydetails.Mobile);
                EmailUtils.SendActivationEmail(companydetails.Email, companydetails.Username, decodedPassword, companydetails.Username);


                return companyDeposit;
            }
            catch(Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
    }
}
