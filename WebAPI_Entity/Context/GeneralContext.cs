using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Entity
{
    public class GeneralContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }

        public GeneralContext(DbContextOptions<GeneralContext> options) :
            base(options)
        {
        }
    }
}
