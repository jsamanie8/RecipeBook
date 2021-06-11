using Recipe.Data.Interfaces;
using Recipe.Models_V2.Domain;
using Recipe.Services_V2.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Recipe.Services_V2.Services
{
    public class UserService : IUserService
    {
        private IDataProvider _data = null;

        public UserService(IDataProvider data)
        {
            _data = data;
        }

        public List<User> Get()
        {
            List<User> result = null;
            string procName = "[dbo].[User_SelectAll]";

            _data.Get(procName, map: delegate (IDataReader reader, short set)
            {
                User user = MapUser(reader);

                if (result == null)
                {
                    result = new List<User>();
                }

                result.Add(user);
            });

            return result;
        }

        private static User MapUser(IDataReader reader)
        {
            var user = new User();
            int index = 0;

            user.Id = reader.GetInt32(index++);
            user.FirstName = reader.GetString(index++);
            user.LastName = reader.GetString(index++);
            user.Password = reader.GetString(index++);
            user.Email = reader.GetString(index++);
            user.Admin = reader.GetBoolean(index++);

            return user;
        }
    }
}
