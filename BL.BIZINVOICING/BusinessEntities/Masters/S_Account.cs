using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class S_Account
    {
        #region Properties
        public long Sus_Acc_Sno { get; set; }
        public string Sus_Acc_No { get; set; }
        public string Status { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods
        public long AddAccount(S_Account sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                suspense_account ps = new suspense_account()
                {

                    sus_acc_no = sc.Sus_Acc_No,

                    sus_acc_status = sc.Status,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.suspense_account.Add(ps);
                context.SaveChanges();
                return ps.sus_acc_sno;
            }
        }
        public bool ValidateAccount(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.suspense_account
                                  where (c.sus_acc_no.ToLower().Equals(name))
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        /*public bool ValidateDeletion(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.sus_acc_sno == no)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }*/
        public bool isDuplicateAccountNumber(string accountNumber,long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.suspense_account
                                  where (c.sus_acc_no.ToLower().Equals(accountNumber.ToLower()) && c.sus_acc_sno != sno)
                                  select c);
                return validation.Count() > 0;
            }
        }
        public List<S_Account> GetAccounts()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.suspense_account
                                select new S_Account
                                {
                                    Sus_Acc_Sno = c.sus_acc_sno,
                                    Sus_Acc_No = c.sus_acc_no,
                                    Status = c.sus_acc_status,
                                    Audit_Date = c.posted_date,
                                }).OrderByDescending(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<S_Account> GetAccounts_Active()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.suspense_account
                                where c.sus_acc_status == "Active"
                                select new S_Account
                                {
                                    Sus_Acc_Sno = c.sus_acc_sno,
                                    Sus_Acc_No = c.sus_acc_no,
                                    Status = c.sus_acc_status,
                                    Audit_Date = c.posted_date,
                                }).OrderByDescending(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public S_Account getAccount(long chsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.suspense_account
                                where c.sus_acc_sno == chsno
                                select new S_Account
                                {
                                    Sus_Acc_Sno = c.sus_acc_sno,
                                    Sus_Acc_No = c.sus_acc_no,
                                    Status = c.sus_acc_status

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public S_Account EditAccount(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.suspense_account
                                where c.sus_acc_sno == sno

                                select new S_Account
                                {
                                    Sus_Acc_Sno = c.sus_acc_sno,
                                    Sus_Acc_No = c.sus_acc_no,
                                    Status = c.sus_acc_status,
                                    AuditBy = c.posted_by,
                                    Audit_Date = c.posted_date

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteAccount(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.suspense_account
                                   where n.sus_acc_sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.suspense_account.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public long UpdateAccount(S_Account dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.suspense_account
                                         where u.sus_acc_sno == dep.Sus_Acc_Sno
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.sus_acc_no = dep.Sus_Acc_No;
                    UpdateContactInfo.sus_acc_status = dep.Status;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;

                    context.SaveChanges();
                    return dep.Sus_Acc_Sno;
                }
                return 0;
            }
        }

        public bool isExistSuspenseAccount(long suspenseAccountId)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var exists = context.suspense_account.Find(suspenseAccountId);
                return exists != null;
            }
        }

        #endregion Methods
    }
}

