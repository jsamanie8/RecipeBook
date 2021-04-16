using Recipe.Data.Interfaces;
using Recipe.Models_V2.Domain;
using Recipe.Services_V2.Interfaces;
using System;
using System.Collections.Generic;
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

        public Owner Get()
        {
            List<Owner> result = null;
            string procName = "[dbo].[Owner_SelectAll]";

            _data.Get(procName);

            return null;
        }
    }
}
