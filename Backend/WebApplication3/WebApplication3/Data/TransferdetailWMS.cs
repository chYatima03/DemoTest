namespace WebApplication3.Data
{
    public class TransferdetailWMS
    {

        public int Id { get; set; }
        public string no { get; set; }
        public int? TransfersId { get; set; }
        public virtual TransferWMS? Transfers { get; set; }
        public string name { get; set; }
        public float qty { get; set; }
        public string unit { get; set; }
        public string outwmsno { get; set; }
        public string inwmsno { get; set; }
        public DateTime DOB { get; set; }
        public string modifiedby { get; set; }
    }
}
