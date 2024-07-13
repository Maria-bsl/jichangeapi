using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class Language
    {

        #region Properties
        public long Loc_Sno { get; set; }
        public string Loc_Eng { get; set; }
        public string Loc_Oth { get; set; }
        public string Table_name { get; set; }
        public string Col_name { get; set; }
        public string Posted_By { get; set; }
        public string Updated_By { get; set; }
        public DateTime? Audit_Date { get; set; }
        public DateTime? Updated_Date { get; set; }
        #endregion Properties

        #region Method
        public long AddLang(Language sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                localization_master pc = new localization_master()
                {
                    loc_eng = sc.Loc_Eng,
                    loc_other = sc.Loc_Oth,
                    table_name = sc.Table_name,
                    column_name = sc.Col_name,
                    posted_by = sc.Posted_By,
                    posted_date = DateTime.Now,
                };
                context.localization_master.Add(pc);
                context.SaveChanges();
                return pc.loc_sno;
            }
        }
        public bool ValidateColumn(string tble, string colm)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.localization_master
                                  where c.table_name == tble && c.column_name == colm //&& (c.history == null || c.history != "yes")
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
                var validation = (from c in context.localization_master
                                  where c.table_name == tble //&& (c.history == null || c.history != "yes")
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<Language> Getlang(string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.localization_master
                                where c.table_name == name

                                select new Language
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
        public List<Language> GetScreens()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.localization_master
                                    // where c.role_status == "Active"
                                select new Language
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
        public Language EditColumn(string tble, string colm)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.localization_master
                                where c.table_name == tble && c.column_name == colm

                                select new Language
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
        public void UpdateLan(Language dep)
        {
            if (dep.Loc_Sno != 0)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {

                    var UpdateContactInfo = (from u in context.localization_master
                                             where u.table_name == dep.Table_name && u.column_name == dep.Col_name //u.sno == dep.Sno
                                             select u).FirstOrDefault();

                    if (UpdateContactInfo != null)
                    {
                        //UpdateContactInfo.code = dep.Code;
                        UpdateContactInfo.table_name = dep.Table_name;
                        UpdateContactInfo.column_name = dep.Col_name;
                        UpdateContactInfo.loc_eng = dep.Loc_Eng;
                        UpdateContactInfo.loc_other = dep.Loc_Oth;
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
                    localization_master pc = new localization_master()
                    {
                        loc_eng = dep.Loc_Eng,
                        loc_other = dep.Loc_Oth,
                        table_name = dep.Table_name,
                        column_name = dep.Col_name,
                        posted_by = dep.Posted_By,
                        posted_date = DateTime.Now,
                    };
                    context.localization_master.Add(pc);
                    context.SaveChanges();
                }
            }
        }
        public void Updatelang(Language dep)
        {

            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.localization_master
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
        public Language EditLang(string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.localization_master
                                where c.table_name == name

                                select new Language
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
