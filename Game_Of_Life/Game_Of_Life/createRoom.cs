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
    public partial class createRoom : Form
    {
        static public int point, gen, row, col;

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void create_Click(object sender, EventArgs e)
        {
            if(int.TryParse(tb_Point.Text,out point) && int.TryParse(tb_gen.Text, out gen) 
                &&int.TryParse(tb_row.Text, out row) && int.TryParse(tb_col.Text, out col))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("값을 다시입력하세요!");
            }
        }

        public createRoom()
        {
            InitializeComponent();
            this.Size = new Size(255,225);
            this.MinimumSize = new Size(255,225);
            this.MaximumSize = new Size(255,225);
        }
    }
}
