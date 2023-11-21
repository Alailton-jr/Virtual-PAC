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
using OxyPlot.Legends;

namespace Ied
{
    public partial class PtocForm : Form
    {
        public PtocConfig.Phase conf51;
        public PiocConfig.Phase conf50;
        public string formName;

        private List<List<object>> levelsConf;

        public PtocForm()
        {
            InitializeComponent();
        }

        private void PiocNeutralForm_Load(object sender, EventArgs e)
        {
            LbTitle.Text = formName;
            levelsConf = new List<List<object>>()
            {
                 new List<object>() {
                    TbPickupL1,
                    CbCurveL1,
                    TbTdL1
                 },
                 new List<object>() {
                    TbPickupL2,
                    CbCurveL2,
                    TbTdL2
                 },
                 new List<object>() {
                    TbPickupL3,
                    CbCurveL3,
                    TbTdL3
                 }
            };
            Initialize();
        }

        void Initialize()
        {
            if (conf51 == null)
                return;

            for (int i = 0; i < levelsConf.Count; i++)
            {
                for (int j = 0; j < levelsConf[i].Count; j++)
                {
                    if (j == 1)
                    {
                        var y = (ComboBox)levelsConf[i][j];
                        y.Enabled = conf51.level[i].enabled;
                    }
                    else
                    {
                        var y = (TextBox)levelsConf[i][j];
                        y.Enabled = conf51.level[i].enabled;
                    }
                }

                var pickup = (TextBox)levelsConf[i][0];
                var curve = (ComboBox)levelsConf[i][1];
                var td = (TextBox)levelsConf[i][2];

                pickup.Text = conf51.level[i].pickup.ToString();
                curve.Text = conf51.level[i].Curve;
                td.Text = conf51.level[i].timeDial.ToString();

            }

            CbLevel1.Checked = conf51.level[0].enabled;
            CbLevel2.Checked = conf51.level[1].enabled;
            CbLevel3.Checked = conf51.level[2].enabled;
        }

        void changeState(int idx, bool state)
        {
            for (int j = 0; j < levelsConf[idx].Count; j++)
            {
                if (j == 1)
                {
                    var y = (ComboBox)levelsConf[idx][j];
                    y.Enabled = state;
                }
                else
                {
                    var y = (TextBox)levelsConf[idx][j];
                    y.Enabled = state;
                }
            }
        }

        private void CbLevel1_CheckedChanged(object sender, EventArgs e)
        {
            conf51.level[0].enabled = CbLevel1.Checked;
            changeState(0, CbLevel1.Checked);
            updateGraph();
        }

        private void CbLevel2_CheckedChanged(object sender, EventArgs e)
        {
            conf51.level[1].enabled = CbLevel2.Checked;
            changeState(1, CbLevel2.Checked);
            updateGraph();
        }

        private void CbLevel3_CheckedChanged(object sender, EventArgs e)
        {
            conf51.level[2].enabled = CbLevel3.Checked;
            changeState(2, CbLevel3.Checked);
            updateGraph();
        }

        private void TbPickupL1_Validated(object sender, EventArgs e)
        {
            conf51.level[0].setPickup(TbPickupL1.Text);
            TbPickupL1.Text = conf51.level[0].pickup.ToString("0.00");
            updateGraph();
        }

        private void TbPickupL2_Validated(object sender, EventArgs e)
        {
            conf51.level[1].setPickup(TbPickupL2.Text);
            TbPickupL2.Text = conf51.level[1].pickup.ToString("0.00");
            updateGraph();
        }

        private void TbPickupL3_Validated(object sender, EventArgs e)
        {
            conf51.level[2].setPickup(TbPickupL3.Text);
            TbPickupL3.Text = conf51.level[2].pickup.ToString("0.00");
            updateGraph();
        }

        private void TbCurveL1_Validated(object sender, EventArgs e)
        {
            conf51.level[0].setCurve(CbCurveL1.Text);
            CbCurveL1.Text = conf51.level[0].Curve;
            updateGraph();
        }

        private void TbCurveL2_Validated(object sender, EventArgs e)
        {
            conf51.level[1].setCurve(CbCurveL2.Text);
            CbCurveL2.Text = conf51.level[1].Curve;
            updateGraph();
        }

        private void TbCurveL3_Validated(object sender, EventArgs e)
        {
            conf51.level[2].setCurve(CbCurveL3.Text);
            CbCurveL3.Text = conf51.level[2].Curve;
            updateGraph();
        }

