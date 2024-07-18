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
    public class DistrictController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetdIST()
        {
            var district = new DISTRICTS();
            try
            {
                var districts = district.GetDistrict();
                if (districts != null)
                {
                    return Request.CreateResponse(new { response = districts, message = new List<string>() });
                }
                else
                {
                    return Request.CreateResponse(new { response = new List<string>(), message = new List<string> { "Failed to retrieve district list." } });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }

        private HttpResponseMessage AddDistrict(DISTRICTS district,AddDistrictForm addDistrictForm)
        {
            var addDistrict = district.AddDistrict(district);
            if (addDistrict > 0)
            {
                var list = new List<string> { addDistrict.ToString(), addDistrictForm.district_name };
                Auditlog ad = new Auditlog();
                for (int i = 0; i < list.Count(); i++)
                {
                    ad.Audit_Type = "Insert";
                    ad.Columnsname = list[i];
                    ad.Table_Name = "District";
                    ad.Newvalues = list[i];
                    ad.AuditBy = addDistrictForm.userid.ToString();
                    ad.Audit_Date = DateTime.Now;
                    ad.Audit_Time = DateTime.Now;
                    ad.AddAudit(ad);
                }
                return Request.CreateResponse(new { response = addDistrict, message = new List<string>() });
            }
            else
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed to add district." } });
            }
        }

        [HttpPost]
        public HttpResponseMessage AddDistrict(AddDistrictForm addDistrictForm)
        {
            if (ModelState.IsValid)
            {
                var district = new DISTRICTS();
                var region = new REGION();
                var foundRegion = region.GetReg().Find(c => c.Region_SNO == addDistrictForm.region_id);
                if (foundRegion == null)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed. Region does not exist." } });
                }
                district.SNO = (long) addDistrictForm.sno;
                district.District_Name = addDistrictForm.district_name;
                district.Region_Id = foundRegion.Region_SNO;
                district.Region_Name = foundRegion.Region_Name;
                district.AuditBy = addDistrictForm.userid.ToString();
                district.District_Status = addDistrictForm.district_status;
                try
                {
                    if (addDistrictForm.sno == 0)
                    {
                        var isExist = district.Validateduplicatechecking(addDistrictForm.district_name.ToLower());
                        if (isExist)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Already exists." } });
                        }
                        else
                        {
                            return this.AddDistrict(district, addDistrictForm);
                        }
                    }
                    else
                    {
                        var updatedDistrict = district.UpdateDISTRICTS(district);
                        if (updatedDistrict > 0)
                        {
                            return Request.CreateResponse(new { response = updatedDistrict, message = new List<string>() });
                        }
                        else
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed to update district." } });
                        }
                    }
                }
                catch(Exception ex)
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
        public HttpResponseMessage DeleteDist(DeleteDistrictForm deleteDistrictForm)
        {
            if (ModelState.IsValid)
            {
                if (deleteDistrictForm.sno > 0)
                {
                    var district = new DISTRICTS();
                    var isExistDistrict = district.isExistDistrict(deleteDistrictForm.sno);
                    if (isExistDistrict)
                    {
                        district.DeleteDISTRICTS(deleteDistrictForm.sno);
                        return Request.CreateResponse(new { response = deleteDistrictForm.sno, message = new List<string>() });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed. District does not exist." } });
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