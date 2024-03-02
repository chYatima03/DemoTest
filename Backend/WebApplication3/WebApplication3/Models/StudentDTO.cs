﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using WebApplication3.Validators;

namespace WebApplication3.Models
{
    public class StudentDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "Studen name is required")]
        [StringLength(30)]
        public string StudentName { get; set; }

        [EmailAddress(ErrorMessage = " Please enter valid email address")]
        public string Email { get; set; }

        //[Range(10,20)]
        //public int Age { get; set; }

        [Required]
        public string Address { get; set; }

        //[DateCheck]
        //public DateTime AdmissionDate { get; set; }

        //public string Password { get; set; }

        //[Compare(nameof(Password))]
        //public string ConfirmPassword { get; set; }
        public DateTime DOB { get; set; }
    }
}
