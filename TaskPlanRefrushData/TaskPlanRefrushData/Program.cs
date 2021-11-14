using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskPlanRefrushData.Methods;
using TaskPlanRefrushData.Models;

namespace TaskPlanRefrushData
{
    class Program
    {
       
        static void Main(string[] args)
        {
            //每个五秒钟连接一次数据库，无延时
            Timer timer = new Timer(new TimerCallback(ExecuteRefrushData),null,0,5000);
        }
        public static void  ExecuteRefrushData(object state)
        {
            UseMethod useMethod = new UseMethod();
            useMethod.ExecuteQuery();
        }
       
    }
}
