using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAndUpDB
{
    /*
        刷新方式监听数据库修改
        */
    class DBListener
    {
        Dictionary<string, string> config;
        int autoid;
        public DBListener()
        {
            autoid = 0;
            config["server"] = "127.0.0.1";
            config["postserver"] = "127.0.0.1";
            // todo ...
        }
        /*
            init with file
            */
        public DBListener(string configfile)
        {
            
            // todo ...
        }
        public void listen()
        {
            // 延时刷新
            // 注册事件？开线程刷新？
            // todo ...

        }
        public void dealUpdate()
        {
            DBTool db = new DBTool(config["dbserver"], config["dbusername"], config["dbpassword"]);
            PostSender ps = new PostSender(config["postserver"]);
            var datalist = db.updateSelect(autoid);
            foreach (var item in datalist)
            {
                ps.sendPost(item);
                // todo 触发一个事件，传入item
            }
           // todo 触发一个事件，传入itemlist
        }
    }
}
