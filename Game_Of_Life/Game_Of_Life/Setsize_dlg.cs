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
    public partial class Setsize_dlg : Form
    {
        int owner_num;
        static Form1 final_owner;
        public Setsize_dlg(int form_num)
        {
            InitializeComponent();
            owner_num = form_num;
            this.Size = new Size(190,230);
            this.MinimumSize = new Size(190, 230);
            this.MaximumSize = new Size(190, 230);
            rowlb.Visible = false;
            collb.Visible = false;
            rowtb.Visible = false;
            coltb.Visible = false;
        }

        private void accept_Click(object sender, EventArgs e)
        {
            int row=0;
            int col=0;
            bool acceptable=false;
            if(rbt1.Checked)
            {
                row = 20;
                col = 20;
                acceptable = true;
            }
            else if (rbt2.Checked)
            {
                row = 30;
                col = 30;
                acceptable = true;
            }
            else if (rbt3.Checked)
            {
                row = 40;
                col = 40;
                acceptable = true;
            }
            else if (rbt4.Checked)
            {
                row = 50;
                col = 50;
                acceptable = true;
            }
            else if (rbt5.Checked)
            {
                if((int.TryParse(rowtb.Text,out row)) && (int.TryParse(coltb.Text, out col)))
                {
                    acceptable = true;
                }
                else
                {
                   MessageBox.Show("사용자값을 정수로 입력해주세요");
                   
                }
            }
            else
            {
                MessageBox.Show("원하는 크기를 체크해주세요");
            }
            if(acceptable)
            {
                Form1 f = new Form1(row, col);
                this.Close();
                if (this.owner_num == 1)
                {
                    final_owner = (Form1)this.Owner;
                    ((Form)this.Owner).Hide();
                }
                else
                {
                    ((Form)this.Owner).Close();
                }
                Form1.old_Form = f;
                f.Show(final_owner);
            }
        }
        private void cancel_Click(object sender, EventArgs e)
        {

        }

        private void rbt5_CheckedChanged(object sender, EventArgs e)
        {
            if (rbt5.Checked == false)
            {
                rowlb.Visible = false;
                collb.Visible = false;
                rowtb.Visible = false;
                coltb.Visible = false;
            }
            else
            {
                rowlb.Visible = true;
                collb.Visible = true;
                rowtb.Visible = true;
                coltb.Visible = true;
            }
        }
    }
}
