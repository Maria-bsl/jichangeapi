using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class InvoiceController : Controller
    {
        #region Global Declrations
        // GET: Invoice
        INVOICE inv = new INVOICE();
        Company cp = new Company();
        Customers cu = new Customers();
        CURRENCY cy = new CURRENCY();
        VatPercentage vatpercetage = new VatPercentage();

        #endregion

        public ActionResult Invoice()
        {
            if(Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }

        public ActionResult Generatedinvoices()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }

        #region Get Invoice Details
        public ActionResult GetchDetails()
        {
            try
            {
                var result = inv.GetINVOICEMas().Where(x => x.approval_status != "2");
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        #endregion

        #region Get Signed Invoices
        public ActionResult GetSignedDetails()
        {
            try
            {
                var result = inv.GetINVOICEMas().Where(x=>x.approval_status=="2");
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        #endregion


        #region Get invoice by Id
        public ActionResult GetInvoiceDetailsbyid(int invid)
        {
            try
            {
                var result = inv.GetINVOICEMas().Where(x => x.Inv_Mas_Sno == invid).FirstOrDefault(); 
               if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }

        public ActionResult GetInvoiceInvoicedetails(int invid)
        {
            try
            {
                var result = inv.GetInvoiceDetails(invid);
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }

        #endregion

        #region     Get Drodown Master Values
        public ActionResult Getcompany()
        {
            try
            {
                var result = cp.GetCompanyMas();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

             return null;
        }

        public ActionResult GetVatPer()
        {
            try
            {
                var result = vatpercetage.GetVatPercentage();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        
        public ActionResult GetCustomers()
        {
            try
            {
                var result = cu.GetCustomers();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        public ActionResult GetCurrency()
        {
            try
            {
                var result = cy.GetCURRENCY();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        #endregion

        public ActionResult GetInvNo(string invno,int invmasno)
            {
            try
            {
                var result = inv.GetINVOICEMas().Where(x=>x.Invoice_No== invno && x.Inv_Mas_Sno!= invmasno).FirstOrDefault();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }

        #region Save Update Invoice
        [HttpPost]
        public ActionResult AddInvoice(string invno, string auname, string date, long chus, long comno, string ccode,string ctype,string cino,
            string twvat, string vtamou, string total, string Inv_remark,int lastrow, List<INVOICE> details, long sno, string warrenty, string goods_status, string delivery_status)
        {
            try
            {
                DateTime dates;
                if(sno==0)
                   dates = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                else
                    dates = DateTime.ParseExact(date, "MM/dd/yyyy", null);


                inv.Invoice_No = invno;
                inv.Invoice_Date = Convert.ToDateTime(dates);
                inv.Com_Mas_Sno = comno;
                inv.Chus_Mas_No = chus;
                inv.Currency_Code = ccode;
                inv.Total_Without_Vt = Decimal.Parse(twvat);
                inv.Vat_Amount = Decimal.Parse(vtamou);
                inv.Total = Decimal.Parse(total);
                inv.Inv_Remarks = Inv_remark;
                inv.warrenty = warrenty;
                inv.goods_status = goods_status;
                inv.delivery_status = delivery_status;
                inv.Customer_ID_Type = ctype;
                inv.Customer_ID_No = cino;
                inv.AuditBy = Session["UserID"].ToString();
                long ssno = 0;

                if (sno == 0)
                {
                    
                    ssno = inv.Addinvoi(inv);
                     for (int i = 0; i < details.Count; i++)
                    {
                        if (details[i].Inv_Mas_Sno == 0)
                        {
                            inv.Inv_Mas_Sno = ssno;
                            inv.Item_Description = details[i].Item_Description;
                            inv.Item_Qty = details[i].Item_Qty;
                            inv.Item_Unit_Price = details[i].Item_Unit_Price;
                            inv.Item_Total_Amount = details[i].Item_Total_Amount;
                            inv.Vat_Percentage = details[i].Vat_Percentage;
                            inv.Vat_Amount = details[i].Vat_Amount;
                            inv.Item_Without_vat = details[i].Item_Without_vat;
                            inv.Remarks = details[i].Remarks;
                            inv.vat_category = details[i].vat_category;
                            inv.Vat_Type = details[i].Vat_Type;
                            inv.AddInvoiceDetails(inv);
                        }
                    }

                }

                else if (sno > 0)
                {

                    inv.Inv_Mas_Sno = sno;
                    inv.UpdateInvoiMas(inv);
                    inv.DeleteInvoicedet(inv);
                    for (int i = 0; i < details.Count; i++)
                    {
                        if (details[i].Inv_Mas_Sno == 0)
                        {
                            inv.Inv_Mas_Sno = sno;
                            inv.Item_Description = details[i].Item_Description;
                            inv.Item_Qty = details[i].Item_Qty;
                            inv.Item_Unit_Price = details[i].Item_Unit_Price;
                            inv.Item_Total_Amount = details[i].Item_Total_Amount;
                            inv.Vat_Percentage = details[i].Vat_Percentage;
                            inv.Vat_Amount = details[i].Vat_Amount;
                            inv.Item_Without_vat = details[i].Item_Without_vat;
                            inv.Remarks = details[i].Remarks;
                            inv.vat_category = details[i].vat_category;
                            inv.AddInvoiceDetails(inv);
                        }
                    }
                    ssno = sno;
                }
                var result1 = ssno;
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return null;
        }
        #endregion

    }
}