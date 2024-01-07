using SkillslabAssigment.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Notification
{
    public class EmailNotificationHandler : INotificationHandler
    {
        private readonly IAccountRepository _accountRepository;
        public EmailNotificationHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task HandleNotificationAsync(string subject, string body, short userID)
        {
            string recipientEmail = await _accountRepository.GetEmailByUserIdAsync(userID);
            string senderEmail = "JForce.Admin@ceridian.com";
            var smtpClent = new SmtpClient("relay.ceridian.com")
            {
                Port = 25,
                EnableSsl = true,
                UseDefaultCredentials = true,
            };
            var mailMessage = new MailMessage(senderEmail, "edooj511@gmail.com")
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() => smtpClent.Send(mailMessage)).ConfigureAwait(false);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    }
}
