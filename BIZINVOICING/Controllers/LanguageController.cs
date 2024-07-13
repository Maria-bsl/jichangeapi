using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class AdminBaseController : Controller
    {
        Language lang = new Language();
        EMP_DET ed = new EMP_DET();
        //Notify noti = new Notify();
        private readonly dynamic returnNull = null;
        //public ActionResult AdminBase()
        //{
        //    try
        //    {

        //        if (Session["sessB"] == null)
        //        {
        //            return RedirectToAction("Loginnew", "Loginnew");
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
        //        Ex.ToString();
        //    }
        //    return View();
        //}
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (Session["UserID"] != null)
                {


                    var result = ed.Validateuserblo(long.Parse(Session["UserID"].ToString()));
                    if (result == true)
                    {
                        ViewBag.menulist = result;
                    }
                    else
                    {
                        if (Session["UserID"].ToString() == "0")
                        {
                            ViewBag.menulist = "true";
                        }
                        else
                        {
                            ViewBag.menulist = null;
                        }
                    }
                    var drt = ed.validatelang(long.Parse(Session["UserID"].ToString()));
                    if (drt != null)
                    {
                        if (drt.Loc_Name == "Swahili")
                        {
                            //grid
                            var bb = lang.EditColumn("Grid", "Sno");
                            ViewData["Sno"] = bb.Loc_Oth == null ? "Sno" : bb.Loc_Oth;
                            bb = lang.EditColumn("Grid", "Actions");
                            ViewData["Actions"] = bb.Loc_Oth == null ? "Actions" : bb.Loc_Oth;
                            bb = lang.EditColumn("Grid", "Status");
                            ViewData["Status"] = bb.Loc_Oth == null ? "Status" : bb.Loc_Oth;
                            bb = lang.EditColumn("Comment", "Date");
                            ViewData["date"] = bb.Loc_Oth == null ? "Date" : bb.Loc_Oth;
                            bb = lang.EditColumn("Comment", "Comment");
                            ViewData["cmt"] = bb.Loc_Oth == null ? "Comment" : bb.Loc_Oth;
                            bb = lang.EditColumn("Comment", "Comment By");
                            ViewData["cby"] = bb.Loc_Oth == null ? "Comment By" : bb.Loc_Oth;
                            bb = lang.EditColumn("Comment", "Change Password");
                            ViewData["change"] = bb.Loc_Oth == null ? "Change Password" : bb.Loc_Oth;
                            bb = lang.EditColumn("Comment", "Current Password");
                            ViewData["ccpp"] = bb.Loc_Oth == null ? "Current Password" : bb.Loc_Oth;
                            bb = lang.EditColumn("Comment", "New Password");
                            ViewData["np"] = bb.Loc_Oth == null ? "New Password" : bb.Loc_Oth;
                            bb = lang.EditColumn("Comment", "Confirm Password");
                            ViewData["ccp"] = bb.Loc_Oth == null ? "Confirm Password" : bb.Loc_Oth;

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
                            bb = lang.EditColumn("Button", "Delete");
                            ViewData["dele"] = bb.Loc_Oth == null ? "Delete" : bb.Loc_Oth;


                            //menu
                            var b1 = lang.EditColumn("Country", "Country");
                            ViewData["cout"] = b1.Loc_Oth == null ? "Country" : b1.Loc_Oth;
                            b1 = lang.EditColumn("Country", "Add Country");
                            ViewData["addcout"] = b1.Loc_Oth == null ? "Add Country" : b1.Loc_Oth;
                            b1 = lang.EditColumn("Country", "Country Details");
                            ViewData["coutdets"] = b1.Loc_Oth == null ? "Country Details" : b1.Loc_Oth;

                            //b1 = lang.EditColumn("Denomination", "Denomination");
                            //ViewData["denom"] = b1.Loc_Oth == null ? "Denomination" : b1.Loc_Oth;
                            //b1 = lang.EditColumn("Diocese", "Diocese Registration");
                            //ViewData["dioc"] = b1.Loc_Oth == null ? "Diocese Registration" : b1.Loc_Oth;
                            
                            //var set = lang.EditColumn("Bank Dashboard", "Setup");
                            //ViewData["setup"] = set.Loc_Oth == null ? "Setup" : set.Loc_Oth;
                            var reg = lang.EditColumn("Region", "Region");
                            ViewData["Region"] = reg.Loc_Oth == null ? "Region" : reg.Loc_Oth;
                            reg = lang.EditColumn("Region", "Add Region");
                            ViewData["addRegion"] = reg.Loc_Oth == null ? "Add Region" : reg.Loc_Oth;
                            reg = lang.EditColumn("Region", "Region Details");
                            ViewData["Regiondets"] = reg.Loc_Oth == null ? "Region Details" : reg.Loc_Oth;


                            var dis = lang.EditColumn("District", "District");
                            ViewData["District"] = dis.Loc_Oth == null ? "District" : dis.Loc_Oth;
                            dis = lang.EditColumn("District", "Add District");
                            ViewData["addDistrict"] = dis.Loc_Oth == null ? "Add District" : dis.Loc_Oth;
                            dis = lang.EditColumn("District", "District Details");
                            ViewData["distdets"] = dis.Loc_Oth == null ? "District Details" : dis.Loc_Oth;


                            var wd = lang.EditColumn("Ward", "Ward");
                            ViewData["Ward"] = wd.Loc_Oth == null ? "Ward" : wd.Loc_Oth;
                            wd = lang.EditColumn("Ward", "Add Ward");
                            ViewData["addWard"] = wd.Loc_Oth == null ? "Add Ward" : wd.Loc_Oth;
                            wd = lang.EditColumn("Ward", "Ward Details");
                            ViewData["Warddets"] = wd.Loc_Oth == null ? "Ward Details" : wd.Loc_Oth;

                            var curr = lang.EditColumn("Currency", "Currency Code");
                            ViewData["currcod"] = curr.Loc_Oth == null ? "Currency Code" : curr.Loc_Oth;
                            curr = lang.EditColumn("Currency", "Currency");
                            ViewData["curr"] = curr.Loc_Oth == null ? "Currency" : curr.Loc_Oth;
                            curr = lang.EditColumn("Currency", "Currency Details");
                            ViewData["currdets"] = curr.Loc_Oth == null ? "Currency Details" : curr.Loc_Oth;
                            curr = lang.EditColumn("Currency", "Currency Name");
                            ViewData["currname"] = curr.Loc_Oth == null ? "Currency Name" : curr.Loc_Oth;
                            curr = lang.EditColumn("Currency", "Add Currency");
                            ViewData["addcurre"] = curr.Loc_Oth == null ? "Add Currency" : curr.Loc_Oth;


                            //var bk = lang.EditColumn("Branch", "Bank Branch");
                            //ViewData["Bank"] = bk.Loc_Oth == null ? "Bank Branch" : bk.Loc_Oth;
                            var cc = lang.EditColumn("Reports Common", "Customer");
                            ViewData["Cust"] = cc.Loc_Oth == null ? "Customer" : cc.Loc_Oth;
                            cc = lang.EditColumn("Reports Common", "Start Date");
                            ViewData["sdate"] = cc.Loc_Oth == null ? "Start Date" : cc.Loc_Oth;
                            cc = lang.EditColumn("Reports Common", "End Date");
                            ViewData["edate"] = cc.Loc_Oth == null ? "End Date" : cc.Loc_Oth;

                            var dd = lang.EditColumn("Designation", "Designation");
                            ViewData["Desg"] = dd.Loc_Oth == null ? "Designation" : dd.Loc_Oth;
                             dd = lang.EditColumn("Designation", "Add Designation");
                            ViewData["addDesg"] = dd.Loc_Oth == null ? "Add Designation" : dd.Loc_Oth;
                            dd = lang.EditColumn("Designation", "Designation Details");
                            ViewData["Desgdets"] = dd.Loc_Oth == null ? "Designation Details" : dd.Loc_Oth;

                            var qs = lang.EditColumn("Question", "Security Question");
                            ViewData["Sques"] = qs.Loc_Oth == null ? "Security Question" : qs.Loc_Oth;
                             qs = lang.EditColumn("Question", "Question Name");
                            ViewData["Sques"] = qs.Loc_Oth == null ? "Question Name" : qs.Loc_Oth;
                            qs = lang.EditColumn("Question", "Answer");
                            ViewData["Sansw"] = qs.Loc_Oth == null ? "Answer" : qs.Loc_Oth;
                            qs = lang.EditColumn("Question", "Add Security Question");
                            ViewData["addSques"] = qs.Loc_Oth == null ? "Add Security Question" : qs.Loc_Oth;
                            qs = lang.EditColumn("Question", "Security Question Details");
                            ViewData["Squesdets"] = qs.Loc_Oth == null ? "Security Question Details" : qs.Loc_Oth;


                            var smtp = lang.EditColumn("Smtp", "Smtp");
                            ViewData["smtp"] = smtp.Loc_Oth == null ? "Smtp" : smtp.Loc_Oth;
                            smtp = lang.EditColumn("Smtp", "From Address");
                            ViewData["frmaddr"] = smtp.Loc_Oth == null ? "From Address" : smtp.Loc_Oth;
                            smtp = lang.EditColumn("Smtp", "Add Smtp");
                            ViewData["addsmtp"] = smtp.Loc_Oth == null ? "Add Smtp" : smtp.Loc_Oth;
                            smtp = lang.EditColumn("Smtp", "Smtp Details");
                            ViewData["smtpdets"] = smtp.Loc_Oth == null ? "Smtp Details" : smtp.Loc_Oth;
                            smtp = lang.EditColumn("Smtp", "Password");
                            ViewData["pwd"] = smtp.Loc_Oth == null ? "Password" : smtp.Loc_Oth;
                            smtp = lang.EditColumn("Smtp", "SSL Enable");
                            ViewData["sslena"] = smtp.Loc_Oth == null ? "SSL Enable" : smtp.Loc_Oth;
                            smtp = lang.EditColumn("Smtp", "Smtp Address");
                            ViewData["smtpadd"] = smtp.Loc_Oth == null ? "Smtp Address" : smtp.Loc_Oth;
                            smtp = lang.EditColumn("Smtp", "Port Number");
                            ViewData["portnum"] = smtp.Loc_Oth == null ? "Port Number" : smtp.Loc_Oth;
                            var Un = lang.EditColumn("Smtp", "Username");
                            ViewData["Un"] = Un.Loc_Oth == null ? "Username" : Un.Loc_Oth;

                            var bu = lang.EditColumn("Bank User", "Bank User");
                            ViewData["BU"] = bu.Loc_Oth == null ? "Bank User" : bu.Loc_Oth;
                            bu = lang.EditColumn("Bank User", "First Name");
                            ViewData["ffname"] = bu.Loc_Oth == null ? "First Name" : bu.Loc_Oth;
                            bu = lang.EditColumn("Bank User", "Middle Name");
                            ViewData["mname"] = bu.Loc_Oth == null ? "Middle Name" : bu.Loc_Oth;
                            bu = lang.EditColumn("Bank User", "Last Name");
                            ViewData["lname"] = bu.Loc_Oth == null ? "Last Name" : bu.Loc_Oth;
                            var BU = lang.EditColumn("Bank User", "Mobile No");
                            ViewData["Mno"] = BU.Loc_Oth == null ? "Mobile No" : BU.Loc_Oth;
                            var Eid = lang.EditColumn("Bank User", "Email ID");
                            ViewData["EID"] = Eid.Loc_Oth == null ? "Email ID" : Eid.Loc_Oth;
                            Eid = lang.EditColumn("Bank User", "Full Name");
                            ViewData["fname"] = Eid.Loc_Oth == null ? "Full Name" : Eid.Loc_Oth;
                            Eid = lang.EditColumn("Bank User", "Employee Id");
                            ViewData["empid"] = Eid.Loc_Oth == null ? "Employee Id" : Eid.Loc_Oth;
                            Eid = lang.EditColumn("Bank User", "Add User");
                            ViewData["adduser"] = Eid.Loc_Oth == null ? "Add User" : Eid.Loc_Oth;
                            Eid = lang.EditColumn("Bank User", "User Details");
                            ViewData["userdets"] = Eid.Loc_Oth == null ? "User Details" : Eid.Loc_Oth;
                            //bu = lang.EditColumn("Bank User", "Parish Users");
                            //ViewData["puser"] = bu.Loc_Oth == null ? "Parish Users" : bu.Loc_Oth;

                            //var ty = lang.EditColumn("Institution Types", "Institution Type");
                            //ViewData["Typ"] = ty.Loc_Oth == null ? "Institution Type" : ty.Loc_Oth;
                            //ty = lang.EditColumn("Institution Registration", "Institution Status");
                            //ViewData["TypStat"] = ty.Loc_Oth == null ? "Institution Status" : ty.Loc_Oth;
                            //ty = lang.EditColumn("Institution Registration", "Registration No");
                            //ViewData["Mregn"] = ty.Loc_Oth == null ? "Registration No" : ty.Loc_Oth;



                            var et = lang.EditColumn("Email Text", "Email Text");
                            ViewData["et"] = et.Loc_Oth == null ? "Email Text" : et.Loc_Oth;
                            et = lang.EditColumn("Email Text", "Flow Id");
                            ViewData["floid"] = et.Loc_Oth == null ? "Flow Id" : et.Loc_Oth;
                            et = lang.EditColumn("Email Text", "Subject English");
                            ViewData["subeng"] = et.Loc_Oth == null ? "Subject English" : et.Loc_Oth;
                            et = lang.EditColumn("Email Text", "Subject Swahili");
                            ViewData["subswa"] = et.Loc_Oth == null ? "Subject Swahili" : et.Loc_Oth;
                            et = lang.EditColumn("Email Text", "Email Swahili Text");
                            ViewData["emailswa"] = et.Loc_Oth == null ? "Email Swahili Text" : et.Loc_Oth;
                            et = lang.EditColumn("Email Text", "Add Email Text");
                            ViewData["emailtxt"] = et.Loc_Oth == null ? "Add Email Text" : et.Loc_Oth;
                            et = lang.EditColumn("Email Text", "Email Details");
                            ViewData["emailtdet"] = et.Loc_Oth == null ? "Email Details" : et.Loc_Oth;


                            //var Inst = lang.EditColumn("Bank Menu", "Institutions");
                            //ViewData["Inst"] = Inst.Loc_Oth == null ? "Institutions" : Inst.Loc_Oth;
                            var app = lang.EditColumn("Bank Menu", "Approval");
                            ViewData["BAp"] = app.Loc_Oth == null ? "Approval" : app.Loc_Oth;
                            app = lang.EditColumn("Bank Menu", "Language");
                            ViewData["Blang"] = app.Loc_Oth == null ? "Language" : app.Loc_Oth;
                           
                            app = lang.EditColumn("Bank Menu", "Setup");
                            ViewData["setup"] = app.Loc_Oth == null ? "Setup" : app.Loc_Oth;
                            var up = lang.EditColumn("Bank Menu", "Return");
                            ViewData["BRt"] = up.Loc_Oth == null ? "Return" : up.Loc_Oth;
                            var Brep = lang.EditColumn("Bank Menu", "Reports");
                            ViewData["Brep"] = Brep.Loc_Oth == null ? "Reports" : Brep.Loc_Oth;
                            var das = lang.EditColumn("Bank Menu", "Dashboard");
                            ViewData["dash"] = das.Loc_Oth == null ? "Dashboard" : das.Loc_Oth;
                            das = lang.EditColumn("Bank Menu", "Approve");
                            ViewData["BApe"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Bank Menu", "Generatedinvoices");
                            ViewData["Geninv"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                             das = lang.EditColumn("Language", "Language");
                            ViewData["lang"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Language", "Select Language");
                            ViewData["sellang"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;

                            das = lang.EditColumn("Vat Percentage", "Vat Percentage");
                            ViewData["vatper"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                             das = lang.EditColumn("Vat Percentage", "Add Vat Percentage");
                            ViewData["addvatper"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Vat Percentage", "Vat Category");
                            ViewData["vatcat"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Vat Percentage", "Description");
                            ViewData["vatdesc"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Vat Percentage", "Vat Percentage Details");
                            ViewData["vatperdets"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;

                            das = lang.EditColumn("Company Master", "Company");
                            ViewData["comp"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                             das = lang.EditColumn("Company Master", "Add Company");
                            ViewData["addcomp"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Company Master");
                            ViewData["compmas"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Company Name");
                            ViewData["compname"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "PostBox No");
                            ViewData["poboxno"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Address");
                            ViewData["addr"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Tin No");
                            ViewData["tino"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Vat No");
                            ViewData["vatno"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Director Name");
                            ViewData["dirnam"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Telephone No");
                            ViewData["teleno"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Fax No");
                            ViewData["faxno"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Company Details");
                            ViewData["compdet"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Bank Name");
                            ViewData["bnknam"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Bank Branch");
                            ViewData["bnkbrch"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Account No");
                            ViewData["accno"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Swift Code");
                            ViewData["swcod"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Master", "Bank Details");
                            ViewData["bnkdets"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;

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
                            ViewData["tota"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Reports Common", "UserLog Report");
                            ViewData["uselog"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;

                            das = lang.EditColumn("Customers", "Customers");
                            ViewData["custos"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "Customer Name");
                            ViewData["custname"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Customers", "Contact Person");
                            ViewData["conctper"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;

                            das = lang.EditColumn("Company Users", "Company Users");
                            ViewData["compus"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Users", "Role");
                            ViewData["custname"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Users", "User Name");
                            ViewData["usname"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;
                            das = lang.EditColumn("Company Users", "User Position");
                            ViewData["userpos"] = das.Loc_Oth == null ? das.Loc_Eng : das.Loc_Oth;

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

                            das = lang.EditColumn("Audit Trails", "Audit Trails");
                            ViewData["Atra"] = das.Loc_Oth == null ? "Audit Trails" : das.Loc_Oth;
                            das = lang.EditColumn("Audit Trails", "Select Page");
                            ViewData["Spag"] = das.Loc_Oth == null ? "Select Page" : das.Loc_Oth;
                            das = lang.EditColumn("Audit Trails", "Column Name");
                            ViewData["Coln"] = das.Loc_Oth == null ? "Column Name" : das.Loc_Oth;
                            das = lang.EditColumn("Audit Trails", "Old Value");
                            ViewData["Oval"] = das.Loc_Oth == null ? "Old Value" : das.Loc_Oth;
                            das = lang.EditColumn("Audit Trails", "New Value");
                            ViewData["Nval"] = das.Loc_Oth == null ? "New Value" : das.Loc_Oth;
                            das = lang.EditColumn("Audit Trails", "Posted/Modified By");
                            ViewData["PoMb"] = das.Loc_Oth == null ? "Posted/Modified By" : das.Loc_Oth;
                            das = lang.EditColumn("Audit Trails", "Audit Date");
                            ViewData["Adat"] = das.Loc_Oth == null ? "Audit Date" : das.Loc_Oth;

                            //var ir = lang.EditColumn("Institution Registration", "Institution Details");
                            //ViewData["IR"] = ir.Loc_Oth == null ? "Institution Details" : ir.Loc_Oth;
                            //var Ivd = lang.EditColumn("Invoice Report", "Invoice Details");
                            //ViewData["ivr"] = Ivd.Loc_Oth == null ? "Invoice Details" : Ivd.Loc_Oth;
                            //Ivd = lang.EditColumn("Invoice Report", "Offer Amount");
                            //ViewData["ofamu"] = Ivd.Loc_Oth == null ? "Offer Amount" : Ivd.Loc_Oth;
                            //Ivd = lang.EditColumn("Invoice Report", "Account No");
                            //ViewData["AccNo"] = Ivd.Loc_Oth == null ? "Account No" : Ivd.Loc_Oth;

                            //var langua = lang.EditColumn("Language", "Language");
                            //ViewData["Lang"] = langua.Loc_Oth == null ? "Language" : langua.Loc_Oth;
                            //var pd = lang.EditColumn("Payment Report", "Payment Details");
                            //ViewData["pd"] = pd.Loc_Oth == null ? "Payment Details" : pd.Loc_Oth;
                            //pd = lang.EditColumn("Payment Report", "Payment Date");
                            //ViewData["pdate"] = pd.Loc_Oth == null ? "Payment Date" : pd.Loc_Oth;
                            //pd = lang.EditColumn("Payment Report", "Paid Amount");
                            //ViewData["pamt"] = pd.Loc_Oth == null ? "Paid Amount" : pd.Loc_Oth;
                            //pd = lang.EditColumn("Payment Report", "Receipt No");
                            //ViewData["recno"] = pd.Loc_Oth == null ? "Receipt No" : pd.Loc_Oth;
                            //var cr = lang.EditColumn("Contribution Report", "Contribution Details");
                            //ViewData["cr"] = cr.Loc_Oth == null ? "Contribution Details" : cr.Loc_Oth;

                            //cr = lang.EditColumn("Contribution Report", "Offer Type");
                            //ViewData["oftype"] = cr.Loc_Oth == null ? "Offer Type" : cr.Loc_Oth;
                            //cr = lang.EditColumn("Contribution Report", "Amount");
                            //ViewData["amt"] = cr.Loc_Oth == null ? "Amount" : cr.Loc_Oth;
                            //var m = lang.EditColumn("Members Report", "Member Details");
                            //ViewData["md"] = m.Loc_Oth == null ? "Member Details" : m.Loc_Oth;
                            //m = lang.EditColumn("Members Report", "Member Registration No");
                            //ViewData["mregno"] = m.Loc_Oth == null ? "Member Registration No" : m.Loc_Oth;
                            //m = lang.EditColumn("Members Report", "Religious Association");
                            //ViewData["mrel"] = m.Loc_Oth == null ? "Religious Association" : m.Loc_Oth;
                            //m = lang.EditColumn("Members Report", "Age Description");
                            //ViewData["magedes"] = m.Loc_Oth == null ? "Age Description" : m.Loc_Oth;
                            //m = lang.EditColumn("Members Report", "Marital Status");
                            //ViewData["marsta"] = m.Loc_Oth == null ? "Marital Status" : m.Loc_Oth;
                            //m = lang.EditColumn("Members Report", "Gender");
                            //ViewData["gen"] = m.Loc_Oth == null ? "Gender" : m.Loc_Oth;
                            //m = lang.EditColumn("Members Report", "Date Of Birth");
                            //ViewData["dob"] = m.Loc_Oth == null ? "Date Of Birth" : m.Loc_Oth;
                            //m = lang.EditColumn("Members Report", "Membership Type");
                            //ViewData["mshiptyp"] = m.Loc_Oth == null ? "Membership Type" : m.Loc_Oth;
                            //m = lang.EditColumn("Members Report", "Member Name");
                            //ViewData["Memna"] = m.Loc_Oth == null ? "Member Name" : m.Loc_Oth;
                            //var mm89 = lang.EditColumn("Language", "Select Language");
                            //ViewData["Sslang"] = mm89.Loc_Oth == null ? "Select Language" : mm89.Loc_Oth;


                            //m = lang.EditColumn("Block", "Block/Unblock Users");
                            //ViewData["blockus"] = m.Loc_Oth == null ? "Block/Unblock Users" : m.Loc_Oth;
                            //m = lang.EditColumn("Block", "Block/Unblock");
                            //ViewData["ublockus"] = m.Loc_Oth == null ? "Block/Unblock" : m.Loc_Oth;
                            //m = lang.EditColumn("Block", "Blocked Users");
                            //ViewData["busers"] = m.Loc_Oth == null ? "Blocked Users" : m.Loc_Oth;
                            //m = lang.EditColumn("Block", "Unblock Users");
                            //ViewData["ubusers"] = m.Loc_Oth == null ? "Unblock Users" : m.Loc_Oth;
                            //m = lang.EditColumn("Block", "Users");
                            //ViewData["users"] = m.Loc_Oth == null ? "Users" : m.Loc_Oth;




                            //var city = lang.EditColumn("Institution Registration", "Institution Registration");
                            //ViewData["Ing"] = city.Loc_Oth == null ? "Institution Registration" : city.Loc_Oth;

                            //city = lang.EditColumn("Diocese", "Diocese Users");
                            //ViewData["duser"] = city.Loc_Oth == null ? "Diocese Users" : city.Loc_Oth;
                            //city = lang.EditColumn("Denomination", "Denomination");
                            //ViewData["nati"] = city.Loc_Oth == null ? "Denomination" : city.Loc_Oth;
                            //var city1 = lang.EditColumn("Institution Registration", "Institution Name");
                            //ViewData["InN"] = city1.Loc_Oth == null ? "Institution Name" : city1.Loc_Oth;
                            //var city2 = lang.EditColumn("Institution Registration", "Contact Person");
                            //ViewData["cp"] = city2.Loc_Oth == null ? "Contact person" : city2.Loc_Oth;
                            //var city3 = lang.EditColumn("Institution Registration", "Registration No");
                            //ViewData["RNO"] = city3.Loc_Oth == null ? "Registration No" : city3.Loc_Oth;
                            //var city4 = lang.EditColumn("Institution Registration", "Physical Address");
                            //ViewData["PDR"] = city4.Loc_Oth == null ? "Physical Address" : city4.Loc_Oth;
                            //var city5 = lang.EditColumn("Institution Registration", "Institution Website");
                            //ViewData["web"] = city5.Loc_Oth == null ? "Institution Website" : city5.Loc_Oth;
                            //var city6 = lang.EditColumn("Institution Registration", "Phone No");
                            //ViewData["phone"] = city6.Loc_Oth == null ? "Phone No" : city6.Loc_Oth;
                            //var city7 = lang.EditColumn("Institution Registration", "Bank Details");
                            //ViewData["BD"] = city7.Loc_Oth == null ? "Bank Details" : city7.Loc_Oth;
                            //var city8 = lang.EditColumn("Institution Registration", "Bank Account No");
                            //ViewData["Actno"] = city8.Loc_Oth == null ? "Bank Account No" : city8.Loc_Oth;




                            //var cm = lang.EditColumn("Comment", "Comment");
                            //ViewData["cm"] = cm.Loc_Oth == null ? "Comment" : cm.Loc_Oth;
                            ////Inst = lang.EditColumn("Bank Menu", "Institutions");
                            //ViewData["Inst"] = Inst.Loc_Oth == null ? "Institutions" : Inst.Loc_Oth;







                            //var tt = lang.EditColumn("Parish", "User Creation");
                            //ViewData["pusecre"] = tt.Loc_Oth == null ? tt.Loc_Eng : tt.Loc_Oth;
                            //tt = lang.EditColumn("Parish", "Parish Users");
                            //ViewData["puse"] = tt.Loc_Oth == null ? tt.Loc_Eng : tt.Loc_Oth;

                            //var t1 = lang.EditColumn("Notifications", "Notifications");
                            //ViewData["Notif"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Notifications", "Notification Creation");
                            //ViewData["Notcre"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Notifications", "Subject");
                            //ViewData["nSub"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Notifications", "Priority");
                            //ViewData["Npri"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Notifications", "Notify Group");
                            //ViewData["Notgrp"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Notifications", "Sum Of Offer Amount");
                            //ViewData["sumof"] = m.Loc_Oth == null ? "Sum Of Offer Amount" : m.Loc_Oth;
                            //t1 = lang.EditColumn("Notifications", "Sum Of Paid Amount");
                            //ViewData["sumpa"] = m.Loc_Oth == null ? "Sum Of Paid Amount" : m.Loc_Oth;
                            //t1 = lang.EditColumn("Notifications", "Balance");
                            //ViewData["Sbal"] = m.Loc_Oth == null ? "Balance" : m.Loc_Oth;
                            //t1 = lang.EditColumn("Notifications", "Payment Summary");
                            //ViewData["paysum"] = m.Loc_Oth == null ? "Payment Summary" : m.Loc_Oth;
                            //t1 = lang.EditColumn("Notifications", "Category");
                            //ViewData["cate"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Notifications", "Group");
                            //ViewData["grp"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Notifications", "Deadline");
                            //ViewData["dead"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Notifications", "Individual");
                            //ViewData["Indiv"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Notifications", "Notifications Approve");
                            //ViewData["NotApp"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;

                            //t1 = lang.EditColumn("SMS", "SMS Settings");
                            //ViewData["smsset"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("SMS", "Service ID");
                            //ViewData["serid"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("SMS", "Mobile Service ID");
                            //ViewData["mobserid"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("SMS", "SMS Text");
                            //ViewData["smstxt"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("SMS", "SMS English Text");
                            //ViewData["smsengtxt"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("SMS", "SMS Swahili Text");
                            //ViewData["smsswatxt"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Email Text", "Flow Id");
                            //ViewData["floid"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Email Text", "Subject English");
                            //ViewData["subeng"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Email Text", "Subject Swahili");
                            //ViewData["subswa"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;




                            //var t1 = lang.EditColumn("Userlog Report", "User Logs");
                            //ViewData["ulogs"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Userlog Report", "Ip Address");
                            //ViewData["uipadd"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Userlog Report", "User Group");
                            //ViewData["ugrps"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Userlog Report", "Login Time");
                            //ViewData["logintym"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Userlog Report", "Logout Time");
                            //ViewData["logouttym"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;
                            //t1 = lang.EditColumn("Program", "Description");
                            //ViewData["des"] = t1.Loc_Oth == null ? t1.Loc_Eng : t1.Loc_Oth;


                        }
                        else
                        {
                            ViewData["Atra"] = "Audit Trails" ;
                            ViewData["Spag"] =  "Select Page" ;
                            ViewData["Coln"] =  "Column Name" ;
                            ViewData["Oval"] =  "Old Value" ;
                            ViewData["Nval"] =  "New Value" ;
                            ViewData["PoMb"] =  "Posted/Modified By" ;
                            ViewData["Adat"] = "Audit Date";
                            ViewData["Sansw"] = "Answer";
   
                               ViewData["invamt"] = "Invoice Amount";
                            ViewData["tota"] = "Total";
                            ViewData["uselog"] = "UserLog Report";
                            ViewData["addnewr"] =  "Add New Row" ;
                            ViewData["dele"] = "Delete" ;
                            ViewData["bnkdets"] = "Bank Details";
                            ViewData["compdet"] = "Company Details";
                            ViewData["bnknam"] = "Bank Name";
                            ViewData["bnkbrch"] = "Bank Branch";
                            ViewData["accno"] = "Account No";
                            ViewData["swcod"] = "Swift Code";
                            ViewData["userdets"] = "User Details";
                            ViewData["vatperdets"] = "Vat Percentage Details";
                            ViewData["emailtdet"] = "Email Details";
                            ViewData["smtpdets"] =  "Smtp Details" ;
                            ViewData["coutdets"] = "Country Details";
                            ViewData["Regiondets"] = "Region Details" ;
                            ViewData["distdets"] =  "District Details";
                            ViewData["Warddets"] = "Ward Details" ;
                            ViewData["Desgdets"] =  "Designation Details" ;
                            ViewData["currcod"] =  "Currency Code" ;
                            ViewData["curr"] = "Currency" ;
                            ViewData["currdets"] =  "Currency Details" ;
                            ViewData["currname"] =  "Currency Name" ;
                            ViewData["addcurre"] =  "Add Currency" ;
                            ViewData["Squesdets"] =  "Security Question Details" ;
                            ViewData["ipaddr"] = "Ip Address";
                            ViewData["usegrp"] = "User Group";
                            ViewData["logtym"] = "Login Time";
                            ViewData["logouttym"] = "Logout Time";
                            ViewData["Sno"] = "Sno";
                            ViewData["Actions"] =  "Actions";
                            ViewData["Status"] =  "Status" ;
                            ViewData["date"] =  "Date" ;
                            ViewData["cmt"] =  "Comment" ;
                            ViewData["cby"] =  "Comment By" ;
                            ViewData["change"] =  "Change Password" ;
                            ViewData["ccpp"] ="Current Password" ;
                            ViewData["np"] = "New Password" ;
                            ViewData["ccp"] =  "Confirm Password" ;
                            ViewData["Sub"] =  "Submit" ;
                            ViewData["PDF"] = "PDF" ;
                            ViewData["Excel"] =  "Excel" ;
                            ViewData["Close"] =  "Close" ;
                            ViewData["Save"] = "Save" ;
                            ViewData["Create"] =  "Create" ;
                            ViewData["Update"] =  "Update" ;
                            ViewData["cout"] =  "Country" ;
                            ViewData["addcout"] =  "Add Country";
                            ViewData["Region"] =  "Region" ;
                            ViewData["addRegion"] =  "Add Region" ;
                            ViewData["District"] =  "District" ;
                            ViewData["addDistrict"] =  "Add District";
                            ViewData["Ward"] = "Ward" ;
                            ViewData["addWard"] =  "Add Ward" ;
                            ViewData["Cust"] =  "Customer" ;
                            ViewData["sdate"] =  "Start Date" ;
                            ViewData["edate"] =  "End Date" ;
                            ViewData["Desg"] = "Designation" ;
                            ViewData["addDesg"] =  "Add Designation";
                            ViewData["Sques"] = "Security Question";
                            ViewData["Sques"] =  "Question Name" ;
                            ViewData["addSques"] ="Add Security Question" ;
                            ViewData["smtp"] = "Smtp" ;
                            ViewData["frmaddr"] =  "From Address" ;
                            ViewData["addsmtp"] =  "Add Smtp" ;
                            ViewData["pwd"] =  "Password" ;
                            ViewData["sslena"] =  "SSL Enable" ;
                            ViewData["smtpadd"] = "Smtp Address" ;
                            ViewData["portnum"] = "Port Number" ;
                            ViewData["Un"] =  "User Name" ;
                            ViewData["BU"] = "Bank User" ;
                            ViewData["ffname"] =  "First Name" ;
                            ViewData["mname"] =  "Middle Name" ;
                            ViewData["lname"] =  "Last Name" ;
                            ViewData["Mno"] =  "Mobile No" ;
                            ViewData["EID"] = "Email ID" ;
                            ViewData["fname"] = "Full Name";
                            ViewData["empid"] = "Employee Id" ;
                            ViewData["adduser"] = "Add User" ;
                            ViewData["et"] = "Email Text" ;
                            ViewData["floid"] =  "Flow Id" ;
                            ViewData["subeng"] = "Subject English";
                            ViewData["subswa"] = "Subject Swahili" ;
                            ViewData["emailswa"] =  "Email Swahili Text" ;
                            ViewData["emailtxt"] =  "Add Email Text" ;
                            ViewData["BAp"] = "Approval" ;
                            ViewData["Blang"] = "Language";
                            ViewData["setup"] =  "Setup" ;
                            ViewData["BRt"] =  "Return" ;
                            ViewData["Brep"] = "Reports";
                            ViewData["dash"] =  "Dashboard";
                            ViewData["BApe"] = "Approve";
                            ViewData["Geninv"] = "Generatedinvoices";
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
                            ViewData["custname"] = "Role";
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
                        ViewData["Atra"] = "Audit Trails";
                        ViewData["Spag"] = "Select Page";
                        ViewData["Coln"] = "Column Name";
                        ViewData["Oval"] = "Old Value";
                        ViewData["Nval"] = "New Value";
                        ViewData["PoMb"] = "Posted/Modified By";
                        ViewData["Adat"] = "Audit Date";
                        ViewData["Sansw"] = "Answer";
                        ViewData["invamt"] = "Invoice Amount";
                        ViewData["tota"] = "Total";
                        ViewData["uselog"] = "UserLog Report";
                        ViewData["addnewr"] = "Add New Row";
                        ViewData["dele"] = "Delete";
                        ViewData["bnkdets"] = "Bank Details";
                        ViewData["compdet"] = "Company Details";
                        ViewData["bnknam"] = "Bank Name";
                        ViewData["bnkbrch"] = "Bank Branch";
                        ViewData["accno"] = "Account No";
                        ViewData["swcod"] = "Swift Code";
                        ViewData["userdets"] = "User Details";
                        ViewData["vatperdets"] = "Vat Percentage Details";
                        ViewData["emailtdet"] = "Email Details";
                        ViewData["smtpdets"] = "Smtp Details";
                        ViewData["coutdets"] = "Country Details";
                        ViewData["Regiondets"] = "Region Details";
                        ViewData["distdets"] = "District Details";
                        ViewData["Warddets"] = "Ward Details";
                        ViewData["Desgdets"] = "Designation Details";
                        ViewData["currcod"] = "Currency Code";
                        ViewData["curr"] = "Currency";
                        ViewData["currdets"] = "Currency Details";
                        ViewData["currname"] = "Currency Name";
                        ViewData["addcurre"] = "Add Currency";
                        ViewData["Squesdets"] = "Security Question Details";
                        ViewData["ipaddr"] = "Ip Address";
                        ViewData["usegrp"] = "User Group";
                        ViewData["logtym"] = "Login Time";
                        ViewData["logouttym"] = "Logout Time";
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
                        ViewData["Geninv"] = "Generatedinvoices";
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
                        ViewData["custname"] = "Role";
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

                    //base.OnActionExecuting(filterContext);
                }
                else
                {
                    ViewData["Atra"] = "Audit Trails";
                    ViewData["Spag"] = "Select Page";
                    ViewData["Coln"] = "Column Name";
                    ViewData["Oval"] = "Old Value";
                    ViewData["Nval"] = "New Value";
                    ViewData["PoMb"] = "Posted/Modified By";
                    ViewData["Adat"] = "Audit Date";
                    ViewData["Sansw"] = "Answer";

                    ViewData["invamt"] = "Invoice Amount";
                    ViewData["tota"] = "Total";
                    ViewData["uselog"] = "UserLog Report";
                    ViewData["addnewr"] = "Add New Row";
                    ViewData["dele"] = "Delete";
                    ViewData["bnkdets"] = "Bank Details";
                    ViewData["compdet"] = "Company Details";
                    ViewData["bnknam"] = "Bank Name";
                    ViewData["bnkbrch"] = "Bank Branch";
                    ViewData["accno"] = "Account No";
                    ViewData["swcod"] = "Swift Code";
                    ViewData["userdets"] = "User Details";
                    ViewData["vatperdets"] = "Vat Percentage Details";
                    ViewData["emailtdet"] = "Email Details";
                    ViewData["smtpdets"] = "Smtp Details";
                    ViewData["coutdets"] = "Country Details";
                    ViewData["Regiondets"] = "Region Details";
                    ViewData["distdets"] = "District Details";
                    ViewData["Warddets"] = "Ward Details";
                    ViewData["Desgdets"] = "Designation Details";
                    ViewData["currcod"] = "Currency Code";
                    ViewData["curr"] = "Currency";
                    ViewData["currdets"] = "Currency Details";
                    ViewData["currname"] = "Currency Name";
                    ViewData["addcurre"] = "Add Currency";
                    ViewData["Squesdets"] = "Security Question Details";
                    ViewData["ipaddr"] = "Ip Address";
                    ViewData["usegrp"] = "User Group";
                    ViewData["logtym"] = "Login Time";
                    ViewData["logouttym"] = "Logout Time";
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
                    ViewData["Geninv"] = "Generatedinvoices";
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
                    ViewData["custname"] = "Role";
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
               // long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.Message.ToString();
            }
            base.OnActionExecuting(filterContext); 

            //var result1 = smtp.Getinstparishcount(long.Parse(Session["InstituteID"].ToString()), Session["admin1"].ToString(), long.Parse(Session["UserID"].ToString()));
            //var a = System.Convert.ToInt32(result1);
            object rCount = 0;
            if (Session["UserID"] != null)
            {
               // rCount = noti.getBilnkbankCo(long.Parse(Session["InstituteID"].ToString()), long.Parse(Session["UserID"].ToString()));
            }

            if (rCount != null)
            {
                ViewBag.Data = rCount;
            }
            else
            {
                ViewBag.Data = "0";
            }


        }
    }
    public class LanguageController : AdminBaseController
    {
        // GET: Language
        Language lang = new Language();
        EMP_DET ed = new EMP_DET();
        CompanyUsers ch =new CompanyUsers();
        private readonly dynamic returnNull = null;
        public ActionResult Language()
        {
            try
            {

                if (Session["sessB"] == null)
                {
                    return RedirectToAction("Loginnew", "Loginnew");
                }
                ed.Detail_Id = Convert.ToInt64(Session["UserID"].ToString());
                ed.UpdateOnlyflsgtrue(ed);
            }
            catch (Exception Ex)
            {
               // long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
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
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.Message.ToString();
            }

            return returnNull;
        }
        public ActionResult Editlang(string type)
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

                }
                //else
                //{
                //    var drt2 = ch.validatelangInst(long.Parse(Session["UserID"].ToString()), long.Parse(Session["InstituteID"].ToString()));
                //    d = drt2.Loc_Name;
                //}

                if (d == "Swahili")
                {
                    return Json(d, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var returnField = new { check = "Engg" };
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
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.Message.ToString();
            }

            return returnNull;
        }
        public ActionResult Adddesg(string table, List<Language> details)
        {
            try
            {

                lang.Table_name = table;
                long ssno = 0;
                lang.Posted_By = Session["UserID"].ToString();
                for (int i = 0; i < details.Count; i++)
                {
                    lang.Loc_Eng = details[i].Loc_Eng;
                    lang.Loc_Oth = details[i].Loc_Oth;
                    lang.Loc_Sno = details[i].Loc_Sno;
                    lang.Col_name = details[i].Loc_Eng;
                    lang.UpdateLan(lang);
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
                ed.Detail_Id = long.Parse(Session["UserID"].ToString());
                ed.Loc_Name = name;
                ed.Updatelang(ed);
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