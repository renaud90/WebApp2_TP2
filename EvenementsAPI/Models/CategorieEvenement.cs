using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.Models
{
    public class CategorieEvenement:BaseEntite
    {
        public int EvenementId { get; set; }
        public Evenement Evenement { get; set; }
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }
    }
}
