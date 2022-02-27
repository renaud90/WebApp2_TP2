using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.DTO
{
    public class ParticipationDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Courriel invalide")]
        public string Courriel { get; set; }
        [Required]
        public int NbPlaces { get; set; }
        public int EvenementId { get; set; }

    }
}
