using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EPS.API.Models;
using BL.BIZINVOICING.BusinessEntities.Common;
using BL.BIZINVOICING.BusinessEntities.Masters;
using BL.BIZINVOICING.BusinessEntities.ConstantFile;

namespace EPS.API.Controllers
{
    public class PaymentDetailsController : ApiController
    {
        private static readonly IPaymentDetailsRepository paymentDetailsRepository = new PaymentDetailsRepository();

        /// <summary>
        /// Add paymennt details.
        /// </summary>
        /// <param name="serviceUserDetails"></param>
        /// <returns></returns>
        //[HttpGet]
        //[ActionName("Index")]
        public HttpResponseMessage GetIsAuthenticatedInvoice(long paymentReference, string token)
        {
            #region "Variables"
            //HttpError error = null;
            InvoiceDetails paymentDetails = new InvoiceDetails();
            IEnumerable<InvoiceDetails> lstUserDetails = null;
            long uid = 0;
            string sCode = "";
            #endregion

            #region "Authenticate Token"
            /*if ((Request.Headers.GetValues("Token") != null && !ServiceUtility.AuthenticateToken(Request.Headers.GetValues("Token").FirstOrDefault())) || Request.Headers.GetValues("Token") == null)
            {
                //error = new HttpError(ServiceUtility.Authentication);
                sCode = ServiceUtility.Authentication;
                var Error = new
                {
                    statusCode = sCode

                };
                return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
            }*/
            #endregion

            #region "Invoke service if Token is valid"

            uid = paymentDetailsRepository.GetIsAuthenticatedInvoice(paymentReference, token);
            if (uid == 0) //incorrect login credentials
            {
                //error = new HttpError(ServiceUtility.Login);
                sCode = ServiceUtility.Login;
                var Error = new
                {
                    statusCode = sCode

                };
                return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
            }
            else if (uid == 2) //incorrect login credentials
            {
                //error = new HttpError(ServiceUtility.Login);
                sCode = ServiceUtility.Authentication;
                var Error = new
                {
                    statusCode = sCode

                };
                return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
            }
            else if (uid == 3) //incorrect login credentials
            {
                //error = new HttpError(ServiceUtility.Login);
                sCode = ServiceUtility.PaidAlready;
                var Error = new
                {
                    statusCode = sCode

                };
                return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
            }
            else //correct login credentials
            {
                //Get User Details
                //lstUserDetails = paymentDetailsRepository.GetInvoiceDetails(paymentReference);
                if (lstUserDetails == null)
                {
                    //error = new HttpError(ServiceUtility.Get );
                    sCode = ServiceUtility.Get;
                    var Error = new
                    {
                        statusCode = sCode

                    };
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, lstUserDetails);
                }
            }

