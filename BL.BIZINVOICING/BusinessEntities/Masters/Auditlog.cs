using DaL.BIZINVOICING.EDMX;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

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

        public class CustomAuditReport
        {
            public long Audit_Sno { get; set; }
            public string Audit_Type { get; set; }
            public string Table_Name { get; set; }
            public string ColumnsName { get; set; }
            public string OldValues { get; set; }
            public string NewValues { get; set; }
            public string AuditBy { get; set; }
            public string AuditorName { get; set; }
            public string ipAddress { get; set; }
            public DateTime Audit_Date { get; set; }
        } 


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

        public long GetAuditReportCount(string startDate, string endDate, string tableName, string action, string auditBy)
        {
            DateTime? fromDate = null;
            if (!string.IsNullOrEmpty(startDate)) fromDate = DateTime.Parse(startDate);
            DateTime? toDate = null;
            if (!string.IsNullOrEmpty(endDate)) toDate = DateTime.Parse(endDate);

            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var length = (from v in context.audit_log
                             join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                             join tracks in (
                                 from t in context.track_details
                                 group t by t.posted_by into g
                                 select g.FirstOrDefault()
                             ) on v.posted_by equals tracks.posted_by into trackGroup
                             from track in trackGroup.DefaultIfEmpty()
                             //where !string.IsNullOrEmpty(auditBy) && v.posted_by == auditBy
                             where string.IsNullOrEmpty(action) || v.audit_type == action
                             where string.IsNullOrEmpty(tableName) || v.table_name == tableName
                             where ((!fromDate.HasValue || v.posted_time >= fromDate) && (!toDate.HasValue || v.posted_time <= toDate))
                             select v).Count();
                return length;
            }
        }


        public List<CustomAuditReport> GetAuditReport(string startDate,string endDate,string tableName,string action,string auditBy,int pageNumber,int pageSize)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {


                DateTime? fromDate = null;
                if (!string.IsNullOrEmpty(startDate)) fromDate = DateTime.Parse(startDate);
                DateTime? toDate = null;
                if (!string.IsNullOrEmpty(endDate)) toDate = DateTime.Parse(endDate);

                var results = (from v in context.audit_log
                               join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                               join tracks in (
                                   from t in context.track_details
                                   group t by t.posted_by into g
                                   select g.FirstOrDefault()
                               ) on v.posted_by equals tracks.posted_by into trackGroup
                               from track in trackGroup.DefaultIfEmpty() 
                               //where !string.IsNullOrEmpty(auditBy) && v.posted_by == auditBy
                               where string.IsNullOrEmpty(action) || v.audit_type == action
                               where string.IsNullOrEmpty(tableName) || v.table_name == tableName
                               where ((!fromDate.HasValue || v.posted_time >= fromDate) && (!toDate.HasValue || v.posted_time <= toDate))
                               select new CustomAuditReport
                               {
                                   Audit_Sno = v.audit_sno,
                                   Audit_Type = v.audit_type,
                                   Table_Name = v.table_name,
                                   ColumnsName = v.column_name,
                                   OldValues = v.old_value,
                                   NewValues = v.new_value,
                                   AuditBy = v.posted_by,
                                   Audit_Date = (DateTime)v.posted_time,
                                   AuditorName = dets.full_name,
                                   ipAddress = track != null ? track.ipadd : null 
                               }).ToList();
                return results ?? new List<CustomAuditReport>();
            }
        }

        public List<Auditlog> GetBloglist(DateTime frm, DateTime to, string tn, string ac, String name,long branch)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (ac == "All")
                {
                    var det = (from v in context.audit_log
                               join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                               where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to
                               && v.audit_type == ac
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
                    return det ?? new List<Auditlog>();
                }
                else
                {
                    var det = (from v in context.audit_log
                               join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                               where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to
                               && v.audit_type == ac
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
                    return det ?? new List<Auditlog>();
                }
                /*if (name == "Company")
                {
                    if (ac == "All")
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to 
                                   && v.audit_type == v.audit_type
                                   && dets.branch_Sno == branch
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
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to 
                                   && v.audit_type == ac
                                   && dets.branch_Sno == branch
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
                else 
                {
                    if (ac == "All")
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to 
                                   && v.audit_type == v.audit_type
                                   && dets.branch_Sno == branch
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
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to 
                                   && v.audit_type == ac
                                   && dets.branch_Sno == branch
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
                }*/
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

        public List<string> SelectTableNamesByAuditBy(string auditBy)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var results = context.audit_log
                    //.Where(e => e.posted_by == auditBy)
                    .Select(e => e.table_name).Distinct().ToList();
                return results ?? new List<string>();
            }
        }

        public List<string> SelectAuditTypesBy(string auditBy)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var results = context.audit_log
                    //.Where(e => e.posted_by == auditBy)
                    .Select(e => e.audit_type).Distinct().ToList();
                return results ?? new List<string>();
            }
        }

        public static void InsertAuditTrail(List<string> values, long userid, string tableName,List<string> tableColumns,long compid = 0)
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
                ad.Comp_Sno = compid;
                ad.AddAudit(ad);
            }
        }

        public static void UpdateAuditTrail(List<string> oldValues,List<string> newValues,long userid,string tableName,List<string> tableColumns,long compid = 0)
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
                ad.Comp_Sno = compid;
                ad.AddAudit(ad);
            }
        }

        public static void deleteAuditTrail(List<string> values, long userid, string tableName, List<string> tableColumns,long compid = 0)
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
                ad.Comp_Sno = compid;
                ad.AddAudit(ad);
            }
        }








    }
}
