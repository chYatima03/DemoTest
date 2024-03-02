namespace WebApplication3.Data
{
    public class Location
    {
        public int Id { get; set; }
        public int? factoriesid { get; set; }
        public virtual Factory? Factory { get; set; }

        public int? storagelocid { get; set; }
        public virtual StorageLocation? StorageLocation { get; set; }
        public string zone { get; set; }
        public string layer { get; set; }
        public string road { get; set; }
        public string column { get; set; }
        public string row { get; set; }
        public string position { get; set; }
        //public int status { get; set; }
        public DateTime DOB { get; set; }
        public string modifiedby { get; set; }
    }
}
