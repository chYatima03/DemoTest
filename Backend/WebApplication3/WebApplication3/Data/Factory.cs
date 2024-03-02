namespace WebApplication3.Data
{
    public class Factory
    {
        public int Id { get; set; }
        public string factoryno { get; set; }
        public string factoryname { get; set; }
        //public bool status { get; set; }
        public DateTime DOB { get; set; }
        public string modifiedby { get; set; }
        public virtual ICollection<StorageLocation> StorageLocations { get; set; }
        public virtual ICollection<Location> Locations { get; set; }

    }
}
