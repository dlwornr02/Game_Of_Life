﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Game_Of_Life
{
    public partial class Form1 : Form
    {
        static int row = 0;
        static int col = 0;
        static bool start = false;
        static Panel[,] p;
        static int[,] p2;
        public static int form_No = 0;
        static int gen_sec = 1000;
        bool mouseDown = false;
        int local_form_num;

        static int point;
        static int generation;
        static int point_limit;
        static int gen_limit;
        //static string s_rt;
        //static int i_rn;
        //static List<string> room;
        static int room_No = -1;
        Setsize_dlg f;

        static ListBox lb_Room = new ListBox();
        Button bt_creRoom = new Button();
        Button bt_enter = new Button();

        static TextBox tb_chat = new TextBox();
        Button bt_ready = new Button();
        Button bt_exit = new Button();

        static Label lb_gen = new Label();
        static private Socket mainSocket; //Socket통신에 사용되는 변수
        private byte[] data = new byte[1024]; //고정길이로 데이터를 보내기위한 배열
        private int size = 1024; //고정길이 1024
        IPAddress thisAddress; //클라이언트(계산기)의 IP를 저장하기위한 변수
        public string server_IP, server_Port;
        bool game_start;

        //static Form1()
        //{
        //    lb_Room.SelectedIndexChanged += lb_Room_SelectedIndexChanged;
        //}
        //static private void lb_Room_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    s_rt = (string)lb_Room.Items[room_No];
        //    MessageBox.Show(s_rt);
        //}
        public Form1(int r, int c)
        {
            form_No++;
            local_form_num = form_No;
            row = r; col = c;
            InitializeComponent();
            p = new Panel[row, col];
            p2 = new int[row, col];
            point = 0;
            generation = 0;
            game_start = false;

            this.Text = "Game of Life";
            this.Size = new Size(col * 10 + 35, row * 10 + 190);
            this.MinimumSize = new Size(col * 10 + 35, row * 10 + 190);
            this.MaximumSize = new Size(col * 10 + 35, row * 10 + 190);

            bt_creRoom.Text = "방만들기";
            bt_enter.Text = "입장";
            bt_creRoom.Click += bt_creRoom_click;
            bt_enter.Click += bt_enter_click;
            bt_creRoom.Size = new Size(80, 20);
            bt_enter.Size = new Size(40, 20);


            bt_ready.Text = "Ready";
            bt_exit.Text = "exit";
            bt_ready.Size = new Size(80, 20);
            bt_exit.Size = new Size(80, 20);
            bt_ready.Click += bt_ready_click;
            bt_exit.Click += bt_exit_click;
            tb_chat.Multiline = true;
            tb_chat.Enabled = false;


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
            startBtn.Location = new Point(this.Width / 3 - 37, this.Height - 70);
            stopBtn.Location = new Point((this.Width / 3) * 2 - 37, this.Height - 70);

            lb_point.Location = new Point(this.Width / 7, this.Height - 135);
            lb_gen.Location = new Point(this.Width / 7 * 3, this.Height - 135);

            lb_point.Text = "Point: 0";
            lb_gen.Text = "generation: 0";

            speed.Location = new Point(0, this.Height - 115);
            speedlb.Location = new Point(this.Width / 3 * 2, this.Height - 115);

            speed.Size = new Size(this.Width / 3 * 2, 50);
            speedlb.Text = gen_sec / 1000 + "sec/g";


            this.Controls.Add(lb_Room);
            this.Controls.Add(bt_enter);
            this.Controls.Add(bt_creRoom);
            this.Controls.Add(lb_gen);

            this.Controls.Add(tb_chat);
            this.Controls.Add(bt_ready);
            this.Controls.Add(bt_exit);

            lb_Room.Visible = false;
            bt_creRoom.Visible = false;
            bt_enter.Visible = false;
            bt_ready.Visible = false;
            bt_exit.Visible = false;



            stopBtn.Enabled = false;

            f = new Setsize_dlg(form_No);

            //사용자의 IP설정
            SetIp();


            //서버와의 통신에 쓰일 Socket의 인스턴스를 생성해준다.
            if (mainSocket == null)
                mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            if (mainSocket.Connected)
            {
                this.Size = new Size((col * 10 + 35) * 2, row * 10 + 190);
                this.MinimumSize = new Size((col * 10 + 35) * 2, row * 10 + 190);
                this.MaximumSize = new Size((col * 10 + 35) * 2, row * 10 + 190);
                setlb_bt_size_position(true);
            }
        }
        private void bt_ready_click(object sender, EventArgs e)
        {
            if (room_No != -1)
            {
                if (((Button)sender).Text == "Ready")
                {
                    if (point <= point_limit)
                    {
                        ((Button)sender).Text = "Release";
                        SendMessaage("Ready " + room_No);
                        bt_exit.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("세포의 개수가 너무 많습니다.\n최대 세포의 수 : " + point_limit);
                    }
                }
                else if (((Button)sender).Text == "Release")
                {
                    ((Button)sender).Text = "Ready";
                    bt_exit.Enabled = true;
                }
            }
            else
            {
                setlb_bt_size_position(true);
                setlb_bt_size_position2(false);
            }

        }
        private void bt_exit_click(object sender, EventArgs e)
        {
            if (room_No != -1)
            {
                SendMessaage("OR" + room_No);
                setlb_bt_size_position(true);
                setlb_bt_size_position2(false);
                tb_chat.Clear();
                room_No = -1;
            }
            else
            {
                setlb_bt_size_position(true);
                setlb_bt_size_position2(false);
            }
        }
        private void bt_enter_click(object sender, EventArgs e)
        {
            if (lb_Room.Items.Count > 0 && lb_Room.SelectedIndex != -1)
            {
                room_No = lb_Room.SelectedIndex;
                SendMessaage("ER" + room_No);

                room_No = lb_Room.SelectedIndex;
                string s = (string)lb_Room.Items[room_No];
                string[] s2 = s.Split(':', ' ', 'x');

                //1,3,5,7
                //"point:" + s2[0] + " gen:" + s2[1] + " size:" + s2[2] + "x" + s2[3] + " user:" + s2[4]
                point_limit = int.Parse(s2[1]);
                gen_limit = int.Parse(s2[3]);
                if (!(row == int.Parse(s2[5]) && col == int.Parse(s2[6]))) //행과열이 다를 때 새로운 form생성
                {
                    Form1 f = new Form1(int.Parse(s2[5]), int.Parse(s2[6]));
                    if (form_No == 2)
                    {
                        this.Hide();
                        f.Owner = this;
                    }
                    else
                    {
                        f.Owner = this.Owner;
                        this.Close();
                    }
                    f.Show();
                    f.setlb_bt_size_position(false);
                    f.setlb_bt_size_position2(true);
                }
                else //row와 col이 같을 때
                {
                    리셋ToolStripMenuItem_Click(null, null);
                    setlb_bt_size_position(false);
                    setlb_bt_size_position2(true);
                }
            }
        }
        private void bt_creRoom_click(object sender, EventArgs e)
        {
            createRoom cr = new createRoom();
            if (cr.ShowDialog() == DialogResult.OK)
            {
                point_limit = createRoom.point;
                gen_limit = createRoom.gen;
                if (!(row == createRoom.row && col == createRoom.col)) //행과열이 다를 때 새로운 form생성
                {
                    Form1 f = new Form1(createRoom.row, createRoom.col);
                    if (form_No == 2)
                    {
                        this.Hide();
                        f.Owner = this;
                    }
                    else
                    {
                        f.Owner = this.Owner;
                        this.Close();
                    }
                    f.Show();
                    f.setlb_bt_size_position(false);
                    f.setlb_bt_size_position2(true);
                }
                else //row와 col이 같을 때
                {
                    리셋ToolStripMenuItem_Click(null, null);
                    setlb_bt_size_position(false);
                    setlb_bt_size_position2(true);
                }
                SendMessaage("CreateRoom " + point_limit + " " + gen_limit + " " + createRoom.row + " " + createRoom.col);
                room_No = lb_Room.Items.Count;

            }
        }


        private void setlb_bt_size_position(bool visible)
        {
            if (visible)
            {
                lb_Room.Size = new Size(col * 10, row * 10);
                lb_Room.Location = new Point(col * 10 + 35, 30);
                bt_creRoom.Location = new Point(col * 10 + 35, row * 10 + 30);
                bt_enter.Location = new Point(col * 10 + 130, row * 10 + 30);
                lb_Room.Visible = true;
                bt_creRoom.Visible = true;
                bt_enter.Visible = true;
            }
            else
            {
                lb_Room.Visible = false;
                bt_creRoom.Visible = false;
                bt_enter.Visible = false;
            }
        }
        private void setlb_bt_size_position2(bool visible)
        {
            if (visible)
            {
                tb_chat.Size = new Size(col * 10, row * 10);
                tb_chat.Location = new Point(col * 10 + 35, 30);
                bt_ready.Location = new Point(col * 10 + 35, row * 10 + 30);
                bt_exit.Location = new Point(col * 10 + 130, row * 10 + 30);
                tb_chat.Visible = true;
                bt_ready.Visible = true;
                bt_exit.Visible = true;
            }
            else
            {
                tb_chat.Visible = false;
                bt_ready.Visible = false;
                bt_exit.Visible = false;
            }
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
                        point++;
                        lb_point.Text = "Point: " + point;
                    }
                    else
                    {
                        ((Panel)sender).BackColor = Color.White;
                        point--;
                        lb_point.Text = "Point: " + point;
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
                    point++;
                    lb_point.Text = "Point: " + point;
                }
                else
                {
                    ((Panel)sender).BackColor = Color.White;
                    point--;
                    lb_point.Text = "Point: " + point;
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
        System.Threading.Thread t = null;
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
                                p2[i, j] = 0;
                            }
                            else if (count == 2 || count == 3)
                            {
                                p2[i, j] = 1;
                            }
                        }
                        else if (p[i, j].BackColor == Color.White)
                        {
                            if (count == 3)
                            {
                                p2[i, j] = 1;
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
                        else if (p2[i, j] == 0 && p[i, j].BackColor == Color.Black)
                            p[i, j].BackColor = Color.White;
                        p2[i, j] = 0;
                    }
                }
                generation++;
                lb_gen.Text = "generation: " + generation;
                System.Threading.Thread.Sleep(gen_sec);
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
            point = 0;
            generation = 0;
            speed.Value = 0;
            gen_sec = 1000 - speed.Value * 50;
            speedlb.Text = gen_sec / 1000.0 + "sec/g";
            lb_gen.Text = "generation: " + generation;
            lb_point.Text = "point: " + point;

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
                sw.Write(row + " ");
                sw.Write(col + " ");
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
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
            //171205 에러처리 owner전하는
            try
            {
                StreamReader sr = new StreamReader(new FileStream("save.txt", FileMode.Open, FileAccess.Read));
                string[] s = sr.ReadLine().Split(' ');

                if (!(row == int.Parse(s[0]) && col == int.Parse(s[1]))) //행과열이 다를 때 새로운 form생성
                {
                    Form1 f = new Form1(int.Parse(s[0]), int.Parse(s[1]));
                    if (form_No == 2)
                    {
                        this.Hide();
                        f.Owner = this;
                    }
                    else
                    {
                        f.Owner = this.Owner;
                        this.Close();
                    }


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
                }
                else //row와 col이 같을 때
                {
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
                }
                sr.Close();
            }
            catch (Exception fioe)
            {
                MessageBox.Show("저장된 데이터가 없습니다.");
            }
        }

        private void speed_Scroll(object sender, EventArgs e)
        {
            gen_sec = 1000 - speed.Value * 50;
            speedlb.Text = gen_sec / 1000.0 + "sec/g";

        }
        //////////////////////////////////////////////////////////////////////////////

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //Form이 종료될 때의 이벤트 처리
        {
            if (t != null)
            {
                t.Abort();
            }
            if (local_form_num == form_No && form_No > 1)
            {
                if (mainSocket.Connected)
                {
                    DisConnectServer();
                }
                this.Owner.Dispose(); //처음으로 생성된 Form은 hide()를 시켜줬기 때문에 반드시 해제를 해줘야 프로세스가 종료됨
            }
            //종료될 때 서버와 연결되어있다면 연결을 해제해줘야한다.
            if (form_No == 1 && mainSocket.Connected)
            {
                DisConnectServer();
            }
        }

        private void SetIp()
        {
            IPHostEntry he = Dns.GetHostEntry(Dns.GetHostName());
            // 처음으로 발견되는 ip 주소를 프로그램의 ip주소로 사용한다.
            foreach (IPAddress addr in he.AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    thisAddress = addr;
                    break;
                }
            }

            // 주소가 없을때
            if (thisAddress == null)
                thisAddress = IPAddress.Loopback;// 로컬호스트 주소(Loop back주소)를 사용한다.
        }

        //server와의 연결을 해제하는 메소드
        private void DisConnectServer()
        {
            if (mainSocket.Connected)
            {
                SendMessaage("close");
                MessageBox.Show("접속종료");
                mainSocket.Shutdown(SocketShutdown.Both);
                mainSocket.Close();
            }
        }

        //서버와 연결하는 메소드
        private void ConnectServer(string i, string p)
        {
            string ip = i;
            string port = p;

            //서버와 연결을 위한 socket생성
            mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            //예외처리
            try { mainSocket.Connect(ip, Convert.ToInt32(port)); }
            catch (Exception ex)
            {
                MessageBox.Show("연결에 실패했습니다!");
                return;
            }
            mainSocket.BeginReceive(data, 0, size, 0, ReceiveData, mainSocket);
            MessageBox.Show("서버와 연결되었습니다.");
            this.Size = new Size((col * 10 + 35) * 2, row * 10 + 190);
            this.MinimumSize = new Size((col * 10 + 35) * 2, row * 10 + 190);
            this.MaximumSize = new Size((col * 10 + 35) * 2, row * 10 + 190);
        }

        //private void enterRoomAdmin()
        //{
        //    if (lb_Room.Items.Count > 0 && lb_Room.SelectedIndex != -1)
        //    {
        //        room_No = lb_Room.SelectedIndex;
        //        string s = (string)lb_Room.Items[lb_Room.SelectedIndex];
        //        string[] s2 = s.Split(':', ' ', 'x');

        //        point_limit = int.Parse(s2[1]);
        //        gen_limit = int.Parse(s2[3]);
        //        if (!(row == int.Parse(s2[5]) && col == int.Parse(s2[6]))) //행과열이 다를 때 새로운 form생성
        //        {
        //            Form1 f = new Form1(int.Parse(s2[5]), int.Parse(s2[6]));
        //            if (form_No == 2)
        //            {
        //                this.Hide();
        //                f.Owner = this;
        //            }
        //            else
        //            {
        //                f.Owner = this.Owner;
        //                this.Close();
        //            }
        //            f.Show();
        //            f.setlb_bt_size_position(false);
        //            f.setlb_bt_size_position2(true);
        //        }
        //        else //row와 col이 같을 때
        //        {
        //            리셋ToolStripMenuItem_Click(null, null);
        //            setlb_bt_size_position(false);
        //            setlb_bt_size_position2(true);
        //        }
        //    }
        //}

        //서버에서 데이터를 받았을 때 발생하는 이벤트를 처리하는 부분(프로토콜)
        private void ReceiveData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            if (!remote.Connected)
            {
                return;
            }
            int recv = remote.EndReceive(iar);
            //if (recv <= 0)
            //{
            //    remote.Close();
            //    return;
            //}
            string stringData = Encoding.UTF8.GetString(data, 0, recv);

            //서버가 종료되었다는 데이터를 받으면 소켓을 닫아준다.
            if (stringData == "Closed")
            {
                MessageBox.Show("서버가 종료되었습니다.");
                mainSocket.Close();
                return;
            }
            else if (stringData.Contains("CreateRoom "))
            {
                string[] s = stringData.Replace("CreateRoom ", "").Split(' ');
                lb_Room.Items.Add("point:" + s[0] + " gen:" + s[1] + " size:" + s[2] + "x" + s[3] + " user:" + "1");
            }
            else if (stringData.Contains("USER JOIN THE ROOM"))
            {
                tb_chat.AppendText(stringData + "\n");
            }
            else if (stringData.Contains("USER OUT THE ROOM"))
            {
                tb_chat.AppendText(stringData + "\n");
            }
            else if (stringData.Contains("ALREADY! GAME! START"))
            {
                if (MessageBox.Show("게임이 이미 시작되었습니다.\n잠시후 다시 입장해주세요.") == DialogResult.OK)
                {
                    room_No = -1;
                    setlb_bt_size_position(true);
                    setlb_bt_size_position2(false);
                }
            }
            //else if (stringData.Contains("ERAdmin"))
            //{
            //    //enterRoomAdmin();
            //}
            else if (stringData.Contains("OR"))
            {
                int n = int.Parse(stringData.Remove(0, 3));
                int u = int.Parse(stringData.Remove(1, 3));
                string s = (((string)lb_Room.Items[n]).Remove(((string)lb_Room.Items[n]).Length - 1, 1));
                lb_Room.Items[n] = s + u;
            }
            else if (stringData.Contains("ER"))
            {
                int n = int.Parse(stringData.Remove(0, 3));
                int u = int.Parse(stringData.Remove(1, 3));
                string s = (((string)lb_Room.Items[n]).Remove(((string)lb_Room.Items[n]).Length - 1, 1));
                lb_Room.Items[n] = s + u;
            }
            else if (stringData.Contains('n'))
            {
                lb_Room.Items.Clear();
                string[] s = stringData.Split('n');

                foreach (var t in s)
                {
                    if (t.Length > 5)
                    {
                        string[] s2 = t.Split(' ');
                        lb_Room.Items.Add("point:" + s2[0] + " gen:" + s2[1] + " size:" + s2[2] + "x" + s2[3] + " user:" + s2[4]);
                    }
                }
                //lb_Room.Items.RemoveAt(0);
            }
            mainSocket.BeginReceive(data, 0, size, 0, ReceiveData, mainSocket);
        }

        //Server로 데이터를 전송하는 부분
        private void SendMessaage(string msg)
        {
            byte[] message = Encoding.UTF8.GetBytes(msg);
            mainSocket.Send(message);
        }




        //MenuItem클릭시 발생하는 이벤트처리하는 부분
        private void 접속ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetServer_IP dlg = new SetServer_IP();
            //dlg의 DialogResult가 OK라면 연결해준다.
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (!mainSocket.Connected)
                {
                    ConnectServer(server_IP, server_Port);
                    setlb_bt_size_position(true);
                }
                else
                {
                    MessageBox.Show("이미 연결되어있습니다.");
                }
            }
        }

        private void 접속해제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainSocket.Connected)
            {
                DisConnectServer();
                this.Size = new Size((col * 10 + 35), row * 10 + 110);
                this.MinimumSize = new Size((col * 10 + 35), row * 10 + 150);
                this.MaximumSize = new Size((col * 10 + 35), row * 10 + 150);
            }
            else
            {
                MessageBox.Show("서버에 연결되어있지 않습니다.");
            }
        }
    }
}