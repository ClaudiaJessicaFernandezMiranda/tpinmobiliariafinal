using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tpinmobiliariafinal.Models;
using tpinmobiliariafinal.Models.Objetos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace tpinmobiliariafinal.ApiControllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ContratosController : ControllerBase
    {
        private readonly DataContext _context;

        public ContratosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Contratos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContrato()
        {
            return await _context.Contrato.Include(inm => inm.inmueble).Include(i => i.inquilino).ToListAsync();
        }

        // GET: api/SUS_CONTRATOS/5
        [HttpGet("SUS_CONTRATOS/{id}")]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContrato(int id)
        {
            var contratos = await _context.Contrato
                .Include(inm => inm.inmueble)
                .Include(i => i.inquilino)
                .Include(p => p.inmueble.Propietario)
                .Where(c => c.IdInmueble == id).ToListAsync();


            if (contratos == null)
            {
                return NotFound();
            }

            return contratos;
        }

        // PUT: api/Contratos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContrato(int id, Contrato contrato)
        {
            if (id != contrato.Id)
            {
                return BadRequest();
            }

            _context.Entry(contrato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContratoExists(id))
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

        // POST: api/Contratos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Contrato>> PostContrato(Contrato contrato)
        {
            _context.Contrato.Add(contrato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContrato", new { id = contrato.Id }, contrato);
        }

        // DELETE: api/Contratos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contrato>> DeleteContrato(int id)
        {
            var contrato = await _context.Contrato.FindAsync(id);
            if (contrato == null)
            {
                return NotFound();
            }

            _context.Contrato.Remove(contrato);
            await _context.SaveChangesAsync();

            return contrato;
        }

        private bool ContratoExists(int id)
        {
            return _context.Contrato.Any(e => e.Id == id);
        }
    }
}
