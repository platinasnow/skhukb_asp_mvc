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
    public class ArticleDAO 
    {
       public static ArrayList getArticleList(string searchText, string selecter, int page)
        {
            using (SqlConnection conn = DBConn.getConn())
            {
                conn.Open();
                var articleList = new ArrayList();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "P_ArticleList_SELECT";
                    cmd.Parameters.Add("@aIdx", System.Data.SqlDbType.NVarChar, 30);
                    cmd.Parameters.Add("@searchText", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@contents", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@writer", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@titleContents", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@page", System.Data.SqlDbType.Int);
                    cmd.Parameters["@aIdx"].Value = searchText;
                    cmd.Parameters["@searchText"].Value = searchText;
                    cmd.Parameters["@" + selecter].Value = selecter;
                    cmd.Parameters["@page"].Value = page;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ArticleVO article = new ArticleVO
                            {
                                aIdx = reader["aIdx"].ToString(),
                                Title = reader["Title"].ToString(),
                                Indate = Convert.ToDateTime(reader["Indate"]),
                                Deleted = Convert.ToChar(reader["Deleted"]),
                                Contents = reader["Contents"].ToString()
                            };
                            articleList.Add(article);
                        }
                    }
                }
                return articleList;
            }
        }
        public static int getTotalCountArticleList(string searchText, string selecter)
        {
            using (SqlConnection conn = DBConn.getConn())
            {
                conn.Open();
                int totalCount = 0;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "P_ArticleList_TotalCount_SELECT";
                    cmd.Parameters.Add("@aIdx", System.Data.SqlDbType.NVarChar, 30);
                    cmd.Parameters.Add("@searchText", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@contents", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@writer", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@titleContents", System.Data.SqlDbType.VarChar, 30);
                    cmd.Parameters["@aIdx"].Value = searchText;
                    cmd.Parameters["@searchText"].Value = searchText;
                    cmd.Parameters["@" + selecter].Value = selecter;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            totalCount = Convert.ToInt32(reader[0]);
                        }
                    }
                }
                return totalCount;
            }
        }

        public static ArticleVO getArticleItem(string aIdx)
        {
            using (SqlConnection conn = DBConn.getConn())
            {
                conn.Open();
                ArticleVO articleItem = null;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "P_ArticleItem_SELECT";
                    cmd.Parameters.Add("@aIdx", System.Data.SqlDbType.NVarChar, 30);
                    cmd.Parameters["@aIdx"].Value = aIdx;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            articleItem = new ArticleVO
                            {
                                aIdx = reader["aIdx"].ToString(),
                                Title = reader["Title"].ToString(),
                                Indate = Convert.ToDateTime(reader["Indate"]),
                                Deleted = Convert.ToChar(reader["Deleted"])
                            };
                        }
                    }
                }
                return articleItem;
            }
        }

        public static void insertArticleList(string aIdx)
        {
            using (SqlConnection conn = DBConn.getConn())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "P_ArticleLIST_INSERT";
                    cmd.Parameters.Add("@aIdx", System.Data.SqlDbType.NVarChar, 30);
                    cmd.Parameters["@aIdx"].Value = aIdx;
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
