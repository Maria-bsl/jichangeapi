using DaL.BIZINVOICING.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class SMS_SETTING
    {
        #region Properties
        public long SNO { get; set; }
        public string USER_Name { get; set; }
        public string Password { get; set; }
        public string From_Address { get; set; }
        public string Mobile_Service { get; set; }
        public DateTime? Effective_Date { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }

        #endregion Properties
        #region Methods
        public long AddSMS(SMS_SETTING sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                sms_settings ps = new sms_settings()
                {
                    username = sc.USER_Name,
                    password = sc.Password,
                    from_address = sc.From_Address,
                    mobile_service = sc.Mobile_Service,
                    effective_date = DateTime.Now,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,


                };
                context.sms_settings.Add(ps);
                context.SaveChanges();
                return ps.sno;
            }
        }

        public SMS_SETTING getSMTPConfig()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.sms_settings
                                orderby c.posted_date descending
                                select new SMS_SETTING
                                {
                                    SNO = c.sno,
                                    USER_Name = c.username,
                                    Password = c.password,
                                    From_Address = c.from_address,
                                    Mobile_Service = c.mobile_service,
                                    Effective_Date = (DateTime)c.effective_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public List<SMS_SETTING> GetSMS()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.sms_settings
                                select new SMS_SETTING
                                {
                                    SNO = c.sno,
                                    USER_Name = c.username,
                                    Password = c.password,
                                    From_Address = c.from_address,
                                    Mobile_Service = c.mobile_service,
                                    Effective_Date = (DateTime)c.effective_date

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public SMS_SETTING getSMSText()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.sms_settings
                                orderby c.effective_date descending
                                select new SMS_SETTING
                                {
                                    SNO = c.sno,
                                    USER_Name = c.username,
                                    Password = c.password,
                                    From_Address = c.from_address,
                                    Mobile_Service = c.mobile_service,
                                    Effective_Date = (DateTime)c.effective_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public SMS_SETTING EditSMS(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.sms_settings
                                where c.sno == sno

                                select new SMS_SETTING
                                {
                                    SNO = c.sno,
                                    USER_Name = c.username,
                                    Password = c.password,
                                    From_Address = c.from_address,
                                    Mobile_Service = c.mobile_service,
                                    Effective_Date = (DateTime)c.effective_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteSMS(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.sms_settings
                                   where n.sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.sms_settings.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateSMS(SMS_SETTING sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.sms_settings
                                         where u.sno == sc.SNO
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.username = sc.USER_Name;
                    UpdateContactInfo.password = sc.Password;
                    UpdateContactInfo.from_address = sc.From_Address;
                    UpdateContactInfo.mobile_service = sc.Mobile_Service;
                    UpdateContactInfo.effective_date = DateTime.Now;
                    UpdateContactInfo.posted_by = sc.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }


        #endregion Methods
    }
}
