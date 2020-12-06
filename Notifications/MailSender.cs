using System.Net.Mail;
using Messaging;
using Messaging.Models;
using Microsoft.Extensions.Configuration;
using Notifications.Models;
using Services;

namespace Notifications
{
    public interface IMailSender
    {
        void SendRegistrationSuccessEmail(UserAddedOrUpdated user);
        void SendDeletedEmail(UserAddedOrUpdated user);
    }

    public class MailSender : IMailSender
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;


        private const string UserAddedTemplate = @" Hello #user_name#! <br/> Welcome to Super Todo App.";
    
        private const string UserDeletedTemplate = @" Hello #user_name#! <br/> We ara sorry you go.";
    
        public MailSender(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public void SendRegistrationSuccessEmail(UserAddedOrUpdated user)
        {
            MailConfiguration mailConfiguration = GetMailConfiguration();
            string sendTemplate = UserAddedTemplate.Replace("#user_name#", user.Name);
        
            var mail = new MailMessage {From = new MailAddress(mailConfiguration.From)};
            mail.To.Add(user.Email);
            mail.Subject = "Welcome to Super Todo App";
            mail.Body = sendTemplate;
            mail.IsBodyHtml = true;
            var smtpClient = GetSmtpClient();
            smtpClient.Send(mail);
        }
    
        public void SendDeletedEmail(UserAddedOrUpdated user)
        {
            MailConfiguration mailConfiguration = GetMailConfiguration();
            string sendTemplate = UserDeletedTemplate.Replace("#user_name#", user.Name);
        
            var mail = new MailMessage {From = new MailAddress(mailConfiguration.From)};
            mail.To.Add(user.Email);
            mail.Subject = "Welcome to Super Todo App";
            mail.Body = sendTemplate;
            mail.IsBodyHtml = true;
            var smtpClient = GetSmtpClient();
            smtpClient.Send(mail);
        }

        private SmtpClient GetSmtpClient()
        {
            MailConfiguration configSection = GetMailConfiguration();

            var smtpClient = new SmtpClient(configSection.Host, configSection.Port)
            {
                EnableSsl = false,
                UseDefaultCredentials = false,
                Host = configSection.Host,
                Credentials = new System.Net.NetworkCredential(configSection.User, configSection.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };

            return smtpClient;
        }

        private MailConfiguration GetMailConfiguration()
        {
            IConfigurationSection configurationSection = _appConfigurationProvider.Configuration.GetSection("Email");

            if (configurationSection == null)
            {
                throw new InvalidConfigurationException($"Required config-section 'Mail' not found.");
            }

            MailConfiguration configSection = configurationSection.Get<MailConfiguration>();
            return configSection;
        }
    }
}