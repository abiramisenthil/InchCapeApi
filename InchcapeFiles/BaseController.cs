using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUsingEF.Data;
using WebApiUsingEF.Model;

namespace WebApiUsingEF.Controllers
{
    public class BaseController : Controller
    {
        protected readonly AppDbContext _dbContext;

        public BaseController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected async Task<Make> GetVehicleMakeInfo(int Id, String Name)
        {

            var record = await _dbContext.Makes
                    .FirstOrDefaultAsync(f => (Id != null && f.MakeId == Id) || (!string.IsNullOrEmpty(Name) && f.MakeName == Name));
            if (record != null)
                return record;


            return null;
        }

        protected async Task<VehicleType> GetVehicleTypeInfo(int Id, String Name)
        {

            var record = await _dbContext.VehicleTypes
                    .FirstOrDefaultAsync(f => (Id != null && f.VehicleTypeId == Id) || (!string.IsNullOrEmpty(Name) && f.VehicleTypeName == Name));
            if (record != null)
                return record;


            return null;
        }

        protected async Task<FinanceType> GetFinanceTypeInfo(int Id, String Name)
        {

            var record = await _dbContext.FinanceTypes
                    .FirstOrDefaultAsync(f => (Id != null && f.FinanceTypeId == Id) || (!string.IsNullOrEmpty(Name) && f.FinanceTypeName == Name));
            if (record != null)
                return record;


            return null;
        }

        protected async Task<FinanceRange> GetFinanceRangeInfo(int Id, String Name)
        {

            var record = await _dbContext.FinanceRanges
                    .FirstOrDefaultAsync(f => (Id != null && f.FinanceRangeId == Id) || (!string.IsNullOrEmpty(Name) && f.Name == Name));
            if (record != null)
                return record;


            return null;
        }
    }
}
