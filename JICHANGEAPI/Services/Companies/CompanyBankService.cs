using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Controllers.smsservices;
using JichangeApi.Models;
using JichangeApi.Services.setup;
using JichangeApi.Utilities;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;

namespace JichangeApi.Services.Companies
{
    public class CompanyBankService
    {
        Payment pay = new Payment();
        private readonly RoleService roleService = new RoleService();

        private static readonly List<string> TABLE_COLUMNS = new List<string> { "comp_mas_sno", "company_name", "pobox_no", "physical_address", "region_id", "district_sno", "ward_sno", "tin_no", "vat_no", "director_name", "email_address", "telephone_no", "fax_no", "mobile_no", "posted_by", "posted_date" };
        private static readonly string TABLE_NAME = "Company";

        private static readonly string COMPANY_BANK = "Company_Bank_Details";
        private static readonly List<string> COMPANY_BANK_COLUMNS = new List<string> { "comp_bank_det_sno", "comp_mas_sno",
            "bank_name", "bank_branch", "account_no", "swift_code" };

        private static readonly string LOCALIZATION_COMPANY = "Localization_Company";
        private static readonly List<string> LOCALIZATION_COMPANY_COLUMNS = new List<string> { "loc_sno", "loc_eng", "loc_other", "comp_mas_sno",
            "dyn_eng", "dyn_other", "table_name", "column_name", "posted_by", "posted_date", "updated_by", "updated_date" };

        private void AppendInsertLocalizationCompanyAuditTrail(long sno, langcompany lang, long userid)
        {
            var values = new List<string> { sno.ToString(), lang?.Loc_Eng, lang?.Loc_Oth, lang?.comp_no.ToString(), "", lang?.Dyn_Swa, 
                lang?.Table_name, lang?.Col_name, userid.ToString(), DateTime.Now.ToString(), userid.ToString(), DateTime.Now.ToString()};
            Auditlog.InsertAuditTrail(values,userid,LOCALIZATION_COMPANY,LOCALIZATION_COMPANY_COLUMNS);
        }

        private void AppendInsertCompanyBankDetailAuditTrail(long sno, CompanyBankMaster bank,long userid)
        {
            var values = new List<string> { sno.ToString(), bank?.CompSno.ToString(), bank?.CompName, bank?.Branch_Sno.ToString(), 
                bank?.AccountNo, bank?.Swiftcode };
            Auditlog.InsertAuditTrail(values, userid, COMPANY_BANK, COMPANY_BANK_COLUMNS);
        }

        private void AppendCompanyBankDetailDeleteAuditTrail(long sno, CompanyBankMaster bank, long userid)
        {
            var values = new List<string> { sno.ToString(), bank?.CompSno.ToString(), bank?.CompName, bank?.Branch_Sno.ToString(),
                bank?.AccountNo, bank?.Swiftcode };
            Auditlog.deleteAuditTrail(values, userid, COMPANY_BANK, COMPANY_BANK_COLUMNS);
        }

