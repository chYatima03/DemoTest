using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class DocumentDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "Document no is required")]
        //[StringLength(50)]
        public string docno { get; set; }

        [Required(ErrorMessage = "Document name is required")]
        //[StringLength(150)]
        public string docname { get; set; }

        //public bool status { get; set; }
        public DateTime DOB { get; set; }
        public string modifiedby { get; set; }
    }
}
