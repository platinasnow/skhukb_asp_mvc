using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using SkhuKB.Util;

namespace SkhuKB.Models
{
    public class FilesVO
    {
        public int fidx { get; set; }
        public int saIdx { get; set; }
        public string fileName { get; set; }
    }

}
