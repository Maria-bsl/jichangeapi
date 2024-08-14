using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using JichangeApi.Models.form;
using JichangeApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginUserController : SetupBaseController
    {
        private readonly LoginUserService loginUserService = new LoginUserService();
        Payment pay = new Payment();

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage AddLogins(AuthLog authLog)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                JsonObject user = loginUserService.LoginUser(authLog);
                return SuccessJsonResponse(user);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.ToString());
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Logout(MainForm mainForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                long userid = loginUserService.LogoutUser((long)mainForm.userid);
                return GetSuccessResponse(userid);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(ex.ToString());
            }
        }

    }
}
