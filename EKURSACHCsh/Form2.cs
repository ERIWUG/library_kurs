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
    public partial class Form2 : Form
    {
        Form1 f1;
        public Form2(Form1 f1)
        {
            this.f1=f1;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("pass.txt");
            string login = textBox1.Text;
            string pass = textBox2.Text;
            string pass1 = textBox3.Text;
            if (pass1 != pass)
            {
                DialogResult dr = MessageBox.Show(
                    "Данные не совпадают",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                Activate();
            }
            else
            {
                string p = login + ":" + pass;
                string line;
                bool c = false;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    if (line == p) { c = true; }
                }
                sr.Close();

                if (c)
                {
                    DialogResult dr = MessageBox.Show(
                        "Вернуться?",
                        "Такой пользователь уже существует",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    if (dr == DialogResult.Yes)
                    {
                        
                        Close();
                        
                    }
                }
                else
                {
                    StreamWriter sw = new StreamWriter("pass.txt", true);
                    sw.Write(p + '\n');
                    sw.Close();
                    DialogResult dr = MessageBox.Show(
                        "Новый пользователь был создан",
                        "Успех",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    
                    Close();
                    
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            f1.Activate();
        }
    }
}
