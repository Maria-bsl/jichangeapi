using BL.BIZINVOICING.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers.setup
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DepositAController : ApiController
    {
        Payment pay = new Payment();
        [HttpPost]
        public HttpResponseMessage Getdeposits()
        {
            var depositAccount = new C_Deposit();
            try
            {
                var result = depositAccount.GetAccounts();
                if (result != null)
                {
                    return Request.CreateResponse(new { response = result, message = new List<string>() });
                }
                else
                {
                    return Request.CreateResponse(new { response = new List<DESIGNATION>(), message = new List<string>() });
                }
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }
    }
}