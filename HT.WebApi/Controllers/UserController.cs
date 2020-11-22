using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HT.IService;
using HT.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HT.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _iUserService;
        public UserController(IUserService iUserService)
        {
            _iUserService = iUserService;
        }
        [Route("GetUsers")]
        [HttpGet]
        public List<User> GetUsers() {
            var list=  _iUserService.GetUsers();
            return list;
        }
    }
}