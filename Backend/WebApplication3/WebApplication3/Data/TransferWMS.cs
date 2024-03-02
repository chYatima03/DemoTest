namespace WebApplication3.Data
{
    public class TransferWMS
    {
        public int Id { get; set; }
        public string no { get; set; }
        public int? DocumentId { get; set; }
        public virtual Document? Document { get; set; }
        public string outstore { get; set; }
        public string instore { get; set; }
        public string currentwmsno { get; set; }
        public DateTime DOB { get; set; }
        public string modifiedby { get; set; }

        public virtual ICollection<TransferdetailWMS> Transferdetails { get; set; }

    }
}
