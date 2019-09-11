using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using EJ.Models.Configurations;
using EJ.Models.Enums;

namespace EJ.Domain.Services
{
    public interface IEMailService
    {
        Task<bool> SendAsync(string toEmailAddress, string subject, string body);
    }
    public class EMailService : IEMailService
    {
        private readonly NotificationConfiguration _notificationConfiguration;
        private const int Timeout = 10 * 1 * 1000;

        public EMailService(IConfiguration configuration)
        {
            _notificationConfiguration = configuration.GetSection("Notifications").Get<NotificationConfiguration>();
        }

        public async Task<bool> SendAsync(string toEmailAddress, string subject, string body)
        {
            var basicCredential = new NetworkCredential(_notificationConfiguration.Email.UserName, _notificationConfiguration.Email.Password);

            try
            {
                using (var message = new MailMessage())
                {
                    message.To.Add(toEmailAddress);
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;
                    message.From = new MailAddress(_notificationConfiguration.Email.From);

                    using (var smtpClient = new SmtpClient())
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = basicCredential;
                        smtpClient.Timeout = Timeout;
                        smtpClient.Host = _notificationConfiguration.Email.Host;
                        smtpClient.EnableSsl = true;
                        smtpClient.Port = 587;
                        await smtpClient.SendMailAsync(message);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
    }
}
