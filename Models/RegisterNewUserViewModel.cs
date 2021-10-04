using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Models
{
    public class RegisterNewUserViewModel
    {
        [Required]
        [Display(Name =" Nome")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = " Apelido")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = " nome de utilizador")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = " escreva a sua Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = " confirme a Password")]
        [Compare("Password")]
        public string Confirm { get; set; }

    }
}
