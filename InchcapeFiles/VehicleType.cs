using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiUsingEF.Model
{
    public class VehicleType
    {
        public VehicleType()
        {
            CarFinances = new HashSet<CarFinance>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; }

        public ICollection<CarFinance> CarFinances { get; set; }
    }
}
