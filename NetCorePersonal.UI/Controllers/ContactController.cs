using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCorePersonal.Core.DTOs.CategoryDtos;
using NetCorePersonal.Core.DTOs.ContactDtos;
using NetCorePersonal.Core.Entities;
using NetCorePersonal.Core.Services;
using NetCorePersonal.Services.Exceptions;
using NetCorePersonal.Services.Services;

namespace NetCorePersonal.UI.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IService<Contact> _service;
        private readonly IMapper _mapper;

        public ContactController(IService<Contact> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var contact = await _service.GetAllAsync();
            var _contact = contact.First();
            if (_contact == null)
            {
                throw new NotFoundException("Contact not found");
            }
            EditContactDto _editContactDto = _mapper.Map<EditContactDto>(_contact);
            return View(_editContactDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditContactDto _editContactDto)
        {
            if (ModelState.IsValid)
            {
                Contact contact = _mapper.Map<Contact>(_editContactDto);
                await _service.UpdateAsync(contact);
                TempData["result"] = "Contact updated successfully";
                return View(_editContactDto);
            }
            return View(_editContactDto);
        }
    }
}
