using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace TaskPlanRefrushData.Models
{
    class MyThread
    {
        public string Url { get; set; }
        public string Data { get; set; }
        public void ThreadStartMain()
        {           
            FileStream stream = new FileStream(Url, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(Data);
            writer.Close();
            stream.Close();
            writer.Dispose();
            stream.Dispose();
        }
    }
}
