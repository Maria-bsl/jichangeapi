using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class LangcoController : Controller
    {
        CompanyUsers ch = new CompanyUsers();
        langcompany lang = new langcompany();
        // GET: LangComp
        //public ActionResult Index()
        //{
        //    return View();
        //}
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            try
            {
                if (System.Web.HttpContext.Current.Session["CompID"] == null)
                {
                    Session["CompID"] = 0;
                }

                if (System.Web.HttpContext.Current.Session["UserID"] == null)
                {
                    Session["UserID"] = 0;
                }

                if (System.Web.HttpContext.Current.Session["admin1"] == null)
                {
                    Session["admin1"] = 0;
                }

                //String[] list = new String[28] { "Marital Status", "Relationship", "Audit Trails", "Language", "Access Rights", "Program", "Institution Details", "Member Details", "Invoice Report", "Payment Details", "Contribution Details", "Member Registration", "Believer Upload", "Targeted Member", "Age Groups", "Contribution Frequency", "Membership Type", "Role", "Offering Type", "Offering Account", "Institution Users", "Religious Association", "Exchange Rate", "Zblock", "Cash Collections", "Receipt", "Notifications", "Offering" };
                //Array.Sort(list);
                //List<String> Calc = new List<String>();
                //for (int i = 0; i < list.Count(); i++)
                //{
                //    long result = act.checkact(long.Parse(Session["CompID"].ToString()), long.Parse(Session["UserID"].ToString()), list[i]);
                //    Calc.Add(Convert.ToString(result));
                //}
                //ViewBag.menulist = Calc;
                //String[] menuheader = new String[4] { "Setup", "Believers", "Reports", "Cash Collections" };
                //Array.Sort(menuheader);
                //List<String> Calc1 = new List<String>();
                //for (int i = 0; i < menuheader.Count(); i++)
                //{
                //    //long result1 = act.Validatescreen(long.Parse(Session["CompID"].ToString()), long.Parse(Session["UserID"].ToString()), menuheader[i]);
                //    Calc1.Add(Convert.ToString(result1));
                //}
                //ViewBag.menuheader = Calc1;


                var drt = ch.validatelangInst(long.Parse(Session["UserID"].ToString()), long.Parse(Session["CompID"].ToString()), Session["admin1"].ToString());
                if (drt != null)
                {


                    if (drt.Loc_Name == "Swahili")
                    {
                        //button
                        var gList = lang.GetLangU(long.Parse(Session["CompID"].ToString()));
                        if (gList != null)
                        {

                            var bb = lang.EditColumn("Grid", "Sno");
                            ViewData["Sno"] = bb.Loc_Oth == null ? "Sno" : bb.Loc_Oth;
                            bb = lang.EditColumn("Grid", "Actions");
                            ViewData["Actions"] = bb.Loc_Oth == null ? "Actions" : bb.Loc_Oth;
                            bb = lang.EditColumn("Grid", "Status");
                            ViewData["Status"] = bb.Loc_Oth == null ? "Status" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Comment", "Date");
                            //ViewData["date"] = bb.Loc_Oth == null ? "Date" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Comment", "Comment");
                            //ViewData["cmt"] = bb.Loc_Oth == null ? "Comment" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Comment", "Comment By");
                            //ViewData["cby"] = bb.Loc_Oth == null ? "Comment By" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Comment", "Change Password");
                            //ViewData["change"] = bb.Loc_Oth == null ? "Change Password" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Comment", "Current Password");
                            //ViewData["ccpp"] = bb.Loc_Oth == null ? "Current Password" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Comment", "New Password");
                            //ViewData["np"] = bb.Loc_Oth == null ? "New Password" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Comment", "Confirm Password");
                            //ViewData["ccp"] = bb.Loc_Oth == null ? "Confirm Password" : bb.Loc_Oth;

                            //bb = lang.EditColumn("Audit Trails", "Audit Trails");
                            //ViewData["Atra"] = bb.Loc_Oth == null ? "Audit Trails" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Audit Trails", "Select Page");
                            //ViewData["Spag"] = bb.Loc_Oth == null ? "Select Page" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Audit Trails", "Column Name");
                            //ViewData["Coln"] = bb.Loc_Oth == null ? "Column Name" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Audit Trails", "Old Value");
                            //ViewData["Oval"] = bb.Loc_Oth == null ? "Old Value" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Audit Trails", "New Value");
                            //ViewData["Nval"] = bb.Loc_Oth == null ? "New Value" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Audit Trails", "Posted/Modified By");
                            //ViewData["PoMb"] = bb.Loc_Oth == null ? "Posted/Modified By" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Audit Trails", "Audit Date");
                            //ViewData["Adat"] = bb.Loc_Oth == null ? "Audit Date" : bb.Loc_Oth;
                            bb = lang.EditColumn("Button", "Submit");
                            ViewData["Sub"] = bb.Loc_Oth == null ? "Submit" : bb.Loc_Oth;
                            bb = lang.EditColumn("Button", "PDF");
                            ViewData["PDF"] = bb.Loc_Oth == null ? "PDF" : bb.Loc_Oth;
                            bb = lang.EditColumn("Button", "Excel");
                            ViewData["Excel"] = bb.Loc_Oth == null ? "Excel" : bb.Loc_Oth;
                            //bb = lang.EditColumn("Button", "Unblock");
                            //ViewData["ublo"] = bb.Loc_Oth == null ? "Unblock" : bb.Loc_Oth;
                            //bb= lang.EditColumn("Button", "Block");
                            //ViewData["blo"] =bb.Loc_Oth == null ? "Block" : bb.Loc_Oth;

                            //button

                            var bb1 = lang.EditColumn("Button", "Close");
                            ViewData["Close"] = bb1.Loc_Oth == null ? "Close" : bb1.Loc_Oth;
                            bb = lang.EditColumn("Button", "Save");
                            ViewData["Save"] = bb.Loc_Oth == null ? "Save" : bb.Loc_Oth;
                            bb = lang.EditColumn("Button", "Create");
                            ViewData["Create"] = bb.Loc_Oth == null ? "Create" : bb.Loc_Oth;
                            bb = lang.EditColumn("Button", "Update");
                            ViewData["Update"] = bb.Loc_Oth == null ? "Update" : bb.Loc_Oth;
                            bb = lang.EditColumn("Button", "Add New Row");
                            ViewData["addnewr"] = bb.Loc_Oth == null ? "Add New Row" : bb.Loc_Oth;
                            bb = lang.EditColumn("Button", "Remove");
                            ViewData["remove"] = bb.Loc_Oth == null ? "Remove" : bb.Loc_Oth;


                            //menu

                            var cc = lang.EditColumn("Reports Common", "Customer");
                            ViewData["Cust"] = cc.Loc_Oth == null ? "Customer" : cc.Loc_Oth;
                            cc = lang.EditColumn("Reports Common", "Start Date");
                            ViewData["sdate"] = cc.Loc_Oth == null ? "Start Date" : cc.Loc_Oth;
                            cc = lang.EditColumn("Reports Common", "End Date");
                            ViewData["edate"] = cc.Loc_Oth == null ? "End Date" : cc.Loc_Oth;
                            cc = lang.EditColumn("Reports Common", "Date");
                            ViewData["date"] = cc.Loc_Oth == null ? "Date" : cc.Loc_Oth;

                            //var smtp = lang.EditColumn("Smtp", "Smtp");
                            //ViewData["smtp"] = smtp.Loc_Oth == null ? "Smtp" : smtp.Loc_Oth;
                            //smtp = lang.EditColumn("Smtp", "From Address");
                            //ViewData["frmaddr"] = smtp.Loc_Oth == null ? "From Address" : smtp.Loc_Oth;
                            //smtp = lang.EditColumn("Smtp", "Add Smtp");
                            //ViewData["addsmtp"] = smtp.Loc_Oth == null ? "Add Smtp" : smtp.Loc_Oth;
                            //smtp = lang.EditColumn("Smtp", "Password");
                            //ViewData["pwd"] = smtp.Loc_Oth == null ? "Password" : smtp.Loc_Oth;
                            //smtp = lang.EditColumn("Smtp", "SSL Enable");
                            //ViewData["sslena"] = smtp.Loc_Oth == null ? "SSL Enable" : smtp.Loc_Oth;
                            //smtp = lang.EditColumn("Smtp", "Smtp Address");
                            //ViewData["smtpadd"] = smtp.Loc_Oth == null ? "Smtp Address" : smtp.Loc_Oth;
                            //smtp = lang.EditColumn("Smtp", "Port Number");
                            //ViewData["portnum"] = smtp.Loc_Oth == null ? "Port Number" : smtp.Loc_Oth;
                            //var Un = lang.EditColumn("Smtp", "User Name");
                            //ViewData["Un"] = Un.Loc_Oth == null ? "User Name" : Un.Loc_Oth;

                            //var bu = lang.EditColumn("Bank User", "Bank User");
                            //ViewData["BU"] = bu.Loc_Oth == null ? "Bank User" : bu.Loc_Oth;
                            //bu = lang.EditColumn("Bank User", "First Name");
                            //ViewData["ffname"] = bu.Loc_Oth == null ? "First Name" : bu.Loc_Oth;
                            //bu = lang.EditColumn("Bank User", "Middle Name");
                            //ViewData["mname"] = bu.Loc_Oth == null ? "Middle Name" : bu.Loc_Oth;
                            //bu = lang.EditColumn("Bank User", "Last Name");
                            //ViewData["lname"] = bu.Loc_Oth == null ? "Last Name" : bu.Loc_Oth;
                            //var BU = lang.EditColumn("Bank User", "Mobile No");
                            //ViewData["Mno"] = BU.Loc_Oth == null ? "Mobile No" : BU.Loc_Oth;
                            //var Eid = lang.EditColumn("Bank User", "Email ID");
                            //ViewData["EID"] = Eid.Loc_Oth == null ? "Email ID" : Eid.Loc_Oth;
                            //Eid = lang.EditColumn("Bank User", "Full Name");
                            //ViewData["fname"] = Eid.Loc_Oth == null ? "Full Name" : Eid.Loc_Oth;
                            //Eid = lang.EditColumn("Bank User", "Employee Id");
                            //ViewData["empid"] = Eid.Loc_Oth == null ? "Employee Id" : Eid.Loc_Oth;
                            //Eid = lang.EditColumn("Bank User", "Add User");
                            //ViewData["adduser"] = Eid.Loc_Oth == null ? "Add User" : Eid.Loc_Oth;

                            //var app = lang.EditColumn("Bank Menu", "Approval");
                            //ViewData["BAp"] = app.Loc_Oth == null ? "Approval" : app.Loc_Oth;
                            //app = lang.EditColumn("Bank Menu", "Language");
                            //ViewData["Blang"] = app.Loc_Oth == null ? "Language" : app.Loc_Oth;

                            //app = lang.EditColumn("Bank Menu", "Setup");
                            //ViewData["setup"] = app.Loc_Oth == null ? "Setup" : app.Loc_Oth;
                            //var up = lang.EditColumn("Bank Menu", "Return");
                            //ViewData["BRt"] = up.Loc_Oth == null ? "Return" : up.Loc_Oth;
                            //var Brep = lang.EditColumn("Bank Menu", "Reports");
                            //ViewData["Brep"] = Brep.Loc_Oth == null ? "Reports" : Brep.Loc_Oth;
                            //var das = lang.EditColumn("Bank Menu", "Dashboard");
                            //ViewData["dash"] = das.Loc_Oth == null ? "Dashboard" : das.Loc_Oth;
                            //das = lang.EditColumn("Bank Menu", "Approve");
                            //ViewData["BApe"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            var das = lang.EditColumn("Language", "Language");
                            ViewData["Blang"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Language", "Select Language");
                            ViewData["sellang"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;

                            //das = lang.EditColumn("Company Master", "Company");
                            //ViewData["comp"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            //das = lang.EditColumn("Company Master", "Add Company");
                            //ViewData["addcomp"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            //das = lang.EditColumn("Company Master", "Company Master");
                            //ViewData["compmas"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            //das = lang.EditColumn("Company Master", "Company Name");
                            //ViewData["compname"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            
                            //das = lang.EditColumn("Company Master", "Director Name");
                            //ViewData["dirnam"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            //das = lang.EditColumn("Company Master", "Telephone No");
                            //ViewData["teleno"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            //das = lang.EditColumn("Company Master", "Fax No");
                            //ViewData["faxno"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;

                            das = lang.EditColumn("Reports Common", "Customer");
                            ViewData["custo"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "Start Date");
                            ViewData["sdate"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "End Date");
                            ViewData["edate"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "UserLog");
                            ViewData["uslog"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "Ip Address");
                            ViewData["ipaddr"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "User Group");
                            ViewData["usegrp"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "Login Time");
                            ViewData["logtym"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "Logout Time");
                            ViewData["logouttym"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "Z Report");
                            ViewData["zrep"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "Invoice Report");
                            ViewData["invrep"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "Invoice Detail Report");
                            ViewData["invdetrep"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "Customer Detail Report");
                            ViewData["custdetrep"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "Non Invoice Report");
                            ViewData["noninvrep"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "Invoice Amount");
                            ViewData["invamt"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "Total");
                            ViewData["tot"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "Reports");
                            ViewData["Brep"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;

                            das = lang.EditColumn("Customers", "Customers");
                            ViewData["custos"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "Customer Name");
                            ViewData["custname"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "Contact Person");
                            ViewData["conctper"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "PostBox No");
                            ViewData["poboxno"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "Address");
                            ViewData["addr"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "Tin No");
                            ViewData["tino"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "Vat No");
                            ViewData["vatno"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "Region");
                            ViewData["Region"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "District");
                            ViewData["District"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "Ward");
                            ViewData["Ward"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "Customer Details");
                            ViewData["custdets"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "Add Customer");
                            ViewData["addcusts"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "Email ID");
                            ViewData["EID"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "Mobile No");
                            ViewData["Mno"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            



                            das = lang.EditColumn("Company Users", "Company Users");
                            ViewData["compus"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Users", "Role");
                            ViewData["role"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Users", "User Name");
                            ViewData["usname"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Users", "User Position");
                            ViewData["userpos"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Users", "Full Name");
                            ViewData["fname"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Users", "Add Company Users");
                            ViewData["addcomuse"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            
                            das = lang.EditColumn("Invoice", "Invoice Details");
                            ViewData["invdet"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Invoice");
                            ViewData["invoice"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Invoice No");
                            ViewData["invno"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Invoice Date");
                            ViewData["invdate"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Customer ID Type");
                            ViewData["custidtyp"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Customer ID No");
                            ViewData["custidno"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Total Without Vat");
                            ViewData["totwithoutvat"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Vat Amount");
                            ViewData["vatamt"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Total Amount");
                            ViewData["totamt"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Remarks");
                            ViewData["remar"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Warrenty");
                            ViewData["warren"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Goods Status");
                            ViewData["goodssts"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Delivery Status");
                            ViewData["delsta"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "VRN Status");
                            ViewData["vrnsta"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Quantity");
                            ViewData["quant"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Unit Price");
                            ViewData["unipri"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Without Vat");
                            ViewData["withoutvat"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Category");
                            ViewData["cate"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Download Invoice");
                            ViewData["dwninvo"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Vat Percentage");
                            ViewData["vatper"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Vat Category");
                            ViewData["vatcat"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Description");
                            ViewData["vatdesc"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Add Invoice");
                            ViewData["addinv"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Currency");
                            ViewData["curren"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Invoice PDF");
                            ViewData["invpdf"] = "Approve";//das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Company");
                            ViewData["compa"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Generated Invoice");
                            ViewData["Geninv"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Generated Invoice Details");
                            ViewData["geninvdet"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Invoice", "Item Details");
                            ViewData["itmdets"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Update Password", "Confirm Password");
                            ViewData["ccp"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Update Password", "New Password");
                            ViewData["np"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Update Password", "Security Question");
                            ViewData["Sques"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Update Password", "Answer");
                            ViewData["Sansw"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;








                            //var cre = gList.Where(a => a.Table_name == "Button" && a.Col_name == "Create").FirstOrDefault();
                            //ViewData["Create"] = cre.Loc_Oth1 == null ? cre.Loc_Eng1 : cre.Loc_Oth1;

                        }
                        else
                        {
                            ViewData["ccp"] = "Confirm Password";
                            ViewData["np"] = "New Password";
                            ViewData["Sques"] = "Security Question";
                            ViewData["Sansw"] = "Answer";
                            ViewData["itmdets"] = "Item Details";
                            ViewData["invamt"] = "Invoice Amount";
                            ViewData["tot"] = "Total";
                            ViewData["addinv"] = "Add Invoice";
                            ViewData["curren"] = "Currency";
                            ViewData["invpdf"] = "Approve";
                            ViewData["compa"] = "Company";
                            ViewData["Geninv"] = "Generated Invoice";
                            ViewData["geninvdet"] = "Generated Invoice Details";

                            ViewData["addnewr"] =  "Add New Row" ;
                            ViewData["remove"] =  "Remove" ;
                            ViewData["addcomuse"] = "Add Company Users";
                            ViewData["custdets"] = "Customer Details";
                            ViewData["addcusts"] = "Add Customer";
                            ViewData["Sno"] = "Sno";
                            ViewData["Actions"] = "Actions";
                            ViewData["Status"] = "Status";
                            ViewData["date"] = "Date";
                            ViewData["cmt"] = "Comment";
                            ViewData["cby"] = "Comment By";
                            ViewData["change"] = "Change Password";
                            ViewData["ccpp"] = "Current Password";
                            ViewData["np"] = "New Password";
                            ViewData["ccp"] = "Confirm Password";
                            ViewData["Sub"] = "Submit";
                            ViewData["PDF"] = "PDF";
                            ViewData["Excel"] = "Excel";
                            ViewData["Close"] = "Close";
                            ViewData["Save"] = "Save";
                            ViewData["Create"] = "Create";
                            ViewData["Update"] = "Update";
                            ViewData["cout"] = "Country";
                            ViewData["addcout"] = "Add Country";
                            ViewData["Region"] = "Region";
                            ViewData["addRegion"] = "Add Region";
                            ViewData["District"] = "District";
                            ViewData["addDistrict"] = "Add District";
                            ViewData["Ward"] = "Ward";
                            ViewData["addWard"] = "Add Ward";
                            ViewData["Cust"] = "Customer";
                            ViewData["sdate"] = "Start Date";
                            ViewData["edate"] = "End Date";
                            ViewData["Desg"] = "Designation";
                            ViewData["addDesg"] = "Add Designation";
                            ViewData["Sques"] = "Security Question";
                            ViewData["Sques"] = "Question Name";
                            ViewData["addSques"] = "Add Security Question";
                            ViewData["smtp"] = "Smtp";
                            ViewData["frmaddr"] = "From Address";
                            ViewData["addsmtp"] = "Add Smtp";
                            ViewData["pwd"] = "Password";
                            ViewData["sslena"] = "SSL Enable";
                            ViewData["smtpadd"] = "Smtp Address";
                            ViewData["portnum"] = "Port Number";
                            ViewData["Un"] = "User Name";
                            ViewData["BU"] = "Bank User";
                            ViewData["ffname"] = "First Name";
                            ViewData["mname"] = "Middle Name";
                            ViewData["lname"] = "Last Name";
                            ViewData["Mno"] = "Mobile No";
                            ViewData["EID"] = "Email ID";
                            ViewData["fname"] = "Full Name";
                            ViewData["empid"] = "Employee Id";
                            ViewData["adduser"] = "Add User";
                            ViewData["et"] = "Email Text";
                            ViewData["floid"] = "Flow Id";
                            ViewData["subeng"] = "Subject English";
                            ViewData["subswa"] = "Subject Swahili";
                            ViewData["emailswa"] = "Email Swahili Text";
                            ViewData["emailtxt"] = "Add Email Text";
                            ViewData["BAp"] = "Approval";
                            ViewData["Blang"] = "Language";
                            ViewData["setup"] = "Setup";
                            ViewData["BRt"] = "Return";
                            ViewData["Brep"] = "Reports";
                            ViewData["dash"] = "Dashboard";
                            ViewData["BApe"] = "Approve";
                            ViewData["lang"] = "Language";
                            ViewData["sellang"] = "Select Language";
                            ViewData["vatper"] = "Vat Percentage";
                            ViewData["addvatper"] = "Add Vat Percentage";
                            ViewData["vatcat"] = "Vat Category";
                            ViewData["vatdesc"] = "Description";
                            ViewData["comp"] = "Company";
                            ViewData["addcomp"] = "Add Company";
                            ViewData["compmas"] = "Company Master";
                            ViewData["compname"] = "Company Name";
                            ViewData["poboxno"] = "PostBox No";
                            ViewData["addr"] = "Address";
                            ViewData["tino"] = "Tin No";
                            ViewData["vatno"] = "Vat No";
                            ViewData["dirnam"] = "Director Name";
                            ViewData["teleno"] = "Telephone No";
                            ViewData["faxno"] = "Fax No";
                            ViewData["custo"] = "Customer";
                            ViewData["sdate"] = "Start Date";
                            ViewData["edate"] = "End Date";
                            ViewData["uslog"] = "UserLog";
                            ViewData["custos"] = "Customers";
                            ViewData["custname"] = "Customer Name";
                            ViewData["conctper"] = "Contact Person";
                            ViewData["compus"] = "Company Users";
                            ViewData["role"] = "Role";
                            ViewData["usname"] = "User Name";
                            ViewData["userpos"] = "User Position";
                            ViewData["invdet"] = "Invoice Details";
                            ViewData["invoice"] = "Invoice";
                            ViewData["invno"] = "Invoice No";
                            ViewData["invdate"] = "Invoice Date";
                            ViewData["custidtyp"] = "Customer ID Type";
                            ViewData["custidno"] = "Customer ID No";
                            ViewData["totwithoutvat"] = "Total Without Vat";
                            ViewData["vatamt"] = "Vat Amount";
                            ViewData["totamt"] = "Total Amount";
                            ViewData["remar"] = "Remarks";
                            ViewData["warren"] = "Warrenty";
                            ViewData["goodssts"] = "Goods Status";
                            ViewData["delsta"] = "Delivery Status";
                            ViewData["vrnsta"] = "VRN Status";
                            ViewData["quant"] = "Quantity";
                            ViewData["unipri"] = "Unit Price";
                            ViewData["withoutvat"] = "Without Vat";
                            ViewData["cate"] = "Category";
                            ViewData["dwninvo"] = "Download Invoice";
                            ViewData["ipaddr"] = "Ip Address";
                            ViewData["usegrp"] = "User Group";
                            ViewData["logtym"] = "Login Time";
                            ViewData["logouttym"] = "Logout Time";
                            ViewData["zrep"] = "Z Report";
                            ViewData["invrep"] = "Invoice Report";
                            ViewData["invdetrep"] = "Invoice Detail Report";
                            ViewData["custdetrep"] = "Customer Detail Report";
                            ViewData["noninvrep"] = "Non Invoice Report";

                        }
                    }

                    else
                    {
                        ViewData["ccp"] = "Confirm Password";
                        ViewData["np"] = "New Password";
                        ViewData["Sques"] = "Security Question";
                        ViewData["Sansw"] = "Answer";
                        ViewData["itmdets"] = "Item Details";
                        ViewData["invamt"] = "Invoice Amount";
                        ViewData["tot"] = "Total";
                        ViewData["addinv"] = "Add Invoice";
                        ViewData["curren"] = "Currency";
                        ViewData["invpdf"] = "Approve";
                        ViewData["compa"] = "Company";
                        ViewData["Geninv"] = "Generated Invoice";
                        ViewData["geninvdet"] = "Generated Invoice Details";
                        ViewData["addnewr"] = "Add New Row";
                        ViewData["remove"] = "Remove";
                        ViewData["addcomuse"] = "Add Company Users";
                        ViewData["custdets"] = "Customer Details";
                        ViewData["addcusts"] = "Add Customer";
                        ViewData["Sno"] = "Sno";
                        ViewData["Actions"] = "Actions";
                        ViewData["Status"] = "Status";
                        ViewData["date"] = "Date";
                        ViewData["cmt"] = "Comment";
                        ViewData["cby"] = "Comment By";
                        ViewData["change"] = "Change Password";
                        ViewData["ccpp"] = "Current Password";
                        ViewData["np"] = "New Password";
                        ViewData["ccp"] = "Confirm Password";
                        ViewData["Sub"] = "Submit";
                        ViewData["PDF"] = "PDF";
                        ViewData["Excel"] = "Excel";
                        ViewData["Close"] = "Close";
                        ViewData["Save"] = "Save";
                        ViewData["Create"] = "Create";
                        ViewData["Update"] = "Update";
                        ViewData["cout"] = "Country";
                        ViewData["addcout"] = "Add Country";
                        ViewData["Region"] = "Region";
                        ViewData["addRegion"] = "Add Region";
                        ViewData["District"] = "District";
                        ViewData["addDistrict"] = "Add District";
                        ViewData["Ward"] = "Ward";
                        ViewData["addWard"] = "Add Ward";
                        ViewData["Cust"] = "Customer";
                        ViewData["sdate"] = "Start Date";
                        ViewData["edate"] = "End Date";
                        ViewData["Desg"] = "Designation";
                        ViewData["addDesg"] = "Add Designation";
                        ViewData["Sques"] = "Security Question";
                        ViewData["Sques"] = "Question Name";
                        ViewData["addSques"] = "Add Security Question";
                        ViewData["smtp"] = "Smtp";
                        ViewData["frmaddr"] = "From Address";
                        ViewData["addsmtp"] = "Add Smtp";
                        ViewData["pwd"] = "Password";
                        ViewData["sslena"] = "SSL Enable";
                        ViewData["smtpadd"] = "Smtp Address";
                        ViewData["portnum"] = "Port Number";
                        ViewData["Un"] = "User Name";
                        ViewData["BU"] = "Bank User";
                        ViewData["ffname"] = "First Name";
                        ViewData["mname"] = "Middle Name";
                        ViewData["lname"] = "Last Name";
                        ViewData["Mno"] = "Mobile No";
                        ViewData["EID"] = "Email ID";
                        ViewData["fname"] = "Full Name";
                        ViewData["empid"] = "Employee Id";
                        ViewData["adduser"] = "Add User";
                        ViewData["et"] = "Email Text";
                        ViewData["floid"] = "Flow Id";
                        ViewData["subeng"] = "Subject English";
                        ViewData["subswa"] = "Subject Swahili";
                        ViewData["emailswa"] = "Email Swahili Text";
                        ViewData["emailtxt"] = "Add Email Text";
                        ViewData["BAp"] = "Approval";
                        ViewData["Blang"] = "Language";
                        ViewData["setup"] = "Setup";
                        ViewData["BRt"] = "Return";
                        ViewData["Brep"] = "Reports";
                        ViewData["dash"] = "Dashboard";
                        ViewData["BApe"] = "Approve";
                        ViewData["lang"] = "Language";
                        ViewData["sellang"] = "Select Language";
                        ViewData["vatper"] = "Vat Percentage";
                        ViewData["addvatper"] = "Add Vat Percentage";
                        ViewData["vatcat"] = "Vat Category";
                        ViewData["vatdesc"] = "Description";
                        ViewData["comp"] = "Company";
                        ViewData["addcomp"] = "Add Company";
                        ViewData["compmas"] = "Company Master";
                        ViewData["compname"] = "Company Name";
                        ViewData["poboxno"] = "PostBox No";
                        ViewData["addr"] = "Address";
                        ViewData["tino"] = "Tin No";
                        ViewData["vatno"] = "Vat No";
                        ViewData["dirnam"] = "Director Name";
                        ViewData["teleno"] = "Telephone No";
                        ViewData["faxno"] = "Fax No";
                        ViewData["custo"] = "Customer";
                        ViewData["sdate"] = "Start Date";
                        ViewData["edate"] = "End Date";
                        ViewData["uslog"] = "UserLog";
                        ViewData["custos"] = "Customers";
                        ViewData["custname"] = "Customer Name";
                        ViewData["conctper"] = "Contact Person";
                        ViewData["compus"] = "Company Users";
                        ViewData["role"] = "Role";
                        ViewData["usname"] = "User Name";
                        ViewData["userpos"] = "User Position";
                        ViewData["invdet"] = "Invoice Details";
                        ViewData["invoice"] = "Invoice";
                        ViewData["invno"] = "Invoice No";
                        ViewData["invdate"] = "Invoice Date";
                        ViewData["custidtyp"] = "Customer ID Type";
                        ViewData["custidno"] = "Customer ID No";
                        ViewData["totwithoutvat"] = "Total Without Vat";
                        ViewData["vatamt"] = "Vat Amount";
                        ViewData["totamt"] = "Total Amount";
                        ViewData["remar"] = "Remarks";
                        ViewData["warren"] = "Warrenty";
                        ViewData["goodssts"] = "Goods Status";
                        ViewData["delsta"] = "Delivery Status";
                        ViewData["vrnsta"] = "VRN Status";
                        ViewData["quant"] = "Quantity";
                        ViewData["unipri"] = "Unit Price";
                        ViewData["withoutvat"] = "Without Vat";
                        ViewData["cate"] = "Category";
                        ViewData["dwninvo"] = "Download Invoice";
                        ViewData["ipaddr"] = "Ip Address";
                        ViewData["usegrp"] = "User Group";
                        ViewData["logtym"] = "Login Time";
                        ViewData["logouttym"] = "Logout Time";
                        ViewData["zrep"] = "Z Report";
                        ViewData["invrep"] = "Invoice Report";
                        ViewData["invdetrep"] = "Invoice Detail Report";
                        ViewData["custdetrep"] = "Customer Detail Report";
                        ViewData["noninvrep"] = "Non Invoice Report";

                    }
                }
                else
                {
                    ViewData["ccp"] = "Confirm Password";
                    ViewData["np"] = "New Password";
                    ViewData["Sques"] = "Security Question";
                    ViewData["Sansw"] = "Answer";
                    ViewData["itmdets"] = "Item Details";
                    ViewData["invamt"] = "Invoice Amount";
                    ViewData["tot"] = "Total";
                    ViewData["addinv"] = "Add Invoice";
                    ViewData["curren"] = "Currency";
                    ViewData["invpdf"] = "Approve";
                    ViewData["compa"] = "Company";
                    ViewData["Geninv"] = "Generated Invoice";
                    ViewData["geninvdet"] = "Generated Invoice Details";
                    ViewData["addnewr"] = "Add New Row";
                    ViewData["remove"] = "Remove";
                    ViewData["addcomuse"] = "Add Company Users";
                    ViewData["Sno"] = "Sno";
                    ViewData["Actions"] = "Actions";
                    ViewData["Status"] = "Status";
                    ViewData["date"] = "Date";
                    ViewData["cmt"] = "Comment";
                    ViewData["cby"] = "Comment By";
                    ViewData["change"] = "Change Password";
                    ViewData["ccpp"] = "Current Password";
                    ViewData["np"] = "New Password";
                    ViewData["ccp"] = "Confirm Password";
                    ViewData["Sub"] = "Submit";
                    ViewData["PDF"] = "PDF";
                    ViewData["Excel"] = "Excel";
                    ViewData["Close"] = "Close";
                    ViewData["Save"] = "Save";
                    ViewData["Create"] = "Create";
                    ViewData["Update"] = "Update";
                    ViewData["cout"] = "Country";
                    ViewData["addcout"] = "Add Country";
                    ViewData["Region"] = "Region";
                    ViewData["addRegion"] = "Add Region";
                    ViewData["District"] = "District";
                    ViewData["addDistrict"] = "Add District";
                    ViewData["Ward"] = "Ward";
                    ViewData["addWard"] = "Add Ward";
                    ViewData["Cust"] = "Customer";
                    ViewData["sdate"] = "Start Date";
                    ViewData["edate"] = "End Date";
                    ViewData["Desg"] = "Designation";
                    ViewData["addDesg"] = "Add Designation";
                    ViewData["Sques"] = "Security Question";
                    ViewData["Sques"] = "Question Name";
                    ViewData["addSques"] = "Add Security Question";
                    ViewData["smtp"] = "Smtp";
                    ViewData["frmaddr"] = "From Address";
                    ViewData["addsmtp"] = "Add Smtp";
                    ViewData["pwd"] = "Password";
                    ViewData["sslena"] = "SSL Enable";
                    ViewData["smtpadd"] = "Smtp Address";
                    ViewData["portnum"] = "Port Number";
                    ViewData["Un"] = "User Name";
                    ViewData["BU"] = "Bank User";
                    ViewData["ffname"] = "First Name";
                    ViewData["mname"] = "Middle Name";
                    ViewData["lname"] = "Last Name";
                    ViewData["Mno"] = "Mobile No";
                    ViewData["EID"] = "Email ID";
                    ViewData["fname"] = "Full Name";
                    ViewData["empid"] = "Employee Id";
                    ViewData["adduser"] = "Add User";
                    ViewData["et"] = "Email Text";
                    ViewData["floid"] = "Flow Id";
                    ViewData["subeng"] = "Subject English";
                    ViewData["subswa"] = "Subject Swahili";
                    ViewData["emailswa"] = "Email Swahili Text";
                    ViewData["emailtxt"] = "Add Email Text";
                    ViewData["BAp"] = "Approval";
                    ViewData["Blang"] = "Language";
                    ViewData["setup"] = "Setup";
                    ViewData["BRt"] = "Return";
                    ViewData["Brep"] = "Reports";
                    ViewData["dash"] = "Dashboard";
                    ViewData["BApe"] = "Approve";
                    ViewData["lang"] = "Language";
                    ViewData["sellang"] = "Select Language";
                    ViewData["vatper"] = "Vat Percentage";
                    ViewData["addvatper"] = "Add Vat Percentage";
                    ViewData["vatcat"] = "Vat Category";
                    ViewData["vatdesc"] = "Description";
                    ViewData["comp"] = "Company";
                    ViewData["addcomp"] = "Add Company";
                    ViewData["compmas"] = "Company Master";
                    ViewData["compname"] = "Company Name";
                    ViewData["poboxno"] = "PostBox No";
                    ViewData["addr"] = "Address";
                    ViewData["tino"] = "Tin No";
                    ViewData["vatno"] = "Vat No";
                    ViewData["dirnam"] = "Director Name";
                    ViewData["teleno"] = "Telephone No";
                    ViewData["faxno"] = "Fax No";
                    ViewData["custo"] = "Customer";
                    ViewData["sdate"] = "Start Date";
                    ViewData["edate"] = "End Date";
                    ViewData["uslog"] = "UserLog";
                    ViewData["custos"] = "Customers";
                    ViewData["custname"] = "Customer Name";
                    ViewData["conctper"] = "Contact Person";
                    ViewData["compus"] = "Company Users";
                    ViewData["role"] = "Role";
                    ViewData["usname"] = "User Name";
                    ViewData["userpos"] = "User Position";
                    ViewData["invdet"] = "Invoice Details";
                    ViewData["invoice"] = "Invoice";
                    ViewData["invno"] = "Invoice No";
                    ViewData["invdate"] = "Invoice Date";
                    ViewData["custidtyp"] = "Customer ID Type";
                    ViewData["custidno"] = "Customer ID No";
                    ViewData["totwithoutvat"] = "Total Without Vat";
                    ViewData["vatamt"] = "Vat Amount";
                    ViewData["totamt"] = "Total Amount";
                    ViewData["remar"] = "Remarks";
                    ViewData["warren"] = "Warrenty";
                    ViewData["goodssts"] = "Goods Status";
                    ViewData["delsta"] = "Delivery Status";
                    ViewData["vrnsta"] = "VRN Status";
                    ViewData["quant"] = "Quantity";
                    ViewData["unipri"] = "Unit Price";
                    ViewData["withoutvat"] = "Without Vat";
                    ViewData["cate"] = "Category";
                    ViewData["dwninvo"] = "Download Invoice";
                    ViewData["ipaddr"] = "Ip Address";
                    ViewData["usegrp"] = "User Group";
                    ViewData["logtym"] = "Login Time";
                    ViewData["logouttym"] = "Logout Time";
                    ViewData["zrep"] = "Z Report";
                    ViewData["invrep"] = "Invoice Report";
                    ViewData["invdetrep"] = "Invoice Detail Report";
                    ViewData["custdetrep"] = "Customer Detail Report";
                    ViewData["noninvrep"] = "Non Invoice Report";

                }

            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.Message.ToString();
            }

            base.OnActionExecuting(filterContext);


            //var rCount = not.getBilnkbankC(long.Parse(Session["CompID"].ToString()), long.Parse(Session["UserID"].ToString()),
            //    Session["admin1"].ToString(), Session["instcat"].ToString());

            //// var rCount = noti.GetNotifications11(long.Parse(Session["UserID"].ToString()));
            //if (rCount != null)
            //{
            //    ViewBag.Data = rCount;
            //}
            //else
            //{
            //    ViewBag.Data = "0";
            //}


        }

    }
    public class LangcController : LangcoController
    {
        // GET: Langc
        langcompany lang = new langcompany();
        EMP_DET ed = new EMP_DET();
        CompanyUsers ch = new CompanyUsers();
        private readonly dynamic returnNull = null;
        
        public ActionResult Langc()
        {
            try
            {
                if (Session["sessComp"] == null)
                {
                    return RedirectToAction("Loginnew", "Loginnew");
                }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return View();
        }
        public ActionResult Getlang(String name)
        {
            try
            {
                var result = lang.Getlang(name);
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var d = 0;
                    return Json(d, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                // long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.Message.ToString();
            }

            return returnNull;
        }
        public ActionResult Editlang(string tbname, string name, string type)
        {
            try
            {
                string d = "";
                if (type == "BNK")
                {
                    var drt = ed.validatelang(long.Parse(Session["UserID"].ToString()));
                    d = drt.Loc_Name;
                }
                else if (type == "Mem")
                {

                }
                else
                {
                    var drt2 = ch.validatelangInst(long.Parse(Session["UserID"].ToString()), long.Parse(Session["CompID"].ToString()));
                    d = drt2.Loc_Name;
                }

                if (d == "Swahili")
                {
                    var data = lang.ValidateColumn(tbname, name);
                    if (data != false)
                    {
                        var result = lang.EditColumn(tbname, name);

                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var result = lang.EditColumn(tbname, name);
                    var returnField = new { check = "Engg", Eng = result.Loc_Eng };
                    return Json(returnField, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.Message.ToString();
            }

            return returnNull;
        }
        public ActionResult Editlangs(string tbname, string name)
        {
            try
            {
                var data = lang.ValidateTemp(long.Parse(Session["CompID"].ToString()), tbname, name);
                if (data != false)
                {
                    var result = lang.EditTemp(long.Parse(Session["CompID"].ToString()),tbname, name);

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.Message.ToString();
            }

            return returnNull;
        }
        public ActionResult Adddesg(string table, List<langcompany> details)
        {
            try
            {

                lang.Table_name = table;
                long ssno = 0;
                lang.Posted_By = Session["UserID"].ToString();
                lang.Inst_reg_sno = long.Parse(Session["CompID"].ToString());
                for (int i = 0; i < details.Count; i++)
                {
                    lang.Loc_Eng = details[i].Loc_Eng;
                    lang.Loc_Oth = details[i].Loc_Oth;
                    //lang.Loc_Eng1 = details[i].Loc_Eng;
                    //lang.Loc_Oth1 = details[i].Loc_Oth1;
                    lang.Loc_Sno = details[i].Loc_Sno;
                    lang.Col_name = details[i].Loc_Eng;

                    lang.UpdateTemp(lang);
                    ssno = details[i].Loc_Sno;
                }

                return Json(ssno, JsonRequestBehavior.AllowGet);

            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.ToString();
            }

            return returnNull;
        }

        [HttpPost]
        public ActionResult Update(string name)
        {
            try
            {
                ch.CompuserSno = long.Parse(Session["UserID"].ToString());
                ch.Loc_Name = name;
                ch.Updatelang(ch);
                return Json(2, JsonRequestBehavior.AllowGet);

            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.Message.ToString();
            }

            return returnNull;
        }
    }



    


    







}