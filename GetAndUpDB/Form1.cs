using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetAndUpDB
{
    public partial class Form1 : Form
    {
        DBListener db;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.listen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 绑定事件方法
            db = new DBListener();
            db.itemchanged += itemChangedHandlerExamplePrint;
            db.itemchanged += db.sendToServer; // 发送数据
        }

        private void itemChangedHandlerExamplePrint(object sender, Dictionary<string, object> dataitem)
        {
            Console.WriteLine("Handle the itemchanged event\n");
            foreach (var dic in dataitem)
            {
                Console.WriteLine("{0} : {1} ", dic.Key, dic.Value);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.endthread();
        }
  
    }
}
