using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CountryController : ApiController
    {
        // GET: Country
        COUNTRY cty = new COUNTRY();
        EMP_DET ed = new EMP_DET();
        private readonly dynamic returnNull = null;
        Auditlog ad = new Auditlog();
        String[] list = new String[2] { "country_sno", "country_name" };


        [HttpPost]
        public HttpResponseMessage AddCountry(CountryForm c)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    cty.Country_Name = c.country_name;
                    cty.SNO = c.sno;
                    long ssno = 0;
                    if (c.sno == 0)
                    {
                        var result = cty.ValidateLicense(c.country_name.ToLower());
                        if (result == true)
                        {
                            return Request.CreateResponse(new { response = result, message = new List<string> { "Failed" } });
                        }
                        else
                        {
                            ssno = cty.Addcountries(cty);
                            if (ssno > 0)
                            {
                                String[] list1 = new String[2] { ssno.ToString(), cty.Country_Name };
                                for (int i = 0; i < list.Count(); i++)
                                {
                                    ad.Audit_Type = "Insert";
                                    ad.Columnsname = list[i];
                                    ad.Table_Name = "Country";
                                    ad.Newvalues = list1[i];
                                    ad.AuditBy = c.userid.ToString();
                                    ad.Audit_Date = DateTime.Now;
                                    ad.Audit_Time = DateTime.Now;

                                    ad.AddAudit(ad);
                                }
                            }
                            return Request.CreateResponse(new { response = ssno, message = new List<string> { } });
                        }
                    }
                    else if (c.sno > 0)
                    {
                        if (c.dummy == false)
                        {
                            return Request.CreateResponse(new { response = c.dummy, message = new List<string> { "Failed" } });

                        }
                        else
                        {
                            var dd = cty.Editcountries(c.sno);
                            if (dd != null)
                            {
                                String[] list2 = new String[2] { dd.SNO.ToString(), dd.Country_Name };
                                String[] list1 = new String[2] { c.sno.ToString(), cty.Country_Name };
                                for (int i = 0; i < list.Count(); i++)
                                {
                                    ad.Audit_Type = "Update";
                                    ad.Columnsname = list[i];
                                    ad.Table_Name = "Country";
                                    ad.Oldvalues = list2[i];
                                    ad.Newvalues = list1[i];
                                    ad.AuditBy = c.userid.ToString();
                                    ad.Audit_Date = DateTime.Now;
                                    ad.Audit_Time = DateTime.Now;
                                    ad.AddAudit(ad);
                                }
                            }
                            cty.Updatecountries(cty);
                            ssno = c.sno;
                            return Request.CreateResponse(new { response = ssno, message = new List<string> { } });
                        }

                    }


                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            return returnNull;
        }

        public HttpResponseMessage GetCounts()
        {

            try
            {
                var result = cty.GETcountries();
                if (result != null)
                {
                    return Request.CreateResponse(new { response = result, message = new List<string> { } });
                }
                else
                {
                    var d = 0;
                    return Request.CreateResponse(new { response = d, message = new List<string> { "Failed" } });
                }
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }

            //return returnNull;
        }


        [HttpDelete]
        public HttpResponseMessage DelCountry(DeleteCountryForm deleteCountryForm)
        {

            try
            {

                cty.SNO = deleteCountryForm.sno;
                var name = cty.ValidateCount(deleteCountryForm.sno);
                if (name == true)
                {
                    return Request.CreateResponse(new { response = name, message = new List<string> { "Failed" } });

                }
                else
                {
                    if (deleteCountryForm.sno > 0)
                    {
                        var dd = cty.Editcountries(deleteCountryForm.sno);
                        if (dd != null)
                        {
                            String[] list2 = new String[2] { dd.SNO.ToString(), dd.Country_Name };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Delete";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Country";
                                ad.Oldvalues = list2[i];
                                ad.AuditBy = deleteCountryForm.userid.ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;

                                ad.AddAudit(ad);
                            }
                        }
                        cty.Deletecountries(deleteCountryForm.sno);
                    }
                    var result = deleteCountryForm.sno;

                    return Request.CreateResponse(new { response = result, message = new List<string> { } });
                }
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }

            //return returnNull;
        }



        [HttpPost]
        public HttpResponseMessage CheckCount(long sno)
        {

            try
            {


                var result = cty.ValidateCount(sno);

                return Request.CreateResponse(new { response = result, message = new List<string> { } });
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }

            //return returnNull;
        }
   
    }
}
