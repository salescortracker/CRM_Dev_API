using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layers.Repositories
{
    public interface IUnitOfWork
    {
        IGeneralRepository<T>
            Repository<T>()
            where T : class;

        Task<int> CompleteAsync();

        Task<IDbContextTransaction>
            BeginTransactionAsync();
    }
}
