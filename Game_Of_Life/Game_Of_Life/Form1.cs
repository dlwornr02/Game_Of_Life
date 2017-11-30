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
                    p[i, j].MouseEnter += panelDrag;

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
        private void panelDrag(object sender, EventArgs e)
        {
            if (start == false && mouseDown)//&& (e).Button == MouseButtons.Left)
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
        }
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
        }
        System.Threading.Thread t=null;
        private void start_Click(object sender, EventArgs e)
        {
            stopBtn.Enabled = true;
            start = true;
            t = new System.Threading.Thread(blink);
            t.Start();
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
            if(local_form_num==form_No && form_No>1)
            {
                this.Owner.Dispose(); //처음으로 생성된 Form은 hide()를 시켜줬기 때문에 반드시 해제를 해줘야 프로세스가 종료됨
            }
        }
        
        private void 게임크기설정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show("저장된 데이터가 없습니다.");
        }

        private void speed_Scroll(object sender, EventArgs e)
        {
            gen_sec = 1000 - speed.Value * 50;
            speedlb.Text = gen_sec/1000.0+"sec/g";
            
        }
    }
}