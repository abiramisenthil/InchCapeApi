namespace WebApiUsingEF.Model
{
    public class FinanceRange
    {
        public FinanceRange()
        {
            CarFinanceRanges = new HashSet<CarFinanceRange>();
        }
        public int FinanceRangeId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public ICollection<CarFinanceRange> CarFinanceRanges { get; set; }
    }
}
