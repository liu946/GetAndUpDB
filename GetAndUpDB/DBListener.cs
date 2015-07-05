using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetAndUpDB
{
    /*
        刷新方式监听数据库修改
        */
    class DBListener
    {
        Dictionary<string, string> config;
        static int refreshtime = 1000;
        int autoid;
        private static bool quitthread = false;

        // 更改数据库事件定义
        public event EventHandler<Dictionary<string,string>> itemchanged;
        public event EventHandler<List<Dictionary<string,string>>> mutiitemchanged;
        public DBListener()
        {
            config = new Dictionary<string, string>();
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
        ~DBListener()
        {
            quitthread = true;
        }
        private delegate void UpdateListener();
        public void listen()
        {
            // 延时刷新
            // 注册事件？开线程刷新？
            // todo ...
            Thread listen = new Thread(findUpdate);
            listen.Start();
            
        }
        public void endthread()
        {
            quitthread = true;

        }
        private static void findUpdate(object content)
        {
            while (!quitthread)
            {
                Console.WriteLine("[{0}] refetch the db data ",DateTime.Now.ToString());

                Thread.Sleep(refreshtime);
            }
            
        } 
        public void dealUpdate()
        {
            DBTool db = new DBTool(config["dbserver"], config["dbusername"], config["dbpassword"]);
            PostSender ps = new PostSender(config["postserver"]);
            var datalist = db.updateSelect(autoid);
            foreach (var item in datalist)
            {
                ps.sendPost(item);
                //  触发一个事件，传入item
                if (itemchanged != null)
                {
                    itemchanged(this, item);
                }
            }
            //  触发一个事件，传入itemlist
            if (mutiitemchanged != null)
            {
                mutiitemchanged(this, datalist);
            }
        }
    }
}
