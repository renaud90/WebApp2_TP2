using EvenementsAPI.Data;
using EvenementsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web2Data.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EvenementsContext context)
        {
            // Look for any data
            if (context.Evenements.Any() || context.Villes.Any() || context.Categories.Any() || context.Participations.Any())
            {
                return;   // DB has been seeded
            }


            var categories = new Categorie[]
            {
                new Categorie{ Nom="Concert"},
                new Categorie{ Nom="Conférence"},
                new Categorie{ Nom="Match sportif"}
            };

            context.Categories.AddRange(categories);

            var villes = new Ville[]
            {
                new Ville{ Nom="Sherbrooke", Region=Region.ESTRIE},
                new Ville{ Nom="Gatineau", Region=Region.OUTAOUAI},
            };

            context.Villes.AddRange(villes);
            //context.SaveChanges();

            context.SaveChanges();
        }
    }
}
