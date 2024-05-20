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
    public partial class ProdistForm : Form
    {

        List<CheckBox> cbxList;
        private ProdistFormPanels panels;

        public ProdistForm()
        {
            InitializeComponent();

            cbxList = new List<CheckBox>()
            {
                CbxVarVolt,
            };

            panels = new ProdistFormPanels();

        }

        private void CbxVarVolt_CheckStateChanged(object sender, EventArgs e)
        {
            var local = sender as CheckBox;

            if (local.Checked != true)
            {
                local.CheckStateChanged -= CbxVarVolt_CheckStateChanged;
                local.Checked = true;
                local.CheckStateChanged += CbxVarVolt_CheckStateChanged;
                return;
            }

            foreach(var cbx in cbxList)
            {
                if (cbx != local)
                {
                    cbx.CheckStateChanged -= CbxVarVolt_CheckStateChanged;
                    cbx.Checked = false;
                    cbx.CheckStateChanged += CbxVarVolt_CheckStateChanged;
                }
            }
            
            if (local == CbxVarVolt)
            {
                PnMain.Controls.Add(panels.varVoltage);
                panels.varVoltage.Dock = DockStyle.Fill;
                panels.varVoltage.BringToFront();
                panels.varVoltage.Visible = true;
            }


        }
    }
}
