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
        Payment pay = new Payment();
        public List<Object> GetAuditTrailReport(AuditTrailForm auditTrailForm)
        {
            try
            {
                //TableDetails tb = new TableDetails();
                //Auditlog ad = new Auditlog();
                DateTime FromDateVal = DateTime.Parse(auditTrailForm.Startdate);
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
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
    }
}
