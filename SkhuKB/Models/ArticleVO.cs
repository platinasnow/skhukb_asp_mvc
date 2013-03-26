using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using SkhuKB.Util;

namespace SkhuKB.Models
{
    public class ArticleVO
    {
        public string aIdx { get; set; }
        public string Title { get; set; }
        public DateTime Indate { get; set; }
        public char Deleted { get; set; }
        public string Contents { get; set; }
    }
}
