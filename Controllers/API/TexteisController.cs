using AAF.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Controllers.API
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
    public class TexteisController:Controller
    {
        private readonly ITexteiRepository texteiRepository;

        public TexteisController(ITexteiRepository texteiRepository)
        {
            this.texteiRepository = texteiRepository;
        }

        [HttpGet]
        public IActionResult GetTexteis()
        {
            return Ok(this.texteiRepository.GetAllWithUser());
        }





    }
}
