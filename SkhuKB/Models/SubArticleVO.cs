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
    public class SubArticleVO
    {
        public int saIdx { get; set; }
        public string aIdx { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string Writer { get; set; }
        public DateTime Indate { get; set; }
        public char Deleted { get; set; }

        public SubArticleVO() { }
        public SubArticleVO(int saIdx, string aIdx)
        {
            this.saIdx = saIdx;
            this.aIdx = aIdx;
        }
    }

}
