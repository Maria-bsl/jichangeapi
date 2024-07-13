using BL.BIZINVOICING.BusinessEntities.Masters;
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
    public class InvoiceRepController : ApiController
    {


        INVOICE inv = new INVOICE();
        CustomerMaster cm = new CustomerMaster();
        CompanyBankMaster cb = new CompanyBankMaster();
        private readonly dynamic returnNull = null;
        // GET: InvoiceRep
       
       
        [HttpPost]
        public HttpResponseMessage CustList(SingletonSno c)
        {
            if (ModelState.IsValid) { 
                try
                {

                    var result = inv.GetCustomers((long)c.Sno);
                    if (result != null)
                    {

                        return Request.CreateResponse(new {response = result, message ="Success"});
                    }
                    else
                    {
                        return Request.CreateResponse(new {response = 0, message ="Failed"});
                    }


                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
            }else 
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            return null;

        }


        [HttpPost]
        public HttpResponseMessage CustList1(SingletonSno c)
        {
            if (ModelState.IsValid) { 
                    try
                    {

                        var result = inv.GetCustomers111((long) c.Sno);
                        if (result != null)
                        {

                            return Request.CreateResponse(new {response = result, message ="Success"});
                        }
                        else
                        {
                            return Request.CreateResponse(new {response = 0, message ="Failed"});
                        }


                    }
                    catch (Exception Ex)
                    {
                        Ex.ToString();
                    }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            return null;
        }


        [HttpPost]
        public HttpResponseMessage GetCustDetails(SingletonSno c)
        {

            if (ModelState.IsValid) { 
                    try
                    {
                        string ash = null;
                        if (c.Sno == Convert.ToInt64(ash))
                        {
                            return Request.CreateResponse(new {response = 0, message ="Failed"});
                        }
                        else
                        {
                            var result = cm.GetCust1((long)c.Sno);
                            if (result == null)
                            {
                                int d = 0;
                                return Request.CreateResponse(new {response = 0, message ="Failed"});
                            }
                            else
                            {
                                return Request.CreateResponse(new {response = result, message ="Success"});
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                        Ex.ToString();
                    }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            return returnNull;
        }



        [HttpPost]
        public HttpResponseMessage CompList()
        {

            try
            {

                var result = cb.CompGet1();
                if (result != null)
                {

                    return Request.CreateResponse(new {response = result, message ="Success"});
                }
                else
                {
                    return Request.CreateResponse(new {response = 0, message ="Failed"});
                }


            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        [HttpPost]
        public HttpResponseMessage CompListB(BranchRef b)
        {
            if (ModelState.IsValid) { 
                try
                {

                    var result = cb.GetCompany1_Branch((long)b.branch);
                    if (result != null)
                    {

                        return Request.CreateResponse(new { response = result, message = "Success" });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = "Failed" });
                    }


                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            return null;
        }


        //[HttpPost]
        //public HttpResponseMessage CompList1()
        //{

        //    try
        //    {

        //        var result = cb.CompGet1();
        //        if (result != null)
        //        {

        //            return Request.CreateResponse(new {response = result, message ="Success"});
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(new {response = 0, message ="Failed"});
        //        }


        //    }
        //    catch (Exception Ex)
        //    {
        //        Ex.ToString();
        //    }

        //    return null;
        //}
        [HttpPost]
        public HttpResponseMessage InvList(SingletonSno c)
        {
            if (ModelState.IsValid) {   
                    try
                    {
                        string ash = null;
                        if (c.Sno == Convert.ToInt64(ash))
                        {
                            return Request.CreateResponse(new {response = c.Sno, message ="Failed"});
                        }
                        else
                        {
                            var result = inv.GetInvoiceNos((long)c.Sno);  //Cust_mas_sno ==> Sno
                            if (result == null)
                            {
                                int d = 0;
                                return Request.CreateResponse(new {response = 0, message ="Failed"});
                            }
                            else
                            {
                                return Request.CreateResponse(new {response = result, message ="Success"});
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                        Ex.ToString();
                    }
                }
                else
                {
                    var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Request.CreateResponse(new { response = 0, message = errorMessages});
                }
            return returnNull;
        }


        //[HttpPost]
        //public HttpResponseMessage InvList(long Sno)
        //{

        //    try
        //    {

        //        var result = inv.GetInvoiceNos(Sno);
        //        if (result != null)
        //        {

        //            return Request.CreateResponse(new {response = result, message ="Success"});
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(new {response = 0, message ="Failed"});
        //        }


        //    }
        //    catch (Exception Ex)
        //    {
        //        Ex.ToString();
        //    }

        //    return null;
        //}


        [HttpPost]
        public HttpResponseMessage customerList(SingletonComp c)
        {
            if (ModelState.IsValid) { 
                try
                {
                    string ash = null;
                    if (c.compid == Convert.ToInt64(ash))
                    {
                        return Request.CreateResponse(new {response = 0, message ="Failed"});
                    }
                    else
                    {
                        var result = cm.CustGet((long) c.compid);
                        if (result == null)
                        {
                            int d = 0;
                            return Request.CreateResponse(new {response = 0, message ="Failed"});
                        }
                        else
                        {
                            return Request.CreateResponse(new {response = result, message ="Success"});
                        }
                    }

                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            return null;
        }


        [HttpPost]
        public HttpResponseMessage GetInvReport(InvRepoModel i)
        {
            if (ModelState.IsValid) { 
                try
                {

                    var result = inv.GetInvRep((long)i.Comp, long.Parse(i.cusid), i.stdate, i.enddate);
                    if (result != null)
                    {

                        return Request.CreateResponse(new {response = result, message ="Success"});
                    }
                    else
                    {
                        return Request.CreateResponse(new {response = 0, message ="Failed"});
                    }


                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            return null;
        }


        [HttpPost]
        public HttpResponseMessage GetInvDetReport(InvDetRepModel m)
        {
            if (ModelState.IsValid) { 
                try
                {

                    var result = inv.GetInvDetRep((long)m.Comp, m.invs, m.stdate, m.enddate,(long) m.Cust);
                    if (result != null)
                    {

                        return Request.CreateResponse(new {response = result, message ="Success"});
                    }
                    else
                    {
                        return Request.CreateResponse(new {response = 0, message ="Failed"});
                    }


                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            return null;
        }
    }
}
