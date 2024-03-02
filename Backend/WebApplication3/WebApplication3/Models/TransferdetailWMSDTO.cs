using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class TransferdetailWMSDTO
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required(ErrorMessage = "Stock no is required")]
        [StringLength(15)]
        public string no { get; set; }
        [Required(ErrorMessage = "Stock outstore is required")]
        [StringLength(25)]
        public string outwmsno { get; set; }
        [Required(ErrorMessage = "Stock Instore is required")]
        [StringLength(25)]
        public string inwmsno { get; set; }


        public string modifiedby { get; set; }
    }
}
