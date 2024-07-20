using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class BranchM
    {
        #region Properties
        public string Name { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public long Sno { get; set; }
        public long? Branch_Sno { get; set; }

        public string AuditBy { get; set; }
        public DateTime Audit_Date { get; set; }
        #endregion properties
        #region methods
        public long AddBranch(BranchM sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                branch_name ps = new branch_name()
                {
                    name = sc.Name,
                    location = sc.Location,
                    status = sc.Status,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.branch_name.Add(ps);
                context.SaveChanges();
                return ps.sno;
            }
        }
        public bool ValidateBranch(String name)//, String code
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.branch_name
                                  where(c.name.ToLower() == name.ToLower())//&& c.location == code
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool IsDuplicatedName(String name,long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.branch_name
                                  where ((c.name.ToLower() == name.ToLower()) && c.sno != sno)//&& c.location == code
                                  select c);
                return validation.Count() > 0;
            }
        }

        public bool ValidateDelete(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.branch_Sno == sno)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        public List<BranchM> GetBranches()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.branch_name
                                select new BranchM
                                {
                                    Sno = c.sno,
                                    Name = c.name,
                                    Location = c.location,
                                    Status = c.status,
                                    Audit_Date = (DateTime)c.posted_date,
                                }).OrderByDescending(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<BranchM> GetBranches_Active()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.branch_name
                                where c.status == "Active"
                                select new BranchM
                                {
                                    Sno = c.sno,
                                    Name = c.name,
                                    Location = c.location,
                                    Status = c.status,
                                    Audit_Date = (DateTime)c.posted_date,
                                }).OrderBy(z => z.Name).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<BranchM> GetBranches_Active(long bno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.branch_name
                                where c.status == "Active" && c.sno == bno
                                select new BranchM
                                {
                                    Sno = c.sno,
                                    Name = c.name,
                                    Location = c.location,
                                    Status = c.status,
                                    Audit_Date = (DateTime)c.posted_date,
                                }).OrderBy(z => z.Name).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public BranchM getBranch(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.branch_name
                                where c.sno == sno
                                select new BranchM
                                {
                                    Sno = c.sno,
                                    Name = c.name,
                                    Location = c.location,
                                    Status = c.status,
                                    AuditBy = c.posted_by,
                                    Audit_Date = (DateTime)c.posted_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public BranchM EditBranch(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.branch_name
                                where c.sno == sno
                                select new BranchM
                                {
                                    Sno = c.sno,
                                    Branch_Sno = c.sno,
                                    Name = c.name,
                                    Location = c.location,
                                    Status = c.status,
                                    AuditBy = c.posted_by,
                                    Audit_Date = (DateTime)c.posted_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteBranch(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.branch_name
                                   where n.sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.branch_name.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public long UpdateBranch(BranchM dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.branch_name
                                         where u.sno == dep.Sno
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.name = dep.Name;
                    UpdateContactInfo.location = dep.Location;
                    UpdateContactInfo.status = dep.Status;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;

                    context.SaveChanges();
                    return UpdateContactInfo.sno;
                }
                return 0;
            }
        }

        public bool isExistBranch(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                //var exist = (from c in context.branch_name where (c.sno == sno) select c);
                //return exist.Count() > 0;
                var exists = context.branch_name.Find(sno);
                return exists != null;
            }
        }


        #endregion Methods
    }
}

