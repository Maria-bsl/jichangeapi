using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BIZINVOICING.BusinessEntities.Common;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class CompanyUsers
    {
        #region properties

        public long CompuserSno { get; set; }
        public long Compmassno { get; set; }
        public String Username { get; set; }
        public String Loc_Name { get; set; }
        public string pwd { get; set; }
        public String Password { get; set; }
        public String Descript { get; set; }
        public String  Usertype { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public String Flogin { get; set; }
        public long Sno { get; set; }
        public String Qname { get; set; }
        public String Qans { get; set; }
        public int Logatt { get; set; }
        public DateTime LogTime { get; set; }
        public string LogStatus { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Localization { get; set; }
        public string Flag { get; set; }
        public DateTime Ctime { get; set; }
        public string Userpos { get; set; }
        public int Mail_sta { get; set; }
        public string PostedBy { get; set; }
        public DateTime PostedDate {get; set; }
        #endregion properties

        public long AddCompanyUsers(CompanyUsers sc)
        {

            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                company_users pc = new company_users()
                {
                    comp_users_sno = sc.CompuserSno,
                    comp_mas_sno = sc.Compmassno,
                    username = sc.Username,
                    password = sc.Password,
                    user_type = sc.Usertype,
                    created_date = sc.CreatedDate,
                    expiry_date = sc.ExpiryDate,
                    f_login = sc.Flogin,
                    //sno = sc.Sno,
                    //q_name = sc.Qname,
                    //q_ans = sc.Qans,
                    //log_att = sc.Logatt,
                    //log_time = sc.LogTime,
                    //log_status = sc.LogStatus,
                    user_fullname = sc.Fullname,
                    email_address = sc.Email,
                    mobile_no = sc.Mobile,
                    //localization = sc.Localization,
                    //flag = sc.Flag,
                    //ctime = sc.Ctime,
                    user_position = sc.Userpos,
                    //mail_status = sc.Mail_sta,
                    posted_by = sc.PostedBy,
                    posted_date = DateTime.Now
                };
                context.company_users.Add(pc);
                context.SaveChanges();
                return pc.comp_users_sno;
            }
        }
        public long AddCompanyUsers1(CompanyUsers sc)
        {

            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                company_users pc = new company_users()
                {
                    comp_users_sno = sc.CompuserSno,
                    comp_mas_sno = sc.Compmassno,
                    username = sc.Username,
                    password = sc.Password,
                    user_type = sc.Usertype,
                    f_login=sc.Flogin,
                    created_date = sc.CreatedDate,
                    expiry_date = sc.ExpiryDate,
                    email_address = sc.Email,
                    mobile_no = sc.Mobile,
                    posted_date = sc.PostedDate,
                    posted_by = sc.PostedBy
                };
                context.company_users.Add(pc);
                context.SaveChanges();
                return pc.comp_users_sno;
            }
        }
        public CompanyUsers validatelangInst(long uno, long isno)
        {

            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {


                var edetails = (from c in context.company_users.Where(c => c.comp_users_sno == uno)
                                select new CompanyUsers
                                {
                                    Loc_Name = c.localization,

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                
                    else
                        return null;
                
            }
        }


        public void Updatelang(CompanyUsers dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.company_users
                                         where u.comp_users_sno == dep.CompuserSno
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.localization = dep.Loc_Name;
                    context.SaveChanges();
                }
            }
        }


        public CompanyUsers validatelangInst(long uno, long isno, string van)
        {

            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                
                    var edetails = (from c in context.company_users.Where(c => c.comp_users_sno == uno)
                                    select new CompanyUsers
                                    {
                                        Loc_Name = c.localization,

                                    }).FirstOrDefault();
                    if (edetails != null)
                        return edetails;
                    else return null;
                
            }

        }


        public CompanyUsers GetCompanyUsers(long company_sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_users
                                where sc.comp_mas_sno == company_sno
                                select new CompanyUsers
                                {
                                    CompuserSno = sc.comp_users_sno,
                                    Username = sc.username,
                                    //Descript=c.descript,
                                    Password = sc.password,
                                    Usertype = sc.user_type,
                                    Fullname = sc.user_fullname,
                                    Email = sc.email_address,
                                    Mobile = sc.mobile_no,
                                    Userpos = sc.user_position

                                }).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }


        public List<CompanyUsers> GetCompanyUsers()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_users
                                //join c in context.roles_master on sc.user_type equals c.code
                                select new CompanyUsers
                                {
                                    CompuserSno = sc.comp_users_sno,
                                    //Compmassno = (long)sc.comp_mas_sno,
                                    Username = sc.username,
                                   //Descript=c.descript,
                                    //Password = sc.password,
                                    Usertype = sc.user_type,
                                    //CreatedDate = (DateTime)sc.created_date,
                                    //ExpiryDate = (DateTime)sc.expiry_date,
                                    //Flogin = sc.f_login,
                                    //Sno = (long)sc.sno,
                                    //Qname = sc.q_name,
                                    //Qans = sc.q_ans,
                                    //Logatt = (int)sc.log_att,
                                    //LogTime = (DateTime)sc.log_time,
                                    //LogStatus = sc.log_status,
                                    Fullname = sc.user_fullname,
                                    Email = sc.email_address,
                                    Mobile = sc.mobile_no,
                                    //Localization = sc.localization,
                                    //Flag = sc.flag,
                                    //Ctime = (DateTime)sc.ctime,
                                    //Userpos = sc.user_position,
                                    //Mail_sta = (int)sc.mail_status,
                                    //PostedBy = sc.posted_by,
                                    //PostedDate = (DateTime)sc.posted_date

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyUsers> GetCompanyUsers1(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_users
                                where sc.comp_mas_sno == cno
                                    //join c in context.roles_master on sc.user_type equals c.code
                                select new CompanyUsers
                                {
                                    CompuserSno = sc.comp_users_sno,
                                    //Compmassno = (long)sc.comp_mas_sno,
                                    Username = sc.username,
                                    //Descript=c.descript,
                                    //Password = sc.password,
                                    Usertype = sc.user_type,
                                    //CreatedDate = (DateTime)sc.created_date,
                                    //ExpiryDate = (DateTime)sc.expiry_date,
                                    //Flogin = sc.f_login,
                                    //Sno = (long)sc.sno,
                                    //Qname = sc.q_name,
                                    //Qans = sc.q_ans,
                                    //Logatt = (int)sc.log_att,
                                    //LogTime = (DateTime)sc.log_time,
                                    //LogStatus = sc.log_status,
                                    Fullname = sc.user_fullname,
                                    Email = sc.email_address,
                                    Mobile = sc.mobile_no,
                                    //Localization = sc.localization,
                                    //Flag = sc.flag,
                                    //Ctime = (DateTime)sc.ctime,
                                    Userpos = sc.user_position,
                                    //Mail_sta = (int)sc.mail_status,
                                    //PostedBy = sc.posted_by,
                                    //PostedDate = (DateTime)sc.posted_date

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }


        public CompanyUsers GetCompanyid(long uno)
        {

            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {


                var edetails = (from c in context.company_users.Where(c => c.comp_users_sno == uno)
                                select new CompanyUsers
                                {
                                    Compmassno = (long)c.comp_mas_sno,

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;

                else
                    return null;

            }
        }


        public CompanyUsers CheckUser(string mobile)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from sc in context.company_users
                                where sc.mobile_no == mobile
                                select new CompanyUsers
                                {
                                    CompuserSno = sc.comp_users_sno,
                                    Compmassno = (long)sc.comp_mas_sno,
                                    Username = sc.username,
                                    //Password = sc.password,
                                    Usertype = sc.user_type,
                                    //CreatedDate = (DateTime)sc.created_date,
                                    //ExpiryDate = (DateTime)sc.expiry_date,
                                    //Flogin = sc.f_login,
                                    //Sno = (long)sc.sno,
                                    //Qname = sc.q_name,
                                    //Qans = sc.q_ans,
                                    //Logatt = (int)sc.log_att,
                                    //LogTime = (DateTime)sc.log_time,
                                    //LogStatus = sc.log_status,
                                    Fullname = sc.user_fullname,
                                    Email = sc.email_address,
                                    Mobile = sc.mobile_no,
                                    //Localization = sc.localization,
                                    //Flag = sc.flag,
                                    //Ctime = (DateTime)sc.ctime,
                                    Userpos = sc.user_position,
                                    //Mail_sta = (int)sc.mail_status,
                                    //PostedBy = sc.posted_by,
                                    //PostedDate = (DateTime)sc.posted_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;

            }
        }

        public CompanyUsers CheckLogin(String uname, String pwd)
        {

            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                var edetails = (from c in context.company_users
                                    join c1 in context.company_master on c.comp_mas_sno equals c1.comp_mas_sno
                                where c.username == uname && c.password == pwd && c1.status.ToLower().Equals("approved") && c.log_status == null
                                select new CompanyUsers
                                {
                                    Compmassno = (long)c.comp_mas_sno,
                                    CompuserSno = c.comp_users_sno,
                                    Usertype = c.user_type,
                                    Fullname = c.user_fullname,
                                    Sno = (long)c1.branch_sno,
                                    Email = c.email_address,
                                    Flogin = c.f_login,
                                    Username = c.username
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public CompanyUsers EditCompanyUsers(long sno)//according to use we can use sno
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from sc in context.company_users
                                where sc.comp_users_sno == sno
                                select new CompanyUsers
                                {
                                    CompuserSno = sc.comp_users_sno,
                                    Compmassno = (long)sc.comp_mas_sno,
                                    Username = sc.username,
                                    Password = sc.password,
                                    Usertype = sc.user_type,
                                    //CreatedDate = (DateTime)sc.created_date,
                                    //ExpiryDate = (DateTime)sc.expiry_date,
                                    //Flogin = sc.f_login,
                                    //Sno = (long)sc.sno,
                                    //Qname = sc.q_name,
                                    //Qans = sc.q_ans,
                                    //Logatt = (int)sc.log_att,
                                    //LogTime = (DateTime)sc.log_time,
                                    //LogStatus = sc.log_status,
                                    Fullname = sc.user_fullname,
                                    Email = sc.email_address,
                                    Mobile = sc.mobile_no,
                                    //Localization = sc.localization,
                                    //Flag = sc.flag,
                                    //Ctime = (DateTime)sc.ctime,
                                    Userpos = sc.user_position,
                                    //Mail_sta = (int)sc.mail_status,
                                    //PostedBy = sc.posted_by,
                                    //PostedDate = (DateTime)sc.posted_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void UpdateCompanyUsers(CompanyUsers T)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var update = (from sc in context.company_users
                              where sc.comp_users_sno== T.CompuserSno
                              select sc).FirstOrDefault();
                if (update != null)
                {
                    /*update.comp_users_sno = T.CompuserSno;
                    update.comp_mas_sno = T.Compmassno;
                    update.username = T.Username;
                    //update.password = T.Password;
                    update.user_type = T.Usertype;
                    update.created_date = T.CreatedDate;
                    update.expiry_date = T.ExpiryDate;
                    update.sno = T.Sno;
                   *//*update.q_name = T.Qname;
                    update.q_ans = T.Qans;*//*
                    update.log_att = T.Logatt;
                    update.log_time = T.LogTime;
                    update.log_status = T.LogStatus;
                    update.user_fullname = T.Fullname;
                    update.email_address = T.Email;
                    update.mobile_no = T.Mobile;
                    update.localization = T.Localization;
                    update.flag = T.Flag;
                    update.ctime = T.Ctime;
                    update.user_position = T.Userpos;
                    update.mail_status = T.Mail_sta;
                    update.posted_by = T.PostedBy;
                    update.posted_date = DateTime.Now;*/

                    update.username = T.Username;
                    update.user_type = T.Usertype;
                    update.f_login = T.Flogin;
                    update.user_fullname = T.Fullname;
                    update.email_address = T.Email;
                    update.mobile_no = T.Mobile;
                    update.user_position = T.Userpos;
                    update.posted_by = T.PostedBy;
                    update.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }

        public void UpdateCompanyUsersP(CompanyUsers T)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var update = (from sc in context.company_users
                              where sc.comp_users_sno == T.CompuserSno
                              select sc).FirstOrDefault();
                if (update != null)
                {
                    
                    update.password = T.Password;
                   
                    context.SaveChanges();
                }
            }
        }

        public void DeleteCompany(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.company_users.Where(n => n.comp_users_sno == no)
                                   select n).First();
                if (noteDetails != null)
                {
                    context.company_users.Remove(noteDetails);
                    context.SaveChanges();
                }
            }
        }

        public bool ValidateduplicateEmail(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validationM = (from c in context.company_master
                                   where (c.email_address == name)
                                   select c);
                var validationU = (from c in context.company_users
                                   where (c.email_address == name)
                                   select c);
               
                if (validationU.Count() > 0||validationM.Count() > 0 )
                    return true;
                else
                    return false;
            }
        }
        public bool ValidateduplicateEmail1(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                
                var validationU = (from c in context.company_users
                                   where (c.email_address == name && c.email_address != null)
                                   select c);
               
                if (validationU.Count() > 0 )
                    return true;
                else
                    return false;
            }
        }
        public bool ValidateMobile(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                var validationU = (from c in context.company_users
                                   where (c.mobile_no == name && c.mobile_no != null)
                                   select c);

                if (validationU.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool Validatepwdbank(string name, long Userid)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                var validationemp = (from c in context.company_users
                                     where (c.password == name && c.f_login == "false" && c.comp_users_sno == Userid)
                                     select c);

                if (validationemp.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        public void UpdateQuestionEMP(CompanyUsers dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.company_users
                                         where u.comp_users_sno == dep.CompuserSno 
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                 /*   UpdateContactInfo.sno = dep.Sno;
                    UpdateContactInfo.q_name = dep.Qname;
                    UpdateContactInfo.q_ans = dep.Qans;*/
                    UpdateContactInfo.password = dep.Password;
                    //UpdateContactInfo.f_login = dep.Flogin;
                    UpdateContactInfo.posted_by = dep.PostedBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }


        public bool Validateduplicateuser(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validationM = (from c in context.company_master
                                   where (c.company_name == name)
                                   select c);
                var validationU = (from c in context.company_users
                                   where (c.email_address == name)
                                   select c);

                if (validationU.Count() > 0 || validationM.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool Validateduplicateuser1(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                //var validationM = (from c in context.company_master
                //                   where (c.company_name == name)
                //                   select c);
                var validationU = (from c in context.company_users
                                   where (c.username == name)
                                   select c);

                if (validationU.Count() > 0 )
                    return true;
                else
                    return false;
            }
        }

        public bool IsExistMobileNumber(string mobileNumber)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.company_users where c.mobile_no.ToLower().Equals(mobileNumber.ToLower()) select c);
                return validation.Count() > 0;
            }
        }

    public long? GetVendorUserCounts(long company_sno)
    {
        using (BIZINVOICEEntities context = new BIZINVOICEEntities())
        {
            var users = (from c in context.company_users
                               where (c.comp_mas_sno == company_sno)
                               select c);

            if (users.Count() > 0)
                return users.Count();
            else
                return 0;
        }
    }

    }

}
