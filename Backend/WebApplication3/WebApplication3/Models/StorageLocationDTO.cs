using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using WebApplication3.Data;

namespace WebApplication3.Models
{
    public class StorageLocationDTO
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required(ErrorMessage = "Storage Location no is required")]
        [StringLength(50)]
        public string no { get; set; }
        [Required(ErrorMessage = "Storage Location name is required")]
        [StringLength(150)]
        public string name { get; set; }
        public DateTime DOB { get; set; }
        public string modifiedby { get; set; }
        public virtual ICollection<Location> Locations { get; set; }

    }
}
