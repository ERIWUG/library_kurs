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

    public struct Book
    {
        public int id;
        public string name;
        public string[] author;

        public int[] date;
        public string path;
        public int amount;
        public Book(int j)
        {
            this.id = 0;
            author = new string[3];
            date = new int[3];
            name = "";
            path = "";
            amount = 0;
        }
        public void Write(StreamWriter sw)
        {
            sw.WriteLine(id);

            for (int j = 0; j < 3; j++)
            {
                sw.WriteLine(author[j]);
            }
            sw.WriteLine(name);
            for (int j = 0; j < 3; j++)
            {
                sw.WriteLine(date[j]);
            }
            sw.WriteLine(amount);
            sw.WriteLine(path);

        }
    }
    public partial class Form3 : Form
    {
        public Book[] b;
        string log;
        bool fl;
        public Form3(string log, bool fl)
        {
            this.log = log;
            this.fl = fl;
            InitializeComponent();
        }

        public void PB_Click(object sender, EventArgs e)
        {
            var pb = sender as PictureBox;
            int id = Convert.ToInt32(pb.Name[pb.Name.Length - 1]) - 48;
            Form4 t = new Form4(b, id, this, log);
            t.Show();


        }
        public void sort(Book[] b,int fl)
        {
            StreamReader sr = new StreamReader("books.txt");
            int n = int.Parse(sr.ReadLine());
            sr.Close();
            Book tmp = new Book(0);
            switch (fl)
            {
                case 0:
                    for(int i = 0; i < n; i++)
                    {
                        for(int j = 0; j < n; j++)
                        {
                            if (String.Compare(b[i].name,b[j].name)>0) { b[i].id = j; b[j].id = i; ; tmp = b[i]; b[i] = b[j]; b[j] = tmp; }
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (String.Compare(b[i].author[0], b[j].author[0]) > 0) { b[i].id = j; b[j].id = i; ; tmp = b[i]; b[i] = b[j]; b[j] = tmp; }
                        }
                    }
                    break;
                case 2:
                    if (n != 0)
                    {
                        for(int i = 0; i < n; i++)
                        {
                            for(int j = 0; j < n; j++)
                            {
                                if (b[i].date[2] > b[j].date[2]) { b[i].id = j;b[j].id = i; ; tmp = b[i]; b[i] = b[j]; b[j] = tmp; }
                                else if (b[i].date[2] == b[j].date[2] && b[i].date[1] > b[j].date[1]) { b[i].id = j; b[j].id = i; tmp = b[i]; b[i] = b[j]; b[j] = tmp; }
                            }
                        }
                    }
                    break;
            }
            for(int i = 0; i < n; i++)
            {
                var a = Controls.Find("Book_Panel" + Convert.ToString(b[i].id), true)[0] as Panel;
                Pair(b[i], a, false);
            }
        }

        public void Add_Panel_Elem(Panel p, string id)
        {
            PictureBox PB = new PictureBox();
            PB.Location = new Point(3, 3);
            PB.Size = new Size(123, 125);
            PB.Name = "Book_PBox" + id;
            PB.SizeMode = PictureBoxSizeMode.Zoom;
            PB.Click += new System.EventHandler(PB_Click);
            p.Controls.Add(PB);

            Label N = new Label();
            N.Location = new Point(132, 3);
            N.Size = new Size(100, 17);
            N.Name = "Book_Name" + id;
            p.Controls.Add(N);

            Label Au = new Label();
            Au.Location = new Point(132, 33);
            Au.Size = new Size(100, 17);
            Au.Name = "Book_Author" + id;
            p.Controls.Add(Au);

            Label D = new Label();
            D.Location = new Point(132, 62);
            D.Size = new Size(76, 17);
            D.Name = "Book_Date" + id;
            p.Controls.Add(D);

            Label TA = new Label();
            TA.Location = new Point(132, 95);
            TA.Size = new Size(90, 17);
            TA.Name = "Book_TA" + id;
            TA.Text = "Количество:";
            p.Controls.Add(TA);

            Label A = new Label();
            A.Location = new Point(227, 95);
            A.Size = new Size(96, 17);
            A.Name = "Book_Amount" + id;
            p.Controls.Add(A);


        }

        public void Pair(Book b, Panel p,bool c)
        {
            if (c) { Add_Panel_Elem(p, Convert.ToString(b.id)); }
            
            var PB = p.Controls.Find("Book_PBox" + Convert.ToString(b.id), true)[0] as PictureBox;
            PB.ImageLocation = b.path;
            var N = p.Controls.Find("Book_Name" + Convert.ToString(b.id), true)[0] as Label;
            N.Text = b.name;
            var Au = p.Controls.Find("Book_Author" + Convert.ToString(b.id), true)[0] as Label;
            Au.Text = b.author[0] + " " + b.author[1] + " " + b.author[2];
            var D = p.Controls.Find("Book_Date" + Convert.ToString(b.id), true)[0] as Label;
            D.Text = Convert.ToString(b.date[0]) + "." + Convert.ToString(b.date[1]) + "." + Convert.ToString(b.date[2]);
            var Am = p.Controls.Find("Book_Amount" + Convert.ToString(b.id), true)[0] as Label;
            Am.Text = Convert.ToString(b.amount);
        }

        public Book Create_Book(Book b, StreamReader sr)
        {

            b.id = int.Parse(sr.ReadLine());
            for (int j = 0; j < 3; j++)
            {
                b.author[j] = sr.ReadLine();
            }

            b.name = sr.ReadLine();
            for (int j = 0; j < 3; j++)
            {
                b.date[j] = int.Parse(sr.ReadLine());
            }

            b.amount = int.Parse(sr.ReadLine());
            b.path = sr.ReadLine();
            return b;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            button2.Visible = fl;
            button3.Visible = fl;
            button1.Text = log;
            StreamReader sr = new StreamReader("books.txt", System.Text.Encoding.UTF8);
            int n = Convert.ToInt32(sr.ReadLine());
            b = new Book[n];

            for (int i = 0; i < n; i++)
            {
                b[i] = new Book(i);
                b[i] = Create_Book(b[i], sr);
                var a = Controls.Find("Book_Panel" + Convert.ToString(b[i].id), true)[0] as Panel;
                Pair(b[i], a,true);

            }
            sr.Close();
        }

       

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            StreamReader sr = new StreamReader("books.txt", System.Text.Encoding.UTF8);
            int n = Convert.ToInt32(sr.ReadLine());
            sr.Close();
            if (e.KeyCode == Keys.Enter) 
            {
                if(textBox1.Text =="")
                {
                    
                    for (int i = 0; i < n; i++)
                    {
                        var a = Controls.Find("Book_Panel" + Convert.ToString(b[i].id), true)[0] as Panel;
                        a.Controls.Clear();
                        Pair(b[i], a, true);
                    }
                    
                }
                else
                {
                    Book[] b_search = new Book[5];
                    string search = textBox1.Text;
                    int kol = 0;
                    for(int i = 0; i < n; i++)
                    {
                      
                        if (b[i].author[0] == search || (Convert.ToString(b[i].date[0]) + "." + Convert.ToString(b[i].date[1]) + "." + Convert.ToString(b[i].date[2])) == search || b[i].name == search)
                        {
                            b_search[kol] = new Book();b_search[kol]= b[i];kol += 1;
                        }
                    }

                    for (int i = 0; i < n; i++)
                    {
                        var a = Controls.Find("Book_Panel" + Convert.ToString(b[i].id), true)[0] as Panel;
                        a.Controls.Clear();
                    }

                    for(int i = 0; i < kol; i++)
                    {
                        var a = Controls.Find("Book_Panel" + i, true)[0] as Panel;
                        Pair(b_search[i], a, true) ;
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5(log);
            f5.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6(b, this);
            f6.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7(b, this);
            f7.Show();
        }





        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            sort(b, 0);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            sort(b, 1);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            sort(b, 2);
        }

        private void panel2_MouseHover(object sender, EventArgs e)
        {
            panel2.Size = new Size(168, 86);
        }

        

        private void radioButton1_MouseHover(object sender, EventArgs e)
        {
            panel2.Size = new Size(168, 86);
        }

        private void radioButton2_MouseHover(object sender, EventArgs e)
        {
            panel2.Size = new Size(168, 86);
        }

        private void radioButton3_MouseHover(object sender, EventArgs e)
        {
            panel2.Size = new Size(168, 86);
        }

        private void Form3_MouseHover(object sender, EventArgs e)
        {
            panel2.Size = new Size(168, 25);
        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {
            panel2.Size = new Size(168, 25);
        }
    }
}