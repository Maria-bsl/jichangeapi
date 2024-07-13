using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class S_SMTP
    {
        #region Properties
        public long SNO { get; set; }
        public string From_Address { get; set; }
        public string SMTP_Address { get; set; }
        public string SMTP_Port { get; set; }
        public string SMTP_UName { get; set; }
        public string SMTP_Password { get; set; }
        public DateTime? Effective_Date { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        public string AuditAction { get; set; }
        public string AuditDone { get; set; }
        public long AuditID { get; set; }
        public string History { get; set; }
        public string SSL_Enable { get; set; }
        #endregion Properties
        #region Methods
        public long AddSMTP(S_SMTP sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                smtp_settings ps = new smtp_settings()
                {
                    sno = sc.SNO,
                    from_address = sc.From_Address,
                    smtp_address = sc.SMTP_Address,
                    smtp_port = sc.SMTP_Port,
                    username = sc.SMTP_UName,
                    smtp_password = sc.SMTP_Password,
                    effective_date = DateTime.Now,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                    ssl_enable = sc.SSL_Enable

                };
                context.smtp_settings.Add(ps);
                context.SaveChanges();
                return ps.sno;
            }
        }

        public List<S_SMTP> GetSMTPS()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.smtp_settings
                                    //where c.ssl_enable == "True"
                                select new S_SMTP
                                {
                                    SNO = c.sno,
                                    From_Address = c.from_address,
                                    SSL_Enable = c.ssl_enable,
                                    SMTP_UName = c.username,
                                    SMTP_Port = c.smtp_port,
                                    SMTP_Address = c.smtp_address,
                                    Effective_Date = (DateTime)c.effective_date

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public S_SMTP ValidateFromName(string add)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.smtp_settings
                                where c.from_address == add
                                select new S_SMTP
                                {
                                    SNO = c.sno,
                                    From_Address = c.from_address


                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public S_SMTP getSMTPText()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.smtp_settings
                                orderby c.effective_date descending
                                select new S_SMTP
                                {
                                    SNO = c.sno,
                                    From_Address = c.from_address,
                                    SMTP_Address = c.smtp_address,
                                    SMTP_Port = c.smtp_port,
                                    SMTP_UName = c.username,
                                    SMTP_Password = c.smtp_password,
                                    Effective_Date = (DateTime)c.effective_date,
                                    SSL_Enable = c.ssl_enable
                                }).Take(1).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public S_SMTP EditSMTP(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.smtp_settings
                                where c.sno == sno

                                select new S_SMTP
                                {
                                    SNO = c.sno,
                                    From_Address = c.from_address,
                                    SMTP_Address = c.smtp_address,
                                    SMTP_Port = c.smtp_port,
                                    SMTP_UName = c.username,
                                    SMTP_Password = c.smtp_password,
                                    SSL_Enable = c.ssl_enable,
                                    Effective_Date = c.effective_date,
                                    AuditBy = c.posted_by,
                                    Audit_Date = c.posted_date,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteSMTP(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.smtp_settings
                                   where n.sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.smtp_settings.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateSMTP(S_SMTP dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.smtp_settings
                                         where u.sno == dep.SNO
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.from_address = dep.From_Address;
                    UpdateContactInfo.smtp_address = dep.SMTP_Address;
                    UpdateContactInfo.smtp_port = dep.SMTP_Port;
                    UpdateContactInfo.username = dep.SMTP_UName;
                    UpdateContactInfo.smtp_password = dep.SMTP_Password;
                    UpdateContactInfo.effective_date = DateTime.Now;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    UpdateContactInfo.ssl_enable = dep.SSL_Enable;
                    context.SaveChanges();
                }
            }
        }
        public bool Validateduplicatedata(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.smtp_settings
                                  where (c.username == name)
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

