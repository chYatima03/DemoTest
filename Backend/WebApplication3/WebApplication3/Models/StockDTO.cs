using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class StockDTO
    {
        [ValidateNever]
        public int Id { get; set; }
        [StringLength(25)]
        public string no { get; set; }
        [Required(ErrorMessage = "Stock lotno is required")]
        [StringLength(15)]
        public string lotno { get; set; }
        [Required(ErrorMessage = "Stock name is required")]
        [StringLength(150)]
        public string name { get; set; }
        [Required(ErrorMessage = "Stock qty is required")]
        [StringLength(10)]
        public float qty { get; set; }
        [Required(ErrorMessage = "Stock unit is required")]
        [StringLength(10)]
        public string unit { get; set; }
        [Required(ErrorMessage = "Stock expiredate is required")]
        public DateTime expiredate { get; set; }
        [Required(ErrorMessage = "Stock current warehouse is required")]
        [StringLength(25)]
        public string currentwmsno { get; set; }

        public int stockstatus { get; set; }

        public string modifiedby { get; set; }
    }
}
