using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Data.SqlClient;
using SkhuKB.Util;
using SkhuKB.Models;

namespace SkhuKB.DAO
{
    public class FileDAO : Controller
    {
        public static ArrayList getSubArticleFiles(int saIdx)
        {
            using (SqlConnection conn = DBConn.getConn())
            {
                conn.Open();
                ArrayList subArticleFiles = new ArrayList();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "P_SubArticleFiles_SELECT";
                    cmd.Parameters.Add("@saIdx", System.Data.SqlDbType.Int);
                    cmd.Parameters["@saIdx"].Value = saIdx;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FilesVO file = new FilesVO
                            {
                                fidx = Convert.ToInt32(reader["fidx"]),
                                saIdx = Convert.ToInt32(reader["saIdx"]),
                                fileName = reader["fileName"].ToString()
                            };
                            subArticleFiles.Add(file);
                        }
                    }
                }
                return subArticleFiles;
            }
        }

        public static void insertSubArticleFile(int saIdx, string fileName)
        {
            using (SqlConnection conn = DBConn.getConn())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "P_SubArticleFile_INSERT";
                    cmd.Parameters.Add("@saIdx", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@fileName", System.Data.SqlDbType.NVarChar, 200);

                    cmd.Parameters["@saIdx"].Value = saIdx;
                    cmd.Parameters["@fileName"].Value = fileName;
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
