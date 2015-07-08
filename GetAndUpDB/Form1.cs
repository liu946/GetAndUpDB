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
            db = new DBListener();
            db.itemchanged += itemChangedHandler;
        }

        private void itemChangedHandler(object sender, Dictionary<string, string> dataitem)
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
