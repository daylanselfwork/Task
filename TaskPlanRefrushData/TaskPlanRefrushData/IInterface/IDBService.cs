using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanRefrushData.IInterface
{
    public interface IDBService
    {
        IDbConnection DBConnection { get; set; }
        IDataReader DataReader { get; set; }
        IDataAdapter DataAdapter { get; set; }
        IDbCommand DbCommand { get; set; }
        IDbTransaction DbTransaction { get; set; }

        DataSet dataSet { get; set; }

        string ConnectionString { get; set; }
        void BeginTransaction();
        void RollBackTransaction();
        void CommitTransaction();
        void OpenDB();
        void CloseDB();
        bool UseExcuteSql(string sql, Dictionary<string, object> dic);
        object ExecuteScalar(string sql, Dictionary<string, object> dic);
        DataSet ExecuteDataSet(string sql, Dictionary<string, object> dic);
        void AddParameters(Dictionary<string, object> dic);
        void Dispose();

    }
}