        public static void AppendInsertAuditTrail(long compsno, CompanyBankMaster companyBankMaster, long userid)
        {
            try
            {
                List<string> values = new List<string> { compsno.ToString(), companyBankMaster.CompName, companyBankMaster.PostBox, companyBankMaster.Address, companyBankMaster.RegId.ToString(), companyBankMaster.DistSno.ToString(), companyBankMaster.WardSno.ToString(), companyBankMaster.TinNo, companyBankMaster.VatNo, companyBankMaster.DirectorName, companyBankMaster.Email, companyBankMaster.TelNo, companyBankMaster.FaxNo, companyBankMaster.MobNo, userid.ToString(), DateTime.Now.ToString() };
                Auditlog.InsertAuditTrail(values, userid, TABLE_NAME, TABLE_COLUMNS);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

     
        public static void AppendUpdateAuditTrail(long compsno, CompanyBankMaster oldCompany, CompanyBankMaster newCompany, long userid)
        {
            List<string> old = new List<string> { oldCompany.CompSno.ToString(), oldCompany.CompName,oldCompany.PostBox, oldCompany.Address, oldCompany.RegId.ToString(),oldCompany.DistSno.ToString(),oldCompany.WardSno.ToString(),oldCompany.TinNo,oldCompany.VatNo,oldCompany.DirectorName,
                                       oldCompany.Email,oldCompany.TelNo,oldCompany.FaxNo,oldCompany.MobNo, string.Empty, oldCompany.Posteddate.ToString() };
            List<string> update = new List<string> { compsno.ToString(), newCompany.CompName, newCompany.PostBox, newCompany.Address, newCompany.RegId.ToString(), newCompany.DistSno.ToString(), newCompany.WardSno.ToString(), newCompany.TinNo, newCompany.VatNo, newCompany.DirectorName, newCompany.Email, newCompany.TelNo, newCompany.FaxNo, newCompany.MobNo, userid.ToString(), DateTime.Now.ToString() };

            Auditlog.UpdateAuditTrail(old, update, userid, TABLE_NAME, TABLE_COLUMNS);
        }
        private langcompany AddCompanyLang(long companySno,long userid)
        {
            try
            {
                langcompany langCompany = new langcompany();
                List<langcompany> glang = langCompany.GetlocalengI();
                foreach (langcompany li in glang)
                {
                    langCompany.Loc_Eng = li.Loc_Eng;
                    langCompany.Loc_Eng1 = li.Loc_Eng1;
                    langCompany.Table_name = li.Table_name;
                    langCompany.Col_name = li.Col_name;
                    langCompany.Loc_Oth1 = li.Dyn_Swa;
                    langCompany.comp_no = companySno;
                    var addedSno = langCompany.AddLang(langCompany);
                    AppendInsertLocalizationCompanyAuditTrail(addedSno, langCompany, userid);
                }
                return langCompany;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        private CompanyUsers AddCompanyUser(CompanyBankMaster companyBankMaster, long companySno,long userid)
        {
            CompanyUsers companyUsers = new CompanyUsers();
            try
            {
                companyUsers.Compmassno = companySno;
                companyUsers.Email = companyBankMaster.Email;
                companyUsers.Usertype = "001";
                companyUsers.Mobile = companyBankMaster.MobNo;
                companyUsers.Flogin = "false";
                companyUsers.Fullname = companyBankMaster.MobNo;
                companyUsers.Username = companyBankMaster.MobNo;
                companyUsers.Password = PasswordGeneratorUtil.GetEncryptedData(PasswordGeneratorUtil.CreateRandomPassword(8));
                companyUsers.CreatedDate = DateTime.Now;
                companyUsers.PostedDate = DateTime.Now;
                companyUsers.ExpiryDate = System.DateTime.Now.AddMonths(3);
                companyUsers.PostedBy = userid.ToString();
                companyUsers.Userpos = roleService.GetRoleList().Find(e => e.Description.ToLower().Equals("admin")).Sno.ToString();
                long addedCompSno = companyUsers.AddCompanyUsers1(companyUsers);
                companyUsers.CompuserSno = addedCompSno;
                CompanyUsersService.AppendInsertAuditTrail(addedCompSno, companyUsers, userid);
                return companyUsers;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        private CompanyBankMaster CreateCompanyBank(CompanyBankAddModel companyBankAddModel)
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                companyBankMaster.CompSno = companyBankAddModel.compsno;
                companyBankMaster.CompName = companyBankAddModel.compname;
                companyBankMaster.PostBox = companyBankAddModel.pbox;
                companyBankMaster.Address = companyBankAddModel.addr;
                companyBankMaster.RegId = (long)companyBankAddModel.rsno;
                companyBankMaster.DistSno = (long)companyBankAddModel.dsno;
                companyBankMaster.WardSno = (long)companyBankAddModel.wsno;
                companyBankMaster.TinNo = companyBankAddModel.tin;
                companyBankMaster.VatNo = companyBankAddModel.vat;
                companyBankMaster.DirectorName = companyBankAddModel.dname;
                companyBankMaster.Email = companyBankAddModel.email;
                companyBankMaster.TelNo = companyBankAddModel.telno;
                companyBankMaster.FaxNo = companyBankAddModel.fax;
                companyBankMaster.MobNo = companyBankAddModel.mob;
                companyBankMaster.Branch_Sno = companyBankAddModel.branch;
                companyBankMaster.Checker = companyBankAddModel.check_status;
                companyBankMaster.Status = "Pending";
                companyBankMaster.Postedby = companyBankAddModel.userid.ToString();
                return companyBankMaster;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        private List<string> CheckCompanyBankErrors(CompanyBankMaster companyBankMaster)
        {
            try
            {
                CompanyUsers companyUsers = new CompanyUsers();
                if (companyBankMaster.ValidateCount(companyBankMaster.CompName.ToLower(), companyBankMaster.TinNo))
                {
                    return new List<string> { "Company name exists" };
                }
                else if (companyBankMaster.ValidateTinNumber(companyBankMaster.TinNo))
                {
                    return new List<string> { "Tin number exists" };
                }
                else if (companyUsers.ValidateduplicateEmail1(companyBankMaster.Email))
                {
                    return new List<string> { "Email exists" };
                }
                else if (companyUsers.ValidateMobile(companyBankMaster.MobNo))
                {
                    return new List<string> { "Mobile number exists" };
                }
                else if (companyUsers.Validateduplicateuser1(companyBankMaster.MobNo))
                {
                    return new List<string> { "User already exists" };
                }
                else
                {
                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        private List<string> IsValidUpdateCompanyBank(CompanyBankMaster companyBankMaster)
        {
            CompanyUsers companyUsers = new CompanyUsers();
            try
            {
                CompanyBankMaster getcom = companyBankMaster.EditCompany(companyBankMaster.CompSno);
                if (getcom == null) { return new List<string> { SetupBaseController.NOT_FOUND_MESSAGE }; }
                bool flag = !(getcom.MobNo == companyBankMaster.MobNo);
                bool cmp = !(getcom.CompName == companyBankMaster.CompName);
                bool email = !(getcom.Email == companyBankMaster.Email);
                if (companyBankMaster.ValidateCount(companyBankMaster.CompName.ToLower(), companyBankMaster.TinNo) && cmp == true)
                {
                    return new List<string> { "Company name exists" };
                }
                else if (companyBankMaster.IsDuplicateTinNumber(companyBankMaster.TinNo, companyBankMaster.CompSno))
                {
                    return new List<string> { "Tin number exists" };
                }
                else if (companyUsers.ValidateMobile(companyBankMaster.MobNo) && flag == true)
                {
                    return new List<string> { "Mobile number exists" };
                }
                else { return new List<string>(); }
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        private void AddCompanyBankDetails(long compsno, CompanyBankMaster companyBankMaster, CompanyBankAddModel companyBankAddModel)
        {
            try
            {
                for (int i = 0; i < companyBankAddModel.details.Count(); i++)
                {
                    if (companyBankAddModel.details[i].AccountNo != null && companyBankAddModel.details[i].AccountNo.Length > 0)
                    {
                        companyBankMaster.CompSno = compsno;
                        companyBankMaster.AccountNo = companyBankAddModel.details[i].AccountNo;
                        long detsno = companyBankMaster.AddBank(companyBankMaster);
                        AppendInsertCompanyBankDetailAuditTrail(detsno, companyBankMaster,(long) companyBankAddModel.userid);
                    }
                }
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        private CompanyBankMaster CreateCompanyBankL(AddCompanyBankL addCompanyBankL)
        {
            CompanyBankMaster companyBankMaster = new CompanyBankMaster();
            try
            {
                companyBankMaster.CompSno = addCompanyBankL.compsno;
                companyBankMaster.CompName = addCompanyBankL.compname;
                companyBankMaster.PostBox = addCompanyBankL.pbox;
                companyBankMaster.Address = addCompanyBankL.addr;
                companyBankMaster.RegId = addCompanyBankL.rsno;
                companyBankMaster.DistSno = addCompanyBankL.dsno;
                companyBankMaster.WardSno = addCompanyBankL.wsno;
                companyBankMaster.TinNo = addCompanyBankL.tin;
                companyBankMaster.VatNo = addCompanyBankL.vat;
                companyBankMaster.DirectorName = addCompanyBankL.dname;
                companyBankMaster.Email = addCompanyBankL.email;
                companyBankMaster.TelNo = addCompanyBankL.telno;
                companyBankMaster.FaxNo = addCompanyBankL.fax;
                companyBankMaster.MobNo = addCompanyBankL.mob;
                companyBankMaster.Branch_Sno = addCompanyBankL.branch;
                companyBankMaster.Checker = addCompanyBankL.check_status;
                companyBankMaster.Status = "Pending";
                //companyBankMaster.Postedby = addCompanyBankL.userid.ToString();
                return companyBankMaster;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public long InsertCompanyBankLAndReturnId(AddCompanyBankL addCompanyBankL)
        {
            try
            {
                CompanyBankMaster companyBankMaster = CreateCompanyBankL(addCompanyBankL);
                List<string> errors = CheckCompanyBankErrors(companyBankMaster);
                if (errors.Count > 0) { throw new Exception(errors[0]); }
                long compsno = companyBankMaster.AddCompany(companyBankMaster);
                AppendInsertAuditTrail(compsno, companyBankMaster, (long)addCompanyBankL.userid);
                if (compsno > 0)
                {
                    langcompany lang = AddCompanyLang(compsno,(long) addCompanyBankL.userid);
                    CompanyUsers companyUsers = AddCompanyUser(companyBankMaster, compsno, (long) addCompanyBankL.userid);
                    companyBankMaster.CompSno = compsno;
                    companyBankMaster.AccountNo = addCompanyBankL.accno;
                    long detsno = companyBankMaster.AddBank(companyBankMaster);
                    AppendInsertCompanyBankDetailAuditTrail(detsno, companyBankMaster, (long)addCompanyBankL.userid);

                    new SmsService().SendSuccessSmsToNewUser(addCompanyBankL.mob, addCompanyBankL.mob);
                    EmailUtils.SendSuccessEmail(companyBankMaster.Email, companyBankMaster.CompName);

                  
                    return compsno;
                }
                else
                {
                    throw new Exception("Failed to create company");
                }
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public void UpdateCompanyBankL(AddCompanyBankL addCompanyBankL)
        {
            try
            {
                CompanyBankMaster companyBankMaster = CreateCompanyBankL(addCompanyBankL);
                List<string> errors = IsValidUpdateCompanyBank(companyBankMaster);
                if (errors.Count > 0) { throw new Exception(errors[0]); }
                CompanyBankMaster getcom = companyBankMaster.EditCompany(addCompanyBankL.compsno);
                companyBankMaster.AccountNo = addCompanyBankL.accno;
                long detsno = companyBankMaster.AddBank(companyBankMaster);
                companyBankMaster.UpdateCompany(companyBankMaster);
                AppendUpdateAuditTrail(addCompanyBankL.compsno, getcom, companyBankMaster, (long)addCompanyBankL.userid);
                var banks = companyBankMaster.EditBank(addCompanyBankL.compsno);
                if (banks != null && banks.Count() > 0)
                {
                    //AppendCompanyBankDetailDeleteAuditTrail(addCompanyBankL.compsno, companyBankMaster, (long)addCompanyBankL.userid);
                    banks.ForEach(bank => AppendCompanyBankDetailDeleteAuditTrail(bank.CompSno, bank, (long)addCompanyBankL.userid));
                    companyBankMaster.DeleteBank(companyBankMaster);
                }
                //companyBankMaster.DeleteBank(companyBankMaster);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public long InsertCompanyBankAndReturnId(CompanyBankAddModel companyBankAddModel)
        {
            try
            {
                CompanyBankMaster companyBankMaster = CreateCompanyBank(companyBankAddModel);
                List<string> errors = CheckCompanyBankErrors(companyBankMaster);
                if (errors.Count > 0) { throw new ArgumentException(errors[0]); }
                long addedCompany = companyBankMaster.AddCompany(companyBankMaster);
                AppendInsertAuditTrail(addedCompany, companyBankMaster, (long)companyBankAddModel.userid);
                if (addedCompany > 0)
                {
                    langcompany lang = AddCompanyLang(addedCompany, (long) companyBankAddModel.userid);
                    CompanyUsers companyUsers = AddCompanyUser(companyBankMaster, addedCompany, (long) companyBankAddModel.userid);
                    AddCompanyBankDetails(addedCompany, companyBankMaster, companyBankAddModel);

                    new SmsService().SendSuccessSmsToNewUser(companyBankAddModel.mob, companyBankAddModel.mob);
                    EmailUtils.SendSuccessEmail(companyBankMaster.Email, companyBankMaster.CompName);

                    return addedCompany;
                }
                else
                {
                    throw new Exception("Failed to create company");
                }
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.ToString());
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public void UpdateCompanyBank(CompanyBankAddModel companyBankAddModel)
        {
            try
            {
                CompanyBankMaster companyBankMaster = CreateCompanyBank(companyBankAddModel);
                List<string> errors = IsValidUpdateCompanyBank(companyBankMaster);
                if (errors.Count > 0) { throw new Exception(errors[0]); }
                CompanyBankMaster getcom = companyBankMaster.EditCompany(companyBankAddModel.compsno);
                if (getcom == null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
                companyBankMaster.UpdateCompany(companyBankMaster);
                AppendUpdateAuditTrail(companyBankAddModel.compsno, getcom, companyBankMaster, (long)companyBankAddModel.userid);
                var banks = companyBankMaster.EditBank(companyBankAddModel.compsno);
                if (banks != null && banks.Count() > 0)
                {
                    banks.ForEach(bank => AppendCompanyBankDetailDeleteAuditTrail(bank.CompSno, bank, (long)companyBankAddModel.userid));
                    companyBankMaster.DeleteBank(companyBankMaster);
                }
                
                AddCompanyBankDetails(companyBankMaster.CompSno, companyBankMaster, companyBankAddModel);
                if (!getcom.Email.ToLower().Equals(companyBankAddModel.email))
                {
                    EmailUtils.SendSuccessEmail(companyBankMaster.Email, companyBankMaster.CompName);
                }
                else if (getcom.MobNo.Equals(companyBankAddModel.mob))
                {
                    new SmsService().SendSuccessSmsToNewUser(companyBankAddModel.mob, companyBankAddModel.mob);
                }
            }
            catch(ArgumentException ex)
            {
                throw new ArgumentException(ex.ToString());
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<CompanyBankMaster> GetCompaniesList()
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                List<CompanyBankMaster> result = companyBankMaster.GetCompany_S();
                if (result != null) { return result; }
                return new List<CompanyBankMaster>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<CompanyBankMaster> GetCompanyListWithStatus(string status)
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                List<CompanyBankMaster> result = companyBankMaster.GetCompany_A(status);
                if (result != null) { return result; }
                return new List<CompanyBankMaster>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<CompanyBankMaster> GetCompanyListWithSuspenseAccountIncluded(long compsno)
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                List<CompanyBankMaster> result = companyBankMaster.GetCompany_Suspense(compsno);
                if (result != null) { return result; }
                return new List<CompanyBankMaster>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<CompanyBankMaster> GetCompanyListSuspnseAccounts()
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                List<CompanyBankMaster> result = companyBankMaster.GetCompany_Sus();
                if (result != null) { return result; }
                return new List<CompanyBankMaster>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<CompanyBankMaster> GetCompanyListBankAccounts(long compid)
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                List<CompanyBankMaster> result = companyBankMaster.GetBank_S(compid);
                if (result != null) { return result; }
                return new List<CompanyBankMaster>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<CompanyBankMaster> GetCompanyListBankAccountDetails(long compid) 
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                List<CompanyBankMaster> result = companyBankMaster.GetBank(compid);
                if (result != null) { return result; }
                return new List<CompanyBankMaster>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public bool IsExistAccountNumber(string accountNumber)
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                bool exits = companyBankMaster.Validateaccount(accountNumber);
                return exits;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public CompanyBankMaster GetCompanyDetail(long companyId)
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                return companyBankMaster.FindCompanyById(companyId);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public bool IsExistCompany(long companyId)
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                return companyBankMaster.EditCompanyss(companyId) != null;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public bool DeleteCompany(long companyId) 
        { 
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                CompanyBankMaster company = companyBankMaster.EditCompanyss(companyId);
                company.DeleteBank(company);
                company.DeleteCompany(companyId);
                return true;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

    }
}
