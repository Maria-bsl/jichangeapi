using DaL.BIZINVOICING.EDMX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
   public class Auditlog
    {
        public long Audit_Sno { get; set; }
        public long Comp_Sno { get; set; }
        public String Audit_Type { get; set; }
        public String Table_Name { get; set; }
        public string Columnsname { get; set; }
        public string Oldvalues { get; set; }
        public string Newvalues { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        public DateTime? Audit_Time { get; set; }


        public long AddAudit(Auditlog sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                audit_log ps = new audit_log()
                {
                    audit_type = sc.Audit_Type,
                    table_name = sc.Table_Name,
                    column_name = sc.Columnsname,
                    old_value = sc.Oldvalues,
                    new_value = sc.Newvalues,
                    posted_by = sc.AuditBy,
                   comp_mas_sno = sc.Comp_Sno,
                    posted_date = DateTime.Now,
                    posted_time = DateTime.Now,
                };
                context.audit_log.Add(ps);
                context.SaveChanges();
                return ps.audit_sno;
            }
        }

        public List<Auditlog> Getlog(DateTime frm, DateTime to, string tn, string ac)
        {
            //DateTime add = to.AddDays(1);
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from mr in context.audit_log
                                where mr.posted_date >= frm && mr.posted_date <= to && mr.table_name.Contains(tn) &&
                                (ac == "All" ? mr.audit_type == mr.audit_type : mr.audit_type == ac)
                                select new Auditlog
                                {
                                    Audit_Sno = mr.audit_sno,
                                    Audit_Type = mr.audit_type,
                                    Table_Name = mr.table_name,
                                    Columnsname = mr.column_name,
                                    Oldvalues = mr.old_value,
                                    Newvalues = mr.new_value,
                                    AuditBy = mr.posted_by,
                                    Audit_Date = mr.posted_date,
                                    Audit_Time = mr.posted_time,
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<Auditlog> Getname(string tn, long ssno, string adtype, int count)
        {
            //DateTime add = to.AddDays(1);
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from mr in context.audit_log
                                where mr.table_name.Contains(tn) && mr.audit_type == adtype /*&& mr.insti_reg_sno == ssno*/
                                select new Auditlog
                                {
                                    Audit_Sno = mr.audit_sno,
                                    Audit_Type = mr.audit_type,
                                    Table_Name = mr.table_name,
                                    Columnsname = mr.column_name,
                                    Oldvalues = mr.old_value,
                                    Newvalues = mr.new_value,
                                    AuditBy = mr.posted_by,
                                    Audit_Date = mr.posted_date,
                                    Audit_Time = mr.posted_time,
                                }).OrderByDescending(z => z.Audit_Time).Take(count).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<Auditlog> GetBloglist(DateTime frm, DateTime to, string tn, string ac, String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "Company")
                {
                    if (ac == "All")
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   //join dets in context.institution_users on v.posted_by equals dets.insti_users_sno.ToString()
                                   //join inst in context.institution_registration on v.insti_reg_sno equals inst.insti_reg_sno
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to /*&& v.insti_reg_sno == inst.insti_reg_sno*/ &&
                                     v.audit_type == v.audit_type
                                   select new Auditlog
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       AuditBy = dets.full_name,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();

                        if (det != null && det.Count > 0)
                        {
                            return det;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   //join dets in context.institution_users on v.posted_by equals dets.insti_users_sno.ToString()
                                   //join inst in context.institution_registration on v.insti_reg_sno equals inst.insti_reg_sno
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to /*&& v.insti_reg_sno == inst.insti_reg_sno*/ &&
                                     v.audit_type == ac
                                   select new Auditlog
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       AuditBy = dets.full_name,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();

                        if (det != null && det.Count > 0)
                        {
                            return det;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                else if (name == "Bank")
                {
                    if (ac == "All")
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to &&
                                     v.audit_type == v.audit_type
                                   select new Auditlog
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       AuditBy = dets.full_name,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();
                        if (det != null && det.Count > 0)
                            return det;
                        else
                            return null;
                    }
                    else
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to &&
                                      v.audit_type == ac
                                   select new Auditlog
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       AuditBy = dets.full_name,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();
                        if (det != null && det.Count > 0)
                            return det;
                        else
                            return null;
                    }
                }
                else
                {
                    return null;
                }

            }
        }
        public List<Auditlog> GetBloglist1(DateTime frm, DateTime to, string tn, string ac, String name,long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "Company")
                {
                    if (ac == "All")
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   //join dets in context.institution_users on v.posted_by equals dets.insti_users_sno.ToString()
                                   //join inst in context.institution_registration on v.insti_reg_sno equals inst.insti_reg_sno
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to /*&& v.insti_reg_sno == inst.insti_reg_sno*/ &&
                                     v.audit_type == v.audit_type && v.comp_mas_sno==sno
                                   select new Auditlog
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       AuditBy = dets.full_name,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();

                        if (det != null && det.Count > 0)
                        {
                            return det;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   //join dets in context.institution_users on v.posted_by equals dets.insti_users_sno.ToString()
                                   //join inst in context.institution_registration on v.insti_reg_sno equals inst.insti_reg_sno
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to /*&& v.insti_reg_sno == inst.insti_reg_sno*/ &&
                                     v.audit_type == ac && v.comp_mas_sno == sno
                                   select new Auditlog
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       AuditBy = dets.full_name,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();

                        if (det != null && det.Count > 0)
                        {
                            return det;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                else if (name == "Bank")
                {
                    if (ac == "All")
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to &&
                                     v.audit_type == v.audit_type
                                   select new Auditlog
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       AuditBy = dets.full_name,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();
                        if (det != null && det.Count > 0)
                            return det;
                        else
                            return null;
                    }
                    else
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to &&
                                      v.audit_type == ac
                                   select new Auditlog
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       AuditBy = dets.full_name,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();
                        if (det != null && det.Count > 0)
                            return det;
                        else
                            return null;
                    }
                }
                else
                {
                    return null;
                }

            }
        }
        public List<Auditlog> Getloglist(DateTime frm, DateTime to, string tn, string ac, String name, long isno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "Institution")
                {
                    if (ac == "All")
                    {
                        var det = (from v in context.audit_log
                                   //join dets in context.institution_users on v.posted_by equals dets.insti_users_sno.ToString()
                                   //join inst in context.institution_registration on v.insti_reg_sno equals inst.insti_reg_sno
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to /*&& v.insti_reg_sno == isno*/ &&
                                     v.audit_type == v.audit_type
                                   select new Auditlog
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       //AuditBy = dets.user_fullname,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();

                        if (det != null && det.Count > 0)
                        {
                            return det;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        var det = (from v in context.audit_log
                                   //join dets in context.institution_users on v.posted_by equals dets.insti_users_sno.ToString()
                                   //join inst in context.institution_registration on v.insti_reg_sno equals inst.insti_reg_sno
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to /*&& v.insti_reg_sno == isno*/ &&
                                     v.audit_type == ac
                                   select new Auditlog
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       //AuditBy = dets.user_fullname,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();

                        if (det != null && det.Count > 0)
                        {
                            return det;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                

                else
                {
                    return null;
                }

            }
        }


        public static void InsertAuditTrail(List<string> values, long userid, string tableName,List<string> tableColumns)
        {
            Debug.Assert(values.Count() == tableColumns.Count(), "Audit trail lists must be of the same size");
            Auditlog ad = new Auditlog();
            for (int i = 0; i < values.Count(); i++)
            {
                ad.Audit_Type = "Insert";
                ad.Columnsname = tableColumns[i];
                ad.Table_Name = tableName;
                ad.Newvalues = values[i];
                ad.AuditBy = userid.ToString();
                ad.Audit_Date = DateTime.Now;
                ad.Audit_Time = DateTime.Now;
                ad.AddAudit(ad);
            }
        }

        public static void UpdateAuditTrail(List<string> oldValues,List<string> newValues,long userid,string tableName,List<string> tableColumns)
        {
            Debug.Assert(oldValues.Count() == tableColumns.Count(), "Audit trail lists must be of the same size");
            Debug.Assert(newValues.Count() == tableColumns.Count(), "Audit trail lists must be of the same size");
            Auditlog ad = new Auditlog();
            for (int i = 0; i < tableColumns.Count(); i++)
            {
                ad.Audit_Type = "Update";
                ad.Columnsname = tableColumns[i];
                ad.Table_Name = tableName;
                ad.Oldvalues = oldValues[i];
                ad.Newvalues = newValues[i];
                ad.AuditBy = userid.ToString();
                ad.Audit_Date = DateTime.Now;
                ad.Audit_Time = DateTime.Now;
                ad.AddAudit(ad);
            }
        }

        public static void deleteAuditTrail(List<string> values, long userid, string tableName, List<string> tableColumns)
        {
            Debug.Assert(values.Count() == tableColumns.Count(), "Audit trail lists must be of the same size");
            Auditlog ad = new Auditlog();
            for (int i = 0; i < tableColumns.Count(); i++)
            {
                ad.Audit_Type = "Delete";
                ad.Columnsname = tableColumns[i];
                ad.Table_Name = tableName;
                ad.Oldvalues = values[i];
                ad.AuditBy = userid.ToString();
                ad.Audit_Date = DateTime.Now;
                ad.Audit_Time = DateTime.Now;
                ad.AddAudit(ad);
            }
        }








    }
}
