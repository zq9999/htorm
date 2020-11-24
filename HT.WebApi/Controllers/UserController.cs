using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HT.IService;
using HT.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HT.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _iUserService;
        ILogger<UserController> _logger;
        public UserController(IUserService iUserService, ILogger<UserController> logger)
        {
            _iUserService = iUserService;
            _logger = logger;
        }
        [Route("GetUsers")]
        [HttpGet]
        public List<User> GetUsers() {
            _logger.LogInformation("GetUsers");
            var list=  _iUserService.GetUsers();
            return list;
        }
    }
}