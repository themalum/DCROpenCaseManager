using OpenCaseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCaseManager.Managers
{
    public interface IDataModelManager
    {
        DataModel DataModel { get; set; }

        /// <summary>
        /// Get default data model
        /// </summary>
        /// <param name="sQLOperation"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        void GetDefaultDataModel(Enums.SQLOperation sQLOperation, string entityName);

        void AddResultSet(List<string> resultSet);

        void AddParameter(string columnName, Enums.ParameterType columnType, string columnValue);

        void AddFilter(string columnName, Enums.ParameterType columnType, string columnValue, Enums.CompareOperator _operator, Enums.LogicalOperator logicalOperator = 0);

        void AddOrderBy(string columnName, bool isDescending);
    }
}
