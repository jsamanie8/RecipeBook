using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Recipe.Data.Interfaces
{
    public interface IDataProvider
    {
        void Get(string storedProc, Action<IDataReader, short> map);
    }
}
