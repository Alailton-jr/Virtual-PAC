using OxyPlot.Axes;
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
using System.Globalization;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;
using OxyPlot.Legends;

namespace Ied
{
    public partial class PiocForm : Form
    {

        Ctl main = MainForm.main;

        public PiocConfig.Phase conf50;
        public PtocConfig.Phase conf51;
        public string formName;

        public PiocForm()
        {
            InitializeComponent();
        }

        private List<Label> lbCurrent;
        private List<Label> lbTimeDelay;
        private List<TextBox> tbCurrent;
        private List<TextBox> tbTimeDelay;
        private List<CheckBox> cbLevels;
        private List<TableLayoutPanel[]> tlpLevels;

        private void Prot50_Load(object sender, EventArgs e)
        {
            LbTitle.Text = formName;


            lbCurrent = new List<Label> { Lb50pCurrentL1, Lb50pCurrentL2, Lb50pCurrentL3 };
            tbCurrent = new List<TextBox> { Tb50pPickupL1, Tb50pPickupL2, Tb50pPickupL3 };
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
                cbLevels[i].Checked = conf50.level[i].enabled;
                tbCurrent[i].Text = ((double)conf50.level[i].pickup).ToString("0.00");
                tbTimeDelay[i].Text = ((double)conf50.level[i].timeDelay).ToString("0.00");
            }

            foreach (var cb in cbLevels)
                panel50PLevelChange(cb, EventArgs.Empty);

            if (Pn50pConfig.Enabled)
                LbTitle.ForeColor = Color.Lavender;
            else
                LbTitle.ForeColor = Color.Black;
            updateGraph();
        }

        private void panel50PLevelChange(object sender, EventArgs e)
        {
            var local = (CheckBox)sender;
            local.CheckStateChanged -= panel50PLevelChange;
            var idx = cbLevels.IndexOf(local);
            cbLevels[idx].ForeColor = cbLevels[idx].Checked ? Color.Lavender : Color.Black;
            conf50.level[idx].enabled = cbLevels[idx].Checked;
            tlpLevels[idx][0].Enabled = tlpLevels[idx][1].Enabled = cbLevels[idx].Checked;
            updateGraph();
            local.CheckStateChanged += panel50PLevelChange;
        }

