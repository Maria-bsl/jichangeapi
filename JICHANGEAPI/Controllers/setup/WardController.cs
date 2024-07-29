using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;
using JichangeApi.Services.setup;

namespace JichangeApi.Controllers.setup
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WardController : SetupBaseController
    {
        private readonly WardService wardService = new WardService();

        [HttpPost]
        public HttpResponseMessage GetWard()
        {
            WARD ward = new WARD();
            try
            {
                List<WARD> wards = wardService.GetWardList();
                return GetSuccessResponse(wards);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddWard(AddWardForm addWardForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                if (addWardForm.sno == 0)
                {
                    WARD ward = wardService.InsertWard(addWardForm);
                    return GetSuccessResponse(ward);
                }
                else
                {
                    WARD ward = wardService.UpdateWard(addWardForm);
                    return GetSuccessResponse(ward);    
                }
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage FindWard(long sno)
        {
            try
            {
                WARD ward = wardService.FindWard(sno);
                return GetSuccessResponse(ward);
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage DeleteWard(RemoveWardForm removeWardForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                long deletedWard = wardService.DeleteWard((long)removeWardForm.sno, (long)removeWardForm.userid);
                return GetSuccessResponse(deletedWard);
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}