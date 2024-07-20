using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
   public class WARD
    {

        #region Properties
        public long SNO { get; set; }
        public string Ward_Name { get; set; }
        public string Region_Name { get; set; }
        public string District_Name { get; set; }
        public long Region_Id { get; set; }

        public long District_Sno { get; set; }
        public string Ward_Status { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods
        public long AddWARD(WARD sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                ward_master ps = new ward_master()
                {

                    ward_name = sc.Ward_Name,
                    region_id = sc.Region_Id,
                    district_sno = sc.District_Sno,
                    ward_status = sc.Ward_Status,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.ward_master.Add(ps);
                context.SaveChanges();
                return ps.ward_sno;
            }
        }
        public bool ValidateWARD(long rgn,string name,long rname)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.ward_master
                                  where (c.district_sno == rgn && c.ward_name.ToLower().Equals(name.ToLower())  && c.region_id == rname)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool isDuplicateWard(long districtId,string wardName,long regionId)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.ward_master
                                  where (c.district_sno != districtId && c.ward_name.ToLower().Equals(wardName.ToLower()) && c.region_id != regionId)
                                  select c);
                return validation.Count() > 0;
            }
        }
        public List<WARD> GetWARDAct(long regno)//need to updatelong regno, long dno
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.ward_master.Where(z => z.ward_status == "Active" && z.district_sno == regno /*&& z.district_sno == dno*/)
                                select new WARD
                                {
                                    SNO = c.ward_sno,
                                    Ward_Name = c.ward_name,
                                    Region_Id = (long)c.region_id,
                                    District_Sno = (long)c.district_sno,
                                    Ward_Status = c.ward_status
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public bool ValidateWARD1(long rgn, string name, long rname)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.ward_master
                                  where (c.district_sno == rgn && c.ward_name.ToLower().Equals(name) && c.region_id == rname)
                                  select c);
                if (validation.Count() > 0)
                    return false;
                else
                    return true;
            }
        }
        public List<WARD> GetWARDActive(long regno, long dno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.ward_master.Where(z => z.ward_status == "Active" && z.region_id == regno && z.district_sno == dno)
                                select new WARD
                                {
                                    SNO = c.ward_sno,
                                    Ward_Name = c.ward_name,
                                    Region_Id = (long)c.region_id,
                                    District_Sno = (long)c.district_sno,
                                    Ward_Status = c.ward_status
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public bool checkward(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.ward_master
                                  join det in context.customer_master on c.ward_sno equals det.ward_sno
                                  where (c.ward_sno == sno)
                                  select c);
                var validation1 = (from c in context.ward_master
                                  join det in context.company_master on c.ward_sno equals det.ward_sno
                                  where (c.ward_sno == sno)
                                  select c);
                if (validation.Count() > 0 || validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<WARD> GetWARD()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.ward_master join det in context.region_master on c.region_id equals det.region_sno 
                                 join dis in context.district_master on c.district_sno equals dis.district_sno
                                select new WARD
                                {
                                    SNO = c.ward_sno,
                                    Ward_Name = c.ward_name,
                                    District_Name = dis.district_name,
                                    Region_Name = det.region_name,
                                    Region_Id = (long)c.region_id,
                                    District_Sno = (long)c.district_sno,
                                    Ward_Status = c.ward_status,
                                    Audit_Date=c.posted_date,
                                }).OrderByDescending(z=>z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
       
        public List<WARD> GetWARDActive()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.ward_master.Where(z=>z.ward_status=="Active")
                                select new WARD
                                {
                                    SNO = c.ward_sno,
                                    Ward_Name = c.ward_name,
                                    Region_Id = (long)c.region_id,
                                    District_Sno = (long)c.district_sno,
                                    Ward_Status = c.ward_status
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public WARD getWARDText(long chsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.ward_master
                                where c.ward_sno == chsno
                                select new WARD
                                {
                                    SNO = c.ward_sno,
                                    Ward_Name = c.ward_name,
                                    Region_Id = (long)c.region_id,
                                    District_Sno = (long)c.district_sno,
                                    Ward_Status = c.ward_status
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public WARD EditWARD(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                /*var edetails = (from c in context.ward_master
                                where c.ward_sno == sno

                                select new WARD
                                {
                                    SNO = c.ward_sno,
                                    Ward_Name = c.ward_name,
                                    Region_Id = (long)c.region_id,
                                    District_Sno = (long)c.district_sno,
                                    Ward_Status = c.ward_status,
                                    AuditBy = c.posted_by,
                                    Audit_Date = c.posted_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;*/
                var adetails = (from c in context.ward_master
                                join det in context.region_master on c.region_id equals det.region_sno
                                join dis in context.district_master on c.district_sno equals dis.district_sno
                                where c.ward_sno == sno
                                select new WARD
                                {
                                    SNO = c.ward_sno,
                                    Ward_Name = c.ward_name,
                                    District_Name = dis.district_name,
                                    Region_Name = det.region_name,
                                    Region_Id = (long)c.region_id,
                                    District_Sno = (long)c.district_sno,
                                    Ward_Status = c.ward_status,
                                    Audit_Date = c.posted_date,
                                }).FirstOrDefault();
                return adetails != null ? adetails : null;
            }
        }

        public void DeleteWARD(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.ward_master.Where(n => n.ward_sno == no)
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.ward_master.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public bool isExistWard(long wardSno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var ward = context.ward_master.Find(wardSno);
                return ward != null;
            }
        }

        public long UpdateWARD(WARD dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.ward_master
                                         where u.ward_sno == dep.SNO
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {

                    UpdateContactInfo.ward_name = dep.Ward_Name;
                    UpdateContactInfo.region_id = dep.Region_Id;
                    UpdateContactInfo.district_sno = dep.District_Sno;
                    UpdateContactInfo.ward_status = dep.Ward_Status;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    context.SaveChanges();
                    return UpdateContactInfo.ward_sno;
                }
                return 0;
            }
        }


        #endregion Methods
    }
}
