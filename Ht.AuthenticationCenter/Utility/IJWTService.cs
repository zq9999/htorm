﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens; 
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt; 
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ht.AuthenticationCenter.Utility
{
    public interface IJWTService
    {
        string GetToken(string UserName);
    } 
    
}
