using DaL.BIZINVOICING.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class Roles
    {

        public long Sno { get; set; }
        public String Code { get; set; }
        public String Description { get; set; }
        public String Admin1 { get; set; }
        public string Status { get; set; }
        public long Compmassno   { get; set; }
        public string AuditBy { get; set; }
        public DateTime PostedDate   { get; set; }

        public long AddRole(Roles sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                roles_master ps = new roles_master()
                {
                    code = sc.Code,
                    descript = sc.Description,
                    role_status = sc.Status,
                    admin1 = sc.Admin1,
                    comp_mas_sno = sc.Compmassno,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.roles_master.Add(ps);
                context.SaveChanges();
                return ps.sno;
            }
        }
        public List<Roles> GetRole(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.roles_master
                                where c.role_status == "Active" && c.comp_mas_sno == sno
                                select new Roles
                                {
                                    Sno = c.sno,
                                    Compmassno = (long)c.comp_mas_sno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Admin1 = c.admin1,
                                    Status = c.role_status,
                                    PostedDate =(DateTime) c.posted_date,
                                }).OrderBy(z => z.PostedDate).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public void Deleterole(string no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.roles_master
                                   where n.code == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.roles_master.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }
        public List<Roles> GetRole2(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.roles_master
                                where c.role_status == "Active" && c.comp_mas_sno == sno
                                select new Roles
                                {
                                    Sno = c.sno,
                                    Compmassno = (long)c.comp_mas_sno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Admin1 = c.admin1,
                                    Status = c.role_status,
                                    PostedDate = (DateTime)c.posted_date,
                                }).OrderBy(z => z.PostedDate).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public Roles editroleText(long chsno, long isno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.roles_master
                                where c.sno == chsno && c.comp_mas_sno == isno
                                select new Roles
                                {
                                    Sno = c.sno,
                                    Compmassno = (long)c.comp_mas_sno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Status = c.role_status,
                                    PostedDate = (DateTime)c.posted_date,
                                    Admin1 = c.admin1,
                                    AuditBy = c.posted_by

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void UpdateRole(Roles dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.roles_master
                                         where u.sno == dep.Sno
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.descript = dep.Description;
                    UpdateContactInfo.role_status = dep.Status;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }
        public bool ValidateRole(String name, long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.roles_master
                                  where (c.descript.ToLower().Equals(name)) && c.comp_mas_sno == sno
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public long GetLastInsertedId()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var getsno = context.roles_master.OrderByDescending(x => x.sno).FirstOrDefault().code;

                if (getsno != null)
                    return long.Parse(getsno.ToString());
                else
                    return 0;
            }
        }
        public bool ValidateDeletion(string no, long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.company_users
                                  where (c.user_type == no && c.comp_mas_sno == sno)
                                  select c);
                //var validation1 = (from c in context.arights_master
                //                   where (c.rcode == no && c.insti_reg_sno == sno)
                //                   select c);
                if (validation.Count() > 0 /*&& validation1.Count() > 0*/)
                    return true;
                else
                    return false;
            }
        }


    }
}
