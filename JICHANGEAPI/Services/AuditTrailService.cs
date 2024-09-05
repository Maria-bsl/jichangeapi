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
                //TableDetails tb = new TableDetails();
                //Auditlog ad = new Auditlog();
                /*DateTime FromDateVal = DateTime.Parse(auditTrailForm.Startdate);
                DateTime toDateVal = DateTime.Parse(auditTrailForm.Enddate);
                var dd = new TableDetails().Getlog(auditTrailForm.tbname);
                var objlistmem = new Auditlog().GetBloglist(FromDateVal, toDateVal, auditTrailForm.tbname, auditTrailForm.act, dd.Relation,(long) auditTrailForm.branch);
                if (objlistmem != null)
                {
                    List<Object> Time = new List<Object>();
                    foreach (Auditlog c in objlistmem)
                    {
                        Time.Add(new
                        {

                            ovalue = c.Oldvalues,
                            nvalue = c.Newvalues*//*.Split(' ')[1] == "12:00:00" ? c.Newvalues: c.Newvalues*//*,
                            atype = c.Audit_Type,
                            colname = c.Columnsname,
                            aby = c.AuditBy,
                            adate = c.Audit_Date,

                        });
                    }
                    return Time;
                }
                return new List<Object>();*/

                Auditlog ad = new Auditlog();
                var count = ad.GetAuditReportCount(auditTrailForm.Startdate, auditTrailForm.Enddate, auditTrailForm.tbname, auditTrailForm.act, auditTrailForm.userid.ToString());
                var audit_logs = ad.GetAuditReport(auditTrailForm.Startdate, auditTrailForm.Enddate, auditTrailForm.tbname, auditTrailForm.act, auditTrailForm.userid.ToString(),(int) auditTrailForm.pageNumber,(int) auditTrailForm.pageSize);
                string logs = JsonSerializer.Serialize(audit_logs);
                string length = JsonSerializer.Serialize(count);
                JsonObject jsonObject = new JsonObject(); //JsonNode.Parse(jsonString).AsObject();
                jsonObject.Add("size", JsonNode.Parse(length).AsValue());
                jsonObject.Add("content", JsonNode.Parse(logs).AsArray());


                return jsonObject;
                /*List<Object> Times = new List<Object>();
                foreach (var c in audit_logs)
                {
                    Times.Add(new
                    {
                        ovalue = c.Oldvalues,
                        nvalue = c.Newvalues, 
                        atype = c.Audit_Type,
                        colname = c.Columnsname,
                        aby = c.AuditBy,
                        adate = c.Audit_Date,
                    });
                } 
                return Times;*/
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
