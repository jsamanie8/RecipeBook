using Recipe.Data.Interfaces;
using Recipe.Helpers.Security;
using Recipe.Models_V2.Domain;
using Recipe.Models_V2.Requests.User;
using Recipe.Services_V2.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Recipe.Services_V2.Services
{
    public class UserService : IUserService
    {
        private IDataProvider _data = null;
        private Salt _salt = null;

        public UserService(IDataProvider data)
        {
            _data = data;
            _salt = new Salt();
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
        
        public int Add(UserAddRequest model)
        {
            string procName = "[dbo].[User_Insert]";
            int id = 0;
            string hashedPwd = _salt.SaltPassword(model.Password);
            model.Password = hashedPwd;

            _data.Add(procName,
                paramMapper: delegate (SqlParameterCollection col)
                {
                    AddParameters(model, col);
                    var idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;
                    col.Add(idOut);
                },
                returnParams: delegate (SqlParameterCollection returnCol)
                {
                    var oId = returnCol["@Id"].Value;
                    Int32.TryParse(oId.ToString(), out id);
                });

            return id;
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
    
        private static void AddParameters(UserAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@FirstName", model.FirstName);
            col.AddWithValue("@LastName", model.LastName);
            col.AddWithValue("@Password", model.Password);
            col.AddWithValue("@Email", model.Email);
            col.AddWithValue("@Admin", model.Admin);
        }
    }
}
