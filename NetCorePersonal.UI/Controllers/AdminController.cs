using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using NetCorePersonal.Core.DTOs.ProjectDtos;
using NetCorePersonal.Core.DTOs.UserDtos;
using NetCorePersonal.Core.Entities;
using NetCorePersonal.Core.Services;
using NetCorePersonal.Services.Exceptions;
using NetCorePersonal.UI.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace NetCorePersonal.UI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IService<User> _service;
        private readonly IMapper _mapper;
        private readonly IHasher _hasher;

        public AdminController(IService<User> service, IMapper mapper, IConfiguration configuration, IHasher hasher)
        {
            _service = service;
            _mapper = mapper;
            _hasher = hasher;
        }

        public IActionResult Index()
        {
            return View();
        }

        private void ProfileInfoLoader()
        {
            int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _service.Where(x => x.Id == id).FirstOrDefault();

            ViewData["FullName"] = user.FullName;
            ViewData["Username"] = user.Username;
            ViewData["Email"] = user.Email;
            ViewData["Phone"] = user.Phone;
            ViewData["WebSite"] = user.WebSite;
            ViewData["About"] = user.About;
            ViewData["ProfileImage"] = user.ProfileImage;
        }

        public IActionResult ShowProfile()
        {
            ProfileInfoLoader();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfileImage([Required]IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string fileName = $"p_{file.Name}.{file.ContentType.Split('/')[1]}";   // image/png   image/jpg
                Stream stream = new FileStream($"wwwroot/uploads/User/{fileName}", FileMode.OpenOrCreate);
                file.CopyTo(stream);
                stream.Close();
                stream.Dispose();

                int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _service.Where(x => x.Id == id).FirstOrDefault();
                user.ProfileImage = fileName;
                await _service.UpdateAsync(user);
                ViewData["result"] = "Profile image updated successfully";
            }
            ProfileInfoLoader();
            return View(nameof(ShowProfile));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeFullName([Required][StringLength(50)] string fullname)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _service.Where(x => x.Id == id).FirstOrDefault();

                user.FullName = fullname;
                await _service.UpdateAsync(user);
                ViewData["result"] = "FullName updated successfully";
            }
            ProfileInfoLoader();
            return View(nameof(ShowProfile));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUsername([Required][StringLength(50)] string username)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _service.Where(x => x.Id == id).FirstOrDefault();

                user.Username = username;
                await _service.UpdateAsync(user);
                ViewData["result"] = "Username updated successfully";
            }
            ProfileInfoLoader();
            return View(nameof(ShowProfile));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmail([Required][StringLength(100)] string email)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _service.Where(x => x.Id == id).FirstOrDefault();

                user.Email = email;
                await _service.UpdateAsync(user);
                ViewData["result"] = "Email updated successfully";
            }
            ProfileInfoLoader();
            return View(nameof(ShowProfile));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePhone([Required] string phone)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _service.Where(x => x.Id == id).FirstOrDefault();

                user.Phone = phone;
                await _service.UpdateAsync(user);
                ViewData["result"] = "Phone updated successfully";
            }
            ProfileInfoLoader();
            return View(nameof(ShowProfile));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeWebSite([Required][StringLength(100)] string website)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _service.Where(x => x.Id == id).FirstOrDefault();

                user.WebSite = website;
                await _service.UpdateAsync(user);
                ViewData["result"] = "WebSite updated successfully";
            }
            ProfileInfoLoader();
            return View(nameof(ShowProfile));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAbout([Required] string about)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _service.Where(x => x.Id == id).FirstOrDefault();

                user.About = about;
                await _service.UpdateAsync(user);
                ViewData["result"] = "About updated successfully";
            }
            ProfileInfoLoader();
            return View(nameof(ShowProfile));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([Required][StringLength(100)] string password)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _service.Where(x => x.Id == id).FirstOrDefault();

                user.Password = _hasher.DoMD5Hashed(password);
                user.RePassword = _hasher.DoMD5Hashed(password);
                await _service.UpdateAsync(user);
                ViewData["result"] = "Password updated successfully";
            }
            ProfileInfoLoader();
            return View(nameof(ShowProfile));
        }

    }
}
