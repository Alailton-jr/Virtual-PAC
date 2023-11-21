using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestSet;

namespace Ied
{
    public partial class IecSvForm : Form
    {
        public IecSvForm()
        {
            InitializeComponent();
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open File Dialog";
            openFileDialog1.Filter = "ICD Files (*.icd)|*.icd|SCD Files (*.scd)|*.scd|SCL Files (*.scl)|*.scl|CID Files (*.cid)|*.cid|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1; // Set the default filter index to .icd
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                var form = new ImportSclForm();
                var data = form.openExt(selectedFilePath, ImportSclForm.formType.SampledValue);
                if (data == null)
                    MessageBox.Show("Não foi possível abrir o arquivo.");
                else
                {
                    TbSvAppID.Text = "  " + data.appId;
                    TbSvId.Text = "  " + data.svId;
                    TbSvMacDest.Text = "  " + data.macDst;
                    TbSVNoAsdu.Text = "  " + data.nofASDU.ToString();
                    TbSvRate.Text = "  " + data.smpRate.ToString();
                    TbSvRev.Text = "  " + data.confRev.ToString();
                    TbSvVLan.Text = "  " + data.vlanid;
                    TbVLanPriority.Text = "  " + data.vlanPriority;
                    DgvSend.Rows.Clear();
                    foreach (var fcda in data.fcdaList)
                    {
                        DgvSend.Rows.Add(fcda.ldInst, fcda.lnClass, fcda.lnInst, fcda.doName, fcda.daName, fcda.daName == "q" ? "Quality" : "INT32");
                    }
                }
            }

        }
    }
}
