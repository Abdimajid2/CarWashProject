using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;
namespace Backend.API.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            configuration = _configuration;
        }

        public async Task SendConfirmationEmailAsync(string toEmail, string confirmationToken)
        {
            var MailMessage = new MimeMessage();

            //creating where the email is coming from
            MailMessage.From.Add(new MailboxAddress
                (_configuration["Eskilstuna biltvätt bokning"],
                _configuration["Noreplay@Eskilstunabiltvatt.se"]));


            MailMessage.To.Add(new MailboxAddress("", toEmail));

            //email subject
            MailMessage.Subject = "Bokningsbekräftelse - Eskilstuna billtvätt";




        }
    }
}
