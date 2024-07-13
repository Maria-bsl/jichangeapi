using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class DISTRICTS
    {
        #region Properties
        public long SNO { get; set; }
        public string District_Name { get; set; }
        public long Region_Id { get; set; }
        public string Region_Name { get; set; }
        public string District_Status { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods
        public long AddDistrict(DISTRICTS sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                district_master ps = new district_master()
                {
                    district_sno = sc.SNO,
                    district_name = sc.District_Name,
                    region_id =sc.Region_Id,
                    district_status=sc.District_Status,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.district_master.Add(ps);
                context.SaveChanges();
                return ps.district_sno;
            }
        }
        public bool Validatedistrict(string name, long rgn)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.district_master
                                  where (c.district_name == name && c.region_id ==rgn) 
                                  select c);
                if (validation.Count() > 0)
                    return false;
                else
                    return true;
            }
        }

        public bool Validatedistrict(string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.district_master
                                  where (c.district_name == name)
                                  select c);
                if (validation.Count() > 0)
                    return false;
                else
                    return true;
            }
        }
        public bool Validateduplicatechecking(string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.district_master
                                  where (c.district_name.ToLower().Equals(name))
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<DISTRICTS> GetDistrictActive(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.district_master.Where(c => c.region_id == no && c.district_status == "Active")
                                select new DISTRICTS
                                {
                                    SNO = c.district_sno,
                                    District_Name = c.district_name,
                                    Region_Id = (long)c.region_id,
                                    District_Status = c.district_status
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public bool Checkdistrict(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.ward_master.Where(c => c.district_sno == sno)
                                  select c);
                //var validationch = (from c in context.institution_registration.Where(c => c.district_sno == sno)
                //                    select c);
                //var validationbra = (from c in context.branch_name.Where(c => c.district_sno == sno)
                //                    select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<DISTRICTS> GetDistrict()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.district_master join reg in context.region_master on c.region_id equals reg.region_sno
                                select new DISTRICTS
                                {
                                    SNO=c.district_sno,
                                    District_Name = c.district_name,
                                    Region_Id = (long)c.region_id,
                                    Region_Name = reg.region_name,
                                    District_Status=c.district_status,
                                  Audit_Date=c.posted_date,
                                }).OrderByDescending(z=>z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<DISTRICTS> GetDists()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.district_master.Where(c => c.district_status == "Active")
                                select new DISTRICTS
                                {
                                    SNO = c.district_sno,
                                    District_Name = c.district_name

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public DISTRICTS getDistrictText(long chsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.district_master
                                where c.district_sno == chsno
                                select new DISTRICTS
                                {
                                    SNO = c.district_sno,
                                    District_Name = c.district_name,
                                    Region_Id = (long)c.region_id,
                                    District_Status = c.district_status

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public DISTRICTS EditDISTRICTS(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.district_master
                                where c.district_sno == sno

                                select new DISTRICTS
                                {
                                    SNO = c.district_sno,
                                    District_Name = c.district_name,
                                    Region_Id = (long)c.region_id,
                                    District_Status = c.district_status,
                                    AuditBy = c.posted_by,
                                    Audit_Date = c.posted_date

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        
        public void DeleteDISTRICTS(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.district_master.Where(n => n.district_sno == no)
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.district_master.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateDISTRICTS(DISTRICTS dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.district_master
                                         where u.district_sno == dep.SNO
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                     
                    UpdateContactInfo.district_name = dep.District_Name;
                    UpdateContactInfo.region_id = dep.Region_Id;
                    UpdateContactInfo.district_status = dep.District_Status;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }


        #endregion Methods
    }
}



