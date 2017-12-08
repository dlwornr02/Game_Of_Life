using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class SetServer_IP : Form
    {
        public SetServer_IP()
        {
            InitializeComponent();
            this.Size = new Size(245,200);
            this.MinimumSize = new Size(245,200);
            this.MaximumSize = new Size(245,200);
        }

        private void connect_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            //((Form1)this.Owner).server_IP = tb_IP.Text;
            //((Form1)this.Owner).server_Port = tb_Port.Text;
            Form1.server_IP = tb_IP.Text;
            Form1.server_Port = tb_Port.Text;
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
