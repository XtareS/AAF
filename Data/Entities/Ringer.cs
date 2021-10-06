using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AAF.Data.Entities
{
    public class Ringer:IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "imagem das costas")]
        public string ImageFront { get; set; }

        [Display(Name = "imagem da frente")]
        public string ImageBack { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        [Display(Name = "Disponível ?")]
        public bool Disponivel { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }

        public User User { get; set; }


        public string ImageFullPathFront
        {
            get
            {

                if (string.IsNullOrEmpty(this.ImageFront))

                {
                    return null;
                }

                return $"https://webSite{this.ImageFront.Substring(1)}";
            }

        }

        public string ImageFullPathBack
        {
            get
            {

                if (string.IsNullOrEmpty(this.ImageBack))

                {
                    return null;
                }

                return $"https://webSite{this.ImageBack.Substring(1)}";
            }

        }



    }
}
