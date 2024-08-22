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


                    string body = data.Email_Text.Replace("}+uname+{", username).Replace(" }+pwd+{", pwd + "\n").Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");

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
                   
                    string body = string.Format("{0},You have Successfully registered on JICHANGE Portal, {1} Your account is pending for approval. ", company, email);
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
                    string body = string.Format("{0},JICHANGE Confirmation code for delivery is {1}, verify through this link: {2}", email, otp, linkurl);
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

                    mm.Body = string.Format("Hello {0}, \n Kindly pay {1} for invoice number {2}.Payment reference number is {3}.\n Regards,\n{4} ", customername, amount, invoiceno, controlno, vendor);
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
                    string pdfPath = GenerateCustomizedInvoicePdf(invoiceno);
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
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = "Invoice Amendment";
                    mm.Body = string.Format("Hello {0}, Invoice number {1} has been amended, New invoice amount is {2}, reference number for payment is {3}. Regards,{4} ", customername, invoiceno, amount, controlno, vendor);

                    if (data != null)
                    {
                        mm.Subject = data.Subject;

                        /*  Hello "}+customername+{", Invoice number "}+invno+{" has been amended, New invoice amount is "}+amount+{". reference number for payment is "}+controlno+{". Regards, "}+vendor+{" */

                        string content = data.Email_Text.Replace("}+customername+{", customername + "\n").Replace("}+invno+{", invoiceno).Replace("}+amount+{", amount).Replace("}+controlno+{", controlno + "\n").Replace("}+vendor+{", vendor);

                        mm.Body = content;
                    }
                   
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
        public static void SendCustomerCancelledInvoiceEmail(string email, string customername, string invoiceno, string controlno, string vendor, string amount)
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
                    var data = em.GetLatestEmailTextsListByFlow("4");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Subject;
                    mm.Subject = "Invoice Cancellation";
                    mm.Body = string.Format("Hello {0}, invoice number {1} with reference number {2} has been cancelled. Reach out for new order and invoice. Regards,{3} ", customername, invoiceno, controlno, vendor);

                    if (data != null)
                    {
                        mm.Subject = data.Subject;

                        /*  Hello "}+customername+{", Invoice number "}+invno+{" with reference number "}+controlno+{" has been cancelled. Reach out for new order and invoice. Regards, "}+vendor+{" */

                        string content = data.Email_Text.Replace("}+customername+{", customername + "\n").Replace("}+invno+{", invoiceno).Replace("}+controlno+{", controlno + "\n").Replace("}+vendor+{", vendor);

                        mm.Body = content;
                    }



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


        #region  Invoice Pdf
        public static string GenerateCustomizedInvoicePdf(string invoiceno)
        {

            // Get Invoice and Invoice Items from Invoiceno here

            var invoice = new INVOICE().GetInvoiceByInvoiceNo(invoiceno);
            var invoiceitems = new INVOICE().GetInvoiceDetails(invoice.Inv_Mas_Sno);

            var companyinfo = new CompanyBankMaster().FindCompanyById(invoice.CompanySno);

            //string path = "/Invoices/";

            // Set the file path for the PDF
            string filePath = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath() + ConfigurationManager.AppSettings["invoices"], $"{invoice.Cust_Name}_{invoice.Invoice_No}.pdf");
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

            // Add a blank line after the logo
            //document.Add(new Paragraph("\n"));
            // Step 1: Create a Paragraph
            Paragraph paragraph = new Paragraph();

            // Step 2: Add Left-Aligned Content
            Chunk leftContent = new Chunk("Left Content");

            // Step 3: Add a Tab to Push Content to the Right
            Chunk tab = new Chunk(new VerticalPositionMark()); // Acts as a spacer to the right

            // Step 4: Add Right-Aligned Content
            Chunk rightContent = new Chunk("Right Content");

            // Step 5: Add Content to the Paragraph
            paragraph.Add(leftContent);
            paragraph.Add(tab);  // Adds a "tab" space
            paragraph.Add(rightContent);
            // Step 2: Add Invoice Header
            Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.GRAY);
            Font headerFontbelow = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.GRAY);
            Paragraph header = new Paragraph("INVOICE", headerFont);
            header.Alignment = Element.ALIGN_CENTER;
            Paragraph header1 = new Paragraph("Company Name : " + companyinfo.CompName, headerFontbelow);
            header1.Alignment = Element.ALIGN_LEFT;
            header1.Add(leftContent);
            header1.Add(tab);
            Paragraph header2 = new Paragraph("Company Address : " + companyinfo.Address, headerFontbelow);
            header2.Alignment = Element.ALIGN_LEFT;
            Paragraph header3 = new Paragraph("Company Tin : " + companyinfo.TinNo, headerFontbelow);
            header3.Alignment = Element.ALIGN_LEFT;
            Paragraph header4 = new Paragraph("Company Mobile : " + companyinfo.MobNo, headerFontbelow);
            header4.Alignment = Element.ALIGN_LEFT;
            Paragraph header5 = new Paragraph("Customer Name : " + invoice.Cust_Name, headerFontbelow);
            header5.Alignment = Element.ALIGN_RIGHT;
            Paragraph header6 = new Paragraph("Control Number : " + invoice.Control_No, headerFontbelow);
            header6.Alignment = Element.ALIGN_RIGHT;
            Paragraph header7 = new Paragraph("Invoice No : " + invoice.Invoice_No, headerFontbelow);
            header7.Alignment = Element.ALIGN_RIGHT;
            Paragraph header8 = new Paragraph("Date Created : " + invoice.Invoice_Date, headerFontbelow);
            header8.Alignment = Element.ALIGN_RIGHT;

            document.Add(header);
            document.Add(header1);
            document.Add(header2);
            document.Add(header3);
            document.Add(header4);
            document.Add(header5);
            document.Add(header6);
            document.Add(header7);
            document.Add(header8);

            // Add a blank line after the header
            document.Add(new Paragraph("\n"));

            // Step 3: Create Table for Invoice Details
            PdfPTable table = new PdfPTable(4); // 4 columns
            table.WidthPercentage = 100; // Table width as percentage of page width
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            // Set column widths
            float[] columnWidths = { 2f, 1f, 2f, 1f };
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
            PdfPCell totalCell = new PdfPCell(new Phrase("Total", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
            totalCell.BackgroundColor = BaseColor.GRAY;
            totalCell.Colspan = 3;
            totalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            totalCell.Padding = 8f;
            table.AddCell(totalCell);

            PdfPCell totalValueCell = new PdfPCell(new Phrase(invoice.Item_Total_Amount.ToString("N2") +" "+ invoice.Currency_Code, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Padding = 8f
            };
            table.AddCell(totalValueCell);

            // Add the table to the document
            document.Add(table);

            // Step 6: Add Footer with Thank You Message
            Paragraph footer = new Paragraph("System Generated Invoice." + " Date : " + System.DateTime.Now, FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.GRAY))
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 12f
            };
            document.Add(footer);

            /*Paragraph footer2 = new Paragraph("Thank you for your business!", FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.GRAY))
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 12f
            };
            document.Add(footer2);

            Paragraph footer1 = new Paragraph("Thank you for your business!", FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.GRAY))
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 12f
            };
            document.Add(footer1);
            */
            // Close the document
            document.Close();

            return filePath;
        }

        private static void AddTableHeader(PdfPTable table, string headerText)
        {
            PdfPCell headerCell = new PdfPCell(new Phrase(headerText, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
            headerCell.BackgroundColor = BaseColor.GRAY;
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.Padding = 8f;
            table.AddCell(headerCell);
        }

        private static void AddTableCell(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, new Font(Font.FontFamily.HELVETICA, 12)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Padding = 8f;
            table.AddCell(cell);
        }


        #endregion
    }
}
