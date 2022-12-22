using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiUsingEF.Model
{
    public class CarFinanceRange
    {
        public int CarFinanceId { get; set; }

        public int FinanceRangeId { get; set; }

        public CarFinance CarFinance { get; set; }

        public FinanceRange FinanceRange { get; set; }
    }
}
