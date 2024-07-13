using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;
namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class Customers
    {
        #region Properties
        public long Cus_Mas_Sno { get; set; }
        public String Customer_Name { get; set; }
        #endregion Properties
        #region Methods
        public List<Customers> GetCustomers()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.customer_master
                                select new Customers
                                {
                                    Cus_Mas_Sno = c.cust_mas_sno,
                                    Customer_Name = c.customer_name,
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<Customers> GetCustomersS(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.customer_master
                                where c.comp_mas_sno == sno
                                select new Customers
                                {
                                    Cus_Mas_Sno = c.cust_mas_sno,
                                    Customer_Name = c.customer_name,
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        #endregion Methods
    }
}
