using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;
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
    public class RegionController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetRegionDetails()
        {
            var region = new REGION();
            try
            {
                var regions = region.GetReg();
                if (regions != null)
                {
                    return Request.CreateResponse(new { response = regions, message = new List<string>() });
                }
                else
                {
                    return Request.CreateResponse(new { response = new List<REGION>(), message = new List<string> { "No data found." } });
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }

        private HttpResponseMessage AddRegion(REGION region,AddRegionForm addRegionForm)
        {
            var addedRegion = region.AddREGION(region);
            if (addedRegion > 0)
            {
                var list = new List<string> { addedRegion.ToString(), addRegionForm.region };
                Auditlog ad = new Auditlog();
                for (int i = 0; i < list.Count(); i++)
                {
                    ad.Audit_Type = "Insert";
                    ad.Columnsname = list[i];
                    ad.Table_Name = "Region";
                    ad.Newvalues = list[i];
                    ad.AuditBy = addRegionForm.userid.ToString();
                    ad.Audit_Date = DateTime.Now;
                    ad.Audit_Time = DateTime.Now;
                    ad.AddAudit(ad);
                }
                return Request.CreateResponse(new { response = addedRegion, message = new List<string>() });
            }
            else
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed to add region." } });
            }
        }

        public HttpResponseMessage AddRegion(AddRegionForm addRegionForm)
        {
            if (ModelState.IsValid)
            {
                var region = new REGION();
                var country = new COUNTRY();
                var foundCountry = country.GETcountries().Find(c => c.SNO == addRegionForm.csno);
                if (foundCountry == null)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "Country not found." } });
                }
                region.Region_SNO = addRegionForm.sno;
                region.Region_Name = addRegionForm.region;
                region.Country_Sno = addRegionForm.csno;
                region.Country_Name = foundCountry.Country_Name;
                region.Region_Status = addRegionForm.Status;
                region.AuditBy = addRegionForm.userid.ToString();
                try
                {
                    if (addRegionForm.sno == 0)
                    {
                        var isExist = region.Validatedupicate(addRegionForm.region.ToLower());
                        var isExistCountry = region.ValidateREGION(addRegionForm.region.ToLower(), addRegionForm.csno);
                        if (isExist || isExistCountry)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Already exists." } });
                        }
                        else
                        {
                            return this.AddRegion(region,addRegionForm);
                        }
                    }
                    else
                    {
                        var updatedRegion = region.UpdateREGION(region);
                        if (updatedRegion > 0)
                        {
                            return Request.CreateResponse(new { response = updatedRegion, message = new List<string>() });
                        }
                        else
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed to update region." } });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteRegion(DeleteRegionForm deleteRegionForm)
        {
            if (ModelState.IsValid)
            {
                if (deleteRegionForm.sno > 0)
                {
                    var region = new REGION();
                    region.Region_SNO = deleteRegionForm.sno;
                    var isValidDelete = region.isExistRegion(deleteRegionForm.sno);
                    if (isValidDelete)
                    {
                        region.DeleteREGION(deleteRegionForm.sno);
                        return Request.CreateResponse(new { response = deleteRegionForm.sno, message = new List<string> () });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed. Region does not exist." } });
                    }
                }
                else
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "Invalid sno." } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        }
    }
}