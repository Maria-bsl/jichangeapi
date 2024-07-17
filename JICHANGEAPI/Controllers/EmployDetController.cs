
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
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

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployDetController : ApiController
    {
        private readonly List<string> tableColumns = new List<string> { "emp_detail_id", "emp_id_no","fullname","first_name", "middle_name", "last_name", "desg_id","email_id", "mobile_no",
            "created_date","expiry_date", "emp_status","posted_by", "posted_date","username"};

        private static string GetEncryptedData(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

        private static void SendActivationEmail(String email, String fullname, String pwd, String uname)
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
                    var data = em.getEMAILst("2");
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

        [HttpGet]
        public HttpResponseMessage GetdesgDetails()
        {
            try
            {
                var dg = new DESIGNATION();
                var result = dg.GetDesignation();
                return Request.CreateResponse(new { response = result, message = new List<string>() });
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }

        [HttpPost]
        public HttpResponseMessage GetEmpDetails()
        {
            try
            {
                var empDetails = new EMP_DET();
                var results = empDetails.GetEMP();
                return Request.CreateResponse(new { response = results, message = new List<string>() });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }

        [HttpPost]
        public HttpResponseMessage AddEmp(AddBankUserForm addBankUserForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDetails = new EMP_DET();
                    employeeDetails.Emp_Id_No = ((long)addBankUserForm.empid).ToString();
                    employeeDetails.First_Name = addBankUserForm.fname;
                    employeeDetails.Middle_name = addBankUserForm.mname;
                    employeeDetails.Last_name = addBankUserForm.lname;
                    employeeDetails.Full_Name = addBankUserForm.fname + " " + (addBankUserForm.mname != null ? addBankUserForm.mname + " " + addBankUserForm.lname : addBankUserForm.lname);
                    employeeDetails.Desg_Id = (long)addBankUserForm.desg;
                    employeeDetails.Email_Address = addBankUserForm.email;
                    employeeDetails.Mobile_No = addBankUserForm.mobile;
                    employeeDetails.User_name = addBankUserForm.user;
                    employeeDetails.Emp_Status = addBankUserForm.gender;
                    employeeDetails.Branch_Sno = addBankUserForm.branch;
                    employeeDetails.AuditBy = addBankUserForm.userid.ToString();
                    employeeDetails.Created_Date = System.DateTime.Now;
                    employeeDetails.Expiry_Date = System.DateTime.Now.AddMonths(3);
                    employeeDetails.Detail_Id = (long)addBankUserForm.sno;
                    if ((long)addBankUserForm.sno == 0)
                    {
                        var existsEmpId = employeeDetails.Validateuser(((long)addBankUserForm.empid).ToString());
                        var exitsUsername = employeeDetails.Validateduplicate(addBankUserForm.user);
                        if (existsEmpId)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Employee Id is already in use." } });
                        }
                        if (exitsUsername)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Username is already in use." } });
                        }
                        var designation = new DESIGNATION();
                        var foundDesignation = designation.Editdesignation((long)addBankUserForm.desg);
                        if (foundDesignation != null)
                        {
                            var passwordGenerator = new Password();
                            var password = EmployDetController.GetEncryptedData(passwordGenerator.Next());
                            employeeDetails.Password = password;
                            var addedEmployee = employeeDetails.AddEMP(employeeDetails);
                            EmployDetController.SendActivationEmail(addBankUserForm.email, addBankUserForm.fname, password, addBankUserForm.user);
                            var values = new List<string> { addedEmployee.ToString(), employeeDetails.Emp_Id_No,employeeDetails.Full_Name,employeeDetails.First_Name, employeeDetails.Middle_name,  employeeDetails.Last_name,foundDesignation.Desg_Name.ToString(),employeeDetails.Email_Address,
                                employeeDetails.Mobile_No,employeeDetails.Created_Date.ToString(),employeeDetails.Expiry_Date.ToString(), employeeDetails.Emp_Status,addBankUserForm.userid.ToString(), DateTime.Now.ToString(),employeeDetails.User_name };
                            Auditlog.insertAuditTrail(values, (long)addBankUserForm.userid, "Bank User", tableColumns);
                            return Request.CreateResponse(new { response = addedEmployee, message = new List<string>() });
                        }
                        else
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Invalid designation." } });
                        }
                    }
                    else
                    {
                        var designation = new DESIGNATION();
                        var foundDesignation = designation.Editdesignation((long)addBankUserForm.desg);
                        if (foundDesignation == null)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Invalid designation." } });
                        }
                        var employee = employeeDetails.EditEMP((long)addBankUserForm.sno);
                        if (employee == null)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Bank user deos not exist." } });
                        }
                        employeeDetails.UpdateEMP(employeeDetails);
                        var oldValues = new List<string> { employee.Detail_Id.ToString(), employee.Emp_Id_No,employee.Full_Name,employee.First_Name, employee.Middle_name,  employee.Last_name,employee.Desg_name.ToString(),employee.Email_Address,
                                employee.Mobile_No,employee.Created_Date.ToString(),employee.Expiry_Date.ToString(), employee.Emp_Status,employee.AuditBy,employee.Audit_Date.ToString(),employee.User_name };

                        var newValues = new List<string> { ((long) addBankUserForm.sno).ToString(), employeeDetails.Emp_Id_No,employeeDetails.Full_Name,employeeDetails.First_Name, employeeDetails.Middle_name,  employeeDetails.Last_name,foundDesignation.Desg_Name.ToString(),employeeDetails.Email_Address,
                                employeeDetails.Mobile_No,employeeDetails.Created_Date.ToString(),employeeDetails.Expiry_Date.ToString(), employeeDetails.Emp_Status,((long) addBankUserForm.userid).ToString(), DateTime.Now.ToString(),employeeDetails.User_name };
                        Auditlog.updateAuditTrail(oldValues, newValues, (long)addBankUserForm.sno, "Bank User", tableColumns);
                        return Request.CreateResponse(new { response = (long)addBankUserForm.sno, message = new List<string>() });
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
        public HttpResponseMessage GetEmpindivi(GetEmployeeForm getEmployeeForm)
        {
            try
            {
                var employeeDetails = new EMP_DET();
                var result = employeeDetails.getEMPText((long) getEmployeeForm.sno);
                return Request.CreateResponse(new { response = employeeDetails, message = new List<string>() });
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }
    }
}