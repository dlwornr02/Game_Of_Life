//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;


//namespace _171109p
//{
//    public partial class Form1 : Form
//    {
//        const int row = 20;
//        const int col = 20;
//        static bool start=false;
//        static int p_num = 1;
//        static Panel[,] p = new Panel[row, col];
//        static Panel[,] p2 = new Panel[row, col];
//        ~Form1()
//        {
//            t.Interrupt();
//            t.Abort();
//        }
//        public Form1()
//        {
//            InitializeComponent();
//            this.Size = new Size(50 * 20-150, 50 * 20-100);
//            this.MinimumSize = new Size(300, 300);
//            //List<Panel> p = new List<Panel>();
//            this.Text = "Game of Life";
//            for (int i = 0; i < row; i++)
//            {
//                for (int j = 0; j < col; j++)
//                {
//                    p[i, j] = new Panel();
//                    p[i, j].Location = new System.Drawing.Point(j * 20 + 10, i * 20 + 10);
//                    p[i, j].Size = new Size(20, 20);
//                    p[i, j].BorderStyle = BorderStyle.FixedSingle;
//                    p[i, j].MouseClick += panelClicked;
//                    p[i, j].BackColor = Color.White;
//                    this.Controls.Add(p[i, j]);

//                    p2[i, j] = new Panel();
//                    p2[i, j].Location = new System.Drawing.Point(j * 20 + 10, i * 20 + 10);
//                    p2[i, j].Size = new Size(20, 20);
//                    p2[i, j].BorderStyle = BorderStyle.FixedSingle;
//                    p2[i, j].MouseClick += panelClicked;
//                    p2[i, j].BackColor = Color.White;
//                    this.Controls.Add(p2[i, j]);
//                    p2[i, j].Visible = false;
//                }
//            }
//            //for (int i = 0; i < this.Size.Height/20-4; i++)
//            //{
//            //    for (int j = 0; j < this.Size.Width/20-1; j++)
//            //    {
//            //        Panel p1 = new Panel();
//            //        p1.Location = new System.Drawing.Point(j * 20 + 10, i * 20 + 10);
//            //        p1.Size = new Size(20, 20);
//            //        p1.BorderStyle = BorderStyle.FixedSingle;
//            //        p1.MouseClick += panelClicked;
//            //        p1.BackColor = Color.White;
//            //        p.Add(p1);
//            //        this.Controls.Add(p1);
//            //    }
//            //}
//            //startBtn.Location = new Point(this.Width / 3, this.Height - 30);
//            startBtn.Location = new Point(this.Width/3,this.Height-70);
//            stopBtn.Location = new Point((this.Width / 3) * 2, this.Height - 70);
//            stopBtn.Enabled = false;

//        }
//        private void panelClicked(object sender, EventArgs e)
//        {
//            if (start == false)
//            {
//                if (((Panel)sender).BackColor == Color.White)
//                {
//                    ((Panel)sender).BackColor = Color.Black;
//                }
//                else
//                {
//                    ((Panel)sender).BackColor = Color.White;
//                }
//            }

//        }
//        private void Form1_SizeChanged(object sender, EventArgs e)
//        {

//            startBtn.Location = new Point(this.Width / 3, this.Height - 70);
//            stopBtn.Location = new Point((this.Width / 3) * 2, this.Height - 70);
//        }

//        private void stop_Click(object sender, EventArgs e)
//        {
//            start = false;
//            startBtn.Enabled = true;
//            t.Suspend();
//            //if(p_num==1)
//            //{
//            //    for(int i=0;i<row;i++)
//            //    {
//            //        for(int j=0;j<col;j++)
//            //        {
//            //            p2[i, j].BackColor = Color.White;
//            //        }
//            //    }
//            //}
//            //else
//            //{
//            //    for (int i = 0; i < row; i++)
//            //    {
//            //        for (int j = 0; j < col; j++)
//            //        {
//            //            p[i, j].BackColor = Color.White;
//            //        }
//            //    }
//            //}
//        }
//        System.Threading.Thread t = new System.Threading.Thread(blink);

