using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCorePersonal.Core.DTOs.CategoryDtos;
using NetCorePersonal.Core.Entities;
using NetCorePersonal.Core.Services;
using NetCorePersonal.Services.Exceptions;
using System.Collections.Generic;
using System.Net;

namespace NetCorePersonal.UI.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IService<Category> _service;
        private readonly IMapper _mapper;

        public CategoryController(IService<Category> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
              
        public async Task<IActionResult> Index()
        {
            //var categories = await _service.GetAllAsync();
            //return View(_mapper.Map<List<CategoryDto>>(categories.ToList()));
            return View(await _service.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryDto _createCategoryDto)
        {
            if (ModelState.IsValid)
            {
                Category category = _mapper.Map<Category>(_createCategoryDto);
                await _service.AddAsync(category);
                TempData["result"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(_createCategoryDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException($"({id}) nolu category not found");
            }
            return View(_mapper.Map<EditCategoryDto>(category));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCategoryDto _editCategoryDto)
        {
            if (ModelState.IsValid)
            {
                Category category = _mapper.Map<Category>(_editCategoryDto);
                await _service.UpdateAsync(category);
                TempData["result"] = "Category updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(_editCategoryDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _service.GetByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException($"({id}) nolu category not found");
            }
            await _service.RemoveAsync(category);
            TempData["result"] = "Category removed successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
