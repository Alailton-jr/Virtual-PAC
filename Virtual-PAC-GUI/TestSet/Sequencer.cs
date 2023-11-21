using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static TestSet.MainForm;
using Newtonsoft.Json;
using static TestSet.YamlSequencer;

namespace TestSet
{
    public partial class Sequencer : Form
    {
        List<SequenceConfig> setup = MainForm.main.sequencesConfig;

        private enum TextMode
        {
            normal = 0,
            positiva,
            negativa
        }
        public Sequencer()
        {

            InitializeComponent();
            tbIaMod.ContextMenuStrip = contextMenuStripCurrent;
            tbIaAng.ContextMenuStrip = contextMenuStripCurrent;
            tbIbMod.ContextMenuStrip = contextMenuStripCurrent;
            tbIbAng.ContextMenuStrip = contextMenuStripCurrent;
            tbIcMod.ContextMenuStrip = contextMenuStripCurrent;
            tbIcAng.ContextMenuStrip = contextMenuStripCurrent;
            tbInMod.ContextMenuStrip = contextMenuStripCurrent;
            tbInAng.ContextMenuStrip = contextMenuStripCurrent;

        }

        private void SequencerLoad(object sender, EventArgs e)
        {
            //setup.Clear();

            tbIaAng.Validated += CurrentTextValidation;
            tbIaMod.Validated += CurrentTextValidation;
            tbIaAng.KeyPress += CurrentTextValidationKey;
            tbIaMod.KeyPress += CurrentTextValidationKey;

            tbVaAng.Validated += VoltageTextValidation;
            tbVaMod.Validated += VoltageTextValidation;
            tbVaAng.KeyPress += VoltageTextValidationKey;
            tbVaMod.KeyPress += VoltageTextValidationKey;

            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.Margin = new Padding(0);

            TableFormat();

            updateTable();
            updateGraph();


        }

        private void TableFormat()
        {

            //tableSequence.RowHeadersVisible = false;
            tableSequence.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tableSequence.AllowUserToAddRows = false;

            tableSequence.Columns.Clear();

            var wid = tableSequence.Width - 2;
            var column = new DataGridViewTextBoxColumn();
            column.HeaderText = " ";
            column.Name = "Enabled";
            column.Width = (int)(wid * (0.5 / 10.0));

            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.BackColor = Color.FromArgb(84, 98, 110);
            column.DefaultCellStyle.ForeColor = Color.Lavender;

            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.HeaderCell.Style.BackColor = Color.FromArgb(84, 98, 110);
            column.HeaderCell.Style.ForeColor = Color.Lavender;

            tableSequence.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Nome do Teste";
            column.Name = "Nome do Teste";

            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.BackColor = Color.FromArgb(84, 98, 110);
            column.DefaultCellStyle.ForeColor = Color.Lavender;

            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.HeaderCell.Style.BackColor = Color.FromArgb(84, 98, 110);
            column.HeaderCell.Style.ForeColor = Color.Lavender;

            column.Width = (int)(wid * (7.5 / 10.0));
            tableSequence.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Duração";
            column.Name = "Duração";

            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.BackColor = Color.FromArgb(84, 98, 110);
            column.DefaultCellStyle.ForeColor = Color.Lavender;

            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.HeaderCell.Style.BackColor = Color.FromArgb(84, 98, 110);
            column.HeaderCell.Style.ForeColor = Color.Lavender;

            column.Width = (int)(wid * (2.0 / 10.0));
            tableSequence.Columns.Add(column);

            updateTable();

        }

        private void updateTable()
        {
            tableSequence.Rows.Clear();
            for (int i = 0; i < setup.Count; i++)
            {
                var row = new DataGridViewRow();
                var cell1 = new DataGridViewButtonCell();
                cell1.Value = "X";
                row.Cells.Add(cell1);
                row.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                var cell2 = new DataGridViewTextBoxCell();
                cell2.Value = setup[i].name;
                row.Cells.Add(cell2);
                row.Cells[1].ReadOnly = true;
                row.Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                var cell3 = new DataGridViewTextBoxCell();
                cell3.Value = setup[i].time.ToString();
                row.Cells.Add(cell3);
                row.Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                row.Cells[2].ReadOnly = true;

                tableSequence.Rows.Add(row);
                tableSequence.Update();
            }

        }

