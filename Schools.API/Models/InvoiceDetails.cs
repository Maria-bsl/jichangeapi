using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPS.API.Models
{
    public class InvoiceDetails
    {
        #region Properties
        public string paymentReference { get; set; }
        public string payerName { get; set; }
        public string paymentDesc { get; set; }
        public decimal amount { get; set; }
        public string amountType { get; set; }
        public string currency { get; set; }
        public string paymentType { get; set; }
        public string payerID { get; set; }
        

       /*ublic long Comments_ID { get; set; }
        public long Emp_Detail_ID { get; set; }
        public long Transporter_ID { get; set; }
        public long Status_Code { get; set; }
        public long Application_No_Service { get; set; }
        public string Comments { get; set; }
        public DateTime Comments_Date { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Status { get; set; }
        public int Status_ID { get; set; }
        public string Comments_From { get; set; }
        public string Comments_Given { get; set; }

        public long SNo { get; set; }
        public long Application_No_Service { get; set; }
        public long Status_ID { get; set; }
        public string Status { get; set; }
        public string Comments_From { get; set; }
        public string Comments_Forward { get; set; }
        public string Comments_Return { get; set; }
        public string Flag { get; set; }
        public DateTime Update_Date { get; set; }

        public long Application_No_Service { get; set; }
        public long Permit_No { get; set; }
        public string Permit_Application_No { get; set; }
        public string Transporter_Name { get; set; }
        public DateTime Permit_Date { get; set; }
        public DateTime Permit_Fee_Date { get; set; }
        public string Cargo_Type { get; set; }
        public string Truck_Type { get; set; }
        public string Truck_No { get; set; }
        //public string Designation { get; set; }
        public string Trailer_No { get; set; }
        public string Axel_Configuration { get; set; }
        public decimal Total_Width { get; set; }
        public decimal Total_Height { get; set; }
        public decimal Total_Length { get; set; }
        public int Total_Weight { get; set; }
        public string Cargo_Details { get; set; }
        public string Route_From { get; set; }
        public string Route_To { get; set; }
        public string Route_Via { get; set; }
        public string Tanesco_Required { get; set; }
        public string Weight_Ticket { get; set; }
        public int No_Of_Escor_Veh { get; set; }
        public string Escort_Veh_Type1 { get; set; }
        public string Escort_Veh_No1 { get; set; }
        public string Escort_Veh_Type2 { get; set; }
        public string Escort_Veh_No2 { get; set; }
        public string Letter_Trafic_Police { get; set; }
        public string Survey_Route_Report { get; set; }
        public string Advertisement { get; set; }
        public string Cargo_Drawings { get; set; }
        //public string Status { get; set; }
        public string Posted_By { get; set; }
        public DateTime Posted_Date { get; set; }
        public long Permit_Fee { get; set; }
        public long Surcharge_Fee { get; set; }
        public string Examined { get; set; }
        public string Authorized { get; set; }
        //public long AuditBy { get; set; }
        public long Invoice_No { get; set; }*/
        //public long Receipt_No_Service { get; set; }

        #endregion Properties
    }
}