using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Class07b.Models;

namespace Class07b.Data
{
    public class Class07bContext : DbContext
    {
        public Class07bContext (DbContextOptions<Class07bContext> options)
            : base(options)
        {
        }

        public DbSet<Class07b.Models.User> User { get; set; } = default!;
    }
}
