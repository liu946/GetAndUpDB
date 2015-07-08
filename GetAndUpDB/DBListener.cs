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
        int refreshtime = 5000;
        int autoid;
        private bool quitthread = false;

        // 更改数据库事件定义
        public event EventHandler<Dictionary<string,string>> itemchanged;
        public event EventHandler<List<Dictionary<string,string>>> mutiitemchanged;
        public DBListener()
        {
            config = new Dictionary<string, string>();
            autoid = 0;
            config["server"] = "127.0.0.1";
            config["dbserver"] ="L-WIN10";
            config["dbname"] ="histest";
            config["dbusername"] ="root";
            config["dbpassword"] ="123456";
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
        private DBTool getnewdbtool()
        {
            return new DBTool(config["dbserver"], config["dbname"], config["dbusername"], config["dbpassword"]);
        }
        private void findUpdate(object content)
        {
            while (!quitthread)
            {
                Console.WriteLine("[{0}] refetch the db data ",DateTime.Now.ToString());
                DBTool db = getnewdbtool();
                if (db.isupdate(autoid))
                {
                    db.close();
                    dealUpdate();
                    autoid = db.syncUpdate();
                }
                Thread.Sleep(refreshtime);
            }
            
        } 
        public  void dealUpdate()
        {
            DBTool db = getnewdbtool();
            

            var datalist = db.updateSelect(autoid);
            foreach (var item in datalist)
            {
                //  触发一个事件，传入item
                if (itemchanged != null)
                {
                    Console.WriteLine("[{0}]find item added in database", DateTime.Now.ToString());
                    itemchanged(this, item);
                }
            }
            //  触发一个事件，传入itemlist
            if (mutiitemchanged != null)
            {
                Console.WriteLine("[{0}]list of items({1}) added in database", DateTime.Now.ToString(),datalist.Count);
                mutiitemchanged(this, datalist);
            }
            db.close();
        }
        public void sendToServer(object o,Dictionary<string,string> data)
        {
            PostSender ps = new PostSender(config["postserver"]);
            ps.sendPost(data);
        }
    }
}
