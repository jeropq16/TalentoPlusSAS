using System.Net;
using System.Net.Mail;

namespace TalentoPlus.Web.Services;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var host = _config["Smtp:Host"];
        var port = int.Parse(_config["Smtp:Port"]);
        var user = _config["Smtp:User"];
        var pass = _config["Smtp:Password"];
        var ssl = bool.Parse(_config["Smtp:EnableSsl"]);

        var smtp = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(user, pass),
            EnableSsl = ssl
        };

        var message = new MailMessage(user!, to, subject, body)
        {
            IsBodyHtml = true
        };

        await smtp.SendMailAsync(message);
    }
}