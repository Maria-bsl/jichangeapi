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
    public class RegionController : SetupBaseController
    {
        private readonly RegionService regionService = new RegionService();
        Payment pay = new Payment();

        [HttpPost]
        public HttpResponseMessage GetRegionDetails()
        {
            try
            {
                var results = regionService.GetRegionsList();
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
        public HttpResponseMessage AddRegion(AddRegionForm addRegionForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {

                if (addRegionForm.sno == 0)
                {
                    REGION region = regionService.InsertRegion(addRegionForm);
                    return GetSuccessResponse(region);
                }
                else
                {
                    REGION region = regionService.UpdateRegion(addRegionForm);
                    return GetSuccessResponse(region);
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
        public HttpResponseMessage FindRegion(long sno)
        {
            try
            {
                REGION region = regionService.FindRegion(sno);
                return GetSuccessResponse(region);
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
        public HttpResponseMessage DeleteRegion(DeleteRegionForm deleteRegionForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                long deletedSno = regionService.DeleteRegion((long)deleteRegionForm.sno, (long)deleteRegionForm.userid);
                return GetSuccessResponse(deletedSno);
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