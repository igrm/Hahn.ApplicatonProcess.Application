using Hahn.ApplicatonProcess.February2021.Data.Infrastructure;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private AssetContext _dbContext;

        public UnitOfWork(AssetContext context)
        {
            _dbContext = context;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
