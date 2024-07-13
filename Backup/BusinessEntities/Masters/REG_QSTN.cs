using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
   public class REG_QSTN
    {
        #region Properties
        public long SNO { get; set; }
        public string Q_Name { get; set; }
        public string Q_Status { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods
        public long AddREG_QSTN(REG_QSTN sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                reg_question ps = new reg_question()
                {

                    q_name = sc.Q_Name,
                    q_status = sc.Q_Status,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.reg_question.Add(ps);
                context.SaveChanges();
                return ps.sno;
            }
        }
        public List<REG_QSTN> GetQSTNActive()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.reg_question.Where(c => c.q_status == "Active")
                                select new REG_QSTN
                                {
                                    SNO = c.sno,
                                    Q_Name = c.q_name,
                                    Q_Status = c.q_status
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public bool ValidateQSTN(string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.reg_question
                                  where (c.q_name.ToLower().Equals(name))
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool CheckQues(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
              
                var validationq = (from c in context.emp_detail
                                    where (c.sno == no)
                                    select c);
                if (validationq.Count()>0)
                    return true;
                else
                    return false;
            }
        }
        public List<REG_QSTN> GetQSTN()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.reg_question
                                select new REG_QSTN
                                {
                                    SNO = c.sno,
                                    Q_Name = c.q_name,
                                    Q_Status = c.q_status,
                                    Audit_Date=c.posted_date,
                                }).OrderByDescending(z=>z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public REG_QSTN getREG_QSTNText(long chsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.reg_question
                                where c.sno == chsno
                                select new REG_QSTN
                                {
                                    SNO = c.sno,
                                    Q_Name = c.q_name,
                                    Q_Status = c.q_status
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public REG_QSTN EditREG_QSTN(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.reg_question
                                where c.sno == sno

                                select new REG_QSTN
                                {
                                    SNO = c.sno,
                                    Q_Name = c.q_name,
                                    Q_Status = c.q_status,
                                    AuditBy = c.posted_by,
                                    Audit_Date = c.posted_date

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteREG_QSTN(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.reg_question
                                   where n.sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.reg_question.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateREG_QSTN(REG_QSTN dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.reg_question
                                         where u.sno == dep.SNO
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {

                    UpdateContactInfo.q_name = dep.Q_Name;
                    UpdateContactInfo.q_status = dep.Q_Status;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }


        #endregion Methods
    }
}
