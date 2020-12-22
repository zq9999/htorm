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

            #region jwt �ԳƼ���У��
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,//�Ƿ���֤Issuer
            //        ValidateAudience = true,//�Ƿ���֤Audience
            //        ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
            //        ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
            //        ValidAudience = this.Configuration["audience"],//Audience
            //        ValidIssuer = this.Configuration["issuer"],//Issuer���������ǰ��ǩ��jwt������һ��
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["SecurityKey"])),//�õ�SecurityKey
            //        //AudienceValidator = (m, n, z) =>
            //        //{
            //        //    return m != null && m.FirstOrDefault().Equals(this.Configuration["audience"]);
            //        //},//�Զ���У����򣬿����µ�¼��֮ǰ����Ч
            //    };
            //});
            #endregion

            #region jwt�ǶԳƼ���
            string path = Path.Combine(Directory.GetCurrentDirectory(), "key.public.json");
            string key = File.ReadAllText(path);//this.Configuration["SecurityKey"];
            var keyParams = JsonConvert.DeserializeObject<RSAParameters>(key);
            var credentials = new SigningCredentials(new RsaSecurityKey(keyParams), SecurityAlgorithms.RsaSha256Signature);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//�Ƿ���֤Issuer
                    ValidateAudience = true,//�Ƿ���֤Audience
                    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    ValidAudience = this.Configuration["Audience"],//Audience
                    ValidIssuer = this.Configuration["Issuer"],//Issuer���������ǰ��ǩ��jwt������һ��
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
                    //},//�Զ���У����򣬿����µ�¼��֮ǰ����Ч
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


            // ���Swagger
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
            app.UseAuthentication();//ע�������һ�䣬������֤
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

            //ע��Consul
            this.Configuration.ConsulRegist();
        }
    }
}
