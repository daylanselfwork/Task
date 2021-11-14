using Dm;
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
    public class TestController : ApiController
    {
        private string Sign;       
       /* private IDbDataParameter DbDataParameter;*/
        public TestController()
        {
            ReadDbConfig();
        }
        // GET: api/Default
        public IEnumerable<TASKS> Get()
        {
            string sql = "select * from TASKS";               
            List<TASKS> tasks = DBHelper.ExecuteDataSet(sql,null);
            return tasks;
        }

        // GET: api/Default/5
        public IEnumerable<TASKS> Get(int id)
        {
            string sql = "select * from TASKS where ID = " + Sign + "ID";        
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters[Sign + "ID"] = id;           
            List<TASKS> task = DBHelper.ExecuteDataSet(sql,parameters);
            return task;
        }
        public bool Post(string NAME, string PATHS, string ARGUEMENTS, string STARTTIME,
          string STARTHOURSINTERVAL, string STARTMINUTESINTERVAL, string STARTSECONDSINTERVAL, string STATE,
          string WORKINGDIRECOTORY
           )
        {
            string sql = "insert into TASKS(NAME,PATHS,FILENAME,ARGUEMENTS," +
                "STARTTIME,STARTHOURSINTERVAL,STARTMINUTESINTERVAL," +
                "STARTSECONDSINTERVAL,CREATETIME,WORKINGDIRECOTORY) " +
                "values(" + Sign + "NAME," + "+" + Sign + "+PATHS," + Sign +
                "FILENAME" + Sign + "ARGUEMENTS" + Sign + "STARTTIME" + Sign
                + "STARTHOURSINTERVAL" + Sign + "STARTMINUTESINTERVAL" + Sign + "STARTSECONDSINTERVAL" +
                 "CREATETIME" + Sign + "WORKINGDIRECOTORY" +
                ")";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                [Sign + "NAME"] = NAME,
                [Sign + "PATHS"] = PATHS,
                [Sign + "FILENAME"] = Path.GetFileName(PATHS),
                [Sign + "ARGUEMENTS"] = ARGUEMENTS,
                [Sign + "STARTTIME"] = STARTTIME,
                [Sign + "STARTHOURSINTERVAL"] = STARTHOURSINTERVAL,
                [Sign + "STARTMINUTESINTERVAL"] = STARTMINUTESINTERVAL,
                [Sign + "STARTSECONDSINTERVAL"] = STARTSECONDSINTERVAL,
                [Sign + "CREATETIME"] = WORKINGDIRECOTORY,
            };
            bool result = DBHelper.UseExecuteSql(sql, parameters);
            if (result)
            {
                var request = HttpContext.Current.Request;
                var file = request.Files[0];
                var savePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "UploadFile/";
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                request.SaveAs(savePath + file.FileName, false);
            }
            return result;
        }
        // POST: api/Default
        public bool Post(TASKS tasks)
        {
            string sql = "insert into TASKS(NAME,PATHS,FILENAME,ARGUEMENTS," +
                "STARTTIME,STARTHOURSINTERVAL,STARTMINUTESINTERVAL," +
                "STARTSECONDSINTERVAL,CREATETIME,WORKINGDIRECOTORY) " +
                "values("+Sign+ "NAME," + "+"+Sign+ "+PATHS," + Sign+ 
                "FILENAME"+ Sign + "ARGUEMENTS" + Sign + "STARTTIME" + Sign
                + "STARTHOURSINTERVAL" + Sign + "STARTMINUTESINTERVAL" + Sign + "STARTSECONDSINTERVAL" +
                 "CREATETIME" + Sign + "WORKINGDIRECOTORY" + 
                ")";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                [Sign+ "NAME"] = tasks.NAME,
                [Sign+ "PATHS"] = tasks.PATHS,
                [Sign+ "FILENAME"] = tasks.FILENAME,
                [Sign+ "ARGUEMENTS"] = tasks.ARGUEMENTS,
                [Sign+ "STARTTIME"] = tasks.STARTTIME,
                [Sign+ "STARTHOURSINTERVAL"] = tasks.STARTHOURSINTERVAL,
                [Sign+ "STARTMINUTESINTERVAL"] = tasks.STARTMINUTESINTERVAL,
                [Sign+ "STARTSECONDSINTERVAL"] = tasks.STARTSECONDSINTERVAL,
                [Sign+ "CREATETIME"] = tasks.WORKINGDIRECOTORY,
            };           
            bool result = DBHelper.UseExecuteSql(sql,parameters);
            if (result)
            {
                var request = HttpContext.Current.Request;
                var file = request.Files[0];
                var savePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "UploadFile/";
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                request.SaveAs(savePath + file.FileName, false);
            }
            return result;
        }
        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="tasks"></param>
        // PUT: api/Default/5
        public bool Put(TASKS tasks)
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
                [Sign + "NAME"] = tasks.NAME,
                [Sign + "PATHS"] = tasks.PATHS,
                [Sign + "FILENAME"] = tasks.FILENAME,
                [Sign + "ARGUEMENTS"] = tasks.ARGUEMENTS,
                [Sign + "STARTTIME"] = tasks.STARTTIME,
                [Sign + "STARTHOURSINTERVAL"] = tasks.STARTHOURSINTERVAL,
                [Sign + "STARTMINUTESINTERVAL"] = tasks.STARTMINUTESINTERVAL,
                [Sign + "STARTSECONDSINTERVAL"] = tasks.STARTSECONDSINTERVAL,
                [Sign + "UPDATETIME"] = tasks.UPDATETIME,
                [Sign + "STATE"] = tasks.STATE,
                [Sign + "WORKINGDIRECOTORY"] = tasks.WORKINGDIRECOTORY,
                [Sign + "ID"] = tasks.ID,
            };            
            bool result = DBHelper.UseExecuteSql(sql,parameters);           
            return result;
        }

        // DELETE: api/Default/5
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
