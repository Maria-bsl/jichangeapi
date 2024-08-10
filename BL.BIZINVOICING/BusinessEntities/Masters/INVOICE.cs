using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BL.BIZINVOICING.BusinessEntities.Common;
using DaL.BIZINVOICING.EDMX;
using iTextSharp.text;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class INVOICE
    {
        #region Properties
        public long Inv_Mas_Sno { get; set; }
        public long Inv_Det_Sno { get; set; }
        public DateTime? Invoice_Date { get; set; }
        public String Payment_Type { get; set; }
        public String Invoice_No { get; set; }
        public DateTime? Due_Date { get; set; }
        public DateTime? Invoice_Expired_Date { get; set; }
        public long Chus_Mas_No { get; set; }
        public String Chus_Name { get; set; }
        public long Com_Mas_Sno { get; set; }
        public String Company_Name { get; set; }

        public String Inv_Remarks { get; set; }
        public String Remarks { get; set; }

        public String vat_category { get; set; }
        public string Currency_Code { get; set; }
        public string Currency_Name { get; set; }

        public decimal Total_Without_Vt { get; set; }
        public  decimal Total_Vt { get; set; }
        public  decimal Total { get; set; }
        public  decimal Item_Qty { get; set; }
        public  decimal Item_Unit_Price { get; set; }
        public decimal Item_Total_Amount { get; set; }
        public decimal Vat_Percentage { get; set; }
        public decimal Vat_Amount { get; set; }
        public decimal Item_Without_vat{ get; set; }
        public string Item_Description { get; set; }
        public string AuditBy { get; set; }

        public string warrenty { get; set; }
        public string Vat_Type { get; set; }
        public string goods_status { get; set; }
        public string delivery_status { get; set; }
        public DateTime  Audit_Date { get; set; }


        public int ?grand_count { get; set; }
        public int? daily_count { get; set; }
        public string approval_status { get; set; }
        public DateTime approval_date { get; set; }
        public DateTime p_date { get; set; }
        public String Customer_ID_Type { get; set; }
        public String Customer_ID_No { get; set; }
        public long Zreport_Sno { get; set; }
        public string Zreport_Status { get; set; }
        public string Zreport_Status1 { get; set; }
        public DateTime Zreport_Date { get; set; }
        public DateTime? Zreport_Date1 { get; set; }
        public string Zreport_Date2 { get; set; }
        public string Control_No { get; set; }
        public String Reason { get; set; }
        public string Status { get; set; }
        public string Mobile { get;  set; }
        public string Email { get; set; }
        #endregion Properties



        #region Methods


        public long Addinvoi(INVOICE sc)
        {

            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                invoice_master pc = new invoice_master()
                {
                    invoice_no = sc.Invoice_No,
                    invoice_date = sc.Invoice_Date,
                    due_date = sc.Due_Date,
                    invoice_expired = sc.Invoice_Expired_Date,
                    payment_type = sc.Payment_Type,
                    comp_mas_sno = sc.Com_Mas_Sno,
                    cust_mas_sno = sc.Chus_Mas_No,
                    currency_code = sc.Currency_Code,
                    total_without_vat = sc.Total_Without_Vt,
                    total_amount = sc.Total,
                    vat_amount = sc.Vat_Amount,
                    inv_remarks = sc.Inv_Remarks,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                    approval_status = sc.approval_status,
                    approval_date = sc.approval_date,
                    control_no = sc.Control_No,
                    warrenty= sc.warrenty,
                    goods_status=sc.goods_status,
                    delivery_status=sc.delivery_status,
                    customer_id_type = sc.Customer_ID_Type,
                    customer_id_no = sc.Customer_ID_No,
                    p_date = DateTime.Now
                 
                };
                context.invoice_master.Add(pc);
                context.SaveChanges();
                return pc.inv_mas_sno;
            }
        }

        public void AddInvoiceDetails(INVOICE sc)
        {
                using (BIZINVOICEEntities context = new BIZINVOICEEntities())
                {
                    invoice_details pc = new invoice_details()
                    {

                        inv_mas_sno = sc.Inv_Mas_Sno,
                        item_description = sc.Item_Description,
                        item_qty = sc.Item_Qty,
                        item_unit_price = sc.Item_Unit_Price,
                        item_total_amount = sc.Item_Total_Amount,
                        vat_percentage = sc.Vat_Percentage,
                        vat_amount = sc.Vat_Amount,
                        item_without_vat = sc.Item_Without_vat,
                        vat_category = sc.vat_category,
                        remarks = sc.Remarks,
                        vat_type = sc.Vat_Type
                    };
                    context.invoice_details.Add(pc);
                    context.SaveChanges();
                }
           
        }
        public void AddZReport(INVOICE sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                zreport_details pc = new zreport_details()
                {

                    zreport_date = sc.Zreport_Date,
                    zreport_status = sc.Zreport_Status,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now
                };
                context.zreport_details.Add(pc);
                context.SaveChanges();
            }

        }
        public bool ValidateZReport(DateTime dt)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.zreport_details
                                  where (c.zreport_date == dt)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool ValidateControl(string cn)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.invoice_master
                                  where ((!string.IsNullOrEmpty(c.control_no)) && (c.control_no == cn)) //&& c.comp_mas_sno == comno
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool ValidateNo(string no, long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.invoice_master
                                  where c.invoice_no == no && c.comp_mas_sno == cno //&& c.comp_mas_sno == comno
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<INVOICE> GETZReports()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.zreport_details
                                select new INVOICE
                                {
                                    Zreport_Sno = c.zreport_sno,
                                    Zreport_Date = (DateTime)c.zreport_date,
                                    Zreport_Status = c.zreport_status

                                }).OrderBy(z => z.Zreport_Sno).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public void DeleteInvoicedet(INVOICE bt)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                context.invoice_details.RemoveRange(context.invoice_details.Where(c => c.inv_mas_sno == bt.Inv_Mas_Sno));
                context.SaveChanges();
            }
        }
        public void UpdateInvoidet(INVOICE dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.invoice_details
                                         where u.inv_det_sno == dep.Inv_Det_Sno
                                         select u).FirstOrDefault();
                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.inv_mas_sno = dep.Inv_Mas_Sno;
                    UpdateContactInfo.item_description = dep.Item_Description;
                    UpdateContactInfo.item_qty = dep.Item_Qty;
                    UpdateContactInfo.item_unit_price = dep.Item_Unit_Price;
                    UpdateContactInfo.item_total_amount = dep.Item_Total_Amount;
                    UpdateContactInfo.vat_percentage = dep.Vat_Percentage;
                    UpdateContactInfo.vat_amount = dep.Vat_Amount;
                    UpdateContactInfo.item_without_vat = dep.Item_Without_vat;
                    UpdateContactInfo.remarks = Remarks;
                    context.SaveChanges();
                }
            }
        }
        public long GetMax()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                return  context.invoice_master.Select(p => p.grand_count).Max() ?? 0;

            }
        }
        public int GetDaily()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                //int dailycount = 0;

                var dcount = context.invoice_master.ToList().Where(x => Convert.ToDateTime(x.p_date) == Convert.ToDateTime(System.DateTime.Now.Date));
                if (dcount.Count() > 0)
                {
                    //var item = dcount.Max(x => x.daily_count);
                    return dcount.Max(x => x.daily_count) ?? 0;
                }
                else
                {
                    return 0;
                }
                
            }
        }
        public void UpdateInvoiMas(INVOICE dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.invoice_master
                                         where u.inv_mas_sno == dep.Inv_Mas_Sno 
                                             select u).FirstOrDefault();
                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.invoice_no = dep.Invoice_No;
                    UpdateContactInfo.invoice_date = dep.Invoice_Date;
                    UpdateContactInfo.due_date = dep.Due_Date;
                    UpdateContactInfo.invoice_expired = dep.Invoice_Expired_Date;
                    UpdateContactInfo.payment_type = dep.Payment_Type;
                    UpdateContactInfo.comp_mas_sno = dep.Com_Mas_Sno;
                    UpdateContactInfo.cust_mas_sno = dep.Chus_Mas_No;
                    UpdateContactInfo.currency_code = dep.Currency_Code;
                    UpdateContactInfo.total_without_vat = dep.Total_Without_Vt;
                    UpdateContactInfo.total_amount = dep.Total;
                    UpdateContactInfo.vat_amount = dep.Vat_Amount;
                    UpdateContactInfo.inv_remarks = dep.Inv_Remarks;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    //UpdateContactInfo.posted_date = DateTime.Now;
                    UpdateContactInfo.customer_id_type = dep.Customer_ID_Type;
                    UpdateContactInfo.customer_id_no = dep.Customer_ID_No;
                    context.SaveChanges();
                }
            }
        }
        public void UpdateStatus(INVOICE dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.invoice_master
                                         where u.inv_mas_sno == dep.Inv_Mas_Sno
                                         select u).FirstOrDefault();
                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.approval_status = dep.approval_status;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    //UpdateContactInfo.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        public int Getinvcountind(long uid,string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "today")
                {

                    return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Day == DateTime.Now.Day);
                }
                else if (name == "week")
                {
                    var d7 = DateTime.Now.AddDays(-7);
                    var d1 = DateTime.Now.AddDays(-1);
                    return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1));

                }
                else if (name == "mnth")
                {
                    return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Month == DateTime.Now.Month);
                }
                else
                {
                    return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Year == DateTime.Now.Year);
                }
                //var adetails = (from c in context.invoice_master
                //                where c.comp_mas_sno==uid && c.posted_date==date
                //                select c).ToList();
                //if (adetails != null && adetails.Count > 0)
                //    return adetails.Count;
                //else
                //    return 0;
            }
        }
        public int Getinvcount(long uid, DateTime date)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.comp_mas_sno == uid && c.posted_date == date
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        public int GetAcount(long uid, string Aa,DateTime date)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                where c.comp_mas_sno == uid && c1.vat_type == Aa&&c.posted_date==date
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        public int GetCount_D(string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "today")
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.posted_date == DateTime.Now
                                    select c).ToList();
                    return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Day == DateTime.Now.Day);
                }
                else if (name == "week")
                {
                    var d7 = DateTime.Now.AddDays(-7);
                    var d1 = DateTime.Now.AddDays(-1);
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    //where System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1)
                                    where c.posted_date >= d7 && c.posted_date <=d1
                                    select c).ToList();
                    return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1));

                }
                else if (name == "mnth")
                {
                    var d1 = DateTime.Now.AddMonths(-1);
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    //where c.posted_date.Value.Month == DateTime.Now.Month
                                    where c.posted_date >= d1 && c.posted_date <= DateTime.Now
                                    select c).ToList();
                    return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Month == DateTime.Now.Month);
                }
                else
                {
                    var d1 = DateTime.Now.AddYears(-1);
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    //where c.posted_date.Value.Year == DateTime.Now.Year
                                    where c.posted_date >= d1 && c.posted_date <= DateTime.Now
                                    select c).ToList();
                    return adetails.Count;
                    // return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Year == DateTime.Now.Year);
                }
                
            }
        }
        public int GetAcount1(long uid, string Aa,string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "today")
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Aa && c.posted_date.Value.Day == DateTime.Now.Day
                                    select c).ToList();
                        return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Day == DateTime.Now.Day);
                }
                else if (name == "week")
                {
                    var d7 = DateTime.Now.AddDays(-7);
                    var d1 = DateTime.Now.AddDays(-1);
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Aa &&
                                    System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1)
                                    
                    select c).ToList();
                    return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1));

                }
                else if (name == "mnth")
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Aa && c.posted_date.Value.Month == DateTime.Now.Month
                                    select c).ToList();
                    return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Month == DateTime.Now.Month);
                }
                else
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Aa && c.posted_date.Value.Year == DateTime.Now.Year
                                    select c).ToList();
                    return adetails.Count;
                    // return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Year == DateTime.Now.Year);
                }
                //var adetails = (from c in context.invoice_master
                //                join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                //                where c.comp_mas_sno == uid && c1.vat_type == Aa&&c.posted_date==date
                //                select c).ToList();
                //if (adetails != null && adetails.Count > 0)
                //    return adetails.Count;
                //else
                //    return 0;
            }
        }
        public int GetBcount1(long uid, string Bb, string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "today")
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Bb && c.posted_date.Value.Day == DateTime.Now.Day
                                    select c).ToList();
                        return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Day == DateTime.Now.Day);
                }
                else if (name == "week")
                {
                    var d7 = DateTime.Now.AddDays(-7);
                    var d1 = DateTime.Now.AddDays(-1);
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Bb &&
                                    System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1)
                                    
                    select c).ToList();
                    return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1));

                }
                else if (name == "mnth")
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Bb && c.posted_date.Value.Month == DateTime.Now.Month
                                    select c).ToList();
                    return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Month == DateTime.Now.Month);
                }
                else
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Bb && c.posted_date.Value.Year == DateTime.Now.Year
                                    select c).ToList();
                    return adetails.Count;
                    // return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Year == DateTime.Now.Year);
                }
                //var adetails = (from c in context.invoice_master
                //                join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                //                where c.comp_mas_sno == uid && c1.vat_type == Aa&&c.posted_date==date
                //                select c).ToList();
                //if (adetails != null && adetails.Count > 0)
                //    return adetails.Count;
                //else
                //    return 0;
            }
        }
        public int GetCcount1(long uid, string Cc, string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "today")
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Cc && c.posted_date.Value.Day == DateTime.Now.Day
                                    select c).ToList();
                        return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Day == DateTime.Now.Day);
                }
                else if (name == "week")
                {
                    var d7 = DateTime.Now.AddDays(-7);
                    var d1 = DateTime.Now.AddDays(-1);
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Cc &&
                                    System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1)
                                    
                    select c).ToList();
                    return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1));

                }
                else if (name == "mnth")
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Cc && c.posted_date.Value.Month == DateTime.Now.Month
                                    select c).ToList();
                    return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Month == DateTime.Now.Month);
                }
                else
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Cc && c.posted_date.Value.Year == DateTime.Now.Year
                                    select c).ToList();
                    return adetails.Count;
                    // return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Year == DateTime.Now.Year);
                }
                //var adetails = (from c in context.invoice_master
                //                join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                //                where c.comp_mas_sno == uid && c1.vat_type == Aa&&c.posted_date==date
                //                select c).ToList();
                //if (adetails != null && adetails.Count > 0)
                //    return adetails.Count;
                //else
                //    return 0;
            }
        }
        public int GetDcount1(long uid, string Dd, string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "today")
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Dd && c.posted_date.Value.Day == DateTime.Now.Day
                                    select c).ToList();
                        return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Day == DateTime.Now.Day);
                }
                else if (name == "week")
                {
                    var d7 = DateTime.Now.AddDays(-7);
                    var d1 = DateTime.Now.AddDays(-1);
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Dd &&
                                    System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1)
                                    
                    select c).ToList();
                    return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1));

                }
                else if (name == "mnth")
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Dd && c.posted_date.Value.Month == DateTime.Now.Month
                                    select c).ToList();
                    return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Month == DateTime.Now.Month);
                }
                else
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Dd && c.posted_date.Value.Year == DateTime.Now.Year
                                    select c).ToList();
                    return adetails.Count;
                    // return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Year == DateTime.Now.Year);
                }
                //var adetails = (from c in context.invoice_master
                //                join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                //                where c.comp_mas_sno == uid && c1.vat_type == Aa&&c.posted_date==date
                //                select c).ToList();
                //if (adetails != null && adetails.Count > 0)
                //    return adetails.Count;
                //else
                //    return 0;
            }
        }
        public int GetEcount1(long uid, string Ee, string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "today")
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Ee && c.posted_date.Value.Day == DateTime.Now.Day
                                    select c).ToList();
                        return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Day == DateTime.Now.Day);
                }
                else if (name == "week")
                {
                    var d7 = DateTime.Now.AddDays(-7);
                    var d1 = DateTime.Now.AddDays(-1);
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Ee &&
                                    System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1)
                                    
                    select c).ToList();
                    return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(p.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1));

                }
                else if (name == "mnth")
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Ee && c.posted_date.Value.Month == DateTime.Now.Month
                                    select c).ToList();
                    return adetails.Count;
                    //return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Month == DateTime.Now.Month);
                }
                else
                {
                    var adetails = (from c in context.invoice_master
                                    join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                    where c.comp_mas_sno == uid && c1.vat_type == Ee && c.posted_date.Value.Year == DateTime.Now.Year
                                    select c).ToList();
                    return adetails.Count;
                    // return context.invoice_master.Where(p => p.comp_mas_sno == uid).Count(p => p.posted_date.Value.Year == DateTime.Now.Year);
                }
                //var adetails = (from c in context.invoice_master
                //                join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                //                where c.comp_mas_sno == uid && c1.vat_type == Aa&&c.posted_date==date
                //                select c).ToList();
                //if (adetails != null && adetails.Count > 0)
                //    return adetails.Count;
                //else
                //    return 0;
            }
        }
        public int GetBcount(long uid, string Bb, DateTime date)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                where c.comp_mas_sno == uid && c1.vat_type == Bb && c.posted_date == date
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        public int GetCcount(long uid, string Cc, DateTime date)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                where c.comp_mas_sno == uid && c1.vat_type == Cc && c.posted_date == date
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        public int GetDcount(long uid, string Dd, DateTime date)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                where c.comp_mas_sno == uid && c1.vat_type == Dd && c.posted_date == date
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }



        public int GetEcount(long uid, string Ee, DateTime date)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                where c.comp_mas_sno == uid && c1.vat_type == Ee && c.posted_date == date
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        public int Getinvcountnlyappind(long uid,String name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "today")
                {
                     var adetails = (from c in context.invoice_master
                                      where c.approval_status == "2" && c.comp_mas_sno == uid && c.posted_date==DateTime.Now
                                      select c).ToList();
                    return adetails.Count;
                }
                else if (name == "week")
                {
                    var d7 = DateTime.Now.AddDays(-7);
                    var d1 = DateTime.Now.AddDays(-1);
                    var adetails = (from c in context.invoice_master
                                    where c.approval_status == "2" && c.comp_mas_sno == uid && c.posted_date.Value.Month == DateTime.Now.Month&&
                                    System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1)
                                    select c).ToList();
                    return adetails.Count;

                }
                else if (name == "mnth")
                {
                    var adetails = (from c in context.invoice_master
                                    where c.approval_status == "2" && c.comp_mas_sno == uid && c.posted_date.Value.Month == DateTime.Now.Month
                                    select c).ToList();
                    return adetails.Count;
                }
                else
                {
                    var adetails = (from c in context.invoice_master
                                    where c.approval_status == "2" && c.comp_mas_sno == uid && c.posted_date.Value.Year == DateTime.Now.Year
                                    select c).ToList();
                    return adetails.Count;
                }
                //var adetails = (from c in context.invoice_master
                //                where c.approval_status=="2" &&c.comp_mas_sno== uid&&c.posted_date==date
                //                select c).ToList();
                //if (adetails != null && adetails.Count > 0)
                //    return adetails.Count;
                //else
                //    return 0;
            }
        }
        public int Getinvcountnlyapp(long uid, DateTime date)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.approval_status == "2" && c.comp_mas_sno == uid && c.posted_date == date
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        public long? GetCount()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.approval_status == "2" 
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        public long? GetCount_C(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.approval_status == "2" && c.comp_mas_sno == cno
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }
        public int GetExpired_C(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.invoice_expired != null && c.invoice_expired <= DateTime.Now && c.comp_mas_sno == cno
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }

        public long? GetExpired_VendorCount(long company_sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.invoice_expired != null && c.invoice_expired <= DateTime.Now && c.comp_mas_sno == company_sno
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }

        public long? GetDue_VendorCount(long company_sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                where c.due_date >= DateTime.Now && (c.invoice_expired == null || c.invoice_expired > DateTime.Now)
                                && c.comp_mas_sno == company_sno
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }



        public long? GetPendingInvoice_VendorCount(long company_sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.approval_status.Contains("Pending") && c.comp_mas_sno == company_sno
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }




        public int GetExpired_Count()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.invoice_expired != null && c.invoice_expired <= DateTime.Now
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }

        public int GetDue_Count()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                where c.due_date >= DateTime.Now && (c.invoice_expired == null || c.invoice_expired > DateTime.Now)
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }

        public int GetDueCountByBranch(long branch)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join c1 in context.invoice_details on c.inv_mas_sno equals c1.inv_mas_sno
                                where c.due_date >= DateTime.Now && (c.invoice_expired == null || c.invoice_expired > DateTime.Now)
                                && c.company_master.branch_sno == branch
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }

        public int GetExpiredCountByBranch(long branch)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.invoice_expired != null && c.invoice_expired <= DateTime.Now && c.company_master.branch_sno == branch
                                select c).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails.Count;
                else
                    return 0;
            }
        }

        public decimal Gettotamtwithvat(long uid,DateTime date)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.comp_mas_sno== uid&& c.posted_date==date
                                select new INVOICE
                                {
                                    Total = (long)c.total_amount,
                                }).ToList().Sum(x => x.Total);
                
                if (adetails != null )
                    return adetails;
                else
                    return 0;
            }
        }
        public decimal Gettotamtwithvatind(long uid,string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "today")
                {
                    var adetails = (from c in context.invoice_master
                                    where c.comp_mas_sno == uid && c.posted_date == DateTime.Now
                                    select new INVOICE
                                    {
                                        Total = (long)c.total_amount,
                                    }).ToList().Sum(x => x.Total);
                    return adetails;
                }
                else if (name == "week")
                {
                    var d7 = DateTime.Now.AddDays(-7);
                    var d1 = DateTime.Now.AddDays(-1);
                    var adetails = (from c in context.invoice_master
                                    where  c.comp_mas_sno == uid && c.posted_date.Value.Month == DateTime.Now.Month &&
                                    System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1)
                                    select new INVOICE
                                    {
                                        Total = (long)c.total_amount,
                                    }).ToList().Sum(x => x.Total);
                    return adetails;

                }
                else if (name == "mnth")
                {
                    var adetails = (from c in context.invoice_master
                                    where  c.comp_mas_sno == uid && c.posted_date.Value.Month == DateTime.Now.Month
                                    select new INVOICE
                                    {
                                        Total = (long)c.total_amount,
                                    }).ToList().Sum(x => x.Total);
                    return adetails;
                }
                else
                {
                    var adetails = (from c in context.invoice_master
                                    where  c.comp_mas_sno == uid && c.posted_date.Value.Year == DateTime.Now.Year
                                    select new INVOICE
                                    {
                                        Total = (long)c.total_amount,
                                    }).ToList().Sum(x => x.Total);
                    return adetails;
                }
                //var adetails = (from c in context.invoice_master
                //                where c.comp_mas_sno== uid&& c.posted_date==date
                //                select new INVOICE
                //                {
                //                    Total = (long)c.total_amount,
                //                }).ToList().Sum(x => x.Total);
                
                //if (adetails != null )
                //    return adetails;
                //else
                //    return 0;
            }
        }
        public decimal Gettotamtwithoutvat(long uid,DateTime date)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.comp_mas_sno== uid&&c.posted_date==date
                                select new INVOICE
                                {
                                    Total_Without_Vt = (long)c.total_without_vat,
                                }).ToList().Sum(x => x.Total_Without_Vt);
                
                if (adetails != null )
                    return adetails;
                else
                    return 0;
            }
        }
        public decimal Gettotamtwithoutvatind(long uid,string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "today")
                {
                    var adetails = (from c in context.invoice_master
                                    where c.comp_mas_sno == uid && c.posted_date == DateTime.Now
                                    select new INVOICE
                                    {
                                        Total_Without_Vt = (long)c.total_without_vat,
                                    }).ToList().Sum(x => x.Total_Without_Vt);
                    return adetails;
                }
                else if (name == "week")
                {
                    var d7 = DateTime.Now.AddDays(-7);
                    var d1 = DateTime.Now.AddDays(-1);
                    var adetails = (from c in context.invoice_master
                                    where c.comp_mas_sno == uid && c.posted_date.Value.Month == DateTime.Now.Month &&
                                    System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1)
                                    select new INVOICE
                                    {
                                        Total_Without_Vt = (long)c.total_without_vat,
                                    }).ToList().Sum(x => x.Total_Without_Vt);
                    return adetails;

                }
                else if (name == "mnth")
                {
                    var adetails = (from c in context.invoice_master
                                    where c.comp_mas_sno == uid && c.posted_date.Value.Month == DateTime.Now.Month
                                    select new INVOICE
                                    {
                                        Total_Without_Vt = (long)c.total_without_vat,
                                    }).ToList().Sum(x => x.Total_Without_Vt);
                    return adetails;
                }
                else
                {
                    var adetails = (from c in context.invoice_master
                                    where c.comp_mas_sno == uid && c.posted_date.Value.Year == DateTime.Now.Year
                                    select new INVOICE
                                    {
                                        Total_Without_Vt = (long)c.total_without_vat,
                                   }).ToList().Sum(x => x.Total_Without_Vt);
                    return adetails;
                }
                //var adetails = (from c in context.invoice_master
                //                where c.comp_mas_sno== uid&&c.posted_date==date
                //                select new INVOICE
                //                {
                //                    Total_Without_Vt = (long)c.total_without_vat,
                //                }).ToList().Sum(x => x.Total_Without_Vt);
                
                //if (adetails != null )
                //    return adetails;
                //else
                //    return 0;
            }
        }
        public decimal Gettotvat(long uid,DateTime date)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.comp_mas_sno== uid&&c.posted_date==date
                                select new INVOICE
                                {
                                    Vat_Amount = (long)c.vat_amount,
                                }).ToList().Sum(x => x.Vat_Amount);
                
                if (adetails != null )
                    return adetails;
                else
                    return 0;
            }
        }
        public decimal Gettotvatind(long uid,string name)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (name == "today")
                {
                    var adetails = (from c in context.invoice_master
                                    where c.comp_mas_sno == uid && c.posted_date == DateTime.Now
                                    select new INVOICE
                                    {
                                        Vat_Amount = (long)c.vat_amount,
                                    }).ToList().Sum(x => x.Vat_Amount);
                    return adetails;
                }
                else if (name == "week")
                {
                    var d7 = DateTime.Now.AddDays(-7);
                    var d1 = DateTime.Now.AddDays(-1);
                    var adetails = (from c in context.invoice_master
                                    where c.comp_mas_sno == uid && c.posted_date.Value.Month == DateTime.Now.Month &&
                                    System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) >= System.Data.Entity.DbFunctions.TruncateTime(d7) && System.Data.Entity.DbFunctions.TruncateTime(c.posted_date) <= System.Data.Entity.DbFunctions.TruncateTime(d1)
                                    select new INVOICE
                                    {
                                        Vat_Amount = (long)c.vat_amount,
                                    }).ToList().Sum(x => x.Vat_Amount);
                    return adetails;

                }
                else if (name == "mnth")
                {
                    var adetails = (from c in context.invoice_master
                                    where c.comp_mas_sno == uid && c.posted_date.Value.Month == DateTime.Now.Month
                                    select new INVOICE
                                    {
                                        Vat_Amount = (long)c.vat_amount,
                                    }).ToList().Sum(x => x.Vat_Amount);
                    return adetails;
                }
                else
                {
                    var adetails = (from c in context.invoice_master
                                    where c.comp_mas_sno == uid && c.posted_date.Value.Year == DateTime.Now.Year
                                    select new INVOICE
                                    {
                                        Vat_Amount = (long)c.vat_amount,
                                    }).ToList().Sum(x => x.Vat_Amount);
                    return adetails;
                }
                //var adetails = (from c in context.invoice_master
                //                where c.comp_mas_sno== uid&&c.posted_date==date
                //                select new INVOICE
                //                {
                //                    Vat_Amount = (long)c.vat_amount,
                //                }).ToList().Sum(x => x.Vat_Amount);

                //if (adetails != null )
                //    return adetails;
                //else
                //    return 0;
            }
        }

        public INVOICE GetINVOICEMas1(long cno, long invid)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join cmp in context.company_master on c.comp_mas_sno equals cmp.comp_mas_sno
                                join cur in context.currency_master on c.currency_code equals cur.currency_code
                                where c.comp_mas_sno == cno && c.inv_mas_sno == invid
                                select new INVOICE
                                {
                                    Com_Mas_Sno = c.company_master.comp_mas_sno,
                                    Company_Name = cmp.company_name,
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    Invoice_Date = c.invoice_date,
                                    Due_Date = c.due_date,
                                    Invoice_Expired_Date = c.invoice_expired,
                                    Payment_Type = c.payment_type,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Chus_Name = det.customer_name,
                                    Currency_Code = c.currency_code,
                                    Currency_Name = cur.currency_name,
                                    Control_No = c.control_no,
                                    Remarks = c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    Total = (decimal)c.total_amount,
                                    Total_Vt = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    //Vat_Amount = (long)c.vat_amount,
                                    warrenty = c.warrenty,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    grand_count = (int)c.grand_count,
                                    daily_count = (int)c.daily_count,
                                    approval_status = c.approval_status,
                                    approval_date = (DateTime) c.approval_date

                                }).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }

        public INVOICE GetINVOICEMas2(long cno, long invid)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join cmp in context.company_master on c.comp_mas_sno equals cmp.comp_mas_sno
                                join cur in context.currency_master on c.currency_code equals cur.currency_code
                                where c.comp_mas_sno == cno && c.inv_mas_sno == invid 
                                select new INVOICE
                                {
                                    Com_Mas_Sno = c.company_master.comp_mas_sno,
                                    Company_Name = cmp.company_name,
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    Invoice_Date = c.invoice_date,
                                    Due_Date = c.due_date,
                                    Invoice_Expired_Date = c.invoice_expired,
                                    Payment_Type = c.payment_type,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Chus_Name = det.customer_name,
                                    Currency_Code = c.currency_code,
                                    Currency_Name = cur.currency_name,
                                    Control_No = c.control_no,
                                    Remarks = c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    Total = (decimal)c.total_amount,
                                    Total_Vt = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    //Vat_Amount = (long)c.vat_amount,
                                    warrenty = c.warrenty,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    grand_count = (int)c.grand_count,
                                    daily_count = (int)c.daily_count,
                                    approval_status = c.approval_status,
                                    approval_date = approval_date

                                }).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }

        public List<INVOICE> GetINVOICEMas(long cno, int? page,int? limit)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var invoices = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join cmp in context.company_master on c.comp_mas_sno equals cmp.comp_mas_sno
                                join cur in context.currency_master on c.currency_code equals cur.currency_code
                                where !(from d in context.payment_details
                                        select d.invoice_sno).Contains(c.invoice_no) && 
                                (c.comp_mas_sno == cno)
                                select new INVOICE
                                {
                                    Com_Mas_Sno = c.company_master.comp_mas_sno,
                                    Company_Name = cmp.company_name,
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    Invoice_Date = c.invoice_date,
                                    Due_Date = c.due_date,
                                    Invoice_Expired_Date = c.invoice_expired,
                                    Payment_Type = c.payment_type,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Chus_Name = det.customer_name,
                                    Currency_Code = c.currency_code,
                                    Currency_Name = cur.currency_name,
                                    Control_No = c.control_no,
                                    Remarks = c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    Total = (decimal)c.total_amount,
                                    Total_Vt = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    //Vat_Amount = (long)c.vat_amount,
                                    warrenty = c.warrenty,
                                    AuditBy = c.posted_by,
                                    p_date = (DateTime)c.posted_date,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    grand_count = (int)c.grand_count,
                                    daily_count = (int)c.daily_count,
                                    approval_status = c.approval_status,
                                    approval_date = approval_date,

                                }).OrderBy(i => i.p_date);

                if (page == null && limit == null)
                {
                    return invoices.ToList() ?? new List<INVOICE>();
                }
                else
                {
                    var results = invoices.Skip(((int)page - 1) * (int) limit).Take((int)limit); 
                    return results.ToList() ?? new List<INVOICE>();
                }
            }
        }

        public List<INVOICE> GetINVOICEMas(long cno) 
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join cmp in context.company_master on c.comp_mas_sno equals cmp.comp_mas_sno
                                join cur in context.currency_master on c.currency_code equals cur.currency_code
                                where !(from d in context.payment_details
                                        select d.invoice_sno).Contains(c.invoice_no)
                                && (c.comp_mas_sno == cno)
                                select new INVOICE
                                {
                                    Com_Mas_Sno = c.company_master.comp_mas_sno,
                                    Company_Name = cmp.company_name,
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    Invoice_Date = c.invoice_date,
                                    Due_Date = c.due_date,
                                    Invoice_Expired_Date = c.invoice_expired,
                                    Payment_Type = c.payment_type,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Chus_Name = det.customer_name,
                                    Currency_Code = c.currency_code,
                                    Currency_Name = cur.currency_name,
                                    Control_No = c.control_no,
                                    Remarks = c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    Total = (decimal)c.total_amount,
                                    Total_Vt = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    //Vat_Amount = (long)c.vat_amount,
                                    warrenty = c.warrenty,
                                    AuditBy = c.posted_by,
                                    p_date = (DateTime)c.posted_date,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    grand_count = (int)c.grand_count,
                                    daily_count = (int)c.daily_count,
                                    approval_status = c.approval_status,
                                    approval_date = approval_date,

                                }).ToList();


                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public List<INVOICE> GetINVOICEMas_D(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join cmp in context.company_master on c.comp_mas_sno equals cmp.comp_mas_sno
                                join cur in context.currency_master on c.currency_code equals cur.currency_code
                                join d in context.payment_details on c.comp_mas_sno equals d.comp_mas_sno
                                join e in context.payment_details on c.control_no equals e.control_no
                                where c.comp_mas_sno == cno
                                select new INVOICE
                                {
                                    Com_Mas_Sno = c.company_master.comp_mas_sno,
                                    Company_Name = cmp.company_name,
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    Invoice_Date = c.invoice_date,
                                    Due_Date = c.due_date,
                                    Invoice_Expired_Date = c.invoice_expired,
                                    Payment_Type = c.payment_type,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Chus_Name = det.customer_name,
                                    Currency_Code = c.currency_code,
                                    Currency_Name = cur.currency_name,
                                    Remarks = c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    Total = (decimal)c.total_amount,
                                    Total_Vt = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    //Vat_Amount= (long)c.vat_amount,
                                    warrenty = c.warrenty,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    grand_count = (int)c.grand_count,
                                    daily_count = (int)c.daily_count,
                                    approval_status = c.approval_status,

                                    approval_date = approval_date

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> GetINVOICEMas_Pen(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join cmp in context.company_master on c.comp_mas_sno equals cmp.comp_mas_sno
                                join cur in context.currency_master on c.currency_code equals cur.currency_code
                                //join d in context.payment_details on c.comp_mas_sno equals d.comp_mas_sno
                                join e in context.payment_details on c.control_no equals e.control_no into z
                                where c.comp_mas_sno == cno && z.Count() == 0 && c.approval_status == "2"
                                select new INVOICE
                                {
                                    Com_Mas_Sno = c.company_master.comp_mas_sno,
                                    Company_Name = cmp.company_name,
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    Invoice_Date = c.invoice_date,
                                    Due_Date = c.due_date,
                                    Invoice_Expired_Date = c.invoice_expired,
                                    Payment_Type = c.payment_type,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Chus_Name = det.customer_name,
                                    Currency_Code = c.currency_code,
                                    Currency_Name = cur.currency_name,
                                    Remarks = c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    Total = (decimal)c.total_amount,
                                    Total_Vt = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    //Vat_Amount= (long)c.vat_amount,
                                    warrenty = c.warrenty,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    grand_count = (int)c.grand_count,
                                    daily_count = (int)c.daily_count,
                                    approval_status = c.approval_status,

                                    approval_date = approval_date

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> GetINVOICEMas_Lat(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join cmp in context.company_master on c.comp_mas_sno equals cmp.comp_mas_sno
                                join cur in context.currency_master on c.currency_code equals cur.currency_code
                                where c.comp_mas_sno == cno  && c.approval_status == "2"
                                select new INVOICE
                                {
                                    Com_Mas_Sno = c.company_master.comp_mas_sno,
                                    Company_Name = cmp.company_name,
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    Invoice_Date = c.invoice_date,
                                    Due_Date = c.due_date,
                                    Invoice_Expired_Date = c.invoice_expired,
                                    Payment_Type = c.payment_type,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Chus_Name = det.customer_name,
                                    Currency_Code = c.currency_code,
                                    Currency_Name = cur.currency_name,
                                    Remarks = c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    Total = (decimal)c.total_amount,
                                    Total_Vt = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    //Vat_Amount= (long)c.vat_amount,
                                    warrenty = c.warrenty,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    grand_count = (int)c.grand_count,
                                    daily_count = (int)c.daily_count,
                                    approval_status = c.approval_status,
                                    Control_No = c.control_no,
                                    approval_date = approval_date

                                }).OrderByDescending(a => a.Com_Mas_Sno).Take(5).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public List<INVOICE> GetINVOICEMasterByVendor(long company_sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join cmp in context.company_master on c.comp_mas_sno equals cmp.comp_mas_sno
                                join cur in context.currency_master on c.currency_code equals cur.currency_code
                                where c.comp_mas_sno == company_sno
                                select new INVOICE
                                {
                                    Com_Mas_Sno = c.company_master.comp_mas_sno,
                                    Company_Name = cmp.company_name,
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    Invoice_Date = c.invoice_date,
                                    Due_Date = c.due_date,
                                    Invoice_Expired_Date = c.invoice_expired,
                                    Payment_Type = c.payment_type,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Chus_Name = det.customer_name,
                                    Currency_Code = c.currency_code,
                                    Currency_Name = cur.currency_name,
                                    Remarks = c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    Total = (decimal)c.total_amount,
                                    Total_Vt = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    //Vat_Amount= (long)c.vat_amount,
                                    warrenty = c.warrenty,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    grand_count = (int)c.grand_count,
                                    daily_count = (int)c.daily_count,
                                    approval_status = c.approval_status,

                                    approval_date = approval_date

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public List<INVOICE> GetINVOICEMasterByBranch(long branch)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join cmp in context.company_master on c.comp_mas_sno equals cmp.comp_mas_sno
                                join cur in context.currency_master on c.currency_code equals cur.currency_code
                                where cmp.branch_sno == branch
                                select new INVOICE
                                {
                                    Com_Mas_Sno = c.company_master.comp_mas_sno,
                                    Company_Name = cmp.company_name,
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    Invoice_Date = c.invoice_date,
                                    Due_Date = c.due_date,
                                    Invoice_Expired_Date = c.invoice_expired,
                                    Payment_Type = c.payment_type,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Chus_Name = det.customer_name,
                                    Currency_Code = c.currency_code,
                                    Currency_Name = cur.currency_name,
                                    Remarks = c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    Total = (decimal)c.total_amount,
                                    Total_Vt = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    //Vat_Amount= (long)c.vat_amount,
                                    warrenty = c.warrenty,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    grand_count = (int)c.grand_count,
                                    daily_count = (int)c.daily_count,
                                    approval_status = c.approval_status,

                                    approval_date = approval_date

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> GetINVOICEMas1()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join cmp in context.company_master on c.comp_mas_sno equals cmp.comp_mas_sno
                                join cur in context.currency_master on c.currency_code equals cur.currency_code
                                //where c.comp_mas_sno == cno
                                select new INVOICE
                                {
                                    Com_Mas_Sno = c.company_master.comp_mas_sno,
                                    Company_Name = cmp.company_name,
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    Invoice_Date = c.invoice_date,
                                    Due_Date = c.due_date,
                                    Invoice_Expired_Date = c.invoice_expired,
                                    Payment_Type = c.payment_type,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Chus_Name = det.customer_name,
                                    Currency_Code = c.currency_code,
                                    Currency_Name = cur.currency_name,
                                    Remarks = c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    Total = (decimal)c.total_amount,
                                    Total_Vt = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    //Vat_Amount= (long)c.vat_amount,
                                    warrenty = c.warrenty,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    grand_count = (int)c.grand_count,
                                    daily_count = (int)c.daily_count,
                                    approval_status = c.approval_status,

                                    approval_date = approval_date

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> GetINVOICEMasE(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join cmp in context.company_master on c.comp_mas_sno equals cmp.comp_mas_sno
                                join cur in context.currency_master on c.currency_code equals cur.currency_code
                                where c.invoice_expired != null && c.invoice_expired <= DateTime.Now && c.comp_mas_sno == cno
                                select new INVOICE
                                {
                                    Com_Mas_Sno = c.company_master.comp_mas_sno,
                                    Company_Name = cmp.company_name,
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    Invoice_Date = c.invoice_date,
                                    Due_Date = c.due_date,
                                    Invoice_Expired_Date = c.invoice_expired,
                                    Payment_Type = c.payment_type,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Chus_Name = det.customer_name,
                                    Currency_Code = c.currency_code,
                                    Currency_Name = cur.currency_name,
                                    Remarks = c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    Total = (decimal)c.total_amount,
                                    Total_Vt = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    //Vat_Amount= (long)c.vat_amount,
                                    warrenty = c.warrenty,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    grand_count = (int)c.grand_count,
                                    daily_count = (int)c.daily_count,
                                    approval_status = c.approval_status,

                                    approval_date = approval_date

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public InvoicePDfData GetINVOICEpdf(long invoicenumber)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join dets in context.company_master on c.comp_mas_sno equals dets.comp_mas_sno
                                join bks in context.company_bank_details on dets.comp_mas_sno equals bks.comp_mas_sno
                                where c.inv_mas_sno == invoicenumber
                                select new InvoicePDfData
                                {
                                        CompName= c.company_master.company_name,
                                        CompPostBox= c.company_master.pobox_no,
                                        CompAddress= c.company_master.physical_address,
                                        CompTelNo = c.company_master.telephone_no,
                                        CompFaxNo = c.company_master.fax_no,
                                        Control_No = c.control_no,
                                        Due_Date = c.due_date,
                                        Invoice_Expired_Date = c.invoice_expired,
                                        CompMobNo = c.company_master.mobile_no,
                                        CompEmail = c.company_master.email_address,
                                        CompVatNo = c.company_master.vat_no,
                                        TinNo = c.company_master.tin_no,
                                        Posteddate = (DateTime)c.posted_date,
                                        Cust_Sno = (long)c.cust_mas_sno,
                                        CompanySno = (long)c.comp_mas_sno,
                                        Invoice_No = c.invoice_no,
                                        BankName= bks.bank_name,
                                        AccountNo=bks.account_no,
                                        Cust_Name=det.customer_name,
                                    CompContactPerson= dets.director_name,
                                       CustPhone = det.mobile_no,
                                       ConPerson=det.contact_person,
                                        Item_Total_Amount = (decimal)c.total_amount,
                                        Vat_Amount= (decimal)c.vat_amount,
                                        Total_Without_Vt = (decimal)c.total_without_vat,
                                        Currency_Code=c.currency_code,
                                        CustAddress=det.physical_address,
                                        CustomerPostboxNo=det.pobox_no,
                                          warrenty = c.warrenty,
                                         goods_status = c.goods_status,
                                        delivery_status = c.delivery_status,
                                        Invoice_Date=c.invoice_date,
                                        Remarks=c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    grand_count = (int)c.grand_count,
                                    daily_count = (int)c.daily_count,
                                    approval_status = c.approval_status==null?"0": c.approval_status.ToString(),
                                    Inv_Mas_Sno=c.inv_mas_sno,
                                    approval_date = approval_date
                                    

                                }).FirstOrDefault();
                


                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }

        public InvoicePDfData GetControl(string cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join dets in context.company_master on c.comp_mas_sno equals dets.comp_mas_sno
                                //join bks in context.company_bank_details on dets.comp_mas_sno equals bks.comp_mas_sno
                                where c.control_no == cno
                                select new InvoicePDfData
                                {
                                    Payment_Type = c.payment_type,
                                    CompName = c.company_master.company_name,
                                    CompPostBox = c.company_master.pobox_no,
                                    CompAddress = c.company_master.physical_address,
                                    CompTelNo = c.company_master.telephone_no,
                                    CompFaxNo = c.company_master.fax_no,
                                    Control_No = c.control_no,
                                    CompMobNo = c.company_master.mobile_no,
                                    CompEmail = c.company_master.email_address,
                                    CompVatNo = c.company_master.vat_no,
                                    TinNo = c.company_master.tin_no,
                                    Posteddate = (DateTime)c.posted_date,
                                    Cust_Sno = (long)c.cust_mas_sno,
                                    CompanySno = (long)c.comp_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    //BankName = bks.bank_name,
                                    //AccountNo = bks.account_no,
                                    Cust_Name = det.customer_name,
                                    CustEmail = det.email_address,
                                    CompContactPerson = dets.director_name,
                                    CustPhone = det.mobile_no,
                                    ConPerson = det.contact_person,
                                    Item_Total_Amount = (decimal)c.total_amount,
                                    Vat_Amount = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    Currency_Code = c.currency_code,
                                    CustAddress = det.physical_address,
                                    CustomerPostboxNo = det.pobox_no,
                                    warrenty = c.warrenty,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    Invoice_Date = c.invoice_date,
                                    Remarks = c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    grand_count = (int)c.grand_count,
                                    daily_count = (int)c.daily_count,
                                    approval_status = c.approval_status == null ? "0" : c.approval_status.ToString(),
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    approval_date = approval_date

                                }).FirstOrDefault();



                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }
        public InvoicePDfData GetControl_A(string cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join dets in context.company_master on c.comp_mas_sno equals dets.comp_mas_sno
                                //join bks in context.company_bank_details on dets.comp_mas_sno equals bks.comp_mas_sno
                                where c.control_no == cno && c.approval_status == "2"
                                select new InvoicePDfData
                                {
                                    Payment_Type = c.payment_type,
                                    CompName = c.company_master.company_name,
                                    CompPostBox = c.company_master.pobox_no,
                                    CompAddress = c.company_master.physical_address,
                                    CompTelNo = c.company_master.telephone_no,
                                    CompFaxNo = c.company_master.fax_no,
                                    Control_No = c.control_no,
                                    CompMobNo = c.company_master.mobile_no,
                                    CompEmail = c.company_master.email_address,
                                    CompVatNo = c.company_master.vat_no,
                                    TinNo = c.company_master.tin_no,
                                    Posteddate = (DateTime)c.posted_date,
                                    Cust_Sno = (long)c.cust_mas_sno,
                                    CompanySno = (long)c.comp_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    //BankName = bks.bank_name,
                                    //AccountNo = bks.account_no,
                                    Cust_Name = det.customer_name,
                                    CustEmail = det.email_address,
                                    CompContactPerson = dets.director_name,
                                    CustPhone = det.mobile_no,
                                    ConPerson = det.contact_person,
                                    Item_Total_Amount = (decimal)c.total_amount,
                                    Currency_Code = c.currency_code,
                                    CustAddress = det.physical_address,
                                    CustomerPostboxNo = det.pobox_no,
                                    warrenty = c.warrenty,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    Invoice_Date = c.invoice_date,
                                    Remarks = c.inv_remarks,
                                    Inv_Mas_Sno = c.inv_mas_sno
                           
                                }).FirstOrDefault();



                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }
        public List<InvoicePDfData> GetControl_D(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                join dets in context.company_master on c.comp_mas_sno equals dets.comp_mas_sno
                                //join bks in context.company_bank_details on dets.comp_mas_sno equals bks.comp_mas_sno
                                where c.comp_mas_sno == cno //&& c.approval_status == "2"
                                select new InvoicePDfData
                                {
                                    
                                    Payment_Type = c.payment_type,
                                    CompName = c.company_master.company_name,
                                    CompPostBox = c.company_master.pobox_no,
                                    CompAddress = c.company_master.physical_address,
                                    CompTelNo = c.company_master.telephone_no,
                                    CompFaxNo = c.company_master.fax_no,
                                    Control_No = c.control_no,
                                    CompMobNo = c.company_master.mobile_no,
                                    CompEmail = c.company_master.email_address,
                                    CompVatNo = c.company_master.vat_no,
                                    TinNo = c.company_master.tin_no,
                                    Posteddate = (DateTime)c.posted_date,
                                    Cust_Sno = (long)c.cust_mas_sno,
                                    CompanySno = (long)c.comp_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    //BankName = bks.bank_name,
                                    //AccountNo = bks.account_no,
                                    Cust_Name = det.customer_name,
                                    CustEmail = det.email_address,
                                    CompContactPerson = dets.director_name,
                                    CustPhone = det.mobile_no,
                                    ConPerson = det.contact_person,
                                    Item_Total_Amount = (decimal)c.total_amount,
                                    Currency_Code = c.currency_code,
                                    CustAddress = det.physical_address,
                                    CustomerPostboxNo = det.pobox_no,
                                    warrenty = c.warrenty,
                                    goods_status = c.goods_status,
                                    delivery_status = c.delivery_status,
                                    Invoice_Date = c.invoice_date,
                                    Remarks = c.inv_remarks,
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    approval_status = c.approval_status

                                }).OrderByDescending(a => a.Inv_Mas_Sno).ToList();



                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<InvoicePDfData> GetTotal(DateTime dati)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                //DateTime add = dati.AddDays(1);
                //string dt = DateTime.Now.AddDays(-1).ToShortDateString();
                //string de = dati.ToString("MM/dd/yyyy");
                //DateTime dt1 = DateTime.Parse(de);
                var adetails = (from c in context.invoice_master
                                //where (c.approval_status == "2") && (c.posted_date >= dt1 && c.posted_date <= add )
                                where (c.approval_status == "2") && (c.p_date == dati)
                                select new InvoicePDfData
                                {
                                    CompVatNo = c.company_master.vat_no,
                                    TinNo = c.company_master.tin_no,
                                    Invoice_No = c.invoice_no,
                                    Item_Total_Amount = (decimal)c.total_amount,
                                    Vat_Amount = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    
                                    Inv_Mas_Sno = c.inv_mas_sno,
                       
                                }).ToList();



                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> GetVATTotal(string type, DateTime dati)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                //DateTime add = dati.AddDays(1);
                //string dt = DateTime.Now.AddDays(-1).ToShortDateString();
                //string de = dati.ToString("MM/dd/yyyy");
                //DateTime dt1 = DateTime.Parse(de);
                var adetails = (from d in context.invoice_details
                                join c in context.invoice_master on d.inv_mas_sno equals c.inv_mas_sno
                                //where ((c.approval_status == "2") && (c.posted_date >= dt1 && c.posted_date <= add) && (d.vat_type == type))
                                where ((c.approval_status == "2") && (c.p_date == dati) && (d.vat_type == type))
                                select new INVOICE
                                {
                                    Vat_Type = d.vat_type,
                                    Vat_Percentage = (decimal)d.vat_percentage,
                                    Item_Total_Amount = (decimal)d.item_total_amount,
                                    Vat_Amount = (decimal)d.vat_amount,
                                    Item_Without_vat = (decimal)d.item_without_vat,
                                    Inv_Mas_Sno = (long)d.inv_mas_sno,

                                }).ToList();



                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> GetVATTotal1(long sno,string type)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from d in context.invoice_details
                                join c in context.invoice_master on d.inv_mas_sno equals c.inv_mas_sno
                                where c.inv_mas_sno == sno && d.vat_type == type
                                select new INVOICE
                                {
                                    Vat_Type = d.vat_type,
                                    Vat_Percentage = (decimal)d.vat_percentage,
                                    Item_Total_Amount = (decimal)d.item_total_amount,
                                    Vat_Amount = (decimal)d.vat_amount,
                                    Item_Without_vat = (decimal)d.item_without_vat,
                                    Inv_Mas_Sno = (long)d.inv_mas_sno,

                                }).ToList();



                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<InvoicePDfData> GetGTotal()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.approval_status == "2" 
                                select new InvoicePDfData
                                {
                                    CompVatNo = c.company_master.vat_no,
                                    TinNo = c.company_master.tin_no,
                                    Invoice_No = c.invoice_no,
                                    Item_Total_Amount = (decimal)c.total_amount,
                                    Vat_Amount = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,

                                    Inv_Mas_Sno = c.inv_mas_sno,

                                }).ToList();



                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public void UpdateInvoiMasForTRA1(INVOICE dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.invoice_master
                                         where u.inv_mas_sno == dep.Inv_Mas_Sno
                                         select u).FirstOrDefault();

                
                if (UpdateContactInfo != null)
                {

                    UpdateContactInfo.goods_status = dep.goods_status ;
                    UpdateContactInfo.grand_count = dep.grand_count;
                    //UpdateContactInfo.daily_count = dep.daily_count;
                    UpdateContactInfo.control_no = dep.Control_No;
                    context.SaveChanges();
                }
            }
        }
        public void UpdateInvoiMasForTRA(INVOICE dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.invoice_master
                                         where u.inv_mas_sno == dep.Inv_Mas_Sno
                                         select u).FirstOrDefault();

                var grand_max = (from c in context.invoice_master
                                 
                                select c).Max(c => c.grand_count);
                if(grand_max == null)
                {
                    grand_max = 0;
                }
                int dailycount = 0;

                var dcount = context.invoice_master.ToList().Where(x => Convert.ToDateTime(x.p_date) == Convert.ToDateTime(System.DateTime.Now.Date));
                if (dcount.Count() > 0)
                {
                    var item = dcount.Max(x => x.daily_count);
                    dailycount = (int)item;
                }
                 

                //var daily_count = (from c in context.invoice_master
                //                   where Convert.ToDateTime( c.approval_date).Date == Convert.ToDateTime(System.DateTime.Now.Date)
                //                   select c).Max(c => c.daily_count);

                if (UpdateContactInfo != null)
                {
                     
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    //UpdateContactInfo.posted_date = DateTime.Now;
                    UpdateContactInfo.grand_count = grand_max + 1;
                    UpdateContactInfo.daily_count = dailycount+1;
                    //UpdateContactInfo.approval_status = dep.approval_status;
                    //UpdateContactInfo.approval_date = dep.approval_date;
                    context.SaveChanges();
                }
            }
        }

        public void UpdateInvoiceStatusDeliveryCode(INVOICE dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.invoice_master
                                         where u.inv_mas_sno == dep.Inv_Mas_Sno
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.posted_date = DateTime.Now;
                    UpdateContactInfo.delivery_status = dep.delivery_status;
                    context.SaveChanges();
                }
            }
        }


        public void UpdateInvoiceDeliveryCode(INVOICE dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.invoice_master
                                         where u.inv_mas_sno == dep.Inv_Mas_Sno
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    UpdateContactInfo.delivery_status = dep.delivery_status;
                    UpdateContactInfo.grand_count = dep.grand_count;
                    context.SaveChanges();
                }
            }
        }



        public void UpdateInvoice(INVOICE dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.invoice_master
                                         where u.inv_mas_sno == dep.Inv_Mas_Sno
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    UpdateContactInfo.approval_status = dep.approval_status;
                    UpdateContactInfo.approval_date = dep.approval_date;
                    UpdateContactInfo.control_no = dep.Control_No;
                    UpdateContactInfo.goods_status = dep.goods_status;
                    context.SaveChanges();
                }
            }
        }

        public void UpdateInvoiceDate(INVOICE dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.invoice_master
                                         where u.inv_mas_sno == dep.Inv_Mas_Sno
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        public INVOICE EditINVOICEMas(long Sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master where c.inv_mas_sno == Sno
                                select new INVOICE
                                {

                                    Invoice_No = c.invoice_no,
                                    Invoice_Date = c.invoice_date,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Com_Mas_Sno = (long)c.comp_mas_sno,
                                    Currency_Code = c.currency_code,
                                    Total = (decimal)c.total_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    Vat_Amount = (decimal)c.vat_amount,
                                    Inv_Remarks = c.inv_remarks
                                }).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }
        public INVOICE getDGCount(long Sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                where c.inv_mas_sno == Sno && c.daily_count != null && c.grand_count != null
                                select new INVOICE
                                {
                                    daily_count = c.daily_count,
                                    grand_count = (int)c.grand_count
                                }).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> GetInvoiceDetails(long Sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_details where c.inv_mas_sno == Sno //context.invoice_details.Where(c => c.inv_mas_sno == Sno)
                                select new INVOICE
                                {
                                    Inv_Mas_Sno = (long)c.inv_mas_sno,
                                    Item_Description = c.item_description,
                                    Item_Qty = (decimal)c.item_qty,
                                    Item_Unit_Price = (decimal)c.item_unit_price,
                                    Item_Total_Amount = (decimal)c.item_total_amount,
                                    Vat_Percentage = (decimal)c.vat_percentage,
                                    Vat_Amount = (decimal)c.vat_amount,
                                    Item_Without_vat = (decimal)c.item_without_vat,
                                    Remarks = c.remarks,
                                    vat_category = c.vat_category,
                                    Vat_Type = c.vat_type
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> GetCustomers(long Sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                where c.approval_status == "2" && c.comp_mas_sno==Sno
                                select new INVOICE
                                {
                                    Chus_Mas_No = (long)det.cust_mas_sno,
                                    Chus_Name = det.customer_name
    
                                }).Distinct().ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> GetCustomers111(long Sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                where/* c.approval_status != "2" &&*/ c.comp_mas_sno==Sno
                                select new INVOICE
                                {
                                    Chus_Mas_No = (long)det.cust_mas_sno,
                                    Chus_Name = det.customer_name
    
                                }).Distinct().ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> GetCustomers11(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                where c.approval_status != "2"&& c.comp_mas_sno==sno
                                select new INVOICE
                                {
                                    Chus_Mas_No = (long)det.cust_mas_sno,
                                    Chus_Name = det.customer_name
    
                                }).Distinct().ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> SelectActiveInvoicesByCustomer(long customerId)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var invoices = (from c in context.invoice_master join cust in context.customer_master on c.cust_mas_sno equals cust.cust_mas_sno
                                where cust.cust_mas_sno == customerId
                                select new INVOICE
                                {
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Chus_Name = cust.customer_name

                                }).ToList();
                return invoices != null || invoices.Count() > 0 ? invoices : new List<INVOICE>();
            }
        }
        public List<INVOICE> GetCustomers1(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                where c.approval_status == "2" && det.comp_mas_sno== sno
                                select new INVOICE
                                {
                                    Chus_Mas_No = (long)det.cust_mas_sno,
                                    Chus_Name = det.customer_name
    
                                }).Distinct().ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> GetInvoiceNos(long Sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                where c.approval_status == "2" && c.cust_mas_sno==Sno
                                select new INVOICE
                                {
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no

                                }).Distinct().ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> GetInvoiceNos_N(long Sno, long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                /*where (c.approval_status == "2") &&
                                 (Sno == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == Sno)//c.cust_mas_sno == Sno
                                 && (c.comp_mas_sno == cno)*/
                                select new INVOICE
                                {
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no

                                }).Distinct().ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }


        public INVOICE GetInvoiceCDetails(long invoice_sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join cus in context.customer_master on c.cust_mas_sno equals cus.cust_mas_sno
                                where c.inv_mas_sno == invoice_sno //&& c.delivery_status != "Delivered"
                                select new INVOICE
                                {
                                    Chus_Name = cus.customer_name,
                                    Control_No = c.control_no,
                                    goods_status = c.goods_status,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Email = cus.email_address,
                                    Inv_Mas_Sno = (long)c.inv_mas_sno,
                                    Total = (decimal)c.total_amount,
                                    Mobile = cus.mobile_no,
                                    AuditBy = c.posted_by,
                                    Audit_Date = (DateTime)c.posted_date
                                }).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }

        


        public INVOICE GetInvoiceCodeDetails(long code)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join cus in context.customer_master on c.cust_mas_sno equals cus.cust_mas_sno
                                where c.grand_count == code && c.delivery_status == "Pending"
                                select new INVOICE
                                {
                                    Chus_Name = cus.customer_name,
                                    Control_No = c.control_no,
                                    grand_count = (int)c.grand_count,
                                    delivery_status = c.delivery_status,
                                    approval_status = c.approval_status,
                                    goods_status = c.goods_status,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Inv_Mas_Sno = (long)c.inv_mas_sno,
                                    Total = (decimal)c.total_amount,
                                    Mobile = cus.mobile_no,
                                    AuditBy = c.posted_by,
                                    Audit_Date = (DateTime)c.posted_date
                                }).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }



        public List<INVOICE> GetInvoiceNos_(long Sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                where (c.approval_status == "2") &&
                                 (Sno == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == Sno)//c.cust_mas_sno == Sno
                                 /**//*&& (c.comp_mas_sno == cno)*/
                                select new INVOICE
                                {
                                    Inv_Mas_Sno = c.inv_mas_sno,
                                    Invoice_No = c.invoice_no

                                }).Distinct().ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<INVOICE> GetInvRep(long Comp, long cust, string stdate, string enddate)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (stdate == "" && enddate == "")
                {
                    List<INVOICE> listinvoice = (from c in context.invoice_master
                                                 join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                                 where (c.approval_status == "2")
                                                 && (cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                                 && (Comp == 0 ? c.comp_mas_sno == c.comp_mas_sno : c.comp_mas_sno == Comp)
                                                 select new INVOICE
                                                 {
                                                     Invoice_No = c.invoice_no,
                                                     Invoice_Date = c.invoice_date,
                                                     Chus_Mas_No = det.cust_mas_sno,
                                                     Chus_Name = det.customer_name,
                                                     Customer_ID_Type = c.customer_id_type,
                                                     Customer_ID_No = c.customer_id_no,
                                                     Total = (decimal)c.total_amount,
                                                     Total_Vt = (decimal)c.vat_amount,
                                                     Total_Without_Vt = (decimal)c.total_without_vat,
                                                     //Vat_Amount= (long)c.vat_amount,
                                                     warrenty = c.warrenty,
                                                     goods_status = c.goods_status,
                                                     delivery_status = c.delivery_status,
                                                     approval_date = approval_date

                                                 }).ToList();
                    return listinvoice;
                }
                else
                {
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    DateTime fdate = DateTime.Parse(stdate);
                    //DateTime tdate = DateTime.Parse(edate).AddHours(23).AddMinutes(59).AddSeconds(59);
                    DateTime tdate = DateTime.Parse(enddate);
                    List<INVOICE> listinvoice = (from c in context.invoice_master
                                                 join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                                 where (c.approval_status == "2") && (c.invoice_date >= fdate && c.invoice_date <= tdate) &&
                                                 (cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                                 && (Comp == 0 ? c.comp_mas_sno == c.comp_mas_sno : c.comp_mas_sno == Comp)
                                                 select new INVOICE
                                                 {
                                                     Invoice_No = c.invoice_no,
                                                     Invoice_Date = c.invoice_date,
                                                     Chus_Mas_No = det.cust_mas_sno,
                                                     Chus_Name = det.customer_name,
                                                     Customer_ID_Type = c.customer_id_type,
                                                     Customer_ID_No = c.customer_id_no,
                                                     Total = (decimal)c.total_amount,
                                                     Total_Vt = (decimal)c.vat_amount,
                                                     Total_Without_Vt = (decimal)c.total_without_vat,
                                                     //Vat_Amount= (long)c.vat_amount,
                                                     warrenty = c.warrenty,
                                                     goods_status = c.goods_status,
                                                     delivery_status = c.delivery_status,
                                                     approval_date = approval_date

                                                 }).ToList();
                    return listinvoice;
                }
            }
        }
        public List<INVOICE> GetInvRep(List<long> companyIds,List<long> customerIds, string stdate, string enddate,bool allowCancelInvoice)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                DateTime? fromDate = null;
                if (!string.IsNullOrEmpty(stdate)) fromDate = DateTime.Parse(stdate);
                DateTime? toDate = null;
                if (!string.IsNullOrEmpty(enddate)) toDate = DateTime.Parse(enddate);

                List<INVOICE> invoices = (from c in context.invoice_master
                                          join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                          join c1 in context.company_master on c.comp_mas_sno equals c1.comp_mas_sno
                                          where (allowCancelInvoice ? allowCancelInvoice : c.approval_status == "2")
                                          && ((companyIds.Contains(0)) || (companyIds.Contains((long)c.comp_mas_sno)))
                                          && ((customerIds.Contains(0)) || (customerIds.Contains((long)c.cust_mas_sno)))
                                          && (!fromDate.HasValue || fromDate <= c.posted_date)
                                          && (!toDate.HasValue || toDate >= c.posted_date)
                                          select new INVOICE
                                          {
                                              Inv_Mas_Sno = c.inv_mas_sno,
                                              Invoice_No = c.invoice_no,
                                              Chus_Mas_No = det.cust_mas_sno,
                                              Chus_Name = det.customer_name,
                                              Company_Name = c1.company_name,
                                              Currency_Code = c.currency_code,
                                              Payment_Type = c.payment_type,
                                              Customer_ID_Type = c.customer_id_type,
                                              Customer_ID_No = c.customer_id_no,
                                              Total = (decimal)c.total_amount,
                                              Total_Vt = (decimal)c.vat_amount,
                                              Total_Without_Vt = (decimal)c.total_without_vat,
                                              Control_No = c.control_no,
                                              p_date = (DateTime) c.posted_date,
                                              Invoice_Expired_Date = (DateTime) c.invoice_expired,
                                              Due_Date = (DateTime) c.due_date,
                                              Invoice_Date = (DateTime) c.invoice_date,
                                              warrenty = c.warrenty,
                                              goods_status = c.goods_status,
                                              delivery_status = c.delivery_status,
                                              approval_date = approval_date
                                          }).ToList();
                return invoices != null ? invoices : new List<INVOICE>();
            }
        }
        public List<INVOICE> GetInvRep1(long Comp,long cust, string stdate, string enddate)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (stdate == "" && enddate == "")
                {
                    List<INVOICE> listinvoice = (from c in context.invoice_master
                                                 join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                                 where (c.approval_status == "2") 
                                                 && (cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                                 && (Comp == 0 ? c.comp_mas_sno == c.comp_mas_sno : c.comp_mas_sno == Comp)
                                                 select new INVOICE
                                                 {
                                                     Invoice_No = c.invoice_no,
                                                     Invoice_Date = c.invoice_date,
                                                     Chus_Mas_No = det.cust_mas_sno,
                                                     Chus_Name = det.customer_name,
                                                     Customer_ID_Type = c.customer_id_type,
                                                     Customer_ID_No = c.customer_id_no,
                                                     Total = (decimal)c.total_amount,
                                                     Total_Vt = (decimal)c.vat_amount,
                                                     Total_Without_Vt = (decimal)c.total_without_vat,
                                                     //Vat_Amount= (long)c.vat_amount,
                                                     warrenty = c.warrenty,
                                                     goods_status = c.goods_status,
                                                     delivery_status = c.delivery_status,
                                                     approval_date = (DateTime)approval_date,
                                                     p_date = (DateTime)c.p_date,
                                                     Control_No = c.control_no,
                                                     Currency_Code = c.currency_code,
                                                     Payment_Type = c.payment_type,
                                                     Status = c.goods_status,
                                                     Company_Name = c.company_master.company_name,
                                                     Due_Date = c.due_date,
                                                     Invoice_Expired_Date = c.invoice_expired


                                                 }).ToList();
                    return listinvoice;
                }
                else
                {
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    DateTime fdate = DateTime.Parse(stdate);
                    //DateTime tdate = DateTime.Parse(edate).AddHours(23).AddMinutes(59).AddSeconds(59);
                    DateTime tdate = DateTime.Parse(enddate);
                    List<INVOICE> listinvoice = (from c in context.invoice_master
                                                 join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                                 where (c.approval_status == "2") && (c.invoice_date >= fdate && c.invoice_date <= tdate) &&
                                                 (cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                                 && (Comp == 0 ? c.comp_mas_sno == c.comp_mas_sno : c.comp_mas_sno == Comp)
                                                 select new INVOICE
                                                 {
                                                     Invoice_No = c.invoice_no,
                                                     Invoice_Date = c.invoice_date,
                                                     Chus_Mas_No = det.cust_mas_sno,
                                                     Chus_Name = det.customer_name,
                                                     Customer_ID_Type = c.customer_id_type,
                                                     Customer_ID_No = c.customer_id_no,
                                                     Total = (decimal)c.total_amount,
                                                     Total_Vt = (decimal)c.vat_amount,
                                                     Total_Without_Vt = (decimal)c.total_without_vat,
                                                     //Vat_Amount= (long)c.vat_amount,
                                                     warrenty = c.warrenty,
                                                     goods_status = c.goods_status,
                                                     delivery_status = c.delivery_status,
                                                     approval_date = approval_date,
                                                     p_date = (DateTime)c.p_date,
                                                     Control_No = c.control_no,
                                                     Currency_Code = c.currency_code,
                                                     Payment_Type = c.payment_type,
                                                     Status = c.goods_status,
                                                     Company_Name = c.company_master.company_name,
                                                     Due_Date = c.due_date,
                                                     Invoice_Expired_Date = c.invoice_expired

                                                 }).ToList();
                    return listinvoice;
                }
                
            }
        }public List<INVOICE> GetInvRep11(long Comp,long cust, string stdate, string enddate)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (stdate == "" && enddate == "")
                {
                    List<INVOICE> listinvoice = (from c in context.invoice_master
                                                 join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                                 where (c.approval_status != "2") 
                                                 && (cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                                 && (Comp == 0 ? c.comp_mas_sno == c.comp_mas_sno : c.comp_mas_sno == Comp)
                                                 select new INVOICE
                                                 {
                                                     Invoice_No = c.invoice_no,
                                                     Invoice_Date = c.invoice_date,
                                                     Chus_Mas_No = det.cust_mas_sno,
                                                     Chus_Name = det.customer_name,
                                                     Customer_ID_Type = c.customer_id_type,
                                                     Customer_ID_No = c.customer_id_no,
                                                     Total = (decimal)c.total_amount,
                                                     Total_Vt = (decimal)c.vat_amount,
                                                     Total_Without_Vt = (decimal)c.total_without_vat,
                                                     //Vat_Amount= (long)c.vat_amount,
                                                     warrenty = c.warrenty,
                                                     goods_status = c.goods_status,
                                                     delivery_status = c.delivery_status,
                                                     approval_date = approval_date,
                                                     p_date = (DateTime)c.p_date,
                                                     Control_No = c.control_no,
                                                     Currency_Code = c.currency_code,
                                                     Payment_Type = c.payment_type,
                                                     Status = c.goods_status,
                                                     Company_Name = c.company_master.company_name,
                                                     Due_Date = c.due_date,
                                                     Invoice_Expired_Date = c.invoice_expired


                                                 }).ToList();
                    return listinvoice;
                }
                else
                {
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    DateTime fdate = DateTime.Parse(stdate);
                    //DateTime tdate = DateTime.Parse(edate).AddHours(23).AddMinutes(59).AddSeconds(59);
                    DateTime tdate = DateTime.Parse(enddate);
                    List<INVOICE> listinvoice = (from c in context.invoice_master
                                                 join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                                 where (c.approval_status != "2") && (c.invoice_date >= fdate && c.invoice_date <= tdate) &&
                                                 (cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                                 && (Comp == 0 ? c.comp_mas_sno == c.comp_mas_sno : c.comp_mas_sno == Comp)
                                                 select new INVOICE
                                                 {
                                                     Invoice_No = c.invoice_no,
                                                     Invoice_Date = c.invoice_date,
                                                     Chus_Mas_No = det.cust_mas_sno,
                                                     Chus_Name = det.customer_name,
                                                     Customer_ID_Type = c.customer_id_type,
                                                     Customer_ID_No = c.customer_id_no,
                                                     Total = (decimal)c.total_amount,
                                                     Total_Vt = (decimal)c.vat_amount,
                                                     Total_Without_Vt = (decimal)c.total_without_vat,
                                                     //Vat_Amount= (long)c.vat_amount,
                                                     warrenty = c.warrenty,
                                                     goods_status = c.goods_status,
                                                     delivery_status = c.delivery_status,
                                                     approval_date = approval_date,
                                                     p_date = (DateTime)c.p_date,
                                                     Control_No = c.control_no,
                                                     Currency_Code = c.currency_code,
                                                     Payment_Type = c.payment_type,
                                                     Status = c.goods_status,
                                                     Company_Name = c.company_master.company_name,
                                                     Due_Date = c.due_date,
                                                     Invoice_Expired_Date = c.invoice_expired


                                                 }).ToList();
                    return listinvoice;
                }
                
            }
        }
        public List<INVOICE> GetInvRep1(long cust, string stdate, string enddate)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (stdate == "" && enddate == "")
                {
                    List<INVOICE> listinvoice = (from c in context.invoice_master
                                                 join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                                 where (c.approval_status != "2") && (cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                                 select new INVOICE
                                                 {
                                                     Invoice_No = c.invoice_no,
                                                     Invoice_Date = c.invoice_date,
                                                     Chus_Mas_No = det.cust_mas_sno,
                                                     Chus_Name = det.customer_name,
                                                     Customer_ID_Type = c.customer_id_type,
                                                     Customer_ID_No = c.customer_id_no,
                                                     Total = (decimal)c.total_amount,
                                                     Total_Vt = (decimal)c.vat_amount,
                                                     Total_Without_Vt = (decimal)c.total_without_vat,
                                                     //Vat_Amount= (long)c.vat_amount,
                                                     warrenty = c.warrenty,
                                                     goods_status = c.goods_status,
                                                     delivery_status = c.delivery_status,
                                                     approval_date = approval_date,
                                                     p_date = (DateTime)c.p_date,
                                                     Control_No = c.control_no,
                                                     Currency_Code = c.currency_code,
                                                     Payment_Type = c.payment_type,
                                                     Status = c.goods_status,
                                                     Company_Name = c.company_master.company_name,
                                                     Due_Date = c.due_date,
                                                     Invoice_Expired_Date = c.invoice_expired


                                                 }).ToList();
                    return listinvoice;
                }
                else
                {
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    DateTime fdate = DateTime.Parse(sdate);
                    //DateTime tdate = DateTime.Parse(edate).AddHours(23).AddMinutes(59).AddSeconds(59);
                    DateTime tdate = DateTime.Parse(edate);
                    List<INVOICE> listinvoice = (from c in context.invoice_master
                                                 join det in context.customer_master on c.cust_mas_sno equals det.cust_mas_sno
                                                 where (c.approval_status != "2") && (c.invoice_date >= fdate && c.invoice_date <= tdate) &&
                                                 (cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                                 select new INVOICE
                                                 {
                                                     Invoice_No = c.invoice_no,
                                                     Invoice_Date = c.invoice_date,
                                                     Chus_Mas_No = det.cust_mas_sno,
                                                     Chus_Name = det.customer_name,
                                                     Customer_ID_Type = c.customer_id_type,
                                                     Customer_ID_No = c.customer_id_no,
                                                     Total = (decimal)c.total_amount,
                                                     Total_Vt = (decimal)c.vat_amount,
                                                     Total_Without_Vt = (decimal)c.total_without_vat,
                                                     //Vat_Amount= (long)c.vat_amount,
                                                     warrenty = c.warrenty,
                                                     goods_status = c.goods_status,
                                                     delivery_status = c.delivery_status,
                                                     approval_date = approval_date,
                                                     p_date = (DateTime)c.p_date,
                                                     Control_No = c.control_no,
                                                     Currency_Code = c.currency_code,
                                                     Payment_Type = c.payment_type,
                                                     Status = c.goods_status,
                                                     Company_Name = c.company_master.company_name,
                                                     Due_Date = c.due_date,
                                                     Invoice_Expired_Date = c.invoice_expired


                                                 }).ToList();
                    return listinvoice;
                }
                
            }
        }
        public List<String> GetZrep( string stdate, string enddate)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                

                    string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    DateTime fdate = DateTime.Parse(sdate);
                    DateTime tdate = DateTime.Parse(edate);
                    var selectedDates = new List<DateTime?>();
                    var concat = new List<String>();
                    
                    var m = (from c in context.zreport_details select c.zreport_date).ToList();
                    for (var date = fdate; date <= tdate; date = date.AddDays(1))
                    {
                        selectedDates.Add(date);
                        
                    }
                    
                    for (var i = 0; i < selectedDates.Count(); i++)
                    {
                        for (var j = 0; j < m.Count(); j++)
                        {
                            if (m.Contains(selectedDates[i]))
                            {
                                Zreport_Date1 = selectedDates[i];
                                Zreport_Status = "Submitted";
                                concat.Add(Zreport_Date1.ToString() + ',' + Zreport_Status);
                                break;
                                
                            }
                            else
                            {
                                Zreport_Date1 = selectedDates[i];
                                Zreport_Status = "Non Submitted";
                                concat.Add(Zreport_Date1.ToString() + ',' + Zreport_Status);
                                break;

                            }
                        }
                    }
                   
                    return concat;

                   
            }
        }public List<String> GetZrep1(long inst, string stdate, string enddate)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                

                    string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    DateTime fdate = DateTime.Parse(sdate);
                    DateTime tdate = DateTime.Parse(edate);
                    var selectedDates = new List<DateTime?>();
                    var concat = new List<String>();
                    
                    var m = (from c in context.zreport_details where inst == 0 ? c.comp_mas_sno == c.comp_mas_sno : c.comp_mas_sno == inst select c.zreport_date).ToList();
                    for (var date = fdate; date <= tdate; date = date.AddDays(1))
                    {
                        selectedDates.Add(date);
                        
                    }
                    
                    for (var i = 0; i < selectedDates.Count(); i++)
                    {
                        for (var j = 0; j < m.Count(); j++)
                        {
                            if (m.Contains(selectedDates[i]))
                            {
                                Zreport_Date1 = selectedDates[i];
                                Zreport_Status = "Submitted";
                                concat.Add(Zreport_Date1.ToString() + ',' + Zreport_Status);
                                break;
                                
                            }
                            else
                            {
                                Zreport_Date1 = selectedDates[i];
                                Zreport_Status = "Non Submitted";
                                concat.Add(Zreport_Date1.ToString() + ',' + Zreport_Status);
                                break;

                            }
                        }
                    }
                   
                    return concat;

                   
            }
        }
        public List<INVOICE> GetInvDetRep(long Comp,long inv, string stdate, string enddate, long Cust)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (stdate == "" && enddate == "")
                {
                    List<INVOICE> listinvoice = (from c in context.invoice_details
                                                 join det in context.invoice_master on c.inv_mas_sno equals det.inv_mas_sno
                                                 //join cus in context.customer_master on det.cust_mas_sno equals cus.cust_mas_sno
                                                 where (det.approval_status == "2") && (inv == 0 ? det.cust_mas_sno == det.cust_mas_sno : det.inv_mas_sno == inv)
                                                 && (Cust == 0 ?  det.inv_mas_sno == det.inv_mas_sno: det.cust_mas_sno == Cust)
                                                 && (Comp == 0 ?  det.comp_mas_sno == det.comp_mas_sno : det.comp_mas_sno == Comp)
                                                 select new INVOICE
                                                 {
                                                     Inv_Mas_Sno = (long)c.inv_mas_sno,
                                                     Inv_Det_Sno = c.inv_det_sno,
                                                     Item_Description = c.item_description,
                                                     Vat_Percentage = (decimal)c.vat_percentage,
                                                     Item_Qty = (decimal)c.item_qty,
                                                     Item_Unit_Price = (decimal)c.item_unit_price,
                                                     Item_Total_Amount = (decimal)c.item_total_amount,
                                                     Vat_Amount = (decimal)c.vat_amount,
                                                     Item_Without_vat = (decimal)c.item_without_vat,
                                                     Remarks = c.remarks,
                                                     Invoice_Date = (DateTime)det.invoice_date

                                                 }).ToList();
                    return listinvoice;
                }
                else
                {
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    DateTime fdate = DateTime.Parse(stdate);
                    //DateTime tdate = DateTime.Parse(edate).AddHours(23).AddMinutes(59).AddSeconds(59);
                    DateTime tdate = DateTime.Parse(enddate);
                    List<INVOICE> listinvoice = (from c in context.invoice_details
                                                 join det in context.invoice_master on c.inv_mas_sno equals det.inv_mas_sno
                                                 //join cus in context.customer_master on det.cust_mas_sno equals cus.cust_mas_sno
                                                 where (det.approval_status == "2") && (inv == 0 ? det.cust_mas_sno == det.cust_mas_sno : det.inv_mas_sno == inv)
                                                 && (Cust == 0 ? det.inv_mas_sno == det.inv_mas_sno : det.cust_mas_sno == Cust)
                                                 && (Comp == 0 ? det.comp_mas_sno == det.comp_mas_sno : det.comp_mas_sno == Comp)
                                                 && (det.invoice_date >= fdate && det.invoice_date <= tdate) 
                                                 //(cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                                 select new INVOICE
                                                 {
                                                     Inv_Mas_Sno = (long)c.inv_mas_sno,
                                                     Inv_Det_Sno = c.inv_det_sno,
                                                     Item_Description = c.item_description,
                                                     Vat_Percentage = (decimal)c.vat_percentage,
                                                     Item_Qty = (decimal)c.item_qty,
                                                     Item_Unit_Price = (decimal)c.item_unit_price,
                                                     Item_Total_Amount = (decimal)c.item_total_amount,
                                                     Vat_Amount = (decimal)c.vat_amount,
                                                     Item_Without_vat = (decimal)c.item_without_vat,
                                                     Remarks = c.remarks,
                                                     Invoice_Date = (DateTime)det.invoice_date

                                                 }).ToList();
                    return listinvoice;
                }

            }
        }
        public List<INVOICE> GetInvDetRep(long inv, string stdate, string enddate, long Cust)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (stdate == "" && enddate == "")
                {
                    List<INVOICE> listinvoice = (from c in context.invoice_details
                                                 join det in context.invoice_master on c.inv_mas_sno equals det.inv_mas_sno
                                                 //join cus in context.customer_master on det.cust_mas_sno equals cus.cust_mas_sno
                                                 where (det.approval_status == "2") && (inv == 0 ? det.cust_mas_sno == det.cust_mas_sno : det.inv_mas_sno == inv)
                                                 && (Cust == 0 ?  det.inv_mas_sno == det.inv_mas_sno: det.cust_mas_sno == Cust)
                                                 //&& (Comp == 0 ?  det.comp_mas_sno == det.comp_mas_sno : det.comp_mas_sno == Comp)
                                                 select new INVOICE
                                                 {
                                                     Inv_Mas_Sno = (long)c.inv_mas_sno,
                                                     Inv_Det_Sno = c.inv_det_sno,
                                                     Item_Description = c.item_description,
                                                     Vat_Percentage = (decimal)c.vat_percentage,
                                                     Item_Qty = (decimal)c.item_qty,
                                                     Item_Unit_Price = (decimal)c.item_unit_price,
                                                     Item_Total_Amount = (decimal)c.item_total_amount,
                                                     Vat_Amount = (decimal)c.vat_amount,
                                                     Item_Without_vat = (decimal)c.item_without_vat,
                                                     Remarks = c.remarks,
                                                     Invoice_Date = (DateTime)det.invoice_date

                                                 }).ToList();
                    return listinvoice;
                }
                else
                {
                    string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    DateTime fdate = DateTime.Parse(sdate);
                    //DateTime tdate = DateTime.Parse(edate).AddHours(23).AddMinutes(59).AddSeconds(59);
                    DateTime tdate = DateTime.Parse(edate);
                    List<INVOICE> listinvoice = (from c in context.invoice_details
                                                 join det in context.invoice_master on c.inv_mas_sno equals det.inv_mas_sno
                                                 //join cus in context.customer_master on det.cust_mas_sno equals cus.cust_mas_sno
                                                 where (det.approval_status == "2") && (inv == 0 ? det.cust_mas_sno == det.cust_mas_sno : det.inv_mas_sno == inv)
                                                 && (Cust == 0 ? det.inv_mas_sno == det.inv_mas_sno : det.cust_mas_sno == Cust)
                                                // && (Comp == 0 ? det.comp_mas_sno == det.comp_mas_sno : det.comp_mas_sno == Comp)
                                                 && (det.invoice_date >= fdate && det.invoice_date <= tdate) 
                                                 //(cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                                 select new INVOICE
                                                 {
                                                     Inv_Mas_Sno = (long)c.inv_mas_sno,
                                                     Inv_Det_Sno = c.inv_det_sno,
                                                     Item_Description = c.item_description,
                                                     Vat_Percentage = (decimal)c.vat_percentage,
                                                     Item_Qty = (decimal)c.item_qty,
                                                     Item_Unit_Price = (decimal)c.item_unit_price,
                                                     Item_Total_Amount = (decimal)c.item_total_amount,
                                                     Vat_Amount = (decimal)c.vat_amount,
                                                     Item_Without_vat = (decimal)c.item_without_vat,
                                                     Remarks = c.remarks,
                                                     Invoice_Date = (DateTime)det.invoice_date

                                                 }).ToList();
                    return listinvoice;
                }

            }
        }
        public List<INVOICE> GetInvDetRep1(long Comp,long inv, string stdate, string enddate, long Cust)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (stdate == "" && enddate == "")
                {
                    List<INVOICE> listinvoice = (from c in context.invoice_details
                                                 join det in context.invoice_master on c.inv_mas_sno equals det.inv_mas_sno
                                                 //join cus in context.customer_master on det.cust_mas_sno equals cus.cust_mas_sno
                                                 where (det.approval_status == "2") && (inv == 0 ? det.cust_mas_sno == det.cust_mas_sno : det.inv_mas_sno == inv)
                                                 && (Cust == 0 ?  det.inv_mas_sno == det.inv_mas_sno: det.cust_mas_sno == Cust)
                                                 && (Comp == 0 ?  det.comp_mas_sno == det.comp_mas_sno: det.comp_mas_sno == Comp)
                                                 select new INVOICE
                                                 {
                                                     Inv_Mas_Sno = (long)c.inv_mas_sno,
                                                     Inv_Det_Sno = c.inv_det_sno,
                                                     Item_Description = c.item_description,
                                                     Vat_Percentage = (decimal)c.vat_percentage,
                                                     Item_Qty = (decimal)c.item_qty,
                                                     Item_Unit_Price = (decimal)c.item_unit_price,
                                                     Item_Total_Amount = (decimal)c.item_total_amount,
                                                     Vat_Amount = (decimal)c.vat_amount,
                                                     Item_Without_vat = (decimal)c.item_without_vat,
                                                     Remarks = c.remarks,
                                                     Invoice_Date = (DateTime)det.invoice_date

                                                 }).ToList();
                    return listinvoice;
                }
                else
                {
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    DateTime fdate = DateTime.Parse(stdate);
                    //DateTime tdate = DateTime.Parse(edate).AddHours(23).AddMinutes(59).AddSeconds(59);
                    DateTime tdate = DateTime.Parse(enddate);
                    List<INVOICE> listinvoice = (from c in context.invoice_details
                                                 join det in context.invoice_master on c.inv_mas_sno equals det.inv_mas_sno
                                                 //join cus in context.customer_master on det.cust_mas_sno equals cus.cust_mas_sno
                                                 where (det.approval_status == "2") && (inv == 0 ? det.cust_mas_sno == det.cust_mas_sno : det.inv_mas_sno == inv)
                                                 && (Cust == 0 ? det.inv_mas_sno == det.inv_mas_sno : det.cust_mas_sno == Cust)
                                                 && (det.invoice_date >= fdate && det.invoice_date <= tdate)
                                                 && (Comp == 0 ? det.comp_mas_sno == det.comp_mas_sno : det.comp_mas_sno == Comp)
                                                 //(cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                                 select new INVOICE
                                                 {
                                                     Inv_Mas_Sno = (long)c.inv_mas_sno,
                                                     Inv_Det_Sno = c.inv_det_sno,
                                                     Item_Description = c.item_description,
                                                     Vat_Percentage = (decimal)c.vat_percentage,
                                                     Item_Qty = (decimal)c.item_qty,
                                                     Item_Unit_Price = (decimal)c.item_unit_price,
                                                     Item_Total_Amount = (decimal)c.item_total_amount,
                                                     Vat_Amount = (decimal)c.vat_amount,
                                                     Item_Without_vat = (decimal)c.item_without_vat,
                                                     Remarks = c.remarks,
                                                     Invoice_Date = (DateTime)det.invoice_date

                                                 }).ToList();
                    return listinvoice;
                }

            }
        }
        public List<INVOICE> GetInvDetRep_1(long Comp, string inv, string stdate, string enddate, long Cust)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                if (stdate == "" && enddate == "")
                {
                    List<INVOICE> listinvoice = (from c in context.invoice_details
                                                 join det in context.invoice_master on c.inv_mas_sno equals det.inv_mas_sno
                                                 //join cus in context.customer_master on det.cust_mas_sno equals cus.cust_mas_sno
                                                 where (det.approval_status == "2") && (Cust == 0 ? det.cust_mas_sno == det.cust_mas_sno : det.cust_mas_sno == Cust)
                                                 && (Cust == 0 ? det.inv_mas_sno == det.inv_mas_sno : det.cust_mas_sno == Cust)
                                                 && (Comp == 0 ? det.comp_mas_sno == det.comp_mas_sno : det.comp_mas_sno == Comp)
                                                 select new INVOICE
                                                 {
                                                     Inv_Mas_Sno = (long)c.inv_mas_sno,
                                                     Inv_Det_Sno = c.inv_det_sno,
                                                     Item_Description = c.item_description,
                                                     Vat_Percentage = (decimal)c.vat_percentage,
                                                     Item_Qty = (decimal)c.item_qty,
                                                     Item_Unit_Price = (decimal)c.item_unit_price,
                                                     Item_Total_Amount = (decimal)c.item_total_amount,
                                                     Vat_Amount = (decimal)c.vat_amount,
                                                     Item_Without_vat = (decimal)c.item_without_vat,
                                                     Remarks = c.remarks,
                                                     Invoice_Date = (DateTime)det.invoice_date

                                                 }).ToList();
                    return listinvoice;
                }
                else
                {
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string sdate = DateTime.ParseExact(stdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    //string edate = DateTime.ParseExact(enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    DateTime fdate = DateTime.Parse(stdate);
                    //DateTime tdate = DateTime.Parse(edate).AddHours(23).AddMinutes(59).AddSeconds(59);
                    DateTime tdate = DateTime.Parse(enddate);
                    List<INVOICE> listinvoice = (from c in context.invoice_details
                                                 join det in context.invoice_master on c.inv_mas_sno equals det.inv_mas_sno
                                                 //join cus in context.customer_master on det.cust_mas_sno equals cus.cust_mas_sno
                                                 where (det.approval_status == "2") && (Cust == 0 ? det.cust_mas_sno == det.cust_mas_sno : det.cust_mas_sno == Cust)
                                                 && (Cust == 0 ? det.inv_mas_sno == det.inv_mas_sno : det.cust_mas_sno == Cust)
                                                 && (det.invoice_date >= fdate && det.invoice_date <= tdate)
                                                 && (Comp == 0 ? det.comp_mas_sno == det.comp_mas_sno : det.comp_mas_sno == Comp)
                                                 //(cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                                 select new INVOICE
                                                 {
                                                     Inv_Mas_Sno = (long)c.inv_mas_sno,
                                                     Inv_Det_Sno = c.inv_det_sno,
                                                     Item_Description = c.item_description,
                                                     Vat_Percentage = (decimal)c.vat_percentage,
                                                     Item_Qty = (decimal)c.item_qty,
                                                     Item_Unit_Price = (decimal)c.item_unit_price,
                                                     Item_Total_Amount = (decimal)c.item_total_amount,
                                                     Vat_Amount = (decimal)c.vat_amount,
                                                     Item_Without_vat = (decimal)c.item_without_vat,
                                                     Remarks = c.remarks,
                                                     Invoice_Date = (DateTime)det.invoice_date

                                                 }).ToList();
                    return listinvoice;
                }

            }
        }
        #endregion Methods




    }
}

    
