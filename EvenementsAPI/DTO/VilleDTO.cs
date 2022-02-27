using EvenementsAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.DTO
{
    public class VilleDTO 
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public Region Region { get; set; }
    }
}
