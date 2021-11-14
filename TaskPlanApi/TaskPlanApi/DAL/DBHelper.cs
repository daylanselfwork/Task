using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using TaskPlanApi.IInterface;
using TaskPlanApi.Models;

namespace TaskPlanApi.DAL
{
    public class DBHelper
    {
        public static IDBService Database;      
        
        
        /// <summary>
        /// 读取自定义配置，判断实例化哪种数据库
        /// </summary>     
        public static void ReadDbConfig()
        {
            try
            {
                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectStr"].ConnectionString;
                string provider = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectStr"].ProviderName;
                if (provider.Trim().Contains("SqlClient"))
                {
                    Database = new UseSqlServer(connStr);
                }
                else if (provider.Trim().Contains("Dm"))
                {
                    Database = new UseDm(connStr);
                }
                else
                {
                    throw new Exception("没有配置SqlServer或Dm数据库");
                }

            }
            catch (Exception)
            {

                throw new Exception("配置文件读取错误");
            }
        }
        /// <summary>
        /// 执行添加修改删除语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool UseExecuteSql(string sql, Dictionary<string, object> dic)
        {
            ReadDbConfig();
            return Database.UseExcuteSql(sql,dic);
        }
        /// <summary>
        /// 执行查询语句,返回第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static TASKS ExecuteScalar(string sql, Dictionary<string, object> dic)
        {
            ReadDbConfig();
            object obj = Database.ExecuteScalar(sql,dic);
            return obj as TASKS;
        }
        /// <summary>
        /// 执行查询语句，并将结果填充到DataSet数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static List<TASKS> ExecuteDataSet(string sql, Dictionary<string, object> dic)
        {
            ReadDbConfig();
            DataTable data = Database.ExecuteDataSet(sql,dic).Tables[0];
            List<TASKS> task = new List<TASKS>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                TASKS tasks = new TASKS();
                tasks.ID = Convert.ToInt32(data.Rows[i]["ID"]);
                tasks.NAME = Convert.ToString(data.Rows[i]["NAME"]);
                tasks.PATHS = Convert.ToString(data.Rows[i]["PATHS"]);
                tasks.FILENAME = Convert.ToString(data.Rows[i]["FILENAME"]);
                tasks.ARGUEMENTS = Convert.ToString(data.Rows[i]["ARGUEMENTS"]);
                tasks.STARTTIME = Convert.ToString(data.Rows[i]["STARTTIME"]);
                tasks.STARTHOURSINTERVAL = Convert.ToInt32(data.Rows[i]["STARTHOURSINTERVAL"]);
                tasks.STARTMINUTESINTERVAL = Convert.ToInt32(data.Rows[i]["STARTMINUTESINTERVAL"]);
                tasks.STARTSECONDSINTERVAL = Convert.ToInt32(data.Rows[i]["STARTSECONDSINTERVAL"]);
                tasks.CREATETIME = Convert.ToString(data.Rows[i]["CREATETIME"]);
                tasks.UPDATETIME = Convert.ToString(data.Rows[i]["UPDATETIME"]);
                tasks.STATE = Convert.ToInt32(data.Rows[i]["STATE"]);
                tasks.WORKINGDIRECOTORY = Convert.ToString(data.Rows[i]["WORKINGDIRECOTORY"]);
                task.Add(tasks);
            }
            return task;
        }       
      

    }
}