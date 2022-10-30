using AutoMapper;
using NetCorePersonal.Core.DTOs.CategoryDtos;
using NetCorePersonal.Core.DTOs.ContactDtos;
using NetCorePersonal.Core.DTOs.ExperienceDtos;
using NetCorePersonal.Core.DTOs.ProjectDtos;
using NetCorePersonal.Core.DTOs.UserDtos;
using NetCorePersonal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Services.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, EditCategoryDto>().ReverseMap();
            CreateMap<Project, ProjectsWithCategoryDto>().ReverseMap();
            CreateMap<Project, CreateProjectDto>().ReverseMap();
            CreateMap<Project, EditProjectDto>().ReverseMap();
            CreateMap<Experience, ExperienceDto>().ReverseMap();
            CreateMap<Experience, CreateExperienceDto>().ReverseMap();
            CreateMap<Experience, EditExperienceDto>().ReverseMap();
            CreateMap<Contact, EditContactDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
