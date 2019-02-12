using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenCaseManager.Models;

namespace OpenCaseManager.Managers
{
    public class DataModelManager : IDataModelManager
    {
        public DataModel DataModel { get; set; }

        /// <summary>
        /// Get default data model
        /// </summary>
        /// <param name="sQLOperation"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void GetDefaultDataModel(Enums.SQLOperation sQLOperation, string entityName)
        {
            DataModel = new DataModel()
            {
                Type = sQLOperation.ToString(),
                Entity = entityName
            };
            if (sQLOperation == Enums.SQLOperation.SP)
                DataModel.Parameters = new List<Parameter>();
        }

        /// <summary>
        /// Add parameters to data model
        /// </summary>
        /// <param name="dataModel"></param>
        /// <param name="columnName"></param>
        /// <param name="columnType"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public void AddParameter(string columnName, Enums.ParameterType columnType, string columnValue)
        {
            if (DataModel.Parameters == null)
                DataModel.Parameters = new List<Parameter>();
            DataModel.Parameters.Add(new Parameter()
            {
                ColumnName = columnName,
                ColumnType = columnType.ToString().Remove(0, 1),
                ColumnValue = columnValue
            });
        }

        /// <summary>
        /// Add result set to data model
        /// </summary>
        /// <param name="dataModel"></param>
        /// <param name="resultSet"></param>
        /// <returns></returns>
        public void AddResultSet(List<string> resultSet)
        {
            if (DataModel.ResultSet == null)
                DataModel.ResultSet = new List<string>();
            DataModel.ResultSet = resultSet;
        }

        /// <summary>
        /// Add filter to data model
        /// </summary>
        /// <param name="dataModel"></param>
        /// <param name="columnName"></param>
        /// <param name="columnType"></param>
        /// <param name="columnValue"></param>
        /// <param name="_operator"></param>
        /// <param name="logicalOperator"></param>
        /// <returns></returns>
        public void AddFilter(string columnName, Enums.ParameterType columnType, string columnValue, Enums.CompareOperator _operator, Enums.LogicalOperator logicalOperator = Enums.LogicalOperator.none)
        {
            if (DataModel.Filters == null)
                DataModel.Filters = new List<Filter>();
            DataModel.Filters.Add(new Filter()
            {
                Column = columnName,
                ValueType = columnType.ToString().Remove(0, 1),
                Value = columnValue,
                Operator = _operator.ToString(),
                LogicalOperator = logicalOperator.ToString()
            });
        }

        /// <summary>
        /// Add order to data model
        /// </summary>
        /// <param name="dataModel"></param>
        /// <param name="columnName"></param>
        /// <param name="isDescending"></param>
        /// <returns></returns>
        public void AddOrderBy(string columnName, bool isDescending)
        {
            if (DataModel.Order == null)
                DataModel.Order = new List<Order>();
            DataModel.Order.Add(new Order()
            {
                Column = columnName,
                Descending = isDescending
            });
        }
    }
}