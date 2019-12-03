using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendEmail
{
    class Program
    {
        static string smtpAddress = ConfigurationSettings.AppSettings["smtpAdress"].ToString();
        static int portNumber = Convert.ToInt32(ConfigurationSettings.AppSettings["toEmail"]);
        static bool enableSSL = Convert.ToBoolean(ConfigurationSettings.AppSettings["enableSsl"]);
        static string emailFromAddress = ConfigurationSettings.AppSettings["fromEmail"].ToString(); //Sender Email Address  
        static string password = ConfigurationSettings.AppSettings["emailPassword"].ToString(); //Sender Password  
        static string emailToAddress = ConfigurationSettings.AppSettings["toEmail"].ToString(); //Receiver Email Address  
        static string subject = "Hello";
        static string body = "Hello, This is Email sending test using gmail.";
        static void Main(string[] args)
        {
            SendEmail();
        }
        public static void SendEmail()
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(emailToAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
    }
}