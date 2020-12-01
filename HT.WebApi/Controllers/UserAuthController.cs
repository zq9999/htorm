using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HT.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UserAuthController : ControllerBase
    {
        [Route("GetUsers")]
        
        public List<string> GetUsers() {
            base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");//允许跨域
            return new List<string>() { "jack","jim","wade"};
        }
        [Route("GetUsersByName")]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public dynamic  GetUsersByName(string name)
        {
            base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");//允许跨域
            var data=new { name=name,age=17,birthday=DateTime.Now.AddYears(-30) };
             return data;
        }
    }
}
