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
using OxyPlot.Axes;
using OxyPlot.WindowsForms;
using static Ied.MainForm;
using static Ied.PdisConfig;
using static System.Net.Mime.MediaTypeNames;
using OxyPlot.Legends;
using OxyPlot.Annotations;

namespace Ied
{
    public partial class PdisForm : Form
    {
        public PdisForm()
        {
            InitializeComponent();
        }

        private PdisConfig config;

        private TextBox[] ajuste;
        private TextBox[] angle;
        private TextBox[] delay;
        private CheckBox[] enabled;
        private ComboBox[] type;

        private void PdisForm_Load(object sender, EventArgs e)
        {
            if (main.pdis == null)
            {
                main.pdis = new PdisConfig();
            }
            config = main.pdis;

            ajuste = new TextBox[] { TbAjusteZ1, TbAjusteZ2, TbAjusteZ3 };
            angle = new TextBox[] { TbAngleZ1, TbAngleZ2, TbAngleZ3 };
            delay = new TextBox[] { TbDelayZ1, TbDelayZ2, TbDelayZ3 };
            type = new ComboBox[] { CbTypeZ1, CbTypeZ2, CbTypeZ3 };
            enabled = new CheckBox[] { CbZoneEnabled1, CbZoneEnabled2, CbZoneEnabled3 };
            updateData();
        }

        private void updateData()
        {
            for (int i = 0; i < 3; i++)
            {
                ajuste[i].Text = config.zone[i].ajuste.ToString();
                angle[i].Text = config.zone[i].angle.ToString();
                delay[i].Text = config.zone[i].timeDelay.ToString();
                type[i].Text = config.zone[i].type.ToString().Replace("ancia", "ância");
                enabled[i].Checked = config.zone[i].enabled;
                angle[i].Enabled = type[i].Text == "Admitância";

                ajuste[i].Enabled = enabled[i].Checked;
                angle[i].Enabled = enabled[i].Checked;
                delay[i].Enabled = enabled[i].Checked;
                type[i].Enabled = enabled[i].Checked;

            }
        }

        private void TbAjusteZ1_Validated(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                var local = (TextBox)sender;
                if (ajuste.Contains(local))
                {
                    int index = Array.IndexOf(ajuste, local);
                    config.zone[index].ajuste = local.Text;
                    local.Text = config.zone[index].ajuste.ToString();
                }
                else if (angle.Contains(local))
                {
                    int index = Array.IndexOf(angle, local);
                    config.zone[index].angle = local.Text;
                    local.Text = config.zone[index].angle.ToString();
                }
                else if (delay.Contains(local))
                {
                    int index = Array.IndexOf(delay, local);
                    config.zone[index].timeDelay = local.Text;
                    local.Text = config.zone[index].timeDelay.ToString();
                }
            }
            else if (sender is ComboBox)
            {
                var local = (ComboBox)sender;
                if (type.Contains(local))
                {
                    int index = Array.IndexOf(type, local);
                    config.zone[index].type = Enum.Parse<PdisType>(local.Text.Replace("ância", "ancia"));
                    local.Text = config.zone[index].type.ToString().Replace("ancia", "ância");
                    angle[index].Enabled = type[index].Text == "Admitância";
                }
            }
            else if (sender is CheckBox)
            {
                var local = (CheckBox)sender;
                if (enabled.Contains(local))
                {
                    int index = Array.IndexOf(enabled, local);
                    config.zone[index].enabled = local.Checked;
                    local.Checked = config.zone[index].enabled;

                    ajuste[index].Enabled = enabled[index].Checked;
                    angle[index].Enabled = enabled[index].Checked;
                    delay[index].Enabled = enabled[index].Checked;
                    type[index].Enabled = enabled[index].Checked;
                }
            }
            updateGraph();
        }

