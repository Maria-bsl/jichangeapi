using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using JichangeApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleController : SetupController
    {
        private readonly RoleService roleService = new RoleService();

        public HttpResponseMessage GetRolesAct()
        {
            try
            {
                List<Roles> roles = roleService.GetRoleList();
                return GetSuccessResponse(roles);
            }
            catch (Exception Ex)
            {
                return GetServerErrorResponse(Ex.Message);
            }
        }
    }
}
