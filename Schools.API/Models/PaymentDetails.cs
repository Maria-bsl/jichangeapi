using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPS.API.Models
{
    public class PaymentDetails
    {
        #region Properties
        public long SNO { get; set; }
        public long Comp_Mas_SNO { get; set; }
        public long Cust_Mas_SNO { get; set; }
        public decimal Item_Total_Amount { get; set; }
        public string Control_No { get; set; }
        public string Payment_SNo { get; set; }
        public DateTime Payment_Date { get; set; }
        public string Payment_Type { get; set; }
        public string transactionRef { get; set; }
        public string currency { get; set; }
        public string Receipt_No { get; set; }
        public string Batch_No { get; set; }
        public string Authorize_Id { get; set; }
        public string Secure_Hash { get; set; }
        public string Response_Code { get; set; }
        public string Merchant { get; set; }
        public string Message { get; set; }
        public string Card { get; set; }
        public string token { get; set; }
        public string Status { get; set; }
        public string Response { get; set; }
        public DateTime Audit_Date { get; set; }
        public string AuditAction { get; set; }
        public string AuditDone { get; set; }
        public long AuditID { get; set; }
        public long PaidAmount { get; set; }
        public decimal BOT { get; set; }



        //public long Application_No_Service { get; set; }
        public string Term_Type { get; set; }
        public long Fee_Sno { get; set; }
        public long Term_Sno { get; set; }
        public DateTime Payment_Time { get; set; }
        public string payerName { get; set; }
        public string paymentReference { get; set; }
        public string amountType { get; set; }
        public string paymentType { get; set; }
        public string Fee_Data_Sno { get; set; }
        public string Currency_Code { get; set; }
        public long Requested_Amount { get; set; }
        public string paymentDesc { get; set; }
        public string payerID { get; set; }
        public string transactionChannel { get; set; }
        public string PR_WB_ID { get; set; }
        //public string Payment_Type { get; set; }
        public string institutionID { get; set; }
        public string Admission_No { get; set; }
        public string checksum { get; set; }
        public string Charge_Type { get; set; }
        public string Fee_Type { get; set; }
        public string Currency_Type { get; set; }
        public string Posted_By { get; set; }
        public DateTime Posted_Date { get; set; }
        public DateTime transactionDate { get; set; }
        public long amount { get; set; }
        public long Surcharge_Fee { get; set; }
        public string Examined { get; set; }
        public string Authorized { get; set; }
        //public long AuditBy { get; set; }
        public long Receipt_No_Service { get; set; }

        public string Error_Text { get; set; }

        public long Sno { get; set; }
        public DateTime Upload_Date { get; set; }
        public long Facilit_Reg_Sno { get; set; }
        public string Facility_Name { get; set; }
        public string Student_Full_Name { get; set; }
        public string Parent_Name { get; set; }
        public string Parent_Mobile_No { get; set; }
        public string Parent_Email_Address { get; set; }

        public long UserId { get; set; }

       

        #endregion Properties
    }
}