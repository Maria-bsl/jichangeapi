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
                    posted_date = Tra.posted_date

                };
                context.registration_ack.Add(obj);
                context.SaveChanges();
                return obj.reg_ack_sno;
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
                                     address = c.address
                                     

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

    }
}
