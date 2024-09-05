using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BIZINVOICING.BusinessEntities.Common;
using DaL.BIZINVOICING.EDMX;
using Org.BouncyCastle.Crypto.Macs;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class InvoiceC
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
        public String Cmpny_Name { get; set; }
        public String Customer_Name { get; set; }

        public String Inv_Remarks { get; set; }
        public String Remarks { get; set; }

        public String vat_category { get; set; }
        public string Currency_Code { get; set; }
        public string Currency_Name { get; set; }

        public decimal Invoice_Amount { get; set; }
        public decimal Amment_Amount { get; set; }
        public decimal Total { get; set; }
        public decimal Item_Qty { get; set; }
       
        public string AuditBy { get; set; }

        public string Reason { get; set; }
        
        public string goods_status { get; set; }
        public string delivery_status { get; set; }
        public DateTime Audit_Date { get; set; }


       
        public DateTime p_date { get; set; }
        public long Inv_Can_No { get; set; }
        public long Inv_Amm_No { get; set; }
        public long Cust_Mas_No { get; set; }
        
        public string Control_No { get; set; }
        public long? vendor_id { get;  set; }
        public string vendor { get;  set; }
        public long? branch_sno { get;  set; }
        public string branch { get;  set; }
        public int no_of_invoices { get;  set; }
        public decimal? invoice_amount { get;  set; }
        public int no_of_payments { get;  set; }

        public long? receipt_amount;
        #endregion Properties
        #region methods

        public long AddCancel(InvoiceC sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                invoice_cancellation ps = new invoice_cancellation()
                {
                    control_no = sc.Control_No,
                    comp_mas_sno = sc.Com_Mas_Sno,
                    cust_mas_sno = sc.Cust_Mas_No,
                    inv_mas_sno = sc.Inv_Mas_Sno,
                    invoice_amount = sc.Invoice_Amount,
                    reason_for_cancel = sc.Reason,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.invoice_cancellation.Add(ps);
                context.SaveChanges();
                return ps.inv_can_sno;
            }

        }
        public bool ValidateCancel(String controlno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.invoice_cancellation
                                  where (c.control_no == controlno)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<InvoiceC> GetBranches(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_cancellation
                                where c.comp_mas_sno == sno
                                select new InvoiceC
                                {
                                    Inv_Can_No = c.inv_can_sno,
                                    Control_No = c.control_no,
                                    Com_Mas_Sno = (long)c.comp_mas_sno,
                                    Cust_Mas_No = (long)c.cust_mas_sno,
                                    Inv_Mas_Sno = (long)c.inv_mas_sno,
                                    Invoice_Amount = (decimal)c.invoice_amount,
                                    Reason = c.reason_for_cancel,
                                    AuditBy = c.posted_by,
                                    Audit_Date = (DateTime)c.posted_date
                                }).OrderByDescending(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public InvoiceC getCancels(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.invoice_cancellation
                                where c.inv_can_sno == sno
                                select new InvoiceC
                                {
                                    Inv_Can_No = c.inv_can_sno,
                                    Control_No = c.control_no,
                                    Com_Mas_Sno = (long)c.comp_mas_sno,
                                    Cust_Mas_No = (long)c.cust_mas_sno,
                                    Inv_Mas_Sno = (long)c.inv_mas_sno,
                                    Invoice_Amount = (decimal)c.invoice_amount,
                                    Reason = c.reason_for_cancel,
                                    AuditBy = c.posted_by,
                                    Audit_Date = (DateTime)c.posted_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }

        public long AddAmmend(InvoiceC sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                invoice_ammendment ps = new invoice_ammendment()
                {
                    control_no = sc.Control_No,
                    comp_mas_sno = sc.Com_Mas_Sno,
                    cust_mas_sno = sc.Cust_Mas_No,
                    inv_mas_sno = sc.Inv_Mas_Sno,
                    invoice_amount = sc.Invoice_Amount,
                    amment_amount = sc.Amment_Amount,
                    payment_type = sc.Payment_Type,
                    due_date = sc.Due_Date,
                    expired_date = sc.Invoice_Expired_Date,
                    reason_for_amm = sc.Reason,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.invoice_ammendment.Add(ps);
                context.SaveChanges();
                return ps.inv_amm_sno;
            }

        }
        public bool ValidateAmmend(String controlno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.invoice_ammendment
                                  where (c.control_no == controlno)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<InvoiceC> GetAmmends(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.invoice_ammendment
                                where c.comp_mas_sno == sno
                                select new InvoiceC
                                {
                                    Inv_Amm_No = c.inv_amm_sno,
                                    Control_No = c.control_no,
                                    Com_Mas_Sno = (long)c.comp_mas_sno,
                                    Cust_Mas_No = (long)c.cust_mas_sno,
                                    Inv_Mas_Sno = (long)c.inv_mas_sno,
                                    Invoice_Amount = (decimal)c.invoice_amount,
                                    Amment_Amount = (decimal)c.amment_amount,
                                    Reason = c.reason_for_amm,
                                    AuditBy = c.posted_by,
                                    Audit_Date = (DateTime)c.posted_date
                                }).OrderByDescending(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public InvoiceC getAmmend(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.invoice_ammendment
                                where c.inv_amm_sno == sno
                                select new InvoiceC
                                {
                                    Inv_Amm_No = c.inv_amm_sno,
                                    Control_No = c.control_no,
                                    Com_Mas_Sno = (long)c.comp_mas_sno,
                                    Cust_Mas_No = (long)c.cust_mas_sno,
                                    Inv_Mas_Sno = (long)c.inv_mas_sno,
                                    Invoice_Amount = (decimal)c.invoice_amount,
                                    Amment_Amount = (decimal)c.amment_amount,
                                    Reason = c.reason_for_amm,
                                    AuditBy = c.posted_by,
                                    Audit_Date = (DateTime)c.posted_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }

        public List<InvoiceC> GetAmendRep(List<long> companyIds,List<long> customerIds,List<long> invoiceIds, string stdate, string enddate)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                DateTime? fdate = null;
                if (!string.IsNullOrEmpty(stdate)) fdate = DateTime.Parse(stdate);
                DateTime? tdate = null;
                if (!string.IsNullOrEmpty(enddate)) tdate = DateTime.Parse(enddate);

                List<InvoiceC> invoices = (from c in context.invoice_ammendment
                                           join det in context.invoice_master on c.inv_mas_sno equals det.inv_mas_sno
                                           join cus in context.customer_master on c.cust_mas_sno equals cus.cust_mas_sno
                                           join d in context.company_master on det.comp_mas_sno equals d.comp_mas_sno
                                           where (det.approval_status != "Cancel")
                                           && (companyIds.Contains(0) || companyIds.Contains((long) c.comp_mas_sno))
                                           && (customerIds.Contains(0) || customerIds.Contains((long)c.cust_mas_sno))
                                           && (invoiceIds.Contains(0) || invoiceIds.Contains((long)c.inv_mas_sno))
                                           && (!fdate.HasValue || fdate <= c.posted_date)
                                           && (!tdate.HasValue || tdate >= c.posted_date)
                                           select new InvoiceC
                                           {
                                               Invoice_No = det.invoice_no,
                                               Customer_Name = cus.customer_name,
                                               Payment_Type = det.payment_type,
                                               Control_No = c.control_no,
                                               Reason = c.reason_for_amm,
                                               Invoice_Amount = (decimal)c.invoice_amount,
                                               Amment_Amount = (decimal)c.amment_amount,
                                               Audit_Date = (DateTime)c.posted_date,
                                               Invoice_Expired_Date = c.expired_date,
                                               Currency_Code = det.currency_code,
                                               Due_Date = (DateTime)c.due_date,
                                               Cmpny_Name = d.company_name,
                                               Invoice_Date = det.invoice_date
                                           }).OrderBy(z => z.Cmpny_Name).ThenBy(z => z.Invoice_No).ToList();
                return invoices != null ? invoices : new List<InvoiceC>();

            }
        }

        public List<InvoiceC> GetAmendRep(long Comp, string inv, string stdate, string enddate, long cust)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                DateTime? fdate = null;
                if (!string.IsNullOrEmpty(stdate)) fdate = DateTime.Parse(stdate);
                DateTime? tdate = null;
                if (!string.IsNullOrEmpty(enddate)) tdate = DateTime.Parse(enddate);

                List<InvoiceC> listinvoice = (from c in context.invoice_ammendment
                                              join det in context.invoice_master on c.inv_mas_sno equals det.inv_mas_sno
                                              join cus in context.customer_master on c.cust_mas_sno equals cus.cust_mas_sno
                                              where (cust == 0 ? true : c.cust_mas_sno == cust) && det.approval_status != "Cancel"
                                              && (!fdate.HasValue || c.posted_date >= fdate)
                                              && (!tdate.HasValue || c.posted_date >= tdate)
                                              && (Comp == 0 ? true : c.comp_mas_sno == Comp)
                                              && (!string.IsNullOrEmpty(inv) ? det.invoice_no == inv : true)
                                              select new InvoiceC
                                              {
                                                  Invoice_No = det.invoice_no,
                                                  Customer_Name = cus.customer_name,
                                                  Payment_Type = det.payment_type,
                                                  Control_No = c.control_no,
                                                  Reason = c.reason_for_amm,
                                                  Invoice_Amount = (decimal)c.invoice_amount,
                                                  Amment_Amount = (decimal)c.amment_amount,
                                                  Audit_Date = (DateTime)c.posted_date,
                                                  Invoice_Expired_Date = c.expired_date,
                                                  Currency_Code = det.currency_code,
                                                  Due_Date = (DateTime)c.due_date,

                                              }).OrderByDescending(z => z.Audit_Date).ToList();
                return listinvoice != null ? listinvoice : new List<InvoiceC>();
            }
        }

        public List<InvoiceC> GetCancelRep(List<long> companyIds,List<long> customerIds,List<long> invoiceIds, string stdate, string enddate)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                DateTime? fdate = null;
                if (!string.IsNullOrEmpty(stdate)) fdate = DateTime.Parse(stdate);
                DateTime? tdate = null;
                if (!string.IsNullOrEmpty(enddate)) tdate = DateTime.Parse(enddate);

                List<InvoiceC> invoices = (from c in context.invoice_cancellation
                                           join det in context.invoice_master on c.inv_mas_sno equals det.inv_mas_sno
                                           join b in context.company_master on c.comp_mas_sno equals b.comp_mas_sno
                                           join cus in context.customer_master on c.cust_mas_sno equals cus.cust_mas_sno
                                           where (companyIds.Contains(0) || companyIds.Contains((long)c.comp_mas_sno))
                                           && (customerIds.Contains(0) || customerIds.Contains((long)c.cust_mas_sno))
                                           && (invoiceIds.Contains(0) || invoiceIds.Contains((long)c.inv_mas_sno))
                                           && (!fdate.HasValue || fdate <= c.posted_date)
                                           && (!tdate.HasValue || tdate >= c.posted_date)

                                           select new InvoiceC
                                           {
                                               Invoice_No = det.invoice_no,
                                               Customer_Name = cus.customer_name,
                                               Cmpny_Name = b.company_name,
                                               Com_Mas_Sno = b.comp_mas_sno,
                                               Payment_Type = det.payment_type,
                                               Control_No = c.control_no,
                                               Reason = c.reason_for_cancel,
                                               Invoice_Amount = (decimal)c.invoice_amount,
                                               Audit_Date = (DateTime)c.posted_date,
                                               Currency_Code = det.currency_code,
                                               p_date = (DateTime)c.posted_date,
                                               goods_status = det.approval_status
                                           }).OrderByDescending(z => z.Audit_Date).ToList();
                return invoices != null ? invoices : new List<InvoiceC>();
            }
        }

        public List<InvoiceC> GetCancelRep(long Comp, string inv, string stdate, string enddate, long cust)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                DateTime? fdate = null;
                if (!string.IsNullOrEmpty(stdate)) fdate = DateTime.Parse(stdate);
                DateTime? tdate = null;
                if (!string.IsNullOrEmpty(enddate)) tdate = DateTime.Parse(enddate);

                List<InvoiceC> listinvoice = (from c in context.invoice_cancellation
                                              join det in context.invoice_master on c.inv_mas_sno equals det.inv_mas_sno
                                              join cus in context.customer_master on c.cust_mas_sno equals cus.cust_mas_sno
                                              where (cust == 0 ? true : c.cust_mas_sno == cust) 
                                              && (!fdate.HasValue || fdate >= c.posted_date)
                                              && (!tdate.HasValue || tdate <= c.posted_date)
                                              && (Comp == 0 ? true : c.comp_mas_sno == Comp)
                                              && (!string.IsNullOrEmpty(inv) ? det.invoice_no == inv : true)
                                              select new InvoiceC
                                              {
                                                  Invoice_No = det.invoice_no,
                                                  Customer_Name = cus.customer_name,
                                                  Payment_Type = det.payment_type,
                                                  Control_No = c.control_no,
                                                  Reason = c.reason_for_cancel,
                                                  Invoice_Amount = (decimal)c.invoice_amount,
                                                  Audit_Date = (DateTime)c.posted_date,
                                                  Currency_Code = det.currency_code,
                                                  p_date = (DateTime)c.posted_date,
                                              }).OrderByDescending(z => z.Audit_Date).ToList();
                return listinvoice != null ? listinvoice : new List<InvoiceC>();
            }
        }
        #endregion
        public List<InvoiceC> InvoiceConsolidatedReport(string stdate, string enddate)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                /*DateTime fdate = DateTime.Parse(stdate);
                DateTime tdate = DateTime.Parse(enddate);*/
                DateTime? fdate = null;
                if (!string.IsNullOrEmpty(stdate)) fdate = DateTime.Parse(stdate);
                DateTime? tdate = null;
                if (!string.IsNullOrEmpty(enddate)) tdate = DateTime.Parse(enddate);


                var result = from A in context.invoice_master
                             join B in context.company_master on A.comp_mas_sno equals B.comp_mas_sno
                             join C in context.branch_name on B.branch_sno equals C.sno
                             where A.approval_status == "2"
                             && (!fdate.HasValue || fdate <= A.invoice_date) 
                             && (!tdate.HasValue || tdate >= A.invoice_date)
                             group new { A, B, C } by new
                             {
                                 A.comp_mas_sno,
                                 A.currency_code,
                                 B.company_name,
                                 B.branch_sno,
                                 C.name
                             } into g
                             select new InvoiceC
                             {
                                 vendor_id = g.Key.comp_mas_sno,
                                 vendor = g.Key.company_name,
                                 branch_sno = g.Key.branch_sno,
                                 Currency_Code = g.Key.currency_code,
                                 branch = g.Key.name,
                                 no_of_invoices = g.Count(x => (x.A.inv_mas_sno).ToString() != null),
                                 invoice_amount = g.Sum(x => x.A.total_amount)
                             };

            

                var list = result.ToList();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }

        public List<InvoiceC> PaymentConsolidatedReport(string stdate, string enddate)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                DateTime? fromDate = null;
                if (!string.IsNullOrEmpty(stdate)) fromDate = DateTime.Parse(stdate);
                DateTime? toDate = null;
                if (!string.IsNullOrEmpty(enddate)) toDate = DateTime.Parse(enddate);


                var result = from A in context.payment_details
                             join B in context.company_master on A.comp_mas_sno equals B.comp_mas_sno
                             join C in context.branch_name on B.branch_sno equals C.sno
                             where A.status == "Passed"
                                   && (!fromDate.HasValue || fromDate <= A.payment_date)
                                   && (!toDate.HasValue || toDate >= A.payment_date)
                             group A by new
                             {
                                 A.comp_mas_sno,
                                 A.currency_code,
                                 B.company_name,
                                 B.branch_sno,
                                 C.name
                             } into g
                             select new InvoiceC
                             {
                                 vendor_id = g.Key.comp_mas_sno,
                                 vendor = g.Key.company_name,
                                 branch_sno = g.Key.branch_sno,
                                 Currency_Code = g.Key.currency_code,
                                 branch = g.Key.name,
                                 no_of_payments = g.Count(),
                                 receipt_amount = g.Sum(x => x.paid_amount)
                             };


               /* var result = from A in context.payment_details
                             join B in context.company_master on A.comp_mas_sno equals B.comp_mas_sno
                             join C in context.branch_name on B.branch_sno equals C.sno
                             where A.status.Contains("Passed") &&
                                   A.payment_date >= fdate &&
                                   A.payment_date <= tdate
                             group A by new
                             {
                                 A.comp_mas_sno,
                                 B.company_name,
                                 B.branch_sno,
                                 C.name
                             } into g
                             select new InvoiceC
                             {
                                 vendor_id = g.Key.comp_mas_sno,
                                 vendor = g.Key.company_name,
                                 branch_sno = g.Key.branch_sno,
                                 branch = g.Key.name,
                                 no_of_payments = g.Count(),
                                 Receipt_amount = g.Sum(x => x.paid_amount)
                             };*/

                var list = result.ToList();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }



    }
}
