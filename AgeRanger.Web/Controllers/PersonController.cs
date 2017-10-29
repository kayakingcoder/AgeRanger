using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AgeRanger.Service.DTO;
using AgeRanger.Service;
using AgeRanger.Core;

namespace AgeRanger.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Person")]
    public class PersonController : Controller
    {
        protected IPersonService personService;

        public PersonController(IPersonService personService )
        {
            this.personService = personService;
        }

        // GET: api/Person
        [HttpGet]
        public IEnumerable<PersonDTO> Get(ListFilter filter)
        {
            var result = personService.Get(filter);
            return result;
        }

        // GET: api/Person/5
        [HttpGet("{id}", Name = "PersonLink")]
        public string Get(int id)
        {
            throw new NotImplementedException();
        }
        
        // POST: api/Person
        [HttpPost]
        public IActionResult Create([FromBody]PersonDTO person)
        {
            var newPerson = personService.Add(person);

            return CreatedAtRoute("PersonLink", new { id = newPerson.Id }, newPerson);
        }
        
        // PUT: api/Person/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]PersonDTO person)
        {
            try
            {
                var updatedPerson = personService.Update(person);

                return CreatedAtRoute("PersonLink", new { id = updatedPerson.Id }, updatedPerson);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
