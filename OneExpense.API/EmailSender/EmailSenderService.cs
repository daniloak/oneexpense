using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace OneExpense.API.EmailSender
{
    public class EmailSenderService : IEmailSender
    {
        public EmailSenderService(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await Execute(email, subject, message);
        }

        async Task Execute(string email, string subject, string message)
        {
            var apiKey = Options.SendGridKey;
            var client = new SendGridClient(apiKey, httpErrorAsException: true);
            var from = new EmailAddress("dacceto@gmail.com", "OneExpense");
            var to = new EmailAddress(email, email);
            var plainTextContent = message;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
