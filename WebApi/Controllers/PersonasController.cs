using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonasController : ControllerBase
    {
        private readonly Context context;

        public PersonasController(Context context)
        {
            this.context = context;
        }

        // GET: api/Personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personas>>> Get()
        {
            List<Personas> Personas = new List<Personas>();
            try
            {
                Personas = await context.Personas.Where(x => x.Nulo == false).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return Personas;
        }

        // GET: api/Personas/5
        [HttpGet("{id}", Name = "ObtenerPersona")]
        public async Task<ActionResult<Personas>> Get(int id)
        {
            var Personas = await context.Personas.FirstOrDefaultAsync(x => x.Id == id && x.Nulo == false);

            if (Personas == null)
                return NotFound();

            return Personas;
        }

        // POST: api/Personas
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Personas Personas)
        {
            try
            {
                await context.Personas.AddAsync(Personas);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return new CreatedAtRouteResult("ObtenerPersona", new { id = Personas.Id }, Personas);
        }

        // PUT: api/Personas/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] Personas Personas)
        {
            try
            {
                context.Entry(Personas).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return Ok();
        }

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Personas>> Delete(int id)
        {
            //aqui utilizo LinQ para buscar en la base de datos el registro seleccionado
            Personas c = (from x in context.Personas
                          where x.Id == id
                          select x).First();

            if (c.Id == default(int))
            {
                return NotFound();
            }
            //aqui cambio el registro seleccionado a nulo
            c.Nulo = true;

            await context.SaveChangesAsync();
            return NoContent();
        }




    }
}
