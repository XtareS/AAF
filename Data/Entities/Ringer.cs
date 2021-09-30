using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AAF.Data.Entities
{
    public class Ringer 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "imagem das costas")]
        public string ImageFront { get; set; }

        [Display(Name = "imagem da frente")]
        public string ImageBack { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Disponível ?")]
        public bool Disponivel { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }


    }
}
