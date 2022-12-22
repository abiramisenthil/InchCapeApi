using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUsingEF.Data;
using WebApiUsingEF.Model;

namespace WebApiUsingEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarFinancesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarFinancesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarFinance>>> GetCarFinances()
        {
            return await _context.CarFinances
                .AsNoTracking()
                .Include(i => i.Make)
                .Include(i => i.VehicleType)
                .Include(i => i.FinanceType)
                .Include(i =>i.CarFinanceRanges).ThenInclude(i=>i.FinanceRange)
                .ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarFinance(int id, CarFinance carFinance)
        {
            if (id != carFinance.FinanceTypeId)
            {
                return BadRequest();
            }

            _context.Entry(carFinance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var carFinanceRange = await _context.CarFinanceRanges.Where(r => r.CarFinanceId == carFinance.CarFinanceId).ToListAsync();

                if(carFinanceRange.Count()>0)
                {
                    _context.CarFinanceRanges.RemoveRange(carFinanceRange);
                    await _context.SaveChangesAsync();
                }

                if(carFinance.CarFinanceRanges.Count()>0)
                {
                    _context.CarFinanceRanges.AddRange(carFinance.CarFinanceRanges);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarFinanceExists(id))
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

        //[HttpPost]
        //public async Task<ActionResult<CarFinance>> AddCarInsurance(CarFinance carFinance)
        //{
        //    _context.CarFinances.Add(carFinance);
        //    await _context.SaveChangesAsync();

        //    return Ok(carFinance);
        //}

        private bool CarFinanceExists(int id)
        {
            return _context.CarFinances.Any(e => e.CarFinanceId == id);
        }
    }
}
