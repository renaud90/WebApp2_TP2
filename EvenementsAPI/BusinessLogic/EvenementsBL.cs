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
    public class EvenementsBL : IEvenementsBL
    {
        private readonly IEvenementRepository _repoEvenement;
        private readonly IRepository<Participation> _repoParticipations;
        private readonly IVilleRepository _repoVilles;
        private readonly IRepository<Categorie> _repoCategories;
        private readonly IRepository<CategorieEvenement> _repoEvCat;
        public EvenementsBL(IRepository<CategorieEvenement> repoEvCat, IEvenementRepository repoEvenement, IRepository<Participation> repoParticipations, IVilleRepository repoVilles, IRepository<Categorie> repoCat)
        {
            _repoEvenement = repoEvenement;
            _repoParticipations = repoParticipations;
            _repoVilles = repoVilles;
            _repoCategories = repoCat;
            _repoEvCat = repoEvCat;
        }
        public IEnumerable<EvenementDTO> GetList(int pageIndex, int pageCount, string filter) 
        {
            var evenements = _repoEvenement.GetAll();
            if (!String.IsNullOrEmpty(filter))
            {
                evenements = evenements.Where(_ => _.Titre.ToLower().Contains(filter.ToLower()) || _.Description.ToLower().Contains(filter.ToLower()));
            }
            evenements = evenements.OrderBy(_ => _.DateDebut);
            return evenements.Select(e => new EvenementDTO
            {
                Id = e.Id,
                NomOrganisateur = e.NomOrganisateur,
                CategoriesId = _repoEvCat.GetAll().Where(_ => _.EvenementId == e.Id).Select(_ => _.CategorieId),
                Adresse = e.Adresse,
                DateDebut = e.DateDebut,
                DateFin = e.DateFin,
                Description = e.Description,
                Prix = e.Prix,
                Titre = e.Titre,
                VilleId = e.VilleId
            }).Skip((pageIndex - 1) * pageCount).Take(pageCount);
        }
        public EvenementDTO Get(int id) 
        {
            var e = _repoEvenement.GetById(id);
            return new EvenementDTO
            {
                Id = e.Id,
                NomOrganisateur = e.NomOrganisateur,
                CategoriesId = _repoEvCat.GetAll().Where(_ => _.EvenementId == e.Id).Select(_ => _.CategorieId),
                Adresse = e.Adresse,
                DateDebut = e.DateDebut,
                DateFin = e.DateFin,
                Description = e.Description,
                Prix = e.Prix,
                Titre = e.Titre,
                VilleId = e.VilleId
            };
        }
        public IEnumerable<ParticipationDTO> GetParticipations(int id) 
        {
            var evenement = _repoEvenement.GetById(id);

            if (evenement == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            return _repoParticipations.GetAll().Where(_ => _.EvenementId == id && _.IsValid).Select(p => new ParticipationDTO { 
                Id = p.Id,
                Courriel = p.Courriel,
                NbPlaces = p.NbPlaces,
                Nom = p.Nom,
                Prenom = p.Prenom,
                EvenementId = p.EvenementId
            });
        }
        public EvenementDTO Add(EvenementDTO value) 
        {
            ValiderModele(value);
            var evenement = new Evenement {
                NomOrganisateur = value.NomOrganisateur,
                Adresse = value.Adresse,
                DateDebut = value.DateDebut,
                DateFin = value.DateFin,
                Description = value.Description,
                Prix = value.Prix,
                Titre = value.Titre,
                VilleId = value.VilleId
            };
            

            value.Id = _repoEvenement.Add(evenement);
            foreach (int catId in value.CategoriesId)
            {
                _repoEvCat.Add(new CategorieEvenement { CategorieId = catId, EvenementId = value.Id });
            }
            return value;
        }
        public EvenementDTO Update(int id, EvenementDTO value) 
        {
            ValiderModele(value);
            var evenement = new Evenement
            {
                Id = id,
                NomOrganisateur = value.NomOrganisateur,
                Adresse = value.Adresse,
                DateDebut = value.DateDebut,
                DateFin = value.DateFin,
                Description = value.Description,
                Prix = value.Prix,
                Titre = value.Titre,
                VilleId = value.VilleId
            };
           
            var evenementExists = _repoEvenement.GetById(id) != null;

            if (!evenementExists)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            _repoEvenement.Update(evenement);
            foreach(int evCatId in _repoEvCat.GetAll().Where(_ => _.EvenementId == id).Select(_ => _.Id))
            {
                _repoEvCat.Delete(evCatId);
            }
            foreach (int catId in value.CategoriesId)
            {
                _repoEvCat.Add(new CategorieEvenement { CategorieId = catId, EvenementId = id });
            }

            return value;
        }
        public void Delete(int id) 
        {
            var evenement = _repoEvenement.GetById(id);

            if (evenement == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element inexistant (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            _repoEvenement.Delete(id);
            
        }

        private void ValiderModele(EvenementDTO value)
        {
            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            if (String.IsNullOrEmpty(value.Adresse) ||
                String.IsNullOrEmpty(value.Description) ||
                String.IsNullOrEmpty(value.NomOrganisateur) ||
                value.DateDebut == DateTime.MinValue ||
                value.DateFin == DateTime.MinValue ||
                value.CategoriesId.Count() < 1 ||
                value.VilleId < 1)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides: champs non renseignés" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            foreach (int cat in value.CategoriesId)
            {
                if (_repoCategories.GetAll().FirstOrDefault(_ => _.Id == cat) == null)
                {
                    throw new HttpException
                    {
                        Errors = new { Errors = $"Parametres d'entrée non valides: catégorie avec Id {cat} inexistante " },
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
            }
            if (_repoVilles.GetAll().FirstOrDefault(_ => _.Id == value.VilleId) == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Parametres d'entrée non valides: ville avec id {value.VilleId} non existante" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }
    }
}
