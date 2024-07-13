using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class Company
    {
        #region Properties
        public long Comp_Mas_Sno { get; set; }
        public String Company_Name { get; set; }
        #endregion Properties
        #region Methods
        public List<Company> GetCompanyMas()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.company_master
                                select new Company
                                {
                                    Comp_Mas_Sno = c.comp_mas_sno,
                                    Company_Name = c.company_name,
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public Company GetCompanyS(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.company_master
                                where c.comp_mas_sno == sno
                                select new Company
                                {
                                    Comp_Mas_Sno = c.comp_mas_sno,
                                    Company_Name = c.company_name,
                                }).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }
        #endregion Methods
    }
}