            #endregion
        }
        [HttpPost]
        public HttpResponseMessage AddPayment(PaymentDetails servicePaymentDetails)
        {
            try
            {
                APIResponse<PaymentReceipt> apiResponse;
                PaymentReceipt preceipt = new PaymentReceipt();
                #region "Variables"
                //HttpError error = null;
                string sCode = "";
                PaymentDetails paymentDetails = new PaymentDetails();
                #endregion

                #region "Authenticate Token"
                
                #endregion

                #region "Invoke service if Token is valid"

                preceipt.receipt = paymentDetailsRepository.PostAddPayment(servicePaymentDetails);
                if (preceipt.receipt == -1)
                {
                    
                    APIResponse<PaymentDetails> apiResponse1 = new APIResponse<PaymentDetails>(APIResultCode.UnknownError, Utilites.GetEnumDescription((APIResultCode)APIResultCode.UnknownError));
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, apiResponse1);
                }
                else if (preceipt.receipt == 1)
                {
                    //error = new HttpError(ServiceUtility.ApplicationorInvoiceNotExist);
                    sCode = ServiceUtility.ApplicationorInvoiceNotExist;
                    var Error = new
                    {
                        status = sCode

                    };

                    return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
                }
                else if (preceipt.receipt == 2)
                {
                    //error = new HttpError(ServiceUtility.PaidAlready);
                    sCode = ServiceUtility.PaidAlready;
                    var Error = new
                    {
                        status = sCode

                    };
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
                }
                
                else if (paymentDetails.UserId == 4)
                {
                    //error = new HttpError(ServiceUtility.PaidAlready);
                    sCode = ServiceUtility.UnKnownSource;
                    var Error = new
                    {
                        status = sCode

                    };
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
                }
                else if (preceipt.receipt == 5)
                {
                    //error = new HttpError(ServiceUtility.TNO);
                    sCode = ServiceUtility.Authentication;
                    var Error = new
                    {
                        status = sCode

                    };
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
                }
               
                else
                {
                    
                    apiResponse = new APIResponse<PaymentReceipt>(preceipt);
                    apiResponse.status = APIResultCode.Ok;
                    apiResponse.statusDesc = Utilites.GetEnumDescription((APIResultCode)APIResultCode.Ok);
                    return Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                }

                #endregion
            }
            catch(Exception ex)
            {
                APIResponse<PaymentDetails> apiResponse = new APIResponse<PaymentDetails>(APIResultCode.UnknownError, Utilites.GetEnumDescription((APIResultCode)APIResultCode.UnknownError));
                return Request.CreateResponse(HttpStatusCode.InternalServerError, apiResponse);
            }
        }
        
        [HttpPost]
        public HttpResponseMessage GetDetails(PaymentDetails servicePaymentDetails)
        {

            #region "Variables"
            APIResponse<InvoiceDetails> apiResponse;
            //HttpError error = null;
            string sCode = "";
            PaymentDetails paymentDetails = new PaymentDetails();
            InvoiceDetails invoiceDetails = null;
            #endregion

            #region "Authenticate Token"
            #endregion

            #region "Invoke service if Token is valid"
            paymentDetails.UserId = paymentDetailsRepository.PostGetDetails(servicePaymentDetails);
            if (paymentDetails.UserId == -1)
            {
                //error = new HttpError(ServiceUtility.Add);//"Payment Details"
                /*sCode = ServiceUtility.Add;
                var Error = new
                {
                    status = sCode

                };
                return Request.CreateResponse(HttpStatusCode.BadRequest, Error);*/
                APIResponse<PaymentDetails> apiResponse1 = new APIResponse<PaymentDetails>(APIResultCode.UnknownError, Utilites.GetEnumDescription((APIResultCode)APIResultCode.UnknownError));
                return Request.CreateResponse(HttpStatusCode.InternalServerError, apiResponse1);
            }
            else if (paymentDetails.UserId == 1)
            {
                //error = new HttpError(ServiceUtility.ApplicationorInvoiceNotExist);
                sCode = ServiceUtility.ApplicationorInvoiceNotExist;
                var Error = new
                {
                    status = sCode

                };

                return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
            }
            else if (paymentDetails.UserId == 5)
            {
                //error = new HttpError(ServiceUtility.PaidAlready);
                sCode = ServiceUtility.Authentication;
                var Error = new
                {
                    status = sCode

                };
                return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
            }
            else if (paymentDetails.UserId == 4)
            {
                //error = new HttpError(ServiceUtility.PaidAlready);
                sCode = ServiceUtility.UnKnownSource;
                var Error = new
                {
                    status = sCode

                };
                return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
            }
            else if (paymentDetails.UserId == 3)
            {
                //error = new HttpError(ServiceUtility.PDate);
                sCode = ServiceUtility.PaidAlreadyV;
                var Error = new
                {
                    statusCode = sCode

                };
                return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
            }
            /*else if (paymentDetails.UserId == 7)
            {
                //error = new HttpError(ServiceUtility.PAmount);
                sCode = ServiceUtility.PAmount;
                var Error = new
                {
                    statusCode = sCode

                };
                return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
            }
            else if (paymentDetails.UserId == 4)
            {
                //error = new HttpError(ServiceUtility.RNo);
                sCode = ServiceUtility.RNo;
                var Error = new
                {
                    statusCode = sCode

                };
                return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
            }*/
            
            else
            {
                invoiceDetails = paymentDetailsRepository.GetInvoiceDetails(servicePaymentDetails);
                if (invoiceDetails == null)
                {
                    //error = new HttpError(ServiceUtility.Get );
                    sCode = ServiceUtility.Get;
                    var Error = new
                    {
                        status = sCode

                    };
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Error);
                }
                else
                {
                    apiResponse = new APIResponse<InvoiceDetails>(invoiceDetails);
                    apiResponse.status = APIResultCode.Ok;
                    apiResponse.statusDesc = Utilites.GetEnumDescription((APIResultCode)APIResultCode.Ok);
                    return Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                    //return Request.CreateResponse(HttpStatusCode.OK, lstUserDetails);
                }
            }

            #endregion
        }
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}