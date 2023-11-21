using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Ied.MainForm;

namespace Ied
{
    public partial class PtuvForm : Form
    {

        private PtovConfig conf59;
        private PtuvConfig conf27;

        public PtuvForm()
        {
            InitializeComponent();
        }

        private List<Label> lbVoltage;
        private List<Label> lbTimeDelay;
        private List<TextBox> tbVoltage;
        private List<TextBox> tbTimeDelay;
        private List<CheckBox> cbLevels;
        private List<TableLayoutPanel[]> tlpLevels;

        private void PtuvForm_Load(object sender, EventArgs e)
        {
            if (main.ptov == null) main.ptov = new PtovConfig();
            if (main.ptuv == null) main.ptuv = new PtuvConfig();
            conf59 = main.ptov;
            conf27 = main.ptuv;

            lbVoltage = new List<Label> { Lb50pCurrentL1, Lb50pCurrentL2, Lb50pCurrentL3 };
            tbVoltage = new List<TextBox> { Tb50pPickupL1, Tb50pPickupL2, Tb50pPickupL3 };
            tbTimeDelay = new List<TextBox> { Tb50pTripL1, Tb50pTripL2, Tb50pTripL3 };
            lbTimeDelay = new List<Label> { Lb50pTimeL1, Lb50pTimeL2, Lb50pTimeL3 };
            cbLevels = new List<CheckBox> { Cb50pLevel1, Cb50pLevel2, Cb50pLevel3 };
            tlpLevels = new List<TableLayoutPanel[]>
            {
                new TableLayoutPanel[] {Tlp50pPickupL1, Tlp50pTripL1},
                new TableLayoutPanel [] {Tlp50pPickupL2, Tlp50pTripL2 },
                new TableLayoutPanel[] {Tlp50pPickupL3, Tlp50pTripL3 }
            };
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < 3; i++)
            {
                cbLevels[i].Checked = conf27.level[i].enabled;
                tbVoltage[i].Text = ((double)conf27.level[i].pickup).ToString("0.00");
                tbTimeDelay[i].Text = ((double)conf27.level[i].timeDelay).ToString("0.00");
            }

            foreach (var cb in cbLevels)
                levelChange(cb, EventArgs.Empty);

            if (conf27.timeType == TimeType.cycle)
                RbCycle.Checked = true;
            else
                RbSeconds.Checked = true;

            if (Pn50pConfig.Enabled)
                LbTitle.ForeColor = Color.Lavender;
            else
                LbTitle.ForeColor = Color.Black;
            updateGraph();
        }

        private void levelChange(object sender, EventArgs e)
        {
            var local = (CheckBox)sender;
            local.CheckStateChanged -= levelChange;
            var idx = cbLevels.IndexOf(local);
            cbLevels[idx].ForeColor = cbLevels[idx].Checked ? Color.Lavender : Color.Black;
            conf27.level[idx].enabled = cbLevels[idx].Checked;
            tlpLevels[idx][0].Enabled = tlpLevels[idx][1].Enabled = cbLevels[idx].Checked;
            updateGraph();
            local.CheckStateChanged += levelChange;
        }

        private void RbVolts_CheckedChanged(object sender, EventArgs e)
        {
            if (RbVolts.Checked)
            {
                conf27.voltageType = VoltageType.Volts;
                for (int i = 0; i < 3; i++)
                {
                    lbVoltage[i].Text = "[V]";
                    tbVoltage[i].Text = ((double)conf27.level[i].pickup).ToString("0.00");
                }
            }
            else
            {
                conf27.voltageType = VoltageType.Pu;
                for (int i = 0; i < 3; i++)
                {
                    lbVoltage[i].Text = "[Pu]";
                    tbVoltage[i].Text = ((double)conf27.level[i].pickup / (double)main.general.NominalVoltage).ToString("0.00");
                }
            }

        }

