using BL.BIZINVOICING.BusinessEntities.Common;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SetupController : SetupBaseController
    {

        // GET: Setup
        CompanyBankMaster co = new CompanyBankMaster();
        CustomerMaster cm = new CustomerMaster();
        INVOICE innn = new INVOICE();
        Payment pay = new Payment();
        EMP_DET emp = new EMP_DET();
        InvoicePDfData ipd = new InvoicePDfData();
        private readonly dynamic returnNull = null;


        #region Setup For Web
       /* [HttpPost]
        public HttpResponseMessage Setup()
        {

            if (Session["branch"] != null)
            {
                var countcomp = co.GetCompanycount();
               

                var countPcomp = co.GetCompanyPencount();
                
                var idleC = 0;
                var ApprovedInvoices = 0;
                var PendingInvoices = 0;
                var CancelInvoices = 0;
                var resu = co.ActiveC();
                if (resu != null)
                {
                    idleC = resu.Count();
                }
                var acc = idleC.ToString();
                var acci = countcomp - idleC;
                var result = innn.GetINVOICEMas1().Where(x => x.approval_status == "2" && x.approval_status != "Cancel");
                if (result != null)
                {
                    ApprovedInvoices = result.Count();
                }
                var trans = ApprovedInvoices.ToString();

                var result1 = innn.GetINVOICEMas1().Where(x => x.approval_status == null);
                if (result1 != null)
                {
                    PendingInvoices = result1.Count();
                }
                var transc = PendingInvoices.ToString();

                var result2 = innn.GetINVOICEMas1().Where(x => x.approval_status == "2");
                if (result2 != null)
                {
                    CancelInvoices = result2.Count();
                }
                var trans2 = CancelInvoices.ToString();

                var uCount = emp.GetCount();
                var user = uCount;

                var tDay = innn.GetCount_D("today");
                var today = tDay;
                var tWeek = innn.GetCount_D("week");
                var week = tWeek;
                var tMon = innn.GetCount_D("mnth");
                var month = tMon;
                var tYear = innn.GetCount_D("year");
                var year = tYear;

                var accC = countcomp;
                long count = 0;
                var getCon = pay.GetControl_Dash();
                if (getCon != null)
                {

                    long amount = 0;
                    long ramount = 0;
                    for (int i = 0; i < getCon.Count; i++)
                    {
                        var getC = pay.GetPayment_Dash(getCon[i].Control_No);
                        if (getC != null)
                        {
                            amount = getC.Sum(x => x.Amount);
                            ramount = getC.Sum(x => x.Requested_Amount);
                            if (amount == ramount)
                            {
                                count = count + 1;
                            }
                        }
                    }
                }
                var ct = count;
                var it = ApprovedInvoices - count;

                CompanyData cd = new CompanyData();
                var getComp = co.GetCompany_S();
                if (getComp != null)
                {
                    cd.CompanyItemlist = co.GetCompany_S();
                }
                else
                {
                    cd.CompanyItemlist = null;
                }
                //cd.CompanyItemlist = co.GetCompany_S();
                if (desig.ToString() == "Administrator")
                {
                    cd.CompanyItemlist = co.GetCompany_S();
                }
                else
                {
                    cd.CompanyItemlist = co.GetCompany_S(long.Parse(branch.ToString()));
                }
                //return View(cd);
                string storPendingInvoicesreg = string.Empty; var storeregname = ""; string countRegwisecomp = string.Empty;
                var listreg = co.Compregwiselist();
                for (var i = 0; i < listreg.Count(); i++)
                {
                    countRegwisecomp = co.GetCompanyRegwisedefaultcount(userid.ToString(), listreg[i].RegId).ToString();
                    if (string.IsNullOrEmpty(storPendingInvoicesreg))
                    {
                        storPendingInvoicesreg = "\"" + countRegwisecomp + "\"";
                    }
                    else
                    {
                        storPendingInvoicesreg += ", " + "\"" + countRegwisecomp + "\"";
                    }

                    storeregname += "'" + listreg[i].RegName + "'" + ",";
                }
                var regname = storeregname.TrimEnd(',');
                storPendingInvoicesreg = storPendingInvoicesreg.Replace("\"", "");
                var countreg = storPendingInvoicesreg;

               *//* ViewBag.regnameli = regname;
                ViewBag.regcountli = countreg;
                ViewData["regnam"] = regname;*/

                /*var result = t.Gettoken();

                var credate = result.Created_Date;
                var expsec = result.Expire_In;*/
               /*int secs = int.Parse(expsec);
                int expmin = secs / 60;
                var todayl = DateTime.Now;
                var diffinmin = today - credate;
                var mini = diffinmin.Minutes;
                if (expmin >= mini)
                {
                    ViewBag.expireddate = "Active";
                }
                else
                {
                    ViewBag.expireddate = "InActive";
                }*//*

            }
            else
            {

                var date = DateTime.Now;
                //var countcust = cm.GetCustcount(long.Parse(company.compid.ToString()), date);
                //var countinv = innn.Getinvcount(long.Parse(company.compid.ToString()), date);
                //var countinvapp = innn.Getinvcountnlyapp(long.Parse(company.compid.ToString()), date);
                //var totamtwithvat = innn.Gettotamtwithvat(long.Parse(company.compid.ToString()), date);
                //var totamtwithoutvat = innn.Gettotamtwithoutvat(long.Parse(company.compid.ToString()), date);
                //var totvat = innn.Gettotvat(long.Parse(company.compid.ToString()), date);

                var Aa = "A"; var Bb = "B"; var Cc = "C"; var Dd = "D"; var Ee = "E";
                var bycategoryA = innn.GetCancelInvoices(long.Parse(company.compid.ToString()), Aa, date);
                var bycategoryB = innn.GetBcount(long.Parse(company.compid.ToString()), Bb, date);
                var bycategoryC = innn.GetCcount(long.Parse(company.compid.ToString()), Cc, date);
                var bycategoryE = innn.GetPendingInvoices(long.Parse(company.compid.ToString()), Ee, date);
                var bycategoryD = innn.GetDcount(long.Parse(company.compid.ToString()), Dd, date);


                ViewData["bycata"] = bycategoryA;
                ViewData["bycatb"] = bycategoryB;
                ViewData["bycatc"] = bycategoryC;
                ViewData["bycate"] = bycategoryE;
                ViewData["bycatd"] = bycategoryD;


                //ViewData["CustC"] = countcust;
                //ViewData["inv"] = countinv;
                //ViewData["invapp"] = countinvapp;
                //ViewData["totwithvat"] = totamtwithvat;
                //ViewData["totwithoutvat"] = totamtwithoutvat;
                //ViewData["totvat"] = totvat;

                var ApprovedInvoices = innn.GetCount_C(long.Parse(company.compid.ToString()));
                //ViewData["TRANS"] = ApprovedInvoices;
                long count = 0;
                long pi = 0;
                var getCon = pay.GetControl_Dash_C(long.Parse(company.compid.ToString()));
                if (getCon != null)
                {

                    long amount = 0;
                    long ramount = 0;
                    for (int i = 0; i < getCon.Count; i++)
                    {
                        var getC = pay.GetPayment_Dash(getCon[i].Control_No);
                        if (getC != null)
                        {
                            amount = getC.Sum(x => x.Amount);
                            ramount = getC.Sum(x => x.Requested_Amount);
                            if (amount == ramount)
                            {
                                count = count + 1;
                            }
                        }
                    }
                    pi = getCon.Count;
                }
                var getP = innn.GetINVOICEMas_D(long.Parse(company.compid.ToString()));
                if (getP != null)
                {
                    pi = getP.Count;
                }
                var dInv = innn.GetINVOICEMas_Pen(long.Parse(company.compid.ToString()));
                if (dInv != null)
                {
                    count = dInv.Count;
                }
                ViewData["PI1"] = count;
                ViewData["DI1"] = count;
                ViewData["PaI"] = pi;
                var iPendingInvoices = innn.GetExpired_C(long.Parse(company.compid.ToString()));
                ViewData["IE1"] = iPendingInvoices;
                var iCcount = cm.GetCustCount_C(long.Parse(company.compid.ToString()));
                ViewData["CUST1"] = iCcount;
                CompanyData id = new CompanyData();
                id.InvoiceItemlist = innn.GetControl_D(long.Parse(company.compid.ToString()));
                return View(id);

            }


            //return View();
        }

*/
        #endregion


        [HttpPost]
        public HttpResponseMessage Overview([FromBody] RequestSetupModel request)
        {
            if (request == null)
            {
                return GetServerErrorResponse("Request Can not be null");
            }

            if (!string.IsNullOrEmpty(request.branch.ToString()))
            {
                var Bankbranch = request.branch;
                if (Bankbranch == 0)
                {
                    var vendorCount = co.GetCompanycount();
                    var VendorPendingCount = co.GetCompanyPencount();

                    var ActiveCompanyWithInvoices = 0;
                    var ApprovedInvoices = 0;
                    var PendingInvoices = 0;
                    var CancelInvoices = 0;
                    var CompanyWithInvoices = co.ActiveC();
                    if (CompanyWithInvoices != null)
                    {
                        ActiveCompanyWithInvoices = CompanyWithInvoices.Count();
                    }
                    var ActiveCompany = ActiveCompanyWithInvoices.ToString();
                    var CompanyWithoutInvoices = vendorCount - ActiveCompanyWithInvoices;
                    /* Invoice */
                    var result = innn.GetINVOICEMas1().Where(x => x.approval_status == "2" && x.approval_status != "Cancel");
                    if (result != null)
                    {
                        ApprovedInvoices = result.Count();
                    }
                    var ApprovedInvoicesCount = ApprovedInvoices.ToString();

                    var result1 = innn.GetINVOICEMas1().Where(x => x.approval_status == "1");
                    if (result1 != null)
                    {
                        PendingInvoices = result1.Count();
                    }
                    var PendingInvoicesCount = PendingInvoices.ToString();

                    var result2 = innn.GetINVOICEMas1().Where(x => x.approval_status == "Cancel");
                    if (result2 != null)
                    {
                        CancelInvoices = result2.Count();
                    }
                    var CancelInvoicesCount = CancelInvoices.ToString();

                    var BankUser = emp.GetCount();
                    var BankUserCount = BankUser;

                    var tDay = innn.GetCount_D("today");
                    var today = tDay;
                    var tWeek = innn.GetCount_D("week");
                    var week = tWeek;
                    var tMon = innn.GetCount_D("mnth");
                    var month = tMon;
                    var tYear = innn.GetCount_D("year");
                    var year = tYear;

                    var VendorTotalCount = vendorCount;
                    long count = 0;
                    var getCon = pay.GetControl_Dash();
                    if (getCon != null)
                    {

                        long amount = 0;
                        long ramount = 0;
                        for (int i = 0; i < getCon.Count; i++)
                        {
                            var getC = pay.GetPayment_Dash(getCon[i].Control_No);
                            if (getC != null)
                            {
                                amount = getC.Sum(x => x.Amount);
                                ramount = getC.Sum(x => x.Requested_Amount);
                                if (amount == ramount)
                                {
                                    count++;
                                }
                            }
                        }
                    }
                    var PaymentTransactionCount = count;
                    var UnpaidInvoices = ApprovedInvoices - count;

                    CompanyData cd = new CompanyData();
                    var getComp = co.GetCompany_S();
                    if (getComp != null)
                    {
                        cd.CompanyItemlist = co.GetCompany_S();
                    }
                    else
                    {
                        cd.CompanyItemlist = null;
                    }

                    var statistics = new List<ItemListModel>
                    {
                        new ItemListModel { Name = "Transaction", Statistic = PaymentTransactionCount.ToString() },
                        new ItemListModel { Name = "Customer", Statistic = "1" },
                        new ItemListModel { Name = "Users", Statistic = BankUserCount.ToString() },
                        new ItemListModel { Name = "Pendings", Statistic = PendingInvoicesCount },
                        new ItemListModel { Name = "Due", Statistic = "1" },
                        new ItemListModel { Name = "Expired", Statistic = "1" }
                    };

                    var response = new ItemListModelResponse
                    {
                        Response = statistics,
                        Message = "Success"
                    };
                    return GetSuccessResponse(response);
                }

                // Pass Branch To get specific Branch Data here below

                return GetSuccessResponse(0);
            }
            else if (request.compid.HasValue)
            {

                    SingletonComp company = new SingletonComp();
                    var date = DateTime.Now;

                    var ApprovedInvoices = innn.GetCount_C(long.Parse(company.compid.ToString()));
                    //ViewData["TRANS"] = ApprovedInvoices;
                    long count = 0;
                    long pi = 0;
                    var getCon = pay.GetControl_Dash_C(long.Parse(company.compid.ToString()));
                    if (getCon != null)
                    {

                        long amount = 0;
                        long ramount = 0;
                        for (int i = 0; i < getCon.Count; i++)
                        {
                            var getC = pay.GetPayment_Dash(getCon[i].Control_No);
                            if (getC != null)
                            {
                                amount = getC.Sum(x => x.Amount);
                                ramount = getC.Sum(x => x.Requested_Amount);
                                if (amount == ramount)
                                {
                                    count++;
                                }
                            }
                        }
                        pi = getCon.Count;
                    }
                    var getP = innn.GetINVOICEMas_D(long.Parse(company.compid.ToString()));
                    if (getP != null)
                    {
                        pi = getP.Count;
                    }
                    var dInv = innn.GetINVOICEMas_Pen(long.Parse(company.compid.ToString()));
                    if (dInv != null)
                    {
                        count = dInv.Count;
                    }
                    var pi1 = count;
                    var di1 = count;
                    var pa1 = pi;
                    var iPendingInvoices = innn.GetExpired_C(long.Parse(company.compid.ToString()));
                    var ie = iPendingInvoices;
                    var iCcount = cm.GetCustCount_C(long.Parse(company.compid.ToString()));
                    var cust1 = iCcount;
                    CompanyData id = new CompanyData();
                    id.InvoiceItemlist = innn.GetControl_D(long.Parse(company.compid.ToString()));




                    var statistics = new List<ItemListModel>
                    {
                        new ItemListModel { Name = "Transaction", Statistic = "1" },
                        new ItemListModel { Name = "Customer", Statistic = "1" },
                        new ItemListModel { Name = "Users", Statistic = "1" },
                        new ItemListModel { Name = "Pendings", Statistic = "1" },
                        new ItemListModel { Name = "Due", Statistic = "1" },
                        new ItemListModel { Name = "Expired", Statistic = "1" }
                    };

                    var response = new ItemListModelResponse
                    {
                        Response = statistics,
                        Message = "Success"
                    };
                    return GetSuccessResponse(response);

               
            }
            else
            {
                return GetServerErrorResponse("Invalid request: Body parameter must be provided.");
            }

        }


        #region Comment Endpoint from Web
        /*[HttpPost]
        public HttpResponseMessage Getonclickday(string name)
        {

            try
            {

                var countcust1 = cm.GetCustcountind(long.Parse(company.compid.ToString()), name);
                var countinv1 = innn.Getinvcountind(long.Parse(company.compid.ToString()), name);
                var countinvapp1 = innn.Getinvcountnlyappind(long.Parse(company.compid.ToString()), name);
                var totamtwithvat1 = innn.Gettotamtwithvatind(long.Parse(company.compid.ToString()), name);
                var totamtwithoutvat1 = innn.Gettotamtwithoutvatind(long.Parse(company.compid.ToString()), name);
                var totvat1 = innn.Gettotvatind(long.Parse(company.compid.ToString()), name);

                var countcust = cm.GetCustcountind(long.Parse(company.compid.ToString()), name);
                var countinv = innn.Getinvcountind(long.Parse(company.compid.ToString()), name);
                var countinvapp = innn.Getinvcountnlyappind(long.Parse(company.compid.ToString()), name);
                var totamtwithvat = innn.Gettotamtwithvatind(long.Parse(company.compid.ToString()), name);
                var totamtwithoutvat = innn.Gettotamtwithoutvatind(long.Parse(company.compid.ToString()), name);
                var totvat = innn.Gettotvatind(long.Parse(company.compid.ToString()), name);
                ViewData["CustC"] = "";
                ViewData["inv"] = "";
                ViewData["invapp"] = "";
                ViewData["totwithvat"] = "";
                ViewData["totwithoutvat"] = "";
                ViewData["totvat"] = "";

                var Aa = "A"; var Bb = "B"; var Cc = "C"; var Dd = "D"; var Ee = "E";
                var bycategoryA1 = innn.GetCancelInvoices1(long.Parse(company.compid.ToString()), Aa, name);
                var bycategoryB1 = innn.GetBcount1(long.Parse(company.compid.ToString()), Bb, name);
                var bycategoryC1 = innn.GetCcount1(long.Parse(company.compid.ToString()), Cc, name);
                var bycategoryE1 = innn.GetPendingInvoices1(long.Parse(company.compid.ToString()), Ee, name);
                var bycategoryD1 = innn.GetDcount1(long.Parse(company.compid.ToString()), Dd, name);

                ViewData["catA1"] = bycategoryA1;
                ViewData["catB1"] = bycategoryB1;
                ViewData["catC1"] = bycategoryC1;
                ViewData["catD1"] = bycategoryD1;
                ViewData["catE1"] = bycategoryE1;

                //return null;
                var dat = new
                {
                    cust = countcust,
                    inv = countinv,
                    invapp = countinvapp,
                    amtwitvat = totamtwithvat,
                    witoutvat = totamtwithoutvat,
                    tvat = totvat,
                    catA = bycategoryA1,
                    catB = bycategoryB1,
                    catC = bycategoryC1,
                    catD = bycategoryD1,
                    catE = bycategoryE1
                };
                if (dat != null)
                {
                    return Json(dat, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var d = 0;
                    return Json(d, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public HttpResponseMessage Getonclickdefaultcompany()
        {

            try
            {

                var date = DateTime.Now;
                var countcust = cm.GetCustcount(long.Parse(company.compid.ToString()), date);
                var countinv = innn.Getinvcount(long.Parse(company.compid.ToString()), date);
                var countinvapp = innn.Getinvcountnlyapp(long.Parse(company.compid.ToString()), date);
                var totamtwithvat = innn.Gettotamtwithvat(long.Parse(company.compid.ToString()), date);
                var totamtwithoutvat = innn.Gettotamtwithoutvat(long.Parse(company.compid.ToString()), date);
                var totvat = innn.Gettotvat(long.Parse(company.compid.ToString()), date);



                //return null;
                var dat = new
                {
                    cust = countcust,
                    inv = countinv,
                    invapp = countinvapp,
                    amtwitvat = totamtwithvat,
                    witoutvat = totamtwithoutvat,
                    tvat = totvat
                };
                if (dat != null)
                {
                    return Json(dat, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var d = 0;
                    return Json(d, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }
        public HttpResponseMessage Getonclick(string name)
        {

            try
            {
                string storPendingInvoicesreg = string.Empty; var storeregname = ""; string countRegwisecomp = string.Empty;
                var listreg = co.Compregwiselist();
                for (var i = 0; i < listreg.Count(); i++)
                {
                    //var mnthoryrly = name;
                    countRegwisecomp = co.GetCompanyRegwisPendingInvoices(Session["UserID"].ToString(), listreg[i].RegId, name).ToString();
                    if (string.IsNullOrEmpty(storPendingInvoicesreg))
                    {
                        storPendingInvoicesreg = "\"" + countRegwisecomp + "\"";
                    }
                    else
                    {
                        storPendingInvoicesreg += ", " + "\"" + countRegwisecomp + "\"";
                    }

                    storeregname += listreg[i].RegName + ",";
                }
                var regname = storeregname.TrimEnd(',');
                storPendingInvoicesreg = storPendingInvoicesreg.Replace("\"", "");
                var countreg = storPendingInvoicesreg;
                ViewBag.regnameli = regname;
                ViewBag.regcountli = countreg;
                ViewData["regnam"] = regname;
                var dat = new
                {
                    name1 = regname,
                    countregi = countreg,

                };
                if (dat != null)
                {
                    return Json(dat, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var d = 0;
                    return Json(d, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }*/
        //public  HttpResponseMessage Company()
        //{

        //    var Count = new { countcomp };
        //    return Json(Count, JsonRequestBehavior.AllowGet);

        //}

        //[HttpPost]
        //public HttpResponseMessage Gettoken()
        //{

        //    try
        //    {
        //        var result = t.GetSMTPS();
        //        if (result != null)
        //        {
        //            return Json(result, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            var d = 0;
        //            return Json(d, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Ex.ToString();
        //    }

        //    return returnNull;
        //}

        #endregion


        [HttpPost]
        public HttpResponseMessage Invoices()
        {

            /*  For Invoice: Transaction, Invoice Approved, Invoice Pending, Invoice_Cancel */



            var statistics = new List<ItemListModel>
                    {
                        new ItemListModel { Name = "Transaction", Statistic = "1" },
                        new ItemListModel { Name = "Invoice Approved", Statistic = "1" },
                        new ItemListModel { Name = "Invoice Pending", Statistic = "1" },
                        new ItemListModel { Name = "Invoice Cancel", Statistic = "1" },
                    };

            var response = new ItemListModelResponse
            {
                Response = statistics,
                Message = "Success"
            };

            return null;
        }



    }
}
