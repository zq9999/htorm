using HT.DAL;
using HT.IService;
using HT.Model;
using System;
using System.Collections.Generic;

namespace HT.Service
{
    public class UserService: IUserService
    {
        SqlHelper sqlHelper = new DAL.SqlHelper();
        public List<User> GetUsers() {
            List<User> list = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                User user = new User();
                user.Name = "jack" + i;
                user.Account = "000" + i;
                list.Add(user);
            }
            return list;
        }

    }
}
