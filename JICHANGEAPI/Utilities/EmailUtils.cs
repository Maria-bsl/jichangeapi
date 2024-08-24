using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BL.BIZINVOICING.BusinessEntities.Masters;
using System.Runtime.Remoting.Messaging;
using System.Configuration;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Hosting;
using iTextSharp.text.pdf.draw;

namespace JichangeApi.Utilities
{
    public class EmailUtils 
    {
        private readonly Payment pay = new Payment();
        public static void SendActivationEmail(string email, string fullname, string pwd, string username)
        {
            EMAIL em = new EMAIL();
            S_SMTP ss = new S_SMTP();
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();

                using (MailMessage mm = new MailMessage())
                {
                    var m = ss.getSMTPText();
                    var data = em.GetLatestEmailTextsListByFlow("1");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Subject;
                    string drt = data.Email_Text;
                    /*var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Loginnew", "Loginnew"),
                       Query = null,
                   };

                    Uri uri = urlBuilder.Uri;*/
                    //string url = "web_url";
                    string weburl = ConfigurationManager.AppSettings["MyWebUrl"];
                    string url = "<a href='" + weburl + "' target='_blank'>" + weburl + "</a>";
                    //location.href = '/Loginnew/Loginnew';

                    //String body = data.Email_Text.Replace("}+cName+{", uname).Replace("}+uname+{", auname).Replace("}+pwd+{", pwd).Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");

                    //Welcome to Jichange Portal, login name: }+uname+{   Password: }+pwd+{  Join through the link below.  }+actLink+{  Change the password immediately after logging into the system.  All the best,  Jichange Team.}


                    string body = data.Email_Text.Replace("}+uname+{", username).Replace(" }+pwd+{", pwd).Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");

                    //m1(weburl);
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    if (string.IsNullOrEmpty(m.SMTP_UName))
                    {
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.Host = m.SMTP_Address;
                    }
                    else
                    {
                        smtp.Host = m.SMTP_Address;
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.EnableSsl = Convert.ToBoolean(m.SSL_Enable);
                        NetworkCredential NetworkCred = new NetworkCredential(m.SMTP_UName, Utilites.DecodeFrom64(m.SMTP_Password));
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                    }
                    smtp.Send(mm);
                }
            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                // Utilites.logfile("lcituion user", drt, Ex.ToString());
                Payment pay = new Payment
                {
                    Message = Ex.ToString()
                };
                pay.AddErrorLogs(pay);
            }

        }

