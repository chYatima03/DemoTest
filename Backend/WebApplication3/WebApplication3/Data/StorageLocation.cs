namespace WebApplication3.Data
{
    public class StorageLocation
    {
        public int Id { get; set; }
        public string no { get; set; }
        public string name { get; set; }
        public DateTime DOB { get; set; }
        public string modifiedby { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
