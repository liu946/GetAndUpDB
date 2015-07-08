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
        List<string> insterestionfield;
        string connString;
        /// <summary>
        /// 初始化，建立connect保存
        /// </summary>
        public DBTool(string _dbserver,string _database,string _username,string _password)
        {
            string insterestingfieldstr = "ID,AUTOID,NAME,AGE,TELE,EMAIL,TIME,DOCTOR_NAME,REGISTER_ID";
            insterestionfield = new List<string>(insterestingfieldstr.Split(','));
            // 建立connect 保存
            connString = "server=" + _dbserver + ";database=" + _database + ";uid=" + _username + ";pwd=" + _password + ";";
            sqlConnection = new SqlConnection(connString);
            try
            {
                sqlConnection.Open();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                Console.WriteLine("sql server reconnect !");
                //throw;
            }
            
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
            
        }
        public Dictionary<string,object> find(string id)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["OTHERS"] = "{";
            string sql = "SELECT * FROM V_DATA where ID = "+id+";";
            SqlCommand sqlCmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader dr = sqlCmd.ExecuteReader();
            dr.Read();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (insterestionfield.Contains<string>(dr.GetName(i)))
                {
                    data[dr.GetName(i)] = dr[dr.GetName(i)].ToString(); ;
                }
                else
                {
                    data["OTHERS"] += dr.GetName(i) + ":\"" + dr[dr.GetName(i)].ToString() + "\",";
                }
            }

            data["OTHERS"] += "}";
            return data;
        }
        /*
            增量数据存在查询
        */
        public bool isupdate(int autoid)
        {
            string sql = "SELECT AUTOID FROM V_DATA where AUTOID > " + autoid + ";";
            SqlCommand sqlCmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader dr = sqlCmd.ExecuteReader();
            dr.Read();
            bool hasrows = dr.HasRows;
            dr.Close();
            return hasrows;
        }
        public int syncUpdate()
        {
            // todo return max autoid
            string sql = "SELECT top 1 AUTOID FROM V_DATA ORDER BY AUTOID DESC;";
            SqlCommand sqlCmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader dr = sqlCmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                return int.Parse(dr["AUTOID"].ToString());
            }else
            {
                return 0;
            }
        }
        /*
            返回增量数组
        */
        public List< Dictionary<string,object>> updateSelect(int autoid)
        {
            List<Dictionary<string, object>> datalist = new List<Dictionary<string, object>>();
            
            string sql = "SELECT * FROM V_DATA where AUTOID > " + autoid + ";";
            SqlCommand sqlCmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data["OTHERS"] = "{";
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    
                    if (insterestionfield.Contains<string>(dr.GetName(i)))
                    {
                        data[dr.GetName(i)] = dr[dr.GetName(i)].ToString(); ;
                    }
                    else
                    {
                        data["OTHERS"] += "" + dr.GetName(i) + ":\"" + dr[dr.GetName(i)].ToString() + "\",";
                    }
                }
                data["OTHERS"] += "}";
                datalist.Add(data);
            }
            dr.Close();
            for (int i = 0; i < datalist.Count; i++)
            {
                datalist[i]["childitem"] = getchilditem(int.Parse(datalist[i]["ID"].ToString()));
            }
            return datalist;
        }
        public List<Dictionary<string,object>> getchilditem(int id)
        {
            List<Dictionary<string, object>> datalist = new List<Dictionary<string, object>>();
            string sql = "SELECT * FROM V_DETAIL where id = "+id+";";
            var sqlc= new SqlConnection(connString);
            SqlCommand sqlCmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                   data[dr.GetName(i)] = dr[dr.GetName(i)].ToString(); ; 
                }
                datalist.Add(data);
            }
            dr.Close();
            return datalist;
            
        }
    }
}
