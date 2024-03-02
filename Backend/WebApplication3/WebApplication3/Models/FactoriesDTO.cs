using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using WebApplication3.Data;

namespace WebApplication3.Models
{
    public class FactoriesDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "Factory no is required")]
        [StringLength(50)]
        public string factoryno { get; set; }

        [Required(ErrorMessage = "Factory name is required")]
        [StringLength(150)]
        public string factoryname { get; set; }
        //public bool status { get; set; }
        public DateTime DOB { get; set; }
        public string modifiedby { get; set; }

    }
}
