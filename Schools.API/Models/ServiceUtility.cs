using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using BL.BIZINVOICING.BusinessEntities.Common;


namespace EPS.API.Models
{
    public class ServiceUtility
    {
        #region "Constants"

        public const string Success = "200"; //Payment details successfully inserted;
        public const string Authentication = "201"; //Access Denied. Null/Invalid Authentication Token.;
        public const string PDate = "1022";//Payment Date is null or empty or Invalid.";
        public const string PAmount = "1023";//Payment Amount is null or empty.";
        public const string Add = "1024";//Error in add ";
        public const string RNo = "204";//Receipt No is null or empty ";
        public const string TNO = "1026";//Transaction No is null or empty ";
        public const string PMode = "1027";//Payment Mode is null or empty ";
        public const string Login = "1030"; //"Invoice no is not valid, please check Invoice no"//"The Email ID and Password you entered to login are incorrect. Please try again.";
        public const string NoAccount = "Your account with this email ID could not be located. Please try again.";
        public const string PaidAlready = "207";//Fee paid already.";
        public const string UnKnownSource = "208";//Fee paid already.";
        public const string PaidAlreadyV = "203";//Fee paid already.";
        public const string CancelAppointment = "Error in cancel appointment.";
        public const string NotRegistered = "Could not locate any registered account with this email ID. Please register to login into the application.";
        public const string ResetPassword = "Your temporary password has been emailed to you.";
        public const string ChangePassword = "Your password is changed successfully. Please login again to access your account.";
        public const string Invalid = "Please enter valid ";
        public const string ApplicationorInvoiceNotExist = "204";//Application number or Invoice number does not exists.";
        public const string InvoiceNotExist = "Invoice number not exist";
        public const string NoLocations = "No location(s) found.";
        public const string NoAppointments = "No appointments matching your information could be found. Please try again or make a new appointment.";
        public const string Get = "1031";//This Invoice number dont have details
        public const string BOTAmount = "1032";//Amount not matched
        public const string Expired = "1033";//Invoice Expired
        #endregion

        /// <summary>
        /// Authenticate token 
        /// </summary>
        /// <param name="AuthToken"></param>
        /// <returns></returns>
        public static bool AuthenticateToken(string AuthToken)
        {
            //string aToken = Utility.DecodeFrom64(AuthToken);
            string aToken = AuthToken;
            if (aToken != ConfigurationManager.AppSettings["Token"])
                return false;
            else
                return true;
        }
       
    }
}