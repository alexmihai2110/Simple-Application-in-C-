using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cosultations.Models
{
    public class RegisterVm 
    {

        [EmailAddress(ErrorMessage = "Email is not valid!")]
        [MaxLength(100, ErrorMessage = "100 characters is the maximum")]
        [Required(ErrorMessage = "Required!")]
       
        public string Email { get; set; }

        [MaxLength(100, ErrorMessage = "Campul trebuie sa aiba maximum 100 de caracatere")]
        [Required(ErrorMessage = "Required!")]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "100 characters is the maximum")]
        [Required(ErrorMessage = "Required!")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "100 characters is the maximum")]
        [Required(ErrorMessage = "Required!")]
        public string Password { get; set; }
        [MaxLength(100, ErrorMessage = "100 characters is the maximum")]
        [Required(ErrorMessage = "Required!")]
        public string ConfirmPassword { get; set; }



    }
}
