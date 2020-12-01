using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ht.AuthenticationCenter.Utility;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ht.AuthenticationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class AuthenticationController : ControllerBase
    {
        IJWTService _iJWTService;
        public AuthenticationController(IJWTService iJWTService) {
            _iJWTService = iJWTService;
        }
        [Route("Login")]
        [HttpPost]
        public string Login(string name, string password)
        {
            if ("zq".Equals(name) && "123321".Equals(password))//应该数据库
            {
                string token = this._iJWTService.GetToken(name);
                return JsonConvert.SerializeObject(new
                {
                    result = true,
                    token
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    result = false,
                    token = ""
                });
            }
        }
    }
}
