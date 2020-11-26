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
    public class UserController : ControllerBase
    {
         
     
        //[Route("GetUsers")]
        //[HttpGet]
        //public List<User> GetUsers() {
        //    var list=  _iUserService.GetUsers();
        //    return list;
        //}

        public List<string> GetNames() {
            List<string> list = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                list.Add("james"+i);
            }
            return list;
        }
    }
}