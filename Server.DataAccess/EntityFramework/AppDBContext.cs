using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Lib.Entities;

namespace Server.DataAccess.EntityFramework
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }


        public AppDBContext() { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<AppRefreshToken> AppRefreshTokens { get; set; }
    }
}



