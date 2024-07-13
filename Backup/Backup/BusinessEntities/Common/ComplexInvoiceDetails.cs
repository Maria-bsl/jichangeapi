using BL.BIZINVOICING.BusinessEntities.Masters;
using DaL.BIZINVOICING.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BIZINVOICING.BusinessEntities.Common
{
   public class ComplexInvoiceDetails
    {
        public CustomerMaster Customerdata { get; set; }
        public CompanyBankMaster CompanyData { get; set; }

        public INVOICE InvoiceData { get; set; }



    }


    public class InvoicePDfData
    {
        //Bank Data
        public int? grand_count { get; set; }
        public int? daily_count { get; set; }
        public string approval_status { get; set; }
        public DateTime approval_date { get; set; }
        public long CompSno { set; get; }
        public string CompName { set; get; }
        public string CompPostBox { set; get; }
        public string CompAddress { set; get; }

        public string CompContactPerson { set; get; }
        public long RegId { set; get; }
        public long DistSno { set; get; }
        public long WardSno { set; get; }
        public string RegName { set; get; }
        public string DistName { set; get; }
        public string WardName { set; get; }
        public string TinNo { set; get; }
        public string CompVatNo { set; get; }
        public string DirectorName { set; get; }
        public string CompEmail { set; get; }
        public string CompTelNo { set; get; }
        public string CompFaxNo { set; get; }
        public string CompMobNo { set; get; }
        public byte[] CompLogo { set; get; }
        public byte[] DirectorSig { set; get; }
        public string Postedby { set; get; }
        public DateTime Posteddate { set; get; }
        public long BankSno { set; get; }
        public long CompanySno { set; get; }
        public string BankName { set; get; }
        public string BankBranch { set; get; }
        public string AccountNo { set; get; }
        public string Swiftcode { set; get; }
        //customer Data
        public long Cust_Sno { set; get; }
        public string Cust_Name { set; get; }
        public string CustomerPostboxNo { get; set; }
        public string CustAddress { get; set; }
        public long Region_SNO { get; set; }
        public string Region_Name { get; set; }
        public long CustDistSno { get; set; }
        public string CustDistName { get; set; }
        public long CustWardSno { get; set; }
        public string CustWardName { get; set; }
        public string CustTinNo { get; set; }
        public string CustVatNo { get; set; }
        public string ConPerson { get; set; }
        public string CustEmail { get; set; }
        public string CustPhone { set; get; }

        public string Posted_by { get; set; }
        public DateTime Posted_Date { get; set; }

        // Invoice Data
        public long Inv_Mas_Sno { get; set; }
        public long Inv_Det_Sno { get; set; }
        public DateTime? Invoice_Date { get; set; }
        public String Invoice_No { get; set; }
        public long Chus_Mas_No { get; set; }
        public String Chus_Name { get; set; }
        public long Com_Mas_Sno { get; set; }
        public String Inv_Remarks { get; set; }
        public String Remarks { get; set; }
        public string Currency_Code { get; set; }
        public decimal Total_Without_Vt { get; set; }
        public decimal Total_Vt { get; set; }
        public decimal Total { get; set; }
        public decimal Item_Qty { get; set; }
        public decimal Item_Unit_Price { get; set; }
        public decimal Item_Total_Amount { get; set; }
        public decimal Vat_Percentage { get; set; }
        public decimal Vat_Amount { get; set; }
        public decimal Item_Without_vat { get; set; }
        public string Item_Description { get; set; }
        public string AuditBy { get; set; }
        public DateTime Audit_Date { get; set; }
        public string warrenty { get; set; }
        public string goods_status { get; set; }
        public string delivery_status { get; set; }
        public string AmountWords { get; set; }
        public String Customer_ID_Type { get; set; }
        public String Customer_ID_No { get; set; }

        public List<INVOICE> InvoiceItemlist { get; set; }

    }
}
