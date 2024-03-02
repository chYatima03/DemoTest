namespace WebApplication3.Data
{
    public class Document
    {
        public int Id { get; set; }
        public string docno { get; set; }
        public string docname { get; set; }
        //public bool status { get; set; }
        public DateTime DOB { get; set; }
        public string modifiedby { get; set; }

        public virtual ICollection<TransferWMS> Transfers { get; set; }
    }
}
