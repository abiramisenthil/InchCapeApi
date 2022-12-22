using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiUsingEF.Model
{
    public class FinanceType
    {
        public FinanceType()
        {
            CarFinances = new HashSet<CarFinance>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FinanceTypeId { get; set; }

        public string FinanceTypeName { get; set; }

        public ICollection<CarFinance> CarFinances { get; set; }

     }
}
