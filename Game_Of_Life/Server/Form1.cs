using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    //public partial class Form1 : Form
    //{
    //    Socket server;
    //    public Form1()
    //    {
    //        InitializeComponent();

    //    }
    //    private void Setip
    //    private void AcceptConn(IAsyncResult iar)
    //    {
    //        Socket oldserver = null;
    //        try
    //        {
    //            oldserver = (Socket)iar.AsyncState;
    //            client 
    //        }
    //    }
    public partial class Form1 : Form

    {
        private byte[] data = new byte[1024];
        private int size = 1024;
        private Socket mainSocket = null;
        List<Socket> connectedClients = new List<Socket> { };
        IPAddress thisAddress;
        delegate void AppendTextDelegate(Control ctrl, string s);
        AppendTextDelegate _textAppender;
        public Form1()
        {
            InitializeComponent();
            SetIp();
            mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _textAppender = new AppendTextDelegate(AppendText);
        }
        private void SetIp()
        {
            IPHostEntry he = Dns.GetHostEntry(Dns.GetHostName());
            // 처음으로 발견되는 ipv4 주소를 사용한다.
            foreach (IPAddress addr in he.AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    thisAddress = addr;
                    break;
                }
            }
            // 주소가 없다면..
            if (thisAddress == null)
                thisAddress = IPAddress.Loopback;// 로컬호스트 주소를 사용한다.
            txtIp.Text = thisAddress.ToString();
        }
        private void btStart_Click(object sender, EventArgs e)
        {
            startServer();
        }
        private void btStop_Click(object sender, EventArgs e)
        {
            StopServer();
        }
        public void StopServer()
        {
            if (this.mainSocket != null || this.mainSocket.Connected)
            {
                try
                {
                    this.mainSocket.Shutdown(SocketShutdown.Both);
                }
                catch (SocketException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            mainSocket.Close();
        }
        public void startServer()
        {
            int port;
            if (!int.TryParse(txtPort.Text, out port))
            {
                MessageBox.Show("포트 번호가 잘못 입력되었거나 입력되지 않았습니다.");
                txtPort.Focus();
                txtPort.SelectAll();
                return;
            }
            IPEndPoint iep = new IPEndPoint(thisAddress, port);
            mainSocket.Bind(iep);
            mainSocket.Listen(5);
            mainSocket.BeginAccept(AcceptConn, null);
            txtServerLog.Text += "Server Start!";
        }
        private void AcceptConn(IAsyncResult ar)
        {
            // 클라이언트의 연결 요청을 수락한다.
            Socket client = mainSocket.EndAccept(ar);
            // 또 다른 클라이언트의 연결을 대기한다.
            mainSocket.BeginAccept(AcceptConn, null);
            // 연결된 클라이언트 리스트에 추가해준다.
            connectedClients.Add(client);
            // 텍스트박스에 클라이언트가 연결되었다고 써준다.
            AppendText(txtServerLog, string.Format("클라이언트 (@ {0})가 연결되었습니다.", client.RemoteEndPoint));
            // 클라이언트의 데이터를 받는다.
            client.BeginReceive(data, 0, size, 0, ReceiveData, client);
        }
        private void SendData(IAsyncResult iar)
        {
            Socket client = (Socket)iar.AsyncState;
            int sent = client.EndSend(iar);
            client.BeginReceive(data, 0, size, SocketFlags.None, ReceiveData, client);
        }
        private void ReceiveData(IAsyncResult iar)
        {
            Socket client = (Socket)iar.AsyncState;
            if (!client.Connected)
                return;
            int recv = client.EndReceive(iar);
            if (recv <= 0)
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                return;
            }
            string recvData = Encoding.UTF8.GetString(data, 0, recv);
            AppendText(this.txtServerLog, client.RemoteEndPoint +  " : " + recvData);
            int client_n = connectedClients.IndexOf(client)+1;

            byte[] message2 = Encoding.UTF8.GetBytes(recvData);
            foreach (var clients in this.connectedClients)
            {
                if (clients != client)
                {
                    clients.Send(message2);
                }
            }

            //client.BeginSend(message2, 0, message2.Length, SocketFlags.None, SendData, client);
            message2 = Encoding.UTF8.GetBytes("");
            client.BeginSend(message2, 0, message2.Length, SocketFlags.None, SendData, client);
        }
        void AppendText(Control ctrl, string s)
        {
            if (ctrl.InvokeRequired) ctrl.Invoke(_textAppender, ctrl, s);
            else
            {
                string source = ctrl.Text;
                ctrl.Text = source + Environment.NewLine + s;
            }
        }
        private void txtSend_KeyUp(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessaage();
            }
        }
        private void SendMessaage()
        {
            byte[] message = Encoding.UTF8.GetBytes(txtSend.Text.Trim());
            foreach (var client in this.connectedClients)
            {
                client.Send(message);
            }
            AppendText(this.txtServerLog, txtSend.Text.Trim());
            txtSend.Clear();

        }
        private void btSend_Click(object sender, EventArgs e)
        {
            SendMessaage();
        }
    }
}
