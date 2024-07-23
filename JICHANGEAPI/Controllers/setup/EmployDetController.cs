
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form.setup.insert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using PasswordGenerator;
using System.Net.Mail;
using System.Net;
using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using JichangeApi.Models.form;
using JichangeApi.Models.form.setup.remove;

namespace JichangeApi.Controllers.setup
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployDetController : SetupBaseController
    {
        private static readonly List<string> tableColumns = new List<string> { "emp_detail_id", "emp_id_no","fullname","first_name", "middle_name", "last_name", "desg_id","email_id", "mobile_no",
            "created_date","expiry_date", "emp_status","posted_by", "posted_date","username"};

        private static readonly string tableName = "Bank User";

        private static string GetEncryptedData(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

        private void SendActivationEmail(String email, String fullname, String pwd, String uname)
        {
            Guid activationCode = Guid.NewGuid();
            SmtpClient smtp = new SmtpClient();
            S_SMTP stp = new S_SMTP();
            EMAIL em = new EMAIL();
            string drt = "";

            try
            {
                using (MailMessage mm = new MailMessage())
                {
                    var m = stp.getSMTPText();
                    var data = em.getEMAILst("1");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Subject;
                    drt = data.Subject;
                    /*var urlBuilder = new System.UriBuilder(Request.Url.AbsoluteUri)
                    {
                        Path = Url.Action("Loginnew", "Loginnew"),
                        Query = null,
                    };

                    Uri uri = urlBuilder.Uri;
                    string url = urlBuilder.ToString();*/
                    string url = "";
                    String body = data.Email_Text.Replace("}+cName+{", fullname).Replace("}+uname+{", uname).Replace("}+pwd+{", pwd).Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");

                    mm.Body = body;
                    mm.IsBodyHtml = true;

                    smtp.Host = m.SMTP_Address;
                    smtp.EnableSsl = Convert.ToBoolean(m.SSL_Enable);
                    NetworkCredential NetworkCred = new NetworkCredential(m.From_Address, m.SMTP_Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = Convert.ToInt16(m.SMTP_Port);
                    smtp.Send(mm);
                }
            }
            catch (Exception Ex)
            {
                Utilites.logfile("Bank", drt, Ex.ToString());
            }
        }

        private void AppendInsertAuditTrail(long sno, EMP_DET emploee, long userid)
        {
            var values = new List<string> { sno.ToString(), emploee.Emp_Id_No,emploee.Full_Name,emploee.First_Name, emploee.Middle_name,  emploee.Last_name,emploee.Desg_name.ToString(),emploee.Email_Address,
                                emploee.Mobile_No,emploee.Created_Date.ToString(),emploee.Expiry_Date.ToString(), emploee.Emp_Status,userid.ToString(), DateTime.Now.ToString(),emploee.User_name };
            Auditlog.InsertAuditTrail(values, userid, EmployDetController.tableName, EmployDetController.tableColumns);
        }

        private void AppendUpdateAuditTrail(long sno, EMP_DET oldEmployee, EMP_DET newEmployee, long userid)
        {
            var oldValues = new List<string> { sno.ToString(), oldEmployee.Emp_Id_No,oldEmployee.Full_Name,oldEmployee.First_Name, oldEmployee.Middle_name,  oldEmployee.Last_name,oldEmployee.Desg_name.ToString(),oldEmployee.Email_Address,
                                oldEmployee.Mobile_No,oldEmployee.Created_Date.ToString(),oldEmployee.Expiry_Date.ToString(), oldEmployee.Emp_Status,oldEmployee.AuditBy,oldEmployee.Audit_Date.ToString(),oldEmployee.User_name };

            var newValues = new List<string> { sno.ToString(), newEmployee.Emp_Id_No,newEmployee.Full_Name,newEmployee.First_Name, newEmployee.Middle_name,  newEmployee.Last_name,newEmployee.Desg_name,newEmployee.Email_Address,
                                newEmployee.Mobile_No,newEmployee.Created_Date.ToString(),newEmployee.Expiry_Date.ToString(), newEmployee.Emp_Status,userid.ToString(), DateTime.Now.ToString(),newEmployee.User_name };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, EmployDetController.tableName, EmployDetController.tableColumns);
        }

        private void AppendDeleteAuditTrail(long sno, EMP_DET employee, long userid)
        {
            var values = new List<string> { employee.Detail_Id.ToString(), employee.Emp_Id_No,employee.Full_Name,employee.First_Name, employee.Middle_name,  employee.Last_name,employee.Desg_Id.ToString(),employee.Email_Address,
                                                        employee.Mobile_No,employee.Created_Date.ToString(),employee.Expiry_Date.ToString(), employee.Emp_Status,employee.AuditBy,employee.Audit_Date.ToString(),employee.User_name };
            Auditlog.deleteAuditTrail(values, userid, EmployDetController.tableName, EmployDetController.tableColumns);
        }


        [HttpGet]
        public HttpResponseMessage GetdesgDetails()
        {
            DESIGNATION designation = new DESIGNATION();
            try
            {
                var results = designation.GetDesignation();
                return this.GetList<List<DESIGNATION>, DESIGNATION>(results);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetEmpDetails()
        {
            EMP_DET employDet = new EMP_DET();
            try
            {
                var results = employDet.GetEMP();
                return this.GetList<List<EMP_DET>, EMP_DET>(results);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private string GetUserFullname(string firstname,string middlename,string lastname)
        {
            if (middlename == null && lastname != null)
            {
                return firstname.Trim() + " " + lastname.Trim();
            }
            else
            {
                return firstname.Trim() + " " + middlename.Trim() + " " + lastname.Trim();
            }
        }

        private EMP_DET CreateEmployeDetail(AddBankUserForm addBankUserForm,DESIGNATION designation)
        {
            EMP_DET employeeDetails = new EMP_DET();
            employeeDetails.Emp_Id_No = addBankUserForm.empid;
            employeeDetails.First_Name = addBankUserForm.fname;
            employeeDetails.Middle_name = addBankUserForm.mname;
            employeeDetails.Last_name = addBankUserForm.lname;
            employeeDetails.Full_Name = GetUserFullname(addBankUserForm.fname, addBankUserForm.mname, addBankUserForm.lname);
            employeeDetails.Desg_Id = (long)addBankUserForm.desg;
            employeeDetails.Desg_name = designation.Desg_Name;
            employeeDetails.Email_Address = addBankUserForm.email;
            employeeDetails.Mobile_No = addBankUserForm.mobile;
            employeeDetails.User_name = addBankUserForm.user;
            employeeDetails.Emp_Status = addBankUserForm.gender;
            employeeDetails.Branch_Sno = addBankUserForm.branch;
            employeeDetails.AuditBy = addBankUserForm.userid.ToString();
            employeeDetails.Created_Date = DateTime.Now;
            employeeDetails.Expiry_Date = DateTime.Now.AddMonths(3);
            employeeDetails.Detail_Id = (long)addBankUserForm.sno;
            var password = EmployDetController.GetEncryptedData(new Password().Next());
            employeeDetails.Password = password;
            return employeeDetails;
        }

        private HttpResponseMessage InsertBankUser(EMP_DET employee,AddBankUserForm addBankUserForm)
        {
            try
            {
                bool existsEmpId = employee.Validateuser(addBankUserForm.empid);
                if (existsEmpId)
                {
                    var messages = new List<string> { "Employee Id exists" };
                    this.GetCustomErrorMessageResponse(messages);
                }
                bool exitsUsername = employee.Validateduplicate(addBankUserForm.user);
                if (exitsUsername)
                {
                    var messages = new List<string> { "Username exists" };
                    this.GetCustomErrorMessageResponse(messages);
                }
                long addedEmployee = employee.AddEMP(employee);
                var fullName = GetUserFullname(addBankUserForm.fname, addBankUserForm.mname, addBankUserForm.lname);
                SendActivationEmail(employee.Email_Address, employee.Full_Name, employee.Password,employee.User_name);
                AppendInsertAuditTrail(addedEmployee, employee, (long) addBankUserForm.userid);
                return FindEmployee(addedEmployee);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private HttpResponseMessage UpdateBankUser(EMP_DET employee,AddBankUserForm addBankUserForm)
        {
            try
            {
                bool isExist = employee.isExistEmployee((long) addBankUserForm.sno);
                if (!isExist) return this.GetNotFoundResponse();
                bool isDuplicateEmployeeId = employee.isDuplicateEmployeeId(addBankUserForm.empid, (long)addBankUserForm.sno);
                if (isDuplicateEmployeeId)
                {
                    var messages = new List<string> { "Employee Id exists" };
                    this.GetCustomErrorMessageResponse(messages);
                }
                bool isDuplicateUsername = employee.isDuplicateEmployeeUsername(addBankUserForm.user, (long)addBankUserForm.sno);
                if (isDuplicateUsername)
                {
                    var messages = new List<string> { "Username exists" };
                    this.GetCustomErrorMessageResponse(messages);
                }
                EMP_DET found = employee.FindEmployee((long)addBankUserForm.sno);
                long updatedEmployee = employee.UpdateEmployee(employee);
                AppendUpdateAuditTrail(updatedEmployee, found, employee, (long)addBankUserForm.userid);
                return FindEmployee(updatedEmployee);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddEmp(AddBankUserForm addBankUserForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                DESIGNATION designation = new DESIGNATION();
                DESIGNATION foundDesignation = designation.Editdesignation((long)addBankUserForm.desg);
                if (foundDesignation == null)
                {
                    var messages = new List<string> { "Designation not found" };
                    this.GetCustomErrorMessageResponse(messages);
                }
                EMP_DET employeeDetail = CreateEmployeDetail(addBankUserForm,foundDesignation);
                if ((long)addBankUserForm.sno == 0) { return InsertBankUser(employeeDetail, addBankUserForm); }
                else { return UpdateBankUser(employeeDetail,addBankUserForm);  }
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetEmpindivi(GetEmployeeForm getEmployeeForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            return FindEmployee((long) getEmployeeForm.sno);
        }

        [HttpGet]
        public HttpResponseMessage FindEmployee(long sno)
        {
            try
            {
                EMP_DET employee = new EMP_DET();
                bool isExist = employee.isExistEmployee(sno);
                if (!isExist) return this.GetNotFoundResponse();
                EMP_DET found = employee.FindEmployee(sno);
                return this.GetSuccessResponse(found);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost] 
        public HttpResponseMessage DeleteEmployee(DeleteBankUserForm deleteBankUserForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                EMP_DET employee = new EMP_DET();
                bool isExists = employee.isExistEmployee((long) deleteBankUserForm.sno);
                if (!isExists) return this.GetNotFoundResponse();
                AppendDeleteAuditTrail((long)deleteBankUserForm.sno, employee, (long)deleteBankUserForm.userid);
                employee.DeleteEMP((long)deleteBankUserForm.sno);
                return this.GetSuccessResponse((long)deleteBankUserForm.sno);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}