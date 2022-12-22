using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiUsingEF.Model
{
    public class CarFinance
    {
        public CarFinance()
        {
            CarFinanceRanges = new HashSet<CarFinanceRange>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarFinanceId { get; set; }

        public int? MakeId { get; set; }

        public Make Make { get; set; }

        public int? VehicleTypeId { get; set; }

        public VehicleType VehicleType { get; set; }

        public int? FinanceTypeId { get; set; }

        public FinanceType FinanceType { get; set; }

        public ICollection<CarFinanceRange> CarFinanceRanges { get; set; }
    }
}
