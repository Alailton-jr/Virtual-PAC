using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using static Ied.Iec61850Config;

namespace Ied
{
    public partial class IecGoose : Form
    {
        public IecGoose()
        {
            InitializeComponent();
        }

        private List<Iec61850Config.DataSetConfig> dataSetList;
        private void IecGoose_Load(object sender, EventArgs e)
        {
            if (MainForm.main.iecConf.goSendList == null) MainForm.main.iecConf.goSendList = new List<Iec61850Config.GoSend>();
            dataList = MainForm.main.iecConf.goSendList;

            if (MainForm.main.iecConf.dataSet == null) MainForm.main.iecConf.dataSet = new List<Iec61850Config.DataSetConfig>();
            dataSetList = MainForm.main.iecConf.dataSet;
            foreach (var dataSet in dataSetList)
                CbDataSet.Items.Add(dataSet.dataSetName);
            
            updateTable();
            UpdateTextBox();
        }

        private List<Iec61850Config.GoSend> dataList;
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            dataList.Add(new Iec61850Config.GoSend());
            updateTable();
            UpdateTextBox();
        }

        private void UpdateTextBox()
        {
            //if (DgvSend.CurrentCell == null) return;
            //int idx = DgvSend.CurrentCell.RowIndex; // Get the current row index
            if (currentCell >= 0 && currentCell < dataList.Count) // Check if the index is valid
            {
                TbMacDst.Text = dataList[currentCell].macDst;
                TbGoId.Text = dataList[currentCell].goId;
                TbAppId.Text = dataList[currentCell].appId.ToString();
                TbVlanId.Text = dataList[currentCell].vLanId.ToString(); // Format as hexadecimal
                TbVlanPriority.Text = dataList[currentCell].vLanPriority.ToString();
                TbMinTime.Text = $"{dataList[currentCell].minTime} ms"; // Add "ms" to minTime
                TbMaxTime.Text = $"{dataList[currentCell].maxTime} ms"; // Add "ms" to maxTime
                TbCbRef.Text = dataList[currentCell].cbRef;
                if (!dataSetList.Any(data => data.dataSetName == dataList[currentCell].dataSetName))
                    dataList[currentCell].dataSetName = "";
                CbDataSet.Text = dataList[currentCell].dataSetName;
                TbConfRev.Text = dataList[currentCell].confRef.ToString();
            }
        }

        private int currentCell;
        private void validateData()
        {
            if (currentCell == null) return;
            if (currentCell >= 0 && currentCell< DgvSend.RowCount)
            {
                //int idx = DgvSend.CurrentCell.RowIndex;
                dataList[currentCell].setMac(TbMacDst.Text);
                dataList[currentCell].goId = TbGoId.Text;
                dataList[currentCell].setAppId(TbAppId.Text);
                dataList[currentCell].setvLanId(TbVlanId.Text);
                dataList[currentCell].setvLanPriotiry(TbVlanId.Text);
                dataList[currentCell].setMinTime(TbMinTime.Text);
                dataList[currentCell].setMaxTime(TbMaxTime.Text);
                dataList[currentCell].cbRef = TbCbRef.Text;
                dataList[currentCell].dataSetName = CbDataSet.Text;
                dataList[currentCell].dataSet = dataSetList.FirstOrDefault(data => data.dataSetName == dataList[currentCell].dataSetName);
            }
            UpdateTextBox();
            updateTable();
        }

        private void TbMacDst_Validated(object sender, EventArgs e)
        {
            validateData();
        }

        private void DgvSend_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            currentCell = e.RowIndex;
            UpdateTextBox();
        }

        private void updateTable()
        {
            DgvSend.Rows.Clear();
            foreach (var data in dataList)
            {
                DgvSend.Rows.Add("", data.cbRef, data.goId, data.dataSetName);
            }
        }

        private void TbCbRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                validateData();
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (DgvSend.CurrentCell == null) return;
            if (DgvSend.CurrentCell.RowIndex >= 0)
            {
                int idx = DgvSend.CurrentCell.RowIndex;
                dataList.RemoveAt(idx);
                updateTable();
                UpdateTextBox();
            }
        }
    }
}
