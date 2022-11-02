using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using NetCorePersonal.Core.DTOs.AccountDtos;
using NetCorePersonal.Core.Entities;
using NetCorePersonal.Core.Services;
using NetCorePersonal.UI.Helpers;
using NetCorePersonal.UI.Models;
using System.Security.Claims;

namespace NetCorePersonal.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IService<User> _service;
        private readonly IHasher _hasher;
        private readonly IConfiguration _configuration;

        public AccountController(IService<User> service, IHasher hasher, IConfiguration configuration)
        {
            _service = service;
            _hasher = hasher;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto _loginDto)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = _hasher.DoMD5Hashed(_loginDto.Password);
                User user = _service.Where(x => x.Username == _loginDto.Username && x.Password == hashedPassword).FirstOrDefault();

                if (user == null)
                {
                    ModelState.AddModelError("", "Usernama or password incorrect");
                    return View(_loginDto);
                }

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.FullName ?? string.Empty));
                claims.Add(new Claim(ClaimTypes.Actor, user.ProfileImage));
                claims.Add(new Claim("Username", user.Username));

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                TempData["result"] = "Login is success";
                //ViewBag.ProfileImage = user.ProfileImageFileName;

                return RedirectToAction("Index", "Admin");
            }
            return View(_loginDto);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["result"] = "Logout is success";
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto _forgotPasswordDto)
        {
            if (ModelState.IsValid)
            {
                User user = _service.Where(x=>x.Email == _forgotPasswordDto.Email).FirstOrDefault();
                if (user == null)
                {
                    TempData["result"] = "User not found";
                    return View(_forgotPasswordDto);
                }

                // Email Values
                int _port = Convert.ToInt32(_configuration.GetValue<string>("EmailSettings:Port"));
                string _email = _configuration.GetValue<string>("EmailSettings:Email");
                string _password = _configuration.GetValue<string>("EmailSettings:Password");
                string _smtpClient = _configuration.GetValue<string>("EmailSettings:SmtpClient");

                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress("Welat BARAN - ADMIN", _email);
                mimeMessage.From.Add(mailboxAddressFrom);
                MailboxAddress mailboxAddressTo = new MailboxAddress("User", _forgotPasswordDto.Email);
                mimeMessage.To.Add(mailboxAddressTo);

                Random random = new Random();
                string newPass = random.Next(100000, 999999).ToString();
                var hashedNewPass = _hasher.DoMD5Hashed(newPass);
                user.Password = hashedNewPass;
               await  _service.UpdateAsync(user);

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = $"New Password : {newPass}";
                mimeMessage.Body = bodyBuilder.ToMessageBody();

                mimeMessage.Subject = "Welat BARAN Website - New Password";

                SmtpClient client = new SmtpClient();
                client.Connect(_smtpClient, _port, false);
                client.Authenticate(_email, _password);
                client.Send(mimeMessage);
                client.Disconnect(true);
                TempData["result"] = "New password sent successfully";
                return RedirectToAction(nameof(Login));
            }
            return View();
        }
    }
}
