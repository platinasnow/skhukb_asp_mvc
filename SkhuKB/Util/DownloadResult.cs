using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkhuKB.Util
{
    public class DownloadResult : ActionResult
    {
        public string VirtualPath { get; set; }
        public string FileDownloadName { get; set; }

        public DownloadResult() { }

        public DownloadResult(string virtualPath)
        {
            this.VirtualPath = VirtualPath;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (!String.IsNullOrEmpty(FileDownloadName))
            {
                context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + StringUtil.getOriginalFileName(this.FileDownloadName.Replace("%", "")));
            }
            string filePath = context.HttpContext.Server.MapPath(this.VirtualPath);
            context.HttpContext.Response.TransmitFile(filePath);
        }

    }
}
