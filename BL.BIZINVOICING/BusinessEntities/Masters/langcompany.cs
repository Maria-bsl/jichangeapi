using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class langcompany
    {
        
            #region Properties
            public long Loc_Sno { get; set; }
            public long Inst_reg_sno { get; set; }
            public string Loc_Eng { get; set; }
            public string Loc_Oth { get; set; }
            public string Loc_Eng1 { get; set; }
            public string Loc_Oth1 { get; set; }
            public string Table_name { get; set; }
            public string Dyn_Swa { get; set; }
            public string Col_name { get; set; }
            public string Posted_By { get; set; }
            public string Updated_By { get; set; }
            public long comp_no { get; set; }
            public DateTime? Audit_Date { get; set; }
            public DateTime? Updated_Date { get; set; }
            #endregion Properties

            #region Method
            public long AddLang(langcompany sc)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    localization_company pc = new localization_company()
                    {
                        loc_eng = sc.Loc_Eng,
                        loc_other = sc.Loc_Oth1,
                        dyn_eng = sc.Loc_Eng1,
                        dyn_other = sc.Loc_Oth1,
                        comp_mas_sno = sc.comp_no,
                        table_name = sc.Table_name,
                        column_name = sc.Col_name,
                        //posted_by = sc.Posted_By,
                        posted_date = DateTime.Now,
                    };
                    context.localization_company.Add(pc);
                    context.SaveChanges();
                    return pc.loc_sno;
                }
            }


            public bool ValidateColumn(string tble)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var validation = (from c in context.localization_company
                                      where c.table_name == tble
                                      select c);
                    if (validation.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            public bool validatesno(long sno)
            {

                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {

                    var validation = (from c in context.localization_company
                                      where c.comp_mas_sno == sno //&& (c.history == null || c.history != "yes")
                                      select c);
                    if (validation.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            public bool Validate(string tble)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var validation = (from c in context.localization_company
                                      where c.table_name == tble //&& (c.history == null || c.history != "yes")
                                      select c);
                    if (validation.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            public List<langcompany> Getlang(string name)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var adetails = (from c in context.localization_company
                                    where c.table_name == name

                                    select new langcompany
                                    {
                                        Loc_Sno = c.loc_sno,
                                        Loc_Eng = c.loc_eng,
                                        Loc_Oth = c.loc_other,
                                        Col_name = c.column_name,
                                        Table_name = c.table_name,
                                        Audit_Date = c.posted_date,
                                    }).ToList();
                    if (adetails != null && adetails.Count > 0)
                        return adetails;
                    else
                        return null;
                }
            }
            public List<langcompany> Getlocaleng()
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var adetails = (from c in context.temp_localization
                                    select new langcompany
                                    {
                                        Loc_Sno = c.loc_sno,
                                        Loc_Eng = c.loc_eng,
                                        Loc_Eng1 = c.dyn_eng,
                                        Col_name = c.column_name,
                                        Table_name = c.table_name,
                                        Dyn_Swa = c.dyn_swa
                                    }).OrderBy(z => z.Loc_Sno).ToList();
                    if (adetails != null && adetails.Count > 0)
                        return adetails;
                    else
                        return null;
                }
            }
            public List<langcompany> GetlocalengInst()
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var adetails = (from c in context.temp_localization
                                    select new langcompany
                                    {
                                        Loc_Sno = c.loc_sno,
                                        Loc_Eng = c.loc_eng,
                                        Loc_Eng1 = c.dyn_eng,
                                        Col_name = c.column_name,
                                        Table_name = c.table_name,
                                        Dyn_Swa = c.dyn_swa
                                    }).OrderBy(z => z.Loc_Sno).ToList();
                    if (adetails != null && adetails.Count > 0)
                        return adetails;
                    else
                        return null;
                }
            }
            public List<langcompany> GetScreens()
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var adetails = (from c in context.localization_company
                                        // where c.role_status == "Active"
                                    select new langcompany
                                    {
                                        Loc_Sno = c.loc_sno,
                                        Loc_Eng = c.loc_eng,
                                        Loc_Oth = c.loc_other,
                                        Col_name = c.column_name,
                                        Table_name = c.table_name,
                                        Audit_Date = c.posted_date,

                                    }).OrderBy(z => z.Loc_Sno).ToList();
                    if (adetails != null && adetails.Count > 0)
                        return adetails;
                    else
                        return null;
                }
            }
            public langcompany EditColumn(string tble, string colm)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var edetails = (from c in context.localization_company
                                    where c.table_name == tble && c.column_name == colm

                                    select new langcompany
                                    {
                                        Loc_Sno = c.loc_sno,
                                        Loc_Eng = c.loc_eng,
                                        Loc_Oth = c.loc_other,
                                        Col_name = c.column_name,
                                        Table_name = c.table_name,
                                    }).FirstOrDefault();
                    if (edetails != null)
                        return edetails;
                    else
                        return null;
                }
            }
            public langcompany EditColumn1(string tble, string colm)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var edetails = (from c in context.temp_localization
                                    where c.table_name == tble && c.column_name == colm

                                    select new langcompany
                                    {
                                        Loc_Sno = c.loc_sno,
                                        Loc_Eng = c.loc_eng,
                                        Loc_Eng1 = c.dyn_eng,
                                        Loc_Oth1 = c.dyn_swa,
                                        Col_name = c.column_name,
                                        Table_name = c.table_name,
                                    }).FirstOrDefault();
                    if (edetails != null)
                        return edetails;
                    else
                        return null;
                }
            }
            public List<langcompany> GetLangU(long instno)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var adetails = (from c in context.temp_localization
                                    select new langcompany
                                    {
                                        Loc_Sno = c.loc_sno,
                                        Loc_Eng = c.loc_eng,
                                        Loc_Eng1 = c.dyn_eng,
                                        Loc_Oth1 = c.dyn_swa,
                                        Col_name = c.column_name,
                                        Table_name = c.table_name

                                    }).ToList();
                    var adetails1 = (from c in context.localization_company
                                     where c.comp_mas_sno == instno
                                     select new langcompany
                                     {
                                         Loc_Sno = c.loc_sno,
                                         Loc_Eng = c.loc_eng,
                                         Loc_Eng1 = c.dyn_eng,
                                         Loc_Oth1 = c.dyn_other,
                                         Col_name = c.column_name,
                                         Table_name = c.table_name

                                     }).ToList();
                    var union_all = adetails.Union(adetails1).ToList();
                    //var results = union_all.ToList();
                    if (union_all != null && union_all.Count > 0)
                        return union_all;
                    else
                        return null;
                }
            }
            /*public List<langcompany> GetLangUAPI(langcompany dep)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var adetails = (from c in context.temp_localization
                                    select new langcompany
                                    {
                                        Loc_Sno = c.loc_sno,
                                        Loc_Eng = c.loc_eng,
                                        Loc_Eng1 = c.dyn_eng,
                                        Loc_Oth1 = c.dyn_swa,
                                        Col_name = c.column_name,
                                        Table_name = c.table_name

                                    }).ToList();
                    var adetails1 = (from c in context.localization_company
                                     where c.comp_mas_sno == dep.Inst_reg_sno
                                     select new langcompany
                                     {
                                         Loc_Sno = c.loc_sno,
                                         Loc_Eng = c.loc_eng,
                                         Loc_Eng1 = c.dyn_eng,
                                         Loc_Oth1 = c.dyn_other,
                                         Col_name = c.column_name,
                                         Table_name = c.table_name

                                     }).ToList();
                    var union_all = adetails.Union(adetails1).ToList();
                    //var results = union_all.ToList();
                    if (union_all != null && union_all.Count > 0)
                        return union_all;
                    else
                        return null;
                }
            }*/
            //public langcompany EditColumn1(string tble, string colm, long sno)
            //{
            //    using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            //    {
            //        var edetails = (from c in context.localization_company
            //                        where c.table_name == tble && c.column_name == colm && c.comp_mas_sno == sno

            //                        select new langcompany
            //                        {
            //                            Loc_Sno = c.loc_sno,
            //                            Loc_Eng = c.loc_eng,
            //                            Loc_Eng1 = c.dyn_eng,
            //                            Loc_Oth1 = c.dyn_other,
            //                            Col_name = c.column_name,
            //                            Table_name = c.table_name,
            //                        }).FirstOrDefault();
            //        if (edetails != null)
            //            return edetails;
            //        else
            //            return null;
            //    }
            //}
            public bool ValidateColumn(string tble, string colm)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var validation = (from c in context.localization_company
                                      where c.table_name == tble && c.column_name == colm //&& (c.history == null || c.history != "yes")
                                      select c);
                    if (validation.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            public List<langcompany> EditColumn1t(string tble, long sno)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var edetails = (from c in context.localization_company
                                    where c.table_name == tble && c.comp_mas_sno == sno

                                    select new langcompany
                                    {
                                        Loc_Sno = c.loc_sno,
                                        Loc_Eng = c.loc_eng,
                                        Loc_Oth = c.loc_other,
                                        Loc_Eng1 = c.dyn_eng,
                                        Loc_Oth1 = c.dyn_other,
                                        comp_no = (long)c.comp_mas_sno,
                                        Col_name = c.column_name,
                                        Table_name = c.table_name,
                                    }).ToList();
                    if (edetails != null)
                        return edetails;
                    else
                        return null;
                }
            }
            public bool ValidateTemp(long sno, string tble, string colm)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var validation = (from c in context.localization_company
                                      where c.comp_mas_sno == sno && c.table_name == tble && c.column_name == colm
                                      select c);
                    if (validation.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            public langcompany EditTemp(long sno, string tble, string colm)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var edetails = (from c in context.localization_company
                                    where c.comp_mas_sno == sno && c.table_name == tble && c.column_name == colm

                                    select new langcompany
                                    {
                                        Loc_Sno = c.loc_sno,
                                        Loc_Eng = c.loc_eng,
                                        Loc_Oth = c.loc_other,
                                        Loc_Eng1 = c.dyn_eng,
                                        Loc_Oth1 = c.dyn_other,
                                        comp_no = (long)c.comp_mas_sno,
                                        Col_name = c.column_name,
                                        Table_name = c.table_name,
                                    }).FirstOrDefault();
                    if (edetails != null)
                        return edetails;
                    else
                        return null;
                }
            }
        public List<langcompany> GetlocalengI()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.temp_localization
                                select new langcompany
                                {
                                    Loc_Sno = c.loc_sno,
                                    Loc_Eng = c.loc_eng,
                                    Loc_Eng1 = c.dyn_eng,
                                    Col_name = c.column_name,
                                    Table_name = c.table_name,
                                    Dyn_Swa = c.dyn_swa
                                }).OrderBy(z => z.Loc_Sno).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public void UpdateTemp(langcompany dep)
            {
            if (dep.Loc_Sno != 0)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var UpdateContactInfo = (from u in context.localization_company
                                                 where u.loc_sno == dep.Loc_Sno && u.comp_mas_sno == dep.Inst_reg_sno && u.table_name == dep.Table_name && u.column_name == dep.Col_name
                                             select u).FirstOrDefault();

                    if (UpdateContactInfo != null)
                    {
                        //UpdateContactInfo.loc_eng = dep.Loc_Eng;
                        UpdateContactInfo.loc_other = dep.Loc_Oth;
                        UpdateContactInfo.updated_by = dep.Updated_By;
                        UpdateContactInfo.updated_date = DateTime.Now;

                        context.SaveChanges();
                    }
                }
            }
            else
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    localization_company pc = new localization_company()
                    {
                        loc_eng = dep.Loc_Eng,
                        loc_other = dep.Loc_Oth,
                        table_name = dep.Table_name,
                        column_name = dep.Col_name,
                        comp_mas_sno=dep.Inst_reg_sno,
                        dyn_eng=dep.Loc_Eng,
                        posted_by = dep.Posted_By,
                        posted_date = DateTime.Now,
                    };
                    context.localization_company.Add(pc);
                    context.SaveChanges();
                }
            }
        }
            public void UpdateLan(langcompany dep)
            {
                if (dep.Loc_Sno != 0)
                {
                    using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                    {

                        var UpdateContactInfo = (from u in context.localization_company
                                                 where u.table_name == dep.Table_name && u.column_name == dep.Col_name && u.comp_mas_sno == dep.comp_no //u.sno == dep.Sno
                                                 select u).FirstOrDefault();

                        if (UpdateContactInfo != null)
                        {
                            //UpdateContactInfo.code = dep.Code;
                            UpdateContactInfo.table_name = dep.Table_name;
                            UpdateContactInfo.column_name = dep.Col_name;

                            UpdateContactInfo.loc_eng = dep.Loc_Eng;
                            UpdateContactInfo.loc_other = dep.Loc_Oth;
                            UpdateContactInfo.dyn_eng = dep.Loc_Eng1;
                            UpdateContactInfo.dyn_other = dep.Loc_Oth1;
                            UpdateContactInfo.comp_mas_sno = dep.comp_no;
                            UpdateContactInfo.updated_by = dep.Posted_By;
                            UpdateContactInfo.updated_date = DateTime.Now;
                            context.SaveChanges();
                        }
                    }
                }
                else
                {
                    using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                    {
                        localization_company pc = new localization_company()
                        {
                            loc_eng = dep.Loc_Eng,
                            loc_other = dep.Loc_Oth,
                            dyn_eng = dep.Loc_Eng1,
                            dyn_other = dep.Loc_Oth1,
                            comp_mas_sno = dep.comp_no,
                            table_name = dep.Table_name,
                            column_name = dep.Col_name,
                            posted_by = dep.Posted_By,
                            posted_date = DateTime.Now,
                        };
                        context.localization_company.Add(pc);
                        context.SaveChanges();
                    }
                }
            }
            public void Updatelang(langcompany dep)
            {

                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var UpdateContactInfo = (from u in context.localization_company
                                             where u.loc_sno == dep.Loc_Sno
                                             select u).FirstOrDefault();

                    if (UpdateContactInfo != null)
                    {
                        UpdateContactInfo.table_name = dep.Table_name;
                        UpdateContactInfo.column_name = dep.Col_name;
                        UpdateContactInfo.loc_eng = dep.Loc_Eng;
                        UpdateContactInfo.loc_other = dep.Loc_Oth;
                        UpdateContactInfo.updated_by = dep.Updated_By;
                        UpdateContactInfo.updated_date = DateTime.Now;

                        context.SaveChanges();
                    }
                }
            }
            public langcompany EditLang(string name)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var edetails = (from c in context.localization_company
                                    where c.table_name == name

                                    select new langcompany
                                    {
                                        Loc_Sno = c.loc_sno,
                                        Loc_Eng = c.loc_eng,
                                        Loc_Oth = c.loc_other,
                                        Col_name = c.column_name,
                                        Table_name = c.table_name,
                                    }).FirstOrDefault();
                    if (edetails != null)
                        return edetails;
                    else
                        return null;
                }
            }

            #endregion Method
        }
    }