        private void updateGraph()
        {
            List<DataPoint> points = new List<DataPoint>();
            List<List<DataPoint>> curveDataPointsList = new List<List<DataPoint>>();
            double x0 = 0;
            double x1 = (double)main.general.NominalCurrent * 20;
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
                            if (conf50.level[i].enabled)
                            {
                                if (x > (double)conf50.level[i].pickup)
                                    if ((double)conf50.level[i].timeDelay < y)
                                        y = (double)conf50.level[i].timeDelay;
                            }
                        }

                    }
                    if (CbGrap51.Checked)
                    {
                        for (int i = 2; i >= 0; i--)
                        {
                            if (conf51.level[i].enabled)
                            {
                                if (x > 1.05 * conf51.level[i].pickup)
                                {
                                    var curve = conf51.curves[conf51.level[i].Curve];
                                    _y = conf51.level[i].timeDial * (curve["A"] + curve["B"] / (Math.Pow((x / conf51.level[i].pickup), curve["C"]) - 1));
                                    if (_y < y)
                                        y = _y;
                                }
                            }
                        }
                    }
                    points.Add(new DataPoint(x / (double)main.general.NominalCurrent, y));
                }
                else
                {
                    int j = 0;
                    if (CbGrap50.Checked)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (conf50.level[i].enabled)
                            {
                                if (x > (double)conf50.level[i].pickup)
                                    y = (double)conf50.level[i].timeDelay;
                                else
                                    y = 0xffff;
                                if (curveDataPointsList.Count - 1 < j)
                                    curveDataPointsList.Add(new List<DataPoint>());
                                curveDataPointsList[j].Add(new DataPoint(x / (double)main.general.NominalCurrent, y));
                                j++;
                            }
                        }
                    }
                    if (CbGrap51.Checked)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (conf51.level[i].enabled)
                            {
                                if (x > 1.05 * conf51.level[i].pickup)
                                {
                                    var curve = conf51.curves[conf51.level[i].Curve];
                                    y = conf51.level[i].timeDial * (curve["A"] + curve["B"] / (Math.Pow((x / conf51.level[i].pickup), curve["C"]) - 1));
                                    if (curveDataPointsList.Count - 1 < j)
                                        curveDataPointsList.Add(new List<DataPoint>());
                                    curveDataPointsList[j].Add(new DataPoint(x / (double)main.general.NominalCurrent, y));
                                    j++;
                                }
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
                        while (!conf51.level[i].enabled && i < 2)
                            i++;
                    }
                    else
                        typeFlag = true;

                    if (typeFlag)
                    {
                        i -= 3;
                        while (!conf50.level[i].enabled && i < 2)
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

        private void TbPickupValidated(object sender, EventArgs e)
        {
            var local = (TextBox)sender;
            local.Validated -= TbPickupValidated;
            var idx = tbCurrent.IndexOf(local);
            conf50.level[idx].pickup = local.Text;
            if (conf50.currentType == CurrentType.Pu)
            {
                conf50.level[idx].pickup = (double)conf50.level[idx].pickup * (double)main.general.NominalCurrent;
                local.Text = ((double)conf50.level[idx].pickup / (double)main.general.NominalCurrent).ToString("0.00");
            }
            else
                local.Text = ((double)conf50.level[idx].pickup).ToString("0.00");

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
                var idx = tbCurrent.IndexOf(local);
                conf50.level[idx].pickup = local.Text;
                if (conf50.currentType == CurrentType.Pu)
                {
                    conf50.level[idx].pickup = (double)conf50.level[idx].pickup * (double)main.general.NominalCurrent;
                    local.Text = ((double)conf50.level[idx].pickup / (double)main.general.NominalCurrent).ToString("0.00");
                }
                else local.Text = ((double)conf50.level[idx].pickup).ToString("0.00");

                updateGraph();
                local.Validated += TbPickupValidated;
            }
        }

        private void TbTripValidated(object sender, EventArgs e)
        {
            var local = (TextBox)sender;
            local.Validated -= TbTripValidated;
            var idx = tbTimeDelay.IndexOf(local);
            conf50.level[idx].timeDelay = local.Text;
            if (conf50.timeType == TimeType.cycle)
            {
                conf50.level[idx].timeDelay = (double)conf50.level[idx].timeDelay / (double)main.general.frequency;
                local.Text = ((double)conf50.level[idx].timeDelay * (double)main.general.frequency).ToString("0.00");
            }
            else local.Text = ((double)conf50.level[idx].timeDelay).ToString("0.0000");

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
                conf50.level[idx].timeDelay = local.Text;
                if (conf50.timeType == TimeType.cycle)
                {
                    conf50.level[idx].timeDelay = (double)conf50.level[idx].timeDelay / (double)main.general.frequency;
                    local.Text = ((double)conf50.level[idx].timeDelay * (double)main.general.frequency).ToString("0.00");
                }
                else local.Text = ((double)conf50.level[idx].timeDelay).ToString("0.0000");

                updateGraph();
                local.Validated += TbTripValidated;
            }
        }

        private void CbGrap50_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }

        private void CbGrap51_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }

        private void CbGrap50_CheckedChanged_1(object sender, EventArgs e)
        {
            updateGraph();
        }

        private void rbCurveSplited_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }

        private void Rb50pAmpere_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb50pAmpere.Checked)
            {
                conf50.currentType = CurrentType.Ampere;
                for (int i = 0; i < 3; i++)
                {
                    lbCurrent[i].Text = "[A]";
                    tbCurrent[i].Text = ((double)conf50.level[i].pickup).ToString("0.00");
                }
            }
            else
            {
                conf50.currentType = CurrentType.Pu;
                for (int i = 0; i < 3; i++)
                {
                    lbCurrent[i].Text = "[pu]";
                    tbCurrent[i].Text = ((double)conf50.level[i].pickup / (double)main.general.NominalCurrent).ToString("0.00");
                }
            }
        }

        private void Rb50pSeconds_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb50pSeconds.Checked)
            {
                conf50.timeType = TimeType.seconds;
                for (int i = 0; i < 3; i++)
                {
                    lbTimeDelay[i].Text = "[s]";
                    tbTimeDelay[i].Text = ((double)conf50.level[i].timeDelay).ToString("0.0000");
                }
            }
            else
            {
                conf50.timeType = TimeType.cycle;
                for (int i = 0; i < 3; i++)
                {
                    lbTimeDelay[i].Text = "[cy]";
                    tbTimeDelay[i].Text = ((double)conf50.level[i].timeDelay * (double)main.general.frequency).ToString("0.00");
                }
            }
        }
    }
}
