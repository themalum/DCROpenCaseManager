using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDBManager
    {
        IDbConnection GetDatabaseConnection();
        void CloseConnection(IDbConnection connection);
        DataTable SelectData(string command);
        DataTable InsertData(string command);
        DataTable UpdateData(string command);
        DataTable ExecuteStoreProcedure(string commandText, Dictionary<string, dynamic> parameters);
    }
}
