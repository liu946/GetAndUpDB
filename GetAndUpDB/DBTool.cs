using System;
using System.Collections.Generic;
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
       
        /* 
            初始化，建立connect保存
            */
        public DBTool(string _dbserver,string _username,string _password)
        {
            // 建立connect 保存
        }
        /*
            使用主键查数据
        */
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
