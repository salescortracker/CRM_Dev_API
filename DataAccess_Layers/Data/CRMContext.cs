using DataAccess_Layers.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layers.Data
{
    public class CRMContext : DbContext
    {
        public CRMContext(
            DbContextOptions<CRMContext> options)
            : base(options)
        {
        }
        public DbSet<UserLogin> UserLogin { get; set; }

    }
}
