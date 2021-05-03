using Recipe.Data.Interfaces;
using Recipe.Models_V2.Domain;
using Recipe.Models_V2.Requests.Owner;
using Recipe.Services_V2.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Recipe.Services_V2.Services
{
    public class OwnerService : IOwnerService
    {
        private IDataProvider _data = null;

        public OwnerService(IDataProvider data)
        {
            _data = data;
        }

        public List<Owner> Get()
        {
            List<Owner> result = null;
            string procName = "[dbo].[Owner_SelectAll]";
            
            

            _data.Get(procName, map: delegate (IDataReader reader, short set) 
            {
                Owner owner = MapOwner(reader);

                if (result == null)
                {
                    result = new List<Owner>();
                }

                result.Add(owner);
            });

            return result;
        }

        public int Add(OwnerAddRequest model)
        {
            //Pass in OwnerAddRequestModel and a userId to record in db. TODO
            string procName = "[dbo].[Owner_Insert]";
            int id = 0;

            _data.Add(procName,
                paramMapper: delegate (SqlParameterCollection col)
                {
                    AddParameters(model, col);
                    var idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;
                    col.Add(idOut);
                },
                returnParams: delegate (SqlParameterCollection returnColl)
                {
                    var oId = returnColl["@Id"].Value;
                    Int32.TryParse(oId.ToString(), out id);
                });

            return id;
        }

        private static Owner MapOwner(IDataReader reader)
        {
            var owner = new Owner();
            int index = 0;

            owner.Id = reader.GetInt32(index++);
            owner.FirstName = reader.GetString(index++);
            owner.LastName = reader.GetString(index++);
            owner.Password = reader.GetString(index++);
            owner.Email = reader.GetString(index++);

            return owner;
        }

        private static void AddParameters(OwnerAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@FirstName", model.FirstName);
            col.AddWithValue("@LastName", model.LastName);
            col.AddWithValue("@Password", model.Password);
            col.AddWithValue("@Email", model.Email);
        }
    }
}
