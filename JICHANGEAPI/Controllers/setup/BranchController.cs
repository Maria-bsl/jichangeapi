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
    public class BranchController : SetupBaseController
    {
        Payment pay = new Payment();
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage GetBranchLists()
        {
            var branch = new BranchM();
            try
            {
                var results = branch.GetBranches();
                return this.GetList<List<BranchM>, BranchM>(results);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private BranchM createBranchM(AddBranchForm addBranchForm)
        {
            BranchM branchM = new BranchM();
            branchM.Sno = addBranchForm.Branch_Sno;
            branchM.Name = addBranchForm.Name;
            branchM.Location = addBranchForm.Location;
            branchM.Status = addBranchForm.Status;
            branchM.AuditBy = addBranchForm.AuditBy;
            return branchM;
        }

        private HttpResponseMessage InsertBranch(BranchM branchM)
        {
            try
            {
                bool existsBranch = branchM.ValidateBranch(branchM.Name);
                if (existsBranch) return this.GetAlreadyExistsErrorResponse();
                long addedBranch = branchM.AddBranch(branchM);
                return this.FindBranch(addedBranch);
            }
            catch(Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private HttpResponseMessage UpdateBranch(BranchM branchM)
        {
            try
            {
                bool exitsBranch = branchM.isExistBranch(branchM.Sno);
                if (!exitsBranch) return this.GetNotFoundResponse();
                bool isDuplicatedName = branchM.IsDuplicatedName(branchM.Name, branchM.Sno);
                if (isDuplicatedName) return this.GetAlreadyExistsErrorResponse();
                long updatedBranch = branchM.UpdateBranch(branchM);
                return this.FindBranch(updatedBranch);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddBranch(AddBranchForm addBranchForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            BranchM branchM = createBranchM(addBranchForm);
            if (addBranchForm.Branch_Sno == 0) { return InsertBranch(branchM); }
            else { return UpdateBranch(branchM); }
        }

        [HttpGet]
        public HttpResponseMessage FindBranch(long sno)
        {
            try
            {
                BranchM branchM = new BranchM();
                bool exitsBranch = branchM.isExistBranch(sno);
                if (!exitsBranch) return this.GetNotFoundResponse();
                BranchM found = branchM.EditBranch(sno);
                return this.GetSuccessResponse(found);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteBranch(string Sno)
        {
            try
            {
                List<string> modelStateErrors = this.ModelStateErrors();
                if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
                var branchM = new BranchM();
                var exitsBranch = branchM.isExistBranch(long.Parse(Sno));
                if (!exitsBranch) return this.GetNotFoundResponse();
                branchM.DeleteBranch(long.Parse(Sno));
                return this.GetSuccessResponse(long.Parse(Sno));
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}