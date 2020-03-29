using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public interface ISP_Call : IDisposable
    {
        IEnumerable<T> ReturnList<T>(string procedtureName , DynamicParameters param = null);

        void ExecuteWithoutReturn(string procedtureName, DynamicParameters param = null);
       
        T ExecuteReturnScalar<T>(string procedtureName, DynamicParameters param = null);
    }
}
