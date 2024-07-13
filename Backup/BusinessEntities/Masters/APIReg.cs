using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class APIReg
    {
        #region Properties
        public long SNO { get; set; }
        public string RegIP_Test { get; set; }
        public string RegIP_Live { get; set; }
        public string InvIP_Test { get; set; }
        public string InvIP_Live { get; set; }
        public string TokenIP_Test { get; set; }
        public string TokenIP_Live { get; set; }
        public string VerifyIP_Test { get; set; }
        public string VerifyIP_Live { get; set; }
        public string Test_CN { get; set; }
        public string Live_CN { get; set; }
        public string Cert_Serial { get; set; }
        public string Cert_Key { get; set; }
        public string Res1 { get; set; }
        public string Res2 { get; set; }
        public string Res3 { get; set; }
        public string Res4 { get; set; }
        public string Res5 { get; set; }
        public string Res6 { get; set; }
        public string Res7 { get; set; }
        public string Res8 { get; set; }
        public string Res9 { get; set; }
        public string Res10 { get; set; }

        #endregion Properties
        #region Methods


        /*public List<APIReg> GETcountries()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.countries
                                select new APIReg
                                {
                                    SNO = c.country_sno,
                                    Country_Name = c.country_name,

                                }).OrderByDescending(z => z.SNO).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }*/
        public APIReg getAPI()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.apiurl_details
                                select new APIReg
                                {
                                    SNO = c.sno,
                                    RegIP_Test = c.regip_test,
                                    RegIP_Live = c.regip_live,
                                    InvIP_Test = c.invip_test,
                                    InvIP_Live = c.invip_live,
                                    TokenIP_Test = c.tokenip_test,
                                    TokenIP_Live = c.tokenip_live,
                                    VerifyIP_Test = c.verifyip_test,
                                    VerifyIP_Live = c.verifyip_live,
                                    Cert_Key = c.cert_key,
                                    Cert_Serial = c.cert_serial,
                                    Test_CN = c.testcn,
                                    Live_CN = c.livecn,
                                    Res1 = c.res1,
                                    Res2 = c.res2,
                                    Res3 = c.res3,
                                    Res4 = c.res4,
                                    Res5 = c.res5,
                                    Res6 = c.res6,
                                    Res7 = c.res7,
                                    Res8 = c.res8,
                                    Res9 = c.res9,
                                    Res10 = c.res10
                                }).OrderByDescending(a => a.SNO).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        
        /*public void Deletecountries(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.countries
                                   where n.country_sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.countries.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }*/

        

        #endregion Methods


    }
}
