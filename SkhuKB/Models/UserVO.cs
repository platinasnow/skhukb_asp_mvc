using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using SkhuKB.Util;

namespace SkhuKB.Models
{
    public class UserVO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
