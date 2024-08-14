using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;
using JichangeApi.Services.setup;
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
    public class DistrictController : SetupBaseController
    {
        private readonly DistrictService districtService = new DistrictService();
        Payment pay = new Payment();

        [HttpPost]
        public HttpResponseMessage GetdIST()
        {
            try
            {
                List<DISTRICTS> results = districtService.GetDistrictList();
                return GetSuccessResponse(results);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddDistrict(AddDistrictForm addDistrictForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                if (addDistrictForm.sno == 0)
                {
                    DISTRICTS district = districtService.InsertDistrict(addDistrictForm);
                    return GetSuccessResponse(district);
                }
                else
                {
                    DISTRICTS district = districtService.UpdateDistrict(addDistrictForm);
                    return GetSuccessResponse(district);
                }
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage FindDistrict(long sno)
        {
            try
            {
                DISTRICTS district = districtService.FindDistrict(sno);
                return GetSuccessResponse(district);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage DeleteDist(DeleteDistrictForm deleteDistrictForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                long sno = districtService.DeleteDistrict((long)deleteDistrictForm.sno, (long)deleteDistrictForm.userid);
                return GetSuccessResponse(sno);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}