using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using NetCorePersonal.Core.DTOs.AccountDtos;
using NetCorePersonal.Core.Entities;
using NetCorePersonal.Core.Services;
using NetCorePersonal.UI.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NetCorePersonal.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<User> _serviceUser;
        private readonly IService<Contact> _serviceContact;
        private readonly IService<Category> _serviceCategory;
        private readonly IService<Experience> _serviceExperience;
        private readonly IService<Project> _serviceProject;
        private readonly IConfiguration _configuration;

        public HomeController(IService<User> serviceUser, IService<Contact> serviceContact, IService<Category> serviceCategory, IService<Experience> serviceExperience, IService<Project> serviceProject, IConfiguration configuration)
        {
            _serviceUser = serviceUser;
            _serviceContact = serviceContact;
            _serviceCategory = serviceCategory;
            _serviceExperience = serviceExperience;
            _serviceProject = serviceProject;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _serviceUser.GetAllAsync();
            var _user = user.First();
            ViewData["SocialProfileImage"] = _user.ProfileImage;
            ViewData["SocialFullName"] = _user.FullName;
            ViewData["About"] = _user.About;
            ViewData["Email"] = _user.Email;
            ViewData["WebSite"] = _user.WebSite;
            ViewData["Phone"] = _user.Phone;

            var contact = await _serviceContact.GetAllAsync();
            var _contact = contact.First();
            ViewData["SocialTwitter"] = _contact.Twitter;
            ViewData["SocialInstagram"] = _contact.Instagram;
            ViewData["SocialTelegram"] = _contact.Telegram;
            ViewData["SocialLinkedin"] = _contact.Linkedin;
            ViewData["ContactLocation"] = _contact.Location;
            ViewData["ContactEmail"] = _contact.Email;
            ViewData["ContactPhone"] = _contact.Phone;
            ViewData["ContactIFrame"] = _contact.IFrame;

            var experience = await _serviceExperience.GetAllAsync();
            ViewBag.Experience = experience;

            var projects = await _serviceProject.GetAllAsync();
            ViewBag.Projects = projects.OrderByDescending(x=>x.Id);

            var categories = await _serviceCategory.GetAllAsync();
            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(ContactViewModel _contactViewModel)
        {
            if (ModelState.IsValid)
            {
                // Email Values
                int _port = Convert.ToInt32(_configuration.GetValue<string>("EmailSettings:Port"));
                string _email = _configuration.GetValue<string>("EmailSettings:Email");
                string _password = _configuration.GetValue<string>("EmailSettings:Password");
                string _smtpClient = _configuration.GetValue<string>("EmailSettings:SmtpClient");

                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress(_contactViewModel.Name, _contactViewModel.Email);
                mimeMessage.From.Add(mailboxAddressFrom);
                MailboxAddress mailboxAddressTo = new MailboxAddress("Welat BARAN - ADMIN", _email);
                mimeMessage.To.Add(mailboxAddressTo);

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = _contactViewModel.Message;
                mimeMessage.Body = bodyBuilder.ToMessageBody();

                mimeMessage.Subject = _contactViewModel.Subject;

                SmtpClient client = new SmtpClient();
                client.Connect(_smtpClient, _port, false);
                client.Authenticate(_email, _password);
                client.Send(mimeMessage);
                client.Disconnect(true);
                return View(nameof(Index));
            }
            return View(_contactViewModel);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}