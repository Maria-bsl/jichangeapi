using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using DaL.BIZINVOICING.EDMX;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{


    public class COUNTRY
    {
        #region Properties
        public long SNO { get; set; }
        public string Country_Name { get; set; }


        #endregion Properties
        #region Methods

        public long Addcountries(COUNTRY sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                country ps = new country()
                {

                    country_name = sc.Country_Name,
                };
                context.countries.Add(ps);
                context.SaveChanges();
                return ps.country_sno;
            }
        }
        public bool ValidateLicense(String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.countries
                                  where (c.country_name.ToLower().Equals(name))
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool ValidateCount(long name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.countries
                                  join det in context.region_master on c.country_sno equals det.country_sno
                                  where (c.country_sno == name)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool isExistCountry(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var exists = context.countries.Find(sno);
                return exists != null;
            }
        }

        public List<COUNTRY> GETcountries()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.countries
                                select new COUNTRY
                                {
                                    SNO = c.country_sno,
                                    Country_Name = c.country_name,

                                }).OrderByDescending(z => z.SNO).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public COUNTRY getcountriesText(long chsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.countries
                                where c.country_sno == chsno
                                select new COUNTRY
                                {
                                    SNO = c.country_sno,
                                    Country_Name = c.country_name,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public COUNTRY Editcountries(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.countries
                                where c.country_sno == sno

                                select new COUNTRY
                                {
                                    SNO = c.country_sno,
                                    Country_Name = c.country_name,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public long Deletecountries(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.countries
                                   where n.country_sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.countries.Remove(noteDetails);
                    context.SaveChanges();
                    return no;
                }
                else
                {
                    return 0;
                }

            }

        }

        public long Updatecountries(COUNTRY dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.countries
                                         where u.country_sno == dep.SNO
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {

                    UpdateContactInfo.country_name = dep.Country_Name;


                    context.SaveChanges();
                    return dep.SNO;
                }
                return 0;
            }

        }

        #endregion Methods


    }
}