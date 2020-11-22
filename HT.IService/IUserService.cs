using System;

namespace HT.IService
{
    public interface IUserService
    {
        System.Collections.Generic.List<HT.Model.User> GetUsers();
    }
}
