﻿using Dm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TaskPlanApi.DAL;
using TaskPlanApi.Models;

namespace TaskPlanApi.Controllers
{
    public class DataController : ApiController
    {
        private string Sign;       
       /* private IDbDataParameter DbDataParameter;*/
        public DataController()
        {
            ReadDbConfig();
        }
        // GET: api/Default
        [HttpGet]
        public IEnumerable<TASKS> Query()
        {
            string sql = "select * from TASKS";               
            List<TASKS> tasks = DBHelper.ExecuteDataSet(sql,null);
            return tasks;
        }
        [HttpGet]
        // GET: api/Default/5
        public TASKS Query(int id)
        {
            string sql = "select * from TASKS where ID = " + Sign + "ID";        
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters[Sign + "ID"] = id;
            try
            {
                TASKS task = DBHelper.ExecuteDataSet(sql, parameters)[0];
                return task;
            }
            catch (Exception)
            {

                return null;
            }
            
           
        }    
        [HttpGet]
        public bool Add(string NAME, string PATHS, string ARGUEMENTS, string STARTTIME,
          string STARTHOURSINTERVAL, string STARTMINUTESINTERVAL, string STARTSECONDSINTERVAL, string STATE,
          string WORKINGDIRECOTORY
           )
        {
            string sql = "insert into TASKS(NAME,PATHS,FILENAME,ARGUEMENTS," +
                "STARTTIME,STARTHOURSINTERVAL,STARTMINUTESINTERVAL," +
                "STARTSECONDSINTERVAL,CREATETIME,STATE,WORKINGDIRECOTORY) " +
                "values(" + Sign + "NAME," + Sign + "PATHS," + Sign +
                "FILENAME," + Sign + "ARGUEMENTS," + Sign + "STARTTIME," + Sign
                + "STARTHOURSINTERVAL," + Sign + "STARTMINUTESINTERVAL," + Sign + "STARTSECONDSINTERVAL," +Sign+
                 "CREATETIME," + Sign + "STATE," + Sign + "WORKINGDIRECOTORY" +
                ")";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                [Sign + "NAME"] = NAME,
                [Sign + "PATHS"] = PATHS,
                [Sign + "FILENAME"] = Path.GetFileName(PATHS),
                [Sign + "ARGUEMENTS"] = ARGUEMENTS,
                [Sign + "STARTTIME"] = Convert.ToDateTime(STARTTIME),
                [Sign + "STATE"] = STATE,
                [Sign + "STARTHOURSINTERVAL"] = STARTHOURSINTERVAL,
                [Sign + "STARTMINUTESINTERVAL"] = STARTMINUTESINTERVAL,
                [Sign + "STARTSECONDSINTERVAL"] = STARTSECONDSINTERVAL,
                [Sign + "CREATETIME"] = DateTime.Now,
                [Sign + "WORKINGDIRECOTORY"] = Path.GetDirectoryName(WORKINGDIRECOTORY)

            };
            bool result = DBHelper.UseExecuteSql(sql, parameters);
            return result;
        }        
        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="tasks"></param>
        [HttpGet]
        public bool Update(string NAME, string PATHS,string ARGUEMENTS, string STARTTIME,string STARTHOURSINTERVAL,
            string STARTMINUTESINTERVAL,string STARTSECONDSINTERVAL,string STATE,string WORKINGDIRECOTORY,string ID)
        {
            string sql = "update TASKS set NAME=+"+Sign+ "NAME,PATHS=" + Sign + 
                "PATHS,FILENAME=" + Sign + "FILENAME," + Sign + "ARGUEMENTS,STARTTIME =" 
                + Sign + "STARTTIME,STARTHOURSINTERVAL=" + Sign +
                "STARTHOURSINTERVAL,STARTMINUTESINTERVAL=" + Sign +
                "STARTMINUTESINTERVAL,STARTSECONDSINTERVAL=" + Sign + 
                "STARTSECONDSINTERVAL,UPDATETIME=" + Sign + 
                "UPDATETIME,STATE=" + Sign + "STATE,WORKINGDIRECOTORY=" +
                Sign + "WORKINGDIRECOTORY where ID = " + Sign + "ID,";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                [Sign + "NAME"] = NAME,
                [Sign + "PATHS"] = PATHS,
                [Sign + "FILENAME"] = Path.GetFileName(PATHS),
                [Sign + "ARGUEMENTS"] = ARGUEMENTS,
                [Sign + "STARTTIME"] = Convert.ToDateTime(STARTTIME),
                [Sign + "STARTHOURSINTERVAL"] = Convert.ToInt32(STARTHOURSINTERVAL),
                [Sign + "STARTMINUTESINTERVAL"] = Convert.ToInt32(STARTMINUTESINTERVAL),
                [Sign + "STARTSECONDSINTERVAL"] = Convert.ToInt32(STARTSECONDSINTERVAL),
                [Sign + "UPDATETIME"] = DateTime.Now,
                [Sign + "STATE"] = Convert.ToInt32(STATE),
                [Sign + "WORKINGDIRECOTORY"] = WORKINGDIRECOTORY,
                [Sign + "ID"] = Convert.ToInt32(ID),
            };            
            bool result = DBHelper.UseExecuteSql(sql,parameters);           
            return result;
        }

        [HttpGet]
        public bool Delete(int id)
        {
            string sql = "delete from TASKS where ID="+Sign+"ID";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                [Sign + "ID"] = id
            };            
          
            bool result = DBHelper.UseExecuteSql(sql,parameters);
            return result;
        }
        private void ReadDbConfig()
        {
            string provider = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectStr"].ProviderName;

            if (provider.Trim().Contains("SqlClient"))
            {
                Sign = "@";
                
            }
            else if (provider.Trim().Contains("Dm"))
            {
                Sign = ":";
               
            }
            else
            {
                throw new Exception("没有配置SqlServer或Dm数据库");
            }
        }
    }
}
