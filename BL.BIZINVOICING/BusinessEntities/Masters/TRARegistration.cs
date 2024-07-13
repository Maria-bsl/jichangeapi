using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BIZINVOICING.BusinessEntities.Common;
using DaL.BIZINVOICING.EDMX;


namespace BL.BIZINVOICING.BusinessEntities.Masters
{
   public class TRARegistration
    {

         public int? ack_code    { get; set; }
       public string ack_message  { get; set; }
       public string regid        { get; set; }
       public string serial       { get; set; }
       public string uin          { get; set; }
       public int tin_no       { get; set; }
       public string vrn          { get; set; }
       public string mobile_no    { get; set; }
       public string street       { get; set; }
       public string city         { get; set; }
       public string address      { get; set; }
       public string country      { get; set; }
       public string company_name { get; set; }
       public string receiptcode  { get; set; }
       public string region       { get; set; }
       public long gc             { get; set; }
       public string taxoffice    { get; set; }
       public string username     { get; set; }
       public string password     { get; set; }
       public string tokenpath    { get; set; }
       public string posted_by    { get; set; }
       public DateTime posted_date { get; set; }
        public string reg_ack_det_sno { get; set; }
        public long reg_ack_sno { get; set; }
        public string tax_code { get; set; }
        public int tax_percentage { get; set; }
        public long Comp_Mas_Sno { get; set; }
        public string Pfx_Pass { get; set; }
        public string Cert_Serial { get; set; }
        public string Certi_Path { get; set; }
        public string Inv_Path { get; set; }
        public string Log_Path { get; set; }


        public long AddTRARegistration(TRARegistration Tra)
        {

            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                registration_ack obj = new registration_ack()
                {
                    ack_code= Tra.ack_code,
                    ack_message = Tra.ack_message,
                    regid = Tra.regid,
                    serial = Tra.serial,
                    uin = Tra.uin,
                    tin_no = Tra.tin_no,
                    vrn = Tra.vrn,
                    mobile_no = Tra.mobile_no,
                    street = Tra.street,
                    city = Tra.city,
                    address = Tra.address,
                    country = Tra.country,
                    company_name = Tra.company_name,
                    receiptcode = Tra.receiptcode,
                    region = Tra.region,
                    gc = Tra.gc,
                    taxoffice = Tra.taxoffice,
                    username = Tra.username,
                    password = Tra.password,
                    tokenpath = Tra.tokenpath,
                    posted_by = Tra.posted_by,
                    posted_date = Tra.posted_date,
                    comp_mas_sno = Tra.Comp_Mas_Sno,
                    pfx_password = Tra.Pfx_Pass,
                    cert_serial = Tra.Cert_Serial,
                    certificate_path = Tra.Certi_Path,
                    invoice_path = Tra.Inv_Path,
                    log_path = Tra.Log_Path

                };
                context.registration_ack.Add(obj);
                context.SaveChanges();
                return obj.reg_ack_sno;
            }
        }
        public void UpdateRegistration(TRARegistration dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.registration_ack
                                         where u.reg_ack_sno == dep.reg_ack_sno
                                         select u).FirstOrDefault();
                
