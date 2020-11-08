using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tpinmobiliariafinal.Models;
using tpinmobiliariafinal.Models.Objetos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;

namespace tpinmobiliariafinal.ApiControllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PropietariosController : ControllerBase
    {
        private readonly DataContext _context;

        public PropietariosController(DataContext context)
        {
            _context = context;
        }

        // POST: api/Propietarios/API_LOGIN
        [HttpPost("API_LOGIN")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login l)
        {
            try
            {
                var hash = DataValues.getHashed(l.Password);
                var p = _context.Propietario.FirstOrDefault(u => u.Mail == l.Usuario && u.Password == hash);
                if (p == null) return BadRequest("Tus credenciales no coinciden con una cuenta en nuestro sistema");
                else return Ok(new JwtSecurityTokenHandler().WriteToken(DataValues.getToken(p)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Propietarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Propietario>>> GetPropietarios()
        {
            return await _context.Propietario.ToListAsync();
        }

        // GET: api/Propietarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Propietario>> GetPropietario(int id)
        {
            var propietario = await _context.Propietario.FindAsync(id);

            if (propietario == null)
            {
                return BadRequest("Error, no se encontro perfil");
            }

            return propietario;
        }

        // GET: api/Propietarios/LOGEADO
        [HttpGet("LOGEADO")]
        public async Task<ActionResult<Propietario>> GetLogeado()
        {
            var propietario = await _context.Propietario.SingleAsync(l => l.Mail == User.Identity.Name);

            if (propietario == null) return NotFound();
            
            return propietario;
        }

        // PUT: api/Propietarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("MODIFICAR")]
        public async Task<IActionResult> PutPropietario([FromBody] Propietario p)
        {
            _context.Entry(p).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropietarioExists(p.Id))
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

        // POST: api/Propietarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Propietario>> PostPropietario(Propietario propietario)
        {
            _context.Propietario.Add(propietario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPropietario", new { id = propietario.Id }, propietario);
        }

        // DELETE: api/Propietarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Propietario>> DeletePropietario(int id)
        {
            var propietario = await _context.Propietario.FindAsync(id);
            if (propietario == null)
            {
                return NotFound();
            }

            _context.Propietario.Remove(propietario);
            await _context.SaveChangesAsync();

            return propietario;
        }

        private bool PropietarioExists(int id)
        {
            return _context.Propietario.Any(e => e.Id == id);
        }
    }
}