//        private void start_Click(object sender, EventArgs e)
//        {
//            stopBtn.Enabled = true;
//            start = true;
//            //Task t = new Task(new Action(blink));
//            if (t.ThreadState == System.Threading.ThreadState.Unstarted)
//                t.Start();
//            else
//                t.Resume();
//            //t.Suspend();
//            ((Button)sender).Enabled = false;
//        }
//        private static void blink()
//        {
//            while (start)
//            {
//                if (p_num == 1)
//                {

//                    for (int i = 0; i < row; i++)
//                    {
//                        for (int j = 0; j < col; j++)
//                        {
//                            int count = 0;
//                            //int count = p[i, j].BackColor == Color.Black ? -1 : 0;
//                            for (int k = i - 1; k <= i + 1; k++)
//                            {
//                                for (int f = j - 1; f <= j + 1; f++)
//                                {
//                                    if (k >= 0 && f >= 0 && k<row && f<col)
//                                    {
//                                        count += p[k, f].BackColor == Color.Black ? 1 : 0;
//                                    }
//                                }
//                            }
//                            if (p[i, j].BackColor == Color.Black)
//                            {
//                                count--;
//                                if (count == 0 || count == 1 || count >= 4)
//                                {
//                                    p2[i, j].BackColor = Color.White;
//                                }
//                                else if (count == 2 || count == 3)
//                                {
//                                    p2[i, j].BackColor = Color.Black;
//                                }
//                            }
//                            else if (p[i, j].BackColor == Color.White)
//                            {
//                                if (count == 3)
//                                {
//                                    p2[i, j].BackColor = Color.Black;
//                                }
//                                else
//                                {
//                                    p2[i, j].BackColor = Color.White;
//                                }
//                            }
//                        }

//                    }
//                    for (int i = 0; i < row; i++)
//                    {
//                        for (int j = 0; j < col; j++)
//                        {
//                            p[i, j].Visible = false;
//                            p2[i, j].Visible = true;
//                        }
//                    }
//                    p_num = 2;
//                }
//                else if (p_num == 2)
//                {

//                    for (int i = 0; i < row; i++)
//                    {
//                        for (int j = 0; j <col; j++)
//                        {
//                            int count = 0;
//                            //int count = p2[i, j].BackColor == Color.Black ? -1 : 0;
//                            for (int k = i - 1; k <= i + 1; k++)
//                            {
//                                for (int f = j - 1; f <= j + 1; f++)
//                                {
//                                    if (k >= 0 && f >= 0 && k < row && f < col)
//                                    {
//                                        count += p2[k, f].BackColor == Color.Black ? 1 : 0;
//                                    }
//                                }
//                            }
//                            if (p2[i, j].BackColor == Color.Black)
//                            {
//                                count--;
//                                if (count == 0 || count == 1 || count >= 4)
//                                {
//                                    p[i, j].BackColor = Color.White;
//                                }
//                                else if (count == 2 || count == 3)
//                                {
//                                    p[i, j].BackColor = Color.Black;
//                                }
//                            }
//                            else if (p2[i, j].BackColor == Color.White)
//                            {
//                                if (count == 3)
//                                {
//                                    p[i, j].BackColor = Color.Black;
//                                }
//                                else
//                                {
//                                    p[i, j].BackColor = Color.White;
//                                }

//                            }
//                        }
//                    }
//                    for (int i = 0; i < row; i++)
//                    {
//                        for (int j = 0; j < col; j++)
//                        {
//                            p2[i, j].Visible = false;
//                            p[i, j].Visible = true;
//                        }
//                    }
//                    p_num = 1;
//                }

//                //p_num = (p_num == 1) ? (2) : (1);
//                //if (p[0, 0].BackColor == Color.White)
//                //    p[0, 0].BackColor = Color.Black;
//                //else
//                //    p[0, 0].BackColor = Color.White;
//                //System.Threading.Thread.Sleep(50);
//            }
//        }
//    }
//}


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

