using System.Net.Mail;
using System.Net;

namespace BeChinhPhucToan_BE.Services
{
    public class MailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _emailFrom;
        private readonly string _password;

        // Constructor để cấu hình SMTP
        public MailService(string smtpServer, int smtpPort, string emailFrom, string password)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _emailFrom = emailFrom;
            _password = password;
        }

        // Hàm gửi email
        public void SendEmail(string emailTo, string subject, string body)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.Credentials = new NetworkCredential(_emailFrom, _password);
                    client.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress(_emailFrom),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(emailTo);

                    client.Send(mailMessage);
                    Console.WriteLine("Email đã gửi thành công.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gửi email: {ex.Message}");
            }
        }
    }
}
