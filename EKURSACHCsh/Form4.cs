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
    public partial class Form4 : Form
    {
        Book[] book;
        Form3 f1;
        int id;
        string log;
        public Form4(Book[] b,int id,Form3 f1, string log)
        {
            this.log = log;
            this.book = b;
            this.f1 = f1;
            this.id = id;
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Book b = book[id];
          
            pictureBox1.ImageLocation = b.path;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            label1.Text = b.name;
            label2.Text = b.author[0] + " " + b.author[1] + " " + b.author[2];
            label5.Text = Convert.ToString(b.date[0]) + "." + Convert.ToString(b.date[1]) + "." + Convert.ToString(b.date[2]);
            label4.Text = Convert.ToString(b.amount);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("books.txt");
            int n = int.Parse(sr.ReadLine());
            sr.Close();
            book[id].amount -= 1;
            f1.Close();
            if (book[id].amount <= 0)
            {
                for (int i = id; i < n - 1; i++)
                {
                    book[id] = book[id + 1];
                    book[id].id -= 1;
                }
                StreamWriter sw = new StreamWriter("books.txt");
                sw.WriteLine(n - 1);
                for (int i = 0; i < n - 1; i++)
                {
                    book[i].Write(sw);
                }
                sw.Close();
            }
            else
            {
                StreamWriter sw = new StreamWriter("books.txt");
                sw.WriteLine(n);
                for (int i = 0; i < n; i++)
                {
                    book[i].Write(sw);
                }
                sw.Close();
            }


            FileInfo fi1 = new FileInfo(log + ".txt");
            if (!fi1.Exists)
            {
                StreamWriter sw1 = fi1.CreateText();
                sw1.WriteLine(book[id].name + " " + book[id].author[0] +" "+book[id].author[1]+" "+book[id].author[2]+ "Количество: 1");
                sw1.Close();
               
            }
            else
            {
                StreamWriter sw1 = new StreamWriter(log+".txt",true);
                sw1.WriteLine(book[id].name + " " + book[id].author[0] + " " + book[id].author[1] + " " + book[id].author[2] + "Количество: 1");
                sw1.Close();
               
            }


            Form3 f = new Form3(log,(log=="Admin"));
            f.Show();
            Close();

        }
    }
}
