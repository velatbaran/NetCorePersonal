using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NetCorePersonal.Core.DTOs;
using NetCorePersonal.Core.Entities;
using NetCorePersonal.Core.Services;

namespace NetCorePersonal.Web.Controllers
{
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
            return View( _mapper.Map<IEnumerable<CategoryDto>>(await _service.GetAllAsync()));
        }
    }
}
