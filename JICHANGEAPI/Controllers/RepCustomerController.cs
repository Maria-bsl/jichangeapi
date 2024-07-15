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
    public class RepCustomerController : ApiController
    {
        // GET: RepCustomer
        CustomerMaster cm = new CustomerMaster();
     

        [HttpPost]
        public HttpResponseMessage GetcustDetReport(SingletonGetCustDetRepModel c)
        {
            if (ModelState.IsValid) { 
                    try
                    {

                        var result = cm.CustGetrep(c.Comp, c.reg, c.dist);
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
