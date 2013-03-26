using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace SkhuKB.Util
{
    public class DBConn 
    {
        public static SqlConnection getConn(){
            var con = new SqlConnection();
            //con.ConnectionString = "Data Source=203.246.75.192;Initial Catalog=skhukb; User ID=skhukb;Password=skhu@kb";
            //con.ConnectionString = "Data Source=SNOW; User ID=sa;Password=kurenai0";
            con.ConnectionString = "Data Source=localhost;Initial Catalog=SNOW; User ID=sa;Password=kurenai0";
            return con;
        }
      
    }
}
