using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Quan_ly_sinh_vien.DAL
{
    public static class Database
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["QLSinhVien"].ConnectionString;

        // Lấy kết nối đến SQL Server
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        // Thực thi câu lệnh SELECT trả về DataTable
        public static DataTable ExecuteQuery(
            string query,
            SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // Thực thi INSERT, UPDATE, DELETE
        public static int ExecuteNonQuery(
            string query,
            SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Thực thi câu lệnh trả về một giá trị
        public static object ExecuteScalar(
            string query,
            SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    return cmd.ExecuteScalar();
                }
            }
        }

        // Thực thi Stored Procedure trả về DataTable
        public static DataTable ExecuteProcedure(
            string spName,
            SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}
