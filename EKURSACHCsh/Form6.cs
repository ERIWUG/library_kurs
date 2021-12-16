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
    public partial class Form6 : Form
    {
        Book[] book;
        Form3 f3;
        public Form6(Book[] b, Form3 f3)
        {
            this.f3 = f3;
            this.book = b;
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
               
                try
                {
                    Book b = book[int.Parse(textBox1.Text)];
                    pictureBox1.ImageLocation = b.path;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    label1.Text = b.name;
                    label2.Text = b.author[0] + " " + b.author[1] + " " + b.author[2];
                    label5.Text = Convert.ToString(b.date[0]) + "." + Convert.ToString(b.date[1]) + "." + Convert.ToString(b.date[2]);
                    label4.Text = Convert.ToString(b.amount);
                }
                catch { };

                
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                        "Уверены, что хотите удалить именно эту книгу?",
                        "Подтвержение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
            if (dr == DialogResult.Yes)
            {

                int id = int.Parse(textBox1.Text);
                StreamReader sr = new StreamReader("books.txt");
                int n = int.Parse(sr.ReadLine());
                sr.Close();
                for (int i = id; i < n - 1; i++)
                {
                    book[id] = book[id + 1];
                    book[id].id -= 1;
                }
                StreamWriter sw = new StreamWriter("books.txt");
                sw.WriteLine(n - 1);
                for(int i = 0; i < n - 1; i++)
                {
                    book[i].Write(sw);
                }
                sw.Close();
                f3.Close();
                Form3 f = new Form3("Admin", true);
                f.Show();
                f.Activate();
                Close();

            }
        }
    }
}
