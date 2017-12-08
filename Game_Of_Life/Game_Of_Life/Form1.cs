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
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Game_Of_Life
{
    public partial class Form1 : Form
    {
        public static int form_No = 0;
        static int row = 0;
        static int col = 0;
        static bool start = false;
        static Panel[,] p;
        static int[,] p2;
        static int gen_sec = 1000;
        static bool mouseDown = false;

        static int point;
        static int generation;
        static int point_limit;
        static int gen_limit;
        static int room_No;
        static Setsize_dlg f;

        static ListBox lb_Room;
        static Button bt_creRoom = new Button();
        static Button bt_enter = new Button();

        static TextBox tb_chat;
        static Button bt_ready = new Button();
        static Button bt_exit = new Button();

        static Label lb_gen;
        static Label lb_point;

        static private Socket mainSocket; //Socket통신에 사용되는 변수
        static private byte[] data = new byte[1024]; //고정길이로 데이터를 보내기위한 배열
        static private int size = 1024; //고정길이 1024
        static IPAddress thisAddress; //클라이언트(계산기)의 IP를 저장하기위한 변수
        static public string server_IP, server_Port;
        static bool game_start;

        static Form1 old_Form;

        int local_form_num;

        static Form1()
        {
            game_start = false;
            lb_point = new Label();
            lb_gen = new Label();
            tb_chat = new TextBox();
            lb_Room = new ListBox();
            room_No = -1;
        }

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
            set_Form_Size1();
            

            bt_creRoom.Text = "방만들기";
            bt_enter.Text = "입장";
            bt_creRoom.Size = new Size(80, 20);
            bt_enter.Size = new Size(40, 20);

            bt_ready.Text = "Ready";
            bt_exit.Text = "exit";
            bt_ready.Size = new Size(80, 20);
            bt_exit.Size = new Size(80, 20);
            tb_chat.Multiline = true;
            tb_chat.Enabled = false;

            if (form_No == 1) //static이라 여러번 이벤트핸들러를 등록하면여러번 처리되서 에러난다.
            {
                bt_creRoom.Click += bt_creRoom_click;
                bt_enter.Click += bt_enter_click;
                bt_ready.Click += bt_ready_click;
                bt_exit.Click += bt_exit_click;
            }

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
            this.Controls.Add(lb_point);

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
                set_Form_Size2();
            }
        }
        private void set_Form_Size1()
        {
            this.Size = new Size((col * 10 + 35), row * 10 + 190);
            this.MinimumSize = new Size((col * 10 + 35), row * 10 + 190);
            this.MaximumSize = new Size((col * 10 + 35), row * 10 + 190);
        }
        private void set_Form_Size2()
        {
            this.Size = new Size((col * 10 + 35) * 2, row * 10 + 190);
            this.MinimumSize = new Size((col * 10 + 35) * 2, row * 10 + 190);
            this.MaximumSize = new Size((col * 10 + 35) * 2, row * 10 + 190);
            setlb_bt_size_position(true);
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
                    SendMessaage("Release " + room_No);
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
                lb_Room.Items.Clear();
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
                        old_Form = f;
                    }
                    else
                    {
                        f.Owner = this.Owner;
                        old_Form.Close();
                        old_Form = f;
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
                        old_Form = f;
                    }
                    else
                    {
                        f.Owner = this.Owner;
                        old_Form.Close();
                        old_Form = f;
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
        static Thread t = null;
        private void start_Click(object sender, EventArgs e)
        {
            stopBtn.Enabled = true;
            start = true;
            t = new Thread(blink);
            t.Start();
            ((Button)sender).Enabled = false;
        }
        private static void blink()
        {
            while (true)
            {
                point = 0;
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
                        {
                            p[i, j].BackColor = Color.Black;
                            point++;
                        }
                        else if (p2[i, j] == 0 && p[i, j].BackColor == Color.Black)
                            p[i, j].BackColor = Color.White;
                        p2[i, j] = 0;
                    }
                }
                generation++;
                lb_gen.Text = "generation: " + generation;
                lb_point.Text = "Point : " + point;
                if (game_start && generation == gen_limit)
                {
                    byte[] message = Encoding.UTF8.GetBytes("GAME@OVER@" + room_No + "@" + point);
                    mainSocket.Send(message);
                    break;
                }
                Thread.Sleep(gen_sec);
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
            //171205 예외처리 owner전하는
            try
            {
                StreamReader sr = new StreamReader(new FileStream("save.txt", FileMode.Open, FileAccess.Read));
                string[] s = sr.ReadLine().Split(' ');
                point = 0;
                if (!(row == int.Parse(s[0]) && col == int.Parse(s[1]))) //행과열이 다를 때 새로운 form생성
                {

                    Form1 f = new Form1(int.Parse(s[0]), int.Parse(s[1]));
                    if (form_No == 2)
                    {
                        this.Hide();
                        f.Owner = this;
                        old_Form = f;
                    }
                    else
                    {
                        f.Owner = this.Owner;
                        old_Form.Close();
                        old_Form = f;
                    }

                    for (int i = 0; i < int.Parse(s[0]); i++)
                    {
                        for (int j = 0; j < int.Parse(s[1]); j++)
                        {
                            if (int.Parse(s[i * int.Parse(s[1]) + j + 2]) == 1)
                            {
                                p[i, j].BackColor = Color.Black;
                                point++;
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
                                point++;
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
            lb_point.Text = "Point : " + point;
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
                if (room_No != -1)
                {
                    this.bt_exit_click(bt_exit, null);
                }
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
            set_Form_Size2();
        }

        //서버에서 데이터를 받았을 때 발생하는 이벤트를 처리하는 부분(프로토콜)
        private void ReceiveData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            if (!remote.Connected)
            {
                return;
            }
            int recv = remote.EndReceive(iar);
            string stringData = Encoding.UTF8.GetString(data, 0, recv);

            //서버가 종료되었다는 데이터를 받으면 소켓을 닫아준다.
            if (stringData == "Closed")
            {
                if (room_No != -1)
                {
                    lb_Room.Items.Clear();
                    setlb_bt_size_position(true);
                    setlb_bt_size_position2(false);
                    tb_chat.Clear();
                    room_No = -1;
                }
                MessageBox.Show("서버가 종료되었습니다.");
                mainSocket.Close();
                setlb_bt_size_position(false);
                setlb_bt_size_position2(false);
                set_Form_Size1();
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
                //클라이언트에서 OVER을 보내고 Server 그대로 다시 흘러들어오게해서 에러남;;
                //OVER의 ER을 처리하려고하니 예외발생
            }
            else if (stringData.Contains("@WHOLE@GAME@END"))
            {
                string[] s = stringData.Split('@');
                if (point == int.Parse(s[0]))
                {
                    MessageBox.Show("Game Over\nYou Win!");
                }
                else if (point < int.Parse(s[0]))
                {
                    MessageBox.Show("Game Over\nYou Lose..\nBest Score of User : " + s[0]);
                }
                game_start = false;
                generation = 0;
                startBtn.Enabled = true;
                bt_exit.Enabled = true;
                bt_ready.Enabled = true;
                bt_ready.Text = "Ready";
                stopBtn.Enabled = false;
                start = false;
                lb_gen.Text = "generation : " + generation;
            }
            else if (stringData.Contains("GAME@@ START@@"))
            {
                MessageBox.Show("게임을 시작하지");
                game_start = true;
                gen_sec = 100;
                generation = 0;
                startBtn.Enabled = false;
                bt_exit.Enabled = false;
                bt_ready.Enabled = false;
                start_Click(startBtn, null);
                stopBtn.Enabled = false;
            }
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
            else if (stringData.Contains("n"))
            {
                string[] s = stringData.Split('n');

                foreach (var t in s)
                {
                    if (t.Length > 5)
                    {
                        string[] s2 = t.Split(' ');
                        //cross thread 예외 Invoke메소드로 해결
                        lb_Room.Invoke((MethodInvoker)delegate () {
                            lb_Room.Items.Add("point:" + s2[0] + " gen:" + s2[1] + " size:" + s2[2] + "x" + s2[3] + " user:" + s2[4]); });
                    }
                }
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
                set_Form_Size1();
            }
            else
            {
                MessageBox.Show("서버에 연결되어있지 않습니다.");
            }
        }
    }
}