namespace _171109p
{
    public partial class Form1 : Form
    {
        static int row=0;
        static int col=0;
        static bool start = false;
        static Panel[,] p;
        static int[,] p2;
        public static int form_No = 0;
        static int gen_sec=1000;
        bool mouseDown = false;
        int local_form_num;
        Form2 f;
        ~Form1()
        {
            //t.Interrupt();
            //t.Abort();
        }
        public Form1(int r, int c)
        {
            form_No++;
            local_form_num = form_No;
            row = r; col = c;
            InitializeComponent();
            p = new Panel[row, col];
            p2 = new int[row, col];
            this.Size = new Size(col * 10 + 35, row * 10 + 110);
            this.MinimumSize = new Size(col * 10 + 35, row * 10 + 150);
            this.MaximumSize = new Size(col * 10 + 35, row * 10 + 150);
            //this.MinimumSize = new Size(300, 300);
            //List<Panel> p = new List<Panel>();
            this.Text = "Game of Life";
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    p[i, j] = new Panel();
                    p[i, j].Location = new System.Drawing.Point(j * 10 + 10, i * 10 + 30);
                    p[i, j].Size = new Size(10, 10);
                    p[i, j].BorderStyle = BorderStyle.FixedSingle;

                    p[i, j].MouseClick += panelClicked;
                    //p[i, j].MouseEnter += panelDrag;

                    //p[i, j].MouseMove += panelDrag;
                    //MouseButtons.Left;
                    //p[i, j].MouseEnter += panelDrag;
                    //p[i, j].MouseDown += p_MouseDown;
                    //p[i, j].MouseUp += p_MouseUp;

                    p[i, j].BackColor = Color.White;
                    this.Controls.Add(p[i, j]);

                    p2[i, j] = 0;
                }
            }
            startBtn.Location = new Point(this.Width / 3-37, this.Height - 70);
            stopBtn.Location = new Point((this.Width / 3) * 2-37, this.Height - 70);
            speed.Location = new Point(0, this.Height - 115);
            speedlb.Location = new Point(this.Width / 3 * 2, this.Height - 115);

            speed.Size = new Size(this.Width/3*2,50);
            speedlb.Text = gen_sec / 1000 + "sec/g";



            stopBtn.Enabled = false;

