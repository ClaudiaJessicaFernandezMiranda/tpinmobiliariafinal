using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tpinmobiliariafinal.Models;
using tpinmobiliariafinal.Models.Objetos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace tpinmobiliariafinal.ApiControllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class InmueblesController : ControllerBase
    {
        private readonly DataContext _context;

        public InmueblesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Inmuebles
        [HttpGet("SUS_INMUEBLES")]
        public async Task<ActionResult<IEnumerable<Inmueble>>> GetInmueble()
        {
            var inmuebles = await _context.Inmueble.Include(p => p.Propietario).Where(i => i.Propietario.Mail == User.Identity.Name && i.Estado == 1).ToListAsync();
            if (inmuebles == null) return NotFound();
            return inmuebles;
        }

        // GET: api/InmueblesDes
        [HttpGet("SUS_INMUEBLES_DES")]
        public async Task<ActionResult<IEnumerable<Inmueble>>> GetInmuebleDes()
        {
            var inmuebles = await _context.Inmueble.Include(p => p.Propietario).Where(i => i.Propietario.Mail == User.Identity.Name && i.Estado == 0).ToListAsync();
            if (inmuebles == null) return NotFound();
            return inmuebles;
        }

        // GET: api/Inmuebles/5       
        [HttpGet("{id}")]
        public async Task<ActionResult<Inmueble>> GetInmueble(int id)
        {
            var inmueble = await _context.Inmueble.FindAsync(id);

            if (inmueble == null)
            {
                return NotFound();
            }

            return inmueble;
        }

        // PUT: api/Inmuebles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInmueble(int id, Inmueble inmueble)
        {
            if (id != inmueble.Id)
            {
                return BadRequest();
            }

            _context.Entry(inmueble).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InmuebleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Inmuebles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Inmueble>> PostInmueble(Inmueble inmueble)
        {
            _context.Inmueble.Add(inmueble);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInmueble", new { id = inmueble.Id }, inmueble);
        }

        // DELETE: api/BAJA_LOGICA/5
        [HttpDelete("BAJA_LOGICA/{id}")]
        public async Task<ActionResult<Boolean>> DeleteInmueble(int id)
        {
            var inmueble = await _context.Inmueble.FindAsync(id);
            if (inmueble == null) return BadRequest("No existe ese inmueble");
            if (inmueble.Estado == 1) inmueble.Estado = 0;          
            else inmueble.Estado = 1;

            await _context.SaveChangesAsync();
            return true;
        }

        private bool InmuebleExists(int id)
        {
            return _context.Inmueble.Any(e => e.Id == id);
        }
    }
}
