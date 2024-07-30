using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace JichangeApi.Services
{
    public class ForgetPasswordService 
    {

        User_otp ota = new User_otp();
        CompanyUsers cus = new CompanyUsers();
        EMP_DET emp = new EMP_DET();

        public User_otp ValidateOtpHandler(SingletonVOtp m)
        {
            try
            {
                var result1 = cus.CheckUser(m.mobile);

                if (result1 != null)
                {
                    var validateotp = ota.ValidateUser_otp(m.otp_code);
                    var dets = ota.GetDetails(m.otp_code);
                    if (validateotp != false || DateTime.Now > dets.posted_date)
                    {
                        return dets;
                    }
                    else
                    {
                        return null;
                    }
                }
                if (result1 == null)
                {

                    var result = emp.CheckUserBank(m.mobile);
                    if (result != null)
                    {

                        var validateotp = ota.ValidateUser_otp(m.otp_code);
                        var dets = ota.GetDetails(m.otp_code);
                        if (validateotp != false || DateTime.Now > dets.posted_date)
                        {
                            return dets;
                        }
                        else
                        {
                            return null;
                        }

                    }

                }
                return null;

            }
            catch (Exception ex)
            {
                Payment pay = new Payment();
                pay.Error_Text = ex.ToString();
                pay.AddErrorLogs(pay);

                return null;
            }
        }
    }
}