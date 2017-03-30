using Hitbit.Models;
using Microsoft.EntityFrameworkCore;

namespace Hitbit.Db
{
    class HitbitContext : DbContext
    {
        public DbSet<Phrase> Phrase { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Hitbit.db3");
        }
    }
}
