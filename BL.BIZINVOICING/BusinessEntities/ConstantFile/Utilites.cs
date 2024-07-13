using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using DaL.BIZINVOICING.EDMX;
using System.Reflection;
using System.ComponentModel;
namespace BL.BIZINVOICING.BusinessEntities.ConstantFile
{
    public class Utilites
    {
        public const string Reg_URL = "https://virtual.tra.go.tz/efdmsRctApi/api/vfdRegReq";
        public const string Token_URL = "https://virtual.tra.go.tz/efdmsRctApi/vfdtoken";
        public const string INV_URL = "https://virtual.tra.go.tz/efdmsRctApi/api/efdmsRctInfo";
        public const string QR_URL = "https://virtual.tra.go.tz/efdmsRctVerify/";
        public const string CERT_KEY = "10TZ100576";
        public const string CERT_SERIAL = "747d90b923284aa2410e1086a9a6f947";
        public const string Test_CN = "TRAEFDTEST01";
        //public static string RemoveSpecialCharacters(string str)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < str.Length; i++)
        //    {
        //        if ((str[i] >= '0' && str[i] <= '9') || (str[i] >= 'A' && str[i] <= 'z') || (str[i]. ==''))

        //        {
        //            sb.Append(str[i]);
        //        }
        //    }

        //    return sb.ToString();
        //}
        public static String RemoveSpecialCharacters(string str)
        {

            // return Regex.Replace(str, "[^a-zA-Z0-9\\s&.]+", "", RegexOptions.IgnoreCase);
            return Regex.Replace(str, "/[^a-zA-Z0-9\\s\\/]/", "", RegexOptions.IgnoreCase);
        }
        //public string RemoveSpecialChars(string input)
        //{
        //    return Regex.Replace(input, @"[^0-9a-zA-Z\._]", string.Empty);
        //}
        //   void bbb()
        //    {
        //        var modifiedEntities = ChangeTracker.Entries()
        //        .Where(p => p.State == EntityState.Modified).ToList();
        //        var now = DateTime.UtcNow;

        //        foreach (var change in modifiedEntities)
        //        {
        //            var entityName = change.Entity.GetType().Name;
        //            var primaryKey = GetPrimaryKeyValue(change);

        //            foreach (var prop in change.OriginalValues.PropertyNames)
        //            {
        //                var originalValue = change.OriginalValues[prop].ToString();
        //                var currentValue = change.CurrentValues[prop].ToString();
        //                if (originalValue != currentValue)
        //                {
        //                    ChangeLog log = new ChangeLog()
        //                    {
        //                        EntityName = entityName,
        //                        PrimaryKeyValue = primaryKey.ToString(),
        //                        PropertyName = prop,
        //                        OldValue = originalValue,
        //                        NewValue = currentValue,
        //                        DateChanged = now
        //                    };
        //                    ChangeLogs.Add(log);
        //                }
        //            }
        //        }
        //    }
        //void bind(String auditype,String tablename, String oldvalues,String newvalues,string audityby)
        //    {
        //        //var names = typeof(Administrator).GetProperties().Select(property => property.Name).ToArray();
        //        var col = typeof(tablename).GetProperties().Select(x => x.Name).ToList();
        //        al.Audit_Type = audityby;
        //        al.Table_Name = tablename;
        //        al.Columnsname = col.;
        //        al.Oldvalues = oldvalues;
        //        al.Newvalues = newvalues;
        //        al.AuditBy = audityby;
        //    }

        //public static Regex()
        //{
        //    _regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
        //          RegexOptions.CultureInvariant | RegexOptions.Compiled);
        //}
      public static bool  IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public static string GetEncryptedData(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        public static string DecodeFrom64(string password)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(password);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        public static void logfile(String ClassName, String ErrorNumber, String ErrorMessage)
        {
            System.IO.StreamWriter sw = null;
            String Message = "";

           
                try
                {
                    string path= @"D:\Logs\Clogs.txt";
                    sw = new System.IO.StreamWriter(path, true);
                    Message = Environment.NewLine + "Date: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + Environment.NewLine + "ClassName: " + ClassName + Environment.NewLine + "ErrorNumber: " + ErrorNumber + Environment.NewLine + "ErrorMessage: " + ErrorMessage + Environment.NewLine;
                    sw.WriteLine(Message);
                }
                catch (Exception ex)
                {
                    try
                    {
                        String exception = ex.ToString();
                    sw = new System.IO.StreamWriter("~/Logs/Clogs.txt", true);
                    //sw = new System.IO.StreamWriter(@"D:\SAIT\logs\aarogyasri.txt", true);
                    Message = Environment.NewLine + "Date: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + Environment.NewLine + "ClassName: Debug" + Environment.NewLine + "ErrorMessage:" + ex.ToString() + Environment.NewLine;
                        sw.WriteLine(Message);
                    }
                    catch (Exception Ex1)
                    { }

                }
                finally
                {
                    sw.Close();
                }
            

        }
    }
}
