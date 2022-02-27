using EvenementsAPI.Data;
using EvenementsAPI.DTO;
using EvenementsAPI.Models;
using EvenementsAPI.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public class ParticipationsBL : IParticipationsBL
    {
        private readonly IEvenementRepository _repoEvenement;
        private readonly IRepository<Participation> _repoParticipations;
        public ParticipationsBL(IEvenementRepository repoEvenement, IRepository<Participation> repoParticipations)
        {
            _repoEvenement = repoEvenement;
            _repoParticipations = repoParticipations;
        }
        public ParticipationDTO Add(ParticipationDTO value)
        {
            var participation = new Participation
            {
                Courriel = value.Courriel,
                EvenementId = value.EvenementId,
                NbPlaces = value.NbPlaces,
                Nom = value.Nom,
                Prenom = value.Prenom
            };
            ValiderModeleDeParticipation(participation);

            _repoParticipations.Add(participation);

            return value;
        }

        public bool GetStatus(int id)
        {
            var participation = _repoParticipations.GetById(id);
            if (participation == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            if (participation.IsValid)
            {
                return true;
            }
            verifyParticipation(participation);
            return false;
        }

        public IEnumerable<ParticipationDTO> GetList()
        {
            return _repoParticipations.GetAll().Where(_ => _.IsValid).Select(_ => new ParticipationDTO {
                Courriel = _.Courriel,
                EvenementId = _.EvenementId,
                NbPlaces = _.NbPlaces,
                Nom = _.Nom,
                Prenom = _.Prenom
            });
        }

        public ParticipationDTO Get(int id)
        {
            var participation = _repoParticipations.GetById(id);
            if (participation == null || !participation.IsValid)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            return new ParticipationDTO {
                Id = participation.Id,
                Courriel = participation.Courriel,
                EvenementId = participation.EvenementId,
                NbPlaces = participation.NbPlaces,
                Nom = participation.Nom,
                Prenom = participation.Prenom
            };

        }

        
        public void Delete(int id)
        {
            var participation = _repoParticipations.GetById(id);
            if (participation == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            _repoParticipations.Delete(id);
        }

        private void ValiderModeleDeParticipation(Participation value)
        {
            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            if (String.IsNullOrEmpty(value.Nom) ||
                String.IsNullOrEmpty(value.Prenom) ||
                String.IsNullOrEmpty(value.Courriel) ||
                value.EvenementId < 1 || value.NbPlaces < 1)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
           
            if (_repoEvenement.GetAll().FirstOrDefault(e => e.Id == value.EvenementId) == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Parametres d'entrée non valides: événement avec Id {value.EvenementId} inexistant" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            if (_repoParticipations.GetAll().FirstOrDefault(p => p.Courriel == value.Courriel && p.EvenementId == value.EvenementId) != null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Parametres d'entrée non valides: une participation enregistrée à l'adresse courriel {value.Courriel} pour l'événement avec Id {value.EvenementId} existe déjà" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        private void verifyParticipation(Participation participation)
        {
            var isValid = new Random().Next(1, 10) >= 1 ? true : false;//Simuler la validation externe;
            participation.IsValid = isValid;
            _repoParticipations.Update(participation);
        }
    }
}
