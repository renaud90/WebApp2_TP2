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
    public class VillesBL : IVillesBL
    {
        private readonly IVilleRepository _repoVille;
        private readonly IEvenementRepository _repoEvenement;
        private readonly IRepository<CategorieEvenement> _repoEvenementCat;
        public VillesBL(IVilleRepository repoVille, IEvenementRepository repoEvenement, IRepository<CategorieEvenement> repoEvenementCat)
        {
            _repoVille = repoVille;
            _repoEvenement = repoEvenement;
            _repoEvenementCat = repoEvenementCat;
        }
        public VilleDTO Add(VilleDTO value)
        {
            var ville = new Ville { Nom = value.Nom, Region = value.Region };
            ValiderModeleVille(ville);

            value.Id = _repoVille.Add(ville);

            return value;
        }

        public IEnumerable<VilleDTO> GetList()
        {
            return _repoVille.GetAll().Select(v => new VilleDTO
            {
               Id = v.Id,
               Nom = v.Nom,
               Region = v.Region
            });
        }

        public VilleDTO Get(int id)
        {
            var ville = _repoVille.GetById(id);
            if (ville == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            return new VilleDTO { 
                Id = ville.Id, 
                Nom = ville.Nom, 
                Region = ville.Region
            };

        }

        public IEnumerable<EvenementDTO> GetEvenements(int id)
        {
            var ville = _repoVille.GetById(id);
            if (ville == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            return _repoEvenement.GetAll().Where(_ => _.VilleId == id).Select(e => new EvenementDTO { 
                Id = e.Id,
                NomOrganisateur = e.NomOrganisateur,
                Adresse = e.Adresse,
                CategoriesId = _repoEvenementCat.GetAll().Where(_ => _.EvenementId == id).Select(_ => _.CategorieId),
                DateDebut = e.DateDebut,
                DateFin = e.DateFin,
                Description = e.Description,
                Prix = e.Prix,
                Titre = e.Titre,
                VilleId = e.VilleId
            });

        }

        public VilleDTO Updade(int id, VilleDTO value)
        {
            var ville = new Ville { Id = id, Nom = value.Nom, Region = value.Region };
            ValiderModeleVille(ville);

            var villeBD = _repoVille.GetById(id);


            if (villeBD == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            _repoVille.Update(ville);

            return value;
        }

        public void Delete(int id)
        {
            var ville = _repoVille.GetById(id);
            if (ville == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            var villeAssociee = _repoEvenement.GetAll().FirstOrDefault(e => e.VilleId == id);

            if (villeAssociee != null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element (id = {id}) ne peut être supprimé, car associé à au moins un événement" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }


            _repoVille.Delete(id); 
        }

        public ICollection<VilleDTO> GetByNbEvenementsOrdered()
        {
           return _repoVille.GetByNbEvenementsOrdered();
        }

        private void ValiderModeleVille(Ville value)
        {
            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            if (String.IsNullOrEmpty(value.Nom))
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides: nom de ville non fourni" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        
    }
}
