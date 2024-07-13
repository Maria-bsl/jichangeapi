using DaL.BIZINVOICING.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
   public class VatPercentage
    {

        #region Properties Vat per
        public long vat_per_sno { get; set; }
        public string Vat_Category { get; set; }
        public string Vat_Description { get; set; }
        public int? vat_percentageValue { get; set; }
        public DateTime Posted_Date { get; set; }
        #endregion Properties
        #region Methods
        public List<VatPercentage> GetVatPercentage()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.vat_percentage
                                select new VatPercentage
                                {
                                    vat_per_sno = c.vat_per_sno,
                                    vat_percentageValue = (int)c.vat_percentage1,
                                    Vat_Category = c.vat_category,
                                    Vat_Description = c.vat_description
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public long AddVat(VatPercentage sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                vat_percentage ps = new vat_percentage()
                {

                    vat_percentage1 = sc.vat_percentageValue,
                    vat_category = sc.Vat_Category,
                    vat_description = sc.Vat_Description,
                    posted_date = DateTime.Now
                };
                context.vat_percentage.Add(ps);
                context.SaveChanges();
                return ps.vat_per_sno;
            }
        }
        public bool ValidateLicense(string vatc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.vat_percentage
                                  where (c.vat_category == vatc)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        public void DeleteVatper(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.vat_percentage
                                   where n.vat_per_sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.vat_percentage.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateVatpercentage(VatPercentage vat)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.vat_percentage
                                         where u.vat_per_sno == vat.vat_per_sno
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {

                    UpdateContactInfo.vat_percentage1 = vat.vat_percentageValue;
                    UpdateContactInfo.vat_category = vat.Vat_Category;
                    UpdateContactInfo.vat_description = vat.Vat_Description;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }

        }
        public VatPercentage getVatText(long chsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.vat_percentage
                                where c.vat_per_sno == chsno
                                select new VatPercentage
                                {
                                    vat_per_sno = c.vat_per_sno,
                                    vat_percentageValue = c.vat_percentage1,
                                    Vat_Category = c.vat_category,
                                    Vat_Description = c.vat_description
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public VatPercentage Editcountries(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.vat_percentage
                                where c.vat_per_sno == sno

                                select new VatPercentage
                                {
                                    vat_per_sno = c.vat_per_sno,
                                    vat_percentageValue = c.vat_percentage1,
                                    Vat_Category = c.vat_category,
                                    Vat_Description = c.vat_description
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }

        public bool ValidateCount(long name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.vat_percentage
                                  join det in context.invoice_details on c.vat_percentage1 equals det.vat_percentage
                                  where (c.vat_percentage1 == name)
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
