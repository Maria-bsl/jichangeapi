using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class C_Deposit
    {
        #region Properties
        public long Comp_Dep_Acc_Sno { get; set; }
        public string Deposit_Acc_No { get; set; }
        public long Comp_Mas_Sno { get; set; }
        public string AuditBy { get; set; }
        public string Reason { get; set; }
        public string Company { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods
        public long AddAccount(C_Deposit sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                company_deposit_account ps = new company_deposit_account()
                {

                    deposit_acc_no = sc.Deposit_Acc_No,
                    comp_mas_sno = sc.Comp_Mas_Sno,
                    reason = sc.Reason,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.company_deposit_account.Add(ps);
                context.SaveChanges();
                return ps.comp_dep_acc_sno;
            }
        }
        public bool ValidateAccount(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.company_deposit_account
                                  where (c.deposit_acc_no.ToLower().Equals(name))
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
                                  where (c.comp_dep_acc_sno == no)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }*/
        public List<C_Deposit> GetAccounts()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.company_deposit_account
                                join d in context.company_master on c.comp_mas_sno equals d.comp_mas_sno
                                select new C_Deposit
                                {
                                    Comp_Dep_Acc_Sno = c.comp_dep_acc_sno,
                                    Deposit_Acc_No = c.deposit_acc_no,
                                    Reason = c.reason,
                                    Company = d.company_name,
                                    Comp_Mas_Sno = (long)c.comp_mas_sno,
                                    Audit_Date = c.posted_date,
                                }).OrderByDescending(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<C_Deposit> GetAccounts_Active()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.company_deposit_account
                                join d in context.company_master on c.comp_mas_sno equals d.comp_mas_sno
                                
                                select new C_Deposit
                                {
                                    Comp_Dep_Acc_Sno = c.comp_dep_acc_sno,
                                    Deposit_Acc_No = c.deposit_acc_no,
                                    Reason = c.reason,
                                    Company = d.company_name,
                                    Comp_Mas_Sno = (long)c.comp_mas_sno,
                                    Audit_Date = c.posted_date,
                                }).OrderByDescending(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public C_Deposit GetMAccount(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.company_deposit_account
                                join d in context.company_master on c.comp_mas_sno equals d.comp_mas_sno
                                where d.comp_mas_sno == cno
                                select new C_Deposit
                                {
                                    Comp_Dep_Acc_Sno = c.comp_dep_acc_sno,
                                    Deposit_Acc_No = c.deposit_acc_no,
                                    Reason = c.reason,
                                    Company = d.company_name,
                                    Comp_Mas_Sno = (long)c.comp_mas_sno,
                                    Audit_Date = c.posted_date,
                                }).OrderByDescending(z => z.Comp_Dep_Acc_Sno).FirstOrDefault();
                if (adetails != null )
                    return adetails;
                else
                    return null;
            }
        }
        public C_Deposit getAccount(long chsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.company_deposit_account
                                where c.comp_dep_acc_sno == chsno
                                select new C_Deposit
                                {
                                    Comp_Dep_Acc_Sno = c.comp_dep_acc_sno,
                                    Deposit_Acc_No = c.deposit_acc_no,
                                    Comp_Mas_Sno = (long)c.comp_mas_sno,
                                    Reason = c.reason

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public C_Deposit EditAccount(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.company_deposit_account
                                where c.comp_dep_acc_sno == sno

                                select new C_Deposit
                                {
                                    Comp_Dep_Acc_Sno = c.comp_dep_acc_sno,
                                    Deposit_Acc_No = c.deposit_acc_no,
                                    Comp_Mas_Sno = (long)c.comp_mas_sno,
                                    AuditBy = c.posted_by,
                                    Reason = c.reason,
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
                var noteDetails = (from n in context.company_deposit_account
                                   where n.comp_dep_acc_sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.company_deposit_account.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateAccount(C_Deposit dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.company_deposit_account
                                         where u.comp_dep_acc_sno == dep.Comp_Dep_Acc_Sno
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.deposit_acc_no = dep.Deposit_Acc_No;
                    UpdateContactInfo.comp_mas_sno = dep.Comp_Mas_Sno;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    UpdateContactInfo.reason = dep.Reason;

                    context.SaveChanges();
                }
            }
        }


        #endregion Methods
    }
}


