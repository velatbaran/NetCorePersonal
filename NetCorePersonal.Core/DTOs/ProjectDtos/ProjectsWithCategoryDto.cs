using NetCorePersonal.Core.DTOs.CategoryDtos;

namespace NetCorePersonal.Core.DTOs.ProjectDtos
{
    public class ProjectsWithCategoryDto : ProjectDto
    {
        public CategoryDto Category { get; set; }
    }
}
