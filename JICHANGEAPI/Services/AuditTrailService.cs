using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services
{
    public class AuditTrailService
    {
        public List<Object> GetAuditTrailReport(AuditTrailForm auditTrailForm)
        {
            try
            {
                TableDetails tb = new TableDetails();
                EMP_DET ed = new EMP_DET();
                Auditlog ad = new Auditlog();
                string startDate = DateTime.ParseExact(auditTrailForm.Startdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                string enddate = DateTime.ParseExact(auditTrailForm.Enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                DateTime FromDateVal = DateTime.Parse(startDate);
                DateTime toDateVal = DateTime.Parse(enddate);
                var dd = tb.Getlog(auditTrailForm.tbname);
                var objlistmem = ad.GetBloglist(FromDateVal, toDateVal, auditTrailForm.tbname, auditTrailForm.act, dd.Relation,(long) auditTrailForm.branch);
                if (objlistmem != null)
                {
                    List<Object> Time = new List<Object>();
                    foreach (Auditlog c in objlistmem)
                    {
                        Time.Add(new
                        {

                            ovalue = c.Oldvalues,
                            nvalue = c.Newvalues/*.Split(' ')[1] == "12:00:00" ? c.Newvalues: c.Newvalues*/,
                            atype = c.Audit_Type,
                            colname = c.Columnsname,
                            aby = c.AuditBy,
                            adate = c.Audit_Date,

                        });
                    }
                    return Time;
                }
                return new List<Object>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
