using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace JichangeApi.Services
{
    public class AuditTrailService
    {
        Payment pay = new Payment();
        public JsonObject GetAuditTrailReport(AuditTrailForm auditTrailForm)
        {
            try
            {
                Auditlog ad = new Auditlog();
                var count = ad.GetAuditReportCount(auditTrailForm.Startdate, auditTrailForm.Enddate, auditTrailForm.tbname, auditTrailForm.act, auditTrailForm.userid.ToString());
                var audit_logs = ad.GetAuditReport(auditTrailForm.Startdate, auditTrailForm.Enddate, auditTrailForm.tbname, auditTrailForm.act, auditTrailForm.userid.ToString(),(int) auditTrailForm.pageNumber,(int) auditTrailForm.pageSize);
                string logs = JsonSerializer.Serialize(audit_logs);
                string length = JsonSerializer.Serialize(count);
                JsonObject jsonObject = new JsonObject(); //JsonNode.Parse(jsonString).AsObject();
                jsonObject.Add("size", JsonNode.Parse(length).AsValue());
                jsonObject.Add("content", JsonNode.Parse(logs).AsArray());


                return jsonObject;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public JsonObject GetAuditTrailsVendorReport(AuditTrailForm auditTrailForm)
        {
            try
            {
                Auditlog audit = new Auditlog();
                var count = audit.GetAuditTrailsVendorReportCount(auditTrailForm.Startdate, auditTrailForm.Enddate, auditTrailForm.tbname, auditTrailForm.act, auditTrailForm.userid.ToString());
                var audit_logs = audit.GetAuditTrailsVendorReport(auditTrailForm.Startdate, auditTrailForm.Enddate, auditTrailForm.tbname, auditTrailForm.act, auditTrailForm.userid.ToString(), (int)auditTrailForm.pageNumber, (int)auditTrailForm.pageSize);
                string logs = JsonSerializer.Serialize(audit_logs);
                string length = JsonSerializer.Serialize(count);
                JsonObject jsonObject = new JsonObject();
                jsonObject.Add("size", JsonNode.Parse(length).AsValue());
                jsonObject.Add("content", JsonNode.Parse(logs).AsArray());
                return jsonObject;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public List<string> GetTableNamesByAuditBy(string userid)
        {
            try
            {
                var tableNames = new Auditlog().SelectTableNamesByAuditBy(userid);
                return tableNames;
            }
            catch(ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<string> GetAuditTypesBy(string userid)
        {
            try
            {
                var tableNames = new Auditlog().SelectAuditTypesBy(userid);
                return tableNames;
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
