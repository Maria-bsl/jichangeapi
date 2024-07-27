﻿using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace JichangeApi.Services.setup
{
    public class RegionService
    {
        public List<REGION> GetRegionsList()
        {
            try
            {
                REGION region = new REGION();
                var results = region.GetReg();
                if (results != null) { return results; }
                return new List<REGION>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public REGION GetRegionById(long regionId)
        {
            try
            {
                REGION region = new REGION();
                bool exists = region.isExistRegion(regionId);
                if (!exists) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                REGION found = region.EditREGION(regionId);
                return found;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
