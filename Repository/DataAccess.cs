using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DataAccess : IDatabaseHandler
    {
        private string ConnectionString { get; set; }

        public DataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Open SQL Connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Close Open SQL Connection
        /// </summary>
        /// <param name="connection"></param>
        public void CloseConnection(IDbConnection connection)
        {
            var sqlConnection = (SqlConnection)connection;
            sqlConnection.Close();
            sqlConnection.Dispose();
        }

        /// <summary>
        /// Create Command to execute SQL Query
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new SqlCommand
            {
                CommandText = commandText,
                Connection = (SqlConnection)connection,
                CommandType = commandType
            };
        }

        /// <summary>
        /// Create Adapter to read the command data
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IDataAdapter CreateAdapter(IDbCommand command)
        {
            return new SqlDataAdapter((SqlCommand)command);
        }

        /// <summary>
        /// Create Parameter for SQL Command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(IDbCommand command)
        {
            SqlCommand SQLcommand = (SqlCommand)command;
            return SQLcommand.CreateParameter();
        }
    }
}
