using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace EKURSACHCsh
{
    public partial class Form5 : Form
    {
        string log;
        public Form5(string log)
        {
            this.log = log;
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            label1.Text = log;
            FileInfo fi1 = new FileInfo(log+".txt");
            if (!fi1.Exists)
            {
                StreamWriter sw = fi1.CreateText();
                sw.Close();
                textBox1.Text = "У вас нет задолженности по книгам";
            }
            else
            {
                StreamReader sw = new StreamReader(log + ".txt");
                string line;
                while ((line = sw.ReadLine()) != null)
                {
                    textBox1.Text += line + "\r\n";
                }
                if (textBox1.Text =="") { textBox1.Text = "У вас нет задолженности по книгам"; }
                sw.Close();
            }
        }
    }
}
