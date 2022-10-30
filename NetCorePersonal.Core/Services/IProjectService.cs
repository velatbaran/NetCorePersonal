using NetCorePersonal.Core.DTOs.ProjectDtos;
using NetCorePersonal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Core.Services
{
    public interface IProjectService : IService<Project>
    {
        Task<List<ProjectsWithCategoryDto>> GetProjectsWithCategory();
    }
}
