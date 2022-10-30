using NetCorePersonal.Core.UnitOfWorks;
using NetCorePersonal.Repository.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Commit()
        {
            _databaseContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}
