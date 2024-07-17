using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
  public  class CURRENCY
    {
        #region Properties
        public string Currency_Code { get; set; }
        public string Currency_Name { get; set; }

        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods
        public void AddCURRENCY(CURRENCY sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                currency_master ps = new currency_master()
                {
                    currency_code = sc.Currency_Code,
                    currency_name = sc.Currency_Name,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.currency_master.Add(ps);
                context.SaveChanges();
            }
        }
        public List<CURRENCY> ValidateCURRENCY(String name,String code)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.currency_master.Where(c=>c.currency_code.ToLower().Equals(code) || c.currency_name.ToLower().Equals(name))

                                select new CURRENCY
                                {
                                    Currency_Code = c.currency_code,
                                    Currency_Name = c.currency_name,

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CURRENCY> ValidateCname(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.currency_master.Where(c =>  c.currency_name.ToLower().Equals(name))
                                select new CURRENCY
                                {
                                    
                                    Currency_Name = c.currency_name,

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public bool ValidateDelete(String code)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.invoice_master
                                  where (c.currency_code == code)
                                  select c);
                //var validation1 = (from c in context.offering_master
                //                   where (c.currency_code == code)
                //                   select c);
                //var validation2 = (from c in context.payment_details
                //                   where (c.currency_code == code)
                //                   select c);
                if (validation.Count() > 0 )
                    return true;
                else
                    return false;
                
               
            }
        }

        public bool isExistCurrencyCode(String code)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.currency_master where (c.currency_code.ToLower().Equals(code)) select c);
                return validation.Count() > 0;
            }
        }



        public List<CURRENCY> GetCURRENCY()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.currency_master
                                select new CURRENCY
                                {
                                    Currency_Code = c.currency_code,
                                    Currency_Name = c.currency_name,
                                    Audit_Date=c.posted_date,
                                }).OrderByDescending(z=>z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        //public List<CURRENCY> GetCURRENCY1(long sno)
        //{
        //    using (BIZINVOICEEntities context = new BIZINVOICEEntities())
        //    {
        //        var adetails = (from c in context.currency_master join ex in context.exchange_master on c.currency_code equals ex.currency_code
        //                        where ex.insti_reg_sno==sno
        //                        select new CURRENCY
        //                        {
        //                            Currency_Code = c.currency_code,
        //                            Currency_Name = c.currency_name,
        //                            Audit_Date = c.posted_date,
        //                        }).Distinct().ToList();
        //        if (adetails != null && adetails.Count > 0)
        //            return adetails;
        //        else
        //            return null;
        //    }
        //}
        public CURRENCY getCURRENCYText(string chsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.currency_master
                                where c.currency_code == chsno
                                select new CURRENCY
                                {
                                    Currency_Code = c.currency_code,
                                    Currency_Name = c.currency_name,
                                    AuditBy = c.posted_by,
                                    Audit_Date = c.posted_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public CURRENCY EditCURRENCY(string sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.currency_master
                                where c.currency_code == sno

                                select new CURRENCY
                                {
                                    Currency_Code = c.currency_code,
                                    Currency_Name = c.currency_name,

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteCURRENCY( string no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.currency_master
                                   where n.currency_code == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.currency_master.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateCURRENCY(CURRENCY dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.currency_master
                                         where u.currency_code == dep.Currency_Code
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.currency_name = dep.Currency_Name;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }


        #endregion Methods
    }
}
