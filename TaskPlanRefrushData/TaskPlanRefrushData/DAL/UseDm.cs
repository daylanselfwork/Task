using Dm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TaskPlanRefrushData.IInterface;
using TaskPlanRefrushData.Models;

namespace TaskPlanApi.DAL
{
    public class UseDm : IDBService,IDisposable
    {
        public IDbConnection DBConnection { get; set; } 
        public IDataReader DataReader { get; set; }
        public IDataAdapter DataAdapter { get; set; }       
        public IDbCommand DbCommand { get; set; }
        public IDbTransaction DbTransaction { get; set; }
        public DataSet dataSet { set; get; }
        public string ConnectionString { get; set; }      
     

        public UseDm(string ConnectionString)
        {
            //ConnectionString = "server=.;User Id=ASSIGNMENT;Pwd=helloworld123;";
            DbCommand = new DmCommand();
            DBConnection = new DmConnection(ConnectionString);
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
            if (DbTransaction==null)
            {
               DbTransaction = DBConnection.BeginTransaction() as DmTransaction;
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
        public bool UseExcuteSql(string sql, Dictionary<string, object> dic)
        {
            try
            {
                OpenDB();
                BeginTransaction();
                DbCommand.CommandText = sql;
                AddParameters(dic);
                int result = DbCommand.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception)
            {

                RollBackTransaction();
                CloseDB();
                return false;
            }
           
        }
        public DataSet ExecuteDataSet(string sql, Dictionary<string, object> dic)
        {
            OpenDB();
            DbCommand.CommandText = sql;
            AddParameters(dic);
            dataSet = new DataSet();
            DataAdapter = new DmDataAdapter(DbCommand as DmCommand);
            DataAdapter.Fill(dataSet);
            return dataSet;          
        }

        public object ExecuteScalar(string sql, Dictionary<string, object> dic)
        {
            
            try
            {
                OpenDB();
                DbCommand.CommandText = sql;
                AddParameters(dic);
                return DbCommand.ExecuteScalar();
            }
            catch (Exception)
            {               
                CloseDB();
                return null;
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
                    DbCommand.Parameters.Add(new DmParameter(item.Key, item.Value));
                }
            }
        }
    }
}