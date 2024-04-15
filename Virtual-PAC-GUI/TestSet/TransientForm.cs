
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Data;
using static System.Windows.Forms.Design.AxImporter;
using static TestSet.MainForm;
using MathNet.Numerics;

namespace TestSet
{
    public partial class TransientForm : Form
    {
        private int curSV;
        private List<Button> buttonList;
        private TransientConfig config;
        private List<ComboBox> comboBoxes;
        private List<CheckBox> checkBoxList;
        private List<string> cbOptions;
        private bool running = false;


        public TransientForm()
        {
            InitializeComponent();

            curSV = 0;
            buttonList = new List<Button>();

            if (main.continuousConfig == null)
                main.continuousConfig = new List<ContinuousConfig>() { new ContinuousConfig() };
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

            if (main.transientConfig == null)
                main.transientConfig = new List<TransientConfig>();
            while (main.transientConfig.Count < main.numSV)
                main.transientConfig.Add(new TransientConfig());

            config = main.transientConfig[curSV];

            comboBoxes = new List<ComboBox>
            {
                CbIa,
                CbIb,
                CbIc,
                CbIn,
                CbVa,
                CbVb,
                CbVc,
                CbVn
            };
            checkBoxList = new List<CheckBox>
            {
                ChkIa,
                ChkIb,
                ChkIc,
                ChkIn,
                ChkVa,
                ChkVb,
                ChkVc,
                ChkVn
            };
            addSVButtons();
            UpdateFields();
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
                main.continuousConfig.RemoveAt(idx);
                main.networkConfig.RemoveAt(idx);
                main.sequencesConfig.RemoveAt(idx);
                main.transientConfig.RemoveAt(idx);
                main.numSV--;
                if (curSV >= main.numSV)
                {
                    curSV = main.numSV - 1;
                }
                addSVButtons();
                updateGraph();
                updateGraph();
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
                config = main.transientConfig[curSV];
                UpdateFields();
                updateGraph();
            }
            else
            {
                main.continuousConfig.Add(new ContinuousConfig());
                main.networkConfig.Add(new NetworkConfig(main.networkConfig.Count));
                main.sequencesConfig.Add(SequenceConfig.defaultSequences());
                main.transientConfig.Add(new TransientConfig());

                main.numSV++;
                curSV = main.numSV - 1;
                addSVButtons();
                config = main.transientConfig[curSV];
                UpdateFields();
                updateGraph();
            }
        }

        private void UpdateFields()
        {
            TbFileName.Text = config.fileName;
            TbNDados.Text = config.nData.ToString();
            cbOptions = new List<string> { "None" };
            for (int i = 1; i < config.data.Count; i++)
                cbOptions.Add($"Data {i}");
            foreach (ComboBox cb in comboBoxes)
            {
                cb.Items.Clear();
                cb.SelectedIndex = -1;
                cb.Items.AddRange(cbOptions.ToArray());
            }
            for (int i = 0; i < 8; i++)
            {
                comboBoxes[i].SelectedIndexChanged -= CbIa_SelectedIndexChanged;
                try
                {
                    comboBoxes[i].SelectedIndex = config.setup[i] + 1;
                }
                catch (Exception)
                {
                    comboBoxes[i].SelectedIndex = 0;
                    config.setup[i] = -1;
                }
                comboBoxes[i].SelectedIndexChanged += CbIa_SelectedIndexChanged;
            }
        }

        static double[] Convolve(double[] data, double[] kernel)
        {

            // Determine the size of padding needed
            int padSize = kernel.Length - 1;
            int dataSize = data.Length;
            int resultSize = dataSize + padSize;

            // Create padded data array
            double[] paddedData = new double[resultSize];
            Array.Copy(data, 0, paddedData, padSize / 2, dataSize);

            dataSize = paddedData.Length;
            int kernelSize = kernel.Length;
            resultSize = dataSize + kernelSize - 1;
            double[] result = new double[resultSize];

            for (int i = 0; i < resultSize; i++)
            {
                result[i] = 0.0;
                for (int j = 0; j < kernelSize; j++)
                {
                    if (i - j >= 0 && i - j < dataSize)
                    {
                        result[i] += paddedData[i - j] * kernel[j];
                    }
                }
            }
            return result;
        }

