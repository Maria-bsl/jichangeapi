using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using JichangeApi.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace JichangeApi.Controllers.smsservices
{
    public class SmsService
    {
        #region Class Instances
        SMS_SETTING smst = new SMS_SETTING();
        SMS_TEXT sms = new SMS_TEXT();
        S_SMTP ss = new S_SMTP();
        S_SMTP stp = new S_SMTP();
        EMAIL em = new EMAIL();
        CustomerMaster cu = new CustomerMaster();
        

        #endregion;


        #region for SMS Text template

        private string GetInviteeWelcomeText(string inviteeName, string qrCodeIdentity, int? card_size)
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;

            var invitee_name = textInfo.ToTitleCase(inviteeName.ToLower());

            var qr_code = qrCodeIdentity.Substring(9);

            if (card_size == 1)
            {
                return string.Format($"ID: {qr_code}. {invitee_name} karibu katika sherehe ya harusi ya Hilary & Erminia, 2/7/2022, KKKT mbezi Beach & Sea Breeze Hall.");
            }

            return string.Format($"ID: {qr_code}. {invitee_name} karibuni katika sherehe ya harusi ya Hilary & Erminia, 2/7/2022, KKKT mbezi Beach & Sea Breeze Hall.");
        }

        private static bool IsLocalMobileNumber(string mobile_number)
        {
            return mobile_number.Replace("+", "").Substring(0, 3) == "255";
        }

        private void SendLocalInviteeWelcomeSMS(string visitorMobileNumber, string sms_text)
        {
            if (IsLocalMobileNumber(visitorMobileNumber))
            {
                try
                {
                    var sm = smst.getSMTPConfig();
                    var m = ss.getSMTPText();
                    string username = sm.USER_Name;
                    string password = DecodeFrom64(sm.Password);
                    string senderb = sm.From_Address;
                    //  string mess = data.SMS_Text;
                    string gsm = visitorMobileNumber;

                    var formattedSMSbody = HttpUtility.UrlEncode(sms_text);

                    string url = "http://api.infobip.com/api/v3/sendsms/plain?user=" + username + "&password=" + password + "&sender=" + senderb + "&SMSText=" + formattedSMSbody + "&GSM=" + gsm;
                    WebRequest myReq = WebRequest.Create(url);
                    myReq.Method = "POST";
                    WebResponse wr = myReq.GetResponse();
                    StreamReader sr = new StreamReader(wr.GetResponseStream(), System.Text.Encoding.Default);
                    string Res = sr.ReadToEnd();
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                }
            }
        }

        private static string DecodeFrom64(string password)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(password);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        #endregion

        #region SMS methods

        public void SendWelcomeSmsToNewUser(string username, string password, string mobile_no)
        {
            if (username != null)
            {
                var mobileNumber = mobile_no;

                var decryptedPassword = DecodeFrom64(password);
                var formattedMessageBody = FormatMessageBody(username, decryptedPassword);

                SendSMSAction(mobileNumber, formattedMessageBody);

            }

        }

        public void SendOTPSmsToDeliveryCustomer(string Mobile_Number, string otp)
        {
            if (Mobile_Number != null)
            {
                //var otp = OTP.GenerateOTP(6);
                var mobileNumber = Mobile_Number;

               // var decryptedPassword = DecodeFrom64(password);
                var formattedMessageBody = FormatOtpMessageBody(Mobile_Number, otp);

                SendSMSAction(mobileNumber, formattedMessageBody);

            }

        }

        private static string FormatMessageBody(string customerName, string password)
        {
            return string.Format("{0}, you have successfully registered on JICHANGE system, the URL is "+ ConfigurationManager.AppSettings["MyWebUrl"] + "and Your password is {1}", customerName, password);
        }

        private static string FormatOtpMessageBody(string cust_number, string code)
        {
            return string.Format("{0},JICHANGE verification code is  {1}", cust_number, code);
        }

        private void SendSMSAction(string visitorMobileNumber, string SmsBody)
        {
            
            try
            {
                var sm = smst.getSMTPConfig();
                //var data = sms.getSMSLst(1); // on User Registration
                var m = stp.getSMTPText();
                string username = sm.USER_Name;
                string password = DecodeFrom64(sm.Password);
                string senderb = sm.From_Address;
                //  string mess = data.SMS_Text;
                //string gsm = ReplaceFirstOccurrence(visitorMobileNumber.Trim(), "0", "255");
                string gsm = visitorMobileNumber;
                string body = HttpUtility.UrlEncode(SmsBody);

                string url = "http://api.infobip.com/api/v3/sendsms/plain?user=" + username + "&password=" + password + "&sender=" + senderb + "&SMSText=" + body + "&GSM=" + gsm;
                WebRequest myReq = WebRequest.Create(url);
                myReq.Method = "POST";
                WebResponse wr = myReq.GetResponse();
                StreamReader sr = new StreamReader(wr.GetResponseStream(), Encoding.Default);
                string Res = sr.ReadToEnd();
                Console.WriteLine(Res);
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

        }

        private static string ReplaceFirstOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.IndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        #endregion;



    }
}