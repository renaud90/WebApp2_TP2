using EvenementsAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public interface IParticipationsBL
    {
        public IEnumerable<ParticipationDTO> GetList();
        public ParticipationDTO Get(int id);
        public bool GetStatus(int id);

        public ParticipationDTO Add(ParticipationDTO value);

        public void Delete(int id);
    }
}
