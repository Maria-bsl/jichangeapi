using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BIZINVOICING.BusinessEntities.Common;
using DaL.BIZINVOICING.EDMX;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class INVOICE
    {
        #region Properties
        public long Inv_Mas_Sno { get; set; }
        public long Inv_Det_Sno { get; set; }
        public DateTime? Invoice_Date { get; set; }
        public String Invoice_No { get; set; }
        public long Chus_Mas_No { get; set; }
        public String Chus_Name { get; set; }
        public long Com_Mas_Sno { get; set; }
        public String Cmpny_Name { get; set; }

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
        public String Customer_ID_Type { get; set; }
        public String Customer_ID_No { get; set; }
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
                    comp_mas_sno = sc.Com_Mas_Sno,
                    cust_mas_sno = sc.Chus_Mas_No,
                    currency_code = sc.Currency_Code,
                    total_without_vat = sc.Total_Without_Vt,
                    total_amount = sc.Total,
                    vat_amount = sc.Vat_Amount,
                    inv_remarks = sc.Inv_Remarks,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                    warrenty= sc.warrenty,
                    goods_status=sc.goods_status,
                    delivery_status=sc.delivery_status,
                    customer_id_type = sc.Customer_ID_Type,
                    customer_id_no = sc.Customer_ID_No
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

                var dcount = context.invoice_master.ToList().Where(x => Convert.ToDateTime(x.approval_date) == Convert.ToDateTime(System.DateTime.Now.Date));
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
        public List<INVOICE> GetINVOICEMas()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_master
                                join det in context.customer_master  on c.cust_mas_sno equals det.cust_mas_sno
                                join  cmp in context.company_master on c.comp_mas_sno equals cmp.comp_mas_sno
                                join cur in context.currency_master on c.currency_code equals cur.currency_code
                                select new INVOICE
                                {
                                    Com_Mas_Sno = c.company_master.comp_mas_sno,
                                    Cmpny_Name= cmp.company_name,
                                    Inv_Mas_Sno =c.inv_mas_sno,
                                    Invoice_No = c.invoice_no,
                                    Invoice_Date = c.invoice_date,
                                    Chus_Mas_No = (long)c.cust_mas_sno,
                                    Chus_Name = det.customer_name,
                                    Currency_Code = c.currency_code,
                                    Currency_Name= cur.currency_name,
                                    Remarks= c.inv_remarks,
                                    Customer_ID_Type = c.customer_id_type,
                                    Customer_ID_No = c.customer_id_no,
                                    Total = (decimal)c.total_amount,
                                    Total_Vt = (decimal)c.vat_amount,
                                    Total_Without_Vt = (decimal)c.total_without_vat,
                                    //Vat_Amount= (long)c.vat_amount,
                                    warrenty= c.warrenty,
                                    goods_status=c.goods_status,
                                    delivery_status=c.delivery_status,
                                    grand_count= (int)c.grand_count,
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


        public InvoicePDfData GetINVOICEpdf(int invoicenumber)
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
                                        CompMobNo = c.company_master.mobile_no,
                                        CompEmail = c.company_master.email_address,
                                        CompVatNo = c.company_master.vat_no,
                                        TinNo = c.company_master.tin_no,
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
        public List<InvoicePDfData> GetTotal()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                string dt = DateTime.Now.AddDays(-1).ToShortDateString();
                DateTime dt1 = DateTime.Parse(dt);
                var adetails = (from c in context.invoice_master
                                where c.approval_status == "2" && c.approval_date == dt1
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
        public List<INVOICE> GetVATTotal(string type)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                string dt = DateTime.Now.AddDays(-1).ToShortDateString();
                DateTime dt1 = DateTime.Parse(dt);
                var adetails = (from d in context.invoice_details
                                join c in context.invoice_master on d.inv_mas_sno equals c.inv_mas_sno
                                where c.approval_status == "2" && c.approval_date == dt1 && d.vat_type == type
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

                var dcount = context.invoice_master.ToList().Where(x => Convert.ToDateTime(x.approval_date) == Convert.ToDateTime(System.DateTime.Now.Date));
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
                    UpdateContactInfo.posted_date = DateTime.Now;
                    UpdateContactInfo.grand_count = grand_max + 1;
                    UpdateContactInfo.daily_count = dailycount+1;
                    UpdateContactInfo.approval_status = dep.approval_status;
                    UpdateContactInfo.approval_date = dep.approval_date;
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
        public List<INVOICE> GetInvoiceDetails(long Sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_details.Where(c => c.inv_mas_sno == Sno)
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

        #endregion Methods



      
    }
}

    
