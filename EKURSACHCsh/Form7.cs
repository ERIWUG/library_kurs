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
    public partial class Form7 : Form
    {
        Book[] book;
        Form3 f3;
        public Form7(Book[] b, Form3 f3)
        {
            this.book = b;
            this.f3 = f3;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Book b = new Book(0);

            pictureBox1.ImageLocation = textBox1.Text;
            StreamReader sr = new StreamReader("books.txt");
            int id = int.Parse(sr.ReadLine());
            sr.Close();
            b.id = id;
            b.author = textBox5.Text.Split(' ');
            for (int i = 0; i < 3; i++)
            {
                b.date[i] = int.Parse(textBox3.Text.Split(':')[i]);
            }
            b.name = textBox4.Text;
           

            b.amount = int.Parse(textBox2.Text);
            b.path = textBox1.Text;
            DialogResult dr = MessageBox.Show(
                        "Уверены, что хотите создать именно эту книгу?",
                        "Подтвержение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
            if (dr == DialogResult.Yes)
            {
                StreamWriter sw = new StreamWriter("books.txt");
                sw.WriteLine(id + 1);
                for (int i = 0; i < id; i++)
                {
                    book[i].Write(sw);
                }
                b.Write(sw);
                sw.Close();
                f3.Close();
                Form3 f = new Form3("Admin", true);
                f.Show();
                f.Activate();
                Close();
            }
            else { Activate(); }
        }


    }
}
