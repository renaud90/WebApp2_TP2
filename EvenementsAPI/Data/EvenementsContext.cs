using EvenementsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.Data
{
    public class EvenementsContext : DbContext
    {
        public EvenementsContext (DbContextOptions<EvenementsContext> options) : base(options) { }

        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Ville> Villes { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Categorie> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evenement>().HasMany(_ => _.Participations).WithOne(e => e.Evenement).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Participation>().Property<bool>("IsValid");
            modelBuilder.Entity<Participation>().HasQueryFilter(p => EF.Property<bool>(p, "IsValid") == true);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            CheckForIsValidProp();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void CheckForIsValidProp()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var isValidProp = entry.Properties.FirstOrDefault(x => x.Metadata.Name == "IsValid");
                if (isValidProp != null)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            isValidProp.CurrentValue = false;
                            break;
                    }
                }
            }
        }
    }
}
