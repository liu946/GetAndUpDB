using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAndUpDB
{
    /**
        数据库连接工具类
        */
    class DBTool
    {
        SqlConnection sqlConnection;

        /// <summary>
        /// 初始化，建立connect保存
        /// </summary>
        public DBTool(string _dbserver,string _database,string _username,string _password)
        {
            // 建立connect 保存
            string connString = "server=" + _dbserver + ";database=" + _database + ";uid=" + _username + ";pwd=" + _password + ";";
            sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();
        }
        ~DBTool()
        {
            close();
        }
        /// <summary>
        /// 使用主键查数据
        /// </summary>
        public void close()
        {
            sqlConnection.Close();
        }
        public Dictionary<string,string> find(string id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            // todo
            return data;
        }
        /*
            增量数据存在查询
        */
        public bool isupdate(int autoid)
        {
            return true;
        }
        public int syncUpdate()
        {
            // todo return max autoid

            return 0;

        }
        /*
            返回增量数组
        */
        public List< Dictionary<string,string>> updateSelect(int autoid)
        {
            List<Dictionary<string, string>> datalist = new List<Dictionary<string, string>>();

            return datalist;
        }
    }
}
