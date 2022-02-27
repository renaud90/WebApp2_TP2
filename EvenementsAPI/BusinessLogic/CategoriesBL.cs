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
    public class CategoriesBL : ICategoriesBL
    {
        private readonly IRepository<Categorie> _repo;
        private readonly IRepository<CategorieEvenement> _repoEvenementsCat;
        public CategoriesBL(IRepository<Categorie> repo, IRepository<CategorieEvenement> repoEvenementsCat)
        {
            _repo = repo;
            _repoEvenementsCat = repoEvenementsCat;
        }
        public IEnumerable<CategorieDTO> GetList()
        {
            return _repo.GetAll().Select(c => new CategorieDTO
            {
                Id = c.Id,
                Nom = c.Nom
            });
           
        }
        public CategorieDTO Get(int id)
        {
            var cat = _repo.GetById(id);
            return new CategorieDTO { Id = cat.Id, Nom = cat.Nom };
        }

        public CategorieDTO Add(CategorieDTO value)
        {
            var cat = new Categorie { Nom = value.Nom };
            ValiderModele(cat);

            value.Id = _repo.Add(cat);

            return value;
        }
        public CategorieDTO Update(int id, CategorieDTO value)
        {
            var cat = new Categorie { Id = id, Nom = value.Nom };
            ValiderModele(cat);

            var categorie = _repo.GetById(id);

            if (categorie == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            _repo.Update(cat);

            return value;
        }
        public void Delete(int id)
        {
            var categorie = _repo.GetById(id);

            if (categorie == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element inexistant (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var categorieAssociee = _repoEvenementsCat.GetAll().Where(_ => _.CategorieId == categorie.Id).Count() > 0;

            if (categorieAssociee)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element (id = {id}) ne peut être supprimé, car associé à au moins un événement" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            _repo.Delete(id);
        }

        private void ValiderModele(Categorie value)
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
                    Errors = new { Errors = "Parametres d'entrée non valides: nom de catégorie inexistant" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }
    }
}