        private void TbTdL1_Validated(object sender, EventArgs e)
        {
            conf51.level[0].setTimeDial(TbTdL1.Text);
            TbTdL1.Text = conf51.level[0].timeDial.ToString("0.00");
            updateGraph();
        }

        private void TbTdL2_Validated(object sender, EventArgs e)
        {
            conf51.level[1].setTimeDial(TbTdL2.Text);
            TbTdL2.Text = conf51.level[1].timeDial.ToString("0.00");
            updateGraph();
        }

        private void TbTdL3_Validated(object sender, EventArgs e)
        {
            conf51.level[2].setTimeDial(TbTdL3.Text);
            TbTdL3.Text = conf51.level[2].timeDial.ToString("0.00");
            updateGraph();
        }

        private void TbTdL1_Validated(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                conf51.level[0].setTimeDial(TbTdL1.Text);
                TbTdL1.Text = conf51.level[0].timeDial.ToString("0.00");
                updateGraph();
            }
        }

        private void TbTdL2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                conf51.level[1].setTimeDial(TbTdL2.Text);
                TbTdL2.Text = conf51.level[1].timeDial.ToString("0.00");
                updateGraph();
            }
        }

        private void TbTdL3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                conf51.level[2].setTimeDial(TbTdL3.Text);
                TbTdL3.Text = conf51.level[2].timeDial.ToString("0.00");
                updateGraph();
            }
        }

        private void TbPickupL1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                conf51.level[0].setTimeDial(TbPickupL1.Text);
                TbPickupL1.Text = conf51.level[0].timeDial.ToString("0.00");
                updateGraph();
            }
        }

        private void TbPickupL2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                conf51.level[1].setTimeDial(TbPickupL2.Text);
                TbPickupL2.Text = conf51.level[1].timeDial.ToString("0.00");
                updateGraph();
            }
        }

        private void TbPickupL3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                conf51.level[2].setTimeDial(TbPickupL3.Text);
                TbPickupL3.Text = conf51.level[2].timeDial.ToString("0.00");
                updateGraph();
            }
        }

        void updateGraph()
        {
            List<DataPoint> points = new List<DataPoint>();
            List<List<DataPoint>> curveDataPointsList = new List<List<DataPoint>>();
            double x0 = 0;
            //for (int i = 2; i >= 0; i--)
            //{
            //    if (conf51.level[i].pickup > x0)
            //        x0 = conf51.level[i].pickup;
            //}

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
                var colors = new List<OxyColor>() { OxyColors.Red, OxyColors.AliceBlue, OxyColors.DarkOrange, OxyColors.Red, OxyColors.AliceBlue, OxyColors.DarkOrange };
                var typeFlag = false;
                int j = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (conf50.level[i].enabled && CbGrap50.Checked)
                    {
                        var lineSeries = new LineSeries()
                        {
                            Color = colors[j],
                            Title = $"PTOC Level {i}",
                            TextColor = colors[j],
                        };
                        lineSeries.Points.AddRange(curveDataPointsList[j]);
                        lineSeries.LineLegendPosition = LineLegendPosition.Start;
                        plotModel.Series.Add(lineSeries);
                        plotModel.Legends.Add(new Legend()
                        {
                            LegendTitle = $"Curvas",
                            LegendPosition = LegendPosition.RightTop,
                            LegendTextColor = colors[j],
                            LegendTitleColor = OxyColors.LightCyan,
                        });
                        j++;
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    if (conf51.level[i].enabled && CbGrap51.Checked)
                    {
                        var lineSeries = new LineSeries()
                        {
                            Color = colors[j],
                            Title = $"PIOC Level {i}",
                            TextColor = colors[j],
                        };
                        lineSeries.Points.AddRange(curveDataPointsList[j]);
                        lineSeries.LineLegendPosition = LineLegendPosition.Start;
                        plotModel.Series.Add(lineSeries);
                        plotModel.Legends.Add(new Legend()
                        {
                            LegendTitle = $"Curvas",
                            LegendPosition = LegendPosition.RightTop,
                            LegendTextColor = colors[j],
                            LegendTitleColor = OxyColors.LightCyan,
                        });
                        j++;
                    }
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

        private void rbCurveSplited_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }

        private string tbType = "1";
        private void TbMaxTime_Validated(object sender, EventArgs e)
        {
            TbMaxTime.Validated -= TbMaxTime_Validated;
            if (double.TryParse(TbMaxTime.Text, out double x))
                tbType = TbMaxTime.Text;
            else
                TbMaxTime.Text = tbType;
            TbMaxTime.Validated -= TbMaxTime_Validated;
            updateGraph();
        }
    }
}
