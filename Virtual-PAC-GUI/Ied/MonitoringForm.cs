using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using static Ied.MainForm;
using System.Security.Cryptography;
using System.Data.Common;
using System.Collections.Immutable;
using System.Reflection;
using System.Net.Sockets;
using Newtonsoft.Json;
using YamlDotNet.Core;

namespace Ied
{
    public partial class MonitoringForm : Form
    {

        CommunicationConfig communication = MainForm.main.communication;

        public MonitoringForm()
        {
            InitializeComponent();
            LoadTable();
        }


        private void LoadTable()
        {
            var names = new List<string>() { "Ia", "Ib", "Ic", "In", "Va", "Vb", "Vc", "Vn" };

            DataGVPhasorsCurrent.ColumnCount = 3;
            DataGVPhasorsCurrent.Rows.Clear();
            DataGVPhasorsCurrent.RowHeadersVisible = false;
            DataGVPhasorsCurrent.ColumnHeadersVisible = false;
            DataGVPhasorsCurrent.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGVPhasorsCurrent.AllowUserToAddRows = false;

            for (int i = 0; i < 4; i++)
            {
                DataGVPhasorsCurrent.Rows.Add(names[i], 0.ToString("0.00") + " [A]", 0.ToString("0.00") + " [°]");
            }

            int wid = DataGVPhasorsCurrent.Width / 3 - 1;
            int hei = DataGVPhasorsCurrent.Height / 3 - 1;
            for (int i = 0; i < DataGVPhasorsCurrent.ColumnCount; i++)
            {
                DataGVPhasorsCurrent.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGVPhasorsCurrent.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(84, 98, 110);
                DataGVPhasorsCurrent.Columns[i].DefaultCellStyle.ForeColor = Color.Lavender;
                DataGVPhasorsCurrent.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGVPhasorsCurrent.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(84, 98, 110);
                DataGVPhasorsCurrent.Columns[i].HeaderCell.Style.ForeColor = Color.Lavender;
                DataGVPhasorsCurrent.Columns[i].Width = wid;
            }

            int width = DataGVPhasorsCurrent.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 3;
            int height = DataGVPhasorsCurrent.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + 3;
            DataGVPhasorsCurrent.Width = width;
            DataGVPhasorsCurrent.Height = height;
            DataGVPhasorsCurrent.CurrentCell = null;
            DataGVPhasorsCurrent.Update();


            DataGVPhasorsVoltage.ColumnCount = 3;
            DataGVPhasorsVoltage.Rows.Clear();
            DataGVPhasorsVoltage.RowHeadersVisible = false;
            DataGVPhasorsVoltage.ColumnHeadersVisible = false;
            DataGVPhasorsVoltage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGVPhasorsVoltage.AllowUserToAddRows = false;

            for (int i = 4; i < 8; i++)
                DataGVPhasorsVoltage.Rows.Add(names[i], 0.ToString("0.00") + " [A]", 0.ToString("0.00") + " [°]");

            for (int i = 0; i < DataGVPhasorsCurrent.ColumnCount; i++)
            {
                DataGVPhasorsVoltage.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGVPhasorsVoltage.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(84, 98, 110);
                DataGVPhasorsVoltage.Columns[i].DefaultCellStyle.ForeColor = Color.Lavender;
                DataGVPhasorsVoltage.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGVPhasorsVoltage.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(84, 98, 110);
                DataGVPhasorsVoltage.Columns[i].HeaderCell.Style.ForeColor = Color.Lavender;
                DataGVPhasorsVoltage.Columns[i].Width = wid;
            }

            width = DataGVPhasorsVoltage.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 3;
            height = DataGVPhasorsVoltage.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + 3;
            DataGVPhasorsVoltage.Width = width;
            DataGVPhasorsVoltage.Height = height;


            DataGVPhasorsVoltage.CurrentCell = null;
            DataGVPhasorsVoltage.Update();
        }

        private bool monitoring = false;

        private void UpdateTable(List<List<double>> values)
        {
            var names = new List<string>() { "Ia", "Ib", "Ic", "In", "Va", "Vb", "Vc", "Vn" };

            if (values == null)
                return;

            var angRef = values[0][1];

            for (int i = 0; i < 4; i++)
            {
                if (values[i] != null)
                {
                    DataGVPhasorsCurrent.Rows[i].Cells[1].Value = values[i][0].ToString("0.00") + " [A]";
                    DataGVPhasorsCurrent.Rows[i].Cells[2].Value = ((values[i][1] - angRef) * 180 / 3.1415926).ToString("0.00") + " [°]";
                }
                else
                {
                    DataGVPhasorsCurrent.Rows[i].Cells[1].Value = 0.ToString("0.00") + " [A]";
                    DataGVPhasorsCurrent.Rows[i].Cells[2].Value = 0.ToString("0.00") + " [°]";
                }
            }
            for (int i = 4; i < 8; i++)
            {

                if (values[i] != null)
                {
                    DataGVPhasorsVoltage.Rows[i - 4].Cells[1].Value = values[i][0].ToString("0.00") + " [A]";
                    DataGVPhasorsVoltage.Rows[i - 4].Cells[2].Value = ((values[i][1] - angRef) * 180 / 3.1415926).ToString("0.00") + " [°]";
                }
                else
                {
                    DataGVPhasorsVoltage.Rows[i - 4].Cells[1].Value = 0.ToString("0.00") + " [A]";
                    DataGVPhasorsVoltage.Rows[i - 4].Cells[2].Value = 0.ToString("0.00") + " [°]";
                }

            }
        }


        private Thread monitoringThread;

        private void BtnStartMonitor_Click(object sender, EventArgs e)
        {
            if (!monitoring)
            {
                BtnStartMonitor.Text = "Parar";
                monitoring = true;
                monitoringThread = new Thread(StartMonitoring);
                monitoringThread.Start();
            }
            else
            {
                BtnStartMonitor.Text = "Iniciar";
                monitoring = !monitoring;
            }

        }

        private void StartMonitoring()
        {
            main.serverCon.ConnectionLost += (sender, e) =>
            {
                monitoring = false;
            };
            main.serverCon.changeConProperties(main.communication.ip, main.communication.port);
            while (monitoring)
            {
                string res = main.serverCon.SendData("getCurrentValues", "all");
                if (res != null)
                {
                    var values = JsonConvert.DeserializeObject<List<List<double>>>(res);
                    if (values != null)
                    {
                        if (monitoring) UpdateTable(values);
                    }  
                }
                Thread.Sleep(1000);
            }
        }

        private void UpdateButtonText(object state)
        {
            string newText = (string)state;

            if (BtnStartMonitor.InvokeRequired)
            {
                // Call the method on the main UI thread using Invoke
                BtnStartMonitor.Invoke(new Action<string>(UpdateButtonText), newText);
            }
            else
            {
                // Access or modify the button text directly from the main UI thread
                BtnStartMonitor.Text = newText;
            }
        }

    }
}
