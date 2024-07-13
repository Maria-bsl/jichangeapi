using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaL.BIZINVOICING.EDMX;

namespace BL.BIZINVOICING.BusinessEntities.Masters
{
    public class Token
    {
        #region Properties
        public long Token_Ack_SNO { get; set; }
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public string Expire_In { get; set; }
        public string Routing_Key { get; set; }
        public DateTime Created_Date { get; set; }
        #endregion Properties
        #region Methods
        public long AddToken(Token sc)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                token_ack ps = new token_ack()
                {
                    access_token = sc.Access_Token,
                    token_type = sc.Token_Type,
                    expire_in = sc.Expire_In,
                    routing_key = sc.Routing_Key,
                    created_date = DateTime.Now

                };
                context.token_ack.Add(ps);
                context.SaveChanges();
                return ps.token_ack_sno;
            }
        }

        public List<Token> GetSMTPS()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.token_ack
                                    //where c.ssl_enable == "True"
                                select new Token
                                {
                                    Token_Ack_SNO = c.token_ack_sno,
                                    Access_Token = c.access_token,
                                    Token_Type = c.token_type,
                                    Expire_In = c.expire_in,
                                    Routing_Key = c.routing_key,
                                    Created_Date = (DateTime)c.created_date

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public Token Gettoken()
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var adetails = (from c in context.token_ack
                                    //where c.ssl_enable == "True"
                                select new Token
                                {
                                    Token_Ack_SNO = c.token_ack_sno,
                                    Access_Token = c.access_token,
                                    Token_Type = c.token_type,
                                    Expire_In = c.expire_in,
                                    Routing_Key = c.routing_key,
                                    Created_Date = (DateTime)c.created_date

                                }).OrderByDescending(c=>c.Created_Date).FirstOrDefault();
                if (adetails != null)
                    return adetails;
                else
                    return null;
            }
        }
        
        public Token EditToken(string username)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var edetails = (from c in context.token_ack
                                where c.routing_key == username

                                select new Token
                                {
                                    Token_Ack_SNO = c.token_ack_sno,
                                    Access_Token = c.access_token,
                                    Token_Type = c.token_type,
                                    Expire_In = c.expire_in,
                                    Routing_Key = c.routing_key,
                                    Created_Date = (DateTime)c.created_date
                                }).OrderByDescending(k => k.Token_Ack_SNO).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteSMTP(long no)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var noteDetails = (from n in context.token_ack
                                   where n.token_ack_sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.token_ack.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateSMTP(Token dep)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var UpdateContactInfo = (from u in context.token_ack
                                         where u.token_ack_sno == dep.Token_Ack_SNO
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.created_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        public bool Validateduplicatedata(long sno)
        {
            using (BIZINVOICEEntities context = new BIZINVOICEEntities())
            {
                var validation = (from c in context.token_ack
                                  where (c.token_ack_sno == sno)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion Methods
    }
}

