using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TestSet.MainForm;
using static TestSet.NetworkConfig;

namespace TestSet
{
    public partial class continuous : Form
    {
        private ContinuousConfig config;
        private int curSV;
        private List<Button> buttonList;
        private bool running;

        public continuous()
        {
            InitializeComponent();
        }
    
        private void continuous_Load(object sender, EventArgs e)
        {
            curSV = 0;
            buttonList = new List<Button>();

            if (main.continuousConfig == null)
                main.continuousConfig = new List<ContinuousConfig>() { new ContinuousConfig()};
            while (main.continuousConfig.Count < main.numSV)
                main.continuousConfig.Add(new ContinuousConfig());

            if (main.networkConfig == null)
                main.networkConfig = new List<NetworkConfig> { new NetworkConfig(0) };
            while (main.networkConfig.Count < main.numSV)
                main.networkConfig.Add(new NetworkConfig(main.networkConfig.Count));

            if (main.sequencesConfig == null)
                main.sequencesConfig = new List<List<SequenceConfig>>() { SequenceConfig.defaultSequences() };
            while (main.sequencesConfig.Count < main.numSV)
                main.sequencesConfig.Add(SequenceConfig.defaultSequences());

            config = main.continuousConfig[curSV];

            BtnUpdate.Enabled = false;
            running = false;

            AddChangeKey();
            addSVButtons();
            updateValues();
            loadTable();

        }

        private void addSVButtons()
        {
            buttonList.Clear();
            TPnSV.Controls.Clear();
            for (int i = 0; i < main.numSV; i++)
            {
                Button newButton = new Button();
                newButton.Tag = i;
                newButton.Text = main.networkConfig[i].svConfig.svId;
                newButton.Click += changeSVPanel;
                newButton.Dock = DockStyle.Fill;
                newButton.Font = new Font("Segoe UI", 9, FontStyle.Bold);

                Panel panel = new Panel();
                panel.Dock = DockStyle.Fill;
                panel.Margin = new Padding(8, 2, 8, 2);
                panel.Controls.Add(newButton);

                Button deletButton = new Button();
                deletButton.Tag = i;
                deletButton.Text = "X"; // Add a image Here
                deletButton.Click += deleteSVPanel;
                deletButton.Dock = DockStyle.Right;
                deletButton.Size = new Size(28, 20);


                panel.Controls.Add(new Panel() { Dock = DockStyle.Right, Width = 0 });
                panel.Controls.Add(deletButton);
                panel.Controls.Add(newButton);

                TPnSV.Controls.Add(panel, i % 5, i > 4 ? 1 : 0);

                buttonList.Add(newButton);
                newButton.BringToFront();

                if (i == curSV)
                {
                    newButton.ForeColor = Color.BlueViolet;
                }
                else
                {
                    newButton.ForeColor = Color.Lavender;
                }

            }
            if (main.numSV < 10)
            {
                Button newButton = new Button();
                newButton.Tag = -1;
                newButton.Text = "+"; // Add a image Here
                newButton.Click += changeSVPanel;
                newButton.Dock = DockStyle.Fill;
                Panel panel = new Panel();
                panel.Dock = DockStyle.Fill;
                panel.Margin = new Padding(8, 2, 8, 2);
                panel.Controls.Add(newButton);
                TPnSV.Controls.Add(panel, main.numSV % 5, main.numSV > 4 ? 1 : 0);
                buttonList.Add(newButton);
            }
        }

        private void deleteSVPanel(object sender, EventArgs e)
        {
            Button local = (Button)sender;
            int idx = (int)local.Tag;
            if (idx >= 0 && main.networkConfig.Count > 1)
            {
                main.networkConfig.RemoveAt(idx);
                main.continuousConfig.RemoveAt(idx);

                main.numSV--;
                if (curSV >= main.numSV)
                {
                    curSV = main.numSV - 1;
                    config = main.continuousConfig[curSV];
                    updateValues();
                }
                addSVButtons();
            }
        }

        private void changeSVPanel(object sender, EventArgs e)
        {
            Button local = (Button)sender;
            int idx = (int)local.Tag;
            if (idx >= 0)
            {
                buttonList[curSV].ForeColor = Color.Lavender;
                curSV = idx;
                buttonList[curSV].ForeColor = Color.BlueViolet;

                config = main.continuousConfig[curSV];
                updateValues();
            }
            else
            {
                main.continuousConfig.Add(new ContinuousConfig());
                main.networkConfig.Add(new NetworkConfig(main.networkConfig.Count));

                main.numSV++;
                curSV = main.numSV - 1;
                config = main.continuousConfig[curSV];
                updateValues();
                addSVButtons();
            }
        }