            f = new Form2(form_No);

        }
        private void panelClicked(object sender, MouseEventArgs e)
        {
            if (start == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (((Panel)sender).BackColor == Color.White)
                    {
                        ((Panel)sender).BackColor = Color.Black;
                    }
                    else
                    {
                        ((Panel)sender).BackColor = Color.White;
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    mouseDown = !mouseDown;
                }
            }

        }
        //private void panelDrag(object sender, EventArgs e)
        //{
        //    if (start == false && mouseDown)//&& (e).Button == MouseButtons.Left)
        //    {
        //        if (((Panel)sender).BackColor == Color.White)
        //        {
        //            ((Panel)sender).BackColor = Color.Black;
        //        }
        //        else
        //        {
        //            ((Panel)sender).BackColor = Color.White;
        //        }
        //    }
        //}
        private void Form1_SizeChanged(object sender, EventArgs e)
        {

            startBtn.Location = new Point(this.Width / 3 - 37, this.Height - 70);
            stopBtn.Location = new Point((this.Width / 3) * 2 - 37, this.Height - 70);
        }

        private void stop_Click(object sender, EventArgs e)
        {
            start = false;
            ((Button)sender).Enabled = false;
            t.Abort();
            startBtn.Enabled = true;
            //System.Threading.Thread.Sleep(50);
        }
        //System.Threading.Thread t = new System.Threading.Thread(blink);
        System.Threading.Thread t=null;
        private void start_Click(object sender, EventArgs e)
        {
            stopBtn.Enabled = true;
            start = true;
            t = new System.Threading.Thread(blink);
            t.Start();
            //if (t.ThreadState == System.Threading.ThreadState.Unstarted)
            //    t.Start();
            //else
            //    t.Resume();
            ((Button)sender).Enabled = false;
        }
        private static void blink()
        {
            while (true)
            {
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            int count = 0;
                            for (int k = i - 1; k <= i + 1; k++)
                            {
                                for (int f = j - 1; f <= j + 1; f++)
                                {
                                    if (k >= 0 && f >= 0 && k < row && f < col)
                                    {
                                        count += p[k, f].BackColor == Color.Black ? 1 : 0;
                                    }
                                }
                            }
                            if (p[i, j].BackColor == Color.Black)
                            {
                                count--;
                                if (count == 0 || count == 1 || count >= 4)
                                {
                                    p2[i, j]= 0;
                                }
                                else if (count == 2 || count == 3)
                                {
                                    p2[i, j]=1;
                                }
                            }
                            else if (p[i, j].BackColor == Color.White)
                            {
                                if (count == 3)
                                {
                                    p2[i, j]=1;
                                }
                                else
                                {
                                    p2[i, j] = 0;
                                }
                            }
                        }
                    }
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if (p2[i, j] == 1)
                                p[i, j].BackColor = Color.Black;
                            else if(p2[i,j]==0  &&  p[i, j].BackColor ==Color.Black)
                                p[i, j].BackColor = Color.White;
                            p2[i, j] = 0;
                        }
                    }
                System.Threading.Thread.Sleep(gen_sec);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (t != null)
            {
                t.Abort();
            }
            //if (local_form_num > 2)
            //{
            //    this.Owner.Close();
            //}
            if(local_form_num==form_No && form_No>1)//&& ((Form1)this.Owner).local_form_num==1)
            {
                //System.Environment.Exit(0);
                this.Owner.Dispose(); //처음으로 생성된 Form은 Hide를 시켜줬기 때문에 반드시 해제를 해줘야 프로세스가 종료됨
            }
            //t.Start();
            //System.Threading.Thread.Sleep(50);
            //try
            //{
            //    //t.Interrupt();
            //    t.Abort();
            //}
            //catch(Exception a)
            //{
            //    Console.WriteLine(a.StackTrace);
            //    //t.Interrupt();
            //    //t.Abort();
            //}
        }
        
        private void 게임크기설정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //f.ParentForm = this;
            f.ShowDialog(this);
        }

        private void 리셋ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    p[i, j].BackColor = Color.White;
                }
            }
            speed.Value = 0;
            gen_sec = 1000 - speed.Value * 50;
            speedlb.Text = gen_sec / 1000.0 + "sec/g";
            
        }
        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (t != null)
            {
                t.Abort();
            }
            this.Close();
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //?x?인지
            //어떤 세포가 살아있는지
            //끝?
            using (StreamWriter sw = new StreamWriter(new FileStream("save.txt", FileMode.Create)))
            {
                sw.Write(row+ " ");
                sw.Write(col + " ");
                for(int i =0;i<row;i++)
                {
                    for(int j=0;j<col;j++)
                    {
                        if (p[i, j].BackColor == Color.Black)
                        {
                            sw.Write("1 ");
                        }
                        else if (p[i, j].BackColor == Color.White)
                        {
                            sw.Write("0 ");
                        }
                        
                    }
                }
            }
        }

        private void 불러오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader(new FileStream("save.txt", FileMode.Open, FileAccess.Read));
                string[] s = sr.ReadLine().Split(' ');

                if (form_No == 1)
                {
                    this.Hide();
                }
                else
                {
                    this.Close();
                }

                Form1 f = new Form1(int.Parse(s[0]), int.Parse(s[1]));
                for (int i = 0; i < int.Parse(s[0]); i++)
                {
                    for (int j = 0; j < int.Parse(s[1]); j++)
                    {
                        if (int.Parse(s[i * int.Parse(s[1]) + j + 2]) == 1)
                        {
                            p[i, j].BackColor = Color.Black;
                        }
                        else if (int.Parse(s[i * int.Parse(s[1]) + j + 2]) == 0)
                        {
                            p[i, j].BackColor = Color.White;
                        }
                    }
                }
                f.Show();
                sr.Close();
            }
            catch(Exception fioe)
            {
                //this.ShowDialog();
                MessageBox.Show("저장된 데이터가 없습니다.");
                Console.WriteLine(fioe.StackTrace);
            }
        }

        private void speed_Scroll(object sender, EventArgs e)
        {
            gen_sec = 1000 - speed.Value * 50;
            speedlb.Text = gen_sec/1000.0+"sec/g";
            
        }

        //private void p_MouseDown(object sender, MouseEventArgs e)
        //{
        //    mouseDown = true;
        //}

        //private void p_MouseUp(object sender, MouseEventArgs e)
        //{
        //    mouseDown = false;
        //}
    }
}