using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DbManager : IDBManager
    {
        private IDatabaseHandler _database;

        public DbManager(IDatabaseHandler database)
        {
            _database = database;
        }

        /// <summary>
        /// Get database open connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetDatabaseConnection()
        {
            return _database.CreateConnection();
        }

        /// <summary>
        /// Close opened sql connectino
        /// </summary>
        /// <param name="connection"></param>
        public void CloseConnection(IDbConnection connection)
        {
            _database.CloseConnection(connection);
        }

        /// <summary>
        /// Select data from database
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataTable SelectData(string commandText)
        {
            using (var connection = _database.CreateConnection())
            {
                connection.Open();

                using (var command = _database.CreateCommand(commandText, CommandType.Text, connection))
                {
                    var dataset = new DataSet();
                    var dataAdaper = _database.CreateAdapter(command);
                    dataAdaper.Fill(dataset);
                    CloseConnection(connection);
                    if (dataset.Tables.Count > 0)
                        return dataset.Tables[0];
                    else
                        return new DataTable();
                }
            }
        }

        /// <summary>
        /// Insert data to database
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataTable InsertData(string commandText)
        {
            using (var connection = _database.CreateConnection())
            {
                connection.Open();

                using (var command = _database.CreateCommand(commandText, CommandType.Text, connection))
                {
                    var dataset = new DataSet();
                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                    command.CommandText = "SELECT SCOPE_IDENTITY() as Id";
                    var dataAdaper = _database.CreateAdapter(command);
                    dataAdaper.Fill(dataset);
                    CloseConnection(connection);

                    if (dataset.Tables.Count > 0)
                        return dataset.Tables[0];
                    else
                        return new DataTable();
                }
            }
        }

        /// <summary>
        /// Update data to database
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataTable UpdateData(string commandText)
        {
            using (var connection = _database.CreateConnection())
            {
                connection.Open();

                using (var command = _database.CreateCommand(commandText, CommandType.Text, connection))
                {
                    var dataset = new DataSet();
                    command.ExecuteNonQuery();
                    CloseConnection(connection);
                    return new DataTable();
                }
            }
        }

        /// <summary>
        /// Execute SQL Stored Procedure
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable ExecuteStoreProcedure(string commandText, Dictionary<string, dynamic> parameters)
        {
            using (var connection = _database.CreateConnection())
            {
                connection.Open();

                using (var command = _database.CreateCommand(commandText, CommandType.StoredProcedure, connection))
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(new SqlParameter("@" + parameter.Key, parameter.Value));
                    }

                    var dataset = new DataSet();
                    var dataAdaper = _database.CreateAdapter(command);
                    dataAdaper.Fill(dataset);
                    CloseConnection(connection);

                    if (dataset.Tables.Count > 0)
                        return dataset.Tables[0];
                    else
                        return new DataTable();
                }
            }
        }
    }
}
