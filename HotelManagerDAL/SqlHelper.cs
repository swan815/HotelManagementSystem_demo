using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HotelManager.DAL
{
    /// <summary>
    /// 数据库管理工具类
    /// </summary>
    public static class SqlHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static readonly string CONNECTIONSTRING = "Data Source=USER-UNUIK73QH2\\SQLEXPRESSNEW;Initial Catalog=HotelManagerSys;Integrated Security=True";

        private static SqlConnection conn = null;
        private static SqlCommand comm = null;

        /// <summary>
        /// 取得连接对象
        /// </summary>
        /// <returns></returns>
        private static SqlConnection GetConnection()
        {
            if (conn == null)
            {
                conn = new SqlConnection(CONNECTIONSTRING);
            }
            return conn;
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        private static void OpenConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        private static void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 设置SqlCommand对象       
        /// </summary>
        /// <param name="cmd">SqlCommand对象 </param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数集合</param>
        private static void SetCommand(SqlCommand cmd, string cmdText, CommandType cmdType, SqlParameter[] paras)
        {
            cmd.Connection = GetConnection();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (paras != null)
            {
                cmd.Parameters.AddRange(paras);
            }
        }


        /// <summary>
        /// 查询第一行第一列的值（参数化 存储过程)
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="para">参数集合</param>
        /// <returns>第一行第一列</returns>
        public static object ExecuteScalar(string sql, CommandType type, SqlParameter[] para)
        {
            try
            {
                comm = new SqlCommand();
                SetCommand(comm, sql, type, para);
                OpenConnection();
                return comm.ExecuteScalar();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// 查询第一行第一列的值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql)
        {

            try
            {
                comm = new SqlCommand(sql,GetConnection());
                OpenConnection();
                return comm.ExecuteScalar();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// 查询一个数据集(参数化 存储过程)
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="para">参数集合</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader DataReader(string sql,CommandType type,SqlParameter[] para)
        {
            try
            {
                comm = new SqlCommand();
                SetCommand(comm, sql, type, para);
                OpenConnection();
                return comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 查询一个数据集
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader DataReader(string sql)
        {
            try
            {
                comm = new SqlCommand(sql,GetConnection());
                OpenConnection();
                return comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 查询受影响的行数(参数化 存储过程)
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="para">参数集合</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string sql, CommandType type, SqlParameter[] para)
        {
            try
            {
                comm = new SqlCommand();
                SetCommand(comm, sql, type, para);
                OpenConnection();
                return comm.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 受影响行数(事务)
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="para">参数集合</param>
        /// <returns>是否提交成功</returns>
        public static bool GetExecuteNonQuery(string sql, CommandType type, SqlParameter[] para)
        {
            SqlTransaction trans = null;
            try
            {
                OpenConnection();
                trans = GetConnection().BeginTransaction();
                comm = new SqlCommand();
                SetCommand(comm, sql, type, para);
                comm.Transaction = trans;
                comm.ExecuteNonQuery();
                trans.Commit();
                return true;
            }
            catch (SqlException)
            {
                trans.Rollback();
                return false;
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