        private void RbSeconds_CheckedChanged(object sender, EventArgs e)
        {
            if (RbSeconds.Checked)
            {
                conf27.timeType = TimeType.seconds;
                for (int i = 0; i < 3; i++)
                {
                    lbTimeDelay[i].Text = "[s]";
                    tbTimeDelay[i].Text = ((double)conf27.level[i].timeDelay).ToString("0.0000");
                }
            }
            else
            {
                conf27.timeType = TimeType.cycle;
                for (int i = 0; i < 3; i++)
                {
                    lbTimeDelay[i].Text = "[cy]";
                    tbTimeDelay[i].Text = ((double)conf27.level[i].timeDelay * (double)main.general.frequency).ToString("0.00");
                }
            }

        }

        private void TbPickupValidated(object sender, EventArgs e)
        {
            var local = (TextBox)sender;
            local.Validated -= TbPickupValidated;
            var idx = tbVoltage.IndexOf(local);
            conf27.level[idx].pickup = local.Text;
            if (conf27.voltageType == VoltageType.Pu)
            {
                conf27.level[idx].pickup = (double)conf27.level[idx].pickup * (double)main.general.NominalVoltage;
                local.Text = ((double)conf27.level[idx].pickup / (double)main.general.NominalVoltage).ToString("0.00");
            }
            else
                local.Text = ((double)conf27.level[idx].pickup).ToString("0.00");
            updateGraph();
            local.Validated += TbPickupValidated;
        }

