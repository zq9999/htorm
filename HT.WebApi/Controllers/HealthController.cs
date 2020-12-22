using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HT.WebApi.Controllers
{
   
    [ApiController]
    public class HealthController : ControllerBase
    {
        [Route("api/Health/Index")]
        [HttpGet]
        public ActionResult Index() {
            return Ok();
        }
    }
}
