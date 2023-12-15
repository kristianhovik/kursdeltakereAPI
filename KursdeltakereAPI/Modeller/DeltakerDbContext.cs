using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using kursdeltakereAPI.Modeller;

namespace kursdeltakereAPI.Modeller
{
    public class DeltakerDbContext : DbContext
    {
        public DbSet<Kursdeltaker> Kursdeltakere { get; set; }

        public DeltakerDbContext(DbContextOptions<DeltakerDbContext> options) : base(options)
        {
        }
    }
}