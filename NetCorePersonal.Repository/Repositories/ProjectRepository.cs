using Microsoft.EntityFrameworkCore;
using NetCorePersonal.Core.Entities;
using NetCorePersonal.Core.Repositories;
using NetCorePersonal.Repository.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Repository.Repositories
{
    public class ProjectRepository : GenericRepository<Project>,IProjectRepository
    {
        public ProjectRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<List<Project>> GetProjectsWithCategory()
        {
            return await _databaseContext.Projects.Include(x => x.Category).ToListAsync();
        }
    }
}
