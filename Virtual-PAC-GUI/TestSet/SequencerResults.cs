using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static TestSet.MainForm;

namespace TestSet
{
    public partial class SequencerResults : Form
    {
        List<SequenceConfig> setup = MainForm.main.sequencesConfig;
        TestRun test = MainForm.main.testRun;

        public SequencerResults()
        {
            InitializeComponent();
        }

        public void ExternOpenFrom()
        {
            MainForm.main.testRun = new TestRun()
            {
                currentTime = -1,
                delay = -1,
                endedTime = -1,
                startedTime = -1,
                supposedTripTime = -1,
                tripTime = -1
            };
            test = MainForm.main.testRun;
            this.ShowDialog();

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

        void UpdateGraphBinary()
        {
            if (setup.Count == 0)
                return;
            if (test.currentTime <= 0)
                return;
            List<List<DataPoint>> points = new List<List<DataPoint>>();

            for (int i = 0; i < test.input.Count; i++)
            {
                var _points = new List<DataPoint>();
                for (double x = 0; x < test.currentTime; x += 1 / 100)
                {

                }
                points.Add(_points);
            }

        }

        void UpdateGraph()
        {
            if (setup.Count == 0)
                return;
            if (test.currentTime <= 0)
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
                        if (x > test.currentTime)
                            break;
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
                    if (x > test.currentTime)
                        break;
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
                        if (x > test.currentTime)
                            break;

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
                    if (x > test.currentTime)
                        break;
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


            PltViewValues.Model = plotModel;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            //MainForm.main.startSequencer();
            main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);
            string res = main.serverCon.SendData("startSequencer", "All");
            if (res == null) MessageBox.Show("It Wasn't possible to connected to vMU!");

            test.input = new List<List<double>>();
            TimeGraphUpdate.Start();
        }
        private void BtnStop_Click(object sender, EventArgs e)
        {
            TimeGraphUpdate.Stop();
        }
        private void TimeGraphUpdate_Tick(object sender, EventArgs e)
        {
            if (!MainForm.main.connectionFlag)
            {
                TimeGraphUpdate.Stop();
                return;
            }
            main.serverCon.changeConProperties(main.communicationConfig.ip, main.communicationConfig.port);
            string res = main.serverCon.SendData("getSequencerResults", "All");
            if (res == null) MessageBox.Show("It Wasn't possible to connected to vMU!");
            else
            {  
            }



            UpdateGraph();

            RichTExtBoxDebug.Text = 
                "Trip Time:" + test.tripTime.ToString() + "\n" +
                "Current Time:" + test.currentTime.ToString() + "\n" +
                "supposedTripTime:" + test.supposedTripTime.ToString() + "\n";
        }
    }
}
