using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class DESIGNATION
    {
        #region Properties
        public long Desg_Id { get; set; }     
        public string Desg_Name { get; set; }

        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods
        public long AddUser(DESIGNATION sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                designation_list ps = new designation_list()
                {
                  
                    desg_name = sc.Desg_Name,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.designation_list.Add(ps);
                context.SaveChanges();
                return ps.desg_id;
            }
        }
        public bool ValidateDesignation( String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.designation_list
                                  where ( c.desg_name.ToLower().Equals(name))
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool ValidateDeletion(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.desg_id == no)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool isExistDesignation(long designationId)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var exists = context.designation_list.Find(designationId);
                return exists != null;
            }
        }

        public List<DESIGNATION> GetDesignation()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.designation_list
                                select new DESIGNATION
                                {
                                    Desg_Id = c.desg_id,
                                    Desg_Name = c.desg_name,
                                    Audit_Date=c.posted_date,
                                }).OrderByDescending(z=>z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public DESIGNATION getDesignationText(long chsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.designation_list
                                where c.desg_id == chsno
                                select new DESIGNATION
                                {
                                    Desg_Id = c.desg_id,
                                    Desg_Name = c.desg_name,
                                    AuditBy = c.posted_by,
                                    Audit_Date = c.posted_date

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public DESIGNATION Editdesignation(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.designation_list
                                where c.desg_id == sno

                                select new DESIGNATION
                                {
                                    Desg_Id = c.desg_id,
                                    Desg_Name = c.desg_name,
                                    AuditBy = c.posted_by,
                                    Audit_Date = c.posted_date
                                    
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteDesignation(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.designation_list
                                   where n.desg_id == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.designation_list.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateDesignation(DESIGNATION dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.designation_list
                                         where u.desg_id == dep.Desg_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.desg_name = dep. Desg_Name;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }


        #endregion Methods
    }
}

   
