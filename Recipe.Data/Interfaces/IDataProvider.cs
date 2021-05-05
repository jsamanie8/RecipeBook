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
        int Add(string storedProc, Action<SqlParameterCollection> paramMapper, Action<SqlParameterCollection> returnParams = null);
        int Update(string storedProc, Action<SqlParameterCollection> paramMapper);
    }
}