        private void updateGraph()
        {
            if (config.data == null || config.data.Count == 0)
            {
                PltMain.Model = null;
                return;
            }

            List<int> selected = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                if (checkBoxList[i].Checked)
                    selected.Add(i);
            }
            if (selected.Count == 0)
            {
                PltMain.Model = null;
                return;
            }
            List<List<double>> data = new List<List<double>>();
            foreach (int i in selected)
            {
                if (config.setup[i] != -1)
                    data.Add(config.data[config.setup[i] + 1]);
            }
            if (data.Count == 0)
            {
                PltMain.Model = null;
                return;
            }
            bool plotRMS = CbkPlotRMS.Checked;
            PlotModel plotModel = new PlotModel();
            for (int i = 0; i < data.Count; i++)
            {
                LineSeries line = new LineSeries();
                line.Title = $"Data {selected[i]}";

                if (plotRMS)
                {
                    double[] squaredData = data[i].Select(x => x * x).ToArray();
                    int windowSize = (int)(1 / (config.data[0][1] - config.data[0][0]) / 60);
                    double[] kernel = Enumerable.Repeat(1.0 / windowSize, windowSize).ToArray();
                    double[] rmsData = Convolve(squaredData, kernel);
                    for (int j = 0; j < data[i].Count; j++)
                    {
                        line.Points.Add(new DataPoint(config.data[0][j], Math.Sqrt(rmsData[j])));
                    }
                }
                else
                {
                    for (int j = 0; j < data[i].Count; j++)
                    {
                        line.Points.Add(new DataPoint(config.data[0][j], data[i][j]));
                    }
                }


                plotModel.Series.Add(line);
            }
            // Set the axis limits
            double minX = config.data[0].Min();
            double maxX = config.data[0].Max();
            double minY = plotRMS ? 0 : data.SelectMany(d => d).Min();
            double maxY = plotRMS ? data.SelectMany(d => d).Max() / Math.Sqrt(2) : data.SelectMany(d => d).Max();

            // Add some margin to the limits
            double marginX = (maxX - minX) * 0.05;
            double marginY = (maxY - minY) * 0.05;
            minX -= marginX;
            maxX += marginX;
            minY -= marginY;
            maxY += marginY;
            plotModel.Title = "Transient Data";
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time (s)", Minimum = minX, Maximum = maxX });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Value", Minimum = minY, Maximum = maxY });

            // self Adjust axis limits


            PltMain.Model = plotModel;

        }

        private void BtnLoadFile_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "PsCAD Output (*.out)|*.out|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (!(openFileDialog1.ShowDialog() == DialogResult.OK))
                return;
            else
            {
                string filePath = openFileDialog1.FileName;
                if (!config.LoadDataFromFile(filePath)) MessageBox.Show("Error: Could not read file from disk. Original error: ");
                UpdateFields();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }

        private void CbIa_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox local = (ComboBox)sender;
            sbyte idx = (sbyte)local.SelectedIndex;
            config.setup[comboBoxes.IndexOf(local)] = idx - 1;
            updateGraph();
        }

        private void CbkPlotRMS_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (!running)
            {
                // Check
                bool error = false;
                for (int i = 0; i < main.transientConfig.Count; i++)
                {
                    if (String.IsNullOrEmpty(main.transientConfig[i].fileName))
                    {
                        error = true;
                        MessageBox.Show($"The Sv {main.networkConfig[i].svConfig.svId} doesn't have a file.");
                        break;
                    }
                    if (!main.transientConfig[i].setup.Any(x => x != -1))
                    {
                        error = true;
                        MessageBox.Show($"The Sv {main.networkConfig[i].svConfig.svId} doesn't have any data selected.");
                        break;
                    }
                }
                if (error) return;

                var yamlFile = main.CreateYamlTransient();
                if (yamlFile != null)
                {
                    main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);
                    string res = main.serverCon.SendData("setupTransient", yamlFile);
                    if (res == null || res == "error")
                    {
                        MessageBox.Show("It Wasn't possible to connected to vMU!");
                        return;
                    }
                    List<string> fileSent = new List<string>();
                    for (int i = 0; i < main.transientConfig.Count; i++)
                    {
                        if (fileSent.Contains(main.transientConfig[i].fileName)) continue;
                        res = main.serverCon.SendData("receiveTransientFiles", "Hello");
                        if (res == null || res == "error")
                        {
                            MessageBox.Show("It Wasn't possible to connected to vMU!");
                            return;
                        }
                        res = main.serverCon.SendFile(main.transientConfig[i].fileName);
                        fileSent.Add(main.transientConfig[i].fileName);
                        if (res == null || res == "error")
                        {
                            MessageBox.Show("It Wasn't possible to connected to vMU!");
                            return;
                        }
                        res = main.serverCon.SendData("startTransient", " ");
                        if (res == null || res == "error")
                        {
                            MessageBox.Show("It Wasn't possible to connected to vMU!");
                            return;
                        }
                        BtnStart.Text = "Stop";
                        running = true;
                    }
                }
            }
            else
            {
                main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);
                string res = main.serverCon.SendData("stopTransient", " ");
                if (res == null || res == "error")
                {
                    MessageBox.Show("It Wasn't possible to connected to vMU!");
                    return;
                }
                BtnStart.Text = "Start";
                running = false;
            }
        }

        private void CbxLoop_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var tconfig in main.transientConfig)
            {
                tconfig.loop = CbxLoop.Checked;
            }
        }
    }
}
