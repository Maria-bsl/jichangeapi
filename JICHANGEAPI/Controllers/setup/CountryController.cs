using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using JichangeApi.Models.form;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;
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
    public class CountryController : SetupBaseController
    {
        private static readonly List<string> tableColumns = new List<string> { "country_sno", "country_name" };
        private static readonly string tableName = "Country";

        [HttpPost]
        public HttpResponseMessage GetCountries()
        {
            var country = new COUNTRY();
            try
            {
                var results = country.GETcountries();
                return this.GetList<List<COUNTRY>, COUNTRY>(results);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private COUNTRY createCountry(AddCountryForm addCountryForm) 
        {
            COUNTRY country = new COUNTRY();
            country.Country_Name = addCountryForm.Country_Name;
            country.SNO = addCountryForm.sno;
            return country;
        }

        private HttpResponseMessage InsertCountry(AddCountryForm addCountryForm)
        {
            try
            {
                COUNTRY country = createCountry(addCountryForm);
                bool isExistCountry = country.ValidateLicense(addCountryForm.Country_Name);
                if (isExistCountry) return this.GetAlreadyExistsErrorResponse();
                long addedCountry = country.Addcountries(country);
                AppendInsertCountryAuditTrail(addedCountry,country,(long) addCountryForm.userid);
                return this.FindCountry(addedCountry);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private void AppendInsertCountryAuditTrail(long countrySno,COUNTRY country,long userid)
        {
            List<string> insertAudits = new List<string> { countrySno.ToString(), country.Country_Name };
            Auditlog.InsertAuditTrail(insertAudits, userid, CountryController.tableName, CountryController.tableColumns);
        }

        private void AppendUpdateCountryAuditTrail(long countrySno,COUNTRY oldCountry,COUNTRY newCountry,long userid)
        {
            List<string> oldValues = new List<string> { countrySno.ToString(), oldCountry.Country_Name };
            List<string> newValues = new List<string> { countrySno.ToString(), newCountry.Country_Name };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, CountryController.tableName, CountryController.tableColumns);
        }

        private void AppendDeleteCountryAuditTrail(long countrySno,COUNTRY country,long userid)
        {
            List<string> values = new List<string> { countrySno.ToString(), country.Country_Name };
            Auditlog.deleteAuditTrail(values,userid, CountryController.tableName, CountryController.tableColumns);
        }

        private HttpResponseMessage UpdateCountry(AddCountryForm addCountryForm)
        {
            try
            {
                COUNTRY country = createCountry(addCountryForm);
                bool isExitsCountry = country.isExistCountry(addCountryForm.sno);
                if (!isExitsCountry) return this.GetNotFoundResponse();
                bool isDuplicatedName = country.IsDuplicatedName(addCountryForm.Country_Name, addCountryForm.sno);
                if (isDuplicatedName) return this.GetAlreadyExistsErrorResponse();
                COUNTRY oldCountry = country.Editcountries(addCountryForm.sno);
                long updatedCountry = country.Updatecountries(country);
                AppendUpdateCountryAuditTrail(updatedCountry, oldCountry, country, (long) addCountryForm.userid);
                return this.FindCountry(updatedCountry);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddCountry(AddCountryForm addCountryForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            else if (addCountryForm.sno == 0) { return InsertCountry(addCountryForm); }
            else { return UpdateCountry(addCountryForm); }
        }

        [HttpGet]
        public HttpResponseMessage FindCountry(long sno)
        {
            try
            {
                COUNTRY country = new COUNTRY();
                bool isExitsCountry = country.isExistCountry(sno);
                if (!isExitsCountry) return this.GetNotFoundResponse();
                COUNTRY found = country.Editcountries(sno);
                return this.GetSuccessResponse(found);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteCount(DeleteCountryForm deleteCountryForm)
        {
            try
            {
                List<string> modelStateErrors = this.ModelStateErrors();
                if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
                COUNTRY country = new COUNTRY();
                bool isExistSCountry = country.isExistCountry(deleteCountryForm.sno);
                if (!isExistSCountry) return this.GetNotFoundResponse();
                AppendDeleteCountryAuditTrail(deleteCountryForm.sno, country, deleteCountryForm.sno);
                country.Deletecountries(deleteCountryForm.sno);
                return this.GetSuccessResponse(deleteCountryForm.sno);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}
