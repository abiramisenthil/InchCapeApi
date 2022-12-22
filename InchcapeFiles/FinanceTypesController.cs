using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUsingEF.Data;
using WebApiUsingEF.Model;

namespace WebApiUsingEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FinanceTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FinanceTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinanceType>>> GetfinanceTypes()
        {
            return await _context.FinanceTypes.ToListAsync();
        }

        // GET: api/FinanceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinanceType>> GetFinanceType(int id)
        {
            var financeType = await _context.FinanceTypes.FindAsync(id);

            if (financeType == null)
            {
                return NotFound();
            }

            return financeType;
        }

        // PUT: api/FinanceTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinanceType(int id, FinanceType financeType)
        {
            if (id != financeType.FinanceTypeId)
            {
                return BadRequest();
            }

            _context.Entry(financeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinanceTypeExists(id))
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

        // POST: api/FinanceTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FinanceType>> PostFinanceType(FinanceType financeType)
        {
            _context.FinanceTypes.Add(financeType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFinanceType", new { id = financeType.FinanceTypeId }, financeType);
        }

        // DELETE: api/FinanceTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinanceType(int id)
        {
            var financeType = await _context.FinanceTypes.FindAsync(id);
            if (financeType == null)
            {
                return NotFound();
            }

            _context.FinanceTypes.Remove(financeType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinanceTypeExists(int id)
        {
            return _context.FinanceTypes.Any(e => e.FinanceTypeId == id);
        }
    }
}
