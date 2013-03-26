using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using SkhuKB.Util;

namespace SkhuKB.DAO
{
    public class UserDAO 
    {
        public static bool IsUserInfo(string userId, string password)
        {
            using (SqlConnection conn = DBConn.getConn())
            {
                int isUserInfo = 0;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "P_SKHU_WIKI_USER_SELECT";
                    cmd.Parameters.Add("@UserId", System.Data.SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters["@UserId"].Value = userId;
                    cmd.Parameters["@Password"].Value = password;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            isUserInfo = Convert.ToInt32(reader["isUserInfo"]);
                        }                       
                    }
                }
                if (isUserInfo == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
        

    }
}
