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
    public class SubArticleDAO 
    {
        public static ArrayList getSubArticleList(string aIdx, char deleted)
        {
            using (SqlConnection conn = DBConn.getConn())
            {
                conn.Open();
                ArrayList subArticleList = new ArrayList();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "P_SubArticleList_SELECT";
                    cmd.Parameters.Add("@aIdx", System.Data.SqlDbType.NVarChar, 30);
                    cmd.Parameters.Add("@deleted", System.Data.SqlDbType.Char, 1);
                    cmd.Parameters["@aIdx"].Value = aIdx;
                    cmd.Parameters["@deleted"].Value = deleted;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SubArticleVO subArticle = new SubArticleVO
                            {
                                saIdx = Convert.ToInt32(reader["saIdx"]),
                                aIdx = reader["aIdx"].ToString(),
                                Title = reader["Title"].ToString(),
                                Contents = reader["Contents"].ToString(),
                                Indate = Convert.ToDateTime(reader["Indate"]),
                                Deleted = Convert.ToChar(reader["Deleted"]),
                                Writer = reader["Writer"].ToString()
                            };
                            subArticleList.Add(subArticle);
                        }
                    }
                }
                return subArticleList;
            }
        }
        public static SubArticleVO getSubArticleItem(int saIdx, char deleted)
        {
            using (SqlConnection conn = DBConn.getConn())
            {
                conn.Open();
                SubArticleVO subArticleItem = null;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "P_SubArticleItem_SELECT";
                    cmd.Parameters.Add("@saIdx", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@deleted", System.Data.SqlDbType.Char, 1);
                    cmd.Parameters["@saIdx"].Value = saIdx;
                    cmd.Parameters["@deleted"].Value = deleted;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subArticleItem = new SubArticleVO
                            {
                                saIdx = Convert.ToInt32(reader["saIdx"]),
                                aIdx = reader["aIdx"].ToString(),
                                Title = reader["Title"].ToString(),
                                Contents = reader["Contents"].ToString(),
                                Indate = Convert.ToDateTime(reader["Indate"]),
                                Deleted = Convert.ToChar(reader["Deleted"]),
                                Writer = reader["Writer"].ToString()
                            };
                        }
                    }
                }
                return subArticleItem;
            }
        }

        public static int InsertSubArticleList(string aIdx, string title, string contents, string writer)
        {
            using (SqlConnection conn = DBConn.getConn())
            {
                int saIdx = 0;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "P_SubArticleList_INSERT";
                    cmd.Parameters.Add("@aIdx", System.Data.SqlDbType.NVarChar, 30);
                    cmd.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@Contents", System.Data.SqlDbType.Text);
                    cmd.Parameters.Add("@Writer", System.Data.SqlDbType.NVarChar, 30);

                    cmd.Parameters["@aIdx"].Value = aIdx;
                    cmd.Parameters["@Title"].Value = title;
                    cmd.Parameters["@Contents"].Value = contents;
                    cmd.Parameters["@Writer"].Value = writer;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            saIdx = Convert.ToInt32(reader["value"]);
                        }
                    }
                    return saIdx;
                }
            }

        }

        public static void deleteSubArticleList(int saIdx)
        {
            using (SqlConnection conn = DBConn.getConn())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "P_SubArticleList_DELETE";
                    cmd.Parameters.Add("@saIdx", System.Data.SqlDbType.Int);

                    cmd.Parameters["@saIdx"].Value = saIdx;
                    cmd.ExecuteNonQuery();

                }
            }
        }

    }
}
