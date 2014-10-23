using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using log4net;

namespace WYS.Helpers
{
    public static class EmailManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(EmailManager));



        public static void SendEmail(String addressRecepient, String subject, String body)
        {
            var username = ConfigurationManager.AppSettings.Get("SMTP_USER_NAME");
            var password = ConfigurationManager.AppSettings.Get("SMTP_USER_PASSWORD");
            var host = ConfigurationManager.AppSettings.Get("SMTP_HOST");
            var port = ConfigurationManager.AppSettings.Get("SMTP_PORT");

            try
            {
                var client = new SmtpClient(host, Convert.ToInt32(port))
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(username, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network

                };

                var mailMessage = new MailMessage();
                mailMessage.To.Add(addressRecepient);
                mailMessage.From = new MailAddress(username);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
               // client.SendCompleted += Client_SendCompleted;
                client.Send(mailMessage);
            }
            catch (Exception exception)
            {
                Logger.Error("API LAYER: ERROR IN CLASS: EmailManager, METHOD: SendEmail, Exception Message =>> " + exception.Message);
                throw;
            }


        }

/*

        private static void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }
*/




    }
}