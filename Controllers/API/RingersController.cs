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
    public class RingersController:Controller
    {
        private readonly IRingerRepository ringerRepository;

        public RingersController(IRingerRepository ringerRepository)
        {
            this.ringerRepository = ringerRepository;
        }

        [HttpGet]
        public IActionResult GetRingers()
        {
            return Ok(this.ringerRepository.GetAllWithUser());
        }


    }
}
