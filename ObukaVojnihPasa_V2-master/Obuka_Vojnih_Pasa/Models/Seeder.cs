using Microsoft.AspNetCore.Identity;
using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models

{
    public class Seeder
    {
        private readonly ObukaPasaContext context;

        public Seeder(ObukaPasaContext _context)
        {
            this.context = _context;
        }
        public void Seed()
        {
            if (context.Database.CanConnect())
            {
                if (!context.Roles.Any())
                {
                    seedRolesTable(context);
                }

                if (!context.Users.Any())
                {
                    seedUsers();
                }
                if (!context.UserRoles.Any())
                {
                    seedUserRolesTable(context);
                }
                if (!context.Obuke.Any())
                {
                    seedObukeTable(context);
                }
            }
        }

        private void seedObukeTable(ObukaPasaContext context)
        {
            
            //dodaj obuke

            context.SaveChanges();
        }

        private void seedUserRolesTable(ObukaPasaContext context)
        {
            context.UserRoles.Add(
               new IdentityUserRole<string>() { UserId = "1", RoleId = "1" }
             
           );

            context.SaveChanges();
        }

        private void seedRolesTable(ObukaPasaContext context)
        {
            context.Roles.AddRange(
              new IdentityRole() { Id = "1", Name = "Admin" },
              new IdentityRole() { Id = "2", Name = "Korisnik" }
              );

            context.SaveChanges();
        }

        private void seedUsers()
        {
            Admin admin = new Admin() { Id = "1", AccessFailedCount = 0, ConcurrencyStamp = "b58d0834-4428-4b8b-8585-54f91a26ba71", Email = "admin@glogy.com", EmailConfirmed = true, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = "admin@glogy.com", NormalizedUserName = "admin", PasswordHash = "AQAAAAEAACcQAAAAEHiB1olFqn60xI9ojOF+8WvHgi8kuCKYRt2e5LESVrtfpEn3IhVq+HYJ7BWZDXaE9w==", PhoneNumber = "", PhoneNumberConfirmed = false, SecurityStamp = "ac1d41a1-6a1b-43d5-83d0-1eb64223e354", TwoFactorEnabled = false, UserName = "admin" };

            context.Add(admin);
            context.SaveChanges();
        }
    }
}