        private void TbPickupValidatedKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                var local = (TextBox)sender;
                local.Validated -= TbPickupValidated;
                var idx = tbVoltage.IndexOf(local);
                conf27.level[idx].pickup = local.Text;
                if (conf27.voltageType == VoltageType.Pu)
                {
                    conf27.level[idx].pickup = (double)conf27.level[idx].pickup * (double)main.general.NominalVoltage;
                    local.Text = ((double)conf27.level[idx].pickup / (double)main.general.NominalVoltage).ToString("0.00");
                }
                else
                    local.Text = ((double)conf27.level[idx].pickup).ToString("0.00");
                updateGraph();
                local.Validated += TbPickupValidated;
            }
        }

        private void TbTripValidated(object sender, EventArgs e)
        {
            var local = (TextBox)sender;
            local.Validated -= TbTripValidated;
            var idx = tbTimeDelay.IndexOf(local);
            conf27.level[idx].timeDelay = local.Text;
            if (conf27.timeType == TimeType.cycle)
            {
                conf27.level[idx].timeDelay = (double)conf27.level[idx].timeDelay / (double)main.general.frequency;
                local.Text = ((double)conf27.level[idx].timeDelay * (double)main.general.frequency).ToString("0.00");
            }
            else local.Text = ((double)conf27.level[idx].timeDelay).ToString("0.0000");

            updateGraph();
            local.Validated += TbTripValidated;
        }

        private void TbTripValidatedKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                var local = (TextBox)sender;
                local.Validated -= TbTripValidated;
                var idx = tbTimeDelay.IndexOf(local);
                conf27.level[idx].timeDelay = local.Text;
                if (conf27.timeType == TimeType.cycle)
                {
                    conf27.level[idx].timeDelay = (double)conf27.level[idx].timeDelay / (double)main.general.frequency;
                    local.Text = ((double)conf27.level[idx].timeDelay * (double)main.general.frequency).ToString("0.00");
                }
                else local.Text = ((double)conf27.level[idx].timeDelay).ToString("0.0000");

                updateGraph();
                local.Validated += TbTripValidated;
            }
        }

        private void updateGraph()
        {
            List<DataPoint> points = new List<DataPoint>();
            List<List<DataPoint>> curveDataPointsList = new List<List<DataPoint>>();
            double x0 = 0;
            double x1 = (double)main.general.NominalVoltage * 2;
            double y = 0xffff;
            double _y = 0xffff;
            for (double x = x0; x <= x1; x += x1 / 2000)
            {
                y = 0xffff;
                if (rbCurveTogether.Checked)
                {
                    if (CbGrap50.Checked)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (conf27.level[i].enabled)
                            {
                                if (x < (double)conf27.level[i].pickup)
                                    if ((double)conf27.level[i].timeDelay < y)
                                        y = (double)conf27.level[i].timeDelay;
                            }
                        }

                    }
                    if (CbGrap51.Checked)
                    {
                        for (int i = 2; i >= 0; i--)
                        {
                            if (conf59.level[i].enabled)
                            {
                                if (x > (double)conf59.level[i].pickup)
                                    if ((double)conf59.level[i].timeDelay < y)
                                        y = (double)conf59.level[i].timeDelay;
                            }
                        }
                    }
                    points.Add(new DataPoint(x / (double)main.general.NominalVoltage, y));
                }
                else
                {
                    int j = 0;
                    if (CbGrap50.Checked)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (conf27.level[i].enabled)
                            {
                                if (x < (double)conf27.level[i].pickup)
                                    y = (double)conf27.level[i].timeDelay;
                                else
                                    y = 0xffff;
                                if (curveDataPointsList.Count - 1 < j)
                                    curveDataPointsList.Add(new List<DataPoint>());
                                curveDataPointsList[j].Add(new DataPoint(x / (double)main.general.NominalVoltage, y));
                                j++;
                            }
                        }
                    }
                    if (CbGrap51.Checked)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (conf59.level[i].enabled)
                            {
                                if (x > (double)conf59.level[i].pickup)
                                    y = (double)conf59.level[i].timeDelay;
                                else
                                    y = 0xffff;
                                if (curveDataPointsList.Count - 1 < j)
                                    curveDataPointsList.Add(new List<DataPoint>());
                                curveDataPointsList[j].Add(new DataPoint(x / (double)main.general.NominalVoltage, y));
                                j++;
                            }
                        }
                    }
                }
            }

            var plotModel = new PlotModel
            {
                Title = "Coordenograma",
                TitleColor = OxyColors.Lavender,
            };
            if (rbCurveTogether.Checked)
            {
                var lineSeries = new LineSeries()
                {
                    Color = OxyColors.Red,
                };
                lineSeries.Points.AddRange(points);
                plotModel.Series.Add(lineSeries);
            }
            else
            {
                int i = 0;
                var colors = new List<OxyColor>() { OxyColors.Red, OxyColors.AliceBlue, OxyColors.DarkOrange, OxyColors.Red, OxyColors.AliceBlue, OxyColors.DarkOrange };
                var typeFlag = false;
                foreach (var _points in curveDataPointsList)
                {
                    if (i < 3)
                    {
                        while (!conf59.level[i].enabled && i < 2)
                            i++;
                    }
                    else
                        typeFlag = true;

                    if (typeFlag)
                    {
                        i -= 3;
                        while (!conf27.level[i].enabled && i < 2)
                            i++;
                    }

                    var lineSeries = new LineSeries()
                    {
                        Color = colors[i],
                        Title = $"Level {i + 1}",
                        TextColor = colors[i],
                    };
                    lineSeries.Points.AddRange(_points);
                    lineSeries.LineLegendPosition = LineLegendPosition.Start;
                    plotModel.Series.Add(lineSeries);
                    plotModel.Legends.Add(new Legend()
                    {
                        LegendTitle = $"Curvas",
                        LegendPosition = LegendPosition.RightTop,
                        LegendTextColor = colors[i],
                        LegendTitleColor = OxyColors.LightCyan,
                    });
                    i++;
                    if (typeFlag)
                        i += 3;
                }
            }

            plotModel.IsLegendVisible = true;

            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Pickup Current [Pu]",
                AbsoluteMinimum = 0,
                AbsoluteMaximum = 20,
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
                Title = "Time [s]",
                Minimum = 0,
                Maximum = double.Parse(TbMaxTime.Text),
                AbsoluteMinimum = 0,
                AbsoluteMaximum = 2 * double.Parse(TbMaxTime.Text),
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
            PltPlot.Model = null;
            PltPlot.Model = plotModel;
        }

        private void CbGrap50_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }
    }
}