                if (UpdateContactInfo != null)
                {

                    UpdateContactInfo.ack_code = dep.ack_code;
                    UpdateContactInfo.ack_message = dep.ack_message;
                    UpdateContactInfo.regid = dep.regid;
                    UpdateContactInfo.serial = dep.serial;
                    UpdateContactInfo.uin = dep.uin;
                    UpdateContactInfo.tin_no = dep.tin_no;
                    UpdateContactInfo.vrn = dep.vrn;
                    UpdateContactInfo.mobile_no = dep.mobile_no;
                    UpdateContactInfo.street = dep.street;
                    UpdateContactInfo.city = dep.city;
                    UpdateContactInfo.address = dep.address;
                    UpdateContactInfo.country = dep.country;
                    UpdateContactInfo.company_name = dep.company_name;
                    UpdateContactInfo.receiptcode = dep.receiptcode;
                    UpdateContactInfo.region = dep.region;
                    UpdateContactInfo.gc = dep.gc;
                    UpdateContactInfo.taxoffice = dep.taxoffice;
                    UpdateContactInfo.username = dep.username;
                    UpdateContactInfo.password = dep.password;
                    UpdateContactInfo.tokenpath = dep.tokenpath;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    UpdateContactInfo.comp_mas_sno = dep.Comp_Mas_Sno;
                    UpdateContactInfo.pfx_password = dep.Pfx_Pass;
                    UpdateContactInfo.cert_serial = dep.Cert_Serial;
                    UpdateContactInfo.certificate_path = dep.Certi_Path;
                    UpdateContactInfo.invoice_path = dep.Inv_Path;
                    UpdateContactInfo.log_path = dep.Log_Path;
                    context.SaveChanges();
                }
            }

        }
        public void DeleteRegDet(TRARegistration bt)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {

                context.registration_ack_details.RemoveRange(context.registration_ack_details.Where(c => c.reg_ack_sno == bt.reg_ack_sno));
                context.SaveChanges();
            }
        }
        public void AddregistrationackDetails(TRARegistration sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                registration_ack_details pc = new registration_ack_details()
                {

    
                    reg_ack_sno =sc.reg_ack_sno,
                    tax_code =sc.tax_code,
                    tax_percentage =sc.tax_percentage

                };
                context.registration_ack_details.Add(pc);
                context.SaveChanges();
            }

        }


        public List<TRARegistration> GetTRAData(int TinNo)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.registration_ack.Where(c => c.tin_no == TinNo)

                                select new TRARegistration
                                {
                                     ack_code=c.ack_code,
                                     username=c.username,
                                     password=c.password,
                                     regid=c.regid,
                                     serial=c.serial,
                                     receiptcode=c.receiptcode,
                                     vrn = c.vrn,
                                     uin = c.uin,
                                     company_name = c.company_name,
                                     street = c.street,
                                     mobile_no = c.mobile_no,
                                     country = c.country,
                                     address = c.address,
                                     posted_date = (DateTime)c.posted_date

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public TRARegistration GetData(int TinNo)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.registration_ack.Where(c => c.tin_no == TinNo)

                                select new TRARegistration
                                {
                                    reg_ack_sno = c.reg_ack_sno,
                                    ack_code = c.ack_code,
                                    username = c.username,
                                    password = c.password,
                                    regid = c.regid,
                                    serial = c.serial,
                                    receiptcode = c.receiptcode,
                                    vrn = c.vrn,
                                    uin = c.uin,
                                    company_name = c.company_name,
                                    street = c.street,
                                    mobile_no = c.mobile_no,
                                    country = c.country,
                                    address = c.address,
                                    taxoffice = c.taxoffice,
                                    tin_no = (int)c.tin_no,
                                    posted_date = (DateTime)c.posted_date

                                }).OrderByDescending(a => a.reg_ack_sno).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }
        public bool ValidateReg(int tin)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.registration_ack
                                  where (c.tin_no == tin)
                                  select c);
                if (validation.Count() > 0)

                    return true;
                else
                    return false;
            }
        }
        public TRARegistration GetDataU()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.registration_ack
                                select new TRARegistration
                                {
                                    reg_ack_sno = c.reg_ack_sno,
                                    ack_code = c.ack_code,
                                    username = c.username,
                                    password = c.password,
                                    regid = c.regid,
                                    serial = c.serial,
                                    receiptcode = c.receiptcode,
                                    vrn = c.vrn,
                                    uin = c.uin,
                                    company_name = c.company_name,
                                    street = c.street,
                                    mobile_no = c.mobile_no,
                                    country = c.country,
                                    address = c.address,
                                    taxoffice = c.taxoffice,
                                    tin_no = (int)c.tin_no,
                                    posted_date = (DateTime)c.posted_date

                                }).OrderByDescending(a => a.reg_ack_sno).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }

    }
}