        public void addTable(int i)
        {
            var row = new DataGridViewRow();
            var cell1 = new DataGridViewButtonCell();
            cell1.Value = "X";
            row.Cells.Add(cell1);
            row.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            var cell2 = new DataGridViewTextBoxCell();
            cell2.Value = setup[i].name;
            row.Cells.Add(cell2);
            row.Cells[1].ReadOnly = true;
            row.Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            var cell3 = new DataGridViewTextBoxCell();
            cell3.Value = setup[i].time.ToString();
            row.Cells.Add(cell3);
            row.Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            row.Cells[2].ReadOnly = true;

            tableSequence.Rows.Add(row);
            tableSequence.Update();
        }

        private int TextValidate(System.Windows.Forms.TextBox tb)
        {
            if (double.TryParse(tb.Text, out var value))
                return 0;
            else
                return 1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = tbNameSequence.Text;
            int lastDigit = 0;
            while (setup.Exists(obj => obj.name == name))
            {
                if (char.IsDigit(name[^1]))
                {
                    lastDigit += 1;
                    name = name.Remove(name.Length - 1) + lastDigit;
                }
                else
                    name += '1';
            }

            #region Validation

            int vali = 0;
            vali += TextValidate(tbIaMod);
            vali += TextValidate(tbIbMod);
            vali += TextValidate(tbIcMod);
            vali += TextValidate(tbInMod);
            vali += TextValidate(tbVaMod);
            vali += TextValidate(tbVbMod);
            vali += TextValidate(tbVcMod);
            vali += TextValidate(tbVnMod);

            vali += TextValidate(tbIaMod);
            vali += TextValidate(tbIbAng);
            vali += TextValidate(tbIcAng);
            vali += TextValidate(tbInAng);
            vali += TextValidate(tbVaAng);
            vali += TextValidate(tbVbAng);
            vali += TextValidate(tbVcAng);
            vali += TextValidate(tbVnAng);

            #endregion

            if (vali > 0)
            {
                MessageBox.Show("Valores Inválidos");
            }
            else
            {
                var list = new SequenceConfig(name, double.Parse(tbDuration.Text));
                list.data.Add(new SequenceConfig.variable("Ia", double.Parse(tbIaMod.Text), double.Parse(tbIaAng.Text)));
                list.data.Add(new SequenceConfig.variable("Ib", double.Parse(tbIbMod.Text), double.Parse(tbIbAng.Text)));
                list.data.Add(new SequenceConfig.variable("Ic", double.Parse(tbIcMod.Text), double.Parse(tbIcAng.Text)));
                list.data.Add(new SequenceConfig.variable("In", double.Parse(tbInMod.Text), double.Parse(tbInAng.Text)));
                list.data.Add(new SequenceConfig.variable("Va", double.Parse(tbVaMod.Text), double.Parse(tbVaAng.Text)));
                list.data.Add(new SequenceConfig.variable("Vb", double.Parse(tbVbMod.Text), double.Parse(tbVbAng.Text)));
                list.data.Add(new SequenceConfig.variable("Vc", double.Parse(tbVcMod.Text), double.Parse(tbVcAng.Text)));
                list.data.Add(new SequenceConfig.variable("Vn", double.Parse(tbVnMod.Text), double.Parse(tbVnAng.Text)));

                setup.Add(list);
                addTable(setup.Count - 1);

                updateGraph();

            }
        }

        private void tableSequence_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (e.ColumnIndex == 0)
            {
                if (setup.Count > 0)
                {
                    setup.RemoveAt(e.RowIndex);
                    tableSequence.Rows.RemoveAt(e.RowIndex);
                }

            }
            else if (e.ColumnIndex > 0)
            {
                var x = setup[e.RowIndex];
                tbIaMod.Text = x.data[0].module.ToString();
                tbIaAng.Text = x.data[0].angle.ToString();
                tbIbMod.Text = x.data[1].module.ToString();
                tbIbAng.Text = x.data[1].angle.ToString();
                tbIcMod.Text = x.data[2].module.ToString();
                tbIcAng.Text = x.data[2].angle.ToString();
                tbInMod.Text = x.data[3].module.ToString();
                tbInAng.Text = x.data[3].angle.ToString();

                tbVaMod.Text = x.data[4].module.ToString();
                tbVaAng.Text = x.data[4].angle.ToString();
                tbVbMod.Text = x.data[5].module.ToString();
                tbVbAng.Text = x.data[5].angle.ToString();
                tbVcMod.Text = x.data[6].module.ToString();
                tbVcAng.Text = x.data[6].angle.ToString();
                tbVnMod.Text = x.data[7].module.ToString();
                tbVnAng.Text = x.data[7].angle.ToString();

                tbNameSequence.Text = x.name;
                tbDuration.Text = x.time.ToString();
            }
        }

