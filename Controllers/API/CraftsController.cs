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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CraftsController:Controller
    {
        private readonly ICraftRepository craftRepository;

        public CraftsController(ICraftRepository craftRepository)
        {
            this.craftRepository = craftRepository;
        }

        [HttpGet]
        public IActionResult GetCrafts()
        {
            return Ok(this.craftRepository.GetAllWithUser());
        }
    }
}
