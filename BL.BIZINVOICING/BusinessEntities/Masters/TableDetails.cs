using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BIZINVOICING.BusinessEntities.Masters;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
   public class TableDetails
    {

        #region Properties

        public string tab_name { get; set; }
        public long Sno { get; set; }
        public String Relation { get; set; }
        #endregion Properties
        #region Method
        public TableDetails Getlog(string tab)
        {
            //DateTime add = to.AddDays(1);
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from mr in context.table_details
                                where mr.table_name == tab
                                select new TableDetails
                                {
                                    tab_name = mr.table_name,
                                    Sno = mr.sno,
                                    Relation = mr.table_relation

                                }).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }
        #endregion Method

    }
}
