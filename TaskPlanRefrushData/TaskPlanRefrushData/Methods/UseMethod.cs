using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskPlanApi.DAL;
using TaskPlanRefrushData.Models;

namespace TaskPlanRefrushData.Methods
{
    class UseMethod
    {
        private string Sign;
        public UseMethod()
        {
            ReadDbConfig();
        }
        private void ReadDbConfig()
        {
            string provider = ConfigurationManager.ConnectionStrings["ConnectStr"].ProviderName;

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
       
        /// <summary>
        /// 获取正在执行的任务
        /// </summary>
        public void ExecuteQuery()
        {
            string sql = "select * from TASKS where STATE="+Sign+"STATE";
            Dictionary<string, object> dic = new Dictionary<string, object>()
            {
                [Sign + "STATE"] = 1
            };
            List<TASKS> tasks = DBHelper.ExecuteDataSet(sql, dic);

            foreach (var item in tasks)
            {
                if (item.STARTTIME!=null)
                {
                    ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(RushTaskTime);
                    Thread thread = new Thread(parameterizedThreadStart);
                    thread.Start(item);
                    thread.Join();
                    
                }
                else
                {
                    if (item.STARTHOURSINTERVAL != 0&&item.STARTSECONDSINTERVAL!=0&&item.STARTMINUTESINTERVAL!=0)
                    {
                        
                    }
                }
                
                
            }
        }
        /// <summary>
        /// 通过线程来创建计时器，计时器每隔一秒钟调用一次方法
        /// </summary>
        /// <param name="tasks"></param>
        public void RushTaskTime(object tasks)
        {
           
            Timer timer = new Timer(new TimerCallback(ExecuteThreading), tasks, 0, 1000);      
            ExecuteThreading(tasks);
        }
        public void ExecuteThreading(object tasks)
        {
            TASKS task = tasks as TASKS;
            if (DateTime.Now.ToString().Equals(task.STARTTIME.ToString()))
            {
                ExecuteProcess(task);
            }
        }
        /// <summary>
        /// 使用进程执行任务
        /// </summary>
        public void ExecuteProcess(object tasks)
        {
            string url = ConfigurationManager.AppSettings["ProcessUrl"];
            TASKS task = tasks as TASKS;
            string strArgument =task.NAME + " "+task.FILENAME+" "+ task.PATHS+" "+ task.ARGUEMENTS;
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = task.FILENAME,
                Arguments = strArgument,
                WorkingDirectory = task.WORKINGDIRECOTORY,
                CreateNoWindow = false

            };
            process.StartInfo = startInfo;
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.ErrorDataReceived += Process_ErrorDataReceived;
            process.OutputDataReceived += Process_OutputDataReceived;
            

        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            MyThread myThread = new MyThread();
            myThread.Url = "../ProcessConsolelog.txt";
            myThread.Data = e.Data;
            Thread thread = new Thread(new ThreadStart(myThread.ThreadStartMain));
            thread.Start();
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            MyThread myThread = new MyThread();
            myThread.Url = "../ProcessErrorlog.txt";
            myThread.Data = e.Data;
            Thread thread = new Thread(new ThreadStart(myThread.ThreadStartMain));
            thread.Start();
        }
        
       
    }
}
