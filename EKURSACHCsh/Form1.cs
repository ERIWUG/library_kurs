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
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            Activate();
            InitializeComponent();
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(this);
            f2.Show();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("pass.txt");
            string login = textBox1.Text;
            string pass = textBox2.Text;
            string p = login + ":" + pass;
            string line;
            bool c = false;
            while ((line = await sr.ReadLineAsync()) != null)
            {
                if (line == p) { c = true; }
            }
            if (c) 
            {
                bool fl = false;
                if (login == "Admin") { fl = true; }
                Form3 f3 = new Form3(login,fl);
                Hide();
                f3.Show();
            }
            else 
            {
                MessageBox.Show(
                    "Данные не совпадают",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                Activate();
            }
            sr.Close();
        }
    }
}
