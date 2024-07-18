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

namespace JichangeApi.Controllers.setup
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WardController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetWard()
        {
            var ward = new WARD();
            try
            {
                var wards = ward.GetWARD();
                if (wards != null)
                {
                    return Request.CreateResponse(new { response = wards, message = new List<string>() });
                }
                else
                {
                    return Request.CreateResponse(new { response = new List<string>(), message = new List<string> { "Failed to retrieve ward list." } });
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }

        [HttpPost]
        public HttpResponseMessage AddWard(AddWardForm addWardForm)
        {
            if (ModelState.IsValid)
            {
                var ward = new WARD();
                var district = new DISTRICTS();
                var foundDistrict = district.GetDistrict().Find(c => c.SNO == addWardForm.district_sno);
                if (foundDistrict == null)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed. Ward does not exist." } });
                }
                ward.SNO = (long) addWardForm.sno;
                ward.Ward_Name = addWardForm.ward_name;
                ward.Region_Name = foundDistrict.Region_Name;
                ward.District_Name = foundDistrict.District_Name;
                ward.Region_Id = foundDistrict.Region_Id;
                ward.District_Sno = foundDistrict.SNO;
                ward.Ward_Status = addWardForm.ward_status;
                try
                {
                    if (addWardForm.sno == 0)
                    {
                        var isExist = ward.ValidateWARD(foundDistrict.SNO,addWardForm.ward_name.ToLower(),foundDistrict.Region_Id);
                        if (isExist)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Already exists." } });
                        }
                        else
                        {
                            var addWard = ward.AddWARD(ward);
                            return Request.CreateResponse(new { response = addWard, message = new List<string>() });
                        }
                    }
                    else
                    {
                        var updateWard = ward.UpdateWARD(ward);
                        if (updateWard > 0)
                        {
                            return Request.CreateResponse(new { response = updateWard, message = new List<string>() });
                        }
                        else
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed to update district." } });
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
        public HttpResponseMessage DeleteWard(RemoveWardForm removeWardForm)
        {
            if (ModelState.IsValid)
            {
                var ward = new WARD();
                var exists = ward.isExistWard(removeWardForm.sno);
                if (exists)
                {
                    ward.DeleteWARD(removeWardForm.sno);
                    return Request.CreateResponse(new { response = removeWardForm.sno, message = new List<string>() });
                }
                else
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed. Ward does not exist." } });
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