using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using JichangeApi.Models.form;
using JichangeApi.Models.form.setup.insert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers.setup
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BranchController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetBranchLists()
        {
            var branch = new BranchM();
            try
            {
                var results = branch.GetBranches();
                if (results != null)
                {
                    return Request.CreateResponse(new { response = results, message = new List<string>() });
                }
                else
                {
                    return Request.CreateResponse(new { response = new List<BranchM>(), message = new List<string> { "Failed to retrieve branch list" } });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }

        [HttpPost]
        public HttpResponseMessage AddBranch(AddBranchForm branchForm)
        {
            if (ModelState.IsValid)
            {
                var branch = new BranchM();
                branch.Sno = branchForm.Branch_Sno;
                branch.Name = branchForm.Name;
                branch.Location = branchForm.Location;
                branch.Status = branchForm.Status;
                branch.AuditBy = branchForm.AuditBy;
                if (branchForm.Branch_Sno == 0)
                {
                    try
                    {
                        var existsBranch = branch.ValidateBranch(branch.Name);
                        if (existsBranch)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Already exists." } });
                        }
                        var addedBranch = branch.AddBranch(branch);
                        if (addedBranch > 0)
                        {
                            return Request.CreateResponse(new { response = addedBranch, message = new List<string>() });
                        }
                        else
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed to add new branch." } });
                        }
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
                    }

                }
                else
                {
                    try
                    {
                        var existsBranch = branch.isExistBranch(branchForm.Branch_Sno);
                        if (!existsBranch)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Branch does not exist." } });
                        }
                        var updatedBranch = branch.UpdateBranch(branch);
                        if (updatedBranch > 0)
                        {
                            return Request.CreateResponse(new { response = updatedBranch, message = new List<string>() });
                        }
                        else
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Failed to update branch." } });
                        }
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
                    }
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteBranch(string Sno)
        {
            try
            {
                var branch = new BranchM();
                var exitsBranch = branch.isExistBranch(long.Parse(Sno));
                if (exitsBranch)
                {
                    branch.DeleteBranch(long.Parse(Sno));
                    return Request.CreateResponse(new { response = "Branch deleted successfully!", message = new List<string>() });
                }
                else
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "Branch does not exist." } });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }
    }
}