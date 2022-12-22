namespace WebApiUsingEF.Model
{
    public class Make
    {
        public Make()
        {
            CarFinances = new HashSet<CarFinance>();
        }
        public int MakeId { get; set; }
        public string MakeName { get; set; }

        public ICollection<CarFinance> CarFinances { get; set; }
    }
}
