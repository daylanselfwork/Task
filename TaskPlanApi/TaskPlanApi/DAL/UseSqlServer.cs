using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TaskPlanApi.IInterface;

namespace TaskPlanApi.DAL
{
    public class UseSqlServer : IDBService,IDisposable
    {
        public IDbConnection DBConnection { get; set; }
        public IDataReader DataReader { get; set; }
        public IDataAdapter DataAdapter { get; set; }      
        public IDbCommand DbCommand { get; set; }
        public IDbTransaction DbTransaction { get; set; }
        public DataSet dataSet { get; set; }
        public string ConnectionString { get; set; }      
       

        public UseSqlServer(string ConnectionString)
        {
            //ConnectionString = "server=.;User Id=sa;Pwd=helloworld123;";
            DbCommand = new SqlCommand();
            DBConnection = new SqlConnection(ConnectionString);
            DbCommand.Connection = DBConnection;
        }
       
        public void OpenDB()
        {
            if (DBConnection.State == ConnectionState.Closed)
            {
                DBConnection.Open();
            }
            if (DBConnection.State == ConnectionState.Broken)
            {
                DBConnection.Close();
                DBConnection.Open();
            }
        }

        public void CloseDB()
        {
            DBConnection.Close();
            DbCommand = null;
            DbTransaction = null;
            DBConnection = null;
            
            DataReader = null;
            DataAdapter = null;
            
        }
        public void BeginTransaction()
        {
            if (DbTransaction == null)
            {
                DbTransaction = DBConnection.BeginTransaction() as SqlTransaction;
            }
            DbCommand.Transaction = DbTransaction;
        }
        public void CommitTransaction()
        {
            DbTransaction.Commit();
        }
        public void RollBackTransaction()
        {
            DbTransaction.Rollback();
        }
        public DataSet ExecuteDataSet(string sql, Dictionary<string, object> dic)
        {
            try
            {
                OpenDB();
                BeginTransaction();
                DbCommand.CommandText = sql;
                AddParameters(dic);
                dataSet = new DataSet();
                DataAdapter = new SqlDataAdapter(DbCommand as SqlCommand);
                DataAdapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception)
            {

                RollBackTransaction();
                CloseDB();
                return null;

            }

        }

        public object ExecuteScalar(string sql, Dictionary<string, object> dic)
        {

            try
            {
                OpenDB();
                BeginTransaction();
                DbCommand.CommandText = sql;
                AddParameters(dic);
                return DbCommand.ExecuteScalar();
            }
            catch (Exception)
            {

                RollBackTransaction();
                CloseDB();
                return null;
            }
        }

       
       

        public bool UseExcuteSql(string sql, Dictionary<string, object> dic)
        {
            try
            {
                OpenDB();
                BeginTransaction();
                DbCommand.CommandText = sql;
                AddParameters(dic);
                int result = DbCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    CommitTransaction();
                    return result > 0;
                }
                else
                {
                    RollBackTransaction();
                    return false;
                }

            }
            catch (Exception)
            {

                RollBackTransaction();
                CloseDB();
                return false;
            }
        }      
            
        public void Dispose()
        {
            CloseDB();
        }

        public void AddParameters(Dictionary<string, object> dic)
        {
            if (dic != null)
            {
                foreach (var item in dic)
                {
                    DbCommand.Parameters.Add(new SqlParameter(item.Key, item.Value));
                }
            }
        }
    }
}