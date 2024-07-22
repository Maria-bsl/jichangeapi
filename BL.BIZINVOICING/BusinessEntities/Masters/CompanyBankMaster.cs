using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class CompanyBankMaster
    {
        #region Properties
        public long CompSno { set; get; }
        public long Sus_Ac_SNo { set; get; }
        public string Sus_Acc { set; get; }
        public string CompName { set; get; }
        public string Status { set; get; }
        public string PostBox { set; get; }
        public string Address { set; get; }
        public long RegId { set; get; }
        public long DistSno { set; get; }
        public long WardSno { set; get; }
        public string RegName { set; get; }
        public string DistName { set; get; }
        public string WardName { set; get; }
        public string TinNo { set; get; }
        public string VatNo { set; get; }
        public  string DirectorName { set; get; }
        public string Email { set; get; }
        public string TelNo { set; get; }
        public string FaxNo { set; get; }
        public string MobNo { set; get; }
        public byte[] CompLogo { set; get; }
        public byte[]  DirectorSig{ set; get; }
        public string Postedby { set; get; }
        public  DateTime Posteddate { set; get; }
        public long BankSno { set; get; }
        public long CompanySno { set; get; }
        public long? Branch_Sno { set; get; }
        public string BankName { set; get; }
        public string BankBranch { set; get; }
        public string AccountNo { set; get; }
        public string Swiftcode { set; get; }
        public string Checker { set; get; }
        #endregion  properties
        #region method
        public long AddCompany(CompanyBankMaster T)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                company_master cm = new company_master()
                {
                    company_name = T.CompName,

                    pobox_no=T.PostBox,
                    physical_address=T.Address,
                    status=T.Status,
                    region_id=T.RegId,
                    district_sno=T.DistSno,
                    ward_sno=T.WardSno,
                    tin_no=T.TinNo,
                    vat_no=T.VatNo,
                    director_name=T.DirectorName,
                    email_address=T.Email,
                    telephone_no=T.TelNo,
                    fax_no=T.FaxNo,
                    mobile_no=T.MobNo,
                    //comp_logo=T.CompLogo,
                    //director_digital_sig=T.DirectorSig,
                    posted_by=T.Postedby,
                    posted_date=DateTime.Now,
                    branch_sno = T.Branch_Sno,
                    checker = T.Checker

                };
                context.company_master.Add(cm);
                context.SaveChanges();
                return cm.comp_mas_sno;
            }
        }
        public List<CompanyBankMaster> CompGet(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var list = (from c in context.company_master
                            where c.comp_mas_sno==sno
                            select new CompanyBankMaster
                            {
                                CompSno = c.comp_mas_sno,
                                CompName = c.company_name,
                            }).ToList();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }

        }
        public int GetCompanycount()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.company_master
                                where c.status == "Approved"
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        public int GetCompanyPencount()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.company_master
                                where c.status == "Pending"
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        public int GetCompanyRegwisecount(string uid,long no,string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
               if (name == "month")
                {
                    return context.company_master.Where(p => p.region_id == no).Count(p => p.posted_date.Value.Month == DateTime.Now.Month);
                }
                else
                {
                    return context.company_master.Where(p => p.region_id == no).Count(p => p.posted_date.Value.Year == DateTime.Now.Year);
                }
                //var adetails = (from c in context.company_master
                //                    where c.region_id == no
                //                    select c).ToList();
                
                //if (adetails != null && adetails.Count > 0)
                //    return adetails.Count;
                //else
                //    return 0;
            }
        }
        public int GetCompanyRegwisedefaultcount(string uid,long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                //if (name == "month")
                // {
                //     return context.company_master.Where(p => p.region_id == no).Count(p => p.posted_date.Value.Month == DateTime.Now.Month);
                // }
                // else
                // {
                //     return context.company_master.Where(p => p.region_id == no).Count(p => p.posted_date.Value.Year == DateTime.Now.Year);
                // }
                var adetails = (from c in context.company_master
                                where c.region_id == no && c.posted_date.Value.Month == DateTime.Now.Month
                                select c).ToList();

                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        public List<CompanyBankMaster> Compregwiselist()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var list = (from c in context.region_master

                            select new CompanyBankMaster
                            {
                                RegId=c.region_sno,
                                RegName=c.region_name
                            }).Distinct().ToList();

                if (list != null )
                    return list;
                else
                    return null;
            }

        }
        public List<CompanyBankMaster> CompGet1()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var list = (from c in context.company_master
                          
                            select new CompanyBankMaster
                            {
                                CompSno = c.comp_mas_sno,
                                CompName = c.company_name,
                            }).ToList();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }

        }
        public long AddBank(CompanyBankMaster T)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                company_bank_details bd = new company_bank_details()
                {
                     comp_mas_sno= T.CompSno,
                     bank_name= T.BankName,
                     bank_branch= T.BankBranch,
                     account_no= T.AccountNo,
                     swift_code= T.Swiftcode ,

                };
                context.company_bank_details.Add(bd);
                context.SaveChanges();
                return T.BankSno;//may be bd.banksno
            }
        }
        public List<CompanyBankMaster> GetCompany()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                    //join reg in context.region_master on sc.region_id equals reg.region_sno
                                    //join dist in context.district_master on sc.district_sno equals dist.district_sno
                                    //join ward in context.ward_master on sc.ward_sno equals ward.ward_sno
                                    //where sc.facility_reg_sno == facino
                                where sc.status == "Pending"
                                select new CompanyBankMaster
                                {
                                    CompSno =sc.comp_mas_sno,
                                    CompName=sc.company_name,
                                    PostBox=sc.pobox_no,
                                    Address=sc.physical_address,
                                    RegId=(long)sc.region_id,
                                    //RegName=reg.region_name,
                                    DistSno= (long)sc.district_sno,
                                    //DistName=dist.district_name,
                                    WardSno= (long)sc.ward_sno,
                                    //WardName=ward.ward_name,
                                    Branch_Sno = sc.branch_sno,
                                    TinNo=sc.tin_no,
                                    VatNo=sc.vat_no,
                                    DirectorName=sc.director_name,
                                    Email=sc.email_address,
                                    TelNo=sc.telephone_no,
                                    FaxNo=sc.fax_no,
                                    MobNo=sc.mobile_no,
                                    Status=sc.status,
                                    Checker = sc.checker
                                    //CompLogo=sc.comp_logo,
                                    //DirectorSig=sc.director_digital_sig,
              
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> GetCompany_S()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                //where sc.status == "Pending"
                                select new CompanyBankMaster
                                {
                                    CompSno = sc.comp_mas_sno,
                                    CompName = sc.company_name,
                                    PostBox = sc.pobox_no,
                                    Address = sc.physical_address,
                                    RegId = (long)sc.region_id,
                                    //RegName=reg.region_name,
                                    DistSno = (long)sc.district_sno,
                                    //DistName=dist.district_name,
                                    WardSno = (long)sc.ward_sno,
                                    //WardName=ward.ward_name,
                                    Branch_Sno = sc.branch_sno,
                                    TinNo = sc.tin_no,
                                    VatNo = sc.vat_no,
                                    DirectorName = sc.director_name,
                                    Email = sc.email_address,
                                    TelNo = sc.telephone_no,
                                    FaxNo = sc.fax_no,
                                    MobNo = sc.mobile_no,
                                    Status = sc.status,
                                    Checker = sc.checker
                                    //CompLogo=sc.comp_logo,
                                    //DirectorSig=sc.director_digital_sig,

                                }).OrderByDescending(a => a.CompSno).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> GetCompany_S(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                    where sc.branch_sno == cno
                                select new CompanyBankMaster
                                {
                                    CompSno = sc.comp_mas_sno,
                                    CompName = sc.company_name,
                                    PostBox = sc.pobox_no,
                                    Address = sc.physical_address,
                                    RegId = (long)sc.region_id,
                                    //RegName=reg.region_name,
                                    DistSno = (long)sc.district_sno,
                                    //DistName=dist.district_name,
                                    WardSno = (long)sc.ward_sno,
                                    //WardName=ward.ward_name,
                                    Branch_Sno = sc.branch_sno,
                                    TinNo = sc.tin_no,
                                    VatNo = sc.vat_no,
                                    DirectorName = sc.director_name,
                                    Email = sc.email_address,
                                    TelNo = sc.telephone_no,
                                    FaxNo = sc.fax_no,
                                    MobNo = sc.mobile_no,
                                    Status = sc.status,
                                    Checker = sc.checker
                                    //CompLogo=sc.comp_logo,
                                    //DirectorSig=sc.director_digital_sig,

                                }).OrderByDescending(a => a.CompSno).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> GetCompany_A()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                    //join reg in context.region_master on sc.region_id equals reg.region_sno
                                    //join dist in context.district_master on sc.district_sno equals dist.district_sno
                                    //join ward in context.ward_master on sc.ward_sno equals ward.ward_sno
                                    //where sc.facility_reg_sno == facino
                                where sc.status == "Approved"
                                select new CompanyBankMaster
                                {
                                    CompSno = sc.comp_mas_sno,
                                    CompName = sc.company_name
                                    

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> GetCompany_Suspense(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                    join reg in context.suspense_account on sc.sus_acc_sno equals reg.sus_acc_sno
                                    //join dist in context.district_master on sc.district_sno equals dist.district_sno
                                    //join ward in context.ward_master on sc.ward_sno equals ward.ward_sno
                                    where sc.comp_mas_sno == cno
                                where sc.status == "Approved"
                                select new CompanyBankMaster
                                {
                                    CompSno = sc.comp_mas_sno,
                                    CompName = sc.company_name,
                                    Sus_Ac_SNo = (long)sc.sus_acc_sno,
                                    Sus_Acc = reg.sus_acc_no


                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> GetCompany_Sus()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.suspense_account
                                //join dist in context.district_master on sc.district_sno equals dist.district_sno
                                //join ward in context.ward_master on sc.ward_sno equals ward.ward_sno
                                where c.sus_acc_status == "Active"
                                select new CompanyBankMaster
                                {
                                    Sus_Ac_SNo = (long)c.sus_acc_sno,
                                    Sus_Acc = c.sus_acc_no


                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> GetCompany1()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                //join reg in context.region_master on sc.region_id equals reg.region_sno
                                //join dist in context.district_master on sc.district_sno equals dist.district_sno
                                //join ward in context.ward_master on sc.ward_sno equals ward.ward_sno
                                 where sc.status=="Pending"
                                select new CompanyBankMaster
                                {
                                    CompSno =sc.comp_mas_sno,
                                    CompName=sc.company_name,
                                    PostBox=sc.pobox_no,
                                    Address=sc.physical_address,
                                    RegId=(long)sc.region_id,
                                    //RegName=reg.region_name,
                                    DistSno= (long)sc.district_sno,
                                    Branch_Sno = sc.branch_sno,
                                    //DistName=dist.district_name,
                                    WardSno= (long)sc.ward_sno,
                                    //WardName=ward.ward_name,
                                    TinNo=sc.tin_no,
                                    VatNo=sc.vat_no,
                                    DirectorName=sc.director_name,
                                    Email=sc.email_address,
                                    TelNo=sc.telephone_no,
                                    FaxNo=sc.fax_no,
                                    MobNo=sc.mobile_no,
                                    Status=sc.status,
                                    Checker = sc.checker
                                    //CompLogo=sc.comp_logo,
                                    //DirectorSig=sc.director_digital_sig,
              
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public CompanyBankMaster GetCompany_MStatus(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                where sc.comp_mas_sno == cno
                                where sc.status == "Approved"
                                select new CompanyBankMaster
                                {
                                    Status = sc.status,
                                    CompName = sc.company_name


                                }).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }

        public List<CompanyBankMaster> GetCompany1_Branch(long bsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                    //join reg in context.region_master on sc.region_id equals reg.region_sno
                                    //join dist in context.district_master on sc.district_sno equals dist.district_sno
                                    //join ward in context.ward_master on sc.ward_sno equals ward.ward_sno
                                where sc.status == "Pending" && sc.branch_sno == bsno
                                select new CompanyBankMaster
                                {
                                    CompSno = sc.comp_mas_sno,
                                    CompName = sc.company_name,
                                    PostBox = sc.pobox_no,
                                    Address = sc.physical_address,
                                    RegId = (long)sc.region_id,
                                    //RegName=reg.region_name,
                                    DistSno = (long)sc.district_sno,
                                    Branch_Sno = sc.branch_sno,
                                    //DistName=dist.district_name,
                                    WardSno = (long)sc.ward_sno,
                                    //WardName=ward.ward_name,
                                    TinNo = sc.tin_no,
                                    VatNo = sc.vat_no,
                                    DirectorName = sc.director_name,
                                    Email = sc.email_address,
                                    TelNo = sc.telephone_no,
                                    FaxNo = sc.fax_no,
                                    MobNo = sc.mobile_no,
                                    Status = sc.status,
                                    Checker = sc.checker
                                    //CompLogo=sc.comp_logo,
                                    //DirectorSig=sc.director_digital_sig,

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> GetCompany1_A()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                    //join reg in context.region_master on sc.region_id equals reg.region_sno
                                    //join dist in context.district_master on sc.district_sno equals dist.district_sno
                                    //join ward in context.ward_master on sc.ward_sno equals ward.ward_sno
                                //where sc.status == "Pending"
                                select new CompanyBankMaster
                                {
                                    CompSno = sc.comp_mas_sno,
                                    CompName = sc.company_name,
                                    PostBox = sc.pobox_no,
                                    Address = sc.physical_address,
                                    RegId = (long)sc.region_id,
                                    //RegName=reg.region_name,
                                    DistSno = (long)sc.district_sno,
                                    Branch_Sno = sc.branch_sno,
                                    //DistName=dist.district_name,
                                    WardSno = (long)sc.ward_sno,
                                    //WardName=ward.ward_name,
                                    TinNo = sc.tin_no,
                                    VatNo = sc.vat_no,
                                    DirectorName = sc.director_name,
                                    Email = sc.email_address,
                                    TelNo = sc.telephone_no,
                                    FaxNo = sc.fax_no,
                                    MobNo = sc.mobile_no,
                                    Status = sc.status,
                                    Checker = sc.checker
                                    //CompLogo=sc.comp_logo,
                                    //DirectorSig=sc.director_digital_sig,

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> GetCompany1_D()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                    //where sc.status == "Approved"
                                select new CompanyBankMaster
                                {
                                    CompSno = sc.comp_mas_sno,
                                    CompName = sc.company_name,
                                    PostBox = sc.pobox_no,
                                    Address = sc.physical_address,
                                    RegId = (long)sc.region_id,
                                    //RegName=reg.region_name,
                                    DistSno = (long)sc.district_sno,
                                    Branch_Sno = sc.branch_sno,
                                    //DistName=dist.district_name,
                                    WardSno = (long)sc.ward_sno,
                                    //WardName=ward.ward_name,
                                    TinNo = sc.tin_no,
                                    VatNo = sc.vat_no,
                                    DirectorName = sc.director_name,
                                    Email = sc.email_address,
                                    TelNo = sc.telephone_no,
                                    FaxNo = sc.fax_no,
                                    MobNo = sc.mobile_no,
                                    Status = sc.status,
                                    Checker = sc.checker
                                    //CompLogo=sc.comp_logo,
                                    //DirectorSig=sc.director_digital_sig,

                                }).OrderByDescending(a => a.CompSno).Take(5).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> GetCompany1_A_Q(string stat)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                    //join reg in context.region_master on sc.region_id equals reg.region_sno
                                    //join dist in context.district_master on sc.district_sno equals dist.district_sno
                                    //join ward in context.ward_master on sc.ward_sno equals ward.ward_sno
                                    where sc.status == stat
                                select new CompanyBankMaster
                                {
                                    CompSno = sc.comp_mas_sno,
                                    CompName = sc.company_name,
                                    PostBox = sc.pobox_no,
                                    Address = sc.physical_address,
                                    RegId = (long)sc.region_id,
                                    //RegName=reg.region_name,
                                    DistSno = (long)sc.district_sno,
                                    Branch_Sno = sc.branch_sno,
                                    //DistName=dist.district_name,
                                    WardSno = (long)sc.ward_sno,
                                    //WardName=ward.ward_name,
                                    TinNo = sc.tin_no,
                                    VatNo = sc.vat_no,
                                    DirectorName = sc.director_name,
                                    Email = sc.email_address,
                                    TelNo = sc.telephone_no,
                                    FaxNo = sc.fax_no,
                                    MobNo = sc.mobile_no,
                                    Status = sc.status,
                                    Checker = sc.checker
                                    //CompLogo=sc.comp_logo,
                                    //DirectorSig=sc.director_digital_sig,

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public List<CompanyBankMaster> GetCompany1_Branch_A(long bsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                    //join reg in context.region_master on sc.region_id equals reg.region_sno
                                    //join dist in context.district_master on sc.district_sno equals dist.district_sno
                                    //join ward in context.ward_master on sc.ward_sno equals ward.ward_sno
                                where sc.branch_sno == bsno//sc.status == "Pending" && 
                                select new CompanyBankMaster
                                {
                                    CompSno = sc.comp_mas_sno,
                                    CompName = sc.company_name,
                                    PostBox = sc.pobox_no,
                                    Address = sc.physical_address,
                                    RegId = (long)sc.region_id,
                                    //RegName=reg.region_name,
                                    DistSno = (long)sc.district_sno,
                                    Branch_Sno = sc.branch_sno,
                                    //DistName=dist.district_name,
                                    WardSno = (long)sc.ward_sno,
                                    //WardName=ward.ward_name,
                                    TinNo = sc.tin_no,
                                    VatNo = sc.vat_no,
                                    DirectorName = sc.director_name,
                                    Email = sc.email_address,
                                    TelNo = sc.telephone_no,
                                    FaxNo = sc.fax_no,
                                    MobNo = sc.mobile_no,
                                    Status = sc.status,
                                    Checker = sc.checker
                                    //CompLogo=sc.comp_logo,
                                    //DirectorSig=sc.director_digital_sig,

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> GetCompany1_Branch_D(long bsno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                    //join reg in context.region_master on sc.region_id equals reg.region_sno
                                    //join dist in context.district_master on sc.district_sno equals dist.district_sno
                                    //join ward in context.ward_master on sc.ward_sno equals ward.ward_sno
                                where sc.branch_sno == bsno //&& sc.status == "Approved" 
                                select new CompanyBankMaster
                                {
                                    CompSno = sc.comp_mas_sno,
                                    CompName = sc.company_name,
                                    PostBox = sc.pobox_no,
                                    Address = sc.physical_address,
                                    RegId = (long)sc.region_id,
                                    //RegName=reg.region_name,
                                    DistSno = (long)sc.district_sno,
                                    Branch_Sno = sc.branch_sno,
                                    //DistName=dist.district_name,
                                    WardSno = (long)sc.ward_sno,
                                    //WardName=ward.ward_name,
                                    TinNo = sc.tin_no,
                                    VatNo = sc.vat_no,
                                    DirectorName = sc.director_name,
                                    Email = sc.email_address,
                                    TelNo = sc.telephone_no,
                                    FaxNo = sc.fax_no,
                                    MobNo = sc.mobile_no,
                                    Status = sc.status,
                                    Checker = sc.checker
                                    //CompLogo=sc.comp_logo,
                                    //DirectorSig=sc.director_digital_sig,

                                }).OrderByDescending(a => a.CompSno).Take(5).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> GetCompany1_Branch_A_Q(long bsno, string stat)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                    //join reg in context.region_master on sc.region_id equals reg.region_sno
                                    //join dist in context.district_master on sc.district_sno equals dist.district_sno
                                    //join ward in context.ward_master on sc.ward_sno equals ward.ward_sno
                                where sc.branch_sno == bsno && sc.status == stat 
                                select new CompanyBankMaster
                                {
                                    CompSno = sc.comp_mas_sno,
                                    CompName = sc.company_name,
                                    PostBox = sc.pobox_no,
                                    Address = sc.physical_address,
                                    RegId = (long)sc.region_id,
                                    //RegName=reg.region_name,
                                    DistSno = (long)sc.district_sno,
                                    Branch_Sno = sc.branch_sno,
                                    //DistName=dist.district_name,
                                    WardSno = (long)sc.ward_sno,
                                    //WardName=ward.ward_name,
                                    TinNo = sc.tin_no,
                                    VatNo = sc.vat_no,
                                    DirectorName = sc.director_name,
                                    Email = sc.email_address,
                                    TelNo = sc.telephone_no,
                                    FaxNo = sc.fax_no,
                                    MobNo = sc.mobile_no,
                                    Status = sc.status,
                                    Checker = sc.checker
                                    //CompLogo=sc.comp_logo,
                                    //DirectorSig=sc.director_digital_sig,

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> GetBank(long sno)//we can take sno i think long sno
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_bank_details
                                //join oft in context.standard_terms on sc.term_sno equals oft.term_sno
                                //join cu in context.standard_currency_master on sc.currency_code equals cu.currency_sno
                                where sc.comp_mas_sno == sno
                                select new CompanyBankMaster
                                {
                                  BankSno= sc.comp_bank_det_sno,
                                  CompSno=(long)sc.comp_mas_sno,
                                  BankName=sc.bank_name,
                                  BankBranch=sc.bank_branch,
                                  AccountNo=sc.account_no,
                                  Swiftcode=sc.swift_code,
                                }).OrderBy(a => a.BankSno).ToList();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> GetBank_S(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_bank_details
                                where sc.comp_mas_sno == sno
                                select new CompanyBankMaster
                                {
                                    BankSno = sc.comp_bank_det_sno,
                                    CompSno = (long)sc.comp_mas_sno,
                                    AccountNo = sc.account_no
                                }).OrderBy(a => a.BankSno).ToList();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }
        ///their is a getbank method without sno GetDetails()
        public CompanyBankMaster EditCompany(long sno)//according to use we can use sno
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.company_master
                                //join MA in context.standard_grades_structure on c.grade_sno equals MA.grade_sno
                               where c.comp_mas_sno == sno
                                select new CompanyBankMaster
                                {

                                    CompSno = c.comp_mas_sno,
                                    CompName = c.company_name,
                                    PostBox = c.pobox_no,
                                    Address = c.physical_address,
                                    RegId = (long)c.region_id,
                                    DistSno = (long)c.district_sno,
                                    WardSno = (long)c.ward_sno,
                                    TinNo = c.tin_no,
                                    VatNo = c.vat_no,
                                    DirectorName = c.director_name,
                                    Email = c.email_address,
                                    TelNo = c.telephone_no,
                                    FaxNo = c.fax_no,
                                    MobNo = c.mobile_no,
                                    Checker = c.checker
                                    //CompLogo = c.comp_logo,
                                    //DirectorSig = c.director_digital_sig,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public CompanyBankMaster EditCompanys()//according to use we can use sno
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.company_master
                                    //join MA in context.standard_grades_structure on c.grade_sno equals MA.grade_sno
                                //where c.comp_mas_sno == sno
                                select new CompanyBankMaster
                                {

                                    CompSno = c.comp_mas_sno,
                                    CompName = c.company_name,
                                    PostBox = c.pobox_no,
                                    Address = c.physical_address,
                                    RegId = (long)c.region_id,
                                    DistSno = (long)c.district_sno,
                                    WardSno = (long)c.ward_sno,
                                    TinNo = c.tin_no,
                                    VatNo = c.vat_no,
                                    DirectorName = c.director_name,
                                    Email = c.email_address,
                                    TelNo = c.telephone_no,
                                    FaxNo = c.fax_no,
                                    MobNo = c.mobile_no,
                                    Checker = c.checker
                                    //CompLogo = c.comp_logo,
                                    //DirectorSig = c.director_digital_sig,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public CompanyBankMaster EditCompanyss(long sno)//according to use we can use sno
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.company_master
                                    //join MA in context.standard_grades_structure on c.grade_sno equals MA.grade_sno
                                join reg in context.region_master on c.region_id equals reg.region_sno
                                join dist in context.district_master on c.district_sno equals dist.district_sno
                                join ward in context.ward_master on c.ward_sno equals ward.ward_sno
                                where c.comp_mas_sno == sno
                                select new CompanyBankMaster
                                {

                                    CompSno = c.comp_mas_sno,
                                    CompName = c.company_name,
                                    PostBox = c.pobox_no,
                                    Address = c.physical_address,
                                    RegId = (long)c.region_id,
                                    RegName = reg.region_name,
                                    DistSno = (long)c.district_sno,
                                    DistName=dist.district_name,
                                    WardSno = (long)c.ward_sno,
                                    WardName=ward.ward_name,
                                    TinNo = c.tin_no,
                                    VatNo = c.vat_no,
                                    DirectorName = c.director_name,
                                    Email = c.email_address,
                                    TelNo = c.telephone_no,
                                    FaxNo = c.fax_no,
                                    MobNo = c.mobile_no,
                                    //CompLogo = c.comp_logo,
                                    //DirectorSig = c.director_digital_sig,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> EditBank(long sno)//check sno
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_bank_details
                                //join reg in context.standard_terms on sc.term_sno equals reg.term_sno
                                //join cn in context.standard_currency_master on sc.currency_code equals cn.currency_sno
                               where sc.comp_mas_sno == sno
                                select new CompanyBankMaster
                                {
                                    BankSno = sc.comp_bank_det_sno,
                                    CompSno = (long)sc.comp_mas_sno,
                                    BankName = sc.bank_name,
                                    BankBranch = sc.bank_branch,
                                    AccountNo = sc.account_no,
                                    Swiftcode = sc.swift_code,
                                }).ToList();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }
        public List<CompanyBankMaster> ActiveC()//check sno
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from sc in context.company_master
                                    join cn in context.invoice_master on sc.comp_mas_sno equals cn.comp_mas_sno
                                //where sc.comp_mas_sno == sno
                                select new CompanyBankMaster
                                {
                                    CompSno = sc.comp_mas_sno
                                }).Distinct().ToList();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }
        public void UpdateCompany(CompanyBankMaster T)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var update = (from u in context.company_master
                              where u.comp_mas_sno == T.CompSno
                              select u).FirstOrDefault();
                if (update != null)
                {
                    update.company_name = T.CompName;

                    update.pobox_no = T.PostBox;
                    update.physical_address = T.Address;
                    update.region_id = T.RegId;
                    update.district_sno = T.DistSno;
                    update.ward_sno = T.WardSno;
                    update.tin_no = T.TinNo;
                    update.vat_no = T.VatNo;
                    update.director_name = T.DirectorName;
                    update.email_address = T.Email;
                    update.telephone_no = T.TelNo;
                    update.fax_no = T.FaxNo;
                    update.mobile_no = T.MobNo;
                    update.branch_sno = T.Branch_Sno;
                    //update.comp_logo = T.CompLogo;
                    //update.director_digital_sig = T.DirectorSig;
                    update.posted_by = T.Postedby;
                    update.posted_date = DateTime.Now;
                    update.checker = T.Checker;
                    context.SaveChanges();
                }
            }
        }
        public void UpdateCompanysta(CompanyBankMaster T)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var update = (from u in context.company_master
                              where u.comp_mas_sno == T.CompSno && u.status=="Pending"
                              select u).FirstOrDefault();
                if (update != null)
                {
                    update.status = T.Status;
                    update.sus_acc_sno = T.Sus_Ac_SNo;
                    //update.comp_logo = T.CompLogo;
                    //update.director_digital_sig = T.DirectorSig;
                    update.posted_by = T.Postedby;
                    update.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        public void UpdateAccount(CompanyBankMaster T)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var update = (from u in context.company_master
                              where u.comp_mas_sno == T.CompSno 
                              select u).FirstOrDefault();
                if (update != null)
                {
                    update.sus_acc_sno = T.Sus_Ac_SNo;
                    //update.comp_logo = T.CompLogo;
                    //update.director_digital_sig = T.DirectorSig;
                    update.posted_by = T.Postedby;
                    update.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }

        public void UpdateBank(CompanyBankMaster T)
        {
            if (T.BankSno != 0)
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    var update = (from u in context.company_bank_details
                                  where u.comp_bank_det_sno == T.BankSno && u.comp_mas_sno== T.CompanySno
                                  select u).FirstOrDefault();
                    if (update != null)
                    {
                        update.comp_mas_sno = T.CompanySno;
                        update.bank_name = T.BankName;
                        update.bank_branch = T.BankBranch;
                        update.account_no = T.AccountNo;
                        update.swift_code = T.Swiftcode;
                       
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    company_bank_details dl = new company_bank_details()
                    {
                        comp_bank_det_sno=T.CompanySno,
                        comp_mas_sno = T.CompanySno,
                        bank_name = T.BankName,
                        bank_branch = T.BankBranch,
                        account_no = T.AccountNo,
                        swift_code = T.Swiftcode,
                    };
                    context.company_bank_details.Add(dl);
                    context.SaveChanges();
                }
            }
        }
        public void DeleteCompany(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.company_master.Where(n => n.comp_mas_sno== no)
                                   select n).First();
                if (noteDetails != null)
                {
                    context.company_master.Remove(noteDetails);
                    context.SaveChanges();
                }
            }
        }
        public void DeleteBank(CompanyBankMaster M)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                context.company_bank_details.RemoveRange(context.company_bank_details.Where
               (c => c.comp_mas_sno== M.CompSno));
                context.SaveChanges();
            }
        }
        public bool Validateaccount(string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.company_bank_details
                                  where (c.account_no == name)
                                  select c);
                if (validation.Count() > 0)

                    return true;
                else
                    return false;
            }
        }
        public bool ValidateCount(string name,string tinno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                var validation = (from c in context.company_master
                                  where c.company_name == name
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        //public bool Validateduplicatechecking(long edesc, DateTime date)//desc is edited
        //{
        //    using (BIZINVOICEEntities context = new BIZINVOICEEntities())
        //    {
        //        var checkdata = (from c in context.company_master
        //                         where c.comp_mas_sno == edesc && c.sheet_date == date 
        //                         select c);
        //        if (checkdata.Count() > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //}//for dup validation
        public bool ValidateUpdateorDelete(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())//may be u can add det sno 
            {
                var validationdeletion = (from v in context.company_master
                                          join a in context.invoice_master on v.comp_mas_sno equals a.comp_mas_sno
                                          where a.comp_mas_sno == no 
                                          select v);
                if (validationdeletion.Count() != 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion method


    }
}
