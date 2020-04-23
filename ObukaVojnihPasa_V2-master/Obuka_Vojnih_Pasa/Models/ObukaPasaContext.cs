
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;


namespace Obuka_Vojnih_Pasa.Models
{
    public class ObukaPasaContext : IdentityDbContext<ApplicationUser>
    {

        public ObukaPasaContext(DbContextOptions<ObukaPasaContext> options) : base(options)
        {
            
        }

   
        public DbSet<Obuka> Obuke { get; set; }
        public DbSet<Instruktor> Instruktori { get; set; }
        public DbSet<Pas> Psi { get; set; }
        public DbSet<Angazovanje> Angazovanja { get; set; }
        public DbSet<Zadatak> Zadaci { get; set; }
        public DbSet<Admin> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Obuka
            modelBuilder.Entity<Obuka>().ToTable("Obuka");
            modelBuilder.Entity<Obuka>().HasKey(o => o.Id);
            modelBuilder.Entity<Obuka>().Property(o => o.Id).IsRequired();
            //Instruktor

            modelBuilder.Entity<Instruktor>().HasOne(e => e.Obuka).WithMany(c => c.Instruktori).HasForeignKey(c => c.ObukaId);
            //Pas
            modelBuilder.Entity<Pas>().ToTable("Pas");
            modelBuilder.Entity<Pas>().HasKey(p => p.Id);
            modelBuilder.Entity<Pas>().Property(p => p.BrojZdravstveneKnjizice).IsRequired();
            modelBuilder.Entity<Pas>().HasOne(p => p.Obuka).WithMany(o => o.Psi).HasForeignKey(p=>p.ObukaId);
           

            //Angazovanje

            modelBuilder.Entity<Angazovanje>().ToTable("Angazovanje");
            modelBuilder.Entity<Angazovanje>().HasKey(a => new { a.PasId, a.ZadatakId });
            modelBuilder.Entity<Angazovanje>().HasOne(a => a.Pas).WithMany(p => p.Angazovanja).HasForeignKey(a => a.PasId);
            modelBuilder.Entity<Angazovanje>().HasOne(a => a.Zadatak).WithMany(z => z.Angazovanja).HasForeignKey(a => a.ZadatakId);

            //Zadatak
            modelBuilder.Entity<Zadatak>().ToTable("Zadatak");
            modelBuilder.Entity<Zadatak>().HasKey(z => z.Id);


         

         
        }

       
    }
}
