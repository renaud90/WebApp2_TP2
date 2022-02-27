using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.DTO
{
    public class EvenementDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateDebut { get; set; }
        [Required]
        public DateTime DateFin { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public string Description { get; set; }
        [MinLength(1)]
        public IEnumerable<int> CategoriesId { get; set; }
        [Required]
        public string Adresse { get; set; }
        [Required]
        public string NomOrganisateur { get; set; }
        [Required]
        public int VilleId { get; set; }
        public double Prix { get; set;}
    }
}
