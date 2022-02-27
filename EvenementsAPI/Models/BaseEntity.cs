using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.Models
{
    public abstract class BaseEntite
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
