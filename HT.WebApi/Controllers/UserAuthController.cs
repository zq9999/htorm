using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HT.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UserAuthController : ControllerBase
    {
        public UserAuthController()
        {
            int a = 1;
        }

        [HttpGet]
        [Route("GetUsers")] 
        public List<string> GetUsers() {
            //base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");//允许跨域

            var nickName = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(c => c.Type.Equals("NickName"))?.Value;
            Console.WriteLine($"This is GetUserByName 校验 {nickName}");

            var role = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
            Console.WriteLine($"This is role 校验 {role}"); 

            return new List<string>() { "jack","jim","wade"};
        }

        [HttpGet]
        [Route("GetUsersByName")]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public dynamic  GetUsersByName(string name)
        {
            //base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");//允许跨域
            var data=new { name=name,age=17,birthday=DateTime.Now.AddYears(-30) };
             return data;
        }
    }
}
