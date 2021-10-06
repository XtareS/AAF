using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Models
{
    public class ChangeUserViewModel
    {
        [Required]
        [Display(Name = " Nome")]
        public string FirstName { get; set; }

       
        [Required]
        [Display(Name = " Apelido")]
        public string LastName { get; set; }


        [Display(Name = " nome de utilizador")]
        public string UserName { get; set; }


     }
}
