using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Controllers.smsservices;
using JichangeApi.Models;
using JichangeApi.Models.form;
using JichangeApi.Utilities;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;

namespace JichangeApi.Services.Companies
{
    public class CompanyUsersService
    {
        private static readonly List<string> TABLE_COLUMNS = new List<string> { "comp_users_sno", "comp_mas_sno", "username",  "user_type", "created_date", "expiry_date",
             "posted_by", "posted_date"};
        public static readonly string TABLE_NAME = "Companyusers";
        private void AppendInsertAuditTrail(long sno, CompanyUsers user, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), user.Compmassno.ToString(), user.Username, user.Usertype, System.DateTime.Now.ToString(), System.DateTime.Now.AddMonths(3).ToString(),userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, CompanyUsersService.TABLE_NAME,CompanyUsersService.TABLE_COLUMNS);
        }
        private void AppendUpdateAuditTrail(long sno, CompanyUsers oldUser, CompanyUsers newUser, long userid)
        {
            List<string> oldUserValues = new List<string> { sno.ToString(), oldUser.Compmassno.ToString(), oldUser.Username, oldUser.Usertype, oldUser.CreatedDate.ToString(), oldUser.ExpiryDate.ToString(),userid.ToString(), oldUser.PostedDate.ToString() };
            List<string> newUserValues = new List<string> { sno.ToString(), newUser.Compmassno.ToString(), newUser.Username, newUser.Usertype, System.DateTime.Now.ToString(), System.DateTime.Now.AddMonths(3).ToString(),userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldUserValues, newUserValues, userid, CompanyUsersService.TABLE_NAME, CompanyUsersService.TABLE_COLUMNS);
        }
        private void AppendDeleteAuditTrail(long sno, CompanyUsers user, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), user.Compmassno.ToString(), user.Username, user.Usertype, user.CreatedDate.ToString(), user.ExpiryDate.ToString(), user.PostedBy, user.PostedDate.ToString() };
            Auditlog.deleteAuditTrail(values, userid, CompanyUsersService.TABLE_NAME, CompanyUsersService.TABLE_COLUMNS);
        }

        private CompanyUsers CreateCompanyUser(AddCompanyUserForm addCompanyUserForm)
        {
            CompanyUsers user = new CompanyUsers();
            user.CompuserSno = (long) addCompanyUserForm.sno;
            user.Compmassno = (long) addCompanyUserForm.compid;
            user.Username = addCompanyUserForm.auname;
            user.Mobile = addCompanyUserForm.mob;
            user.Userpos = addCompanyUserForm.pos;
            user.Email = addCompanyUserForm.mail;
            user.Fullname = addCompanyUserForm.uname;
            user.Flogin = "false";
            user.CreatedDate = System.DateTime.Now;
            user.ExpiryDate = System.DateTime.Now.AddMonths(3);
            user.Usertype = addCompanyUserForm.chname;
            string password = PasswordGeneratorUtil.CreateRandomPassword(8);
            user.Password = PasswordGeneratorUtil.GetEncryptedData(password);
            user.PostedBy = addCompanyUserForm.userid.ToString();
            return user;
        }
        private List<string> CheckInsertCompanyUserErrors(CompanyUsers user)
        {
            if (user.ValidateduplicateEmail(user.Email))
            {
                return new List<string> { "Email already exist" };
            }
            else if (user.Validateduplicateuser(user.Username))
            {
                return new List<string> { "User already exist" };
            }
            else { return new List<string>(); }
        }
        public List<CompanyUsers> GetCompanyUsersList(SingletonComp singletonComp)
        {
            try
            {
                CompanyUsers companyUsers = new CompanyUsers();
                List<CompanyUsers> result = companyUsers.GetCompanyUsers1((long) singletonComp.compid);
                return result != null ? result : new List<CompanyUsers>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CompanyUsers EditCompanyUser(long companyUserId)
        {
            try
            {
                CompanyUsers companyUsers = new CompanyUsers();
                CompanyUsers user = companyUsers.EditCompanyUsers(companyUserId);
                return user != null ? user : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CompanyUsers InsertCompanyUser(AddCompanyUserForm addCompanyUserForm)
        {
            try
            {
                CompanyUsers user = CreateCompanyUser(addCompanyUserForm);
                List<string> errors = CheckInsertCompanyUserErrors(user);
                if (errors.Count > 0) { throw new ArgumentException(errors[0]); }
                long addedUser = user.AddCompanyUsers(user);
                if (addedUser > 0)
                {
                    SmsService sms = new SmsService();
                    sms.SendWelcomeSmsToNewUser(user.Username, PasswordGeneratorUtil.DecodeFrom64(user.Password), user.Mobile);
                    EmailUtils.SendActivationEmail(user.Email, user.Username, PasswordGeneratorUtil.DecodeFrom64(user.Password), user.Fullname);
                    AppendInsertAuditTrail(addedUser, user, (long)addCompanyUserForm.userid);
                    return EditCompanyUser(addedUser);
                }
                else throw new ArgumentException("Failed to insert company user");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CompanyUsers UpdateCompanyUser(AddCompanyUserForm addCompanyUserForm)
        {
            try
            {
                CompanyUsers user = CreateCompanyUser(addCompanyUserForm);
                CompanyUsers found = EditCompanyUser((long)addCompanyUserForm.sno);
                AppendUpdateAuditTrail((long) addCompanyUserForm.sno,found,user,(long) addCompanyUserForm.userid);
                user.UpdateCompanyUsers(user);
                return EditCompanyUser((long)addCompanyUserForm.sno);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public CompanyUsers UpdateCompanyUserPassword(CompanyUsers user)
        {
            try
            {
               /* CompanyUsers user = CreateCompanyUser(addCompanyUserForm);
                CompanyUsers found = user.EditCompanyUsers((long)addCompanyUserForm.sno);
                if (found != null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
                AppendUpdateAuditTrail((long)addCompanyUserForm.sno, found, user, (long)addCompanyUserForm.userid);*/
                user.UpdateCompanyUsersP(user);
                return EditCompanyUser((long)user.CompuserSno);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public EMP_DET UpdateBankUserPassword(EMP_DET user)
        {
            try
            {
                /* CompanyUsers user = CreateCompanyUser(addCompanyUserForm);
                 CompanyUsers found = user.EditCompanyUsers((long)addCompanyUserForm.sno);
                 if (found != null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
                 AppendUpdateAuditTrail((long)addCompanyUserForm.sno, found, user, (long)addCompanyUserForm.userid);
                user.UpdateCompanyUsersP(user);
                return EditCompanyUser((long)user.CompuserSno);*/
                return null;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsDuplicateEmail(string email)
        {
            try
            {
                CompanyUsers companyUser = new CompanyUsers();
                bool isDuplicate = companyUser.ValidateduplicateEmail(email);
                return isDuplicate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsDuplicateUser(string username)
        {
            try
            {
                CompanyUsers companyUser = new CompanyUsers();
                bool isDuplicate = companyUser.Validateduplicateuser(username);
                return isDuplicate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public long RemoveCompanyUser(DeleteCompanyUserForm form)
        {
            try
            {
                CompanyUsers companyUser = new CompanyUsers();
                CompanyUsers found = companyUser.EditCompanyUsers((long) form.sno);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                AppendDeleteAuditTrail((long)form.sno, found, (long)form.userid);
                found.DeleteCompany((long)form.sno);
                return (long)form.sno;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
