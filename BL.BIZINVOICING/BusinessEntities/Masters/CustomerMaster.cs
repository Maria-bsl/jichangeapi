using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class CustomerMaster
    {
        #region Properties
        public long Cust_Sno { set; get; }
        public long CompanySno { set; get; }
        public string Cust_Name { set; get; }
        public string PostboxNo { get; set; }
        public string Address { get; set; }
        public long? Region_SNO { get; set; }
        public string Region_Name { get; set; }
        public long? DistSno { get; set; }
        public string DistName { get; set; }
        public long? WardSno { get; set; }
        public string WardName { get; set; }
        public string TinNo { get; set; }
        public string VatNo { get; set; }
        public string ConPerson { get; set; }
        public string Email { get; set; }
        public string Phone { set; get; }
        public string Checker { set; get; }

        public string Posted_by { get; set; }
        public DateTime Posted_Date { get; set; }


        #endregion Properties
         
        #region method
        public long CustAdd(CustomerMaster T)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                customer_master sd = new customer_master()
                {

                    customer_name = T.Cust_Name,
                    pobox_no= T.PostboxNo,
                    physical_address = T.Address,
                    region_id = T.Region_SNO,
                    comp_mas_sno=T.CompanySno,
                    //Region_Name = a.region_name,
                   district_sno = T.DistSno,
                    //DistName = b.district_name,
                   ward_sno = T.WardSno,
                    //WardName = d.ward_name,
                    //reg dist ward
                    tin_no = T.TinNo,
                    vat_no = T.VatNo,
                    contact_person = T.ConPerson,
                    email_address=T.Email,
                    mobile_no = T.Phone, 
                    posted_by = T.Posted_by,
                    posted_date = DateTime.Now,
                    checker = T.Checker

                };
                context.customer_master.Add(sd);
                context.SaveChanges();
                return sd.cust_mas_sno;
            }
        }
        public int GetCustcountind(long uid,string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "today")
                {

                    return context.customer_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Day == DateTime.Now.Day );
                }
                else if (name == "week")
                {
                    var d7 = DateTime.Now.AddDays(-7);
                    var d1 = DateTime.Now.AddDays(-1);
                    return context.customer_master.Where(p => p.comp_mas_sno == uid).Count(p => System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1));

                }
                else if (name == "mnth")
                {
                    return context.customer_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Month == DateTime.Now.Month);
                }
                else
                {
                    return context.customer_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Year == DateTime.Now.Year);
                }
                //var adetails = (from c in context.customer_master
                //                where c.comp_mas_sno==uid&& c.posted_date==date
                //                select c).ToList();
                //if (adetails != null && adetails.Count > 0)
                //    return adetails.Count;
                //else
                //    return 0;
            }
        }
        public int GetCustcount(long uid, DateTime date)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {var adetails = (from c in context.customer_master
                where c.comp_mas_sno == uid && c.posted_date == date
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        
                public void CustUpdate(CustomerMaster T)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var update = (from c in context.customer_master
                              where c.cust_mas_sno == T.Cust_Sno
                              select c).FirstOrDefault();
                if (update != null)
                {
                    update.customer_name = T.Cust_Name;
                     update.pobox_no = T.PostboxNo;
                    update.physical_address = T.Address;
                    update.region_id = T.Region_SNO;
                    //Region_Name = a.region_name,
                    update.district_sno = T.DistSno;
                    //DistName = b.district_name,
                    update.ward_sno = T.WardSno;
                    //WardName = d.ward_name,
                    //reg dist ward
                    update.tin_no = T.TinNo;
                    update.vat_no = T.VatNo;
                    update.contact_person = T.ConPerson;
                    update.email_address = T.Email;
                    update.mobile_no = T.Phone;
                    update.posted_by = T.Posted_by;
                    update.posted_date = DateTime.Now;
                    update.checker = T.Checker;
                     context.SaveChanges(); 
                   
                    
                }
            }
        }
        //public void Standard_Update_Approve(CustomerMaster sdd)
        //{
        //    using (BIZPAYEntities context = new BIZPAYEntities())
        //    {
        //        var update = (from c in context.standard_divisions
        //                      where c.division_sno == sdd.Division_Sno
        //                      select c).FirstOrDefault();
        //        if (update != null)
        //        {
        //            update.status = sdd.Status;
        //            update.checked_by = sdd.Checked_By;
        //            update.checked_date = sdd.Checked_Date;
        //            context.SaveChanges();
        //        }
        //    }
        //}

        public void CustDelete(long SSno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                var del = (from c in context.customer_master
                           where c.cust_mas_sno == SSno
                           select c).First();

                if (del != null)
                {
                    context.customer_master.Remove(del);
                    context.SaveChanges();
                }

            }
        }
        public CustomerMaster get_Cust(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var update = (from c in context.customer_master
                              
                              where c.cust_mas_sno == sno
                              select new CustomerMaster
                              {
                                  Cust_Sno=c.cust_mas_sno,
                                  Cust_Name=c.customer_name,
                                  PostboxNo=c.pobox_no,
                                  Address=c.physical_address,
                                  TinNo=c.tin_no,
                                  VatNo=c.vat_no,
                                  ConPerson=c.contact_person,
                                  Email=c.email_address,
                                  Phone=c.mobile_no,
                                  Checker = c.checker
                              }).FirstOrDefault();
                if (update != null)
                    return update;
                else
                    return null;


            }
        }//if required need to join//reg need to be added/
        public List<CustomerMaster> GetCust1(long compid)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.customer_master
                                join c1 in context.company_master on c.comp_mas_sno equals c1.comp_mas_sno
                                where (compid == 0 || c.comp_mas_sno == compid)
                                select new CustomerMaster
                                {
                                    Cust_Sno= c.cust_mas_sno,
                                   Cust_Name = c.customer_name,
                                    
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public List<CustomerMaster> SelectCustomersByCompanyIds(List<long> companyIds)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.customer_master
                                join c1 in context.company_master on c.comp_mas_sno equals c1.comp_mas_sno
                                where (companyIds.Contains(0) || companyIds.Contains((long) c.comp_mas_sno))
                                select new CustomerMaster
                                {
                                    Cust_Sno = c.cust_mas_sno,
                                    Cust_Name = c.customer_name,
                                    Phone = c.mobile_no

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public long? GetCustCount_C(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.customer_master
                                where c.comp_mas_sno == cno
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }


        public int GetAllCustCount()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.customer_master
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }


        public int GetAllBranchCustCount(long branch)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.customer_master
                                where c.company_master.branch_sno == branch
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }


        public CustomerMaster CustGetId(long sno, long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var list = (from c in context.customer_master
                            where c.comp_mas_sno == sno && c.cust_mas_sno == cno
                            select new CustomerMaster
                            {
                                Cust_Sno = c.cust_mas_sno,
                                Cust_Name = c.customer_name,
                                PostboxNo = c.pobox_no,
                                Address = c.physical_address,
                                Region_SNO = c.region_id,
                                DistSno = c.district_sno,
                                WardSno = c.ward_sno,
                                TinNo = c.tin_no,
                                VatNo = c.vat_no,
                                ConPerson = c.contact_person,
                                Email = c.email_address,
                                Phone = c.mobile_no,
                                Checker = c.checker
                            }).FirstOrDefault();

                if (list != null)
                    return list;
                else
                    return null;
            }

        }
        public List<CustomerMaster> CustGet(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var list = (from c in context.customer_master
                            where c.comp_mas_sno==sno
                            select new CustomerMaster
                            {
                                Cust_Sno = c.cust_mas_sno,
                                Cust_Name = c.customer_name,
                                PostboxNo = c.pobox_no,
                                Address = c.physical_address,
                                Region_SNO=c.region_id,
                                DistSno=c.district_sno,
                                WardSno =c.ward_sno,
                                TinNo = c.tin_no,
                                VatNo = c.vat_no,
                                ConPerson = c.contact_person,
                                Email = c.email_address,
                                Phone = c.mobile_no,
                                Checker = c.checker
                            }).ToList();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }

        }
        public List<CustomerMaster> CustGet()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var list = (from c in context.customer_master
                            //where c.comp_mas_sno==sno
                            select new CustomerMaster
                            {
                                Cust_Sno = c.cust_mas_sno,
                                Cust_Name = c.customer_name,
                                PostboxNo = c.pobox_no,
                                Address = c.physical_address,
                                Region_SNO=c.region_id,
                                DistSno=c.district_sno,
                                WardSno =c.ward_sno,
                                TinNo = c.tin_no,
                                VatNo = c.vat_no,
                                ConPerson = c.contact_person,
                                Email = c.email_address,
                                Phone = c.mobile_no,
                                Checker = c.checker
                            }).ToList();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }

        }
        public List<CustomerMaster> CustGetrep(long reg, long dist)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (reg==0)
                {
                    var list = (from c in context.customer_master
                                where (reg == 0 ? c.region_id == c.region_id : c.region_id == reg)
                                             


                                select new CustomerMaster
                                {
                                    Cust_Sno = c.cust_mas_sno,
                                    Cust_Name = c.customer_name,
                                    PostboxNo = c.pobox_no,
                                    Address = c.physical_address,
                                    Region_SNO = c.region_id,
                                    //Region_Name=a.region_name,
                                    DistSno = c.district_sno,
                                    //DistName = b.district_name,
                                    WardSno = c.ward_sno,
                                    //WardName=d.ward_name,
                                    TinNo = c.tin_no,
                                    VatNo = c.vat_no,
                                    ConPerson = c.contact_person,
                                    Email = c.email_address,
                                    Phone = c.mobile_no,
                                    Checker = c.checker
                                }).ToList();
                    return list;
                }
                else
                {
                    var list = (from c in context.customer_master
                                where (reg == 0 ? c.region_id == c.region_id : c.region_id == reg)
                                             && (dist == 0 ? c.district_sno == c.district_sno : c.district_sno == dist)


                                select new CustomerMaster
                                {
                                    Cust_Sno = c.cust_mas_sno,
                                    Cust_Name = c.customer_name,
                                    PostboxNo = c.pobox_no,
                                    Address = c.physical_address,
                                    Region_SNO = c.region_id,
                                    //Region_Name=a.region_name,
                                    DistSno = c.district_sno,
                                    //DistName = b.district_name,
                                    WardSno = c.ward_sno,
                                    //WardName=d.ward_name,
                                    TinNo = c.tin_no,
                                    VatNo = c.vat_no,
                                    ConPerson = c.contact_person,
                                    Email = c.email_address,
                                    Phone = c.mobile_no,
                                    Checker = c.checker
                                }).ToList();
                    return list;
                }
                
                    
                
               
            }

        }
        public List<CustomerMaster> GetCustomerReportByCompanies(List<long> companies)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var customers = (from c in context.customer_master
                                 join c1 in context.company_master on c.comp_mas_sno equals c1.comp_mas_sno
                                 where ((companies.Contains(0)) || (companies.Contains((long)c.comp_mas_sno)))
                                 //&& ((regions.Contains(0)) || (regions.Contains((long)c.region_id)))
                                 //&& ((districts.Contains(0)) || (districts.Contains((long)c.district_sno)))
                                 select new CustomerMaster
                                 {
                                     Cust_Sno = c.cust_mas_sno,
                                     Cust_Name = c.customer_name,
                                     
                                     PostboxNo = c.pobox_no,
                                     Address = c.physical_address,
                                     Region_SNO = c.region_id,
                                     //Region_Name=a.region_name,
                                     DistSno = c.district_sno,
                                     //DistName = b.district_name,
                                     WardSno = c.ward_sno,
                                     //WardName=d.ward_name,
                                     TinNo = c.tin_no,
                                     VatNo = c.vat_no,
                                     ConPerson = c.contact_person,
                                     Email = c.email_address,
                                     Phone = c.mobile_no,
                                     Checker = c.checker,
                                     Posted_Date = (DateTime)c.posted_date
                                 }).OrderByDescending(z => z.Cust_Sno).ToList();
                return customers ?? new List<CustomerMaster>();
            }
        }


        public List<CustomerMaster> CustGetrep(long Comp,long reg, long dist)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (reg==0)
                {
                    var list = (from c in context.customer_master
                                where (reg == 0 ? c.region_id == c.region_id : c.region_id == reg)

                                &&(Comp == 0 ? c.comp_mas_sno == c.comp_mas_sno : c.comp_mas_sno == Comp)

                                select new CustomerMaster
                                {
                                    Cust_Sno = c.cust_mas_sno,
                                    Cust_Name = c.customer_name,
                                    PostboxNo = c.pobox_no,
                                    Address = c.physical_address,
                                    Region_SNO = c.region_id,
                                    //Region_Name=a.region_name,
                                    DistSno = c.district_sno,
                                    //DistName = b.district_name,
                                    WardSno = c.ward_sno,
                                    //WardName=d.ward_name,
                                    TinNo = c.tin_no,
                                    VatNo = c.vat_no,
                                    ConPerson = c.contact_person,
                                    Email = c.email_address,
                                    Phone = c.mobile_no,
                                    Checker = c.checker,
                                    Posted_Date = (DateTime) c.posted_date
                                }).OrderByDescending(z => z.Cust_Sno).ToList();
                    return list;
                }
                else
                {
                    var list = (from c in context.customer_master
                                where (reg == 0 ? c.region_id == c.region_id : c.region_id == reg)
                                             && (dist == 0 ? c.district_sno == c.district_sno : c.district_sno == dist)
                                              && (Comp == 0 ? c.comp_mas_sno == c.comp_mas_sno : c.comp_mas_sno == Comp)

                                select new CustomerMaster
                                {
                                    Cust_Sno = c.cust_mas_sno,
                                    Cust_Name = c.customer_name,
                                    PostboxNo = c.pobox_no,
                                    Address = c.physical_address,
                                    Region_SNO = c.region_id,
                                    //Region_Name=a.region_name,
                                    DistSno = c.district_sno,
                                    //DistName = b.district_name,
                                    WardSno = c.ward_sno,
                                    //WardName=d.ward_name,
                                    TinNo = c.tin_no,
                                    VatNo = c.vat_no,
                                    ConPerson = c.contact_person,
                                    Email = c.email_address,
                                    Phone = c.mobile_no,
                                    Checker = c.checker
                                }).ToList();
                    return list;
                }
                
                    
                
               
            }

        }
        public List<CustomerMaster> CustGetrep1(long Comp,long reg, long dist)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (reg==0)
                {
                    var list = (from c in context.customer_master
                                where (reg == 0 ? c.region_id == c.region_id : c.region_id == reg)

                               && (Comp == 0 ? c.comp_mas_sno == c.comp_mas_sno : c.comp_mas_sno == Comp)

                                select new CustomerMaster
                                {
                                    Cust_Sno = c.cust_mas_sno,
                                    Cust_Name = c.customer_name,
                                    PostboxNo = c.pobox_no,
                                    Address = c.physical_address,
                                    Region_SNO = c.region_id,
                                    //Region_Name=a.region_name,
                                    DistSno = c.district_sno,
                                    //DistName = b.district_name,
                                    WardSno = c.ward_sno,
                                    //WardName=d.ward_name,
                                    TinNo = c.tin_no,
                                    VatNo = c.vat_no,
                                    ConPerson = c.contact_person,
                                    Email = c.email_address,
                                    Phone = c.mobile_no,
                                    Checker = c.checker
                                }).ToList();
                    return list;
                }
                else
                {
                    var list = (from c in context.customer_master
                                where (reg == 0 ? c.region_id == c.region_id : c.region_id == reg)
                                             && (dist == 0 ? c.district_sno == c.district_sno : c.district_sno == dist)
                                             && (Comp == 0 ? c.comp_mas_sno == c.comp_mas_sno : c.comp_mas_sno == Comp)

                                select new CustomerMaster
                                {
                                    Cust_Sno = c.cust_mas_sno,
                                    Cust_Name = c.customer_name,
                                    PostboxNo = c.pobox_no,
                                    Address = c.physical_address,
                                    Region_SNO = c.region_id,
                                    //Region_Name=a.region_name,
                                    DistSno = c.district_sno,
                                    //DistName = b.district_name,
                                    WardSno = c.ward_sno,
                                    //WardName=d.ward_name,
                                    TinNo = c.tin_no,
                                    VatNo = c.vat_no,
                                    ConPerson = c.contact_person,
                                    Email = c.email_address,
                                    Phone = c.mobile_no,
                                    Checker = c.checker
                                }).ToList();
                    return list;
                }
                
                    
                
               
            }

        }
        //public List<CustomerMaster> GetAppPending(string fid)
        //{
        //    using (BIZINVOICEEntities context = new BIZINVOICEEntities())
        //    {
        //        var list = (from c in context.customer_master
        //                    //join det in context.places on c.loc equals det.place_sno.ToString()
        //                   // where c.facility_reg_sno == facino && c.status == "Pending" && c.posted_by != fid
        //                    select new CustomerMaster
        //                    {
        //                        Cust_Sno = c.cust_mas_sno,
        //                        Cust_Name = c.customer_name,
        //                        PostboxNo = c.pobox_no,
        //                        Address = c.physical_address,
        //                        TinNo = c.tin_no,
        //                        VatNo = c.vat_no,
        //                        ConPerson = c.contact_person,
        //                        Email = c.email_address,
        //                        Phone = c.mobile_no,
        //                    }).ToList();

        //        if (list != null && list.Count > 0)
        //            return list;
        //        else
        //            return null;
        //    }

        //}
        //public List<CustomerMaster> GetCust_App()
        //{
        //    using (BIZINVOICEEntities context = new BIZINVOICEEntities())
        //    {
        //        var adetails = (from c in context.customer_master
        //                       // join det in context.places on c.loc equals det.place_name
        //                       // where c.facility_reg_sno == facino && c.status == "Approved"
        //                        select new CustomerMaster
        //                        {
        //                            Cust_Sno = c.cust_mas_sno,
        //                            Cust_Name = c.customer_name,
        //                            PostboxNo = c.pobox_no,
        //                            Address = c.physical_address,
        //                            TinNo = c.tin_no,
        //                            VatNo = c.vat_no,
        //                            ConPerson = c.contact_person,
        //                            Email = c.email_address,
        //                            Phone = c.mobile_no,

        //                        }).ToList();
        //        if (adetails != null && adetails.Count > 0)
        //            return adetails;
        //        else
        //            return null;
        //    }
        //}
        public CustomerMaster EditCust(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var list = (from c in context.customer_master
                            join a in context.region_master on c.region_id equals a.region_sno
                            join b in context.district_master on c.district_sno equals b.district_sno
                            join d in context.ward_master on c.ward_sno equals d.ward_sno
                            where c.cust_mas_sno == sno 
                            select new CustomerMaster
                            {
                                Cust_Sno = c.cust_mas_sno,
                                Cust_Name = c.customer_name,
                                PostboxNo = c.pobox_no,
                                Address = c.physical_address,
                                Region_SNO = c.region_id,
                                Region_Name = a.region_name, //was commented
                                DistSno = c.district_sno,
                                DistName = b.district_name, //was commented
                                WardSno = c.ward_sno,
                                WardName = d.ward_name, //was commented
                                CompanySno=(long)c.comp_mas_sno,
                                TinNo = c.tin_no,
                                VatNo = c.vat_no,
                                ConPerson = c.contact_person,
                                Email = c.email_address,
                                Phone = c.mobile_no,

                            }).FirstOrDefault();

                if (list != null)
                    return list;
                else
                    return null;
            }

        }
        public CustomerMaster getTIN(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var list = (from c in context.customer_master
                            where c.cust_mas_sno == sno
                            select new CustomerMaster
                            {
                                TinNo = c.tin_no
                       
                            }).FirstOrDefault();

                if (list != null)
                    return list;
                else
                    return null;
            }

        }
        //public CustomerMaster ValidateLicense(String DName)
        //{
        //    using (BIZPAYEntities context = new BIZPAYEntities())
        //    {
        //        var validation = (from c in context.standard_divisions
        //                          where c.division_name == DName && c.facility_reg_sno == fno
        //                          select new Standard_Divisions
        //                          {
        //                              Division_Sno = c.division_sno,
        //                              Division_Name = c.division_name,
        //                              Address1 = c.address1,
        //                              Address2 = c.address2,
        //                              Loc = c.loc,
        //                              Phone = c.phone,
        //                              Status = c.status,
        //                              Facility_reg_Sno = (long)c.facility_reg_sno,
        //                              Facility_Name = c.facility_name

        //                          }).FirstOrDefault();
        //        if (validation != null)
        //            return validation;
        //        else
        //            return null;
        //    }
        //}
        public bool ValidateCount(string dno,string tin)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                var validation = (from c in context.customer_master
                                 where (c.customer_name == dno) || (c.tin_no == tin && c.tin_no != null && c.tin_no != "")
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public string IsDuplicateCustomer(string mobileNumber,string email,string tinNumber)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                /*var existsName = from c in context.customer_master where c.customer_name.ToLower().Equals(customerName.ToLower()) select c;
                if (existsName.Count() > 0) return "Exists name";*/
                var existsMobile = from c in context.customer_master where c.mobile_no.ToLower().Equals(mobileNumber.ToLower()) select c;
                if (existsMobile.Count() > 0) return "Exists phone";
                else if (tinNumber != null && tinNumber.Length > 0)
                {
                    var existsTinNumber = from c in context.customer_master where c.tin_no.ToLower().Equals(tinNumber.ToLower()) select c;
                    if (existsTinNumber.Count() > 0) return "Exists tin";
                }
                else if (email != null && email.Length > 0)
                {
                    var existsEmail = from c in context.customer_master where c.email_address.ToLower().Equals(email.ToLower()) select c;
                    if (existsEmail.Count() > 0) return "Exists email";
                }
                else
                {
                    return "";
                }
                return "";
            }
        }

        public string IsDuplicateCustomer(string mobileNumber, string email, string tinNumber,long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                /*var existsName = from c in context.customer_master where (c.customer_name.ToLower().Equals(customerName.ToLower()) && c.cust_mas_sno != sno) select c;
                if (existsName.Count() > 0) return "Exists name";*/
                var existsMobile = from c in context.customer_master where (c.mobile_no.ToLower().Equals(mobileNumber.ToLower()) && c.cust_mas_sno != sno) select c;
                if (existsMobile.Count() > 0) return "Exists phone";
                else if (tinNumber != null && tinNumber.Length > 0)
                {
                    var existsTinNumber = from c in context.customer_master where (c.tin_no.ToLower().Equals(tinNumber.ToLower()) && c.cust_mas_sno != sno) select c;
                    if (existsTinNumber.Count() > 0) return "Exists tin";
                }
                else if (email != null && email.Length > 0)
                {
                    var existsEmail = from c in context.customer_master where (c.email_address.ToLower().Equals(email.ToLower()) && c.cust_mas_sno != sno) select c;
                    if (existsEmail.Count() > 0) return "Exists email";
                }
                else
                {
                    return "";
                }
                return "";
            }
        }


        public bool ValidateDeleteorUpdate(long ssno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
               

                var validationUpdate2 = (from v in context.customer_master
                                         join b in context.invoice_master on v.cust_mas_sno equals b.cust_mas_sno
                                         where b.cust_mas_sno == ssno
                                         select v);
                
                if (validationUpdate2.Count() != 0 )
                    return true;
                else
                    return false;
            }
        }

        public bool isExistCustomer(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var exists = context.customer_master.Find(sno);
                return exists != null;
            }
        }

        public CustomerMaster FindCustomer(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var found = context.customer_master.Find(sno);
                CustomerMaster customerMaster = new CustomerMaster();
                customerMaster.Cust_Sno = found.cust_mas_sno;
                customerMaster.Cust_Sno = found.cust_mas_sno;
                customerMaster.Cust_Name = found.customer_name;
                customerMaster.PostboxNo = found.pobox_no;
                customerMaster.Address = found.physical_address;
                customerMaster.Region_SNO = found.region_id;
                //customerMaster.Region_Name = found.region_name, //was commented
                customerMaster.DistSno = found.district_sno;
                //customerMaster.DistName = b.district_name, //was commented
                customerMaster.WardSno = found.ward_sno;
                //customerMaster.WardName = d.ward_name, //was commented
                customerMaster.CompanySno = (long)found.comp_mas_sno;
                customerMaster.TinNo = found.tin_no;
                customerMaster.VatNo = found.vat_no;
                customerMaster.ConPerson = found.contact_person;
                customerMaster.Email = found.email_address;
                customerMaster.Phone = found.mobile_no;
                return customerMaster;
            }
        }



        # endregion method
    }
}
