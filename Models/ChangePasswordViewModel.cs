using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name ="Palavra-Passe Antiga")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = " Nova Palavra-Passe ")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string Confirm { get; set; }
    }
}
