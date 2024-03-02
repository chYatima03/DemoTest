namespace WebApplication3.Data
{
    public class Stock
    {
        public int Id { get; set; }
        public string no { get; set; }
        public string lotno { get; set; }
        public string name { get; set; }
        public string qty { get; set; }
        public string unit { get; set; }
        public DateTime expiredate { get; set; }
        public string currentwmsno { get; set; }

        public int stockstatus { get; set; }
        public DateTime DOB { get; set; }
        public string modifiedby { get; set; }
    }
}
