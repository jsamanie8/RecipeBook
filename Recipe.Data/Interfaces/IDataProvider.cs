using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Recipe.Data.Interfaces
{
    public interface IDataProvider
    {
        void Get(string storedProc, Action<IDataReader, short> map);
        //Add is a WIP. TODO
        int Add(string storedProc, Action<SqlParameterCollection> paramMapper, Action<SqlParameterCollection> returnParams = null);
    }
}
