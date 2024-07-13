using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using DaL.BIZINVOICING.EDMX;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
   public class REGION
    {
        #region Properties
        public long Region_SNO { get; set; }
        public string Region_Name { get; set; }
        public long Country_Sno { get; set; }
        public string Country_Name { get; set; }
        public string Region_Status { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods

        public long AddREGION(REGION sc)
        {
            
                using ( BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    region_master ps = new region_master()
                    {

                        region_name = sc.Region_Name,
                        country_sno = sc.Country_Sno,
                        country_name = sc.Country_Name,
                        region_status = sc.Region_Status,
                        posted_by = sc.AuditBy,
                        posted_date = DateTime.Now,
                    };
                    context.region_master.Add(ps);
                    context.SaveChanges();
                    return ps.region_sno;
                }
            
        }
        public bool ValidateREGION(string Region, long name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.region_master
                                  where (c.region_name.ToLower().Equals(Region) && c.country_sno == name)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<REGION> GetReg()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.region_master
                                select new REGION
                                {
                                    Region_SNO = c.region_sno,
                                    Region_Name = c.region_name,
                                    //Country_Sno = (long)c.country_sno,
                                    //Country_Name = c.country_name,
                                    //Region_Status = c.region_status,
                                    //Audit_Date = c.posted_date,
                                })./*OrderByDescending(z => z.Audit_Date).*/ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<REGION> GetReg(long Sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.region_master
                                join c1 in context.company_master on c.region_sno equals c1.region_id
                                where c1.comp_mas_sno==Sno
                                select new REGION
                                {
                                    Region_SNO = c.region_sno,
                                    Region_Name = c.region_name,
                                    //Country_Sno = (long)c.country_sno,
                                    //Country_Name = c.country_name,
                                    //Region_Status = c.region_status,
                                    //Audit_Date = c.posted_date,
                                })./*OrderByDescending(z => z.Audit_Date).*/ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public bool Validatedupicate(string Region)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.region_master
                                  where (c.region_name.ToLower().Equals(Region))
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<REGION> GetREGIONActive(long Regsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.region_master.Where(c => c.region_status == "Active" && c.country_sno == Regsno)
                                select new REGION
                                {
                                    Region_SNO = c.region_sno,
                                    Region_Name = c.region_name,
                                    Country_Sno = (long)c.country_sno,
                                    Country_Name = c.country_name,
                                    Region_Status = c.region_status
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }


        public List<REGION> GetREGION()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.region_master
                                select new REGION
                                {
                                    Region_SNO = c.region_sno,
                                    Region_Name = c.region_name,
                                    Country_Sno = (long)c.country_sno,
                                    Country_Name = c.country_name,
                                    Region_Status = c.region_status,
                                    Audit_Date=c.posted_date,
                                }).OrderByDescending(z=>z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<REGION> GetREGIONdetils()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.region_master.Where(c=>c.region_status=="Active")

                                select new REGION
                                {
                                    Region_SNO = c.region_sno,
                                    Region_Name = c.region_name,
                                    Country_Sno = (long)c.country_sno,
                                    Country_Name = c.country_name,
                                    Region_Status = c.region_status
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }


        public bool ValidateDeletion(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.district_master.Where(c => c.region_id == sno)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<REGION> GetREG()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.region_master

                                select new REGION
                                {
                                    Region_SNO = c.region_sno,
                                    Region_Name = c.region_name
                                   
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public REGION getREGIONText(long chsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.region_master
                                where c.region_sno == chsno
                                select new REGION
                                {
                                    Region_SNO = c.region_sno,
                                    Region_Name = c.region_name,
                                    Country_Sno = (long)c.country_sno,
                                    Country_Name = c.country_name,
                                    Region_Status = c.region_status
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public REGION EditREGION(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.region_master
                                where c.region_sno == sno

                                select new REGION
                                {
                                    Region_SNO = c.region_sno,
                                    Region_Name = c.region_name,
                                    Country_Sno = (long)c.country_sno,
                                    Country_Name = c.country_name,
                                    Region_Status = c.region_status,
                                    AuditBy = c.posted_by,
                                    Audit_Date = c.posted_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteREGION(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.region_master
                                   where n.region_sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.region_master.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateREGION(REGION dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.region_master
                                         where u.region_sno == dep.Region_SNO
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.region_name = dep.Region_Name;
                    UpdateContactInfo.country_sno = dep.Country_Sno;
                    UpdateContactInfo.country_name = dep.Country_Name;
                    UpdateContactInfo.region_status = dep.Region_Status;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }


        #endregion Methods
    }
}
