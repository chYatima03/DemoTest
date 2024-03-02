using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using WebApplication3.Data;

namespace WebApplication3.Models
{
    public class LocationDTO
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required(ErrorMessage = "Factory no is required")]
        [StringLength(50)]
        public string zone { get; set; }
        [Required(ErrorMessage = "Location zone is required")]
        [StringLength(50)]
        public string layer { get; set; }
        [Required(ErrorMessage = "Location layer is required")]
        [StringLength(50)]
        public string road { get; set; }
        [Required(ErrorMessage = "Location road is required")]
        [StringLength(50)]
        public string column { get; set; }
        [Required(ErrorMessage = "Location column is required")]
        [StringLength(50)]
        public string row { get; set; }
        [Required(ErrorMessage = "Location row is required")]
        [StringLength(50)]
        public string position { get; set; }
        [Required(ErrorMessage = "Location position is required")]
        [StringLength(50)]
        //public int status { get; set; }
        public DateTime DOB { get; set; }
        public string modifiedby { get; set; }
    }
}
