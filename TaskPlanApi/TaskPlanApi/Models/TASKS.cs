using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskPlanApi.Models
{
    public class TASKS
    {
        public int ID { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 可执行程序路径
        /// </summary>
        public string PATHS { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FILENAME{get;set;}
        /// <summary>
        /// 参数
        /// </summary>
        public string ARGUEMENTS { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        public string STARTTIME { get; set; }
        /// <summary>
        /// 几小时执行一次
        /// </summary>
        public int STARTHOURSINTERVAL { get; set; }
        /// <summary>
        /// 几分钟执行一次
        /// </summary>
        public int STARTMINUTESINTERVAL { get; set; }
        /// <summary>
        /// 几秒钟执行一次
        /// </summary>
        public int STARTSECONDSINTERVAL { get; set; }
        /// <summary>
        /// 任务创建时间
        /// </summary>
        public string CREATETIME { get; set; }
        /// <summary>
        /// 任务更新时间
        /// </summary>
        public string UPDATETIME { get; set; }
        /// <summary>
        /// 任务状态0为关闭，1为开启
        /// </summary>
        public int STATE { get; set; }
        /// <summary>
        /// 任务工作路径
        /// </summary>
        public string WORKINGDIRECOTORY { get; set; }
    }
}