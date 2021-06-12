using Recipe.Models_V2.Domain;
using Recipe.Models_V2.Requests.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Services_V2.Interfaces
{
    public interface IUserService
    {
        List<User> Get();
        int Add(UserAddRequest model);
    }
}
