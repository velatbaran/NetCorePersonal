using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using NetCorePersonal.Core.DTOs.CategoryDtos;
using NetCorePersonal.Core.DTOs.ProjectDtos;
using NetCorePersonal.Core.Entities;
using NetCorePersonal.Core.Services;
using NetCorePersonal.Services.Exceptions;
using NetCorePersonal.Services.Services;

namespace NetCorePersonal.UI.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IService<Category> _categoryServis;
        private readonly IMapper _mapper;
        public ProjectController(IProjectService projectService, IService<Category> categoryServis, IMapper mapper)
        {
            _projectService = projectService;
            _categoryServis = categoryServis;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<ProjectsWithCategoryDto> projectDtos = await _projectService.GetProjectsWithCategory();
            return View(projectDtos);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryServis.GetAllAsync(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProjectDto _createProjectDto, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                //string fileName = $"p_{userid}.jpg";
                string fileName = $"p_{_createProjectDto.Name}.{Image.ContentType.Split('/')[1]}";   // image/png   image/jpg
                Stream stream = new FileStream($"wwwroot/uploads/Projects/{fileName}", FileMode.OpenOrCreate);
                Image.CopyTo(stream);
                stream.Close();
                stream.Dispose();

                _createProjectDto.Image = fileName;
                await _projectService.AddAsync(_mapper.Map<Project>(_createProjectDto));
                TempData["result"] = "Project created successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(await _categoryServis.GetAllAsync(), "Id", "Title");
            return View(_createProjectDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
            {
                throw new NotFoundException($"({id}) nolu project not found");
            }
            ViewBag.Categories = new SelectList(await _categoryServis.GetAllAsync(), "Id", "Title", project.CategoryId);
            return View(_mapper.Map<EditProjectDto>(project));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProjectDto _editProjectDto, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                string fileName = $"p_{_editProjectDto.Name}.{Image.ContentType.Split('/')[1]}";   // image/png   image/jpg
                Stream stream = new FileStream($"wwwroot/uploads/Projects/{fileName}", FileMode.OpenOrCreate);
                Image.CopyTo(stream);
                stream.Close();
                stream.Dispose();

                _editProjectDto.Image = fileName;
                Project project = _mapper.Map<Project>(_editProjectDto);
                await _projectService.UpdateAsync(project);
                TempData["result"] = "Project updated successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(await _categoryServis.GetAllAsync(), "Id", "Title", _editProjectDto.CategoryId);
            return View(_editProjectDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Project project = await _projectService.GetByIdAsync(id);
            if (project == null)
            {
                throw new NotFoundException($"({id}) nolu project not found");
            }
            await _projectService.RemoveAsync(project);
            TempData["result"] = "Project removed successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
