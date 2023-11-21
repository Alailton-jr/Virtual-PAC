using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ied
{
    public partial class PdirForm : Form
    {
        public PdirForm()
        {
            InitializeComponent();
        }

        PdirConfig config;

        private void PdirForm_Load(object sender, EventArgs e)
        {
            if (MainForm.main.pdir == null) MainForm.main.pdir = new PdirConfig();
            config = MainForm.main.pdir;
            updateValues();
        }

        private void updateValues()
        {
            if (config != null)
            {
                TbPickup.Text = ((double)config.pickup).ToString("0.00");
                TbRTorq.Text = ((double)config.angle).ToString("0.00");
                CbPolarization.Text = $"{(int)config.polarity}°";
            }
        }
        private void validateValues()
        {
            config.pickup = TbPickup.Text;
            config.angle = TbRTorq.Text;
            config.polarity = CbPolarization.Text;

            updateValues();
        }

        //private void updateGraph()
        //{
        //    var lineSeries = new LineSeries();
        //    double rTorq = config._angle * Math.PI / 180;

        //    var plotModel = new PlotModel();

        //    // Define the range and step for X and K
        //    double xStep = 0.1;
        //    double kStep = 0.1;

        //    for (double x = 0; x <= 2 * Math.PI; x += xStep)
        //    {
        //        for (double k = 0; k <= 2; k += kStep)
        //        {
        //            double y = k * Math.Cos(0 - x); // Replace 0 with your chosen value for r

        //            if (y > 10)
        //            {
        //                var scatterSeries = new ScatterSeries();
        //                scatterSeries.MarkerType = MarkerType.Circle;
        //                scatterSeries.MarkerSize = 3;
        //                scatterSeries.Points.Add(new ScatterPoint(x, k));
        //                plotModel.Series.Add(scatterSeries);
        //            }
        //        }
        //    }

        //    // Customize the plot
        //    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "K" });
        //    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "X" });


        //    var plotModel = new PlotModel();
        //    plotModel.Series.Add(heatMapSeries);
        //    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "K" });
        //    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "X" });
        //    PltPlot.Model = plotModel;

        //}
    }
}