        private void AddChangeKey()
        {
            TbIaMod.Validated += valueChanged;
            TbIbMod.Validated += valueChanged;
            TbIcMod.Validated += valueChanged;
            TbInMod.Validated += valueChanged;
            TbVaMod.Validated += valueChanged;
            TbVbMod.Validated += valueChanged;
            TbVcMod.Validated += valueChanged;
            TbVnMod.Validated += valueChanged;

            TbIaAng.Validated += valueChanged;
            TbIbAng.Validated += valueChanged;
            TbIcAng.Validated += valueChanged;
            TbInAng.Validated += valueChanged;
            TbVaAng.Validated += valueChanged;
            TbVbAng.Validated += valueChanged;
            TbVcAng.Validated += valueChanged;
            TbVnAng.Validated += valueChanged;

            TbIaMod.KeyPress += valueChangedKey;
            TbIbMod.KeyPress += valueChangedKey;
            TbIcMod.KeyPress += valueChangedKey;
            TbInMod.KeyPress += valueChangedKey;
            TbVaMod.KeyPress += valueChangedKey;
            TbVbMod.KeyPress += valueChangedKey;
            TbVcMod.KeyPress += valueChangedKey;
            TbVnMod.KeyPress += valueChangedKey;

            TbIaAng.KeyPress += valueChangedKey;
            TbIbAng.KeyPress += valueChangedKey;
            TbIcAng.KeyPress += valueChangedKey;
            TbInAng.KeyPress += valueChangedKey;
            TbVaAng.KeyPress += valueChangedKey;
            TbVbAng.KeyPress += valueChangedKey;
            TbVcAng.KeyPress += valueChangedKey;
            TbVnAng.KeyPress += valueChangedKey;
        }

        private void loadTable()
        {
            var names = new List<string>() { "Ia", "Ib", "Ic", "In", "Va", "Vb", "Vc", "Vn" };

            DgvPub.ColumnCount = 3;
            DgvPub.Rows.Clear();
            DgvPub.RowHeadersVisible = false;
            DgvPub.ColumnHeadersVisible = false;
            DgvPub.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DgvPub.AllowUserToAddRows = false;

            for (int i = 0; i < 4; i++)
                DgvPub.Rows.Add(names[i], 0.ToString("0.00") + " [A]", 0.ToString("0.00") + " [°]");
            for (int i = 4; i < 8; i++)
                DgvPub.Rows.Add(names[i], 0.ToString("0.00") + " [V]", 0.ToString("0.00") + " [°]");

            int width = DgvPub.Width / 3 - 1;
            int height = DgvPub.Height / 3 - 1;
            for (int i = 0; i < DgvPub.ColumnCount; i++)
            {
                DgvPub.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DgvPub.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(84, 98, 110);
                DgvPub.Columns[i].DefaultCellStyle.ForeColor = Color.Lavender;
                DgvPub.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DgvPub.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(84, 98, 110);
                DgvPub.Columns[i].HeaderCell.Style.ForeColor = Color.Lavender;
                DgvPub.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DgvPub.Columns[i].Width = width;
            }

            width = DgvPub.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 6;
            height = DgvPub.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + 6;
            DgvPub.Width = width;
            DgvPub.Height = height;
            DgvPub.CurrentCell = null;
            DgvPub.Update();
        }

        private double NormalizeAngle(double angleDegrees)
        {
            while (angleDegrees > 180)
            {
                angleDegrees -= 360;
            }

            while (angleDegrees <= -180)
            {
                angleDegrees += 360;
            }

            return angleDegrees;
        }

        private void updateTable(List<List<double>> values)
        {
            var names = new List<string>() { "Ia", "Ib", "Ic", "In", "Va", "Vb", "Vc", "Vn" };

            if (values == null)
                return;
            var angRef = values[0][1];
            for (int i = 0; i < 4; i++)
            {
                if (values[i] != null)
                {
                    DgvPub.Rows[i].Cells[1].Value = values[i][0].ToString("0.00") + " [A]";
                    DgvPub.Rows[i].Cells[2].Value = NormalizeAngle((values[i][1] - angRef) * 180 / 3.1415926).ToString("0.00") + " [°]";
                }
                else
                {
                    DgvPub.Rows[i].Cells[1].Value = 0.ToString("0.00") + " [A]";
                    DgvPub.Rows[i].Cells[2].Value = 0.ToString("0.00") + " [°]";
                }
            }
            for (int i = 4; i < 8; i++)
            {

                if (values[i] != null)
                {
                    DgvPub.Rows[i].Cells[1].Value = values[i][0].ToString("0.00") + " [A]";
                    DgvPub.Rows[i].Cells[2].Value = NormalizeAngle((values[i][1] - angRef) * 180 / 3.1415926).ToString("0.00") + " [°]";
                }
                else
                {
                    DgvPub.Rows[i].Cells[1].Value = 0.ToString("0.00") + " [A]";
                    DgvPub.Rows[i].Cells[2].Value = 0.ToString("0.00") + " [°]";
                }

            }
            DgvPub.CurrentCell = null;
        }

