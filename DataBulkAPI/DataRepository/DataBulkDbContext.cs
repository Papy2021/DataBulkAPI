using DataBulkAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataBulkAPI.DataRepository
{
    public class DataBulkDbContext:DbContext
    {
        public DataBulkDbContext(DbContextOptions<DataBulkDbContext> options):base(options)
        {   
        }

        public DbSet<ActorModel> Actors { get; set; }
        public DbSet<UserModel> Users { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
         
        }

    }
}
