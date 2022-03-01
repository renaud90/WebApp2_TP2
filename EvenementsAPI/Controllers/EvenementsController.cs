using EvenementsAPI.BusinessLogic;
using EvenementsAPI.DTO;
using EvenementsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EvenementsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EvenementsController : ControllerBase
    {
        private readonly IEvenementsBL _evenementsBL;

        public EvenementsController(IEvenementsBL evenementsBL)
        {
            _evenementsBL = evenementsBL;
        }

        // GET: api/<UsagersController>?filter=concert
        /// <summary>
        /// Lister toutes les événements de l'application
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<EvenementDTO>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<EvenementDTO>> Get(int pageIndex = 1, int pageCount = 5, [FromQuery] string filter = null)
        {
            return Ok(_evenementsBL.GetList(pageIndex, pageCount, filter));
        }

        // GET api/evenements/5
        /// <summary>
        /// Obtenir une événement par son ID
        /// </summary>
        /// <param name="id"> ID de l'événement</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EvenementDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<EvenementDTO> Get(int id)
        {
            var evenement = _evenementsBL.Get(id);
            return evenement is null ? NotFound(new { Errors = $"Element introuvable (id = {id})" }) : Ok(evenement);
        }

        // GET api/evenements/5/ventes
        /// <summary>
        /// Obtenir une événement par son ID
        /// </summary>
        /// <param name="id"> ID de l'événement</param>
        [HttpGet("{id}/ventes")]
        [ProducesResponseType(typeof(EvenementDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<double> GetVentes(int id)
        {
            return Ok(_evenementsBL.GetTotalVentes(id));
        }

        // GET api/usagers/5/participations
        /// <summary>
        /// Obtenir la liste des participation d'un événement par l'ID de l'événement
        /// </summary>
        /// <param name="id"> ID de l'événement</param>
        [HttpGet("{id}/participations")]
        [ProducesResponseType(typeof(EvenementDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<EvenementDTO> GetParticipations(int id)
        {
            var evenement = _evenementsBL.GetParticipations(id);
            return evenement is null ? NotFound(new { Errors = $"Element introuvable (id = {id})" }) : Ok(evenement);
        }

        // POST api/usagers
        /// <summary>
        /// Ajouter un nouvel événement
        /// </summary>
        /// <param name="value">L'événement à ajouter</param>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult Post([FromBody] EvenementDTO value)
        {
            value = _evenementsBL.Add(value);
            return CreatedAtAction(nameof(Get), new { id = value.Id }, null);

        }

        // PUT api/usagers/5
        /// <summary>
        /// Modifier un événement existant
        /// </summary>
        /// <param name="id">ID de l'événement</param>
        /// <param name="value">L'événement à modifier</param>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult Put(int id, [FromBody] EvenementDTO value)
        {
            _evenementsBL.Update(id, value);
            return NoContent();
        }

        // DELETE api/usagers/5
        /// <summary>
        /// Suppression d'un événement
        /// </summary>
        /// <param name="id">ID de l'événement</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult Delete(int id)
        {
            _evenementsBL.Delete(id);
            return NoContent();
        }
    }
}
