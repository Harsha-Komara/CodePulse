
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options):IdentityDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "18c3cea2-d75f-46f9-b26d-989e4d3df5a4";
            var writerRoleId = "163a7855-8637-41db-aa98-70679ce06a06";

            var roles = new List<IdentityRole>()
            {
                new()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER",
                    ConcurrencyStamp = readerRoleId
                },
                new()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER",
                    ConcurrencyStamp = writerRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);




            var adminUserId = "15993a48-7cc6-4841-9cf6-6a16b3ede740";

            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "Admin123",
                NormalizedUserName = "ADMIN123",
                Email = "admin123@gmail.com",
                NormalizedEmail = "admin123@gmail.com".ToUpper(),
                ConcurrencyStamp = adminUserId,
                SecurityStamp = adminUserId,
                PasswordHash =
                "AQAAAAIAAYagAAAAEJ9JJEWvgYF08xykM1IHTvM17mSA + ydH3EpiFqReZUuH6onRvbsVcoi9wudaWJYWJQ =="
            };

            builder.Entity<IdentityUser>().HasData(admin);



            var adminUserRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminUserRoles);
        }
    }
}
