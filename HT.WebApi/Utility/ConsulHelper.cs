﻿using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HT.WebApi.Utility
{
    /// <summary>
    /// 自己封装的注册类,nuget安装  Consul
    /// </summary>
    public static class ConsulHelper
    {
        public static void ConsulRegist(this IConfiguration configuration)
        {
            ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri(configuration["Consul:consulUrl"]);
                c.Datacenter = "dc1";
            });
            string ip = configuration["Consul:ip"];
            string id = configuration["Consul:id"];
            string groupName = configuration["Consul:groupName"];
            int port = int.Parse(configuration["Consul:port"]);
            //int weight = string.IsNullOrWhiteSpace(configuration["weight"]) ? 1 : int.Parse(configuration["weight"]);//命令行参数必须传入
            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "service" + id,//唯一的
                Name = groupName,//组名称-Group
                Address = ip,//其实应该写ip地址
                Port = port,//不同实例
                //Tags = new string[] { weight.ToString() },//标签
                Check = new AgentServiceCheck()//配置心跳检查的
                {
                    Interval = TimeSpan.FromSeconds(12),
                    HTTP = $"http://{ip}:{port}/Api/Health/Index",
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5)
                }
            });
            Console.WriteLine($"http://{ip}:{port}完成注册");
        }
    }
}
