using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _171109p
{
    public partial class customDig : Form
    {
        public customDig(string labeltext)
        {
            InitializeComponent();
            
            this.Size = new Size(labeltext.Length * 15, 130);
            this.MinimumSize = new Size(labeltext.Length*20, 130);
            this.MaximumSize = new Size(labeltext.Length*20, 130);
            message.Text = labeltext;
            message.Location = new Point(this.Width / 2 - labeltext.Length * 5,9);
            cancel.Location = new Point(this.Width/2-50,this.Height-70);
        }
    }
}
