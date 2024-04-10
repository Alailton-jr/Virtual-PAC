using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quality
{
    public partial class SnifferForm : Form
    {
        public SnifferForm()
        {
            InitializeComponent();
            CbxVtcd_CheckedChanged(CbxGeneral, null);
        }

        private void CbxVtcd_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox local = (CheckBox)sender;
            if (local.Tag == null) return;

            CbxGeneral.CheckedChanged -= CbxVtcd_CheckedChanged;
            CbxVtcd.CheckedChanged -= CbxVtcd_CheckedChanged;
            CbxVtld.CheckedChanged -= CbxVtcd_CheckedChanged;


            CbxVtcd.Checked = false;
            CbxVtld.Checked = false;
            CbxGeneral.Checked = false;

            local.Checked = true;
            if (int.Parse((string)local.Tag) == 1)
            {
                PnQuality.Controls.Add(PnGeneral);
                PnGeneral.Dock = DockStyle.Fill;
                PnGeneral.BringToFront();
            }
            else if (int.Parse((string)local.Tag) == 2)
            {
                PnQuality.Controls.Add(PnVtcd);
                PnVtcd.Dock = DockStyle.Fill;
                PnVtcd.BringToFront();
            }
            else if (int.Parse((string)local.Tag) == 3)
            {
                PnQuality.Controls.Clear();
                PnQuality.Controls.Add(PnVtld);
                PnVtld.Dock = DockStyle.Fill;
                PnVtld.BringToFront();
            }
            CbxGeneral.CheckedChanged += CbxVtcd_CheckedChanged;
            CbxVtcd.CheckedChanged += CbxVtcd_CheckedChanged;
            CbxVtld.CheckedChanged += CbxVtcd_CheckedChanged;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
