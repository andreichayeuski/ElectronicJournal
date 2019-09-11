using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Mailing
{
    public class MailHelper
    {
        /// <summary>
        /// Отправляет по указанному адресу сообщение. emailReceiver - список разделённый ';'
        /// </summary>
        /// <returns></returns>
        public static bool SendEmailMessage(string emailReceiver, string messageTheme, string messageBody, out string error)
        {
            try
            {
                //TODO: move to core
                //var client = new SmtpClient(ConfigurationManager.AppSettings["host"], int.Parse(ConfigurationManager.AppSettings["port"]))
                //{
                //    Credentials = new MyCredentials(),
                //    EnableSsl = bool.Parse(ConfigurationManager.AppSettings["enableSSL"])
                //};

                //var message = new MailMessage
                //{
                //    Subject = messageTheme,
                //    BodyEncoding = Encoding.UTF8,
                //    From = new MailAddress(ConfigurationManager.AppSettings["userFrom"])
                //};
                //foreach (var mail in emailReceiver.Split(';'))
                //{
                //    message.To.Add(new MailAddress(mail));
                //}
               
                //message.Body = messageBody;
                //client.Send(message);
                error = "";
                return true;
            }
            catch (Exception ex)
            {
                error = string.Format("Error: {0} {1} {2}", ex.Message, ex.InnerException, ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Отправляет по указанному адресу сообщение cприкреплением. emailReceiver - список разделённый ';'
        /// </summary>
        /// <returns></returns>
        public static bool SendEmailMessage(string emailReceiver, string messageTheme, string messageBody, List<Attachment> attachments, out string error)
        {
            try
            {
                //var client = new SmtpClient(ConfigurationManager.AppSettings["host"], int.Parse(ConfigurationManager.AppSettings["port"]))
                //{
                //    Credentials = new MyCredentials(),
                //    EnableSsl = bool.Parse(ConfigurationManager.AppSettings["enableSSL"])
                //};

                //var message = new MailMessage
                //{
                //    Subject = messageTheme,
                //    BodyEncoding = Encoding.UTF8,
                //    From = new MailAddress(ConfigurationManager.AppSettings["userFrom"])
                //};
                //foreach (var mail in emailReceiver.Split(';'))
                //{
                //    message.To.Add(new MailAddress(mail));
                //} 
                //message.Body = messageBody;
                //foreach (var file in attachments)
                //{
                //    message.Attachments.Add(file);
                //}
                //client.Send(message);
                error = "";
                return true;
            }
            catch (Exception ex)
            {
                error = string.Format("Error: {0} {1} {2}", ex.Message, ex.InnerException, ex.StackTrace);
                return false;
            }
        }
    }

    public class MyCredentials : ICredentialsByHost
    {
        #region ICredentialsByHost Members

        public NetworkCredential GetCredential(string host, int port, string authenticationType)
        {
            NetworkCredential credential=null;
            //if (bool.Parse(ConfigurationManager.AppSettings["IsLocalSMTP"]))
            //{
            //    credential = new NetworkCredential(ConfigurationManager.AppSettings["userName"],
            //                                                         ConfigurationManager.AppSettings["userPassword"],
            //                                                         ConfigurationManager.AppSettings["domain"]);
            //}
            //else
            //{
            //    credential = new NetworkCredential(ConfigurationManager.AppSettings["userName"],
            //                                                         ConfigurationManager.AppSettings["userPassword"]);
            //}

            return credential;
        }

        #endregion
    }
}
