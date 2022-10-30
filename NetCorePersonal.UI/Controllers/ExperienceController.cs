using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCorePersonal.Core.DTOs.CategoryDtos;
using NetCorePersonal.Core.DTOs.ExperienceDtos;
using NetCorePersonal.Core.Entities;
using NetCorePersonal.Core.Services;
using NetCorePersonal.Services.Exceptions;

namespace NetCorePersonal.UI.Controllers
{
    [Authorize]
    public class ExperienceController : Controller
    {
        private readonly IService<Experience> _service;
        private readonly IMapper _mapper;

        public ExperienceController(IService<Experience> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ExperienceDto>>(await _service.GetAllAsync()));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateExperienceDto _createExperienceDto)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(_mapper.Map<Experience>(_createExperienceDto));
                TempData["result"] = "Experience created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(_createExperienceDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var experience = await _service.GetByIdAsync(id);
            if (experience == null)
            {
                throw new NotFoundException($"({id}) nolu experience not found");
            }
            return View(_mapper.Map<EditExperienceDto>(experience));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditExperienceDto _editExperienceDto)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(_mapper.Map<Experience>(_editExperienceDto));
                TempData["result"] = "Experience updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(_editExperienceDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Experience experience = await _service.GetByIdAsync(id);
            if (experience == null)
            {
                throw new NotFoundException($"({id}) nolu experience not found");
            }
            await _service.RemoveAsync(experience);
            TempData["result"] = "Experience removed successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
