using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUsingEF.Data;
using WebApiUsingEF.Model;

namespace WebApiUsingEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        public AdminController(AppDbContext dbContext) : base(dbContext)
        {

        }

        [HttpGet("GetMakes")]
        public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        {
            return await _dbContext.Makes.ToListAsync();
        }

        [HttpPost("AddMake")]
        public async Task<ActionResult<Make>> AddMake(Make make)
        {
            if (!string.IsNullOrEmpty(make.MakeName))
            {
                await InsertOrUpdateVehicleMake(make.MakeId, make.MakeName);
                return Ok(make);
            }

            return BadRequest();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Make>> UpdateMake(Make make)
        {
            if (!string.IsNullOrEmpty(make.MakeName))
            {
                await InsertOrUpdateVehicleMake(make.MakeId, string.Empty, make.MakeName);
                return Ok(make);
            }

            return BadRequest();
        }

        [HttpGet("GetVehicleType")]
        public async Task<ActionResult<IEnumerable<VehicleType>>> GetVehicleTypes()
        {
            return await _dbContext.VehicleTypes.ToListAsync();
        }

        [HttpPost("AddVehicleType")]
        public async Task<ActionResult<VehicleType>> AddVehicleType(VehicleType vehicleType)
        {
            if (!string.IsNullOrEmpty(vehicleType.VehicleTypeName))
            {
                await InsertOrUpdateVehicleType(vehicleType.VehicleTypeId, vehicleType.VehicleTypeName);
                return Ok(vehicleType);
            }

            return BadRequest();

        }

        [HttpPut("updateVehicleType/{id}")]
        public async Task<ActionResult<VehicleType>> UpdateVehicleType(VehicleType vehicleType)
        {
            if (!string.IsNullOrEmpty(vehicleType.VehicleTypeName))
            {
                await InsertOrUpdateVehicleType(vehicleType.VehicleTypeId, string.Empty, vehicleType.VehicleTypeName);
                return Ok(vehicleType);
            }

            return BadRequest();
        }

        [HttpGet("GetFinanceType")]
        public async Task<ActionResult<IEnumerable<FinanceType>>> GetFinanceTypes()
        {
            return await _dbContext.FinanceTypes.ToListAsync();
        }

        [HttpPost("AddFinanceType")]
        public async Task<ActionResult<FinanceType>> AddFinanceType(FinanceType FinanceType)
        {
            if (!string.IsNullOrEmpty(FinanceType.FinanceTypeName))
            {
                await InsertOrUpdateFinanceType(FinanceType.FinanceTypeId, FinanceType.FinanceTypeName);
                return Ok(FinanceType);
            }

            return BadRequest();
        }

        [HttpPut("UpdateFianceType/{id}")]
        public async Task<ActionResult<FinanceType>> UpdateFinanceType(FinanceType financeType)
        {
            if (!string.IsNullOrEmpty(financeType.FinanceTypeName))
            {
                await InsertOrUpdateFinanceType(financeType.FinanceTypeId, string.Empty, financeType.FinanceTypeName);
                return Ok(financeType);
            }

            return BadRequest();
        }

        [HttpGet("GetFinanceRange")]
        public async Task<ActionResult<IEnumerable<FinanceRange>>> GetFinanceRanges()
        {
            return await _dbContext.FinanceRanges.ToListAsync();
        }

        [HttpPost("AddFinanceRange")]
        public async Task<ActionResult<FinanceRange>> PostFinanceRange(FinanceRange financeRange)
        {
            _dbContext.FinanceRanges.Add(financeRange);
            await _dbContext.SaveChangesAsync();

            return Ok(financeRange);
        }

        protected virtual async Task<Make> InsertOrUpdateVehicleMake(int Id, String Name, string newName = "")
        {
            //Name will be null when we call this method for Update Purpose

            var isMakeExists = await GetVehicleMakeInfo(Id, Name);
            // When Vehicle make is not found then insert
            if (isMakeExists == null)
            {
                var newMake = new Make()
                {
                    MakeId = Id,
                    MakeName = Name
                };
                try
                {
                    await _dbContext.Makes.AddAsync(newMake);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return newMake;
            }
            else if (Id == isMakeExists.MakeId && !string.IsNullOrEmpty(newName)) // When vehicle make is exisit then update
            {
                _dbContext.Entry(isMakeExists).State = EntityState.Modified;

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return isMakeExists;
            }

            return isMakeExists;
        }

        protected async Task<VehicleType> InsertOrUpdateVehicleType(int Id, String Name, string newName = "")
        {
            //Name will be null when we call this method for Update Purpose

            var isVehicleTypeExists = await GetVehicleTypeInfo(Id, Name);
            // When Vehicle type is not found then insert
            if (isVehicleTypeExists == null)
            {
                var newVehicleType = new VehicleType()
                {
                    VehicleTypeId = Id,
                    VehicleTypeName = Name
                };
                try
                {
                    await _dbContext.VehicleTypes.AddAsync(newVehicleType);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return newVehicleType;
            }
            else if (Id == isVehicleTypeExists.VehicleTypeId && !string.IsNullOrEmpty(newName)) // When vehicle type is exisit then update
            {
                _dbContext.Entry(isVehicleTypeExists).State = EntityState.Modified;

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return isVehicleTypeExists;
            }

            return isVehicleTypeExists;
        }

        protected async Task<FinanceType> InsertOrUpdateFinanceType(int Id, String Name, string newName = "")
        {
            //Name will be null when we call this method for Update Purpose

            var isFinanceTypeExists = await GetFinanceTypeInfo(Id, Name);
            // When Finance Type is not found then insert
            if (isFinanceTypeExists == null)
            {
                var newFinanceType = new FinanceType()
                {
                    FinanceTypeId = Id,
                    FinanceTypeName = Name
                };
                try
                {
                    await _dbContext.FinanceTypes.AddAsync(newFinanceType);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return newFinanceType;
            }
            else if (Id == isFinanceTypeExists.FinanceTypeId && !string.IsNullOrEmpty(newName)) // When FinanceType is exisit then update
            {
                _dbContext.Entry(isFinanceTypeExists).State = EntityState.Modified;

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return isFinanceTypeExists;
            }

            return isFinanceTypeExists;
        }
    }
}
