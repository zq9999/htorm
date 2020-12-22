using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HT.WebApi.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace HT.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddScoped<IUserService, UserService>();

            #region jwt 对称加密校验
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,//是否验证Issuer
            //        ValidateAudience = true,//是否验证Audience
            //        ValidateLifetime = true,//是否验证失效时间
            //        ValidateIssuerSigningKey = true,//是否验证SecurityKey
            //        ValidAudience = this.Configuration["audience"],//Audience
            //        ValidIssuer = this.Configuration["issuer"],//Issuer，这两项和前面签发jwt的设置一致
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["SecurityKey"])),//拿到SecurityKey
            //        //AudienceValidator = (m, n, z) =>
            //        //{
            //        //    return m != null && m.FirstOrDefault().Equals(this.Configuration["audience"]);
            //        //},//自定义校验规则，可以新登录后将之前的无效
            //    };
            //});
            #endregion

            #region jwt非对称加密
            string path = Path.Combine(Directory.GetCurrentDirectory(), "key.public.json");
            string key = File.ReadAllText(path);//this.Configuration["SecurityKey"];
            var keyParams = JsonConvert.DeserializeObject<RSAParameters>(key);
            var credentials = new SigningCredentials(new RsaSecurityKey(keyParams), SecurityAlgorithms.RsaSha256Signature);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = this.Configuration["Audience"],//Audience
                    ValidIssuer = this.Configuration["Issuer"],//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new RsaSecurityKey(keyParams),
                    
                    //IssuerSigningKeyValidator = (m, n, z) =>
                    // {
                    //     Console.WriteLine("This is IssuerValidator");
                    //     return true;
                    // },
                    //IssuerValidator = (m, n, z) =>
                    // {
                    //     Console.WriteLine("This is IssuerValidator");
                    //     return "http://localhost:5726";
                    // },
                    //AudienceValidator = (m, n, z) =>
                    //{
                    //    Console.WriteLine("This is AudienceValidator");
                    //    return true;
                    //    //return m != null && m.FirstOrDefault().Equals(this.Configuration["Audience"]);
                    //},//自定义校验规则，可以新登录后将之前的无效
                };
            });
            #endregion
            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin", builder =>
                {
                    builder.WithOrigins(
                        "http://localhost:5000",
                        "http://localhost:5001",
                        "http://localhost:5002",
                        "http://localhost:5003")
                        .WithHeaders("Origin", "X-Requested-With", "Content-Type", "Accept", "x-token", "Authorization")
                        .WithMethods("POST", "GET", "PUT", "OPTIONS", "DELETE");
                    
                });
            });


            // 添加Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Demo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            #region jwt
            app.UseAuthentication();//注意添加这一句，启用验证
            #endregion

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Demo v1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AnyOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //注册Consul
            this.Configuration.ConsulRegist();
        }
    }
}