        private TextMode CurrentRotation = TextMode.normal;
        private TextMode VoltageRotation = TextMode.normal;

        private void CurrentRotacaoPositiva()
        {
            var modBase = tbIaMod.Text;
            tbIaMod.Text = modBase;
            tbIbMod.Text = modBase;
            tbIcMod.Text = modBase;

            var angBase = double.Parse(tbIaAng.Text);
            tbIaAng.Text = angBase.ToString("0.0");
            tbIbAng.Text = (angBase - 120).ToString("0.0");
            tbIcAng.Text = (angBase + 120).ToString("0.0");
        }
        private void CurrentRotacaoNegativa()
        {
            var modBase = tbIaMod.Text;
            tbIaMod.Text = modBase;
            tbIbMod.Text = modBase;
            tbIcMod.Text = modBase;

            var angBase = double.Parse(tbIaAng.Text);
            tbIaAng.Text = angBase.ToString("0,0");
            tbIbAng.Text = (angBase + 120).ToString("0,0");
            tbIcAng.Text = (angBase - 120).ToString("0,0");
        }
        private void StripMenuCurrentPositiva(object sender, EventArgs e)
        {
            CurrentRotation = TextMode.positiva;
            CurrentRotacaoPositiva();
            tbIbMod.Enabled = false;
            tbIbAng.Enabled = false;
            tbIcMod.Enabled = false;
            tbIcAng.Enabled = false;
        }
        private void StripMenuCurrentNegativa(object sender, EventArgs e)
        {
            CurrentRotation = TextMode.negativa;
            CurrentRotacaoNegativa();
            tbIbMod.Enabled = false;
            tbIbAng.Enabled = false;
            tbIcMod.Enabled = false;
            tbIcAng.Enabled = false;
        }
        private void StripMenuCurrentNormal(object sender, EventArgs e)
        {
            CurrentRotation = TextMode.normal;
            tbIbMod.Enabled = true;
            tbIbAng.Enabled = true;
            tbIcMod.Enabled = true;
            tbIcAng.Enabled = true;
        }
        private void CurrentTextValidation(object sender, EventArgs e)
        {
            if (CurrentRotation == TextMode.positiva)
                CurrentRotacaoPositiva();
            else if (CurrentRotation == TextMode.negativa)
                CurrentRotacaoNegativa();
        }
        private void CurrentTextValidationKey(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (CurrentRotation == TextMode.positiva)
                    CurrentRotacaoPositiva();
                else if (CurrentRotation == TextMode.negativa)
                    CurrentRotacaoNegativa();
            }
        }

        private void VoltageRotacaoPositiva()
        {
            var modBase = tbVaMod.Text;
            tbVaMod.Text = modBase;
            tbVbMod.Text = modBase;
            tbVcMod.Text = modBase;

            var angBase = double.Parse(tbVaAng.Text);
            tbVaAng.Text = angBase.ToString("0.0");
            tbVbAng.Text = (angBase - 120).ToString("0.0");
            tbVcAng.Text = (angBase + 120).ToString("0.0");
        }
        private void VoltageRotacaoNegativa()
        {
            var modBase = tbVaMod.Text;
            tbVaMod.Text = modBase;
            tbVbMod.Text = modBase;
            tbVcMod.Text = modBase;

            var angBase = double.Parse(tbVaAng.Text);
            tbVaAng.Text = angBase.ToString("0,0");
            tbVbAng.Text = (angBase + 120).ToString("0,0");
            tbVcAng.Text = (angBase - 120).ToString("0,0");
        }
        private void StripMenuVoltagePositiva(object sender, EventArgs e)
        {
            VoltageRotation = TextMode.positiva;
            VoltageRotacaoPositiva();
            tbVbMod.Enabled = false;
            tbVbAng.Enabled = false;
            tbVcMod.Enabled = false;
            tbVcAng.Enabled = false;
        }
        private void StripMenuVoltageNegativa(object sender, EventArgs e)
        {
            VoltageRotation = TextMode.negativa;
            VoltageRotacaoNegativa();
            tbVbMod.Enabled = false;
            tbVbAng.Enabled = false;
            tbVcMod.Enabled = false;
            tbVcAng.Enabled = false;
        }
        private void StripMenuVoltageNormal(object sender, EventArgs e)
        {
            VoltageRotation = TextMode.normal;
            tbVbMod.Enabled = true;
            tbVbAng.Enabled = true;
            tbVcMod.Enabled = true;
            tbVcAng.Enabled = true;
        }
        private void VoltageTextValidation(object sender, EventArgs e)
        {
            if (VoltageRotation == TextMode.positiva)
                VoltageRotacaoPositiva();
            else if (VoltageRotation == TextMode.negativa)
                VoltageRotacaoNegativa();
        }
        private void VoltageTextValidationKey(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (VoltageRotation == TextMode.positiva)
                    VoltageRotacaoPositiva();
                else if (VoltageRotation == TextMode.negativa)
                    VoltageRotacaoNegativa();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            string name = tbNameSequence.Text;
            int indx = tableSequence.CurrentRow.Index;
            if (name != setup[indx].name)
            {
                int lastDigit = 0;
                while (setup.Exists(obj => obj.name == name))
                {
                    if (char.IsDigit(name[^1]))
                    {
                        lastDigit += 1;
                        name = name.Remove(name.Length - 1) + lastDigit;
                    }
                    else
                        name += '1';
                }
            }

            #region Validation

            int vali = 0;
            vali += TextValidate(tbIaMod);
            vali += TextValidate(tbIbMod);
            vali += TextValidate(tbIcMod);
            vali += TextValidate(tbInMod);
            vali += TextValidate(tbVaMod);
            vali += TextValidate(tbVbMod);
            vali += TextValidate(tbVcMod);
            vali += TextValidate(tbVnMod);

            vali += TextValidate(tbIaMod);
            vali += TextValidate(tbIbAng);
            vali += TextValidate(tbIcAng);
            vali += TextValidate(tbInAng);
            vali += TextValidate(tbVaAng);
            vali += TextValidate(tbVbAng);
            vali += TextValidate(tbVcAng);
            vali += TextValidate(tbVnAng);

            #endregion

            if (vali > 0)
            {
                MessageBox.Show("Valores Inválidos");
            }
            else
            {
                var list = new SequenceConfig(name, double.Parse(tbDuration.Text));
                list.data.Add(new SequenceConfig.variable("Ia", double.Parse(tbIaMod.Text), double.Parse(tbIaAng.Text)));
                list.data.Add(new SequenceConfig.variable("Ib", double.Parse(tbIbMod.Text), double.Parse(tbIbAng.Text)));
                list.data.Add(new SequenceConfig.variable("Ic", double.Parse(tbIcMod.Text), double.Parse(tbIcAng.Text)));
                list.data.Add(new SequenceConfig.variable("In", double.Parse(tbInMod.Text), double.Parse(tbInAng.Text)));
                list.data.Add(new SequenceConfig.variable("Va", double.Parse(tbVaMod.Text), double.Parse(tbVaAng.Text)));
                list.data.Add(new SequenceConfig.variable("Vb", double.Parse(tbVbMod.Text), double.Parse(tbVbAng.Text)));
                list.data.Add(new SequenceConfig.variable("Vc", double.Parse(tbVcMod.Text), double.Parse(tbVcAng.Text)));
                list.data.Add(new SequenceConfig.variable("Vn", double.Parse(tbVnMod.Text), double.Parse(tbVnAng.Text)));

                setup[indx] = list;
                updateTable();
                updateGraph();

            }

        }


        private Thread testTh;
        private bool testRunning = false;
        private void BtnStart_Click(object sender, EventArgs e)
        {

            if (!testRunning)
            {
                //TbDebug.Visible = false;

                var yamlFile = main.CreateYamlNetwork();
                if (yamlFile != null)
                {
                    main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);
                    string res = main.serverCon.SendData("setupNetwork", yamlFile);
                    if (res == null || res == "error")
                    {
                        MessageBox.Show("It Wasn't possible to connected to vMU!");
                        return;
                    }
                }

                yamlFile = main.CreateYamlSequencer();
                if (yamlFile != null)
                {
                    main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);
                    string res = main.serverCon.SendData("setupSequencer", yamlFile);
                    if (res == null || res == "error")
                    {
                        MessageBox.Show("It Wasn't possible to connected to vMU!");
                        return;
                    }
                }

                testRunning = true;
                testTh = new Thread(testThread);
                testTh.Start();
                BtnStart.Text = "Parar Sequências";
                main.serverCon.ConnectionLost += ConnectionStopHandler;
            }
            else
            {
                //TbDebug.Visible = false;
                BtnStart.Text = "Iniciar Sequências";
                testRunning = false;
                testTh.Join();
            }
        }

        private void ConnectionStopHandler(object? sender, EventArgs e)
        {
            BtnStart.BeginInvoke(new Action(() => BtnStart.Text = "Iniciar Sequências"));
            testRunning = false;
        }

        private void testThread()
        {
            main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);
            string res = main.serverCon.SendData("startSequencer", "All");
            if (res == null) MessageBox.Show("It Wasn't possible to connected to vMU!");
            double[] result;
            Thread.Sleep(800);
            while (testRunning)
            {
                res = main.serverCon.SendData("getSequencerResults", "All");
                if (res == null || res == "error") ;
                else
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<double[]>(res);
                        if (result != null)
                        {
                            pltViewTest.BeginInvoke(new Action(() => updateTestGraph(result[0])));
                            if (testRunning) TbDebug.BeginInvoke(new Action(() => TbDebug.Text = result[0].ToString()));

                            updateTestGraph(result[0]);
                            if (result[1] == 1)
                                testRunning = false;
                        }
                    }
                    catch {; }
                };
                Thread.Sleep(100);
            }
            try
            {
                BtnStart.BeginInvoke(new Action(() => BtnStart.Text = "Iniciar Sequências"));
            }
            catch {; }

            testRunning = false;
        }

        private void updateTestGraph(double currentTime)
        {
            if (setup.Count == 0)
                return;

            double timeMulti = 1;
            double freq = MainForm.main.networkConfig.svConfig.frequency;
            string xAxisLegend = "Time [s]";
            if (graphTimeDomain == GraphTimeDomain.cycle)
            {
                timeMulti = freq;
                xAxisLegend = "Time [cycle]";
            }

            List<List<DataPoint>> points = new List<List<DataPoint>>
            {
                new List<DataPoint>(),
                new List<DataPoint>(),
                new List<DataPoint>(),
                new List<DataPoint>()
            };
            List<string> names = new List<string>()
            {
                "Ia",
                "Ib",
                "Ic",
                "In"
            };
            if (graphType == GraphType.current)
            {
                names = new List<string>
                {
                    "Ia",
                    "Ib",
                    "Ic",
                    "In"
                };
                double t0 = 0;
                double x;
                double y;
                foreach (SequenceConfig sequence in setup)
                {
                    for (x = t0; x <= sequence.time + t0; x += 1 / 1000.0)
                    {
                        if (x > currentTime) break;
                        for (int i = 0; i < 4; i++)
                        {
                            if (graphValuesDomain == GraphValuesDomain.frequency)
                                y = sequence.data[i].module;
                            else if (graphValuesDomain == GraphValuesDomain.time)
                                y = Math.Sqrt(2) * sequence.data[i].module * Math.Cos(x * Math.PI * 2 * freq + sequence.data[i].angle);
                            else
                                y = 0;
                            points[i].Add(new DataPoint(x * timeMulti, y));
                        }
                    }
                    t0 = x;
                }
            }
            else if (graphType == GraphType.voltage)
            {
                names = new List<string>
                {
                    "Va",
                    "Vb",
                    "Vc",
                    "Vn"
                };
                double t0 = 0;
                double x;
                double y;
                foreach (SequenceConfig sequence in setup)
                {
                    for (x = t0; x <= sequence.time + t0; x += 1 / 1000.0)
                    {
                        for (int i = 4; i < 8; i++)
                        {
                            if (graphValuesDomain == GraphValuesDomain.frequency)
                                y = sequence.data[i].module;
                            else if (graphValuesDomain == GraphValuesDomain.time)
                                y = Math.Sqrt(2) * sequence.data[i].module * Math.Cos(x * Math.PI * 2 * freq + sequence.data[i].angle);
                            else
                                y = 0;
                            points[i - 4].Add(new DataPoint(x * timeMulti, y));
                        }
                    }
                    t0 = x;

                }
            }
            var plotModel = new PlotModel
            {
                Title = "Sequencer",
                TitleColor = OxyColors.Lavender,
                IsLegendVisible = true,
            };

            for (int i = 0; i < 4; i++)
            {
                if (points[i].Count > 0)
                {
                    var lineSeries = new LineSeries()
                    {
                        Title = names[i],
                        TextColor = OxyColors.Lavender
                    };
                    lineSeries.Points.AddRange(points[i]);
                    plotModel.Series.Add(lineSeries);
                    plotModel.Legends.Add(new Legend()
                    {
                        LegendTextColor = OxyColors.Lavender,
                        LegendPosition = LegendPosition.TopLeft
                    });
                }
            }

            double max = points.SelectMany(list => list).Max(dataPoint => dataPoint.Y);
            double min = points.SelectMany(list => list).Min(dataPoint => dataPoint.Y);
            if (min < 0)
                min *= 1.1;


            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = xAxisLegend,
                TitleFontSize = 14,
                Minimum = 0,
                AxislineColor = OxyColors.Lavender,
                TextColor = OxyColors.Lavender,
                TitleColor = OxyColors.Lavender,
                ExtraGridlineColor = OxyColors.Lavender,
                MajorGridlineColor = OxyColor.FromRgb(84, 98, 110),
                MinorTicklineColor = OxyColors.Lavender,
                TicklineColor = OxyColors.Lavender,
                MajorGridlineStyle = LineStyle.Dot,
            };
            plotModel.Axes.Add(xAxis);
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = max * 1.1,
                Minimum = min,
                AxislineColor = OxyColors.Lavender,
                TextColor = OxyColors.Lavender,
                TitleColor = OxyColors.Lavender,
                ExtraGridlineColor = OxyColors.Lavender,
                MajorGridlineColor = OxyColor.FromRgb(84, 98, 110),
                MinorTicklineColor = OxyColors.Lavender,
                TicklineColor = OxyColors.Lavender,
                MajorGridlineStyle = LineStyle.Dot,

            };
            plotModel.Axes.Add(yAxis);

            plotModel.PlotAreaBorderColor = OxyColors.Lavender;
            pltViewTest.Model = plotModel;
        }

        private enum GraphType
        {
            current = 0,
            voltage
        }
        private enum GraphValuesDomain
        {
            frequency = 0,
            time,
        }
        private enum GraphTimeDomain
        {
            seconds = 0,
            cycle
        }

        private GraphType graphType = GraphType.current;
        private GraphValuesDomain graphValuesDomain = GraphValuesDomain.frequency;
        private GraphTimeDomain graphTimeDomain = GraphTimeDomain.seconds;

        private void updateGraph()
        {

            if (setup.Count == 0)
                return;

            double timeMulti = 1;
            double freq = MainForm.main.networkConfig.svConfig.frequency;
            string xAxisLegend = "Time [s]";
            if (graphTimeDomain == GraphTimeDomain.cycle)
            {
                timeMulti = freq;
                xAxisLegend = "Time [cycle]";
            }

            List<List<DataPoint>> points = new List<List<DataPoint>>
            {
                new List<DataPoint>(),
                new List<DataPoint>(),
                new List<DataPoint>(),
                new List<DataPoint>()
            };
            List<string> names = new List<string>()
            {
                "Ia",
                "Ib",
                "Ic",
                "In"
            };
            if (graphType == GraphType.current)
            {
                names = new List<string>
                {
                    "Ia",
                    "Ib",
                    "Ic",
                    "In"
                };
                double t0 = 0;
                double x;
                double y;
                foreach (SequenceConfig sequence in setup)
                {
                    for (x = t0; x <= sequence.time + t0; x += 1 / 1000.0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (graphValuesDomain == GraphValuesDomain.frequency)
                                y = sequence.data[i].module;
                            else if (graphValuesDomain == GraphValuesDomain.time)
                                y = Math.Sqrt(2) * sequence.data[i].module * Math.Cos(x * Math.PI * 2 * freq + sequence.data[i].angle);
                            else
                                y = 0;
                            points[i].Add(new DataPoint(x * timeMulti, y));
                        }
                    }
                    t0 = x;

                }
            }
            else if (graphType == GraphType.voltage)
            {
                names = new List<string>
                {
                    "Va",
                    "Vb",
                    "Vc",
                    "Vn"
                };
                double t0 = 0;
                double x;
                double y;
                foreach (SequenceConfig sequence in setup)
                {
                    for (x = t0; x <= sequence.time + t0; x += 1 / 1000.0)
                    {
                        for (int i = 4; i < 8; i++)
                        {
                            if (graphValuesDomain == GraphValuesDomain.frequency)
                                y = sequence.data[i].module;
                            else if (graphValuesDomain == GraphValuesDomain.time)
                                y = Math.Sqrt(2) * sequence.data[i].module * Math.Cos(x * Math.PI * 2 * freq + sequence.data[i].angle);
                            else
                                y = 0;
                            points[i - 4].Add(new DataPoint(x * timeMulti, y));
                        }
                    }
                    t0 = x;

                }
            }
            var plotModel = new PlotModel
            {
                Title = "Sequencer",
                TitleColor = OxyColors.Lavender,
                IsLegendVisible = true,
            };

            for (int i = 0; i < 4; i++)
            {
                if (points[i].Count > 0)
                {
                    var lineSeries = new LineSeries()
                    {
                        Title = names[i],
                        TextColor = OxyColors.Lavender
                    };
                    lineSeries.Points.AddRange(points[i]);
                    plotModel.Series.Add(lineSeries);
                    plotModel.Legends.Add(new Legend()
                    {
                        LegendTextColor = OxyColors.Lavender,
                        LegendPosition = LegendPosition.TopLeft
                    });
                }
            }

            double max = points.SelectMany(list => list).Max(dataPoint => dataPoint.Y);
            double min = points.SelectMany(list => list).Min(dataPoint => dataPoint.Y);
            if (min < 0)
                min *= 1.1;


            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = xAxisLegend,
                TitleFontSize = 14,
                Minimum = 0,
                AxislineColor = OxyColors.Lavender,
                TextColor = OxyColors.Lavender,
                TitleColor = OxyColors.Lavender,
                ExtraGridlineColor = OxyColors.Lavender,
                MajorGridlineColor = OxyColor.FromRgb(84, 98, 110),
                MinorTicklineColor = OxyColors.Lavender,
                TicklineColor = OxyColors.Lavender,
                MajorGridlineStyle = LineStyle.Dot,
            };
            plotModel.Axes.Add(xAxis);
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = max * 1.1,
                Minimum = min,
                AxislineColor = OxyColors.Lavender,
                TextColor = OxyColors.Lavender,
                TitleColor = OxyColors.Lavender,
                ExtraGridlineColor = OxyColors.Lavender,
                MajorGridlineColor = OxyColor.FromRgb(84, 98, 110),
                MinorTicklineColor = OxyColors.Lavender,
                TicklineColor = OxyColors.Lavender,
                MajorGridlineStyle = LineStyle.Dot,

            };
            plotModel.Axes.Add(yAxis);

            plotModel.PlotAreaBorderColor = OxyColors.Lavender;


            pltView.Model = plotModel;

        }

        private void BtnGraphCurrent_Click(object sender, EventArgs e)
        {

            graphType = GraphType.current;
            updateGraph();

        }

        private void BtnGraphVoltage_Click(object sender, EventArgs e)
        {
            graphType = GraphType.voltage;
            updateGraph();

        }

        private void ToolStripMenuSeconds(object sender, EventArgs e)
        {
            graphTimeDomain = GraphTimeDomain.seconds;
            updateGraph();
        }

        private void ToolStripMenuCycle(object sender, EventArgs e)
        {
            graphTimeDomain = GraphTimeDomain.cycle;
            updateGraph();
        }

        private void ToolStripMenuFrequency(object sender, EventArgs e)
        {
            graphValuesDomain = GraphValuesDomain.frequency;
            updateGraph();
        }

        private void ToolStripMenuInstant(object sender, EventArgs e)
        {
            graphValuesDomain = GraphValuesDomain.time;
            updateGraph();
        }

        private void TableSizeChanged(object sender, EventArgs e)
        {
            TableFormat();
        }
    }
}