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
            _configuration = configuration;
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

            var confirmationUrl = $"{_configuration["App:BaseUrl"]}/api/Booking/confirm/{confirmationToken}";

            //path to html template for confirmation mail body
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HTMLService", "htmlpage.html");
            var htmltemplate = await File.ReadAllTextAsync(confirmationUrl);

            var htmlBody = htmltemplate.Replace("{{CONFIRMATION_URL}}", confirmationUrl);

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody, TextBody = $"Vänligen bekräfta din bokning genom att besöka {confirmationUrl}" };

            MailMessage.Body = bodyBuilder.ToMessageBody();


            var smtpClient = new MailKit.Net.Smtp.SmtpClient();

            await smtpClient.ConnectAsync(templatePath);


        }
    }
}