        private void updateGraph()
        {

            List<DataPoint>[] curves = new List<DataPoint>[] { new List<DataPoint>(), new List<DataPoint>(), new List<DataPoint>() };


            var plotModel = new PlotModel
            {
                Title = "Diagrama X/R",
                TitleColor = OxyColors.Lavender,
            };
            var colors = new List<OxyColor>() { OxyColors.Red, OxyColors.AliceBlue, OxyColors.DarkOrange, OxyColors.Red, OxyColors.AliceBlue, OxyColors.DarkOrange };
            Zones zone;
            double x, y, theta, xMax = -1, xMin = -1.2321, yMin = -1.2321, yMax = -1;
            for (int i = 0; i < 3; i++)
            {
                zone = config.zone[i];
                if (!zone.enabled) continue;
                curves[i].Clear();
                switch (zone.type)
                {
                    case PdisType.Impedancia:

                        for (theta = 0; theta <= 2 * Math.PI; theta += 0.01)
                        {
                            x = (double)zone.ajuste * Math.Cos(theta);
                            y = (double)zone.ajuste * Math.Sin(theta);
                            curves[i].Add(new DataPoint(x, y));

                            if (x > xMax || xMax == -1) xMax = x;
                            if (x < xMin || xMin == -1.2321) xMin = x;
                            if (y > yMax || yMax == -1) yMax = y;
                            if (y < yMin || yMin == -1.2321) yMin = y;
                        }
                        break;
                    case PdisType.Admitancia:
                        for (theta = 0; theta <= 2 * Math.PI; theta += 0.01)
                        {
                            x = (double)zone.ajuste * (Math.Cos(theta + ((double)zone.angle * Math.PI / 180)) + 0.707);
                            y = (double)zone.ajuste * (Math.Sin(theta + ((double)zone.angle * Math.PI / 180)) + 0.707);
                            curves[i].Add(new DataPoint(x, y));
                            if (x > xMax || xMax == -1) xMax = x;
                            if (x < xMin || xMin == -1.2321) xMin = x;
                            if (y > yMax || yMax == -1) yMax = y;
                            if (y < yMin || yMin == -1.2321) yMin = y;
                        }
                        break;

                    case PdisType.Reatancia:
                        var horizontalLine = new LineAnnotation
                        {
                            Type = LineAnnotationType.Horizontal,
                            Color = colors[i],
                            LineStyle = LineStyle.Solid,
                            Y = (double)zone.ajuste,
                            Text = $"Zone {i + 1}",
                            TextPosition = new DataPoint(0, (double)zone.ajuste),
                            TextColor = colors[i]
                        };
                        plotModel.Annotations.Add(horizontalLine);
                        break;
                }
                //plotModel.Legends.Add(new Legend()
                //{
                //    LegendTitle = $"Zones",
                //    LegendPosition = LegendPosition.RightTop,
                //    LegendTextColor = colors[i],
                //    LegendTitleColor = OxyColors.LightCyan,
                //});

                if (curves[i].Count == 0) continue;
                var lineSeries = new LineSeries()
                {
                    Color = colors[i],
                    Title = $"Zones {i + 1}",
                    TextColor = colors[i],
                };
                lineSeries.Points.AddRange(curves[i]);
                lineSeries.LineLegendPosition = LineLegendPosition.Start;
                plotModel.Series.Add(lineSeries);
            }

            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "R",
                AbsoluteMinimum = xMin,
                AbsoluteMaximum = xMax,
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
                Title = "X",
                Minimum = yMin,
                Maximum = yMax,
                AbsoluteMinimum = yMin,
                AbsoluteMaximum = yMax,
                AxislineColor = OxyColors.Lavender,
                TextColor = OxyColors.Lavender,
                TitleColor = OxyColors.Lavender,
                ExtraGridlineColor = OxyColors.Lavender,
                MajorGridlineColor = OxyColor.FromRgb(84, 98, 110),
                MinorTicklineColor = OxyColors.Lavender,
                TicklineColor = OxyColors.Lavender,
                MajorGridlineStyle = LineStyle.Dot,
            };

            var xAxisAnnotation = new LineAnnotation
            {
                Type = LineAnnotationType.Horizontal,
                Color = OxyColors.Black,
                Y = 0,
            };
            var yAxisAnnotation = new LineAnnotation
            {
                Type = LineAnnotationType.Vertical,
                Color = OxyColors.Black,
                X = 0,
            };
            plotModel.Annotations.Add(xAxisAnnotation);
            plotModel.Annotations.Add(yAxisAnnotation);
            plotModel.Axes.Add(yAxis);

            plotModel.PlotAreaBorderColor = OxyColors.Lavender;
            PltPlot.Model = null;
            PltPlot.Model = plotModel;

        }

        private void rbCurveSplited_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }


    }

}
