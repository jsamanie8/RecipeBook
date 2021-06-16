using Recipe.Data.Interfaces;
using Recipe.Models_V2.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Recipe.Services_V2.Services
{
    public class LoginService
    {
        private IDataProvider _data = null;

        public LoginService(IDataProvider data)
        {
            _data = data;
        }

        public List<User> VerifyUser(Login model)
        {
            List<User> userLoggedIn = null;
            User user = new User();
            string procName = "[dbo].[User_SelectByEmail]";
            string hashedPassword = "";

            _data.ExecuteProc(procName, paramMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Email", model.Email);
                },
                map: delegate (IDataReader reader, short set)
                {
                    int index = 0;
                    hashedPassword = reader.GetString(3);
                    bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, hashedPassword);

                    if (isValidPassword)
                    {
                        user.Id = reader.GetInt32(index++);
                        user.FirstName = reader.GetString(index++);
                        user.LastName = reader.GetString(index++);
                        user.Password = reader.GetString(index++);
                        user.Email = reader.GetString(index++);
                        user.Admin = reader.GetBoolean(index++);
                    }
                    else
                    {
                        throw new Exception("Password is not valid!");
                    }

                    userLoggedIn.Add(user);
                });
            
            return userLoggedIn;
        }
    }
}