        private void validateValues()
        {
            config.data[0].setValueMod(TbIaMod.Text);
            config.data[0].setValueAng(TbIaAng.Text);

            if (currentRotation == valuesRotation.normal)
            {
                config.data[1].setValueMod(TbIbMod.Text);
                config.data[1].setValueAng(TbIbAng.Text);

                config.data[2].setValueMod(TbIcMod.Text);
                config.data[2].setValueAng(TbIcAng.Text);
            }
            else if (currentRotation == valuesRotation.postive)
            {
                config.data[1].Mod = config.data[0].Mod;
                config.data[1].Ang = config.data[0].Ang;
                config.data[2].Mod = config.data[0].Mod;
                config.data[2].Ang = config.data[0].Ang;
                config.data[1].Ang -= 120;
                config.data[2].Ang += 120;
            }
            else if (currentRotation == valuesRotation.negative)
            {
                config.data[1].Mod = config.data[0].Mod;
                config.data[1].Ang = config.data[0].Ang;
                config.data[2].Mod = config.data[0].Mod;
                config.data[2].Ang = config.data[0].Ang;
                config.data[1].Ang += 120;
                config.data[2].Ang -= 120;
            }

            config.data[3].setValueMod(TbInMod.Text);
            config.data[3].setValueAng(TbInAng.Text);


            config.data[4].setValueMod(TbVaMod.Text);
            config.data[4].setValueAng(TbVaAng.Text);

            if (voltageRotation == valuesRotation.normal)
            {
                config.data[5].setValueMod(TbVbMod.Text);
                config.data[5].setValueAng(TbVbAng.Text);

                config.data[6].setValueMod(TbVcMod.Text);
                config.data[6].setValueAng(TbVcAng.Text);
            }
            else if (voltageRotation == valuesRotation.postive)
            {
                config.data[5].Mod = config.data[4].Mod;
                config.data[5].Ang = config.data[4].Ang;
                config.data[6].Mod = config.data[4].Mod;
                config.data[6].Ang = config.data[4].Ang;
                config.data[5].Ang -= 120;
                config.data[6].Ang += 120;
            }
            else if (voltageRotation == valuesRotation.negative)
            {
                config.data[5].Mod = config.data[4].Mod;
                config.data[5].Ang = config.data[4].Ang;
                config.data[6].Mod = config.data[4].Mod;
                config.data[6].Ang = config.data[4].Ang;
                config.data[5].Ang += 120;
                config.data[6].Ang -= 120;
            }


            config.data[7].setValueMod(TbVnMod.Text);
            config.data[7].setValueAng(TbVnAng.Text);
        }

        private void updateValues()
        {

            TbIaMod.Text = config.data[0].Mod.ToString("0.00") + " A";
            TbIaAng.Text = config.data[0].Ang.ToString("0.0");

            TbIbMod.Text = config.data[1].Mod.ToString("0.00") + " A";
            TbIbAng.Text = config.data[1].Ang.ToString("0.0");

            TbIcMod.Text = config.data[2].Mod.ToString("0.00") + " A";
            TbIcAng.Text = config.data[2].Ang.ToString("0.0");

            TbInMod.Text = config.data[3].Mod.ToString("0.00") + " A";
            TbInAng.Text = config.data[3].Ang.ToString("0.0");

            TbVaMod.Text = config.data[4].Mod.ToString("0.00") + " V";
            TbVaAng.Text = config.data[4].Ang.ToString("0.0");

            TbVbMod.Text = config.data[5].Mod.ToString("0.00") + " V";
            TbVbAng.Text = config.data[5].Ang.ToString("0.0");

            TbVcMod.Text = config.data[6].Mod.ToString("0.00") + " V";
            TbVcAng.Text = config.data[6].Ang.ToString("0.0");

            TbVnMod.Text = config.data[7].Mod.ToString("0.00") + " V";
            TbVnAng.Text = config.data[7].Ang.ToString("0.0");

        }

        private void valueChanged(object sender, EventArgs e)
        {
            validateValues();
            updateValues();
        }
        
        private void valueChangedKey(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                validateValues();
                updateValues();
            }
        }

        private enum valuesRotation
        {
            normal = 0,
            postive,
            negative
        }
        private valuesRotation currentRotation;
        private valuesRotation voltageRotation;

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentRotation = valuesRotation.normal;

