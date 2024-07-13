using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;


namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class APIResponse<T>
    {
        public APIResponse(APIResultCode statusCode, string message)
        {
            status = statusCode;
            statusDesc = message;
        }

        public APIResponse(T result)
        {
            data = result;
        }
        /*public APIResponse(APIResultCode statusCode, string message,T result)
        {
            data = result;
        }*/
        public APIResponse()
        {
        }

        public APIResultCode status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string statusDesc { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T data { get; set; }
    }

    public enum APIResultCode
    {
        [Description("success")]
        Ok = 200,

        [Description("Invalid Auth Token")]
        InvalidAuthToken = 41,

        [Description("Username not exist")]
        InvalidUsername = 51,

        [Description("Invalid Username (OR) Password")]
        InvalidUserNameOrPassWord = 101,

        [Description("Success to Log Error")]
        SuccessToLogError = 110,

        [Description("Failed to Log Error")]
        FailedToLogError = 111,

        [Description("Unknown Error")]
        UnknownError = 1000
    }
}
