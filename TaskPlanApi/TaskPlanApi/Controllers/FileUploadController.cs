using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace TaskPlanApi.Controllers
{
    public class FileUploadController : ApiController
    {
        public FileUploadController()
        {

        }
        [HttpPost]
        public string Upload()
        {
            /*  var request = HttpContext.Current.Request;
              var file = request;*/
            string path = "~/UploadFile/";
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                HttpPostedFile file = request.Files[0];
                string[] filecombin = file.FileName.Split('.');
                if (file == null || String.IsNullOrEmpty(file.FileName) || file.ContentLength == 0 || filecombin.Length < 2)
                {
                    return "上传出错";
                }

                //项目相对路径
                string local = path.Replace("~/", "").Replace('/', '\\');
                //物理路径
                string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, local);
                //文件名称
                string saveName = file.FileName;
                //扩展名
                string extension = filecombin[filecombin.Length - 1];
                //新名称(GUID格式)
                string uName = Guid.NewGuid().ToString("N");
                //判断文件存放路径是否存在
                if (!System.IO.Directory.Exists(localPath))
                {
                    System.IO.Directory.CreateDirectory(localPath);
                }
                //文件保存路径
                string localURL = Path.Combine(local, saveName + "." + extension);
                //保存文件
                file.SaveAs(Path.Combine(localPath, saveName + "." + extension));
                return "上传成功";

            }
            catch (Exception)
            {
                return "上传失败";
            }


        }
    }
}
