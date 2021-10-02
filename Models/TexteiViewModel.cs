using AAF.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Models
{
    public class TexteiViewModel:Textei
    {

        [Display(Name = "imagem das costas")]
        public IFormFile ImageFileFront { get; set; }

        [Display(Name = "imagem da frente")]
        public IFormFile ImageFileBack { get; set; }

    }
}
