using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Ht.AuthenticationCenter.Utility
{
    public class JWTRSService : IJWTService
    {
        private readonly IConfiguration _configuration;
        IWebHostEnvironment _env;
        public JWTRSService(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public string GetToken(string UserName)
        {
            var claims = new[]
            {
                   new Claim(ClaimTypes.Name, UserName), 
                   //new Claim(ClaimTypes.Role,userModel.Role),
                   //new Claim("Role", userModel.Role),//这个不能角色授权
                   new Claim("InfoJson","{userName:'wade',age:12}")//各种信息拼装
            };

            //string keyDir = Directory.GetCurrentDirectory();
            //if (RSAHelper.TryGetKeyParameters(keyDir, true, out RSAParameters keyParams) == false)
            //{
            //    keyParams = RSAHelper.GenerateAndSaveKey(keyDir);
            //}
           var keyPath=  Path.Combine(_env.ContentRootPath, "key.private.json");
           var key= File.ReadAllText(keyPath);
            //var rsParam= JsonConvert.DeserializeObject<RSAParameters>(_configuration["SecurityKey"]);
            var rsParam = JsonConvert.DeserializeObject<RSAParameters>(key);
            var credentials = new SigningCredentials(new RsaSecurityKey(rsParam), SecurityAlgorithms.RsaSha256Signature);

            var token = new JwtSecurityToken(
               issuer: _configuration["issuer"],
               audience: _configuration["audience"],
               claims: claims,
               expires: DateTime.Now.AddMinutes(60),//5分钟有效期
               signingCredentials: credentials);
            var handler = new JwtSecurityTokenHandler();
            string tokenString = handler.WriteToken(token);
            return tokenString;
        }
    }
}
