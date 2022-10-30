using AutoMapper;
using NetCorePersonal.Core.DTOs.ProjectDtos;
using NetCorePersonal.Core.Entities;
using NetCorePersonal.Core.Repositories;
using NetCorePersonal.Core.Services;
using NetCorePersonal.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Services.Services
{
    public class ProjectServis : Service<Project>, IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public ProjectServis(IGenericRepository<Project> repository, IUnitOfWork unitOfWork, IProjectRepository projectRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<List<ProjectsWithCategoryDto>> GetProjectsWithCategory()
        {
            var projects = await _projectRepository.GetProjectsWithCategory();
            List<ProjectsWithCategoryDto> projectDtos = _mapper.Map<List<ProjectsWithCategoryDto>>(projects.ToList());
            return projectDtos;
        }
    }
}