        public static void SendSuccessEmail(string email, string company)
        {
            EMAIL em = new EMAIL();
            S_SMTP ss = new S_SMTP();
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();

                using (MailMessage mm = new MailMessage())
                {
                    var m = ss.getSMTPText();
                    var data = em.GetLatestEmailTextsListByFlow("1");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Subject;
                    string drt = data.Email_Text;
                    /*var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Loginnew", "Loginnew"),
                       Query = null,
                   };

                    Uri uri = urlBuilder.Uri;*/
                    //string url = "web_url";
                    string weburl = ConfigurationManager.AppSettings["MyWebUrl"];
                    string url = "<a href='" + weburl + "' target='_blank'>" + weburl + "</a>";
                   
                    string body = string.Format("Hello {0},<br /> You have Successfully registered on JICHANGE Portal, <br />{1} Your account is pending for approval. ", company, email);
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    if (string.IsNullOrEmpty(m.SMTP_UName))
                    {
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.Host = m.SMTP_Address;
                    }
                    else
                    {
                        smtp.Host = m.SMTP_Address;
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.EnableSsl = Convert.ToBoolean(m.SSL_Enable);
                        NetworkCredential NetworkCred = new NetworkCredential(m.SMTP_UName, Utilites.DecodeFrom64(m.SMTP_Password));
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                    }
                    smtp.Send(mm);
                }
            }
            catch (Exception Ex)
            {
                //throw new Exception(Ex.ToString());
                Payment pay = new Payment
                {
                    Message = Ex.ToString()
                };
                pay.AddErrorLogs(pay);
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                // Utilites.logfile("lcituion user", drt, Ex.ToString());
            }

        }


        public static void SendCustomerDeliveryCodeEmail(string email, string otp, string mobile)
        {
            EMAIL em = new EMAIL();
            S_SMTP ss = new S_SMTP();
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();

                using (MailMessage mm = new MailMessage())
                {
                    var m = ss.getSMTPText();
                    var data = em.GetLatestEmailTextsListByFlow("6"); // OTP
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Subject;
                    string drt = data.Email_Text;
                    /*var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Loginnew", "Loginnew"),
                       Query = null,
                   };

                    Uri uri = urlBuilder.Uri;*/
                    //string url = "web_url";
                    string weburl = ConfigurationManager.AppSettings["MyWebUrl"];
                    string url = "<a href='" + weburl + "' target='_blank'>" + weburl + "</a>";
                    string encrypt = PasswordGeneratorUtil.GetEncryptedData(mobile);// MjU1NzUzNjg4ODY3
                    var linkurl = ConfigurationManager.AppSettings["MyCodeUrl"] + encrypt;
                    string body = string.Format("Hello {0},<br /> JICHANGE Confirmation code for delivery is {1},<br /> verify through this link: {2}", email, otp, linkurl);
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    if (string.IsNullOrEmpty(m.SMTP_UName))
                    {
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.Host = m.SMTP_Address;
                    }
                    else
                    {
                        smtp.Host = m.SMTP_Address;
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.EnableSsl = Convert.ToBoolean(m.SSL_Enable);
                        NetworkCredential NetworkCred = new NetworkCredential(m.SMTP_UName, Utilites.DecodeFrom64(m.SMTP_Password));
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                    }
                    smtp.Send(mm);
                }
            }
            catch (Exception Ex)
            {
                //throw new Exception(Ex.ToString());
                Payment pay = new Payment
                {
                    Message = Ex.ToString()
                };
                pay.AddErrorLogs(pay);
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                // Utilites.logfile("lcituion user", drt, Ex.ToString());
            }

        }

        public static void SendSubjectTextBodyEmail(string email,string subject,string text,string body)
        {
            try
            {
                S_SMTP smtp = new S_SMTP();
                SmtpClient smtpClient = new SmtpClient();
                MailMessage mailMessage = new MailMessage();
                var esmtp = smtp.getSMTPText();
                if (esmtp == null) throw new ArgumentException("Error occured with the smtp");
                int port = Int32.Parse(esmtp.SMTP_Port);
                if (string.IsNullOrEmpty(esmtp.SMTP_UName))
                {
                    smtpClient = new SmtpClient(esmtp.SMTP_Address, port);
                    mailMessage = new MailMessage(esmtp.From_Address, email, subject, body);
                    mailMessage.IsBodyHtml = true;
                }
                else
                {
                    mailMessage = new MailMessage(esmtp.From_Address, email, subject, body);
                    mailMessage.IsBodyHtml = true;
                    smtpClient = new SmtpClient(esmtp.SMTP_Address, port);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = Convert.ToBoolean(esmtp.SSL_Enable);
                    smtpClient.Credentials = new NetworkCredential(esmtp.SMTP_UName, Utilites.DecodeFrom64(esmtp.SMTP_Password));
                }
                smtpClient.Send(mailMessage);
            }
            catch (ArgumentException ex)
            {
                Payment pay = new Payment
                {
                    Message = ex.ToString()
                };
                pay.AddErrorLogs(pay);
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                Payment pay = new Payment
                {
                    Message = ex.ToString()
                };
                pay.AddErrorLogs(pay);
                throw new Exception(ex.Message);
            }
        }


        #region  Invoice Mails Section

        /*
         *  New Invoice:
            Hello (customer name), 
            Kindly pay (currency & amount) for invoice number (invoice number). Payment reference number is (control number).
            Regards,
            (Vendor name)

            ....for email you can attach the invoice....
         */
        public static void SendCustomerNewInvoiceEmail(string email, string customername, string invoiceno, string controlno, string vendor, string amount)
        {
            EMAIL em = new EMAIL();
            S_SMTP ss = new S_SMTP();
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();

                using (MailMessage mm = new MailMessage())
                {
                    var m = ss.getSMTPText();
                    var data = em.GetLatestEmailTextsListByFlow("2"); // Invoice Generation
                    /*Hello { 0}, Kindly pay { 1}
                    for invoice number { 2}. Payment reference number is { 3 }. Regards, { 4}
                    "*/
                    mm.Body = string.Format("Hello {0}, <br /> Kindly pay {1} for invoice number {2}. <br />Payment reference number is {3}. <br /><br />Regards, <br />{4}", customername, amount, invoiceno, controlno, vendor);
                    mm.Subject = "INVOICE";

                    if (data != null)
                    {
                        mm.Subject = data.Subject;
                        
                        /*  Hello "}+customername+{", Kindly pay "}+amount+{" for invoice number "}+invno+{". Payment reference number is "}+controlno+{". Regards, "}+vendor+{" */

                       string content = data.Email_Text.Replace("}+customername+{", customername ).Replace("}+amount+{", amount).Replace("}+invno+{", invoiceno).Replace("}+controlno+{", controlno).Replace("}+vendor+{", vendor);

                        mm.Body = content;
                    }
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);

                    //string body = string.Format("Hello {0}, Kindly pay {1} for invoice number {2}.Payment reference number is {3}. Regards,{4} ", customername, amount, invoiceno, controlno, vendor);


                    /* Attach PDF Invoice here */
                    string pdfPath = GenerateNewInvoicePdf(invoiceno);
                    Attachment pdfAttachment = new Attachment(pdfPath);
                    mm.Attachments.Add(pdfAttachment);


                    mm.IsBodyHtml = true;
                    if (string.IsNullOrEmpty(m.SMTP_UName))
                    {
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.Host = m.SMTP_Address;
                    }
                    else
                    {
                        smtp.Host = m.SMTP_Address;
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.EnableSsl = Convert.ToBoolean(m.SSL_Enable);
                        NetworkCredential NetworkCred = new NetworkCredential(m.SMTP_UName, Utilites.DecodeFrom64(m.SMTP_Password));
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                    }
                    smtp.Send(mm);
                }
            }
            catch (Exception Ex)
            {Payment pay = new Payment { Message = Ex.ToString() };
                pay.AddErrorLogs(pay);
            }

        }

        /*
            Invoice Ammendment:
            Hello (vendor name),
            Invoice number (invoice number) has been amended. New invoice amount is (currency & amount), reference number for payment is (control number).
            Regards,
            (Vendor name)
         */
        public static void SendCustomerAmmendedInvoiceEmail(string email, string customername, string invoiceno, string controlno, string vendor, string amount)
        {
            EMAIL em = new EMAIL();
            S_SMTP ss = new S_SMTP();
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();

                using (MailMessage mm = new MailMessage())
                {
                    var m = ss.getSMTPText();
                    var data = em.GetLatestEmailTextsListByFlow("5"); // Invoice Amendment
                 
                    mm.Subject = "Invoice Amendment";
                    mm.Body = string.Format("Hello {0},<br /> Invoice number {1} has been amended.<br /> New invoice amount is {2}, reference number for payment is {3}. <br /><br />Regards, <br />{4} ", customername, invoiceno, amount, controlno, vendor);

                    if (data != null)
                    {
                        mm.Subject = data.Subject;

                        /*  Hello "}+customername+{", Invoice number "}+invno+{" has been amended, New invoice amount is "}+amount+{". reference number for payment is "}+controlno+{". Regards, "}+vendor+{" */

                        string content = data.Email_Text.Replace("}+customername+{", customername + "\n").Replace("}+invno+{", invoiceno).Replace("}+amount+{", amount).Replace("}+controlno+{", controlno + "\n").Replace("}+vendor+{", vendor);

                        mm.Body = content;
                    }


                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);


                    /* Attach PDF Invoice here */
                    string pdfPath = AmmendedInvoicePdf(invoiceno);
                    Attachment pdfAttachment = new Attachment(pdfPath);
                    mm.Attachments.Add(pdfAttachment);


                    mm.IsBodyHtml = true;
                    if (string.IsNullOrEmpty(m.SMTP_UName))
                    {
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.Host = m.SMTP_Address;
                    }
                    else
                    {
                        smtp.Host = m.SMTP_Address;
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.EnableSsl = Convert.ToBoolean(m.SSL_Enable);
                        NetworkCredential NetworkCred = new NetworkCredential(m.SMTP_UName, Utilites.DecodeFrom64(m.SMTP_Password));
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                    }
                    smtp.Send(mm);
                }
            }
            catch (Exception Ex)
            {Payment pay = new Payment { Message = Ex.ToString() };
                pay.AddErrorLogs(pay);
            }

        }

        /*
         *  Cancel Invoice:
            Hello (customer name),
            Invoice number (invoice number) with reference number (control number) has been cancelled. Reach us for new order and invoice.
            Regards,
            (Vendor Name)

            ...... For email you can attach cancelled Invoice with reason descriptions....
         */
        public static void SendCustomerCancelledInvoiceEmail(string email, string customername, string invoiceno, string controlno, string vendor)
        {
            EMAIL em = new EMAIL();
            S_SMTP ss = new S_SMTP();
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();

                using (MailMessage mm = new MailMessage())
                {
                    var m = ss.getSMTPText();
                    var data = em.GetLatestEmailTextsListByFlow("4"); // Invoice Cancellation

                    mm.Subject = "Invoice Cancellation";
                    mm.Body = string.Format("Hello {0}, <br /> invoice number {1} with reference number {2} has been cancelled.<br /> Reach out for new order and invoice.<br /><br /> Regards, <br />{3}", customername, invoiceno, controlno, vendor);

                    if (data != null)
                    {
                        mm.Subject = data.Subject;

                        /*  Hello "}+customername+{", Invoice number "}+invno+{" with reference number "}+controlno+{" has been cancelled. Reach out for new order and invoice. Regards, "}+vendor+{" */

                        string content = data.Email_Text.Replace("}+customername+{", customername).Replace("}+invno+{", invoiceno).Replace("}+controlno+{", controlno ).Replace("}+vendor+{", vendor);

                        mm.Body = content;
                    }

                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);

                    /* Attach PDF Invoice here */
                    string pdfPath = CancelledInvoicePdf(invoiceno);
                    Attachment pdfAttachment = new Attachment(pdfPath);
                    mm.Attachments.Add(pdfAttachment);

                    mm.IsBodyHtml = true;
                    if (string.IsNullOrEmpty(m.SMTP_UName))
                    {
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.Host = m.SMTP_Address;
                    }
                    else
                    {
                        smtp.Host = m.SMTP_Address;
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.EnableSsl = Convert.ToBoolean(m.SSL_Enable);
                        NetworkCredential NetworkCred = new NetworkCredential(m.SMTP_UName, Utilites.DecodeFrom64(m.SMTP_Password));
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                    }
                    smtp.Send(mm);
                }
            }
            catch (Exception Ex)
            {Payment pay = new Payment { Message = Ex.ToString() };
                pay.AddErrorLogs(pay);
            }

        }


        #endregion


        #region  Invoice Pdf for Attachment
        public static string GenerateNewInvoicePdf(string invoiceno)
        {

            // Get Invoice and Invoice Items from Invoiceno here

            var invoice = new INVOICE().GetInvoiceByInvoiceNo(invoiceno);
            var invoiceitems = new INVOICE().GetInvoiceDetails(invoice.Inv_Mas_Sno);

            var invoice_date = invoice.Invoice_Date.GetValueOrDefault().Date;
            var date_issued = invoice_date.ToString("yyyy-MM-dd");

            var companyinfo = new CompanyBankMaster().FindCompanyById(invoice.CompanySno);

            //string path = "/Invoices/";

            // Set the file path for the PDF
            string filePath = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath() + ConfigurationManager.AppSettings["invoices"], $"{invoice.Cust_Name}_{invoice.Inv_Mas_Sno}_new.pdf");
            string filePath1 = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath(), $"Invoice_{invoice.Invoice_No}.pdf");

            string filePath2 = ConfigurationManager.AppSettings["invoices"] + $"Invoice_{invoice.Invoice_No}.pdf";

            // Create a new PDF document
            Document document = new Document(PageSize.A4, 25, 25, 10, 10);
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            // Step 1: Add Company Logo
            string logoPath = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath() + ConfigurationManager.AppSettings["invoices"], "logo.png"); // Path to your logo image

            string logoPath1 = HostingEnvironment.ApplicationHost.GetPhysicalPath() + ConfigurationManager.AppSettings["invoices"] + "logo.png"; // Path to your logo image
            if (File.Exists(logoPath))
            {
                Image logo = Image.GetInstance(logoPath);
                logo.ScalePercent(24f); // Resize the logo if needed
                logo.Alignment = Element.ALIGN_CENTER;
                document.Add(logo);
            }

            BaseColor LabelColor = new BaseColor(37, 150, 190); // RGB Base custom color
            BaseColor HeaderColor = new BaseColor(12, 98, 133, 255); // for table header and total background
            BaseColor tableColor = new BaseColor(11, 99, 133);
            BaseColor ShadesColor = new BaseColor(12, 103, 148);
            BaseColor textColor = new BaseColor(20, 36, 44); 

            // Step 1.0: Create a Paragraph
            Paragraph paragraph = new Paragraph();
            Paragraph paragraph1 = new Paragraph();
            Paragraph paragraph2 = new Paragraph();
            Paragraph paragraph3 = new Paragraph();
           
            // Step 1.1: Add a Tab to Push Content to the Right
            Chunk tab = new Chunk(new VerticalPositionMark()); // Acts as a spacer to the right  BaseColor.GRAY

            // Step 2: Add Invoice Header
            Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, HeaderColor);
            Font headerFontbelow = FontFactory.GetFont(FontFactory.HELVETICA, 12, textColor);
            Paragraph header = new Paragraph("INVOICE", headerFont);
            header.Alignment = Element.ALIGN_CENTER;
            Chunk leftContent1 = new Chunk("Company Name : " + companyinfo.CompName, headerFontbelow);
            paragraph.Add(leftContent1);
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.Add(tab);
            Chunk rightContent5 = new Chunk("Customer Name : " + invoice.Cust_Name, headerFontbelow);
            paragraph.Add(rightContent5);
            paragraph.Alignment = Element.ALIGN_RIGHT;
            Chunk leftContent2 = new Chunk("Company Address : " + companyinfo.Address, headerFontbelow);
            paragraph1.Add(leftContent2);
            paragraph1.Alignment = Element.ALIGN_LEFT;
            paragraph1.Add(tab);
            Chunk rightContent6 = new Chunk("Control Number : " + invoice.Control_No + " - " + invoice.Payment_Type, headerFontbelow);
            paragraph1.Add(rightContent6);
            paragraph1.Alignment = Element.ALIGN_RIGHT;
            Chunk leftContent3 = new Chunk("Company Tin : " + companyinfo.TinNo, headerFontbelow);
            paragraph2.Add(leftContent3);
            paragraph2.Alignment = Element.ALIGN_LEFT;
            paragraph2.Add(tab);
            Chunk rightContent7 = new Chunk("Invoice No : " + invoice.Invoice_No , headerFontbelow);
            paragraph2.Add(rightContent7);
            paragraph2.Alignment = Element.ALIGN_RIGHT;
            Chunk leftContent4 = new Chunk("Company Mobile : " + companyinfo.MobNo, headerFontbelow);
            paragraph3.Add(leftContent4);
            paragraph3.Alignment = Element.ALIGN_LEFT;
            paragraph3.Add(tab);
            Chunk rightContent8 = new Chunk("Date Created : " + date_issued, headerFontbelow);
            paragraph3.Add(rightContent8);
            paragraph3.Alignment = Element.ALIGN_RIGHT;

            document.Add(header);
            document.Add(paragraph);
            document.Add(paragraph2);
            document.Add(paragraph3);
            document.Add(paragraph1);

            // Add a blank line after the header
            document.Add(new Paragraph("\n"));

            // Step 3: Create Table for Invoice Details
            PdfPTable table = new PdfPTable(4)
            {
                WidthPercentage = 100, // Table width as percentage of page width
                SpacingBefore = 20f,
                SpacingAfter = 30f
            }; // 4 columns

            // Set column widths
            float[] columnWidths = { 3f, 1.2f, 1.3f, 2f };
            table.SetWidths(columnWidths);

            // Add table headers
            AddTableHeader(table, "Description");
            AddTableHeader(table, "Quantity");
            AddTableHeader(table, "Unit Price");
            AddTableHeader(table, "Amount");

            // Step 4: Add Invoice Items
            foreach (var item in invoiceitems)
            {
                AddTableCell(table, item.Item_Description);
                AddTableCell(table, item.Item_Qty.ToString());
                AddTableCell(table, item.Item_Unit_Price.ToString("N2"));
                AddTableCell(table, (item.Item_Qty * item.Item_Unit_Price).ToString("N2"));
            }
           
            // Step 5: Add Total Row
            PdfPCell totalCell = new PdfPCell(new Phrase("Total", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)))
            {
                BackgroundColor = HeaderColor,
                Colspan = 3,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Padding = 8f
            };
            table.AddCell(totalCell);

            PdfPCell totalValueCell = new PdfPCell(new Phrase(invoice.Item_Total_Amount.ToString("N2") +" "+ invoice.Currency_Code, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Padding = 8f
            };
            table.AddCell(totalValueCell);

            // Add the table to the document
            document.Add(table);

            // Step 6: How to Pay
            Paragraph footer_how = new Paragraph("How to Pay:", FontFactory.GetFont(FontFactory.HELVETICA, 10, textColor))
            {
                Alignment = Element.ALIGN_LEFT,
                SpacingBefore = 12f
            };
            document.Add(footer_how);
            // Lipia kwa kupiga *150 * 03# au SimBanking App, Tawi lolote la CRDB, CRDB Wakala au mitandao ya simu.
            Paragraph footer_pay = new Paragraph("Pay by dialing *150*03# or SimBanking App, any CRDB Branch, CRDB Agency or mobile networks.", FontFactory.GetFont(FontFactory.HELVETICA, 10, textColor))
            {
                Alignment = Element.ALIGN_LEFT,
                SpacingBefore = 12f
            };
            document.Add(footer_pay);

            document.Add(new Paragraph("\n"));

            // Step 7: Add Footer with Thank You Message
            Paragraph footer = new Paragraph("System Generated Invoice." + " Date : " + System.DateTime.Now, FontFactory.GetFont(FontFactory.HELVETICA, 10, textColor))
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 12f
            };
            document.Add(footer);

          
            // Close the document
            document.Close();

            return filePath;
        }

        
        public static string AmmendedInvoicePdf(string invoiceno)
        {
            // Get Invoice and Invoice Items from Invoiceno here

            var invoice = new INVOICE().GetInvoiceByInvoiceNoAmend(invoiceno);
            var invoiceitems = new INVOICE().GetInvoiceDetails(invoice.Inv_Mas_Sno);

            var invoice_date = invoice.Invoice_Date.GetValueOrDefault().Date;
            var date_issued = invoice_date.ToString("yyyy-MM-dd");

            var companyinfo = new CompanyBankMaster().FindCompanyById(invoice.CompanySno);

            //string path = "/Invoices/";

            // Set the file path for the PDF
            string filePath = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath() + ConfigurationManager.AppSettings["invoices"], $"{invoice.Cust_Name}_{invoice.Inv_Mas_Sno}_amended.pdf");
            string filePath1 = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath(), $"Invoice_{invoice.Invoice_No}.pdf");

            string filePath2 = ConfigurationManager.AppSettings["invoices"] + $"Invoice_{invoice.Invoice_No}.pdf";

            // Create a new PDF document
            Document document = new Document(PageSize.A4, 25, 25, 10, 10);
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            // Step 1: Add Company Logo
            string logoPath = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath() + ConfigurationManager.AppSettings["invoices"], "logo.png"); // Path to your logo image

            string logoPath1 = HostingEnvironment.ApplicationHost.GetPhysicalPath() + ConfigurationManager.AppSettings["invoices"] + "logo.png"; // Path to your logo image
            if (File.Exists(logoPath))
            {
                Image logo = Image.GetInstance(logoPath);
                logo.ScalePercent(24f); // Resize the logo if needed
                logo.Alignment = Element.ALIGN_CENTER;
                document.Add(logo);
            }

            BaseColor LabelColor = new BaseColor(37, 150, 190); // RGB Base custom color
            BaseColor HeaderColor = new BaseColor(12, 98, 133, 255); // for table header and total background
            BaseColor tableColor = new BaseColor(11, 99, 133);
            BaseColor ShadesColor = new BaseColor(12, 103, 148);
            BaseColor textColor = new BaseColor(20, 36, 44);

            // Step 1.0: Create a Paragraph
            Paragraph paragraph = new Paragraph();
            Paragraph paragraph1 = new Paragraph();
            Paragraph paragraph2 = new Paragraph();
            Paragraph paragraph3 = new Paragraph();

            // Step 1.1: Add a Tab to Push Content to the Right
            Chunk tab = new Chunk(new VerticalPositionMark()); // Acts as a spacer to the right  BaseColor.GRAY

            // Step 2: Add Invoice Header
            Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, HeaderColor);
            Font headerFontbelow = FontFactory.GetFont(FontFactory.HELVETICA, 12, textColor);
            Paragraph header = new Paragraph("AMENDED INVOICE", headerFont);
            header.Alignment = Element.ALIGN_CENTER;
            Chunk leftContent1 = new Chunk("Company Name : " + companyinfo.CompName, headerFontbelow);
            paragraph.Add(leftContent1);
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.Add(tab);
            Chunk rightContent5 = new Chunk("Customer Name : " + invoice.Cust_Name, headerFontbelow);
            paragraph.Add(rightContent5);
            paragraph.Alignment = Element.ALIGN_RIGHT;
            Chunk leftContent2 = new Chunk("Company Address : " + companyinfo.Address, headerFontbelow);
            paragraph1.Add(leftContent2);
            paragraph1.Alignment = Element.ALIGN_LEFT;
            paragraph1.Add(tab);
            Chunk rightContent6 = new Chunk("Control Number : " + invoice.Control_No + " - " + invoice.Payment_Type, headerFontbelow);
            paragraph1.Add(rightContent6);
            paragraph1.Alignment = Element.ALIGN_RIGHT;
            Chunk leftContent3 = new Chunk("Company Tin : " + companyinfo.TinNo, headerFontbelow);
            paragraph2.Add(leftContent3);
            paragraph2.Alignment = Element.ALIGN_LEFT;
            paragraph2.Add(tab);
            Chunk rightContent7 = new Chunk("Invoice No : " + invoice.Invoice_No, headerFontbelow);
            paragraph2.Add(rightContent7);
            paragraph2.Alignment = Element.ALIGN_RIGHT;
            Chunk leftContent4 = new Chunk("Company Mobile : " + companyinfo.MobNo, headerFontbelow);
            paragraph3.Add(leftContent4);
            paragraph3.Alignment = Element.ALIGN_LEFT;
            paragraph3.Add(tab);
            Chunk rightContent8 = new Chunk("Date Created : " + date_issued, headerFontbelow);
            paragraph3.Add(rightContent8);
            paragraph3.Alignment = Element.ALIGN_RIGHT;


            document.Add(header);
            document.Add(paragraph);
            document.Add(paragraph2);
            document.Add(paragraph3);
            document.Add(paragraph1);

            Paragraph reason = new Paragraph("Reason For Amendment : " + invoice.Reason, headerFontbelow);
            header.Alignment = Element.ALIGN_LEFT;

            document.Add(reason);
            // Add a blank line after the header
            document.Add(new Paragraph("\n"));

            // Step 3: Create Table for Invoice Details
            PdfPTable table = new PdfPTable(4)
            {
                WidthPercentage = 100, // Table width as percentage of page width
                SpacingBefore = 20f,
                SpacingAfter = 30f
            }; // 4 columns

            // Set column widths
            float[] columnWidths = { 3f, 1.2f, 1.3f, 2f };
            table.SetWidths(columnWidths);

            // Add table headers
            AddTableHeader(table, "Description");
            AddTableHeader(table, "Quantity");
            AddTableHeader(table, "Unit Price");
            AddTableHeader(table, "Amount");

            // Step 4: Add Invoice Items
            foreach (var item in invoiceitems)
            {
                AddTableCell(table, item.Item_Description);
                AddTableCell(table, item.Item_Qty.ToString());
                AddTableCell(table, item.Item_Unit_Price.ToString("N2"));
                AddTableCell(table, (item.Item_Qty * item.Item_Unit_Price).ToString("N2"));
            }

            // Step 5: Add Total Row
            PdfPCell totalCell = new PdfPCell(new Phrase("Total", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)))
            {
                BackgroundColor = HeaderColor,
                Colspan = 3,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Padding = 8f
            };
            table.AddCell(totalCell);

            PdfPCell totalValueCell = new PdfPCell(new Phrase(invoice.Item_Total_Amount.ToString("N2") + " " + invoice.Currency_Code, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Padding = 8f
            };
            table.AddCell(totalValueCell);

            // Add the table to the document
            document.Add(table);

            // Step 6: How to Pay
            Paragraph footer_how = new Paragraph("How to Pay:", FontFactory.GetFont(FontFactory.HELVETICA, 10, textColor))
            {
                Alignment = Element.ALIGN_LEFT,
                SpacingBefore = 12f
            };
            document.Add(footer_how);
            // Lipia kwa kupiga *150 * 03# au SimBanking App, Tawi lolote la CRDB, CRDB Wakala au mitandao ya simu.
            Paragraph footer_pay = new Paragraph("Pay by dialing *150*03# or SimBanking App, any CRDB Branch, CRDB Agency or mobile networks.", FontFactory.GetFont(FontFactory.HELVETICA, 10, textColor))
            {
                Alignment = Element.ALIGN_LEFT,
                SpacingBefore = 12f
            };
            document.Add(footer_pay);

            document.Add(new Paragraph("\n"));

            // Step 7: Add Footer with Thank You Message
            Paragraph footer = new Paragraph("System Generated Invoice." + " Date : " + System.DateTime.Now, FontFactory.GetFont(FontFactory.HELVETICA, 10, textColor))
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 12f
            };
            document.Add(footer);


            // Close the document
            document.Close();

            return filePath;
        }

        public static string CancelledInvoicePdf(string invoiceno)
        {
            // Get Invoice and Invoice Items from Invoiceno here

            var invoice = new INVOICE().GetInvoiceByInvoiceNoCancelled(invoiceno);
            var invoiceitems = new INVOICE().GetInvoiceDetails(invoice.Inv_Mas_Sno);

            var invoice_date = invoice.Invoice_Date.GetValueOrDefault().Date;
            var date_issued = invoice_date.ToString("yyyy-MM-dd");

            var companyinfo = new CompanyBankMaster().FindCompanyById(invoice.CompanySno);

            //string path = "/Invoices/";

            // Set the file path for the PDF
            string filePath = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath() + ConfigurationManager.AppSettings["invoices"], $"{invoice.Cust_Name}_{invoice.Inv_Mas_Sno}_Cancelled.pdf");
            string filePath1 = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath(), $"Invoice_{invoice.Invoice_No}.pdf");

            string filePath2 = ConfigurationManager.AppSettings["invoices"] + $"Invoice_{invoice.Invoice_No}.pdf";

            // Create a new PDF document
            Document document = new Document(PageSize.A4, 25, 25, 10, 10);
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            // Step 1: Add Company Logo
            string logoPath = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath() + ConfigurationManager.AppSettings["invoices"], "logo.png"); // Path to your logo image

            string logoPath1 = HostingEnvironment.ApplicationHost.GetPhysicalPath() + ConfigurationManager.AppSettings["invoices"] + "logo.png"; // Path to your logo image
            if (File.Exists(logoPath))
            {
                Image logo = Image.GetInstance(logoPath);
                logo.ScalePercent(24f); // Resize the logo if needed
                logo.Alignment = Element.ALIGN_CENTER;
                document.Add(logo);
            }

            BaseColor LabelColor = new BaseColor(37, 150, 190); // RGB Base custom color
            BaseColor HeaderColor = new BaseColor(12, 98, 133, 255); // for table header and total background
            BaseColor tableColor = new BaseColor(11, 99, 133);
            BaseColor ShadesColor = new BaseColor(12, 103, 148);
            BaseColor textColor = new BaseColor(20, 36, 44);  // RED Color

            // Step 1.0: Create a Paragraph
            Paragraph paragraph = new Paragraph();
            Paragraph paragraph1 = new Paragraph();
            Paragraph paragraph2 = new Paragraph();
            Paragraph paragraph3 = new Paragraph();

            // Step 1.1: Add a Tab to Push Content to the Right
            Chunk tab = new Chunk(new VerticalPositionMark()); // Acts as a spacer to the right  BaseColor.GRAY

            // Step 2: Add Invoice Header
            Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, HeaderColor);
            Font headerFontbelow = FontFactory.GetFont(FontFactory.HELVETICA, 12, textColor);
            Paragraph header = new Paragraph("CANCELLED INVOICE", headerFont);
            header.Alignment = Element.ALIGN_CENTER;
            Chunk leftContent1 = new Chunk("Company Name : " + companyinfo.CompName, headerFontbelow);
            paragraph.Add(leftContent1);
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.Add(tab);
            Chunk rightContent5 = new Chunk("Customer Name : " + invoice.Cust_Name, headerFontbelow);
            paragraph.Add(rightContent5);
            paragraph.Alignment = Element.ALIGN_RIGHT;
            Chunk leftContent2 = new Chunk("Company Address : " + companyinfo.Address, headerFontbelow);
            paragraph1.Add(leftContent2);
            paragraph1.Alignment = Element.ALIGN_LEFT;
            paragraph1.Add(tab);
            Chunk rightContent6 = new Chunk("Control Number : " + invoice.Control_No + " - " + invoice.Payment_Type, headerFontbelow);
            paragraph1.Add(rightContent6);
            paragraph1.Alignment = Element.ALIGN_RIGHT;
            Chunk leftContent3 = new Chunk("Company Tin : " + companyinfo.TinNo, headerFontbelow);
            paragraph2.Add(leftContent3);
            paragraph2.Alignment = Element.ALIGN_LEFT;
            paragraph2.Add(tab);
            Chunk rightContent7 = new Chunk("Invoice No : " + invoice.Invoice_No, headerFontbelow);
            paragraph2.Add(rightContent7);
            paragraph2.Alignment = Element.ALIGN_RIGHT;
            Chunk leftContent4 = new Chunk("Company Mobile : " + companyinfo.MobNo, headerFontbelow);
            paragraph3.Add(leftContent4);
            paragraph3.Alignment = Element.ALIGN_LEFT;
            paragraph3.Add(tab);
            Chunk rightContent8 = new Chunk("Date Created : " + date_issued, headerFontbelow);
            paragraph3.Add(rightContent8);
            paragraph3.Alignment = Element.ALIGN_RIGHT;


            document.Add(header);
            document.Add(paragraph);
            document.Add(paragraph2);
            document.Add(paragraph3);
            document.Add(paragraph1);

            Paragraph reason = new Paragraph("Reason For Cancellation : " + invoice.Reason, headerFontbelow);
            header.Alignment = Element.ALIGN_LEFT;

            document.Add(reason);

            // Add a blank line after the header
            document.Add(new Paragraph("\n"));

            // Step 3: Create Table for Invoice Details
            PdfPTable table = new PdfPTable(4)
            {
                WidthPercentage = 100, // Table width as percentage of page width
                SpacingBefore = 20f,
                SpacingAfter = 30f
            }; // 4 columns

            // Set column widths
            float[] columnWidths = { 3f, 1.2f, 1.3f, 2f };
            table.SetWidths(columnWidths);

            // Add table headers
            AddTableHeader(table, "Description");
            AddTableHeader(table, "Quantity");
            AddTableHeader(table, "Unit Price");
            AddTableHeader(table, "Amount");

            // Step 4: Add Invoice Items
            foreach (var item in invoiceitems)
            {
                AddTableCell(table, item.Item_Description);
                AddTableCell(table, item.Item_Qty.ToString());
                AddTableCell(table, item.Item_Unit_Price.ToString("N2"));
                AddTableCell(table, (item.Item_Qty * item.Item_Unit_Price).ToString("N2"));
            }

            // Step 5: Add Total Row
            PdfPCell totalCell = new PdfPCell(new Phrase("Total", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)))
            {
                BackgroundColor = HeaderColor,
                Colspan = 3,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Padding = 8f
            };
            table.AddCell(totalCell);

            PdfPCell totalValueCell = new PdfPCell(new Phrase(invoice.Item_Total_Amount.ToString("N2") + " " + invoice.Currency_Code, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Padding = 8f
            };
            table.AddCell(totalValueCell);

            // Add the table to the document
            document.Add(table);


            document.Add(new Paragraph("\n"));

            // Step 7: Add Footer with Thank You Message
            Paragraph footer = new Paragraph("System Generated Invoice." + " Date : " + System.DateTime.Now, FontFactory.GetFont(FontFactory.HELVETICA, 10, textColor))
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 12f
            };
            document.Add(footer);


            // Close the document
            document.Close();

            // Step 1: Open the created PDF and prepare to add the watermark
            PdfReader reader = new PdfReader(filePath);
            PdfStamper stamper = new PdfStamper(reader, new FileStream(filePath.Replace($"{invoice.Cust_Name}_{invoice.Inv_Mas_Sno}_Cancelled.pdf", $"{ invoice.Cust_Name }_{ invoice.Inv_Mas_Sno }_Cancelled.pdf"), FileMode.Create));

           // PdfStamper stamper = new PdfStamper(reader, new FileStream(filePath.Replace($"{invoice.Cust_Name}_{invoice.Inv_Mas_Sno}_Cancelled.pdf", $"{ invoice.Cust_Name }_{ invoice.Inv_Mas_Sno }_Cancelled_watermarked.pdf"), FileMode.Create));

            int totalPages = reader.NumberOfPages;
            PdfContentByte content;

            // Step 2: Define the watermark text and its properties -- CONFIDENTIAL
            string watermarkText = "CANCELLED";
            BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);
            float fontSize = 60f;
            float xPosition, yPosition;
            float rotationAngle = 45f;

            // Define the custom color (Example:0, 0, 255, 100 semi-transparent blue)
            BaseColor customColor = new BaseColor(255, 0, 0, 128); // RED - RGB with transparency

            // Step 3: Loop through each page and add the watermark
            for (int i = 1; i <= totalPages; i++)
            {
                content = stamper.GetOverContent(i); // Get the content to write over existing content

                // Set the watermark properties
                PdfGState gState = new PdfGState();
                gState.FillOpacity = 0.3f; // Set transparency (0.0 to 1.0)
                content.SetGState(gState);

                content.BeginText();
                content.SetColorFill(customColor);
                content.SetFontAndSize(baseFont, fontSize);

                // Calculate the position to center the watermark on the page
                xPosition = (document.PageSize.Width) / 2;
                yPosition = (document.PageSize.Height) / 2;

                // Apply rotation and alignment
                content.ShowTextAligned(Element.ALIGN_CENTER, watermarkText, xPosition, yPosition, rotationAngle);

                content.EndText();
            }

            // Step 4: Close the stamper and reader
            stamper.Close();
            reader.Close();

            return filePath;
        }



        private static void AddTableHeader(PdfPTable table, string headerText)
        {

            BaseColor LabelColor = new BaseColor(12, 98, 133, 255);

            PdfPCell headerCell = new PdfPCell(new Phrase(headerText, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)))
            {
                BackgroundColor = LabelColor,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 8f
            };
            table.AddCell(headerCell);
        }

        private static void AddTableCell(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, new Font(Font.FontFamily.HELVETICA, 12)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 8f
            };
            table.AddCell(cell);
        }


        #endregion
    }
}
