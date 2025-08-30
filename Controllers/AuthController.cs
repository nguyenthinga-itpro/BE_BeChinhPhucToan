using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using BeChinhPhucToan_BE.Models;
using Microsoft.EntityFrameworkCore;
using BeChinhPhucToan_BE.Data;
using System.Security.Cryptography.Xml;
using BeChinhPhucToan_BE.Services;
using Twilio.Types;
using System.Reflection;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("Authentification")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // DTOs cho các yêu cầu
        public class LoginInfo
        {
            public string Email { get; set; }
            public string OTP { get; set; }
        }

        public class EmailInfo
        {
            public string Email { get; set; }
        }

        private void sendEmail(string email, string otp)
        {
            // Cấu hình thông tin email
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string emailFrom = "bechinhphuctoan@gmail.com";
            string password = "eidi pohi nvxf zndi";
            string emailTo = email;

            // Tạo instance của MailService
            MailService mailService = new MailService(smtpServer, smtpPort, emailFrom, password);
            // Gửi email
            string subject = "Xác nhận truy cập ứng dụng Bé Chinh Phục Toán";
            string body = $@"
                            <html>
                                <body>
                                    <p>Mã OTP của bạn là: <strong style='font-size: 24px'>{otp}</strong>.</p>
                                    <p>Vui lòng không chia sẻ mã này với bất kỳ ai.</p>
                                </body>
                            </html>";

            mailService.SendEmail(emailTo, subject, body);
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user is null)
                return NotFound(new { message = "Email chưa được đăng ký!" });

            // Tạo OTP
            var otp = new Random().Next(100000, 999999).ToString(); 
            
            user.OtpCode = otp;
            user.OtpExpiration = DateTime.UtcNow.AddMinutes(5);
            await _context.SaveChangesAsync();
            sendEmail(user.Email, otp);
            return Ok(new { message = "OTP đã được gửi thành công!", user = user });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginInfo request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (user is null)
                return NotFound(new { message = "Email chưa được đăng ký!" });

            // Kiểm tra OTP
            if (user.OtpCode != request.OTP || user.OtpExpiration < DateTime.UtcNow)
            {
                return BadRequest(new { message = "OTP không hợp lệ hoặc đã hết hạn." });
            }

            // Xóa OTP sau khi đăng nhập
            user.OtpCode = null;
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            return Ok(new
            {
                tokenType = "Bearer",
                accessToken = token,
                expiresIn = "3 days", // Token expiration in seconds
                refreshToken = refreshToken
            });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(72),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
