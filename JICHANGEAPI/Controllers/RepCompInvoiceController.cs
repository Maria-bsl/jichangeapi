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
    public class RepCompInvoiceController : SetupBaseController
    {

        // GET: RepCompInvoice
        INVOICE inv = new INVOICE();
        CustomerMaster cm = new CustomerMaster();
        CompanyBankMaster c = new CompanyBankMaster();
        private readonly dynamic returnNull = null;

        [HttpPost]
        public HttpResponseMessage CustList(SingletonSno c)
        {
            if (ModelState.IsValid) { 
                try
                {

                    var result = inv.GetCustomers1(long.Parse(c.Sno.ToString()));
                    if (result != null)
                    {

                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        return Request.CreateResponse(new {response = 0, message ="Failed"});
                    }


                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            //return null;
        }


        [HttpPost]
        public HttpResponseMessage CompList(SingletonSno cb)
        {
            if (ModelState.IsValid) { 
                try
                {

                    var result = c.CompGet(long.Parse(cb.Sno.ToString()));
                    if (result != null)
                    {

                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        return Request.CreateResponse(new {response = 0, message ="Failed"});
                    }


                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            //return null;
        }


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
                        //long.Parse(c.Sno.ToString())GetInvoiceNos_S(a,b)
                        var result = inv.GetInvoiceNos_(long.Parse(c.Sno.ToString()));
                        if (result == null)
                        {
                            //int d = 0;
                            return Request.CreateResponse(new {response = 0, message ="Failed"});
                        }
                        else
                        {
                            return Request.CreateResponse(new { response = result, message = new List<string> { } });
                        }
                    }
                }
                catch (Exception Ex)
                {
                    //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            //return returnNull;
        }


        [HttpPost]
        public HttpResponseMessage customerList()
        {

            try
            {

                var result = cm.CustGet();
                if (result != null)
                {

                    return Request.CreateResponse(new { response = result, message = new List<string> { } });
                }
                else
                {
                    return Request.CreateResponse(new {response = 0, message ="Failed"});
                }


            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }

            //return null;
        }

        [HttpPost]
        public HttpResponseMessage GetInvReport(InvRepoModel invRepoModel)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                INVOICE invoice = new INVOICE();
                invRepoModel.cusid = invRepoModel.cusid.ToString().ToLower() == "all" ? "0" : invRepoModel.cusid;
                invRepoModel.Comp = invRepoModel.Comp.ToString().ToLower() == "all" ? 0 : invRepoModel.Comp;
                var result = inv.GetInvRep1((long)invRepoModel.Comp, long.Parse(invRepoModel.cusid), invRepoModel.stdate, invRepoModel.enddate);
                if (result == null) { return this.GetNoDataFoundResponse();  }
                else { return this.GetSuccessResponse(result);  }
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }


        /*[HttpPost]
        public HttpResponseMessage GetInvReport(InvRepoModel i)
        { 
            
            if(i.cusid.ToString().ToLower() == "all")
                    {
                        i.cusid = "0";
                    }

            if (ModelState.IsValid) { 
                try
                {
                   
                    var result = inv.GetInvRep1((long)i.Comp, long.Parse(i.cusid), i.stdate, i.enddate);
                    if (result != null)
                    {

                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        return Request.CreateResponse(new {response = 0, message ="Failed"});
                    }

                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        }*/


        [HttpPost]
        public HttpResponseMessage GetInvDetReport(InvDetRepModel m)
        {
            if (ModelState.IsValid) { 
                try
                {

                    var result = inv.GetInvDetRep_1((long)m.Comp, m.invs.ToString(), m.stdate, m.enddate, (long)m.Cust);
                    if (result != null)
                    {

                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        return Request.CreateResponse(new {response = 0, message ="Failed"});
                    }


                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            //return null;
        }





    }
}
