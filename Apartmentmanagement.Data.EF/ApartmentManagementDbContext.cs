using ApartmentManagement.Data.Entities;
using ApartmentManagement.Data.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace ApartmentManagement.Data.EF
{
    public class ApartmentManagementDbContext :IdentityDbContext<Account, Function, Guid>
    {
        public ApartmentManagementDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Account> Accounts { set; get; }
        public DbSet<Permission> Permissions { set; get; }
        public DbSet<Function> Functions { set; get; }


      
        public DbSet<Banner> Banners { set; get; }

        public DbSet<Area> Areas { set; get; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Advertisement> advertisements { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsGroup> NewsGroups { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<TypePosts> TypePosts { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable(name: "Accounts");
            });

            modelBuilder.Entity<Function>(entity =>
            {
                entity.ToTable(name: "Functions");
            });

            modelBuilder.Entity<Permission>()
                .HasKey(c => new { c.FunctionId, c.AccountId });

            modelBuilder.Entity<PostCategory>()
            .HasKey(c => new { c.PostsId, c.CatagoryId });


        }

        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity as IDateTracking;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.DateCreated = DateTime.Now;
                    }
                    changedOrAddedItem.DateModified = DateTime.Now;
                }
            }
            return base.SaveChanges();

        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApartmentManagementDbContext>
        {

            public ApartmentManagementDbContext CreateDbContext(string[] args)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<ApartmentManagementDbContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);
                return new ApartmentManagementDbContext(builder.Options);
            }
        }
    }
}