            TbIbMod.Enabled = true;
            TbIcMod.Enabled = true;
            TbIbAng.Enabled = true;
            TbIcAng.Enabled = true;
            validateValues();
            updateValues();
        }

        private void RotPositiveoolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentRotation = valuesRotation.postive;

            TbIbMod.Enabled = false;
            TbIcMod.Enabled = false;
            TbIbAng.Enabled = false;
            TbIcAng.Enabled = false;
            validateValues();
            updateValues();
        }

        private void RotNegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentRotation = valuesRotation.negative;

            TbIbMod.Enabled = false;
            TbIcMod.Enabled = false;
            TbIbAng.Enabled = false;
            TbIcAng.Enabled = false;
            validateValues();
            updateValues();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            voltageRotation = valuesRotation.normal;

            TbVbMod.Enabled = true;
            TbVcMod.Enabled = true;
            TbVbAng.Enabled = true;
            TbVcAng.Enabled = true;
            validateValues();
            updateValues();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            voltageRotation = valuesRotation.postive;

            TbVbMod.Enabled = false;
            TbVcMod.Enabled = false;
            TbVbAng.Enabled = false;
            TbVcAng.Enabled = false;
            validateValues();
            updateValues();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

            voltageRotation = valuesRotation.negative;

            TbVbMod.Enabled = false;
            TbVcMod.Enabled = false;
            TbVbAng.Enabled = false;
            TbVcAng.Enabled = false;
            validateValues();
            updateValues();

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            main.serverCon.ConnectionLost += ConnectionStopHandler;
            if (running)
            {
                running = false;
                BtnStart.Text = "Iniciar";
                BtnUpdate.Enabled = false;
                DgvPub.Enabled = false;
                //if (getValuesThread != null)
                    //getValuesThread.Join();

                // Send Stop continuous
                main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);
                string res = main.serverCon.SendData("stopContinuous", "All");
                if (res == null || res == "error") MessageBox.Show("It Wasn't possible to connected to vMU!");
            }
            else
            {
                // Send Yaml Continuos File

                var yamlFile = main.CreateYamlContinuous();
                if (yamlFile != null)
                {
                    main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);
                    string res = main.serverCon.SendData("setupContinuous", yamlFile);
                    if (res == null || res == "error")
                    {
                        MessageBox.Show("It Wasn't possible to connected to vMU!");
                        return;
                    }
                    else
                    {
                        res = main.serverCon.SendData("startContinous", "All");
                        if (res == null || res == "error")
                        {
                            MessageBox.Show("It Wasn't possible to connected to vMU!");
                            return;
                        }
                        else
                        {
                            //getValuesThread = new Thread(getValues);
                            //getValuesThread.Start();
                        }
                    }
                }
                running = true;
                BtnStart.Text = "Parar";
                BtnUpdate.Enabled = true;
                DgvPub.Enabled = true;
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            var yamlFile = main.CreateYamlContinuous();
            if (yamlFile != null)
            {
                main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);
                string res = main.serverCon.SendData("setupContinuous", yamlFile);
                if (res == null) MessageBox.Show("It Wasn't possible to connected to vMU!");
                else
                {
                    res = main.serverCon.SendData("updateContinuous", "All");
                    if (res == null) MessageBox.Show("It Wasn't possible to connected to vMU!");
                }
            }
        }

        //private Thread getValuesThread;
        
        //private void getValues()
        //{
        //    while (running)
        //    {
        //        try
        //        {
        //            main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);
        //            string res = main.serverCon.SendData("continousValues", "All");
        //            if (res != null && res != "error")
        //            {
        //                var values = JsonConvert.DeserializeObject<List<List<double>>>(res);
        //                if (values == null)
        //                    return;

        //                if (running) DgvPub.Invoke(new Action(() => updateTable(values)));
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //        Thread.Sleep(1000);
        //    }
        //}
        
        private void UpdateUiControls(bool enable)
        {
            if (BtnStart.InvokeRequired || BtnUpdate.InvokeRequired || DgvPub.InvokeRequired)
            {
                // We are not on the UI thread; invoke the method on the UI thread.
                this.Invoke(new Action(() => UpdateUiControls(enable)));
            }
            else
            {
                // We are on the UI thread; update the controls.
                BtnStart.Text = enable ? "Parar" : "Iniciar";
                BtnUpdate.Enabled = enable;
                DgvPub.Enabled = enable;
                running = enable;
            }
        }

        private void ConnectionStopHandler(object? sender, EventArgs e)
        {
            UpdateUiControls(false);
        }

        private void continuous_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;
            if (main.serverCon != null)
            {
                main.serverCon.ConnectionLost -= ConnectionStopHandler;
                main.serverCon.closeConnection();
            }
        }
    }
}
