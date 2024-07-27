using DaL.BIZINVOICING.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class User_otp
    {

        #region Properties

            public long user_otp_sno { get; set; }
            public string code { get; set; }
            public string mobile_no { get; set; }
            public DateTime? posted_date { get; set; }
        #endregion

        #region Method
        public long AddOtp(User_otp sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                user_otp ps = new user_otp()
                {
                    code = sc.code,
                    user_otp_sno = sc.user_otp_sno,
                    mobile_no = sc.mobile_no,
                    posted_date = DateTime.Now
                };
                context.user_otp.Add(ps);
                context.SaveChanges();
                return ps.user_otp_sno;
            }
        }

        public User_otp GetDetails(string otp)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var det = (from n in context.user_otp
                                   where n.code == otp
                                   select
                 new User_otp
                {
                    code = n.code,
                    user_otp_sno = n.user_otp_sno,
                    mobile_no = n.mobile_no,
                    posted_date = DateTime.Now
                }).FirstOrDefault();

                if (det != null)
                    return det;
                else
                    return null;
            }
        }
        /* public List<User_otp> GetUser_otp(long sno)
         {
             using (BIZINVOICEEntities context = new BIZINVOICEEntities())
             {
                 var adetails = (from c in context.user_otp
                                 where c.User_otp_status == "Active" && c.comp_mas_sno == sno
                                 select new User_otps
                                 {
                                     Sno = c.sno,
                                     Compmassno = (long)c.comp_mas_sno,
                                     Description = c.descript,
                                     Code = c.code,
                                     Admin1 = c.admin1,
                                     Status = c.User_otp_status,
                                     PostedDate = (DateTime)c.posted_date,
                                 }).OrderBy(z => z.PostedDate).ToList();
                 if (adetails != null && adetails.Count > 0)
                     return adetails;
                 else
                     return null;
             }
         }*/

        public void DeleteUser_otp(string no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.user_otp
                                   where n.code == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.user_otp.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }
       /* public List<User_otp> GetUser_otp2(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.user_otp
                                where c.User_otp == "Active" && c.comp_mas_sno == sno
                                select new User_otps
                                {
                                    Sno = c.sno,
                                    Compmassno = (long)c.comp_mas_sno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Admin1 = c.admin1,
                                    Status = c.User_otp_status,
                                    PostedDate = (DateTime)c.posted_date,
                                }).OrderBy(z => z.PostedDate).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
*/
       
        public void UpdateUser_otp(User_otp dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.user_otp
                                         where u.code == dep.code
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.code = dep.code;
                    UpdateContactInfo.user_otp_sno = dep.user_otp_sno;
                    UpdateContactInfo.mobile_no = dep.mobile_no;
                    UpdateContactInfo.posted_date = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }


        public bool ValidateUser_otp(string sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.user_otp
                                  where c.code == sno
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }


        public long GetLastInsertedId()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var getsno = context.user_otp.OrderByDescending(x => x.code).FirstOrDefault().code;

                if (getsno != null)
                    return long.Parse(getsno.ToString());
                else
                    return 0;
            }
        }


        public bool ValidateDeletion(string no, long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.company_users
                                  where (c.user_type == no && c.comp_mas_sno == sno)
                                  select c);
                //var validation1 = (from c in context.arights_master
                //                   where (c.rcode == no && c.insti_reg_sno == sno)
                //                   select c);
                if (validation.Count() > 0 /*&& validation1.Count() > 0*/)
                    return true;
                else
                    return false;
            }
        }


        #endregion
    }
}
