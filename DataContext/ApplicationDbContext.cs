using Microsoft.EntityFrameworkCore;
using SuperMegaProjekt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SuperMegaProjekt.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<SuperMegaProjekt.Model.Student> Student { get; set; }

        public DbSet<SuperMegaProjekt.Model.Group> Group { get; set; }
    }
}
