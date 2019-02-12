using OpenCaseManager.Models;
using System.Data;

namespace OpenCaseManager.Managers
{
    public interface IManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        DataTable SelectData(DataModel dataModel);
        DataTable UpdateData(DataModel dataModel);
        DataTable InsertData(DataModel dataModel);
        DataTable ExecuteStoreProcedure(DataModel dataModel);
    }
}
