using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class EMP_DET
    {
        #region Properties
        public long Detail_Id { get; set; }
        public long? Branch_Sno { get; set; }
        public string Emp_Id_No { get; set; }
        public string Full_Name { get; set; }
        public string Branch_Name { get; set; }
        public string First_Name { get; set; }
        public string Middle_name { get; set; }
        public string Last_name { get; set; }
        public string Desg_name { get; set; }
        public string User_name { get; set; }
        public string Loc_Name { get; set; }
        public string Flag { get; set; }
        public DateTime C_time { get; set; }

        public long Desg_Id { get; set; }

        public string Email_Address { get; set; }
        public string Password { get; set; }
        public string Mobile_No { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Expiry_Date { get; set; }
        public string F_Login { get; set; }
        public int SNO { get; set; }
        public string Q_Name { get; set; }
        public string Q_Ans { get; set; }
        public String App_Status { get; set; }
        public int Log_Att { get; set; }
        public DateTime? Log_Time { get; set; }
        public String Log_Status { get; set; }
        public String Emp_Status { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods

        public long AddEMP(EMP_DET sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                emp_detail ps = new emp_detail()
                {
                    emp_id_no = sc.Emp_Id_No,
                    full_name = sc.Full_Name,
                    first_name = sc.First_Name,
                    middle_name = sc.Middle_name,
                    last_name = sc.Last_name,
                    desg_id = sc.Desg_Id,
                    username = sc.User_name,
                    mobile_no = sc.Mobile_No,
                    email_id = sc.Email_Address,
                    branch_Sno = sc.Branch_Sno,
                    pwd = sc.Password,
                    created_date = sc.Created_Date,
                    expiry_date = sc.Expiry_Date,
                    f_login = "false",
                    app_status = sc.App_Status,
                    emp_status = sc.Emp_Status,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.emp_detail.Add(ps);
                context.SaveChanges();
                return ps.emp_detail_id;
            }
        }

        public void UpdateOnlyflsg(EMP_DET dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail.Where(c => c.emp_detail_id == dep.Detail_Id)
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    //UpdateContactInfo.fla= "false";
                    context.SaveChanges();
                }
            }
        }
        public bool Validatepwdbank(string name, long Userid)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                var validationemp = (from c in context.emp_detail
                                     where (c.pwd == name && c.f_login == "false" && c.emp_detail_id == Userid)
                                     select c);

                if (validationemp.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public int GetCount()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.emp_detail
                                where c.emp_status == "Active"
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        public int GetEmployeeUserCountByBranch(long branch)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.emp_detail
                                where c.emp_status == "Active" && c.branch_Sno == branch
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        public EMP_DET FPassword(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                var edetails = (from c in context.emp_detail
                                where c.username == name && c.emp_status == "Active"
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    First_Name = c.first_name,
                                    Middle_name = c.middle_name,
                                    Last_name = c.last_name,
                                    User_name = c.username,
                                    Mobile_No = c.mobile_no,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public EMP_DET FPasswordE(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                var edetails = (from c in context.emp_detail
                                where c.email_id == name && c.emp_status == "Active"
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    First_Name = c.first_name,
                                    Middle_name = c.middle_name,
                                    Last_name = c.last_name,
                                    User_name = c.username,
                                    Mobile_No = c.mobile_no,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public List<EMP_DET> GetEMP()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.emp_detail
                                join d in context.designation_list on c.desg_id equals d.desg_id
                                
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    First_Name = c.first_name,
                                    Desg_name = d.desg_name,
                                    Middle_name = c.middle_name,
                                    Last_name = c.last_name,
                                    User_name = c.username,
                                    Mobile_No = c.mobile_no,
                                   // Branch_Name = br.branch_name1,
                                    Branch_Sno = c.branch_Sno,
                                    Desg_Id = (long)c.desg_id,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    Created_Date = c.created_date,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status,
                                    Audit_Date = c.posted_date,
                                }).OrderByDescending(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public EMP_DET FindEmployee(long employeeId)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var exists = context.emp_detail.Find(employeeId);
                if (exists == null)
                {
                    return null;
                }
                var designation = new DESIGNATION();
                var branch = new BranchM();
                var employee = new EMP_DET();
                employee.Detail_Id = exists.emp_detail_id;
                employee.Emp_Id_No = exists.emp_id_no;
                employee.Full_Name = exists.full_name;
                employee.First_Name = exists.first_name;
                employee.Middle_name = exists.middle_name;
                employee.Last_name = exists.last_name;
                employee.User_name = exists.username;
                employee.Mobile_No = exists.mobile_no;
                employee.Branch_Sno = exists.branch_Sno;
                employee.Desg_Id = (long) exists.desg_id;
                employee.Desg_name = designation.GetDesignation().Find(e => e.Desg_Id == exists.desg_id).Desg_Name;
                //employee.Branch_Name = branch.GetBranches().Find(e => e.Branch_Sno == exists.branch_Sno).Name;
                employee.Email_Address = exists.email_id;
                employee.Created_Date = exists.created_date;
                employee.F_Login = exists.f_login;
                employee.Emp_Status = exists.emp_status;
                employee.Audit_Date = exists.posted_date;
                return employee;
            }
        }

        public List<EMP_DET> GetEMPAct()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.emp_detail
                                join d in context.designation_list on c.desg_id equals d.desg_id
                              //  join br in context.branch_name on c.branch_sno equals br.sno
                                where c.emp_status == "Active"
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    // Branch_Name = br.branch_name1,
                                    Branch_Sno = c.branch_Sno,
                                    First_Name = c.first_name,
                                    Desg_name = d.desg_name,
                                    Middle_name = c.middle_name,
                                    Last_name = c.last_name,
                                    User_name = c.username,
                                    Mobile_No = c.mobile_no,
                                    Desg_Id = (long)c.desg_id,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    Created_Date = c.created_date,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status,
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public bool isDuplicateEmployeeId(string employeeId,long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.emp_id_no.ToLower().Equals(employeeId.ToLower()) && c.sno != sno)
                                  select c);
                return validation.Count() > 0;
            }
        }
        public bool isDuplicateEmployeeUsername(string username,long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.username.ToLower().Equals(username.ToLower()) && c.sno != sno)
                                  select c);
                return validation.Count() > 0;
            }
        }
        public bool Validateuser(String id)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.emp_id_no.ToLower().Equals(id))
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool Validateduplicate(String id)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.username == id)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        //public bool ValidateLoginAlreadyExist(long sno)
        //{
        //    using (BIZINVOICEEntities context = new BIZINVOICEEntities())
        //    {
        //        var validation = (from c in context.emp_detail.Where(c =>c.flag == "true" && c.emp_detail_id == sno)
        //                          select c);
        //        if (validation.Count() > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //}
        //public EMP_DET Validdateloginornot(long chsno)
        //{
        //    using (BIZINVOICEEntities context = new BIZINVOICEEntities())
        //    {
        //        var edetails = (from c in context.emp_detail.Where(c=>c.emp_detail_id == chsno && c.emp_status == "Active")
        //                        select new EMP_DET
        //                        {
        //                            Flag=c.flag,
        //                            C_time=(DateTime)c.ctime,
        //                        }).FirstOrDefault();
        //        if (edetails != null)
        //            return edetails;
        //        else
        //            return null;
        //    }
        //}
        public EMP_DET getEMPText(long chsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.emp_detail
                                join d in context.designation_list on c.desg_id equals d.desg_id
                            //    join br in context.branch_name on c.branch_sno equals br.sno
                                where c.emp_detail_id == chsno && c.emp_status == "Active"
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    //  Branch_Name = br.branch_name1,
                                    Branch_Sno = c.branch_Sno,
                                    First_Name = c.first_name,
                                    Middle_name = c.middle_name,
                                    Last_name = c.last_name,
                                    Desg_Id = (long)c.desg_id,
                                    Desg_name = d.desg_name,
                                    User_name = c.username,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    Created_Date = c.created_date,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public EMP_DET EditEMP(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.emp_detail
                                join d in context.designation_list on c.desg_id equals d.desg_id
                             //   join br in context.branch_name on c.branch_sno equals br.sno
                                where c.emp_detail_id == sno
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    First_Name = c.first_name,
                                    //  Branch_Name = br.branch_name1,
                                    Branch_Sno = c.branch_Sno,
                                    Middle_name = c.middle_name,
                                    Mobile_No = c.mobile_no,
                                    Last_name = c.last_name,
                                    User_name = c.username,
                                    Desg_Id = (long)c.desg_id,
                                    Desg_name = d.desg_name,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    Created_Date = c.created_date,
                                    Expiry_Date = c.expiry_date,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status,
                                    AuditBy = c.posted_by,
                                    Audit_Date = c.posted_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public bool ValidateuserId(String id)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.username == id && c.emp_status == "Approved")
                                  select c);
                //var validationTU = (from c in context.institution_users
                //                    where (c.username == id)
                //                    select c);
                //var validationM = (from c in context.member_registration
                //                   where (c.username == id && c.status == "Approved")
                //                   select c);
                if (validation.Count() > 0 )
                    return true;
                else
                    return false;
            }
        }
        public bool Validateuserpwd(String id)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.pwd == id)
                                  select c);
               
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        //public bool ValidateuserpwdInst(String id, long sno,long uid)
        //{
        //    using (BIZINVOICEEntities context = new BIZINVOICEEntities())
        //    {
               
        //        var validationTu = (from c in context.institution_users
        //                            where (c.password == id && c.insti_reg_sno == sno && c.insti_users_sno==uid)
        //                            select c);
                
        //        if (validationTu.Count() > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //}

        //public bool ValidateuserpwdMeg(String id, long sno,long uid)
        //{
        //    using (BIZINVOICEEntities context = new BIZINVOICEEntities())
        //    {

        //        var validationPwd = (from c in context.member_registration
        //                             where (c.password == id && c.insti_reg_sno == sno && c.member_reg_sno==uid)
        //                             select c);
        //        if (validationPwd.Count() > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //}


        public EMP_DET CheckUserBank(string mobile)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.emp_detail
                                    //join c1 in context.company_master on c.co
                                where c.mobile_no == mobile
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    First_Name = c.first_name,
                                    Middle_name = c.middle_name,
                                    Mobile_No = c.mobile_no,
                                    Last_name = c.last_name,
                                    User_name = c.username,
                                    Desg_Id = (long)c.desg_id,
                                    Expiry_Date = c.expiry_date,
                                    Branch_Sno = c.branch_Sno,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    Created_Date = c.created_date,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }

        public EMP_DET CheckLogin(String uname, String pwd)
        {

            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                var edetails = (from c in context.emp_detail
                                //join c1 in context.company_master on c.co
                                where c.username == uname && c.pwd == pwd && c.emp_status == "Active" && c.log_status == null
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    First_Name = c.first_name,
                                    Middle_name = c.middle_name,
                                    Mobile_No = c.mobile_no,
                                    Last_name = c.last_name,
                                    User_name = c.username,
                                    Desg_Id = (long)c.desg_id,
                                    Expiry_Date = c.expiry_date,
                                    Branch_Sno = c.branch_Sno,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    Created_Date = c.created_date,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteEMP(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.emp_detail
                                   where n.emp_detail_id == no
                                   select n).First();

                if (noteDetails != null)
                {
                    context.emp_detail.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateEMP(EMP_DET dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.emp_id_no = dep.Emp_Id_No;
                    UpdateContactInfo.full_name = dep.Full_Name;
                    UpdateContactInfo.first_name = dep.First_Name;
                    UpdateContactInfo.middle_name = dep.Middle_name;
                    UpdateContactInfo.last_name = dep.Last_name;
                    UpdateContactInfo.desg_id = dep.Desg_Id;
                    UpdateContactInfo.emp_status = dep.Emp_Status;
                    UpdateContactInfo.username = dep.User_name;
                    UpdateContactInfo.email_id = dep.Email_Address;
                    UpdateContactInfo.mobile_no = dep.Mobile_No;
                    UpdateContactInfo.branch_Sno = dep.Branch_Sno;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }

        public long UpdateEmployee(EMP_DET data)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var employee = context.emp_detail.Find(data.Detail_Id);
                if (employee != null)
                {
                    employee.emp_id_no = data.Emp_Id_No;
                    employee.full_name = data.Full_Name;
                    employee.first_name = data.First_Name;
                    employee.middle_name = data.Middle_name;
                    employee.last_name = data.Last_name;
                    employee.desg_id = data.Desg_Id;
                    employee.emp_status = data.Emp_Status;
                    employee.username = data.User_name;
                    employee.email_id = data.Email_Address;
                    employee.mobile_no = data.Mobile_No;
                    employee.branch_Sno = data.Branch_Sno;
                    employee.posted_by = data.AuditBy;
                    employee.posted_date = DateTime.Now;
                    context.SaveChanges();
                    return data.Detail_Id;
                }
                return 0;
            }
        } 

        public void Updatelang(EMP_DET dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.localization = dep.Loc_Name;
                    context.SaveChanges();
                }
            }
        }
        public void UpdateQuestionEMP(EMP_DET dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id && u.f_login == "false"
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                  /*  UpdateContactInfo.sno = dep.SNO;
                    UpdateContactInfo.q_name = dep.Q_Name;
                    UpdateContactInfo.q_ans = dep.Q_Ans;*/
                    UpdateContactInfo.pwd = dep.Password;
                    UpdateContactInfo.f_login = dep.F_Login;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        //public void UpdateOnlyflsg(EMP_DET dep)
        //{
        //    using (BIZINVOICEEntities context = new BIZINVOICEEntities())
        //    {
        //        var UpdateContactInfo = (from u in context.emp_detail.Where(c => c.emp_detail_id == dep.Detail_Id)
        //                                 select u).FirstOrDefault();

        //        if (UpdateContactInfo != null)
        //        {
        //            UpdateContactInfo.flag = "false";
        //            context.SaveChanges();
        //        }
        //    }
        //}
        public void UpdateOnlypwd(EMP_DET dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail.Where(c => c.emp_detail_id == dep.Detail_Id)
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.pwd =dep.Password;
                    context.SaveChanges();
                }
            }
        }
        public void UpdateOnlyflsgtrue(EMP_DET dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail.Where(c => c.emp_detail_id == dep.Detail_Id)
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    //UpdateContactInfo.flag = "true";
                    //UpdateContactInfo.ctime = DateTime.Now;
                    UpdateContactInfo.log_att = 0;
                    context.SaveChanges();
                }
            }
        }
        public EMP_DET validatelang(long sno)
        {

            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                var edetails = (from c in context.emp_detail.Where(c => c.emp_detail_id == sno && c.emp_status == "Active")
                                select new EMP_DET
                                {
                                    Loc_Name = c.localization,

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }

        public bool isExistEmployee(long employeeId)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var exists = context.emp_detail.Find(employeeId);
                return exists != null;
            }
        }

        public EMP_DET CheckuserLogStatbnk(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                var edetails = (from c in context.emp_detail
                                where c.username == name && c.emp_status == "Active"
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Log_Time = c.log_time,
                                    Log_Status = c.log_status
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void UpdateLogDatebnk(EMP_DET dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.log_att = dep.Log_Att;
                    context.SaveChanges();
                }
            }
        }

        public void Updatelogattbnk(EMP_DET dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.log_att = dep.Log_Att;
                    UpdateContactInfo.log_time = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }

        public void BlockUserbnk(EMP_DET dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.log_status = dep.Log_Status;
                    context.SaveChanges();
                }
            }
        }
        public void UnblockUserbnk(EMP_DET dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.log_status = null;
                    UpdateContactInfo.log_att = 0;
                    UpdateContactInfo.f_login = "false";
                    UpdateContactInfo.pwd = dep.Password;
                    context.SaveChanges();
                }
            }
        }
        public EMP_DET Checkuserattbnk(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                var edetails = (from c in context.emp_detail
                                where c.username == name && c.emp_status == "Active" && c.log_att != null
                                select new EMP_DET
                                {
                                    Log_Att = (int)c.log_att,
                                    Log_Status = c.log_status
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public bool Validateuserblo(long usno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.emp_detail
                                  join det in context.designation_list on c.desg_id equals det.desg_id
                                  where c.emp_detail_id == usno && det.desg_name == "Administrator"
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        #endregion Methods
    }
}
