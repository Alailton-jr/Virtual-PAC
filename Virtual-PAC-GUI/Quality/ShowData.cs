using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quality
{
    public partial class ShowData : Form
    {
        private List<List<double>> data;
        private List<CheckBox> checkBoxes;


        public ShowData()
        {
            InitializeComponent();
            checkBoxes = new List<CheckBox>()
            {
                CbxCn1,
                CbxCn2,
                CbxCn3,
                CbxCn4,
                CbxCn5,
                CbxCn6,
                CbxCn7,
                CbxCn8,
            };
        }

        public void OpenExt(List<List<double>> _data, string svID, double duration)
        {
            data = _data;
            updateGraph();
            TbDuration.Text = duration.ToString("0.00") + " s";
            TbSvID.Text = svID;
            this.Show();
        }

        // Move Window
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void updateGraph()
        {
            // data - List<List<double>>
            // pltData - PlotView

            var model = new PlotModel {
                Title = "Captura SV",
                TitleColor = OxyColor.FromRgb(255, 255, 255),
            };

            for (int i = 0; i < data.Count; i++)
            {
                if (!checkBoxes[i].Checked) continue;
                var lineSeries = new LineSeries();
                lineSeries.Title = $"Canal {i + 1}";
                for (int j = 0; j < data[i].Count; j++)
                {
                    lineSeries.Points.Add(new DataPoint(j, data[i][j]));
                }
                model.Series.Add(lineSeries);
            }
            // Customize plot appearance
            model.DefaultColors = OxyPalettes.Jet(data.Count).Colors;

            // Customize plot axes
            model.Axes.Add(new OxyPlot.Axes.LinearAxis {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                Title = "N Sample",
                TitleColor = OxyColor.FromRgb(255, 255, 255),
                TextColor = OxyColor.FromRgb(255, 255, 255),
                MajorGridlineStyle = LineStyle.Solid,
            });
            model.Axes.Add(new OxyPlot.Axes.LinearAxis {
                Position = OxyPlot.Axes.AxisPosition.Left,
                Title = "Valor",
                MajorGridlineStyle = LineStyle.Solid,
                TitleColor = OxyColor.FromRgb(255, 255, 255),
                TextColor = OxyColor.FromRgb(255, 255, 255),
            });

            PltData.Model = model;
        }

        private void CbxCn1_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
