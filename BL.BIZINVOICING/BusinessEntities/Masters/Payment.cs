using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class Payment
    {
        #region Properties
        public long SNO { get; set; }
        public string Payment_SNo { get; set; }
        public DateTime Payment_Date { get; set; }
        public string Payment_Type { get; set; }
        public string Amount_Type { get; set; }
        public string Payer_Name { get; set; }
        public string Payment_Trans_No { get; set; }
        public string Currency { get; set; }
        public long Invoice_Amount { get; set; }
        public long Invoice_Amount_Local { get; set; }
        public string Receipt_No { get; set; }
        public string Batch_No { get; set; }
        public string Remarks { get; set; }
        public string Authorize_Id { get; set; }
        public string Secure_Hash { get; set; }
        public string Response_Code { get; set; }
        public string Merchant { get; set; }
        public string Message { get; set; }
        public string Card { get; set; }
        public string Token { get; set; }
        public string Status { get; set; }
        public string Response { get; set; }
        public DateTime Audit_Date { get; set; }
        public string AuditAction { get; set; }
        public string AuditDone { get; set; }
        public long AuditID { get; set; }
        public long PaidAmount { get; set; }
        public long ModifiedAmount { get; set; }
        public long Amount30 { get; set; }
        public long Amount70 { get; set; }
        public decimal BOT { get; set; }
        public long AcademicSno { get; set; }
        public string Acad_Year { get; set; }
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }



        //public long Application_No_Service { get; set; }
        public string Term_Type { get; set; }
        public long Fee_Sno { get; set; }
        public long Term_Sno { get; set; }
        public DateTime Payment_Time { get; set; }
        public DateTime Approve_Date { get; set; }
        public string Fee_Data_Sno { get; set; }
        public string Currency_Code { get; set; }
        public long Requested_Amount { get; set; }
        public string Payment_Desc { get; set; }
        public string Payer_Id { get; set; }
        public string Trans_Channel { get; set; }
        public string PR_WB_ID { get; set; }
        //public string Payment_Type { get; set; }
        public string Institution_ID { get; set; }
        public string Control_No { get; set; }
        public string Chksum { get; set; }
        public string Charge_Type { get; set; }
        public string Invoice_Sno { get; set; }
        public string Currency_Type { get; set; }
        public string Posted_By { get; set; }
        public string Approved_By { get; set; }
        public DateTime Posted_Date { get; set; }
        public long Amount { get; set; }
        public long Surcharge_Fee { get; set; }
        public string Examined { get; set; }
        public string Authorized { get; set; }
        //public long AuditBy { get; set; }
        public long Receipt_No_Service { get; set; }

        public long Comp_Mas_Sno { get; set; }
        public string Company_Name { get; set; }
        public long Cust_Mas_Sno { get; set; }
        public string Customer_Name { get; set; }
        public long Class_Sno { get; set; }
        public string Class_Name { get; set; }
        public long Section_Sno { get; set; }
        public string Section_Name { get; set; }
        public long Comp_Mas_SNO { get; set; }
        public long Cust_Mas_SNO { get; set; }
        public decimal Item_Total_Amount { get; set; }
        public string Error_Text { get; set; }

        #endregion Properties
        #region Methods
        
        public void AddErrorLogs(Payment py)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                service_error_logs pt = new service_error_logs()
                {
                    error = py.Error_Text,
                    posted_date = DateTime.Now


                };
                context.service_error_logs.Add(pt);
                context.SaveChanges();
            }
        }
        /*public void AddOther(Payment py)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                other_details pt = new other_details()
                {
                    column1 = py.Col1,
                    column2 = py.Col2,
                    column3 = py.Col3,
                    posted_date = DateTime.Now


                };
                context.other_details.AddObject(pt);
                context.SaveChanges();
            }
        }*/
        public void AddPayment(Payment py)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                payment_details pt = new payment_details()
                {
                    payment_sno = py.Payment_SNo,
                    payment_date = DateTime.Now,
                    payment_time = DateTime.Now,
                    control_no = py.Control_No,
                    trans_channel = py.Trans_Channel,
                    payment_type = py.Payment_Type,
                    payer_name = py.Payer_Name,
                    amount_type = py.Amount_Type,
                    payment_desc = py.Payment_Desc,
                    payer_id = py.Payer_Id,
                    institution_id = py.Institution_ID,
                    currency_code = py.Currency_Code,
                    requested_amount = py.Requested_Amount,
                    chksum = py.Chksum,
                    paid_amount = py.Amount,
                    pay_trans_no = py.Payment_Trans_No,
                    receipt_no = py.Receipt_No,
                    message = py.Message,
                    batch_no = py.Batch_No,
                    authorize_id = py.Authorize_Id,
                    secure_hash = py.Secure_Hash,
                    response_code = py.Response_Code,
                    merchant = py.Merchant,
                    card = py.Card,
                    token = py.Token,
                    status = py.Status,
                    response = py.Response,
                    posted_by = py.Posted_By,
                    posted_date = DateTime.Now,
                    comp_mas_sno = py.Comp_Mas_Sno,
                    //company_name = py.Company_Name,
                    cust_mas_sno = py.Cust_Mas_Sno,
                    //customer_name = py.Customer_Name,
                    invoice_sno = py.Invoice_Sno,
                    amount30 = py.Amount30,
                    amount70 = py.Amount70

                };
                context.payment_details.Add(pt);
                context.SaveChanges();
            }
        }
        public void AddPaymentI(Payment py)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                payment_details pt = new payment_details()
                {
                    payment_sno = py.Payment_SNo,
                    payment_date = DateTime.Now,
                    payment_time = DateTime.Now,
                    control_no = py.Control_No,
                    trans_channel = py.Trans_Channel,
                    payment_type = py.Payment_Type,
                    payer_name = py.Payer_Name,
                    amount_type = py.Amount_Type,
                    payment_desc = py.Payment_Desc,
                    payer_id = py.Payer_Id,
                    institution_id = py.Institution_ID,
                    currency_code = py.Currency_Code,
                    requested_amount = py.Requested_Amount,
                    chksum = py.Chksum,
                    paid_amount = py.Amount,
                    pay_trans_no = py.Payment_Trans_No,
                    receipt_no = py.Receipt_No,
                    message = py.Message,
                    batch_no = py.Batch_No,
                    authorize_id = py.Authorize_Id,
                    secure_hash = py.Secure_Hash,
                    response_code = py.Response_Code,
                    merchant = py.Merchant,
                    card = py.Card,
                    token = py.Token,
                    status = py.Status,
                    response = py.Response,
                    posted_by = py.Posted_By,
                    posted_date = DateTime.Now,
                    comp_mas_sno = py.Comp_Mas_Sno,
                    //company_name = py.Company_Name,
                    cust_mas_sno = py.Cust_Mas_Sno,
                    //customer_name = py.Customer_Name,
                    invoice_sno = py.Invoice_Sno,
                    amount30 = py.Amount30,
                    amount70 = py.Amount70

                };
                context.payment_details.Add(pt);
                context.SaveChanges();
            }
        }
        public void AddPayment_Service(Payment py)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                payment_details pt = new payment_details()
                {
                    payment_sno = py.Payment_SNo,
                    payment_date = DateTime.Now,
                    payment_time = DateTime.Now,
                    control_no = py.Control_No,
                    trans_channel = py.Trans_Channel,
                    payment_type = py.Payment_Type,
                    payer_name = py.Payer_Name,
                    amount_type = py.Amount_Type,
                    payment_desc = py.Payment_Desc,
                    payer_id = py.Payer_Id,
                    institution_id = py.Institution_ID,
                    currency_code = py.Currency_Code,
                    requested_amount = py.Requested_Amount,
                    chksum = py.Chksum,
                    paid_amount = py.Amount,
                    pay_trans_no = py.Payment_Trans_No,
                    receipt_no = py.Receipt_No,
                    message = py.Message,
                    batch_no = py.Batch_No,
                    authorize_id = py.Authorize_Id,
                    secure_hash = py.Secure_Hash,
                    response_code = py.Response_Code,
                    merchant = py.Merchant,
                    card = py.Card,
                    token = py.Token,
                    status = py.Status,
                    response = py.Response,
                    posted_by = py.Posted_By,
                    posted_date = DateTime.Now

                };
                context.payment_details.Add(pt);
                context.SaveChanges();
            }
        }
        /*public void AddPayment_Reverse(Payment py)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                payment_details_reverse pt = new payment_details_reverse()
                {
                    payment_sno = py.Payment_SNo,
                    payment_date = py.Payment_Date,
                    payment_time = py.Payment_Time,
                    fee_data_sno = py.Fee_Data_Sno,
                    trans_channel = py.Trans_Channel,
                    payment_type = py.Payment_Type,
                    payer_name = py.Payer_Name,
                    amount_type = py.Amount_Type,
                    payment_desc = py.Payment_Desc,
                    payer_id = py.Payer_Id,
                    institution_id = py.Institution_ID,
                    admission_no = py.Admission_No,
                    term_sno = py.Term_Sno,
                    term_type = py.Term_Type,
                    fee_sno = py.Fee_Sno,
                    fee_type = py.Fee_Type,
                    currency_code = py.Currency_Code,
                    requested_amount = py.Requested_Amount,
                    paid_amount = py.Amount,
                    pay_trans_no = py.Payment_Trans_No,
                    receipt_no = py.Receipt_No,
                    message = py.Message,
                    batch_no = py.Batch_No,
                    authorize_id = py.Authorize_Id,
                    secure_hash = py.Secure_Hash,
                    response_code = py.Response_Code,
                    merchant = py.Merchant,
                    card = py.Card,
                    token = py.Token,
                    posted_date = DateTime.Now,
                    facility_reg_sno = py.Facilit_Reg_Sno,
                    facility_name = py.Facility_Name,
                    medium_sno = py.Medium_Sno,
                    medium_type = py.Medium_Type,
                    class_sno = py.Class_Sno,
                    class_name = py.Class_Name,
                    section_sno = py.Section_Sno,
                    section_name = py.Section_Name,
                    academic_sno = py.AcademicSno,
                    acad_year = py.Acad_Year,
                    remarks = py.Remarks,
                    mod_amount = py.ModifiedAmount,
                    postedby = py.Posted_By

                };
                context.payment_details_reverse.AddObject(pt);
                context.SaveChanges();
            }
        }
        public List<Payment> GetPaymentI()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.payment_details
                                where c.status == "Pending" || c.status == "Return" 

                                select new Payment
                                {
                                    SNO = c.sno,
                                    Payment_SNo = c.payment_sno,
                                    Payment_Date = c.payment_date,
                                    Fee_Data_Sno = c.fee_data_sno,
                                    Payer_Name = c.payer_name,
                                    Admission_No = c.admission_no,
                                    Payment_Type = c.payment_type,
                                    Term_Sno = (long)c.term_sno,
                                    Term_Type = c.term_type,
                                    Fee_Type = c.fee_type,
                                    Receipt_No = c.receipt_no,
                                    Requested_Amount = (long)c.requested_amount,
                                    PaidAmount = (long)c.paid_amount,
                                    Section_Name = c.section_name,
                                    Class_Name = c.class_name,
                                    Acad_Year = c.acad_year,
                                    Facilit_Reg_Sno = (long)c.facility_reg_sno,
                                    Facility_Name = c.facility_name,
                                    Trans_Channel = c.trans_channel,
                                    Amount_Type = c.amount_type,
                                    Medium_Type = c.medium_type
                                }).OrderBy(a => a.SNO).ToList();
                if (edetails != null && edetails.Count > 0)
                    return edetails;
                else
                    return null;
            }
        }*/
        public Payment GetSNOI(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.payment_details
                                where c.sno == sno

                                select new Payment
                                {
                                    SNO = c.sno,
                                    Payment_SNo = c.payment_sno,
                                    Payment_Date = c.payment_date,
                                    Payment_Time = c.payment_time,
                                    Trans_Channel = c.trans_channel,
                                    Payer_Name = c.payer_name,
                                    Receipt_No = c.receipt_no,
                                    Payment_Trans_No = c.pay_trans_no,
                                    Requested_Amount = (long)c.requested_amount,
                                    PaidAmount = (long)c.paid_amount,
                                    Payer_Id = c.payer_id,
                                    Institution_ID = c.institution_id,
                                    Payment_Type = c.payment_type,
                                    Amount_Type = c.amount_type,
                                    Currency_Code = c.currency_code,
                                    Token = c.token,
                                    Chksum = c.chksum,
                                    Status = c.status,
                                    Posted_By = c.posted_by,
                                    Payment_Desc = c.payment_desc,
                                    Control_No = c.control_no,
                                    Comp_Mas_Sno = (long)c.comp_mas_sno,
                                    //Company_Name = c.company_name,
                                    Cust_Mas_Sno = (long)c.cust_mas_sno,
                                    //Customer_Name = c.customer_name,
                                    Invoice_Sno = c.invoice_sno

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void Update_App(Payment reg)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.payment_details
                                         where u.sno == reg.SNO
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.response_code = "0";
                    UpdateContactInfo.status = "Passed";
                    UpdateContactInfo.secure_hash = reg.Approved_By;
                    context.SaveChanges();
                }
            }
        }
        public void Update_Ret(Payment reg)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.payment_details
                                         where u.sno == reg.SNO
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.status = "Return";
                    UpdateContactInfo.secure_hash = reg.Approved_By;
                    context.SaveChanges();
                }
            }
        }
        public void Update_Up(Payment reg)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.payment_details
                                         where u.sno == reg.SNO
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.payment_date = reg.Payment_Date;
                    UpdateContactInfo.payment_time = reg.Payment_Time;
                    UpdateContactInfo.trans_channel = reg.Trans_Channel;
                    UpdateContactInfo.payment_desc = reg.Payment_Desc;
                    UpdateContactInfo.payer_id = reg.Payer_Id;
                    UpdateContactInfo.institution_id = reg.Institution_ID;
                    UpdateContactInfo.status = reg.Status;
                    UpdateContactInfo.token = reg.Token;
                    UpdateContactInfo.chksum = reg.Chksum;
                    UpdateContactInfo.paid_amount = reg.Amount;
                    UpdateContactInfo.posted_by = reg.Posted_By;
                    UpdateContactInfo.payer_name = reg.Payer_Name;
                    UpdateContactInfo.amount_type = reg.Amount_Type;
                    UpdateContactInfo.amount30 = reg.Amount30;
                    UpdateContactInfo.amount70 = reg.Amount70;
                    context.SaveChanges();
                }
            }
        }
        public bool ValidateAppno(string appno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from u in context.payment_details
                                  where u.payment_sno == appno
                                  select u);

                if (validation.Count() > 0)
                    return true;
                else
                    return false;

            }


        }
        public bool ValidateRefno(string appno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from u in context.payment_details
                                  where u.receipt_no == appno
                                  select u);

                if (validation.Count() > 0)
                    return true;
                else
                    return false;

            }


        }
        public bool ValidateINVno_Status(string invno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from u in context.payment_details
                                  where u.control_no == invno && u.status == "Passed"
                                  select u);

                if (validation.Count() > 0)
                    return true;
                else
                    return false;

            }


        }
        
        public bool Validate_Invoice(string invno, long sid)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from u in context.payment_details
                                  where u.comp_mas_sno == sid && u.control_no == invno && u.status == "Passed"
                                  select u);

                if (validation.Count() > 0)
                    return true;
                else
                    return false;

            }

        }
        
        
        public List<Payment> GetPayment_Paid(string fdata)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.payment_details
                                where (c.control_no == fdata) && (c.status == "Passed")

                                select new Payment
                                {
                                    Requested_Amount = (long)c.requested_amount,
                                    Amount = (long)c.paid_amount
                                }).ToList();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public List<Payment> GetPayment_Paid1(string fdata)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.payment_details
                                where (c.control_no == fdata) && (c.status == "Passed")

                                select new Payment
                                {
                                    Requested_Amount = (long)c.requested_amount,
                                    Amount = (long)c.paid_amount
                                }).ToList();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public List<Payment> GetControl_Dash()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.payment_details
                                where (c.status == "Passed")

                                select new Payment
                                {
                                    Control_No = c.control_no
                                }).Distinct().ToList();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public List<Payment> GetControl_Dash_C(long cno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.payment_details
                                where (c.status == "Passed" && c.comp_mas_sno == cno)

                                select new Payment
                                {
                                    Control_No = c.control_no
                                }).Distinct().ToList();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }

        public List<Payment> GetPayment_Dash(string fdata)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.payment_details
                                where (c.control_no == fdata) && (c.status == "Passed")

                                select new Payment
                                {
                                    Control_No = c.control_no,
                                    Requested_Amount = (long)c.requested_amount,
                                    Amount = (long)c.paid_amount
                                }).ToList();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }

        public void UpdateR_Amount(Payment reg)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.payment_details
                                         where u.receipt_no == reg.Receipt_No
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.paid_amount = reg.ModifiedAmount;
                    context.SaveChanges();
                }
            }
        }
        public Payment GetReceipt(string Rec)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.payment_details
                                where c.receipt_no == Rec

                                select new Payment
                                {
                                    Payment_SNo = c.payment_sno,
                                    Payment_Date = c.payment_date,
                                    Payment_Time = c.payment_time,
                                    Trans_Channel = c.trans_channel,
                                    Payer_Name = c.payer_name,
                                    Receipt_No = c.receipt_no,
                                    Payment_Trans_No = c.pay_trans_no,
                                    Requested_Amount = (long)c.requested_amount,
                                    PaidAmount = (long)c.paid_amount,
                                    Institution_ID = c.institution_id,
                                    Payment_Type = c.payment_type,
                                    Amount_Type = c.amount_type,
                                    Currency_Code = c.currency_code,
                                    Control_No = c.control_no,
                                    Comp_Mas_Sno = (long)c.comp_mas_sno,
                                    //Company_Name = c.company_name,
                                    Cust_Mas_Sno = (long)c.cust_mas_sno,
                                    //Customer_Name = c.customer_name,
                                    Invoice_Sno = c.invoice_sno
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public Payment GetSNO(string Rec)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.payment_details
                                where c.payment_sno == Rec

                                select new Payment
                                {
                                    SNO = c.sno,
                                    Payment_SNo = c.payment_sno,
                                    Payment_Date = c.payment_date,
                                    Payment_Time = c.payment_time,
                                    Trans_Channel = c.trans_channel,
                                    Payer_Name = c.payer_name,
                                    Receipt_No = c.receipt_no,
                                    Payment_Trans_No = c.pay_trans_no,
                                    Requested_Amount = (long)c.requested_amount,
                                    PaidAmount = (long)c.paid_amount,
                                    Institution_ID = c.institution_id,
                                    Payment_Type = c.payment_type,
                                    Amount_Type = c.amount_type,
                                    Currency_Code = c.currency_code,
                                    Control_No = c.control_no,
                                    Comp_Mas_Sno = (long)c.comp_mas_sno,
                                    //Company_Name = c.company_name,
                                    Cust_Mas_Sno = (long)c.cust_mas_sno,
                                    //Customer_Name = c.customer_name,
                                    Invoice_Sno = c.invoice_sno

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public List<Payment> GetPaymentReport(long Comp, string inv, string stdate, string enddate, long cust)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                DateTime? fdate = null;
                if (!string.IsNullOrEmpty(stdate)) fdate = DateTime.Parse(stdate);
                DateTime? tdate = null;
                if (!string.IsNullOrEmpty(enddate)) tdate = DateTime.Parse(enddate);

                List<Payment> payments = (from c in context.payment_details
                                join det in context.invoice_master on c.invoice_sno equals det.invoice_no
                                join cus in context.customer_master on c.cust_mas_sno equals cus.cust_mas_sno
                                where (cust == 0 ? true : c.cust_mas_sno == cust)
                                && (!fdate.HasValue || c.posted_date >= fdate)
                                && (!tdate.HasValue || c.posted_date >= tdate)
                                && (Comp == 0 ? true : c.comp_mas_sno == Comp)
                                && (!string.IsNullOrEmpty(inv) ? det.invoice_no == inv : true)

                                select new Payment
                                {
                                    SNO = c.sno,
                                    Payment_SNo = c.payment_sno,
                                    Payment_Date = c.payment_date,
                                    Payment_Time = c.payment_time,
                                    Trans_Channel = c.trans_channel,
                                    Payer_Name = c.payer_name,
                                    Receipt_No = c.receipt_no,
                                    Payment_Trans_No = c.pay_trans_no,
                                    Requested_Amount = (long)c.requested_amount,
                                    PaidAmount = (long)c.paid_amount,
                                    Institution_ID = c.institution_id,
                                    Payment_Type = c.payment_type,
                                    Amount_Type = c.amount_type,
                                    Currency_Code = c.currency_code,
                                    Control_No = c.control_no,
                                    Comp_Mas_Sno = (long)c.comp_mas_sno,
                                    //Company_Name = c.company_name,
                                    Cust_Mas_Sno = (long)c.cust_mas_sno,
                                    Customer_Name = cus.customer_name,
                                    Invoice_Sno = c.invoice_sno,
                                    Audit_Date = (DateTime)c.posted_date

                                }).ToList();
                return payments != null ? payments : new List<Payment>();
                /*DateTime fdate = DateTime.Parse(stdate);
                DateTime tdate = DateTime.Parse(enddate);
                if (string.IsNullOrEmpty(inv))
                {
                    var edetails = (from c in context.payment_details
                                    join det in context.invoice_master on c.invoice_sno equals det.invoice_no
                                    join cus in context.customer_master on c.cust_mas_sno equals cus.cust_mas_sno
                                    where (cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                   && (c.posted_date >= fdate && c.posted_date <= tdate)
                                    && (c.comp_mas_sno == Comp)

                                    select new Payment
                                    {
                                        SNO = c.sno,
                                        Payment_SNo = c.payment_sno,
                                        Payment_Date = c.payment_date,
                                        Payment_Time = c.payment_time,
                                        Trans_Channel = c.trans_channel,
                                        Payer_Name = c.payer_name,
                                        Receipt_No = c.receipt_no,
                                        Payment_Trans_No = c.pay_trans_no,
                                        Requested_Amount = (long)c.requested_amount,
                                        PaidAmount = (long)c.paid_amount,
                                        Institution_ID = c.institution_id,
                                        Payment_Type = c.payment_type,
                                        Amount_Type = c.amount_type,
                                        Currency_Code = c.currency_code,
                                        Control_No = c.control_no,
                                        Comp_Mas_Sno = (long)c.comp_mas_sno,
                                        //Company_Name = c.company_name,
                                        Cust_Mas_Sno = (long)c.cust_mas_sno,
                                        Customer_Name = cus.customer_name,
                                        Invoice_Sno = c.invoice_sno,
                                        Audit_Date = (DateTime)c.posted_date

                                    }).ToList();
                    if (edetails != null)
                        return edetails;
                    else
                        return null;
                }
                else
                {
                    var edetails = (from c in context.payment_details
                                    join det in context.invoice_master on c.invoice_sno equals det.invoice_no
                                    join cus in context.customer_master on c.cust_mas_sno equals cus.cust_mas_sno
                                    where (cust == 0 ? c.cust_mas_sno == c.cust_mas_sno : c.cust_mas_sno == cust)
                                   && (c.posted_date >= fdate && c.posted_date <= tdate)
                                    && (c.comp_mas_sno == Comp) && (c.control_no == inv)

                                    select new Payment
                                    {
                                        SNO = c.sno,
                                        Payment_SNo = c.payment_sno,
                                        Payment_Date = c.payment_date,
                                        Payment_Time = c.payment_time,
                                        Trans_Channel = c.trans_channel,
                                        Payer_Name = c.payer_name,
                                        Receipt_No = c.receipt_no,
                                        Payment_Trans_No = c.pay_trans_no,
                                        Requested_Amount = (long)c.requested_amount,
                                        PaidAmount = (long)c.paid_amount,
                                        Institution_ID = c.institution_id,
                                        Payment_Type = c.payment_type,
                                        Amount_Type = c.amount_type,
                                        Currency_Code = c.currency_code,
                                        Control_No = c.control_no,
                                        Comp_Mas_Sno = (long)c.comp_mas_sno,
                                        //Company_Name = c.company_name,
                                        Cust_Mas_Sno = (long)c.cust_mas_sno,
                                        Customer_Name = cus.customer_name,
                                        Invoice_Sno = c.invoice_sno,
                                        Audit_Date = (DateTime)c.posted_date

                                    }).ToList();
                    if (edetails != null)
                        return edetails;
                    else
                        return null;
                }*/
            }
        }

        public Payment GetControl(string control)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.payment_details
                                where c.control_no == control && c.status == "Passed"
                                select new Payment
                                {
                                    Payment_SNo = c.payment_sno,
                                    Payment_Date = c.payment_date,
                                    Payment_Time = c.payment_time,
                                    Trans_Channel = c.trans_channel,
                                    Payer_Name = c.payer_name,
                                    Receipt_No = c.receipt_no,
                                    Payment_Desc = c.payment_desc,
                                    Payer_Id = c.payer_id,
                                    Payment_Trans_No = c.pay_trans_no,
                                    Requested_Amount = (long)c.requested_amount,
                                    PaidAmount = (long)c.paid_amount,
                                    Institution_ID = c.institution_id,
                                    Payment_Type = c.payment_type,
                                    Amount_Type = c.amount_type,
                                    Currency_Code = c.currency_code,
                                    Token = c.token,
                                    Chksum = c.chksum,
                                    Control_No = c.control_no,
                                    Comp_Mas_Sno = (long)c.comp_mas_sno,
                                    Cust_Mas_Sno = (long)c.cust_mas_sno,
                                    Invoice_Sno = c.invoice_sno


                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        


        public long GetLastInsertedId()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                return (from x in context.payment_details
                        select x).OrderByDescending(x => x.sno).First().sno;
            }
        }
        public string GetLastInsertedReceipt()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                return (from x in context.payment_details
                        select x).OrderByDescending(x => x.sno).First().payment_sno;
            }
        }
        #endregion  Methods
    }
}

