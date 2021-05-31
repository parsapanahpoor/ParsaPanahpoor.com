using Microsoft.EntityFrameworkCore;
using Models.Entities.Permissions;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContext
{
    public  class ParsaPanahpoorDBContext : DbContext
    {

        //public ParsaPanahpoorDBContext(DbContextOptions<ParsaPanahpoorDBContext> options) 
        //    : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-8KN3PBT\\PARSA;Initial Catalog=ParsaPanahpoor_DB;Integrated Security=True;MultipleActiveResultSets=true");

        }


        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UsersRoles { get; set; }


        #endregion

        #region Permission

        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        #endregion





        #region ModelBuilder & Fluent API

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;



            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>()
            .HasQueryFilter(r => !r.IsDelete);

  


            base.OnModelCreating(modelBuilder);
        }


        #endregion
    }
}
