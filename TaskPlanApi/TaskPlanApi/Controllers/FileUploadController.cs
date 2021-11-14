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
        [HttpGet]
        public bool Upload()
        {
            /*  var request = HttpContext.Current.Request;
              var file = request;*/
            HttpRequest request = HttpContext.Current.Request;
            HttpFileCollection fileCollection = request.Files;
            if (fileCollection.Count>0)
            {
                HttpPostedFile file = fileCollection[0];
                var savePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "UploadFile/";
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                file.SaveAs(savePath+file.FileName);
                return true;
            }
            return false;
            
            
        }
    }
}
