using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Data
{
    internal class ApplicationDbContext
    {
        internal async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        internal DbSet<T> Set<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
