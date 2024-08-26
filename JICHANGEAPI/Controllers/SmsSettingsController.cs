using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SmsSettingsController : SetupBaseController
    {
        private readonly SmsSettingsService smsSettingsService = new SmsSettingsService();
        private readonly Payment pay = new Payment();
        public HttpResponseMessage GetSmsSettingsList()
        {
            try
            {
                List<SMS_SETTING> smsSettings = smsSettingsService.getSmsSettingsList();
                return GetSuccessResponse(smsSettings);

            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);
                return GetServerErrorResponse(ex.Message);
            }
        }
    }
}
