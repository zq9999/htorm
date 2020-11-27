using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HT.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        ILogger<UserController> _logger;
        public UserController( ILogger<UserController> logger)
        { 
            _logger = logger;
        }
        //[Route("GetUsers")]
        //[HttpGet]
        //public List<User> GetUsers() {
        //    _logger.LogInformation("GetUsers");
        //    var list=  _iUserService.GetUsers();
        //    return list;
        //}
        [HttpGet]
        public List<string> Index() {
            base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");//允许跨域
            _logger.LogInformation($"{DateTime.Now} index");
            return new List<string>() {"jack","wade","jim" };
        }
    }
}