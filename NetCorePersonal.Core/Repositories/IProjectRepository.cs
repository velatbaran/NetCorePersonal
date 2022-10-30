﻿using NetCorePersonal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Core.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<List<Project>> GetProjectsWithCategory();
    }
}
